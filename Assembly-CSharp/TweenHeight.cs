using System;
using UnityEngine;

// Token: 0x0200008D RID: 141
[RequireComponent(typeof(UIWidget))]
[AddComponentMenu("NGUI/Tween/Tween Height")]
public class TweenHeight : UITweener
{
	// Token: 0x170000CD RID: 205
	// (get) Token: 0x060005D4 RID: 1492 RVA: 0x00034B47 File Offset: 0x00032D47
	public UIWidget cachedWidget
	{
		get
		{
			if (this.mWidget == null)
			{
				this.mWidget = base.GetComponent<UIWidget>();
			}
			return this.mWidget;
		}
	}

	// Token: 0x170000CE RID: 206
	// (get) Token: 0x060005D5 RID: 1493 RVA: 0x00034B69 File Offset: 0x00032D69
	// (set) Token: 0x060005D6 RID: 1494 RVA: 0x00034B71 File Offset: 0x00032D71
	[Obsolete("Use 'value' instead")]
	public int height
	{
		get
		{
			return this.value;
		}
		set
		{
			this.value = value;
		}
	}

	// Token: 0x170000CF RID: 207
	// (get) Token: 0x060005D7 RID: 1495 RVA: 0x00034B7A File Offset: 0x00032D7A
	// (set) Token: 0x060005D8 RID: 1496 RVA: 0x00034B87 File Offset: 0x00032D87
	public int value
	{
		get
		{
			return this.cachedWidget.height;
		}
		set
		{
			this.cachedWidget.height = value;
		}
	}

	// Token: 0x060005D9 RID: 1497 RVA: 0x00034B98 File Offset: 0x00032D98
	protected override void OnUpdate(float factor, bool isFinished)
	{
		if (this.fromTarget)
		{
			this.from = this.fromTarget.width;
		}
		if (this.toTarget)
		{
			this.to = this.toTarget.width;
		}
		this.value = Mathf.RoundToInt((float)this.from * (1f - factor) + (float)this.to * factor);
		if (this.updateTable)
		{
			if (this.mTable == null)
			{
				this.mTable = NGUITools.FindInParents<UITable>(base.gameObject);
				if (this.mTable == null)
				{
					this.updateTable = false;
					return;
				}
			}
			this.mTable.repositionNow = true;
		}
	}

	// Token: 0x060005DA RID: 1498 RVA: 0x00034C50 File Offset: 0x00032E50
	public static TweenHeight Begin(UIWidget widget, float duration, int height)
	{
		TweenHeight tweenHeight = UITweener.Begin<TweenHeight>(widget.gameObject, duration, 0f);
		tweenHeight.from = widget.height;
		tweenHeight.to = height;
		if (duration <= 0f)
		{
			tweenHeight.Sample(1f, true);
			tweenHeight.enabled = false;
		}
		return tweenHeight;
	}

	// Token: 0x060005DB RID: 1499 RVA: 0x00034C9E File Offset: 0x00032E9E
	[ContextMenu("Set 'From' to current value")]
	public override void SetStartToCurrentValue()
	{
		this.from = this.value;
	}

	// Token: 0x060005DC RID: 1500 RVA: 0x00034CAC File Offset: 0x00032EAC
	[ContextMenu("Set 'To' to current value")]
	public override void SetEndToCurrentValue()
	{
		this.to = this.value;
	}

	// Token: 0x060005DD RID: 1501 RVA: 0x00034CBA File Offset: 0x00032EBA
	[ContextMenu("Assume value of 'From'")]
	private void SetCurrentValueToStart()
	{
		this.value = this.from;
	}

	// Token: 0x060005DE RID: 1502 RVA: 0x00034CC8 File Offset: 0x00032EC8
	[ContextMenu("Assume value of 'To'")]
	private void SetCurrentValueToEnd()
	{
		this.value = this.to;
	}

	// Token: 0x040005D4 RID: 1492
	public int from = 100;

	// Token: 0x040005D5 RID: 1493
	public int to = 100;

	// Token: 0x040005D6 RID: 1494
	[Tooltip("If set, 'from' value will be set to match the specified rectangle")]
	public UIWidget fromTarget;

	// Token: 0x040005D7 RID: 1495
	[Tooltip("If set, 'to' value will be set to match the specified rectangle")]
	public UIWidget toTarget;

	// Token: 0x040005D8 RID: 1496
	[Tooltip("Whether there is a table that should be updated")]
	public bool updateTable;

	// Token: 0x040005D9 RID: 1497
	private UIWidget mWidget;

	// Token: 0x040005DA RID: 1498
	private UITable mTable;
}
