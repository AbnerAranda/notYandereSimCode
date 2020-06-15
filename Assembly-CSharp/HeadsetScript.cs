using System;
using UnityEngine;

// Token: 0x020002E1 RID: 737
public class HeadsetScript : MonoBehaviour
{
	// Token: 0x06001708 RID: 5896 RVA: 0x000C02FC File Offset: 0x000BE4FC
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Yandere.Inventory.Schemes.UpdateInstructions();
			this.Prompt.Yandere.Inventory.Headset = true;
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04001EC1 RID: 7873
	public PromptScript Prompt;
}
