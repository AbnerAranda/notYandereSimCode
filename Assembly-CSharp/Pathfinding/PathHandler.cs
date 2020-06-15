using System;
using System.Text;

namespace Pathfinding
{
	// Token: 0x02000554 RID: 1364
	public class PathHandler
	{
		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x0600242F RID: 9263 RVA: 0x0019B588 File Offset: 0x00199788
		public ushort PathID
		{
			get
			{
				return this.pathID;
			}
		}

		// Token: 0x06002430 RID: 9264 RVA: 0x0019B590 File Offset: 0x00199790
		public PathHandler(int threadID, int totalThreadCount)
		{
			this.threadID = threadID;
			this.totalThreadCount = totalThreadCount;
		}

		// Token: 0x06002431 RID: 9265 RVA: 0x0019B5CD File Offset: 0x001997CD
		public void InitializeForPath(Path p)
		{
			this.pathID = p.pathID;
			this.heap.Clear();
		}

		// Token: 0x06002432 RID: 9266 RVA: 0x0019B5E6 File Offset: 0x001997E6
		public void DestroyNode(GraphNode node)
		{
			PathNode pathNode = this.GetPathNode(node);
			pathNode.node = null;
			pathNode.parent = null;
			pathNode.pathID = 0;
			pathNode.G = 0U;
			pathNode.H = 0U;
		}

		// Token: 0x06002433 RID: 9267 RVA: 0x0019B614 File Offset: 0x00199814
		public void InitializeNode(GraphNode node)
		{
			int nodeIndex = node.NodeIndex;
			if (nodeIndex >= this.nodes.Length)
			{
				PathNode[] array = new PathNode[Math.Max(128, this.nodes.Length * 2)];
				this.nodes.CopyTo(array, 0);
				for (int i = this.nodes.Length; i < array.Length; i++)
				{
					array[i] = new PathNode();
				}
				this.nodes = array;
			}
			this.nodes[nodeIndex].node = node;
		}

		// Token: 0x06002434 RID: 9268 RVA: 0x0019B68C File Offset: 0x0019988C
		public PathNode GetPathNode(int nodeIndex)
		{
			return this.nodes[nodeIndex];
		}

		// Token: 0x06002435 RID: 9269 RVA: 0x0019B696 File Offset: 0x00199896
		public PathNode GetPathNode(GraphNode node)
		{
			return this.nodes[node.NodeIndex];
		}

		// Token: 0x06002436 RID: 9270 RVA: 0x0019B6A8 File Offset: 0x001998A8
		public void ClearPathIDs()
		{
			for (int i = 0; i < this.nodes.Length; i++)
			{
				if (this.nodes[i] != null)
				{
					this.nodes[i].pathID = 0;
				}
			}
		}

		// Token: 0x04004078 RID: 16504
		private ushort pathID;

		// Token: 0x04004079 RID: 16505
		public readonly int threadID;

		// Token: 0x0400407A RID: 16506
		public readonly int totalThreadCount;

		// Token: 0x0400407B RID: 16507
		public readonly BinaryHeap heap = new BinaryHeap(128);

		// Token: 0x0400407C RID: 16508
		public PathNode[] nodes = new PathNode[0];

		// Token: 0x0400407D RID: 16509
		public readonly StringBuilder DebugStringBuilder = new StringBuilder();
	}
}
