using System;
using UnityEngine;

// Token: 0x02000240 RID: 576
public class ClubWindowScript : MonoBehaviour
{
	// Token: 0x0600126F RID: 4719 RVA: 0x000871A4 File Offset: 0x000853A4
	private void Start()
	{
		this.Window.SetActive(false);
		if (SchoolGlobals.SchoolAtmosphere <= 0.9f)
		{
			this.ActivityDescs[7] = this.LowAtmosphereDesc;
			return;
		}
		if (SchoolGlobals.SchoolAtmosphere <= 0.8f)
		{
			this.ActivityDescs[7] = this.MedAtmosphereDesc;
		}
	}

	// Token: 0x06001270 RID: 4720 RVA: 0x000871F4 File Offset: 0x000853F4
	private void Update()
	{
		if (this.Window.activeInHierarchy)
		{
			if (this.Timer > 0.5f)
			{
				if (Input.GetButtonDown("A"))
				{
					if (!this.Quitting && !this.Activity)
					{
						this.Yandere.Club = this.Club;
						this.Yandere.ClubAccessory();
						this.Yandere.TargetStudent.Interaction = StudentInteractionType.ClubJoin;
						this.ClubManager.ActivateClubBenefit();
					}
					else if (this.Quitting)
					{
						this.ClubManager.DeactivateClubBenefit();
						ClubGlobals.SetQuitClub(this.Club, true);
						this.Yandere.Club = ClubType.None;
						this.Yandere.ClubAccessory();
						this.Yandere.TargetStudent.Interaction = StudentInteractionType.ClubQuit;
						this.Quitting = false;
						this.Yandere.StudentManager.UpdateBooths();
					}
					else if (this.Activity)
					{
						this.Yandere.TargetStudent.Interaction = StudentInteractionType.ClubActivity;
					}
					this.Yandere.TargetStudent.TalkTimer = 100f;
					this.Yandere.TargetStudent.ClubPhase = 2;
					this.PromptBar.ClearButtons();
					this.PromptBar.Show = false;
					this.Window.SetActive(false);
				}
				if (Input.GetButtonDown("B"))
				{
					if (!this.Quitting && !this.Activity)
					{
						this.Yandere.TargetStudent.Interaction = StudentInteractionType.ClubJoin;
					}
					else if (this.Quitting)
					{
						this.Yandere.TargetStudent.Interaction = StudentInteractionType.ClubQuit;
						this.Quitting = false;
					}
					else if (this.Activity)
					{
						this.Yandere.TargetStudent.Interaction = StudentInteractionType.ClubActivity;
						this.Activity = false;
					}
					this.Yandere.TargetStudent.TalkTimer = 100f;
					this.Yandere.TargetStudent.ClubPhase = 3;
					this.PromptBar.ClearButtons();
					this.PromptBar.Show = false;
					this.Window.SetActive(false);
				}
				if (Input.GetButtonDown("X") && !this.Quitting && !this.Activity)
				{
					if (!this.Warning.activeInHierarchy)
					{
						this.ClubInfo.SetActive(false);
						this.Warning.SetActive(true);
					}
					else
					{
						this.ClubInfo.SetActive(true);
						this.Warning.SetActive(false);
					}
				}
			}
			this.Timer += Time.deltaTime;
		}
		if (this.PerformingActivity)
		{
			this.ActivityWindow.localScale = Vector3.Lerp(this.ActivityWindow.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10f);
			return;
		}
		if (this.ActivityWindow.localScale.x > 0.1f)
		{
			this.ActivityWindow.localScale = Vector3.Lerp(this.ActivityWindow.localScale, Vector3.zero, Time.deltaTime * 10f);
			return;
		}
		if (this.ActivityWindow.localScale.x != 0f)
		{
			this.ActivityWindow.localScale = Vector3.zero;
		}
	}

	// Token: 0x06001271 RID: 4721 RVA: 0x0008751C File Offset: 0x0008571C
	public void UpdateWindow()
	{
		this.ClubName.text = this.ClubNames[(int)this.Club];
		if (!this.Quitting && !this.Activity)
		{
			this.ClubDesc.text = this.ClubDescs[(int)this.Club];
			this.PromptBar.ClearButtons();
			this.PromptBar.Label[0].text = "Accept";
			this.PromptBar.Label[1].text = "Refuse";
			this.PromptBar.Label[2].text = "More Info";
			this.PromptBar.UpdateButtons();
			this.PromptBar.Show = true;
			this.BottomLabel.text = "Will you join the " + this.ClubNames[(int)this.Club] + "?";
		}
		else if (this.Activity)
		{
			this.ClubDesc.text = "Club activities last until 6:00 PM. If you choose to participate in club activities now, the day will end.\n\nIf you don't join by 5:30 PM, you won't be able to participate in club activities today.\n\nIf you don't participate in club activities at least once a week, you will be removed from the club.";
			this.PromptBar.ClearButtons();
			this.PromptBar.Label[0].text = "Yes";
			this.PromptBar.Label[1].text = "No";
			this.PromptBar.UpdateButtons();
			this.PromptBar.Show = true;
			this.BottomLabel.text = "Will you participate in club activities?";
		}
		else if (this.Quitting)
		{
			this.ClubDesc.text = "Are you sure you want to quit this club? If you quit, you will never be allowed to return.";
			this.PromptBar.ClearButtons();
			this.PromptBar.Label[0].text = "Confirm";
			this.PromptBar.Label[1].text = "Deny";
			this.PromptBar.UpdateButtons();
			this.PromptBar.Show = true;
			this.BottomLabel.text = "Will you quit the " + this.ClubNames[(int)this.Club] + "?";
		}
		this.ClubInfo.SetActive(true);
		this.Warning.SetActive(false);
		this.Window.SetActive(true);
		this.Timer = 0f;
	}

	// Token: 0x0400165D RID: 5725
	public ClubManagerScript ClubManager;

	// Token: 0x0400165E RID: 5726
	public PromptBarScript PromptBar;

	// Token: 0x0400165F RID: 5727
	public YandereScript Yandere;

	// Token: 0x04001660 RID: 5728
	public Transform ActivityWindow;

	// Token: 0x04001661 RID: 5729
	public GameObject ClubInfo;

	// Token: 0x04001662 RID: 5730
	public GameObject Window;

	// Token: 0x04001663 RID: 5731
	public GameObject Warning;

	// Token: 0x04001664 RID: 5732
	public string[] ActivityDescs;

	// Token: 0x04001665 RID: 5733
	public string[] ClubNames;

	// Token: 0x04001666 RID: 5734
	public string[] ClubDescs;

	// Token: 0x04001667 RID: 5735
	public string MedAtmosphereDesc;

	// Token: 0x04001668 RID: 5736
	public string LowAtmosphereDesc;

	// Token: 0x04001669 RID: 5737
	public UILabel ActivityLabel;

	// Token: 0x0400166A RID: 5738
	public UILabel BottomLabel;

	// Token: 0x0400166B RID: 5739
	public UILabel ClubName;

	// Token: 0x0400166C RID: 5740
	public UILabel ClubDesc;

	// Token: 0x0400166D RID: 5741
	public bool PerformingActivity;

	// Token: 0x0400166E RID: 5742
	public bool Activity;

	// Token: 0x0400166F RID: 5743
	public bool Quitting;

	// Token: 0x04001670 RID: 5744
	public float Timer;

	// Token: 0x04001671 RID: 5745
	public ClubType Club;
}
