using System;
using UnityEngine;

// Token: 0x02000325 RID: 805
public class LockpickScript : MonoBehaviour
{
	// Token: 0x06001815 RID: 6165 RVA: 0x000D62E1 File Offset: 0x000D44E1
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Yandere.Inventory.LockPick = true;
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x040022D2 RID: 8914
	public PromptScript Prompt;
}
