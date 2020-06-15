using System;
using UnityEngine;

namespace MaidDereMinigame
{
	// Token: 0x020004F7 RID: 1271
	public abstract class AIMover : MonoBehaviour
	{
		// Token: 0x06001FF8 RID: 8184
		public abstract ControlInput GetInput();

		// Token: 0x06001FF9 RID: 8185 RVA: 0x00189024 File Offset: 0x00187224
		private void FixedUpdate()
		{
			ControlInput input = this.GetInput();
			base.transform.Translate(new Vector2(input.horizontal, 0f) * Time.fixedDeltaTime * this.moveSpeed);
		}

		// Token: 0x04003E21 RID: 15905
		protected float moveSpeed = 3f;
	}
}
