using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000424 RID: 1060
public class ThanksForPlayingScript : MonoBehaviour
{
	// Token: 0x06001C4D RID: 7245 RVA: 0x001530D4 File Offset: 0x001512D4
	private void Start()
	{
		this.Ryoba["f02_faceCouncilGrace_00"].layer = 1;
		this.Ryoba.Play("f02_faceCouncilGrace_00");
		this.YandereKun["AltYanKunFace"].layer = 1;
		this.YandereKun.Play("AltYanKunFace");
		this.Darkness.color = new Color(0f, 0f, 0f, 1f);
		this.Alpha = 1f;
	}

	// Token: 0x06001C4E RID: 7246 RVA: 0x00153160 File Offset: 0x00151360
	private void Update()
	{
		if (!this.FadeOut)
		{
			this.Alpha = Mathf.MoveTowards(this.Alpha, 0f, Time.deltaTime * 0.5f);
			this.Darkness.color = new Color(0f, 0f, 0f, this.Alpha);
		}
		else
		{
			this.Alpha = Mathf.MoveTowards(this.Alpha, 1f, Time.deltaTime * 0.5f);
			this.Darkness.color = new Color(1f, 1f, 1f, this.Alpha);
			if (this.Alpha == 1f)
			{
				SceneManager.LoadScene("TitleScene");
			}
		}
		if (this.Yandere.position.z > 1f && this.Yandere.position.z < 10f)
		{
			this.ThankYouPanel.alpha = Mathf.MoveTowards(this.ThankYouPanel.alpha, 1f, Time.deltaTime * 0.5f);
		}
		else
		{
			this.ThankYouPanel.alpha = Mathf.MoveTowards(this.ThankYouPanel.alpha, 0f, Time.deltaTime * 0.5f);
		}
		if (this.Yandere.position.z > 20f && this.Yandere.position.z < 120f)
		{
			this.FinalGamePanel.alpha = Mathf.MoveTowards(this.FinalGamePanel.alpha, 1f, Time.deltaTime * 0.5f);
		}
		else
		{
			this.FinalGamePanel.alpha = Mathf.MoveTowards(this.FinalGamePanel.alpha, 0f, Time.deltaTime * 0.5f);
		}
		if (this.Yandere.position.z > 30f && this.Yandere.position.z < 40f)
		{
			this.RivalPanel.alpha = Mathf.MoveTowards(this.RivalPanel.alpha, 1f, Time.deltaTime * 0.5f);
		}
		else
		{
			this.RivalPanel.alpha = Mathf.MoveTowards(this.RivalPanel.alpha, 0f, Time.deltaTime * 0.5f);
		}
		if (this.Yandere.position.z > 50f && this.Yandere.position.z < 60f)
		{
			this.QualityPanel.alpha = Mathf.MoveTowards(this.QualityPanel.alpha, 1f, Time.deltaTime * 0.5f);
		}
		else
		{
			this.QualityPanel.alpha = Mathf.MoveTowards(this.QualityPanel.alpha, 0f, Time.deltaTime * 0.5f);
		}
		if (this.Yandere.position.z > 70f && this.Yandere.position.z < 80f)
		{
			this.WeaponsPanel.alpha = Mathf.MoveTowards(this.WeaponsPanel.alpha, 1f, Time.deltaTime * 0.5f);
		}
		else
		{
			this.WeaponsPanel.alpha = Mathf.MoveTowards(this.WeaponsPanel.alpha, 0f, Time.deltaTime * 0.5f);
		}
		if (this.Yandere.position.z > 90f && this.Yandere.position.z < 100f)
		{
			this.StoryPanel.alpha = Mathf.MoveTowards(this.StoryPanel.alpha, 1f, Time.deltaTime * 0.5f);
		}
		else
		{
			this.StoryPanel.alpha = Mathf.MoveTowards(this.StoryPanel.alpha, 0f, Time.deltaTime * 0.5f);
		}
		if (this.Yandere.position.z > 110f && this.Yandere.position.z < 120f)
		{
			this.MorePanel.alpha = Mathf.MoveTowards(this.MorePanel.alpha, 1f, Time.deltaTime * 0.5f);
		}
		else
		{
			this.MorePanel.alpha = Mathf.MoveTowards(this.MorePanel.alpha, 0f, Time.deltaTime * 0.5f);
		}
		if (this.Yandere.position.z > 130f && this.Yandere.position.z < 140f)
		{
			this.CrowdfundPanel.alpha = Mathf.MoveTowards(this.CrowdfundPanel.alpha, 1f, Time.deltaTime * 0.5f);
			if (Input.GetButtonDown("A"))
			{
				this.FadeOut = true;
				return;
			}
		}
		else
		{
			this.CrowdfundPanel.alpha = Mathf.MoveTowards(this.CrowdfundPanel.alpha, 0f, Time.deltaTime * 0.5f);
		}
	}

	// Token: 0x04003514 RID: 13588
	public UIPanel ThankYouPanel;

	// Token: 0x04003515 RID: 13589
	public UIPanel FinalGamePanel;

	// Token: 0x04003516 RID: 13590
	public UIPanel RivalPanel;

	// Token: 0x04003517 RID: 13591
	public UIPanel QualityPanel;

	// Token: 0x04003518 RID: 13592
	public UIPanel WeaponsPanel;

	// Token: 0x04003519 RID: 13593
	public UIPanel StoryPanel;

	// Token: 0x0400351A RID: 13594
	public UIPanel MorePanel;

	// Token: 0x0400351B RID: 13595
	public UIPanel CrowdfundPanel;

	// Token: 0x0400351C RID: 13596
	public Transform Yandere;

	// Token: 0x0400351D RID: 13597
	public UISprite Darkness;

	// Token: 0x0400351E RID: 13598
	public Animation YandereKun;

	// Token: 0x0400351F RID: 13599
	public Animation Ryoba;

	// Token: 0x04003520 RID: 13600
	public bool FadeOut;

	// Token: 0x04003521 RID: 13601
	public float Alpha;
}
