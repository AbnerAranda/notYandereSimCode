using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000533 RID: 1331
	public static class AstarMath
	{
		// Token: 0x06002239 RID: 8761 RVA: 0x0019284B File Offset: 0x00190A4B
		[Obsolete("Use VectorMath.ClosestPointOnLine instead")]
		public static Vector3 NearestPoint(Vector3 lineStart, Vector3 lineEnd, Vector3 point)
		{
			return VectorMath.ClosestPointOnLine(lineStart, lineEnd, point);
		}

		// Token: 0x0600223A RID: 8762 RVA: 0x00192855 File Offset: 0x00190A55
		[Obsolete("Use VectorMath.ClosestPointOnLineFactor instead")]
		public static float NearestPointFactor(Vector3 lineStart, Vector3 lineEnd, Vector3 point)
		{
			return VectorMath.ClosestPointOnLineFactor(lineStart, lineEnd, point);
		}

		// Token: 0x0600223B RID: 8763 RVA: 0x0019285F File Offset: 0x00190A5F
		[Obsolete("Use VectorMath.ClosestPointOnLineFactor instead")]
		public static float NearestPointFactor(Int3 lineStart, Int3 lineEnd, Int3 point)
		{
			return VectorMath.ClosestPointOnLineFactor(lineStart, lineEnd, point);
		}

		// Token: 0x0600223C RID: 8764 RVA: 0x00192869 File Offset: 0x00190A69
		[Obsolete("Use VectorMath.ClosestPointOnLineFactor instead")]
		public static float NearestPointFactor(Int2 lineStart, Int2 lineEnd, Int2 point)
		{
			return VectorMath.ClosestPointOnLineFactor(lineStart, lineEnd, point);
		}

		// Token: 0x0600223D RID: 8765 RVA: 0x00192873 File Offset: 0x00190A73
		[Obsolete("Use VectorMath.ClosestPointOnSegment instead")]
		public static Vector3 NearestPointStrict(Vector3 lineStart, Vector3 lineEnd, Vector3 point)
		{
			return VectorMath.ClosestPointOnSegment(lineStart, lineEnd, point);
		}

		// Token: 0x0600223E RID: 8766 RVA: 0x0019287D File Offset: 0x00190A7D
		[Obsolete("Use VectorMath.ClosestPointOnSegmentXZ instead")]
		public static Vector3 NearestPointStrictXZ(Vector3 lineStart, Vector3 lineEnd, Vector3 point)
		{
			return VectorMath.ClosestPointOnSegmentXZ(lineStart, lineEnd, point);
		}

		// Token: 0x0600223F RID: 8767 RVA: 0x00192887 File Offset: 0x00190A87
		[Obsolete("Use VectorMath.SqrDistancePointSegmentApproximate instead")]
		public static float DistancePointSegment(int x, int z, int px, int pz, int qx, int qz)
		{
			return VectorMath.SqrDistancePointSegmentApproximate(x, z, px, pz, qx, qz);
		}

		// Token: 0x06002240 RID: 8768 RVA: 0x00192896 File Offset: 0x00190A96
		[Obsolete("Use VectorMath.SqrDistancePointSegmentApproximate instead")]
		public static float DistancePointSegment(Int3 a, Int3 b, Int3 p)
		{
			return VectorMath.SqrDistancePointSegmentApproximate(a, b, p);
		}

		// Token: 0x06002241 RID: 8769 RVA: 0x001928A0 File Offset: 0x00190AA0
		[Obsolete("Use VectorMath.SqrDistancePointSegment instead")]
		public static float DistancePointSegmentStrict(Vector3 a, Vector3 b, Vector3 p)
		{
			return VectorMath.SqrDistancePointSegment(a, b, p);
		}

		// Token: 0x06002242 RID: 8770 RVA: 0x001928AA File Offset: 0x00190AAA
		[Obsolete("Use AstarSplines.CubicBezier instead")]
		public static Vector3 CubicBezier(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
		{
			return AstarSplines.CubicBezier(p0, p1, p2, p3, t);
		}

		// Token: 0x06002243 RID: 8771 RVA: 0x001928B7 File Offset: 0x00190AB7
		[Obsolete("Use Mathf.InverseLerp instead")]
		public static float MapTo(float startMin, float startMax, float value)
		{
			return Mathf.InverseLerp(startMin, startMax, value);
		}

		// Token: 0x06002244 RID: 8772 RVA: 0x001928C1 File Offset: 0x00190AC1
		public static float MapTo(float startMin, float startMax, float targetMin, float targetMax, float value)
		{
			return Mathf.Lerp(targetMin, targetMax, Mathf.InverseLerp(startMin, startMax, value));
		}

		// Token: 0x06002245 RID: 8773 RVA: 0x001928D4 File Offset: 0x00190AD4
		public static string FormatBytesBinary(int bytes)
		{
			double num = (bytes >= 0) ? 1.0 : -1.0;
			bytes = Mathf.Abs(bytes);
			if (bytes < 1024)
			{
				return (double)bytes * num + " bytes";
			}
			if (bytes < 1048576)
			{
				return ((double)bytes / 1024.0 * num).ToString("0.0") + " KiB";
			}
			if (bytes < 1073741824)
			{
				return ((double)bytes / 1048576.0 * num).ToString("0.0") + " MiB";
			}
			return ((double)bytes / 1073741824.0 * num).ToString("0.0") + " GiB";
		}

		// Token: 0x06002246 RID: 8774 RVA: 0x0019299F File Offset: 0x00190B9F
		private static int Bit(int a, int b)
		{
			return a >> b & 1;
		}

		// Token: 0x06002247 RID: 8775 RVA: 0x001929AC File Offset: 0x00190BAC
		public static Color IntToColor(int i, float a)
		{
			float num = (float)(AstarMath.Bit(i, 2) + AstarMath.Bit(i, 3) * 2 + 1);
			int num2 = AstarMath.Bit(i, 1) + AstarMath.Bit(i, 4) * 2 + 1;
			int num3 = AstarMath.Bit(i, 0) + AstarMath.Bit(i, 5) * 2 + 1;
			return new Color(num * 0.25f, (float)num2 * 0.25f, (float)num3 * 0.25f, a);
		}

		// Token: 0x06002248 RID: 8776 RVA: 0x00192A14 File Offset: 0x00190C14
		public static Color HSVToRGB(float h, float s, float v)
		{
			float num = 0f;
			float num2 = 0f;
			float num3 = 0f;
			float num4 = s * v;
			float num5 = h / 60f;
			float num6 = num4 * (1f - Math.Abs(num5 % 2f - 1f));
			if (num5 < 1f)
			{
				num = num4;
				num2 = num6;
			}
			else if (num5 < 2f)
			{
				num = num6;
				num2 = num4;
			}
			else if (num5 < 3f)
			{
				num2 = num4;
				num3 = num6;
			}
			else if (num5 < 4f)
			{
				num2 = num6;
				num3 = num4;
			}
			else if (num5 < 5f)
			{
				num = num6;
				num3 = num4;
			}
			else if (num5 < 6f)
			{
				num = num4;
				num3 = num6;
			}
			float num7 = v - num4;
			num += num7;
			num2 += num7;
			num3 += num7;
			return new Color(num, num2, num3);
		}

		// Token: 0x06002249 RID: 8777 RVA: 0x00192AD7 File Offset: 0x00190CD7
		[Obsolete("Use VectorMath.SqrDistanceXZ instead")]
		public static float SqrMagnitudeXZ(Vector3 a, Vector3 b)
		{
			return VectorMath.SqrDistanceXZ(a, b);
		}

		// Token: 0x0600224A RID: 8778 RVA: 0x00192AE0 File Offset: 0x00190CE0
		[Obsolete("Obsolete", true)]
		public static float DistancePointSegment2(int x, int z, int px, int pz, int qx, int qz)
		{
			throw new NotImplementedException("Obsolete");
		}

		// Token: 0x0600224B RID: 8779 RVA: 0x00192AE0 File Offset: 0x00190CE0
		[Obsolete("Obsolete", true)]
		public static float DistancePointSegment2(Vector3 a, Vector3 b, Vector3 p)
		{
			throw new NotImplementedException("Obsolete");
		}

		// Token: 0x0600224C RID: 8780 RVA: 0x00192AE0 File Offset: 0x00190CE0
		[Obsolete("Use Int3.GetHashCode instead", true)]
		public static int ComputeVertexHash(int x, int y, int z)
		{
			throw new NotImplementedException("Obsolete");
		}

		// Token: 0x0600224D RID: 8781 RVA: 0x00192AE0 File Offset: 0x00190CE0
		[Obsolete("Obsolete", true)]
		public static float Hermite(float start, float end, float value)
		{
			throw new NotImplementedException("Obsolete");
		}

		// Token: 0x0600224E RID: 8782 RVA: 0x00192AE0 File Offset: 0x00190CE0
		[Obsolete("Obsolete", true)]
		public static float MapToRange(float targetMin, float targetMax, float value)
		{
			throw new NotImplementedException("Obsolete");
		}

		// Token: 0x0600224F RID: 8783 RVA: 0x00192AE0 File Offset: 0x00190CE0
		[Obsolete("Obsolete", true)]
		public static string FormatBytes(int bytes)
		{
			throw new NotImplementedException("Obsolete");
		}

		// Token: 0x06002250 RID: 8784 RVA: 0x00192AE0 File Offset: 0x00190CE0
		[Obsolete("Obsolete", true)]
		public static float MagnitudeXZ(Vector3 a, Vector3 b)
		{
			throw new NotImplementedException("Obsolete");
		}

		// Token: 0x06002251 RID: 8785 RVA: 0x00192AE0 File Offset: 0x00190CE0
		[Obsolete("Obsolete", true)]
		public static int Repeat(int i, int n)
		{
			throw new NotImplementedException("Obsolete");
		}

		// Token: 0x06002252 RID: 8786 RVA: 0x00192AE0 File Offset: 0x00190CE0
		[Obsolete("Use Mathf.Abs instead", true)]
		public static float Abs(float a)
		{
			throw new NotImplementedException("Obsolete");
		}

		// Token: 0x06002253 RID: 8787 RVA: 0x00192AE0 File Offset: 0x00190CE0
		[Obsolete("Use Mathf.Abs instead", true)]
		public static int Abs(int a)
		{
			throw new NotImplementedException("Obsolete");
		}

		// Token: 0x06002254 RID: 8788 RVA: 0x00192AE0 File Offset: 0x00190CE0
		[Obsolete("Use Mathf.Min instead", true)]
		public static float Min(float a, float b)
		{
			throw new NotImplementedException("Obsolete");
		}

		// Token: 0x06002255 RID: 8789 RVA: 0x00192AE0 File Offset: 0x00190CE0
		[Obsolete("Use Mathf.Min instead", true)]
		public static int Min(int a, int b)
		{
			throw new NotImplementedException("Obsolete");
		}

		// Token: 0x06002256 RID: 8790 RVA: 0x00192AE0 File Offset: 0x00190CE0
		[Obsolete("Use Mathf.Min instead", true)]
		public static uint Min(uint a, uint b)
		{
			throw new NotImplementedException("Obsolete");
		}

		// Token: 0x06002257 RID: 8791 RVA: 0x00192AE0 File Offset: 0x00190CE0
		[Obsolete("Use Mathf.Max instead", true)]
		public static float Max(float a, float b)
		{
			throw new NotImplementedException("Obsolete");
		}

		// Token: 0x06002258 RID: 8792 RVA: 0x00192AE0 File Offset: 0x00190CE0
		[Obsolete("Use Mathf.Max instead", true)]
		public static int Max(int a, int b)
		{
			throw new NotImplementedException("Obsolete");
		}

		// Token: 0x06002259 RID: 8793 RVA: 0x00192AE0 File Offset: 0x00190CE0
		[Obsolete("Use Mathf.Max instead", true)]
		public static uint Max(uint a, uint b)
		{
			throw new NotImplementedException("Obsolete");
		}

		// Token: 0x0600225A RID: 8794 RVA: 0x00192AE0 File Offset: 0x00190CE0
		[Obsolete("Use Mathf.Max instead", true)]
		public static ushort Max(ushort a, ushort b)
		{
			throw new NotImplementedException("Obsolete");
		}

		// Token: 0x0600225B RID: 8795 RVA: 0x00192AE0 File Offset: 0x00190CE0
		[Obsolete("Use Mathf.Sign instead", true)]
		public static float Sign(float a)
		{
			throw new NotImplementedException("Obsolete");
		}

		// Token: 0x0600225C RID: 8796 RVA: 0x00192AE0 File Offset: 0x00190CE0
		[Obsolete("Use Mathf.Sign instead", true)]
		public static int Sign(int a)
		{
			throw new NotImplementedException("Obsolete");
		}

		// Token: 0x0600225D RID: 8797 RVA: 0x00192AE0 File Offset: 0x00190CE0
		[Obsolete("Use Mathf.Clamp instead", true)]
		public static float Clamp(float a, float b, float c)
		{
			throw new NotImplementedException("Obsolete");
		}

		// Token: 0x0600225E RID: 8798 RVA: 0x00192AE0 File Offset: 0x00190CE0
		[Obsolete("Use Mathf.Clamp instead", true)]
		public static int Clamp(int a, int b, int c)
		{
			throw new NotImplementedException("Obsolete");
		}

		// Token: 0x0600225F RID: 8799 RVA: 0x00192AE0 File Offset: 0x00190CE0
		[Obsolete("Use Mathf.Clamp01 instead", true)]
		public static float Clamp01(float a)
		{
			throw new NotImplementedException("Obsolete");
		}

		// Token: 0x06002260 RID: 8800 RVA: 0x00192AE0 File Offset: 0x00190CE0
		[Obsolete("Use Mathf.Clamp01 instead", true)]
		public static int Clamp01(int a)
		{
			throw new NotImplementedException("Obsolete");
		}

		// Token: 0x06002261 RID: 8801 RVA: 0x00192AE0 File Offset: 0x00190CE0
		[Obsolete("Use Mathf.Lerp instead", true)]
		public static float Lerp(float a, float b, float t)
		{
			throw new NotImplementedException("Obsolete");
		}

		// Token: 0x06002262 RID: 8802 RVA: 0x00192AE0 File Offset: 0x00190CE0
		[Obsolete("Use Mathf.RoundToInt instead", true)]
		public static int RoundToInt(float v)
		{
			throw new NotImplementedException("Obsolete");
		}

		// Token: 0x06002263 RID: 8803 RVA: 0x00192AE0 File Offset: 0x00190CE0
		[Obsolete("Use Mathf.RoundToInt instead", true)]
		public static int RoundToInt(double v)
		{
			throw new NotImplementedException("Obsolete");
		}
	}
}
