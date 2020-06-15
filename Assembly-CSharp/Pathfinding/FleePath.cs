using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020005AA RID: 1450
	public class FleePath : RandomPath
	{
		// Token: 0x0600272F RID: 10031 RVA: 0x001AFF84 File Offset: 0x001AE184
		public static FleePath Construct(Vector3 start, Vector3 avoid, int searchLength, OnPathDelegate callback = null)
		{
			FleePath path = PathPool.GetPath<FleePath>();
			path.Setup(start, avoid, searchLength, callback);
			return path;
		}

		// Token: 0x06002730 RID: 10032 RVA: 0x001AFF98 File Offset: 0x001AE198
		protected void Setup(Vector3 start, Vector3 avoid, int searchLength, OnPathDelegate callback)
		{
			base.Setup(start, searchLength, callback);
			this.aim = avoid - start;
			this.aim *= 10f;
			this.aim = start - this.aim;
		}
	}
}
