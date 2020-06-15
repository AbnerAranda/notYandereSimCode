using System;
using UnityEngine;

// Token: 0x02000164 RID: 356
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Convert/NormalMap")]
public class CameraFilterPack_Convert_Normal : MonoBehaviour
{
	// Token: 0x17000283 RID: 643
	// (get) Token: 0x06000D6B RID: 3435 RVA: 0x0006BEA2 File Offset: 0x0006A0A2
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

	// Token: 0x06000D6C RID: 3436 RVA: 0x0006BED6 File Offset: 0x0006A0D6
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Color_Convert_Normal");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000D6D RID: 3437 RVA: 0x0006BEF8 File Offset: 0x0006A0F8
	private void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
	{
		if (this.SCShader != null)
		{
			this.material.SetFloat("_Heigh", this._Heigh);
			this.material.SetFloat("_Intervale", this._Intervale);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000D6E RID: 3438 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000D6F RID: 3439 RVA: 0x0006BF54 File Offset: 0x0006A154
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001041 RID: 4161
	public Shader SCShader;

	// Token: 0x04001042 RID: 4162
	[Range(0f, 0.5f)]
	public float _Heigh = 0.0125f;

	// Token: 0x04001043 RID: 4163
	[Range(0f, 0.25f)]
	public float _Intervale = 0.0025f;

	// Token: 0x04001044 RID: 4164
	private Material SCMaterial;
}
