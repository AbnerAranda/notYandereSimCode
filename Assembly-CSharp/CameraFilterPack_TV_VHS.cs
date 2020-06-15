using System;
using UnityEngine;

// Token: 0x0200020B RID: 523
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/VHS/VHS")]
public class CameraFilterPack_TV_VHS : MonoBehaviour
{
	// Token: 0x1700032A RID: 810
	// (get) Token: 0x06001179 RID: 4473 RVA: 0x0007DA25 File Offset: 0x0007BC25
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

	// Token: 0x0600117A RID: 4474 RVA: 0x0007DA59 File Offset: 0x0007BC59
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/TV_VHS");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x0600117B RID: 4475 RVA: 0x0007DA7C File Offset: 0x0007BC7C
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
			this.material.SetFloat("_Value3", this.Calibrage);
			this.material.SetFloat("_Value4", this.WhiteParasite);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x0600117C RID: 4476 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x0600117D RID: 4477 RVA: 0x0007DB74 File Offset: 0x0007BD74
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001495 RID: 5269
	public Shader SCShader;

	// Token: 0x04001496 RID: 5270
	private float TimeX = 1f;

	// Token: 0x04001497 RID: 5271
	private Material SCMaterial;

	// Token: 0x04001498 RID: 5272
	[Range(1f, 256f)]
	public float Cryptage = 64f;

	// Token: 0x04001499 RID: 5273
	[Range(1f, 100f)]
	public float Parasite = 32f;

	// Token: 0x0400149A RID: 5274
	[Range(0f, 3f)]
	public float Calibrage;

	// Token: 0x0400149B RID: 5275
	[Range(0f, 1f)]
	public float WhiteParasite = 1f;
}
