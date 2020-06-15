using System;
using UnityEngine;

// Token: 0x020004AF RID: 1199
public class RoseSpawnerScript : MonoBehaviour
{
	// Token: 0x06001E69 RID: 7785 RVA: 0x0017E41E File Offset: 0x0017C61E
	private void Start()
	{
		this.SpawnRose();
	}

	// Token: 0x06001E6A RID: 7786 RVA: 0x0017E426 File Offset: 0x0017C626
	private void Update()
	{
		this.Timer += Time.deltaTime;
		if (this.Timer > 0.1f)
		{
			this.SpawnRose();
		}
	}

	// Token: 0x06001E6B RID: 7787 RVA: 0x0017E450 File Offset: 0x0017C650
	private void SpawnRose()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.Rose, base.transform.position, Quaternion.identity);
		gameObject.GetComponent<Rigidbody>().AddForce(base.transform.forward * this.ForwardForce);
		gameObject.GetComponent<Rigidbody>().AddForce(base.transform.up * this.UpwardForce);
		gameObject.transform.localEulerAngles = new Vector3(UnityEngine.Random.Range(0f, 360f), UnityEngine.Random.Range(0f, 360f), UnityEngine.Random.Range(0f, 360f));
		base.transform.localPosition = new Vector3(UnityEngine.Random.Range(-5f, 5f), base.transform.localPosition.y, base.transform.localPosition.z);
		base.transform.LookAt(this.DramaGirl);
		this.Timer = 0f;
	}

	// Token: 0x04003CAC RID: 15532
	public Transform DramaGirl;

	// Token: 0x04003CAD RID: 15533
	public Transform Target;

	// Token: 0x04003CAE RID: 15534
	public GameObject Rose;

	// Token: 0x04003CAF RID: 15535
	public float Timer;

	// Token: 0x04003CB0 RID: 15536
	public float ForwardForce;

	// Token: 0x04003CB1 RID: 15537
	public float UpwardForce;
}
