using System;
using UnityEngine;

// Token: 0x0200017A RID: 378
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Drawing/CellShading2")]
public class CameraFilterPack_Drawing_CellShading2 : MonoBehaviour
{
	// Token: 0x17000299 RID: 665
	// (get) Token: 0x06000DEF RID: 3567 RVA: 0x0006DEB8 File Offset: 0x0006C0B8
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

	// Token: 0x06000DF0 RID: 3568 RVA: 0x0006DEEC File Offset: 0x0006C0EC
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Drawing_CellShading2");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000DF1 RID: 3569 RVA: 0x0006DF10 File Offset: 0x0006C110
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
			this.material.SetFloat("_EdgeSize", this.EdgeSize);
			this.material.SetFloat("_ColorLevel", this.ColorLevel);
			this.material.SetFloat("_Distortion", this.Blur);
			this.material.SetVector("_ScreenResolution", new Vector2((float)Screen.width, (float)Screen.height));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000DF2 RID: 3570 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000DF3 RID: 3571 RVA: 0x0006DFEB File Offset: 0x0006C1EB
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040010C3 RID: 4291
	public Shader SCShader;

	// Token: 0x040010C4 RID: 4292
	private float TimeX = 1f;

	// Token: 0x040010C5 RID: 4293
	private Material SCMaterial;

	// Token: 0x040010C6 RID: 4294
	[Range(0f, 1f)]
	public float EdgeSize = 0.1f;

	// Token: 0x040010C7 RID: 4295
	[Range(0f, 10f)]
	public float ColorLevel = 4f;

	// Token: 0x040010C8 RID: 4296
	[Range(0f, 1f)]
	public float Blur = 1f;
}
