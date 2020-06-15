using System;
using UnityEngine;

// Token: 0x0200048D RID: 1165
public class YanvaniaWineScript : MonoBehaviour
{
	// Token: 0x06001E05 RID: 7685 RVA: 0x00178194 File Offset: 0x00176394
	private void Update()
	{
		if (base.transform.parent == null)
		{
			this.Rotation += Time.deltaTime * 360f;
			base.transform.localEulerAngles = new Vector3(this.Rotation, this.Rotation, this.Rotation);
			if (base.transform.position.y < 6.5f)
			{
				UnityEngine.Object.Instantiate<GameObject>(this.Shards, new Vector3(base.transform.position.x, 6.5f, base.transform.position.z), Quaternion.identity).transform.localEulerAngles = new Vector3(-90f, 0f, 0f);
				AudioSource.PlayClipAtPoint(base.GetComponent<AudioSource>().clip, base.transform.position);
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}
	}

	// Token: 0x04003BAB RID: 15275
	public GameObject Shards;

	// Token: 0x04003BAC RID: 15276
	public float Rotation;
}
