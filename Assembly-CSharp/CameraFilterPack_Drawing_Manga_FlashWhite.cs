using System;
using UnityEngine;

// Token: 0x02000189 RID: 393
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Drawing/Manga_FlashWhite")]
public class CameraFilterPack_Drawing_Manga_FlashWhite : MonoBehaviour
{
	// Token: 0x170002A8 RID: 680
	// (get) Token: 0x06000E49 RID: 3657 RVA: 0x0006F239 File Offset: 0x0006D439
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

	// Token: 0x06000E4A RID: 3658 RVA: 0x0006F26D File Offset: 0x0006D46D
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Drawing_Manga_FlashWhite");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000E4B RID: 3659 RVA: 0x0006F290 File Offset: 0x0006D490
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
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000E4C RID: 3660 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000E4D RID: 3661 RVA: 0x0006F39F File Offset: 0x0006D59F
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001110 RID: 4368
	public Shader SCShader;

	// Token: 0x04001111 RID: 4369
	private float TimeX = 1f;

	// Token: 0x04001112 RID: 4370
	private Material SCMaterial;

	// Token: 0x04001113 RID: 4371
	[Range(1f, 10f)]
	public float Size = 1f;

	// Token: 0x04001114 RID: 4372
	[Range(0f, 30f)]
	public int Speed = 5;

	// Token: 0x04001115 RID: 4373
	[Range(-1f, 1f)]
	public float PosX = 0.5f;

	// Token: 0x04001116 RID: 4374
	[Range(-1f, 1f)]
	public float PosY = 0.5f;

	// Token: 0x04001117 RID: 4375
	[Range(0f, 1f)]
	public float Intensity = 1f;
}
