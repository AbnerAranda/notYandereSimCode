using System;
using UnityEngine;

// Token: 0x020002E6 RID: 742
public class HidingSpotScript : MonoBehaviour
{
	// Token: 0x06001718 RID: 5912 RVA: 0x000C3C50 File Offset: 0x000C1E50
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Circle[0].fillAmount = 1f;
			if (!this.Prompt.Yandere.Chased && this.Prompt.Yandere.Chasers == 0 && this.Prompt.Yandere.Pursuer == null)
			{
				if (this.Bench)
				{
					this.Prompt.Yandere.MyController.radius = 0.1f;
				}
				else
				{
					this.Prompt.Yandere.MyController.center = new Vector3(this.Prompt.Yandere.MyController.center.x, 0.3f, this.Prompt.Yandere.MyController.center.z);
					this.Prompt.Yandere.MyController.radius = 0f;
					this.Prompt.Yandere.MyController.height = 0.5f;
				}
				this.Prompt.Yandere.HideAnim = this.AnimName;
				this.Prompt.Yandere.HidingSpot = this.Spot;
				this.Prompt.Yandere.ExitSpot = this.Exit;
				this.Prompt.Yandere.CanMove = false;
				this.Prompt.Yandere.Hiding = true;
				this.Prompt.Yandere.EmptyHands();
				this.PromptBar.ClearButtons();
				this.PromptBar.Label[1].text = "Stop";
				this.PromptBar.UpdateButtons();
				this.PromptBar.Show = true;
			}
		}
	}

	// Token: 0x04001F3B RID: 7995
	public PromptBarScript PromptBar;

	// Token: 0x04001F3C RID: 7996
	public PromptScript Prompt;

	// Token: 0x04001F3D RID: 7997
	public Transform Exit;

	// Token: 0x04001F3E RID: 7998
	public Transform Spot;

	// Token: 0x04001F3F RID: 7999
	public string AnimName;

	// Token: 0x04001F40 RID: 8000
	public bool Bench;
}
