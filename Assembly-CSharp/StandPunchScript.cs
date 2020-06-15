using System;
using UnityEngine;

// Token: 0x020003F8 RID: 1016
public class StandPunchScript : MonoBehaviour
{
	// Token: 0x06001AFF RID: 6911 RVA: 0x0011033C File Offset: 0x0010E53C
	private void OnTriggerEnter(Collider other)
	{
		StudentScript component = other.gameObject.GetComponent<StudentScript>();
		if (component != null && component.StudentID > 1)
		{
			component.JojoReact();
		}
	}

	// Token: 0x04002BEC RID: 11244
	public Collider MyCollider;
}
