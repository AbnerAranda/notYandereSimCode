using System;
using UnityEngine;

// Token: 0x02000180 RID: 384
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Drawing/Laplacian")]
public class CameraFilterPack_Drawing_Laplacian : MonoBehaviour
{
	// Token: 0x1700029F RID: 671
	// (get) Token: 0x06000E13 RID: 3603 RVA: 0x0006E727 File Offset: 0x0006C927
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

	// Token: 0x06000E14 RID: 3604 RVA: 0x0006E75B File Offset: 0x0006C95B
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Drawing_Laplacian");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000E15 RID: 3605 RVA: 0x0006E77C File Offset: 0x0006C97C
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
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000E16 RID: 3606 RVA: 0x0006ACA0 File Offset: 0x00068EA0
	private void Update()
	{
		bool isPlaying = Application.isPlaying;
	}

	// Token: 0x06000E17 RID: 3607 RVA: 0x0006E819 File Offset: 0x0006CA19
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040010E5 RID: 4325
	public Shader SCShader;

	// Token: 0x040010E6 RID: 4326
	private float TimeX = 1f;

	// Token: 0x040010E7 RID: 4327
	private Material SCMaterial;
}
