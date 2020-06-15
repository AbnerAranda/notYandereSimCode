using System;
using UnityEngine;

// Token: 0x0200030C RID: 780
public class IntroYandereScript : MonoBehaviour
{
	// Token: 0x060017A0 RID: 6048 RVA: 0x000D0AA8 File Offset: 0x000CECA8
	private void LateUpdate()
	{
		this.Hips.localEulerAngles = new Vector3(this.Hips.localEulerAngles.x + this.X, this.Hips.localEulerAngles.y, this.Hips.localEulerAngles.z);
		this.Spine.localEulerAngles = new Vector3(this.Spine.localEulerAngles.x + this.X, this.Spine.localEulerAngles.y, this.Spine.localEulerAngles.z);
		this.Spine1.localEulerAngles = new Vector3(this.Spine1.localEulerAngles.x + this.X, this.Spine1.localEulerAngles.y, this.Spine1.localEulerAngles.z);
		this.Spine2.localEulerAngles = new Vector3(this.Spine2.localEulerAngles.x + this.X, this.Spine2.localEulerAngles.y, this.Spine2.localEulerAngles.z);
		this.Spine3.localEulerAngles = new Vector3(this.Spine3.localEulerAngles.x + this.X, this.Spine3.localEulerAngles.y, this.Spine3.localEulerAngles.z);
		this.Neck.localEulerAngles = new Vector3(this.Neck.localEulerAngles.x + this.X, this.Neck.localEulerAngles.y, this.Neck.localEulerAngles.z);
		this.Head.localEulerAngles = new Vector3(this.Head.localEulerAngles.x + this.X, this.Head.localEulerAngles.y, this.Head.localEulerAngles.z);
		this.RightUpLeg.localEulerAngles = new Vector3(this.RightUpLeg.localEulerAngles.x - this.X, this.RightUpLeg.localEulerAngles.y, this.RightUpLeg.localEulerAngles.z);
		this.RightLeg.localEulerAngles = new Vector3(this.RightLeg.localEulerAngles.x - this.X, this.RightLeg.localEulerAngles.y, this.RightLeg.localEulerAngles.z);
		this.RightFoot.localEulerAngles = new Vector3(this.RightFoot.localEulerAngles.x - this.X, this.RightFoot.localEulerAngles.y, this.RightFoot.localEulerAngles.z);
		this.LeftUpLeg.localEulerAngles = new Vector3(this.LeftUpLeg.localEulerAngles.x - this.X, this.LeftUpLeg.localEulerAngles.y, this.LeftUpLeg.localEulerAngles.z);
		this.LeftLeg.localEulerAngles = new Vector3(this.LeftLeg.localEulerAngles.x - this.X, this.LeftLeg.localEulerAngles.y, this.LeftLeg.localEulerAngles.z);
		this.LeftFoot.localEulerAngles = new Vector3(this.LeftFoot.localEulerAngles.x - this.X, this.LeftFoot.localEulerAngles.y, this.LeftFoot.localEulerAngles.z);
	}

	// Token: 0x04002182 RID: 8578
	public Transform Hips;

	// Token: 0x04002183 RID: 8579
	public Transform Spine;

	// Token: 0x04002184 RID: 8580
	public Transform Spine1;

	// Token: 0x04002185 RID: 8581
	public Transform Spine2;

	// Token: 0x04002186 RID: 8582
	public Transform Spine3;

	// Token: 0x04002187 RID: 8583
	public Transform Neck;

	// Token: 0x04002188 RID: 8584
	public Transform Head;

	// Token: 0x04002189 RID: 8585
	public Transform RightUpLeg;

	// Token: 0x0400218A RID: 8586
	public Transform RightLeg;

	// Token: 0x0400218B RID: 8587
	public Transform RightFoot;

	// Token: 0x0400218C RID: 8588
	public Transform LeftUpLeg;

	// Token: 0x0400218D RID: 8589
	public Transform LeftLeg;

	// Token: 0x0400218E RID: 8590
	public Transform LeftFoot;

	// Token: 0x0400218F RID: 8591
	public float X;
}
