using System;
using UnityEngine;

// Token: 0x02000117 RID: 279
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Pixel/Snow_8bits")]
public class CameraFilterPack_Atmosphere_Snow_8bits : MonoBehaviour
{
	// Token: 0x17000236 RID: 566
	// (get) Token: 0x06000B5C RID: 2908 RVA: 0x000629F5 File Offset: 0x00060BF5
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

	// Token: 0x06000B5D RID: 2909 RVA: 0x00062A29 File Offset: 0x00060C29
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Atmosphere_Snow_8bits");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000B5E RID: 2910 RVA: 0x00062A4C File Offset: 0x00060C4C
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
			this.material.SetFloat("_Value", this.Threshold);
			this.material.SetFloat("_Value2", this.Size);
			this.material.SetFloat("_Value3", this.DirectionX);
			this.material.SetFloat("_Value4", this.Fade);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000B5F RID: 2911 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000B60 RID: 2912 RVA: 0x00062B44 File Offset: 0x00060D44
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000E09 RID: 3593
	public Shader SCShader;

	// Token: 0x04000E0A RID: 3594
	private float TimeX = 1f;

	// Token: 0x04000E0B RID: 3595
	private Material SCMaterial;

	// Token: 0x04000E0C RID: 3596
	[Range(0.9f, 2f)]
	public float Threshold = 1f;

	// Token: 0x04000E0D RID: 3597
	[Range(8f, 256f)]
	public float Size = 64f;

	// Token: 0x04000E0E RID: 3598
	[Range(-0.5f, 0.5f)]
	public float DirectionX;

	// Token: 0x04000E0F RID: 3599
	[Range(0f, 1f)]
	public float Fade = 1f;
}
