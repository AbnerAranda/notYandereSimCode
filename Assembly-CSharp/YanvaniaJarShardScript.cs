using System;
using UnityEngine;

// Token: 0x02000485 RID: 1157
public class YanvaniaJarShardScript : MonoBehaviour
{
	// Token: 0x06001DEC RID: 7660 RVA: 0x001762F8 File Offset: 0x001744F8
	private void Start()
	{
		this.Rotation = UnityEngine.Random.Range(-360f, 360f);
		base.GetComponent<Rigidbody>().AddForce(UnityEngine.Random.Range(-100f, 100f), UnityEngine.Random.Range(0f, 100f), UnityEngine.Random.Range(-100f, 100f));
	}

	// Token: 0x06001DED RID: 7661 RVA: 0x00176354 File Offset: 0x00174554
	private void Update()
	{
		this.MyRotation += this.Rotation;
		base.transform.eulerAngles = new Vector3(this.MyRotation, this.MyRotation, this.MyRotation);
		if (base.transform.position.y < 6.5f)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04003B61 RID: 15201
	public float MyRotation;

	// Token: 0x04003B62 RID: 15202
	public float Rotation;
}
