using System;
using UnityEngine;

// Token: 0x02000484 RID: 1156
public class YanvaniaJarScript : MonoBehaviour
{
	// Token: 0x06001DEA RID: 7658 RVA: 0x00176210 File Offset: 0x00174410
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 19 && !this.Destroyed)
		{
			UnityEngine.Object.Instantiate<GameObject>(this.Explosion, base.transform.position + Vector3.up * 0.5f, Quaternion.identity);
			this.Destroyed = true;
			AudioClipPlayer.Play2D(this.Break, base.transform.position);
			for (int i = 1; i < 11; i++)
			{
				UnityEngine.Object.Instantiate<GameObject>(this.Shard, base.transform.position + Vector3.up * UnityEngine.Random.Range(0f, 1f) + Vector3.right * UnityEngine.Random.Range(-0.5f, 0.5f), Quaternion.identity);
			}
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04003B5D RID: 15197
	public GameObject Explosion;

	// Token: 0x04003B5E RID: 15198
	public bool Destroyed;

	// Token: 0x04003B5F RID: 15199
	public AudioClip Break;

	// Token: 0x04003B60 RID: 15200
	public GameObject Shard;
}
