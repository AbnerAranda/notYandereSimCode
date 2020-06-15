using System;
using UnityEngine;

// Token: 0x02000349 RID: 841
public class NurseScript : MonoBehaviour
{
	// Token: 0x06001899 RID: 6297 RVA: 0x000E2046 File Offset: 0x000E0246
	private void Awake()
	{
		Animation component = this.Character.GetComponent<Animation>();
		component["f02_noBlink_00"].layer = 1;
		component.Blend("f02_noBlink_00");
	}

	// Token: 0x0600189A RID: 6298 RVA: 0x000E206E File Offset: 0x000E026E
	private void LateUpdate()
	{
		this.SkirtCenter.localEulerAngles = new Vector3(-15f, this.SkirtCenter.localEulerAngles.y, this.SkirtCenter.localEulerAngles.z);
	}

	// Token: 0x04002440 RID: 9280
	public GameObject Character;

	// Token: 0x04002441 RID: 9281
	public Transform SkirtCenter;
}
