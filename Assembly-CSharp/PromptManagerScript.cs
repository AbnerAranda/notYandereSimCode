using System;
using UnityEngine;

// Token: 0x02000381 RID: 897
public class PromptManagerScript : MonoBehaviour
{
	// Token: 0x0600196F RID: 6511 RVA: 0x000F584C File Offset: 0x000F3A4C
	private void Update()
	{
		if (this.Yandere.transform.position.z < -38f)
		{
			if (!this.Outside)
			{
				this.Outside = true;
				foreach (PromptScript promptScript in this.Prompts)
				{
					if (promptScript != null)
					{
						promptScript.enabled = false;
					}
				}
				return;
			}
		}
		else if (this.Outside)
		{
			this.Outside = false;
			foreach (PromptScript promptScript2 in this.Prompts)
			{
				if (promptScript2 != null)
				{
					promptScript2.enabled = true;
				}
			}
		}
	}

	// Token: 0x040026E5 RID: 9957
	public PromptScript[] Prompts;

	// Token: 0x040026E6 RID: 9958
	public int ID;

	// Token: 0x040026E7 RID: 9959
	public Transform Yandere;

	// Token: 0x040026E8 RID: 9960
	public bool Outside;
}
