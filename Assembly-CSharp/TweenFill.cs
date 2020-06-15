using System;
using UnityEngine;

// Token: 0x0200008C RID: 140
[RequireComponent(typeof(UIBasicSprite))]
[AddComponentMenu("NGUI/Tween/Tween Fill")]
public class TweenFill : UITweener
{
	// Token: 0x060005CC RID: 1484 RVA: 0x00034A3B File Offset: 0x00032C3B
	private void Cache()
	{
		this.mCached = true;
		this.mSprite = base.GetComponent<UISprite>();
	}

	// Token: 0x170000CC RID: 204
	// (get) Token: 0x060005CD RID: 1485 RVA: 0x00034A50 File Offset: 0x00032C50
	// (set) Token: 0x060005CE RID: 1486 RVA: 0x00034A7F File Offset: 0x00032C7F
	public float value
	{
		get
		{
			if (!this.mCached)
			{
				this.Cache();
			}
			if (this.mSprite != null)
			{
				return this.mSprite.fillAmount;
			}
			return 0f;
		}
		set
		{
			if (!this.mCached)
			{
				this.Cache();
			}
			if (this.mSprite != null)
			{
				this.mSprite.fillAmount = value;
			}
		}
	}

	// Token: 0x060005CF RID: 1487 RVA: 0x00034AA9 File Offset: 0x00032CA9
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.value = Mathf.Lerp(this.from, this.to, factor);
	}

	// Token: 0x060005D0 RID: 1488 RVA: 0x00034AC4 File Offset: 0x00032CC4
	public static TweenFill Begin(GameObject go, float duration, float fill)
	{
		TweenFill tweenFill = UITweener.Begin<TweenFill>(go, duration, 0f);
		tweenFill.from = tweenFill.value;
		tweenFill.to = fill;
		if (duration <= 0f)
		{
			tweenFill.Sample(1f, true);
			tweenFill.enabled = false;
		}
		return tweenFill;
	}

	// Token: 0x060005D1 RID: 1489 RVA: 0x00034B0D File Offset: 0x00032D0D
	public override void SetStartToCurrentValue()
	{
		this.from = this.value;
	}

	// Token: 0x060005D2 RID: 1490 RVA: 0x00034B1B File Offset: 0x00032D1B
	public override void SetEndToCurrentValue()
	{
		this.to = this.value;
	}

	// Token: 0x040005D0 RID: 1488
	[Range(0f, 1f)]
	public float from = 1f;

	// Token: 0x040005D1 RID: 1489
	[Range(0f, 1f)]
	public float to = 1f;

	// Token: 0x040005D2 RID: 1490
	private bool mCached;

	// Token: 0x040005D3 RID: 1491
	private UIBasicSprite mSprite;
}
