using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020005B3 RID: 1459
	public class EndingConditionProximity : ABPathEndingCondition
	{
		// Token: 0x06002773 RID: 10099 RVA: 0x001B1B10 File Offset: 0x001AFD10
		public EndingConditionProximity(ABPath p, float maxDistance) : base(p)
		{
			this.maxDistance = maxDistance;
		}

		// Token: 0x06002774 RID: 10100 RVA: 0x001B1B2C File Offset: 0x001AFD2C
		public override bool TargetFound(PathNode node)
		{
			return ((Vector3)node.node.position - this.abPath.originalEndPoint).sqrMagnitude <= this.maxDistance * this.maxDistance;
		}

		// Token: 0x04004297 RID: 17047
		public float maxDistance = 10f;
	}
}
