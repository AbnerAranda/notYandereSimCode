using System;
using UnityEngine;

// Token: 0x02000479 RID: 1145
public class YanvaniaBigFireballScript : MonoBehaviour
{
	// Token: 0x06001DC7 RID: 7623 RVA: 0x00174410 File Offset: 0x00172610
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == "YanmontChan")
		{
			other.gameObject.GetComponent<YanvaniaYanmontScript>().TakeDamage(15);
			UnityEngine.Object.Instantiate<GameObject>(this.Explosion, base.transform.position, Quaternion.identity);
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04003B09 RID: 15113
	public GameObject Explosion;
}
