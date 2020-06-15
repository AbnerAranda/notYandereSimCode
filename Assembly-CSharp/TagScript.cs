using System;
using UnityEngine;

// Token: 0x02000416 RID: 1046
public class TagScript : MonoBehaviour
{
	// Token: 0x06001C16 RID: 7190 RVA: 0x0014A7FC File Offset: 0x001489FC
	private void Start()
	{
		this.Sprite.color = new Color(1f, 0f, 0f, 0f);
		this.MainCameraCamera = this.MainCamera.GetComponent<Camera>();
	}

	// Token: 0x06001C17 RID: 7191 RVA: 0x0014A834 File Offset: 0x00148A34
	private void Update()
	{
		if (this.Target != null && Vector3.Angle(this.MainCamera.forward, this.MainCamera.position - this.Target.position) > 90f)
		{
			Vector2 vector = this.MainCameraCamera.WorldToScreenPoint(this.Target.position);
			base.transform.position = this.UICamera.ScreenToWorldPoint(new Vector3(vector.x, vector.y, 1f));
		}
	}

	// Token: 0x04003457 RID: 13399
	public UISprite Sprite;

	// Token: 0x04003458 RID: 13400
	public Camera UICamera;

	// Token: 0x04003459 RID: 13401
	public Camera MainCameraCamera;

	// Token: 0x0400345A RID: 13402
	public Transform MainCamera;

	// Token: 0x0400345B RID: 13403
	public Transform Target;
}
