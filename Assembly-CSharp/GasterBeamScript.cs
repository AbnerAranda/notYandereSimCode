using System;
using UnityEngine;

// Token: 0x020002B0 RID: 688
public class GasterBeamScript : MonoBehaviour
{
	// Token: 0x0600143D RID: 5181 RVA: 0x000B349A File Offset: 0x000B169A
	private void Start()
	{
		if (this.LoveLoveBeam)
		{
			base.transform.localScale = new Vector3(0f, 0f, 0f);
		}
	}

	// Token: 0x0600143E RID: 5182 RVA: 0x000B34C4 File Offset: 0x000B16C4
	private void Update()
	{
		if (this.LoveLoveBeam)
		{
			base.transform.localScale = Vector3.Lerp(base.transform.localScale, new Vector3(100f, this.Target, this.Target), Time.deltaTime * 10f);
			if (base.transform.localScale.x > 99.99f)
			{
				this.Target = 0f;
				if (base.transform.localScale.y < 0.1f)
				{
					UnityEngine.Object.Destroy(base.gameObject);
				}
			}
		}
	}

	// Token: 0x0600143F RID: 5183 RVA: 0x000B355C File Offset: 0x000B175C
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 9)
		{
			StudentScript component = other.gameObject.GetComponent<StudentScript>();
			if (component != null)
			{
				component.DeathType = DeathType.EasterEgg;
				component.BecomeRagdoll();
				Rigidbody rigidbody = component.Ragdoll.AllRigidbodies[0];
				rigidbody.isKinematic = false;
				rigidbody.AddForce((rigidbody.transform.root.position - base.transform.root.position) * this.Strength);
				rigidbody.AddForce(Vector3.up * 1000f);
			}
		}
	}

	// Token: 0x04001CE6 RID: 7398
	public float Strength = 1000f;

	// Token: 0x04001CE7 RID: 7399
	public float Target = 2f;

	// Token: 0x04001CE8 RID: 7400
	public bool LoveLoveBeam;
}
