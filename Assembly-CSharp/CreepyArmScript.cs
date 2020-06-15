using System;
using UnityEngine;

// Token: 0x02000252 RID: 594
public class CreepyArmScript : MonoBehaviour
{
	// Token: 0x060012CB RID: 4811 RVA: 0x00096EF4 File Offset: 0x000950F4
	private void Update()
	{
		base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y + Time.deltaTime * 0.1f, base.transform.position.z);
	}
}
