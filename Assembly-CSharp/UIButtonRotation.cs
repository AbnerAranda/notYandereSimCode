using System;
using UnityEngine;

// Token: 0x02000049 RID: 73
[AddComponentMenu("NGUI/Interaction/Button Rotation")]
public class UIButtonRotation : MonoBehaviour
{
	// Token: 0x06000197 RID: 407 RVA: 0x00014EC5 File Offset: 0x000130C5
	private void Start()
	{
		if (!this.mStarted)
		{
			this.mStarted = true;
			if (this.tweenTarget == null)
			{
				this.tweenTarget = base.transform;
			}
			this.mRot = this.tweenTarget.localRotation;
		}
	}

	// Token: 0x06000198 RID: 408 RVA: 0x00014F01 File Offset: 0x00013101
	private void OnEnable()
	{
		if (this.mStarted)
		{
			this.OnHover(UICamera.IsHighlighted(base.gameObject));
		}
	}

	// Token: 0x06000199 RID: 409 RVA: 0x00014F1C File Offset: 0x0001311C
	private void OnDisable()
	{
		if (this.mStarted && this.tweenTarget != null)
		{
			TweenRotation component = this.tweenTarget.GetComponent<TweenRotation>();
			if (component != null)
			{
				component.value = this.mRot;
				component.enabled = false;
			}
		}
	}

	// Token: 0x0600019A RID: 410 RVA: 0x00014F68 File Offset: 0x00013168
	private void OnPress(bool isPressed)
	{
		if (base.enabled)
		{
			if (!this.mStarted)
			{
				this.Start();
			}
			TweenRotation.Begin(this.tweenTarget.gameObject, this.duration, isPressed ? (this.mRot * Quaternion.Euler(this.pressed)) : (UICamera.IsHighlighted(base.gameObject) ? (this.mRot * Quaternion.Euler(this.hover)) : this.mRot)).method = UITweener.Method.EaseInOut;
		}
	}

	// Token: 0x0600019B RID: 411 RVA: 0x00014FF0 File Offset: 0x000131F0
	private void OnHover(bool isOver)
	{
		if (base.enabled)
		{
			if (!this.mStarted)
			{
				this.Start();
			}
			TweenRotation.Begin(this.tweenTarget.gameObject, this.duration, isOver ? (this.mRot * Quaternion.Euler(this.hover)) : this.mRot).method = UITweener.Method.EaseInOut;
		}
	}

	// Token: 0x0600019C RID: 412 RVA: 0x00015050 File Offset: 0x00013250
	private void OnSelect(bool isSelected)
	{
		if (base.enabled && (!isSelected || UICamera.currentScheme == UICamera.ControlScheme.Controller))
		{
			this.OnHover(isSelected);
		}
	}

	// Token: 0x04000323 RID: 803
	public Transform tweenTarget;

	// Token: 0x04000324 RID: 804
	public Vector3 hover = Vector3.zero;

	// Token: 0x04000325 RID: 805
	public Vector3 pressed = Vector3.zero;

	// Token: 0x04000326 RID: 806
	public float duration = 0.2f;

	// Token: 0x04000327 RID: 807
	private Quaternion mRot;

	// Token: 0x04000328 RID: 808
	private bool mStarted;
}
