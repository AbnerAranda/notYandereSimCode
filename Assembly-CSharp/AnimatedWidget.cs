using System;
using UnityEngine;

// Token: 0x02000087 RID: 135
[ExecuteInEditMode]
public class AnimatedWidget : MonoBehaviour
{
	// Token: 0x060005A1 RID: 1441 RVA: 0x00034070 File Offset: 0x00032270
	private void OnEnable()
	{
		this.mWidget = base.GetComponent<UIWidget>();
		this.LateUpdate();
	}

	// Token: 0x060005A2 RID: 1442 RVA: 0x00034084 File Offset: 0x00032284
	private void LateUpdate()
	{
		if (this.mWidget != null)
		{
			this.mWidget.width = Mathf.RoundToInt(this.width);
			this.mWidget.height = Mathf.RoundToInt(this.height);
		}
	}

	// Token: 0x040005AC RID: 1452
	public float width = 1f;

	// Token: 0x040005AD RID: 1453
	public float height = 1f;

	// Token: 0x040005AE RID: 1454
	private UIWidget mWidget;
}
