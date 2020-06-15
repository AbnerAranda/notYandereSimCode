using System;
using UnityEngine;

// Token: 0x020002C2 RID: 706
public static class DateGlobals
{
	// Token: 0x1700038F RID: 911
	// (get) Token: 0x060014DA RID: 5338 RVA: 0x000B73D7 File Offset: 0x000B55D7
	// (set) Token: 0x060014DB RID: 5339 RVA: 0x000B73F7 File Offset: 0x000B55F7
	public static int Week
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_Week");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_Week", value);
		}
	}

	// Token: 0x17000390 RID: 912
	// (get) Token: 0x060014DC RID: 5340 RVA: 0x000B7418 File Offset: 0x000B5618
	// (set) Token: 0x060014DD RID: 5341 RVA: 0x000B7438 File Offset: 0x000B5638
	public static DayOfWeek Weekday
	{
		get
		{
			return GlobalsHelper.GetEnum<DayOfWeek>("Profile_" + GameGlobals.Profile + "_Weekday");
		}
		set
		{
			GlobalsHelper.SetEnum<DayOfWeek>("Profile_" + GameGlobals.Profile + "_Weekday", value);
		}
	}

	// Token: 0x17000391 RID: 913
	// (get) Token: 0x060014DE RID: 5342 RVA: 0x000B7459 File Offset: 0x000B5659
	// (set) Token: 0x060014DF RID: 5343 RVA: 0x000B7479 File Offset: 0x000B5679
	public static int PassDays
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_PassDays");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_PassDays", value);
		}
	}

	// Token: 0x17000392 RID: 914
	// (get) Token: 0x060014E0 RID: 5344 RVA: 0x000B749A File Offset: 0x000B569A
	// (set) Token: 0x060014E1 RID: 5345 RVA: 0x000B74BA File Offset: 0x000B56BA
	public static bool DayPassed
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_DayPassed");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_DayPassed", value);
		}
	}

	// Token: 0x060014E2 RID: 5346 RVA: 0x000B74DC File Offset: 0x000B56DC
	public static void DeleteAll()
	{
		Globals.Delete("Profile_" + GameGlobals.Profile + "_Week");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_Weekday");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_PassDays");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_DayPassed");
	}

	// Token: 0x04001D5C RID: 7516
	private const string Str_Week = "Week";

	// Token: 0x04001D5D RID: 7517
	private const string Str_Weekday = "Weekday";

	// Token: 0x04001D5E RID: 7518
	private const string Str_PassDays = "PassDays";

	// Token: 0x04001D5F RID: 7519
	private const string Str_DayPassed = "DayPassed";
}
