using System;
using UnityEngine;

// Token: 0x0200008A RID: 138
[AddComponentMenu("NGUI/Tween/Tween Color")]
public class TweenColor : UITweener
{
	// Token: 0x060005B4 RID: 1460 RVA: 0x000346A0 File Offset: 0x000328A0
	private void Cache()
	{
		this.mCached = true;
		this.mWidget = base.GetComponent<UIWidget>();
		if (this.mWidget != null)
		{
			return;
		}
		this.mSr = base.GetComponent<SpriteRenderer>();
		if (this.mSr != null)
		{
			return;
		}
		Renderer component = base.GetComponent<Renderer>();
		if (component != null)
		{
			this.mMat = component.material;
			return;
		}
		this.mLight = base.GetComponent<Light>();
		if (this.mLight == null)
		{
			this.mWidget = base.GetComponentInChildren<UIWidget>();
		}
	}

	// Token: 0x170000C7 RID: 199
	// (get) Token: 0x060005B5 RID: 1461 RVA: 0x0003472D File Offset: 0x0003292D
	// (set) Token: 0x060005B6 RID: 1462 RVA: 0x00034735 File Offset: 0x00032935
	[Obsolete("Use 'value' instead")]
	public Color color
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

	// Token: 0x170000C8 RID: 200
	// (get) Token: 0x060005B7 RID: 1463 RVA: 0x00034740 File Offset: 0x00032940
	// (set) Token: 0x060005B8 RID: 1464 RVA: 0x000347C8 File Offset: 0x000329C8
	public Color value
	{
		get
		{
			if (!this.mCached)
			{
				this.Cache();
			}
			if (this.mWidget != null)
			{
				return this.mWidget.color;
			}
			if (this.mMat != null)
			{
				return this.mMat.color;
			}
			if (this.mSr != null)
			{
				return this.mSr.color;
			}
			if (this.mLight != null)
			{
				return this.mLight.color;
			}
			return Color.black;
		}
		set
		{
			if (!this.mCached)
			{
				this.Cache();
			}
			if (this.mWidget != null)
			{
				this.mWidget.color = value;
				return;
			}
			if (this.mMat != null)
			{
				this.mMat.color = value;
				return;
			}
			if (this.mSr != null)
			{
				this.mSr.color = value;
				return;
			}
			if (this.mLight != null)
			{
				this.mLight.color = value;
				this.mLight.enabled = (value.r + value.g + value.b > 0.01f);
			}
		}
	}

	// Token: 0x060005B9 RID: 1465 RVA: 0x00034874 File Offset: 0x00032A74
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.value = Color.Lerp(this.from, this.to, factor);
	}

	// Token: 0x060005BA RID: 1466 RVA: 0x00034890 File Offset: 0x00032A90
	public static TweenColor Begin(GameObject go, float duration, Color color)
	{
		TweenColor tweenColor = UITweener.Begin<TweenColor>(go, duration, 0f);
		tweenColor.from = tweenColor.value;
		tweenColor.to = color;
		if (duration <= 0f)
		{
			tweenColor.Sample(1f, true);
			tweenColor.enabled = false;
		}
		return tweenColor;
	}

	// Token: 0x060005BB RID: 1467 RVA: 0x000348D9 File Offset: 0x00032AD9
	[ContextMenu("Set 'From' to current value")]
	public override void SetStartToCurrentValue()
	{
		this.from = this.value;
	}

	// Token: 0x060005BC RID: 1468 RVA: 0x000348E7 File Offset: 0x00032AE7
	[ContextMenu("Set 'To' to current value")]
	public override void SetEndToCurrentValue()
	{
		this.to = this.value;
	}

	// Token: 0x060005BD RID: 1469 RVA: 0x000348F5 File Offset: 0x00032AF5
	[ContextMenu("Assume value of 'From'")]
	private void SetCurrentValueToStart()
	{
		this.value = this.from;
	}

	// Token: 0x060005BE RID: 1470 RVA: 0x00034903 File Offset: 0x00032B03
	[ContextMenu("Assume value of 'To'")]
	private void SetCurrentValueToEnd()
	{
		this.value = this.to;
	}

	// Token: 0x040005C6 RID: 1478
	public Color from = Color.white;

	// Token: 0x040005C7 RID: 1479
	public Color to = Color.white;

	// Token: 0x040005C8 RID: 1480
	private bool mCached;

	// Token: 0x040005C9 RID: 1481
	private UIWidget mWidget;

	// Token: 0x040005CA RID: 1482
	private Material mMat;

	// Token: 0x040005CB RID: 1483
	private Light mLight;

	// Token: 0x040005CC RID: 1484
	private SpriteRenderer mSr;
}
