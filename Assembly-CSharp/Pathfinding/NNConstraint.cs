using System;

namespace Pathfinding
{
	// Token: 0x02000557 RID: 1367
	public class NNConstraint
	{
		// Token: 0x0600243D RID: 9277 RVA: 0x0019B953 File Offset: 0x00199B53
		public virtual bool SuitableGraph(int graphIndex, NavGraph graph)
		{
			return (this.graphMask >> graphIndex & 1) != 0;
		}

		// Token: 0x0600243E RID: 9278 RVA: 0x0019B968 File Offset: 0x00199B68
		public virtual bool Suitable(GraphNode node)
		{
			return (!this.constrainWalkability || node.Walkable == this.walkable) && (!this.constrainArea || this.area < 0 || (ulong)node.Area == (ulong)((long)this.area)) && (!this.constrainTags || (this.tags >> (int)node.Tag & 1) != 0);
		}

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x0600243F RID: 9279 RVA: 0x0019B9CF File Offset: 0x00199BCF
		public static NNConstraint Default
		{
			get
			{
				return new NNConstraint();
			}
		}

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x06002440 RID: 9280 RVA: 0x0019B9D6 File Offset: 0x00199BD6
		public static NNConstraint None
		{
			get
			{
				return new NNConstraint
				{
					constrainWalkability = false,
					constrainArea = false,
					constrainTags = false,
					constrainDistance = false,
					graphMask = -1
				};
			}
		}

		// Token: 0x04004091 RID: 16529
		public int graphMask = -1;

		// Token: 0x04004092 RID: 16530
		public bool constrainArea;

		// Token: 0x04004093 RID: 16531
		public int area = -1;

		// Token: 0x04004094 RID: 16532
		public bool constrainWalkability = true;

		// Token: 0x04004095 RID: 16533
		public bool walkable = true;

		// Token: 0x04004096 RID: 16534
		public bool distanceXZ;

		// Token: 0x04004097 RID: 16535
		public bool constrainTags = true;

		// Token: 0x04004098 RID: 16536
		public int tags = -1;

		// Token: 0x04004099 RID: 16537
		public bool constrainDistance = true;
	}
}
