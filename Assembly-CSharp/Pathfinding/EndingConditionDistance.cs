using System;

namespace Pathfinding
{
	// Token: 0x020005A9 RID: 1449
	public class EndingConditionDistance : PathEndingCondition
	{
		// Token: 0x0600272C RID: 10028 RVA: 0x001AFF4F File Offset: 0x001AE14F
		public EndingConditionDistance(Path p, int maxGScore) : base(p)
		{
			this.maxGScore = maxGScore;
		}

		// Token: 0x0600272D RID: 10029 RVA: 0x001AFF67 File Offset: 0x001AE167
		public override bool TargetFound(PathNode node)
		{
			return (ulong)node.G >= (ulong)((long)this.maxGScore);
		}

		// Token: 0x04004276 RID: 17014
		public int maxGScore = 100;
	}
}
