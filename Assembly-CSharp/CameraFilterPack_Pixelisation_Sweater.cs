using System;
using UnityEngine;

// Token: 0x020001EE RID: 494
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Pixelisation/Pixelisation_Sweater")]
public class CameraFilterPack_Pixelisation_Sweater : MonoBehaviour
{
	// Token: 0x1700030D RID: 781
	// (get) Token: 0x060010CA RID: 4298 RVA: 0x0007AAF2 File Offset: 0x00078CF2
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

	// Token: 0x060010CB RID: 4299 RVA: 0x0007AB26 File Offset: 0x00078D26
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_Sweater") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/Pixelisation_Sweater");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x060010CC RID: 4300 RVA: 0x0007AB5C File Offset: 0x00078D5C
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
			this.material.SetFloat("_Fade", this.Fade);
			this.material.SetFloat("_SweaterSize", this.SweaterSize);
			this.material.SetFloat("_Intensity", this._Intensity);
			this.material.SetTexture("Texture2", this.Texture2);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x060010CD RID: 4301 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x060010CE RID: 4302 RVA: 0x0007AC27 File Offset: 0x00078E27
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040013DF RID: 5087
	public Shader SCShader;

	// Token: 0x040013E0 RID: 5088
	private float TimeX = 1f;

	// Token: 0x040013E1 RID: 5089
	private Material SCMaterial;

	// Token: 0x040013E2 RID: 5090
	[Range(16f, 128f)]
	public float SweaterSize = 64f;

	// Token: 0x040013E3 RID: 5091
	[Range(0f, 2f)]
	public float _Intensity = 1.4f;

	// Token: 0x040013E4 RID: 5092
	[Range(0f, 1f)]
	public float Fade = 1f;

	// Token: 0x040013E5 RID: 5093
	private Texture2D Texture2;
}
