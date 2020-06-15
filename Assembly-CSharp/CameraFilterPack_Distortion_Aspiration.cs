using System;
using UnityEngine;

// Token: 0x02000165 RID: 357
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Distortion/Aspiration")]
public class CameraFilterPack_Distortion_Aspiration : MonoBehaviour
{
	// Token: 0x17000284 RID: 644
	// (get) Token: 0x06000D71 RID: 3441 RVA: 0x0006BF8C File Offset: 0x0006A18C
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

	// Token: 0x06000D72 RID: 3442 RVA: 0x0006BFC0 File Offset: 0x0006A1C0
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Distortion_Aspiration");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000D73 RID: 3443 RVA: 0x0006BFE4 File Offset: 0x0006A1E4
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
			this.material.SetFloat("_Value", 1f - this.Value);
			this.material.SetFloat("_Value2", this.PosX);
			this.material.SetFloat("_Value3", this.PosY);
			this.material.SetFloat("_Value4", this.Value4);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000D74 RID: 3444 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000D75 RID: 3445 RVA: 0x0006C0E2 File Offset: 0x0006A2E2
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001045 RID: 4165
	public Shader SCShader;

	// Token: 0x04001046 RID: 4166
	private float TimeX = 1f;

	// Token: 0x04001047 RID: 4167
	private Material SCMaterial;

	// Token: 0x04001048 RID: 4168
	[Range(0f, 1f)]
	public float Value = 0.8f;

	// Token: 0x04001049 RID: 4169
	[Range(-1f, 1f)]
	public float PosX = 0.5f;

	// Token: 0x0400104A RID: 4170
	[Range(-1f, 1f)]
	public float PosY = 0.5f;

	// Token: 0x0400104B RID: 4171
	[Range(0f, 10f)]
	private float Value4 = 1f;
}
