using System;
using UnityEngine;

// Token: 0x02000091 RID: 145
[AddComponentMenu("NGUI/Tween/Tween Rotation")]
public class TweenRotation : UITweener
{
	// Token: 0x170000D6 RID: 214
	// (get) Token: 0x06000603 RID: 1539 RVA: 0x000355BC File Offset: 0x000337BC
	public Transform cachedTransform
	{
		get
		{
			if (this.mTrans == null)
			{
				this.mTrans = base.transform;
			}
			return this.mTrans;
		}
	}

	// Token: 0x170000D7 RID: 215
	// (get) Token: 0x06000604 RID: 1540 RVA: 0x000355DE File Offset: 0x000337DE
	// (set) Token: 0x06000605 RID: 1541 RVA: 0x000355E6 File Offset: 0x000337E6
	[Obsolete("Use 'value' instead")]
	public Quaternion rotation
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

	// Token: 0x170000D8 RID: 216
	// (get) Token: 0x06000606 RID: 1542 RVA: 0x000355EF File Offset: 0x000337EF
	// (set) Token: 0x06000607 RID: 1543 RVA: 0x000355FC File Offset: 0x000337FC
	public Quaternion value
	{
		get
		{
			return this.cachedTransform.localRotation;
		}
		set
		{
			this.cachedTransform.localRotation = value;
		}
	}

	// Token: 0x06000608 RID: 1544 RVA: 0x0003560C File Offset: 0x0003380C
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.value = (this.quaternionLerp ? Quaternion.Slerp(Quaternion.Euler(this.from), Quaternion.Euler(this.to), factor) : Quaternion.Euler(new Vector3(Mathf.Lerp(this.from.x, this.to.x, factor), Mathf.Lerp(this.from.y, this.to.y, factor), Mathf.Lerp(this.from.z, this.to.z, factor))));
	}

	// Token: 0x06000609 RID: 1545 RVA: 0x000356A4 File Offset: 0x000338A4
	public static TweenRotation Begin(GameObject go, float duration, Quaternion rot)
	{
		TweenRotation tweenRotation = UITweener.Begin<TweenRotation>(go, duration, 0f);
		tweenRotation.from = tweenRotation.value.eulerAngles;
		tweenRotation.to = rot.eulerAngles;
		if (duration <= 0f)
		{
			tweenRotation.Sample(1f, true);
			tweenRotation.enabled = false;
		}
		return tweenRotation;
	}

	// Token: 0x0600060A RID: 1546 RVA: 0x000356FC File Offset: 0x000338FC
	[ContextMenu("Set 'From' to current value")]
	public override void SetStartToCurrentValue()
	{
		this.from = this.value.eulerAngles;
	}

	// Token: 0x0600060B RID: 1547 RVA: 0x00035720 File Offset: 0x00033920
	[ContextMenu("Set 'To' to current value")]
	public override void SetEndToCurrentValue()
	{
		this.to = this.value.eulerAngles;
	}

	// Token: 0x0600060C RID: 1548 RVA: 0x00035741 File Offset: 0x00033941
	[ContextMenu("Assume value of 'From'")]
	private void SetCurrentValueToStart()
	{
		this.value = Quaternion.Euler(this.from);
	}

	// Token: 0x0600060D RID: 1549 RVA: 0x00035754 File Offset: 0x00033954
	[ContextMenu("Assume value of 'To'")]
	private void SetCurrentValueToEnd()
	{
		this.value = Quaternion.Euler(this.to);
	}

	// Token: 0x040005EA RID: 1514
	public Vector3 from;

	// Token: 0x040005EB RID: 1515
	public Vector3 to;

	// Token: 0x040005EC RID: 1516
	public bool quaternionLerp;

	// Token: 0x040005ED RID: 1517
	private Transform mTrans;
}
