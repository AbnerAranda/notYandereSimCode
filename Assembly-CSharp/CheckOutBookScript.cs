using System;
using UnityEngine;

// Token: 0x02000232 RID: 562
public class CheckOutBookScript : MonoBehaviour
{
	// Token: 0x06001238 RID: 4664 RVA: 0x000819E5 File Offset: 0x0007FBE5
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Yandere.Inventory.Book = true;
			this.UpdatePrompt();
		}
	}

	// Token: 0x06001239 RID: 4665 RVA: 0x00081A1C File Offset: 0x0007FC1C
	public void UpdatePrompt()
	{
		if (this.Prompt.Yandere.Inventory.Book)
		{
			this.Prompt.enabled = false;
			this.Prompt.Hide();
			return;
		}
		this.Prompt.enabled = true;
		this.Prompt.Hide();
	}

	// Token: 0x04001593 RID: 5523
	public PromptScript Prompt;
}
