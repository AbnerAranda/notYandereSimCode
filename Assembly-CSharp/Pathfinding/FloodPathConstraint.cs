using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020005AC RID: 1452
	public class FloodPathConstraint : NNConstraint
	{
		// Token: 0x0600273D RID: 10045 RVA: 0x001B031F File Offset: 0x001AE51F
		public FloodPathConstraint(FloodPath path)
		{
			if (path == null)
			{
				Debug.LogWarning("FloodPathConstraint should not be used with a NULL path");
			}
			this.path = path;
		}

		// Token: 0x0600273E RID: 10046 RVA: 0x001B033B File Offset: 0x001AE53B
		public override bool Suitable(GraphNode node)
		{
			return base.Suitable(node) && this.path.HasPathTo(node);
		}

		// Token: 0x0400427C RID: 17020
		private readonly FloodPath path;
	}
}
