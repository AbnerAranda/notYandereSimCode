using System;
using UnityEngine;

// Token: 0x0200019C RID: 412
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/FX/Ascii")]
public class CameraFilterPack_FX_Ascii : MonoBehaviour
{
	// Token: 0x170002BB RID: 699
	// (get) Token: 0x06000EBC RID: 3772 RVA: 0x000710AB File Offset: 0x0006F2AB
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

	// Token: 0x06000EBD RID: 3773 RVA: 0x000710DF File Offset: 0x0006F2DF
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/FX_Ascii");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000EBE RID: 3774 RVA: 0x00071100 File Offset: 0x0006F300
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
			this.material.SetFloat("Value", this.Value);
			this.material.SetFloat("Fade", this.Fade);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000EBF RID: 3775 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000EC0 RID: 3776 RVA: 0x000711CC File Offset: 0x0006F3CC
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001193 RID: 4499
	public Shader SCShader;

	// Token: 0x04001194 RID: 4500
	[Range(0f, 2f)]
	public float Value = 1f;

	// Token: 0x04001195 RID: 4501
	[Range(0.01f, 1f)]
	public float Fade = 1f;

	// Token: 0x04001196 RID: 4502
	private float TimeX = 1f;

	// Token: 0x04001197 RID: 4503
	private Material SCMaterial;
}
