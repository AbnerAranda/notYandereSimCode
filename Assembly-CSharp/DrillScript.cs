using System;
using UnityEngine;

// Token: 0x02000273 RID: 627
public class DrillScript : MonoBehaviour
{
	// Token: 0x0600136D RID: 4973 RVA: 0x000A78D6 File Offset: 0x000A5AD6
	private void LateUpdate()
	{
		base.transform.Rotate(Vector3.up * Time.deltaTime * 3600f);
	}
}
