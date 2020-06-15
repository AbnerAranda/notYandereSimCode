using System;
using UnityEngine;

// Token: 0x020001DA RID: 474
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Glitch/NewGlitch5")]
public class CameraFilterPack_NewGlitch5 : MonoBehaviour
{
	// Token: 0x170002F9 RID: 761
	// (get) Token: 0x0600104C RID: 4172 RVA: 0x00078313 File Offset: 0x00076513
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

	// Token: 0x0600104D RID: 4173 RVA: 0x00078347 File Offset: 0x00076547
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/CameraFilterPack_NewGlitch5");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x0600104E RID: 4174 RVA: 0x00078368 File Offset: 0x00076568
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
			this.material.SetFloat("Fade", this._Fade);
			this.material.SetFloat("Parasite", this._Parasite);
			this.material.SetFloat("ZoomX", this._ZoomX);
			this.material.SetFloat("ZoomY", this._ZoomY);
			this.material.SetFloat("PosX", this._PosX);
			this.material.SetFloat("PosY", this._PosY);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x0600104F RID: 4175 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06001050 RID: 4176 RVA: 0x000784A2 File Offset: 0x000766A2
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001347 RID: 4935
	public Shader SCShader;

	// Token: 0x04001348 RID: 4936
	private float TimeX = 1f;

	// Token: 0x04001349 RID: 4937
	private Material SCMaterial;

	// Token: 0x0400134A RID: 4938
	[Range(0f, 1f)]
	public float __Speed = 1f;

	// Token: 0x0400134B RID: 4939
	[Range(0f, 1f)]
	public float _Fade = 1f;

	// Token: 0x0400134C RID: 4940
	[Range(0f, 1f)]
	public float _Parasite = 1f;

	// Token: 0x0400134D RID: 4941
	[Range(0f, 0f)]
	public float _ZoomX = 1f;

	// Token: 0x0400134E RID: 4942
	[Range(0f, 0f)]
	public float _ZoomY = 1f;

	// Token: 0x0400134F RID: 4943
	[Range(0f, 0f)]
	public float _PosX = 1f;

	// Token: 0x04001350 RID: 4944
	[Range(0f, 0f)]
	public float _PosY = 1f;
}
