using System;
using UnityEngine;

// Token: 0x02000236 RID: 566
public class ChinaDressScript : MonoBehaviour
{
	// Token: 0x06001241 RID: 4673 RVA: 0x00081DF0 File Offset: 0x0007FFF0
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Yandere.WearChinaDress();
			this.Prompt.Hide();
			this.Prompt.enabled = false;
			base.enabled = false;
		}
	}

	// Token: 0x040015A4 RID: 5540
	public PromptScript Prompt;
}
