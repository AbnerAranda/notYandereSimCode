using System;
using UnityEngine;

// Token: 0x02000337 RID: 823
public class MiyukiEnemyScript : MonoBehaviour
{
	// Token: 0x0600184F RID: 6223 RVA: 0x000DA66E File Offset: 0x000D886E
	private void Start()
	{
		base.transform.position = this.SpawnPoints[this.ID].position;
		base.transform.rotation = this.SpawnPoints[this.ID].rotation;
	}

	// Token: 0x06001850 RID: 6224 RVA: 0x000DA6AC File Offset: 0x000D88AC
	private void Update()
	{
		if (this.Enemy.activeInHierarchy)
		{
			if (!this.Down)
			{
				this.Float += Time.deltaTime * this.Speed;
				if (this.Float > this.Limit)
				{
					this.Down = true;
				}
			}
			else
			{
				this.Float -= Time.deltaTime * this.Speed;
				if (this.Float < -1f * this.Limit)
				{
					this.Down = false;
				}
			}
			this.Enemy.transform.position += new Vector3(0f, this.Float * Time.deltaTime, 0f);
			if (this.Enemy.transform.position.y > this.SpawnPoints[this.ID].position.y + 1.5f)
			{
				this.Enemy.transform.position = new Vector3(this.Enemy.transform.position.x, this.SpawnPoints[this.ID].position.y + 1.5f, this.Enemy.transform.position.z);
			}
			if (this.Enemy.transform.position.y < this.SpawnPoints[this.ID].position.y + 0.5f)
			{
				this.Enemy.transform.position = new Vector3(this.Enemy.transform.position.x, this.SpawnPoints[this.ID].position.y + 0.5f, this.Enemy.transform.position.z);
				return;
			}
		}
		else
		{
			this.RespawnTimer += Time.deltaTime;
			if (this.RespawnTimer > 5f)
			{
				base.transform.position = this.SpawnPoints[this.ID].position;
				base.transform.rotation = this.SpawnPoints[this.ID].rotation;
				this.Enemy.SetActive(true);
				this.RespawnTimer = 0f;
			}
		}
	}

	// Token: 0x06001851 RID: 6225 RVA: 0x000DA900 File Offset: 0x000D8B00
	private void OnTriggerEnter(Collider other)
	{
		if (this.Enemy.activeInHierarchy && other.gameObject.tag == "missile")
		{
			UnityEngine.Object.Instantiate<GameObject>(this.HitEffect, other.transform.position, Quaternion.identity);
			UnityEngine.Object.Destroy(other.gameObject);
			this.Health -= 1f;
			if (this.Health == 0f)
			{
				UnityEngine.Object.Instantiate<GameObject>(this.DeathEffect, other.transform.position, Quaternion.identity);
				this.Enemy.SetActive(false);
				this.Health = 50f;
				this.ID++;
				if (this.ID >= this.SpawnPoints.Length)
				{
					this.ID = 0;
				}
			}
		}
	}

	// Token: 0x0400235A RID: 9050
	public float Float;

	// Token: 0x0400235B RID: 9051
	public float Limit;

	// Token: 0x0400235C RID: 9052
	public float Speed;

	// Token: 0x0400235D RID: 9053
	public bool Dead;

	// Token: 0x0400235E RID: 9054
	public bool Down;

	// Token: 0x0400235F RID: 9055
	public GameObject DeathEffect;

	// Token: 0x04002360 RID: 9056
	public GameObject HitEffect;

	// Token: 0x04002361 RID: 9057
	public GameObject Enemy;

	// Token: 0x04002362 RID: 9058
	public Transform[] SpawnPoints;

	// Token: 0x04002363 RID: 9059
	public float RespawnTimer;

	// Token: 0x04002364 RID: 9060
	public float Health;

	// Token: 0x04002365 RID: 9061
	public int ID;
}
