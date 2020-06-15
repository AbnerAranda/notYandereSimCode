﻿using System;
using UnityEngine;

// Token: 0x02000175 RID: 373
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Distortion/Twist_Square")]
public class CameraFilterPack_Distortion_Twist_Square : MonoBehaviour
{
	// Token: 0x17000294 RID: 660
	// (get) Token: 0x06000DD1 RID: 3537 RVA: 0x0006D672 File Offset: 0x0006B872
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

	// Token: 0x06000DD2 RID: 3538 RVA: 0x0006D6A6 File Offset: 0x0006B8A6
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Distortion_Twist_Square");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000DD3 RID: 3539 RVA: 0x0006D6C8 File Offset: 0x0006B8C8
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
			this.material.SetFloat("_CenterX", this.CenterX);
			this.material.SetFloat("_CenterY", this.CenterY);
			this.material.SetFloat("_Distortion", this.Distortion);
			this.material.SetFloat("_Size", this.Size);
			this.material.SetVector("_ScreenResolution", new Vector2((float)Screen.width, (float)Screen.height));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000DD4 RID: 3540 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000DD5 RID: 3541 RVA: 0x0006D7B9 File Offset: 0x0006B9B9
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x0400109F RID: 4255
	public Shader SCShader;

	// Token: 0x040010A0 RID: 4256
	private float TimeX = 1f;

	// Token: 0x040010A1 RID: 4257
	private Material SCMaterial;

	// Token: 0x040010A2 RID: 4258
	[Range(-2f, 2f)]
	public float CenterX = 0.5f;

	// Token: 0x040010A3 RID: 4259
	[Range(-2f, 2f)]
	public float CenterY = 0.5f;

	// Token: 0x040010A4 RID: 4260
	[Range(-3.14f, 3.14f)]
	public float Distortion = 0.5f;

	// Token: 0x040010A5 RID: 4261
	[Range(-2f, 2f)]
	public float Size = 0.25f;
}
