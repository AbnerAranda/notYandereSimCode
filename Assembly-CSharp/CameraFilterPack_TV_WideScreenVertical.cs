using System;
using UnityEngine;

// Token: 0x02000215 RID: 533
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/TV/WideScreenVertical")]
public class CameraFilterPack_TV_WideScreenVertical : MonoBehaviour
{
	// Token: 0x17000334 RID: 820
	// (get) Token: 0x060011B5 RID: 4533 RVA: 0x0007E81D File Offset: 0x0007CA1D
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

	// Token: 0x060011B6 RID: 4534 RVA: 0x0007E851 File Offset: 0x0007CA51
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/TV_WideScreenVertical");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x060011B7 RID: 4535 RVA: 0x0007E874 File Offset: 0x0007CA74
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
			this.material.SetFloat("_Value", this.Size);
			this.material.SetFloat("_Value2", this.Smooth);
			this.material.SetFloat("_Value3", this.StretchX);
			this.material.SetFloat("_Value4", this.StretchY);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x060011B8 RID: 4536 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x060011B9 RID: 4537 RVA: 0x0007E96C File Offset: 0x0007CB6C
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040014CD RID: 5325
	public Shader SCShader;

	// Token: 0x040014CE RID: 5326
	private float TimeX = 1f;

	// Token: 0x040014CF RID: 5327
	private Material SCMaterial;

	// Token: 0x040014D0 RID: 5328
	[Range(0f, 0.8f)]
	public float Size = 0.55f;

	// Token: 0x040014D1 RID: 5329
	[Range(0.001f, 0.4f)]
	public float Smooth = 0.01f;

	// Token: 0x040014D2 RID: 5330
	[Range(0f, 10f)]
	private float StretchX = 1f;

	// Token: 0x040014D3 RID: 5331
	[Range(0f, 10f)]
	private float StretchY = 1f;
}
