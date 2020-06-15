using System;
using UnityEngine;

// Token: 0x02000169 RID: 361
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Distortion/Dream")]
public class CameraFilterPack_Distortion_Dream : MonoBehaviour
{
	// Token: 0x17000288 RID: 648
	// (get) Token: 0x06000D89 RID: 3465 RVA: 0x0006C5D1 File Offset: 0x0006A7D1
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

	// Token: 0x06000D8A RID: 3466 RVA: 0x0006C605 File Offset: 0x0006A805
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Distortion_Dream");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000D8B RID: 3467 RVA: 0x0006C628 File Offset: 0x0006A828
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
			this.material.SetFloat("_Distortion", this.Distortion);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000D8C RID: 3468 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000D8D RID: 3469 RVA: 0x0006C6AE File Offset: 0x0006A8AE
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x0400105F RID: 4191
	public Shader SCShader;

	// Token: 0x04001060 RID: 4192
	private float TimeX = 1f;

	// Token: 0x04001061 RID: 4193
	[Range(1f, 10f)]
	public float Distortion = 1f;

	// Token: 0x04001062 RID: 4194
	private Material SCMaterial;
}
