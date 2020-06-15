using System;
using UnityEngine;

// Token: 0x02000048 RID: 72
[AddComponentMenu("NGUI/Interaction/Button Offset")]
public class UIButtonOffset : MonoBehaviour
{
	// Token: 0x0600018E RID: 398 RVA: 0x00014C90 File Offset: 0x00012E90
	private void Start()
	{
		if (!this.mStarted)
		{
			this.mStarted = true;
			if (this.tweenTarget == null)
			{
				this.tweenTarget = base.transform;
			}
			this.mPos = this.tweenTarget.localPosition;
		}
	}

	// Token: 0x0600018F RID: 399 RVA: 0x00014CCC File Offset: 0x00012ECC
	private void OnEnable()
	{
		if (this.mStarted)
		{
			this.OnHover(UICamera.IsHighlighted(base.gameObject));
		}
	}

	// Token: 0x06000190 RID: 400 RVA: 0x00014CE8 File Offset: 0x00012EE8
	private void OnDisable()
	{
		if (this.mStarted && this.tweenTarget != null)
		{
			TweenPosition component = this.tweenTarget.GetComponent<TweenPosition>();
			if (component != null)
			{
				component.value = this.mPos;
				component.enabled = false;
			}
		}
	}

	// Token: 0x06000191 RID: 401 RVA: 0x00014D34 File Offset: 0x00012F34
	private void OnPress(bool isPressed)
	{
		this.mPressed = isPressed;
		if (base.enabled)
		{
			if (!this.mStarted)
			{
				this.Start();
			}
			TweenPosition.Begin(this.tweenTarget.gameObject, this.duration, isPressed ? (this.mPos + this.pressed) : (UICamera.IsHighlighted(base.gameObject) ? (this.mPos + this.hover) : this.mPos)).method = UITweener.Method.EaseInOut;
		}
	}

	// Token: 0x06000192 RID: 402 RVA: 0x00014DB8 File Offset: 0x00012FB8
	private void OnHover(bool isOver)
	{
		if (base.enabled)
		{
			if (!this.mStarted)
			{
				this.Start();
			}
			TweenPosition.Begin(this.tweenTarget.gameObject, this.duration, isOver ? (this.mPos + this.hover) : this.mPos).method = UITweener.Method.EaseInOut;
		}
	}

	// Token: 0x06000193 RID: 403 RVA: 0x00014E13 File Offset: 0x00013013
	private void OnDragOver()
	{
		if (this.mPressed)
		{
			TweenPosition.Begin(this.tweenTarget.gameObject, this.duration, this.mPos + this.hover).method = UITweener.Method.EaseInOut;
		}
	}

	// Token: 0x06000194 RID: 404 RVA: 0x00014E4A File Offset: 0x0001304A
	private void OnDragOut()
	{
		if (this.mPressed)
		{
			TweenPosition.Begin(this.tweenTarget.gameObject, this.duration, this.mPos).method = UITweener.Method.EaseInOut;
		}
	}

	// Token: 0x06000195 RID: 405 RVA: 0x00014E76 File Offset: 0x00013076
	private void OnSelect(bool isSelected)
	{
		if (base.enabled && (!isSelected || UICamera.currentScheme == UICamera.ControlScheme.Controller))
		{
			this.OnHover(isSelected);
		}
	}

	// Token: 0x0400031C RID: 796
	public Transform tweenTarget;

	// Token: 0x0400031D RID: 797
	public Vector3 hover = Vector3.zero;

	// Token: 0x0400031E RID: 798
	public Vector3 pressed = new Vector3(2f, -2f);

	// Token: 0x0400031F RID: 799
	public float duration = 0.2f;

	// Token: 0x04000320 RID: 800
	[NonSerialized]
	private Vector3 mPos;

	// Token: 0x04000321 RID: 801
	[NonSerialized]
	private bool mStarted;

	// Token: 0x04000322 RID: 802
	[NonSerialized]
	private bool mPressed;
}
