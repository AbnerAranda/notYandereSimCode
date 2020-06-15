using System;
using UnityEngine;

// Token: 0x0200017E RID: 382
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Drawing/EnhancedComics")]
public class CameraFilterPack_Drawing_EnhancedComics : MonoBehaviour
{
	// Token: 0x1700029D RID: 669
	// (get) Token: 0x06000E07 RID: 3591 RVA: 0x0006E3D6 File Offset: 0x0006C5D6
	private Material material
	{
		get
		{
			if (this.SCMaterial == null)
			{
				this.SCMaterial = new Material(this.SCShader);
				this.SCMaterial.hideFlags = HideFlags.HideAndDontSave;
			}
			return this.SCMaterial;
		}
	}

	// Token: 0x06000E08 RID: 3592 RVA: 0x0006E40A File Offset: 0x0006C60A
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Drawing_EnhancedComics");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000E09 RID: 3593 RVA: 0x0006E42C File Offset: 0x0006C62C
	private void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
	{
		if (this.SCShader != null)
		{
			this.TimeX += Time.deltaTime;
			if (this.TimeX > 100f)
			{
				this.TimeX = 0f;
			}
			this.material.SetFloat("_TimeX", this.TimeX);
			this.material.SetFloat("_DotSize", this.DotSize);
			this.material.SetFloat("_ColorR", this._ColorR);
			this.material.SetFloat("_ColorG", this._ColorG);
			this.material.SetFloat("_ColorB", this._ColorB);
			this.material.SetFloat("_Blood", this._Blood);
			this.material.SetColor("_ColorRGB", this.ColorRGB);
			this.material.SetFloat("_SmoothStart", this._SmoothStart);
			this.material.SetFloat("_SmoothEnd", this._SmoothEnd);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000E0A RID: 3594 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000E0B RID: 3595 RVA: 0x0006E54F File Offset: 0x0006C74F
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040010D5 RID: 4309
	public Shader SCShader;

	// Token: 0x040010D6 RID: 4310
	private float TimeX = 1f;

	// Token: 0x040010D7 RID: 4311
	private Material SCMaterial;

	// Token: 0x040010D8 RID: 4312
	[Range(0f, 1f)]
	public float DotSize = 0.15f;

	// Token: 0x040010D9 RID: 4313
	[Range(0f, 1f)]
	public float _ColorR = 0.9f;

	// Token: 0x040010DA RID: 4314
	[Range(0f, 1f)]
	public float _ColorG = 0.4f;

	// Token: 0x040010DB RID: 4315
	[Range(0f, 1f)]
	public float _ColorB = 0.4f;

	// Token: 0x040010DC RID: 4316
	[Range(0f, 1f)]
	public float _Blood = 0.5f;

	// Token: 0x040010DD RID: 4317
	[Range(0f, 1f)]
	public float _SmoothStart = 0.02f;

	// Token: 0x040010DE RID: 4318
	[Range(0f, 1f)]
	public float _SmoothEnd = 0.1f;

	// Token: 0x040010DF RID: 4319
	public Color ColorRGB = new Color(1f, 0f, 0f);
}
