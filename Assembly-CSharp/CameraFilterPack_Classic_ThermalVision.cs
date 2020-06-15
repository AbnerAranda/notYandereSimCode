using System;
using UnityEngine;

// Token: 0x0200014C RID: 332
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Classic/ThermalVision")]
public class CameraFilterPack_Classic_ThermalVision : MonoBehaviour
{
	// Token: 0x1700026B RID: 619
	// (get) Token: 0x06000CD9 RID: 3289 RVA: 0x0006968C File Offset: 0x0006788C
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

	// Token: 0x06000CDA RID: 3290 RVA: 0x000696C0 File Offset: 0x000678C0
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/CameraFilterPack_Classic_ThermalVision");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000CDB RID: 3291 RVA: 0x000696E4 File Offset: 0x000678E4
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
			this.material.SetFloat("Crt", this._Crt);
			this.material.SetFloat("Curve", this._Curve);
			this.material.SetFloat("Color1", this._Color1);
			this.material.SetFloat("Color2", this._Color2);
			this.material.SetFloat("Color3", this._Color3);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000CDC RID: 3292 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000CDD RID: 3293 RVA: 0x0006981E File Offset: 0x00067A1E
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000FB3 RID: 4019
	public Shader SCShader;

	// Token: 0x04000FB4 RID: 4020
	private float TimeX = 1f;

	// Token: 0x04000FB5 RID: 4021
	private Material SCMaterial;

	// Token: 0x04000FB6 RID: 4022
	[Range(0f, 1f)]
	public float __Speed = 1f;

	// Token: 0x04000FB7 RID: 4023
	[Range(0f, 1f)]
	public float _Fade = 1f;

	// Token: 0x04000FB8 RID: 4024
	[Range(0f, 1f)]
	public float _Crt = 1f;

	// Token: 0x04000FB9 RID: 4025
	[Range(0f, 1f)]
	public float _Curve = 1f;

	// Token: 0x04000FBA RID: 4026
	[Range(0f, 1f)]
	public float _Color1 = 1f;

	// Token: 0x04000FBB RID: 4027
	[Range(0f, 1f)]
	public float _Color2 = 1f;

	// Token: 0x04000FBC RID: 4028
	[Range(0f, 1f)]
	public float _Color3 = 1f;
}
