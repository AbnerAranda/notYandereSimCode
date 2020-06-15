using System;
using Pathfinding.Util;

namespace Pathfinding
{
	// Token: 0x0200058E RID: 1422
	public class NavmeshTile : INavmeshHolder, ITransformedGraph, INavmesh
	{
		// Token: 0x06002661 RID: 9825 RVA: 0x001A9EED File Offset: 0x001A80ED
		public void GetTileCoordinates(int tileIndex, out int x, out int z)
		{
			x = this.x;
			z = this.z;
		}

		// Token: 0x06002662 RID: 9826 RVA: 0x001A30EA File Offset: 0x001A12EA
		public int GetVertexArrayIndex(int index)
		{
			return index & 4095;
		}

		// Token: 0x06002663 RID: 9827 RVA: 0x001A9F00 File Offset: 0x001A8100
		public Int3 GetVertex(int index)
		{
			int num = index & 4095;
			return this.verts[num];
		}

		// Token: 0x06002664 RID: 9828 RVA: 0x001A9F21 File Offset: 0x001A8121
		public Int3 GetVertexInGraphSpace(int index)
		{
			return this.vertsInGraphSpace[index & 4095];
		}

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06002665 RID: 9829 RVA: 0x001A9F35 File Offset: 0x001A8135
		public GraphTransform transform
		{
			get
			{
				return this.graph.transform;
			}
		}

		// Token: 0x06002666 RID: 9830 RVA: 0x001A9F44 File Offset: 0x001A8144
		public void GetNodes(Action<GraphNode> action)
		{
			if (this.nodes == null)
			{
				return;
			}
			for (int i = 0; i < this.nodes.Length; i++)
			{
				action(this.nodes[i]);
			}
		}

		// Token: 0x040041E5 RID: 16869
		public int[] tris;

		// Token: 0x040041E6 RID: 16870
		public Int3[] verts;

		// Token: 0x040041E7 RID: 16871
		public Int3[] vertsInGraphSpace;

		// Token: 0x040041E8 RID: 16872
		public int x;

		// Token: 0x040041E9 RID: 16873
		public int z;

		// Token: 0x040041EA RID: 16874
		public int w;

		// Token: 0x040041EB RID: 16875
		public int d;

		// Token: 0x040041EC RID: 16876
		public TriangleMeshNode[] nodes;

		// Token: 0x040041ED RID: 16877
		public BBTree bbTree;

		// Token: 0x040041EE RID: 16878
		public bool flag;

		// Token: 0x040041EF RID: 16879
		public NavmeshBase graph;
	}
}
