using System;

// Token: 0x020002D1 RID: 721
public static class YanvaniaGlobals
{
	// Token: 0x17000413 RID: 1043
	// (get) Token: 0x06001683 RID: 5763 RVA: 0x000BCC2D File Offset: 0x000BAE2D
	// (set) Token: 0x06001684 RID: 5764 RVA: 0x000BCC4D File Offset: 0x000BAE4D
	public static bool DraculaDefeated
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_DraculaDefeated");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_DraculaDefeated", value);
		}
	}

	// Token: 0x17000414 RID: 1044
	// (get) Token: 0x06001685 RID: 5765 RVA: 0x000BCC6E File Offset: 0x000BAE6E
	// (set) Token: 0x06001686 RID: 5766 RVA: 0x000BCC8E File Offset: 0x000BAE8E
	public static bool MidoriEasterEgg
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_MidoriEasterEgg");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_MidoriEasterEgg", value);
		}
	}

	// Token: 0x06001687 RID: 5767 RVA: 0x000BCCAF File Offset: 0x000BAEAF
	public static void DeleteAll()
	{
		Globals.Delete("Profile_" + GameGlobals.Profile + "_DraculaDefeated");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_MidoriEasterEgg");
	}

	// Token: 0x04001E12 RID: 7698
	private const string Str_DraculaDefeated = "DraculaDefeated";

	// Token: 0x04001E13 RID: 7699
	private const string Str_MidoriEasterEgg = "MidoriEasterEgg";
}
