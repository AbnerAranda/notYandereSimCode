using System;

namespace Pathfinding
{
	// Token: 0x02000581 RID: 1409
	public interface INavmesh
	{
		// Token: 0x06002566 RID: 9574
		void GetNodes(Action<GraphNode> del);
	}
}
