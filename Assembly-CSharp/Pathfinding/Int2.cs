using System;

namespace Pathfinding
{
	// Token: 0x0200053F RID: 1343
	public struct Int2 : IEquatable<Int2>
	{
		// Token: 0x06002311 RID: 8977 RVA: 0x001973F5 File Offset: 0x001955F5
		public Int2(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x06002312 RID: 8978 RVA: 0x00197405 File Offset: 0x00195605
		public long sqrMagnitudeLong
		{
			get
			{
				return (long)this.x * (long)this.x + (long)this.y * (long)this.y;
			}
		}

		// Token: 0x06002313 RID: 8979 RVA: 0x00197426 File Offset: 0x00195626
		public static Int2 operator +(Int2 a, Int2 b)
		{
			return new Int2(a.x + b.x, a.y + b.y);
		}

		// Token: 0x06002314 RID: 8980 RVA: 0x00197447 File Offset: 0x00195647
		public static Int2 operator -(Int2 a, Int2 b)
		{
			return new Int2(a.x - b.x, a.y - b.y);
		}

		// Token: 0x06002315 RID: 8981 RVA: 0x00197468 File Offset: 0x00195668
		public static bool operator ==(Int2 a, Int2 b)
		{
			return a.x == b.x && a.y == b.y;
		}

		// Token: 0x06002316 RID: 8982 RVA: 0x00197488 File Offset: 0x00195688
		public static bool operator !=(Int2 a, Int2 b)
		{
			return a.x != b.x || a.y != b.y;
		}

		// Token: 0x06002317 RID: 8983 RVA: 0x001974AB File Offset: 0x001956AB
		public static long DotLong(Int2 a, Int2 b)
		{
			return (long)a.x * (long)b.x + (long)a.y * (long)b.y;
		}

		// Token: 0x06002318 RID: 8984 RVA: 0x001974CC File Offset: 0x001956CC
		public override bool Equals(object o)
		{
			if (o == null)
			{
				return false;
			}
			Int2 @int = (Int2)o;
			return this.x == @int.x && this.y == @int.y;
		}

		// Token: 0x06002319 RID: 8985 RVA: 0x00197468 File Offset: 0x00195668
		public bool Equals(Int2 other)
		{
			return this.x == other.x && this.y == other.y;
		}

		// Token: 0x0600231A RID: 8986 RVA: 0x00197503 File Offset: 0x00195703
		public override int GetHashCode()
		{
			return this.x * 49157 + this.y * 98317;
		}

		// Token: 0x0600231B RID: 8987 RVA: 0x00197520 File Offset: 0x00195720
		[Obsolete("Deprecated becuase it is not used by any part of the A* Pathfinding Project")]
		public static Int2 Rotate(Int2 v, int r)
		{
			r %= 4;
			return new Int2(v.x * Int2.Rotations[r * 4] + v.y * Int2.Rotations[r * 4 + 1], v.x * Int2.Rotations[r * 4 + 2] + v.y * Int2.Rotations[r * 4 + 3]);
		}

		// Token: 0x0600231C RID: 8988 RVA: 0x0019757F File Offset: 0x0019577F
		public static Int2 Min(Int2 a, Int2 b)
		{
			return new Int2(Math.Min(a.x, b.x), Math.Min(a.y, b.y));
		}

		// Token: 0x0600231D RID: 8989 RVA: 0x001975A8 File Offset: 0x001957A8
		public static Int2 Max(Int2 a, Int2 b)
		{
			return new Int2(Math.Max(a.x, b.x), Math.Max(a.y, b.y));
		}

		// Token: 0x0600231E RID: 8990 RVA: 0x001975D1 File Offset: 0x001957D1
		public static Int2 FromInt3XZ(Int3 o)
		{
			return new Int2(o.x, o.z);
		}

		// Token: 0x0600231F RID: 8991 RVA: 0x001975E4 File Offset: 0x001957E4
		public static Int3 ToInt3XZ(Int2 o)
		{
			return new Int3(o.x, 0, o.y);
		}

		// Token: 0x06002320 RID: 8992 RVA: 0x001975F8 File Offset: 0x001957F8
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"(",
				this.x,
				", ",
				this.y,
				")"
			});
		}

		// Token: 0x04003FF3 RID: 16371
		public int x;

		// Token: 0x04003FF4 RID: 16372
		public int y;

		// Token: 0x04003FF5 RID: 16373
		private static readonly int[] Rotations = new int[]
		{
			1,
			0,
			0,
			1,
			0,
			1,
			-1,
			0,
			-1,
			0,
			0,
			-1,
			0,
			-1,
			1,
			0
		};
	}
}
