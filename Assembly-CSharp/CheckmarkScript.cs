using System;
using UnityEngine;

// Token: 0x02000499 RID: 1177
public class CheckmarkScript : MonoBehaviour
{
	// Token: 0x06001E33 RID: 7731 RVA: 0x0017C293 File Offset: 0x0017A493
	private void Start()
	{
		while (this.ID < this.Checkmarks.Length)
		{
			this.Checkmarks[this.ID].SetActive(false);
			this.ID++;
		}
		this.ID = 0;
	}

	// Token: 0x06001E34 RID: 7732 RVA: 0x0017C2D0 File Offset: 0x0017A4D0
	private void Update()
	{
		if (Input.GetKeyDown("space") && this.ButtonPresses < 26)
		{
			this.ButtonPresses++;
			this.ID = UnityEngine.Random.Range(0, this.Checkmarks.Length - 4);
			while (this.Checkmarks[this.ID].active)
			{
				this.ID = UnityEngine.Random.Range(0, this.Checkmarks.Length - 4);
			}
			this.Checkmarks[this.ID].SetActive(true);
		}
	}

	// Token: 0x04003C6A RID: 15466
	public GameObject[] Checkmarks;

	// Token: 0x04003C6B RID: 15467
	public int ButtonPresses;

	// Token: 0x04003C6C RID: 15468
	public int ID;
}
