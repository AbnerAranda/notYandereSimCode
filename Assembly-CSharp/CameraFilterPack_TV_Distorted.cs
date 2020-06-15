using System;
using UnityEngine;

// Token: 0x020001FF RID: 511
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/TV/Distorted")]
public class CameraFilterPack_TV_Distorted : MonoBehaviour
{
	// Token: 0x1700031E RID: 798
	// (get) Token: 0x06001131 RID: 4401 RVA: 0x0007C9CA File Offset: 0x0007ABCA
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

	// Token: 0x06001132 RID: 4402 RVA: 0x0007C9FE File Offset: 0x0007ABFE
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/TV_Distorted");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06001133 RID: 4403 RVA: 0x0007CA20 File Offset: 0x0007AC20
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
			this.material.SetFloat("_Distortion", this.Distortion);
			this.material.SetFloat("_RGB", this.RGB);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06001134 RID: 4404 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06001135 RID: 4405 RVA: 0x0007CABC File Offset: 0x0007ACBC
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001456 RID: 5206
	public Shader SCShader;

	// Token: 0x04001457 RID: 5207
	private float TimeX = 1f;

	// Token: 0x04001458 RID: 5208
	[Range(0f, 10f)]
	public float Distortion = 1f;

	// Token: 0x04001459 RID: 5209
	[Range(-0.01f, 0.01f)]
	public float RGB = 0.002f;

	// Token: 0x0400145A RID: 5210
	private Material SCMaterial;
}
