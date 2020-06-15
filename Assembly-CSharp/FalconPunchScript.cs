using System;
using UnityEngine;

// Token: 0x0200029D RID: 669
public class FalconPunchScript : MonoBehaviour
{
	// Token: 0x06001400 RID: 5120 RVA: 0x000AF5CB File Offset: 0x000AD7CB
	private void Start()
	{
		if (this.Mecha)
		{
			this.MyRigidbody.AddForce(base.transform.forward * this.Speed * 10f);
		}
	}

	// Token: 0x06001401 RID: 5121 RVA: 0x000AF600 File Offset: 0x000AD800
	private void Update()
	{
		if (!this.IgnoreTime)
		{
			this.Timer += Time.deltaTime;
			if (this.Timer > this.TimeLimit)
			{
				this.MyCollider.enabled = false;
			}
		}
		if (this.Shipgirl)
		{
			this.MyRigidbody.AddForce(base.transform.forward * this.Speed);
		}
	}

	// Token: 0x06001402 RID: 5122 RVA: 0x000AF66C File Offset: 0x000AD86C
	private void OnTriggerEnter(Collider other)
	{
		Debug.Log("A punch collided with something.");
		if (other.gameObject.layer == 9)
		{
			Debug.Log("A punch collided with something on the Characters layer.");
			StudentScript component = other.gameObject.GetComponent<StudentScript>();
			if (component != null)
			{
				Debug.Log("A punch collided with a student.");
				if (component.StudentID > 1)
				{
					Debug.Log("A punch collided with a student and killed them.");
					UnityEngine.Object.Instantiate<GameObject>(this.FalconExplosion, component.transform.position + new Vector3(0f, 1f, 0f), Quaternion.identity);
					component.DeathType = DeathType.EasterEgg;
					component.BecomeRagdoll();
					Rigidbody rigidbody = component.Ragdoll.AllRigidbodies[0];
					rigidbody.isKinematic = false;
					Vector3 vector = rigidbody.transform.position - component.Yandere.transform.position;
					if (this.Falcon)
					{
						rigidbody.AddForce(vector * this.Strength);
					}
					else if (this.Bancho)
					{
						rigidbody.AddForce(vector.x * this.Strength, 5000f, vector.z * this.Strength);
					}
					else
					{
						rigidbody.AddForce(vector.x * this.Strength, 10000f, vector.z * this.Strength);
					}
				}
			}
		}
		if (this.Destructive && other.gameObject.layer != 2 && other.gameObject.layer != 8 && other.gameObject.layer != 9 && other.gameObject.layer != 13 && other.gameObject.layer != 17)
		{
			GameObject gameObject = null;
			StudentScript component2 = other.gameObject.transform.root.GetComponent<StudentScript>();
			if (component2 != null)
			{
				if (component2.StudentID <= 1)
				{
					gameObject = component2.gameObject;
				}
			}
			else
			{
				gameObject = other.gameObject;
			}
			if (gameObject != null)
			{
				UnityEngine.Object.Instantiate<GameObject>(this.FalconExplosion, base.transform.position + new Vector3(0f, 0f, 0f), Quaternion.identity);
				UnityEngine.Object.Destroy(gameObject);
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}
	}

	// Token: 0x04001C1B RID: 7195
	public GameObject FalconExplosion;

	// Token: 0x04001C1C RID: 7196
	public Rigidbody MyRigidbody;

	// Token: 0x04001C1D RID: 7197
	public Collider MyCollider;

	// Token: 0x04001C1E RID: 7198
	public float Strength = 100f;

	// Token: 0x04001C1F RID: 7199
	public float Speed = 100f;

	// Token: 0x04001C20 RID: 7200
	public bool Destructive;

	// Token: 0x04001C21 RID: 7201
	public bool IgnoreTime;

	// Token: 0x04001C22 RID: 7202
	public bool Shipgirl;

	// Token: 0x04001C23 RID: 7203
	public bool Bancho;

	// Token: 0x04001C24 RID: 7204
	public bool Falcon;

	// Token: 0x04001C25 RID: 7205
	public bool Mecha;

	// Token: 0x04001C26 RID: 7206
	public float TimeLimit = 0.5f;

	// Token: 0x04001C27 RID: 7207
	public float Timer;
}
