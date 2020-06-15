using System;
using UnityEngine;

// Token: 0x02000406 RID: 1030
public class StudentCrusherScript : MonoBehaviour
{
	// Token: 0x06001B30 RID: 6960 RVA: 0x00113C1C File Offset: 0x00111E1C
	private void OnTriggerEnter(Collider other)
	{
		if (other.transform.root.gameObject.layer == 9)
		{
			StudentScript component = other.transform.root.gameObject.GetComponent<StudentScript>();
			if (component != null && component.StudentID > 1)
			{
				if (this.Mecha.Speed > 0.9f)
				{
					UnityEngine.Object.Instantiate<GameObject>(component.BloodyScream, base.transform.position + Vector3.up, Quaternion.identity);
					component.BecomeRagdoll();
				}
				if (this.Mecha.Speed > 5f)
				{
					component.Ragdoll.Dismember();
				}
			}
		}
	}

	// Token: 0x04002C9E RID: 11422
	public MechaScript Mecha;
}
