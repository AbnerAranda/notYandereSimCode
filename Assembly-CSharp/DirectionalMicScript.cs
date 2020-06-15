using System;
using UnityEngine;

// Token: 0x0200026A RID: 618
public class DirectionalMicScript : MonoBehaviour
{
	// Token: 0x0600134E RID: 4942 RVA: 0x000A5117 File Offset: 0x000A3317
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Yandere.Inventory.DirectionalMic = true;
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04001A4D RID: 6733
	public PromptScript Prompt;
}
