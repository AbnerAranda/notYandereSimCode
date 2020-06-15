﻿using System;
using UnityEngine;

// Token: 0x02000172 RID: 370
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Distortion/ShockWave")]
public class CameraFilterPack_Distortion_ShockWave : MonoBehaviour
{
	// Token: 0x17000291 RID: 657
	// (get) Token: 0x06000DBF RID: 3519 RVA: 0x0006D183 File Offset: 0x0006B383
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

	// Token: 0x06000DC0 RID: 3520 RVA: 0x0006D1B7 File Offset: 0x0006B3B7
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Distortion_ShockWave");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000DC1 RID: 3521 RVA: 0x0006D1D8 File Offset: 0x0006B3D8
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
			this.material.SetFloat("_Value", this.PosX);
			this.material.SetFloat("_Value2", this.PosY);
			this.material.SetFloat("_Value3", this.Speed);
			this.material.SetFloat("_Value4", this.Size);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000DC2 RID: 3522 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000DC3 RID: 3523 RVA: 0x0006D2D0 File Offset: 0x0006B4D0
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x0400108A RID: 4234
	public Shader SCShader;

	// Token: 0x0400108B RID: 4235
	private float TimeX = 1f;

	// Token: 0x0400108C RID: 4236
	private Material SCMaterial;

	// Token: 0x0400108D RID: 4237
	[Range(-1.5f, 1.5f)]
	public float PosX = 0.5f;

	// Token: 0x0400108E RID: 4238
	[Range(-1.5f, 1.5f)]
	public float PosY = 0.5f;

	// Token: 0x0400108F RID: 4239
	[Range(0f, 10f)]
	public float Speed = 1f;

	// Token: 0x04001090 RID: 4240
	[Range(0f, 10f)]
	private float Size = 1f;
}
