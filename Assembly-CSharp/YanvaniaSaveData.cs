using System;

// Token: 0x020003C5 RID: 965
[Serializable]
public class YanvaniaSaveData
{
	// Token: 0x06001A40 RID: 6720 RVA: 0x00101A78 File Offset: 0x000FFC78
	public static YanvaniaSaveData ReadFromGlobals()
	{
		return new YanvaniaSaveData
		{
			draculaDefeated = YanvaniaGlobals.DraculaDefeated,
			midoriEasterEgg = YanvaniaGlobals.MidoriEasterEgg
		};
	}

	// Token: 0x06001A41 RID: 6721 RVA: 0x00101A95 File Offset: 0x000FFC95
	public static void WriteToGlobals(YanvaniaSaveData data)
	{
		YanvaniaGlobals.DraculaDefeated = data.draculaDefeated;
		YanvaniaGlobals.MidoriEasterEgg = data.midoriEasterEgg;
	}

	// Token: 0x04002974 RID: 10612
	public bool draculaDefeated;

	// Token: 0x04002975 RID: 10613
	public bool midoriEasterEgg;
}
