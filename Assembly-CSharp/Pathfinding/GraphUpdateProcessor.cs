using System;
using System.Collections.Generic;
using System.Threading;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200053C RID: 1340
	internal class GraphUpdateProcessor
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060022DE RID: 8926 RVA: 0x00196224 File Offset: 0x00194424
		// (remove) Token: 0x060022DF RID: 8927 RVA: 0x0019625C File Offset: 0x0019445C
		public event Action OnGraphsUpdated;

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x060022E0 RID: 8928 RVA: 0x00196291 File Offset: 0x00194491
		public bool IsAnyGraphUpdateQueued
		{
			get
			{
				return this.graphUpdateQueue.Count > 0;
			}
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x060022E1 RID: 8929 RVA: 0x001962A1 File Offset: 0x001944A1
		public bool IsAnyGraphUpdateInProgress
		{
			get
			{
				return this.anyGraphUpdateInProgress;
			}
		}

		// Token: 0x060022E2 RID: 8930 RVA: 0x001962AC File Offset: 0x001944AC
		public GraphUpdateProcessor(AstarPath astar)
		{
			this.astar = astar;
		}

		// Token: 0x060022E3 RID: 8931 RVA: 0x00196316 File Offset: 0x00194516
		public AstarWorkItem GetWorkItem()
		{
			return new AstarWorkItem(new Action(this.QueueGraphUpdatesInternal), new Func<bool, bool>(this.ProcessGraphUpdates));
		}

		// Token: 0x060022E4 RID: 8932 RVA: 0x00196338 File Offset: 0x00194538
		public void EnableMultithreading()
		{
			if (this.graphUpdateThread == null || !this.graphUpdateThread.IsAlive)
			{
				this.graphUpdateThread = new Thread(new ThreadStart(this.ProcessGraphUpdatesAsync));
				this.graphUpdateThread.IsBackground = true;
				this.graphUpdateThread.Priority = System.Threading.ThreadPriority.Lowest;
				this.graphUpdateThread.Start();
			}
		}

		// Token: 0x060022E5 RID: 8933 RVA: 0x00196394 File Offset: 0x00194594
		public void DisableMultithreading()
		{
			if (this.graphUpdateThread != null && this.graphUpdateThread.IsAlive)
			{
				this.exitAsyncThread.Set();
				if (!this.graphUpdateThread.Join(5000))
				{
					Debug.LogError("Graph update thread did not exit in 5 seconds");
				}
				this.graphUpdateThread = null;
			}
		}

		// Token: 0x060022E6 RID: 8934 RVA: 0x001963E5 File Offset: 0x001945E5
		public void AddToQueue(GraphUpdateObject ob)
		{
			this.graphUpdateQueue.Enqueue(ob);
		}

		// Token: 0x060022E7 RID: 8935 RVA: 0x001963F4 File Offset: 0x001945F4
		private void QueueGraphUpdatesInternal()
		{
			bool flag = false;
			while (this.graphUpdateQueue.Count > 0)
			{
				GraphUpdateObject graphUpdateObject = this.graphUpdateQueue.Dequeue();
				if (graphUpdateObject.requiresFloodFill)
				{
					flag = true;
				}
				foreach (object obj in this.astar.data.GetUpdateableGraphs())
				{
					IUpdatableGraph updatableGraph = (IUpdatableGraph)obj;
					NavGraph graph = updatableGraph as NavGraph;
					if (graphUpdateObject.nnConstraint == null || graphUpdateObject.nnConstraint.SuitableGraph(this.astar.data.GetGraphIndex(graph), graph))
					{
						GraphUpdateProcessor.GUOSingle item = default(GraphUpdateProcessor.GUOSingle);
						item.order = GraphUpdateProcessor.GraphUpdateOrder.GraphUpdate;
						item.obj = graphUpdateObject;
						item.graph = updatableGraph;
						this.graphUpdateQueueRegular.Enqueue(item);
					}
				}
			}
			if (flag)
			{
				GraphUpdateProcessor.GUOSingle item2 = default(GraphUpdateProcessor.GUOSingle);
				item2.order = GraphUpdateProcessor.GraphUpdateOrder.FloodFill;
				this.graphUpdateQueueRegular.Enqueue(item2);
			}
			GraphModifier.TriggerEvent(GraphModifier.EventType.PreUpdate);
			this.anyGraphUpdateInProgress = true;
		}

		// Token: 0x060022E8 RID: 8936 RVA: 0x0019650C File Offset: 0x0019470C
		private bool ProcessGraphUpdates(bool force)
		{
			if (force)
			{
				this.asyncGraphUpdatesComplete.WaitOne();
			}
			else if (!this.asyncGraphUpdatesComplete.WaitOne(0))
			{
				return false;
			}
			this.ProcessPostUpdates();
			if (!this.ProcessRegularUpdates(force))
			{
				return false;
			}
			GraphModifier.TriggerEvent(GraphModifier.EventType.PostUpdate);
			if (this.OnGraphsUpdated != null)
			{
				this.OnGraphsUpdated();
			}
			this.anyGraphUpdateInProgress = false;
			return true;
		}

		// Token: 0x060022E9 RID: 8937 RVA: 0x00196570 File Offset: 0x00194770
		private bool ProcessRegularUpdates(bool force)
		{
			while (this.graphUpdateQueueRegular.Count > 0)
			{
				GraphUpdateProcessor.GUOSingle guosingle = this.graphUpdateQueueRegular.Peek();
				GraphUpdateThreading graphUpdateThreading = (guosingle.order == GraphUpdateProcessor.GraphUpdateOrder.FloodFill) ? GraphUpdateThreading.SeparateThread : guosingle.graph.CanUpdateAsync(guosingle.obj);
				if (force || !Application.isPlaying || this.graphUpdateThread == null || !this.graphUpdateThread.IsAlive)
				{
					graphUpdateThreading &= (GraphUpdateThreading)(-2);
				}
				if ((graphUpdateThreading & GraphUpdateThreading.UnityInit) != GraphUpdateThreading.UnityThread)
				{
					if (this.StartAsyncUpdatesIfQueued())
					{
						return false;
					}
					guosingle.graph.UpdateAreaInit(guosingle.obj);
				}
				if ((graphUpdateThreading & GraphUpdateThreading.SeparateThread) != GraphUpdateThreading.UnityThread)
				{
					this.graphUpdateQueueRegular.Dequeue();
					this.graphUpdateQueueAsync.Enqueue(guosingle);
					if ((graphUpdateThreading & GraphUpdateThreading.UnityPost) != GraphUpdateThreading.UnityThread && this.StartAsyncUpdatesIfQueued())
					{
						return false;
					}
				}
				else
				{
					if (this.StartAsyncUpdatesIfQueued())
					{
						return false;
					}
					this.graphUpdateQueueRegular.Dequeue();
					if (guosingle.order == GraphUpdateProcessor.GraphUpdateOrder.FloodFill)
					{
						this.FloodFill();
					}
					else
					{
						try
						{
							guosingle.graph.UpdateArea(guosingle.obj);
						}
						catch (Exception arg)
						{
							Debug.LogError("Error while updating graphs\n" + arg);
						}
					}
					if ((graphUpdateThreading & GraphUpdateThreading.UnityPost) != GraphUpdateThreading.UnityThread)
					{
						guosingle.graph.UpdateAreaPost(guosingle.obj);
					}
				}
			}
			return !this.StartAsyncUpdatesIfQueued();
		}

		// Token: 0x060022EA RID: 8938 RVA: 0x001966AC File Offset: 0x001948AC
		private bool StartAsyncUpdatesIfQueued()
		{
			if (this.graphUpdateQueueAsync.Count > 0)
			{
				this.asyncGraphUpdatesComplete.Reset();
				this.graphUpdateAsyncEvent.Set();
				return true;
			}
			return false;
		}

		// Token: 0x060022EB RID: 8939 RVA: 0x001966D8 File Offset: 0x001948D8
		private void ProcessPostUpdates()
		{
			while (this.graphUpdateQueuePost.Count > 0)
			{
				GraphUpdateProcessor.GUOSingle guosingle = this.graphUpdateQueuePost.Dequeue();
				if ((guosingle.graph.CanUpdateAsync(guosingle.obj) & GraphUpdateThreading.UnityPost) != GraphUpdateThreading.UnityThread)
				{
					try
					{
						guosingle.graph.UpdateAreaPost(guosingle.obj);
					}
					catch (Exception arg)
					{
						Debug.LogError("Error while updating graphs (post step)\n" + arg);
					}
				}
			}
		}

		// Token: 0x060022EC RID: 8940 RVA: 0x0019674C File Offset: 0x0019494C
		private void ProcessGraphUpdatesAsync()
		{
			AutoResetEvent[] array = new AutoResetEvent[]
			{
				this.graphUpdateAsyncEvent,
				this.exitAsyncThread
			};
			for (;;)
			{
				WaitHandle[] waitHandles = array;
				if (WaitHandle.WaitAny(waitHandles) == 1)
				{
					break;
				}
				while (this.graphUpdateQueueAsync.Count > 0)
				{
					GraphUpdateProcessor.GUOSingle guosingle = this.graphUpdateQueueAsync.Dequeue();
					try
					{
						if (guosingle.order == GraphUpdateProcessor.GraphUpdateOrder.GraphUpdate)
						{
							guosingle.graph.UpdateArea(guosingle.obj);
							this.graphUpdateQueuePost.Enqueue(guosingle);
						}
						else
						{
							if (guosingle.order != GraphUpdateProcessor.GraphUpdateOrder.FloodFill)
							{
								throw new NotSupportedException(string.Concat(guosingle.order));
							}
							this.FloodFill();
						}
					}
					catch (Exception arg)
					{
						Debug.LogError("Exception while updating graphs:\n" + arg);
					}
				}
				this.asyncGraphUpdatesComplete.Set();
			}
			this.graphUpdateQueueAsync.Clear();
			this.asyncGraphUpdatesComplete.Set();
		}

		// Token: 0x060022ED RID: 8941 RVA: 0x00196838 File Offset: 0x00194A38
		public void FloodFill(GraphNode seed)
		{
			this.FloodFill(seed, this.lastUniqueAreaIndex + 1U);
			this.lastUniqueAreaIndex += 1U;
		}

		// Token: 0x060022EE RID: 8942 RVA: 0x00196858 File Offset: 0x00194A58
		public void FloodFill(GraphNode seed, uint area)
		{
			if (area > 131071U)
			{
				Debug.LogError("Too high area index - The maximum area index is " + 131071U);
				return;
			}
			if (area < 0U)
			{
				Debug.LogError("Too low area index - The minimum area index is 0");
				return;
			}
			Stack<GraphNode> stack = StackPool<GraphNode>.Claim();
			stack.Push(seed);
			seed.Area = area;
			while (stack.Count > 0)
			{
				stack.Pop().FloodFill(stack, area);
			}
			StackPool<GraphNode>.Release(stack);
		}

		// Token: 0x060022EF RID: 8943 RVA: 0x001968C8 File Offset: 0x00194AC8
		public void FloodFill()
		{
			NavGraph[] graphs = this.astar.graphs;
			if (graphs == null)
			{
				return;
			}
			foreach (NavGraph navGraph in graphs)
			{
				if (navGraph != null)
				{
					navGraph.GetNodes(delegate(GraphNode node)
					{
						node.Area = 0U;
					});
				}
			}
			this.lastUniqueAreaIndex = 0U;
			uint area = 0U;
			int forcedSmallAreas = 0;
			Stack<GraphNode> stack = StackPool<GraphNode>.Claim();
			Action<GraphNode> <>9__1;
			foreach (NavGraph navGraph2 in graphs)
			{
				if (navGraph2 != null)
				{
					NavGraph navGraph3 = navGraph2;
					Action<GraphNode> action;
					if ((action = <>9__1) == null)
					{
						action = (<>9__1 = delegate(GraphNode node)
						{
							if (node.Walkable && node.Area == 0U)
							{
								uint area = area;
								area += 1U;
								uint area2 = area;
								if (area > 131071U)
								{
									area = area;
									area -= 1U;
									area2 = area;
									int forcedSmallAreas;
									if (forcedSmallAreas == 0)
									{
										forcedSmallAreas = 1;
									}
									forcedSmallAreas = forcedSmallAreas;
									forcedSmallAreas++;
								}
								stack.Clear();
								stack.Push(node);
								int num = 1;
								node.Area = area2;
								while (stack.Count > 0)
								{
									num++;
									stack.Pop().FloodFill(stack, area2);
								}
							}
						});
					}
					navGraph3.GetNodes(action);
				}
			}
			this.lastUniqueAreaIndex = area;
			if (forcedSmallAreas > 0)
			{
				Debug.LogError(string.Concat(new object[]
				{
					forcedSmallAreas,
					" areas had to share IDs. This usually doesn't affect pathfinding in any significant way (you might get 'Searched whole area but could not find target' as a reason for path failure) however some path requests may take longer to calculate (specifically those that fail with the 'Searched whole area' error).The maximum number of areas is ",
					131071U,
					"."
				}));
			}
			StackPool<GraphNode>.Release(stack);
		}

		// Token: 0x04003FE2 RID: 16354
		private readonly AstarPath astar;

		// Token: 0x04003FE3 RID: 16355
		private Thread graphUpdateThread;

		// Token: 0x04003FE4 RID: 16356
		private bool anyGraphUpdateInProgress;

		// Token: 0x04003FE5 RID: 16357
		private readonly Queue<GraphUpdateObject> graphUpdateQueue = new Queue<GraphUpdateObject>();

		// Token: 0x04003FE6 RID: 16358
		private readonly Queue<GraphUpdateProcessor.GUOSingle> graphUpdateQueueAsync = new Queue<GraphUpdateProcessor.GUOSingle>();

		// Token: 0x04003FE7 RID: 16359
		private readonly Queue<GraphUpdateProcessor.GUOSingle> graphUpdateQueuePost = new Queue<GraphUpdateProcessor.GUOSingle>();

		// Token: 0x04003FE8 RID: 16360
		private readonly Queue<GraphUpdateProcessor.GUOSingle> graphUpdateQueueRegular = new Queue<GraphUpdateProcessor.GUOSingle>();

		// Token: 0x04003FE9 RID: 16361
		private readonly ManualResetEvent asyncGraphUpdatesComplete = new ManualResetEvent(true);

		// Token: 0x04003FEA RID: 16362
		private readonly AutoResetEvent graphUpdateAsyncEvent = new AutoResetEvent(false);

		// Token: 0x04003FEB RID: 16363
		private readonly AutoResetEvent exitAsyncThread = new AutoResetEvent(false);

		// Token: 0x04003FEC RID: 16364
		private uint lastUniqueAreaIndex;

		// Token: 0x02000733 RID: 1843
		private enum GraphUpdateOrder
		{
			// Token: 0x040049E1 RID: 18913
			GraphUpdate,
			// Token: 0x040049E2 RID: 18914
			FloodFill
		}

		// Token: 0x02000734 RID: 1844
		private struct GUOSingle
		{
			// Token: 0x040049E3 RID: 18915
			public GraphUpdateProcessor.GraphUpdateOrder order;

			// Token: 0x040049E4 RID: 18916
			public IUpdatableGraph graph;

			// Token: 0x040049E5 RID: 18917
			public GraphUpdateObject obj;
		}
	}
}
