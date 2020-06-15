using System;
using UnityEngine;

// Token: 0x0200021F RID: 543
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Vision/Psycho")]
public class CameraFilterPack_Vision_Psycho : MonoBehaviour
{
	// Token: 0x1700033E RID: 830
	// (get) Token: 0x060011F1 RID: 4593 RVA: 0x0007F955 File Offset: 0x0007DB55
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

	// Token: 0x060011F2 RID: 4594 RVA: 0x0007F989 File Offset: 0x0007DB89
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Vision_Psycho");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x060011F3 RID: 4595 RVA: 0x0007F9AC File Offset: 0x0007DBAC
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
			this.material.SetFloat("_Value", this.HoleSize);
			this.material.SetFloat("_Value2", this.HoleSmooth);
			this.material.SetFloat("_Value3", this.Color1);
			this.material.SetFloat("_Value4", this.Color2);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x060011F4 RID: 4596 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x060011F5 RID: 4597 RVA: 0x0007FAA4 File Offset: 0x0007DCA4
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001516 RID: 5398
	public Shader SCShader;

	// Token: 0x04001517 RID: 5399
	private float TimeX = 1f;

	// Token: 0x04001518 RID: 5400
	private Material SCMaterial;

	// Token: 0x04001519 RID: 5401
	[Range(0.01f, 1f)]
	public float HoleSize = 0.6f;

	// Token: 0x0400151A RID: 5402
	[Range(-1f, 1f)]
	public float HoleSmooth = 0.3f;

	// Token: 0x0400151B RID: 5403
	[Range(-2f, 2f)]
	public float Color1 = 0.2f;

	// Token: 0x0400151C RID: 5404
	[Range(-2f, 2f)]
	public float Color2 = 0.9f;
}
