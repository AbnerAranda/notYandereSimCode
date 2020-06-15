using System;
using UnityEngine;

// Token: 0x02000086 RID: 134
[ExecuteInEditMode]
[RequireComponent(typeof(UIWidget))]
public class AnimatedColor : MonoBehaviour
{
	// Token: 0x0600059E RID: 1438 RVA: 0x00034036 File Offset: 0x00032236
	private void OnEnable()
	{
		this.mWidget = base.GetComponent<UIWidget>();
		this.LateUpdate();
	}

	// Token: 0x0600059F RID: 1439 RVA: 0x0003404A File Offset: 0x0003224A
	private void LateUpdate()
	{
		this.mWidget.color = this.color;
	}

	// Token: 0x040005AA RID: 1450
	public Color color = Color.white;

	// Token: 0x040005AB RID: 1451
	private UIWidget mWidget;
}
