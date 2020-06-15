using System;
using UnityEngine;

// Token: 0x02000238 RID: 568
public class CigsScript : MonoBehaviour
{
	// Token: 0x06001246 RID: 4678 RVA: 0x0008209C File Offset: 0x0008029C
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			SchemeGlobals.SetSchemeStage(3, 3);
			this.Prompt.Yandere.Inventory.Schemes.UpdateInstructions();
			this.Prompt.Yandere.Inventory.Cigs = true;
			UnityEngine.Object.Destroy(base.gameObject);
			this.Prompt.Yandere.StudentManager.TaskManager.CheckTaskPickups();
		}
	}

	// Token: 0x040015AB RID: 5547
	public PromptScript Prompt;
}
