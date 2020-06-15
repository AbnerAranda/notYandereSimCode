using System;
using UnityEngine;

// Token: 0x020002FC RID: 764
public class HomeWindowScript : MonoBehaviour
{
	// Token: 0x0600176D RID: 5997 RVA: 0x000CAD88 File Offset: 0x000C8F88
	private void Start()
	{
		this.Sprite.color = new Color(this.Sprite.color.r, this.Sprite.color.g, this.Sprite.color.b, 0f);
	}

	// Token: 0x0600176E RID: 5998 RVA: 0x000CADDC File Offset: 0x000C8FDC
	private void Update()
	{
		this.Sprite.color = new Color(this.Sprite.color.r, this.Sprite.color.g, this.Sprite.color.b, Mathf.Lerp(this.Sprite.color.a, this.Show ? 1f : 0f, Time.deltaTime * 10f));
	}

	// Token: 0x04002094 RID: 8340
	public UISprite Sprite;

	// Token: 0x04002095 RID: 8341
	public bool Show;
}
