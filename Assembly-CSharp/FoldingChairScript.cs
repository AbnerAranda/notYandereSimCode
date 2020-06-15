using System;
using UnityEngine;

// Token: 0x020004AC RID: 1196
public class FoldingChairScript : MonoBehaviour
{
	// Token: 0x06001E61 RID: 7777 RVA: 0x0017E2FC File Offset: 0x0017C4FC
	private void Start()
	{
		int num = UnityEngine.Random.Range(0, this.Student.Length);
		UnityEngine.Object.Instantiate<GameObject>(this.Student[num], base.transform.position - new Vector3(0f, 0.4f, 0f), base.transform.rotation);
	}

	// Token: 0x04003CA9 RID: 15529
	public GameObject[] Student;
}
