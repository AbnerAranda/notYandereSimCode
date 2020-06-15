using System;
using UnityEngine;

// Token: 0x0200042F RID: 1071
public class TornadoScript : MonoBehaviour
{
	// Token: 0x06001C7B RID: 7291 RVA: 0x001560BC File Offset: 0x001542BC
	private void Update()
	{
		this.Timer += Time.deltaTime;
		if (this.Timer > 0.5f)
		{
			base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y + Time.deltaTime, base.transform.position.z);
			this.MyCollider.enabled = true;
		}
	}

	// Token: 0x06001C7C RID: 7292 RVA: 0x0015613C File Offset: 0x0015433C
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 9)
		{
			StudentScript component = other.gameObject.GetComponent<StudentScript>();
			if (component != null && component.StudentID > 1)
			{
				this.Scream = UnityEngine.Object.Instantiate<GameObject>(component.Male ? this.MaleBloodyScream : this.FemaleBloodyScream, component.transform.position + Vector3.up, Quaternion.identity);
				this.Scream.transform.parent = component.HipCollider.transform;
				this.Scream.transform.localPosition = Vector3.zero;
				component.DeathType = DeathType.EasterEgg;
				component.BecomeRagdoll();
				Rigidbody rigidbody = component.Ragdoll.AllRigidbodies[0];
				rigidbody.isKinematic = false;
				rigidbody.AddForce(Vector3.up * this.Strength);
			}
		}
	}

	// Token: 0x040035A4 RID: 13732
	public GameObject FemaleBloodyScream;

	// Token: 0x040035A5 RID: 13733
	public GameObject MaleBloodyScream;

	// Token: 0x040035A6 RID: 13734
	public GameObject Scream;

	// Token: 0x040035A7 RID: 13735
	public Collider MyCollider;

	// Token: 0x040035A8 RID: 13736
	public float Strength = 10000f;

	// Token: 0x040035A9 RID: 13737
	public float Timer;
}
