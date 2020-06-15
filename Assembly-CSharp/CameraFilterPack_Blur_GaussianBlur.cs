using System;
using UnityEngine;

// Token: 0x0200013F RID: 319
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Blur/GaussianBlur")]
public class CameraFilterPack_Blur_GaussianBlur : MonoBehaviour
{
	// Token: 0x1700025E RID: 606
	// (get) Token: 0x06000C8B RID: 3211 RVA: 0x0006800B File Offset: 0x0006620B
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

	// Token: 0x06000C8C RID: 3212 RVA: 0x0006803F File Offset: 0x0006623F
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Blur_GaussianBlur");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000C8D RID: 3213 RVA: 0x00068060 File Offset: 0x00066260
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
			this.material.SetFloat("_Distortion", this.Size);
			this.material.SetVector("_ScreenResolution", new Vector2((float)Screen.width, (float)Screen.height));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000C8E RID: 3214 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000C8F RID: 3215 RVA: 0x0006810F File Offset: 0x0006630F
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000F5F RID: 3935
	public Shader SCShader;

	// Token: 0x04000F60 RID: 3936
	private float TimeX = 1f;

	// Token: 0x04000F61 RID: 3937
	[Range(1f, 16f)]
	public float Size = 10f;

	// Token: 0x04000F62 RID: 3938
	private Material SCMaterial;
}
