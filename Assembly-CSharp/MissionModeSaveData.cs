using System;
using System.Collections.Generic;

// Token: 0x020003BB RID: 955
[Serializable]
public class MissionModeSaveData
{
	// Token: 0x06001A22 RID: 6690 RVA: 0x00100068 File Offset: 0x000FE268
	public static MissionModeSaveData ReadFromGlobals()
	{
		MissionModeSaveData missionModeSaveData = new MissionModeSaveData();
		foreach (int num in MissionModeGlobals.KeysOfMissionCondition())
		{
			missionModeSaveData.missionCondition.Add(num, MissionModeGlobals.GetMissionCondition(num));
		}
		missionModeSaveData.missionDifficulty = MissionModeGlobals.MissionDifficulty;
		missionModeSaveData.missionMode = MissionModeGlobals.MissionMode;
		missionModeSaveData.missionRequiredClothing = MissionModeGlobals.MissionRequiredClothing;
		missionModeSaveData.missionRequiredDisposal = MissionModeGlobals.MissionRequiredDisposal;
		missionModeSaveData.missionRequiredWeapon = MissionModeGlobals.MissionRequiredWeapon;
		missionModeSaveData.missionTarget = MissionModeGlobals.MissionTarget;
		missionModeSaveData.missionTargetName = MissionModeGlobals.MissionTargetName;
		missionModeSaveData.nemesisDifficulty = MissionModeGlobals.NemesisDifficulty;
		return missionModeSaveData;
	}

	// Token: 0x06001A23 RID: 6691 RVA: 0x00100100 File Offset: 0x000FE300
	public static void WriteToGlobals(MissionModeSaveData data)
	{
		foreach (KeyValuePair<int, int> keyValuePair in data.missionCondition)
		{
			MissionModeGlobals.SetMissionCondition(keyValuePair.Key, keyValuePair.Value);
		}
		MissionModeGlobals.MissionDifficulty = data.missionDifficulty;
		MissionModeGlobals.MissionMode = data.missionMode;
		MissionModeGlobals.MissionRequiredClothing = data.missionRequiredClothing;
		MissionModeGlobals.MissionRequiredDisposal = data.missionRequiredDisposal;
		MissionModeGlobals.MissionRequiredWeapon = data.missionRequiredWeapon;
		MissionModeGlobals.MissionTarget = data.missionTarget;
		MissionModeGlobals.MissionTargetName = data.missionTargetName;
		MissionModeGlobals.NemesisDifficulty = data.nemesisDifficulty;
	}

	// Token: 0x0400290C RID: 10508
	public IntAndIntDictionary missionCondition = new IntAndIntDictionary();

	// Token: 0x0400290D RID: 10509
	public int missionDifficulty;

	// Token: 0x0400290E RID: 10510
	public bool missionMode;

	// Token: 0x0400290F RID: 10511
	public int missionRequiredClothing;

	// Token: 0x04002910 RID: 10512
	public int missionRequiredDisposal;

	// Token: 0x04002911 RID: 10513
	public int missionRequiredWeapon;

	// Token: 0x04002912 RID: 10514
	public int missionTarget;

	// Token: 0x04002913 RID: 10515
	public string missionTargetName = string.Empty;

	// Token: 0x04002914 RID: 10516
	public int nemesisDifficulty;
}
