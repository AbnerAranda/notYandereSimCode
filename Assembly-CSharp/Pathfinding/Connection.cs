using System;

namespace Pathfinding
{
	// Token: 0x0200054C RID: 1356
	public struct Connection
	{
		// Token: 0x0600239B RID: 9115 RVA: 0x0019A114 File Offset: 0x00198314
		public Connection(GraphNode node, uint cost, byte shapeEdge = 255)
		{
			this.node = node;
			this.cost = cost;
			this.shapeEdge = shapeEdge;
		}

		// Token: 0x0600239C RID: 9116 RVA: 0x0019A12B File Offset: 0x0019832B
		public override int GetHashCode()
		{
			return this.node.GetHashCode() ^ (int)this.cost;
		}

		// Token: 0x0600239D RID: 9117 RVA: 0x0019A140 File Offset: 0x00198340
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			Connection connection = (Connection)obj;
			return connection.node == this.node && connection.cost == this.cost && connection.shapeEdge == this.shapeEdge;
		}

		// Token: 0x0400403A RID: 16442
		public GraphNode node;

		// Token: 0x0400403B RID: 16443
		public uint cost;

		// Token: 0x0400403C RID: 16444
		public byte shapeEdge;
	}
}
