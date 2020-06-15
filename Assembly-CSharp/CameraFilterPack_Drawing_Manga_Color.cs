﻿using System;
using UnityEngine;

// Token: 0x02000187 RID: 391
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Drawing/Manga_Color")]
public class CameraFilterPack_Drawing_Manga_Color : MonoBehaviour
{
	// Token: 0x170002A6 RID: 678
	// (get) Token: 0x06000E3D RID: 3645 RVA: 0x0006EF52 File Offset: 0x0006D152
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

	// Token: 0x06000E3E RID: 3646 RVA: 0x0006EF86 File Offset: 0x0006D186
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Drawing_Manga_Color");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000E3F RID: 3647 RVA: 0x0006EFA8 File Offset: 0x0006D1A8
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
			this.material.SetFloat("_DotSize", this.DotSize);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000E40 RID: 3648 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000E41 RID: 3649 RVA: 0x0006F02E File Offset: 0x0006D22E
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001103 RID: 4355
	public Shader SCShader;

	// Token: 0x04001104 RID: 4356
	private float TimeX = 1f;

	// Token: 0x04001105 RID: 4357
	private Material SCMaterial;

	// Token: 0x04001106 RID: 4358
	[Range(1f, 8f)]
	public float DotSize = 1.6f;

	// Token: 0x04001107 RID: 4359
	public static float ChangeDotSize;
}
