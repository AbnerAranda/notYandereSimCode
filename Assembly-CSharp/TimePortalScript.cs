using System;
using UnityEngine;

// Token: 0x02000425 RID: 1061
public class TimePortalScript : MonoBehaviour
{
	// Token: 0x06001C50 RID: 7248 RVA: 0x0015365C File Offset: 0x0015185C
	private void Update()
	{
		if (Input.GetKeyDown("space"))
		{
			this.Suck = true;
		}
		if (this.Suck)
		{
			UnityEngine.Object.Instantiate<GameObject>(this.BlackHole, base.transform.position + new Vector3(0f, 1f, 0f), Quaternion.identity);
			this.Timer += Time.deltaTime;
			if (this.Timer > 1.1f)
			{
				this.Delinquent[this.ID].Suck = true;
				this.Timer = 1f;
				this.ID++;
				if (this.ID > 9)
				{
					base.enabled = false;
				}
			}
		}
	}

	// Token: 0x04003522 RID: 13602
	public DelinquentScript[] Delinquent;

	// Token: 0x04003523 RID: 13603
	public GameObject BlackHole;

	// Token: 0x04003524 RID: 13604
	public float Timer;

	// Token: 0x04003525 RID: 13605
	public bool Suck;

	// Token: 0x04003526 RID: 13606
	public int ID;
}
