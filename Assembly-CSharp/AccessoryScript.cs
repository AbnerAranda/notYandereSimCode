using System;
using UnityEngine;

// Token: 0x020000C0 RID: 192
public class AccessoryScript : MonoBehaviour
{
	// Token: 0x060009E4 RID: 2532 RVA: 0x0004D080 File Offset: 0x0004B280
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Hide();
			this.Prompt.enabled = false;
			this.Prompt.MyCollider.enabled = false;
			base.transform.parent = this.Target;
			base.transform.localPosition = new Vector3(this.X, this.Y, this.Z);
			base.transform.localEulerAngles = Vector3.zero;
			base.enabled = false;
		}
	}

	// Token: 0x0400085F RID: 2143
	public PromptScript Prompt;

	// Token: 0x04000860 RID: 2144
	public Transform Target;

	// Token: 0x04000861 RID: 2145
	public float X;

	// Token: 0x04000862 RID: 2146
	public float Y;

	// Token: 0x04000863 RID: 2147
	public float Z;
}
