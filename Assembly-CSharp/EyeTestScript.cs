using System;
using UnityEngine;

// Token: 0x0200049A RID: 1178
public class EyeTestScript : MonoBehaviour
{
	// Token: 0x06001E36 RID: 7734 RVA: 0x0017C358 File Offset: 0x0017A558
	private void Start()
	{
		this.MyAnimation["moodyEyes_00"].layer = 1;
		this.MyAnimation.Play("moodyEyes_00");
		this.MyAnimation["moodyEyes_00"].weight = 1f;
		this.MyAnimation.Play("moodyEyes_00");
	}

	// Token: 0x04003C6D RID: 15469
	public Animation MyAnimation;
}
