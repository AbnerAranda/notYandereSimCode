using System;
using UnityEngine;

// Token: 0x0200037C RID: 892
public class PowerSwitchScript : MonoBehaviour
{
	// Token: 0x06001955 RID: 6485 RVA: 0x000F34EC File Offset: 0x000F16EC
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Circle[0].fillAmount = 1f;
			this.On = !this.On;
			if (this.On)
			{
				this.Prompt.Label[0].text = "     Turn Off";
				this.MyAudio.clip = this.Flick[1];
			}
			else
			{
				this.Prompt.Label[0].text = "     Turn On";
				this.MyAudio.clip = this.Flick[0];
			}
			if (this.BathroomLight != null)
			{
				this.BathroomLight.enabled = !this.BathroomLight.enabled;
			}
			this.CheckPuddle();
			this.MyAudio.Play();
		}
	}

	// Token: 0x06001956 RID: 6486 RVA: 0x000F35D4 File Offset: 0x000F17D4
	public void CheckPuddle()
	{
		if (this.On)
		{
			if (this.DrinkingFountain.Puddle != null && this.DrinkingFountain.Puddle.gameObject.activeInHierarchy && this.PowerOutlet.SabotagedOutlet.activeInHierarchy)
			{
				this.Electricity.SetActive(true);
				return;
			}
		}
		else
		{
			this.Electricity.SetActive(false);
		}
	}

	// Token: 0x0400269A RID: 9882
	public DrinkingFountainScript DrinkingFountain;

	// Token: 0x0400269B RID: 9883
	public PowerOutletScript PowerOutlet;

	// Token: 0x0400269C RID: 9884
	public GameObject Electricity;

	// Token: 0x0400269D RID: 9885
	public Light BathroomLight;

	// Token: 0x0400269E RID: 9886
	public PromptScript Prompt;

	// Token: 0x0400269F RID: 9887
	public AudioSource MyAudio;

	// Token: 0x040026A0 RID: 9888
	public AudioClip[] Flick;

	// Token: 0x040026A1 RID: 9889
	public bool On;
}
