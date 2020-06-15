using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaidDereMinigame
{
	// Token: 0x0200050C RID: 1292
	public class FlipBook : MonoBehaviour
	{
		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x0600204E RID: 8270 RVA: 0x0018A7F7 File Offset: 0x001889F7
		public static FlipBook Instance
		{
			get
			{
				if (FlipBook.instance == null)
				{
					FlipBook.instance = UnityEngine.Object.FindObjectOfType<FlipBook>();
				}
				return FlipBook.instance;
			}
		}

		// Token: 0x0600204F RID: 8271 RVA: 0x0018A815 File Offset: 0x00188A15
		private void Awake()
		{
			base.StartCoroutine(this.OpenBook());
		}

		// Token: 0x06002050 RID: 8272 RVA: 0x0018A824 File Offset: 0x00188A24
		private IEnumerator OpenBook()
		{
			yield return new WaitForSeconds(1f);
			this.FlipToPage(1);
			yield break;
		}

		// Token: 0x06002051 RID: 8273 RVA: 0x0018A833 File Offset: 0x00188A33
		private void Update()
		{
			if (this.stopInputs)
			{
				return;
			}
			if (this.curPage > 1 && Input.GetButtonDown("B") && this.canGoBack)
			{
				this.FlipToPage(1);
			}
		}

		// Token: 0x06002052 RID: 8274 RVA: 0x0018A862 File Offset: 0x00188A62
		public void StopInputs()
		{
			this.stopInputs = true;
		}

		// Token: 0x06002053 RID: 8275 RVA: 0x0018A86B File Offset: 0x00188A6B
		public void FlipToPage(int page)
		{
			SFXController.PlaySound(SFXController.Sounds.PageTurn);
			base.StartCoroutine(this.FlipToPageRoutine(page));
		}

		// Token: 0x06002054 RID: 8276 RVA: 0x0018A881 File Offset: 0x00188A81
		private IEnumerator FlipToPageRoutine(int page)
		{
			bool flag = this.curPage < page;
			this.canGoBack = false;
			if (flag)
			{
				while (this.curPage < page)
				{
					List<FlipBookPage> list = this.flipBookPages;
					int num = this.curPage;
					this.curPage = num + 1;
					list[num].Transition(flag);
				}
				yield return new WaitForSeconds(0.4f);
				this.flipBookPages[this.curPage].ObjectActive(true);
			}
			else
			{
				this.flipBookPages[this.curPage].ObjectActive(false);
				while (this.curPage > page)
				{
					List<FlipBookPage> list2 = this.flipBookPages;
					int num = this.curPage - 1;
					this.curPage = num;
					list2[num].Transition(flag);
				}
				yield return new WaitForSeconds(0.6f);
				this.flipBookPages[this.curPage].ObjectActive(true);
			}
			this.canGoBack = true;
			yield break;
		}

		// Token: 0x04003E7C RID: 15996
		private static FlipBook instance;

		// Token: 0x04003E7D RID: 15997
		public List<FlipBookPage> flipBookPages;

		// Token: 0x04003E7E RID: 15998
		private int curPage;

		// Token: 0x04003E7F RID: 15999
		private bool canGoBack;

		// Token: 0x04003E80 RID: 16000
		private bool stopInputs;
	}
}
