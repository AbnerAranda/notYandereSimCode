using System;
using UnityEngine;

// Token: 0x020001BE RID: 446
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Glitch/Mozaic")]
public class CameraFilterPack_Glitch_Mozaic : MonoBehaviour
{
	// Token: 0x170002DD RID: 733
	// (get) Token: 0x06000F88 RID: 3976 RVA: 0x00074915 File Offset: 0x00072B15
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

	// Token: 0x06000F89 RID: 3977 RVA: 0x00074949 File Offset: 0x00072B49
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Glitch_Mozaic");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000F8A RID: 3978 RVA: 0x0007496C File Offset: 0x00072B6C
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
			this.material.SetFloat("_Value", this.Intensity);
			this.material.SetFloat("_Value2", this.Value2);
			this.material.SetFloat("_Value3", this.Value3);
			this.material.SetFloat("_Value4", this.Value4);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000F8B RID: 3979 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000F8C RID: 3980 RVA: 0x00074A64 File Offset: 0x00072C64
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001282 RID: 4738
	public Shader SCShader;

	// Token: 0x04001283 RID: 4739
	private float TimeX = 1f;

	// Token: 0x04001284 RID: 4740
	private Material SCMaterial;

	// Token: 0x04001285 RID: 4741
	[Range(0.001f, 10f)]
	public float Intensity = 1f;

	// Token: 0x04001286 RID: 4742
	[Range(0f, 10f)]
	private float Value2 = 1f;

	// Token: 0x04001287 RID: 4743
	[Range(0f, 10f)]
	private float Value3 = 1f;

	// Token: 0x04001288 RID: 4744
	[Range(0f, 10f)]
	private float Value4 = 1f;
}
