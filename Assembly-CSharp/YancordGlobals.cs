using System;
using UnityEngine;

// Token: 0x020002D5 RID: 725
public static class YancordGlobals
{
	// Token: 0x1700043D RID: 1085
	// (get) Token: 0x060016DE RID: 5854 RVA: 0x000BDCC9 File Offset: 0x000BBEC9
	// (set) Token: 0x060016DF RID: 5855 RVA: 0x000BDCE9 File Offset: 0x000BBEE9
	public static bool JoinedYancord
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_JoinedYancord");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_JoinedYancord", value);
		}
	}

	// Token: 0x1700043E RID: 1086
	// (get) Token: 0x060016E0 RID: 5856 RVA: 0x000BDD0A File Offset: 0x000BBF0A
	// (set) Token: 0x060016E1 RID: 5857 RVA: 0x000BDD2A File Offset: 0x000BBF2A
	public static int CurrentConversation
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_CurrentConversation");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_CurrentConversation", value);
		}
	}

	// Token: 0x060016E2 RID: 5858 RVA: 0x000BDD4B File Offset: 0x000BBF4B
	public static void DeleteAll()
	{
		Globals.Delete("Profile_" + GameGlobals.Profile + "_JoinedYancord");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_CurrentConversation");
	}

	// Token: 0x04001E3D RID: 7741
	private const string Str_JoinedYancord = "JoinedYancord";

	// Token: 0x04001E3E RID: 7742
	private const string Str_CurrentConversation = "CurrentConversation";
}
