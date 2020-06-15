using System;
using UnityEngine;

// Token: 0x020000E1 RID: 225
public class BloodSprayColliderScript : MonoBehaviour
{
	// Token: 0x06000A64 RID: 2660 RVA: 0x000562A0 File Offset: 0x000544A0
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 13)
		{
			YandereScript component = other.gameObject.GetComponent<YandereScript>();
			if (component != null)
			{
				component.Bloodiness = 100f;
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}
	}
}
