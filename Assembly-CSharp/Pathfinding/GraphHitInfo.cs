using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000556 RID: 1366
	public struct GraphHitInfo
	{
		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x0600243B RID: 9275 RVA: 0x0019B8FC File Offset: 0x00199AFC
		public float distance
		{
			get
			{
				return (this.point - this.origin).magnitude;
			}
		}

		// Token: 0x0600243C RID: 9276 RVA: 0x0019B922 File Offset: 0x00199B22
		public GraphHitInfo(Vector3 point)
		{
			this.tangentOrigin = Vector3.zero;
			this.origin = Vector3.zero;
			this.point = point;
			this.node = null;
			this.tangent = Vector3.zero;
		}

		// Token: 0x0400408C RID: 16524
		public Vector3 origin;

		// Token: 0x0400408D RID: 16525
		public Vector3 point;

		// Token: 0x0400408E RID: 16526
		public GraphNode node;

		// Token: 0x0400408F RID: 16527
		public Vector3 tangentOrigin;

		// Token: 0x04004090 RID: 16528
		public Vector3 tangent;
	}
}
