using System;
using UnityEngine;

// Token: 0x020000F2 RID: 242
public class BugScript : MonoBehaviour
{
	// Token: 0x06000A9D RID: 2717 RVA: 0x00058852 File Offset: 0x00056A52
	private void Start()
	{
		this.MyRenderer.enabled = false;
	}

	// Token: 0x06000A9E RID: 2718 RVA: 0x00058860 File Offset: 0x00056A60
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.MyAudio.clip = this.Praise[UnityEngine.Random.Range(0, this.Praise.Length)];
			this.MyAudio.Play();
			this.MyRenderer.enabled = true;
			this.Prompt.Yandere.Inventory.PantyShots += 5;
			base.enabled = false;
			this.Prompt.enabled = false;
			this.Prompt.Hide();
		}
	}

	// Token: 0x04000B4E RID: 2894
	public PromptScript Prompt;

	// Token: 0x04000B4F RID: 2895
	public Renderer MyRenderer;

	// Token: 0x04000B50 RID: 2896
	public AudioSource MyAudio;

	// Token: 0x04000B51 RID: 2897
	public AudioClip[] Praise;
}
