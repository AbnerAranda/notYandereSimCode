using System;
using UnityEngine;

// Token: 0x0200037B RID: 891
public class PowerOutletScript : MonoBehaviour
{
	// Token: 0x06001953 RID: 6483 RVA: 0x000F3278 File Offset: 0x000F1478
	private void Update()
	{
		if (this.PowerStrip == null)
		{
			if (this.Prompt.Yandere.PickUp != null)
			{
				if (this.Prompt.Yandere.PickUp.Electronic)
				{
					this.Prompt.enabled = true;
				}
				else if (this.Prompt.enabled)
				{
					this.Prompt.Hide();
					this.Prompt.enabled = false;
				}
				if (this.Prompt.Circle[0].fillAmount == 0f)
				{
					this.Prompt.Circle[0].fillAmount = 1f;
					this.PowerStrip = this.Prompt.Yandere.PickUp.gameObject;
					this.Prompt.Yandere.EmptyHands();
					this.PowerStrip.transform.parent = base.transform;
					this.PowerStrip.transform.localPosition = new Vector3(0f, 0f, 0f);
					this.PowerStrip.SetActive(false);
					this.PluggedOutlet.SetActive(true);
					this.Prompt.HideButton[0] = true;
					return;
				}
			}
			else if (this.Prompt.enabled)
			{
				this.Prompt.Hide();
				this.Prompt.enabled = false;
				return;
			}
		}
		else if (this.Prompt.Yandere.EquippedWeapon != null)
		{
			if (this.Prompt.Yandere.EquippedWeapon.WeaponID == 6)
			{
				this.Prompt.HideButton[1] = false;
				this.Prompt.enabled = true;
			}
			else if (this.Prompt.enabled)
			{
				this.Prompt.Hide();
				this.Prompt.enabled = false;
			}
			if (this.Prompt.Circle[1].fillAmount == 0f)
			{
				this.Prompt.Circle[1].fillAmount = 1f;
				this.SabotagedOutlet.SetActive(true);
				this.PluggedOutlet.SetActive(false);
				this.PowerSwitch.CheckPuddle();
				this.Prompt.Hide();
				this.Prompt.enabled = false;
				base.enabled = false;
				return;
			}
		}
		else if (this.Prompt.enabled)
		{
			this.Prompt.Hide();
			this.Prompt.enabled = false;
		}
	}

	// Token: 0x04002694 RID: 9876
	public PromptScript Prompt;

	// Token: 0x04002695 RID: 9877
	public PowerSwitchScript PowerSwitch;

	// Token: 0x04002696 RID: 9878
	public GameObject PowerStrip;

	// Token: 0x04002697 RID: 9879
	public GameObject PluggedOutlet;

	// Token: 0x04002698 RID: 9880
	public GameObject SabotagedOutlet;

	// Token: 0x04002699 RID: 9881
	public bool Sabotaged;
}
