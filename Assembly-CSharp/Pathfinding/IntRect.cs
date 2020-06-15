using System;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000560 RID: 1376
	[Serializable]
	public struct IntRect
	{
		// Token: 0x0600245C RID: 9308 RVA: 0x0019BDDA File Offset: 0x00199FDA
		public IntRect(int xmin, int ymin, int xmax, int ymax)
		{
			this.xmin = xmin;
			this.xmax = xmax;
			this.ymin = ymin;
			this.ymax = ymax;
		}

		// Token: 0x0600245D RID: 9309 RVA: 0x0019BDF9 File Offset: 0x00199FF9
		public bool Contains(int x, int y)
		{
			return x >= this.xmin && y >= this.ymin && x <= this.xmax && y <= this.ymax;
		}

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x0600245E RID: 9310 RVA: 0x0019BE24 File Offset: 0x0019A024
		public int Width
		{
			get
			{
				return this.xmax - this.xmin + 1;
			}
		}

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x0600245F RID: 9311 RVA: 0x0019BE35 File Offset: 0x0019A035
		public int Height
		{
			get
			{
				return this.ymax - this.ymin + 1;
			}
		}

		// Token: 0x06002460 RID: 9312 RVA: 0x0019BE46 File Offset: 0x0019A046
		public bool IsValid()
		{
			return this.xmin <= this.xmax && this.ymin <= this.ymax;
		}

		// Token: 0x06002461 RID: 9313 RVA: 0x0019BE69 File Offset: 0x0019A069
		public static bool operator ==(IntRect a, IntRect b)
		{
			return a.xmin == b.xmin && a.xmax == b.xmax && a.ymin == b.ymin && a.ymax == b.ymax;
		}

		// Token: 0x06002462 RID: 9314 RVA: 0x0019BEA5 File Offset: 0x0019A0A5
		public static bool operator !=(IntRect a, IntRect b)
		{
			return a.xmin != b.xmin || a.xmax != b.xmax || a.ymin != b.ymin || a.ymax != b.ymax;
		}

		// Token: 0x06002463 RID: 9315 RVA: 0x0019BEE4 File Offset: 0x0019A0E4
		public override bool Equals(object obj)
		{
			IntRect intRect = (IntRect)obj;
			return this.xmin == intRect.xmin && this.xmax == intRect.xmax && this.ymin == intRect.ymin && this.ymax == intRect.ymax;
		}

		// Token: 0x06002464 RID: 9316 RVA: 0x0019BF32 File Offset: 0x0019A132
		public override int GetHashCode()
		{
			return this.xmin * 131071 ^ this.xmax * 3571 ^ this.ymin * 3109 ^ this.ymax * 7;
		}

		// Token: 0x06002465 RID: 9317 RVA: 0x0019BF64 File Offset: 0x0019A164
		public static IntRect Intersection(IntRect a, IntRect b)
		{
			return new IntRect(Math.Max(a.xmin, b.xmin), Math.Max(a.ymin, b.ymin), Math.Min(a.xmax, b.xmax), Math.Min(a.ymax, b.ymax));
		}

		// Token: 0x06002466 RID: 9318 RVA: 0x0019BFBA File Offset: 0x0019A1BA
		public static bool Intersects(IntRect a, IntRect b)
		{
			return a.xmin <= b.xmax && a.ymin <= b.ymax && a.xmax >= b.xmin && a.ymax >= b.ymin;
		}

		// Token: 0x06002467 RID: 9319 RVA: 0x0019BFFC File Offset: 0x0019A1FC
		public static IntRect Union(IntRect a, IntRect b)
		{
			return new IntRect(Math.Min(a.xmin, b.xmin), Math.Min(a.ymin, b.ymin), Math.Max(a.xmax, b.xmax), Math.Max(a.ymax, b.ymax));
		}

		// Token: 0x06002468 RID: 9320 RVA: 0x0019C052 File Offset: 0x0019A252
		public IntRect ExpandToContain(int x, int y)
		{
			return new IntRect(Math.Min(this.xmin, x), Math.Min(this.ymin, y), Math.Max(this.xmax, x), Math.Max(this.ymax, y));
		}

		// Token: 0x06002469 RID: 9321 RVA: 0x0019C089 File Offset: 0x0019A289
		public IntRect Expand(int range)
		{
			return new IntRect(this.xmin - range, this.ymin - range, this.xmax + range, this.ymax + range);
		}

		// Token: 0x0600246A RID: 9322 RVA: 0x0019C0B0 File Offset: 0x0019A2B0
		public IntRect Rotate(int r)
		{
			int num = IntRect.Rotations[r * 4];
			int num2 = IntRect.Rotations[r * 4 + 1];
			int num3 = IntRect.Rotations[r * 4 + 2];
			int num4 = IntRect.Rotations[r * 4 + 3];
			int val = num * this.xmin + num2 * this.ymin;
			int val2 = num3 * this.xmin + num4 * this.ymin;
			int val3 = num * this.xmax + num2 * this.ymax;
			int val4 = num3 * this.xmax + num4 * this.ymax;
			return new IntRect(Math.Min(val, val3), Math.Min(val2, val4), Math.Max(val, val3), Math.Max(val2, val4));
		}

		// Token: 0x0600246B RID: 9323 RVA: 0x0019C15B File Offset: 0x0019A35B
		public IntRect Offset(Int2 offset)
		{
			return new IntRect(this.xmin + offset.x, this.ymin + offset.y, this.xmax + offset.x, this.ymax + offset.y);
		}

		// Token: 0x0600246C RID: 9324 RVA: 0x0019C196 File Offset: 0x0019A396
		public IntRect Offset(int x, int y)
		{
			return new IntRect(this.xmin + x, this.ymin + y, this.xmax + x, this.ymax + y);
		}

		// Token: 0x0600246D RID: 9325 RVA: 0x0019C1C0 File Offset: 0x0019A3C0
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"[x: ",
				this.xmin,
				"...",
				this.xmax,
				", y: ",
				this.ymin,
				"...",
				this.ymax,
				"]"
			});
		}

		// Token: 0x0600246E RID: 9326 RVA: 0x0019C23C File Offset: 0x0019A43C
		public void DebugDraw(GraphTransform transform, Color color)
		{
			Vector3 vector = transform.Transform(new Vector3((float)this.xmin, 0f, (float)this.ymin));
			Vector3 vector2 = transform.Transform(new Vector3((float)this.xmin, 0f, (float)this.ymax));
			Vector3 vector3 = transform.Transform(new Vector3((float)this.xmax, 0f, (float)this.ymax));
			Vector3 vector4 = transform.Transform(new Vector3((float)this.xmax, 0f, (float)this.ymin));
			Debug.DrawLine(vector, vector2, color);
			Debug.DrawLine(vector2, vector3, color);
			Debug.DrawLine(vector3, vector4, color);
			Debug.DrawLine(vector4, vector, color);
		}

		// Token: 0x040040B2 RID: 16562
		public int xmin;

		// Token: 0x040040B3 RID: 16563
		public int ymin;

		// Token: 0x040040B4 RID: 16564
		public int xmax;

		// Token: 0x040040B5 RID: 16565
		public int ymax;

		// Token: 0x040040B6 RID: 16566
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
