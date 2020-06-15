using System;
using UnityEngine;

// Token: 0x0200020C RID: 524
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/VHS/VHS_Rewind")]
public class CameraFilterPack_TV_VHS_Rewind : MonoBehaviour
{
	// Token: 0x1700032B RID: 811
	// (get) Token: 0x0600117F RID: 4479 RVA: 0x0007DBC2 File Offset: 0x0007BDC2
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

	// Token: 0x06001180 RID: 4480 RVA: 0x0007DBF6 File Offset: 0x0007BDF6
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/TV_VHS_Rewind");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06001181 RID: 4481 RVA: 0x0007DC18 File Offset: 0x0007BE18
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
			this.material.SetFloat("_Value", this.Cryptage);
			this.material.SetFloat("_Value2", this.Parasite);
			this.material.SetFloat("_Value3", this.Parasite2);
			this.material.SetFloat("_Value4", this.WhiteParasite);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06001182 RID: 4482 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06001183 RID: 4483 RVA: 0x0007DD10 File Offset: 0x0007BF10
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x0400149C RID: 5276
	public Shader SCShader;

	// Token: 0x0400149D RID: 5277
	private float TimeX = 1f;

	// Token: 0x0400149E RID: 5278
	private Material SCMaterial;

	// Token: 0x0400149F RID: 5279
	[Range(0f, 1f)]
	public float Cryptage = 1f;

	// Token: 0x040014A0 RID: 5280
	[Range(-20f, 20f)]
	public float Parasite = 9f;

	// Token: 0x040014A1 RID: 5281
	[Range(-20f, 20f)]
	public float Parasite2 = 12f;

	// Token: 0x040014A2 RID: 5282
	[Range(0f, 1f)]
	private float WhiteParasite = 1f;
}
