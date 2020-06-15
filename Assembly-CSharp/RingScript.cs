using System;
using UnityEngine;

// Token: 0x02000397 RID: 919
public class RingScript : MonoBehaviour
{
	// Token: 0x060019CC RID: 6604 RVA: 0x000FDD60 File Offset: 0x000FBF60
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			SchemeGlobals.SetSchemeStage(2, 2);
			this.Prompt.Yandere.Inventory.Schemes.UpdateInstructions();
			this.Prompt.Yandere.Inventory.Ring = true;
			this.Prompt.Yandere.TheftTimer = 0.1f;
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x04002807 RID: 10247
	public PromptScript Prompt;
}
