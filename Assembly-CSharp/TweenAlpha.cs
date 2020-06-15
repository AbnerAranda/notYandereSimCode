using System;
using UnityEngine;

// Token: 0x02000089 RID: 137
[AddComponentMenu("NGUI/Tween/Tween Alpha")]
public class TweenAlpha : UITweener
{
	// Token: 0x170000C5 RID: 197
	// (get) Token: 0x060005A9 RID: 1449 RVA: 0x00034363 File Offset: 0x00032563
	// (set) Token: 0x060005AA RID: 1450 RVA: 0x0003436B File Offset: 0x0003256B
	[Obsolete("Use 'value' instead")]
	public float alpha
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

	// Token: 0x060005AB RID: 1451 RVA: 0x00034374 File Offset: 0x00032574
	private void OnDestroy()
	{
		if (this.autoCleanup && this.mMat != null && this.mShared != this.mMat)
		{
			UnityEngine.Object.Destroy(this.mMat);
			this.mMat = null;
		}
	}

	// Token: 0x060005AC RID: 1452 RVA: 0x000343B4 File Offset: 0x000325B4
	private void Cache()
	{
		this.mCached = true;
		this.mRect = base.GetComponent<UIRect>();
		this.mSr = base.GetComponent<SpriteRenderer>();
		if (this.mRect == null && this.mSr == null)
		{
			this.mLight = base.GetComponent<Light>();
			if (this.mLight == null)
			{
				Renderer component = base.GetComponent<Renderer>();
				if (component != null)
				{
					this.mShared = component.sharedMaterial;
					this.mMat = component.material;
				}
				if (this.mMat == null)
				{
					this.mRect = base.GetComponentInChildren<UIRect>();
					return;
				}
			}
			else
			{
				this.mBaseIntensity = this.mLight.intensity;
			}
		}
	}

	// Token: 0x170000C6 RID: 198
	// (get) Token: 0x060005AD RID: 1453 RVA: 0x0003446C File Offset: 0x0003266C
	// (set) Token: 0x060005AE RID: 1454 RVA: 0x00034508 File Offset: 0x00032708
	public float value
	{
		get
		{
			if (!this.mCached)
			{
				this.Cache();
			}
			if (this.mRect != null)
			{
				return this.mRect.alpha;
			}
			if (this.mSr != null)
			{
				return this.mSr.color.a;
			}
			if (this.mMat == null)
			{
				return 1f;
			}
			if (string.IsNullOrEmpty(this.colorProperty))
			{
				return this.mMat.color.a;
			}
			return this.mMat.GetColor(this.colorProperty).a;
		}
		set
		{
			if (!this.mCached)
			{
				this.Cache();
			}
			if (this.mRect != null)
			{
				this.mRect.alpha = value;
				return;
			}
			if (this.mSr != null)
			{
				Color color = this.mSr.color;
				color.a = value;
				this.mSr.color = color;
				return;
			}
			if (!(this.mMat != null))
			{
				if (this.mLight != null)
				{
					this.mLight.intensity = this.mBaseIntensity * value;
				}
				return;
			}
			if (string.IsNullOrEmpty(this.colorProperty))
			{
				Color color2 = this.mMat.color;
				color2.a = value;
				this.mMat.color = color2;
				return;
			}
			Color color3 = this.mMat.GetColor(this.colorProperty);
			color3.a = value;
			this.mMat.SetColor(this.colorProperty, color3);
		}
	}

	// Token: 0x060005AF RID: 1455 RVA: 0x000345F7 File Offset: 0x000327F7
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.value = Mathf.Lerp(this.from, this.to, factor);
	}

	// Token: 0x060005B0 RID: 1456 RVA: 0x00034614 File Offset: 0x00032814
	public static TweenAlpha Begin(GameObject go, float duration, float alpha, float delay = 0f)
	{
		TweenAlpha tweenAlpha = UITweener.Begin<TweenAlpha>(go, duration, delay);
		tweenAlpha.from = tweenAlpha.value;
		tweenAlpha.to = alpha;
		if (duration <= 0f)
		{
			tweenAlpha.Sample(1f, true);
			tweenAlpha.enabled = false;
		}
		return tweenAlpha;
	}

	// Token: 0x060005B1 RID: 1457 RVA: 0x00034659 File Offset: 0x00032859
	public override void SetStartToCurrentValue()
	{
		this.from = this.value;
	}

	// Token: 0x060005B2 RID: 1458 RVA: 0x00034667 File Offset: 0x00032867
	public override void SetEndToCurrentValue()
	{
		this.to = this.value;
	}

	// Token: 0x040005BB RID: 1467
	[Range(0f, 1f)]
	public float from = 1f;

	// Token: 0x040005BC RID: 1468
	[Range(0f, 1f)]
	public float to = 1f;

	// Token: 0x040005BD RID: 1469
	[Tooltip("If used on a renderer, the material should probably be cleaned up after this script gets destroyed...")]
	public bool autoCleanup;

	// Token: 0x040005BE RID: 1470
	[Tooltip("Color to adjust")]
	public string colorProperty;

	// Token: 0x040005BF RID: 1471
	[NonSerialized]
	private bool mCached;

	// Token: 0x040005C0 RID: 1472
	[NonSerialized]
	private UIRect mRect;

	// Token: 0x040005C1 RID: 1473
	[NonSerialized]
	private Material mShared;

	// Token: 0x040005C2 RID: 1474
	[NonSerialized]
	private Material mMat;

	// Token: 0x040005C3 RID: 1475
	[NonSerialized]
	private Light mLight;

	// Token: 0x040005C4 RID: 1476
	[NonSerialized]
	private SpriteRenderer mSr;

	// Token: 0x040005C5 RID: 1477
	[NonSerialized]
	private float mBaseIntensity = 1f;
}
