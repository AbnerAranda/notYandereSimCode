using System;

// Token: 0x020002D3 RID: 723
public static class TutorialGlobals
{
	// Token: 0x17000415 RID: 1045
	// (get) Token: 0x0600168C RID: 5772 RVA: 0x000BCDD3 File Offset: 0x000BAFD3
	// (set) Token: 0x0600168D RID: 5773 RVA: 0x000BCDF3 File Offset: 0x000BAFF3
	public static bool IgnoreClothing
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_IgnoreClothing");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_IgnoreClothing", value);
		}
	}

	// Token: 0x17000416 RID: 1046
	// (get) Token: 0x0600168E RID: 5774 RVA: 0x000BCE14 File Offset: 0x000BB014
	// (set) Token: 0x0600168F RID: 5775 RVA: 0x000BCE34 File Offset: 0x000BB034
	public static bool IgnoreCouncil
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_IgnoreCouncil");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_IgnoreCouncil", value);
		}
	}

	// Token: 0x17000417 RID: 1047
	// (get) Token: 0x06001690 RID: 5776 RVA: 0x000BCE55 File Offset: 0x000BB055
	// (set) Token: 0x06001691 RID: 5777 RVA: 0x000BCE75 File Offset: 0x000BB075
	public static bool IgnoreTeacher
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_IgnoreTeacher");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_IgnoreTeacher", value);
		}
	}

	// Token: 0x17000418 RID: 1048
	// (get) Token: 0x06001692 RID: 5778 RVA: 0x000BCE96 File Offset: 0x000BB096
	// (set) Token: 0x06001693 RID: 5779 RVA: 0x000BCEB6 File Offset: 0x000BB0B6
	public static bool IgnoreLocker
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_IgnoreLocker");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_IgnoreLocker", value);
		}
	}

	// Token: 0x17000419 RID: 1049
	// (get) Token: 0x06001694 RID: 5780 RVA: 0x000BCED7 File Offset: 0x000BB0D7
	// (set) Token: 0x06001695 RID: 5781 RVA: 0x000BCEF7 File Offset: 0x000BB0F7
	public static bool IgnorePolice
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_IgnorePolice");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_IgnorePolice", value);
		}
	}

	// Token: 0x1700041A RID: 1050
	// (get) Token: 0x06001696 RID: 5782 RVA: 0x000BCF18 File Offset: 0x000BB118
	// (set) Token: 0x06001697 RID: 5783 RVA: 0x000BCF38 File Offset: 0x000BB138
	public static bool IgnoreSanity
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_IgnoreSanity");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_IgnoreSanity", value);
		}
	}

	// Token: 0x1700041B RID: 1051
	// (get) Token: 0x06001698 RID: 5784 RVA: 0x000BCF59 File Offset: 0x000BB159
	// (set) Token: 0x06001699 RID: 5785 RVA: 0x000BCF79 File Offset: 0x000BB179
	public static bool IgnoreSenpai
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_IgnoreSenpai");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_IgnoreSenpai", value);
		}
	}

	// Token: 0x1700041C RID: 1052
	// (get) Token: 0x0600169A RID: 5786 RVA: 0x000BCF9A File Offset: 0x000BB19A
	// (set) Token: 0x0600169B RID: 5787 RVA: 0x000BCFBA File Offset: 0x000BB1BA
	public static bool IgnoreVision
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_IgnoreVision");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_IgnoreVision", value);
		}
	}

	// Token: 0x1700041D RID: 1053
	// (get) Token: 0x0600169C RID: 5788 RVA: 0x000BCFDB File Offset: 0x000BB1DB
	// (set) Token: 0x0600169D RID: 5789 RVA: 0x000BCFFB File Offset: 0x000BB1FB
	public static bool IgnoreWeapon
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_IgnoreWeapon");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_IgnoreWeapon", value);
		}
	}

	// Token: 0x1700041E RID: 1054
	// (get) Token: 0x0600169E RID: 5790 RVA: 0x000BD01C File Offset: 0x000BB21C
	// (set) Token: 0x0600169F RID: 5791 RVA: 0x000BD03C File Offset: 0x000BB23C
	public static bool IgnoreBlood
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_IgnoreBlood");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_IgnoreBlood", value);
		}
	}

	// Token: 0x1700041F RID: 1055
	// (get) Token: 0x060016A0 RID: 5792 RVA: 0x000BD05D File Offset: 0x000BB25D
	// (set) Token: 0x060016A1 RID: 5793 RVA: 0x000BD07D File Offset: 0x000BB27D
	public static bool IgnoreClass
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_IgnoreClass");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_IgnoreClass", value);
		}
	}

	// Token: 0x17000420 RID: 1056
	// (get) Token: 0x060016A2 RID: 5794 RVA: 0x000BD09E File Offset: 0x000BB29E
	// (set) Token: 0x060016A3 RID: 5795 RVA: 0x000BD0BE File Offset: 0x000BB2BE
	public static bool IgnorePhoto
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_IgnorePhoto");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_IgnorePhoto", value);
		}
	}

	// Token: 0x17000421 RID: 1057
	// (get) Token: 0x060016A4 RID: 5796 RVA: 0x000BD0DF File Offset: 0x000BB2DF
	// (set) Token: 0x060016A5 RID: 5797 RVA: 0x000BD0FF File Offset: 0x000BB2FF
	public static bool IgnoreClub
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_IgnoreClub");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_IgnoreClub", value);
		}
	}

	// Token: 0x17000422 RID: 1058
	// (get) Token: 0x060016A6 RID: 5798 RVA: 0x000BD120 File Offset: 0x000BB320
	// (set) Token: 0x060016A7 RID: 5799 RVA: 0x000BD140 File Offset: 0x000BB340
	public static bool IgnoreInfo
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_IgnoreInfo");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_IgnoreInfo", value);
		}
	}

	// Token: 0x17000423 RID: 1059
	// (get) Token: 0x060016A8 RID: 5800 RVA: 0x000BD161 File Offset: 0x000BB361
	// (set) Token: 0x060016A9 RID: 5801 RVA: 0x000BD181 File Offset: 0x000BB381
	public static bool IgnorePool
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_IgnorePool");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_IgnorePool", value);
		}
	}

	// Token: 0x17000424 RID: 1060
	// (get) Token: 0x060016AA RID: 5802 RVA: 0x000BD1A2 File Offset: 0x000BB3A2
	// (set) Token: 0x060016AB RID: 5803 RVA: 0x000BD1C2 File Offset: 0x000BB3C2
	public static bool IgnoreRep
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_IgnoreClass");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_IgnoreClass", value);
		}
	}

	// Token: 0x060016AC RID: 5804 RVA: 0x000BD1E4 File Offset: 0x000BB3E4
	public static void DeleteAll()
	{
		Globals.Delete("Profile_" + GameGlobals.Profile + "_IgnoreClothing");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_IgnoreCouncil");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_IgnoreTeacher");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_IgnoreLocker");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_IgnorePolice");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_IgnoreSanity");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_IgnoreSenpai");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_IgnoreVision");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_IgnoreWeapon");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_IgnoreBlood");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_IgnoreClass");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_IgnorePhoto");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_IgnoreClub");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_IgnoreInfo");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_IgnorePool");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_IgnoreClass");
	}

	// Token: 0x04001E15 RID: 7701
	private const string Str_IgnoreClothing = "IgnoreClothing";

	// Token: 0x04001E16 RID: 7702
	private const string Str_IgnoreCouncil = "IgnoreCouncil";

	// Token: 0x04001E17 RID: 7703
	private const string Str_IgnoreTeacher = "IgnoreTeacher";

	// Token: 0x04001E18 RID: 7704
	private const string Str_IgnoreLocker = "IgnoreLocker";

	// Token: 0x04001E19 RID: 7705
	private const string Str_IgnorePolice = "IgnorePolice";

	// Token: 0x04001E1A RID: 7706
	private const string Str_IgnoreSanity = "IgnoreSanity";

	// Token: 0x04001E1B RID: 7707
	private const string Str_IgnoreSenpai = "IgnoreSenpai";

	// Token: 0x04001E1C RID: 7708
	private const string Str_IgnoreVision = "IgnoreVision";

	// Token: 0x04001E1D RID: 7709
	private const string Str_IgnoreWeapon = "IgnoreWeapon";

	// Token: 0x04001E1E RID: 7710
	private const string Str_IgnoreBlood = "IgnoreBlood";

	// Token: 0x04001E1F RID: 7711
	private const string Str_IgnoreClass = "IgnoreClass";

	// Token: 0x04001E20 RID: 7712
	private const string Str_IgnorePhoto = "IgnorePhoto";

	// Token: 0x04001E21 RID: 7713
	private const string Str_IgnoreClub = "IgnoreClub";

	// Token: 0x04001E22 RID: 7714
	private const string Str_IgnoreInfo = "IgnoreInfo";

	// Token: 0x04001E23 RID: 7715
	private const string Str_IgnorePool = "IgnorePool";

	// Token: 0x04001E24 RID: 7716
	private const string Str_IgnoreRep = "IgnoreClass";
}
