using System;
using UnityEngine;

// Token: 0x0200021A RID: 538
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Vision/Blood_Fast")]
public class CameraFilterPack_Vision_Blood_Fast : MonoBehaviour
{
	// Token: 0x17000339 RID: 825
	// (get) Token: 0x060011D3 RID: 4563 RVA: 0x0007F079 File Offset: 0x0007D279
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

	// Token: 0x060011D4 RID: 4564 RVA: 0x0007F0AD File Offset: 0x0007D2AD
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Vision_Blood_Fast");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x060011D5 RID: 4565 RVA: 0x0007F0D0 File Offset: 0x0007D2D0
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
			this.material.SetFloat("_Value", this.HoleSize);
			this.material.SetFloat("_Value2", this.HoleSmooth);
			this.material.SetFloat("_Value3", this.Color1);
			this.material.SetFloat("_Value4", this.Color2);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x060011D6 RID: 4566 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x060011D7 RID: 4567 RVA: 0x0007F1C8 File Offset: 0x0007D3C8
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040014EF RID: 5359
	public Shader SCShader;

	// Token: 0x040014F0 RID: 5360
	private float TimeX = 1f;

	// Token: 0x040014F1 RID: 5361
	private Material SCMaterial;

	// Token: 0x040014F2 RID: 5362
	[Range(0.01f, 1f)]
	public float HoleSize = 0.6f;

	// Token: 0x040014F3 RID: 5363
	[Range(-1f, 1f)]
	public float HoleSmooth = 0.3f;

	// Token: 0x040014F4 RID: 5364
	[Range(-2f, 2f)]
	public float Color1 = 0.2f;

	// Token: 0x040014F5 RID: 5365
	[Range(-2f, 2f)]
	public float Color2 = 0.9f;
}
