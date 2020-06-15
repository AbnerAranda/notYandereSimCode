using System;
using UnityEngine;

// Token: 0x020002B7 RID: 695
public class GhostScript : MonoBehaviour
{
	// Token: 0x06001456 RID: 5206 RVA: 0x000B4D80 File Offset: 0x000B2F80
	private void Update()
	{
		if (Time.timeScale > 0.0001f)
		{
			if (this.Frame > 0)
			{
				base.GetComponent<Animation>().enabled = false;
				base.gameObject.SetActive(false);
				this.Frame = 0;
			}
			this.Frame++;
		}
	}

	// Token: 0x06001457 RID: 5207 RVA: 0x000B4DCF File Offset: 0x000B2FCF
	public void Look()
	{
		this.Neck.LookAt(this.SmartphoneCamera.position);
	}

	// Token: 0x04001D29 RID: 7465
	public Transform SmartphoneCamera;

	// Token: 0x04001D2A RID: 7466
	public Transform Neck;

	// Token: 0x04001D2B RID: 7467
	public Transform GhostEyeLocation;

	// Token: 0x04001D2C RID: 7468
	public Transform GhostEye;

	// Token: 0x04001D2D RID: 7469
	public int Frame;

	// Token: 0x04001D2E RID: 7470
	public bool Move;
}
