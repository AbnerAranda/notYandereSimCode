using System;
using UnityEngine;

// Token: 0x020002CD RID: 717
public static class SchoolGlobals
{
	// Token: 0x060015E0 RID: 5600 RVA: 0x000BA596 File Offset: 0x000B8796
	public static bool GetDemonActive(int demonID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_DemonActive_",
			demonID.ToString()
		}));
	}

	// Token: 0x060015E1 RID: 5601 RVA: 0x000BA5D0 File Offset: 0x000B87D0
	public static void SetDemonActive(int demonID, bool value)
	{
		string text = demonID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_DemonActive_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_DemonActive_",
			text
		}), value);
	}

	// Token: 0x060015E2 RID: 5602 RVA: 0x000BA636 File Offset: 0x000B8836
	public static int[] KeysOfDemonActive()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_DemonActive_");
	}

	// Token: 0x060015E3 RID: 5603 RVA: 0x000BA656 File Offset: 0x000B8856
	public static bool GetGardenGraveOccupied(int graveID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_GardenGraveOccupied_",
			graveID.ToString()
		}));
	}

	// Token: 0x060015E4 RID: 5604 RVA: 0x000BA690 File Offset: 0x000B8890
	public static void SetGardenGraveOccupied(int graveID, bool value)
	{
		string text = graveID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_GardenGraveOccupied_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_GardenGraveOccupied_",
			text
		}), value);
	}

	// Token: 0x060015E5 RID: 5605 RVA: 0x000BA6F6 File Offset: 0x000B88F6
	public static int[] KeysOfGardenGraveOccupied()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_GardenGraveOccupied_");
	}

	// Token: 0x170003EF RID: 1007
	// (get) Token: 0x060015E6 RID: 5606 RVA: 0x000BA716 File Offset: 0x000B8916
	// (set) Token: 0x060015E7 RID: 5607 RVA: 0x000BA736 File Offset: 0x000B8936
	public static int KidnapVictim
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_KidnapVictim");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_KidnapVictim", value);
		}
	}

	// Token: 0x170003F0 RID: 1008
	// (get) Token: 0x060015E8 RID: 5608 RVA: 0x000BA757 File Offset: 0x000B8957
	// (set) Token: 0x060015E9 RID: 5609 RVA: 0x000BA777 File Offset: 0x000B8977
	public static int Population
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_Population");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_Population", value);
		}
	}

	// Token: 0x170003F1 RID: 1009
	// (get) Token: 0x060015EA RID: 5610 RVA: 0x000BA798 File Offset: 0x000B8998
	// (set) Token: 0x060015EB RID: 5611 RVA: 0x000BA7B8 File Offset: 0x000B89B8
	public static bool RoofFence
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_RoofFence");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_RoofFence", value);
		}
	}

	// Token: 0x170003F2 RID: 1010
	// (get) Token: 0x060015EC RID: 5612 RVA: 0x000BA7D9 File Offset: 0x000B89D9
	// (set) Token: 0x060015ED RID: 5613 RVA: 0x000BA7F9 File Offset: 0x000B89F9
	public static float SchoolAtmosphere
	{
		get
		{
			return PlayerPrefs.GetFloat("Profile_" + GameGlobals.Profile + "_SchoolAtmosphere");
		}
		set
		{
			PlayerPrefs.SetFloat("Profile_" + GameGlobals.Profile + "_SchoolAtmosphere", value);
		}
	}

	// Token: 0x170003F3 RID: 1011
	// (get) Token: 0x060015EE RID: 5614 RVA: 0x000BA81A File Offset: 0x000B8A1A
	// (set) Token: 0x060015EF RID: 5615 RVA: 0x000BA83A File Offset: 0x000B8A3A
	public static bool SchoolAtmosphereSet
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_SchoolAtmosphereSet");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_SchoolAtmosphereSet", value);
		}
	}

	// Token: 0x170003F4 RID: 1012
	// (get) Token: 0x060015F0 RID: 5616 RVA: 0x000BA85B File Offset: 0x000B8A5B
	// (set) Token: 0x060015F1 RID: 5617 RVA: 0x000BA87B File Offset: 0x000B8A7B
	public static bool ReactedToGameLeader
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_ReactedToGameLeader");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_ReactedToGameLeader", value);
		}
	}

	// Token: 0x170003F5 RID: 1013
	// (get) Token: 0x060015F2 RID: 5618 RVA: 0x000BA89C File Offset: 0x000B8A9C
	// (set) Token: 0x060015F3 RID: 5619 RVA: 0x000BA8BC File Offset: 0x000B8ABC
	public static bool HighSecurity
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_HighSecurity");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_HighSecurity", value);
		}
	}

	// Token: 0x170003F6 RID: 1014
	// (get) Token: 0x060015F4 RID: 5620 RVA: 0x000BA8DD File Offset: 0x000B8ADD
	// (set) Token: 0x060015F5 RID: 5621 RVA: 0x000BA8FD File Offset: 0x000B8AFD
	public static bool SCP
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_SCP");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_SCP", value);
		}
	}

	// Token: 0x060015F6 RID: 5622 RVA: 0x000BA920 File Offset: 0x000B8B20
	public static void DeleteAll()
	{
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_DemonActive_", SchoolGlobals.KeysOfDemonActive());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_GardenGraveOccupied_", SchoolGlobals.KeysOfGardenGraveOccupied());
		Globals.Delete("Profile_" + GameGlobals.Profile + "_KidnapVictim");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_Population");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_RoofFence");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_SchoolAtmosphere");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_SchoolAtmosphereSet");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_ReactedToGameLeader");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_HighSecurity");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_SCP");
	}

	// Token: 0x04001DD0 RID: 7632
	private const string Str_DemonActive = "DemonActive_";

	// Token: 0x04001DD1 RID: 7633
	private const string Str_GardenGraveOccupied = "GardenGraveOccupied_";

	// Token: 0x04001DD2 RID: 7634
	private const string Str_KidnapVictim = "KidnapVictim";

	// Token: 0x04001DD3 RID: 7635
	private const string Str_Population = "Population";

	// Token: 0x04001DD4 RID: 7636
	private const string Str_RoofFence = "RoofFence";

	// Token: 0x04001DD5 RID: 7637
	private const string Str_SchoolAtmosphere = "SchoolAtmosphere";

	// Token: 0x04001DD6 RID: 7638
	private const string Str_SchoolAtmosphereSet = "SchoolAtmosphereSet";

	// Token: 0x04001DD7 RID: 7639
	private const string Str_ReactedToGameLeader = "ReactedToGameLeader";

	// Token: 0x04001DD8 RID: 7640
	private const string Str_SCP = "SCP";

	// Token: 0x04001DD9 RID: 7641
	private const string Str_HighSecurity = "HighSecurity";
}
