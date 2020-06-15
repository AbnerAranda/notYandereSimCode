using System;
using UnityEngine;

// Token: 0x0200032B RID: 811
public class MaskingTapeScript : MonoBehaviour
{
	// Token: 0x0600182A RID: 6186 RVA: 0x000D867C File Offset: 0x000D687C
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Yandere.Inventory.MaskingTape = true;
			this.Box.Prompt.enabled = true;
			this.Box.enabled = true;
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04002319 RID: 8985
	public CarryableCardboardBoxScript Box;

	// Token: 0x0400231A RID: 8986
	public PromptScript Prompt;
}
