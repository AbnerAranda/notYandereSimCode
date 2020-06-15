using System;

namespace Pathfinding
{
	// Token: 0x02000587 RID: 1415
	public interface INavmeshHolder : ITransformedGraph, INavmesh
	{
		// Token: 0x060025E6 RID: 9702
		Int3 GetVertex(int i);

		// Token: 0x060025E7 RID: 9703
		Int3 GetVertexInGraphSpace(int i);

		// Token: 0x060025E8 RID: 9704
		int GetVertexArrayIndex(int index);

		// Token: 0x060025E9 RID: 9705
		void GetTileCoordinates(int tileIndex, out int x, out int z);
	}
}
