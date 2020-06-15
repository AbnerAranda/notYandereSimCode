using System;
using UnityEngine;

// Token: 0x020001EB RID: 491
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Pixelisation/Dot")]
public class CameraFilterPack_Pixelisation_Dot : MonoBehaviour
{
	// Token: 0x1700030A RID: 778
	// (get) Token: 0x060010B8 RID: 4280 RVA: 0x0007A6C1 File Offset: 0x000788C1
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

	// Token: 0x060010B9 RID: 4281 RVA: 0x0007A6F5 File Offset: 0x000788F5
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Pixelisation_Dot");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x060010BA RID: 4282 RVA: 0x0007A718 File Offset: 0x00078918
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
			this.material.SetFloat("_Value2", this.LightBackGround);
			this.material.SetFloat("_Value3", this.Speed);
			this.material.SetFloat("_Value4", this.Size2);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x060010BB RID: 4283 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x060010BC RID: 4284 RVA: 0x0007A810 File Offset: 0x00078A10
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040013D0 RID: 5072
	public Shader SCShader;

	// Token: 0x040013D1 RID: 5073
	private float TimeX = 1f;

	// Token: 0x040013D2 RID: 5074
	private Material SCMaterial;

	// Token: 0x040013D3 RID: 5075
	[Range(0.0001f, 0.5f)]
	public float Size = 0.005f;

	// Token: 0x040013D4 RID: 5076
	[Range(0f, 1f)]
	public float LightBackGround = 0.3f;

	// Token: 0x040013D5 RID: 5077
	[Range(0f, 10f)]
	private float Speed = 1f;

	// Token: 0x040013D6 RID: 5078
	[Range(0f, 10f)]
	private float Size2 = 1f;
}
