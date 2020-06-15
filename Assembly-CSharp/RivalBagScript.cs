using System;
using UnityEngine;

// Token: 0x02000398 RID: 920
public class RivalBagScript : MonoBehaviour
{
	// Token: 0x060019CE RID: 6606 RVA: 0x000FDDDE File Offset: 0x000FBFDE
	private void Start()
	{
		this.Prompt.enabled = false;
		this.Prompt.Hide();
		base.enabled = false;
	}

	// Token: 0x060019CF RID: 6607 RVA: 0x000FDE00 File Offset: 0x000FC000
	private void Update()
	{
		if (this.Clock.Period == 2 || this.Clock.Period == 4)
		{
			this.Prompt.HideButton[0] = true;
		}
		else if (this.Prompt.Yandere.Inventory.Cigs)
		{
			this.Prompt.HideButton[0] = false;
		}
		else
		{
			this.Prompt.HideButton[0] = true;
		}
		if (this.Prompt.Yandere.Inventory.Cigs)
		{
			this.Prompt.enabled = true;
			if (this.Prompt.Circle[0].fillAmount == 0f)
			{
				SchemeGlobals.SetSchemeStage(3, 4);
				this.Schemes.UpdateInstructions();
				this.Prompt.Yandere.Inventory.Cigs = false;
				this.Prompt.enabled = false;
				this.Prompt.Hide();
				base.enabled = false;
			}
		}
		if (this.Clock.Period == 2 || this.Clock.Period == 4)
		{
			this.Prompt.HideButton[1] = true;
		}
		else if (this.Prompt.Yandere.Inventory.Ring)
		{
			this.Prompt.HideButton[1] = false;
		}
		else
		{
			this.Prompt.HideButton[1] = true;
		}
		if (this.Prompt.Yandere.Inventory.Ring)
		{
			this.Prompt.enabled = true;
			if (this.Prompt.Circle[1].fillAmount == 0f)
			{
				SchemeGlobals.SetSchemeStage(2, 3);
				this.Schemes.UpdateInstructions();
				this.Prompt.Yandere.Inventory.Ring = false;
				this.Prompt.enabled = false;
				this.Prompt.Hide();
				base.enabled = false;
			}
		}
	}

	// Token: 0x04002808 RID: 10248
	public SchemesScript Schemes;

	// Token: 0x04002809 RID: 10249
	public ClockScript Clock;

	// Token: 0x0400280A RID: 10250
	public PromptScript Prompt;
}
