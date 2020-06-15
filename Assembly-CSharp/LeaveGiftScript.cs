using System;
using UnityEngine;

// Token: 0x0200031C RID: 796
public class LeaveGiftScript : MonoBehaviour
{
	// Token: 0x060017FB RID: 6139 RVA: 0x000D3F54 File Offset: 0x000D2154
	private void Start()
	{
		this.Box.SetActive(false);
		this.EndOfDay.SenpaiGifts = CollectibleGlobals.SenpaiGifts;
		if (this.EndOfDay.SenpaiGifts == 0)
		{
			this.Prompt.Hide();
			this.Prompt.enabled = false;
			base.enabled = false;
		}
	}

	// Token: 0x060017FC RID: 6140 RVA: 0x000D3FA8 File Offset: 0x000D21A8
	private void Update()
	{
		Debug.Log(Vector3.Distance(this.Prompt.Yandere.transform.position, this.Prompt.Yandere.Senpai.position));
		if (this.Prompt.InView)
		{
			if (Vector3.Distance(this.Prompt.Yandere.transform.position, this.Prompt.Yandere.Senpai.position) > 10f)
			{
				if (this.Prompt.Circle[0].fillAmount == 0f)
				{
					this.EndOfDay.SenpaiGifts--;
					this.Prompt.Hide();
					this.Prompt.enabled = false;
					this.Box.SetActive(true);
					base.enabled = false;
					return;
				}
			}
			else
			{
				this.Prompt.Hide();
			}
		}
	}

	// Token: 0x04002272 RID: 8818
	public EndOfDayScript EndOfDay;

	// Token: 0x04002273 RID: 8819
	public PromptScript Prompt;

	// Token: 0x04002274 RID: 8820
	public GameObject Box;
}
