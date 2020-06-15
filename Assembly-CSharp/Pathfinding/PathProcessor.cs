using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000546 RID: 1350
	public class PathProcessor
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06002365 RID: 9061 RVA: 0x00199094 File Offset: 0x00197294
		// (remove) Token: 0x06002366 RID: 9062 RVA: 0x001990CC File Offset: 0x001972CC
		public event Action<Path> OnPathPreSearch;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06002367 RID: 9063 RVA: 0x00199104 File Offset: 0x00197304
		// (remove) Token: 0x06002368 RID: 9064 RVA: 0x0019913C File Offset: 0x0019733C
		public event Action<Path> OnPathPostSearch;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06002369 RID: 9065 RVA: 0x00199174 File Offset: 0x00197374
		// (remove) Token: 0x0600236A RID: 9066 RVA: 0x001991AC File Offset: 0x001973AC
		public event Action OnQueueUnblocked;

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x0600236B RID: 9067 RVA: 0x001991E1 File Offset: 0x001973E1
		public int NumThreads
		{
			get
			{
				return this.pathHandlers.Length;
			}
		}

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x0600236C RID: 9068 RVA: 0x001991EB File Offset: 0x001973EB
		public bool IsUsingMultithreading
		{
			get
			{
				return this.threads != null;
			}
		}

		// Token: 0x0600236D RID: 9069 RVA: 0x001991F8 File Offset: 0x001973F8
		internal PathProcessor(AstarPath astar, PathReturnQueue returnQueue, int processors, bool multithreaded)
		{
			this.astar = astar;
			this.returnQueue = returnQueue;
			if (processors < 0)
			{
				throw new ArgumentOutOfRangeException("processors");
			}
			if (!multithreaded && processors != 1)
			{
				throw new Exception("Only a single non-multithreaded processor is allowed");
			}
			this.queue = new ThreadControlQueue(processors);
			this.pathHandlers = new PathHandler[processors];
			for (int i = 0; i < processors; i++)
			{
				this.pathHandlers[i] = new PathHandler(i, processors);
			}
			if (multithreaded)
			{
				this.threads = new Thread[processors];
				for (int j = 0; j < processors; j++)
				{
					PathHandler pathHandler = this.pathHandlers[j];
					this.threads[j] = new Thread(delegate()
					{
						this.CalculatePathsThreaded(pathHandler);
					});
					this.threads[j].Name = "Pathfinding Thread " + j;
					this.threads[j].IsBackground = true;
					this.threads[j].Start();
				}
				return;
			}
			this.threadCoroutine = this.CalculatePaths(this.pathHandlers[0]);
		}

		// Token: 0x0600236E RID: 9070 RVA: 0x0019932C File Offset: 0x0019752C
		private int Lock(bool block)
		{
			this.queue.Block();
			if (block)
			{
				while (!this.queue.AllReceiversBlocked)
				{
					if (this.IsUsingMultithreading)
					{
						Thread.Sleep(1);
					}
					else
					{
						this.TickNonMultithreaded();
					}
				}
			}
			this.nextLockID++;
			this.locks.Add(this.nextLockID);
			return this.nextLockID;
		}

		// Token: 0x0600236F RID: 9071 RVA: 0x00199394 File Offset: 0x00197594
		private void Unlock(int id)
		{
			if (!this.locks.Remove(id))
			{
				throw new ArgumentException("This lock has already been released");
			}
			if (this.locks.Count == 0)
			{
				if (this.OnQueueUnblocked != null)
				{
					this.OnQueueUnblocked();
				}
				this.queue.Unblock();
			}
		}

		// Token: 0x06002370 RID: 9072 RVA: 0x001993E5 File Offset: 0x001975E5
		public PathProcessor.GraphUpdateLock PausePathfinding(bool block)
		{
			return new PathProcessor.GraphUpdateLock(this, block);
		}

		// Token: 0x06002371 RID: 9073 RVA: 0x001993F0 File Offset: 0x001975F0
		public void TickNonMultithreaded()
		{
			if (this.threadCoroutine != null)
			{
				try
				{
					this.threadCoroutine.MoveNext();
				}
				catch (Exception ex)
				{
					this.threadCoroutine = null;
					if (!(ex is ThreadControlQueue.QueueTerminationException))
					{
						Debug.LogException(ex);
						Debug.LogError("Unhandled exception during pathfinding. Terminating.");
						this.queue.TerminateReceivers();
						try
						{
							this.queue.PopNoBlock(false);
						}
						catch
						{
						}
					}
				}
			}
		}

		// Token: 0x06002372 RID: 9074 RVA: 0x00199470 File Offset: 0x00197670
		public void JoinThreads()
		{
			if (this.threads != null)
			{
				for (int i = 0; i < this.threads.Length; i++)
				{
					if (!this.threads[i].Join(50))
					{
						Debug.LogError("Could not terminate pathfinding thread[" + i + "] in 50ms, trying Thread.Abort");
						this.threads[i].Abort();
					}
				}
			}
		}

		// Token: 0x06002373 RID: 9075 RVA: 0x001994D0 File Offset: 0x001976D0
		public void AbortThreads()
		{
			if (this.threads == null)
			{
				return;
			}
			for (int i = 0; i < this.threads.Length; i++)
			{
				if (this.threads[i] != null && this.threads[i].IsAlive)
				{
					this.threads[i].Abort();
				}
			}
		}

		// Token: 0x06002374 RID: 9076 RVA: 0x00199520 File Offset: 0x00197720
		public int GetNewNodeIndex()
		{
			if (this.nodeIndexPool.Count <= 0)
			{
				int num = this.nextNodeIndex;
				this.nextNodeIndex = num + 1;
				return num;
			}
			return this.nodeIndexPool.Pop();
		}

		// Token: 0x06002375 RID: 9077 RVA: 0x00199558 File Offset: 0x00197758
		public void InitializeNode(GraphNode node)
		{
			if (!this.queue.AllReceiversBlocked)
			{
				throw new Exception("Trying to initialize a node when it is not safe to initialize any nodes. Must be done during a graph update. See http://arongranberg.com/astar/docs/graph-updates.php#direct");
			}
			for (int i = 0; i < this.pathHandlers.Length; i++)
			{
				this.pathHandlers[i].InitializeNode(node);
			}
		}

		// Token: 0x06002376 RID: 9078 RVA: 0x001995A0 File Offset: 0x001977A0
		public void DestroyNode(GraphNode node)
		{
			if (node.NodeIndex == -1)
			{
				return;
			}
			this.nodeIndexPool.Push(node.NodeIndex);
			for (int i = 0; i < this.pathHandlers.Length; i++)
			{
				this.pathHandlers[i].DestroyNode(node);
			}
		}

		// Token: 0x06002377 RID: 9079 RVA: 0x001995EC File Offset: 0x001977EC
		private void CalculatePathsThreaded(PathHandler pathHandler)
		{
			try
			{
				long num = 100000L;
				long targetTick = DateTime.UtcNow.Ticks + num;
				for (;;)
				{
					Path path = this.queue.Pop();
					IPathInternals pathInternals = path;
					pathInternals.PrepareBase(pathHandler);
					pathInternals.AdvanceState(PathState.Processing);
					if (this.OnPathPreSearch != null)
					{
						this.OnPathPreSearch(path);
					}
					long ticks = DateTime.UtcNow.Ticks;
					pathInternals.Prepare();
					if (!path.IsDone())
					{
						this.astar.debugPathData = pathInternals.PathHandler;
						this.astar.debugPathID = path.pathID;
						pathInternals.Initialize();
						while (!path.IsDone())
						{
							pathInternals.CalculateStep(targetTick);
							targetTick = DateTime.UtcNow.Ticks + num;
							if (this.queue.IsTerminating)
							{
								path.FailWithError("AstarPath object destroyed");
							}
						}
						path.duration = (float)(DateTime.UtcNow.Ticks - ticks) * 0.0001f;
					}
					pathInternals.Cleanup();
					if (path.immediateCallback != null)
					{
						path.immediateCallback(path);
					}
					if (this.OnPathPostSearch != null)
					{
						this.OnPathPostSearch(path);
					}
					this.returnQueue.Enqueue(path);
					pathInternals.AdvanceState(PathState.ReturnQueue);
				}
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is ThreadControlQueue.QueueTerminationException)
				{
					if (this.astar.logPathResults == PathLog.Heavy)
					{
						Debug.LogWarning("Shutting down pathfinding thread #" + pathHandler.threadID);
					}
					return;
				}
				Debug.LogException(ex);
				Debug.LogError("Unhandled exception during pathfinding. Terminating.");
				this.queue.TerminateReceivers();
			}
			Debug.LogError("Error : This part should never be reached.");
			this.queue.ReceiverTerminated();
		}

		// Token: 0x06002378 RID: 9080 RVA: 0x001997BC File Offset: 0x001979BC
		private IEnumerator CalculatePaths(PathHandler pathHandler)
		{
			long maxTicks = (long)(this.astar.maxFrameTime * 10000f);
			long targetTick = DateTime.UtcNow.Ticks + maxTicks;
			for (;;)
			{
				Path p = null;
				bool blockedBefore = false;
				while (p == null)
				{
					try
					{
						p = this.queue.PopNoBlock(blockedBefore);
						blockedBefore |= (p == null);
					}
					catch (ThreadControlQueue.QueueTerminationException)
					{
						yield break;
					}
					if (p == null)
					{
						yield return null;
					}
				}
				IPathInternals ip = p;
				maxTicks = (long)(this.astar.maxFrameTime * 10000f);
				ip.PrepareBase(pathHandler);
				ip.AdvanceState(PathState.Processing);
				Action<Path> onPathPreSearch = this.OnPathPreSearch;
				if (onPathPreSearch != null)
				{
					onPathPreSearch(p);
				}
				long ticks = DateTime.UtcNow.Ticks;
				long totalTicks = 0L;
				ip.Prepare();
				if (!p.IsDone())
				{
					this.astar.debugPathData = ip.PathHandler;
					this.astar.debugPathID = p.pathID;
					ip.Initialize();
					while (!p.IsDone())
					{
						ip.CalculateStep(targetTick);
						if (p.IsDone())
						{
							break;
						}
						totalTicks += DateTime.UtcNow.Ticks - ticks;
						yield return null;
						ticks = DateTime.UtcNow.Ticks;
						if (this.queue.IsTerminating)
						{
							p.FailWithError("AstarPath object destroyed");
						}
						targetTick = DateTime.UtcNow.Ticks + maxTicks;
					}
					totalTicks += DateTime.UtcNow.Ticks - ticks;
					p.duration = (float)totalTicks * 0.0001f;
				}
				ip.Cleanup();
				OnPathDelegate immediateCallback = p.immediateCallback;
				if (immediateCallback != null)
				{
					immediateCallback(p);
				}
				Action<Path> onPathPostSearch = this.OnPathPostSearch;
				if (onPathPostSearch != null)
				{
					onPathPostSearch(p);
				}
				this.returnQueue.Enqueue(p);
				ip.AdvanceState(PathState.ReturnQueue);
				if (DateTime.UtcNow.Ticks > targetTick)
				{
					yield return null;
					targetTick = DateTime.UtcNow.Ticks + maxTicks;
				}
				p = null;
				ip = null;
			}
			yield break;
		}

		// Token: 0x0400401C RID: 16412
		internal readonly ThreadControlQueue queue;

		// Token: 0x0400401D RID: 16413
		private readonly AstarPath astar;

		// Token: 0x0400401E RID: 16414
		private readonly PathReturnQueue returnQueue;

		// Token: 0x0400401F RID: 16415
		private readonly PathHandler[] pathHandlers;

		// Token: 0x04004020 RID: 16416
		private readonly Thread[] threads;

		// Token: 0x04004021 RID: 16417
		private IEnumerator threadCoroutine;

		// Token: 0x04004022 RID: 16418
		private int nextNodeIndex = 1;

		// Token: 0x04004023 RID: 16419
		private readonly Stack<int> nodeIndexPool = new Stack<int>();

		// Token: 0x04004024 RID: 16420
		private readonly List<int> locks = new List<int>();

		// Token: 0x04004025 RID: 16421
		private int nextLockID;

		// Token: 0x02000739 RID: 1849
		public struct GraphUpdateLock
		{
			// Token: 0x06002D00 RID: 11520 RVA: 0x001CFFED File Offset: 0x001CE1ED
			public GraphUpdateLock(PathProcessor pathProcessor, bool block)
			{
				this.pathProcessor = pathProcessor;
				this.id = pathProcessor.Lock(block);
			}

			// Token: 0x17000679 RID: 1657
			// (get) Token: 0x06002D01 RID: 11521 RVA: 0x001D0003 File Offset: 0x001CE203
			public bool Held
			{
				get
				{
					return this.pathProcessor != null && this.pathProcessor.locks.Contains(this.id);
				}
			}

			// Token: 0x06002D02 RID: 11522 RVA: 0x001D0025 File Offset: 0x001CE225
			public void Release()
			{
				this.pathProcessor.Unlock(this.id);
			}

			// Token: 0x040049F2 RID: 18930
			private PathProcessor pathProcessor;

			// Token: 0x040049F3 RID: 18931
			private int id;
		}
	}
}
