using System;
using UnityEngine;

// Token: 0x02000301 RID: 769
public class IDCardScript : MonoBehaviour
{
	// Token: 0x06001780 RID: 6016 RVA: 0x000CBCE8 File Offset: 0x000C9EE8
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Circle[0].fillAmount = 1f;
			this.Prompt.Yandere.StolenObject = base.gameObject;
			if (!this.Fake)
			{
				this.Prompt.Yandere.Inventory.IDCard = true;
				this.Prompt.Yandere.TheftTimer = 1f;
			}
			else
			{
				this.Prompt.Yandere.Inventory.FakeID = true;
			}
			this.Prompt.Hide();
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x040020BE RID: 8382
	public PromptScript Prompt;

	// Token: 0x040020BF RID: 8383
	public bool Fake;
}
