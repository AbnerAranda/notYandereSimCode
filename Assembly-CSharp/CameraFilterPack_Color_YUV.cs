using System;
using UnityEngine;

// Token: 0x02000158 RID: 344
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Colors/Color_YUV")]
public class CameraFilterPack_Color_YUV : MonoBehaviour
{
	// Token: 0x17000277 RID: 631
	// (get) Token: 0x06000D21 RID: 3361 RVA: 0x0006A77B File Offset: 0x0006897B
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

	// Token: 0x06000D22 RID: 3362 RVA: 0x0006A7AF File Offset: 0x000689AF
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Color_YUV");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000D23 RID: 3363 RVA: 0x0006A7D0 File Offset: 0x000689D0
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
			this.material.SetFloat("_Y", this._Y);
			this.material.SetFloat("_U", this._U);
			this.material.SetFloat("_V", this._V);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000D24 RID: 3364 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000D25 RID: 3365 RVA: 0x0006A8B2 File Offset: 0x00068AB2
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000FF1 RID: 4081
	public Shader SCShader;

	// Token: 0x04000FF2 RID: 4082
	private float TimeX = 1f;

	// Token: 0x04000FF3 RID: 4083
	private Material SCMaterial;

	// Token: 0x04000FF4 RID: 4084
	[Range(-1f, 1f)]
	public float _Y;

	// Token: 0x04000FF5 RID: 4085
	[Range(-1f, 1f)]
	public float _U;

	// Token: 0x04000FF6 RID: 4086
	[Range(-1f, 1f)]
	public float _V;
}
