using System;
using UnityEngine;

// Token: 0x02000480 RID: 1152
public class YanvaniaCutsceneTriggerScript : MonoBehaviour
{
	// Token: 0x06001DDA RID: 7642 RVA: 0x001749FC File Offset: 0x00172BFC
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == "YanmontChan")
		{
			this.BossBattleWall.SetActive(true);
			this.Yanmont.EnterCutscene = true;
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04003B1F RID: 15135
	public YanvaniaYanmontScript Yanmont;

	// Token: 0x04003B20 RID: 15136
	public GameObject BossBattleWall;
}
