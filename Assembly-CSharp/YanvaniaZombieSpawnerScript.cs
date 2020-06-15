using System;
using UnityEngine;

// Token: 0x02000491 RID: 1169
public class YanvaniaZombieSpawnerScript : MonoBehaviour
{
	// Token: 0x06001E1D RID: 7709 RVA: 0x0017AB94 File Offset: 0x00178D94
	private void Update()
	{
		if (this.Yanmont.transform.position.y > 0f)
		{
			this.ID = 0;
			this.SpawnTimer += Time.deltaTime;
			if (this.SpawnTimer > 1f)
			{
				while (this.ID < 4)
				{
					if (this.Zombies[this.ID] == null)
					{
						this.SpawnSide = UnityEngine.Random.Range(1, 3);
						if (this.Yanmont.transform.position.x < this.LeftBoundary + 5f)
						{
							this.SpawnSide = 2;
						}
						if (this.Yanmont.transform.position.x > this.RightBoundary - 5f)
						{
							this.SpawnSide = 1;
						}
						if (this.Yanmont.transform.position.x < this.LeftBoundary)
						{
							this.RelativePoint = this.LeftBoundary;
						}
						else if (this.Yanmont.transform.position.x > this.RightBoundary)
						{
							this.RelativePoint = this.RightBoundary;
						}
						else
						{
							this.RelativePoint = this.Yanmont.transform.position.x;
						}
						if (this.SpawnSide == 1)
						{
							this.SpawnPoints[0].x = this.RelativePoint - 2.5f;
							this.SpawnPoints[1].x = this.RelativePoint - 3.5f;
							this.SpawnPoints[2].x = this.RelativePoint - 4.5f;
							this.SpawnPoints[3].x = this.RelativePoint - 5.5f;
						}
						else
						{
							this.SpawnPoints[0].x = this.RelativePoint + 2.5f;
							this.SpawnPoints[1].x = this.RelativePoint + 3.5f;
							this.SpawnPoints[2].x = this.RelativePoint + 4.5f;
							this.SpawnPoints[3].x = this.RelativePoint + 5.5f;
						}
						this.Zombies[this.ID] = UnityEngine.Object.Instantiate<GameObject>(this.Zombie, this.SpawnPoints[this.ID], Quaternion.identity);
						this.NewZombieScript = this.Zombies[this.ID].GetComponent<YanvaniaZombieScript>();
						this.NewZombieScript.LeftBoundary = this.LeftBoundary;
						this.NewZombieScript.RightBoundary = this.RightBoundary;
						this.NewZombieScript.Yanmont = this.Yanmont;
						break;
					}
					this.ID++;
				}
				this.SpawnTimer = 0f;
			}
		}
	}

	// Token: 0x04003C2C RID: 15404
	public YanvaniaZombieScript NewZombieScript;

	// Token: 0x04003C2D RID: 15405
	public GameObject Zombie;

	// Token: 0x04003C2E RID: 15406
	public YanvaniaYanmontScript Yanmont;

	// Token: 0x04003C2F RID: 15407
	public float SpawnTimer;

	// Token: 0x04003C30 RID: 15408
	public float RelativePoint;

	// Token: 0x04003C31 RID: 15409
	public float RightBoundary;

	// Token: 0x04003C32 RID: 15410
	public float LeftBoundary;

	// Token: 0x04003C33 RID: 15411
	public int SpawnSide;

	// Token: 0x04003C34 RID: 15412
	public int ID;

	// Token: 0x04003C35 RID: 15413
	public GameObject[] Zombies;

	// Token: 0x04003C36 RID: 15414
	public Vector3[] SpawnPoints;
}
