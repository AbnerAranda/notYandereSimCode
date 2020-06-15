using System;
using UnityEngine;

// Token: 0x020001B9 RID: 441
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Glasses/Vampire")]
public class CameraFilterPack_Glasses_On_2 : MonoBehaviour
{
	// Token: 0x170002D8 RID: 728
	// (get) Token: 0x06000F6A RID: 3946 RVA: 0x00073BCE File Offset: 0x00071DCE
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

	// Token: 0x06000F6B RID: 3947 RVA: 0x00073C02 File Offset: 0x00071E02
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_Glasses_On3") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/Glasses_OnX");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000F6C RID: 3948 RVA: 0x00073C38 File Offset: 0x00071E38
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

	// Token: 0x06000F6D RID: 3949 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000F6E RID: 3950 RVA: 0x00073D9D File Offset: 0x00071F9D
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x0400123C RID: 4668
	public Shader SCShader;

	// Token: 0x0400123D RID: 4669
	private float TimeX = 1f;

	// Token: 0x0400123E RID: 4670
	[Range(0f, 1f)]
	public float Fade = 0.2f;

	// Token: 0x0400123F RID: 4671
	[Range(0f, 0.1f)]
	public float VisionBlur = 0.005f;

	// Token: 0x04001240 RID: 4672
	public Color GlassesColor = new Color(0f, 0f, 0f, 1f);

	// Token: 0x04001241 RID: 4673
	public Color GlassesColor2 = new Color(0.25f, 0.25f, 0.25f, 0.25f);

	// Token: 0x04001242 RID: 4674
	[Range(0f, 1f)]
	public float GlassDistortion = 0.6f;

	// Token: 0x04001243 RID: 4675
	[Range(0f, 1f)]
	public float GlassAberration = 0.5f;

	// Token: 0x04001244 RID: 4676
	[Range(0f, 1f)]
	public float UseFinalGlassColor = 1f;

	// Token: 0x04001245 RID: 4677
	[Range(0f, 1f)]
	public float UseScanLine;

	// Token: 0x04001246 RID: 4678
	[Range(1f, 512f)]
	public float UseScanLineSize = 358f;

	// Token: 0x04001247 RID: 4679
	public Color GlassColor = new Color(1f, 0f, 0f, 1f);

	// Token: 0x04001248 RID: 4680
	private Material SCMaterial;

	// Token: 0x04001249 RID: 4681
	private Texture2D Texture2;
}
