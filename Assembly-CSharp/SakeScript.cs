using System;
using UnityEngine;

// Token: 0x020003B0 RID: 944
public class SakeScript : MonoBehaviour
{
	// Token: 0x06001A01 RID: 6657 RVA: 0x000FF48A File Offset: 0x000FD68A
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Yandere.Inventory.Sake = true;
			this.UpdatePrompt();
		}
	}

	// Token: 0x06001A02 RID: 6658 RVA: 0x000FF4C4 File Offset: 0x000FD6C4
	public void UpdatePrompt()
	{
		if (this.Prompt.Yandere.Inventory.Sake)
		{
			this.Prompt.enabled = false;
			this.Prompt.Hide();
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		this.Prompt.enabled = true;
		this.Prompt.Hide();
	}

	// Token: 0x040028DB RID: 10459
	public PromptScript Prompt;
}
