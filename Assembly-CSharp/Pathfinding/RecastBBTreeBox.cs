using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000592 RID: 1426
	public class RecastBBTreeBox
	{
		// Token: 0x06002686 RID: 9862 RVA: 0x001AB2E8 File Offset: 0x001A94E8
		public RecastBBTreeBox(RecastMeshObj mesh)
		{
			this.mesh = mesh;
			Vector3 min = mesh.bounds.min;
			Vector3 max = mesh.bounds.max;
			this.rect = Rect.MinMaxRect(min.x, min.z, max.x, max.z);
		}

		// Token: 0x06002687 RID: 9863 RVA: 0x001AB33D File Offset: 0x001A953D
		public bool Contains(Vector3 p)
		{
			return this.rect.Contains(p);
		}

		// Token: 0x040041F8 RID: 16888
		public Rect rect;

		// Token: 0x040041F9 RID: 16889
		public RecastMeshObj mesh;

		// Token: 0x040041FA RID: 16890
		public RecastBBTreeBox c1;

		// Token: 0x040041FB RID: 16891
		public RecastBBTreeBox c2;
	}
}
