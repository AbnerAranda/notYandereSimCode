using System;
using System.Collections.Generic;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000534 RID: 1332
	public static class Polygon
	{
		// Token: 0x06002264 RID: 8804 RVA: 0x00192AEC File Offset: 0x00190CEC
		[Obsolete("Use VectorMath.SignedTriangleAreaTimes2XZ instead")]
		public static long TriangleArea2(Int3 a, Int3 b, Int3 c)
		{
			return VectorMath.SignedTriangleAreaTimes2XZ(a, b, c);
		}

		// Token: 0x06002265 RID: 8805 RVA: 0x00192AF6 File Offset: 0x00190CF6
		[Obsolete("Use VectorMath.SignedTriangleAreaTimes2XZ instead")]
		public static float TriangleArea2(Vector3 a, Vector3 b, Vector3 c)
		{
			return VectorMath.SignedTriangleAreaTimes2XZ(a, b, c);
		}

		// Token: 0x06002266 RID: 8806 RVA: 0x00192B00 File Offset: 0x00190D00
		[Obsolete("Use TriangleArea2 instead to avoid confusion regarding the factor 2")]
		public static long TriangleArea(Int3 a, Int3 b, Int3 c)
		{
			return Polygon.TriangleArea2(a, b, c);
		}

		// Token: 0x06002267 RID: 8807 RVA: 0x00192B0A File Offset: 0x00190D0A
		[Obsolete("Use TriangleArea2 instead to avoid confusion regarding the factor 2")]
		public static float TriangleArea(Vector3 a, Vector3 b, Vector3 c)
		{
			return Polygon.TriangleArea2(a, b, c);
		}

		// Token: 0x06002268 RID: 8808 RVA: 0x00192B14 File Offset: 0x00190D14
		[Obsolete("Use ContainsPointXZ instead")]
		public static bool ContainsPoint(Vector3 a, Vector3 b, Vector3 c, Vector3 p)
		{
			return Polygon.ContainsPointXZ(a, b, c, p);
		}

		// Token: 0x06002269 RID: 8809 RVA: 0x00192B1F File Offset: 0x00190D1F
		public static bool ContainsPointXZ(Vector3 a, Vector3 b, Vector3 c, Vector3 p)
		{
			return VectorMath.IsClockwiseMarginXZ(a, b, p) && VectorMath.IsClockwiseMarginXZ(b, c, p) && VectorMath.IsClockwiseMarginXZ(c, a, p);
		}

		// Token: 0x0600226A RID: 8810 RVA: 0x00192B3F File Offset: 0x00190D3F
		[Obsolete("Use ContainsPointXZ instead")]
		public static bool ContainsPoint(Int3 a, Int3 b, Int3 c, Int3 p)
		{
			return Polygon.ContainsPointXZ(a, b, c, p);
		}

		// Token: 0x0600226B RID: 8811 RVA: 0x00192B4A File Offset: 0x00190D4A
		public static bool ContainsPointXZ(Int3 a, Int3 b, Int3 c, Int3 p)
		{
			return VectorMath.IsClockwiseOrColinearXZ(a, b, p) && VectorMath.IsClockwiseOrColinearXZ(b, c, p) && VectorMath.IsClockwiseOrColinearXZ(c, a, p);
		}

		// Token: 0x0600226C RID: 8812 RVA: 0x00192B6A File Offset: 0x00190D6A
		public static bool ContainsPoint(Int2 a, Int2 b, Int2 c, Int2 p)
		{
			return VectorMath.IsClockwiseOrColinear(a, b, p) && VectorMath.IsClockwiseOrColinear(b, c, p) && VectorMath.IsClockwiseOrColinear(c, a, p);
		}

		// Token: 0x0600226D RID: 8813 RVA: 0x00192B8A File Offset: 0x00190D8A
		[Obsolete("Use ContainsPointXZ instead")]
		public static bool ContainsPoint(Vector3[] polyPoints, Vector3 p)
		{
			return Polygon.ContainsPointXZ(polyPoints, p);
		}

		// Token: 0x0600226E RID: 8814 RVA: 0x00192B94 File Offset: 0x00190D94
		public static bool ContainsPoint(Vector2[] polyPoints, Vector2 p)
		{
			int num = polyPoints.Length - 1;
			bool flag = false;
			int i = 0;
			while (i < polyPoints.Length)
			{
				if (((polyPoints[i].y <= p.y && p.y < polyPoints[num].y) || (polyPoints[num].y <= p.y && p.y < polyPoints[i].y)) && p.x < (polyPoints[num].x - polyPoints[i].x) * (p.y - polyPoints[i].y) / (polyPoints[num].y - polyPoints[i].y) + polyPoints[i].x)
				{
					flag = !flag;
				}
				num = i++;
			}
			return flag;
		}

		// Token: 0x0600226F RID: 8815 RVA: 0x00192C74 File Offset: 0x00190E74
		public static bool ContainsPointXZ(Vector3[] polyPoints, Vector3 p)
		{
			int num = polyPoints.Length - 1;
			bool flag = false;
			int i = 0;
			while (i < polyPoints.Length)
			{
				if (((polyPoints[i].z <= p.z && p.z < polyPoints[num].z) || (polyPoints[num].z <= p.z && p.z < polyPoints[i].z)) && p.x < (polyPoints[num].x - polyPoints[i].x) * (p.z - polyPoints[i].z) / (polyPoints[num].z - polyPoints[i].z) + polyPoints[i].x)
				{
					flag = !flag;
				}
				num = i++;
			}
			return flag;
		}

		// Token: 0x06002270 RID: 8816 RVA: 0x00192D54 File Offset: 0x00190F54
		public static int SampleYCoordinateInTriangle(Int3 p1, Int3 p2, Int3 p3, Int3 p)
		{
			double num = (double)(p2.z - p3.z) * (double)(p1.x - p3.x) + (double)(p3.x - p2.x) * (double)(p1.z - p3.z);
			double num2 = ((double)(p2.z - p3.z) * (double)(p.x - p3.x) + (double)(p3.x - p2.x) * (double)(p.z - p3.z)) / num;
			double num3 = ((double)(p3.z - p1.z) * (double)(p.x - p3.x) + (double)(p1.x - p3.x) * (double)(p.z - p3.z)) / num;
			return (int)Math.Round(num2 * (double)p1.y + num3 * (double)p2.y + (1.0 - num2 - num3) * (double)p3.y);
		}

		// Token: 0x06002271 RID: 8817 RVA: 0x00192E48 File Offset: 0x00191048
		[Obsolete("Use VectorMath.RightXZ instead. Note that it now uses a left handed coordinate system (same as Unity)")]
		public static bool LeftNotColinear(Vector3 a, Vector3 b, Vector3 p)
		{
			return VectorMath.RightXZ(a, b, p);
		}

		// Token: 0x06002272 RID: 8818 RVA: 0x00192E52 File Offset: 0x00191052
		[Obsolete("Use VectorMath.RightOrColinearXZ instead. Note that it now uses a left handed coordinate system (same as Unity)")]
		public static bool Left(Vector3 a, Vector3 b, Vector3 p)
		{
			return VectorMath.RightOrColinearXZ(a, b, p);
		}

		// Token: 0x06002273 RID: 8819 RVA: 0x00192E5C File Offset: 0x0019105C
		[Obsolete("Use VectorMath.RightOrColinear instead. Note that it now uses a left handed coordinate system (same as Unity)")]
		public static bool Left(Vector2 a, Vector2 b, Vector2 p)
		{
			return VectorMath.RightOrColinear(a, b, p);
		}

		// Token: 0x06002274 RID: 8820 RVA: 0x00191B54 File Offset: 0x0018FD54
		[Obsolete("Use VectorMath.RightOrColinearXZ instead. Note that it now uses a left handed coordinate system (same as Unity)")]
		public static bool Left(Int3 a, Int3 b, Int3 p)
		{
			return VectorMath.RightOrColinearXZ(a, b, p);
		}

		// Token: 0x06002275 RID: 8821 RVA: 0x00191B4A File Offset: 0x0018FD4A
		[Obsolete("Use VectorMath.RightXZ instead. Note that it now uses a left handed coordinate system (same as Unity)")]
		public static bool LeftNotColinear(Int3 a, Int3 b, Int3 p)
		{
			return VectorMath.RightXZ(a, b, p);
		}

		// Token: 0x06002276 RID: 8822 RVA: 0x00191B5E File Offset: 0x0018FD5E
		[Obsolete("Use VectorMath.RightOrColinear instead. Note that it now uses a left handed coordinate system (same as Unity)")]
		public static bool Left(Int2 a, Int2 b, Int2 p)
		{
			return VectorMath.RightOrColinear(a, b, p);
		}

		// Token: 0x06002277 RID: 8823 RVA: 0x00192E66 File Offset: 0x00191066
		[Obsolete("Use VectorMath.IsClockwiseMarginXZ instead")]
		public static bool IsClockwiseMargin(Vector3 a, Vector3 b, Vector3 c)
		{
			return VectorMath.IsClockwiseMarginXZ(a, b, c);
		}

		// Token: 0x06002278 RID: 8824 RVA: 0x00192E70 File Offset: 0x00191070
		[Obsolete("Use VectorMath.IsClockwiseXZ instead")]
		public static bool IsClockwise(Vector3 a, Vector3 b, Vector3 c)
		{
			return VectorMath.IsClockwiseXZ(a, b, c);
		}

		// Token: 0x06002279 RID: 8825 RVA: 0x00192E7A File Offset: 0x0019107A
		[Obsolete("Use VectorMath.IsClockwiseXZ instead")]
		public static bool IsClockwise(Int3 a, Int3 b, Int3 c)
		{
			return VectorMath.IsClockwiseXZ(a, b, c);
		}

		// Token: 0x0600227A RID: 8826 RVA: 0x00192E84 File Offset: 0x00191084
		[Obsolete("Use VectorMath.IsClockwiseOrColinearXZ instead")]
		public static bool IsClockwiseMargin(Int3 a, Int3 b, Int3 c)
		{
			return VectorMath.IsClockwiseOrColinearXZ(a, b, c);
		}

		// Token: 0x0600227B RID: 8827 RVA: 0x00192E8E File Offset: 0x0019108E
		[Obsolete("Use VectorMath.IsClockwiseOrColinear instead")]
		public static bool IsClockwiseMargin(Int2 a, Int2 b, Int2 c)
		{
			return VectorMath.IsClockwiseOrColinear(a, b, c);
		}

		// Token: 0x0600227C RID: 8828 RVA: 0x00192E98 File Offset: 0x00191098
		[Obsolete("Use VectorMath.IsColinearXZ instead")]
		public static bool IsColinear(Int3 a, Int3 b, Int3 c)
		{
			return VectorMath.IsColinearXZ(a, b, c);
		}

		// Token: 0x0600227D RID: 8829 RVA: 0x00192EA2 File Offset: 0x001910A2
		[Obsolete("Use VectorMath.IsColinearAlmostXZ instead")]
		public static bool IsColinearAlmost(Int3 a, Int3 b, Int3 c)
		{
			return VectorMath.IsColinearAlmostXZ(a, b, c);
		}

		// Token: 0x0600227E RID: 8830 RVA: 0x00192EAC File Offset: 0x001910AC
		[Obsolete("Use VectorMath.IsColinearXZ instead")]
		public static bool IsColinear(Vector3 a, Vector3 b, Vector3 c)
		{
			return VectorMath.IsColinearXZ(a, b, c);
		}

		// Token: 0x0600227F RID: 8831 RVA: 0x00192EB6 File Offset: 0x001910B6
		[Obsolete("Marked for removal since it is not used by any part of the A* Pathfinding Project")]
		public static bool IntersectsUnclamped(Vector3 a, Vector3 b, Vector3 a2, Vector3 b2)
		{
			return VectorMath.RightOrColinearXZ(a, b, a2) != VectorMath.RightOrColinearXZ(a, b, b2);
		}

		// Token: 0x06002280 RID: 8832 RVA: 0x00192ECD File Offset: 0x001910CD
		[Obsolete("Use VectorMath.SegmentsIntersect instead")]
		public static bool Intersects(Int2 start1, Int2 end1, Int2 start2, Int2 end2)
		{
			return VectorMath.SegmentsIntersect(start1, end1, start2, end2);
		}

		// Token: 0x06002281 RID: 8833 RVA: 0x00192ED8 File Offset: 0x001910D8
		[Obsolete("Use VectorMath.SegmentsIntersectXZ instead")]
		public static bool Intersects(Int3 start1, Int3 end1, Int3 start2, Int3 end2)
		{
			return VectorMath.SegmentsIntersectXZ(start1, end1, start2, end2);
		}

		// Token: 0x06002282 RID: 8834 RVA: 0x00192EE3 File Offset: 0x001910E3
		[Obsolete("Use VectorMath.SegmentsIntersectXZ instead")]
		public static bool Intersects(Vector3 start1, Vector3 end1, Vector3 start2, Vector3 end2)
		{
			return VectorMath.SegmentsIntersectXZ(start1, end1, start2, end2);
		}

		// Token: 0x06002283 RID: 8835 RVA: 0x00192EEE File Offset: 0x001910EE
		[Obsolete("Use VectorMath.LineDirIntersectionPointXZ instead")]
		public static Vector3 IntersectionPointOptimized(Vector3 start1, Vector3 dir1, Vector3 start2, Vector3 dir2)
		{
			return VectorMath.LineDirIntersectionPointXZ(start1, dir1, start2, dir2);
		}

		// Token: 0x06002284 RID: 8836 RVA: 0x00192EF9 File Offset: 0x001910F9
		[Obsolete("Use VectorMath.LineDirIntersectionPointXZ instead")]
		public static Vector3 IntersectionPointOptimized(Vector3 start1, Vector3 dir1, Vector3 start2, Vector3 dir2, out bool intersects)
		{
			return VectorMath.LineDirIntersectionPointXZ(start1, dir1, start2, dir2, out intersects);
		}

		// Token: 0x06002285 RID: 8837 RVA: 0x00192F06 File Offset: 0x00191106
		[Obsolete("Use VectorMath.RaySegmentIntersectXZ instead")]
		public static bool IntersectionFactorRaySegment(Int3 start1, Int3 end1, Int3 start2, Int3 end2)
		{
			return VectorMath.RaySegmentIntersectXZ(start1, end1, start2, end2);
		}

		// Token: 0x06002286 RID: 8838 RVA: 0x00192F11 File Offset: 0x00191111
		[Obsolete("Use VectorMath.LineIntersectionFactorXZ instead")]
		public static bool IntersectionFactor(Int3 start1, Int3 end1, Int3 start2, Int3 end2, out float factor1, out float factor2)
		{
			return VectorMath.LineIntersectionFactorXZ(start1, end1, start2, end2, out factor1, out factor2);
		}

		// Token: 0x06002287 RID: 8839 RVA: 0x00192F20 File Offset: 0x00191120
		[Obsolete("Use VectorMath.LineIntersectionFactorXZ instead")]
		public static bool IntersectionFactor(Vector3 start1, Vector3 end1, Vector3 start2, Vector3 end2, out float factor1, out float factor2)
		{
			return VectorMath.LineIntersectionFactorXZ(start1, end1, start2, end2, out factor1, out factor2);
		}

		// Token: 0x06002288 RID: 8840 RVA: 0x00192F2F File Offset: 0x0019112F
		[Obsolete("Use VectorMath.LineRayIntersectionFactorXZ instead")]
		public static float IntersectionFactorRay(Int3 start1, Int3 end1, Int3 start2, Int3 end2)
		{
			return VectorMath.LineRayIntersectionFactorXZ(start1, end1, start2, end2);
		}

		// Token: 0x06002289 RID: 8841 RVA: 0x00192F3A File Offset: 0x0019113A
		[Obsolete("Use VectorMath.LineIntersectionFactorXZ instead")]
		public static float IntersectionFactor(Vector3 start1, Vector3 end1, Vector3 start2, Vector3 end2)
		{
			return VectorMath.LineIntersectionFactorXZ(start1, end1, start2, end2);
		}

		// Token: 0x0600228A RID: 8842 RVA: 0x00192F45 File Offset: 0x00191145
		[Obsolete("Use VectorMath.LineIntersectionPointXZ instead")]
		public static Vector3 IntersectionPoint(Vector3 start1, Vector3 end1, Vector3 start2, Vector3 end2)
		{
			return VectorMath.LineIntersectionPointXZ(start1, end1, start2, end2);
		}

		// Token: 0x0600228B RID: 8843 RVA: 0x00192F50 File Offset: 0x00191150
		[Obsolete("Use VectorMath.LineIntersectionPointXZ instead")]
		public static Vector3 IntersectionPoint(Vector3 start1, Vector3 end1, Vector3 start2, Vector3 end2, out bool intersects)
		{
			return VectorMath.LineIntersectionPointXZ(start1, end1, start2, end2, out intersects);
		}

		// Token: 0x0600228C RID: 8844 RVA: 0x00192F5D File Offset: 0x0019115D
		[Obsolete("Use VectorMath.LineIntersectionPoint instead")]
		public static Vector2 IntersectionPoint(Vector2 start1, Vector2 end1, Vector2 start2, Vector2 end2)
		{
			return VectorMath.LineIntersectionPoint(start1, end1, start2, end2);
		}

		// Token: 0x0600228D RID: 8845 RVA: 0x00192F68 File Offset: 0x00191168
		[Obsolete("Use VectorMath.LineIntersectionPoint instead")]
		public static Vector2 IntersectionPoint(Vector2 start1, Vector2 end1, Vector2 start2, Vector2 end2, out bool intersects)
		{
			return VectorMath.LineIntersectionPoint(start1, end1, start2, end2, out intersects);
		}

		// Token: 0x0600228E RID: 8846 RVA: 0x00192F75 File Offset: 0x00191175
		[Obsolete("Use VectorMath.SegmentIntersectionPointXZ instead")]
		public static Vector3 SegmentIntersectionPoint(Vector3 start1, Vector3 end1, Vector3 start2, Vector3 end2, out bool intersects)
		{
			return VectorMath.SegmentIntersectionPointXZ(start1, end1, start2, end2, out intersects);
		}

		// Token: 0x0600228F RID: 8847 RVA: 0x00192F82 File Offset: 0x00191182
		[Obsolete("Use ConvexHullXZ instead")]
		public static Vector3[] ConvexHull(Vector3[] points)
		{
			return Polygon.ConvexHullXZ(points);
		}

		// Token: 0x06002290 RID: 8848 RVA: 0x00192F8C File Offset: 0x0019118C
		public static Vector3[] ConvexHullXZ(Vector3[] points)
		{
			if (points.Length == 0)
			{
				return new Vector3[0];
			}
			List<Vector3> list = ListPool<Vector3>.Claim();
			int num = 0;
			for (int i = 1; i < points.Length; i++)
			{
				if (points[i].x < points[num].x)
				{
					num = i;
				}
			}
			int num2 = num;
			int num3 = 0;
			for (;;)
			{
				list.Add(points[num]);
				int num4 = 0;
				for (int j = 0; j < points.Length; j++)
				{
					if (num4 == num || !VectorMath.RightOrColinearXZ(points[num], points[num4], points[j]))
					{
						num4 = j;
					}
				}
				num = num4;
				num3++;
				if (num3 > 10000)
				{
					break;
				}
				if (num == num2)
				{
					goto IL_AF;
				}
			}
			Debug.LogWarning("Infinite Loop in Convex Hull Calculation");
			IL_AF:
			Vector3[] result = list.ToArray();
			ListPool<Vector3>.Release(list);
			return result;
		}

		// Token: 0x06002291 RID: 8849 RVA: 0x00193058 File Offset: 0x00191258
		[Obsolete("Use VectorMath.SegmentIntersectsBounds instead")]
		public static bool LineIntersectsBounds(Bounds bounds, Vector3 a, Vector3 b)
		{
			return VectorMath.SegmentIntersectsBounds(bounds, a, b);
		}

		// Token: 0x06002292 RID: 8850 RVA: 0x00193062 File Offset: 0x00191262
		[Obsolete("Scheduled for removal since it is not used by any part of the A* Pathfinding Project")]
		public static Vector3 ClosestPointOnTriangle(Vector3[] triangle, Vector3 point)
		{
			return Polygon.ClosestPointOnTriangle(triangle[0], triangle[1], triangle[2], point);
		}

		// Token: 0x06002293 RID: 8851 RVA: 0x00193080 File Offset: 0x00191280
		public static Vector2 ClosestPointOnTriangle(Vector2 a, Vector2 b, Vector2 c, Vector2 p)
		{
			Vector2 vector = b - a;
			Vector2 vector2 = c - a;
			Vector2 rhs = p - a;
			float num = Vector2.Dot(vector, rhs);
			float num2 = Vector2.Dot(vector2, rhs);
			if (num <= 0f && num2 <= 0f)
			{
				return a;
			}
			Vector2 rhs2 = p - b;
			float num3 = Vector2.Dot(vector, rhs2);
			float num4 = Vector2.Dot(vector2, rhs2);
			if (num3 >= 0f && num4 <= num3)
			{
				return b;
			}
			if (num >= 0f && num3 <= 0f && num * num4 - num3 * num2 <= 0f)
			{
				float d = num / (num - num3);
				return a + vector * d;
			}
			Vector2 rhs3 = p - c;
			float num5 = Vector2.Dot(vector, rhs3);
			float num6 = Vector2.Dot(vector2, rhs3);
			if (num6 >= 0f && num5 <= num6)
			{
				return c;
			}
			if (num2 >= 0f && num6 <= 0f && num5 * num2 - num * num6 <= 0f)
			{
				float d2 = num2 / (num2 - num6);
				return a + vector2 * d2;
			}
			if (num4 - num3 >= 0f && num5 - num6 >= 0f && num3 * num6 - num5 * num4 <= 0f)
			{
				float d3 = (num4 - num3) / (num4 - num3 + (num5 - num6));
				return b + (c - b) * d3;
			}
			return p;
		}

		// Token: 0x06002294 RID: 8852 RVA: 0x001931F0 File Offset: 0x001913F0
		public static Vector3 ClosestPointOnTriangleXZ(Vector3 a, Vector3 b, Vector3 c, Vector3 p)
		{
			Vector2 lhs = new Vector2(b.x - a.x, b.z - a.z);
			Vector2 lhs2 = new Vector2(c.x - a.x, c.z - a.z);
			Vector2 rhs = new Vector2(p.x - a.x, p.z - a.z);
			float num = Vector2.Dot(lhs, rhs);
			float num2 = Vector2.Dot(lhs2, rhs);
			if (num <= 0f && num2 <= 0f)
			{
				return a;
			}
			Vector2 rhs2 = new Vector2(p.x - b.x, p.z - b.z);
			float num3 = Vector2.Dot(lhs, rhs2);
			float num4 = Vector2.Dot(lhs2, rhs2);
			if (num3 >= 0f && num4 <= num3)
			{
				return b;
			}
			float num5 = num * num4 - num3 * num2;
			if (num >= 0f && num3 <= 0f && num5 <= 0f)
			{
				float num6 = num / (num - num3);
				return (1f - num6) * a + num6 * b;
			}
			Vector2 rhs3 = new Vector2(p.x - c.x, p.z - c.z);
			float num7 = Vector2.Dot(lhs, rhs3);
			float num8 = Vector2.Dot(lhs2, rhs3);
			if (num8 >= 0f && num7 <= num8)
			{
				return c;
			}
			float num9 = num7 * num2 - num * num8;
			if (num2 >= 0f && num8 <= 0f && num9 <= 0f)
			{
				float num10 = num2 / (num2 - num8);
				return (1f - num10) * a + num10 * c;
			}
			float num11 = num3 * num8 - num7 * num4;
			if (num4 - num3 >= 0f && num7 - num8 >= 0f && num11 <= 0f)
			{
				float d = (num4 - num3) / (num4 - num3 + (num7 - num8));
				return b + (c - b) * d;
			}
			float num12 = 1f / (num11 + num9 + num5);
			float num13 = num9 * num12;
			float num14 = num5 * num12;
			return new Vector3(p.x, (1f - num13 - num14) * a.y + num13 * b.y + num14 * c.y, p.z);
		}

		// Token: 0x06002295 RID: 8853 RVA: 0x00193454 File Offset: 0x00191654
		public static Vector3 ClosestPointOnTriangle(Vector3 a, Vector3 b, Vector3 c, Vector3 p)
		{
			Vector3 vector = b - a;
			Vector3 vector2 = c - a;
			Vector3 rhs = p - a;
			float num = Vector3.Dot(vector, rhs);
			float num2 = Vector3.Dot(vector2, rhs);
			if (num <= 0f && num2 <= 0f)
			{
				return a;
			}
			Vector3 rhs2 = p - b;
			float num3 = Vector3.Dot(vector, rhs2);
			float num4 = Vector3.Dot(vector2, rhs2);
			if (num3 >= 0f && num4 <= num3)
			{
				return b;
			}
			float num5 = num * num4 - num3 * num2;
			if (num >= 0f && num3 <= 0f && num5 <= 0f)
			{
				float d = num / (num - num3);
				return a + vector * d;
			}
			Vector3 rhs3 = p - c;
			float num6 = Vector3.Dot(vector, rhs3);
			float num7 = Vector3.Dot(vector2, rhs3);
			if (num7 >= 0f && num6 <= num7)
			{
				return c;
			}
			float num8 = num6 * num2 - num * num7;
			if (num2 >= 0f && num7 <= 0f && num8 <= 0f)
			{
				float d2 = num2 / (num2 - num7);
				return a + vector2 * d2;
			}
			float num9 = num3 * num7 - num6 * num4;
			if (num4 - num3 >= 0f && num6 - num7 >= 0f && num9 <= 0f)
			{
				float d3 = (num4 - num3) / (num4 - num3 + (num6 - num7));
				return b + (c - b) * d3;
			}
			float num10 = 1f / (num9 + num8 + num5);
			float d4 = num8 * num10;
			float d5 = num5 * num10;
			return a + vector * d4 + vector2 * d5;
		}

		// Token: 0x06002296 RID: 8854 RVA: 0x00193605 File Offset: 0x00191805
		[Obsolete("Use VectorMath.SqrDistanceSegmentSegment instead")]
		public static float DistanceSegmentSegment3D(Vector3 s1, Vector3 e1, Vector3 s2, Vector3 e2)
		{
			return VectorMath.SqrDistanceSegmentSegment(s1, e1, s2, e2);
		}

		// Token: 0x06002297 RID: 8855 RVA: 0x00193610 File Offset: 0x00191810
		public static void CompressMesh(List<Int3> vertices, List<int> triangles, out Int3[] outVertices, out int[] outTriangles)
		{
			Dictionary<Int3, int> dictionary = Polygon.cached_Int3_int_dict;
			dictionary.Clear();
			int[] array = ArrayPool<int>.Claim(vertices.Count);
			int num = 0;
			for (int i = 0; i < vertices.Count; i++)
			{
				int num2;
				if (!dictionary.TryGetValue(vertices[i], out num2) && !dictionary.TryGetValue(vertices[i] + new Int3(0, 1, 0), out num2) && !dictionary.TryGetValue(vertices[i] + new Int3(0, -1, 0), out num2))
				{
					dictionary.Add(vertices[i], num);
					array[i] = num;
					vertices[num] = vertices[i];
					num++;
				}
				else
				{
					array[i] = num2;
				}
			}
			outTriangles = new int[triangles.Count];
			for (int j = 0; j < outTriangles.Length; j++)
			{
				outTriangles[j] = array[triangles[j]];
			}
			outVertices = new Int3[num];
			for (int k = 0; k < num; k++)
			{
				outVertices[k] = vertices[k];
			}
			ArrayPool<int>.Release(ref array, false);
		}

		// Token: 0x06002298 RID: 8856 RVA: 0x00193724 File Offset: 0x00191924
		public static void TraceContours(Dictionary<int, int> outline, HashSet<int> hasInEdge, Action<List<int>, bool> results)
		{
			List<int> list = ListPool<int>.Claim();
			List<int> list2 = ListPool<int>.Claim();
			list2.AddRange(outline.Keys);
			for (int i = 0; i <= 1; i++)
			{
				bool flag = i == 1;
				for (int j = 0; j < list2.Count; j++)
				{
					int num = list2[j];
					if (flag || !hasInEdge.Contains(num))
					{
						int num2 = num;
						list.Clear();
						list.Add(num2);
						while (outline.ContainsKey(num2))
						{
							int num3 = outline[num2];
							outline.Remove(num2);
							list.Add(num3);
							if (num3 == num)
							{
								break;
							}
							num2 = num3;
						}
						if (list.Count > 1)
						{
							results(list, flag);
						}
					}
				}
			}
			ListPool<int>.Release(ref list2);
			ListPool<int>.Release(ref list);
		}

		// Token: 0x06002299 RID: 8857 RVA: 0x001937F0 File Offset: 0x001919F0
		public static void Subdivide(List<Vector3> points, List<Vector3> result, int subSegments)
		{
			for (int i = 0; i < points.Count - 1; i++)
			{
				for (int j = 0; j < subSegments; j++)
				{
					result.Add(Vector3.Lerp(points[i], points[i + 1], (float)j / (float)subSegments));
				}
			}
			result.Add(points[points.Count - 1]);
		}

		// Token: 0x04003F8F RID: 16271
		private static readonly Dictionary<Int3, int> cached_Int3_int_dict = new Dictionary<Int3, int>();
	}
}
