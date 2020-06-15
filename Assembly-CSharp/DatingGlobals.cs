using System;
using UnityEngine;

// Token: 0x020002C3 RID: 707
public static class DatingGlobals
{
	// Token: 0x17000393 RID: 915
	// (get) Token: 0x060014E3 RID: 5347 RVA: 0x000B7561 File Offset: 0x000B5761
	// (set) Token: 0x060014E4 RID: 5348 RVA: 0x000B7581 File Offset: 0x000B5781
	public static float Affection
	{
		get
		{
			return PlayerPrefs.GetFloat("Profile_" + GameGlobals.Profile + "_Affection");
		}
		set
		{
			PlayerPrefs.SetFloat("Profile_" + GameGlobals.Profile + "_Affection", value);
		}
	}

	// Token: 0x17000394 RID: 916
	// (get) Token: 0x060014E5 RID: 5349 RVA: 0x000B75A2 File Offset: 0x000B57A2
	// (set) Token: 0x060014E6 RID: 5350 RVA: 0x000B75C2 File Offset: 0x000B57C2
	public static float AffectionLevel
	{
		get
		{
			return PlayerPrefs.GetFloat("Profile_" + GameGlobals.Profile + "_AffectionLevel");
		}
		set
		{
			PlayerPrefs.SetFloat("Profile_" + GameGlobals.Profile + "_AffectionLevel", value);
		}
	}

	// Token: 0x060014E7 RID: 5351 RVA: 0x000B75E3 File Offset: 0x000B57E3
	public static bool GetComplimentGiven(int complimentID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_ComplimentGiven_",
			complimentID.ToString()
		}));
	}

	// Token: 0x060014E8 RID: 5352 RVA: 0x000B761C File Offset: 0x000B581C
	public static void SetComplimentGiven(int complimentID, bool value)
	{
		string text = complimentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_ComplimentGiven_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_ComplimentGiven_",
			text
		}), value);
	}

	// Token: 0x060014E9 RID: 5353 RVA: 0x000B7682 File Offset: 0x000B5882
	public static int[] KeysOfComplimentGiven()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_ComplimentGiven_");
	}

	// Token: 0x060014EA RID: 5354 RVA: 0x000B76A2 File Offset: 0x000B58A2
	public static bool GetSuitorCheck(int checkID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_SuitorCheck_",
			checkID.ToString()
		}));
	}

	// Token: 0x060014EB RID: 5355 RVA: 0x000B76DC File Offset: 0x000B58DC
	public static void SetSuitorCheck(int checkID, bool value)
	{
		string text = checkID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_SuitorCheck_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_SuitorCheck_",
			text
		}), value);
	}

	// Token: 0x060014EC RID: 5356 RVA: 0x000B7742 File Offset: 0x000B5942
	public static int[] KeysOfSuitorCheck()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_SuitorCheck_");
	}

	// Token: 0x17000395 RID: 917
	// (get) Token: 0x060014ED RID: 5357 RVA: 0x000B7762 File Offset: 0x000B5962
	// (set) Token: 0x060014EE RID: 5358 RVA: 0x000B7782 File Offset: 0x000B5982
	public static int SuitorProgress
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_SuitorProgress");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_SuitorProgress", value);
		}
	}

	// Token: 0x060014EF RID: 5359 RVA: 0x000B77A3 File Offset: 0x000B59A3
	public static int GetSuitorTrait(int traitID)
	{
		return PlayerPrefs.GetInt(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_SuitorTrait_",
			traitID.ToString()
		}));
	}

	// Token: 0x060014F0 RID: 5360 RVA: 0x000B77DC File Offset: 0x000B59DC
	public static void SetSuitorTrait(int traitID, int value)
	{
		string text = traitID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_SuitorTrait_", text);
		PlayerPrefs.SetInt(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_SuitorTrait_",
			text
		}), value);
	}

	// Token: 0x060014F1 RID: 5361 RVA: 0x000B7842 File Offset: 0x000B5A42
	public static int[] KeysOfSuitorTrait()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_SuitorTrait_");
	}

	// Token: 0x060014F2 RID: 5362 RVA: 0x000B7862 File Offset: 0x000B5A62
	public static bool GetTopicDiscussed(int topicID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_TopicDiscussed_",
			topicID.ToString()
		}));
	}

	// Token: 0x060014F3 RID: 5363 RVA: 0x000B789C File Offset: 0x000B5A9C
	public static void SetTopicDiscussed(int topicID, bool value)
	{
		string text = topicID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_TopicDiscussed_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_TopicDiscussed_",
			text
		}), value);
	}

	// Token: 0x060014F4 RID: 5364 RVA: 0x000B7902 File Offset: 0x000B5B02
	public static int[] KeysOfTopicDiscussed()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_TopicDiscussed_");
	}

	// Token: 0x060014F5 RID: 5365 RVA: 0x000B7922 File Offset: 0x000B5B22
	public static int GetTraitDemonstrated(int traitID)
	{
		return PlayerPrefs.GetInt(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_TraitDemonstrated_",
			traitID.ToString()
		}));
	}

	// Token: 0x060014F6 RID: 5366 RVA: 0x000B795C File Offset: 0x000B5B5C
	public static void SetTraitDemonstrated(int traitID, int value)
	{
		string text = traitID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_TraitDemonstrated_", text);
		PlayerPrefs.SetInt(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_TraitDemonstrated_",
			text
		}), value);
	}

	// Token: 0x060014F7 RID: 5367 RVA: 0x000B79C2 File Offset: 0x000B5BC2
	public static int[] KeysOfTraitDemonstrated()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_TraitDemonstrated_");
	}

	// Token: 0x17000396 RID: 918
	// (get) Token: 0x060014F8 RID: 5368 RVA: 0x000B79E2 File Offset: 0x000B5BE2
	// (set) Token: 0x060014F9 RID: 5369 RVA: 0x000B7A02 File Offset: 0x000B5C02
	public static int RivalSabotaged
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_RivalSabotaged");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_RivalSabotaged", value);
		}
	}

	// Token: 0x060014FA RID: 5370 RVA: 0x000B7A24 File Offset: 0x000B5C24
	public static void DeleteAll()
	{
		Globals.Delete("Profile_" + GameGlobals.Profile + "_Affection");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_AffectionLevel");
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_ComplimentGiven_", DatingGlobals.KeysOfComplimentGiven());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_SuitorCheck_", DatingGlobals.KeysOfSuitorCheck());
		Globals.Delete("Profile_" + GameGlobals.Profile + "_SuitorProgress");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_RivalSabotaged");
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_SuitorTrait_", DatingGlobals.KeysOfSuitorTrait());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_TopicDiscussed_", DatingGlobals.KeysOfTopicDiscussed());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_TraitDemonstrated_", DatingGlobals.KeysOfTraitDemonstrated());
	}

	// Token: 0x04001D60 RID: 7520
	private const string Str_Affection = "Affection";

	// Token: 0x04001D61 RID: 7521
	private const string Str_AffectionLevel = "AffectionLevel";

	// Token: 0x04001D62 RID: 7522
	private const string Str_ComplimentGiven = "ComplimentGiven_";

	// Token: 0x04001D63 RID: 7523
	private const string Str_SuitorCheck = "SuitorCheck_";

	// Token: 0x04001D64 RID: 7524
	private const string Str_SuitorProgress = "SuitorProgress";

	// Token: 0x04001D65 RID: 7525
	private const string Str_SuitorTrait = "SuitorTrait_";

	// Token: 0x04001D66 RID: 7526
	private const string Str_TopicDiscussed = "TopicDiscussed_";

	// Token: 0x04001D67 RID: 7527
	private const string Str_TraitDemonstrated = "TraitDemonstrated_";

	// Token: 0x04001D68 RID: 7528
	private const string Str_RivalSabotaged = "RivalSabotaged";
}
