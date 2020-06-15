using System;
using UnityEngine;

// Token: 0x02000274 RID: 628
public class DrinkingFountainScript : MonoBehaviour
{
	// Token: 0x0600136F RID: 4975 RVA: 0x000A78FC File Offset: 0x000A5AFC
	private void Update()
	{
		if (this.Prompt.Yandere.EquippedWeapon != null)
		{
			if (this.Prompt.Yandere.EquippedWeapon.Blood.enabled)
			{
				this.Prompt.HideButton[0] = false;
				this.Prompt.enabled = true;
			}
			else
			{
				this.Prompt.HideButton[0] = true;
			}
			if (!this.Leak.activeInHierarchy)
			{
				if (this.Prompt.Yandere.EquippedWeapon.WeaponID == 24)
				{
					this.Prompt.HideButton[1] = false;
					this.Prompt.enabled = true;
				}
				else
				{
					this.Prompt.HideButton[1] = true;
				}
			}
			if (this.Prompt.Circle[0].fillAmount == 0f)
			{
				this.Prompt.Circle[0].fillAmount = 1f;
				this.Prompt.Yandere.CharacterAnimation.CrossFade("f02_cleaningWeapon_00");
				this.Prompt.Yandere.Target = this.DrinkPosition;
				this.Prompt.Yandere.CleaningWeapon = true;
				this.Prompt.Yandere.CanMove = false;
				this.WaterStream.Play();
			}
			if (this.Prompt.Circle[1].fillAmount == 0f)
			{
				this.Prompt.HideButton[1] = true;
				this.Puddle.SetActive(true);
				this.Leak.SetActive(true);
				this.MyAudio.Play();
				this.PowerSwitch.CheckPuddle();
				return;
			}
		}
		else if (this.Prompt.enabled)
		{
			this.Prompt.Hide();
			this.Prompt.enabled = false;
		}
	}

	// Token: 0x04001A90 RID: 6800
	public PowerSwitchScript PowerSwitch;

	// Token: 0x04001A91 RID: 6801
	public ParticleSystem WaterStream;

	// Token: 0x04001A92 RID: 6802
	public Transform DrinkPosition;

	// Token: 0x04001A93 RID: 6803
	public GameObject Puddle;

	// Token: 0x04001A94 RID: 6804
	public GameObject Leak;

	// Token: 0x04001A95 RID: 6805
	public PromptScript Prompt;

	// Token: 0x04001A96 RID: 6806
	public AudioSource MyAudio;

	// Token: 0x04001A97 RID: 6807
	public bool Occupied;
}
