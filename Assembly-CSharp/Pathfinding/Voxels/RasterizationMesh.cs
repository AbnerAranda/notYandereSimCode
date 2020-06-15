using System;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding.Voxels
{
	// Token: 0x020005C2 RID: 1474
	public class RasterizationMesh
	{
		// Token: 0x060027CF RID: 10191 RVA: 0x000045DB File Offset: 0x000027DB
		public RasterizationMesh()
		{
		}

		// Token: 0x060027D0 RID: 10192 RVA: 0x001B5018 File Offset: 0x001B3218
		public RasterizationMesh(Vector3[] vertices, int[] triangles, Bounds bounds)
		{
			this.matrix = Matrix4x4.identity;
			this.vertices = vertices;
			this.numVertices = vertices.Length;
			this.triangles = triangles;
			this.numTriangles = triangles.Length;
			this.bounds = bounds;
			this.original = null;
			this.area = 0;
		}

		// Token: 0x060027D1 RID: 10193 RVA: 0x001B506C File Offset: 0x001B326C
		public RasterizationMesh(Vector3[] vertices, int[] triangles, Bounds bounds, Matrix4x4 matrix)
		{
			this.matrix = matrix;
			this.vertices = vertices;
			this.numVertices = vertices.Length;
			this.triangles = triangles;
			this.numTriangles = triangles.Length;
			this.bounds = bounds;
			this.original = null;
			this.area = 0;
		}

		// Token: 0x060027D2 RID: 10194 RVA: 0x001B50BC File Offset: 0x001B32BC
		public void RecalculateBounds()
		{
			Bounds bounds = new Bounds(this.matrix.MultiplyPoint3x4(this.vertices[0]), Vector3.zero);
			for (int i = 1; i < this.numVertices; i++)
			{
				bounds.Encapsulate(this.matrix.MultiplyPoint3x4(this.vertices[i]));
			}
			this.bounds = bounds;
		}

		// Token: 0x060027D3 RID: 10195 RVA: 0x001B5122 File Offset: 0x001B3322
		public void Pool()
		{
			if (this.pool)
			{
				ArrayPool<int>.Release(ref this.triangles, false);
				ArrayPool<Vector3>.Release(ref this.vertices, false);
			}
		}

		// Token: 0x040042D3 RID: 17107
		public MeshFilter original;

		// Token: 0x040042D4 RID: 17108
		public int area;

		// Token: 0x040042D5 RID: 17109
		public Vector3[] vertices;

		// Token: 0x040042D6 RID: 17110
		public int[] triangles;

		// Token: 0x040042D7 RID: 17111
		public int numVertices;

		// Token: 0x040042D8 RID: 17112
		public int numTriangles;

		// Token: 0x040042D9 RID: 17113
		public Bounds bounds;

		// Token: 0x040042DA RID: 17114
		public Matrix4x4 matrix;

		// Token: 0x040042DB RID: 17115
		public bool pool;
	}
}
