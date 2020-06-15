using System;
using UnityEngine;

// Token: 0x020004AD RID: 1197
public class LookAtCamera : MonoBehaviour
{
	// Token: 0x06001E63 RID: 7779 RVA: 0x0017E355 File Offset: 0x0017C555
	private void Start()
	{
		if (this.cameraToLookAt == null)
		{
			this.cameraToLookAt = Camera.main;
		}
	}

	// Token: 0x06001E64 RID: 7780 RVA: 0x0017E370 File Offset: 0x0017C570
	private void Update()
	{
		Vector3 b = new Vector3(0f, this.cameraToLookAt.transform.position.y - base.transform.position.y, 0f);
		base.transform.LookAt(this.cameraToLookAt.transform.position - b);
	}

	// Token: 0x04003CAA RID: 15530
	public Camera cameraToLookAt;
}
