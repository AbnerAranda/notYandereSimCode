using System;
using UnityEngine;

namespace MaidDereMinigame
{
	// Token: 0x0200050D RID: 1293
	public class FlipBookPage : MonoBehaviour
	{
		// Token: 0x06002056 RID: 8278 RVA: 0x0018A897 File Offset: 0x00188A97
		private void Awake()
		{
			this.animator = base.GetComponent<Animator>();
			this.spriteRenderer = base.GetComponent<SpriteRenderer>();
		}

		// Token: 0x06002057 RID: 8279 RVA: 0x0018A8B1 File Offset: 0x00188AB1
		public void Transition(bool toOpen)
		{
			this.animator.SetTrigger(toOpen ? "OpenPage" : "ClosePage");
			if (this.objectToActivate != null)
			{
				this.objectToActivate.SetActive(false);
			}
		}

		// Token: 0x06002058 RID: 8280 RVA: 0x0018A8E7 File Offset: 0x00188AE7
		public void SwitchSort()
		{
			this.spriteRenderer.sortingOrder = 10 - this.spriteRenderer.sortingOrder;
		}

		// Token: 0x06002059 RID: 8281 RVA: 0x0018A902 File Offset: 0x00188B02
		public void ObjectActive(bool toActive = true)
		{
			if (this.objectToActivate != null)
			{
				this.objectToActivate.SetActive(toActive);
			}
		}

		// Token: 0x04003E81 RID: 16001
		[HideInInspector]
		public Animator animator;

		// Token: 0x04003E82 RID: 16002
		[HideInInspector]
		public SpriteRenderer spriteRenderer;

		// Token: 0x04003E83 RID: 16003
		public GameObject objectToActivate;
	}
}
