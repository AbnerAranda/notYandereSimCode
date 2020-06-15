using System;
using UnityEngine;

namespace Pathfinding.Util
{
	// Token: 0x020005F0 RID: 1520
	public class GraphTransform : IMovementPlane, ITransform
	{
		// Token: 0x0600298B RID: 10635 RVA: 0x001C19F4 File Offset: 0x001BFBF4
		public GraphTransform(Matrix4x4 matrix)
		{
			this.matrix = matrix;
			this.inverseMatrix = matrix.inverse;
			this.identity = matrix.isIdentity;
			this.onlyTranslational = GraphTransform.MatrixIsTranslational(matrix);
			this.up = matrix.MultiplyVector(Vector3.up).normalized;
			this.translation = matrix.MultiplyPoint3x4(Vector3.zero);
			this.i3translation = (Int3)this.translation;
			this.rotation = Quaternion.LookRotation(this.TransformVector(Vector3.forward), this.TransformVector(Vector3.up));
			this.inverseRotation = Quaternion.Inverse(this.rotation);
			this.isXY = (this.rotation == Quaternion.Euler(-90f, 0f, 0f));
			this.isXZ = (this.rotation == Quaternion.Euler(0f, 0f, 0f));
		}

		// Token: 0x0600298C RID: 10636 RVA: 0x001C1AED File Offset: 0x001BFCED
		public Vector3 WorldUpAtGraphPosition(Vector3 point)
		{
			return this.up;
		}

		// Token: 0x0600298D RID: 10637 RVA: 0x001C1AF8 File Offset: 0x001BFCF8
		private static bool MatrixIsTranslational(Matrix4x4 matrix)
		{
			return matrix.GetColumn(0) == new Vector4(1f, 0f, 0f, 0f) && matrix.GetColumn(1) == new Vector4(0f, 1f, 0f, 0f) && matrix.GetColumn(2) == new Vector4(0f, 0f, 1f, 0f) && matrix.m33 == 1f;
		}

		// Token: 0x0600298E RID: 10638 RVA: 0x001C1B8C File Offset: 0x001BFD8C
		public Vector3 Transform(Vector3 point)
		{
			if (this.onlyTranslational)
			{
				return point + this.translation;
			}
			return this.matrix.MultiplyPoint3x4(point);
		}

		// Token: 0x0600298F RID: 10639 RVA: 0x001C1BC0 File Offset: 0x001BFDC0
		public Vector3 TransformVector(Vector3 point)
		{
			if (this.onlyTranslational)
			{
				return point;
			}
			return this.matrix.MultiplyVector(point);
		}

		// Token: 0x06002990 RID: 10640 RVA: 0x001C1BE8 File Offset: 0x001BFDE8
		public void Transform(Int3[] arr)
		{
			if (this.onlyTranslational)
			{
				for (int i = arr.Length - 1; i >= 0; i--)
				{
					arr[i] += this.i3translation;
				}
				return;
			}
			for (int j = arr.Length - 1; j >= 0; j--)
			{
				arr[j] = (Int3)this.matrix.MultiplyPoint3x4((Vector3)arr[j]);
			}
		}

		// Token: 0x06002991 RID: 10641 RVA: 0x001C1C64 File Offset: 0x001BFE64
		public void Transform(Vector3[] arr)
		{
			if (this.onlyTranslational)
			{
				for (int i = arr.Length - 1; i >= 0; i--)
				{
					arr[i] += this.translation;
				}
				return;
			}
			for (int j = arr.Length - 1; j >= 0; j--)
			{
				arr[j] = this.matrix.MultiplyPoint3x4(arr[j]);
			}
		}

		// Token: 0x06002992 RID: 10642 RVA: 0x001C1CD4 File Offset: 0x001BFED4
		public Vector3 InverseTransform(Vector3 point)
		{
			if (this.onlyTranslational)
			{
				return point - this.translation;
			}
			return this.inverseMatrix.MultiplyPoint3x4(point);
		}

		// Token: 0x06002993 RID: 10643 RVA: 0x001C1D08 File Offset: 0x001BFF08
		public Int3 InverseTransform(Int3 point)
		{
			if (this.onlyTranslational)
			{
				return point - this.i3translation;
			}
			return (Int3)this.inverseMatrix.MultiplyPoint3x4((Vector3)point);
		}

		// Token: 0x06002994 RID: 10644 RVA: 0x001C1D44 File Offset: 0x001BFF44
		public void InverseTransform(Int3[] arr)
		{
			for (int i = arr.Length - 1; i >= 0; i--)
			{
				arr[i] = (Int3)this.inverseMatrix.MultiplyPoint3x4((Vector3)arr[i]);
			}
		}

		// Token: 0x06002995 RID: 10645 RVA: 0x001C1D87 File Offset: 0x001BFF87
		public static GraphTransform operator *(GraphTransform lhs, Matrix4x4 rhs)
		{
			return new GraphTransform(lhs.matrix * rhs);
		}

		// Token: 0x06002996 RID: 10646 RVA: 0x001C1D9A File Offset: 0x001BFF9A
		public static GraphTransform operator *(Matrix4x4 lhs, GraphTransform rhs)
		{
			return new GraphTransform(lhs * rhs.matrix);
		}

		// Token: 0x06002997 RID: 10647 RVA: 0x001C1DB0 File Offset: 0x001BFFB0
		public Bounds Transform(Bounds bounds)
		{
			if (this.onlyTranslational)
			{
				return new Bounds(bounds.center + this.translation, bounds.size);
			}
			Vector3[] array = ArrayPool<Vector3>.Claim(8);
			Vector3 extents = bounds.extents;
			array[0] = this.Transform(bounds.center + new Vector3(extents.x, extents.y, extents.z));
			array[1] = this.Transform(bounds.center + new Vector3(extents.x, extents.y, -extents.z));
			array[2] = this.Transform(bounds.center + new Vector3(extents.x, -extents.y, extents.z));
			array[3] = this.Transform(bounds.center + new Vector3(extents.x, -extents.y, -extents.z));
			array[4] = this.Transform(bounds.center + new Vector3(-extents.x, extents.y, extents.z));
			array[5] = this.Transform(bounds.center + new Vector3(-extents.x, extents.y, -extents.z));
			array[6] = this.Transform(bounds.center + new Vector3(-extents.x, -extents.y, extents.z));
			array[7] = this.Transform(bounds.center + new Vector3(-extents.x, -extents.y, -extents.z));
			Vector3 vector = array[0];
			Vector3 vector2 = array[0];
			for (int i = 1; i < 8; i++)
			{
				vector = Vector3.Min(vector, array[i]);
				vector2 = Vector3.Max(vector2, array[i]);
			}
			ArrayPool<Vector3>.Release(ref array, false);
			return new Bounds((vector + vector2) * 0.5f, vector2 - vector);
		}

		// Token: 0x06002998 RID: 10648 RVA: 0x001C1FE4 File Offset: 0x001C01E4
		public Bounds InverseTransform(Bounds bounds)
		{
			if (this.onlyTranslational)
			{
				return new Bounds(bounds.center - this.translation, bounds.size);
			}
			Vector3[] array = ArrayPool<Vector3>.Claim(8);
			Vector3 extents = bounds.extents;
			array[0] = this.InverseTransform(bounds.center + new Vector3(extents.x, extents.y, extents.z));
			array[1] = this.InverseTransform(bounds.center + new Vector3(extents.x, extents.y, -extents.z));
			array[2] = this.InverseTransform(bounds.center + new Vector3(extents.x, -extents.y, extents.z));
			array[3] = this.InverseTransform(bounds.center + new Vector3(extents.x, -extents.y, -extents.z));
			array[4] = this.InverseTransform(bounds.center + new Vector3(-extents.x, extents.y, extents.z));
			array[5] = this.InverseTransform(bounds.center + new Vector3(-extents.x, extents.y, -extents.z));
			array[6] = this.InverseTransform(bounds.center + new Vector3(-extents.x, -extents.y, extents.z));
			array[7] = this.InverseTransform(bounds.center + new Vector3(-extents.x, -extents.y, -extents.z));
			Vector3 vector = array[0];
			Vector3 vector2 = array[0];
			for (int i = 1; i < 8; i++)
			{
				vector = Vector3.Min(vector, array[i]);
				vector2 = Vector3.Max(vector2, array[i]);
			}
			ArrayPool<Vector3>.Release(ref array, false);
			return new Bounds((vector + vector2) * 0.5f, vector2 - vector);
		}

		// Token: 0x06002999 RID: 10649 RVA: 0x001C2218 File Offset: 0x001C0418
		Vector2 IMovementPlane.ToPlane(Vector3 point)
		{
			if (this.isXY)
			{
				return new Vector2(point.x, point.y);
			}
			if (!this.isXZ)
			{
				point = this.inverseRotation * point;
			}
			return new Vector2(point.x, point.z);
		}

		// Token: 0x0600299A RID: 10650 RVA: 0x001C2266 File Offset: 0x001C0466
		Vector2 IMovementPlane.ToPlane(Vector3 point, out float elevation)
		{
			if (!this.isXZ)
			{
				point = this.inverseRotation * point;
			}
			elevation = point.y;
			return new Vector2(point.x, point.z);
		}

		// Token: 0x0600299B RID: 10651 RVA: 0x001C2297 File Offset: 0x001C0497
		Vector3 IMovementPlane.ToWorld(Vector2 point, float elevation)
		{
			return this.rotation * new Vector3(point.x, elevation, point.y);
		}

		// Token: 0x040043EA RID: 17386
		public readonly bool identity;

		// Token: 0x040043EB RID: 17387
		public readonly bool onlyTranslational;

		// Token: 0x040043EC RID: 17388
		private readonly bool isXY;

		// Token: 0x040043ED RID: 17389
		private readonly bool isXZ;

		// Token: 0x040043EE RID: 17390
		private readonly Matrix4x4 matrix;

		// Token: 0x040043EF RID: 17391
		private readonly Matrix4x4 inverseMatrix;

		// Token: 0x040043F0 RID: 17392
		private readonly Vector3 up;

		// Token: 0x040043F1 RID: 17393
		private readonly Vector3 translation;

		// Token: 0x040043F2 RID: 17394
		private readonly Int3 i3translation;

		// Token: 0x040043F3 RID: 17395
		private readonly Quaternion rotation;

		// Token: 0x040043F4 RID: 17396
		private readonly Quaternion inverseRotation;

		// Token: 0x040043F5 RID: 17397
		public static readonly GraphTransform identityTransform = new GraphTransform(Matrix4x4.identity);
	}
}
