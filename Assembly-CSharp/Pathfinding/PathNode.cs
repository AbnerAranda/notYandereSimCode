using System;

namespace Pathfinding
{
	// Token: 0x02000553 RID: 1363
	public class PathNode
	{
		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x06002422 RID: 9250 RVA: 0x0019B497 File Offset: 0x00199697
		// (set) Token: 0x06002423 RID: 9251 RVA: 0x0019B4A5 File Offset: 0x001996A5
		public uint cost
		{
			get
			{
				return this.flags & 268435455U;
			}
			set
			{
				this.flags = ((this.flags & 4026531840U) | value);
			}
		}

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x06002424 RID: 9252 RVA: 0x0019B4BB File Offset: 0x001996BB
		// (set) Token: 0x06002425 RID: 9253 RVA: 0x0019B4CC File Offset: 0x001996CC
		public bool flag1
		{
			get
			{
				return (this.flags & 268435456U) > 0U;
			}
			set
			{
				this.flags = ((this.flags & 4026531839U) | (value ? 268435456U : 0U));
			}
		}

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x06002426 RID: 9254 RVA: 0x0019B4EC File Offset: 0x001996EC
		// (set) Token: 0x06002427 RID: 9255 RVA: 0x0019B4FD File Offset: 0x001996FD
		public bool flag2
		{
			get
			{
				return (this.flags & 536870912U) > 0U;
			}
			set
			{
				this.flags = ((this.flags & 3758096383U) | (value ? 536870912U : 0U));
			}
		}

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x06002428 RID: 9256 RVA: 0x0019B51D File Offset: 0x0019971D
		// (set) Token: 0x06002429 RID: 9257 RVA: 0x0019B525 File Offset: 0x00199725
		public uint G
		{
			get
			{
				return this.g;
			}
			set
			{
				this.g = value;
			}
		}

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x0600242A RID: 9258 RVA: 0x0019B52E File Offset: 0x0019972E
		// (set) Token: 0x0600242B RID: 9259 RVA: 0x0019B536 File Offset: 0x00199736
		public uint H
		{
			get
			{
				return this.h;
			}
			set
			{
				this.h = value;
			}
		}

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x0600242C RID: 9260 RVA: 0x0019B53F File Offset: 0x0019973F
		public uint F
		{
			get
			{
				return this.g + this.h;
			}
		}

		// Token: 0x0600242D RID: 9261 RVA: 0x0019B54E File Offset: 0x0019974E
		public void UpdateG(Path path)
		{
			this.g = this.parent.g + this.cost + path.GetTraversalCost(this.node);
		}

		// Token: 0x0400406C RID: 16492
		public GraphNode node;

		// Token: 0x0400406D RID: 16493
		public PathNode parent;

		// Token: 0x0400406E RID: 16494
		public ushort pathID;

		// Token: 0x0400406F RID: 16495
		public ushort heapIndex = ushort.MaxValue;

		// Token: 0x04004070 RID: 16496
		private uint flags;

		// Token: 0x04004071 RID: 16497
		private const uint CostMask = 268435455U;

		// Token: 0x04004072 RID: 16498
		private const int Flag1Offset = 28;

		// Token: 0x04004073 RID: 16499
		private const uint Flag1Mask = 268435456U;

		// Token: 0x04004074 RID: 16500
		private const int Flag2Offset = 29;

		// Token: 0x04004075 RID: 16501
		private const uint Flag2Mask = 536870912U;

		// Token: 0x04004076 RID: 16502
		private uint g;

		// Token: 0x04004077 RID: 16503
		private uint h;
	}
}
