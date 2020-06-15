using System;
using UnityEngine;

// Token: 0x020002D4 RID: 724
public static class CounselorGlobals
{
	// Token: 0x17000425 RID: 1061
	// (get) Token: 0x060016AD RID: 5805 RVA: 0x000BD3D1 File Offset: 0x000BB5D1
	// (set) Token: 0x060016AE RID: 5806 RVA: 0x000BD3F1 File Offset: 0x000BB5F1
	public static int DelinquentPunishments
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_DelinquentPunishments");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_DelinquentPunishments", value);
		}
	}

	// Token: 0x17000426 RID: 1062
	// (get) Token: 0x060016AF RID: 5807 RVA: 0x000BD412 File Offset: 0x000BB612
	// (set) Token: 0x060016B0 RID: 5808 RVA: 0x000BD432 File Offset: 0x000BB632
	public static int CounselorPunishments
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_CounselorPunishments");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_CounselorPunishments", value);
		}
	}

	// Token: 0x17000427 RID: 1063
	// (get) Token: 0x060016B1 RID: 5809 RVA: 0x000BD453 File Offset: 0x000BB653
	// (set) Token: 0x060016B2 RID: 5810 RVA: 0x000BD473 File Offset: 0x000BB673
	public static int CounselorVisits
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_CounselorVisits");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_CounselorVisits", value);
		}
	}

	// Token: 0x17000428 RID: 1064
	// (get) Token: 0x060016B3 RID: 5811 RVA: 0x000BD494 File Offset: 0x000BB694
	// (set) Token: 0x060016B4 RID: 5812 RVA: 0x000BD4B4 File Offset: 0x000BB6B4
	public static int CounselorTape
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_CounselorTape");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_CounselorTape", value);
		}
	}

	// Token: 0x17000429 RID: 1065
	// (get) Token: 0x060016B5 RID: 5813 RVA: 0x000BD4D5 File Offset: 0x000BB6D5
	// (set) Token: 0x060016B6 RID: 5814 RVA: 0x000BD4F5 File Offset: 0x000BB6F5
	public static int ApologiesUsed
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_ApologiesUsed");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_ApologiesUsed", value);
		}
	}

	// Token: 0x1700042A RID: 1066
	// (get) Token: 0x060016B7 RID: 5815 RVA: 0x000BD516 File Offset: 0x000BB716
	// (set) Token: 0x060016B8 RID: 5816 RVA: 0x000BD536 File Offset: 0x000BB736
	public static int WeaponsBanned
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_WeaponsBanned");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_WeaponsBanned", value);
		}
	}

	// Token: 0x1700042B RID: 1067
	// (get) Token: 0x060016B9 RID: 5817 RVA: 0x000BD557 File Offset: 0x000BB757
	// (set) Token: 0x060016BA RID: 5818 RVA: 0x000BD577 File Offset: 0x000BB777
	public static int BloodVisits
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_BloodVisits");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_BloodVisits", value);
		}
	}

	// Token: 0x1700042C RID: 1068
	// (get) Token: 0x060016BB RID: 5819 RVA: 0x000BD598 File Offset: 0x000BB798
	// (set) Token: 0x060016BC RID: 5820 RVA: 0x000BD5B8 File Offset: 0x000BB7B8
	public static int InsanityVisits
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_InsanityVisits");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_InsanityVisits", value);
		}
	}

	// Token: 0x1700042D RID: 1069
	// (get) Token: 0x060016BD RID: 5821 RVA: 0x000BD5D9 File Offset: 0x000BB7D9
	// (set) Token: 0x060016BE RID: 5822 RVA: 0x000BD5F9 File Offset: 0x000BB7F9
	public static int LewdVisits
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_LewdVisits");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_LewdVisits", value);
		}
	}

	// Token: 0x1700042E RID: 1070
	// (get) Token: 0x060016BF RID: 5823 RVA: 0x000BD61A File Offset: 0x000BB81A
	// (set) Token: 0x060016C0 RID: 5824 RVA: 0x000BD63A File Offset: 0x000BB83A
	public static int TheftVisits
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_TheftVisits");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_TheftVisits", value);
		}
	}

	// Token: 0x1700042F RID: 1071
	// (get) Token: 0x060016C1 RID: 5825 RVA: 0x000BD65B File Offset: 0x000BB85B
	// (set) Token: 0x060016C2 RID: 5826 RVA: 0x000BD67B File Offset: 0x000BB87B
	public static int TrespassVisits
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_TrespassVisits");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_TrespassVisits", value);
		}
	}

	// Token: 0x17000430 RID: 1072
	// (get) Token: 0x060016C3 RID: 5827 RVA: 0x000BD69C File Offset: 0x000BB89C
	// (set) Token: 0x060016C4 RID: 5828 RVA: 0x000BD6BC File Offset: 0x000BB8BC
	public static int WeaponVisits
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_WeaponVisits");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_WeaponVisits", value);
		}
	}

	// Token: 0x17000431 RID: 1073
	// (get) Token: 0x060016C5 RID: 5829 RVA: 0x000BD6DD File Offset: 0x000BB8DD
	// (set) Token: 0x060016C6 RID: 5830 RVA: 0x000BD6FD File Offset: 0x000BB8FD
	public static int BloodExcuseUsed
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_BloodExcuseUsed");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_BloodExcuseUsed", value);
		}
	}

	// Token: 0x17000432 RID: 1074
	// (get) Token: 0x060016C7 RID: 5831 RVA: 0x000BD71E File Offset: 0x000BB91E
	// (set) Token: 0x060016C8 RID: 5832 RVA: 0x000BD73E File Offset: 0x000BB93E
	public static int InsanityExcuseUsed
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_InsanityExcuseUsed");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_InsanityExcuseUsed", value);
		}
	}

	// Token: 0x17000433 RID: 1075
	// (get) Token: 0x060016C9 RID: 5833 RVA: 0x000BD75F File Offset: 0x000BB95F
	// (set) Token: 0x060016CA RID: 5834 RVA: 0x000BD77F File Offset: 0x000BB97F
	public static int LewdExcuseUsed
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_LewdExcuseUsed");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_LewdExcuseUsed", value);
		}
	}

	// Token: 0x17000434 RID: 1076
	// (get) Token: 0x060016CB RID: 5835 RVA: 0x000BD7A0 File Offset: 0x000BB9A0
	// (set) Token: 0x060016CC RID: 5836 RVA: 0x000BD7C0 File Offset: 0x000BB9C0
	public static int TheftExcuseUsed
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_TheftExcuseUsed");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_TheftExcuseUsed", value);
		}
	}

	// Token: 0x17000435 RID: 1077
	// (get) Token: 0x060016CD RID: 5837 RVA: 0x000BD7E1 File Offset: 0x000BB9E1
	// (set) Token: 0x060016CE RID: 5838 RVA: 0x000BD801 File Offset: 0x000BBA01
	public static int TrespassExcuseUsed
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_TrespassExcuseUsed");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_TrespassExcuseUsed", value);
		}
	}

	// Token: 0x17000436 RID: 1078
	// (get) Token: 0x060016CF RID: 5839 RVA: 0x000BD822 File Offset: 0x000BBA22
	// (set) Token: 0x060016D0 RID: 5840 RVA: 0x000BD842 File Offset: 0x000BBA42
	public static int WeaponExcuseUsed
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_WeaponExcuseUsed");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_WeaponExcuseUsed", value);
		}
	}

	// Token: 0x17000437 RID: 1079
	// (get) Token: 0x060016D1 RID: 5841 RVA: 0x000BD863 File Offset: 0x000BBA63
	// (set) Token: 0x060016D2 RID: 5842 RVA: 0x000BD883 File Offset: 0x000BBA83
	public static int BloodBlameUsed
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_BloodBlameUsed");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_BloodBlameUsed", value);
		}
	}

	// Token: 0x17000438 RID: 1080
	// (get) Token: 0x060016D3 RID: 5843 RVA: 0x000BD8A4 File Offset: 0x000BBAA4
	// (set) Token: 0x060016D4 RID: 5844 RVA: 0x000BD8C4 File Offset: 0x000BBAC4
	public static int InsanityBlameUsed
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_InsanityBlameUsed");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_InsanityBlameUsed", value);
		}
	}

	// Token: 0x17000439 RID: 1081
	// (get) Token: 0x060016D5 RID: 5845 RVA: 0x000BD8E5 File Offset: 0x000BBAE5
	// (set) Token: 0x060016D6 RID: 5846 RVA: 0x000BD905 File Offset: 0x000BBB05
	public static int LewdBlameUsed
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_LewdBlameUsed");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_LewdBlameUsed", value);
		}
	}

	// Token: 0x1700043A RID: 1082
	// (get) Token: 0x060016D7 RID: 5847 RVA: 0x000BD926 File Offset: 0x000BBB26
	// (set) Token: 0x060016D8 RID: 5848 RVA: 0x000BD946 File Offset: 0x000BBB46
	public static int TheftBlameUsed
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_TheftBlameUsed");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_TheftBlameUsed", value);
		}
	}

	// Token: 0x1700043B RID: 1083
	// (get) Token: 0x060016D9 RID: 5849 RVA: 0x000BD967 File Offset: 0x000BBB67
	// (set) Token: 0x060016DA RID: 5850 RVA: 0x000BD987 File Offset: 0x000BBB87
	public static int TrespassBlameUsed
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_TrespassBlameUsed");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_TrespassBlameUsed", value);
		}
	}

	// Token: 0x1700043C RID: 1084
	// (get) Token: 0x060016DB RID: 5851 RVA: 0x000BD9A8 File Offset: 0x000BBBA8
	// (set) Token: 0x060016DC RID: 5852 RVA: 0x000BD9C8 File Offset: 0x000BBBC8
	public static int WeaponBlameUsed
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_WeaponBlameUsed");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_WeaponBlameUsed", value);
		}
	}

	// Token: 0x060016DD RID: 5853 RVA: 0x000BD9EC File Offset: 0x000BBBEC
	public static void DeleteAll()
	{
		Globals.Delete("Profile_" + GameGlobals.Profile + "_DelinquentPunishments");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_CounselorPunishments");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_CounselorVisits");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_CounselorTape");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_ApologiesUsed");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_WeaponsBanned");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_BloodVisits");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_InsanityVisits");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_LewdVisits");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_TheftVisits");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_TrespassVisits");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_WeaponVisits");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_BloodExcuseUsed");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_InsanityExcuseUsed");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_LewdExcuseUsed");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_TheftExcuseUsed");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_TrespassExcuseUsed");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_WeaponExcuseUsed");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_BloodBlameUsed");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_InsanityBlameUsed");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_LewdBlameUsed");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_TheftBlameUsed");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_TrespassBlameUsed");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_WeaponBlameUsed");
	}

	// Token: 0x04001E25 RID: 7717
	private const string Str_DelinquentPunishments = "DelinquentPunishments";

	// Token: 0x04001E26 RID: 7718
	private const string Str_CounselorPunishments = "CounselorPunishments";

	// Token: 0x04001E27 RID: 7719
	private const string Str_CounselorVisits = "CounselorVisits";

	// Token: 0x04001E28 RID: 7720
	private const string Str_CounselorTape = "CounselorTape";

	// Token: 0x04001E29 RID: 7721
	private const string Str_ApologiesUsed = "ApologiesUsed";

	// Token: 0x04001E2A RID: 7722
	private const string Str_WeaponsBanned = "WeaponsBanned";

	// Token: 0x04001E2B RID: 7723
	private const string Str_BloodVisits = "BloodVisits";

	// Token: 0x04001E2C RID: 7724
	private const string Str_InsanityVisits = "InsanityVisits";

	// Token: 0x04001E2D RID: 7725
	private const string Str_LewdVisits = "LewdVisits";

	// Token: 0x04001E2E RID: 7726
	private const string Str_TheftVisits = "TheftVisits";

	// Token: 0x04001E2F RID: 7727
	private const string Str_TrespassVisits = "TrespassVisits";

	// Token: 0x04001E30 RID: 7728
	private const string Str_WeaponVisits = "WeaponVisits";

	// Token: 0x04001E31 RID: 7729
	private const string Str_BloodExcuseUsed = "BloodExcuseUsed";

	// Token: 0x04001E32 RID: 7730
	private const string Str_InsanityExcuseUsed = "InsanityExcuseUsed";

	// Token: 0x04001E33 RID: 7731
	private const string Str_LewdExcuseUsed = "LewdExcuseUsed";

	// Token: 0x04001E34 RID: 7732
	private const string Str_TheftExcuseUsed = "TheftExcuseUsed";

	// Token: 0x04001E35 RID: 7733
	private const string Str_TrespassExcuseUsed = "TrespassExcuseUsed";

	// Token: 0x04001E36 RID: 7734
	private const string Str_WeaponExcuseUsed = "WeaponExcuseUsed";

	// Token: 0x04001E37 RID: 7735
	private const string Str_BloodBlameUsed = "BloodBlameUsed";

	// Token: 0x04001E38 RID: 7736
	private const string Str_InsanityBlameUsed = "InsanityBlameUsed";

	// Token: 0x04001E39 RID: 7737
	private const string Str_LewdBlameUsed = "LewdBlameUsed";

	// Token: 0x04001E3A RID: 7738
	private const string Str_TheftBlameUsed = "TheftBlameUsed";

	// Token: 0x04001E3B RID: 7739
	private const string Str_TrespassBlameUsed = "TrespassBlameUsed";

	// Token: 0x04001E3C RID: 7740
	private const string Str_WeaponBlameUsed = "WeaponBlameUsed";
}
