using System;
using UnityEngine;

// Token: 0x0200021B RID: 539
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Vision/Crystal")]
public class CameraFilterPack_Vision_Crystal : MonoBehaviour
{
	// Token: 0x1700033A RID: 826
	// (get) Token: 0x060011D9 RID: 4569 RVA: 0x0007F221 File Offset: 0x0007D421
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

	// Token: 0x060011DA RID: 4570 RVA: 0x0007F255 File Offset: 0x0007D455
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Vision_Crystal");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x060011DB RID: 4571 RVA: 0x0007F278 File Offset: 0x0007D478
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
			this.material.SetFloat("_Value", this.Value);
			this.material.SetFloat("_Value2", this.X);
			this.material.SetFloat("_Value3", this.Y);
			this.material.SetFloat("_Value4", this.Value4);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x060011DC RID: 4572 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x060011DD RID: 4573 RVA: 0x0007F370 File Offset: 0x0007D570
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040014F6 RID: 5366
	public Shader SCShader;

	// Token: 0x040014F7 RID: 5367
	private float TimeX = 1f;

	// Token: 0x040014F8 RID: 5368
	private Material SCMaterial;

	// Token: 0x040014F9 RID: 5369
	[Range(-10f, 10f)]
	public float Value = 1f;

	// Token: 0x040014FA RID: 5370
	[Range(-1f, 1f)]
	public float X = 1f;

	// Token: 0x040014FB RID: 5371
	[Range(-1f, 1f)]
	public float Y = 1f;

	// Token: 0x040014FC RID: 5372
	[Range(-1f, 1f)]
	private float Value4 = 1f;
}
