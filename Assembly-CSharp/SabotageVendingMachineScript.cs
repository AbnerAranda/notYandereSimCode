using System;
using UnityEngine;

// Token: 0x020003AE RID: 942
public class SabotageVendingMachineScript : MonoBehaviour
{
	// Token: 0x060019FB RID: 6651 RVA: 0x000FF0B1 File Offset: 0x000FD2B1
	private void Start()
	{
		this.Prompt.enabled = false;
		this.Prompt.Hide();
	}

	// Token: 0x060019FC RID: 6652 RVA: 0x000FF0CC File Offset: 0x000FD2CC
	private void Update()
	{
		if (this.Yandere.Armed)
		{
			if (this.Yandere.EquippedWeapon.WeaponID == 6)
			{
				this.Prompt.enabled = true;
				if (this.Prompt.Circle[0].fillAmount == 0f)
				{
					if (SchemeGlobals.GetSchemeStage(4) == 2)
					{
						SchemeGlobals.SetSchemeStage(4, 3);
						this.Yandere.PauseScreen.Schemes.UpdateInstructions();
					}
					if (this.Yandere.StudentManager.Students[11] != null && DateGlobals.Weekday == DayOfWeek.Thursday)
					{
						this.Yandere.StudentManager.Students[11].Hungry = true;
						this.Yandere.StudentManager.Students[11].Fed = false;
					}
					UnityEngine.Object.Instantiate<GameObject>(this.SabotageSparks, new Vector3(-2.5f, 5.3605f, -32.982f), Quaternion.identity);
					this.VendingMachine.Sabotaged = true;
					this.Prompt.enabled = false;
					this.Prompt.Hide();
					base.enabled = false;
					return;
				}
			}
		}
		else if (this.Prompt.enabled)
		{
			this.Prompt.enabled = false;
			this.Prompt.Hide();
		}
	}

	// Token: 0x040028CF RID: 10447
	public VendingMachineScript VendingMachine;

	// Token: 0x040028D0 RID: 10448
	public GameObject SabotageSparks;

	// Token: 0x040028D1 RID: 10449
	public YandereScript Yandere;

	// Token: 0x040028D2 RID: 10450
	public PromptScript Prompt;
}
