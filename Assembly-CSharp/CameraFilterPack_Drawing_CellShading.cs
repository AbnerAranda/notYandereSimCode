using System;
using UnityEngine;

// Token: 0x02000179 RID: 377
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Drawing/CellShading")]
public class CameraFilterPack_Drawing_CellShading : MonoBehaviour
{
	// Token: 0x17000298 RID: 664
	// (get) Token: 0x06000DE9 RID: 3561 RVA: 0x0006DD59 File Offset: 0x0006BF59
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

	// Token: 0x06000DEA RID: 3562 RVA: 0x0006DD8D File Offset: 0x0006BF8D
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Drawing_CellShading");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000DEB RID: 3563 RVA: 0x0006DDB0 File Offset: 0x0006BFB0
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
			this.material.SetFloat("_EdgeSize", this.EdgeSize);
			this.material.SetFloat("_ColorLevel", this.ColorLevel);
			this.material.SetVector("_ScreenResolution", new Vector2((float)Screen.width, (float)Screen.height));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000DEC RID: 3564 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000DED RID: 3565 RVA: 0x0006DE75 File Offset: 0x0006C075
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040010BE RID: 4286
	public Shader SCShader;

	// Token: 0x040010BF RID: 4287
	private float TimeX = 1f;

	// Token: 0x040010C0 RID: 4288
	private Material SCMaterial;

	// Token: 0x040010C1 RID: 4289
	[Range(0f, 1f)]
	public float EdgeSize = 0.1f;

	// Token: 0x040010C2 RID: 4290
	[Range(0f, 10f)]
	public float ColorLevel = 4f;
}
