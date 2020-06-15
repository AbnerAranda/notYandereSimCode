using System;
using UnityEngine;

// Token: 0x020001DB RID: 475
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Glitch/NewGlitch6")]
public class CameraFilterPack_NewGlitch6 : MonoBehaviour
{
	// Token: 0x170002FA RID: 762
	// (get) Token: 0x06001052 RID: 4178 RVA: 0x00078527 File Offset: 0x00076727
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

	// Token: 0x06001053 RID: 4179 RVA: 0x0007855B File Offset: 0x0007675B
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/CameraFilterPack_NewGlitch6");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06001054 RID: 4180 RVA: 0x0007857C File Offset: 0x0007677C
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
			this.material.SetFloat("_Speed", this.__Speed);
			this.material.SetFloat("FadeLight", this._FadeLight);
			this.material.SetFloat("FadeDark", this._FadeDark);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06001055 RID: 4181 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06001056 RID: 4182 RVA: 0x0007865E File Offset: 0x0007685E
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001351 RID: 4945
	public Shader SCShader;

	// Token: 0x04001352 RID: 4946
	private float TimeX = 1f;

	// Token: 0x04001353 RID: 4947
	private Material SCMaterial;

	// Token: 0x04001354 RID: 4948
	[Range(0f, 1f)]
	public float __Speed = 1f;

	// Token: 0x04001355 RID: 4949
	[Range(0f, 1f)]
	public float _FadeLight = 1f;

	// Token: 0x04001356 RID: 4950
	[Range(0f, 1f)]
	public float _FadeDark = 1f;
}
