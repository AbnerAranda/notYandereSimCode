﻿using System;
using UnityEngine;

// Token: 0x0200035B RID: 859
public class PeekScript : MonoBehaviour
{
	// Token: 0x060018CD RID: 6349 RVA: 0x000E592F File Offset: 0x000E3B2F
	private void Start()
	{
		this.Prompt.Door = true;
	}

	// Token: 0x060018CE RID: 6350 RVA: 0x000E5940 File Offset: 0x000E3B40
	private void Update()
	{
		if (Vector3.Distance(base.transform.position, this.Prompt.Yandere.transform.position) < 2f)
		{
			this.Prompt.Yandere.StudentManager.TutorialWindow.ShowInfoMessage = true;
		}
		if (this.InfoChanWindow.Drop)
		{
			this.Prompt.Circle[0].fillAmount = 1f;
		}
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Circle[0].fillAmount = 1f;
			if (!this.Prompt.Yandere.Chased && this.Prompt.Yandere.Chasers == 0)
			{
				this.Prompt.Yandere.CanMove = false;
				this.PeekCamera.SetActive(true);
				this.PromptBar.ClearButtons();
				this.PromptBar.Label[1].text = "Stop";
				this.PromptBar.UpdateButtons();
				this.PromptBar.Show = true;
			}
		}
		if (this.PeekCamera.activeInHierarchy)
		{
			this.Timer += Time.deltaTime;
			if (this.Timer > 5f && !this.Spoke)
			{
				this.Subtitle.UpdateLabel(SubtitleType.InfoNotice, 0, 6.5f);
				this.Spoke = true;
				base.GetComponent<AudioSource>().Play();
			}
			if (Input.GetButtonDown("B") || this.Prompt.Yandere.Noticed || this.Prompt.Yandere.Sprayed)
			{
				if (!this.Prompt.Yandere.Noticed && !this.Prompt.Yandere.Sprayed)
				{
					this.Prompt.Yandere.CanMove = true;
				}
				this.PeekCamera.SetActive(false);
				this.PromptBar.ClearButtons();
				this.PromptBar.Show = false;
				this.Timer = 0f;
			}
		}
	}

	// Token: 0x040024DC RID: 9436
	public InfoChanWindowScript InfoChanWindow;

	// Token: 0x040024DD RID: 9437
	public PromptBarScript PromptBar;

	// Token: 0x040024DE RID: 9438
	public SubtitleScript Subtitle;

	// Token: 0x040024DF RID: 9439
	public JukeboxScript Jukebox;

	// Token: 0x040024E0 RID: 9440
	public PromptScript Prompt;

	// Token: 0x040024E1 RID: 9441
	public GameObject PeekCamera;

	// Token: 0x040024E2 RID: 9442
	public bool Spoke;

	// Token: 0x040024E3 RID: 9443
	public float Timer;
}
