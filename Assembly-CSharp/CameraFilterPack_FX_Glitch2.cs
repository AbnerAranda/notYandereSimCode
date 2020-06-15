using System;
using UnityEngine;

// Token: 0x020001A6 RID: 422
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Glitch/Glitch2")]
public class CameraFilterPack_FX_Glitch2 : MonoBehaviour
{
	// Token: 0x170002C5 RID: 709
	// (get) Token: 0x06000EF8 RID: 3832 RVA: 0x000720B2 File Offset: 0x000702B2
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

	// Token: 0x06000EF9 RID: 3833 RVA: 0x000720E6 File Offset: 0x000702E6
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/FX_Glitch2");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000EFA RID: 3834 RVA: 0x00072108 File Offset: 0x00070308
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
			this.material.SetFloat("_Glitch", this.Glitch);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000EFB RID: 3835 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000EFC RID: 3836 RVA: 0x000721BE File Offset: 0x000703BE
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040011D6 RID: 4566
	public Shader SCShader;

	// Token: 0x040011D7 RID: 4567
	private float TimeX = 1f;

	// Token: 0x040011D8 RID: 4568
	private Material SCMaterial;

	// Token: 0x040011D9 RID: 4569
	[Range(0f, 1f)]
	public float Glitch = 1f;
}
