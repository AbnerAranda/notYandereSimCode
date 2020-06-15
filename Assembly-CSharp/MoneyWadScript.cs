using System;
using UnityEngine;

// Token: 0x02000338 RID: 824
public class MoneyWadScript : MonoBehaviour
{
	// Token: 0x06001853 RID: 6227 RVA: 0x000DA9D8 File Offset: 0x000D8BD8
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Yandere.Inventory.Money += 100f;
			this.Prompt.Yandere.Inventory.UpdateMoney();
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04002366 RID: 9062
	public PromptScript Prompt;
}
