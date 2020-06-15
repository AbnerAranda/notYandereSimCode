using System;
using UnityEngine;

// Token: 0x02000206 RID: 518
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Old Film/Old_Movie_2")]
public class CameraFilterPack_TV_Old_Movie_2 : MonoBehaviour
{
	// Token: 0x17000325 RID: 805
	// (get) Token: 0x0600115B RID: 4443 RVA: 0x0007D2BA File Offset: 0x0007B4BA
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

	// Token: 0x0600115C RID: 4444 RVA: 0x0007D2EE File Offset: 0x0007B4EE
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/TV_Old_Movie_2");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x0600115D RID: 4445 RVA: 0x0007D310 File Offset: 0x0007B510
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
			this.material.SetFloat("_Value", this.FramePerSecond);
			this.material.SetFloat("_Value2", this.Contrast);
			this.material.SetFloat("_Value3", this.Burn);
			this.material.SetFloat("_Value4", this.SceneCut);
			this.material.SetFloat("_Fade", this.Fade);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x0600115E RID: 4446 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x0600115F RID: 4447 RVA: 0x0007D41E File Offset: 0x0007B61E
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001477 RID: 5239
	public Shader SCShader;

	// Token: 0x04001478 RID: 5240
	private float TimeX = 1f;

	// Token: 0x04001479 RID: 5241
	private Material SCMaterial;

	// Token: 0x0400147A RID: 5242
	[Range(1f, 60f)]
	public float FramePerSecond = 15f;

	// Token: 0x0400147B RID: 5243
	[Range(0f, 5f)]
	public float Contrast = 1f;

	// Token: 0x0400147C RID: 5244
	[Range(0f, 4f)]
	public float Burn;

	// Token: 0x0400147D RID: 5245
	[Range(0f, 16f)]
	public float SceneCut = 1f;

	// Token: 0x0400147E RID: 5246
	[Range(0f, 1f)]
	public float Fade = 1f;
}
