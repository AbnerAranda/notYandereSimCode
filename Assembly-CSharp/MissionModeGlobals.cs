using System;
using UnityEngine;

// Token: 0x020002C7 RID: 711
public static class MissionModeGlobals
{
	// Token: 0x0600153C RID: 5436 RVA: 0x000B8645 File Offset: 0x000B6845
	public static int GetMissionCondition(int id)
	{
		return PlayerPrefs.GetInt("MissionCondition_" + id.ToString());
	}

	// Token: 0x0600153D RID: 5437 RVA: 0x000B8660 File Offset: 0x000B6860
	public static void SetMissionCondition(int id, int value)
	{
		string text = id.ToString();
		KeysHelper.AddIfMissing("MissionCondition_", text);
		PlayerPrefs.SetInt("MissionCondition_" + text, value);
	}

	// Token: 0x0600153E RID: 5438 RVA: 0x000B8691 File Offset: 0x000B6891
	public static int[] KeysOfMissionCondition()
	{
		return KeysHelper.GetIntegerKeys("MissionCondition_");
	}

	// Token: 0x170003B6 RID: 950
	// (get) Token: 0x0600153F RID: 5439 RVA: 0x000B869D File Offset: 0x000B689D
	// (set) Token: 0x06001540 RID: 5440 RVA: 0x000B86A9 File Offset: 0x000B68A9
	public static int MissionDifficulty
	{
		get
		{
			return PlayerPrefs.GetInt("MissionDifficulty");
		}
		set
		{
			PlayerPrefs.SetInt("MissionDifficulty", value);
		}
	}

	// Token: 0x170003B7 RID: 951
	// (get) Token: 0x06001541 RID: 5441 RVA: 0x000B86B6 File Offset: 0x000B68B6
	// (set) Token: 0x06001542 RID: 5442 RVA: 0x000B86C2 File Offset: 0x000B68C2
	public static bool MissionMode
	{
		get
		{
			return GlobalsHelper.GetBool("MissionMode");
		}
		set
		{
			GlobalsHelper.SetBool("MissionMode", value);
		}
	}

	// Token: 0x170003B8 RID: 952
	// (get) Token: 0x06001543 RID: 5443 RVA: 0x000B86CF File Offset: 0x000B68CF
	// (set) Token: 0x06001544 RID: 5444 RVA: 0x000B86DB File Offset: 0x000B68DB
	public static bool MultiMission
	{
		get
		{
			return GlobalsHelper.GetBool("MultiMission");
		}
		set
		{
			GlobalsHelper.SetBool("MultiMission", value);
		}
	}

	// Token: 0x170003B9 RID: 953
	// (get) Token: 0x06001545 RID: 5445 RVA: 0x000B86E8 File Offset: 0x000B68E8
	// (set) Token: 0x06001546 RID: 5446 RVA: 0x000B86F4 File Offset: 0x000B68F4
	public static int MissionRequiredClothing
	{
		get
		{
			return PlayerPrefs.GetInt("MissionRequiredClothing");
		}
		set
		{
			PlayerPrefs.SetInt("MissionRequiredClothing", value);
		}
	}

	// Token: 0x170003BA RID: 954
	// (get) Token: 0x06001547 RID: 5447 RVA: 0x000B8701 File Offset: 0x000B6901
	// (set) Token: 0x06001548 RID: 5448 RVA: 0x000B870D File Offset: 0x000B690D
	public static int MissionRequiredDisposal
	{
		get
		{
			return PlayerPrefs.GetInt("MissionRequiredDisposal");
		}
		set
		{
			PlayerPrefs.SetInt("MissionRequiredDisposal", value);
		}
	}

	// Token: 0x170003BB RID: 955
	// (get) Token: 0x06001549 RID: 5449 RVA: 0x000B871A File Offset: 0x000B691A
	// (set) Token: 0x0600154A RID: 5450 RVA: 0x000B8726 File Offset: 0x000B6926
	public static int MissionRequiredWeapon
	{
		get
		{
			return PlayerPrefs.GetInt("MissionRequiredWeapon");
		}
		set
		{
			PlayerPrefs.SetInt("MissionRequiredWeapon", value);
		}
	}

	// Token: 0x170003BC RID: 956
	// (get) Token: 0x0600154B RID: 5451 RVA: 0x000B8733 File Offset: 0x000B6933
	// (set) Token: 0x0600154C RID: 5452 RVA: 0x000B873F File Offset: 0x000B693F
	public static int MissionTarget
	{
		get
		{
			return PlayerPrefs.GetInt("MissionTarget");
		}
		set
		{
			PlayerPrefs.SetInt("MissionTarget", value);
		}
	}

	// Token: 0x170003BD RID: 957
	// (get) Token: 0x0600154D RID: 5453 RVA: 0x000B874C File Offset: 0x000B694C
	// (set) Token: 0x0600154E RID: 5454 RVA: 0x000B8758 File Offset: 0x000B6958
	public static string MissionTargetName
	{
		get
		{
			return PlayerPrefs.GetString("MissionTargetName");
		}
		set
		{
			PlayerPrefs.SetString("MissionTargetName", value);
		}
	}

	// Token: 0x170003BE RID: 958
	// (get) Token: 0x0600154F RID: 5455 RVA: 0x000B8765 File Offset: 0x000B6965
	// (set) Token: 0x06001550 RID: 5456 RVA: 0x000B8771 File Offset: 0x000B6971
	public static int NemesisDifficulty
	{
		get
		{
			return PlayerPrefs.GetInt("NemesisDifficulty");
		}
		set
		{
			PlayerPrefs.SetInt("NemesisDifficulty", value);
		}
	}

	// Token: 0x170003BF RID: 959
	// (get) Token: 0x06001551 RID: 5457 RVA: 0x000B877E File Offset: 0x000B697E
	// (set) Token: 0x06001552 RID: 5458 RVA: 0x000B878A File Offset: 0x000B698A
	public static bool NemesisAggression
	{
		get
		{
			return GlobalsHelper.GetBool("NemesisAggression");
		}
		set
		{
			GlobalsHelper.SetBool("NemesisAggression", value);
		}
	}

	// Token: 0x06001553 RID: 5459 RVA: 0x000B8798 File Offset: 0x000B6998
	public static void DeleteAll()
	{
		Globals.DeleteCollection("MissionCondition_", MissionModeGlobals.KeysOfMissionCondition());
		Globals.Delete("MissionDifficulty");
		Globals.Delete("MissionMode");
		Globals.Delete("MissionRequiredClothing");
		Globals.Delete("MissionRequiredDisposal");
		Globals.Delete("MissionRequiredWeapon");
		Globals.Delete("MissionTarget");
		Globals.Delete("MissionTargetName");
		Globals.Delete("NemesisDifficulty");
		Globals.Delete("NemesisAggression");
		Globals.Delete("MultiMission");
	}

	// Token: 0x04001D88 RID: 7560
	private const string Str_MissionCondition = "MissionCondition_";

	// Token: 0x04001D89 RID: 7561
	private const string Str_MissionDifficulty = "MissionDifficulty";

	// Token: 0x04001D8A RID: 7562
	private const string Str_MissionMode = "MissionMode";

	// Token: 0x04001D8B RID: 7563
	private const string Str_MissionRequiredClothing = "MissionRequiredClothing";

	// Token: 0x04001D8C RID: 7564
	private const string Str_MissionRequiredDisposal = "MissionRequiredDisposal";

	// Token: 0x04001D8D RID: 7565
	private const string Str_MissionRequiredWeapon = "MissionRequiredWeapon";

	// Token: 0x04001D8E RID: 7566
	private const string Str_MissionTarget = "MissionTarget";

	// Token: 0x04001D8F RID: 7567
	private const string Str_MissionTargetName = "MissionTargetName";

	// Token: 0x04001D90 RID: 7568
	private const string Str_NemesisDifficulty = "NemesisDifficulty";

	// Token: 0x04001D91 RID: 7569
	private const string Str_NemesisAggression = "NemesisAggression";

	// Token: 0x04001D92 RID: 7570
	private const string Str_MultiMission = "MultiMission";
}
