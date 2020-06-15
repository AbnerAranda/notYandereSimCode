using System;
using UnityEngine;

// Token: 0x02000053 RID: 83
[AddComponentMenu("NGUI/Interaction/Drag Scroll View")]
public class UIDragScrollView : MonoBehaviour
{
	// Token: 0x060001E6 RID: 486 RVA: 0x00017098 File Offset: 0x00015298
	private void OnEnable()
	{
		this.mTrans = base.transform;
		if (this.scrollView == null && this.draggablePanel != null)
		{
			this.scrollView = this.draggablePanel;
			this.draggablePanel = null;
		}
		if (this.mStarted && (this.mAutoFind || this.mScroll == null))
		{
			this.FindScrollView();
		}
	}

	// Token: 0x060001E7 RID: 487 RVA: 0x00017104 File Offset: 0x00015304
	private void Start()
	{
		this.mStarted = true;
		this.FindScrollView();
	}

	// Token: 0x060001E8 RID: 488 RVA: 0x00017114 File Offset: 0x00015314
	private void FindScrollView()
	{
		UIScrollView uiscrollView = NGUITools.FindInParents<UIScrollView>(this.mTrans);
		if (this.scrollView == null || (this.mAutoFind && uiscrollView != this.scrollView))
		{
			this.scrollView = uiscrollView;
			this.mAutoFind = true;
		}
		else if (this.scrollView == uiscrollView)
		{
			this.mAutoFind = true;
		}
		this.mScroll = this.scrollView;
	}

	// Token: 0x060001E9 RID: 489 RVA: 0x00017182 File Offset: 0x00015382
	private void OnDisable()
	{
		if (this.mPressed && this.mScroll != null && this.mScroll.GetComponentInChildren<UIWrapContent>() == null)
		{
			this.mScroll.Press(false);
			this.mScroll = null;
		}
	}

	// Token: 0x060001EA RID: 490 RVA: 0x000171C0 File Offset: 0x000153C0
	private void OnPress(bool pressed)
	{
		this.mPressed = pressed;
		if (this.mAutoFind && this.mScroll != this.scrollView)
		{
			this.mScroll = this.scrollView;
			this.mAutoFind = false;
		}
		if (this.scrollView && base.enabled && NGUITools.GetActive(base.gameObject))
		{
			this.scrollView.Press(pressed);
			if (!pressed && this.mAutoFind)
			{
				this.scrollView = NGUITools.FindInParents<UIScrollView>(this.mTrans);
				this.mScroll = this.scrollView;
			}
		}
	}

	// Token: 0x060001EB RID: 491 RVA: 0x00017258 File Offset: 0x00015458
	private void OnDrag(Vector2 delta)
	{
		if (this.scrollView && NGUITools.GetActive(this))
		{
			this.scrollView.Drag();
		}
	}

	// Token: 0x060001EC RID: 492 RVA: 0x0001727A File Offset: 0x0001547A
	private void OnScroll(float delta)
	{
		if (this.scrollView && NGUITools.GetActive(this))
		{
			this.scrollView.Scroll(delta);
		}
	}

	// Token: 0x060001ED RID: 493 RVA: 0x0001729D File Offset: 0x0001549D
	public void OnPan(Vector2 delta)
	{
		if (this.scrollView && NGUITools.GetActive(this))
		{
			this.scrollView.OnPan(delta);
		}
	}

	// Token: 0x0400036B RID: 875
	public UIScrollView scrollView;

	// Token: 0x0400036C RID: 876
	[HideInInspector]
	[SerializeField]
	private UIScrollView draggablePanel;

	// Token: 0x0400036D RID: 877
	private Transform mTrans;

	// Token: 0x0400036E RID: 878
	private UIScrollView mScroll;

	// Token: 0x0400036F RID: 879
	private bool mAutoFind;

	// Token: 0x04000370 RID: 880
	private bool mStarted;

	// Token: 0x04000371 RID: 881
	[NonSerialized]
	private bool mPressed;
}
