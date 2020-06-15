using System;
using UnityEngine;

// Token: 0x02000420 RID: 1056
public class TeleportScript : MonoBehaviour
{
	// Token: 0x06001C42 RID: 7234 RVA: 0x00152D9D File Offset: 0x00150F9D
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Yandere.transform.position = this.Destination.position;
			Physics.SyncTransforms();
		}
	}

	// Token: 0x040034FB RID: 13563
	public PromptScript Prompt;

	// Token: 0x040034FC RID: 13564
	public Transform Destination;
}
