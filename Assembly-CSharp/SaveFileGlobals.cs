using System;
using UnityEngine;

// Token: 0x020002CB RID: 715
public static class SaveFileGlobals
{
	// Token: 0x170003EB RID: 1003
	// (get) Token: 0x060015C7 RID: 5575 RVA: 0x000B9F9B File Offset: 0x000B819B
	// (set) Token: 0x060015C8 RID: 5576 RVA: 0x000B9FBB File Offset: 0x000B81BB
	public static int CurrentSaveFile
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_CurrentSaveFile");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_CurrentSaveFile", value);
		}
	}

	// Token: 0x060015C9 RID: 5577 RVA: 0x000B9FDC File Offset: 0x000B81DC
	public static void DeleteAll()
	{
		Globals.Delete("Profile_" + GameGlobals.Profile + "_CurrentSaveFile");
	}

	// Token: 0x04001DC7 RID: 7623
	private const string Str_CurrentSaveFile = "CurrentSaveFile";
}
