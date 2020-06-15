using System;
using UnityEngine;

// Token: 0x020001BC RID: 444
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Glasses/Futuristic Montain")]
public class CameraFilterPack_Glasses_On_5 : MonoBehaviour
{
	// Token: 0x170002DB RID: 731
	// (get) Token: 0x06000F7C RID: 3964 RVA: 0x000743C5 File Offset: 0x000725C5
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

	// Token: 0x06000F7D RID: 3965 RVA: 0x000743F9 File Offset: 0x000725F9
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_Glasses_On6") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/Glasses_On");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000F7E RID: 3966 RVA: 0x00074430 File Offset: 0x00072630
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

	// Token: 0x06000F7F RID: 3967 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000F80 RID: 3968 RVA: 0x00074595 File Offset: 0x00072795
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001266 RID: 4710
	public Shader SCShader;

	// Token: 0x04001267 RID: 4711
	private float TimeX = 1f;

	// Token: 0x04001268 RID: 4712
	[Range(0f, 1f)]
	public float Fade = 0.2f;

	// Token: 0x04001269 RID: 4713
	[Range(0f, 0.1f)]
	public float VisionBlur = 0.005f;

	// Token: 0x0400126A RID: 4714
	public Color GlassesColor = new Color(0.1f, 0.1f, 0.1f, 1f);

	// Token: 0x0400126B RID: 4715
	public Color GlassesColor2 = new Color(0.45f, 0.45f, 0.45f, 0.25f);

	// Token: 0x0400126C RID: 4716
	[Range(0f, 1f)]
	public float GlassDistortion = 0.6f;

	// Token: 0x0400126D RID: 4717
	[Range(0f, 1f)]
	public float GlassAberration = 0.3f;

	// Token: 0x0400126E RID: 4718
	[Range(0f, 1f)]
	public float UseFinalGlassColor;

	// Token: 0x0400126F RID: 4719
	[Range(0f, 1f)]
	public float UseScanLine = 0.4f;

	// Token: 0x04001270 RID: 4720
	[Range(1f, 512f)]
	public float UseScanLineSize = 358f;

	// Token: 0x04001271 RID: 4721
	public Color GlassColor = new Color(0.1f, 0.3f, 1f, 1f);

	// Token: 0x04001272 RID: 4722
	private Material SCMaterial;

	// Token: 0x04001273 RID: 4723
	private Texture2D Texture2;
}
