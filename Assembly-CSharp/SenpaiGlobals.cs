using System;
using UnityEngine;

// Token: 0x020002CE RID: 718
public static class SenpaiGlobals
{
	// Token: 0x170003F7 RID: 1015
	// (get) Token: 0x060015F7 RID: 5623 RVA: 0x000BAA63 File Offset: 0x000B8C63
	// (set) Token: 0x060015F8 RID: 5624 RVA: 0x000BAA83 File Offset: 0x000B8C83
	public static bool CustomSenpai
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_CustomSenpai");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_CustomSenpai", value);
		}
	}

	// Token: 0x170003F8 RID: 1016
	// (get) Token: 0x060015F9 RID: 5625 RVA: 0x000BAAA4 File Offset: 0x000B8CA4
	// (set) Token: 0x060015FA RID: 5626 RVA: 0x000BAAC4 File Offset: 0x000B8CC4
	public static string SenpaiEyeColor
	{
		get
		{
			return PlayerPrefs.GetString("Profile_" + GameGlobals.Profile + "_SenpaiEyeColor");
		}
		set
		{
			PlayerPrefs.SetString("Profile_" + GameGlobals.Profile + "_SenpaiEyeColor", value);
		}
	}

	// Token: 0x170003F9 RID: 1017
	// (get) Token: 0x060015FB RID: 5627 RVA: 0x000BAAE5 File Offset: 0x000B8CE5
	// (set) Token: 0x060015FC RID: 5628 RVA: 0x000BAB05 File Offset: 0x000B8D05
	public static int SenpaiEyeWear
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_SenpaiEyeWear");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_SenpaiEyeWear", value);
		}
	}

	// Token: 0x170003FA RID: 1018
	// (get) Token: 0x060015FD RID: 5629 RVA: 0x000BAB26 File Offset: 0x000B8D26
	// (set) Token: 0x060015FE RID: 5630 RVA: 0x000BAB46 File Offset: 0x000B8D46
	public static int SenpaiFacialHair
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_SenpaiFacialHair");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_SenpaiFacialHair", value);
		}
	}

	// Token: 0x170003FB RID: 1019
	// (get) Token: 0x060015FF RID: 5631 RVA: 0x000BAB67 File Offset: 0x000B8D67
	// (set) Token: 0x06001600 RID: 5632 RVA: 0x000BAB87 File Offset: 0x000B8D87
	public static string SenpaiHairColor
	{
		get
		{
			return PlayerPrefs.GetString("Profile_" + GameGlobals.Profile + "_SenpaiHairColor");
		}
		set
		{
			PlayerPrefs.SetString("Profile_" + GameGlobals.Profile + "_SenpaiHairColor", value);
		}
	}

	// Token: 0x170003FC RID: 1020
	// (get) Token: 0x06001601 RID: 5633 RVA: 0x000BABA8 File Offset: 0x000B8DA8
	// (set) Token: 0x06001602 RID: 5634 RVA: 0x000BABC8 File Offset: 0x000B8DC8
	public static int SenpaiHairStyle
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_SenpaiHairStyle");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_SenpaiHairStyle", value);
		}
	}

	// Token: 0x170003FD RID: 1021
	// (get) Token: 0x06001603 RID: 5635 RVA: 0x000BABE9 File Offset: 0x000B8DE9
	// (set) Token: 0x06001604 RID: 5636 RVA: 0x000BAC09 File Offset: 0x000B8E09
	public static int SenpaiSkinColor
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_SenpaiSkinColor");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_SenpaiSkinColor", value);
		}
	}

	// Token: 0x06001605 RID: 5637 RVA: 0x000BAC2C File Offset: 0x000B8E2C
	public static void DeleteAll()
	{
		Globals.Delete("Profile_" + GameGlobals.Profile + "_CustomSenpai");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_SenpaiEyeColor");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_SenpaiEyeWear");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_SenpaiFacialHair");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_SenpaiHairColor");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_SenpaiHairStyle");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_SenpaiSkinColor");
	}

	// Token: 0x04001DDA RID: 7642
	private const string Str_CustomSenpai = "CustomSenpai";

	// Token: 0x04001DDB RID: 7643
	private const string Str_SenpaiEyeColor = "SenpaiEyeColor";

	// Token: 0x04001DDC RID: 7644
	private const string Str_SenpaiEyeWear = "SenpaiEyeWear";

	// Token: 0x04001DDD RID: 7645
	private const string Str_SenpaiFacialHair = "SenpaiFacialHair";

	// Token: 0x04001DDE RID: 7646
	private const string Str_SenpaiHairColor = "SenpaiHairColor";

	// Token: 0x04001DDF RID: 7647
	private const string Str_SenpaiHairStyle = "SenpaiHairStyle";

	// Token: 0x04001DE0 RID: 7648
	private const string Str_SenpaiSkinColor = "SenpaiSkinColor";
}
