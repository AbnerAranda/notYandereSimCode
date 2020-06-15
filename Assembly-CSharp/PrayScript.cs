using System;
using UnityEngine;

// Token: 0x0200037E RID: 894
public class PrayScript : MonoBehaviour
{
	// Token: 0x0600195E RID: 6494 RVA: 0x000F468C File Offset: 0x000F288C
	private void Start()
	{
		if (StudentGlobals.GetStudentDead(39))
		{
			this.VictimLabel.color = new Color(this.VictimLabel.color.r, this.VictimLabel.color.g, this.VictimLabel.color.b, 0.5f);
		}
		this.PrayWindow.localScale = Vector3.zero;
		if (MissionModeGlobals.MissionMode || GameGlobals.AlphabetMode)
		{
			this.Disable();
		}
		if (GameGlobals.LoveSick || GameGlobals.AlphabetMode)
		{
			this.Disable();
		}
	}

	// Token: 0x0600195F RID: 6495 RVA: 0x000F471F File Offset: 0x000F291F
	private void Disable()
	{
		this.GenderPrompt.gameObject.SetActive(false);
		base.enabled = false;
		this.Prompt.enabled = false;
		this.Prompt.Hide();
	}

	// Token: 0x06001960 RID: 6496 RVA: 0x000F4750 File Offset: 0x000F2950
	private void Update()
	{
		if (!this.FemaleVictimChecked)
		{
			if (this.StudentManager.Students[39] != null && !this.StudentManager.Students[39].Alive)
			{
				this.FemaleVictimChecked = true;
				this.Victims++;
			}
		}
		else if (this.StudentManager.Students[39] == null)
		{
			this.FemaleVictimChecked = false;
			this.Victims--;
		}
		if (!this.MaleVictimChecked)
		{
			if (this.StudentManager.Students[37] != null && !this.StudentManager.Students[37].Alive)
			{
				this.MaleVictimChecked = true;
				this.Victims++;
			}
		}
		else if (this.StudentManager.Students[37] == null)
		{
			this.MaleVictimChecked = false;
			this.Victims--;
		}
		if (this.JustSummoned)
		{
			this.StudentManager.UpdateMe(this.StudentID);
			this.JustSummoned = false;
		}
		if (this.GenderPrompt.Circle[0].fillAmount == 0f)
		{
			this.GenderPrompt.Circle[0].fillAmount = 1f;
			if (!this.SpawnMale)
			{
				this.VictimLabel.color = new Color(this.VictimLabel.color.r, this.VictimLabel.color.g, this.VictimLabel.color.b, StudentGlobals.GetStudentDead(37) ? 0.5f : 1f);
				this.GenderPrompt.Label[0].text = "     Male Victim";
				this.SpawnMale = true;
			}
			else
			{
				this.VictimLabel.color = new Color(this.VictimLabel.color.r, this.VictimLabel.color.g, this.VictimLabel.color.b, StudentGlobals.GetStudentDead(39) ? 0.5f : 1f);
				this.GenderPrompt.Label[0].text = "     Female Victim";
				this.SpawnMale = false;
			}
		}
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Circle[0].fillAmount = 1f;
			if (!this.Yandere.Chased && this.Yandere.Chasers == 0)
			{
				this.Yandere.TargetStudent = this.Student;
				this.StudentManager.DisablePrompts();
				this.PrayWindow.gameObject.SetActive(true);
				this.Show = true;
				this.Yandere.ShoulderCamera.OverShoulder = true;
				this.Yandere.WeaponMenu.KeyboardShow = false;
				this.Yandere.Obscurance.enabled = false;
				this.Yandere.WeaponMenu.Show = false;
				this.Yandere.YandereVision = false;
				this.Yandere.CanMove = false;
				this.Yandere.Talking = true;
				this.PromptBar.ClearButtons();
				this.PromptBar.Label[0].text = "Accept";
				this.PromptBar.Label[4].text = "Choose";
				this.PromptBar.UpdateButtons();
				this.PromptBar.Show = true;
				this.StudentNumber = (this.SpawnMale ? 37 : 39);
				if (this.StudentManager.Students[this.StudentNumber] != null)
				{
					if (!this.StudentManager.Students[this.StudentNumber].gameObject.activeInHierarchy)
					{
						this.VictimLabel.color = new Color(this.VictimLabel.color.r, this.VictimLabel.color.g, this.VictimLabel.color.b, 0.5f);
					}
					else
					{
						this.VictimLabel.color = new Color(this.VictimLabel.color.r, this.VictimLabel.color.g, this.VictimLabel.color.b, 1f);
					}
				}
			}
		}
		if (!this.Show)
		{
			if (this.PrayWindow.gameObject.activeInHierarchy)
			{
				if (this.PrayWindow.localScale.x > 0.1f)
				{
					this.PrayWindow.localScale = Vector3.Lerp(this.PrayWindow.localScale, Vector3.zero, Time.deltaTime * 10f);
					return;
				}
				this.PrayWindow.localScale = Vector3.zero;
				this.PrayWindow.gameObject.SetActive(false);
				return;
			}
		}
		else
		{
			this.PrayWindow.localScale = Vector3.Lerp(this.PrayWindow.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10f);
			if (this.InputManager.TappedUp)
			{
				this.Selected--;
				if (this.Selected == 7)
				{
					this.Selected = 6;
				}
				this.UpdateHighlight();
			}
			if (this.InputManager.TappedDown)
			{
				this.Selected++;
				if (this.Selected == 7)
				{
					this.Selected = 8;
				}
				this.UpdateHighlight();
			}
			if (Input.GetButtonDown("A"))
			{
				if (this.Selected == 1)
				{
					if (!this.Yandere.SanityBased)
					{
						this.SanityLabel.text = "Disable Sanity Anims";
						this.Yandere.SanityBased = true;
					}
					else
					{
						this.SanityLabel.text = "Enable Sanity Anims";
						this.Yandere.SanityBased = false;
					}
					this.Exit();
					return;
				}
				if (this.Selected == 2)
				{
					this.Yandere.Sanity -= 50f;
					this.Exit();
					return;
				}
				if (this.Selected == 3)
				{
					if (this.VictimLabel.color.a == 1f && this.StudentManager.NPCsSpawned >= this.StudentManager.NPCsTotal)
					{
						if (this.SpawnMale)
						{
							this.MaleVictimChecked = false;
							this.StudentID = 37;
						}
						else
						{
							this.FemaleVictimChecked = false;
							this.StudentID = 39;
						}
						if (this.StudentManager.Students[this.StudentID] != null)
						{
							UnityEngine.Object.Destroy(this.StudentManager.Students[this.StudentID].gameObject);
						}
						this.StudentManager.Students[this.StudentID] = null;
						this.StudentManager.ForceSpawn = true;
						this.StudentManager.SpawnPositions[this.StudentID] = this.SummonSpot;
						this.StudentManager.SpawnID = this.StudentID;
						this.StudentManager.SpawnStudent(this.StudentManager.SpawnID);
						this.StudentManager.SpawnID = 0;
						this.Police.Corpses -= this.Victims;
						this.Victims = 0;
						this.JustSummoned = true;
						this.Exit();
						return;
					}
				}
				else
				{
					if (this.Selected == 4)
					{
						this.SpawnWeapons();
						this.Exit();
						return;
					}
					if (this.Selected == 5)
					{
						if (this.Yandere.Gloved)
						{
							this.Yandere.Gloves.Blood.enabled = false;
						}
						if (this.Yandere.CurrentUniformOrigin == 1)
						{
							this.StudentManager.OriginalUniforms++;
						}
						else
						{
							this.StudentManager.NewUniforms++;
						}
						this.Police.BloodyClothing = 0;
						this.Yandere.Bloodiness = 0f;
						this.Yandere.Sanity = 100f;
						this.Exit();
						return;
					}
					if (this.Selected == 6)
					{
						this.WeaponManager.CleanWeapons();
						this.Exit();
						return;
					}
					if (this.Selected == 8)
					{
						this.Exit();
					}
				}
			}
		}
	}

	// Token: 0x06001961 RID: 6497 RVA: 0x000F4F60 File Offset: 0x000F3160
	private void UpdateHighlight()
	{
		if (this.Selected < 1)
		{
			this.Selected = 8;
		}
		else if (this.Selected > 8)
		{
			this.Selected = 1;
		}
		this.Highlight.transform.localPosition = new Vector3(this.Highlight.transform.localPosition.x, 225f - 50f * (float)this.Selected, this.Highlight.transform.localPosition.z);
	}

	// Token: 0x06001962 RID: 6498 RVA: 0x000F4FE4 File Offset: 0x000F31E4
	private void Exit()
	{
		this.Selected = 1;
		this.UpdateHighlight();
		this.Yandere.ShoulderCamera.OverShoulder = false;
		this.StudentManager.EnablePrompts();
		this.Yandere.TargetStudent = null;
		this.PromptBar.ClearButtons();
		this.PromptBar.Show = false;
		this.Show = false;
		this.Uses++;
		if (this.Uses > 9)
		{
			this.FemaleTurtle.SetActive(true);
		}
	}

	// Token: 0x06001963 RID: 6499 RVA: 0x000F5068 File Offset: 0x000F3268
	public void SpawnWeapons()
	{
		for (int i = 1; i < 6; i++)
		{
			if (this.Weapon[i] != null)
			{
				this.Weapon[i].transform.position = this.WeaponSpot[i].position;
			}
		}
	}

	// Token: 0x040026BB RID: 9915
	public StudentManagerScript StudentManager;

	// Token: 0x040026BC RID: 9916
	public WeaponManagerScript WeaponManager;

	// Token: 0x040026BD RID: 9917
	public InputManagerScript InputManager;

	// Token: 0x040026BE RID: 9918
	public PromptBarScript PromptBar;

	// Token: 0x040026BF RID: 9919
	public StudentScript Student;

	// Token: 0x040026C0 RID: 9920
	public YandereScript Yandere;

	// Token: 0x040026C1 RID: 9921
	public PoliceScript Police;

	// Token: 0x040026C2 RID: 9922
	public UILabel SanityLabel;

	// Token: 0x040026C3 RID: 9923
	public UILabel VictimLabel;

	// Token: 0x040026C4 RID: 9924
	public PromptScript GenderPrompt;

	// Token: 0x040026C5 RID: 9925
	public PromptScript Prompt;

	// Token: 0x040026C6 RID: 9926
	public Transform PrayWindow;

	// Token: 0x040026C7 RID: 9927
	public Transform SummonSpot;

	// Token: 0x040026C8 RID: 9928
	public Transform Highlight;

	// Token: 0x040026C9 RID: 9929
	public Transform[] WeaponSpot;

	// Token: 0x040026CA RID: 9930
	public GameObject[] Weapon;

	// Token: 0x040026CB RID: 9931
	public GameObject FemaleTurtle;

	// Token: 0x040026CC RID: 9932
	public int StudentNumber;

	// Token: 0x040026CD RID: 9933
	public int StudentID;

	// Token: 0x040026CE RID: 9934
	public int Selected;

	// Token: 0x040026CF RID: 9935
	public int Victims;

	// Token: 0x040026D0 RID: 9936
	public int Uses;

	// Token: 0x040026D1 RID: 9937
	public bool FemaleVictimChecked;

	// Token: 0x040026D2 RID: 9938
	public bool MaleVictimChecked;

	// Token: 0x040026D3 RID: 9939
	public bool JustSummoned;

	// Token: 0x040026D4 RID: 9940
	public bool SpawnMale;

	// Token: 0x040026D5 RID: 9941
	public bool Show;
}
