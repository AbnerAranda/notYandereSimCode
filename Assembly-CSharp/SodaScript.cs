using System;
using UnityEngine;

// Token: 0x020003EC RID: 1004
public class SodaScript : MonoBehaviour
{
	// Token: 0x06001ADA RID: 6874 RVA: 0x0010EBB0 File Offset: 0x0010CDB0
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Yandere.Inventory.Soda = true;
			this.Prompt.Yandere.StudentManager.TaskManager.UpdateTaskStatus();
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04002B9B RID: 11163
	public PromptScript Prompt;
}
