using System;
using System.Collections.Generic;
using UnityEngine;

namespace MaidDereMinigame
{
	// Token: 0x0200051B RID: 1307
	public class TipPage : MonoBehaviour
	{
		// Token: 0x06002091 RID: 8337 RVA: 0x0018B5D8 File Offset: 0x001897D8
		public void Init()
		{
			this.cards = new List<TipCard>();
			foreach (object obj in base.transform.GetChild(0))
			{
				foreach (object obj2 in ((Transform)obj))
				{
					Transform transform = (Transform)obj2;
					this.cards.Add(transform.GetComponent<TipCard>());
				}
			}
			base.gameObject.SetActive(false);
		}

		// Token: 0x06002092 RID: 8338 RVA: 0x0018B694 File Offset: 0x00189894
		public void DisplayTips(List<float> tips)
		{
			if (tips == null)
			{
				tips = new List<float>();
			}
			base.gameObject.SetActive(true);
			float num = 0f;
			for (int i = 0; i < this.cards.Count; i++)
			{
				if (tips.Count > i)
				{
					this.cards[i].SetTip(tips[i]);
					num += tips[i];
				}
				else
				{
					this.cards[i].SetTip(0f);
				}
			}
			float basePay = GameController.Instance.activeDifficultyVariables.basePay;
			GameController.Instance.totalPayout = num + basePay;
			this.wageCard.SetTip(basePay);
			this.totalCard.SetTip(num + basePay);
		}

		// Token: 0x06002093 RID: 8339 RVA: 0x0018B74D File Offset: 0x0018994D
		private void Update()
		{
			if (this.stopInteraction)
			{
				return;
			}
			if (Input.GetButtonDown("A"))
			{
				GameController.GoToExitScene(true);
				this.stopInteraction = true;
			}
		}

		// Token: 0x04003EB2 RID: 16050
		public TipCard wageCard;

		// Token: 0x04003EB3 RID: 16051
		public TipCard totalCard;

		// Token: 0x04003EB4 RID: 16052
		private List<TipCard> cards;

		// Token: 0x04003EB5 RID: 16053
		private bool stopInteraction;
	}
}
