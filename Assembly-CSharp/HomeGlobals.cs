using System;

// Token: 0x020002C6 RID: 710
public static class HomeGlobals
{
	// Token: 0x170003B2 RID: 946
	// (get) Token: 0x06001533 RID: 5427 RVA: 0x000B84B9 File Offset: 0x000B66B9
	// (set) Token: 0x06001534 RID: 5428 RVA: 0x000B84D9 File Offset: 0x000B66D9
	public static bool LateForSchool
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_LateForSchool");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_LateForSchool", value);
		}
	}

	// Token: 0x170003B3 RID: 947
	// (get) Token: 0x06001535 RID: 5429 RVA: 0x000B84FA File Offset: 0x000B66FA
	// (set) Token: 0x06001536 RID: 5430 RVA: 0x000B851A File Offset: 0x000B671A
	public static bool Night
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_Night");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_Night", value);
		}
	}

	// Token: 0x170003B4 RID: 948
	// (get) Token: 0x06001537 RID: 5431 RVA: 0x000B853B File Offset: 0x000B673B
	// (set) Token: 0x06001538 RID: 5432 RVA: 0x000B855B File Offset: 0x000B675B
	public static bool StartInBasement
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_StartInBasement");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_StartInBasement", value);
		}
	}

	// Token: 0x170003B5 RID: 949
	// (get) Token: 0x06001539 RID: 5433 RVA: 0x000B857C File Offset: 0x000B677C
	// (set) Token: 0x0600153A RID: 5434 RVA: 0x000B859C File Offset: 0x000B679C
	public static bool MiyukiDefeated
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_MiyukiDefeated");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_MiyukiDefeated", value);
		}
	}

	// Token: 0x0600153B RID: 5435 RVA: 0x000B85C0 File Offset: 0x000B67C0
	public static void DeleteAll()
	{
		Globals.Delete("Profile_" + GameGlobals.Profile + "_LateForSchool");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_Night");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_StartInBasement");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_MiyukiDefeated");
	}

	// Token: 0x04001D84 RID: 7556
	private const string Str_LateForSchool = "LateForSchool";

	// Token: 0x04001D85 RID: 7557
	private const string Str_Night = "Night";

	// Token: 0x04001D86 RID: 7558
	private const string Str_StartInBasement = "StartInBasement";

	// Token: 0x04001D87 RID: 7559
	private const string Str_MiyukiDefeated = "MiyukiDefeated";
}
