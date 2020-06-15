using System;
using UnityEngine;

// Token: 0x0200013A RID: 314
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Blur/Blur Hole")]
public class CameraFilterPack_Blur_BlurHole : MonoBehaviour
{
	// Token: 0x17000259 RID: 601
	// (get) Token: 0x06000C6D RID: 3181 RVA: 0x000677F0 File Offset: 0x000659F0
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

	// Token: 0x06000C6E RID: 3182 RVA: 0x00067824 File Offset: 0x00065A24
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/BlurHole");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000C6F RID: 3183 RVA: 0x00067848 File Offset: 0x00065A48
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
			this.material.SetFloat("_Radius", this._Radius);
			this.material.SetFloat("_SpotSize", this._SpotSize);
			this.material.SetFloat("_CenterX", this._CenterX);
			this.material.SetFloat("_CenterY", this._CenterY);
			this.material.SetFloat("_Alpha", this._AlphaBlur);
			this.material.SetFloat("_Alpha2", this._AlphaBlurInside);
			this.material.SetVector("_ScreenResolution", new Vector2((float)Screen.width, (float)Screen.height));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000C70 RID: 3184 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000C71 RID: 3185 RVA: 0x0006797B File Offset: 0x00065B7B
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000F3F RID: 3903
	public Shader SCShader;

	// Token: 0x04000F40 RID: 3904
	private float TimeX = 1f;

	// Token: 0x04000F41 RID: 3905
	[Range(1f, 16f)]
	public float Size = 10f;

	// Token: 0x04000F42 RID: 3906
	[Range(-1f, 1f)]
	public float _Radius = 0.25f;

	// Token: 0x04000F43 RID: 3907
	[Range(-4f, 4f)]
	public float _SpotSize = 1f;

	// Token: 0x04000F44 RID: 3908
	[Range(0f, 1f)]
	public float _CenterX = 0.5f;

	// Token: 0x04000F45 RID: 3909
	[Range(0f, 1f)]
	public float _CenterY = 0.5f;

	// Token: 0x04000F46 RID: 3910
	[Range(0f, 1f)]
	public float _AlphaBlur = 1f;

	// Token: 0x04000F47 RID: 3911
	[Range(0f, 1f)]
	public float _AlphaBlurInside;

	// Token: 0x04000F48 RID: 3912
	private Material SCMaterial;
}
