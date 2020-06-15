using System;
using UnityEngine;

// Token: 0x020003E2 RID: 994
public class SithBeamScript : MonoBehaviour
{
	// Token: 0x06001AB4 RID: 6836 RVA: 0x0010B2B4 File Offset: 0x001094B4
	private void Update()
	{
		if (this.Projectile)
		{
			base.transform.Translate(base.transform.forward * Time.deltaTime * 15f, Space.World);
		}
		this.Lifespan = Mathf.MoveTowards(this.Lifespan, 0f, Time.deltaTime);
		if (this.Lifespan == 0f)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06001AB5 RID: 6837 RVA: 0x0010B328 File Offset: 0x00109528
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 9)
		{
			StudentScript component = other.gameObject.GetComponent<StudentScript>();
			if (component != null && component.StudentID > 1)
			{
				AudioSource.PlayClipAtPoint(this.Hit, base.transform.position);
				this.RandomNumber = UnityEngine.Random.Range(0, 3);
				if (this.MalePain.Length != 0)
				{
					if (component.Male)
					{
						AudioSource.PlayClipAtPoint(this.MalePain[this.RandomNumber], base.transform.position);
					}
					else
					{
						AudioSource.PlayClipAtPoint(this.FemalePain[this.RandomNumber], base.transform.position);
					}
				}
				UnityEngine.Object.Instantiate<GameObject>(this.BloodEffect, component.transform.position + new Vector3(0f, 1f, 0f), Quaternion.identity);
				component.Health -= this.Damage;
				component.HealthBar.transform.parent.gameObject.SetActive(true);
				component.HealthBar.transform.localScale = new Vector3(component.Health / 100f, 1f, 1f);
				component.Character.transform.localScale = new Vector3(component.Character.transform.localScale.x * -1f, component.Character.transform.localScale.y, component.Character.transform.localScale.z);
				if (component.Health <= 0f)
				{
					component.DeathType = DeathType.EasterEgg;
					component.HealthBar.transform.parent.gameObject.SetActive(false);
					component.BecomeRagdoll();
					component.Ragdoll.AllRigidbodies[0].isKinematic = false;
				}
				else
				{
					component.CharacterAnimation[component.SithReactAnim].time = 0f;
					component.CharacterAnimation.Play(component.SithReactAnim);
					component.Pathfinding.canSearch = false;
					component.Pathfinding.canMove = false;
					component.HitReacting = true;
					component.Routine = false;
					component.Fleeing = false;
				}
				if (this.Projectile)
				{
					UnityEngine.Object.Destroy(base.gameObject);
				}
			}
		}
	}

	// Token: 0x04002AFF RID: 11007
	public GameObject BloodEffect;

	// Token: 0x04002B00 RID: 11008
	public Collider MyCollider;

	// Token: 0x04002B01 RID: 11009
	public float Damage = 10f;

	// Token: 0x04002B02 RID: 11010
	public float Lifespan;

	// Token: 0x04002B03 RID: 11011
	public int RandomNumber;

	// Token: 0x04002B04 RID: 11012
	public AudioClip Hit;

	// Token: 0x04002B05 RID: 11013
	public AudioClip[] FemalePain;

	// Token: 0x04002B06 RID: 11014
	public AudioClip[] MalePain;

	// Token: 0x04002B07 RID: 11015
	public bool Projectile;
}
