using System;
using UnityEngine;

// Token: 0x02000476 RID: 1142
public class YandereShoeLockerScript : MonoBehaviour
{
	// Token: 0x06001DC1 RID: 7617 RVA: 0x00173EF4 File Offset: 0x001720F4
	private void Update()
	{
		if (this.Yandere.Schoolwear == 1 && !this.Yandere.ClubAttire && !this.Yandere.Egg)
		{
			if (this.Label == 2)
			{
				this.Prompt.Label[0].text = "     Change Shoes";
				this.Label = 1;
			}
			if (this.Prompt.Circle[0].fillAmount == 0f)
			{
				this.Prompt.Circle[0].fillAmount = 1f;
				this.Yandere.Casual = !this.Yandere.Casual;
				this.Yandere.ChangeSchoolwear();
				this.Yandere.CanMove = true;
				return;
			}
		}
		else
		{
			this.Prompt.Circle[0].fillAmount = 1f;
			if (this.Label == 1)
			{
				this.Prompt.Label[0].text = "     Not Available";
				this.Label = 2;
			}
		}
	}

	// Token: 0x04003AF8 RID: 15096
	public YandereScript Yandere;

	// Token: 0x04003AF9 RID: 15097
	public PromptScript Prompt;

	// Token: 0x04003AFA RID: 15098
	public int Label = 1;
}
