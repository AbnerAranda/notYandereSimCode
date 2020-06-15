﻿using System;
using UnityEngine;

// Token: 0x02000171 RID: 369
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Distortion/Noise")]
public class CameraFilterPack_Distortion_Noise : MonoBehaviour
{
	// Token: 0x17000290 RID: 656
	// (get) Token: 0x06000DB9 RID: 3513 RVA: 0x0006D047 File Offset: 0x0006B247
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

	// Token: 0x06000DBA RID: 3514 RVA: 0x0006D07B File Offset: 0x0006B27B
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Distortion_Noise");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000DBB RID: 3515 RVA: 0x0006D09C File Offset: 0x0006B29C
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
			this.material.SetVector("_ScreenResolution", new Vector2((float)Screen.width, (float)Screen.height));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000DBC RID: 3516 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000DBD RID: 3517 RVA: 0x0006D14B File Offset: 0x0006B34B
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001086 RID: 4230
	public Shader SCShader;

	// Token: 0x04001087 RID: 4231
	private float TimeX = 1f;

	// Token: 0x04001088 RID: 4232
	private Material SCMaterial;

	// Token: 0x04001089 RID: 4233
	[Range(0f, 3f)]
	public float Distortion = 1f;
}
