using System;
using UnityEngine;

// Token: 0x02000438 RID: 1080
public class TutorialWindowScript : MonoBehaviour
{
	// Token: 0x06001C97 RID: 7319 RVA: 0x00157430 File Offset: 0x00155630
	private void Start()
	{
		base.transform.localScale = new Vector3(0f, 0f, 0f);
		if (OptionGlobals.TutorialsOff)
		{
			base.enabled = false;
			return;
		}
		this.IgnoreClothing = TutorialGlobals.IgnoreClothing;
		this.IgnoreCouncil = TutorialGlobals.IgnoreCouncil;
		this.IgnoreTeacher = TutorialGlobals.IgnoreTeacher;
		this.IgnoreLocker = TutorialGlobals.IgnoreLocker;
		this.IgnorePolice = TutorialGlobals.IgnorePolice;
		this.IgnoreSanity = TutorialGlobals.IgnoreSanity;
		this.IgnoreSenpai = TutorialGlobals.IgnoreSenpai;
		this.IgnoreVision = TutorialGlobals.IgnoreVision;
		this.IgnoreWeapon = TutorialGlobals.IgnoreWeapon;
		this.IgnoreBlood = TutorialGlobals.IgnoreBlood;
		this.IgnoreClass = TutorialGlobals.IgnoreClass;
		this.IgnorePhoto = TutorialGlobals.IgnorePhoto;
		this.IgnoreClub = TutorialGlobals.IgnoreClub;
		this.IgnoreInfo = TutorialGlobals.IgnoreInfo;
		this.IgnorePool = TutorialGlobals.IgnorePool;
		this.IgnoreRep = TutorialGlobals.IgnoreRep;
	}

	// Token: 0x06001C98 RID: 7320 RVA: 0x0015751C File Offset: 0x0015571C
	private void Update()
	{
		if (this.Show)
		{
			base.transform.localScale = Vector3.Lerp(base.transform.localScale, new Vector3(1.2925f, 1.2925f, 1.2925f), Time.unscaledDeltaTime * 10f);
			if (base.transform.localScale.x > 1f)
			{
				if (Input.GetButtonDown("B"))
				{
					OptionGlobals.TutorialsOff = true;
					this.TitleLabel.text = "Tutorials Disabled";
					this.TutorialLabel.text = this.DisabledString;
					this.TutorialLabel.text = this.TutorialLabel.text.Replace('@', '\n');
					this.TutorialImage.mainTexture = this.DisabledTexture;
					this.ShadowLabel.text = this.TutorialLabel.text;
				}
				else if (Input.GetButtonDown("A"))
				{
					this.Yandere.RPGCamera.enabled = true;
					this.Yandere.Blur.enabled = false;
					Time.timeScale = 1f;
					this.Show = false;
					this.Hide = true;
				}
			}
		}
		else if (this.Hide)
		{
			base.transform.localScale = Vector3.Lerp(base.transform.localScale, new Vector3(0f, 0f, 0f), Time.unscaledDeltaTime * 10f);
			if (base.transform.localScale.x < 0.1f)
			{
				base.transform.localScale = new Vector3(0f, 0f, 0f);
				this.Hide = false;
				if (OptionGlobals.TutorialsOff)
				{
					base.enabled = false;
				}
			}
		}
		if (this.Yandere.CanMove && !this.Yandere.Egg && !this.Yandere.Aiming && !this.Yandere.PauseScreen.Show && !this.Yandere.CinematicCamera.activeInHierarchy)
		{
			this.Timer += Time.deltaTime;
			if (this.Timer > 5f)
			{
				if (!this.IgnoreClothing && this.ShowClothingMessage && !this.Show)
				{
					TutorialGlobals.IgnoreClothing = true;
					this.IgnoreClothing = true;
					this.TitleLabel.text = "No Spare Clothing";
					this.TutorialLabel.text = this.ClothingString;
					this.TutorialLabel.text = this.TutorialLabel.text.Replace('@', '\n');
					this.TutorialImage.mainTexture = this.ClothingTexture;
					this.SummonWindow();
				}
				if (!this.IgnoreCouncil && this.ShowCouncilMessage && !this.Show)
				{
					TutorialGlobals.IgnoreCouncil = true;
					this.IgnoreCouncil = true;
					this.TitleLabel.text = "Student Council";
					this.TutorialLabel.text = this.CouncilString;
					this.TutorialLabel.text = this.TutorialLabel.text.Replace('@', '\n');
					this.TutorialImage.mainTexture = this.CouncilTexture;
					this.SummonWindow();
				}
				if (!this.IgnoreTeacher && this.ShowTeacherMessage && !this.Show)
				{
					TutorialGlobals.IgnoreTeacher = true;
					this.IgnoreTeacher = true;
					this.TitleLabel.text = "Teachers";
					this.TutorialLabel.text = this.TeacherString;
					this.TutorialLabel.text = this.TutorialLabel.text.Replace('@', '\n');
					this.TutorialImage.mainTexture = this.TeacherTexture;
					this.SummonWindow();
				}
				if (!this.IgnoreLocker && this.ShowLockerMessage && !this.Show)
				{
					TutorialGlobals.IgnoreLocker = true;
					this.IgnoreLocker = true;
					this.TitleLabel.text = "Notes In Lockers";
					this.TutorialLabel.text = this.LockerString;
					this.TutorialLabel.text = this.TutorialLabel.text.Replace('@', '\n');
					this.TutorialImage.mainTexture = this.LockerTexture;
					this.SummonWindow();
				}
				if (!this.IgnorePolice && this.ShowPoliceMessage && !this.Show)
				{
					TutorialGlobals.IgnorePolice = true;
					this.IgnorePolice = true;
					this.TitleLabel.text = "Police";
					this.TutorialLabel.text = this.PoliceString;
					this.TutorialLabel.text = this.TutorialLabel.text.Replace('@', '\n');
					this.TutorialImage.mainTexture = this.PoliceTexture;
					this.SummonWindow();
				}
				if (!this.IgnoreSanity && this.ShowSanityMessage && !this.Show)
				{
					TutorialGlobals.IgnoreSanity = true;
					this.IgnoreSanity = true;
					this.TitleLabel.text = "Restoring Sanity";
					this.TutorialLabel.text = this.SanityString;
					this.TutorialLabel.text = this.TutorialLabel.text.Replace('@', '\n');
					this.TutorialImage.mainTexture = this.SanityTexture;
					this.SummonWindow();
				}
				if (!this.IgnoreSenpai && this.ShowSenpaiMessage && !this.Show)
				{
					TutorialGlobals.IgnoreSenpai = true;
					this.IgnoreSenpai = true;
					this.TitleLabel.text = "Your Senpai";
					this.TutorialLabel.text = this.SenpaiString;
					this.TutorialLabel.text = this.TutorialLabel.text.Replace('@', '\n');
					this.TutorialImage.mainTexture = this.SenpaiTexture;
					this.SummonWindow();
				}
				if (!this.IgnoreVision)
				{
					if (this.Yandere.StudentManager.WestBathroomArea.bounds.Contains(this.Yandere.transform.position) || this.Yandere.StudentManager.EastBathroomArea.bounds.Contains(this.Yandere.transform.position))
					{
						this.ShowVisionMessage = true;
					}
					if (this.ShowVisionMessage && !this.Show)
					{
						TutorialGlobals.IgnoreVision = true;
						this.IgnoreVision = true;
						this.TitleLabel.text = "Yandere Vision";
						this.TutorialLabel.text = this.VisionString;
						this.TutorialLabel.text = this.TutorialLabel.text.Replace('@', '\n');
						this.TutorialImage.mainTexture = this.VisionTexture;
						this.SummonWindow();
					}
				}
				if (!this.IgnoreWeapon)
				{
					if (this.Yandere.Armed)
					{
						this.ShowWeaponMessage = true;
					}
					if (this.ShowWeaponMessage && !this.Show)
					{
						TutorialGlobals.IgnoreWeapon = true;
						this.IgnoreWeapon = true;
						this.TitleLabel.text = "Weapons";
						this.TutorialLabel.text = this.WeaponString;
						this.TutorialLabel.text = this.TutorialLabel.text.Replace('@', '\n');
						this.TutorialImage.mainTexture = this.WeaponTexture;
						this.SummonWindow();
					}
				}
				if (!this.IgnoreBlood && this.ShowBloodMessage && !this.Show)
				{
					TutorialGlobals.IgnoreBlood = true;
					this.IgnoreBlood = true;
					this.TitleLabel.text = "Bloody Clothing";
					this.TutorialLabel.text = this.BloodString;
					this.TutorialLabel.text = this.TutorialLabel.text.Replace('@', '\n');
					this.TutorialImage.mainTexture = this.BloodTexture;
					this.SummonWindow();
				}
				if (!this.IgnoreClass && this.ShowClassMessage && !this.Show)
				{
					TutorialGlobals.IgnoreClass = true;
					this.IgnoreClass = true;
					this.TitleLabel.text = "Attending Class";
					this.TutorialLabel.text = this.ClassString;
					this.TutorialLabel.text = this.TutorialLabel.text.Replace('@', '\n');
					this.TutorialImage.mainTexture = this.ClassTexture;
					this.SummonWindow();
				}
				if (!this.IgnorePhoto)
				{
					if (this.Yandere.transform.position.z > -50f)
					{
						this.ShowPhotoMessage = true;
					}
					if (this.ShowPhotoMessage && !this.Show)
					{
						TutorialGlobals.IgnorePhoto = true;
						this.IgnorePhoto = true;
						this.TitleLabel.text = "Taking Photographs";
						this.TutorialLabel.text = this.PhotoString;
						this.TutorialLabel.text = this.TutorialLabel.text.Replace('@', '\n');
						this.TutorialImage.mainTexture = this.PhotoTexture;
						this.SummonWindow();
					}
				}
				if (!this.IgnoreClub && this.ShowClubMessage && !this.Show)
				{
					TutorialGlobals.IgnoreClub = true;
					this.IgnoreClub = true;
					this.TitleLabel.text = "Joining Clubs";
					this.TutorialLabel.text = this.ClubString;
					this.TutorialLabel.text = this.TutorialLabel.text.Replace('@', '\n');
					this.TutorialImage.mainTexture = this.ClubTexture;
					this.SummonWindow();
				}
				if (!this.IgnoreInfo && this.ShowInfoMessage && !this.Show)
				{
					TutorialGlobals.IgnoreInfo = true;
					this.IgnoreInfo = true;
					this.TitleLabel.text = "Info-chan's Services";
					this.TutorialLabel.text = this.InfoString;
					this.TutorialLabel.text = this.TutorialLabel.text.Replace('@', '\n');
					this.TutorialImage.mainTexture = this.InfoTexture;
					this.SummonWindow();
				}
				if (!this.IgnorePool && this.ShowPoolMessage && !this.Show)
				{
					TutorialGlobals.IgnorePool = true;
					this.IgnorePool = true;
					this.TitleLabel.text = "Cleaning Blood";
					this.TutorialLabel.text = this.PoolString;
					this.TutorialLabel.text = this.TutorialLabel.text.Replace('@', '\n');
					this.TutorialImage.mainTexture = this.PoolTexture;
					this.SummonWindow();
				}
				if (!this.IgnoreRep && this.ShowRepMessage && !this.Show)
				{
					TutorialGlobals.IgnoreRep = true;
					this.IgnoreRep = true;
					this.TitleLabel.text = "Reputation";
					this.TutorialLabel.text = this.RepString;
					this.TutorialLabel.text = this.TutorialLabel.text.Replace('@', '\n');
					this.TutorialImage.mainTexture = this.RepTexture;
					this.SummonWindow();
				}
			}
		}
	}

	// Token: 0x06001C99 RID: 7321 RVA: 0x00157FC8 File Offset: 0x001561C8
	public void SummonWindow()
	{
		Debug.Log("Summoning tutorial window.");
		this.ShadowLabel.text = this.TutorialLabel.text;
		this.Yandere.RPGCamera.enabled = false;
		this.Yandere.Blur.enabled = true;
		Time.timeScale = 0f;
		this.Show = true;
		this.Timer = 0f;
	}

	// Token: 0x040035E3 RID: 13795
	public YandereScript Yandere;

	// Token: 0x040035E4 RID: 13796
	public bool ShowClothingMessage;

	// Token: 0x040035E5 RID: 13797
	public bool ShowCouncilMessage;

	// Token: 0x040035E6 RID: 13798
	public bool ShowTeacherMessage;

	// Token: 0x040035E7 RID: 13799
	public bool ShowLockerMessage;

	// Token: 0x040035E8 RID: 13800
	public bool ShowPoliceMessage;

	// Token: 0x040035E9 RID: 13801
	public bool ShowSanityMessage;

	// Token: 0x040035EA RID: 13802
	public bool ShowSenpaiMessage;

	// Token: 0x040035EB RID: 13803
	public bool ShowVisionMessage;

	// Token: 0x040035EC RID: 13804
	public bool ShowWeaponMessage;

	// Token: 0x040035ED RID: 13805
	public bool ShowBloodMessage;

	// Token: 0x040035EE RID: 13806
	public bool ShowClassMessage;

	// Token: 0x040035EF RID: 13807
	public bool ShowPhotoMessage;

	// Token: 0x040035F0 RID: 13808
	public bool ShowClubMessage;

	// Token: 0x040035F1 RID: 13809
	public bool ShowInfoMessage;

	// Token: 0x040035F2 RID: 13810
	public bool ShowPoolMessage;

	// Token: 0x040035F3 RID: 13811
	public bool ShowRepMessage;

	// Token: 0x040035F4 RID: 13812
	public bool IgnoreClothing;

	// Token: 0x040035F5 RID: 13813
	public bool IgnoreCouncil;

	// Token: 0x040035F6 RID: 13814
	public bool IgnoreTeacher;

	// Token: 0x040035F7 RID: 13815
	public bool IgnoreLocker;

	// Token: 0x040035F8 RID: 13816
	public bool IgnorePolice;

	// Token: 0x040035F9 RID: 13817
	public bool IgnoreSanity;

	// Token: 0x040035FA RID: 13818
	public bool IgnoreSenpai;

	// Token: 0x040035FB RID: 13819
	public bool IgnoreVision;

	// Token: 0x040035FC RID: 13820
	public bool IgnoreWeapon;

	// Token: 0x040035FD RID: 13821
	public bool IgnoreBlood;

	// Token: 0x040035FE RID: 13822
	public bool IgnoreClass;

	// Token: 0x040035FF RID: 13823
	public bool IgnorePhoto;

	// Token: 0x04003600 RID: 13824
	public bool IgnoreClub;

	// Token: 0x04003601 RID: 13825
	public bool IgnoreInfo;

	// Token: 0x04003602 RID: 13826
	public bool IgnorePool;

	// Token: 0x04003603 RID: 13827
	public bool IgnoreRep;

	// Token: 0x04003604 RID: 13828
	public bool Hide;

	// Token: 0x04003605 RID: 13829
	public bool Show;

	// Token: 0x04003606 RID: 13830
	public UILabel TutorialLabel;

	// Token: 0x04003607 RID: 13831
	public UILabel ShadowLabel;

	// Token: 0x04003608 RID: 13832
	public UILabel TitleLabel;

	// Token: 0x04003609 RID: 13833
	public UITexture TutorialImage;

	// Token: 0x0400360A RID: 13834
	public string DisabledString;

	// Token: 0x0400360B RID: 13835
	public Texture DisabledTexture;

	// Token: 0x0400360C RID: 13836
	public string ClothingString;

	// Token: 0x0400360D RID: 13837
	public Texture ClothingTexture;

	// Token: 0x0400360E RID: 13838
	public string CouncilString;

	// Token: 0x0400360F RID: 13839
	public Texture CouncilTexture;

	// Token: 0x04003610 RID: 13840
	public string TeacherString;

	// Token: 0x04003611 RID: 13841
	public Texture TeacherTexture;

	// Token: 0x04003612 RID: 13842
	public string LockerString;

	// Token: 0x04003613 RID: 13843
	public Texture LockerTexture;

	// Token: 0x04003614 RID: 13844
	public string PoliceString;

	// Token: 0x04003615 RID: 13845
	public Texture PoliceTexture;

	// Token: 0x04003616 RID: 13846
	public string SanityString;

	// Token: 0x04003617 RID: 13847
	public Texture SanityTexture;

	// Token: 0x04003618 RID: 13848
	public string SenpaiString;

	// Token: 0x04003619 RID: 13849
	public Texture SenpaiTexture;

	// Token: 0x0400361A RID: 13850
	public string VisionString;

	// Token: 0x0400361B RID: 13851
	public Texture VisionTexture;

	// Token: 0x0400361C RID: 13852
	public string WeaponString;

	// Token: 0x0400361D RID: 13853
	public Texture WeaponTexture;

	// Token: 0x0400361E RID: 13854
	public string BloodString;

	// Token: 0x0400361F RID: 13855
	public Texture BloodTexture;

	// Token: 0x04003620 RID: 13856
	public string ClassString;

	// Token: 0x04003621 RID: 13857
	public Texture ClassTexture;

	// Token: 0x04003622 RID: 13858
	public string PhotoString;

	// Token: 0x04003623 RID: 13859
	public Texture PhotoTexture;

	// Token: 0x04003624 RID: 13860
	public string ClubString;

	// Token: 0x04003625 RID: 13861
	public Texture ClubTexture;

	// Token: 0x04003626 RID: 13862
	public string InfoString;

	// Token: 0x04003627 RID: 13863
	public Texture InfoTexture;

	// Token: 0x04003628 RID: 13864
	public string PoolString;

	// Token: 0x04003629 RID: 13865
	public Texture PoolTexture;

	// Token: 0x0400362A RID: 13866
	public string RepString;

	// Token: 0x0400362B RID: 13867
	public Texture RepTexture;

	// Token: 0x0400362C RID: 13868
	public string PointsString;

	// Token: 0x0400362D RID: 13869
	public float Timer;
}
