using System;
using UnityEngine;

// Token: 0x020001EC RID: 492
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Pixelisation/OilPaint")]
public class CameraFilterPack_Pixelisation_OilPaint : MonoBehaviour
{
	// Token: 0x1700030B RID: 779
	// (get) Token: 0x060010BE RID: 4286 RVA: 0x0007A869 File Offset: 0x00078A69
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

	// Token: 0x060010BF RID: 4287 RVA: 0x0007A89D File Offset: 0x00078A9D
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Pixelisation_OilPaint");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x060010C0 RID: 4288 RVA: 0x0007A8C0 File Offset: 0x00078AC0
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
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x060010C1 RID: 4289 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x060010C2 RID: 4290 RVA: 0x0007A976 File Offset: 0x00078B76
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040013D7 RID: 5079
	public Shader SCShader;

	// Token: 0x040013D8 RID: 5080
	private float TimeX = 1f;

	// Token: 0x040013D9 RID: 5081
	private Material SCMaterial;

	// Token: 0x040013DA RID: 5082
	[Range(0f, 5f)]
	public float Value = 1f;
}
