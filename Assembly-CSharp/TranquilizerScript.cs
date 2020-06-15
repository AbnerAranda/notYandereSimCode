using System;
using UnityEngine;

// Token: 0x02000433 RID: 1075
public class TranquilizerScript : MonoBehaviour
{
	// Token: 0x06001C88 RID: 7304 RVA: 0x001569F8 File Offset: 0x00154BF8
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Yandere.Inventory.Tranquilizer = true;
			this.Prompt.Yandere.StudentManager.UpdateAllBentos();
			this.Prompt.Yandere.TheftTimer = 0.1f;
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x040035C1 RID: 13761
	public PromptScript Prompt;
}
