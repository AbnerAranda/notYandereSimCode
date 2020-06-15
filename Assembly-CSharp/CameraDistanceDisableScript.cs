using System;
using UnityEngine;

// Token: 0x02000225 RID: 549
public class CameraDistanceDisableScript : MonoBehaviour
{
	// Token: 0x06001215 RID: 4629 RVA: 0x0008049D File Offset: 0x0007E69D
	private void Update()
	{
		if (Vector3.Distance(this.Yandere.position, this.RenderTarget.position) > 15f)
		{
			this.MyCamera.enabled = false;
			return;
		}
		this.MyCamera.enabled = true;
	}

	// Token: 0x04001549 RID: 5449
	public Transform RenderTarget;

	// Token: 0x0400154A RID: 5450
	public Transform Yandere;

	// Token: 0x0400154B RID: 5451
	public Camera MyCamera;
}
