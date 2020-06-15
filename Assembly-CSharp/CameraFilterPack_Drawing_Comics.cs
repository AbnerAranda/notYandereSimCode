using System;
using UnityEngine;

// Token: 0x0200017B RID: 379
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Drawing/Comics")]
public class CameraFilterPack_Drawing_Comics : MonoBehaviour
{
	// Token: 0x1700029A RID: 666
	// (get) Token: 0x06000DF5 RID: 3573 RVA: 0x0006E039 File Offset: 0x0006C239
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

	// Token: 0x06000DF6 RID: 3574 RVA: 0x0006E06D File Offset: 0x0006C26D
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Drawing_Comics");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000DF7 RID: 3575 RVA: 0x0006E090 File Offset: 0x0006C290
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
			this.material.SetFloat("_DotSize", this.DotSize);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000DF8 RID: 3576 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000DF9 RID: 3577 RVA: 0x0006E116 File Offset: 0x0006C316
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040010C9 RID: 4297
	public Shader SCShader;

	// Token: 0x040010CA RID: 4298
	private float TimeX = 1f;

	// Token: 0x040010CB RID: 4299
	private Material SCMaterial;

	// Token: 0x040010CC RID: 4300
	[Range(0f, 1f)]
	public float DotSize = 0.5f;
}
