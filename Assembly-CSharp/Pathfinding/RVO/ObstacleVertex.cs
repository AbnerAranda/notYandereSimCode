using System;
using UnityEngine;

namespace Pathfinding.RVO
{
	// Token: 0x020005D6 RID: 1494
	public class ObstacleVertex
	{
		// Token: 0x0400433D RID: 17213
		public bool ignore;

		// Token: 0x0400433E RID: 17214
		public Vector3 position;

		// Token: 0x0400433F RID: 17215
		public Vector2 dir;

		// Token: 0x04004340 RID: 17216
		public float height;

		// Token: 0x04004341 RID: 17217
		public RVOLayer layer = RVOLayer.DefaultObstacle;

		// Token: 0x04004342 RID: 17218
		public ObstacleVertex next;

		// Token: 0x04004343 RID: 17219
		public ObstacleVertex prev;
	}
}
