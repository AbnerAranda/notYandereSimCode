using System;
using UnityEngine;

// Token: 0x020001F9 RID: 505
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/TV/Artefact")]
public class CameraFilterPack_TV_Artefact : MonoBehaviour
{
	// Token: 0x17000318 RID: 792
	// (get) Token: 0x0600110D RID: 4365 RVA: 0x0007BD9B File Offset: 0x00079F9B
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

	// Token: 0x0600110E RID: 4366 RVA: 0x0007BDCF File Offset: 0x00079FCF
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/TV_Artefact");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x0600110F RID: 4367 RVA: 0x0007BDF0 File Offset: 0x00079FF0
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
			this.material.SetFloat("_Colorisation", this.Colorisation);
			this.material.SetFloat("_Parasite", this.Parasite);
			this.material.SetFloat("_Noise", this.Noise);
			this.material.SetFloat("Fade", this.Fade);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06001110 RID: 4368 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06001111 RID: 4369 RVA: 0x0007BEE8 File Offset: 0x0007A0E8
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001425 RID: 5157
	public Shader SCShader;

	// Token: 0x04001426 RID: 5158
	private float TimeX = 1f;

	// Token: 0x04001427 RID: 5159
	[Range(0f, 1f)]
	public float Fade = 1f;

	// Token: 0x04001428 RID: 5160
	[Range(-10f, 10f)]
	public float Colorisation = 1f;

	// Token: 0x04001429 RID: 5161
	[Range(-10f, 10f)]
	public float Parasite = 1f;

	// Token: 0x0400142A RID: 5162
	[Range(-10f, 10f)]
	public float Noise = 1f;

	// Token: 0x0400142B RID: 5163
	private Material SCMaterial;
}
