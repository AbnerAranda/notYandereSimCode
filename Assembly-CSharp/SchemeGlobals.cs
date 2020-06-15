using System;
using UnityEngine;

// Token: 0x020002CC RID: 716
public static class SchemeGlobals
{
	// Token: 0x170003EC RID: 1004
	// (get) Token: 0x060015CA RID: 5578 RVA: 0x000B9FFC File Offset: 0x000B81FC
	// (set) Token: 0x060015CB RID: 5579 RVA: 0x000BA01C File Offset: 0x000B821C
	public static int CurrentScheme
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_CurrentScheme");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_CurrentScheme", value);
		}
	}

	// Token: 0x170003ED RID: 1005
	// (get) Token: 0x060015CC RID: 5580 RVA: 0x000BA03D File Offset: 0x000B823D
	// (set) Token: 0x060015CD RID: 5581 RVA: 0x000BA05D File Offset: 0x000B825D
	public static bool DarkSecret
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_DarkSecret");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_DarkSecret", value);
		}
	}

	// Token: 0x170003EE RID: 1006
	// (get) Token: 0x060015CE RID: 5582 RVA: 0x000BA07E File Offset: 0x000B827E
	// (set) Token: 0x060015CF RID: 5583 RVA: 0x000BA09E File Offset: 0x000B829E
	public static bool HelpingKokona
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_HelpingKokona");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_HelpingKokona", value);
		}
	}

	// Token: 0x060015D0 RID: 5584 RVA: 0x000BA0BF File Offset: 0x000B82BF
	public static int GetSchemePreviousStage(int schemeID)
	{
		return PlayerPrefs.GetInt(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_SchemePreviousStage_",
			schemeID.ToString()
		}));
	}

	// Token: 0x060015D1 RID: 5585 RVA: 0x000BA0F8 File Offset: 0x000B82F8
	public static void SetSchemePreviousStage(int schemeID, int value)
	{
		string text = schemeID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_SchemePreviousStage_", text);
		PlayerPrefs.SetInt(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_SchemePreviousStage_",
			text
		}), value);
	}

	// Token: 0x060015D2 RID: 5586 RVA: 0x000BA15E File Offset: 0x000B835E
	public static int[] KeysOfSchemePreviousStage()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_SchemePreviousStage_");
	}

	// Token: 0x060015D3 RID: 5587 RVA: 0x000BA17E File Offset: 0x000B837E
	public static int GetSchemeStage(int schemeID)
	{
		return PlayerPrefs.GetInt(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_SchemeStage_",
			schemeID.ToString()
		}));
	}

	// Token: 0x060015D4 RID: 5588 RVA: 0x000BA1B8 File Offset: 0x000B83B8
	public static void SetSchemeStage(int schemeID, int value)
	{
		string text = schemeID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_SchemeStage_", text);
		PlayerPrefs.SetInt(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_SchemeStage_",
			text
		}), value);
	}

	// Token: 0x060015D5 RID: 5589 RVA: 0x000BA21E File Offset: 0x000B841E
	public static int[] KeysOfSchemeStage()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_SchemeStage_");
	}

	// Token: 0x060015D6 RID: 5590 RVA: 0x000BA23E File Offset: 0x000B843E
	public static bool GetSchemeStatus(int schemeID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_SchemeStatus_",
			schemeID.ToString()
		}));
	}

	// Token: 0x060015D7 RID: 5591 RVA: 0x000BA278 File Offset: 0x000B8478
	public static void SetSchemeStatus(int schemeID, bool value)
	{
		string text = schemeID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_SchemeStatus_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_SchemeStatus_",
			text
		}), value);
	}

	// Token: 0x060015D8 RID: 5592 RVA: 0x000BA2DE File Offset: 0x000B84DE
	public static int[] KeysOfSchemeStatus()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_SchemeStatus_");
	}

	// Token: 0x060015D9 RID: 5593 RVA: 0x000BA2FE File Offset: 0x000B84FE
	public static bool GetSchemeUnlocked(int schemeID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_SchemeUnlocked_",
			schemeID.ToString()
		}));
	}

	// Token: 0x060015DA RID: 5594 RVA: 0x000BA338 File Offset: 0x000B8538
	public static void SetSchemeUnlocked(int schemeID, bool value)
	{
		string text = schemeID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_SchemeUnlocked_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_SchemeUnlocked_",
			text
		}), value);
	}

	// Token: 0x060015DB RID: 5595 RVA: 0x000BA39E File Offset: 0x000B859E
	public static int[] KeysOfSchemeUnlocked()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_SchemeUnlocked_");
	}

	// Token: 0x060015DC RID: 5596 RVA: 0x000BA3BE File Offset: 0x000B85BE
	public static bool GetServicePurchased(int serviceID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_ServicePurchased_",
			serviceID.ToString()
		}));
	}

	// Token: 0x060015DD RID: 5597 RVA: 0x000BA3F8 File Offset: 0x000B85F8
	public static void SetServicePurchased(int serviceID, bool value)
	{
		string text = serviceID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_ServicePurchased_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_ServicePurchased_",
			text
		}), value);
	}

	// Token: 0x060015DE RID: 5598 RVA: 0x000BA45E File Offset: 0x000B865E
	public static int[] KeysOfServicePurchased()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_ServicePurchased_");
	}

	// Token: 0x060015DF RID: 5599 RVA: 0x000BA480 File Offset: 0x000B8680
	public static void DeleteAll()
	{
		Globals.Delete("Profile_" + GameGlobals.Profile + "_CurrentScheme");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_DarkSecret");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_HelpingKokona");
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_SchemePreviousStage_", SchemeGlobals.KeysOfSchemePreviousStage());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_SchemeStage_", SchemeGlobals.KeysOfSchemeStage());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_SchemeStatus_", SchemeGlobals.KeysOfSchemeStatus());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_SchemeUnlocked_", SchemeGlobals.KeysOfSchemeUnlocked());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_ServicePurchased_", SchemeGlobals.KeysOfServicePurchased());
	}

	// Token: 0x04001DC8 RID: 7624
	private const string Str_CurrentScheme = "CurrentScheme";

	// Token: 0x04001DC9 RID: 7625
	private const string Str_DarkSecret = "DarkSecret";

	// Token: 0x04001DCA RID: 7626
	private const string Str_HelpingKokona = "HelpingKokona";

	// Token: 0x04001DCB RID: 7627
	private const string Str_SchemePreviousStage = "SchemePreviousStage_";

	// Token: 0x04001DCC RID: 7628
	private const string Str_SchemeStage = "SchemeStage_";

	// Token: 0x04001DCD RID: 7629
	private const string Str_SchemeStatus = "SchemeStatus_";

	// Token: 0x04001DCE RID: 7630
	private const string Str_SchemeUnlocked = "SchemeUnlocked_";

	// Token: 0x04001DCF RID: 7631
	private const string Str_ServicePurchased = "ServicePurchased_";
}
