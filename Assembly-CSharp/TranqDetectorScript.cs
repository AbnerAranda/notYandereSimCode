using System;
using UnityEngine;

// Token: 0x02000432 RID: 1074
public class TranqDetectorScript : MonoBehaviour
{
	// Token: 0x06001C83 RID: 7299 RVA: 0x001565CE File Offset: 0x001547CE
	private void Start()
	{
		this.Checklist.alpha = 0f;
	}

	// Token: 0x06001C84 RID: 7300 RVA: 0x001565E0 File Offset: 0x001547E0
	private void Update()
	{
		if (this.StopChecking)
		{
			this.Checklist.alpha = Mathf.MoveTowards(this.Checklist.alpha, 0f, Time.deltaTime);
			if (this.Checklist.alpha == 0f)
			{
				base.enabled = false;
			}
			return;
		}
		if (this.MyCollider.bounds.Contains(this.Yandere.transform.position))
		{
			if (SchoolGlobals.KidnapVictim > 0)
			{
				this.KidnappingLabel.text = "There is no room for another prisoner in your basement.";
			}
			else
			{
				if (this.Yandere.Inventory.Tranquilizer || this.Yandere.Inventory.Sedative)
				{
					this.TranquilizerIcon.spriteName = "Yes";
				}
				else
				{
					this.TranquilizerIcon.spriteName = "No";
				}
				if (this.Yandere.Followers != 1)
				{
					this.FollowerIcon.spriteName = "No";
				}
				else if (this.Yandere.Follower.Male)
				{
					this.KidnappingLabel.text = "You cannot kidnap male students at this point in time.";
					this.FollowerIcon.spriteName = "No";
				}
				else
				{
					this.KidnappingLabel.text = "Kidnapping Checklist";
					this.FollowerIcon.spriteName = "Yes";
				}
				this.BiologyIcon.spriteName = ((ClassGlobals.BiologyGrade + ClassGlobals.BiologyBonus != 0) ? "Yes" : "No");
				if (!this.Yandere.Armed)
				{
					this.SyringeIcon.spriteName = "No";
				}
				else if (this.Yandere.EquippedWeapon.WeaponID != 3)
				{
					this.SyringeIcon.spriteName = "No";
				}
				else
				{
					this.SyringeIcon.spriteName = "Yes";
				}
				if (this.Door.Open || this.Door.Timer < 1f)
				{
					this.DoorIcon.spriteName = "No";
				}
				else
				{
					this.DoorIcon.spriteName = "Yes";
				}
			}
			this.Checklist.alpha = Mathf.MoveTowards(this.Checklist.alpha, 1f, Time.deltaTime);
			return;
		}
		this.Checklist.alpha = Mathf.MoveTowards(this.Checklist.alpha, 0f, Time.deltaTime);
	}

	// Token: 0x06001C85 RID: 7301 RVA: 0x0015683C File Offset: 0x00154A3C
	public void TranqCheck()
	{
		if (!this.StopChecking && this.KidnappingLabel.text == "Kidnapping Checklist" && this.TranquilizerIcon.spriteName == "Yes" && this.FollowerIcon.spriteName == "Yes" && this.BiologyIcon.spriteName == "Yes" && this.SyringeIcon.spriteName == "Yes" && this.DoorIcon.spriteName == "Yes")
		{
			AudioSource component = base.GetComponent<AudioSource>();
			component.clip = this.TranqClips[UnityEngine.Random.Range(0, this.TranqClips.Length)];
			component.Play();
			this.Door.Prompt.Hide();
			this.Door.Prompt.enabled = false;
			this.Door.enabled = false;
			this.Yandere.Inventory.Tranquilizer = false;
			if (!this.Yandere.Follower.Male)
			{
				this.Yandere.CanTranq = true;
			}
			this.Yandere.EquippedWeapon.Type = WeaponType.Syringe;
			this.Yandere.AttackManager.Stealth = true;
			this.StopChecking = true;
		}
	}

	// Token: 0x06001C86 RID: 7302 RVA: 0x0015699C File Offset: 0x00154B9C
	public void GarroteAttack()
	{
		AudioSource component = base.GetComponent<AudioSource>();
		component.clip = this.TranqClips[UnityEngine.Random.Range(0, this.TranqClips.Length)];
		component.Play();
		this.Yandere.EquippedWeapon.Type = WeaponType.Syringe;
		this.Yandere.AttackManager.Stealth = true;
		this.StopChecking = true;
	}

	// Token: 0x040035B5 RID: 13749
	public YandereScript Yandere;

	// Token: 0x040035B6 RID: 13750
	public DoorScript Door;

	// Token: 0x040035B7 RID: 13751
	public UIPanel Checklist;

	// Token: 0x040035B8 RID: 13752
	public Collider MyCollider;

	// Token: 0x040035B9 RID: 13753
	public UILabel KidnappingLabel;

	// Token: 0x040035BA RID: 13754
	public UISprite TranquilizerIcon;

	// Token: 0x040035BB RID: 13755
	public UISprite FollowerIcon;

	// Token: 0x040035BC RID: 13756
	public UISprite BiologyIcon;

	// Token: 0x040035BD RID: 13757
	public UISprite SyringeIcon;

	// Token: 0x040035BE RID: 13758
	public UISprite DoorIcon;

	// Token: 0x040035BF RID: 13759
	public bool StopChecking;

	// Token: 0x040035C0 RID: 13760
	public AudioClip[] TranqClips;
}
