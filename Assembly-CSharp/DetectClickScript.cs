using System;
using UnityEngine;

// Token: 0x02000265 RID: 613
public class DetectClickScript : MonoBehaviour
{
	// Token: 0x0600133C RID: 4924 RVA: 0x000A1633 File Offset: 0x0009F833
	private void Start()
	{
		this.OriginalPosition = base.transform.localPosition;
		this.OriginalColor = this.Sprite.color;
	}

	// Token: 0x0600133D RID: 4925 RVA: 0x000A1658 File Offset: 0x0009F858
	private void Update()
	{
		RaycastHit raycastHit;
		if (Input.GetMouseButtonDown(0) && Physics.Raycast(this.GUICamera.ScreenPointToRay(Input.mousePosition), out raycastHit, 100f) && raycastHit.collider == this.MyCollider && this.Label.color.a == 1f)
		{
			this.Sprite.color = new Color(1f, 1f, 1f, 1f);
			this.Clicked = true;
		}
	}

	// Token: 0x0600133E RID: 4926 RVA: 0x000A16E1 File Offset: 0x0009F8E1
	private void OnTriggerEnter()
	{
		if (this.Label.color.a == 1f)
		{
			this.Sprite.color = Color.white;
		}
	}

	// Token: 0x0600133F RID: 4927 RVA: 0x000A170A File Offset: 0x0009F90A
	private void OnTriggerExit()
	{
		this.Sprite.color = this.OriginalColor;
	}

	// Token: 0x04001A0F RID: 6671
	public Vector3 OriginalPosition;

	// Token: 0x04001A10 RID: 6672
	public Color OriginalColor;

	// Token: 0x04001A11 RID: 6673
	public Collider MyCollider;

	// Token: 0x04001A12 RID: 6674
	public Camera GUICamera;

	// Token: 0x04001A13 RID: 6675
	public UISprite Sprite;

	// Token: 0x04001A14 RID: 6676
	public UILabel Label;

	// Token: 0x04001A15 RID: 6677
	public bool Clicked;
}
