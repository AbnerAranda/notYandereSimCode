using System;
using UnityEngine;

// Token: 0x020002C8 RID: 712
public static class OptionGlobals
{
	// Token: 0x170003C0 RID: 960
	// (get) Token: 0x06001554 RID: 5460 RVA: 0x000B8818 File Offset: 0x000B6A18
	// (set) Token: 0x06001555 RID: 5461 RVA: 0x000B8838 File Offset: 0x000B6A38
	public static bool DisableBloom
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_DisableBloom");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_DisableBloom", value);
		}
	}

	// Token: 0x170003C1 RID: 961
	// (get) Token: 0x06001556 RID: 5462 RVA: 0x000B8859 File Offset: 0x000B6A59
	// (set) Token: 0x06001557 RID: 5463 RVA: 0x000B8879 File Offset: 0x000B6A79
	public static int DisableFarAnimations
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_DisableFarAnimations");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_DisableFarAnimations", value);
		}
	}

	// Token: 0x170003C2 RID: 962
	// (get) Token: 0x06001558 RID: 5464 RVA: 0x000B889A File Offset: 0x000B6A9A
	// (set) Token: 0x06001559 RID: 5465 RVA: 0x000B88BA File Offset: 0x000B6ABA
	public static bool DisableOutlines
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_DisableOutlines");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_DisableOutlines", value);
		}
	}

	// Token: 0x170003C3 RID: 963
	// (get) Token: 0x0600155A RID: 5466 RVA: 0x000B88DB File Offset: 0x000B6ADB
	// (set) Token: 0x0600155B RID: 5467 RVA: 0x000B88FB File Offset: 0x000B6AFB
	public static bool DisablePostAliasing
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_DisablePostAliasing");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_DisablePostAliasing", value);
		}
	}

	// Token: 0x170003C4 RID: 964
	// (get) Token: 0x0600155C RID: 5468 RVA: 0x000B891C File Offset: 0x000B6B1C
	// (set) Token: 0x0600155D RID: 5469 RVA: 0x000B893C File Offset: 0x000B6B3C
	public static bool EnableShadows
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_EnableShadows");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_EnableShadows", value);
		}
	}

	// Token: 0x170003C5 RID: 965
	// (get) Token: 0x0600155E RID: 5470 RVA: 0x000B895D File Offset: 0x000B6B5D
	// (set) Token: 0x0600155F RID: 5471 RVA: 0x000B897D File Offset: 0x000B6B7D
	public static bool DisableObscurance
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_DisableObscurance");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_DisableObscurance", value);
		}
	}

	// Token: 0x170003C6 RID: 966
	// (get) Token: 0x06001560 RID: 5472 RVA: 0x000B899E File Offset: 0x000B6B9E
	// (set) Token: 0x06001561 RID: 5473 RVA: 0x000B89BE File Offset: 0x000B6BBE
	public static int DrawDistance
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_DrawDistance");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_DrawDistance", value);
		}
	}

	// Token: 0x170003C7 RID: 967
	// (get) Token: 0x06001562 RID: 5474 RVA: 0x000B89DF File Offset: 0x000B6BDF
	// (set) Token: 0x06001563 RID: 5475 RVA: 0x000B89FF File Offset: 0x000B6BFF
	public static int DrawDistanceLimit
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_DrawDistanceLimit");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_DrawDistanceLimit", value);
		}
	}

	// Token: 0x170003C8 RID: 968
	// (get) Token: 0x06001564 RID: 5476 RVA: 0x000B8A20 File Offset: 0x000B6C20
	// (set) Token: 0x06001565 RID: 5477 RVA: 0x000B8A40 File Offset: 0x000B6C40
	public static bool Fog
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_Fog");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_Fog", value);
		}
	}

	// Token: 0x170003C9 RID: 969
	// (get) Token: 0x06001566 RID: 5478 RVA: 0x000B8A61 File Offset: 0x000B6C61
	// (set) Token: 0x06001567 RID: 5479 RVA: 0x000B8A81 File Offset: 0x000B6C81
	public static int FPSIndex
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_FPSIndex");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_FPSIndex", value);
		}
	}

	// Token: 0x170003CA RID: 970
	// (get) Token: 0x06001568 RID: 5480 RVA: 0x000B8AA2 File Offset: 0x000B6CA2
	// (set) Token: 0x06001569 RID: 5481 RVA: 0x000B8AC2 File Offset: 0x000B6CC2
	public static bool HighPopulation
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_HighPopulation");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_HighPopulation", value);
		}
	}

	// Token: 0x170003CB RID: 971
	// (get) Token: 0x0600156A RID: 5482 RVA: 0x000B8AE3 File Offset: 0x000B6CE3
	// (set) Token: 0x0600156B RID: 5483 RVA: 0x000B8B03 File Offset: 0x000B6D03
	public static int LowDetailStudents
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_LowDetailStudents");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_LowDetailStudents", value);
		}
	}

	// Token: 0x170003CC RID: 972
	// (get) Token: 0x0600156C RID: 5484 RVA: 0x000B8B24 File Offset: 0x000B6D24
	// (set) Token: 0x0600156D RID: 5485 RVA: 0x000B8B44 File Offset: 0x000B6D44
	public static int ParticleCount
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_ParticleCount");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_ParticleCount", value);
		}
	}

	// Token: 0x170003CD RID: 973
	// (get) Token: 0x0600156E RID: 5486 RVA: 0x000B8B65 File Offset: 0x000B6D65
	// (set) Token: 0x0600156F RID: 5487 RVA: 0x000B8B85 File Offset: 0x000B6D85
	public static bool RimLight
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_RimLight");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_RimLight", value);
		}
	}

	// Token: 0x170003CE RID: 974
	// (get) Token: 0x06001570 RID: 5488 RVA: 0x000B8BA6 File Offset: 0x000B6DA6
	// (set) Token: 0x06001571 RID: 5489 RVA: 0x000B8BC6 File Offset: 0x000B6DC6
	public static bool DepthOfField
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_DepthOfField");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_DepthOfField", value);
		}
	}

	// Token: 0x170003CF RID: 975
	// (get) Token: 0x06001572 RID: 5490 RVA: 0x000B8BE7 File Offset: 0x000B6DE7
	// (set) Token: 0x06001573 RID: 5491 RVA: 0x000B8C07 File Offset: 0x000B6E07
	public static int Sensitivity
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_Sensitivity");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_Sensitivity", value);
		}
	}

	// Token: 0x170003D0 RID: 976
	// (get) Token: 0x06001574 RID: 5492 RVA: 0x000B8C28 File Offset: 0x000B6E28
	// (set) Token: 0x06001575 RID: 5493 RVA: 0x000B8C48 File Offset: 0x000B6E48
	public static bool InvertAxis
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_InvertAxis");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_InvertAxis", value);
		}
	}

	// Token: 0x170003D1 RID: 977
	// (get) Token: 0x06001576 RID: 5494 RVA: 0x000B8C69 File Offset: 0x000B6E69
	// (set) Token: 0x06001577 RID: 5495 RVA: 0x000B8C89 File Offset: 0x000B6E89
	public static bool TutorialsOff
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_TutorialsOff");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_TutorialsOff", value);
		}
	}

	// Token: 0x170003D2 RID: 978
	// (get) Token: 0x06001578 RID: 5496 RVA: 0x000B8CAA File Offset: 0x000B6EAA
	// (set) Token: 0x06001579 RID: 5497 RVA: 0x000B8CCA File Offset: 0x000B6ECA
	public static bool ToggleRun
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_ToggleRun");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_ToggleRun", value);
		}
	}

	// Token: 0x0600157A RID: 5498 RVA: 0x000B8CEC File Offset: 0x000B6EEC
	public static void DeleteAll()
	{
		Globals.Delete("Profile_" + GameGlobals.Profile + "_DisableBloom");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_DisableFarAnimations");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_DisableOutlines");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_DisablePostAliasing");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_EnableShadows");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_DisableObscurance");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_DrawDistance");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_DrawDistanceLimit");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_Fog");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_FPSIndex");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_HighPopulation");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_LowDetailStudents");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_ParticleCount");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_RimLight");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_DepthOfField");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_Sensitivity");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_InvertAxis");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_TutorialsOff");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_ToggleRun");
	}

	// Token: 0x04001D93 RID: 7571
	private const string Str_DisableBloom = "DisableBloom";

	// Token: 0x04001D94 RID: 7572
	private const string Str_DisableFarAnimations = "DisableFarAnimations";

	// Token: 0x04001D95 RID: 7573
	private const string Str_DisableOutlines = "DisableOutlines";

	// Token: 0x04001D96 RID: 7574
	private const string Str_DisablePostAliasing = "DisablePostAliasing";

	// Token: 0x04001D97 RID: 7575
	private const string Str_EnableShadows = "EnableShadows";

	// Token: 0x04001D98 RID: 7576
	private const string Str_DisableObscurance = "DisableObscurance";

	// Token: 0x04001D99 RID: 7577
	private const string Str_DrawDistance = "DrawDistance";

	// Token: 0x04001D9A RID: 7578
	private const string Str_DrawDistanceLimit = "DrawDistanceLimit";

	// Token: 0x04001D9B RID: 7579
	private const string Str_Fog = "Fog";

	// Token: 0x04001D9C RID: 7580
	private const string Str_FPSIndex = "FPSIndex";

	// Token: 0x04001D9D RID: 7581
	private const string Str_HighPopulation = "HighPopulation";

	// Token: 0x04001D9E RID: 7582
	private const string Str_LowDetailStudents = "LowDetailStudents";

	// Token: 0x04001D9F RID: 7583
	private const string Str_ParticleCount = "ParticleCount";

	// Token: 0x04001DA0 RID: 7584
	private const string Str_RimLight = "RimLight";

	// Token: 0x04001DA1 RID: 7585
	private const string Str_DepthOfField = "DepthOfField";

	// Token: 0x04001DA2 RID: 7586
	private const string Str_Sensitivity = "Sensitivity";

	// Token: 0x04001DA3 RID: 7587
	private const string Str_InvertAxis = "InvertAxis";

	// Token: 0x04001DA4 RID: 7588
	private const string Str_TutorialsOff = "TutorialsOff";

	// Token: 0x04001DA5 RID: 7589
	private const string Str_ToggleRun = "ToggleRun";
}
