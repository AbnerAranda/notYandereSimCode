using System;
using UnityEngine;

// Token: 0x02000163 RID: 355
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Colors/Threshold")]
public class CameraFilterPack_Colors_Threshold : MonoBehaviour
{
	// Token: 0x17000282 RID: 642
	// (get) Token: 0x06000D65 RID: 3429 RVA: 0x0006BD8D File Offset: 0x00069F8D
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

	// Token: 0x06000D66 RID: 3430 RVA: 0x0006BDC1 File Offset: 0x00069FC1
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Colors_Threshold");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000D67 RID: 3431 RVA: 0x0006BDE4 File Offset: 0x00069FE4
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
			this.material.SetFloat("_Distortion", this.Threshold);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000D68 RID: 3432 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000D69 RID: 3433 RVA: 0x0006BE6A File Offset: 0x0006A06A
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x0400103D RID: 4157
	public Shader SCShader;

	// Token: 0x0400103E RID: 4158
	private float TimeX = 1f;

	// Token: 0x0400103F RID: 4159
	[Range(0f, 1f)]
	public float Threshold = 0.3f;

	// Token: 0x04001040 RID: 4160
	private Material SCMaterial;
}
