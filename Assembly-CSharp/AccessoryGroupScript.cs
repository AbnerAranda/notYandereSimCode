using System;
using UnityEngine;

// Token: 0x020000BF RID: 191
public class AccessoryGroupScript : MonoBehaviour
{
	// Token: 0x060009E2 RID: 2530 RVA: 0x0004D054 File Offset: 0x0004B254
	public void SetPartsActive(bool active)
	{
		GameObject[] parts = this.Parts;
		for (int i = 0; i < parts.Length; i++)
		{
			parts[i].SetActive(active);
		}
	}

	// Token: 0x0400085E RID: 2142
	public GameObject[] Parts;
}
