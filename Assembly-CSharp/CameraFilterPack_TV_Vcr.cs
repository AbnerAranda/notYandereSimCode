using System;
using UnityEngine;

// Token: 0x0200020D RID: 525
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/VHS/VCR Distortion")]
public class CameraFilterPack_TV_Vcr : MonoBehaviour
{
	// Token: 0x1700032C RID: 812
	// (get) Token: 0x06001185 RID: 4485 RVA: 0x0007DD69 File Offset: 0x0007BF69
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

	// Token: 0x06001186 RID: 4486 RVA: 0x0007DD9D File Offset: 0x0007BF9D
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/TV_Vcr");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06001187 RID: 4487 RVA: 0x0007DDC0 File Offset: 0x0007BFC0
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
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06001188 RID: 4488 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06001189 RID: 4489 RVA: 0x0007DE46 File Offset: 0x0007C046
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040014A3 RID: 5283
	public Shader SCShader;

	// Token: 0x040014A4 RID: 5284
	private float TimeX = 1f;

	// Token: 0x040014A5 RID: 5285
	[Range(1f, 10f)]
	public float Distortion = 1f;

	// Token: 0x040014A6 RID: 5286
	private Material SCMaterial;
}
