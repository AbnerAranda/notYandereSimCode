using System;
using UnityEngine;

// Token: 0x02000367 RID: 871
public class PianoScript : MonoBehaviour
{
	// Token: 0x0600190A RID: 6410 RVA: 0x000EA8A0 File Offset: 0x000E8AA0
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount < 1f && this.Prompt.Circle[0].fillAmount > 0f)
		{
			this.Prompt.Circle[0].fillAmount = 0f;
			this.Notes[this.ID].Play();
			this.ID++;
			if (this.ID == this.Notes.Length)
			{
				this.ID = 0;
			}
		}
	}

	// Token: 0x04002583 RID: 9603
	public PromptScript Prompt;

	// Token: 0x04002584 RID: 9604
	public AudioSource[] Notes;

	// Token: 0x04002585 RID: 9605
	public int ID;
}
