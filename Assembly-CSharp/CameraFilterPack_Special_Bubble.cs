using System;
using UnityEngine;

// Token: 0x020001F3 RID: 499
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Special/Bubble")]
public class CameraFilterPack_Special_Bubble : MonoBehaviour
{
	// Token: 0x17000312 RID: 786
	// (get) Token: 0x060010E9 RID: 4329 RVA: 0x0007B4AF File Offset: 0x000796AF
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

	// Token: 0x060010EA RID: 4330 RVA: 0x0007B4E3 File Offset: 0x000796E3
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Special_Bubble");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x060010EB RID: 4331 RVA: 0x0007B504 File Offset: 0x00079704
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
			this.material.SetFloat("_Value", this.X);
			this.material.SetFloat("_Value2", this.Y);
			this.material.SetFloat("_Value3", this.Rate);
			this.material.SetFloat("_Value4", this.Value4);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x060010EC RID: 4332 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x060010ED RID: 4333 RVA: 0x0007B5FC File Offset: 0x000797FC
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001403 RID: 5123
	public Shader SCShader;

	// Token: 0x04001404 RID: 5124
	private float TimeX = 1f;

	// Token: 0x04001405 RID: 5125
	private Material SCMaterial;

	// Token: 0x04001406 RID: 5126
	[Range(-4f, 4f)]
	public float X = 0.5f;

	// Token: 0x04001407 RID: 5127
	[Range(-4f, 4f)]
	public float Y = 0.5f;

	// Token: 0x04001408 RID: 5128
	[Range(0f, 5f)]
	public float Rate = 1f;

	// Token: 0x04001409 RID: 5129
	[Range(0f, 10f)]
	private float Value4 = 1f;
}
