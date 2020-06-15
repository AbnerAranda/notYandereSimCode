using System;
using UnityEngine;

// Token: 0x020001F7 RID: 503
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/TV/ARCADE_2")]
public class CameraFilterPack_TV_ARCADE_2 : MonoBehaviour
{
	// Token: 0x17000316 RID: 790
	// (get) Token: 0x06001101 RID: 4353 RVA: 0x0007BA22 File Offset: 0x00079C22
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

	// Token: 0x06001102 RID: 4354 RVA: 0x0007BA56 File Offset: 0x00079C56
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/TV_ARCADE_2");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06001103 RID: 4355 RVA: 0x0007BA78 File Offset: 0x00079C78
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
			this.material.SetFloat("_Value", this.Interferance_Size);
			this.material.SetFloat("_Value2", this.Interferance_Speed);
			this.material.SetFloat("_Value3", this.Contrast);
			this.material.SetFloat("Fade", this.Fade);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06001104 RID: 4356 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06001105 RID: 4357 RVA: 0x0007BB70 File Offset: 0x00079D70
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001416 RID: 5142
	public Shader SCShader;

	// Token: 0x04001417 RID: 5143
	private float TimeX = 1f;

	// Token: 0x04001418 RID: 5144
	private Material SCMaterial;

	// Token: 0x04001419 RID: 5145
	[Range(0f, 10f)]
	public float Interferance_Size = 1f;

	// Token: 0x0400141A RID: 5146
	[Range(0f, 10f)]
	public float Interferance_Speed = 0.5f;

	// Token: 0x0400141B RID: 5147
	[Range(0f, 10f)]
	public float Contrast = 1f;

	// Token: 0x0400141C RID: 5148
	[Range(0f, 1f)]
	public float Fade = 1f;
}
