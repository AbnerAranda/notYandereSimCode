using System;
using UnityEngine;

// Token: 0x0200021E RID: 542
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Vision/Plasma")]
public class CameraFilterPack_Vision_Plasma : MonoBehaviour
{
	// Token: 0x1700033D RID: 829
	// (get) Token: 0x060011EB RID: 4587 RVA: 0x0007F7AC File Offset: 0x0007D9AC
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

	// Token: 0x060011EC RID: 4588 RVA: 0x0007F7E0 File Offset: 0x0007D9E0
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Vision_Plasma");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x060011ED RID: 4589 RVA: 0x0007F804 File Offset: 0x0007DA04
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
			this.material.SetFloat("_Value", this.Value);
			this.material.SetFloat("_Value2", this.Value2);
			this.material.SetFloat("_Value3", this.Intensity);
			this.material.SetFloat("_Value4", this.Value4);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x060011EE RID: 4590 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x060011EF RID: 4591 RVA: 0x0007F8FC File Offset: 0x0007DAFC
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x0400150F RID: 5391
	public Shader SCShader;

	// Token: 0x04001510 RID: 5392
	private float TimeX = 1f;

	// Token: 0x04001511 RID: 5393
	private Material SCMaterial;

	// Token: 0x04001512 RID: 5394
	[Range(-2f, 2f)]
	public float Value = 0.6f;

	// Token: 0x04001513 RID: 5395
	[Range(-2f, 2f)]
	public float Value2 = 0.2f;

	// Token: 0x04001514 RID: 5396
	[Range(0f, 60f)]
	public float Intensity = 15f;

	// Token: 0x04001515 RID: 5397
	[Range(0f, 10f)]
	private float Value4 = 1f;
}
