using System;
using UnityEngine;

// Token: 0x02000399 RID: 921
public class RivalDeskScript : MonoBehaviour
{
	// Token: 0x060019D1 RID: 6609 RVA: 0x000FDFD3 File Offset: 0x000FC1D3
	private void Start()
	{
		if (DateGlobals.Weekday != DayOfWeek.Friday)
		{
			base.enabled = false;
		}
	}

	// Token: 0x060019D2 RID: 6610 RVA: 0x000FDFE4 File Offset: 0x000FC1E4
	private void Update()
	{
		if (!this.Prompt.Yandere.Inventory.AnswerSheet && this.Prompt.Yandere.Inventory.DuplicateSheet)
		{
			this.Prompt.enabled = true;
			if (this.Clock.HourTime > 13f)
			{
				this.Prompt.HideButton[0] = false;
				if (this.Clock.HourTime > 13.5f)
				{
					SchemeGlobals.SetSchemeStage(5, 100);
					this.Schemes.UpdateInstructions();
					this.Prompt.HideButton[0] = true;
				}
			}
			if (this.Prompt.Circle[0].fillAmount == 0f)
			{
				SchemeGlobals.SetSchemeStage(5, 9);
				this.Schemes.UpdateInstructions();
				this.Prompt.Yandere.Inventory.DuplicateSheet = false;
				this.Prompt.Hide();
				this.Prompt.enabled = false;
				this.Cheating = true;
				base.enabled = false;
			}
		}
	}

	// Token: 0x0400280B RID: 10251
	public SchemesScript Schemes;

	// Token: 0x0400280C RID: 10252
	public ClockScript Clock;

	// Token: 0x0400280D RID: 10253
	public PromptScript Prompt;

	// Token: 0x0400280E RID: 10254
	public bool Cheating;
}
