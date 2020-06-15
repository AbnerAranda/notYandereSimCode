using System;
using UnityEngine;

// Token: 0x0200041E RID: 1054
public class TaskPickupScript : MonoBehaviour
{
	// Token: 0x06001C37 RID: 7223 RVA: 0x00152778 File Offset: 0x00150978
	private void Update()
	{
		if (this.Prompt.Circle[this.ButtonID].fillAmount == 0f)
		{
			this.Prompt.Yandere.StudentManager.TaskManager.CheckTaskPickups();
		}
	}

	// Token: 0x040034E5 RID: 13541
	public PromptScript Prompt;

	// Token: 0x040034E6 RID: 13542
	public int ButtonID = 3;
}
