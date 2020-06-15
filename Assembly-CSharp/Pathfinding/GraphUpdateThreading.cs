using System;

namespace Pathfinding
{
	// Token: 0x02000565 RID: 1381
	public enum GraphUpdateThreading
	{
		// Token: 0x040040B8 RID: 16568
		UnityThread,
		// Token: 0x040040B9 RID: 16569
		SeparateThread,
		// Token: 0x040040BA RID: 16570
		UnityInit,
		// Token: 0x040040BB RID: 16571
		UnityPost = 4,
		// Token: 0x040040BC RID: 16572
		SeparateAndUnityInit = 3
	}
}
