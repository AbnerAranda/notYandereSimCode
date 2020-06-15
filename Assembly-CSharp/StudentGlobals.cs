using System;
using UnityEngine;

// Token: 0x020002CF RID: 719
public static class StudentGlobals
{
	// Token: 0x170003FE RID: 1022
	// (get) Token: 0x06001606 RID: 5638 RVA: 0x000BAD0B File Offset: 0x000B8F0B
	// (set) Token: 0x06001607 RID: 5639 RVA: 0x000BAD2B File Offset: 0x000B8F2B
	public static bool CustomSuitor
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_CustomSuitor");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_CustomSuitor", value);
		}
	}

	// Token: 0x170003FF RID: 1023
	// (get) Token: 0x06001608 RID: 5640 RVA: 0x000BAD4C File Offset: 0x000B8F4C
	// (set) Token: 0x06001609 RID: 5641 RVA: 0x000BAD6C File Offset: 0x000B8F6C
	public static int CustomSuitorAccessory
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_CustomSuitorAccessory");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_CustomSuitorAccessory", value);
		}
	}

	// Token: 0x17000400 RID: 1024
	// (get) Token: 0x0600160A RID: 5642 RVA: 0x000BAD8D File Offset: 0x000B8F8D
	// (set) Token: 0x0600160B RID: 5643 RVA: 0x000BADAD File Offset: 0x000B8FAD
	public static bool CustomSuitorBlonde
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_CustomSuitorBlonde");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_CustomSuitorBlonde", value);
		}
	}

	// Token: 0x17000401 RID: 1025
	// (get) Token: 0x0600160C RID: 5644 RVA: 0x000BADCE File Offset: 0x000B8FCE
	// (set) Token: 0x0600160D RID: 5645 RVA: 0x000BADEE File Offset: 0x000B8FEE
	public static bool CustomSuitorBlack
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_CustomSuitorBlack");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_CustomSuitorBlack", value);
		}
	}

	// Token: 0x17000402 RID: 1026
	// (get) Token: 0x0600160E RID: 5646 RVA: 0x000BAE0F File Offset: 0x000B900F
	// (set) Token: 0x0600160F RID: 5647 RVA: 0x000BAE2F File Offset: 0x000B902F
	public static int CustomSuitorEyewear
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_CustomSuitorEyewear");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_CustomSuitorEyewear", value);
		}
	}

	// Token: 0x17000403 RID: 1027
	// (get) Token: 0x06001610 RID: 5648 RVA: 0x000BAE50 File Offset: 0x000B9050
	// (set) Token: 0x06001611 RID: 5649 RVA: 0x000BAE70 File Offset: 0x000B9070
	public static int CustomSuitorHair
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_CustomSuitorHair");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_CustomSuitorHair", value);
		}
	}

	// Token: 0x17000404 RID: 1028
	// (get) Token: 0x06001612 RID: 5650 RVA: 0x000BAE91 File Offset: 0x000B9091
	// (set) Token: 0x06001613 RID: 5651 RVA: 0x000BAEB1 File Offset: 0x000B90B1
	public static int CustomSuitorJewelry
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_CustomSuitorJewelry");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_CustomSuitorJewelry", value);
		}
	}

	// Token: 0x17000405 RID: 1029
	// (get) Token: 0x06001614 RID: 5652 RVA: 0x000BAED2 File Offset: 0x000B90D2
	// (set) Token: 0x06001615 RID: 5653 RVA: 0x000BAEF2 File Offset: 0x000B90F2
	public static bool CustomSuitorTan
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_CustomSuitorTan");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_CustomSuitorTan", value);
		}
	}

	// Token: 0x17000406 RID: 1030
	// (get) Token: 0x06001616 RID: 5654 RVA: 0x000BAF13 File Offset: 0x000B9113
	// (set) Token: 0x06001617 RID: 5655 RVA: 0x000BAF33 File Offset: 0x000B9133
	public static int ExpelProgress
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_ExpelProgress");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_ExpelProgress", value);
		}
	}

	// Token: 0x17000407 RID: 1031
	// (get) Token: 0x06001618 RID: 5656 RVA: 0x000BAF54 File Offset: 0x000B9154
	// (set) Token: 0x06001619 RID: 5657 RVA: 0x000BAF74 File Offset: 0x000B9174
	public static int FemaleUniform
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_FemaleUniform");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_FemaleUniform", value);
		}
	}

	// Token: 0x17000408 RID: 1032
	// (get) Token: 0x0600161A RID: 5658 RVA: 0x000BAF95 File Offset: 0x000B9195
	// (set) Token: 0x0600161B RID: 5659 RVA: 0x000BAFB5 File Offset: 0x000B91B5
	public static int MaleUniform
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_MaleUniform");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_MaleUniform", value);
		}
	}

	// Token: 0x17000409 RID: 1033
	// (get) Token: 0x0600161C RID: 5660 RVA: 0x000BAFD6 File Offset: 0x000B91D6
	// (set) Token: 0x0600161D RID: 5661 RVA: 0x000BAFF6 File Offset: 0x000B91F6
	public static int MemorialStudents
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_MemorialStudents");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_MemorialStudents", value);
		}
	}

	// Token: 0x1700040A RID: 1034
	// (get) Token: 0x0600161E RID: 5662 RVA: 0x000BB017 File Offset: 0x000B9217
	// (set) Token: 0x0600161F RID: 5663 RVA: 0x000BB037 File Offset: 0x000B9237
	public static int MemorialStudent1
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_MemorialStudent1");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_MemorialStudent1", value);
		}
	}

	// Token: 0x1700040B RID: 1035
	// (get) Token: 0x06001620 RID: 5664 RVA: 0x000BB058 File Offset: 0x000B9258
	// (set) Token: 0x06001621 RID: 5665 RVA: 0x000BB078 File Offset: 0x000B9278
	public static int MemorialStudent2
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_MemorialStudent2");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_MemorialStudent2", value);
		}
	}

	// Token: 0x1700040C RID: 1036
	// (get) Token: 0x06001622 RID: 5666 RVA: 0x000BB099 File Offset: 0x000B9299
	// (set) Token: 0x06001623 RID: 5667 RVA: 0x000BB0B9 File Offset: 0x000B92B9
	public static int MemorialStudent3
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_MemorialStudent3");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_MemorialStudent3", value);
		}
	}

	// Token: 0x1700040D RID: 1037
	// (get) Token: 0x06001624 RID: 5668 RVA: 0x000BB0DA File Offset: 0x000B92DA
	// (set) Token: 0x06001625 RID: 5669 RVA: 0x000BB0FA File Offset: 0x000B92FA
	public static int MemorialStudent4
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_MemorialStudent4");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_MemorialStudent4", value);
		}
	}

	// Token: 0x1700040E RID: 1038
	// (get) Token: 0x06001626 RID: 5670 RVA: 0x000BB11B File Offset: 0x000B931B
	// (set) Token: 0x06001627 RID: 5671 RVA: 0x000BB13B File Offset: 0x000B933B
	public static int MemorialStudent5
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_MemorialStudent5");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_MemorialStudent5", value);
		}
	}

	// Token: 0x1700040F RID: 1039
	// (get) Token: 0x06001628 RID: 5672 RVA: 0x000BB15C File Offset: 0x000B935C
	// (set) Token: 0x06001629 RID: 5673 RVA: 0x000BB17C File Offset: 0x000B937C
	public static int MemorialStudent6
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_MemorialStudent6");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_MemorialStudent6", value);
		}
	}

	// Token: 0x17000410 RID: 1040
	// (get) Token: 0x0600162A RID: 5674 RVA: 0x000BB19D File Offset: 0x000B939D
	// (set) Token: 0x0600162B RID: 5675 RVA: 0x000BB1BD File Offset: 0x000B93BD
	public static int MemorialStudent7
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_MemorialStudent7");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_MemorialStudent7", value);
		}
	}

	// Token: 0x17000411 RID: 1041
	// (get) Token: 0x0600162C RID: 5676 RVA: 0x000BB1DE File Offset: 0x000B93DE
	// (set) Token: 0x0600162D RID: 5677 RVA: 0x000BB1FE File Offset: 0x000B93FE
	public static int MemorialStudent8
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_MemorialStudent8");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_MemorialStudent8", value);
		}
	}

	// Token: 0x17000412 RID: 1042
	// (get) Token: 0x0600162E RID: 5678 RVA: 0x000BB21F File Offset: 0x000B941F
	// (set) Token: 0x0600162F RID: 5679 RVA: 0x000BB23F File Offset: 0x000B943F
	public static int MemorialStudent9
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_MemorialStudent9");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_MemorialStudent9", value);
		}
	}

	// Token: 0x06001630 RID: 5680 RVA: 0x000BB260 File Offset: 0x000B9460
	public static string GetStudentAccessory(int studentID)
	{
		return PlayerPrefs.GetString(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentAccessory_",
			studentID.ToString()
		}));
	}

	// Token: 0x06001631 RID: 5681 RVA: 0x000BB29C File Offset: 0x000B949C
	public static void SetStudentAccessory(int studentID, string value)
	{
		string text = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_StudentAccessory_", text);
		PlayerPrefs.SetString(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentAccessory_",
			text
		}), value);
	}

	// Token: 0x06001632 RID: 5682 RVA: 0x000BB302 File Offset: 0x000B9502
	public static int[] KeysOfStudentAccessory()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_StudentAccessory_");
	}

	// Token: 0x06001633 RID: 5683 RVA: 0x000BB322 File Offset: 0x000B9522
	public static bool GetStudentArrested(int studentID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentArrested_",
			studentID.ToString()
		}));
	}

	// Token: 0x06001634 RID: 5684 RVA: 0x000BB35C File Offset: 0x000B955C
	public static void SetStudentArrested(int studentID, bool value)
	{
		string text = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_StudentArrested_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentArrested_",
			text
		}), value);
	}

	// Token: 0x06001635 RID: 5685 RVA: 0x000BB3C2 File Offset: 0x000B95C2
	public static int[] KeysOfStudentArrested()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_StudentArrested_");
	}

	// Token: 0x06001636 RID: 5686 RVA: 0x000BB3E2 File Offset: 0x000B95E2
	public static bool GetStudentBroken(int studentID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentBroken_",
			studentID.ToString()
		}));
	}

	// Token: 0x06001637 RID: 5687 RVA: 0x000BB41C File Offset: 0x000B961C
	public static void SetStudentBroken(int studentID, bool value)
	{
		string text = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_StudentBroken_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentBroken_",
			text
		}), value);
	}

	// Token: 0x06001638 RID: 5688 RVA: 0x000BB482 File Offset: 0x000B9682
	public static int[] KeysOfStudentBroken()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_StudentBroken_");
	}

	// Token: 0x06001639 RID: 5689 RVA: 0x000BB4A2 File Offset: 0x000B96A2
	public static float GetStudentBustSize(int studentID)
	{
		return PlayerPrefs.GetFloat(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentBustSize_",
			studentID.ToString()
		}));
	}

	// Token: 0x0600163A RID: 5690 RVA: 0x000BB4DC File Offset: 0x000B96DC
	public static void SetStudentBustSize(int studentID, float value)
	{
		string text = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_StudentBustSize_", text);
		PlayerPrefs.SetFloat(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentBustSize_",
			text
		}), value);
	}

	// Token: 0x0600163B RID: 5691 RVA: 0x000BB542 File Offset: 0x000B9742
	public static int[] KeysOfStudentBustSize()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_StudentBustSize_");
	}

	// Token: 0x0600163C RID: 5692 RVA: 0x000BB562 File Offset: 0x000B9762
	public static Color GetStudentColor(int studentID)
	{
		return GlobalsHelper.GetColor(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentColor_",
			studentID.ToString()
		}));
	}

	// Token: 0x0600163D RID: 5693 RVA: 0x000BB59C File Offset: 0x000B979C
	public static void SetStudentColor(int studentID, Color value)
	{
		string text = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_StudentColor_", text);
		GlobalsHelper.SetColor(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentColor_",
			text
		}), value);
	}

	// Token: 0x0600163E RID: 5694 RVA: 0x000BB602 File Offset: 0x000B9802
	public static int[] KeysOfStudentColor()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_StudentColor_");
	}

	// Token: 0x0600163F RID: 5695 RVA: 0x000BB622 File Offset: 0x000B9822
	public static bool GetStudentDead(int studentID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentDead_",
			studentID.ToString()
		}));
	}

	// Token: 0x06001640 RID: 5696 RVA: 0x000BB65C File Offset: 0x000B985C
	public static void SetStudentDead(int studentID, bool value)
	{
		string text = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_StudentDead_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentDead_",
			text
		}), value);
	}

	// Token: 0x06001641 RID: 5697 RVA: 0x000BB6C2 File Offset: 0x000B98C2
	public static int[] KeysOfStudentDead()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_StudentDead_");
	}

	// Token: 0x06001642 RID: 5698 RVA: 0x000BB6E2 File Offset: 0x000B98E2
	public static bool GetStudentDying(int studentID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentDying_",
			studentID.ToString()
		}));
	}

	// Token: 0x06001643 RID: 5699 RVA: 0x000BB71C File Offset: 0x000B991C
	public static void SetStudentDying(int studentID, bool value)
	{
		string text = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_StudentDying_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentDying_",
			text
		}), value);
	}

	// Token: 0x06001644 RID: 5700 RVA: 0x000BB782 File Offset: 0x000B9982
	public static int[] KeysOfStudentDying()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_StudentDying_");
	}

	// Token: 0x06001645 RID: 5701 RVA: 0x000BB7A2 File Offset: 0x000B99A2
	public static bool GetStudentExpelled(int studentID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentExpelled_",
			studentID.ToString()
		}));
	}

	// Token: 0x06001646 RID: 5702 RVA: 0x000BB7DC File Offset: 0x000B99DC
	public static void SetStudentExpelled(int studentID, bool value)
	{
		string text = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_StudentExpelled_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentExpelled_",
			text
		}), value);
	}

	// Token: 0x06001647 RID: 5703 RVA: 0x000BB842 File Offset: 0x000B9A42
	public static int[] KeysOfStudentExpelled()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_StudentExpelled_");
	}

	// Token: 0x06001648 RID: 5704 RVA: 0x000BB862 File Offset: 0x000B9A62
	public static bool GetStudentExposed(int studentID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentExposed_",
			studentID.ToString()
		}));
	}

	// Token: 0x06001649 RID: 5705 RVA: 0x000BB89C File Offset: 0x000B9A9C
	public static void SetStudentExposed(int studentID, bool value)
	{
		string text = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_StudentExposed_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentExposed_",
			text
		}), value);
	}

	// Token: 0x0600164A RID: 5706 RVA: 0x000BB902 File Offset: 0x000B9B02
	public static int[] KeysOfStudentExposed()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_StudentExposed_");
	}

	// Token: 0x0600164B RID: 5707 RVA: 0x000BB922 File Offset: 0x000B9B22
	public static Color GetStudentEyeColor(int studentID)
	{
		return GlobalsHelper.GetColor(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentEyeColor_",
			studentID.ToString()
		}));
	}

	// Token: 0x0600164C RID: 5708 RVA: 0x000BB95C File Offset: 0x000B9B5C
	public static void SetStudentEyeColor(int studentID, Color value)
	{
		string text = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_StudentEyeColor_", text);
		GlobalsHelper.SetColor(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentEyeColor_",
			text
		}), value);
	}

	// Token: 0x0600164D RID: 5709 RVA: 0x000BB9C2 File Offset: 0x000B9BC2
	public static int[] KeysOfStudentEyeColor()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_StudentEyeColor_");
	}

	// Token: 0x0600164E RID: 5710 RVA: 0x000BB9E2 File Offset: 0x000B9BE2
	public static bool GetStudentGrudge(int studentID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentGrudge_",
			studentID.ToString()
		}));
	}

	// Token: 0x0600164F RID: 5711 RVA: 0x000BBA1C File Offset: 0x000B9C1C
	public static void SetStudentGrudge(int studentID, bool value)
	{
		string text = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_StudentGrudge_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentGrudge_",
			text
		}), value);
	}

	// Token: 0x06001650 RID: 5712 RVA: 0x000BBA82 File Offset: 0x000B9C82
	public static int[] KeysOfStudentGrudge()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_StudentGrudge_");
	}

	// Token: 0x06001651 RID: 5713 RVA: 0x000BBAA2 File Offset: 0x000B9CA2
	public static string GetStudentHairstyle(int studentID)
	{
		return PlayerPrefs.GetString(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentHairstyle_",
			studentID.ToString()
		}));
	}

	// Token: 0x06001652 RID: 5714 RVA: 0x000BBADC File Offset: 0x000B9CDC
	public static void SetStudentHairstyle(int studentID, string value)
	{
		string text = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_StudentHairstyle_", text);
		PlayerPrefs.SetString(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentHairstyle_",
			text
		}), value);
	}

	// Token: 0x06001653 RID: 5715 RVA: 0x000BBB42 File Offset: 0x000B9D42
	public static int[] KeysOfStudentHairstyle()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_StudentHairstyle_");
	}

	// Token: 0x06001654 RID: 5716 RVA: 0x000BBB62 File Offset: 0x000B9D62
	public static bool GetStudentKidnapped(int studentID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentKidnapped_",
			studentID.ToString()
		}));
	}

	// Token: 0x06001655 RID: 5717 RVA: 0x000BBB9C File Offset: 0x000B9D9C
	public static void SetStudentKidnapped(int studentID, bool value)
	{
		string text = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_StudentKidnapped_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentKidnapped_",
			text
		}), value);
	}

	// Token: 0x06001656 RID: 5718 RVA: 0x000BBC02 File Offset: 0x000B9E02
	public static int[] KeysOfStudentKidnapped()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_StudentKidnapped_");
	}

	// Token: 0x06001657 RID: 5719 RVA: 0x000BBC22 File Offset: 0x000B9E22
	public static bool GetStudentMissing(int studentID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentMissing_",
			studentID.ToString()
		}));
	}

	// Token: 0x06001658 RID: 5720 RVA: 0x000BBC5C File Offset: 0x000B9E5C
	public static void SetStudentMissing(int studentID, bool value)
	{
		string text = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_StudentMissing_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentMissing_",
			text
		}), value);
	}

	// Token: 0x06001659 RID: 5721 RVA: 0x000BBCC2 File Offset: 0x000B9EC2
	public static int[] KeysOfStudentMissing()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_StudentMissing_");
	}

	// Token: 0x0600165A RID: 5722 RVA: 0x000BBCE2 File Offset: 0x000B9EE2
	public static string GetStudentName(int studentID)
	{
		return PlayerPrefs.GetString(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentName_",
			studentID.ToString()
		}));
	}

	// Token: 0x0600165B RID: 5723 RVA: 0x000BBD1C File Offset: 0x000B9F1C
	public static void SetStudentName(int studentID, string value)
	{
		string text = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_StudentName_", text);
		PlayerPrefs.SetString(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentName_",
			text
		}), value);
	}

	// Token: 0x0600165C RID: 5724 RVA: 0x000BBD82 File Offset: 0x000B9F82
	public static int[] KeysOfStudentName()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_StudentName_");
	}

	// Token: 0x0600165D RID: 5725 RVA: 0x000BBDA2 File Offset: 0x000B9FA2
	public static bool GetStudentPhotographed(int studentID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentPhotographed_",
			studentID.ToString()
		}));
	}

	// Token: 0x0600165E RID: 5726 RVA: 0x000BBDDC File Offset: 0x000B9FDC
	public static void SetStudentPhotographed(int studentID, bool value)
	{
		string text = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_StudentPhotographed_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentPhotographed_",
			text
		}), value);
	}

	// Token: 0x0600165F RID: 5727 RVA: 0x000BBE42 File Offset: 0x000BA042
	public static int[] KeysOfStudentPhotographed()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_StudentPhotographed_");
	}

	// Token: 0x06001660 RID: 5728 RVA: 0x000BBE62 File Offset: 0x000BA062
	public static bool GetStudentPhoneStolen(int studentID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentPhoneStolen_",
			studentID.ToString()
		}));
	}

	// Token: 0x06001661 RID: 5729 RVA: 0x000BBE9C File Offset: 0x000BA09C
	public static void SetStudentPhoneStolen(int studentID, bool value)
	{
		string text = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_StudentPhoneStolen_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentPhoneStolen_",
			text
		}), value);
	}

	// Token: 0x06001662 RID: 5730 RVA: 0x000BBF02 File Offset: 0x000BA102
	public static int[] KeysOfStudentPhoneStolen()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_StudentPhoneStolen_");
	}

	// Token: 0x06001663 RID: 5731 RVA: 0x000BBF22 File Offset: 0x000BA122
	public static bool GetStudentReplaced(int studentID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentReplaced_",
			studentID.ToString()
		}));
	}

	// Token: 0x06001664 RID: 5732 RVA: 0x000BBF5C File Offset: 0x000BA15C
	public static void SetStudentReplaced(int studentID, bool value)
	{
		string text = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_StudentReplaced_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentReplaced_",
			text
		}), value);
	}

	// Token: 0x06001665 RID: 5733 RVA: 0x000BBFC2 File Offset: 0x000BA1C2
	public static int[] KeysOfStudentReplaced()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_StudentReplaced_");
	}

	// Token: 0x06001666 RID: 5734 RVA: 0x000BBFE2 File Offset: 0x000BA1E2
	public static int GetStudentReputation(int studentID)
	{
		return PlayerPrefs.GetInt(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentReputation_",
			studentID.ToString()
		}));
	}

	// Token: 0x06001667 RID: 5735 RVA: 0x000BC01C File Offset: 0x000BA21C
	public static void SetStudentReputation(int studentID, int value)
	{
		string text = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_StudentReputation_", text);
		PlayerPrefs.SetInt(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentReputation_",
			text
		}), value);
	}

	// Token: 0x06001668 RID: 5736 RVA: 0x000BC082 File Offset: 0x000BA282
	public static int[] KeysOfStudentReputation()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_StudentReputation_");
	}

	// Token: 0x06001669 RID: 5737 RVA: 0x000BC0A2 File Offset: 0x000BA2A2
	public static float GetStudentSanity(int studentID)
	{
		return PlayerPrefs.GetFloat(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentSanity_",
			studentID.ToString()
		}));
	}

	// Token: 0x0600166A RID: 5738 RVA: 0x000BC0DC File Offset: 0x000BA2DC
	public static void SetStudentSanity(int studentID, float value)
	{
		string text = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_StudentSanity_", text);
		PlayerPrefs.SetFloat(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentSanity_",
			text
		}), value);
	}

	// Token: 0x0600166B RID: 5739 RVA: 0x000BC142 File Offset: 0x000BA342
	public static int[] KeysOfStudentSanity()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_StudentSanity_");
	}

	// Token: 0x0600166C RID: 5740 RVA: 0x000BC162 File Offset: 0x000BA362
	public static int GetStudentSlave()
	{
		return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_StudentSlave");
	}

	// Token: 0x0600166D RID: 5741 RVA: 0x000BC182 File Offset: 0x000BA382
	public static int GetStudentFragileSlave()
	{
		return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_StudentFragileSlave");
	}

	// Token: 0x0600166E RID: 5742 RVA: 0x000BC1A2 File Offset: 0x000BA3A2
	public static void SetStudentSlave(int studentID)
	{
		PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_StudentSlave", studentID);
	}

	// Token: 0x0600166F RID: 5743 RVA: 0x000BC1C3 File Offset: 0x000BA3C3
	public static void SetStudentFragileSlave(int studentID)
	{
		PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_StudentFragileSlave", studentID);
	}

	// Token: 0x06001670 RID: 5744 RVA: 0x000BC1E4 File Offset: 0x000BA3E4
	public static int[] KeysOfStudentSlave()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_StudentSlave");
	}

	// Token: 0x06001671 RID: 5745 RVA: 0x000BC204 File Offset: 0x000BA404
	public static int GetFragileTarget()
	{
		return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_FragileTarget");
	}

	// Token: 0x06001672 RID: 5746 RVA: 0x000BC224 File Offset: 0x000BA424
	public static void SetFragileTarget(int value)
	{
		PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_FragileTarget", value);
	}

	// Token: 0x06001673 RID: 5747 RVA: 0x000BC245 File Offset: 0x000BA445
	public static Vector3 GetReputationTriangle(int studentID)
	{
		return GlobalsHelper.GetVector3(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_Student_",
			studentID,
			"_ReputatonTriangle"
		}));
	}

	// Token: 0x06001674 RID: 5748 RVA: 0x000BC288 File Offset: 0x000BA488
	public static void SetReputationTriangle(int studentID, Vector3 triangle)
	{
		GlobalsHelper.SetVector3(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_Student_",
			studentID,
			"_ReputatonTriangle"
		}), triangle);
	}

	// Token: 0x06001675 RID: 5749 RVA: 0x000BC2D4 File Offset: 0x000BA4D4
	public static void DeleteAll()
	{
		Globals.Delete("Profile_" + GameGlobals.Profile + "_CustomSuitor");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_CustomSuitorAccessory");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_CustomSuitorBlonde");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_CustomSuitorBlack");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_CustomSuitorEyewear");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_CustomSuitorHair");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_CustomSuitorJewelry");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_CustomSuitorTan");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_ExpelProgress");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_FemaleUniform");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_MaleUniform");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_StudentSlave");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_StudentFragileSlave");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_FragileTarget");
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_StudentAccessory_", StudentGlobals.KeysOfStudentAccessory());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_StudentArrested_", StudentGlobals.KeysOfStudentArrested());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_StudentBroken_", StudentGlobals.KeysOfStudentBroken());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_StudentBustSize_", StudentGlobals.KeysOfStudentBustSize());
		GlobalsHelper.DeleteColorCollection("Profile_" + GameGlobals.Profile + "_StudentColor_", StudentGlobals.KeysOfStudentColor());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_StudentDead_", StudentGlobals.KeysOfStudentDead());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_StudentDying_", StudentGlobals.KeysOfStudentDying());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_StudentExpelled_", StudentGlobals.KeysOfStudentExpelled());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_StudentExposed_", StudentGlobals.KeysOfStudentExposed());
		GlobalsHelper.DeleteColorCollection("Profile_" + GameGlobals.Profile + "_StudentEyeColor_", StudentGlobals.KeysOfStudentEyeColor());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_StudentGrudge_", StudentGlobals.KeysOfStudentGrudge());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_StudentHairstyle_", StudentGlobals.KeysOfStudentHairstyle());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_StudentKidnapped_", StudentGlobals.KeysOfStudentKidnapped());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_StudentMissing_", StudentGlobals.KeysOfStudentMissing());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_StudentName_", StudentGlobals.KeysOfStudentName());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_StudentPhotographed_", StudentGlobals.KeysOfStudentPhotographed());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_StudentPhoneStolen_", StudentGlobals.KeysOfStudentPhoneStolen());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_StudentReplaced_", StudentGlobals.KeysOfStudentReplaced());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_StudentReputation_", StudentGlobals.KeysOfStudentReputation());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_StudentSanity_", StudentGlobals.KeysOfStudentSanity());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_StudentSlave", StudentGlobals.KeysOfStudentSlave());
		Globals.Delete("Profile_" + GameGlobals.Profile + "_MemorialStudents");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_MemorialStudent1");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_MemorialStudent2");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_MemorialStudent3");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_MemorialStudent4");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_MemorialStudent5");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_MemorialStudent6");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_MemorialStudent7");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_MemorialStudent8");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_MemorialStudent9");
	}

	// Token: 0x04001DE1 RID: 7649
	private const string Str_CustomSuitor = "CustomSuitor";

	// Token: 0x04001DE2 RID: 7650
	private const string Str_CustomSuitorAccessory = "CustomSuitorAccessory";

	// Token: 0x04001DE3 RID: 7651
	private const string Str_CustomSuitorBlonde = "CustomSuitorBlonde";

	// Token: 0x04001DE4 RID: 7652
	private const string Str_CustomSuitorBlack = "CustomSuitorBlack";

	// Token: 0x04001DE5 RID: 7653
	private const string Str_CustomSuitorEyewear = "CustomSuitorEyewear";

	// Token: 0x04001DE6 RID: 7654
	private const string Str_CustomSuitorHair = "CustomSuitorHair";

	// Token: 0x04001DE7 RID: 7655
	private const string Str_CustomSuitorJewelry = "CustomSuitorJewelry";

	// Token: 0x04001DE8 RID: 7656
	private const string Str_CustomSuitorTan = "CustomSuitorTan";

	// Token: 0x04001DE9 RID: 7657
	private const string Str_ExpelProgress = "ExpelProgress";

	// Token: 0x04001DEA RID: 7658
	private const string Str_FemaleUniform = "FemaleUniform";

	// Token: 0x04001DEB RID: 7659
	private const string Str_MaleUniform = "MaleUniform";

	// Token: 0x04001DEC RID: 7660
	private const string Str_StudentAccessory = "StudentAccessory_";

	// Token: 0x04001DED RID: 7661
	private const string Str_StudentArrested = "StudentArrested_";

	// Token: 0x04001DEE RID: 7662
	private const string Str_StudentBroken = "StudentBroken_";

	// Token: 0x04001DEF RID: 7663
	private const string Str_StudentBustSize = "StudentBustSize_";

	// Token: 0x04001DF0 RID: 7664
	private const string Str_StudentColor = "StudentColor_";

	// Token: 0x04001DF1 RID: 7665
	private const string Str_StudentDead = "StudentDead_";

	// Token: 0x04001DF2 RID: 7666
	private const string Str_StudentDying = "StudentDying_";

	// Token: 0x04001DF3 RID: 7667
	private const string Str_StudentExpelled = "StudentExpelled_";

	// Token: 0x04001DF4 RID: 7668
	private const string Str_StudentExposed = "StudentExposed_";

	// Token: 0x04001DF5 RID: 7669
	private const string Str_StudentEyeColor = "StudentEyeColor_";

	// Token: 0x04001DF6 RID: 7670
	private const string Str_StudentGrudge = "StudentGrudge_";

	// Token: 0x04001DF7 RID: 7671
	private const string Str_StudentHairstyle = "StudentHairstyle_";

	// Token: 0x04001DF8 RID: 7672
	private const string Str_StudentKidnapped = "StudentKidnapped_";

	// Token: 0x04001DF9 RID: 7673
	private const string Str_StudentMissing = "StudentMissing_";

	// Token: 0x04001DFA RID: 7674
	private const string Str_StudentName = "StudentName_";

	// Token: 0x04001DFB RID: 7675
	private const string Str_StudentPhotographed = "StudentPhotographed_";

	// Token: 0x04001DFC RID: 7676
	private const string Str_StudentPhoneStolen = "StudentPhoneStolen_";

	// Token: 0x04001DFD RID: 7677
	private const string Str_StudentReplaced = "StudentReplaced_";

	// Token: 0x04001DFE RID: 7678
	private const string Str_StudentReputation = "StudentReputation_";

	// Token: 0x04001DFF RID: 7679
	private const string Str_StudentSanity = "StudentSanity_";

	// Token: 0x04001E00 RID: 7680
	private const string Str_StudentSlave = "StudentSlave";

	// Token: 0x04001E01 RID: 7681
	private const string Str_StudentFragileSlave = "StudentFragileSlave";

	// Token: 0x04001E02 RID: 7682
	private const string Str_FragileTarget = "FragileTarget";

	// Token: 0x04001E03 RID: 7683
	private const string Str_ReputationTriangle = "ReputatonTriangle";

	// Token: 0x04001E04 RID: 7684
	private const string Str_MemorialStudents = "MemorialStudents";

	// Token: 0x04001E05 RID: 7685
	private const string Str_MemorialStudent1 = "MemorialStudent1";

	// Token: 0x04001E06 RID: 7686
	private const string Str_MemorialStudent2 = "MemorialStudent2";

	// Token: 0x04001E07 RID: 7687
	private const string Str_MemorialStudent3 = "MemorialStudent3";

	// Token: 0x04001E08 RID: 7688
	private const string Str_MemorialStudent4 = "MemorialStudent4";

	// Token: 0x04001E09 RID: 7689
	private const string Str_MemorialStudent5 = "MemorialStudent5";

	// Token: 0x04001E0A RID: 7690
	private const string Str_MemorialStudent6 = "MemorialStudent6";

	// Token: 0x04001E0B RID: 7691
	private const string Str_MemorialStudent7 = "MemorialStudent7";

	// Token: 0x04001E0C RID: 7692
	private const string Str_MemorialStudent8 = "MemorialStudent8";

	// Token: 0x04001E0D RID: 7693
	private const string Str_MemorialStudent9 = "MemorialStudent9";
}
