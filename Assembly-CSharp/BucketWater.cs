using System;
using UnityEngine;

// Token: 0x020000ED RID: 237
[Serializable]
public class BucketWater : BucketContents
{
	// Token: 0x1700020E RID: 526
	// (get) Token: 0x06000A7F RID: 2687 RVA: 0x00056E97 File Offset: 0x00055097
	// (set) Token: 0x06000A80 RID: 2688 RVA: 0x00056E9F File Offset: 0x0005509F
	public float Bloodiness
	{
		get
		{
			return this.bloodiness;
		}
		set
		{
			this.bloodiness = Mathf.Clamp01(value);
		}
	}

	// Token: 0x1700020F RID: 527
	// (get) Token: 0x06000A81 RID: 2689 RVA: 0x00056EAD File Offset: 0x000550AD
	// (set) Token: 0x06000A82 RID: 2690 RVA: 0x00056EB5 File Offset: 0x000550B5
	public bool HasBleach
	{
		get
		{
			return this.hasBleach;
		}
		set
		{
			this.hasBleach = value;
		}
	}

	// Token: 0x17000210 RID: 528
	// (get) Token: 0x06000A83 RID: 2691 RVA: 0x0002D199 File Offset: 0x0002B399
	public override BucketContentsType Type
	{
		get
		{
			return BucketContentsType.Water;
		}
	}

	// Token: 0x17000211 RID: 529
	// (get) Token: 0x06000A84 RID: 2692 RVA: 0x00056EAD File Offset: 0x000550AD
	public override bool IsCleaningAgent
	{
		get
		{
			return this.hasBleach;
		}
	}

	// Token: 0x17000212 RID: 530
	// (get) Token: 0x06000A85 RID: 2693 RVA: 0x0002D199 File Offset: 0x0002B399
	public override bool IsFlammable
	{
		get
		{
			return false;
		}
	}

	// Token: 0x06000A86 RID: 2694 RVA: 0x00022944 File Offset: 0x00020B44
	public override bool CanBeLifted(int strength)
	{
		return true;
	}

	// Token: 0x04000B24 RID: 2852
	[SerializeField]
	private float bloodiness;

	// Token: 0x04000B25 RID: 2853
	[SerializeField]
	private bool hasBleach;
}
