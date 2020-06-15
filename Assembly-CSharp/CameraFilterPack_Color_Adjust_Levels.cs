﻿using System;
using UnityEngine;

// Token: 0x0200014D RID: 333
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Colors/Levels")]
public class CameraFilterPack_Color_Adjust_Levels : MonoBehaviour
{
	// Token: 0x1700026C RID: 620
	// (get) Token: 0x06000CDF RID: 3295 RVA: 0x000698A3 File Offset: 0x00067AA3
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

	// Token: 0x06000CE0 RID: 3296 RVA: 0x000698D7 File Offset: 0x00067AD7
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Color_Levels");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000CE1 RID: 3297 RVA: 0x000698F8 File Offset: 0x00067AF8
	private void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
	{
		if (this.SCShader != null)
		{
			this.TimeX += Time.deltaTime;
			if (this.TimeX > 100f)
			{
				this.TimeX = 0f;
			}
			this.material.SetFloat("levelMinimum", this.levelMinimum);
			this.material.SetFloat("levelMiddle", this.levelMiddle);
			this.material.SetFloat("levelMaximum", this.levelMaximum);
			this.material.SetFloat("minOutput", this.minOutput);
			this.material.SetFloat("maxOutput", this.maxOutput);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000CE2 RID: 3298 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000CE3 RID: 3299 RVA: 0x000699F0 File Offset: 0x00067BF0
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000FBD RID: 4029
	public Shader SCShader;

	// Token: 0x04000FBE RID: 4030
	private float TimeX = 1f;

	// Token: 0x04000FBF RID: 4031
	private Material SCMaterial;

	// Token: 0x04000FC0 RID: 4032
	[Range(0f, 1f)]
	public float levelMinimum;

	// Token: 0x04000FC1 RID: 4033
	[Range(0f, 1f)]
	public float levelMiddle = 0.5f;

	// Token: 0x04000FC2 RID: 4034
	[Range(0f, 1f)]
	public float levelMaximum = 1f;

	// Token: 0x04000FC3 RID: 4035
	[Range(0f, 1f)]
	public float minOutput;

	// Token: 0x04000FC4 RID: 4036
	[Range(0f, 1f)]
	public float maxOutput = 1f;
}
