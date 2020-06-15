using System;
using UnityEngine;

// Token: 0x0200017F RID: 383
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Drawing/Halftone")]
public class CameraFilterPack_Drawing_Halftone : MonoBehaviour
{
	// Token: 0x1700029E RID: 670
	// (get) Token: 0x06000E0D RID: 3597 RVA: 0x0006E5F1 File Offset: 0x0006C7F1
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

	// Token: 0x06000E0E RID: 3598 RVA: 0x0006E625 File Offset: 0x0006C825
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Drawing_Halftone");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000E0F RID: 3599 RVA: 0x0006E648 File Offset: 0x0006C848
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
			this.material.SetFloat("_Distortion", this.Threshold);
			this.material.SetFloat("_DotSize", this.DotSize);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000E10 RID: 3600 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000E11 RID: 3601 RVA: 0x0006E6E4 File Offset: 0x0006C8E4
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040010E0 RID: 4320
	public Shader SCShader;

	// Token: 0x040010E1 RID: 4321
	private float TimeX = 1f;

	// Token: 0x040010E2 RID: 4322
	private Material SCMaterial;

	// Token: 0x040010E3 RID: 4323
	[Range(0f, 1f)]
	public float Threshold = 0.6f;

	// Token: 0x040010E4 RID: 4324
	[Range(1f, 16f)]
	public float DotSize = 4f;
}
