using System;
using UnityEngine;

// Token: 0x0200008B RID: 139
[RequireComponent(typeof(Camera))]
[AddComponentMenu("NGUI/Tween/Tween Field of View")]
public class TweenFOV : UITweener
{
	// Token: 0x170000C9 RID: 201
	// (get) Token: 0x060005C0 RID: 1472 RVA: 0x0003492F File Offset: 0x00032B2F
	public Camera cachedCamera
	{
		get
		{
			if (this.mCam == null)
			{
				this.mCam = base.GetComponent<Camera>();
			}
			return this.mCam;
		}
	}

	// Token: 0x170000CA RID: 202
	// (get) Token: 0x060005C1 RID: 1473 RVA: 0x00034951 File Offset: 0x00032B51
	// (set) Token: 0x060005C2 RID: 1474 RVA: 0x00034959 File Offset: 0x00032B59
	[Obsolete("Use 'value' instead")]
	public float fov
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

	// Token: 0x170000CB RID: 203
	// (get) Token: 0x060005C3 RID: 1475 RVA: 0x00034962 File Offset: 0x00032B62
	// (set) Token: 0x060005C4 RID: 1476 RVA: 0x0003496F File Offset: 0x00032B6F
	public float value
	{
		get
		{
			return this.cachedCamera.fieldOfView;
		}
		set
		{
			this.cachedCamera.fieldOfView = value;
		}
	}

	// Token: 0x060005C5 RID: 1477 RVA: 0x0003497D File Offset: 0x00032B7D
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.value = this.from * (1f - factor) + this.to * factor;
	}

	// Token: 0x060005C6 RID: 1478 RVA: 0x0003499C File Offset: 0x00032B9C
	public static TweenFOV Begin(GameObject go, float duration, float to)
	{
		TweenFOV tweenFOV = UITweener.Begin<TweenFOV>(go, duration, 0f);
		tweenFOV.from = tweenFOV.value;
		tweenFOV.to = to;
		if (duration <= 0f)
		{
			tweenFOV.Sample(1f, true);
			tweenFOV.enabled = false;
		}
		return tweenFOV;
	}

	// Token: 0x060005C7 RID: 1479 RVA: 0x000349E5 File Offset: 0x00032BE5
	[ContextMenu("Set 'From' to current value")]
	public override void SetStartToCurrentValue()
	{
		this.from = this.value;
	}

	// Token: 0x060005C8 RID: 1480 RVA: 0x000349F3 File Offset: 0x00032BF3
	[ContextMenu("Set 'To' to current value")]
	public override void SetEndToCurrentValue()
	{
		this.to = this.value;
	}

	// Token: 0x060005C9 RID: 1481 RVA: 0x00034A01 File Offset: 0x00032C01
	[ContextMenu("Assume value of 'From'")]
	private void SetCurrentValueToStart()
	{
		this.value = this.from;
	}

	// Token: 0x060005CA RID: 1482 RVA: 0x00034A0F File Offset: 0x00032C0F
	[ContextMenu("Assume value of 'To'")]
	private void SetCurrentValueToEnd()
	{
		this.value = this.to;
	}

	// Token: 0x040005CD RID: 1485
	public float from = 45f;

	// Token: 0x040005CE RID: 1486
	public float to = 45f;

	// Token: 0x040005CF RID: 1487
	private Camera mCam;
}
