using System;
using UnityEngine;

// Token: 0x0200005C RID: 92
[AddComponentMenu("NGUI/Interaction/Play Sound")]
public class UIPlaySound : MonoBehaviour
{
	// Token: 0x1700002C RID: 44
	// (get) Token: 0x06000263 RID: 611 RVA: 0x0001953C File Offset: 0x0001773C
	private bool canPlay
	{
		get
		{
			if (!base.enabled)
			{
				return false;
			}
			UIButton component = base.GetComponent<UIButton>();
			return component == null || component.isEnabled;
		}
	}

	// Token: 0x06000264 RID: 612 RVA: 0x0001956B File Offset: 0x0001776B
	private void OnEnable()
	{
		if (this.trigger == UIPlaySound.Trigger.OnEnable)
		{
			NGUITools.PlaySound(this.audioClip, this.volume, this.pitch);
		}
	}

	// Token: 0x06000265 RID: 613 RVA: 0x0001958E File Offset: 0x0001778E
	private void OnDisable()
	{
		if (this.trigger == UIPlaySound.Trigger.OnDisable)
		{
			NGUITools.PlaySound(this.audioClip, this.volume, this.pitch);
		}
	}

	// Token: 0x06000266 RID: 614 RVA: 0x000195B4 File Offset: 0x000177B4
	private void OnHover(bool isOver)
	{
		if (this.trigger == UIPlaySound.Trigger.OnMouseOver)
		{
			if (this.mIsOver == isOver)
			{
				return;
			}
			this.mIsOver = isOver;
		}
		if (this.canPlay && ((isOver && this.trigger == UIPlaySound.Trigger.OnMouseOver) || (!isOver && this.trigger == UIPlaySound.Trigger.OnMouseOut)))
		{
			NGUITools.PlaySound(this.audioClip, this.volume, this.pitch);
		}
	}

	// Token: 0x06000267 RID: 615 RVA: 0x00019614 File Offset: 0x00017814
	private void OnPress(bool isPressed)
	{
		if (this.trigger == UIPlaySound.Trigger.OnPress)
		{
			if (this.mIsOver == isPressed)
			{
				return;
			}
			this.mIsOver = isPressed;
		}
		if (this.canPlay && ((isPressed && this.trigger == UIPlaySound.Trigger.OnPress) || (!isPressed && this.trigger == UIPlaySound.Trigger.OnRelease)))
		{
			NGUITools.PlaySound(this.audioClip, this.volume, this.pitch);
		}
	}

	// Token: 0x06000268 RID: 616 RVA: 0x00019673 File Offset: 0x00017873
	private void OnClick()
	{
		if (this.canPlay && this.trigger == UIPlaySound.Trigger.OnClick)
		{
			NGUITools.PlaySound(this.audioClip, this.volume, this.pitch);
		}
	}

	// Token: 0x06000269 RID: 617 RVA: 0x0001969D File Offset: 0x0001789D
	private void OnSelect(bool isSelected)
	{
		if (this.canPlay && (!isSelected || UICamera.currentScheme == UICamera.ControlScheme.Controller))
		{
			this.OnHover(isSelected);
		}
	}

	// Token: 0x0600026A RID: 618 RVA: 0x000196B9 File Offset: 0x000178B9
	public void Play()
	{
		NGUITools.PlaySound(this.audioClip, this.volume, this.pitch);
	}

	// Token: 0x040003CF RID: 975
	public AudioClip audioClip;

	// Token: 0x040003D0 RID: 976
	public UIPlaySound.Trigger trigger;

	// Token: 0x040003D1 RID: 977
	[Range(0f, 1f)]
	public float volume = 1f;

	// Token: 0x040003D2 RID: 978
	[Range(0f, 2f)]
	public float pitch = 1f;

	// Token: 0x040003D3 RID: 979
	private bool mIsOver;

	// Token: 0x02000631 RID: 1585
	[DoNotObfuscateNGUI]
	public enum Trigger
	{
		// Token: 0x0400458B RID: 17803
		OnClick,
		// Token: 0x0400458C RID: 17804
		OnMouseOver,
		// Token: 0x0400458D RID: 17805
		OnMouseOut,
		// Token: 0x0400458E RID: 17806
		OnPress,
		// Token: 0x0400458F RID: 17807
		OnRelease,
		// Token: 0x04004590 RID: 17808
		Custom,
		// Token: 0x04004591 RID: 17809
		OnEnable,
		// Token: 0x04004592 RID: 17810
		OnDisable
	}
}
