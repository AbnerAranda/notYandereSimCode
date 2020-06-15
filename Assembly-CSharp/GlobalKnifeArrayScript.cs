using System;
using UnityEngine;

// Token: 0x020002B9 RID: 697
public class GlobalKnifeArrayScript : MonoBehaviour
{
	// Token: 0x0600145D RID: 5213 RVA: 0x000B5654 File Offset: 0x000B3854
	public void ActivateKnives()
	{
		foreach (TimeStopKnifeScript timeStopKnifeScript in this.Knives)
		{
			if (timeStopKnifeScript != null)
			{
				timeStopKnifeScript.Unfreeze = true;
			}
		}
		this.ID = 0;
	}

	// Token: 0x04001D35 RID: 7477
	public TimeStopKnifeScript[] Knives;

	// Token: 0x04001D36 RID: 7478
	public int ID;
}
