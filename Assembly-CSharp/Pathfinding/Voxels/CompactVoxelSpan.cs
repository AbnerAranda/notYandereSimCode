using System;

namespace Pathfinding.Voxels
{
	// Token: 0x020005C8 RID: 1480
	public struct CompactVoxelSpan
	{
		// Token: 0x060027D7 RID: 10199 RVA: 0x001B5255 File Offset: 0x001B3455
		public CompactVoxelSpan(ushort bottom, uint height)
		{
			this.con = 24U;
			this.y = bottom;
			this.h = height;
			this.reg = 0;
		}

		// Token: 0x060027D8 RID: 10200 RVA: 0x001B5274 File Offset: 0x001B3474
		public void SetConnection(int dir, uint value)
		{
			int num = dir * 6;
			this.con = (uint)(((ulong)this.con & (ulong)(~(63L << (num & 31)))) | (ulong)((ulong)(value & 63U) << num));
		}

		// Token: 0x060027D9 RID: 10201 RVA: 0x001B52A8 File Offset: 0x001B34A8
		public int GetConnection(int dir)
		{
			return (int)this.con >> dir * 6 & 63;
		}

		// Token: 0x040042E9 RID: 17129
		public ushort y;

		// Token: 0x040042EA RID: 17130
		public uint con;

		// Token: 0x040042EB RID: 17131
		public uint h;

		// Token: 0x040042EC RID: 17132
		public int reg;
	}
}
