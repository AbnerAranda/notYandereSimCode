using System;
using UnityEngine;

// Token: 0x0200047B RID: 1147
public class YanvaniaBlackHoleScript : MonoBehaviour
{
	// Token: 0x06001DCD RID: 7629 RVA: 0x00174598 File Offset: 0x00172798
	private void Update()
	{
		this.Timer += Time.deltaTime;
		if (this.Timer > 1f)
		{
			this.SpawnTimer -= Time.deltaTime;
			if (this.SpawnTimer <= 0f && this.Attacks < 5)
			{
				UnityEngine.Object.Instantiate<GameObject>(this.BlackHoleAttack, base.transform.position, Quaternion.identity);
				this.SpawnTimer = 0.5f;
				this.Attacks++;
			}
		}
	}

	// Token: 0x04003B0C RID: 15116
	public GameObject BlackHoleAttack;

	// Token: 0x04003B0D RID: 15117
	public int Attacks;

	// Token: 0x04003B0E RID: 15118
	public float SpawnTimer;

	// Token: 0x04003B0F RID: 15119
	public float Timer;
}
