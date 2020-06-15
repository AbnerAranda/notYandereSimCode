using System;
using UnityEngine;

// Token: 0x02000161 RID: 353
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Colors/NewPosterize")]
public class CameraFilterPack_Colors_NewPosterize : MonoBehaviour
{
	// Token: 0x17000280 RID: 640
	// (get) Token: 0x06000D59 RID: 3417 RVA: 0x0006B9E7 File Offset: 0x00069BE7
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

	// Token: 0x06000D5A RID: 3418 RVA: 0x0006BA1B File Offset: 0x00069C1B
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Colors_NewPosterize");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000D5B RID: 3419 RVA: 0x0006BA3C File Offset: 0x00069C3C
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
			this.material.SetFloat("_Value2", this.Colors);
			this.material.SetFloat("_Value3", this.Green_Mod);
			this.material.SetFloat("_Value4", this.Value4);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000D5C RID: 3420 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000D5D RID: 3421 RVA: 0x0006BB34 File Offset: 0x00069D34
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x0400102B RID: 4139
	public Shader SCShader;

	// Token: 0x0400102C RID: 4140
	private float TimeX = 1f;

	// Token: 0x0400102D RID: 4141
	private Material SCMaterial;

	// Token: 0x0400102E RID: 4142
	[Range(0f, 2f)]
	public float Gamma = 1f;

	// Token: 0x0400102F RID: 4143
	[Range(0f, 16f)]
	public float Colors = 11f;

	// Token: 0x04001030 RID: 4144
	[Range(-1f, 1f)]
	public float Green_Mod = 1f;

	// Token: 0x04001031 RID: 4145
	[Range(0f, 10f)]
	private float Value4 = 1f;
}
