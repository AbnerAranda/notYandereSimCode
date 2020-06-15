using System;
using UnityEngine;

// Token: 0x0200018A RID: 394
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Drawing/Manga_Flash_Color")]
public class CameraFilterPack_Drawing_Manga_Flash_Color : MonoBehaviour
{
	// Token: 0x170002A9 RID: 681
	// (get) Token: 0x06000E4F RID: 3663 RVA: 0x0006F40D File Offset: 0x0006D60D
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

	// Token: 0x06000E50 RID: 3664 RVA: 0x0006F441 File Offset: 0x0006D641
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Drawing_Manga_Flash_Color");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000E51 RID: 3665 RVA: 0x0006F464 File Offset: 0x0006D664
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
			this.material.SetFloat("_Value", this.Size);
			this.material.SetFloat("_Value2", (float)this.Speed);
			this.material.SetFloat("_Value3", this.PosX);
			this.material.SetFloat("_Value4", this.PosY);
			this.material.SetFloat("_Intensity", this.Intensity);
			this.material.SetColor("Color", this.Color);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000E52 RID: 3666 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000E53 RID: 3667 RVA: 0x0006F589 File Offset: 0x0006D789
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001118 RID: 4376
	public Shader SCShader;

	// Token: 0x04001119 RID: 4377
	private float TimeX = 1f;

	// Token: 0x0400111A RID: 4378
	private Material SCMaterial;

	// Token: 0x0400111B RID: 4379
	[Range(1f, 10f)]
	public float Size = 1f;

	// Token: 0x0400111C RID: 4380
	public Color Color = new Color(0f, 0.7f, 1f, 1f);

	// Token: 0x0400111D RID: 4381
	[Range(0f, 30f)]
	public int Speed = 5;

	// Token: 0x0400111E RID: 4382
	[Range(0f, 1f)]
	public float PosX = 0.5f;

	// Token: 0x0400111F RID: 4383
	[Range(0f, 1f)]
	public float PosY = 0.5f;

	// Token: 0x04001120 RID: 4384
	[Range(0f, 1f)]
	public float Intensity = 1f;
}
