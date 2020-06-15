using System;
using UnityEngine;

// Token: 0x020002D2 RID: 722
public static class WeaponGlobals
{
	// Token: 0x06001688 RID: 5768 RVA: 0x000BCCED File Offset: 0x000BAEED
	public static int GetWeaponStatus(int weaponID)
	{
		return PlayerPrefs.GetInt(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_WeaponStatus_",
			weaponID.ToString()
		}));
	}

	// Token: 0x06001689 RID: 5769 RVA: 0x000BCD28 File Offset: 0x000BAF28
	public static void SetWeaponStatus(int weaponID, int value)
	{
		string text = weaponID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_WeaponStatus_", text);
		PlayerPrefs.SetInt(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_WeaponStatus_",
			text
		}), value);
	}

	// Token: 0x0600168A RID: 5770 RVA: 0x000BCD8E File Offset: 0x000BAF8E
	public static int[] KeysOfWeaponStatus()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_WeaponStatus_");
	}

	// Token: 0x0600168B RID: 5771 RVA: 0x000BCDAE File Offset: 0x000BAFAE
	public static void DeleteAll()
	{
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_WeaponStatus_", WeaponGlobals.KeysOfWeaponStatus());
	}

	// Token: 0x04001E14 RID: 7700
	private const string Str_WeaponStatus = "WeaponStatus_";
}
