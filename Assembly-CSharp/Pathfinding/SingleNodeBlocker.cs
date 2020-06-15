using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020005B5 RID: 1461
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_single_node_blocker.php")]
	public class SingleNodeBlocker : VersionedMonoBehaviour
	{
		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x0600277B RID: 10107 RVA: 0x001B1CE3 File Offset: 0x001AFEE3
		// (set) Token: 0x0600277C RID: 10108 RVA: 0x001B1CEB File Offset: 0x001AFEEB
		public GraphNode lastBlocked { get; private set; }

		// Token: 0x0600277D RID: 10109 RVA: 0x001B1CF4 File Offset: 0x001AFEF4
		public void BlockAtCurrentPosition()
		{
			this.BlockAt(base.transform.position);
		}

		// Token: 0x0600277E RID: 10110 RVA: 0x001B1D08 File Offset: 0x001AFF08
		public void BlockAt(Vector3 position)
		{
			this.Unblock();
			GraphNode node = AstarPath.active.GetNearest(position, NNConstraint.None).node;
			if (node != null)
			{
				this.Block(node);
			}
		}

		// Token: 0x0600277F RID: 10111 RVA: 0x001B1D3B File Offset: 0x001AFF3B
		public void Block(GraphNode node)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			this.manager.InternalBlock(node, this);
			this.lastBlocked = node;
		}

		// Token: 0x06002780 RID: 10112 RVA: 0x001B1D5F File Offset: 0x001AFF5F
		public void Unblock()
		{
			if (this.lastBlocked == null || this.lastBlocked.Destroyed)
			{
				this.lastBlocked = null;
				return;
			}
			this.manager.InternalUnblock(this.lastBlocked, this);
			this.lastBlocked = null;
		}

		// Token: 0x0400429A RID: 17050
		public BlockManager manager;
	}
}
