using System;
using UnityEngine;

// Token: 0x020000F9 RID: 249
public class ChallengeScript : MonoBehaviour
{
	// Token: 0x06000AAE RID: 2734 RVA: 0x000599FC File Offset: 0x00057BFC
	private void Update()
	{
		if (!this.Viewing)
		{
			if (!this.Switch)
			{
				if (this.InputManager.TappedUp || this.InputManager.TappedDown)
				{
					if (this.List == 0)
					{
						this.Arrows.localPosition = new Vector3(this.Arrows.localPosition.x, -300f, this.Arrows.localPosition.z);
						this.ViewButton.SetActive(true);
						this.Panels[0].alpha = 0.5f;
						this.Panels[1].alpha = 1f;
						this.List = 1;
					}
					else
					{
						this.Arrows.localPosition = new Vector3(this.Arrows.localPosition.x, 200f, this.Arrows.localPosition.z);
						this.ViewButton.SetActive(false);
						this.Panels[0].alpha = 1f;
						this.Panels[1].alpha = 0.5f;
						this.List = 0;
					}
				}
				Transform transform = this.ChallengeList[this.List];
				if (this.InputManager.DPadRight || Input.GetKey(KeyCode.RightArrow))
				{
					transform.localPosition = new Vector3(transform.localPosition.x - Time.deltaTime * 1000f, transform.localPosition.y, transform.localPosition.z);
				}
				if (this.InputManager.DPadLeft || Input.GetKey(KeyCode.LeftArrow))
				{
					transform.localPosition = new Vector3(transform.localPosition.x + Time.deltaTime * 1000f, transform.localPosition.y, transform.localPosition.z);
				}
				transform.localPosition = new Vector3(transform.localPosition.x + Input.GetAxis("Horizontal") * -10f, transform.localPosition.y, transform.localPosition.z);
				if (transform.localPosition.x > 500f)
				{
					transform.localPosition = new Vector3(500f, transform.localPosition.y, transform.localPosition.z);
				}
				else if (transform.localPosition.x < -250f * ((float)this.Challenges[this.List] - 3f))
				{
					transform.localPosition = new Vector3(-250f * ((float)this.Challenges[this.List] - 3f), transform.localPosition.y, transform.localPosition.z);
				}
				if (this.LargeIcon.color.a > 0f)
				{
					this.LargeIcon.color = new Color(this.LargeIcon.color.r, this.LargeIcon.color.g, this.LargeIcon.color.b, this.LargeIcon.color.a - Time.deltaTime * 10f);
					if (this.LargeIcon.color.a < 0f)
					{
						this.LargeIcon.color = new Color(this.LargeIcon.color.r, this.LargeIcon.color.g, this.LargeIcon.color.b, 0f);
					}
				}
			}
		}
		else if (this.LargeIcon.color.a < 1f)
		{
			this.LargeIcon.color = new Color(this.LargeIcon.color.r, this.LargeIcon.color.g, this.LargeIcon.color.b, this.LargeIcon.color.a + Time.deltaTime * 10f);
			if (this.LargeIcon.color.a > 1f)
			{
				this.LargeIcon.color = new Color(this.LargeIcon.color.r, this.LargeIcon.color.g, this.LargeIcon.color.b, 1f);
			}
		}
		this.Shadow.color = new Color(this.Shadow.color.r, this.Shadow.color.g, this.Shadow.color.b, this.LargeIcon.color.a * 0.75f);
		if (!this.Switch && Input.GetButtonDown("A") && this.List == 1)
		{
			this.Viewing = true;
		}
		if (Input.GetButtonDown("B"))
		{
			if (this.Viewing)
			{
				this.Viewing = false;
			}
			else
			{
				this.Switch = true;
			}
		}
		if (this.Switch)
		{
			if (this.Phase == 1)
			{
				this.ChallengePanel.alpha -= Time.deltaTime;
				if (this.ChallengePanel.alpha <= 0f)
				{
					this.Phase++;
					return;
				}
			}
			else
			{
				this.CalendarPanel.alpha += Time.deltaTime;
				if (this.CalendarPanel.alpha >= 1f)
				{
					this.Calendar.enabled = true;
					base.enabled = false;
					this.Switch = false;
					this.Phase = 1;
				}
			}
		}
	}

	// Token: 0x04000B7A RID: 2938
	public InputManagerScript InputManager;

	// Token: 0x04000B7B RID: 2939
	public CalendarScript Calendar;

	// Token: 0x04000B7C RID: 2940
	public GameObject ViewButton;

	// Token: 0x04000B7D RID: 2941
	public Transform Arrows;

	// Token: 0x04000B7E RID: 2942
	public Transform[] ChallengeList;

	// Token: 0x04000B7F RID: 2943
	public int[] Challenges;

	// Token: 0x04000B80 RID: 2944
	public UIPanel[] Panels;

	// Token: 0x04000B81 RID: 2945
	public UIPanel ChallengePanel;

	// Token: 0x04000B82 RID: 2946
	public UIPanel CalendarPanel;

	// Token: 0x04000B83 RID: 2947
	public UITexture LargeIcon;

	// Token: 0x04000B84 RID: 2948
	public UISprite Shadow;

	// Token: 0x04000B85 RID: 2949
	public bool Viewing;

	// Token: 0x04000B86 RID: 2950
	public bool Switch;

	// Token: 0x04000B87 RID: 2951
	public int Phase = 1;

	// Token: 0x04000B88 RID: 2952
	public int List;
}
