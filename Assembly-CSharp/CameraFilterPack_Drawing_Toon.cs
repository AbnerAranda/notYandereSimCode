using System;
using UnityEngine;

// Token: 0x0200018F RID: 399
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Drawing/Toon")]
public class CameraFilterPack_Drawing_Toon : MonoBehaviour
{
	// Token: 0x170002AE RID: 686
	// (get) Token: 0x06000E6D RID: 3693 RVA: 0x0006FEB2 File Offset: 0x0006E0B2
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

	// Token: 0x06000E6E RID: 3694 RVA: 0x0006FEE6 File Offset: 0x0006E0E6
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Drawing_Toon");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000E6F RID: 3695 RVA: 0x0006FF08 File Offset: 0x0006E108
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

	// Token: 0x06000E70 RID: 3696 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000E71 RID: 3697 RVA: 0x0006FFA4 File Offset: 0x0006E1A4
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x0400114C RID: 4428
	public Shader SCShader;

	// Token: 0x0400114D RID: 4429
	private Material SCMaterial;

	// Token: 0x0400114E RID: 4430
	private float TimeX = 1f;

	// Token: 0x0400114F RID: 4431
	[Range(0f, 2f)]
	public float Threshold = 1f;

	// Token: 0x04001150 RID: 4432
	[Range(0f, 8f)]
	public float DotSize = 1f;
}
