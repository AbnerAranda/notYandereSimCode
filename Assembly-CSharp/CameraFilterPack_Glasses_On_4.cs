using System;
using UnityEngine;

// Token: 0x020001BB RID: 443
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Glasses/Futuristic Desert")]
public class CameraFilterPack_Glasses_On_4 : MonoBehaviour
{
	// Token: 0x170002DA RID: 730
	// (get) Token: 0x06000F76 RID: 3958 RVA: 0x0007411D File Offset: 0x0007231D
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

	// Token: 0x06000F77 RID: 3959 RVA: 0x00074151 File Offset: 0x00072351
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_Glasses_On5") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/Glasses_On");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000F78 RID: 3960 RVA: 0x00074188 File Offset: 0x00072388
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

	// Token: 0x06000F79 RID: 3961 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000F7A RID: 3962 RVA: 0x000742ED File Offset: 0x000724ED
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001258 RID: 4696
	public Shader SCShader;

	// Token: 0x04001259 RID: 4697
	private float TimeX = 1f;

	// Token: 0x0400125A RID: 4698
	[Range(0f, 1f)]
	public float Fade = 0.2f;

	// Token: 0x0400125B RID: 4699
	[Range(0f, 0.1f)]
	public float VisionBlur = 0.005f;

	// Token: 0x0400125C RID: 4700
	public Color GlassesColor = new Color(0f, 0f, 0f, 1f);

	// Token: 0x0400125D RID: 4701
	public Color GlassesColor2 = new Color(0.25f, 0.25f, 0.25f, 0.25f);

	// Token: 0x0400125E RID: 4702
	[Range(0f, 1f)]
	public float GlassDistortion = 0.6f;

	// Token: 0x0400125F RID: 4703
	[Range(0f, 1f)]
	public float GlassAberration = 0.3f;

	// Token: 0x04001260 RID: 4704
	[Range(0f, 1f)]
	public float UseFinalGlassColor;

	// Token: 0x04001261 RID: 4705
	[Range(0f, 1f)]
	public float UseScanLine = 0.4f;

	// Token: 0x04001262 RID: 4706
	[Range(1f, 512f)]
	public float UseScanLineSize = 358f;

	// Token: 0x04001263 RID: 4707
	public Color GlassColor = new Color(1f, 0.4f, 0f, 1f);

	// Token: 0x04001264 RID: 4708
	private Material SCMaterial;

	// Token: 0x04001265 RID: 4709
	private Texture2D Texture2;
}
