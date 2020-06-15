﻿using System;
using UnityEngine;

// Token: 0x020001A3 RID: 419
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/FX/Earth Quake")]
public class CameraFilterPack_FX_EarthQuake : MonoBehaviour
{
	// Token: 0x170002C2 RID: 706
	// (get) Token: 0x06000EE6 RID: 3814 RVA: 0x00071CA5 File Offset: 0x0006FEA5
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

	// Token: 0x06000EE7 RID: 3815 RVA: 0x00071CD9 File Offset: 0x0006FED9
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/FX_EarthQuake");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000EE8 RID: 3816 RVA: 0x00071CFC File Offset: 0x0006FEFC
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
			this.material.SetFloat("_Value", this.Speed);
			this.material.SetFloat("_Value2", this.X);
			this.material.SetFloat("_Value3", this.Y);
			this.material.SetFloat("_Value4", this.Value4);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000EE9 RID: 3817 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000EEA RID: 3818 RVA: 0x00071DF4 File Offset: 0x0006FFF4
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040011C8 RID: 4552
	public Shader SCShader;

	// Token: 0x040011C9 RID: 4553
	private float TimeX = 1f;

	// Token: 0x040011CA RID: 4554
	private Material SCMaterial;

	// Token: 0x040011CB RID: 4555
	[Range(0f, 100f)]
	public float Speed = 15f;

	// Token: 0x040011CC RID: 4556
	[Range(0f, 0.2f)]
	public float X = 0.008f;

	// Token: 0x040011CD RID: 4557
	[Range(0f, 0.2f)]
	public float Y = 0.008f;

	// Token: 0x040011CE RID: 4558
	[Range(0f, 0.2f)]
	private float Value4 = 1f;
}
