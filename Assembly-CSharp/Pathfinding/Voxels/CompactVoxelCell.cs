using System;

namespace Pathfinding.Voxels
{
	// Token: 0x020005C7 RID: 1479
	public struct CompactVoxelCell
	{
		// Token: 0x060027D6 RID: 10198 RVA: 0x001B5245 File Offset: 0x001B3445
		public CompactVoxelCell(uint i, uint c)
		{
			this.index = i;
			this.count = c;
		}

		// Token: 0x040042E7 RID: 17127
		public uint index;

		// Token: 0x040042E8 RID: 17128
		public uint count;
	}
}
