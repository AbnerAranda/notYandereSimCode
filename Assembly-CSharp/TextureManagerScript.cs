using System;
using UnityEngine;

// Token: 0x02000423 RID: 1059
public class TextureManagerScript : MonoBehaviour
{
	// Token: 0x06001C4B RID: 7243 RVA: 0x00153070 File Offset: 0x00151270
	public Texture2D MergeTextures(Texture2D BackgroundTex, Texture2D TopTex)
	{
		Texture2D texture2D = new Texture2D(1024, 1024);
		Color32[] pixels = BackgroundTex.GetPixels32();
		Color32[] pixels2 = TopTex.GetPixels32();
		for (int i = 0; i < pixels2.Length; i++)
		{
			if (pixels2[i].a != 0)
			{
				pixels[i] = pixels2[i];
			}
		}
		texture2D.SetPixels32(pixels);
		texture2D.Apply();
		return texture2D;
	}

	// Token: 0x0400350D RID: 13581
	public Texture[] UniformTextures;

	// Token: 0x0400350E RID: 13582
	public Texture[] CasualTextures;

	// Token: 0x0400350F RID: 13583
	public Texture[] SocksTextures;

	// Token: 0x04003510 RID: 13584
	public Texture2D PurpleStockings;

	// Token: 0x04003511 RID: 13585
	public Texture2D GreenStockings;

	// Token: 0x04003512 RID: 13586
	public Texture2D Base2D;

	// Token: 0x04003513 RID: 13587
	public Texture2D Overlay2D;
}
