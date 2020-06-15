using System;
using UnityEngine;

// Token: 0x020001E8 RID: 488
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Old Film/Cutting 2")]
public class CameraFilterPack_OldFilm_Cutting2 : MonoBehaviour
{
	// Token: 0x17000307 RID: 775
	// (get) Token: 0x060010A6 RID: 4262 RVA: 0x0007A19D File Offset: 0x0007839D
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

	// Token: 0x060010A7 RID: 4263 RVA: 0x0007A1D1 File Offset: 0x000783D1
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_OldFilm2") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/OldFilm_Cutting2");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x060010A8 RID: 4264 RVA: 0x0007A208 File Offset: 0x00078408
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
			this.material.SetFloat("_Value", 2f - this.Luminosity);
			this.material.SetFloat("_Value2", 1f - this.Vignette);
			this.material.SetFloat("_Value3", this.Negative);
			this.material.SetFloat("_Speed", this.Speed);
			this.material.SetTexture("_MainTex2", this.Texture2);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x060010A9 RID: 4265 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x060010AA RID: 4266 RVA: 0x0007A2F5 File Offset: 0x000784F5
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040013B8 RID: 5048
	public Shader SCShader;

	// Token: 0x040013B9 RID: 5049
	private float TimeX = 1f;

	// Token: 0x040013BA RID: 5050
	[Range(0f, 10f)]
	public float Speed = 5f;

	// Token: 0x040013BB RID: 5051
	[Range(0f, 2f)]
	public float Luminosity = 1f;

	// Token: 0x040013BC RID: 5052
	[Range(0f, 1f)]
	public float Vignette = 1f;

	// Token: 0x040013BD RID: 5053
	[Range(0f, 1f)]
	public float Negative;

	// Token: 0x040013BE RID: 5054
	private Material SCMaterial;

	// Token: 0x040013BF RID: 5055
	private Texture2D Texture2;
}
