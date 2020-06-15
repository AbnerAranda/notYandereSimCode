using System;
using UnityEngine;

// Token: 0x02000080 RID: 128
[AddComponentMenu("NGUI/Internal/Event Listener")]
public class UIEventListener : MonoBehaviour
{
	// Token: 0x17000099 RID: 153
	// (get) Token: 0x06000509 RID: 1289 RVA: 0x00030E18 File Offset: 0x0002F018
	private bool isColliderEnabled
	{
		get
		{
			if (!this.needsActiveCollider)
			{
				return true;
			}
			Collider component = base.GetComponent<Collider>();
			if (component != null)
			{
				return component.enabled;
			}
			Collider2D component2 = base.GetComponent<Collider2D>();
			return component2 != null && component2.enabled;
		}
	}

	// Token: 0x0600050A RID: 1290 RVA: 0x00030E5E File Offset: 0x0002F05E
	private void OnSubmit()
	{
		if (this.isColliderEnabled && this.onSubmit != null)
		{
			this.onSubmit(base.gameObject);
		}
	}

	// Token: 0x0600050B RID: 1291 RVA: 0x00030E81 File Offset: 0x0002F081
	private void OnClick()
	{
		if (this.isColliderEnabled && this.onClick != null)
		{
			this.onClick(base.gameObject);
		}
	}

	// Token: 0x0600050C RID: 1292 RVA: 0x00030EA4 File Offset: 0x0002F0A4
	private void OnDoubleClick()
	{
		if (this.isColliderEnabled && this.onDoubleClick != null)
		{
			this.onDoubleClick(base.gameObject);
		}
	}

	// Token: 0x0600050D RID: 1293 RVA: 0x00030EC7 File Offset: 0x0002F0C7
	private void OnHover(bool isOver)
	{
		if (this.isColliderEnabled && this.onHover != null)
		{
			this.onHover(base.gameObject, isOver);
		}
	}

	// Token: 0x0600050E RID: 1294 RVA: 0x00030EEB File Offset: 0x0002F0EB
	private void OnPress(bool isPressed)
	{
		if (this.isColliderEnabled && this.onPress != null)
		{
			this.onPress(base.gameObject, isPressed);
		}
	}

	// Token: 0x0600050F RID: 1295 RVA: 0x00030F0F File Offset: 0x0002F10F
	private void OnSelect(bool selected)
	{
		if (this.isColliderEnabled && this.onSelect != null)
		{
			this.onSelect(base.gameObject, selected);
		}
	}

	// Token: 0x06000510 RID: 1296 RVA: 0x00030F33 File Offset: 0x0002F133
	private void OnScroll(float delta)
	{
		if (this.isColliderEnabled && this.onScroll != null)
		{
			this.onScroll(base.gameObject, delta);
		}
	}

	// Token: 0x06000511 RID: 1297 RVA: 0x00030F57 File Offset: 0x0002F157
	private void OnDragStart()
	{
		if (this.onDragStart != null)
		{
			this.onDragStart(base.gameObject);
		}
	}

	// Token: 0x06000512 RID: 1298 RVA: 0x00030F72 File Offset: 0x0002F172
	private void OnDrag(Vector2 delta)
	{
		if (this.onDrag != null)
		{
			this.onDrag(base.gameObject, delta);
		}
	}

	// Token: 0x06000513 RID: 1299 RVA: 0x00030F8E File Offset: 0x0002F18E
	private void OnDragOver()
	{
		if (this.isColliderEnabled && this.onDragOver != null)
		{
			this.onDragOver(base.gameObject);
		}
	}

	// Token: 0x06000514 RID: 1300 RVA: 0x00030FB1 File Offset: 0x0002F1B1
	private void OnDragOut()
	{
		if (this.isColliderEnabled && this.onDragOut != null)
		{
			this.onDragOut(base.gameObject);
		}
	}

	// Token: 0x06000515 RID: 1301 RVA: 0x00030FD4 File Offset: 0x0002F1D4
	private void OnDragEnd()
	{
		if (this.onDragEnd != null)
		{
			this.onDragEnd(base.gameObject);
		}
	}

	// Token: 0x06000516 RID: 1302 RVA: 0x00030FEF File Offset: 0x0002F1EF
	private void OnDrop(GameObject go)
	{
		if (this.isColliderEnabled && this.onDrop != null)
		{
			this.onDrop(base.gameObject, go);
		}
	}

	// Token: 0x06000517 RID: 1303 RVA: 0x00031013 File Offset: 0x0002F213
	private void OnKey(KeyCode key)
	{
		if (this.isColliderEnabled && this.onKey != null)
		{
			this.onKey(base.gameObject, key);
		}
	}

	// Token: 0x06000518 RID: 1304 RVA: 0x00031037 File Offset: 0x0002F237
	private void OnTooltip(bool show)
	{
		if (this.isColliderEnabled && this.onTooltip != null)
		{
			this.onTooltip(base.gameObject, show);
		}
	}

	// Token: 0x06000519 RID: 1305 RVA: 0x0003105C File Offset: 0x0002F25C
	public void Clear()
	{
		this.onSubmit = null;
		this.onClick = null;
		this.onDoubleClick = null;
		this.onHover = null;
		this.onPress = null;
		this.onSelect = null;
		this.onScroll = null;
		this.onDragStart = null;
		this.onDrag = null;
		this.onDragOver = null;
		this.onDragOut = null;
		this.onDragEnd = null;
		this.onDrop = null;
		this.onKey = null;
		this.onTooltip = null;
	}

	// Token: 0x0600051A RID: 1306 RVA: 0x000310D4 File Offset: 0x0002F2D4
	public static UIEventListener Get(GameObject go)
	{
		UIEventListener uieventListener = go.GetComponent<UIEventListener>();
		if (uieventListener == null)
		{
			uieventListener = go.AddComponent<UIEventListener>();
		}
		return uieventListener;
	}

	// Token: 0x04000556 RID: 1366
	public object parameter;

	// Token: 0x04000557 RID: 1367
	public UIEventListener.VoidDelegate onSubmit;

	// Token: 0x04000558 RID: 1368
	public UIEventListener.VoidDelegate onClick;

	// Token: 0x04000559 RID: 1369
	public UIEventListener.VoidDelegate onDoubleClick;

	// Token: 0x0400055A RID: 1370
	public UIEventListener.BoolDelegate onHover;

	// Token: 0x0400055B RID: 1371
	public UIEventListener.BoolDelegate onPress;

	// Token: 0x0400055C RID: 1372
	public UIEventListener.BoolDelegate onSelect;

	// Token: 0x0400055D RID: 1373
	public UIEventListener.FloatDelegate onScroll;

	// Token: 0x0400055E RID: 1374
	public UIEventListener.VoidDelegate onDragStart;

	// Token: 0x0400055F RID: 1375
	public UIEventListener.VectorDelegate onDrag;

	// Token: 0x04000560 RID: 1376
	public UIEventListener.VoidDelegate onDragOver;

	// Token: 0x04000561 RID: 1377
	public UIEventListener.VoidDelegate onDragOut;

	// Token: 0x04000562 RID: 1378
	public UIEventListener.VoidDelegate onDragEnd;

	// Token: 0x04000563 RID: 1379
	public UIEventListener.ObjectDelegate onDrop;

	// Token: 0x04000564 RID: 1380
	public UIEventListener.KeyCodeDelegate onKey;

	// Token: 0x04000565 RID: 1381
	public UIEventListener.BoolDelegate onTooltip;

	// Token: 0x04000566 RID: 1382
	public bool needsActiveCollider = true;

	// Token: 0x0200065A RID: 1626
	// (Invoke) Token: 0x06002B06 RID: 11014
	public delegate void VoidDelegate(GameObject go);

	// Token: 0x0200065B RID: 1627
	// (Invoke) Token: 0x06002B0A RID: 11018
	public delegate void BoolDelegate(GameObject go, bool state);

	// Token: 0x0200065C RID: 1628
	// (Invoke) Token: 0x06002B0E RID: 11022
	public delegate void FloatDelegate(GameObject go, float delta);

	// Token: 0x0200065D RID: 1629
	// (Invoke) Token: 0x06002B12 RID: 11026
	public delegate void VectorDelegate(GameObject go, Vector2 delta);

	// Token: 0x0200065E RID: 1630
	// (Invoke) Token: 0x06002B16 RID: 11030
	public delegate void ObjectDelegate(GameObject go, GameObject obj);

	// Token: 0x0200065F RID: 1631
	// (Invoke) Token: 0x06002B1A RID: 11034
	public delegate void KeyCodeDelegate(GameObject go, KeyCode key);
}
