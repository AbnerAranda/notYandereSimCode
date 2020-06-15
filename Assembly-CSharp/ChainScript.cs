using System;
using UnityEngine;

// Token: 0x0200022D RID: 557
public class ChainScript : MonoBehaviour
{
	// Token: 0x0600122C RID: 4652 RVA: 0x000810C8 File Offset: 0x0007F2C8
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Circle[0].fillAmount = 1f;
			if (this.Prompt.Yandere.Inventory.MysteriousKeys > 0)
			{
				AudioSource.PlayClipAtPoint(this.ChainRattle, base.transform.position);
				this.Unlocked++;
				this.Chains[this.Unlocked].SetActive(false);
				this.Prompt.Yandere.Inventory.MysteriousKeys--;
				if (this.Unlocked == 5)
				{
					this.Tarp.Prompt.enabled = true;
					this.Tarp.enabled = true;
					this.Prompt.Hide();
					this.Prompt.enabled = false;
					UnityEngine.Object.Destroy(base.gameObject);
				}
			}
		}
	}

	// Token: 0x0400156C RID: 5484
	public PromptScript Prompt;

	// Token: 0x0400156D RID: 5485
	public TarpScript Tarp;

	// Token: 0x0400156E RID: 5486
	public AudioClip ChainRattle;

	// Token: 0x0400156F RID: 5487
	public GameObject[] Chains;

	// Token: 0x04001570 RID: 5488
	public int Unlocked;
}
