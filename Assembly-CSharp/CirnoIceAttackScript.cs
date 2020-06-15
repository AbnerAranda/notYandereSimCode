using System;
using UnityEngine;

// Token: 0x02000239 RID: 569
public class CirnoIceAttackScript : MonoBehaviour
{
	// Token: 0x06001248 RID: 4680 RVA: 0x0008211E File Offset: 0x0008031E
	private void Start()
	{
		Physics.IgnoreLayerCollision(18, 13, true);
		Physics.IgnoreLayerCollision(18, 18, true);
	}

	// Token: 0x06001249 RID: 4681 RVA: 0x00082134 File Offset: 0x00080334
	private void OnCollisionEnter(Collision collision)
	{
		UnityEngine.Object.Instantiate<GameObject>(this.IceExplosion, base.transform.position, Quaternion.identity);
		if (collision.gameObject.layer == 9)
		{
			StudentScript component = collision.gameObject.GetComponent<StudentScript>();
			if (component != null)
			{
				component.SpawnAlarmDisc();
				component.BecomeRagdoll();
			}
		}
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x040015AC RID: 5548
	public GameObject IceExplosion;
}
