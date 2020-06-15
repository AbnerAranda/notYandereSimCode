using System;
using UnityEngine;

// Token: 0x02000465 RID: 1125
public class WeaponManagerScript : MonoBehaviour
{
	// Token: 0x06001D24 RID: 7460 RVA: 0x0015B7E0 File Offset: 0x001599E0
	public void Start()
	{
		for (int i = 0; i < this.Weapons.Length; i++)
		{
			this.Weapons[i].GlobalID = i;
			if (WeaponGlobals.GetWeaponStatus(i) == 1)
			{
				this.Weapons[i].gameObject.SetActive(false);
			}
		}
		this.ChangeBloodTexture();
	}

	// Token: 0x06001D25 RID: 7461 RVA: 0x0015B830 File Offset: 0x00159A30
	public void UpdateLabels()
	{
		WeaponScript[] weapons = this.Weapons;
		for (int i = 0; i < weapons.Length; i++)
		{
			weapons[i].UpdateLabel();
		}
	}

	// Token: 0x06001D26 RID: 7462 RVA: 0x0015B85C File Offset: 0x00159A5C
	public void CheckWeapons()
	{
		this.MurderWeapons = 0;
		this.Fingerprints = 0;
		for (int i = 0; i < this.Victims.Length; i++)
		{
			this.Victims[i] = 0;
		}
		foreach (WeaponScript weaponScript in this.Weapons)
		{
			if (weaponScript != null && weaponScript.Blood.enabled && !weaponScript.AlreadyExamined)
			{
				this.MurderWeapons++;
				if (weaponScript.FingerprintID > 0)
				{
					this.Fingerprints++;
					for (int k = 0; k < weaponScript.Victims.Length; k++)
					{
						if (weaponScript.Victims[k])
						{
							this.Victims[k] = weaponScript.FingerprintID;
						}
					}
				}
			}
		}
	}

	// Token: 0x06001D27 RID: 7463 RVA: 0x0015B924 File Offset: 0x00159B24
	public void CleanWeapons()
	{
		foreach (WeaponScript weaponScript in this.Weapons)
		{
			if (weaponScript != null)
			{
				weaponScript.Blood.enabled = false;
				weaponScript.FingerprintID = 0;
			}
		}
	}

	// Token: 0x06001D28 RID: 7464 RVA: 0x0015B968 File Offset: 0x00159B68
	public void ChangeBloodTexture()
	{
		foreach (WeaponScript weaponScript in this.Weapons)
		{
			if (weaponScript != null)
			{
				if (!GameGlobals.CensorBlood)
				{
					weaponScript.Blood.material.mainTexture = this.Blood;
					weaponScript.Blood.material.SetColor("_TintColor", new Color(0.25f, 0.25f, 0.25f, 0.5f));
				}
				else
				{
					weaponScript.Blood.material.mainTexture = this.Flower;
					weaponScript.Blood.material.SetColor("_TintColor", new Color(0.5f, 0.5f, 0.5f, 0.5f));
				}
			}
		}
	}

	// Token: 0x06001D29 RID: 7465 RVA: 0x0015BA34 File Offset: 0x00159C34
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Z))
		{
			this.CheckWeapons();
			for (int i = 0; i < this.Victims.Length; i++)
			{
				if (this.Victims[i] != 0)
				{
					if (this.Victims[i] == 100)
					{
						Debug.Log("The student named " + this.JSON.Students[i].Name + " was killed by Yandere-chan!");
					}
					else
					{
						Debug.Log(string.Concat(new string[]
						{
							"The student named ",
							this.JSON.Students[i].Name,
							" was killed by ",
							this.JSON.Students[this.Victims[i]].Name,
							"!"
						}));
					}
				}
			}
		}
		if (this.OriginalWeapon > -1)
		{
			Debug.Log("Re-equipping original weapon.");
			this.Weapons[this.OriginalWeapon].Prompt.Circle[3].fillAmount = 0f;
			this.Weapons[this.OriginalWeapon].gameObject.SetActive(true);
			this.Weapons[this.OriginalWeapon].DoNotDisable = true;
			this.OriginalWeapon = -1;
		}
	}

	// Token: 0x06001D2A RID: 7466 RVA: 0x0015BB70 File Offset: 0x00159D70
	public void TrackDumpedWeapons()
	{
		for (int i = 0; i < this.Weapons.Length; i++)
		{
			if (this.Weapons[i] == null)
			{
				Debug.Log("Weapon #" + i + " was destroyed! Setting status to 1!");
			}
		}
	}

	// Token: 0x06001D2B RID: 7467 RVA: 0x0015BBBC File Offset: 0x00159DBC
	public void SetEquippedWeapon1(WeaponScript Weapon)
	{
		for (int i = 0; i < this.Weapons.Length; i++)
		{
			if (this.Weapons[i] == Weapon)
			{
				this.YandereWeapon1 = i;
			}
		}
	}

	// Token: 0x06001D2C RID: 7468 RVA: 0x0015BBF4 File Offset: 0x00159DF4
	public void SetEquippedWeapon2(WeaponScript Weapon)
	{
		for (int i = 0; i < this.Weapons.Length; i++)
		{
			if (this.Weapons[i] == Weapon)
			{
				this.YandereWeapon2 = i;
			}
		}
	}

	// Token: 0x06001D2D RID: 7469 RVA: 0x0015BC2C File Offset: 0x00159E2C
	public void SetEquippedWeapon3(WeaponScript Weapon)
	{
		for (int i = 0; i < this.Weapons.Length; i++)
		{
			if (this.Weapons[i] == Weapon)
			{
				this.YandereWeapon3 = i;
			}
		}
	}

	// Token: 0x06001D2E RID: 7470 RVA: 0x0015BC64 File Offset: 0x00159E64
	public void EquipWeaponsFromSave()
	{
		this.OriginalEquipped = this.Yandere.Equipped;
		if (this.Yandere.Equipped == 1)
		{
			this.OriginalWeapon = this.YandereWeapon1;
		}
		else if (this.Yandere.Equipped == 2)
		{
			this.OriginalWeapon = this.YandereWeapon2;
		}
		else if (this.Yandere.Equipped == 3)
		{
			this.OriginalWeapon = this.YandereWeapon3;
		}
		if (this.Yandere.Equipped > 0)
		{
			Debug.Log(string.Concat(new object[]
			{
				"The player had a weapon equipped in Slot #",
				this.Yandere.Equipped,
				". That weapon was #",
				this.OriginalWeapon,
				" in the list of all weapons."
			}));
		}
		if (this.YandereWeapon1 > -1)
		{
			Debug.Log("Looks like the player had a " + this.Weapons[this.YandereWeapon1].gameObject.name + " in their possession when they saved.");
			this.Weapons[this.YandereWeapon1].Prompt.Circle[3].fillAmount = 0f;
			this.Weapons[this.YandereWeapon1].gameObject.SetActive(true);
			this.Weapons[this.YandereWeapon1].UnequipImmediately = true;
		}
		if (this.YandereWeapon2 > -1)
		{
			Debug.Log("Looks like the player had a " + this.Weapons[this.YandereWeapon2].gameObject.name + " in their possession when they saved.");
			this.Weapons[this.YandereWeapon2].Prompt.Circle[3].fillAmount = 0f;
			this.Weapons[this.YandereWeapon2].gameObject.SetActive(true);
			this.Weapons[this.YandereWeapon2].UnequipImmediately = true;
		}
		if (this.YandereWeapon3 > -1)
		{
			Debug.Log("Looks like the player had a " + this.Weapons[this.YandereWeapon3].gameObject.name + " equipped when they saved.");
			this.Weapons[this.YandereWeapon3].Prompt.Circle[3].fillAmount = 0f;
			this.Weapons[this.YandereWeapon3].gameObject.SetActive(true);
			this.Weapons[this.YandereWeapon3].UnequipImmediately = true;
		}
	}

	// Token: 0x06001D2F RID: 7471 RVA: 0x0015BEB4 File Offset: 0x0015A0B4
	public void UpdateDelinquentWeapons()
	{
		for (int i = 1; i < this.DelinquentWeapons.Length; i++)
		{
			if (this.DelinquentWeapons[i].DelinquentOwned)
			{
				this.DelinquentWeapons[i].transform.localEulerAngles = new Vector3(0f, 0f, 0f);
				this.DelinquentWeapons[i].transform.localPosition = new Vector3(0f, 0f, 0f);
			}
			else
			{
				this.DelinquentWeapons[i].transform.parent = null;
			}
		}
	}

	// Token: 0x06001D30 RID: 7472 RVA: 0x0015BF44 File Offset: 0x0015A144
	public void RestoreWeaponToStudent()
	{
		if (this.ReturnWeaponID > -1)
		{
			this.Yandere.StudentManager.Students[this.ReturnStudentID].BloodPool = this.Weapons[this.ReturnWeaponID].transform;
			this.Yandere.StudentManager.Students[this.ReturnStudentID].BloodPool = this.Weapons[this.ReturnWeaponID].transform;
			this.Yandere.StudentManager.Students[this.ReturnStudentID].BloodPool = this.Weapons[this.ReturnWeaponID].transform;
			this.Yandere.StudentManager.Students[this.ReturnStudentID].CurrentDestination = this.Weapons[this.ReturnWeaponID].Origin;
			this.Yandere.StudentManager.Students[this.ReturnStudentID].Pathfinding.target = this.Weapons[this.ReturnWeaponID].Origin;
			this.Weapons[this.ReturnWeaponID].Prompt.Hide();
			this.Weapons[this.ReturnWeaponID].Prompt.enabled = false;
			this.Weapons[this.ReturnWeaponID].enabled = false;
			this.Weapons[this.ReturnWeaponID].Returner = this.Yandere.StudentManager.Students[this.ReturnStudentID];
			this.Weapons[this.ReturnWeaponID].transform.parent = this.Yandere.StudentManager.Students[this.ReturnStudentID].RightHand;
			this.Weapons[this.ReturnWeaponID].transform.localPosition = new Vector3(0f, 0f, 0f);
			this.Weapons[this.ReturnWeaponID].transform.localEulerAngles = new Vector3(0f, 0f, 0f);
			this.Yandere.StudentManager.Students[this.ReturnStudentID].CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
		}
	}

	// Token: 0x040036D9 RID: 14041
	public WeaponScript[] DelinquentWeapons;

	// Token: 0x040036DA RID: 14042
	public WeaponScript[] Weapons;

	// Token: 0x040036DB RID: 14043
	public YandereScript Yandere;

	// Token: 0x040036DC RID: 14044
	public JsonScript JSON;

	// Token: 0x040036DD RID: 14045
	public int[] Victims;

	// Token: 0x040036DE RID: 14046
	public int MisplacedWeapons;

	// Token: 0x040036DF RID: 14047
	public int MurderWeapons;

	// Token: 0x040036E0 RID: 14048
	public int Fingerprints;

	// Token: 0x040036E1 RID: 14049
	public int YandereWeapon1 = -1;

	// Token: 0x040036E2 RID: 14050
	public int YandereWeapon2 = -1;

	// Token: 0x040036E3 RID: 14051
	public int YandereWeapon3 = -1;

	// Token: 0x040036E4 RID: 14052
	public int ReturnWeaponID = -1;

	// Token: 0x040036E5 RID: 14053
	public int ReturnStudentID = -1;

	// Token: 0x040036E6 RID: 14054
	public int OriginalEquipped = -1;

	// Token: 0x040036E7 RID: 14055
	public int OriginalWeapon = -1;

	// Token: 0x040036E8 RID: 14056
	public int Frame;

	// Token: 0x040036E9 RID: 14057
	public Texture Flower;

	// Token: 0x040036EA RID: 14058
	public Texture Blood;

	// Token: 0x040036EB RID: 14059
	public bool YandereGuilty;
}
