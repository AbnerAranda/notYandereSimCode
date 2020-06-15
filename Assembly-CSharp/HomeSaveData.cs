using System;

// Token: 0x020003BA RID: 954
[Serializable]
public class HomeSaveData
{
	// Token: 0x06001A1F RID: 6687 RVA: 0x0010001D File Offset: 0x000FE21D
	public static HomeSaveData ReadFromGlobals()
	{
		return new HomeSaveData
		{
			lateForSchool = HomeGlobals.LateForSchool,
			night = HomeGlobals.Night,
			startInBasement = HomeGlobals.StartInBasement
		};
	}

	// Token: 0x06001A20 RID: 6688 RVA: 0x00100045 File Offset: 0x000FE245
	public static void WriteToGlobals(HomeSaveData data)
	{
		HomeGlobals.LateForSchool = data.lateForSchool;
		HomeGlobals.Night = data.night;
		HomeGlobals.StartInBasement = data.startInBasement;
	}

	// Token: 0x04002909 RID: 10505
	public bool lateForSchool;

	// Token: 0x0400290A RID: 10506
	public bool night;

	// Token: 0x0400290B RID: 10507
	public bool startInBasement;
}
