using System;
using UnityEngine;

// Token: 0x020002A9 RID: 681
public class FootprintSpawnerScript : MonoBehaviour
{
	// Token: 0x06001423 RID: 5155 RVA: 0x000B14F4 File Offset: 0x000AF6F4
	private void Start()
	{
		this.GardenArea = GameObject.Find("GardenArea").GetComponent<Collider>();
		this.PoolStairs = GameObject.Find("PoolStairs").GetComponent<Collider>();
		this.NEStairs = GameObject.Find("NEStairs").GetComponent<Collider>();
		this.NWStairs = GameObject.Find("NWStairs").GetComponent<Collider>();
		this.SEStairs = GameObject.Find("SEStairs").GetComponent<Collider>();
		this.SWStairs = GameObject.Find("SWStairs").GetComponent<Collider>();
	}

	// Token: 0x06001424 RID: 5156 RVA: 0x000B1580 File Offset: 0x000AF780
	private void Update()
	{
		if (this.Debugging)
		{
			Debug.Log(string.Concat(new string[]
			{
				"UpThreshold: ",
				(this.Yandere.transform.position.y + this.UpThreshold).ToString(),
				" | DownThreshold: ",
				(this.Yandere.transform.position.y + this.DownThreshold).ToString(),
				" | CurrentHeight: ",
				base.transform.position.y.ToString()
			}));
		}
		this.CanSpawn = (!this.GardenArea.bounds.Contains(base.transform.position) && !this.PoolStairs.bounds.Contains(base.transform.position) && !this.NEStairs.bounds.Contains(base.transform.position) && !this.NWStairs.bounds.Contains(base.transform.position) && !this.SEStairs.bounds.Contains(base.transform.position) && !this.SWStairs.bounds.Contains(base.transform.position));
		if (!this.FootUp)
		{
			if (base.transform.position.y > this.Yandere.transform.position.y + this.UpThreshold)
			{
				this.FootUp = true;
				return;
			}
		}
		else if (base.transform.position.y < this.Yandere.transform.position.y + this.DownThreshold)
		{
			if (this.Yandere.Stance.Current != StanceType.Crouching && this.Yandere.Stance.Current != StanceType.Crawling && this.Yandere.CanMove && !this.Yandere.NearSenpai && this.FootUp)
			{
				AudioSource component = base.GetComponent<AudioSource>();
				if (this.Yandere.Running)
				{
					component.clip = this.RunFootsteps[UnityEngine.Random.Range(0, this.RunFootsteps.Length)];
					component.volume = 0.15f;
					component.Play();
				}
				else
				{
					component.clip = this.WalkFootsteps[UnityEngine.Random.Range(0, this.WalkFootsteps.Length)];
					component.volume = 0.1f;
					component.Play();
				}
			}
			this.FootUp = false;
			if (this.CanSpawn && this.Bloodiness > 0)
			{
				if (base.transform.position.y > -1f && base.transform.position.y < 1f)
				{
					this.Height = 0f;
				}
				else if (base.transform.position.y > 3f && base.transform.position.y < 5f)
				{
					this.Height = 4f;
				}
				else if (base.transform.position.y > 7f && base.transform.position.y < 9f)
				{
					this.Height = 8f;
				}
				else if (base.transform.position.y > 11f && base.transform.position.y < 13f)
				{
					this.Height = 12f;
				}
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.BloodyFootprint, new Vector3(base.transform.position.x, this.Height + 0.012f, base.transform.position.z), Quaternion.identity);
				gameObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x, base.transform.eulerAngles.y, gameObject.transform.eulerAngles.z);
				gameObject.transform.GetChild(0).GetComponent<FootprintScript>().Yandere = this.Yandere;
				gameObject.transform.parent = this.BloodParent;
				this.Bloodiness--;
			}
		}
	}

	// Token: 0x04001C82 RID: 7298
	public YandereScript Yandere;

	// Token: 0x04001C83 RID: 7299
	public GameObject BloodyFootprint;

	// Token: 0x04001C84 RID: 7300
	public AudioClip[] WalkFootsteps;

	// Token: 0x04001C85 RID: 7301
	public AudioClip[] RunFootsteps;

	// Token: 0x04001C86 RID: 7302
	public Transform BloodParent;

	// Token: 0x04001C87 RID: 7303
	public Collider GardenArea;

	// Token: 0x04001C88 RID: 7304
	public Collider PoolStairs;

	// Token: 0x04001C89 RID: 7305
	public Collider NEStairs;

	// Token: 0x04001C8A RID: 7306
	public Collider NWStairs;

	// Token: 0x04001C8B RID: 7307
	public Collider SEStairs;

	// Token: 0x04001C8C RID: 7308
	public Collider SWStairs;

	// Token: 0x04001C8D RID: 7309
	public bool Debugging;

	// Token: 0x04001C8E RID: 7310
	public bool CanSpawn;

	// Token: 0x04001C8F RID: 7311
	public bool FootUp;

	// Token: 0x04001C90 RID: 7312
	public float DownThreshold;

	// Token: 0x04001C91 RID: 7313
	public float UpThreshold;

	// Token: 0x04001C92 RID: 7314
	public float Height;

	// Token: 0x04001C93 RID: 7315
	public int Bloodiness;

	// Token: 0x04001C94 RID: 7316
	public int Collisions;
}
