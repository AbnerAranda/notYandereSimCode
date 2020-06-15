using System;
using UnityEngine;

namespace Pathfinding.Voxels
{
	// Token: 0x020005C1 RID: 1473
	[Obsolete("Use RasterizationMesh instead")]
	public class ExtraMesh : RasterizationMesh
	{
		// Token: 0x060027CD RID: 10189 RVA: 0x001B4FFF File Offset: 0x001B31FF
		public ExtraMesh(Vector3[] vertices, int[] triangles, Bounds bounds) : base(vertices, triangles, bounds)
		{
		}

		// Token: 0x060027CE RID: 10190 RVA: 0x001B500A File Offset: 0x001B320A
		public ExtraMesh(Vector3[] vertices, int[] triangles, Bounds bounds, Matrix4x4 matrix) : base(vertices, triangles, bounds, matrix)
		{
		}
	}
}
