using System;
using UnityEngine;

// Token: 0x0200048E RID: 1166
public class YanvaniaWitchScript : MonoBehaviour
{
	// Token: 0x06001E07 RID: 7687 RVA: 0x00178288 File Offset: 0x00176488
	private void Update()
	{
		Animation component = this.Character.GetComponent<Animation>();
		if (this.AttackTimer < 10f)
		{
			this.AttackTimer += Time.deltaTime;
			if (this.AttackTimer > 0.8f && !this.CastSpell)
			{
				this.CastSpell = true;
				UnityEngine.Object.Instantiate<GameObject>(this.BlackHole, base.transform.position + Vector3.up * 3f + Vector3.right * 6f, Quaternion.identity);
				UnityEngine.Object.Instantiate<GameObject>(this.GroundImpact, base.transform.position + Vector3.right * 1.15f, Quaternion.identity);
			}
			if (component["Staff Spell Ground"].time >= component["Staff Spell Ground"].length)
			{
				component.CrossFade("Staff Stance");
				this.Casting = false;
			}
		}
		else if (Vector3.Distance(base.transform.position, this.Yanmont.transform.position) < 5f)
		{
			this.AttackTimer = 0f;
			this.Casting = true;
			this.CastSpell = false;
			component["Staff Spell Ground"].time = 0f;
			component.CrossFade("Staff Spell Ground");
		}
		if (!this.Casting && component["Receive Damage"].time >= component["Receive Damage"].length)
		{
			component.CrossFade("Staff Stance");
		}
		this.HitReactTimer += Time.deltaTime * 10f;
	}

	// Token: 0x06001E08 RID: 7688 RVA: 0x0017843C File Offset: 0x0017663C
	private void OnTriggerEnter(Collider other)
	{
		if (this.HP > 0f)
		{
			if (other.gameObject.tag == "Player")
			{
				this.Yanmont.TakeDamage(5);
			}
			if (other.gameObject.name == "Heart")
			{
				Animation component = this.Character.GetComponent<Animation>();
				if (!this.Casting)
				{
					component["Receive Damage"].time = 0f;
					component.Play("Receive Damage");
				}
				if (this.HitReactTimer >= 1f)
				{
					UnityEngine.Object.Instantiate<GameObject>(this.HitEffect, other.transform.position, Quaternion.identity);
					this.HitReactTimer = 0f;
					this.HP -= 5f + ((float)this.Yanmont.Level * 5f - 5f);
					AudioSource component2 = base.GetComponent<AudioSource>();
					if (this.HP <= 0f)
					{
						component2.PlayOneShot(this.DeathScream);
						component.Play("Die 2");
						this.Yanmont.EXP += 100f;
						base.enabled = false;
						UnityEngine.Object.Destroy(this.Wall);
						return;
					}
					component2.PlayOneShot(this.HitSound);
				}
			}
		}
	}

	// Token: 0x04003BAD RID: 15277
	public YanvaniaYanmontScript Yanmont;

	// Token: 0x04003BAE RID: 15278
	public GameObject GroundImpact;

	// Token: 0x04003BAF RID: 15279
	public GameObject BlackHole;

	// Token: 0x04003BB0 RID: 15280
	public GameObject Character;

	// Token: 0x04003BB1 RID: 15281
	public GameObject HitEffect;

	// Token: 0x04003BB2 RID: 15282
	public GameObject Wall;

	// Token: 0x04003BB3 RID: 15283
	public AudioClip DeathScream;

	// Token: 0x04003BB4 RID: 15284
	public AudioClip HitSound;

	// Token: 0x04003BB5 RID: 15285
	public float HitReactTimer;

	// Token: 0x04003BB6 RID: 15286
	public float AttackTimer = 10f;

	// Token: 0x04003BB7 RID: 15287
	public float HP = 100f;

	// Token: 0x04003BB8 RID: 15288
	public bool CastSpell;

	// Token: 0x04003BB9 RID: 15289
	public bool Casting;
}
