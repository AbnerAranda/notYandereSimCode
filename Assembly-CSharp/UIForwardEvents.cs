using System;
using UnityEngine;

// Token: 0x02000056 RID: 86
[AddComponentMenu("NGUI/Interaction/Forward Events (Legacy)")]
public class UIForwardEvents : MonoBehaviour
{
	// Token: 0x06000205 RID: 517 RVA: 0x00017ACA File Offset: 0x00015CCA
	private void OnHover(bool isOver)
	{
		if (this.onHover && this.target != null)
		{
			this.target.SendMessage("OnHover", isOver, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x06000206 RID: 518 RVA: 0x00017AF9 File Offset: 0x00015CF9
	private void OnPress(bool pressed)
	{
		if (this.onPress && this.target != null)
		{
			this.target.SendMessage("OnPress", pressed, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x06000207 RID: 519 RVA: 0x00017B28 File Offset: 0x00015D28
	private void OnClick()
	{
		if (this.onClick && this.target != null)
		{
			this.target.SendMessage("OnClick", SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x06000208 RID: 520 RVA: 0x00017B51 File Offset: 0x00015D51
	private void OnDoubleClick()
	{
		if (this.onDoubleClick && this.target != null)
		{
			this.target.SendMessage("OnDoubleClick", SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x06000209 RID: 521 RVA: 0x00017B7A File Offset: 0x00015D7A
	private void OnSelect(bool selected)
	{
		if (this.onSelect && this.target != null)
		{
			this.target.SendMessage("OnSelect", selected, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x0600020A RID: 522 RVA: 0x00017BA9 File Offset: 0x00015DA9
	private void OnDrag(Vector2 delta)
	{
		if (this.onDrag && this.target != null)
		{
			this.target.SendMessage("OnDrag", delta, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x0600020B RID: 523 RVA: 0x00017BD8 File Offset: 0x00015DD8
	private void OnDrop(GameObject go)
	{
		if (this.onDrop && this.target != null)
		{
			this.target.SendMessage("OnDrop", go, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x0600020C RID: 524 RVA: 0x00017C02 File Offset: 0x00015E02
	private void OnSubmit()
	{
		if (this.onSubmit && this.target != null)
		{
			this.target.SendMessage("OnSubmit", SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x0600020D RID: 525 RVA: 0x00017C2B File Offset: 0x00015E2B
	private void OnScroll(float delta)
	{
		if (this.onScroll && this.target != null)
		{
			this.target.SendMessage("OnScroll", delta, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x0400038E RID: 910
	public GameObject target;

	// Token: 0x0400038F RID: 911
	public bool onHover;

	// Token: 0x04000390 RID: 912
	public bool onPress;

	// Token: 0x04000391 RID: 913
	public bool onClick;

	// Token: 0x04000392 RID: 914
	public bool onDoubleClick;

	// Token: 0x04000393 RID: 915
	public bool onSelect;

	// Token: 0x04000394 RID: 916
	public bool onDrag;

	// Token: 0x04000395 RID: 917
	public bool onDrop;

	// Token: 0x04000396 RID: 918
	public bool onSubmit;

	// Token: 0x04000397 RID: 919
	public bool onScroll;
}
