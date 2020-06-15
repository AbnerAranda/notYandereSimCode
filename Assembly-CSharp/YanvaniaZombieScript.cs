using System;
using UnityEngine;

// Token: 0x02000490 RID: 1168
public class YanvaniaZombieScript : MonoBehaviour
{
	// Token: 0x06001E18 RID: 7704 RVA: 0x0017A448 File Offset: 0x00178648
	private void Start()
	{
		base.transform.eulerAngles = new Vector3(base.transform.eulerAngles.x, (this.Yanmont.transform.position.x > base.transform.position.x) ? 90f : -90f, base.transform.eulerAngles.z);
		UnityEngine.Object.Instantiate<GameObject>(this.ZombieEffect, base.transform.position, Quaternion.identity);
		base.transform.position = new Vector3(base.transform.position.x, -0.63f, base.transform.position.z);
		Animation component = this.Character.GetComponent<Animation>();
		component["getup1"].speed = 2f;
		component.Play("getup1");
		base.GetComponent<AudioSource>().PlayOneShot(this.RisingSound);
		this.MyRenderer.material.mainTexture = this.Textures[UnityEngine.Random.Range(0, 22)];
		this.MyCollider.enabled = false;
	}

	// Token: 0x06001E19 RID: 7705 RVA: 0x0017A570 File Offset: 0x00178770
	private void Update()
	{
		AudioSource component = base.GetComponent<AudioSource>();
		if (this.Dying)
		{
			this.DeathTimer += Time.deltaTime;
			if (this.DeathTimer > 1f)
			{
				if (!this.EffectSpawned)
				{
					UnityEngine.Object.Instantiate<GameObject>(this.ZombieEffect, base.transform.position, Quaternion.identity);
					component.PlayOneShot(this.SinkingSound);
					this.EffectSpawned = true;
				}
				base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y - Time.deltaTime, base.transform.position.z);
				if (base.transform.position.y < -0.4f)
				{
					UnityEngine.Object.Destroy(base.gameObject);
				}
			}
		}
		else
		{
			Animation component2 = this.Character.GetComponent<Animation>();
			if (this.Sink)
			{
				base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y - Time.deltaTime * 0.74f, base.transform.position.z);
				if (base.transform.position.y < -0.63f)
				{
					UnityEngine.Object.Destroy(base.gameObject);
				}
			}
			else if (this.Walk)
			{
				this.WalkTimer += Time.deltaTime;
				if (this.WalkType == 1)
				{
					base.transform.Translate(Vector3.forward * Time.deltaTime * this.WalkSpeed1);
					component2.CrossFade("walk1");
				}
				else
				{
					base.transform.Translate(Vector3.forward * Time.deltaTime * this.WalkSpeed2);
					component2.CrossFade("walk2");
				}
				if (this.WalkTimer > 10f)
				{
					this.SinkNow();
				}
			}
			else
			{
				this.Timer += Time.deltaTime;
				if (base.transform.position.y < 0f)
				{
					base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y + Time.deltaTime * 0.74f, base.transform.position.z);
					if (base.transform.position.y > 0f)
					{
						base.transform.position = new Vector3(base.transform.position.x, 0f, base.transform.position.z);
					}
				}
				if (this.Timer > 0.85f)
				{
					this.Walk = true;
					this.MyCollider.enabled = true;
					this.WalkType = UnityEngine.Random.Range(1, 3);
				}
			}
			if (base.transform.position.x < this.LeftBoundary)
			{
				base.transform.position = new Vector3(this.LeftBoundary, base.transform.position.y, base.transform.position.z);
				this.SinkNow();
			}
			if (base.transform.position.x > this.RightBoundary)
			{
				base.transform.position = new Vector3(this.RightBoundary, base.transform.position.y, base.transform.position.z);
				this.SinkNow();
			}
			if (this.HP <= 0)
			{
				UnityEngine.Object.Instantiate<GameObject>(this.DeathEffect, new Vector3(base.transform.position.x, base.transform.position.y + 1f, base.transform.position.z), Quaternion.identity);
				component2.Play("die");
				component.PlayOneShot(this.DeathSound);
				this.MyCollider.enabled = false;
				this.Yanmont.EXP += 10f;
				this.Dying = true;
			}
		}
		if (this.HitReactTimer < 1f)
		{
			this.MyRenderer.material.color = new Color(1f, this.HitReactTimer, this.HitReactTimer, 1f);
			this.HitReactTimer += Time.deltaTime * 10f;
			if (this.HitReactTimer >= 1f)
			{
				this.MyRenderer.material.color = new Color(1f, 1f, 1f, 1f);
			}
		}
	}

	// Token: 0x06001E1A RID: 7706 RVA: 0x0017AA40 File Offset: 0x00178C40
	private void SinkNow()
	{
		Animation component = this.Character.GetComponent<Animation>();
		component["getup1"].time = component["getup1"].length;
		component["getup1"].speed = -2f;
		component.Play("getup1");
		base.GetComponent<AudioSource>().PlayOneShot(this.SinkingSound);
		UnityEngine.Object.Instantiate<GameObject>(this.ZombieEffect, base.transform.position, Quaternion.identity);
		this.MyCollider.enabled = false;
		this.Sink = true;
	}

	// Token: 0x06001E1B RID: 7707 RVA: 0x0017AADC File Offset: 0x00178CDC
	private void OnTriggerEnter(Collider other)
	{
		if (!this.Dying)
		{
			if (other.gameObject.tag == "Player")
			{
				this.Yanmont.TakeDamage(5);
			}
			if (other.gameObject.name == "Heart" && this.HitReactTimer >= 1f)
			{
				UnityEngine.Object.Instantiate<GameObject>(this.HitEffect, other.transform.position, Quaternion.identity);
				base.GetComponent<AudioSource>().PlayOneShot(this.HitSound);
				this.HitReactTimer = 0f;
				this.HP -= 20 + (this.Yanmont.Level * 5 - 5);
			}
		}
	}

	// Token: 0x04003C0F RID: 15375
	public GameObject ZombieEffect;

	// Token: 0x04003C10 RID: 15376
	public GameObject BloodEffect;

	// Token: 0x04003C11 RID: 15377
	public GameObject DeathEffect;

	// Token: 0x04003C12 RID: 15378
	public GameObject HitEffect;

	// Token: 0x04003C13 RID: 15379
	public GameObject Character;

	// Token: 0x04003C14 RID: 15380
	public YanvaniaYanmontScript Yanmont;

	// Token: 0x04003C15 RID: 15381
	public int HP;

	// Token: 0x04003C16 RID: 15382
	public float WalkSpeed1;

	// Token: 0x04003C17 RID: 15383
	public float WalkSpeed2;

	// Token: 0x04003C18 RID: 15384
	public float Damage;

	// Token: 0x04003C19 RID: 15385
	public float HitReactTimer;

	// Token: 0x04003C1A RID: 15386
	public float DeathTimer;

	// Token: 0x04003C1B RID: 15387
	public float WalkTimer;

	// Token: 0x04003C1C RID: 15388
	public float Timer;

	// Token: 0x04003C1D RID: 15389
	public int HitReactState;

	// Token: 0x04003C1E RID: 15390
	public int WalkType;

	// Token: 0x04003C1F RID: 15391
	public float LeftBoundary;

	// Token: 0x04003C20 RID: 15392
	public float RightBoundary;

	// Token: 0x04003C21 RID: 15393
	public bool EffectSpawned;

	// Token: 0x04003C22 RID: 15394
	public bool Dying;

	// Token: 0x04003C23 RID: 15395
	public bool Sink;

	// Token: 0x04003C24 RID: 15396
	public bool Walk;

	// Token: 0x04003C25 RID: 15397
	public Texture[] Textures;

	// Token: 0x04003C26 RID: 15398
	public Renderer MyRenderer;

	// Token: 0x04003C27 RID: 15399
	public Collider MyCollider;

	// Token: 0x04003C28 RID: 15400
	public AudioClip DeathSound;

	// Token: 0x04003C29 RID: 15401
	public AudioClip HitSound;

	// Token: 0x04003C2A RID: 15402
	public AudioClip RisingSound;

	// Token: 0x04003C2B RID: 15403
	public AudioClip SinkingSound;
}
