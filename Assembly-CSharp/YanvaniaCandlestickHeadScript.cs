using System;
using UnityEngine;

// Token: 0x0200047E RID: 1150
public class YanvaniaCandlestickHeadScript : MonoBehaviour
{
	// Token: 0x06001DD5 RID: 7637 RVA: 0x0017489C File Offset: 0x00172A9C
	private void Start()
	{
		Rigidbody component = base.GetComponent<Rigidbody>();
		component.AddForce(base.transform.up * 100f);
		component.AddForce(base.transform.right * 100f);
		this.Value = UnityEngine.Random.Range(-1f, 1f);
	}

	// Token: 0x06001DD6 RID: 7638 RVA: 0x001748FC File Offset: 0x00172AFC
	private void Update()
	{
		this.Rotation += new Vector3(this.Value, this.Value, this.Value);
		base.transform.localEulerAngles = this.Rotation;
		if (base.transform.localPosition.y < 0.23f)
		{
			UnityEngine.Object.Instantiate<GameObject>(this.Fire, base.transform.position, Quaternion.identity);
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04003B19 RID: 15129
	public GameObject Fire;

	// Token: 0x04003B1A RID: 15130
	public Vector3 Rotation;

	// Token: 0x04003B1B RID: 15131
	public float Value;
}
