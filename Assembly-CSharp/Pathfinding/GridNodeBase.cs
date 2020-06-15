using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000585 RID: 1413
	public abstract class GridNodeBase : GraphNode
	{
		// Token: 0x060025C6 RID: 9670 RVA: 0x0019A4CC File Offset: 0x001986CC
		protected GridNodeBase(AstarPath astar) : base(astar)
		{
		}

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x060025C7 RID: 9671 RVA: 0x001A5F4B File Offset: 0x001A414B
		// (set) Token: 0x060025C8 RID: 9672 RVA: 0x001A5F59 File Offset: 0x001A4159
		public int NodeInGridIndex
		{
			get
			{
				return this.nodeInGridIndex & 16777215;
			}
			set
			{
				this.nodeInGridIndex = ((this.nodeInGridIndex & -16777216) | value);
			}
		}

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x060025C9 RID: 9673 RVA: 0x001A5F6F File Offset: 0x001A416F
		public int XCoordinateInGrid
		{
			get
			{
				return this.NodeInGridIndex % GridNode.GetGridGraph(base.GraphIndex).width;
			}
		}

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x060025CA RID: 9674 RVA: 0x001A5F88 File Offset: 0x001A4188
		public int ZCoordinateInGrid
		{
			get
			{
				return this.NodeInGridIndex / GridNode.GetGridGraph(base.GraphIndex).width;
			}
		}

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x060025CB RID: 9675 RVA: 0x001A5FA1 File Offset: 0x001A41A1
		// (set) Token: 0x060025CC RID: 9676 RVA: 0x001A5FB2 File Offset: 0x001A41B2
		public bool WalkableErosion
		{
			get
			{
				return (this.gridFlags & 256) > 0;
			}
			set
			{
				this.gridFlags = (ushort)(((int)this.gridFlags & -257) | (value ? 256 : 0));
			}
		}

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x060025CD RID: 9677 RVA: 0x001A5FD3 File Offset: 0x001A41D3
		// (set) Token: 0x060025CE RID: 9678 RVA: 0x001A5FE4 File Offset: 0x001A41E4
		public bool TmpWalkable
		{
			get
			{
				return (this.gridFlags & 512) > 0;
			}
			set
			{
				this.gridFlags = (ushort)(((int)this.gridFlags & -513) | (value ? 512 : 0));
			}
		}

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x060025CF RID: 9679
		public abstract bool HasConnectionsToAllEightNeighbours { get; }

		// Token: 0x060025D0 RID: 9680 RVA: 0x001A6008 File Offset: 0x001A4208
		public override float SurfaceArea()
		{
			GridGraph gridGraph = GridNode.GetGridGraph(base.GraphIndex);
			return gridGraph.nodeSize * gridGraph.nodeSize;
		}

		// Token: 0x060025D1 RID: 9681 RVA: 0x001A6030 File Offset: 0x001A4230
		public override Vector3 RandomPointOnSurface()
		{
			GridGraph gridGraph = GridNode.GetGridGraph(base.GraphIndex);
			Vector3 a = gridGraph.transform.InverseTransform((Vector3)this.position);
			return gridGraph.transform.Transform(a + new Vector3(UnityEngine.Random.value - 0.5f, 0f, UnityEngine.Random.value - 0.5f));
		}

		// Token: 0x060025D2 RID: 9682 RVA: 0x001A608F File Offset: 0x001A428F
		public override int GetGizmoHashCode()
		{
			return base.GetGizmoHashCode() ^ (int)(109 * this.gridFlags);
		}

		// Token: 0x060025D3 RID: 9683
		public abstract GridNodeBase GetNeighbourAlongDirection(int direction);

		// Token: 0x060025D4 RID: 9684 RVA: 0x001A60A4 File Offset: 0x001A42A4
		public override bool ContainsConnection(GraphNode node)
		{
			for (int i = 0; i < 8; i++)
			{
				if (node == this.GetNeighbourAlongDirection(i))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060025D5 RID: 9685 RVA: 0x001A60CA File Offset: 0x001A42CA
		public override void AddConnection(GraphNode node, uint cost)
		{
			throw new NotImplementedException("GridNodes do not have support for adding manual connections with your current settings.\nPlease disable ASTAR_GRID_NO_CUSTOM_CONNECTIONS in the Optimizations tab in the A* Inspector");
		}

		// Token: 0x060025D6 RID: 9686 RVA: 0x001A60D6 File Offset: 0x001A42D6
		public override void RemoveConnection(GraphNode node)
		{
			throw new NotImplementedException("GridNodes do not have support for adding manual connections with your current settings.\nPlease disable ASTAR_GRID_NO_CUSTOM_CONNECTIONS in the Optimizations tab in the A* Inspector");
		}

		// Token: 0x060025D7 RID: 9687 RVA: 0x00002ACE File Offset: 0x00000CCE
		public void ClearCustomConnections(bool alsoReverse)
		{
		}

		// Token: 0x04004195 RID: 16789
		private const int GridFlagsWalkableErosionOffset = 8;

		// Token: 0x04004196 RID: 16790
		private const int GridFlagsWalkableErosionMask = 256;

		// Token: 0x04004197 RID: 16791
		private const int GridFlagsWalkableTmpOffset = 9;

		// Token: 0x04004198 RID: 16792
		private const int GridFlagsWalkableTmpMask = 512;

		// Token: 0x04004199 RID: 16793
		protected const int NodeInGridIndexLayerOffset = 24;

		// Token: 0x0400419A RID: 16794
		protected const int NodeInGridIndexMask = 16777215;

		// Token: 0x0400419B RID: 16795
		protected int nodeInGridIndex;

		// Token: 0x0400419C RID: 16796
		protected ushort gridFlags;
	}
}
