using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000536 RID: 1334
	public class GraphUpdateShape
	{
		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x060022A9 RID: 8873 RVA: 0x0019419C File Offset: 0x0019239C
		// (set) Token: 0x060022AA RID: 8874 RVA: 0x001941A4 File Offset: 0x001923A4
		public Vector3[] points
		{
			get
			{
				return this._points;
			}
			set
			{
				this._points = value;
				if (this.convex)
				{
					this.CalculateConvexHull();
				}
			}
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x060022AB RID: 8875 RVA: 0x001941BB File Offset: 0x001923BB
		// (set) Token: 0x060022AC RID: 8876 RVA: 0x001941C3 File Offset: 0x001923C3
		public bool convex
		{
			get
			{
				return this._convex;
			}
			set
			{
				if (this._convex != value && value)
				{
					this.CalculateConvexHull();
				}
				this._convex = value;
			}
		}

		// Token: 0x060022AD RID: 8877 RVA: 0x001941E2 File Offset: 0x001923E2
		public GraphUpdateShape()
		{
		}

		// Token: 0x060022AE RID: 8878 RVA: 0x0019420C File Offset: 0x0019240C
		public GraphUpdateShape(Vector3[] points, bool convex, Matrix4x4 matrix, float minimumHeight)
		{
			this.convex = convex;
			this.points = points;
			this.origin = matrix.MultiplyPoint3x4(Vector3.zero);
			this.right = matrix.MultiplyPoint3x4(Vector3.right) - this.origin;
			this.up = matrix.MultiplyPoint3x4(Vector3.up) - this.origin;
			this.forward = matrix.MultiplyPoint3x4(Vector3.forward) - this.origin;
			this.minimumHeight = minimumHeight;
		}

		// Token: 0x060022AF RID: 8879 RVA: 0x001942BF File Offset: 0x001924BF
		private void CalculateConvexHull()
		{
			this._convexPoints = ((this.points != null) ? Polygon.ConvexHullXZ(this.points) : null);
		}

		// Token: 0x060022B0 RID: 8880 RVA: 0x001942DD File Offset: 0x001924DD
		public Bounds GetBounds()
		{
			return GraphUpdateShape.GetBounds(this.convex ? this._convexPoints : this.points, this.right, this.up, this.forward, this.origin, this.minimumHeight);
		}

		// Token: 0x060022B1 RID: 8881 RVA: 0x00194318 File Offset: 0x00192518
		public static Bounds GetBounds(Vector3[] points, Matrix4x4 matrix, float minimumHeight)
		{
			Vector3 b = matrix.MultiplyPoint3x4(Vector3.zero);
			Vector3 vector = matrix.MultiplyPoint3x4(Vector3.right) - b;
			Vector3 vector2 = matrix.MultiplyPoint3x4(Vector3.up) - b;
			Vector3 vector3 = matrix.MultiplyPoint3x4(Vector3.forward) - b;
			return GraphUpdateShape.GetBounds(points, vector, vector2, vector3, b, minimumHeight);
		}

		// Token: 0x060022B2 RID: 8882 RVA: 0x00194378 File Offset: 0x00192578
		private static Bounds GetBounds(Vector3[] points, Vector3 right, Vector3 up, Vector3 forward, Vector3 origin, float minimumHeight)
		{
			if (points == null || points.Length == 0)
			{
				return default(Bounds);
			}
			float num = points[0].y;
			float num2 = points[0].y;
			for (int i = 0; i < points.Length; i++)
			{
				num = Mathf.Min(num, points[i].y);
				num2 = Mathf.Max(num2, points[i].y);
			}
			float num3 = Mathf.Max(minimumHeight - (num2 - num), 0f) * 0.5f;
			num -= num3;
			num2 += num3;
			Vector3 vector = right * points[0].x + up * points[0].y + forward * points[0].z;
			Vector3 vector2 = vector;
			for (int j = 0; j < points.Length; j++)
			{
				Vector3 a = right * points[j].x + forward * points[j].z;
				Vector3 rhs = a + up * num;
				Vector3 rhs2 = a + up * num2;
				vector = Vector3.Min(vector, rhs);
				vector = Vector3.Min(vector, rhs2);
				vector2 = Vector3.Max(vector2, rhs);
				vector2 = Vector3.Max(vector2, rhs2);
			}
			return new Bounds((vector + vector2) * 0.5f + origin, vector2 - vector);
		}

		// Token: 0x060022B3 RID: 8883 RVA: 0x001944F6 File Offset: 0x001926F6
		public bool Contains(GraphNode node)
		{
			return this.Contains((Vector3)node.position);
		}

		// Token: 0x060022B4 RID: 8884 RVA: 0x0019450C File Offset: 0x0019270C
		public bool Contains(Vector3 point)
		{
			point -= this.origin;
			Vector3 p = new Vector3(Vector3.Dot(point, this.right) / this.right.sqrMagnitude, 0f, Vector3.Dot(point, this.forward) / this.forward.sqrMagnitude);
			if (!this.convex)
			{
				return this._points != null && Polygon.ContainsPointXZ(this._points, p);
			}
			if (this._convexPoints == null)
			{
				return false;
			}
			int i = 0;
			int num = this._convexPoints.Length - 1;
			while (i < this._convexPoints.Length)
			{
				if (VectorMath.RightOrColinearXZ(this._convexPoints[i], this._convexPoints[num], p))
				{
					return false;
				}
				num = i;
				i++;
			}
			return true;
		}

		// Token: 0x04003FA3 RID: 16291
		private Vector3[] _points;

		// Token: 0x04003FA4 RID: 16292
		private Vector3[] _convexPoints;

		// Token: 0x04003FA5 RID: 16293
		private bool _convex;

		// Token: 0x04003FA6 RID: 16294
		private Vector3 right = Vector3.right;

		// Token: 0x04003FA7 RID: 16295
		private Vector3 forward = Vector3.forward;

		// Token: 0x04003FA8 RID: 16296
		private Vector3 up = Vector3.up;

		// Token: 0x04003FA9 RID: 16297
		private Vector3 origin;

		// Token: 0x04003FAA RID: 16298
		public float minimumHeight;
	}
}
