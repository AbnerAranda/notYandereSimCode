using System;
using System.Collections.Generic;
using AnimationOrTween;
using UnityEngine;

// Token: 0x02000096 RID: 150
public abstract class UITweener : MonoBehaviour
{
	// Token: 0x170000E2 RID: 226
	// (get) Token: 0x06000635 RID: 1589 RVA: 0x00035E00 File Offset: 0x00034000
	public float amountPerDelta
	{
		get
		{
			if (this.duration == 0f)
			{
				return 1000f;
			}
			if (this.mDuration != this.duration)
			{
				this.mDuration = this.duration;
				this.mAmountPerDelta = Mathf.Abs(1f / this.duration) * Mathf.Sign(this.mAmountPerDelta);
			}
			return this.mAmountPerDelta;
		}
	}

	// Token: 0x170000E3 RID: 227
	// (get) Token: 0x06000636 RID: 1590 RVA: 0x00035E63 File Offset: 0x00034063
	// (set) Token: 0x06000637 RID: 1591 RVA: 0x00035E6B File Offset: 0x0003406B
	public float tweenFactor
	{
		get
		{
			return this.mFactor;
		}
		set
		{
			this.mFactor = Mathf.Clamp01(value);
		}
	}

	// Token: 0x170000E4 RID: 228
	// (get) Token: 0x06000638 RID: 1592 RVA: 0x00035E79 File Offset: 0x00034079
	public AnimationOrTween.Direction direction
	{
		get
		{
			if (this.amountPerDelta >= 0f)
			{
				return AnimationOrTween.Direction.Forward;
			}
			return AnimationOrTween.Direction.Reverse;
		}
	}

	// Token: 0x06000639 RID: 1593 RVA: 0x00035E8B File Offset: 0x0003408B
	private void Reset()
	{
		if (!this.mStarted)
		{
			this.SetStartToCurrentValue();
			this.SetEndToCurrentValue();
		}
	}

	// Token: 0x0600063A RID: 1594 RVA: 0x00035EA1 File Offset: 0x000340A1
	protected virtual void Start()
	{
		this.DoUpdate();
	}

	// Token: 0x0600063B RID: 1595 RVA: 0x00035EA9 File Offset: 0x000340A9
	protected void Update()
	{
		if (!this.useFixedUpdate)
		{
			this.DoUpdate();
		}
	}

	// Token: 0x0600063C RID: 1596 RVA: 0x00035EB9 File Offset: 0x000340B9
	protected void FixedUpdate()
	{
		if (this.useFixedUpdate)
		{
			this.DoUpdate();
		}
	}

	// Token: 0x0600063D RID: 1597 RVA: 0x00035ECC File Offset: 0x000340CC
	protected void DoUpdate()
	{
		float num = (this.ignoreTimeScale && !this.useFixedUpdate) ? Time.unscaledDeltaTime : Time.deltaTime;
		float num2 = (this.ignoreTimeScale && !this.useFixedUpdate) ? Time.unscaledTime : Time.time;
		if (!this.mStarted)
		{
			num = 0f;
			this.mStarted = true;
			this.mStartTime = num2 + this.delay;
		}
		if (num2 < this.mStartTime)
		{
			return;
		}
		this.mFactor += ((this.duration == 0f) ? 1f : (this.amountPerDelta * num * this.timeScale));
		if (this.style == UITweener.Style.Loop)
		{
			if (this.mFactor > 1f)
			{
				this.mFactor -= Mathf.Floor(this.mFactor);
			}
		}
		else if (this.style == UITweener.Style.PingPong)
		{
			if (this.mFactor > 1f)
			{
				this.mFactor = 1f - (this.mFactor - Mathf.Floor(this.mFactor));
				this.mAmountPerDelta = -this.mAmountPerDelta;
			}
			else if (this.mFactor < 0f)
			{
				this.mFactor = -this.mFactor;
				this.mFactor -= Mathf.Floor(this.mFactor);
				this.mAmountPerDelta = -this.mAmountPerDelta;
			}
		}
		if (this.style == UITweener.Style.Once && (this.duration == 0f || this.mFactor > 1f || this.mFactor < 0f))
		{
			this.mFactor = Mathf.Clamp01(this.mFactor);
			this.Sample(this.mFactor, true);
			base.enabled = false;
			if (UITweener.current != this)
			{
				UITweener uitweener = UITweener.current;
				UITweener.current = this;
				if (this.onFinished != null)
				{
					this.mTemp = this.onFinished;
					this.onFinished = new List<EventDelegate>();
					EventDelegate.Execute(this.mTemp);
					for (int i = 0; i < this.mTemp.Count; i++)
					{
						EventDelegate eventDelegate = this.mTemp[i];
						if (eventDelegate != null && !eventDelegate.oneShot)
						{
							EventDelegate.Add(this.onFinished, eventDelegate, eventDelegate.oneShot);
						}
					}
					this.mTemp = null;
				}
				if (this.eventReceiver != null && !string.IsNullOrEmpty(this.callWhenFinished))
				{
					this.eventReceiver.SendMessage(this.callWhenFinished, this, SendMessageOptions.DontRequireReceiver);
				}
				UITweener.current = uitweener;
				return;
			}
		}
		else
		{
			this.Sample(this.mFactor, false);
		}
	}

	// Token: 0x0600063E RID: 1598 RVA: 0x00036156 File Offset: 0x00034356
	public void SetOnFinished(EventDelegate.Callback del)
	{
		EventDelegate.Set(this.onFinished, del);
	}

	// Token: 0x0600063F RID: 1599 RVA: 0x00036165 File Offset: 0x00034365
	public void SetOnFinished(EventDelegate del)
	{
		EventDelegate.Set(this.onFinished, del);
	}

	// Token: 0x06000640 RID: 1600 RVA: 0x00036173 File Offset: 0x00034373
	public void AddOnFinished(EventDelegate.Callback del)
	{
		EventDelegate.Add(this.onFinished, del);
	}

	// Token: 0x06000641 RID: 1601 RVA: 0x00036182 File Offset: 0x00034382
	public void AddOnFinished(EventDelegate del)
	{
		EventDelegate.Add(this.onFinished, del);
	}

	// Token: 0x06000642 RID: 1602 RVA: 0x00036190 File Offset: 0x00034390
	public void RemoveOnFinished(EventDelegate del)
	{
		if (this.onFinished != null)
		{
			this.onFinished.Remove(del);
		}
		if (this.mTemp != null)
		{
			this.mTemp.Remove(del);
		}
	}

	// Token: 0x06000643 RID: 1603 RVA: 0x000361BC File Offset: 0x000343BC
	private void OnDisable()
	{
		this.mStarted = false;
	}

	// Token: 0x06000644 RID: 1604 RVA: 0x000361C5 File Offset: 0x000343C5
	public void Finish()
	{
		if (base.enabled)
		{
			this.Sample((this.mAmountPerDelta > 0f) ? 1f : 0f, true);
			base.enabled = false;
		}
	}

	// Token: 0x06000645 RID: 1605 RVA: 0x000361F8 File Offset: 0x000343F8
	public void Sample(float factor, bool isFinished)
	{
		float num = Mathf.Clamp01(factor);
		if (this.method == UITweener.Method.EaseIn)
		{
			num = 1f - Mathf.Sin(1.57079637f * (1f - num));
			if (this.steeperCurves)
			{
				num *= num;
			}
		}
		else if (this.method == UITweener.Method.EaseOut)
		{
			num = Mathf.Sin(1.57079637f * num);
			if (this.steeperCurves)
			{
				num = 1f - num;
				num = 1f - num * num;
			}
		}
		else if (this.method == UITweener.Method.EaseInOut)
		{
			num -= Mathf.Sin(num * 6.28318548f) / 6.28318548f;
			if (this.steeperCurves)
			{
				num = num * 2f - 1f;
				float num2 = Mathf.Sign(num);
				num = 1f - Mathf.Abs(num);
				num = 1f - num * num;
				num = num2 * num * 0.5f + 0.5f;
			}
		}
		else if (this.method == UITweener.Method.BounceIn)
		{
			num = this.BounceLogic(num);
		}
		else if (this.method == UITweener.Method.BounceOut)
		{
			num = 1f - this.BounceLogic(1f - num);
		}
		this.OnUpdate((this.animationCurve != null) ? this.animationCurve.Evaluate(num) : num, isFinished);
	}

	// Token: 0x06000646 RID: 1606 RVA: 0x0003632C File Offset: 0x0003452C
	private float BounceLogic(float val)
	{
		if (val < 0.363636f)
		{
			val = 7.5685f * val * val;
		}
		else if (val < 0.727272f)
		{
			val = 7.5625f * (val -= 0.545454f) * val + 0.75f;
		}
		else if (val < 0.90909f)
		{
			val = 7.5625f * (val -= 0.818181f) * val + 0.9375f;
		}
		else
		{
			val = 7.5625f * (val -= 0.9545454f) * val + 0.984375f;
		}
		return val;
	}

	// Token: 0x06000647 RID: 1607 RVA: 0x000363B1 File Offset: 0x000345B1
	[Obsolete("Use PlayForward() instead")]
	public void Play()
	{
		this.Play(true);
	}

	// Token: 0x06000648 RID: 1608 RVA: 0x000363B1 File Offset: 0x000345B1
	public void PlayForward()
	{
		this.Play(true);
	}

	// Token: 0x06000649 RID: 1609 RVA: 0x000363BA File Offset: 0x000345BA
	public void PlayReverse()
	{
		this.Play(false);
	}

	// Token: 0x0600064A RID: 1610 RVA: 0x000363C3 File Offset: 0x000345C3
	public virtual void Play(bool forward)
	{
		this.mAmountPerDelta = Mathf.Abs(this.amountPerDelta);
		if (!forward)
		{
			this.mAmountPerDelta = -this.mAmountPerDelta;
		}
		if (!base.enabled)
		{
			base.enabled = true;
			this.mStarted = false;
		}
		this.DoUpdate();
	}

	// Token: 0x0600064B RID: 1611 RVA: 0x00036402 File Offset: 0x00034602
	public void ResetToBeginning()
	{
		this.mStarted = false;
		this.mFactor = ((this.amountPerDelta < 0f) ? 1f : 0f);
		this.Sample(this.mFactor, false);
	}

	// Token: 0x0600064C RID: 1612 RVA: 0x00036437 File Offset: 0x00034637
	public void Toggle()
	{
		if (this.mFactor > 0f)
		{
			this.mAmountPerDelta = -this.amountPerDelta;
		}
		else
		{
			this.mAmountPerDelta = Mathf.Abs(this.amountPerDelta);
		}
		base.enabled = true;
	}

	// Token: 0x0600064D RID: 1613
	protected abstract void OnUpdate(float factor, bool isFinished);

	// Token: 0x0600064E RID: 1614 RVA: 0x00036470 File Offset: 0x00034670
	public static T Begin<T>(GameObject go, float duration, float delay = 0f) where T : UITweener
	{
		T t = go.GetComponent<T>();
		if (t != null && t.tweenGroup != 0)
		{
			t = default(T);
			T[] components = go.GetComponents<T>();
			int i = 0;
			int num = components.Length;
			while (i < num)
			{
				t = components[i];
				if (t != null && t.tweenGroup == 0)
				{
					break;
				}
				t = default(T);
				i++;
			}
		}
		if (t == null)
		{
			t = go.AddComponent<T>();
			if (t == null)
			{
				Debug.LogError(string.Concat(new object[]
				{
					"Unable to add ",
					typeof(T),
					" to ",
					NGUITools.GetHierarchy(go)
				}), go);
				return default(T);
			}
		}
		t.mStarted = false;
		t.mFactor = 0f;
		t.duration = duration;
		t.mDuration = duration;
		t.delay = delay;
		t.mAmountPerDelta = ((duration > 0f) ? Mathf.Abs(1f / duration) : 1000f);
		t.style = UITweener.Style.Once;
		t.animationCurve = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 0f, 0f, 1f),
			new Keyframe(1f, 1f, 1f, 0f)
		});
		t.eventReceiver = null;
		t.callWhenFinished = null;
		t.onFinished.Clear();
		if (t.mTemp != null)
		{
			t.mTemp.Clear();
		}
		t.enabled = true;
		return t;
	}

	// Token: 0x0600064F RID: 1615 RVA: 0x00002ACE File Offset: 0x00000CCE
	public virtual void SetStartToCurrentValue()
	{
	}

	// Token: 0x06000650 RID: 1616 RVA: 0x00002ACE File Offset: 0x00000CCE
	public virtual void SetEndToCurrentValue()
	{
	}

	// Token: 0x04000604 RID: 1540
	public static UITweener current;

	// Token: 0x04000605 RID: 1541
	[HideInInspector]
	public UITweener.Method method;

	// Token: 0x04000606 RID: 1542
	[HideInInspector]
	public UITweener.Style style;

	// Token: 0x04000607 RID: 1543
	[HideInInspector]
	public AnimationCurve animationCurve = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 0f, 0f, 1f),
		new Keyframe(1f, 1f, 1f, 0f)
	});

	// Token: 0x04000608 RID: 1544
	[HideInInspector]
	public bool ignoreTimeScale = true;

	// Token: 0x04000609 RID: 1545
	[HideInInspector]
	public float delay;

	// Token: 0x0400060A RID: 1546
	[HideInInspector]
	public float duration = 1f;

	// Token: 0x0400060B RID: 1547
	[HideInInspector]
	public bool steeperCurves;

	// Token: 0x0400060C RID: 1548
	[HideInInspector]
	public int tweenGroup;

	// Token: 0x0400060D RID: 1549
	[Tooltip("By default, Update() will be used for tweening. Setting this to 'true' will make the tween happen in FixedUpdate() insted.")]
	public bool useFixedUpdate;

	// Token: 0x0400060E RID: 1550
	[HideInInspector]
	public List<EventDelegate> onFinished = new List<EventDelegate>();

	// Token: 0x0400060F RID: 1551
	[HideInInspector]
	public GameObject eventReceiver;

	// Token: 0x04000610 RID: 1552
	[HideInInspector]
	public string callWhenFinished;

	// Token: 0x04000611 RID: 1553
	[NonSerialized]
	public float timeScale = 1f;

	// Token: 0x04000612 RID: 1554
	private bool mStarted;

	// Token: 0x04000613 RID: 1555
	private float mStartTime;

	// Token: 0x04000614 RID: 1556
	private float mDuration;

	// Token: 0x04000615 RID: 1557
	private float mAmountPerDelta = 1000f;

	// Token: 0x04000616 RID: 1558
	private float mFactor;

	// Token: 0x04000617 RID: 1559
	private List<EventDelegate> mTemp;

	// Token: 0x0200066C RID: 1644
	[DoNotObfuscateNGUI]
	public enum Method
	{
		// Token: 0x04004636 RID: 17974
		Linear,
		// Token: 0x04004637 RID: 17975
		EaseIn,
		// Token: 0x04004638 RID: 17976
		EaseOut,
		// Token: 0x04004639 RID: 17977
		EaseInOut,
		// Token: 0x0400463A RID: 17978
		BounceIn,
		// Token: 0x0400463B RID: 17979
		BounceOut
	}

	// Token: 0x0200066D RID: 1645
	[DoNotObfuscateNGUI]
	public enum Style
	{
		// Token: 0x0400463D RID: 17981
		Once,
		// Token: 0x0400463E RID: 17982
		Loop,
		// Token: 0x0400463F RID: 17983
		PingPong
	}
}
