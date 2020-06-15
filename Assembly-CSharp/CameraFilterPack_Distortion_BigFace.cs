﻿using System;
using UnityEngine;

// Token: 0x02000166 RID: 358
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Distortion/BigFace")]
public class CameraFilterPack_Distortion_BigFace : MonoBehaviour
{
	// Token: 0x17000285 RID: 645
	// (get) Token: 0x06000D77 RID: 3447 RVA: 0x0006C13B File Offset: 0x0006A33B
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

	// Token: 0x06000D78 RID: 3448 RVA: 0x0006C16F File Offset: 0x0006A36F
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Distortion_BigFace");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000D79 RID: 3449 RVA: 0x0006C190 File Offset: 0x0006A390
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
			this.material.SetFloat("_Size", this._Size);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000D7A RID: 3450 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000D7B RID: 3451 RVA: 0x0006C25C File Offset: 0x0006A45C
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x0400104C RID: 4172
	public Shader SCShader;

	// Token: 0x0400104D RID: 4173
	private float TimeX = 6.5f;

	// Token: 0x0400104E RID: 4174
	private Material SCMaterial;

	// Token: 0x0400104F RID: 4175
	public float _Size = 5f;

	// Token: 0x04001050 RID: 4176
	[Range(2f, 10f)]
	public float Distortion = 2.5f;
}
