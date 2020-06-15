using System;
using UnityEngine;

// Token: 0x020001BD RID: 445
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Glasses/Spy")]
public class CameraFilterPack_Glasses_On_6 : MonoBehaviour
{
	// Token: 0x170002DC RID: 732
	// (get) Token: 0x06000F82 RID: 3970 RVA: 0x0007466D File Offset: 0x0007286D
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

	// Token: 0x06000F83 RID: 3971 RVA: 0x000746A1 File Offset: 0x000728A1
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_Glasses_On7") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/Glasses_On");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000F84 RID: 3972 RVA: 0x000746D8 File Offset: 0x000728D8
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

	// Token: 0x06000F85 RID: 3973 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000F86 RID: 3974 RVA: 0x0007483D File Offset: 0x00072A3D
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001274 RID: 4724
	public Shader SCShader;

	// Token: 0x04001275 RID: 4725
	private float TimeX = 1f;

	// Token: 0x04001276 RID: 4726
	[Range(0f, 1f)]
	public float Fade = 0.2f;

	// Token: 0x04001277 RID: 4727
	[Range(0f, 0.1f)]
	public float VisionBlur = 0.005f;

	// Token: 0x04001278 RID: 4728
	public Color GlassesColor = new Color(0f, 0f, 0f, 1f);

	// Token: 0x04001279 RID: 4729
	public Color GlassesColor2 = new Color(0.25f, 0.25f, 0.45f, 0.25f);

	// Token: 0x0400127A RID: 4730
	[Range(0f, 1f)]
	public float GlassDistortion = 0.6f;

	// Token: 0x0400127B RID: 4731
	[Range(0f, 1f)]
	public float GlassAberration = 0.3f;

	// Token: 0x0400127C RID: 4732
	[Range(0f, 1f)]
	public float UseFinalGlassColor;

	// Token: 0x0400127D RID: 4733
	[Range(0f, 1f)]
	public float UseScanLine = 0.4f;

	// Token: 0x0400127E RID: 4734
	[Range(1f, 512f)]
	public float UseScanLineSize = 358f;

	// Token: 0x0400127F RID: 4735
	public Color GlassColor = new Color(1f, 0.9f, 0f, 1f);

	// Token: 0x04001280 RID: 4736
	private Material SCMaterial;

	// Token: 0x04001281 RID: 4737
	private Texture2D Texture2;
}
