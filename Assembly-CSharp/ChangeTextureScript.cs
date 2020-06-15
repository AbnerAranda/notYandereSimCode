using System;
using UnityEngine;

// Token: 0x0200022E RID: 558
public class ChangeTextureScript : MonoBehaviour
{
	// Token: 0x0600122E RID: 4654 RVA: 0x000811C0 File Offset: 0x0007F3C0
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.LeftAlt))
		{
			this.ID++;
			if (this.ID == this.Textures.Length)
			{
				this.ID = 1;
			}
			this.MyRenderer.material.mainTexture = this.Textures[this.ID];
		}
	}

	// Token: 0x04001571 RID: 5489
	public Renderer MyRenderer;

	// Token: 0x04001572 RID: 5490
	public Texture[] Textures;

	// Token: 0x04001573 RID: 5491
	public int ID = 1;
}
