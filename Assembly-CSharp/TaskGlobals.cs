using System;
using UnityEngine;

// Token: 0x020002D0 RID: 720
public static class TaskGlobals
{
	// Token: 0x06001676 RID: 5750 RVA: 0x000BC890 File Offset: 0x000BAA90
	public static bool GetGuitarPhoto(int photoID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_GuitarPhoto_",
			photoID.ToString()
		}));
	}

	// Token: 0x06001677 RID: 5751 RVA: 0x000BC8CC File Offset: 0x000BAACC
	public static void SetGuitarPhoto(int photoID, bool value)
	{
		string text = photoID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_GuitarPhoto_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_GuitarPhoto_",
			text
		}), value);
	}

	// Token: 0x06001678 RID: 5752 RVA: 0x000BC932 File Offset: 0x000BAB32
	public static int[] KeysOfGuitarPhoto()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_GuitarPhoto_");
	}

	// Token: 0x06001679 RID: 5753 RVA: 0x000BC952 File Offset: 0x000BAB52
	public static bool GetKittenPhoto(int photoID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_KittenPhoto_",
			photoID.ToString()
		}));
	}

	// Token: 0x0600167A RID: 5754 RVA: 0x000BC98C File Offset: 0x000BAB8C
	public static void SetKittenPhoto(int photoID, bool value)
	{
		string text = photoID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_KittenPhoto_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_KittenPhoto_",
			text
		}), value);
	}

	// Token: 0x0600167B RID: 5755 RVA: 0x000BC9F2 File Offset: 0x000BABF2
	public static int[] KeysOfKittenPhoto()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_KittenPhoto_");
	}

	// Token: 0x0600167C RID: 5756 RVA: 0x000BCA12 File Offset: 0x000BAC12
	public static bool GetHorudaPhoto(int photoID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_HorudaPhoto_",
			photoID.ToString()
		}));
	}

	// Token: 0x0600167D RID: 5757 RVA: 0x000BCA4C File Offset: 0x000BAC4C
	public static void SetHorudaPhoto(int photoID, bool value)
	{
		string text = photoID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_HorudaPhoto_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_HorudaPhoto_",
			text
		}), value);
	}

	// Token: 0x0600167E RID: 5758 RVA: 0x000BCAB2 File Offset: 0x000BACB2
	public static int[] KeysOfHorudaPhoto()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_HorudaPhoto_");
	}

	// Token: 0x0600167F RID: 5759 RVA: 0x000BCAD2 File Offset: 0x000BACD2
	public static int GetTaskStatus(int taskID)
	{
		return PlayerPrefs.GetInt(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_TaskStatus_",
			taskID.ToString()
		}));
	}

	// Token: 0x06001680 RID: 5760 RVA: 0x000BCB0C File Offset: 0x000BAD0C
	public static void SetTaskStatus(int taskID, int value)
	{
		string text = taskID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_TaskStatus_", text);
		PlayerPrefs.SetInt(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_TaskStatus_",
			text
		}), value);
	}

	// Token: 0x06001681 RID: 5761 RVA: 0x000BCB72 File Offset: 0x000BAD72
	public static int[] KeysOfTaskStatus()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_TaskStatus_");
	}

	// Token: 0x06001682 RID: 5762 RVA: 0x000BCB94 File Offset: 0x000BAD94
	public static void DeleteAll()
	{
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_GuitarPhoto_", TaskGlobals.KeysOfGuitarPhoto());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_KittenPhoto_", TaskGlobals.KeysOfKittenPhoto());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_HorudaPhoto_", TaskGlobals.KeysOfHorudaPhoto());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_TaskStatus_", TaskGlobals.KeysOfTaskStatus());
	}

	// Token: 0x04001E0E RID: 7694
	private const string Str_GuitarPhoto = "GuitarPhoto_";

	// Token: 0x04001E0F RID: 7695
	private const string Str_KittenPhoto = "KittenPhoto_";

	// Token: 0x04001E10 RID: 7696
	private const string Str_HorudaPhoto = "HorudaPhoto_";

	// Token: 0x04001E11 RID: 7697
	private const string Str_TaskStatus = "TaskStatus_";
}
