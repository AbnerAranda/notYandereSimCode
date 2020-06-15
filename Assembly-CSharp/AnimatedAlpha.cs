using System;
using UnityEngine;

// Token: 0x02000085 RID: 133
[ExecuteInEditMode]
public class AnimatedAlpha : MonoBehaviour
{
	// Token: 0x0600059B RID: 1435 RVA: 0x00033FC3 File Offset: 0x000321C3
	private void OnEnable()
	{
		this.mWidget = base.GetComponent<UIWidget>();
		this.mPanel = base.GetComponent<UIPanel>();
		this.LateUpdate();
	}

	// Token: 0x0600059C RID: 1436 RVA: 0x00033FE3 File Offset: 0x000321E3
	private void LateUpdate()
	{
		if (this.mWidget != null)
		{
			this.mWidget.alpha = this.alpha;
		}
		if (this.mPanel != null)
		{
			this.mPanel.alpha = this.alpha;
		}
	}

	// Token: 0x040005A7 RID: 1447
	[Range(0f, 1f)]
	public float alpha = 1f;

	// Token: 0x040005A8 RID: 1448
	private UIWidget mWidget;

	// Token: 0x040005A9 RID: 1449
	private UIPanel mPanel;
}
