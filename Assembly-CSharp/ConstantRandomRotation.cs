using System;
using UnityEngine;

// Token: 0x02000247 RID: 583
public class ConstantRandomRotation : MonoBehaviour
{
	// Token: 0x06001293 RID: 4755 RVA: 0x0008B9E8 File Offset: 0x00089BE8
	private void Update()
	{
		int num = UnityEngine.Random.Range(0, 360);
		int num2 = UnityEngine.Random.Range(0, 360);
		int num3 = UnityEngine.Random.Range(0, 360);
		base.transform.Rotate((float)num, (float)num2, (float)num3);
	}
}
