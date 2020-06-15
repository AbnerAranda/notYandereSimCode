using System;
using UnityEngine;

// Token: 0x020001E6 RID: 486
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Vision/ThermaVision")]
public class CameraFilterPack_Oculus_ThermaVision : MonoBehaviour
{
	// Token: 0x17000305 RID: 773
	// (get) Token: 0x0600109A RID: 4250 RVA: 0x00079E61 File Offset: 0x00078061
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

	// Token: 0x0600109B RID: 4251 RVA: 0x00079E95 File Offset: 0x00078095
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Oculus_ThermaVision");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x0600109C RID: 4252 RVA: 0x00079EB8 File Offset: 0x000780B8
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
			this.material.SetFloat("_Value", this.Therma_Variation);
			this.material.SetFloat("_Value2", this.Contrast);
			this.material.SetFloat("_Value3", this.Burn);
			this.material.SetFloat("_Value4", this.SceneCut);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x0600109D RID: 4253 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x0600109E RID: 4254 RVA: 0x00079FB0 File Offset: 0x000781B0
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040013A9 RID: 5033
	public Shader SCShader;

	// Token: 0x040013AA RID: 5034
	private float TimeX = 1f;

	// Token: 0x040013AB RID: 5035
	private Material SCMaterial;

	// Token: 0x040013AC RID: 5036
	[Range(0f, 1f)]
	public float Therma_Variation = 0.5f;

	// Token: 0x040013AD RID: 5037
	[Range(0f, 8f)]
	private float Contrast = 3f;

	// Token: 0x040013AE RID: 5038
	[Range(0f, 4f)]
	private float Burn;

	// Token: 0x040013AF RID: 5039
	[Range(0f, 16f)]
	private float SceneCut = 1f;
}
