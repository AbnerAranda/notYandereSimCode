using System;
using UnityEngine;

namespace MaidDereMinigame
{
	// Token: 0x020004FD RID: 1277
	public class Bubble : MonoBehaviour
	{
		// Token: 0x06002019 RID: 8217 RVA: 0x0018A01E File Offset: 0x0018821E
		private void Awake()
		{
			this.foodRenderer.sprite = null;
		}

		// Token: 0x0600201A RID: 8218 RVA: 0x0018A02C File Offset: 0x0018822C
		private void OnEnable()
		{
			GameController.PauseGame = (BoolParameterEvent)Delegate.Combine(GameController.PauseGame, new BoolParameterEvent(this.Pause));
		}

		// Token: 0x0600201B RID: 8219 RVA: 0x0018A04E File Offset: 0x0018824E
		private void OnDisable()
		{
			GameController.PauseGame = (BoolParameterEvent)Delegate.Remove(GameController.PauseGame, new BoolParameterEvent(this.Pause));
		}

		// Token: 0x0600201C RID: 8220 RVA: 0x0018A070 File Offset: 0x00188270
		public void Pause(bool toPause)
		{
			if (toPause)
			{
				base.GetComponent<SpriteRenderer>().enabled = false;
				this.foodRenderer.gameObject.SetActive(false);
				return;
			}
			base.GetComponent<SpriteRenderer>().enabled = true;
			this.foodRenderer.gameObject.SetActive(true);
		}

		// Token: 0x0600201D RID: 8221 RVA: 0x0018A0B0 File Offset: 0x001882B0
		public void BubbleReachedMax()
		{
			this.foodRenderer.gameObject.SetActive(true);
			this.foodRenderer.sprite = this.food.largeSprite;
		}

		// Token: 0x0600201E RID: 8222 RVA: 0x0018A0D9 File Offset: 0x001882D9
		public void BubbleClosing()
		{
			this.foodRenderer.gameObject.SetActive(false);
		}

		// Token: 0x0600201F RID: 8223 RVA: 0x0017BD0C File Offset: 0x00179F0C
		public void KillBubble()
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x04003E47 RID: 15943
		[HideInInspector]
		public Food food;

		// Token: 0x04003E48 RID: 15944
		public SpriteRenderer foodRenderer;
	}
}
