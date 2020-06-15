using System;
using UnityEngine;

// Token: 0x0200019D RID: 413
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/FX/DarkMatter")]
public class CameraFilterPack_FX_DarkMatter : MonoBehaviour
{
	// Token: 0x170002BC RID: 700
	// (get) Token: 0x06000EC2 RID: 3778 RVA: 0x0007120F File Offset: 0x0006F40F
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

	// Token: 0x06000EC3 RID: 3779 RVA: 0x00071243 File Offset: 0x0006F443
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/FX_DarkMatter");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000EC4 RID: 3780 RVA: 0x00071264 File Offset: 0x0006F464
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
			this.material.SetFloat("_Value", this.Speed);
			this.material.SetFloat("_Value2", this.Intensity);
			this.material.SetFloat("_Value3", this.PosX);
			this.material.SetFloat("_Value4", this.PosY);
			this.material.SetFloat("_Value5", this.Zoom);
			this.material.SetFloat("_Value6", this.DarkIntensity);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000EC5 RID: 3781 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000EC6 RID: 3782 RVA: 0x00071388 File Offset: 0x0006F588
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001198 RID: 4504
	public Shader SCShader;

	// Token: 0x04001199 RID: 4505
	private float TimeX = 1f;

	// Token: 0x0400119A RID: 4506
	private Material SCMaterial;

	// Token: 0x0400119B RID: 4507
	[Range(-10f, 10f)]
	public float Speed = 0.8f;

	// Token: 0x0400119C RID: 4508
	[Range(0f, 1f)]
	public float Intensity = 1f;

	// Token: 0x0400119D RID: 4509
	[Range(-1f, 2f)]
	public float PosX = 0.5f;

	// Token: 0x0400119E RID: 4510
	[Range(-1f, 2f)]
	public float PosY = 0.5f;

	// Token: 0x0400119F RID: 4511
	[Range(-2f, 2f)]
	public float Zoom = 0.33f;

	// Token: 0x040011A0 RID: 4512
	[Range(0f, 5f)]
	public float DarkIntensity = 2f;
}
