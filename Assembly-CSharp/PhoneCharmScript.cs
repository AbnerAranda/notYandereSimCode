using System;
using UnityEngine;

// Token: 0x02000361 RID: 865
public class PhoneCharmScript : MonoBehaviour
{
	// Token: 0x060018DC RID: 6364 RVA: 0x000C790D File Offset: 0x000C5B0D
	private void Update()
	{
		base.transform.eulerAngles = new Vector3(90f, base.transform.eulerAngles.y, base.transform.eulerAngles.z);
	}
}
