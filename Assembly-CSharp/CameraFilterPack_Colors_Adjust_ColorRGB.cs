using System;
using UnityEngine;

// Token: 0x02000159 RID: 345
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/ColorsAdjust/ColorRGB")]
public class CameraFilterPack_Colors_Adjust_ColorRGB : MonoBehaviour
{
	// Token: 0x17000278 RID: 632
	// (get) Token: 0x06000D27 RID: 3367 RVA: 0x0006A8DF File Offset: 0x00068ADF
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

	// Token: 0x06000D28 RID: 3368 RVA: 0x0006A913 File Offset: 0x00068B13
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Colors_Adjust_ColorRGB");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000D29 RID: 3369 RVA: 0x0006A934 File Offset: 0x00068B34
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
			this.material.SetFloat("_Value", this.Red);
			this.material.SetFloat("_Value2", this.Green);
			this.material.SetFloat("_Value3", this.Blue);
			this.material.SetFloat("_Value4", this.Brightness);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000D2A RID: 3370 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000D2B RID: 3371 RVA: 0x0006AA2C File Offset: 0x00068C2C
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000FF7 RID: 4087
	public Shader SCShader;

	// Token: 0x04000FF8 RID: 4088
	private float TimeX = 1f;

	// Token: 0x04000FF9 RID: 4089
	private Material SCMaterial;

	// Token: 0x04000FFA RID: 4090
	[Range(-2f, 2f)]
	public float Red;

	// Token: 0x04000FFB RID: 4091
	[Range(-2f, 2f)]
	public float Green;

	// Token: 0x04000FFC RID: 4092
	[Range(-2f, 2f)]
	public float Blue;

	// Token: 0x04000FFD RID: 4093
	[Range(-1f, 1f)]
	public float Brightness;
}
