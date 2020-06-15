using System;
using UnityEngine;

// Token: 0x020000E8 RID: 232
public class BoneSetsScript : MonoBehaviour
{
	// Token: 0x06000A72 RID: 2674 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Start()
	{
	}

	// Token: 0x06000A73 RID: 2675 RVA: 0x00056898 File Offset: 0x00054A98
	private void Update()
	{
		if (this.Head != null)
		{
			this.RightArm.localPosition = this.RightArmPosition;
			this.RightArm.localEulerAngles = this.RightArmRotation;
			this.LeftArm.localPosition = this.LeftArmPosition;
			this.LeftArm.localEulerAngles = this.LeftArmRotation;
			this.RightLeg.localPosition = this.RightLegPosition;
			this.RightLeg.localEulerAngles = this.RightLegRotation;
			this.LeftLeg.localPosition = this.LeftLegPosition;
			this.LeftLeg.localEulerAngles = this.LeftLegRotation;
			this.Head.localPosition = this.HeadPosition;
		}
		base.enabled = false;
	}

	// Token: 0x04000AE5 RID: 2789
	public Transform[] BoneSet1;

	// Token: 0x04000AE6 RID: 2790
	public Transform[] BoneSet2;

	// Token: 0x04000AE7 RID: 2791
	public Transform[] BoneSet3;

	// Token: 0x04000AE8 RID: 2792
	public Transform[] BoneSet4;

	// Token: 0x04000AE9 RID: 2793
	public Transform[] BoneSet5;

	// Token: 0x04000AEA RID: 2794
	public Transform[] BoneSet6;

	// Token: 0x04000AEB RID: 2795
	public Transform[] BoneSet7;

	// Token: 0x04000AEC RID: 2796
	public Transform[] BoneSet8;

	// Token: 0x04000AED RID: 2797
	public Transform[] BoneSet9;

	// Token: 0x04000AEE RID: 2798
	public Vector3[] BoneSet1Pos;

	// Token: 0x04000AEF RID: 2799
	public Vector3[] BoneSet2Pos;

	// Token: 0x04000AF0 RID: 2800
	public Vector3[] BoneSet3Pos;

	// Token: 0x04000AF1 RID: 2801
	public Vector3[] BoneSet4Pos;

	// Token: 0x04000AF2 RID: 2802
	public Vector3[] BoneSet5Pos;

	// Token: 0x04000AF3 RID: 2803
	public Vector3[] BoneSet6Pos;

	// Token: 0x04000AF4 RID: 2804
	public Vector3[] BoneSet7Pos;

	// Token: 0x04000AF5 RID: 2805
	public Vector3[] BoneSet8Pos;

	// Token: 0x04000AF6 RID: 2806
	public Vector3[] BoneSet9Pos;

	// Token: 0x04000AF7 RID: 2807
	public float Timer;

	// Token: 0x04000AF8 RID: 2808
	public Transform RightArm;

	// Token: 0x04000AF9 RID: 2809
	public Transform LeftArm;

	// Token: 0x04000AFA RID: 2810
	public Transform RightLeg;

	// Token: 0x04000AFB RID: 2811
	public Transform LeftLeg;

	// Token: 0x04000AFC RID: 2812
	public Transform Head;

	// Token: 0x04000AFD RID: 2813
	public Vector3 RightArmPosition;

	// Token: 0x04000AFE RID: 2814
	public Vector3 RightArmRotation;

	// Token: 0x04000AFF RID: 2815
	public Vector3 LeftArmPosition;

	// Token: 0x04000B00 RID: 2816
	public Vector3 LeftArmRotation;

	// Token: 0x04000B01 RID: 2817
	public Vector3 RightLegPosition;

	// Token: 0x04000B02 RID: 2818
	public Vector3 RightLegRotation;

	// Token: 0x04000B03 RID: 2819
	public Vector3 LeftLegPosition;

	// Token: 0x04000B04 RID: 2820
	public Vector3 LeftLegRotation;

	// Token: 0x04000B05 RID: 2821
	public Vector3 HeadPosition;
}
