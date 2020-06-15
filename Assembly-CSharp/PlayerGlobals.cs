using System;
using UnityEngine;

// Token: 0x020002C9 RID: 713
public static class PlayerGlobals
{
	// Token: 0x170003D3 RID: 979
	// (get) Token: 0x0600157B RID: 5499 RVA: 0x000B8F33 File Offset: 0x000B7133
	// (set) Token: 0x0600157C RID: 5500 RVA: 0x000B8F53 File Offset: 0x000B7153
	public static float Money
	{
		get
		{
			return PlayerPrefs.GetFloat("Profile_" + GameGlobals.Profile + "_Money");
		}
		set
		{
			PlayerPrefs.SetFloat("Profile_" + GameGlobals.Profile + "_Money", value);
		}
	}

	// Token: 0x170003D4 RID: 980
	// (get) Token: 0x0600157D RID: 5501 RVA: 0x000B8F74 File Offset: 0x000B7174
	// (set) Token: 0x0600157E RID: 5502 RVA: 0x000B8F94 File Offset: 0x000B7194
	public static int Alerts
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_Alerts");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_Alerts", value);
		}
	}

	// Token: 0x170003D5 RID: 981
	// (get) Token: 0x0600157F RID: 5503 RVA: 0x000B8FB5 File Offset: 0x000B71B5
	// (set) Token: 0x06001580 RID: 5504 RVA: 0x000B8FD5 File Offset: 0x000B71D5
	public static int Enlightenment
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_Enlightenment");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_Enlightenment", value);
		}
	}

	// Token: 0x170003D6 RID: 982
	// (get) Token: 0x06001581 RID: 5505 RVA: 0x000B8FF6 File Offset: 0x000B71F6
	// (set) Token: 0x06001582 RID: 5506 RVA: 0x000B9016 File Offset: 0x000B7216
	public static int EnlightenmentBonus
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_EnlightenmentBonus");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_EnlightenmentBonus", value);
		}
	}

	// Token: 0x170003D7 RID: 983
	// (get) Token: 0x06001583 RID: 5507 RVA: 0x000B9037 File Offset: 0x000B7237
	// (set) Token: 0x06001584 RID: 5508 RVA: 0x000B9057 File Offset: 0x000B7257
	public static int Friends
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_Friends");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_Friends", value);
		}
	}

	// Token: 0x170003D8 RID: 984
	// (get) Token: 0x06001585 RID: 5509 RVA: 0x000B9078 File Offset: 0x000B7278
	// (set) Token: 0x06001586 RID: 5510 RVA: 0x000B9098 File Offset: 0x000B7298
	public static bool Headset
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_Headset");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_Headset", value);
		}
	}

	// Token: 0x170003D9 RID: 985
	// (get) Token: 0x06001587 RID: 5511 RVA: 0x000B90B9 File Offset: 0x000B72B9
	// (set) Token: 0x06001588 RID: 5512 RVA: 0x000B90D9 File Offset: 0x000B72D9
	public static bool FakeID
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_FakeID");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_FakeID", value);
		}
	}

	// Token: 0x170003DA RID: 986
	// (get) Token: 0x06001589 RID: 5513 RVA: 0x000B90FA File Offset: 0x000B72FA
	// (set) Token: 0x0600158A RID: 5514 RVA: 0x000B911A File Offset: 0x000B731A
	public static bool RaibaruLoner
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_RaibaruLoner");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_RaibaruLoner", value);
		}
	}

	// Token: 0x170003DB RID: 987
	// (get) Token: 0x0600158B RID: 5515 RVA: 0x000B913B File Offset: 0x000B733B
	// (set) Token: 0x0600158C RID: 5516 RVA: 0x000B915B File Offset: 0x000B735B
	public static int Kills
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_Kills");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_Kills", value);
		}
	}

	// Token: 0x170003DC RID: 988
	// (get) Token: 0x0600158D RID: 5517 RVA: 0x000B917C File Offset: 0x000B737C
	// (set) Token: 0x0600158E RID: 5518 RVA: 0x000B919C File Offset: 0x000B739C
	public static int Numbness
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_Numbness");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_Numbness", value);
		}
	}

	// Token: 0x170003DD RID: 989
	// (get) Token: 0x0600158F RID: 5519 RVA: 0x000B91BD File Offset: 0x000B73BD
	// (set) Token: 0x06001590 RID: 5520 RVA: 0x000B91DD File Offset: 0x000B73DD
	public static int NumbnessBonus
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_NumbnessBonus");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_NumbnessBonus", value);
		}
	}

	// Token: 0x170003DE RID: 990
	// (get) Token: 0x06001591 RID: 5521 RVA: 0x000B91FE File Offset: 0x000B73FE
	// (set) Token: 0x06001592 RID: 5522 RVA: 0x000B921E File Offset: 0x000B741E
	public static int PantiesEquipped
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_PantiesEquipped");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_PantiesEquipped", value);
		}
	}

	// Token: 0x170003DF RID: 991
	// (get) Token: 0x06001593 RID: 5523 RVA: 0x000B923F File Offset: 0x000B743F
	// (set) Token: 0x06001594 RID: 5524 RVA: 0x000B925F File Offset: 0x000B745F
	public static int PantyShots
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_PantyShots");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_PantyShots", value);
		}
	}

	// Token: 0x06001595 RID: 5525 RVA: 0x000B9280 File Offset: 0x000B7480
	public static bool GetPhoto(int photoID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_Photo_",
			photoID.ToString()
		}));
	}

	// Token: 0x06001596 RID: 5526 RVA: 0x000B92BC File Offset: 0x000B74BC
	public static void SetPhoto(int photoID, bool value)
	{
		string text = photoID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_Photo_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_Photo_",
			text
		}), value);
	}

	// Token: 0x06001597 RID: 5527 RVA: 0x000B9322 File Offset: 0x000B7522
	public static int[] KeysOfPhoto()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_Photo_");
	}

	// Token: 0x06001598 RID: 5528 RVA: 0x000B9342 File Offset: 0x000B7542
	public static bool GetPhotoOnCorkboard(int photoID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_PhotoOnCorkboard_",
			photoID.ToString()
		}));
	}

	// Token: 0x06001599 RID: 5529 RVA: 0x000B937C File Offset: 0x000B757C
	public static void SetPhotoOnCorkboard(int photoID, bool value)
	{
		string text = photoID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_PhotoOnCorkboard_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_PhotoOnCorkboard_",
			text
		}), value);
	}

	// Token: 0x0600159A RID: 5530 RVA: 0x000B93E2 File Offset: 0x000B75E2
	public static int[] KeysOfPhotoOnCorkboard()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_PhotoOnCorkboard_");
	}

	// Token: 0x0600159B RID: 5531 RVA: 0x000B9402 File Offset: 0x000B7602
	public static Vector2 GetPhotoPosition(int photoID)
	{
		return GlobalsHelper.GetVector2(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_PhotoPosition_",
			photoID.ToString()
		}));
	}

	// Token: 0x0600159C RID: 5532 RVA: 0x000B943C File Offset: 0x000B763C
	public static void SetPhotoPosition(int photoID, Vector2 value)
	{
		string text = photoID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_PhotoPosition_", text);
		GlobalsHelper.SetVector2(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_PhotoPosition_",
			text
		}), value);
	}

	// Token: 0x0600159D RID: 5533 RVA: 0x000B94A2 File Offset: 0x000B76A2
	public static int[] KeysOfPhotoPosition()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_PhotoPosition_");
	}

	// Token: 0x0600159E RID: 5534 RVA: 0x000B94C2 File Offset: 0x000B76C2
	public static float GetPhotoRotation(int photoID)
	{
		return PlayerPrefs.GetFloat(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_PhotoRotation_",
			photoID.ToString()
		}));
	}

	// Token: 0x0600159F RID: 5535 RVA: 0x000B94FC File Offset: 0x000B76FC
	public static void SetPhotoRotation(int photoID, float value)
	{
		string text = photoID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_PhotoRotation_", text);
		PlayerPrefs.SetFloat(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_PhotoRotation_",
			text
		}), value);
	}

	// Token: 0x060015A0 RID: 5536 RVA: 0x000B9562 File Offset: 0x000B7762
	public static int[] KeysOfPhotoRotation()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_PhotoRotation_");
	}

	// Token: 0x170003E0 RID: 992
	// (get) Token: 0x060015A1 RID: 5537 RVA: 0x000B9582 File Offset: 0x000B7782
	// (set) Token: 0x060015A2 RID: 5538 RVA: 0x000B95A2 File Offset: 0x000B77A2
	public static float Reputation
	{
		get
		{
			return PlayerPrefs.GetFloat("Profile_" + GameGlobals.Profile + "_Reputation");
		}
		set
		{
			PlayerPrefs.SetFloat("Profile_" + GameGlobals.Profile + "_Reputation", value);
		}
	}

	// Token: 0x170003E1 RID: 993
	// (get) Token: 0x060015A3 RID: 5539 RVA: 0x000B95C3 File Offset: 0x000B77C3
	// (set) Token: 0x060015A4 RID: 5540 RVA: 0x000B95E3 File Offset: 0x000B77E3
	public static int Seduction
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_Seduction");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_Seduction", value);
		}
	}

	// Token: 0x170003E2 RID: 994
	// (get) Token: 0x060015A5 RID: 5541 RVA: 0x000B9604 File Offset: 0x000B7804
	// (set) Token: 0x060015A6 RID: 5542 RVA: 0x000B9624 File Offset: 0x000B7824
	public static int SeductionBonus
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_SeductionBonus");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_SeductionBonus", value);
		}
	}

	// Token: 0x060015A7 RID: 5543 RVA: 0x000B9645 File Offset: 0x000B7845
	public static bool GetSenpaiPhoto(int photoID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_SenpaiPhoto_",
			photoID.ToString()
		}));
	}

	// Token: 0x060015A8 RID: 5544 RVA: 0x000B9680 File Offset: 0x000B7880
	public static void SetSenpaiPhoto(int photoID, bool value)
	{
		string text = photoID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_SenpaiPhoto_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_SenpaiPhoto_",
			text
		}), value);
	}

	// Token: 0x060015A9 RID: 5545 RVA: 0x000B96E6 File Offset: 0x000B78E6
	public static int GetBullyPhoto(int photoID)
	{
		return PlayerPrefs.GetInt(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_BullyPhoto_",
			photoID.ToString()
		}));
	}

	// Token: 0x060015AA RID: 5546 RVA: 0x000B971F File Offset: 0x000B791F
	public static void SetBullyPhoto(int photoID, int value)
	{
		PlayerPrefs.SetInt(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_BullyPhoto_",
			photoID.ToString()
		}), value);
	}

	// Token: 0x060015AB RID: 5547 RVA: 0x000B9759 File Offset: 0x000B7959
	public static int[] KeysOfSenpaiPhoto()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_SenpaiPhoto_");
	}

	// Token: 0x170003E3 RID: 995
	// (get) Token: 0x060015AC RID: 5548 RVA: 0x000B9779 File Offset: 0x000B7979
	// (set) Token: 0x060015AD RID: 5549 RVA: 0x000B9799 File Offset: 0x000B7999
	public static int SenpaiShots
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_SenpaiShots");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_SenpaiShots", value);
		}
	}

	// Token: 0x170003E4 RID: 996
	// (get) Token: 0x060015AE RID: 5550 RVA: 0x000B97BA File Offset: 0x000B79BA
	// (set) Token: 0x060015AF RID: 5551 RVA: 0x000B97DA File Offset: 0x000B79DA
	public static int SocialBonus
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_SocialBonus");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_SocialBonus", value);
		}
	}

	// Token: 0x170003E5 RID: 997
	// (get) Token: 0x060015B0 RID: 5552 RVA: 0x000B97FB File Offset: 0x000B79FB
	// (set) Token: 0x060015B1 RID: 5553 RVA: 0x000B981B File Offset: 0x000B7A1B
	public static int SpeedBonus
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_SpeedBonus");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_SpeedBonus", value);
		}
	}

	// Token: 0x170003E6 RID: 998
	// (get) Token: 0x060015B2 RID: 5554 RVA: 0x000B983C File Offset: 0x000B7A3C
	// (set) Token: 0x060015B3 RID: 5555 RVA: 0x000B985C File Offset: 0x000B7A5C
	public static int StealthBonus
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_StealthBonus");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_StealthBonus", value);
		}
	}

	// Token: 0x060015B4 RID: 5556 RVA: 0x000B987D File Offset: 0x000B7A7D
	public static bool GetStudentFriend(int studentID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentFriend_",
			studentID.ToString()
		}));
	}

	// Token: 0x060015B5 RID: 5557 RVA: 0x000B98B8 File Offset: 0x000B7AB8
	public static void SetStudentFriend(int studentID, bool value)
	{
		string text = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_StudentFriend_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentFriend_",
			text
		}), value);
	}

	// Token: 0x060015B6 RID: 5558 RVA: 0x000B991E File Offset: 0x000B7B1E
	public static int[] KeysOfStudentFriend()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_StudentFriend_");
	}

	// Token: 0x060015B7 RID: 5559 RVA: 0x000B993E File Offset: 0x000B7B3E
	public static bool GetStudentPantyShot(string studentName)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentPantyShot_",
			studentName
		}));
	}

	// Token: 0x060015B8 RID: 5560 RVA: 0x000B9974 File Offset: 0x000B7B74
	public static void SetStudentPantyShot(string studentName, bool value)
	{
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_StudentPantyShot_", studentName);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentPantyShot_",
			studentName
		}), value);
	}

	// Token: 0x060015B9 RID: 5561 RVA: 0x000B99D2 File Offset: 0x000B7BD2
	public static string[] KeysOfStudentPantyShot()
	{
		return KeysHelper.GetStringKeys("Profile_" + GameGlobals.Profile + "_StudentPantyShot_");
	}

	// Token: 0x060015BA RID: 5562 RVA: 0x000B99F2 File Offset: 0x000B7BF2
	public static string[] KeysOfShrineCollectible()
	{
		return KeysHelper.GetStringKeys("Profile_" + GameGlobals.Profile + "_ShrineCollectible");
	}

	// Token: 0x060015BB RID: 5563 RVA: 0x000B9A12 File Offset: 0x000B7C12
	public static bool GetShrineCollectible(int ID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_ShrineCollectible",
			ID.ToString()
		}));
	}

	// Token: 0x060015BC RID: 5564 RVA: 0x000B9A4C File Offset: 0x000B7C4C
	public static void SetShrineCollectible(int ID, bool value)
	{
		string text = ID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_ShrineCollectible", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_ShrineCollectible",
			text
		}), value);
	}

	// Token: 0x170003E7 RID: 999
	// (get) Token: 0x060015BD RID: 5565 RVA: 0x000B9AB2 File Offset: 0x000B7CB2
	// (set) Token: 0x060015BE RID: 5566 RVA: 0x000B9AD2 File Offset: 0x000B7CD2
	public static bool UsingGamepad
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_UsingGamepad");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_UsingGamepad", value);
		}
	}

	// Token: 0x060015BF RID: 5567 RVA: 0x000B9AF4 File Offset: 0x000B7CF4
	public static void DeleteAll()
	{
		Globals.Delete("Profile_" + GameGlobals.Profile + "_Money");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_Alerts");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_Enlightenment");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_EnlightenmentBonus");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_Friends");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_Headset");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_FakeID");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_RaibaruLoner");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_Kills");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_Numbness");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_NumbnessBonus");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_PantiesEquipped");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_PantyShots");
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_Photo_", PlayerGlobals.KeysOfPhoto());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_PhotoOnCorkboard_", PlayerGlobals.KeysOfPhotoOnCorkboard());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_PhotoPosition_", PlayerGlobals.KeysOfPhotoPosition());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_PhotoRotation_", PlayerGlobals.KeysOfPhotoRotation());
		Globals.Delete("Profile_" + GameGlobals.Profile + "_Reputation");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_Seduction");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_SeductionBonus");
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_SenpaiPhoto_", PlayerGlobals.KeysOfSenpaiPhoto());
		Globals.Delete("Profile_" + GameGlobals.Profile + "_SenpaiShots");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_SocialBonus");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_SpeedBonus");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_StealthBonus");
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_StudentFriend_", PlayerGlobals.KeysOfStudentFriend());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_StudentPantyShot_", PlayerGlobals.KeysOfStudentPantyShot());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_ShrineCollectible", PlayerGlobals.KeysOfShrineCollectible());
	}

	// Token: 0x04001DA6 RID: 7590
	private const string Str_Money = "Money";

	// Token: 0x04001DA7 RID: 7591
	private const string Str_Alerts = "Alerts";

	// Token: 0x04001DA8 RID: 7592
	private const string Str_BullyPhoto = "BullyPhoto_";

	// Token: 0x04001DA9 RID: 7593
	private const string Str_Enlightenment = "Enlightenment";

	// Token: 0x04001DAA RID: 7594
	private const string Str_EnlightenmentBonus = "EnlightenmentBonus";

	// Token: 0x04001DAB RID: 7595
	private const string Str_Friends = "Friends";

	// Token: 0x04001DAC RID: 7596
	private const string Str_Headset = "Headset";

	// Token: 0x04001DAD RID: 7597
	private const string Str_FakeID = "FakeID";

	// Token: 0x04001DAE RID: 7598
	private const string Str_RaibaruLoner = "RaibaruLoner";

	// Token: 0x04001DAF RID: 7599
	private const string Str_Kills = "Kills";

	// Token: 0x04001DB0 RID: 7600
	private const string Str_Numbness = "Numbness";

	// Token: 0x04001DB1 RID: 7601
	private const string Str_NumbnessBonus = "NumbnessBonus";

	// Token: 0x04001DB2 RID: 7602
	private const string Str_PantiesEquipped = "PantiesEquipped";

	// Token: 0x04001DB3 RID: 7603
	private const string Str_PantyShots = "PantyShots";

	// Token: 0x04001DB4 RID: 7604
	private const string Str_Photo = "Photo_";

	// Token: 0x04001DB5 RID: 7605
	private const string Str_PhotoOnCorkboard = "PhotoOnCorkboard_";

	// Token: 0x04001DB6 RID: 7606
	private const string Str_PhotoPosition = "PhotoPosition_";

	// Token: 0x04001DB7 RID: 7607
	private const string Str_PhotoRotation = "PhotoRotation_";

	// Token: 0x04001DB8 RID: 7608
	private const string Str_Reputation = "Reputation";

	// Token: 0x04001DB9 RID: 7609
	private const string Str_Seduction = "Seduction";

	// Token: 0x04001DBA RID: 7610
	private const string Str_SeductionBonus = "SeductionBonus";

	// Token: 0x04001DBB RID: 7611
	private const string Str_SenpaiPhoto = "SenpaiPhoto_";

	// Token: 0x04001DBC RID: 7612
	private const string Str_SenpaiShots = "SenpaiShots";

	// Token: 0x04001DBD RID: 7613
	private const string Str_SocialBonus = "SocialBonus";

	// Token: 0x04001DBE RID: 7614
	private const string Str_SpeedBonus = "SpeedBonus";

	// Token: 0x04001DBF RID: 7615
	private const string Str_StealthBonus = "StealthBonus";

	// Token: 0x04001DC0 RID: 7616
	private const string Str_StudentFriend = "StudentFriend_";

	// Token: 0x04001DC1 RID: 7617
	private const string Str_StudentPantyShot = "StudentPantyShot_";

	// Token: 0x04001DC2 RID: 7618
	private const string Str_ShrineCollectible = "ShrineCollectible";

	// Token: 0x04001DC3 RID: 7619
	private const string Str_UsingGamepad = "UsingGamepad";
}
