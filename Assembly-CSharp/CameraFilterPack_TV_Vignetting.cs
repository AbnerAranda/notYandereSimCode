using System;
using UnityEngine;

// Token: 0x02000210 RID: 528
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/TV/Vignetting")]
public class CameraFilterPack_TV_Vignetting : MonoBehaviour
{
	// Token: 0x1700032F RID: 815
	// (get) Token: 0x06001197 RID: 4503 RVA: 0x0007E0BE File Offset: 0x0007C2BE
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

	// Token: 0x06001198 RID: 4504 RVA: 0x0007E0F2 File Offset: 0x0007C2F2
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/TV_Vignetting");
		this.Vignette = (Resources.Load("CameraFilterPack_TV_Vignetting1") as Texture2D);
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06001199 RID: 4505 RVA: 0x0007E128 File Offset: 0x0007C328
	private void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
	{
		if (this.SCShader != null)
		{
			this.material.SetTexture("Vignette", this.Vignette);
			this.material.SetFloat("_Vignetting", this.Vignetting);
			this.material.SetFloat("_Vignetting2", this.VignettingFull);
			this.material.SetColor("_VignettingColor", this.VignettingColor);
			this.material.SetFloat("_VignettingDirt", this.VignettingDirt);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x0600119A RID: 4506 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x0600119B RID: 4507 RVA: 0x0007E1C6 File Offset: 0x0007C3C6
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040014AD RID: 5293
	public Shader SCShader;

	// Token: 0x040014AE RID: 5294
	private Material SCMaterial;

	// Token: 0x040014AF RID: 5295
	private Texture2D Vignette;

	// Token: 0x040014B0 RID: 5296
	[Range(0f, 1f)]
	public float Vignetting = 1f;

	// Token: 0x040014B1 RID: 5297
	[Range(0f, 1f)]
	public float VignettingFull;

	// Token: 0x040014B2 RID: 5298
	[Range(0f, 1f)]
	public float VignettingDirt;

	// Token: 0x040014B3 RID: 5299
	public Color VignettingColor = new Color(0f, 0f, 0f, 1f);
}
