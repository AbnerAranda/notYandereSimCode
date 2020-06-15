using System;
using UnityEngine;

// Token: 0x020002FA RID: 762
public class HomeVideoCameraScript : MonoBehaviour
{
	// Token: 0x06001767 RID: 5991 RVA: 0x000CA704 File Offset: 0x000C8904
	private void Update()
	{
		if (!this.TextSet && !HomeGlobals.Night)
		{
			this.Prompt.Label[0].text = "     Only Available At Night";
		}
		if (!HomeGlobals.Night)
		{
			this.Prompt.Circle[0].fillAmount = 1f;
		}
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.HomeCamera.Destination = this.HomeCamera.Destinations[11];
			this.HomeCamera.Target = this.HomeCamera.Targets[11];
			this.HomeCamera.ID = 11;
			this.HomePrisonerChan.LookAhead = true;
			this.HomeYandere.CanMove = false;
			this.HomeYandere.gameObject.SetActive(false);
		}
		if (this.HomeCamera.ID == 11 && !this.HomePrisoner.Bantering)
		{
			this.Timer += Time.deltaTime;
			AudioSource component = base.GetComponent<AudioSource>();
			if (this.Timer > 2f && !this.AudioPlayed)
			{
				this.Subtitle.text = "...daddy...please...help...I'm scared...I don't wanna die...";
				this.AudioPlayed = true;
				component.Play();
			}
			if (this.Timer > 2f + component.clip.length)
			{
				this.Subtitle.text = string.Empty;
			}
			if (this.Timer > 3f + component.clip.length)
			{
				this.HomeDarkness.FadeSlow = true;
				this.HomeDarkness.FadeOut = true;
			}
		}
	}

	// Token: 0x0400207D RID: 8317
	public HomePrisonerChanScript HomePrisonerChan;

	// Token: 0x0400207E RID: 8318
	public HomeDarknessScript HomeDarkness;

	// Token: 0x0400207F RID: 8319
	public HomePrisonerScript HomePrisoner;

	// Token: 0x04002080 RID: 8320
	public HomeYandereScript HomeYandere;

	// Token: 0x04002081 RID: 8321
	public HomeCameraScript HomeCamera;

	// Token: 0x04002082 RID: 8322
	public PromptScript Prompt;

	// Token: 0x04002083 RID: 8323
	public UILabel Subtitle;

	// Token: 0x04002084 RID: 8324
	public bool AudioPlayed;

	// Token: 0x04002085 RID: 8325
	public bool TextSet;

	// Token: 0x04002086 RID: 8326
	public float Timer;
}
