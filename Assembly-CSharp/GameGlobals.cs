using System;
using UnityEngine;

// Token: 0x020002C5 RID: 709
public static class GameGlobals
{
	// Token: 0x1700039F RID: 927
	// (get) Token: 0x0600150C RID: 5388 RVA: 0x000B7E5D File Offset: 0x000B605D
	// (set) Token: 0x0600150D RID: 5389 RVA: 0x000B7E69 File Offset: 0x000B6069
	public static int Profile
	{
		get
		{
			return PlayerPrefs.GetInt("Profile");
		}
		set
		{
			PlayerPrefs.SetInt("Profile", value);
		}
	}

	// Token: 0x170003A0 RID: 928
	// (get) Token: 0x0600150E RID: 5390 RVA: 0x000B7E76 File Offset: 0x000B6076
	// (set) Token: 0x0600150F RID: 5391 RVA: 0x000B7E82 File Offset: 0x000B6082
	public static int MostRecentSlot
	{
		get
		{
			return PlayerPrefs.GetInt("MostRecentSlot");
		}
		set
		{
			PlayerPrefs.SetInt("MostRecentSlot", value);
		}
	}

	// Token: 0x170003A1 RID: 929
	// (get) Token: 0x06001510 RID: 5392 RVA: 0x000B7E8F File Offset: 0x000B608F
	// (set) Token: 0x06001511 RID: 5393 RVA: 0x000B7EAF File Offset: 0x000B60AF
	public static bool LoveSick
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_LoveSick");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_LoveSick", value);
		}
	}

	// Token: 0x170003A2 RID: 930
	// (get) Token: 0x06001512 RID: 5394 RVA: 0x000B7ED0 File Offset: 0x000B60D0
	// (set) Token: 0x06001513 RID: 5395 RVA: 0x000B7EF0 File Offset: 0x000B60F0
	public static bool MasksBanned
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_MasksBanned");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_MasksBanned", value);
		}
	}

	// Token: 0x170003A3 RID: 931
	// (get) Token: 0x06001514 RID: 5396 RVA: 0x000B7F11 File Offset: 0x000B6111
	// (set) Token: 0x06001515 RID: 5397 RVA: 0x000B7F31 File Offset: 0x000B6131
	public static bool Paranormal
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_Paranormal");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_Paranormal", value);
		}
	}

	// Token: 0x170003A4 RID: 932
	// (get) Token: 0x06001516 RID: 5398 RVA: 0x000B7F52 File Offset: 0x000B6152
	// (set) Token: 0x06001517 RID: 5399 RVA: 0x000B7F72 File Offset: 0x000B6172
	public static bool EasyMode
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_EasyMode");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_EasyMode", value);
		}
	}

	// Token: 0x170003A5 RID: 933
	// (get) Token: 0x06001518 RID: 5400 RVA: 0x000B7F93 File Offset: 0x000B6193
	// (set) Token: 0x06001519 RID: 5401 RVA: 0x000B7FB3 File Offset: 0x000B61B3
	public static bool HardMode
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_HardMode");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_HardMode", value);
		}
	}

	// Token: 0x170003A6 RID: 934
	// (get) Token: 0x0600151A RID: 5402 RVA: 0x000B7FD4 File Offset: 0x000B61D4
	// (set) Token: 0x0600151B RID: 5403 RVA: 0x000B7FF4 File Offset: 0x000B61F4
	public static bool EmptyDemon
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_EmptyDemon");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_EmptyDemon", value);
		}
	}

	// Token: 0x170003A7 RID: 935
	// (get) Token: 0x0600151C RID: 5404 RVA: 0x000B8015 File Offset: 0x000B6215
	// (set) Token: 0x0600151D RID: 5405 RVA: 0x000B8035 File Offset: 0x000B6235
	public static bool CensorBlood
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_CensorBlood");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_CensorBlood", value);
		}
	}

	// Token: 0x170003A8 RID: 936
	// (get) Token: 0x0600151E RID: 5406 RVA: 0x000B8056 File Offset: 0x000B6256
	// (set) Token: 0x0600151F RID: 5407 RVA: 0x000B8076 File Offset: 0x000B6276
	public static bool SpareUniform
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_SpareUniform");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_SpareUniform", value);
		}
	}

	// Token: 0x170003A9 RID: 937
	// (get) Token: 0x06001520 RID: 5408 RVA: 0x000B8097 File Offset: 0x000B6297
	// (set) Token: 0x06001521 RID: 5409 RVA: 0x000B80B7 File Offset: 0x000B62B7
	public static bool BlondeHair
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_BlondeHair");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_BlondeHair", value);
		}
	}

	// Token: 0x170003AA RID: 938
	// (get) Token: 0x06001522 RID: 5410 RVA: 0x000B80D8 File Offset: 0x000B62D8
	// (set) Token: 0x06001523 RID: 5411 RVA: 0x000B80F8 File Offset: 0x000B62F8
	public static bool SenpaiMourning
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_SenpaiMourning");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_SenpaiMourning", value);
		}
	}

	// Token: 0x170003AB RID: 939
	// (get) Token: 0x06001524 RID: 5412 RVA: 0x000B8119 File Offset: 0x000B6319
	// (set) Token: 0x06001525 RID: 5413 RVA: 0x000B8125 File Offset: 0x000B6325
	public static int RivalEliminationID
	{
		get
		{
			return PlayerPrefs.GetInt("RivalEliminationID");
		}
		set
		{
			PlayerPrefs.SetInt("RivalEliminationID", value);
		}
	}

	// Token: 0x170003AC RID: 940
	// (get) Token: 0x06001526 RID: 5414 RVA: 0x000B8132 File Offset: 0x000B6332
	// (set) Token: 0x06001527 RID: 5415 RVA: 0x000B813E File Offset: 0x000B633E
	public static bool NonlethalElimination
	{
		get
		{
			return GlobalsHelper.GetBool("NonlethalElimination");
		}
		set
		{
			GlobalsHelper.SetBool("NonlethalElimination", value);
		}
	}

	// Token: 0x170003AD RID: 941
	// (get) Token: 0x06001528 RID: 5416 RVA: 0x000B814B File Offset: 0x000B634B
	// (set) Token: 0x06001529 RID: 5417 RVA: 0x000B816B File Offset: 0x000B636B
	public static bool ReputationsInitialized
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_ReputationsInitialized");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_ReputationsInitialized", value);
		}
	}

	// Token: 0x170003AE RID: 942
	// (get) Token: 0x0600152A RID: 5418 RVA: 0x000B818C File Offset: 0x000B638C
	// (set) Token: 0x0600152B RID: 5419 RVA: 0x000B81AC File Offset: 0x000B63AC
	public static bool AnswerSheetUnavailable
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_AnswerSheetUnavailable");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_AnswerSheetUnavailable", value);
		}
	}

	// Token: 0x170003AF RID: 943
	// (get) Token: 0x0600152C RID: 5420 RVA: 0x000B81CD File Offset: 0x000B63CD
	// (set) Token: 0x0600152D RID: 5421 RVA: 0x000B81ED File Offset: 0x000B63ED
	public static bool AlphabetMode
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_AlphabetMode");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_AlphabetMode", value);
		}
	}

	// Token: 0x170003B0 RID: 944
	// (get) Token: 0x0600152E RID: 5422 RVA: 0x000B820E File Offset: 0x000B640E
	// (set) Token: 0x0600152F RID: 5423 RVA: 0x000B822E File Offset: 0x000B642E
	public static bool PoliceYesterday
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_PoliceYesterday");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_PoliceYesterday", value);
		}
	}

	// Token: 0x170003B1 RID: 945
	// (get) Token: 0x06001530 RID: 5424 RVA: 0x000B824F File Offset: 0x000B644F
	// (set) Token: 0x06001531 RID: 5425 RVA: 0x000B826F File Offset: 0x000B646F
	public static bool DarkEnding
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_DarkEnding");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_DarkEnding", value);
		}
	}

	// Token: 0x06001532 RID: 5426 RVA: 0x000B8290 File Offset: 0x000B6490
	public static void DeleteAll()
	{
		Globals.Delete("Profile_" + GameGlobals.Profile + "_LoveSick");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_MasksBanned");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_Paranormal");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_EasyMode");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_HardMode");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_EmptyDemon");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_CensorBlood");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_SpareUniform");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_BlondeHair");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_SenpaiMourning");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_RivalEliminationID");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_NonlethalElimination");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_ReputationsInitialized");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_AnswerSheetUnavailable");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_AlphabetMode");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_PoliceYesterday");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_DarkEnding");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_MostRecentSlot");
	}

	// Token: 0x04001D71 RID: 7537
	private const string Str_Profile = "Profile";

	// Token: 0x04001D72 RID: 7538
	private const string Str_MostRecentSlot = "MostRecentSlot";

	// Token: 0x04001D73 RID: 7539
	private const string Str_LoveSick = "LoveSick";

	// Token: 0x04001D74 RID: 7540
	private const string Str_MasksBanned = "MasksBanned";

	// Token: 0x04001D75 RID: 7541
	private const string Str_Paranormal = "Paranormal";

	// Token: 0x04001D76 RID: 7542
	private const string Str_EasyMode = "EasyMode";

	// Token: 0x04001D77 RID: 7543
	private const string Str_HardMode = "HardMode";

	// Token: 0x04001D78 RID: 7544
	private const string Str_EmptyDemon = "EmptyDemon";

	// Token: 0x04001D79 RID: 7545
	private const string Str_CensorBlood = "CensorBlood";

	// Token: 0x04001D7A RID: 7546
	private const string Str_SpareUniform = "SpareUniform";

	// Token: 0x04001D7B RID: 7547
	private const string Str_BlondeHair = "BlondeHair";

	// Token: 0x04001D7C RID: 7548
	private const string Str_SenpaiMourning = "SenpaiMourning";

	// Token: 0x04001D7D RID: 7549
	private const string Str_RivalEliminationID = "RivalEliminationID";

	// Token: 0x04001D7E RID: 7550
	private const string Str_NonlethalElimination = "NonlethalElimination";

	// Token: 0x04001D7F RID: 7551
	private const string Str_ReputationsInitialized = "ReputationsInitialized";

	// Token: 0x04001D80 RID: 7552
	private const string Str_AnswerSheetUnavailable = "AnswerSheetUnavailable";

	// Token: 0x04001D81 RID: 7553
	private const string Str_AlphabetMode = "AlphabetMode";

	// Token: 0x04001D82 RID: 7554
	private const string Str_PoliceYesterday = "PoliceYesterday";

	// Token: 0x04001D83 RID: 7555
	private const string Str_DarkEnding = "DarkEnding";
}
