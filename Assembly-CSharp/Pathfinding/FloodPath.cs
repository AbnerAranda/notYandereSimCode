using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020005AB RID: 1451
	public class FloodPath : Path
	{
		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x06002731 RID: 10033 RVA: 0x00022944 File Offset: 0x00020B44
		internal override bool FloodingPath
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002732 RID: 10034 RVA: 0x001AFFE5 File Offset: 0x001AE1E5
		public bool HasPathTo(GraphNode node)
		{
			return this.parents != null && this.parents.ContainsKey(node);
		}

		// Token: 0x06002733 RID: 10035 RVA: 0x001AFFFD File Offset: 0x001AE1FD
		public GraphNode GetParent(GraphNode node)
		{
			return this.parents[node];
		}

		// Token: 0x06002735 RID: 10037 RVA: 0x001B001A File Offset: 0x001AE21A
		public static FloodPath Construct(Vector3 start, OnPathDelegate callback = null)
		{
			FloodPath path = PathPool.GetPath<FloodPath>();
			path.Setup(start, callback);
			return path;
		}

		// Token: 0x06002736 RID: 10038 RVA: 0x001B0029 File Offset: 0x001AE229
		public static FloodPath Construct(GraphNode start, OnPathDelegate callback = null)
		{
			if (start == null)
			{
				throw new ArgumentNullException("start");
			}
			FloodPath path = PathPool.GetPath<FloodPath>();
			path.Setup(start, callback);
			return path;
		}

		// Token: 0x06002737 RID: 10039 RVA: 0x001B0046 File Offset: 0x001AE246
		protected void Setup(Vector3 start, OnPathDelegate callback)
		{
			this.callback = callback;
			this.originalStartPoint = start;
			this.startPoint = start;
			this.heuristic = Heuristic.None;
		}

		// Token: 0x06002738 RID: 10040 RVA: 0x001B0064 File Offset: 0x001AE264
		protected void Setup(GraphNode start, OnPathDelegate callback)
		{
			this.callback = callback;
			this.originalStartPoint = (Vector3)start.position;
			this.startNode = start;
			this.startPoint = (Vector3)start.position;
			this.heuristic = Heuristic.None;
		}

		// Token: 0x06002739 RID: 10041 RVA: 0x001B009D File Offset: 0x001AE29D
		protected override void Reset()
		{
			base.Reset();
			this.originalStartPoint = Vector3.zero;
			this.startPoint = Vector3.zero;
			this.startNode = null;
			this.parents = new Dictionary<GraphNode, GraphNode>();
			this.saveParents = true;
		}

		// Token: 0x0600273A RID: 10042 RVA: 0x001B00D4 File Offset: 0x001AE2D4
		protected override void Prepare()
		{
			if (this.startNode == null)
			{
				this.nnConstraint.tags = this.enabledTags;
				NNInfo nearest = AstarPath.active.GetNearest(this.originalStartPoint, this.nnConstraint);
				this.startPoint = nearest.position;
				this.startNode = nearest.node;
			}
			else
			{
				this.startPoint = (Vector3)this.startNode.position;
			}
			if (this.startNode == null)
			{
				base.FailWithError("Couldn't find a close node to the start point");
				return;
			}
			if (!base.CanTraverse(this.startNode))
			{
				base.FailWithError("The node closest to the start point could not be traversed");
				return;
			}
		}

		// Token: 0x0600273B RID: 10043 RVA: 0x001B0170 File Offset: 0x001AE370
		protected override void Initialize()
		{
			PathNode pathNode = this.pathHandler.GetPathNode(this.startNode);
			pathNode.node = this.startNode;
			pathNode.pathID = this.pathHandler.PathID;
			pathNode.parent = null;
			pathNode.cost = 0U;
			pathNode.G = base.GetTraversalCost(this.startNode);
			pathNode.H = base.CalculateHScore(this.startNode);
			this.parents[this.startNode] = null;
			this.startNode.Open(this, pathNode, this.pathHandler);
			this.searchedNodes++;
			if (this.pathHandler.heap.isEmpty)
			{
				base.CompleteState = PathCompleteState.Complete;
			}
			this.currentR = this.pathHandler.heap.Remove();
		}

		// Token: 0x0600273C RID: 10044 RVA: 0x001B0240 File Offset: 0x001AE440
		protected override void CalculateStep(long targetTick)
		{
			int num = 0;
			while (base.CompleteState == PathCompleteState.NotCalculated)
			{
				this.searchedNodes++;
				this.currentR.node.Open(this, this.currentR, this.pathHandler);
				if (this.saveParents)
				{
					this.parents[this.currentR.node] = this.currentR.parent.node;
				}
				if (this.pathHandler.heap.isEmpty)
				{
					base.CompleteState = PathCompleteState.Complete;
					return;
				}
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

		// Token: 0x04004277 RID: 17015
		public Vector3 originalStartPoint;

		// Token: 0x04004278 RID: 17016
		public Vector3 startPoint;

		// Token: 0x04004279 RID: 17017
		public GraphNode startNode;

		// Token: 0x0400427A RID: 17018
		public bool saveParents = true;

		// Token: 0x0400427B RID: 17019
		protected Dictionary<GraphNode, GraphNode> parents;
	}
}
