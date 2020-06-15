using System;

// Token: 0x020002BF RID: 703
public static class ClubGlobals
{
	// Token: 0x1700038C RID: 908
	// (get) Token: 0x060014A6 RID: 5286 RVA: 0x000B63C3 File Offset: 0x000B45C3
	// (set) Token: 0x060014A7 RID: 5287 RVA: 0x000B63E3 File Offset: 0x000B45E3
	public static ClubType Club
	{
		get
		{
			return GlobalsHelper.GetEnum<ClubType>("Profile_" + GameGlobals.Profile + "_Club");
		}
		set
		{
			GlobalsHelper.SetEnum<ClubType>("Profile_" + GameGlobals.Profile + "_Club", value);
		}
	}

	// Token: 0x060014A8 RID: 5288 RVA: 0x000B6404 File Offset: 0x000B4604
	public static bool GetClubClosed(ClubType clubID)
	{
		object[] array = new object[4];
		array[0] = "Profile_";
		array[1] = GameGlobals.Profile;
		array[2] = "_ClubClosed_";
		int num = 3;
		int num2 = (int)clubID;
		array[num] = num2.ToString();
		return GlobalsHelper.GetBool(string.Concat(array));
	}

	// Token: 0x060014A9 RID: 5289 RVA: 0x000B644C File Offset: 0x000B464C
	public static void SetClubClosed(ClubType clubID, bool value)
	{
		int num = (int)clubID;
		string text = num.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_ClubClosed_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_ClubClosed_",
			text
		}), value);
	}

	// Token: 0x060014AA RID: 5290 RVA: 0x000B64B4 File Offset: 0x000B46B4
	public static ClubType[] KeysOfClubClosed()
	{
		return KeysHelper.GetEnumKeys<ClubType>("Profile_" + GameGlobals.Profile + "_ClubClosed_");
	}

	// Token: 0x060014AB RID: 5291 RVA: 0x000B64D4 File Offset: 0x000B46D4
	public static bool GetClubKicked(ClubType clubID)
	{
		object[] array = new object[4];
		array[0] = "Profile_";
		array[1] = GameGlobals.Profile;
		array[2] = "_ClubKicked_";
		int num = 3;
		int num2 = (int)clubID;
		array[num] = num2.ToString();
		return GlobalsHelper.GetBool(string.Concat(array));
	}

	// Token: 0x060014AC RID: 5292 RVA: 0x000B651C File Offset: 0x000B471C
	public static void SetClubKicked(ClubType clubID, bool value)
	{
		int num = (int)clubID;
		string text = num.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_ClubKicked_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_ClubKicked_",
			text
		}), value);
	}

	// Token: 0x060014AD RID: 5293 RVA: 0x000B6584 File Offset: 0x000B4784
	public static ClubType[] KeysOfClubKicked()
	{
		return KeysHelper.GetEnumKeys<ClubType>("Profile_" + GameGlobals.Profile + "_ClubKicked_");
	}

	// Token: 0x060014AE RID: 5294 RVA: 0x000B65A4 File Offset: 0x000B47A4
	public static bool GetQuitClub(ClubType clubID)
	{
		object[] array = new object[4];
		array[0] = "Profile_";
		array[1] = GameGlobals.Profile;
		array[2] = "_QuitClub_";
		int num = 3;
		int num2 = (int)clubID;
		array[num] = num2.ToString();
		return GlobalsHelper.GetBool(string.Concat(array));
	}

	// Token: 0x060014AF RID: 5295 RVA: 0x000B65EC File Offset: 0x000B47EC
	public static void SetQuitClub(ClubType clubID, bool value)
	{
		int num = (int)clubID;
		string text = num.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_QuitClub_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_QuitClub_",
			text
		}), value);
	}

	// Token: 0x060014B0 RID: 5296 RVA: 0x000B6654 File Offset: 0x000B4854
	public static ClubType[] KeysOfQuitClub()
	{
		return KeysHelper.GetEnumKeys<ClubType>("Profile_" + GameGlobals.Profile + "_QuitClub_");
	}

	// Token: 0x060014B1 RID: 5297 RVA: 0x000B6674 File Offset: 0x000B4874
	public static void DeleteAll()
	{
		Globals.Delete("Profile_" + GameGlobals.Profile + "_Club");
		foreach (ClubType clubType in ClubGlobals.KeysOfClubClosed())
		{
			object[] array2 = new object[4];
			array2[0] = "Profile_";
			array2[1] = GameGlobals.Profile;
			array2[2] = "_ClubClosed_";
			int num = 3;
			int num2 = (int)clubType;
			array2[num] = num2.ToString();
			Globals.Delete(string.Concat(array2));
		}
		foreach (ClubType clubType2 in ClubGlobals.KeysOfClubKicked())
		{
			object[] array3 = new object[4];
			array3[0] = "Profile_";
			array3[1] = GameGlobals.Profile;
			array3[2] = "_ClubKicked_";
			int num3 = 3;
			int num2 = (int)clubType2;
			array3[num3] = num2.ToString();
			Globals.Delete(string.Concat(array3));
		}
		foreach (ClubType clubType3 in ClubGlobals.KeysOfQuitClub())
		{
			object[] array4 = new object[4];
			array4[0] = "Profile_";
			array4[1] = GameGlobals.Profile;
			array4[2] = "_QuitClub_";
			int num4 = 3;
			int num2 = (int)clubType3;
			array4[num4] = num2.ToString();
			Globals.Delete(string.Concat(array4));
		}
		KeysHelper.Delete("Profile_" + GameGlobals.Profile + "_ClubClosed_");
		KeysHelper.Delete("Profile_" + GameGlobals.Profile + "_ClubKicked_");
		KeysHelper.Delete("Profile_" + GameGlobals.Profile + "_QuitClub_");
	}

	// Token: 0x04001D4A RID: 7498
	private const string Str_Club = "Club";

	// Token: 0x04001D4B RID: 7499
	private const string Str_ClubClosed = "ClubClosed_";

	// Token: 0x04001D4C RID: 7500
	private const string Str_ClubKicked = "ClubKicked_";

	// Token: 0x04001D4D RID: 7501
	private const string Str_QuitClub = "QuitClub_";
}
