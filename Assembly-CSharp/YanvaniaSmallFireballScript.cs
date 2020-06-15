using System;
using UnityEngine;

// Token: 0x02000487 RID: 1159
public class YanvaniaSmallFireballScript : MonoBehaviour
{
	// Token: 0x06001DF2 RID: 7666 RVA: 0x00176440 File Offset: 0x00174640
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == "Heart")
		{
			UnityEngine.Object.Instantiate<GameObject>(this.Explosion, base.transform.position, Quaternion.identity);
			UnityEngine.Object.Destroy(base.gameObject);
		}
		if (other.gameObject.name == "YanmontChan")
		{
			other.gameObject.GetComponent<YanvaniaYanmontScript>().TakeDamage(10);
			UnityEngine.Object.Instantiate<GameObject>(this.Explosion, base.transform.position, Quaternion.identity);
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04003B68 RID: 15208
	public GameObject Explosion;
}
