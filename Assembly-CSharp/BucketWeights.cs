using System;
using UnityEngine;

// Token: 0x020000EF RID: 239
[Serializable]
public class BucketWeights : BucketContents
{
	// Token: 0x17000216 RID: 534
	// (get) Token: 0x06000A8D RID: 2701 RVA: 0x00056EC6 File Offset: 0x000550C6
	// (set) Token: 0x06000A8E RID: 2702 RVA: 0x00056ECE File Offset: 0x000550CE
	public int Count
	{
		get
		{
			return this.count;
		}
		set
		{
			this.count = ((value < 0) ? 0 : value);
		}
	}

	// Token: 0x17000217 RID: 535
	// (get) Token: 0x06000A8F RID: 2703 RVA: 0x00033F12 File Offset: 0x00032112
	public override BucketContentsType Type
	{
		get
		{
			return BucketContentsType.Weights;
		}
	}

	// Token: 0x17000218 RID: 536
	// (get) Token: 0x06000A90 RID: 2704 RVA: 0x0002D199 File Offset: 0x0002B399
	public override bool IsCleaningAgent
	{
		get
		{
			return false;
		}
	}

	// Token: 0x17000219 RID: 537
	// (get) Token: 0x06000A91 RID: 2705 RVA: 0x0002D199 File Offset: 0x0002B399
	public override bool IsFlammable
	{
		get
		{
			return false;
		}
	}

	// Token: 0x06000A92 RID: 2706 RVA: 0x00056EDE File Offset: 0x000550DE
	public override bool CanBeLifted(int strength)
	{
		return strength > 0;
	}

	// Token: 0x04000B26 RID: 2854
	[SerializeField]
	private int count;
}
