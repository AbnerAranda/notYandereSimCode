using System;
using UnityEngine;

// Token: 0x02000168 RID: 360
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Distortion/Dissipation")]
public class CameraFilterPack_Distortion_Dissipation : MonoBehaviour
{
	// Token: 0x17000287 RID: 647
	// (get) Token: 0x06000D83 RID: 3459 RVA: 0x0006C428 File Offset: 0x0006A628
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

	// Token: 0x06000D84 RID: 3460 RVA: 0x0006C45C File Offset: 0x0006A65C
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Distortion_Dissipation");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000D85 RID: 3461 RVA: 0x0006C480 File Offset: 0x0006A680
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
			this.material.SetFloat("_Value", this.Dissipation);
			this.material.SetFloat("_Value2", this.Colors);
			this.material.SetFloat("_Value3", this.Green_Mod);
			this.material.SetFloat("_Value4", this.Value4);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000D86 RID: 3462 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000D87 RID: 3463 RVA: 0x0006C578 File Offset: 0x0006A778
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001058 RID: 4184
	public Shader SCShader;

	// Token: 0x04001059 RID: 4185
	private float TimeX = 1f;

	// Token: 0x0400105A RID: 4186
	private Material SCMaterial;

	// Token: 0x0400105B RID: 4187
	[Range(0f, 2.99f)]
	public float Dissipation = 1f;

	// Token: 0x0400105C RID: 4188
	[Range(0f, 16f)]
	private float Colors = 11f;

	// Token: 0x0400105D RID: 4189
	[Range(-1f, 1f)]
	private float Green_Mod = 1f;

	// Token: 0x0400105E RID: 4190
	[Range(0f, 10f)]
	private float Value4 = 1f;
}
