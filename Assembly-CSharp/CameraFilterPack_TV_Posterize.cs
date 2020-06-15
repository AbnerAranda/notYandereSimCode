using System;
using UnityEngine;

// Token: 0x02000208 RID: 520
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/TV/Posterize")]
public class CameraFilterPack_TV_Posterize : MonoBehaviour
{
	// Token: 0x17000327 RID: 807
	// (get) Token: 0x06001167 RID: 4455 RVA: 0x0007D5DB File Offset: 0x0007B7DB
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

	// Token: 0x06001168 RID: 4456 RVA: 0x0007D60F File Offset: 0x0007B80F
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/TV_Posterize");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06001169 RID: 4457 RVA: 0x0007D630 File Offset: 0x0007B830
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
			this.material.SetFloat("Fade", this.Fade);
			this.material.SetFloat("_Distortion", this.Posterize);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x0600116A RID: 4458 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x0600116B RID: 4459 RVA: 0x0007D6CC File Offset: 0x0007B8CC
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001484 RID: 5252
	public Shader SCShader;

	// Token: 0x04001485 RID: 5253
	private float TimeX = 1f;

	// Token: 0x04001486 RID: 5254
	[Range(1f, 256f)]
	public float Posterize = 64f;

	// Token: 0x04001487 RID: 5255
	[Range(0f, 1f)]
	public float Fade = 1f;

	// Token: 0x04001488 RID: 5256
	private Material SCMaterial;
}
