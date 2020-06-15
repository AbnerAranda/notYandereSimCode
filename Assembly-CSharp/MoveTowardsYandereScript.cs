using System;
using UnityEngine;

// Token: 0x0200033B RID: 827
public class MoveTowardsYandereScript : MonoBehaviour
{
	// Token: 0x0600185B RID: 6235 RVA: 0x000DAFB5 File Offset: 0x000D91B5
	private void Start()
	{
		this.Yandere = GameObject.Find("YandereChan").GetComponent<YandereScript>().Spine[3];
		this.Distance = Vector3.Distance(base.transform.position, this.Yandere.position);
	}

	// Token: 0x0600185C RID: 6236 RVA: 0x000DAFF4 File Offset: 0x000D91F4
	private void Update()
	{
		if (Vector3.Distance(base.transform.position, this.Yandere.position) > this.Distance * 0.5f && base.transform.position.y < this.Yandere.position.y + 0.5f)
		{
			base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y + Time.deltaTime, base.transform.position.z);
		}
		this.Speed += Time.deltaTime;
		base.transform.position = Vector3.MoveTowards(base.transform.position, this.Yandere.position, this.Speed * Time.deltaTime);
		if (Vector3.Distance(base.transform.position, this.Yandere.position) == 0f)
		{
			this.Smoke.emission.enabled = false;
		}
	}

	// Token: 0x04002373 RID: 9075
	public ParticleSystem Smoke;

	// Token: 0x04002374 RID: 9076
	public Transform Yandere;

	// Token: 0x04002375 RID: 9077
	public float Distance;

	// Token: 0x04002376 RID: 9078
	public float Speed;

	// Token: 0x04002377 RID: 9079
	public bool Fall;
}
