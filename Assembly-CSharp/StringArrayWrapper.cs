using System;

// Token: 0x0200043F RID: 1087
[Serializable]
public class StringArrayWrapper : ArrayWrapper<string>
{
	// Token: 0x06001CB1 RID: 7345 RVA: 0x0015853F File Offset: 0x0015673F
	public StringArrayWrapper(int size) : base(size)
	{
	}

	// Token: 0x06001CB2 RID: 7346 RVA: 0x00158548 File Offset: 0x00156748
	public StringArrayWrapper(string[] elements) : base(elements)
	{
	}
}
