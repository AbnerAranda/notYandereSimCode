using System;
using UnityEngine;

// Token: 0x0200020A RID: 522
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/TV/Tiles")]
public class CameraFilterPack_TV_Tiles : MonoBehaviour
{
	// Token: 0x17000329 RID: 809
	// (get) Token: 0x06001173 RID: 4467 RVA: 0x0007D852 File Offset: 0x0007BA52
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

	// Token: 0x06001174 RID: 4468 RVA: 0x0007D886 File Offset: 0x0007BA86
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/TV_Tiles");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06001175 RID: 4469 RVA: 0x0007D8A8 File Offset: 0x0007BAA8
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
			this.material.SetFloat("_Value2", this.Intensity);
			this.material.SetFloat("_Value3", this.StretchX);
			this.material.SetFloat("_Value4", this.StretchY);
			this.material.SetFloat("Fade", this.Fade);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06001176 RID: 4470 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06001177 RID: 4471 RVA: 0x0007D9B6 File Offset: 0x0007BBB6
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x0400148D RID: 5261
	public Shader SCShader;

	// Token: 0x0400148E RID: 5262
	private float TimeX = 1f;

	// Token: 0x0400148F RID: 5263
	private Material SCMaterial;

	// Token: 0x04001490 RID: 5264
	[Range(0.5f, 2f)]
	public float Size = 1f;

	// Token: 0x04001491 RID: 5265
	[Range(0f, 10f)]
	public float Intensity = 4f;

	// Token: 0x04001492 RID: 5266
	[Range(0f, 1f)]
	public float StretchX = 0.6f;

	// Token: 0x04001493 RID: 5267
	[Range(0f, 1f)]
	public float StretchY = 0.4f;

	// Token: 0x04001494 RID: 5268
	[Range(0f, 1f)]
	public float Fade = 0.6f;
}
