using System;
using UnityEngine;

// Token: 0x02000355 RID: 853
public class PaintBucketScript : MonoBehaviour
{
	// Token: 0x060018BA RID: 6330 RVA: 0x000E3420 File Offset: 0x000E1620
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Circle[0].fillAmount = 1f;
			if (this.Prompt.Yandere.Bloodiness == 0f)
			{
				this.Prompt.Yandere.Bloodiness += 100f;
				this.Prompt.Yandere.RedPaint = true;
			}
		}
	}

	// Token: 0x0400248F RID: 9359
	public PromptScript Prompt;
}
