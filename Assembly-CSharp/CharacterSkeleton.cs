using System;
using UnityEngine;

// Token: 0x02000282 RID: 642
[Serializable]
public class CharacterSkeleton
{
	// Token: 0x17000358 RID: 856
	// (get) Token: 0x060013A7 RID: 5031 RVA: 0x000AD393 File Offset: 0x000AB593
	public Transform Head
	{
		get
		{
			return this.head;
		}
	}

	// Token: 0x17000359 RID: 857
	// (get) Token: 0x060013A8 RID: 5032 RVA: 0x000AD39B File Offset: 0x000AB59B
	public Transform Neck
	{
		get
		{
			return this.neck;
		}
	}

	// Token: 0x1700035A RID: 858
	// (get) Token: 0x060013A9 RID: 5033 RVA: 0x000AD3A3 File Offset: 0x000AB5A3
	public Transform Chest
	{
		get
		{
			return this.chest;
		}
	}

	// Token: 0x1700035B RID: 859
	// (get) Token: 0x060013AA RID: 5034 RVA: 0x000AD3AB File Offset: 0x000AB5AB
	public Transform Stomach
	{
		get
		{
			return this.stomach;
		}
	}

	// Token: 0x1700035C RID: 860
	// (get) Token: 0x060013AB RID: 5035 RVA: 0x000AD3B3 File Offset: 0x000AB5B3
	public Transform Pelvis
	{
		get
		{
			return this.pelvis;
		}
	}

	// Token: 0x1700035D RID: 861
	// (get) Token: 0x060013AC RID: 5036 RVA: 0x000AD3BB File Offset: 0x000AB5BB
	public Transform RightShoulder
	{
		get
		{
			return this.rightShoulder;
		}
	}

	// Token: 0x1700035E RID: 862
	// (get) Token: 0x060013AD RID: 5037 RVA: 0x000AD3C3 File Offset: 0x000AB5C3
	public Transform LeftShoulder
	{
		get
		{
			return this.leftShoulder;
		}
	}

	// Token: 0x1700035F RID: 863
	// (get) Token: 0x060013AE RID: 5038 RVA: 0x000AD3CB File Offset: 0x000AB5CB
	public Transform RightUpperArm
	{
		get
		{
			return this.rightUpperArm;
		}
	}

	// Token: 0x17000360 RID: 864
	// (get) Token: 0x060013AF RID: 5039 RVA: 0x000AD3D3 File Offset: 0x000AB5D3
	public Transform LeftUpperArm
	{
		get
		{
			return this.leftUpperArm;
		}
	}

	// Token: 0x17000361 RID: 865
	// (get) Token: 0x060013B0 RID: 5040 RVA: 0x000AD3DB File Offset: 0x000AB5DB
	public Transform RightElbow
	{
		get
		{
			return this.rightElbow;
		}
	}

	// Token: 0x17000362 RID: 866
	// (get) Token: 0x060013B1 RID: 5041 RVA: 0x000AD3E3 File Offset: 0x000AB5E3
	public Transform LeftElbow
	{
		get
		{
			return this.leftElbow;
		}
	}

	// Token: 0x17000363 RID: 867
	// (get) Token: 0x060013B2 RID: 5042 RVA: 0x000AD3EB File Offset: 0x000AB5EB
	public Transform RightLowerArm
	{
		get
		{
			return this.rightLowerArm;
		}
	}

	// Token: 0x17000364 RID: 868
	// (get) Token: 0x060013B3 RID: 5043 RVA: 0x000AD3F3 File Offset: 0x000AB5F3
	public Transform LeftLowerArm
	{
		get
		{
			return this.leftLowerArm;
		}
	}

	// Token: 0x17000365 RID: 869
	// (get) Token: 0x060013B4 RID: 5044 RVA: 0x000AD3FB File Offset: 0x000AB5FB
	public Transform RightPalm
	{
		get
		{
			return this.rightPalm;
		}
	}

	// Token: 0x17000366 RID: 870
	// (get) Token: 0x060013B5 RID: 5045 RVA: 0x000AD403 File Offset: 0x000AB603
	public Transform LeftPalm
	{
		get
		{
			return this.leftPalm;
		}
	}

	// Token: 0x17000367 RID: 871
	// (get) Token: 0x060013B6 RID: 5046 RVA: 0x000AD40B File Offset: 0x000AB60B
	public Transform RightUpperLeg
	{
		get
		{
			return this.rightUpperLeg;
		}
	}

	// Token: 0x17000368 RID: 872
	// (get) Token: 0x060013B7 RID: 5047 RVA: 0x000AD413 File Offset: 0x000AB613
	public Transform LeftUpperLeg
	{
		get
		{
			return this.leftUpperLeg;
		}
	}

	// Token: 0x17000369 RID: 873
	// (get) Token: 0x060013B8 RID: 5048 RVA: 0x000AD41B File Offset: 0x000AB61B
	public Transform RightKnee
	{
		get
		{
			return this.rightKnee;
		}
	}

	// Token: 0x1700036A RID: 874
	// (get) Token: 0x060013B9 RID: 5049 RVA: 0x000AD423 File Offset: 0x000AB623
	public Transform LeftKnee
	{
		get
		{
			return this.leftKnee;
		}
	}

	// Token: 0x1700036B RID: 875
	// (get) Token: 0x060013BA RID: 5050 RVA: 0x000AD42B File Offset: 0x000AB62B
	public Transform RightLowerLeg
	{
		get
		{
			return this.rightLowerLeg;
		}
	}

	// Token: 0x1700036C RID: 876
	// (get) Token: 0x060013BB RID: 5051 RVA: 0x000AD433 File Offset: 0x000AB633
	public Transform LeftLowerLeg
	{
		get
		{
			return this.leftLowerLeg;
		}
	}

	// Token: 0x1700036D RID: 877
	// (get) Token: 0x060013BC RID: 5052 RVA: 0x000AD43B File Offset: 0x000AB63B
	public Transform RightFoot
	{
		get
		{
			return this.rightFoot;
		}
	}

	// Token: 0x1700036E RID: 878
	// (get) Token: 0x060013BD RID: 5053 RVA: 0x000AD443 File Offset: 0x000AB643
	public Transform LeftFoot
	{
		get
		{
			return this.leftFoot;
		}
	}

	// Token: 0x04001B48 RID: 6984
	[SerializeField]
	private Transform head;

	// Token: 0x04001B49 RID: 6985
	[SerializeField]
	private Transform neck;

	// Token: 0x04001B4A RID: 6986
	[SerializeField]
	private Transform chest;

	// Token: 0x04001B4B RID: 6987
	[SerializeField]
	private Transform stomach;

	// Token: 0x04001B4C RID: 6988
	[SerializeField]
	private Transform pelvis;

	// Token: 0x04001B4D RID: 6989
	[SerializeField]
	private Transform rightShoulder;

	// Token: 0x04001B4E RID: 6990
	[SerializeField]
	private Transform leftShoulder;

	// Token: 0x04001B4F RID: 6991
	[SerializeField]
	private Transform rightUpperArm;

	// Token: 0x04001B50 RID: 6992
	[SerializeField]
	private Transform leftUpperArm;

	// Token: 0x04001B51 RID: 6993
	[SerializeField]
	private Transform rightElbow;

	// Token: 0x04001B52 RID: 6994
	[SerializeField]
	private Transform leftElbow;

	// Token: 0x04001B53 RID: 6995
	[SerializeField]
	private Transform rightLowerArm;

	// Token: 0x04001B54 RID: 6996
	[SerializeField]
	private Transform leftLowerArm;

	// Token: 0x04001B55 RID: 6997
	[SerializeField]
	private Transform rightPalm;

	// Token: 0x04001B56 RID: 6998
	[SerializeField]
	private Transform leftPalm;

	// Token: 0x04001B57 RID: 6999
	[SerializeField]
	private Transform rightUpperLeg;

	// Token: 0x04001B58 RID: 7000
	[SerializeField]
	private Transform leftUpperLeg;

	// Token: 0x04001B59 RID: 7001
	[SerializeField]
	private Transform rightKnee;

	// Token: 0x04001B5A RID: 7002
	[SerializeField]
	private Transform leftKnee;

	// Token: 0x04001B5B RID: 7003
	[SerializeField]
	private Transform rightLowerLeg;

	// Token: 0x04001B5C RID: 7004
	[SerializeField]
	private Transform leftLowerLeg;

	// Token: 0x04001B5D RID: 7005
	[SerializeField]
	private Transform rightFoot;

	// Token: 0x04001B5E RID: 7006
	[SerializeField]
	private Transform leftFoot;
}
