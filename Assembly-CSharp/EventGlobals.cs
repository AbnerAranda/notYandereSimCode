using System;

// Token: 0x020002C4 RID: 708
public static class EventGlobals
{
	// Token: 0x17000397 RID: 919
	// (get) Token: 0x060014FB RID: 5371 RVA: 0x000B7B58 File Offset: 0x000B5D58
	// (set) Token: 0x060014FC RID: 5372 RVA: 0x000B7B78 File Offset: 0x000B5D78
	public static bool BefriendConversation
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_BefriendConversation");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_BefriendConversation", value);
		}
	}

	// Token: 0x17000398 RID: 920
	// (get) Token: 0x060014FD RID: 5373 RVA: 0x000B7B99 File Offset: 0x000B5D99
	// (set) Token: 0x060014FE RID: 5374 RVA: 0x000B7BB9 File Offset: 0x000B5DB9
	public static bool KidnapConversation
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_KidnapConversation");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_KidnapConversation", value);
		}
	}

	// Token: 0x17000399 RID: 921
	// (get) Token: 0x060014FF RID: 5375 RVA: 0x000B7BDA File Offset: 0x000B5DDA
	// (set) Token: 0x06001500 RID: 5376 RVA: 0x000B7BFA File Offset: 0x000B5DFA
	public static bool OsanaConversation
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_OsanaConversation");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_OsanaConversation", value);
		}
	}

	// Token: 0x1700039A RID: 922
	// (get) Token: 0x06001501 RID: 5377 RVA: 0x000B7C1B File Offset: 0x000B5E1B
	// (set) Token: 0x06001502 RID: 5378 RVA: 0x000B7C3B File Offset: 0x000B5E3B
	public static bool OsanaEvent1
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_OsanaEvent1");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_OsanaEvent1", value);
		}
	}

	// Token: 0x1700039B RID: 923
	// (get) Token: 0x06001503 RID: 5379 RVA: 0x000B7C5C File Offset: 0x000B5E5C
	// (set) Token: 0x06001504 RID: 5380 RVA: 0x000B7C7C File Offset: 0x000B5E7C
	public static bool OsanaEvent2
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_OsanaEvent2");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_OsanaEvent2", value);
		}
	}

	// Token: 0x1700039C RID: 924
	// (get) Token: 0x06001505 RID: 5381 RVA: 0x000B7C9D File Offset: 0x000B5E9D
	// (set) Token: 0x06001506 RID: 5382 RVA: 0x000B7CBD File Offset: 0x000B5EBD
	public static bool Event1
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_Event1");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_Event1", value);
		}
	}

	// Token: 0x1700039D RID: 925
	// (get) Token: 0x06001507 RID: 5383 RVA: 0x000B7CDE File Offset: 0x000B5EDE
	// (set) Token: 0x06001508 RID: 5384 RVA: 0x000B7CFE File Offset: 0x000B5EFE
	public static bool Event2
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_Event2");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_Event2", value);
		}
	}

	// Token: 0x1700039E RID: 926
	// (get) Token: 0x06001509 RID: 5385 RVA: 0x000B7D1F File Offset: 0x000B5F1F
	// (set) Token: 0x0600150A RID: 5386 RVA: 0x000B7D3F File Offset: 0x000B5F3F
	public static bool LivingRoom
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_LivingRoom");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_LivingRoom", value);
		}
	}

	// Token: 0x0600150B RID: 5387 RVA: 0x000B7D60 File Offset: 0x000B5F60
	public static void DeleteAll()
	{
		Globals.Delete("Profile_" + GameGlobals.Profile + "_BefriendConversation");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_KidnapConversation");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_OsanaConversation");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_OsanaEvent1");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_OsanaEvent2");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_Event1");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_Event2");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_LivingRoom");
	}

	// Token: 0x04001D69 RID: 7529
	private const string Str_BefriendConversation = "BefriendConversation";

	// Token: 0x04001D6A RID: 7530
	private const string Str_KidnapConversation = "KidnapConversation";

	// Token: 0x04001D6B RID: 7531
	private const string Str_OsanaConversation = "OsanaConversation";

	// Token: 0x04001D6C RID: 7532
	private const string Str_Event1 = "Event1";

	// Token: 0x04001D6D RID: 7533
	private const string Str_Event2 = "Event2";

	// Token: 0x04001D6E RID: 7534
	private const string Str_OsanaEvent1 = "OsanaEvent1";

	// Token: 0x04001D6F RID: 7535
	private const string Str_OsanaEvent2 = "OsanaEvent2";

	// Token: 0x04001D70 RID: 7536
	private const string Str_LivingRoom = "LivingRoom";
}
