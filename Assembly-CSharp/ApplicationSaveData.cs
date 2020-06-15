using System;

// Token: 0x020003B1 RID: 945
[Serializable]
public class ApplicationSaveData
{
	// Token: 0x06001A04 RID: 6660 RVA: 0x000FF522 File Offset: 0x000FD722
	public static ApplicationSaveData ReadFromGlobals()
	{
		return new ApplicationSaveData
		{
			versionNumber = ApplicationGlobals.VersionNumber
		};
	}

	// Token: 0x06001A05 RID: 6661 RVA: 0x000FF534 File Offset: 0x000FD734
	public static void WriteToGlobals(ApplicationSaveData data)
	{
		ApplicationGlobals.VersionNumber = data.versionNumber;
	}

	// Token: 0x040028DC RID: 10460
	public float versionNumber;
}
