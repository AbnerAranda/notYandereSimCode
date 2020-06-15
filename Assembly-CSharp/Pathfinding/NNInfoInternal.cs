using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000559 RID: 1369
	public struct NNInfoInternal
	{
		// Token: 0x06002445 RID: 9285 RVA: 0x0019BA68 File Offset: 0x00199C68
		public NNInfoInternal(GraphNode node)
		{
			this.node = node;
			this.constrainedNode = null;
			this.clampedPosition = Vector3.zero;
			this.constClampedPosition = Vector3.zero;
			this.UpdateInfo();
		}

		// Token: 0x06002446 RID: 9286 RVA: 0x0019BA94 File Offset: 0x00199C94
		public void UpdateInfo()
		{
			this.clampedPosition = ((this.node != null) ? ((Vector3)this.node.position) : Vector3.zero);
			this.constClampedPosition = ((this.constrainedNode != null) ? ((Vector3)this.constrainedNode.position) : Vector3.zero);
		}

		// Token: 0x0400409A RID: 16538
		public GraphNode node;

		// Token: 0x0400409B RID: 16539
		public GraphNode constrainedNode;

		// Token: 0x0400409C RID: 16540
		public Vector3 clampedPosition;

		// Token: 0x0400409D RID: 16541
		public Vector3 constClampedPosition;
	}
}
