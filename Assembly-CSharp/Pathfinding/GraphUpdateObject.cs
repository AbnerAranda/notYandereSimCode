using System;
using System.Collections.Generic;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200055D RID: 1373
	public class GraphUpdateObject
	{
		// Token: 0x06002452 RID: 9298 RVA: 0x0019BB78 File Offset: 0x00199D78
		public virtual void WillUpdateNode(GraphNode node)
		{
			if (this.trackChangedNodes && node != null)
			{
				if (this.changedNodes == null)
				{
					this.changedNodes = ListPool<GraphNode>.Claim();
					this.backupData = ListPool<uint>.Claim();
					this.backupPositionData = ListPool<Int3>.Claim();
				}
				this.changedNodes.Add(node);
				this.backupPositionData.Add(node.position);
				this.backupData.Add(node.Penalty);
				this.backupData.Add(node.Flags);
				GridNode gridNode = node as GridNode;
				if (gridNode != null)
				{
					this.backupData.Add((uint)gridNode.InternalGridFlags);
				}
			}
		}

		// Token: 0x06002453 RID: 9299 RVA: 0x0019BC1C File Offset: 0x00199E1C
		public virtual void RevertFromBackup()
		{
			if (!this.trackChangedNodes)
			{
				throw new InvalidOperationException("Changed nodes have not been tracked, cannot revert from backup. Please set trackChangedNodes to true before applying the update.");
			}
			if (this.changedNodes == null)
			{
				return;
			}
			int num = 0;
			for (int i = 0; i < this.changedNodes.Count; i++)
			{
				this.changedNodes[i].Penalty = this.backupData[num];
				num++;
				this.changedNodes[i].Flags = this.backupData[num];
				num++;
				GridNode gridNode = this.changedNodes[i] as GridNode;
				if (gridNode != null)
				{
					gridNode.InternalGridFlags = (ushort)this.backupData[num];
					num++;
				}
				this.changedNodes[i].position = this.backupPositionData[i];
			}
			ListPool<GraphNode>.Release(ref this.changedNodes);
			ListPool<uint>.Release(ref this.backupData);
			ListPool<Int3>.Release(ref this.backupPositionData);
		}

		// Token: 0x06002454 RID: 9300 RVA: 0x0019BD14 File Offset: 0x00199F14
		public virtual void Apply(GraphNode node)
		{
			if (this.shape == null || this.shape.Contains(node))
			{
				node.Penalty = (uint)((ulong)node.Penalty + (ulong)((long)this.addPenalty));
				if (this.modifyWalkability)
				{
					node.Walkable = this.setWalkability;
				}
				if (this.modifyTag)
				{
					node.Tag = (uint)this.setTag;
				}
			}
		}

		// Token: 0x06002455 RID: 9301 RVA: 0x0019BD75 File Offset: 0x00199F75
		public GraphUpdateObject()
		{
		}

		// Token: 0x06002456 RID: 9302 RVA: 0x0019BDA4 File Offset: 0x00199FA4
		public GraphUpdateObject(Bounds b)
		{
			this.bounds = b;
		}

		// Token: 0x040040A2 RID: 16546
		public Bounds bounds;

		// Token: 0x040040A3 RID: 16547
		public bool requiresFloodFill = true;

		// Token: 0x040040A4 RID: 16548
		public bool updatePhysics = true;

		// Token: 0x040040A5 RID: 16549
		public bool resetPenaltyOnPhysics = true;

		// Token: 0x040040A6 RID: 16550
		public bool updateErosion = true;

		// Token: 0x040040A7 RID: 16551
		public NNConstraint nnConstraint = NNConstraint.None;

		// Token: 0x040040A8 RID: 16552
		public int addPenalty;

		// Token: 0x040040A9 RID: 16553
		public bool modifyWalkability;

		// Token: 0x040040AA RID: 16554
		public bool setWalkability;

		// Token: 0x040040AB RID: 16555
		public bool modifyTag;

		// Token: 0x040040AC RID: 16556
		public int setTag;

		// Token: 0x040040AD RID: 16557
		public bool trackChangedNodes;

		// Token: 0x040040AE RID: 16558
		public List<GraphNode> changedNodes;

		// Token: 0x040040AF RID: 16559
		private List<uint> backupData;

		// Token: 0x040040B0 RID: 16560
		private List<Int3> backupPositionData;

		// Token: 0x040040B1 RID: 16561
		public GraphUpdateShape shape;
	}
}
