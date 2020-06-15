using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200057E RID: 1406
	internal class LinkedLevelNode
	{
		// Token: 0x04004168 RID: 16744
		public Vector3 position;

		// Token: 0x04004169 RID: 16745
		public bool walkable;

		// Token: 0x0400416A RID: 16746
		public RaycastHit hit;

		// Token: 0x0400416B RID: 16747
		public float height;

		// Token: 0x0400416C RID: 16748
		public LinkedLevelNode next;
	}
}
