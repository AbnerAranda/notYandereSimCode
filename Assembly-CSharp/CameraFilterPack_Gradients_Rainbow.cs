﻿using System;
using UnityEngine;

// Token: 0x020001C7 RID: 455
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Gradients/Rainbow")]
public class CameraFilterPack_Gradients_Rainbow : MonoBehaviour
{
	// Token: 0x170002E6 RID: 742
	// (get) Token: 0x06000FBE RID: 4030 RVA: 0x000758AA File Offset: 0x00073AAA
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

	// Token: 0x06000FBF RID: 4031 RVA: 0x000758DE File Offset: 0x00073ADE
	private void Start()
	{
		this.SCShader = Shader.Find(this.ShaderName);
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000FC0 RID: 4032 RVA: 0x00075900 File Offset: 0x00073B00
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
			this.material.SetFloat("_Value", this.Switch);
			this.material.SetFloat("_Value2", this.Fade);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000FC1 RID: 4033 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000FC2 RID: 4034 RVA: 0x000759CC File Offset: 0x00073BCC
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040012BE RID: 4798
	public Shader SCShader;

	// Token: 0x040012BF RID: 4799
	private string ShaderName = "CameraFilterPack/Gradients_Rainbow";

	// Token: 0x040012C0 RID: 4800
	private float TimeX = 1f;

	// Token: 0x040012C1 RID: 4801
	private Material SCMaterial;

	// Token: 0x040012C2 RID: 4802
	[Range(0f, 1f)]
	public float Switch = 1f;

	// Token: 0x040012C3 RID: 4803
	[Range(0f, 1f)]
	public float Fade = 1f;
}
