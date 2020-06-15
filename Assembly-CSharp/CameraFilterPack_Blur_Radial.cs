using System;
using UnityEngine;

// Token: 0x02000142 RID: 322
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Blur/Radial")]
public class CameraFilterPack_Blur_Radial : MonoBehaviour
{
	// Token: 0x17000261 RID: 609
	// (get) Token: 0x06000C9D RID: 3229 RVA: 0x0006846B File Offset: 0x0006666B
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

	// Token: 0x06000C9E RID: 3230 RVA: 0x0006849F File Offset: 0x0006669F
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Blur_Radial");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000C9F RID: 3231 RVA: 0x000684C0 File Offset: 0x000666C0
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
			this.material.SetFloat("_Value", this.Intensity);
			this.material.SetFloat("_Value2", this.MovX);
			this.material.SetFloat("_Value3", this.MovY);
			this.material.SetFloat("_Value4", this.blurWidth);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000CA0 RID: 3232 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000CA1 RID: 3233 RVA: 0x000685B8 File Offset: 0x000667B8
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000F6E RID: 3950
	public Shader SCShader;

	// Token: 0x04000F6F RID: 3951
	private float TimeX = 1f;

	// Token: 0x04000F70 RID: 3952
	private Material SCMaterial;

	// Token: 0x04000F71 RID: 3953
	[Range(-0.5f, 0.5f)]
	public float Intensity = 0.125f;

	// Token: 0x04000F72 RID: 3954
	[Range(-2f, 2f)]
	public float MovX = 0.5f;

	// Token: 0x04000F73 RID: 3955
	[Range(-2f, 2f)]
	public float MovY = 0.5f;

	// Token: 0x04000F74 RID: 3956
	[Range(0f, 10f)]
	private float blurWidth = 1f;
}
