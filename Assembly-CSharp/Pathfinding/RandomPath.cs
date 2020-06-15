using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020005AF RID: 1455
	public class RandomPath : ABPath
	{
		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x0600275C RID: 10076 RVA: 0x00022944 File Offset: 0x00020B44
		internal override bool FloodingPath
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x0600275D RID: 10077 RVA: 0x0002D199 File Offset: 0x0002B399
		protected override bool hasEndPoint
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600275E RID: 10078 RVA: 0x001B1404 File Offset: 0x001AF604
		protected override void Reset()
		{
			base.Reset();
			this.searchLength = 5000;
			this.spread = 5000;
			this.aimStrength = 0f;
			this.chosenNodeR = null;
			this.maxGScoreNodeR = null;
			this.maxGScore = 0;
			this.aim = Vector3.zero;
			this.nodesEvaluatedRep = 0;
		}

		// Token: 0x0600275F RID: 10079 RVA: 0x001B145F File Offset: 0x001AF65F
		public RandomPath()
		{
		}

		// Token: 0x06002760 RID: 10080 RVA: 0x001B147D File Offset: 0x001AF67D
		[Obsolete("This constructor is obsolete. Please use the pooling API and the Construct methods")]
		public RandomPath(Vector3 start, int length, OnPathDelegate callback = null)
		{
			throw new Exception("This constructor is obsolete. Please use the pooling API and the Setup methods");
		}

		// Token: 0x06002761 RID: 10081 RVA: 0x001B14A5 File Offset: 0x001AF6A5
		public static RandomPath Construct(Vector3 start, int length, OnPathDelegate callback = null)
		{
			RandomPath path = PathPool.GetPath<RandomPath>();
			path.Setup(start, length, callback);
			return path;
		}

		// Token: 0x06002762 RID: 10082 RVA: 0x001B14B8 File Offset: 0x001AF6B8
		protected RandomPath Setup(Vector3 start, int length, OnPathDelegate callback)
		{
			this.callback = callback;
			this.searchLength = length;
			this.originalStartPoint = start;
			this.originalEndPoint = Vector3.zero;
			this.startPoint = start;
			this.endPoint = Vector3.zero;
			this.startIntPoint = (Int3)start;
			return this;
		}

		// Token: 0x06002763 RID: 10083 RVA: 0x001B1504 File Offset: 0x001AF704
		protected override void ReturnPath()
		{
			if (this.path != null && this.path.Count > 0)
			{
				this.endNode = this.path[this.path.Count - 1];
				this.endPoint = (Vector3)this.endNode.position;
				this.originalEndPoint = this.endPoint;
				this.hTarget = this.endNode.position;
			}
			if (this.callback != null)
			{
				this.callback(this);
			}
		}

		// Token: 0x06002764 RID: 10084 RVA: 0x001B158C File Offset: 0x001AF78C
		protected override void Prepare()
		{
			this.nnConstraint.tags = this.enabledTags;
			NNInfo nearest = AstarPath.active.GetNearest(this.startPoint, this.nnConstraint);
			this.startPoint = nearest.position;
			this.endPoint = this.startPoint;
			this.startIntPoint = (Int3)this.startPoint;
			this.hTarget = (Int3)this.aim;
			this.startNode = nearest.node;
			this.endNode = this.startNode;
			if (this.startNode == null || this.endNode == null)
			{
				base.FailWithError("Couldn't find close nodes to the start point");
				return;
			}
			if (!base.CanTraverse(this.startNode))
			{
				base.FailWithError("The node closest to the start point could not be traversed");
				return;
			}
			this.heuristicScale = this.aimStrength;
		}

		// Token: 0x06002765 RID: 10085 RVA: 0x001B1658 File Offset: 0x001AF858
		protected override void Initialize()
		{
			PathNode pathNode = this.pathHandler.GetPathNode(this.startNode);
			pathNode.node = this.startNode;
			if (this.searchLength + this.spread <= 0)
			{
				base.CompleteState = PathCompleteState.Complete;
				this.Trace(pathNode);
				return;
			}
			pathNode.pathID = base.pathID;
			pathNode.parent = null;
			pathNode.cost = 0U;
			pathNode.G = base.GetTraversalCost(this.startNode);
			pathNode.H = base.CalculateHScore(this.startNode);
			this.startNode.Open(this, pathNode, this.pathHandler);
			this.searchedNodes++;
			if (this.pathHandler.heap.isEmpty)
			{
				base.FailWithError("No open points, the start node didn't open any nodes");
				return;
			}
			this.currentR = this.pathHandler.heap.Remove();
		}

		// Token: 0x06002766 RID: 10086 RVA: 0x001B1738 File Offset: 0x001AF938
		protected override void CalculateStep(long targetTick)
		{
			int num = 0;
			while (base.CompleteState == PathCompleteState.NotCalculated)
			{
				this.searchedNodes++;
				if ((ulong)this.currentR.G >= (ulong)((long)this.searchLength))
				{
					if ((ulong)this.currentR.G > (ulong)((long)(this.searchLength + this.spread)))
					{
						if (this.chosenNodeR == null)
						{
							this.chosenNodeR = this.currentR;
						}
						base.CompleteState = PathCompleteState.Complete;
						break;
					}
					this.nodesEvaluatedRep++;
					if (this.rnd.NextDouble() <= (double)(1f / (float)this.nodesEvaluatedRep))
					{
						this.chosenNodeR = this.currentR;
					}
				}
				else if ((ulong)this.currentR.G > (ulong)((long)this.maxGScore))
				{
					this.maxGScore = (int)this.currentR.G;
					this.maxGScoreNodeR = this.currentR;
				}
				this.currentR.node.Open(this, this.currentR, this.pathHandler);
				if (this.pathHandler.heap.isEmpty)
				{
					if (this.chosenNodeR != null)
					{
						base.CompleteState = PathCompleteState.Complete;
						break;
					}
					if (this.maxGScoreNodeR != null)
					{
						this.chosenNodeR = this.maxGScoreNodeR;
						base.CompleteState = PathCompleteState.Complete;
						break;
					}
					base.FailWithError("Not a single node found to search");
					break;
				}
				else
				{
					this.currentR = this.pathHandler.heap.Remove();
					if (num > 500)
					{
						if (DateTime.UtcNow.Ticks >= targetTick)
						{
							return;
						}
						num = 0;
						if (this.searchedNodes > 1000000)
						{
							throw new Exception("Probable infinite loop. Over 1,000,000 nodes searched");
						}
					}
					num++;
				}
			}
			if (base.CompleteState == PathCompleteState.Complete)
			{
				this.Trace(this.chosenNodeR);
			}
		}

		// Token: 0x0400428B RID: 17035
		public int searchLength;

		// Token: 0x0400428C RID: 17036
		public int spread = 5000;

		// Token: 0x0400428D RID: 17037
		public float aimStrength;

		// Token: 0x0400428E RID: 17038
		private PathNode chosenNodeR;

		// Token: 0x0400428F RID: 17039
		private PathNode maxGScoreNodeR;

		// Token: 0x04004290 RID: 17040
		private int maxGScore;

		// Token: 0x04004291 RID: 17041
		public Vector3 aim;

		// Token: 0x04004292 RID: 17042
		private int nodesEvaluatedRep;

		// Token: 0x04004293 RID: 17043
		private readonly System.Random rnd = new System.Random();
	}
}
