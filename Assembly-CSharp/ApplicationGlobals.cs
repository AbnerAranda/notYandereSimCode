using System;
using UnityEngine;

// Token: 0x020002BD RID: 701
public static class ApplicationGlobals
{
	// Token: 0x1700037C RID: 892
	// (get) Token: 0x06001484 RID: 5252 RVA: 0x000B5DC2 File Offset: 0x000B3FC2
	// (set) Token: 0x06001485 RID: 5253 RVA: 0x000B5DE2 File Offset: 0x000B3FE2
	public static float VersionNumber
	{
		get
		{
			return PlayerPrefs.GetFloat("Profile_" + GameGlobals.Profile + "_VersionNumber");
		}
		set
		{
			PlayerPrefs.SetFloat("Profile_" + GameGlobals.Profile + "_VersionNumber", value);
		}
	}

	// Token: 0x06001486 RID: 5254 RVA: 0x000B5E03 File Offset: 0x000B4003
	public static void DeleteAll()
	{
		Globals.Delete("Profile_" + GameGlobals.Profile + "_VersionNumber");
	}

	// Token: 0x04001D3A RID: 7482
	private const string Str_VersionNumber = "VersionNumber";
}
