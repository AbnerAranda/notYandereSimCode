using System;

namespace Pathfinding.Voxels
{
	// Token: 0x020005C0 RID: 1472
	public struct LinkedVoxelSpan
	{
		// Token: 0x060027CB RID: 10187 RVA: 0x001B4FC2 File Offset: 0x001B31C2
		public LinkedVoxelSpan(uint bottom, uint top, int area)
		{
			this.bottom = bottom;
			this.top = top;
			this.area = area;
			this.next = -1;
		}

		// Token: 0x060027CC RID: 10188 RVA: 0x001B4FE0 File Offset: 0x001B31E0
		public LinkedVoxelSpan(uint bottom, uint top, int area, int next)
		{
			this.bottom = bottom;
			this.top = top;
			this.area = area;
			this.next = next;
		}

		// Token: 0x040042CF RID: 17103
		public uint bottom;

		// Token: 0x040042D0 RID: 17104
		public uint top;

		// Token: 0x040042D1 RID: 17105
		public int next;

		// Token: 0x040042D2 RID: 17106
		public int area;
	}
}
