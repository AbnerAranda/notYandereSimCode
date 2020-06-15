using System;

// Token: 0x0200045D RID: 1117
[Serializable]
public class IntAndIntPair : SerializablePair<int, int>
{
	// Token: 0x06001D03 RID: 7427 RVA: 0x0015923D File Offset: 0x0015743D
	public IntAndIntPair(int first, int second) : base(first, second)
	{
	}

	// Token: 0x06001D04 RID: 7428 RVA: 0x00159247 File Offset: 0x00157447
	public IntAndIntPair() : base(0, 0)
	{
	}
}
