using System;
using UnityEngine;

// Token: 0x020000F6 RID: 246
public class CabinetDoorScript : MonoBehaviour
{
	// Token: 0x06000AA5 RID: 2725 RVA: 0x00058AC8 File Offset: 0x00056CC8
	private void Update()
	{
		if (this.Timer < 2f)
		{
			this.Timer += Time.deltaTime;
			if (this.Open)
			{
				base.transform.localPosition = new Vector3(Mathf.Lerp(base.transform.localPosition.x, 0.41775f, Time.deltaTime * 10f), base.transform.localPosition.y, base.transform.localPosition.z);
				return;
			}
			base.transform.localPosition = new Vector3(Mathf.Lerp(base.transform.localPosition.x, 0f, Time.deltaTime * 10f), base.transform.localPosition.y, base.transform.localPosition.z);
		}
	}

	// Token: 0x04000B57 RID: 2903
	public PromptScript Prompt;

	// Token: 0x04000B58 RID: 2904
	public bool Locked;

	// Token: 0x04000B59 RID: 2905
	public bool Open;

	// Token: 0x04000B5A RID: 2906
	public float Timer;
}
