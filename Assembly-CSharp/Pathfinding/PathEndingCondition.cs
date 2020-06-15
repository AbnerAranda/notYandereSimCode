using System;

namespace Pathfinding
{
	// Token: 0x020005B1 RID: 1457
	public abstract class PathEndingCondition
	{
		// Token: 0x0600276E RID: 10094 RVA: 0x000045DB File Offset: 0x000027DB
		protected PathEndingCondition()
		{
		}

		// Token: 0x0600276F RID: 10095 RVA: 0x001B1ABA File Offset: 0x001AFCBA
		public PathEndingCondition(Path p)
		{
			if (p == null)
			{
				throw new ArgumentNullException("p");
			}
			this.path = p;
		}

		// Token: 0x06002770 RID: 10096
		public abstract bool TargetFound(PathNode node);

		// Token: 0x04004295 RID: 17045
		protected Path path;
	}
}
