using System;
using UnityEngine;

// Token: 0x02000234 RID: 564
public class CheeseScript : MonoBehaviour
{
	// Token: 0x0600123D RID: 4669 RVA: 0x00081AD4 File Offset: 0x0007FCD4
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Subtitle.text = "Knowing the mouse might one day leave its hole and get the cheese...It fills you with determination.";
			this.Prompt.Hide();
			this.Prompt.enabled = false;
			this.GlowingEye.SetActive(true);
			this.Timer = 5f;
		}
		if (this.Timer > 0f)
		{
			this.Timer -= Time.deltaTime;
			if (this.Timer <= 0f)
			{
				this.Prompt.enabled = true;
				this.Subtitle.text = string.Empty;
			}
		}
	}

	// Token: 0x04001597 RID: 5527
	public GameObject GlowingEye;

	// Token: 0x04001598 RID: 5528
	public PromptScript Prompt;

	// Token: 0x04001599 RID: 5529
	public UILabel Subtitle;

	// Token: 0x0400159A RID: 5530
	public float Timer;
}
