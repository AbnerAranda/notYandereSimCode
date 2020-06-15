using System;
using UnityEngine;

// Token: 0x02000145 RID: 325
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Blur/Steam")]
public class CameraFilterPack_Blur_Steam : MonoBehaviour
{
	// Token: 0x17000264 RID: 612
	// (get) Token: 0x06000CAF RID: 3247 RVA: 0x0006892B File Offset: 0x00066B2B
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

	// Token: 0x06000CB0 RID: 3248 RVA: 0x0006895F File Offset: 0x00066B5F
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Blur_Steam");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000CB1 RID: 3249 RVA: 0x00068980 File Offset: 0x00066B80
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
			this.material.SetFloat("_Radius", this.Radius);
			this.material.SetFloat("_Quality", this.Quality);
			this.material.SetVector("_ScreenResolution", new Vector2((float)Screen.width, (float)Screen.height));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000CB2 RID: 3250 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000CB3 RID: 3251 RVA: 0x00068A45 File Offset: 0x00066C45
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000F81 RID: 3969
	public Shader SCShader;

	// Token: 0x04000F82 RID: 3970
	private float TimeX = 1f;

	// Token: 0x04000F83 RID: 3971
	private Material SCMaterial;

	// Token: 0x04000F84 RID: 3972
	[Range(0f, 1f)]
	public float Radius = 0.1f;

	// Token: 0x04000F85 RID: 3973
	[Range(0f, 1f)]
	public float Quality = 0.75f;
}
