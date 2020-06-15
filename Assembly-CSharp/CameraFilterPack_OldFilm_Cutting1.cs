using System;
using UnityEngine;

// Token: 0x020001E7 RID: 487
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Old Film/Cutting 1")]
public class CameraFilterPack_OldFilm_Cutting1 : MonoBehaviour
{
	// Token: 0x17000306 RID: 774
	// (get) Token: 0x060010A0 RID: 4256 RVA: 0x00079FFE File Offset: 0x000781FE
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

	// Token: 0x060010A1 RID: 4257 RVA: 0x0007A032 File Offset: 0x00078232
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_OldFilm1") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/OldFilm_Cutting1");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x060010A2 RID: 4258 RVA: 0x0007A068 File Offset: 0x00078268
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
			this.material.SetFloat("_Value", this.Luminosity);
			this.material.SetFloat("_Value2", 1f - this.Vignette);
			this.material.SetFloat("_Value3", this.Negative);
			this.material.SetFloat("_Speed", this.Speed);
			this.material.SetTexture("_MainTex2", this.Texture2);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x060010A3 RID: 4259 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x060010A4 RID: 4260 RVA: 0x0007A14F File Offset: 0x0007834F
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040013B0 RID: 5040
	public Shader SCShader;

	// Token: 0x040013B1 RID: 5041
	private float TimeX = 1f;

	// Token: 0x040013B2 RID: 5042
	[Range(0f, 10f)]
	public float Speed = 1f;

	// Token: 0x040013B3 RID: 5043
	[Range(0f, 2f)]
	public float Luminosity = 1.5f;

	// Token: 0x040013B4 RID: 5044
	[Range(0f, 1f)]
	public float Vignette = 1f;

	// Token: 0x040013B5 RID: 5045
	[Range(0f, 2f)]
	public float Negative;

	// Token: 0x040013B6 RID: 5046
	private Material SCMaterial;

	// Token: 0x040013B7 RID: 5047
	private Texture2D Texture2;
}
