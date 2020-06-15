using System;
using UnityEngine;

// Token: 0x0200001F RID: 31
public class ClimbFollowScript : MonoBehaviour
{
	// Token: 0x060000CE RID: 206 RVA: 0x00011518 File Offset: 0x0000F718
	private void Update()
	{
		base.transform.position = new Vector3(base.transform.position.x, this.Yandere.position.y, base.transform.position.z);
	}

	// Token: 0x0400026D RID: 621
	public Transform Yandere;
}
