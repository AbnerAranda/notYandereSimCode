using System;
using UnityEngine;

// Token: 0x020001ED RID: 493
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Pixelisation/OilPaintHQ")]
public class CameraFilterPack_Pixelisation_OilPaintHQ : MonoBehaviour
{
	// Token: 0x1700030C RID: 780
	// (get) Token: 0x060010C4 RID: 4292 RVA: 0x0007A9AE File Offset: 0x00078BAE
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

	// Token: 0x060010C5 RID: 4293 RVA: 0x0007A9E2 File Offset: 0x00078BE2
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Pixelisation_OilPaintHQ");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x060010C6 RID: 4294 RVA: 0x0007AA04 File Offset: 0x00078C04
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
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			this.material.SetFloat("_Value", this.Value);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x060010C7 RID: 4295 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x060010C8 RID: 4296 RVA: 0x0007AABA File Offset: 0x00078CBA
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040013DB RID: 5083
	public Shader SCShader;

	// Token: 0x040013DC RID: 5084
	private float TimeX = 1f;

	// Token: 0x040013DD RID: 5085
	private Material SCMaterial;

	// Token: 0x040013DE RID: 5086
	[Range(0f, 5f)]
	public float Value = 2f;
}
