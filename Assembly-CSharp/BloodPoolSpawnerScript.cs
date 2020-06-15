using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020000E0 RID: 224
public class BloodPoolSpawnerScript : MonoBehaviour
{
	// Token: 0x06000A5B RID: 2651 RVA: 0x00055B3C File Offset: 0x00053D3C
	public void Start()
	{
		this.PoolsSpawned = this.Ragdoll.Student.BloodPoolsSpawned;
		if (SceneManager.GetActiveScene().name == "SchoolScene")
		{
			this.GardenArea = GameObject.Find("GardenArea").GetComponent<Collider>();
			this.NEStairs = GameObject.Find("NEStairs").GetComponent<Collider>();
			this.NWStairs = GameObject.Find("NWStairs").GetComponent<Collider>();
			this.SEStairs = GameObject.Find("SEStairs").GetComponent<Collider>();
			this.SWStairs = GameObject.Find("SWStairs").GetComponent<Collider>();
		}
		this.BloodParent = GameObject.Find("BloodParent").transform;
		this.Positions = new Vector3[5];
		this.Positions[0] = Vector3.zero;
		this.Positions[1] = new Vector3(0.5f, 0.012f, 0f);
		this.Positions[2] = new Vector3(-0.5f, 0.012f, 0f);
		this.Positions[3] = new Vector3(0f, 0.012f, 0.5f);
		this.Positions[4] = new Vector3(0f, 0.012f, -0.5f);
	}

	// Token: 0x06000A5C RID: 2652 RVA: 0x00055C93 File Offset: 0x00053E93
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == "BloodPool(Clone)")
		{
			this.LastBloodPool = other.gameObject;
			this.NearbyBlood++;
		}
	}

	// Token: 0x06000A5D RID: 2653 RVA: 0x00055CC6 File Offset: 0x00053EC6
	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.name == "BloodPool(Clone)")
		{
			this.NearbyBlood--;
		}
	}

	// Token: 0x06000A5E RID: 2654 RVA: 0x00055CF0 File Offset: 0x00053EF0
	private void Update()
	{
		if (!this.Falling)
		{
			if (this.MyCollider.enabled)
			{
				if (this.Timer > 0f)
				{
					this.Timer -= Time.deltaTime;
				}
				this.SetHeight();
				Vector3 position = base.transform.position;
				if (SceneManager.GetActiveScene().name == "SchoolScene")
				{
					this.CanSpawn = (!this.GardenArea.bounds.Contains(position) && !this.NEStairs.bounds.Contains(position) && !this.NWStairs.bounds.Contains(position) && !this.SEStairs.bounds.Contains(position) && !this.SWStairs.bounds.Contains(position));
				}
				if (this.CanSpawn && position.y < this.Height + 0.333333343f)
				{
					if (this.NearbyBlood > 0 && this.LastBloodPool == null)
					{
						this.NearbyBlood--;
					}
					if (this.NearbyBlood < 1 && this.Timer <= 0f)
					{
						this.Timer = 0.1f;
						if (this.PoolsSpawned < 10)
						{
							GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.BloodPool, new Vector3(position.x, this.Height + 0.012f, position.z), Quaternion.identity);
							gameObject.transform.localEulerAngles = new Vector3(90f, UnityEngine.Random.Range(0f, 360f), 0f);
							gameObject.transform.parent = this.BloodParent;
							this.PoolsSpawned++;
							this.Ragdoll.Student.BloodPoolsSpawned++;
							return;
						}
						if (this.PoolsSpawned < 20)
						{
							GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.BloodPool, new Vector3(position.x, this.Height + 0.012f, position.z), Quaternion.identity);
							gameObject2.transform.localEulerAngles = new Vector3(90f, UnityEngine.Random.Range(0f, 360f), 0f);
							gameObject2.transform.parent = this.BloodParent;
							this.PoolsSpawned++;
							this.Ragdoll.Student.BloodPoolsSpawned++;
							gameObject2.GetComponent<BloodPoolScript>().TargetSize = 1f - (float)(this.PoolsSpawned - 10) * 0.1f;
							if (this.PoolsSpawned == 20)
							{
								base.gameObject.SetActive(false);
								return;
							}
						}
					}
				}
			}
		}
		else
		{
			this.FallTimer += Time.deltaTime;
			if (this.FallTimer > 10f)
			{
				this.Falling = false;
			}
		}
	}

	// Token: 0x06000A5F RID: 2655 RVA: 0x00055FDC File Offset: 0x000541DC
	public void SpawnBigPool()
	{
		this.SetHeight();
		Vector3 a = new Vector3(this.Hips.position.x, this.Height + 0.012f, this.Hips.position.z);
		for (int i = 0; i < 5; i++)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.BloodPool, a + this.Positions[i], Quaternion.identity);
			gameObject.transform.localEulerAngles = new Vector3(90f, UnityEngine.Random.Range(0f, 360f), 0f);
			gameObject.transform.parent = this.BloodParent;
		}
	}

	// Token: 0x06000A60 RID: 2656 RVA: 0x0005608C File Offset: 0x0005428C
	private void SpawnRow(Transform Location)
	{
		Vector3 position = Location.position;
		Vector3 forward = Location.forward;
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.BloodPool, position + forward * 2f, Quaternion.identity);
		gameObject.transform.localEulerAngles = new Vector3(90f, UnityEngine.Random.Range(0f, 360f), 0f);
		gameObject.transform.parent = this.BloodParent;
		GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.BloodPool, position + forward * 2.5f, Quaternion.identity);
		gameObject2.transform.localEulerAngles = new Vector3(90f, UnityEngine.Random.Range(0f, 360f), 0f);
		gameObject2.transform.parent = this.BloodParent;
		GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(this.BloodPool, position + forward * 3f, Quaternion.identity);
		gameObject3.transform.localEulerAngles = new Vector3(90f, UnityEngine.Random.Range(0f, 360f), 0f);
		gameObject3.transform.parent = this.BloodParent;
	}

	// Token: 0x06000A61 RID: 2657 RVA: 0x000561B8 File Offset: 0x000543B8
	public void SpawnPool(Transform Location)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.BloodPool, Location.position + Location.forward + new Vector3(0f, 0.0001f, 0f), Quaternion.identity);
		gameObject.transform.localEulerAngles = new Vector3(90f, UnityEngine.Random.Range(0f, 360f), 0f);
		gameObject.transform.parent = this.BloodParent;
	}

	// Token: 0x06000A62 RID: 2658 RVA: 0x00056238 File Offset: 0x00054438
	private void SetHeight()
	{
		float y = base.transform.position.y;
		if (y < 4f)
		{
			this.Height = 0f;
			return;
		}
		if (y < 8f)
		{
			this.Height = 4f;
			return;
		}
		if (y < 12f)
		{
			this.Height = 8f;
			return;
		}
		this.Height = 12f;
	}

	// Token: 0x04000AB5 RID: 2741
	public RagdollScript Ragdoll;

	// Token: 0x04000AB6 RID: 2742
	public GameObject LastBloodPool;

	// Token: 0x04000AB7 RID: 2743
	public GameObject BloodPool;

	// Token: 0x04000AB8 RID: 2744
	public Transform BloodParent;

	// Token: 0x04000AB9 RID: 2745
	public Transform Hips;

	// Token: 0x04000ABA RID: 2746
	public Collider MyCollider;

	// Token: 0x04000ABB RID: 2747
	public Collider GardenArea;

	// Token: 0x04000ABC RID: 2748
	public Collider NEStairs;

	// Token: 0x04000ABD RID: 2749
	public Collider NWStairs;

	// Token: 0x04000ABE RID: 2750
	public Collider SEStairs;

	// Token: 0x04000ABF RID: 2751
	public Collider SWStairs;

	// Token: 0x04000AC0 RID: 2752
	public Vector3[] Positions;

	// Token: 0x04000AC1 RID: 2753
	public bool CanSpawn;

	// Token: 0x04000AC2 RID: 2754
	public bool Falling;

	// Token: 0x04000AC3 RID: 2755
	public int PoolsSpawned;

	// Token: 0x04000AC4 RID: 2756
	public int NearbyBlood;

	// Token: 0x04000AC5 RID: 2757
	public float FallTimer;

	// Token: 0x04000AC6 RID: 2758
	public float Height;

	// Token: 0x04000AC7 RID: 2759
	public float Timer;

	// Token: 0x04000AC8 RID: 2760
	public LayerMask Mask;
}
