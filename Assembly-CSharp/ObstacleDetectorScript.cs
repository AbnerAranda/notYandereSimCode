using System;
using UnityEngine;

// Token: 0x0200034C RID: 844
public class ObstacleDetectorScript : MonoBehaviour
{
	// Token: 0x060018A2 RID: 6306 RVA: 0x000E23FF File Offset: 0x000E05FF
	private void Start()
	{
		this.ControllerX.SetActive(false);
		this.KeyboardX.SetActive(false);
	}

	// Token: 0x0400244D RID: 9293
	public YandereScript Yandere;

	// Token: 0x0400244E RID: 9294
	public GameObject ControllerX;

	// Token: 0x0400244F RID: 9295
	public GameObject KeyboardX;

	// Token: 0x04002450 RID: 9296
	public Collider[] ObstacleArray;

	// Token: 0x04002451 RID: 9297
	public int Obstacles;

	// Token: 0x04002452 RID: 9298
	public bool Add;

	// Token: 0x04002453 RID: 9299
	public int ID;
}
