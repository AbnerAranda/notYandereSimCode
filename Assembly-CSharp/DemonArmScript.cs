using System;
using UnityEngine;

// Token: 0x02000261 RID: 609
public class DemonArmScript : MonoBehaviour
{
	// Token: 0x06001330 RID: 4912 RVA: 0x000A08DC File Offset: 0x0009EADC
	private void Start()
	{
		this.MyAnimation = base.GetComponent<Animation>();
		if (!this.Rising)
		{
			this.MyAnimation[this.IdleAnim].speed = this.AnimSpeed * 0.5f;
		}
		this.MyAnimation[this.AttackAnim].speed = 0f;
	}

	// Token: 0x06001331 RID: 4913 RVA: 0x000A093C File Offset: 0x0009EB3C
	private void Update()
	{
		if (!this.Rising)
		{
			if (!this.Attacking)
			{
				this.MyAnimation.CrossFade(this.IdleAnim);
				return;
			}
			this.AnimTime += 0.0166666675f;
			this.MyAnimation[this.AttackAnim].time = this.AnimTime;
			if (!this.Attacked)
			{
				if (this.MyAnimation[this.AttackAnim].time >= this.MyAnimation[this.AttackAnim].length * 0.15f)
				{
					this.ClawCollider.enabled = true;
					this.Attacked = true;
					return;
				}
			}
			else
			{
				if (this.MyAnimation[this.AttackAnim].time >= this.MyAnimation[this.AttackAnim].length * 0.4f)
				{
					this.ClawCollider.enabled = false;
				}
				if (this.MyAnimation[this.AttackAnim].time >= this.MyAnimation[this.AttackAnim].length)
				{
					this.MyAnimation.CrossFade(this.IdleAnim);
					this.ClawCollider.enabled = false;
					this.Attacking = false;
					this.Attacked = false;
					this.AnimTime = 0f;
					return;
				}
			}
		}
		else if (this.MyAnimation[this.AttackAnim].time > this.MyAnimation[this.AttackAnim].length)
		{
			this.Rising = false;
		}
	}

	// Token: 0x06001332 RID: 4914 RVA: 0x000A0ACC File Offset: 0x0009ECCC
	private void OnTriggerEnter(Collider other)
	{
		StudentScript component = other.gameObject.GetComponent<StudentScript>();
		if (component != null && component.StudentID > 1)
		{
			AudioSource component2 = base.GetComponent<AudioSource>();
			component2.clip = this.Whoosh;
			component2.pitch = UnityEngine.Random.Range(-0.9f, 1.1f);
			component2.Play();
			base.GetComponent<Animation>().CrossFade(this.AttackAnim);
			this.Attacking = true;
		}
	}

	// Token: 0x040019E2 RID: 6626
	public GameObject DismembermentCollider;

	// Token: 0x040019E3 RID: 6627
	public Animation MyAnimation;

	// Token: 0x040019E4 RID: 6628
	public Collider ClawCollider;

	// Token: 0x040019E5 RID: 6629
	public bool Attacking;

	// Token: 0x040019E6 RID: 6630
	public bool Attacked;

	// Token: 0x040019E7 RID: 6631
	public bool Rising = true;

	// Token: 0x040019E8 RID: 6632
	public string IdleAnim = "DemonArmIdle";

	// Token: 0x040019E9 RID: 6633
	public string AttackAnim = "DemonArmAttack";

	// Token: 0x040019EA RID: 6634
	public AudioClip Whoosh;

	// Token: 0x040019EB RID: 6635
	public float AnimSpeed = 1f;

	// Token: 0x040019EC RID: 6636
	public float AnimTime;
}
