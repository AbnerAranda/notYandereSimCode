using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200052E RID: 1326
	public class RichSpecial : RichPathPart
	{
		// Token: 0x060021AF RID: 8623 RVA: 0x0018FFF3 File Offset: 0x0018E1F3
		public override void OnEnterPool()
		{
			this.nodeLink = null;
		}

		// Token: 0x060021B0 RID: 8624 RVA: 0x0018FFFC File Offset: 0x0018E1FC
		public RichSpecial Initialize(NodeLink2 nodeLink, GraphNode first)
		{
			this.nodeLink = nodeLink;
			if (first == nodeLink.startNode)
			{
				this.first = nodeLink.StartTransform;
				this.second = nodeLink.EndTransform;
				this.reverse = false;
			}
			else
			{
				this.first = nodeLink.EndTransform;
				this.second = nodeLink.StartTransform;
				this.reverse = true;
			}
			return this;
		}

		// Token: 0x04003F6C RID: 16236
		public NodeLink2 nodeLink;

		// Token: 0x04003F6D RID: 16237
		public Transform first;

		// Token: 0x04003F6E RID: 16238
		public Transform second;

		// Token: 0x04003F6F RID: 16239
		public bool reverse;
	}
}
