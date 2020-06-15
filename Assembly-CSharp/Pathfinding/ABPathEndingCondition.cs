using System;

namespace Pathfinding
{
	// Token: 0x020005B2 RID: 1458
	public class ABPathEndingCondition : PathEndingCondition
	{
		// Token: 0x06002771 RID: 10097 RVA: 0x001B1AD7 File Offset: 0x001AFCD7
		public ABPathEndingCondition(ABPath p)
		{
			if (p == null)
			{
				throw new ArgumentNullException("p");
			}
			this.abPath = p;
			this.path = p;
		}

		// Token: 0x06002772 RID: 10098 RVA: 0x001B1AFB File Offset: 0x001AFCFB
		public override bool TargetFound(PathNode node)
		{
			return node.node == this.abPath.endNode;
		}

		// Token: 0x04004296 RID: 17046
		protected ABPath abPath;
	}
}
