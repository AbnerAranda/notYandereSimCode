using System;

// Token: 0x020003BF RID: 959
[Serializable]
public class SaveFileSaveData
{
	// Token: 0x06001A2E RID: 6702 RVA: 0x00100877 File Offset: 0x000FEA77
	public static SaveFileSaveData ReadFromGlobals()
	{
		return new SaveFileSaveData
		{
			currentSaveFile = SaveFileGlobals.CurrentSaveFile
		};
	}

	// Token: 0x06001A2F RID: 6703 RVA: 0x00100889 File Offset: 0x000FEA89
	public static void WriteToGlobals(SaveFileSaveData data)
	{
		SaveFileGlobals.CurrentSaveFile = data.currentSaveFile;
	}

	// Token: 0x0400293B RID: 10555
	public int currentSaveFile;
}
