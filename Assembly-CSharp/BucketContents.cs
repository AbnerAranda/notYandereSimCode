using System;

// Token: 0x020000EC RID: 236
public abstract class BucketContents
{
	// Token: 0x1700020B RID: 523
	// (get) Token: 0x06000A7A RID: 2682
	public abstract BucketContentsType Type { get; }

	// Token: 0x1700020C RID: 524
	// (get) Token: 0x06000A7B RID: 2683
	public abstract bool IsCleaningAgent { get; }

	// Token: 0x1700020D RID: 525
	// (get) Token: 0x06000A7C RID: 2684
	public abstract bool IsFlammable { get; }

	// Token: 0x06000A7D RID: 2685
	public abstract bool CanBeLifted(int strength);
}
