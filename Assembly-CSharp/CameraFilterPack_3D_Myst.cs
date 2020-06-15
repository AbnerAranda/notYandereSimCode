﻿using System;
using UnityEngine;

// Token: 0x02000105 RID: 261
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/3D/Myst")]
public class CameraFilterPack_3D_Myst : MonoBehaviour
{
	// Token: 0x17000224 RID: 548
	// (get) Token: 0x06000AF0 RID: 2800 RVA: 0x0005FF44 File Offset: 0x0005E144
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

	// Token: 0x06000AF1 RID: 2801 RVA: 0x0005FF78 File Offset: 0x0005E178
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_3D_Myst1") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/3D_Myst");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000AF2 RID: 2802 RVA: 0x0005FFB0 File Offset: 0x0005E1B0
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
			if (this.AutoAnimatedNear)
			{
				this._Distance += Time.deltaTime * this.AutoAnimatedNearSpeed;
				if (this._Distance > 1f)
				{
					this._Distance = -1f;
				}
				if (this._Distance < -1f)
				{
					this._Distance = 1f;
				}
				this.material.SetFloat("_Near", this._Distance);
			}
			else
			{
				this.material.SetFloat("_Near", this._Distance);
			}
			this.material.SetFloat("_Far", this._Size);
			this.material.SetFloat("_Visualize", (float)(this._Visualize ? 1 : 0));
			this.material.SetFloat("_FixDistance", this._FixDistance);
			this.material.SetFloat("_DistortionLevel", this.DistortionLevel * 28f);
			this.material.SetFloat("_DistortionSize", this.DistortionSize * 16f);
			this.material.SetFloat("_LightIntensity", this.LightIntensity * 64f);
			this.material.SetTexture("_MainTex2", this.Texture2);
			float farClipPlane = base.GetComponent<Camera>().farClipPlane;
			this.material.SetFloat("_FarCamera", 1000f / farClipPlane);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			base.GetComponent<Camera>().depthTextureMode = DepthTextureMode.Depth;
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000AF3 RID: 2803 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000AF4 RID: 2804 RVA: 0x000601B1 File Offset: 0x0005E3B1
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000D37 RID: 3383
	public Shader SCShader;

	// Token: 0x04000D38 RID: 3384
	public bool _Visualize;

	// Token: 0x04000D39 RID: 3385
	private float TimeX = 1f;

	// Token: 0x04000D3A RID: 3386
	private Material SCMaterial;

	// Token: 0x04000D3B RID: 3387
	[Range(0f, 100f)]
	public float _FixDistance = 1f;

	// Token: 0x04000D3C RID: 3388
	[Range(-0.99f, 0.99f)]
	public float _Distance = 0.5f;

	// Token: 0x04000D3D RID: 3389
	[Range(0f, 0.5f)]
	public float _Size = 0.1f;

	// Token: 0x04000D3E RID: 3390
	[Range(0f, 10f)]
	public float DistortionLevel = 1.2f;

	// Token: 0x04000D3F RID: 3391
	[Range(0.1f, 10f)]
	public float DistortionSize = 1.4f;

	// Token: 0x04000D40 RID: 3392
	[Range(-2f, 4f)]
	public float LightIntensity = 0.08f;

	// Token: 0x04000D41 RID: 3393
	public bool AutoAnimatedNear;

	// Token: 0x04000D42 RID: 3394
	[Range(-5f, 5f)]
	public float AutoAnimatedNearSpeed = 0.5f;

	// Token: 0x04000D43 RID: 3395
	private Texture2D Texture2;

	// Token: 0x04000D44 RID: 3396
	public static Color ChangeColorRGB;
}
