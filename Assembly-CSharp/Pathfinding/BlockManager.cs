using System;
using System.Collections.Generic;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020005B4 RID: 1460
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_block_manager.php")]
	public class BlockManager : VersionedMonoBehaviour
	{
		// Token: 0x06002775 RID: 10101 RVA: 0x001B1B73 File Offset: 0x001AFD73
		private void Start()
		{
			if (!AstarPath.active)
			{
				throw new Exception("No AstarPath object in the scene");
			}
		}

		// Token: 0x06002776 RID: 10102 RVA: 0x001B1B8C File Offset: 0x001AFD8C
		public bool NodeContainsAnyOf(GraphNode node, List<SingleNodeBlocker> selector)
		{
			List<SingleNodeBlocker> list;
			if (!this.blocked.TryGetValue(node, out list))
			{
				return false;
			}
			for (int i = 0; i < list.Count; i++)
			{
				SingleNodeBlocker singleNodeBlocker = list[i];
				for (int j = 0; j < selector.Count; j++)
				{
					if (singleNodeBlocker == selector[j])
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06002777 RID: 10103 RVA: 0x001B1BE4 File Offset: 0x001AFDE4
		public bool NodeContainsAnyExcept(GraphNode node, List<SingleNodeBlocker> selector)
		{
			List<SingleNodeBlocker> list;
			if (!this.blocked.TryGetValue(node, out list))
			{
				return false;
			}
			for (int i = 0; i < list.Count; i++)
			{
				SingleNodeBlocker singleNodeBlocker = list[i];
				bool flag = false;
				for (int j = 0; j < selector.Count; j++)
				{
					if (singleNodeBlocker == selector[j])
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002778 RID: 10104 RVA: 0x001B1C48 File Offset: 0x001AFE48
		public void InternalBlock(GraphNode node, SingleNodeBlocker blocker)
		{
			AstarPath.active.AddWorkItem(new AstarWorkItem(delegate()
			{
				List<SingleNodeBlocker> list;
				if (!this.blocked.TryGetValue(node, out list))
				{
					list = (this.blocked[node] = ListPool<SingleNodeBlocker>.Claim());
				}
				list.Add(blocker);
			}, null));
		}

		// Token: 0x06002779 RID: 10105 RVA: 0x001B1C8C File Offset: 0x001AFE8C
		public void InternalUnblock(GraphNode node, SingleNodeBlocker blocker)
		{
			AstarPath.active.AddWorkItem(new AstarWorkItem(delegate()
			{
				List<SingleNodeBlocker> list;
				if (this.blocked.TryGetValue(node, out list))
				{
					list.Remove(blocker);
					if (list.Count == 0)
					{
						this.blocked.Remove(node);
						ListPool<SingleNodeBlocker>.Release(ref list);
					}
				}
			}, null));
		}

		// Token: 0x04004298 RID: 17048
		private Dictionary<GraphNode, List<SingleNodeBlocker>> blocked = new Dictionary<GraphNode, List<SingleNodeBlocker>>();

		// Token: 0x02000773 RID: 1907
		public enum BlockMode
		{
			// Token: 0x04004AF5 RID: 19189
			AllExceptSelector,
			// Token: 0x04004AF6 RID: 19190
			OnlySelector
		}

		// Token: 0x02000774 RID: 1908
		public class TraversalProvider : ITraversalProvider
		{
			// Token: 0x17000692 RID: 1682
			// (get) Token: 0x06002DC8 RID: 11720 RVA: 0x001D49ED File Offset: 0x001D2BED
			// (set) Token: 0x06002DC9 RID: 11721 RVA: 0x001D49F5 File Offset: 0x001D2BF5
			public BlockManager.BlockMode mode { get; private set; }

			// Token: 0x06002DCA RID: 11722 RVA: 0x001D49FE File Offset: 0x001D2BFE
			public TraversalProvider(BlockManager blockManager, BlockManager.BlockMode mode, List<SingleNodeBlocker> selector)
			{
				if (blockManager == null)
				{
					throw new ArgumentNullException("blockManager");
				}
				if (selector == null)
				{
					throw new ArgumentNullException("selector");
				}
				this.blockManager = blockManager;
				this.mode = mode;
				this.selector = selector;
			}

			// Token: 0x06002DCB RID: 11723 RVA: 0x001D4A40 File Offset: 0x001D2C40
			public bool CanTraverse(Path path, GraphNode node)
			{
				if (!node.Walkable || (path.enabledTags >> (int)node.Tag & 1) == 0)
				{
					return false;
				}
				if (this.mode == BlockManager.BlockMode.OnlySelector)
				{
					return !this.blockManager.NodeContainsAnyOf(node, this.selector);
				}
				return !this.blockManager.NodeContainsAnyExcept(node, this.selector);
			}

			// Token: 0x06002DCC RID: 11724 RVA: 0x001D4A9F File Offset: 0x001D2C9F
			public uint GetTraversalCost(Path path, GraphNode node)
			{
				return path.GetTagPenalty((int)node.Tag) + node.Penalty;
			}

			// Token: 0x04004AF7 RID: 19191
			private readonly BlockManager blockManager;

			// Token: 0x04004AF9 RID: 19193
			private readonly List<SingleNodeBlocker> selector;
		}
	}
}
