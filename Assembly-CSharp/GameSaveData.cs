using System;

// Token: 0x020003B9 RID: 953
[Serializable]
public class GameSaveData
{
	// Token: 0x06001A1C RID: 6684 RVA: 0x000FFFD2 File Offset: 0x000FE1D2
	public static GameSaveData ReadFromGlobals()
	{
		return new GameSaveData
		{
			loveSick = GameGlobals.LoveSick,
			masksBanned = GameGlobals.MasksBanned,
			paranormal = GameGlobals.Paranormal
		};
	}

	// Token: 0x06001A1D RID: 6685 RVA: 0x000FFFFA File Offset: 0x000FE1FA
	public static void WriteToGlobals(GameSaveData data)
	{
		GameGlobals.LoveSick = data.loveSick;
		GameGlobals.MasksBanned = data.masksBanned;
		GameGlobals.Paranormal = data.paranormal;
	}

	// Token: 0x04002906 RID: 10502
	public bool loveSick;

	// Token: 0x04002907 RID: 10503
	public bool masksBanned;

	// Token: 0x04002908 RID: 10504
	public bool paranormal;
}
