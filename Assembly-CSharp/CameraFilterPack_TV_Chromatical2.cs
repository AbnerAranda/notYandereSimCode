using System;
using UnityEngine;

// Token: 0x020001FD RID: 509
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/TV/Chromatical2")]
public class CameraFilterPack_TV_Chromatical2 : MonoBehaviour
{
	// Token: 0x1700031C RID: 796
	// (get) Token: 0x06001125 RID: 4389 RVA: 0x0007C6DF File Offset: 0x0007A8DF
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

	// Token: 0x06001126 RID: 4390 RVA: 0x0007C713 File Offset: 0x0007A913
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/TV_Chromatical2");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06001127 RID: 4391 RVA: 0x0007C734 File Offset: 0x0007A934
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
			this.material.SetFloat("_Value", this.Aberration);
			this.material.SetFloat("Fade", this.Fade);
			this.material.SetFloat("ZoomFade", this.ZoomFade);
			this.material.SetFloat("ZoomSpeed", this.ZoomSpeed);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06001128 RID: 4392 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06001129 RID: 4393 RVA: 0x0007C82C File Offset: 0x0007AA2C
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x0400144B RID: 5195
	public Shader SCShader;

	// Token: 0x0400144C RID: 5196
	private float TimeX = 1f;

	// Token: 0x0400144D RID: 5197
	private Material SCMaterial;

	// Token: 0x0400144E RID: 5198
	[Range(0f, 10f)]
	public float Aberration = 2f;

	// Token: 0x0400144F RID: 5199
	[Range(0f, 1f)]
	public float Fade = 1f;

	// Token: 0x04001450 RID: 5200
	[Range(0f, 1f)]
	public float ZoomFade = 1f;

	// Token: 0x04001451 RID: 5201
	[Range(0f, 8f)]
	public float ZoomSpeed = 1f;
}
