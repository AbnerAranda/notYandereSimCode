using System;
using UnityEngine;

// Token: 0x020001BA RID: 442
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Glasses/Night Glasses")]
public class CameraFilterPack_Glasses_On_3 : MonoBehaviour
{
	// Token: 0x170002D9 RID: 729
	// (get) Token: 0x06000F70 RID: 3952 RVA: 0x00073E75 File Offset: 0x00072075
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

	// Token: 0x06000F71 RID: 3953 RVA: 0x00073EA9 File Offset: 0x000720A9
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_Glasses_On4") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/Glasses_On");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000F72 RID: 3954 RVA: 0x00073EE0 File Offset: 0x000720E0
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

	// Token: 0x06000F73 RID: 3955 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000F74 RID: 3956 RVA: 0x00074045 File Offset: 0x00072245
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x0400124A RID: 4682
	public Shader SCShader;

	// Token: 0x0400124B RID: 4683
	private float TimeX = 1f;

	// Token: 0x0400124C RID: 4684
	[Range(0f, 1f)]
	public float Fade = 0.3f;

	// Token: 0x0400124D RID: 4685
	[Range(0f, 0.1f)]
	public float VisionBlur = 0.005f;

	// Token: 0x0400124E RID: 4686
	public Color GlassesColor = new Color(0.7f, 0.7f, 0.7f, 1f);

	// Token: 0x0400124F RID: 4687
	public Color GlassesColor2 = new Color(1f, 1f, 1f, 1f);

	// Token: 0x04001250 RID: 4688
	[Range(0f, 1f)]
	public float GlassDistortion = 0.6f;

	// Token: 0x04001251 RID: 4689
	[Range(0f, 1f)]
	public float GlassAberration = 0.3f;

	// Token: 0x04001252 RID: 4690
	[Range(0f, 1f)]
	public float UseFinalGlassColor;

	// Token: 0x04001253 RID: 4691
	[Range(0f, 1f)]
	public float UseScanLine = 0.4f;

	// Token: 0x04001254 RID: 4692
	[Range(1f, 512f)]
	public float UseScanLineSize = 358f;

	// Token: 0x04001255 RID: 4693
	public Color GlassColor = new Color(0f, 0.5f, 0f, 1f);

	// Token: 0x04001256 RID: 4694
	private Material SCMaterial;

	// Token: 0x04001257 RID: 4695
	private Texture2D Texture2;
}
