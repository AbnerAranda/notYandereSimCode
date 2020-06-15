using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200053E RID: 1342
	public struct Int3 : IEquatable<Int3>
	{
		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x060022F3 RID: 8947 RVA: 0x00196E14 File Offset: 0x00195014
		public static Int3 zero
		{
			get
			{
				return default(Int3);
			}
		}

		// Token: 0x060022F4 RID: 8948 RVA: 0x00196E2C File Offset: 0x0019502C
		public Int3(Vector3 position)
		{
			this.x = (int)Math.Round((double)(position.x * 1000f));
			this.y = (int)Math.Round((double)(position.y * 1000f));
			this.z = (int)Math.Round((double)(position.z * 1000f));
		}

		// Token: 0x060022F5 RID: 8949 RVA: 0x00196E84 File Offset: 0x00195084
		public Int3(int _x, int _y, int _z)
		{
			this.x = _x;
			this.y = _y;
			this.z = _z;
		}

		// Token: 0x060022F6 RID: 8950 RVA: 0x00196E9B File Offset: 0x0019509B
		public static bool operator ==(Int3 lhs, Int3 rhs)
		{
			return lhs.x == rhs.x && lhs.y == rhs.y && lhs.z == rhs.z;
		}

		// Token: 0x060022F7 RID: 8951 RVA: 0x00196EC9 File Offset: 0x001950C9
		public static bool operator !=(Int3 lhs, Int3 rhs)
		{
			return lhs.x != rhs.x || lhs.y != rhs.y || lhs.z != rhs.z;
		}

		// Token: 0x060022F8 RID: 8952 RVA: 0x00196EFA File Offset: 0x001950FA
		public static explicit operator Int3(Vector3 ob)
		{
			return new Int3((int)Math.Round((double)(ob.x * 1000f)), (int)Math.Round((double)(ob.y * 1000f)), (int)Math.Round((double)(ob.z * 1000f)));
		}

		// Token: 0x060022F9 RID: 8953 RVA: 0x00196F3A File Offset: 0x0019513A
		public static explicit operator Vector3(Int3 ob)
		{
			return new Vector3((float)ob.x * 0.001f, (float)ob.y * 0.001f, (float)ob.z * 0.001f);
		}

		// Token: 0x060022FA RID: 8954 RVA: 0x00196F68 File Offset: 0x00195168
		public static Int3 operator -(Int3 lhs, Int3 rhs)
		{
			lhs.x -= rhs.x;
			lhs.y -= rhs.y;
			lhs.z -= rhs.z;
			return lhs;
		}

		// Token: 0x060022FB RID: 8955 RVA: 0x00196F9E File Offset: 0x0019519E
		public static Int3 operator -(Int3 lhs)
		{
			lhs.x = -lhs.x;
			lhs.y = -lhs.y;
			lhs.z = -lhs.z;
			return lhs;
		}

		// Token: 0x060022FC RID: 8956 RVA: 0x00196FCB File Offset: 0x001951CB
		public static Int3 operator +(Int3 lhs, Int3 rhs)
		{
			lhs.x += rhs.x;
			lhs.y += rhs.y;
			lhs.z += rhs.z;
			return lhs;
		}

		// Token: 0x060022FD RID: 8957 RVA: 0x00197001 File Offset: 0x00195201
		public static Int3 operator *(Int3 lhs, int rhs)
		{
			lhs.x *= rhs;
			lhs.y *= rhs;
			lhs.z *= rhs;
			return lhs;
		}

		// Token: 0x060022FE RID: 8958 RVA: 0x00197028 File Offset: 0x00195228
		public static Int3 operator *(Int3 lhs, float rhs)
		{
			lhs.x = (int)Math.Round((double)((float)lhs.x * rhs));
			lhs.y = (int)Math.Round((double)((float)lhs.y * rhs));
			lhs.z = (int)Math.Round((double)((float)lhs.z * rhs));
			return lhs;
		}

		// Token: 0x060022FF RID: 8959 RVA: 0x0019707C File Offset: 0x0019527C
		public static Int3 operator *(Int3 lhs, double rhs)
		{
			lhs.x = (int)Math.Round((double)lhs.x * rhs);
			lhs.y = (int)Math.Round((double)lhs.y * rhs);
			lhs.z = (int)Math.Round((double)lhs.z * rhs);
			return lhs;
		}

		// Token: 0x06002300 RID: 8960 RVA: 0x001970CC File Offset: 0x001952CC
		public static Int3 operator /(Int3 lhs, float rhs)
		{
			lhs.x = (int)Math.Round((double)((float)lhs.x / rhs));
			lhs.y = (int)Math.Round((double)((float)lhs.y / rhs));
			lhs.z = (int)Math.Round((double)((float)lhs.z / rhs));
			return lhs;
		}

		// Token: 0x1700053D RID: 1341
		public int this[int i]
		{
			get
			{
				if (i == 0)
				{
					return this.x;
				}
				if (i != 1)
				{
					return this.z;
				}
				return this.y;
			}
			set
			{
				if (i == 0)
				{
					this.x = value;
					return;
				}
				if (i == 1)
				{
					this.y = value;
					return;
				}
				this.z = value;
			}
		}

		// Token: 0x06002303 RID: 8963 RVA: 0x0019715C File Offset: 0x0019535C
		public static float Angle(Int3 lhs, Int3 rhs)
		{
			double num = (double)Int3.Dot(lhs, rhs) / ((double)lhs.magnitude * (double)rhs.magnitude);
			num = ((num < -1.0) ? -1.0 : ((num > 1.0) ? 1.0 : num));
			return (float)Math.Acos(num);
		}

		// Token: 0x06002304 RID: 8964 RVA: 0x001971BB File Offset: 0x001953BB
		public static int Dot(Int3 lhs, Int3 rhs)
		{
			return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z;
		}

		// Token: 0x06002305 RID: 8965 RVA: 0x001971E6 File Offset: 0x001953E6
		public static long DotLong(Int3 lhs, Int3 rhs)
		{
			return (long)lhs.x * (long)rhs.x + (long)lhs.y * (long)rhs.y + (long)lhs.z * (long)rhs.z;
		}

		// Token: 0x06002306 RID: 8966 RVA: 0x00197217 File Offset: 0x00195417
		public Int3 Normal2D()
		{
			return new Int3(this.z, this.y, -this.x);
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x06002307 RID: 8967 RVA: 0x00197234 File Offset: 0x00195434
		public float magnitude
		{
			get
			{
				double num = (double)this.x;
				double num2 = (double)this.y;
				double num3 = (double)this.z;
				return (float)Math.Sqrt(num * num + num2 * num2 + num3 * num3);
			}
		}

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x06002308 RID: 8968 RVA: 0x00197268 File Offset: 0x00195468
		public int costMagnitude
		{
			get
			{
				return (int)Math.Round((double)this.magnitude);
			}
		}

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x06002309 RID: 8969 RVA: 0x00197278 File Offset: 0x00195478
		[Obsolete("This property is deprecated. Use magnitude or cast to a Vector3")]
		public float worldMagnitude
		{
			get
			{
				double num = (double)this.x;
				double num2 = (double)this.y;
				double num3 = (double)this.z;
				return (float)Math.Sqrt(num * num + num2 * num2 + num3 * num3) * 0.001f;
			}
		}

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x0600230A RID: 8970 RVA: 0x001972B4 File Offset: 0x001954B4
		public float sqrMagnitude
		{
			get
			{
				double num = (double)this.x;
				double num2 = (double)this.y;
				double num3 = (double)this.z;
				return (float)(num * num + num2 * num2 + num3 * num3);
			}
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x0600230B RID: 8971 RVA: 0x001972E4 File Offset: 0x001954E4
		public long sqrMagnitudeLong
		{
			get
			{
				long num = (long)this.x;
				long num2 = (long)this.y;
				long num3 = (long)this.z;
				return num * num + num2 * num2 + num3 * num3;
			}
		}

		// Token: 0x0600230C RID: 8972 RVA: 0x00197312 File Offset: 0x00195512
		public static implicit operator string(Int3 obj)
		{
			return obj.ToString();
		}

		// Token: 0x0600230D RID: 8973 RVA: 0x00197324 File Offset: 0x00195524
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"( ",
				this.x,
				", ",
				this.y,
				", ",
				this.z,
				")"
			});
		}

		// Token: 0x0600230E RID: 8974 RVA: 0x00197388 File Offset: 0x00195588
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			Int3 @int = (Int3)obj;
			return this.x == @int.x && this.y == @int.y && this.z == @int.z;
		}

		// Token: 0x0600230F RID: 8975 RVA: 0x00196E9B File Offset: 0x0019509B
		public bool Equals(Int3 other)
		{
			return this.x == other.x && this.y == other.y && this.z == other.z;
		}

		// Token: 0x06002310 RID: 8976 RVA: 0x001973CD File Offset: 0x001955CD
		public override int GetHashCode()
		{
			return this.x * 73856093 ^ this.y * 19349663 ^ this.z * 83492791;
		}

		// Token: 0x04003FED RID: 16365
		public int x;

		// Token: 0x04003FEE RID: 16366
		public int y;

		// Token: 0x04003FEF RID: 16367
		public int z;

		// Token: 0x04003FF0 RID: 16368
		public const int Precision = 1000;

		// Token: 0x04003FF1 RID: 16369
		public const float FloatPrecision = 1000f;

		// Token: 0x04003FF2 RID: 16370
		public const float PrecisionFactor = 0.001f;
	}
}
