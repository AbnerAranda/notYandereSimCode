using System;
using UnityEngine;

// Token: 0x02000256 RID: 598
public class CutsceneManagerScript : MonoBehaviour
{
	// Token: 0x060012F2 RID: 4850 RVA: 0x000996F4 File Offset: 0x000978F4
	private void Update()
	{
		AudioSource component = base.GetComponent<AudioSource>();
		if (this.Phase == 1)
		{
			this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, Mathf.MoveTowards(this.Darkness.color.a, 1f, Time.deltaTime));
			if (this.Darkness.color.a == 1f)
			{
				if (this.Scheme == 5)
				{
					this.Phase++;
					return;
				}
				this.Phase = 4;
				return;
			}
		}
		else
		{
			if (this.Phase == 2)
			{
				this.Subtitle.text = this.Text[this.Line];
				component.clip = this.Voice[this.Line];
				component.Play();
				this.Phase++;
				return;
			}
			if (this.Phase == 3)
			{
				if (!component.isPlaying || Input.GetButtonDown("A"))
				{
					if (this.Line < 2)
					{
						this.Phase--;
						this.Line++;
						return;
					}
					this.Subtitle.text = string.Empty;
					this.Phase++;
					return;
				}
			}
			else
			{
				if (this.Phase == 4)
				{
					Debug.Log("We're activating EndOfDay from CutsceneManager.");
					this.EndOfDay.gameObject.SetActive(true);
					this.EndOfDay.Phase = 14;
					if (this.Scheme == 5)
					{
						this.Counselor.LecturePhase = 5;
					}
					else
					{
						this.Counselor.LecturePhase = 1;
					}
					this.Phase++;
					return;
				}
				if (this.Phase == 6)
				{
					this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, Mathf.MoveTowards(this.Darkness.color.a, 0f, Time.deltaTime));
					if (this.Darkness.color.a == 0f)
					{
						this.Phase++;
						return;
					}
				}
				else if (this.Phase == 7)
				{
					if (this.Scheme == 5)
					{
						this.StudentManager.Students[this.StudentManager.RivalID] != null;
					}
					this.PromptBar.ClearButtons();
					this.PromptBar.Show = false;
					this.Portal.Proceed = true;
					base.gameObject.SetActive(false);
					this.Scheme = 0;
				}
			}
		}
	}

	// Token: 0x040018CC RID: 6348
	public StudentManagerScript StudentManager;

	// Token: 0x040018CD RID: 6349
	public CounselorScript Counselor;

	// Token: 0x040018CE RID: 6350
	public PromptBarScript PromptBar;

	// Token: 0x040018CF RID: 6351
	public EndOfDayScript EndOfDay;

	// Token: 0x040018D0 RID: 6352
	public PortalScript Portal;

	// Token: 0x040018D1 RID: 6353
	public UISprite Darkness;

	// Token: 0x040018D2 RID: 6354
	public UILabel Subtitle;

	// Token: 0x040018D3 RID: 6355
	public AudioClip[] Voice;

	// Token: 0x040018D4 RID: 6356
	public string[] Text;

	// Token: 0x040018D5 RID: 6357
	public int Scheme;

	// Token: 0x040018D6 RID: 6358
	public int Phase = 1;

	// Token: 0x040018D7 RID: 6359
	public int Line = 1;
}
