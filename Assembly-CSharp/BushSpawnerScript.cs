using System;
using UnityEngine;

// Token: 0x020000F5 RID: 245
public class BushSpawnerScript : MonoBehaviour
{
	// Token: 0x06000AA3 RID: 2723 RVA: 0x00058A5C File Offset: 0x00056C5C
	private void Update()
	{
		if (Input.GetKeyDown("z"))
		{
			this.Begin = true;
		}
		if (this.Begin)
		{
			UnityEngine.Object.Instantiate<GameObject>(this.Bush, new Vector3(UnityEngine.Random.Range(-16f, 16f), UnityEngine.Random.Range(0f, 4f), UnityEngine.Random.Range(-16f, 16f)), Quaternion.identity);
		}
	}

	// Token: 0x04000B55 RID: 2901
	public GameObject Bush;

	// Token: 0x04000B56 RID: 2902
	public bool Begin;
}
