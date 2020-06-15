using System;
using UnityEngine;

// Token: 0x020001F2 RID: 498
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Sharpen/Sharpen")]
public class CameraFilterPack_Sharpen_Sharpen : MonoBehaviour
{
	// Token: 0x17000311 RID: 785
	// (get) Token: 0x060010E3 RID: 4323 RVA: 0x0007B34A File Offset: 0x0007954A
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

	// Token: 0x060010E4 RID: 4324 RVA: 0x0007B37E File Offset: 0x0007957E
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Sharpen_Sharpen");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x060010E5 RID: 4325 RVA: 0x0007B3A0 File Offset: 0x000795A0
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
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			this.material.SetFloat("_Value", this.Value);
			this.material.SetFloat("_Value2", this.Value2);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x060010E6 RID: 4326 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x060010E7 RID: 4327 RVA: 0x0007B46C File Offset: 0x0007966C
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040013FE RID: 5118
	public Shader SCShader;

	// Token: 0x040013FF RID: 5119
	[Range(0.001f, 100f)]
	public float Value = 4f;

	// Token: 0x04001400 RID: 5120
	[Range(0.001f, 32f)]
	public float Value2 = 1f;

	// Token: 0x04001401 RID: 5121
	private float TimeX = 1f;

	// Token: 0x04001402 RID: 5122
	private Material SCMaterial;
}
