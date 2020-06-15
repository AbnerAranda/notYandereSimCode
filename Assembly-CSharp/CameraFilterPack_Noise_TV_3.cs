using System;
using UnityEngine;

// Token: 0x020001E1 RID: 481
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Noise/TV_3")]
public class CameraFilterPack_Noise_TV_3 : MonoBehaviour
{
	// Token: 0x17000300 RID: 768
	// (get) Token: 0x06001078 RID: 4216 RVA: 0x000793A5 File Offset: 0x000775A5
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

	// Token: 0x06001079 RID: 4217 RVA: 0x000793D9 File Offset: 0x000775D9
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_TV_Noise3") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/Noise_TV_3");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x0600107A RID: 4218 RVA: 0x00079410 File Offset: 0x00077610
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
			this.material.SetFloat("_Value", this.Fade);
			this.material.SetFloat("_Value2", this.Fade_Additive);
			this.material.SetFloat("_Value3", this.Fade_Distortion);
			this.material.SetFloat("_Value4", this.Value4);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			this.material.SetTexture("Texture2", this.Texture2);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x0600107B RID: 4219 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x0600107C RID: 4220 RVA: 0x0007951E File Offset: 0x0007771E
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001388 RID: 5000
	public Shader SCShader;

	// Token: 0x04001389 RID: 5001
	private float TimeX = 1f;

	// Token: 0x0400138A RID: 5002
	private Material SCMaterial;

	// Token: 0x0400138B RID: 5003
	[Range(0f, 1f)]
	public float Fade = 1f;

	// Token: 0x0400138C RID: 5004
	[Range(0f, 1f)]
	public float Fade_Additive;

	// Token: 0x0400138D RID: 5005
	[Range(0f, 1f)]
	public float Fade_Distortion;

	// Token: 0x0400138E RID: 5006
	[Range(0f, 10f)]
	private float Value4 = 1f;

	// Token: 0x0400138F RID: 5007
	private Texture2D Texture2;
}
