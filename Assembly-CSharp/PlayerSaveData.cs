using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003BD RID: 957
[Serializable]
public class PlayerSaveData
{
	// Token: 0x06001A28 RID: 6696 RVA: 0x00100310 File Offset: 0x000FE510
	public static PlayerSaveData ReadFromGlobals()
	{
		PlayerSaveData playerSaveData = new PlayerSaveData();
		playerSaveData.alerts = PlayerGlobals.Alerts;
		playerSaveData.enlightenment = PlayerGlobals.Enlightenment;
		playerSaveData.enlightenmentBonus = PlayerGlobals.EnlightenmentBonus;
		playerSaveData.headset = PlayerGlobals.Headset;
		playerSaveData.kills = PlayerGlobals.Kills;
		playerSaveData.numbness = PlayerGlobals.Numbness;
		playerSaveData.numbnessBonus = PlayerGlobals.NumbnessBonus;
		playerSaveData.pantiesEquipped = PlayerGlobals.PantiesEquipped;
		playerSaveData.pantyShots = PlayerGlobals.PantyShots;
		foreach (int num in PlayerGlobals.KeysOfPhoto())
		{
			if (PlayerGlobals.GetPhoto(num))
			{
				playerSaveData.photo.Add(num);
			}
		}
		foreach (int num2 in PlayerGlobals.KeysOfPhotoOnCorkboard())
		{
			if (PlayerGlobals.GetPhotoOnCorkboard(num2))
			{
				playerSaveData.photoOnCorkboard.Add(num2);
			}
		}
		foreach (int num3 in PlayerGlobals.KeysOfPhotoPosition())
		{
			playerSaveData.photoPosition.Add(num3, PlayerGlobals.GetPhotoPosition(num3));
		}
		foreach (int num4 in PlayerGlobals.KeysOfPhotoRotation())
		{
			playerSaveData.photoRotation.Add(num4, PlayerGlobals.GetPhotoRotation(num4));
		}
		playerSaveData.reputation = PlayerGlobals.Reputation;
		playerSaveData.seduction = PlayerGlobals.Seduction;
		playerSaveData.seductionBonus = PlayerGlobals.SeductionBonus;
		foreach (int num5 in PlayerGlobals.KeysOfSenpaiPhoto())
		{
			if (PlayerGlobals.GetSenpaiPhoto(num5))
			{
				playerSaveData.senpaiPhoto.Add(num5);
			}
		}
		playerSaveData.senpaiShots = PlayerGlobals.SenpaiShots;
		playerSaveData.socialBonus = PlayerGlobals.SocialBonus;
		playerSaveData.speedBonus = PlayerGlobals.SpeedBonus;
		playerSaveData.stealthBonus = PlayerGlobals.StealthBonus;
		foreach (int num6 in PlayerGlobals.KeysOfStudentFriend())
		{
			if (PlayerGlobals.GetStudentFriend(num6))
			{
				playerSaveData.studentFriend.Add(num6);
			}
		}
		foreach (string text in PlayerGlobals.KeysOfStudentPantyShot())
		{
			if (PlayerGlobals.GetStudentPantyShot(text))
			{
				playerSaveData.studentPantyShot.Add(text);
			}
		}
		return playerSaveData;
	}

	// Token: 0x06001A29 RID: 6697 RVA: 0x00100520 File Offset: 0x000FE720
	public static void WriteToGlobals(PlayerSaveData data)
	{
		PlayerGlobals.Alerts = data.alerts;
		PlayerGlobals.Enlightenment = data.enlightenment;
		PlayerGlobals.EnlightenmentBonus = data.enlightenmentBonus;
		PlayerGlobals.Headset = data.headset;
		PlayerGlobals.Kills = data.kills;
		PlayerGlobals.Numbness = data.numbness;
		PlayerGlobals.NumbnessBonus = data.numbnessBonus;
		PlayerGlobals.PantiesEquipped = data.pantiesEquipped;
		PlayerGlobals.PantyShots = data.pantyShots;
		Debug.Log("Is this being called anywhere?");
		foreach (int photoID in data.photo)
		{
			PlayerGlobals.SetPhoto(photoID, true);
		}
		foreach (int photoID2 in data.photoOnCorkboard)
		{
			PlayerGlobals.SetPhotoOnCorkboard(photoID2, true);
		}
		foreach (KeyValuePair<int, Vector2> keyValuePair in data.photoPosition)
		{
			PlayerGlobals.SetPhotoPosition(keyValuePair.Key, keyValuePair.Value);
		}
		foreach (KeyValuePair<int, float> keyValuePair2 in data.photoRotation)
		{
			PlayerGlobals.SetPhotoRotation(keyValuePair2.Key, keyValuePair2.Value);
		}
		PlayerGlobals.Reputation = data.reputation;
		PlayerGlobals.Seduction = data.seduction;
		PlayerGlobals.SeductionBonus = data.seductionBonus;
		foreach (int photoID3 in data.senpaiPhoto)
		{
			PlayerGlobals.SetSenpaiPhoto(photoID3, true);
		}
		PlayerGlobals.SenpaiShots = data.senpaiShots;
		PlayerGlobals.SocialBonus = data.socialBonus;
		PlayerGlobals.SpeedBonus = data.speedBonus;
		PlayerGlobals.StealthBonus = data.stealthBonus;
		foreach (int studentID in data.studentFriend)
		{
			PlayerGlobals.SetStudentFriend(studentID, true);
		}
		foreach (string studentName in data.studentPantyShot)
		{
			PlayerGlobals.SetStudentPantyShot(studentName, true);
		}
	}

	// Token: 0x04002921 RID: 10529
	public int alerts;

	// Token: 0x04002922 RID: 10530
	public int enlightenment;

	// Token: 0x04002923 RID: 10531
	public int enlightenmentBonus;

	// Token: 0x04002924 RID: 10532
	public bool headset;

	// Token: 0x04002925 RID: 10533
	public int kills;

	// Token: 0x04002926 RID: 10534
	public int numbness;

	// Token: 0x04002927 RID: 10535
	public int numbnessBonus;

	// Token: 0x04002928 RID: 10536
	public int pantiesEquipped;

	// Token: 0x04002929 RID: 10537
	public int pantyShots;

	// Token: 0x0400292A RID: 10538
	public IntHashSet photo = new IntHashSet();

	// Token: 0x0400292B RID: 10539
	public IntHashSet photoOnCorkboard = new IntHashSet();

	// Token: 0x0400292C RID: 10540
	public IntAndVector2Dictionary photoPosition = new IntAndVector2Dictionary();

	// Token: 0x0400292D RID: 10541
	public IntAndFloatDictionary photoRotation = new IntAndFloatDictionary();

	// Token: 0x0400292E RID: 10542
	public float reputation;

	// Token: 0x0400292F RID: 10543
	public int seduction;

	// Token: 0x04002930 RID: 10544
	public int seductionBonus;

	// Token: 0x04002931 RID: 10545
	public IntHashSet senpaiPhoto = new IntHashSet();

	// Token: 0x04002932 RID: 10546
	public int senpaiShots;

	// Token: 0x04002933 RID: 10547
	public int socialBonus;

	// Token: 0x04002934 RID: 10548
	public int speedBonus;

	// Token: 0x04002935 RID: 10549
	public int stealthBonus;

	// Token: 0x04002936 RID: 10550
	public IntHashSet studentFriend = new IntHashSet();

	// Token: 0x04002937 RID: 10551
	public StringHashSet studentPantyShot = new StringHashSet();
}
