using System;
using UnityEngine;

// Token: 0x020001B5 RID: 437
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Film/ColorPerfection")]
public class CameraFilterPack_Film_ColorPerfection : MonoBehaviour
{
	// Token: 0x170002D4 RID: 724
	// (get) Token: 0x06000F52 RID: 3922 RVA: 0x00073472 File Offset: 0x00071672
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

	// Token: 0x06000F53 RID: 3923 RVA: 0x000734A6 File Offset: 0x000716A6
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Film_ColorPerfection");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000F54 RID: 3924 RVA: 0x000734C8 File Offset: 0x000716C8
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
			this.material.SetFloat("_Value", this.Gamma);
			this.material.SetFloat("_Value2", this.Value2);
			this.material.SetFloat("_Value3", this.Value3);
			this.material.SetFloat("_Value4", this.Value4);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000F55 RID: 3925 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000F56 RID: 3926 RVA: 0x000735C0 File Offset: 0x000717C0
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x0400121B RID: 4635
	public Shader SCShader;

	// Token: 0x0400121C RID: 4636
	private float TimeX = 1f;

	// Token: 0x0400121D RID: 4637
	private Material SCMaterial;

	// Token: 0x0400121E RID: 4638
	[Range(0f, 4f)]
	public float Gamma = 0.55f;

	// Token: 0x0400121F RID: 4639
	[Range(0f, 10f)]
	private float Value2 = 1f;

	// Token: 0x04001220 RID: 4640
	[Range(0f, 10f)]
	private float Value3 = 1f;

	// Token: 0x04001221 RID: 4641
	[Range(0f, 10f)]
	private float Value4 = 1f;
}
