﻿using System;
using UnityEngine;

// Token: 0x020001AF RID: 431
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/FX/Psycho")]
public class CameraFilterPack_FX_Psycho : MonoBehaviour
{
	// Token: 0x170002CE RID: 718
	// (get) Token: 0x06000F2E RID: 3886 RVA: 0x00072BF2 File Offset: 0x00070DF2
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

	// Token: 0x06000F2F RID: 3887 RVA: 0x00072C26 File Offset: 0x00070E26
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/FX_Psycho");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000F30 RID: 3888 RVA: 0x00072C48 File Offset: 0x00070E48
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
			this.material.SetFloat("_Distortion", this.Distortion);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000F31 RID: 3889 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000F32 RID: 3890 RVA: 0x00072CCE File Offset: 0x00070ECE
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040011FC RID: 4604
	public Shader SCShader;

	// Token: 0x040011FD RID: 4605
	private Material SCMaterial;

	// Token: 0x040011FE RID: 4606
	private float TimeX = 1f;

	// Token: 0x040011FF RID: 4607
	[Range(0f, 1f)]
	public float Distortion = 1f;
}
