using System;

// Token: 0x0200045C RID: 1116
public class SerializablePair<T, U>
{
	// Token: 0x06001D01 RID: 7425 RVA: 0x00159200 File Offset: 0x00157400
	public SerializablePair(T first, U second)
	{
		this.first = first;
		this.second = second;
	}

	// Token: 0x06001D02 RID: 7426 RVA: 0x00159218 File Offset: 0x00157418
	public SerializablePair() : this(default(T), default(U))
	{
	}

	// Token: 0x0400366F RID: 13935
	public T first;

	// Token: 0x04003670 RID: 13936
	public U second;
}
