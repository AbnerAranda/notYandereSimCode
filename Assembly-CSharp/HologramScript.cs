using System;
using UnityEngine;

// Token: 0x020002E7 RID: 743
public class HologramScript : MonoBehaviour
{
	// Token: 0x0600171A RID: 5914 RVA: 0x000C3E30 File Offset: 0x000C2030
	public void UpdateHolograms()
	{
		GameObject[] holograms = this.Holograms;
		for (int i = 0; i < holograms.Length; i++)
		{
			holograms[i].SetActive(this.TrueFalse());
		}
	}

	// Token: 0x0600171B RID: 5915 RVA: 0x000C3E60 File Offset: 0x000C2060
	private bool TrueFalse()
	{
		return UnityEngine.Random.value >= 0.5f;
	}

	// Token: 0x04001F41 RID: 8001
	public GameObject[] Holograms;
}
