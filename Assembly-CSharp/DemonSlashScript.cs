using System;
using UnityEngine;

// Token: 0x02000264 RID: 612
public class DemonSlashScript : MonoBehaviour
{
	// Token: 0x06001338 RID: 4920 RVA: 0x000A150F File Offset: 0x0009F70F
	private void Start()
	{
		this.MyAudio = base.GetComponent<AudioSource>();
	}

	// Token: 0x06001339 RID: 4921 RVA: 0x000A1520 File Offset: 0x0009F720
	private void Update()
	{
		if (this.MyCollider.enabled)
		{
			this.Timer += Time.deltaTime;
			if (this.Timer > 0.333333343f)
			{
				this.MyCollider.enabled = false;
				this.Timer = 0f;
			}
		}
	}

	// Token: 0x0600133A RID: 4922 RVA: 0x000A1570 File Offset: 0x0009F770
	private void OnTriggerEnter(Collider other)
	{
		Transform root = other.gameObject.transform.root;
		StudentScript component = root.gameObject.GetComponent<StudentScript>();
		if (component != null && component.StudentID != 1 && component.Alive)
		{
			component.DeathType = DeathType.EasterEgg;
			if (!component.Male)
			{
				UnityEngine.Object.Instantiate<GameObject>(this.FemaleBloodyScream, root.transform.position + Vector3.up, Quaternion.identity);
			}
			else
			{
				UnityEngine.Object.Instantiate<GameObject>(this.MaleBloodyScream, root.transform.position + Vector3.up, Quaternion.identity);
			}
			component.BecomeRagdoll();
			component.Ragdoll.Dismember();
			this.MyAudio.Play();
		}
	}

	// Token: 0x04001A0A RID: 6666
	public GameObject FemaleBloodyScream;

	// Token: 0x04001A0B RID: 6667
	public GameObject MaleBloodyScream;

	// Token: 0x04001A0C RID: 6668
	public AudioSource MyAudio;

	// Token: 0x04001A0D RID: 6669
	public Collider MyCollider;

	// Token: 0x04001A0E RID: 6670
	public float Timer;
}
