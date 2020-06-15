using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000055 RID: 85
[AddComponentMenu("NGUI/Interaction/Event Trigger")]
public class UIEventTrigger : MonoBehaviour
{
	// Token: 0x17000025 RID: 37
	// (get) Token: 0x060001F9 RID: 505 RVA: 0x000177FC File Offset: 0x000159FC
	public bool isColliderEnabled
	{
		get
		{
			Collider component = base.GetComponent<Collider>();
			if (component != null)
			{
				return component.enabled;
			}
			Collider2D component2 = base.GetComponent<Collider2D>();
			return component2 != null && component2.enabled;
		}
	}

	// Token: 0x060001FA RID: 506 RVA: 0x00017838 File Offset: 0x00015A38
	private void OnHover(bool isOver)
	{
		if (UIEventTrigger.current != null || !this.isColliderEnabled)
		{
			return;
		}
		UIEventTrigger.current = this;
		if (isOver)
		{
			EventDelegate.Execute(this.onHoverOver);
		}
		else
		{
			EventDelegate.Execute(this.onHoverOut);
		}
		UIEventTrigger.current = null;
	}

	// Token: 0x060001FB RID: 507 RVA: 0x00017877 File Offset: 0x00015A77
	private void OnPress(bool pressed)
	{
		if (UIEventTrigger.current != null || !this.isColliderEnabled)
		{
			return;
		}
		UIEventTrigger.current = this;
		if (pressed)
		{
			EventDelegate.Execute(this.onPress);
		}
		else
		{
			EventDelegate.Execute(this.onRelease);
		}
		UIEventTrigger.current = null;
	}

	// Token: 0x060001FC RID: 508 RVA: 0x000178B6 File Offset: 0x00015AB6
	private void OnSelect(bool selected)
	{
		if (UIEventTrigger.current != null || !this.isColliderEnabled)
		{
			return;
		}
		UIEventTrigger.current = this;
		if (selected)
		{
			EventDelegate.Execute(this.onSelect);
		}
		else
		{
			EventDelegate.Execute(this.onDeselect);
		}
		UIEventTrigger.current = null;
	}

	// Token: 0x060001FD RID: 509 RVA: 0x000178F5 File Offset: 0x00015AF5
	private void OnClick()
	{
		if (UIEventTrigger.current != null || !this.isColliderEnabled)
		{
			return;
		}
		UIEventTrigger.current = this;
		EventDelegate.Execute(this.onClick);
		UIEventTrigger.current = null;
	}

	// Token: 0x060001FE RID: 510 RVA: 0x00017924 File Offset: 0x00015B24
	private void OnDoubleClick()
	{
		if (UIEventTrigger.current != null || !this.isColliderEnabled)
		{
			return;
		}
		UIEventTrigger.current = this;
		EventDelegate.Execute(this.onDoubleClick);
		UIEventTrigger.current = null;
	}

	// Token: 0x060001FF RID: 511 RVA: 0x00017953 File Offset: 0x00015B53
	private void OnDragStart()
	{
		if (UIEventTrigger.current != null)
		{
			return;
		}
		UIEventTrigger.current = this;
		EventDelegate.Execute(this.onDragStart);
		UIEventTrigger.current = null;
	}

	// Token: 0x06000200 RID: 512 RVA: 0x0001797A File Offset: 0x00015B7A
	private void OnDragEnd()
	{
		if (UIEventTrigger.current != null)
		{
			return;
		}
		UIEventTrigger.current = this;
		EventDelegate.Execute(this.onDragEnd);
		UIEventTrigger.current = null;
	}

	// Token: 0x06000201 RID: 513 RVA: 0x000179A1 File Offset: 0x00015BA1
	private void OnDragOver(GameObject go)
	{
		if (UIEventTrigger.current != null || !this.isColliderEnabled)
		{
			return;
		}
		UIEventTrigger.current = this;
		EventDelegate.Execute(this.onDragOver);
		UIEventTrigger.current = null;
	}

	// Token: 0x06000202 RID: 514 RVA: 0x000179D0 File Offset: 0x00015BD0
	private void OnDragOut(GameObject go)
	{
		if (UIEventTrigger.current != null || !this.isColliderEnabled)
		{
			return;
		}
		UIEventTrigger.current = this;
		EventDelegate.Execute(this.onDragOut);
		UIEventTrigger.current = null;
	}

	// Token: 0x06000203 RID: 515 RVA: 0x000179FF File Offset: 0x00015BFF
	private void OnDrag(Vector2 delta)
	{
		if (UIEventTrigger.current != null)
		{
			return;
		}
		UIEventTrigger.current = this;
		EventDelegate.Execute(this.onDrag);
		UIEventTrigger.current = null;
	}

	// Token: 0x04000380 RID: 896
	public static UIEventTrigger current;

	// Token: 0x04000381 RID: 897
	public List<EventDelegate> onHoverOver = new List<EventDelegate>();

	// Token: 0x04000382 RID: 898
	public List<EventDelegate> onHoverOut = new List<EventDelegate>();

	// Token: 0x04000383 RID: 899
	public List<EventDelegate> onPress = new List<EventDelegate>();

	// Token: 0x04000384 RID: 900
	public List<EventDelegate> onRelease = new List<EventDelegate>();

	// Token: 0x04000385 RID: 901
	public List<EventDelegate> onSelect = new List<EventDelegate>();

	// Token: 0x04000386 RID: 902
	public List<EventDelegate> onDeselect = new List<EventDelegate>();

	// Token: 0x04000387 RID: 903
	public List<EventDelegate> onClick = new List<EventDelegate>();

	// Token: 0x04000388 RID: 904
	public List<EventDelegate> onDoubleClick = new List<EventDelegate>();

	// Token: 0x04000389 RID: 905
	public List<EventDelegate> onDragStart = new List<EventDelegate>();

	// Token: 0x0400038A RID: 906
	public List<EventDelegate> onDragEnd = new List<EventDelegate>();

	// Token: 0x0400038B RID: 907
	public List<EventDelegate> onDragOver = new List<EventDelegate>();

	// Token: 0x0400038C RID: 908
	public List<EventDelegate> onDragOut = new List<EventDelegate>();

	// Token: 0x0400038D RID: 909
	public List<EventDelegate> onDrag = new List<EventDelegate>();
}
