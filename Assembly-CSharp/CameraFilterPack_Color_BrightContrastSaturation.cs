using System;
using UnityEngine;

// Token: 0x0200014E RID: 334
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Colors/BrightContrastSaturation")]
public class CameraFilterPack_Color_BrightContrastSaturation : MonoBehaviour
{
	// Token: 0x1700026D RID: 621
	// (get) Token: 0x06000CE5 RID: 3301 RVA: 0x00069A3E File Offset: 0x00067C3E
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

	// Token: 0x06000CE6 RID: 3302 RVA: 0x00069A72 File Offset: 0x00067C72
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Color_BrightContrastSaturation");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000CE7 RID: 3303 RVA: 0x00069A94 File Offset: 0x00067C94
	private void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
	{
		if (this.SCShader != null)
		{
			this.TimeX += Time.deltaTime;
			if (this.TimeX > 100f)
			{
				this.TimeX = 0f;
			}
			this.material.SetFloat("_Brightness", this.Brightness);
			this.material.SetFloat("_Saturation", this.Saturation);
			this.material.SetFloat("_Contrast", this.Contrast);
			this.material.SetFloat("_TimeX", this.TimeX);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000CE8 RID: 3304 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000CE9 RID: 3305 RVA: 0x00069B76 File Offset: 0x00067D76
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000FC5 RID: 4037
	public Shader SCShader;

	// Token: 0x04000FC6 RID: 4038
	private float TimeX = 1f;

	// Token: 0x04000FC7 RID: 4039
	private Material SCMaterial;

	// Token: 0x04000FC8 RID: 4040
	[Range(0f, 10f)]
	public float Brightness = 2f;

	// Token: 0x04000FC9 RID: 4041
	[Range(0f, 10f)]
	public float Saturation = 1.5f;

	// Token: 0x04000FCA RID: 4042
	[Range(0f, 10f)]
	public float Contrast = 1.5f;
}
