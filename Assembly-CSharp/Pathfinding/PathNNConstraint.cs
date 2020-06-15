using System;

namespace Pathfinding
{
	// Token: 0x02000558 RID: 1368
	public class PathNNConstraint : NNConstraint
	{
		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x06002442 RID: 9282 RVA: 0x0019BA39 File Offset: 0x00199C39
		public new static PathNNConstraint Default
		{
			get
			{
				return new PathNNConstraint
				{
					constrainArea = true
				};
			}
		}

		// Token: 0x06002443 RID: 9283 RVA: 0x0019BA47 File Offset: 0x00199C47
		public virtual void SetStart(GraphNode node)
		{
			if (node != null)
			{
				this.area = (int)node.Area;
				return;
			}
			this.constrainArea = false;
		}
	}
}
