using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200055A RID: 1370
	public struct NNInfo
	{
		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x06002447 RID: 9287 RVA: 0x0019BAEB File Offset: 0x00199CEB
		[Obsolete("This field has been renamed to 'position'")]
		public Vector3 clampedPosition
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x06002448 RID: 9288 RVA: 0x0019BAF3 File Offset: 0x00199CF3
		public NNInfo(NNInfoInternal internalInfo)
		{
			this.node = internalInfo.node;
			this.position = internalInfo.clampedPosition;
		}

		// Token: 0x06002449 RID: 9289 RVA: 0x0019BAEB File Offset: 0x00199CEB
		public static explicit operator Vector3(NNInfo ob)
		{
			return ob.position;
		}

		// Token: 0x0600244A RID: 9290 RVA: 0x0019BB0D File Offset: 0x00199D0D
		public static explicit operator GraphNode(NNInfo ob)
		{
			return ob.node;
		}

		// Token: 0x0400409E RID: 16542
		public readonly GraphNode node;

		// Token: 0x0400409F RID: 16543
		public readonly Vector3 position;
	}
}
