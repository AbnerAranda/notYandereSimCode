using System;
using UnityEngine;

// Token: 0x020003E7 RID: 999
public class SmokeBombBoxScript : MonoBehaviour
{
	// Token: 0x06001AC4 RID: 6852 RVA: 0x0010C31C File Offset: 0x0010A51C
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			if (!this.Amnesia)
			{
				this.Alphabet.RemainingBombs = 5;
				this.Alphabet.BombLabel.text = string.Concat(5);
			}
			else
			{
				this.Alphabet.RemainingBombs = 1;
				this.Alphabet.BombLabel.text = string.Concat(1);
			}
			this.Prompt.Circle[0].fillAmount = 1f;
			if (this.Stink)
			{
				this.BombTexture.color = new Color(0f, 0.5f, 0f, 1f);
				this.Prompt.Yandere.Inventory.AmnesiaBomb = false;
				this.Prompt.Yandere.Inventory.SmokeBomb = false;
				this.Prompt.Yandere.Inventory.StinkBomb = true;
			}
			else if (this.Amnesia)
			{
				this.BombTexture.color = new Color(1f, 0.5f, 1f, 1f);
				this.Prompt.Yandere.Inventory.AmnesiaBomb = true;
				this.Prompt.Yandere.Inventory.SmokeBomb = false;
				this.Prompt.Yandere.Inventory.StinkBomb = false;
			}
			else
			{
				this.BombTexture.color = new Color(0.5f, 0.5f, 0.5f, 1f);
				this.Prompt.Yandere.Inventory.AmnesiaBomb = false;
				this.Prompt.Yandere.Inventory.StinkBomb = false;
				this.Prompt.Yandere.Inventory.SmokeBomb = true;
			}
			this.MyAudio.Play();
		}
	}

	// Token: 0x04002B35 RID: 11061
	public AlphabetScript Alphabet;

	// Token: 0x04002B36 RID: 11062
	public UITexture BombTexture;

	// Token: 0x04002B37 RID: 11063
	public PromptScript Prompt;

	// Token: 0x04002B38 RID: 11064
	public AudioSource MyAudio;

	// Token: 0x04002B39 RID: 11065
	public bool Amnesia;

	// Token: 0x04002B3A RID: 11066
	public bool Stink;
}
