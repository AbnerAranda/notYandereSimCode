﻿using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003C3 RID: 963
[Serializable]
public class StudentSaveData
{
	// Token: 0x06001A3A RID: 6714 RVA: 0x00100DF0 File Offset: 0x000FEFF0
	public static StudentSaveData ReadFromGlobals()
	{
		StudentSaveData studentSaveData = new StudentSaveData();
		studentSaveData.customSuitor = StudentGlobals.CustomSuitor;
		studentSaveData.customSuitorAccessory = StudentGlobals.CustomSuitorAccessory;
		studentSaveData.customSuitorBlonde = StudentGlobals.CustomSuitorBlonde;
		studentSaveData.customSuitorEyewear = StudentGlobals.CustomSuitorEyewear;
		studentSaveData.customSuitorHair = StudentGlobals.CustomSuitorHair;
		studentSaveData.customSuitorJewelry = StudentGlobals.CustomSuitorJewelry;
		studentSaveData.customSuitorTan = StudentGlobals.CustomSuitorTan;
		studentSaveData.expelProgress = StudentGlobals.ExpelProgress;
		studentSaveData.femaleUniform = StudentGlobals.FemaleUniform;
		studentSaveData.maleUniform = StudentGlobals.MaleUniform;
		foreach (int num in StudentGlobals.KeysOfStudentAccessory())
		{
			studentSaveData.studentAccessory.Add(num, StudentGlobals.GetStudentAccessory(num));
		}
		foreach (int num2 in StudentGlobals.KeysOfStudentArrested())
		{
			if (StudentGlobals.GetStudentArrested(num2))
			{
				studentSaveData.studentArrested.Add(num2);
			}
		}
		foreach (int num3 in StudentGlobals.KeysOfStudentBroken())
		{
			if (StudentGlobals.GetStudentBroken(num3))
			{
				studentSaveData.studentBroken.Add(num3);
			}
		}
		foreach (int num4 in StudentGlobals.KeysOfStudentBustSize())
		{
			studentSaveData.studentBustSize.Add(num4, StudentGlobals.GetStudentBustSize(num4));
		}
		foreach (int num5 in StudentGlobals.KeysOfStudentColor())
		{
			studentSaveData.studentColor.Add(num5, StudentGlobals.GetStudentColor(num5));
		}
		foreach (int num6 in StudentGlobals.KeysOfStudentDead())
		{
			if (StudentGlobals.GetStudentDead(num6))
			{
				studentSaveData.studentDead.Add(num6);
			}
		}
		foreach (int num7 in StudentGlobals.KeysOfStudentDying())
		{
			if (StudentGlobals.GetStudentDying(num7))
			{
				studentSaveData.studentDying.Add(num7);
			}
		}
		foreach (int num8 in StudentGlobals.KeysOfStudentExpelled())
		{
			if (StudentGlobals.GetStudentExpelled(num8))
			{
				studentSaveData.studentExpelled.Add(num8);
			}
		}
		foreach (int num9 in StudentGlobals.KeysOfStudentExposed())
		{
			if (StudentGlobals.GetStudentExposed(num9))
			{
				studentSaveData.studentExposed.Add(num9);
			}
		}
		foreach (int num10 in StudentGlobals.KeysOfStudentEyeColor())
		{
			studentSaveData.studentEyeColor.Add(num10, StudentGlobals.GetStudentEyeColor(num10));
		}
		foreach (int num11 in StudentGlobals.KeysOfStudentGrudge())
		{
			if (StudentGlobals.GetStudentGrudge(num11))
			{
				studentSaveData.studentGrudge.Add(num11);
			}
		}
		foreach (int num12 in StudentGlobals.KeysOfStudentHairstyle())
		{
			studentSaveData.studentHairstyle.Add(num12, StudentGlobals.GetStudentHairstyle(num12));
		}
		foreach (int num13 in StudentGlobals.KeysOfStudentKidnapped())
		{
			if (StudentGlobals.GetStudentKidnapped(num13))
			{
				studentSaveData.studentKidnapped.Add(num13);
			}
		}
		foreach (int num14 in StudentGlobals.KeysOfStudentMissing())
		{
			if (StudentGlobals.GetStudentMissing(num14))
			{
				studentSaveData.studentMissing.Add(num14);
			}
		}
		foreach (int num15 in StudentGlobals.KeysOfStudentName())
		{
			studentSaveData.studentName.Add(num15, StudentGlobals.GetStudentName(num15));
		}
		foreach (int num16 in StudentGlobals.KeysOfStudentPhotographed())
		{
			if (StudentGlobals.GetStudentPhotographed(num16))
			{
				studentSaveData.studentPhotographed.Add(num16);
			}
		}
		foreach (int num17 in StudentGlobals.KeysOfStudentReplaced())
		{
			if (StudentGlobals.GetStudentReplaced(num17))
			{
				studentSaveData.studentReplaced.Add(num17);
			}
		}
		foreach (int num18 in StudentGlobals.KeysOfStudentReputation())
		{
			studentSaveData.studentReputation.Add(num18, StudentGlobals.GetStudentReputation(num18));
		}
		foreach (int num19 in StudentGlobals.KeysOfStudentSanity())
		{
			studentSaveData.studentSanity.Add(num19, StudentGlobals.GetStudentSanity(num19));
		}
		return studentSaveData;
	}

	// Token: 0x06001A3B RID: 6715 RVA: 0x001011E8 File Offset: 0x000FF3E8
	public static void WriteToGlobals(StudentSaveData data)
	{
		StudentGlobals.CustomSuitor = data.customSuitor;
		StudentGlobals.CustomSuitorAccessory = data.customSuitorAccessory;
		StudentGlobals.CustomSuitorBlonde = data.customSuitorBlonde;
		StudentGlobals.CustomSuitorEyewear = data.customSuitorEyewear;
		StudentGlobals.CustomSuitorHair = data.customSuitorHair;
		StudentGlobals.CustomSuitorJewelry = data.customSuitorJewelry;
		StudentGlobals.CustomSuitorTan = data.customSuitorTan;
		StudentGlobals.ExpelProgress = data.expelProgress;
		StudentGlobals.FemaleUniform = data.femaleUniform;
		StudentGlobals.MaleUniform = data.maleUniform;
		foreach (KeyValuePair<int, string> keyValuePair in data.studentAccessory)
		{
			StudentGlobals.SetStudentAccessory(keyValuePair.Key, keyValuePair.Value);
		}
		foreach (int studentID in data.studentArrested)
		{
			StudentGlobals.SetStudentArrested(studentID, true);
		}
		foreach (int studentID2 in data.studentBroken)
		{
			StudentGlobals.SetStudentBroken(studentID2, true);
		}
		foreach (KeyValuePair<int, float> keyValuePair2 in data.studentBustSize)
		{
			StudentGlobals.SetStudentBustSize(keyValuePair2.Key, keyValuePair2.Value);
		}
		foreach (KeyValuePair<int, Color> keyValuePair3 in data.studentColor)
		{
			StudentGlobals.SetStudentColor(keyValuePair3.Key, keyValuePair3.Value);
		}
		foreach (int studentID3 in data.studentDead)
		{
			StudentGlobals.SetStudentDead(studentID3, true);
		}
		foreach (int studentID4 in data.studentDying)
		{
			StudentGlobals.SetStudentDying(studentID4, true);
		}
		foreach (int studentID5 in data.studentExpelled)
		{
			StudentGlobals.SetStudentExpelled(studentID5, true);
		}
		foreach (int studentID6 in data.studentExposed)
		{
			StudentGlobals.SetStudentExposed(studentID6, true);
		}
		foreach (KeyValuePair<int, Color> keyValuePair4 in data.studentEyeColor)
		{
			StudentGlobals.SetStudentEyeColor(keyValuePair4.Key, keyValuePair4.Value);
		}
		foreach (int studentID7 in data.studentGrudge)
		{
			StudentGlobals.SetStudentGrudge(studentID7, true);
		}
		foreach (KeyValuePair<int, string> keyValuePair5 in data.studentHairstyle)
		{
			StudentGlobals.SetStudentHairstyle(keyValuePair5.Key, keyValuePair5.Value);
		}
		foreach (int studentID8 in data.studentKidnapped)
		{
			StudentGlobals.SetStudentKidnapped(studentID8, true);
		}
		foreach (int studentID9 in data.studentMissing)
		{
			StudentGlobals.SetStudentMissing(studentID9, true);
		}
		foreach (KeyValuePair<int, string> keyValuePair6 in data.studentName)
		{
			StudentGlobals.SetStudentName(keyValuePair6.Key, keyValuePair6.Value);
		}
		foreach (int studentID10 in data.studentPhotographed)
		{
			StudentGlobals.SetStudentPhotographed(studentID10, true);
		}
		foreach (int studentID11 in data.studentReplaced)
		{
			StudentGlobals.SetStudentReplaced(studentID11, true);
		}
		foreach (KeyValuePair<int, int> keyValuePair7 in data.studentReputation)
		{
			StudentGlobals.SetStudentReputation(keyValuePair7.Key, keyValuePair7.Value);
		}
		foreach (KeyValuePair<int, float> keyValuePair8 in data.studentSanity)
		{
			StudentGlobals.SetStudentSanity(keyValuePair8.Key, keyValuePair8.Value);
		}
	}

	// Token: 0x04002952 RID: 10578
	public bool customSuitor;

	// Token: 0x04002953 RID: 10579
	public int customSuitorAccessory;

	// Token: 0x04002954 RID: 10580
	public bool customSuitorBlonde;

	// Token: 0x04002955 RID: 10581
	public int customSuitorEyewear;

	// Token: 0x04002956 RID: 10582
	public int customSuitorHair;

	// Token: 0x04002957 RID: 10583
	public int customSuitorJewelry;

	// Token: 0x04002958 RID: 10584
	public bool customSuitorTan;

	// Token: 0x04002959 RID: 10585
	public int expelProgress;

	// Token: 0x0400295A RID: 10586
	public int femaleUniform;

	// Token: 0x0400295B RID: 10587
	public int maleUniform;

	// Token: 0x0400295C RID: 10588
	public IntAndStringDictionary studentAccessory = new IntAndStringDictionary();

	// Token: 0x0400295D RID: 10589
	public IntHashSet studentArrested = new IntHashSet();

	// Token: 0x0400295E RID: 10590
	public IntHashSet studentBroken = new IntHashSet();

	// Token: 0x0400295F RID: 10591
	public IntAndFloatDictionary studentBustSize = new IntAndFloatDictionary();

	// Token: 0x04002960 RID: 10592
	public IntAndColorDictionary studentColor = new IntAndColorDictionary();

	// Token: 0x04002961 RID: 10593
	public IntHashSet studentDead = new IntHashSet();

	// Token: 0x04002962 RID: 10594
	public IntHashSet studentDying = new IntHashSet();

	// Token: 0x04002963 RID: 10595
	public IntHashSet studentExpelled = new IntHashSet();

	// Token: 0x04002964 RID: 10596
	public IntHashSet studentExposed = new IntHashSet();

	// Token: 0x04002965 RID: 10597
	public IntAndColorDictionary studentEyeColor = new IntAndColorDictionary();

	// Token: 0x04002966 RID: 10598
	public IntHashSet studentGrudge = new IntHashSet();

	// Token: 0x04002967 RID: 10599
	public IntAndStringDictionary studentHairstyle = new IntAndStringDictionary();

	// Token: 0x04002968 RID: 10600
	public IntHashSet studentKidnapped = new IntHashSet();

	// Token: 0x04002969 RID: 10601
	public IntHashSet studentMissing = new IntHashSet();

	// Token: 0x0400296A RID: 10602
	public IntAndStringDictionary studentName = new IntAndStringDictionary();

	// Token: 0x0400296B RID: 10603
	public IntHashSet studentPhotographed = new IntHashSet();

	// Token: 0x0400296C RID: 10604
	public IntHashSet studentReplaced = new IntHashSet();

	// Token: 0x0400296D RID: 10605
	public IntAndIntDictionary studentReputation = new IntAndIntDictionary();

	// Token: 0x0400296E RID: 10606
	public IntAndFloatDictionary studentSanity = new IntAndFloatDictionary();

	// Token: 0x0400296F RID: 10607
	public IntHashSet studentSlave = new IntHashSet();
}
