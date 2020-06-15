using System;

namespace Pathfinding
{
	// Token: 0x0200054F RID: 1359
	public interface ITraversalProvider
	{
		// Token: 0x060023D8 RID: 9176
		bool CanTraverse(Path path, GraphNode node);

		// Token: 0x060023D9 RID: 9177
		uint GetTraversalCost(Path path, GraphNode node);
	}
}
