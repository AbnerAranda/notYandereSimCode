using System;
using UnityEngine;

// Token: 0x020003AB RID: 939
public class RoseBushScript : MonoBehaviour
{
	// Token: 0x060019F2 RID: 6642 RVA: 0x000FED14 File Offset: 0x000FCF14
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Yandere.Inventory.Rose = true;
			base.enabled = false;
			this.Prompt.Hide();
			this.Prompt.enabled = false;
		}
	}

	// Token: 0x040028C1 RID: 10433
	public PromptScript Prompt;
}
