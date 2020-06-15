using System;
using UnityEngine;

// Token: 0x020001B8 RID: 440
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Glasses/Classic Glasses")]
public class CameraFilterPack_Glasses_On : MonoBehaviour
{
	// Token: 0x170002D7 RID: 727
	// (get) Token: 0x06000F64 RID: 3940 RVA: 0x0007392F File Offset: 0x00071B2F
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

	// Token: 0x06000F65 RID: 3941 RVA: 0x00073963 File Offset: 0x00071B63
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_Glasses_On2") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/Glasses_On");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000F66 RID: 3942 RVA: 0x0007399C File Offset: 0x00071B9C
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
			this.material.SetFloat("UseFinalGlassColor", this.UseFinalGlassColor);
			this.material.SetFloat("Fade", this.Fade);
			this.material.SetFloat("VisionBlur", this.VisionBlur);
			this.material.SetFloat("GlassDistortion", this.GlassDistortion);
			this.material.SetFloat("GlassAberration", this.GlassAberration);
			this.material.SetColor("GlassesColor", this.GlassesColor);
			this.material.SetColor("GlassesColor2", this.GlassesColor2);
			this.material.SetColor("GlassColor", this.GlassColor);
			this.material.SetFloat("UseScanLineSize", this.UseScanLineSize);
			this.material.SetFloat("UseScanLine", this.UseScanLine);
			this.material.SetTexture("_MainTex2", this.Texture2);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000F67 RID: 3943 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000F68 RID: 3944 RVA: 0x00073B01 File Offset: 0x00071D01
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x0400122E RID: 4654
	public Shader SCShader;

	// Token: 0x0400122F RID: 4655
	private float TimeX = 1f;

	// Token: 0x04001230 RID: 4656
	[Range(0f, 1f)]
	public float Fade = 0.2f;

	// Token: 0x04001231 RID: 4657
	[Range(0f, 0.1f)]
	public float VisionBlur = 0.0095f;

	// Token: 0x04001232 RID: 4658
	public Color GlassesColor = new Color(0f, 0f, 0f, 1f);

	// Token: 0x04001233 RID: 4659
	public Color GlassesColor2 = new Color(0.25f, 0.25f, 0.25f, 0.25f);

	// Token: 0x04001234 RID: 4660
	[Range(0f, 1f)]
	public float GlassDistortion = 0.45f;

	// Token: 0x04001235 RID: 4661
	[Range(0f, 1f)]
	public float GlassAberration = 0.5f;

	// Token: 0x04001236 RID: 4662
	[Range(0f, 1f)]
	public float UseFinalGlassColor;

	// Token: 0x04001237 RID: 4663
	[Range(0f, 1f)]
	public float UseScanLine;

	// Token: 0x04001238 RID: 4664
	[Range(1f, 512f)]
	public float UseScanLineSize = 1f;

	// Token: 0x04001239 RID: 4665
	public Color GlassColor = new Color(0f, 0f, 0f, 1f);

	// Token: 0x0400123A RID: 4666
	private Material SCMaterial;

	// Token: 0x0400123B RID: 4667
	private Texture2D Texture2;
}
