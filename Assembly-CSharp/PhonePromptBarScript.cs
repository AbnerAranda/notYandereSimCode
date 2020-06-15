using System;
using UnityEngine;

// Token: 0x02000364 RID: 868
public class PhonePromptBarScript : MonoBehaviour
{
	// Token: 0x060018E7 RID: 6375 RVA: 0x000E7B60 File Offset: 0x000E5D60
	private void Start()
	{
		base.transform.localPosition = new Vector3(base.transform.localPosition.x, 630f, base.transform.localPosition.z);
		this.Panel.enabled = false;
	}

	// Token: 0x060018E8 RID: 6376 RVA: 0x000E7BB0 File Offset: 0x000E5DB0
	private void Update()
	{
		float t = Time.unscaledDeltaTime * 10f;
		if (!this.Show)
		{
			if (this.Panel.enabled)
			{
				base.transform.localPosition = new Vector3(base.transform.localPosition.x, Mathf.Lerp(base.transform.localPosition.y, 631f, t), base.transform.localPosition.z);
				if (base.transform.localPosition.y < 630f)
				{
					base.transform.localPosition = new Vector3(base.transform.localPosition.x, 631f, base.transform.localPosition.z);
					this.Panel.enabled = false;
					return;
				}
			}
		}
		else
		{
			base.transform.localPosition = new Vector3(base.transform.localPosition.x, Mathf.Lerp(base.transform.localPosition.y, 530f, t), base.transform.localPosition.z);
		}
	}

	// Token: 0x0400254A RID: 9546
	public UIPanel Panel;

	// Token: 0x0400254B RID: 9547
	public bool Show;

	// Token: 0x0400254C RID: 9548
	public UILabel Label;
}
