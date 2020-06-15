using System;
using UnityEngine;

// Token: 0x020003AA RID: 938
public class RooftopScript : MonoBehaviour
{
	// Token: 0x060019F0 RID: 6640 RVA: 0x000FECC8 File Offset: 0x000FCEC8
	private void Start()
	{
		if (SchoolGlobals.RoofFence)
		{
			GameObject[] dumpPoints = this.DumpPoints;
			for (int i = 0; i < dumpPoints.Length; i++)
			{
				dumpPoints[i].SetActive(false);
			}
			this.Railing.SetActive(false);
			this.Fence.SetActive(true);
		}
	}

	// Token: 0x040028BE RID: 10430
	public GameObject[] DumpPoints;

	// Token: 0x040028BF RID: 10431
	public GameObject Railing;

	// Token: 0x040028C0 RID: 10432
	public GameObject Fence;
}
