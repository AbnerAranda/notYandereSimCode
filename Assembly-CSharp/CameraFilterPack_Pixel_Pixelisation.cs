using System;
using UnityEngine;

// Token: 0x020001E9 RID: 489
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Pixel/Pixelisation")]
public class CameraFilterPack_Pixel_Pixelisation : MonoBehaviour
{
	// Token: 0x17000308 RID: 776
	// (get) Token: 0x060010AC RID: 4268 RVA: 0x0007A343 File Offset: 0x00078543
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

	// Token: 0x060010AD RID: 4269 RVA: 0x0007A377 File Offset: 0x00078577
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Pixel_Pixelisation");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x060010AE RID: 4270 RVA: 0x0007A398 File Offset: 0x00078598
	private void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
	{
		if (this.SCShader != null)
		{
			this.material.SetFloat("_Val", this._Pixelisation);
			this.material.SetFloat("_Val2", this._SizeX);
			this.material.SetFloat("_Val3", this._SizeY);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x060010AF RID: 4271 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x060010B0 RID: 4272 RVA: 0x0007A40A File Offset: 0x0007860A
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040013C0 RID: 5056
	public Shader SCShader;

	// Token: 0x040013C1 RID: 5057
	[Range(0.6f, 120f)]
	public float _Pixelisation = 8f;

	// Token: 0x040013C2 RID: 5058
	[Range(0.6f, 120f)]
	public float _SizeX = 1f;

	// Token: 0x040013C3 RID: 5059
	[Range(0.6f, 120f)]
	public float _SizeY = 1f;

	// Token: 0x040013C4 RID: 5060
	private Material SCMaterial;
}
