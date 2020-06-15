using System;
using UnityEngine;

// Token: 0x02000047 RID: 71
[AddComponentMenu("NGUI/Interaction/Button Message (Legacy)")]
public class UIButtonMessage : MonoBehaviour
{
	// Token: 0x06000185 RID: 389 RVA: 0x00014B40 File Offset: 0x00012D40
	private void Start()
	{
		this.mStarted = true;
	}

	// Token: 0x06000186 RID: 390 RVA: 0x00014B49 File Offset: 0x00012D49
	private void OnEnable()
	{
		if (this.mStarted)
		{
			this.OnHover(UICamera.IsHighlighted(base.gameObject));
		}
	}

	// Token: 0x06000187 RID: 391 RVA: 0x00014B64 File Offset: 0x00012D64
	private void OnHover(bool isOver)
	{
		if (base.enabled && ((isOver && this.trigger == UIButtonMessage.Trigger.OnMouseOver) || (!isOver && this.trigger == UIButtonMessage.Trigger.OnMouseOut)))
		{
			this.Send();
		}
	}

	// Token: 0x06000188 RID: 392 RVA: 0x00014B8C File Offset: 0x00012D8C
	private void OnPress(bool isPressed)
	{
		if (base.enabled && ((isPressed && this.trigger == UIButtonMessage.Trigger.OnPress) || (!isPressed && this.trigger == UIButtonMessage.Trigger.OnRelease)))
		{
			this.Send();
		}
	}

	// Token: 0x06000189 RID: 393 RVA: 0x00014BB4 File Offset: 0x00012DB4
	private void OnSelect(bool isSelected)
	{
		if (base.enabled && (!isSelected || UICamera.currentScheme == UICamera.ControlScheme.Controller))
		{
			this.OnHover(isSelected);
		}
	}

	// Token: 0x0600018A RID: 394 RVA: 0x00014BD0 File Offset: 0x00012DD0
	private void OnClick()
	{
		if (base.enabled && this.trigger == UIButtonMessage.Trigger.OnClick)
		{
			this.Send();
		}
	}

	// Token: 0x0600018B RID: 395 RVA: 0x00014BE8 File Offset: 0x00012DE8
	private void OnDoubleClick()
	{
		if (base.enabled && this.trigger == UIButtonMessage.Trigger.OnDoubleClick)
		{
			this.Send();
		}
	}

	// Token: 0x0600018C RID: 396 RVA: 0x00014C04 File Offset: 0x00012E04
	private void Send()
	{
		if (string.IsNullOrEmpty(this.functionName))
		{
			return;
		}
		if (this.target == null)
		{
			this.target = base.gameObject;
		}
		if (this.includeChildren)
		{
			Transform[] componentsInChildren = this.target.GetComponentsInChildren<Transform>();
			int i = 0;
			int num = componentsInChildren.Length;
			while (i < num)
			{
				componentsInChildren[i].gameObject.SendMessage(this.functionName, base.gameObject, SendMessageOptions.DontRequireReceiver);
				i++;
			}
			return;
		}
		this.target.SendMessage(this.functionName, base.gameObject, SendMessageOptions.DontRequireReceiver);
	}

	// Token: 0x04000317 RID: 791
	public GameObject target;

	// Token: 0x04000318 RID: 792
	public string functionName;

	// Token: 0x04000319 RID: 793
	public UIButtonMessage.Trigger trigger;

	// Token: 0x0400031A RID: 794
	public bool includeChildren;

	// Token: 0x0400031B RID: 795
	private bool mStarted;

	// Token: 0x02000627 RID: 1575
	[DoNotObfuscateNGUI]
	public enum Trigger
	{
		// Token: 0x04004562 RID: 17762
		OnClick,
		// Token: 0x04004563 RID: 17763
		OnMouseOver,
		// Token: 0x04004564 RID: 17764
		OnMouseOut,
		// Token: 0x04004565 RID: 17765
		OnPress,
		// Token: 0x04004566 RID: 17766
		OnRelease,
		// Token: 0x04004567 RID: 17767
		OnDoubleClick
	}
}
