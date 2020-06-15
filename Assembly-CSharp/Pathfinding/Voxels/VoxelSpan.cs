using System;

namespace Pathfinding.Voxels
{
	// Token: 0x020005C9 RID: 1481
	public class VoxelSpan
	{
		// Token: 0x060027DA RID: 10202 RVA: 0x001B52BA File Offset: 0x001B34BA
		public VoxelSpan(uint b, uint t, int area)
		{
			this.bottom = b;
			this.top = t;
			this.area = area;
		}

		// Token: 0x040042ED RID: 17133
		public uint bottom;

		// Token: 0x040042EE RID: 17134
		public uint top;

		// Token: 0x040042EF RID: 17135
		public VoxelSpan next;

		// Token: 0x040042F0 RID: 17136
		public int area;
	}
}
