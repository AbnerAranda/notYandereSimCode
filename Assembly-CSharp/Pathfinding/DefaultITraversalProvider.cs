using System;

namespace Pathfinding
{
	// Token: 0x02000550 RID: 1360
	public static class DefaultITraversalProvider
	{
		// Token: 0x060023DA RID: 9178 RVA: 0x0019A9A4 File Offset: 0x00198BA4
		public static bool CanTraverse(Path path, GraphNode node)
		{
			return node.Walkable && (path.enabledTags >> (int)node.Tag & 1) != 0;
		}

		// Token: 0x060023DB RID: 9179 RVA: 0x0019A9C5 File Offset: 0x00198BC5
		public static uint GetTraversalCost(Path path, GraphNode node)
		{
			return path.GetTagPenalty((int)node.Tag) + node.Penalty;
		}
	}
}
