using System;
using UnityEngine;

// Token: 0x0200015F RID: 351
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Colors/HSV")]
public class CameraFilterPack_Colors_HSV : MonoBehaviour
{
	// Token: 0x1700027E RID: 638
	// (get) Token: 0x06000D4D RID: 3405 RVA: 0x0006B7AD File Offset: 0x000699AD
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

	// Token: 0x06000D4E RID: 3406 RVA: 0x0006B7E1 File Offset: 0x000699E1
	private void Start()
	{
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000D4F RID: 3407 RVA: 0x0006B7F4 File Offset: 0x000699F4
	private void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
	{
		if (this.SCShader != null)
		{
			this.material.SetFloat("_HueShift", this._HueShift);
			this.material.SetFloat("_Sat", this._Saturation);
			this.material.SetFloat("_Val", this._ValueBrightness);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000D50 RID: 3408 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000D51 RID: 3409 RVA: 0x0006B866 File Offset: 0x00069A66
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001022 RID: 4130
	public Shader SCShader;

	// Token: 0x04001023 RID: 4131
	[Range(0f, 360f)]
	public float _HueShift = 180f;

	// Token: 0x04001024 RID: 4132
	[Range(-32f, 32f)]
	public float _Saturation = 1f;

	// Token: 0x04001025 RID: 4133
	[Range(-32f, 32f)]
	public float _ValueBrightness = 1f;

	// Token: 0x04001026 RID: 4134
	private Material SCMaterial;
}
