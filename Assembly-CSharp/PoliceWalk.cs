using System;
using UnityEngine;

// Token: 0x02000370 RID: 880
public class PoliceWalk : MonoBehaviour
{
	// Token: 0x0600192E RID: 6446 RVA: 0x000EEA70 File Offset: 0x000ECC70
	private void Update()
	{
		Vector3 position = base.transform.position;
		position.z += Time.deltaTime;
		base.transform.position = position;
	}
}
