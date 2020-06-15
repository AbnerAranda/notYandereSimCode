using System;

// Token: 0x020000EE RID: 238
[Serializable]
public class BucketGas : BucketContents
{
	// Token: 0x17000213 RID: 531
	// (get) Token: 0x06000A88 RID: 2696 RVA: 0x00022944 File Offset: 0x00020B44
	public override BucketContentsType Type
	{
		get
		{
			return BucketContentsType.Gas;
		}
	}

	// Token: 0x17000214 RID: 532
	// (get) Token: 0x06000A89 RID: 2697 RVA: 0x0002D199 File Offset: 0x0002B399
	public override bool IsCleaningAgent
	{
		get
		{
			return false;
		}
	}

	// Token: 0x17000215 RID: 533
	// (get) Token: 0x06000A8A RID: 2698 RVA: 0x00022944 File Offset: 0x00020B44
	public override bool IsFlammable
	{
		get
		{
			return true;
		}
	}

	// Token: 0x06000A8B RID: 2699 RVA: 0x00022944 File Offset: 0x00020B44
	public override bool CanBeLifted(int strength)
	{
		return true;
	}
}
