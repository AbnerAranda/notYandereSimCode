using System;
using UnityEngine;

// Token: 0x02000230 RID: 560
public class CharacterScript : MonoBehaviour
{
	// Token: 0x06001234 RID: 4660 RVA: 0x00081798 File Offset: 0x0007F998
	private void SetAnimations()
	{
		Animation component = base.GetComponent<Animation>();
		component["f02_yanderePose_00"].layer = 1;
		component["f02_yanderePose_00"].weight = 0f;
		component.Play("f02_yanderePose_00");
		component["f02_shy_00"].layer = 2;
		component["f02_shy_00"].weight = 0f;
		component.Play("f02_shy_00");
		component["f02_fist_00"].layer = 3;
		component["f02_fist_00"].weight = 0f;
		component.Play("f02_fist_00");
		component["f02_mopping_00"].layer = 4;
		component["f02_mopping_00"].weight = 0f;
		component["f02_mopping_00"].speed = 2f;
		component.Play("f02_mopping_00");
		component["f02_carry_00"].layer = 5;
		component["f02_carry_00"].weight = 0f;
		component.Play("f02_carry_00");
		component["f02_mopCarry_00"].layer = 6;
		component["f02_mopCarry_00"].weight = 0f;
		component.Play("f02_mopCarry_00");
		component["f02_bucketCarry_00"].layer = 7;
		component["f02_bucketCarry_00"].weight = 0f;
		component.Play("f02_bucketCarry_00");
		component["f02_cameraPose_00"].layer = 8;
		component["f02_cameraPose_00"].weight = 0f;
		component.Play("f02_cameraPose_00");
		component["f02_dipping_00"].speed = 2f;
		component["f02_cameraPose_00"].weight = 0f;
		component["f02_shy_00"].weight = 0f;
	}

	// Token: 0x04001584 RID: 5508
	public Transform RightBreast;

	// Token: 0x04001585 RID: 5509
	public Transform LeftBreast;

	// Token: 0x04001586 RID: 5510
	public Transform ItemParent;

	// Token: 0x04001587 RID: 5511
	public Transform PelvisRoot;

	// Token: 0x04001588 RID: 5512
	public Transform RightEye;

	// Token: 0x04001589 RID: 5513
	public Transform LeftEye;

	// Token: 0x0400158A RID: 5514
	public Transform Head;

	// Token: 0x0400158B RID: 5515
	public Transform[] Spine;

	// Token: 0x0400158C RID: 5516
	public Transform[] Arm;

	// Token: 0x0400158D RID: 5517
	public SkinnedMeshRenderer MyRenderer;

	// Token: 0x0400158E RID: 5518
	public Renderer RightYandereEye;

	// Token: 0x0400158F RID: 5519
	public Renderer LeftYandereEye;
}
