using System;
using UnityEngine;

// Token: 0x020002DE RID: 734
public class HairBladeScript : MonoBehaviour
{
	// Token: 0x060016F8 RID: 5880 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x060016F9 RID: 5881 RVA: 0x000BED8C File Offset: 0x000BCF8C
	private void OnTriggerEnter(Collider other)
	{
		GameObject gameObject = other.gameObject.transform.root.gameObject;
		if (gameObject.GetComponent<StudentScript>() != null)
		{
			this.Student = gameObject.GetComponent<StudentScript>();
			if (this.Student.StudentID != 1 && this.Student.Alive)
			{
				this.Student.DeathType = DeathType.EasterEgg;
				UnityEngine.Object.Instantiate<GameObject>(this.Student.Male ? this.MaleBloodyScream : this.FemaleBloodyScream, this.Student.transform.position + Vector3.up, Quaternion.identity);
				this.Student.BecomeRagdoll();
				this.Student.Ragdoll.Dismember();
				base.GetComponent<AudioSource>().Play();
			}
		}
	}

	// Token: 0x04001E74 RID: 7796
	public GameObject FemaleBloodyScream;

	// Token: 0x04001E75 RID: 7797
	public GameObject MaleBloodyScream;

	// Token: 0x04001E76 RID: 7798
	public Vector3 PreviousPosition;

	// Token: 0x04001E77 RID: 7799
	public Collider MyCollider;

	// Token: 0x04001E78 RID: 7800
	public float Timer;

	// Token: 0x04001E79 RID: 7801
	public StudentScript Student;
}
