using System;
using UnityEngine;

// Token: 0x0200024C RID: 588
public class CounselorDoorScript : MonoBehaviour
{
	// Token: 0x060012B3 RID: 4787 RVA: 0x00092C6D File Offset: 0x00090E6D
	private void Start()
	{
		this.Prompt.enabled = false;
		this.Prompt.Hide();
	}

	// Token: 0x060012B4 RID: 4788 RVA: 0x00092C88 File Offset: 0x00090E88
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Circle[0].fillAmount = 1f;
			if (!this.Prompt.Yandere.Chased && this.Prompt.Yandere.Chasers == 0 && !this.FadeIn && this.Prompt.Yandere.Bloodiness == 0f && this.Prompt.Yandere.Sanity > 66.66666f && !this.Prompt.Yandere.Carrying && !this.Prompt.Yandere.Dragging)
			{
				if (!this.Counselor.Busy)
				{
					this.Prompt.Yandere.CharacterAnimation.CrossFade(this.Prompt.Yandere.IdleAnim);
					this.Prompt.Yandere.Police.Darkness.enabled = true;
					this.Prompt.Yandere.CanMove = false;
					this.FadeOut = true;
				}
				else
				{
					this.Counselor.CounselorSubtitle.text = this.Counselor.CounselorBusyText;
					this.Counselor.MyAudio.clip = this.Counselor.CounselorBusyClip;
					this.Counselor.MyAudio.Play();
				}
			}
		}
		if (this.FadeOut)
		{
			float a = Mathf.MoveTowards(this.Darkness.color.a, 1f, Time.deltaTime);
			this.Darkness.color = new Color(0f, 0f, 0f, a);
			if (this.Darkness.color.a == 1f)
			{
				if (!this.Exit)
				{
					this.Prompt.Yandere.CharacterAnimation.Play("f02_sit_00");
					this.Prompt.Yandere.transform.position = new Vector3(-27.51f, 0f, 12f);
					this.Prompt.Yandere.transform.localEulerAngles = new Vector3(0f, -90f, 0f);
					this.Counselor.Talk();
					this.FadeOut = false;
					this.FadeIn = true;
				}
				else
				{
					this.Darkness.color = new Color(0f, 0f, 0f, 2f);
					this.Counselor.Quit();
					this.FadeOut = false;
					this.FadeIn = true;
					this.Exit = false;
				}
			}
		}
		if (this.FadeIn)
		{
			float a2 = Mathf.MoveTowards(this.Darkness.color.a, 0f, Time.deltaTime);
			this.Darkness.color = new Color(0f, 0f, 0f, a2);
			if (this.Darkness.color.a == 0f)
			{
				this.FadeIn = false;
			}
		}
	}

	// Token: 0x040017CB RID: 6091
	public CounselorScript Counselor;

	// Token: 0x040017CC RID: 6092
	public PromptScript Prompt;

	// Token: 0x040017CD RID: 6093
	public UISprite Darkness;

	// Token: 0x040017CE RID: 6094
	public bool FadeOut;

	// Token: 0x040017CF RID: 6095
	public bool FadeIn;

	// Token: 0x040017D0 RID: 6096
	public bool Exit;
}
