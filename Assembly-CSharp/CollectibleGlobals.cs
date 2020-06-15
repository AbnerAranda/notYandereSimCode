using System;
using UnityEngine;

// Token: 0x020002C0 RID: 704
public static class CollectibleGlobals
{
	// Token: 0x060014B2 RID: 5298 RVA: 0x000B67F0 File Offset: 0x000B49F0
	public static bool GetHeadmasterTapeCollected(int tapeID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_HeadmasterTapeCollected_",
			tapeID.ToString()
		}));
	}

	// Token: 0x060014B3 RID: 5299 RVA: 0x000B682C File Offset: 0x000B4A2C
	public static void SetHeadmasterTapeCollected(int tapeID, bool value)
	{
		string text = tapeID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_HeadmasterTapeCollected_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_HeadmasterTapeCollected_",
			text
		}), value);
	}

	// Token: 0x060014B4 RID: 5300 RVA: 0x000B6892 File Offset: 0x000B4A92
	public static bool GetHeadmasterTapeListened(int tapeID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_HeadmasterTapeListened_",
			tapeID.ToString()
		}));
	}

	// Token: 0x060014B5 RID: 5301 RVA: 0x000B68CC File Offset: 0x000B4ACC
	public static void SetHeadmasterTapeListened(int tapeID, bool value)
	{
		string text = tapeID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_HeadmasterTapeListened_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_HeadmasterTapeListened_",
			text
		}), value);
	}

	// Token: 0x060014B6 RID: 5302 RVA: 0x000B6932 File Offset: 0x000B4B32
	public static bool GetBasementTapeCollected(int tapeID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_BasementTapeCollected_",
			tapeID.ToString()
		}));
	}

	// Token: 0x060014B7 RID: 5303 RVA: 0x000B696C File Offset: 0x000B4B6C
	public static void SetBasementTapeCollected(int tapeID, bool value)
	{
		string text = tapeID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_BasementTapeCollected_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_BasementTapeCollected_",
			text
		}), value);
	}

	// Token: 0x060014B8 RID: 5304 RVA: 0x000B69D2 File Offset: 0x000B4BD2
	public static int[] KeysOfBasementTapeCollected()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_BasementTapeCollected_");
	}

	// Token: 0x060014B9 RID: 5305 RVA: 0x000B69F2 File Offset: 0x000B4BF2
	public static bool GetBasementTapeListened(int tapeID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_BasementTapeListened_",
			tapeID.ToString()
		}));
	}

	// Token: 0x060014BA RID: 5306 RVA: 0x000B6A2C File Offset: 0x000B4C2C
	public static void SetBasementTapeListened(int tapeID, bool value)
	{
		string text = tapeID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_BasementTapeListened_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_BasementTapeListened_",
			text
		}), value);
	}

	// Token: 0x060014BB RID: 5307 RVA: 0x000B6A92 File Offset: 0x000B4C92
	public static int[] KeysOfBasementTapeListened()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_BasementTapeListened_");
	}

	// Token: 0x060014BC RID: 5308 RVA: 0x000B6AB2 File Offset: 0x000B4CB2
	public static bool GetMangaCollected(int mangaID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_MangaCollected_",
			mangaID.ToString()
		}));
	}

	// Token: 0x060014BD RID: 5309 RVA: 0x000B6AEC File Offset: 0x000B4CEC
	public static void SetMangaCollected(int mangaID, bool value)
	{
		string text = mangaID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_MangaCollected_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_MangaCollected_",
			text
		}), value);
	}

	// Token: 0x060014BE RID: 5310 RVA: 0x000B6B52 File Offset: 0x000B4D52
	public static bool GetGiftPurchased(int giftID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_GiftPurchased_",
			giftID.ToString()
		}));
	}

	// Token: 0x060014BF RID: 5311 RVA: 0x000B6B8C File Offset: 0x000B4D8C
	public static void SetGiftPurchased(int giftID, bool value)
	{
		string text = giftID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_GiftPurchased_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_GiftPurchased_",
			text
		}), value);
	}

	// Token: 0x060014C0 RID: 5312 RVA: 0x000B6BF2 File Offset: 0x000B4DF2
	public static bool GetGiftGiven(int giftID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_GiftGiven_",
			giftID.ToString()
		}));
	}

	// Token: 0x060014C1 RID: 5313 RVA: 0x000B6C2C File Offset: 0x000B4E2C
	public static void SetGiftGiven(int giftID, bool value)
	{
		string text = giftID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_GiftGiven_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_GiftGiven_",
			text
		}), value);
	}

	// Token: 0x1700038D RID: 909
	// (get) Token: 0x060014C2 RID: 5314 RVA: 0x000B6C92 File Offset: 0x000B4E92
	// (set) Token: 0x060014C3 RID: 5315 RVA: 0x000B6CB2 File Offset: 0x000B4EB2
	public static int MatchmakingGifts
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_MatchmakingGifts");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_MatchmakingGifts", value);
		}
	}

	// Token: 0x1700038E RID: 910
	// (get) Token: 0x060014C4 RID: 5316 RVA: 0x000B6CD3 File Offset: 0x000B4ED3
	// (set) Token: 0x060014C5 RID: 5317 RVA: 0x000B6CF3 File Offset: 0x000B4EF3
	public static int SenpaiGifts
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_SenpaiGifts");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_SenpaiGifts", value);
		}
	}

	// Token: 0x060014C6 RID: 5318 RVA: 0x000B6D14 File Offset: 0x000B4F14
	public static bool GetPantyPurchased(int giftID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_PantyPurchased_",
			giftID.ToString()
		}));
	}

	// Token: 0x060014C7 RID: 5319 RVA: 0x000B6D50 File Offset: 0x000B4F50
	public static void SetPantyPurchased(int pantyID, bool value)
	{
		string text = pantyID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_PantyPurchased_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_PantyPurchased_",
			text
		}), value);
	}

	// Token: 0x060014C8 RID: 5320 RVA: 0x000B6DB6 File Offset: 0x000B4FB6
	public static int[] KeysOfMangaCollected()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_MangaCollected_");
	}

	// Token: 0x060014C9 RID: 5321 RVA: 0x000B6DD6 File Offset: 0x000B4FD6
	public static int[] KeysOfGiftPurchased()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_GiftPurchased_");
	}

	// Token: 0x060014CA RID: 5322 RVA: 0x000B6DF6 File Offset: 0x000B4FF6
	public static int[] KeysOfGiftGiven()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_GiftGiven_");
	}

	// Token: 0x060014CB RID: 5323 RVA: 0x000B6E16 File Offset: 0x000B5016
	public static int[] KeysOfPantyPurchased()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_PantyPurchased_");
	}

	// Token: 0x060014CC RID: 5324 RVA: 0x000B6E36 File Offset: 0x000B5036
	public static bool GetTapeCollected(int tapeID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_TapeCollected_",
			tapeID.ToString()
		}));
	}

	// Token: 0x060014CD RID: 5325 RVA: 0x000B6E70 File Offset: 0x000B5070
	public static void SetTapeCollected(int tapeID, bool value)
	{
		string text = tapeID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_TapeCollected_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_TapeCollected_",
			text
		}), value);
	}

	// Token: 0x060014CE RID: 5326 RVA: 0x000B6ED6 File Offset: 0x000B50D6
	public static int[] KeysOfTapeCollected()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_TapeCollected_");
	}

	// Token: 0x060014CF RID: 5327 RVA: 0x000B6EF6 File Offset: 0x000B50F6
	public static bool GetTapeListened(int tapeID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_TapeListened_",
			tapeID.ToString()
		}));
	}

	// Token: 0x060014D0 RID: 5328 RVA: 0x000B6F30 File Offset: 0x000B5130
	public static void SetTapeListened(int tapeID, bool value)
	{
		string text = tapeID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_TapeListened_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_TapeListened_",
			text
		}), value);
	}

	// Token: 0x060014D1 RID: 5329 RVA: 0x000B6F96 File Offset: 0x000B5196
	public static int[] KeysOfTapeListened()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_TapeListened_");
	}

	// Token: 0x060014D2 RID: 5330 RVA: 0x000B6FB8 File Offset: 0x000B51B8
	public static void DeleteAll()
	{
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_BasementTapeCollected_", CollectibleGlobals.KeysOfBasementTapeCollected());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_BasementTapeListened_", CollectibleGlobals.KeysOfBasementTapeListened());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_MangaCollected_", CollectibleGlobals.KeysOfMangaCollected());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_PantyPurchased_", CollectibleGlobals.KeysOfPantyPurchased());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_GiftPurchased_", CollectibleGlobals.KeysOfGiftPurchased());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_GiftGiven_", CollectibleGlobals.KeysOfGiftGiven());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_TapeCollected_", CollectibleGlobals.KeysOfTapeCollected());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_TapeListened_", CollectibleGlobals.KeysOfTapeListened());
		Globals.Delete("Profile_" + GameGlobals.Profile + "_MatchmakingGifts");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_SenpaiGifts");
	}

	// Token: 0x04001D4E RID: 7502
	private const string Str_HeadmasterTapeCollected = "HeadmasterTapeCollected_";

	// Token: 0x04001D4F RID: 7503
	private const string Str_HeadmasterTapeListened = "HeadmasterTapeListened_";

	// Token: 0x04001D50 RID: 7504
	private const string Str_BasementTapeCollected = "BasementTapeCollected_";

	// Token: 0x04001D51 RID: 7505
	private const string Str_BasementTapeListened = "BasementTapeListened_";

	// Token: 0x04001D52 RID: 7506
	private const string Str_MangaCollected = "MangaCollected_";

	// Token: 0x04001D53 RID: 7507
	private const string Str_GiftPurchased = "GiftPurchased_";

	// Token: 0x04001D54 RID: 7508
	private const string Str_GiftGiven = "GiftGiven_";

	// Token: 0x04001D55 RID: 7509
	private const string Str_MatchmakingGifts = "MatchmakingGifts";

	// Token: 0x04001D56 RID: 7510
	private const string Str_SenpaiGifts = "SenpaiGifts";

	// Token: 0x04001D57 RID: 7511
	private const string Str_PantyPurchased = "PantyPurchased_";

	// Token: 0x04001D58 RID: 7512
	private const string Str_TapeCollected = "TapeCollected_";

	// Token: 0x04001D59 RID: 7513
	private const string Str_TapeListened = "TapeListened_";
}
