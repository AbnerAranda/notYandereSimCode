﻿using System;
using UnityEngine;

// Token: 0x020001AC RID: 428
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/FX/InverChromiLum")]
public class CameraFilterPack_FX_InverChromiLum : MonoBehaviour
{
	// Token: 0x170002CB RID: 715
	// (get) Token: 0x06000F1C RID: 3868 RVA: 0x0007286E File Offset: 0x00070A6E
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

	// Token: 0x06000F1D RID: 3869 RVA: 0x000728A2 File Offset: 0x00070AA2
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/FX_InverChromiLum");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000F1E RID: 3870 RVA: 0x000728C4 File Offset: 0x00070AC4
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
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000F1F RID: 3871 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000F20 RID: 3872 RVA: 0x00072961 File Offset: 0x00070B61
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040011F2 RID: 4594
	public Shader SCShader;

	// Token: 0x040011F3 RID: 4595
	private float TimeX = 1f;

	// Token: 0x040011F4 RID: 4596
	private Material SCMaterial;
}
