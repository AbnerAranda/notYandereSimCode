using System;
using UnityEngine;

// Token: 0x02000212 RID: 530
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/TV/WideScreenCircle")]
public class CameraFilterPack_TV_WideScreenCircle : MonoBehaviour
{
	// Token: 0x17000331 RID: 817
	// (get) Token: 0x060011A3 RID: 4515 RVA: 0x0007E326 File Offset: 0x0007C526
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

	// Token: 0x060011A4 RID: 4516 RVA: 0x0007E35A File Offset: 0x0007C55A
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/TV_WideScreenCircle");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x060011A5 RID: 4517 RVA: 0x0007E37C File Offset: 0x0007C57C
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
			this.material.SetFloat("_Value2", this.Smooth);
			this.material.SetFloat("_Value3", this.StretchX);
			this.material.SetFloat("_Value4", this.StretchY);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x060011A6 RID: 4518 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x060011A7 RID: 4519 RVA: 0x0007E474 File Offset: 0x0007C674
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040014B8 RID: 5304
	public Shader SCShader;

	// Token: 0x040014B9 RID: 5305
	private float TimeX = 1f;

	// Token: 0x040014BA RID: 5306
	private Material SCMaterial;

	// Token: 0x040014BB RID: 5307
	[Range(0f, 0.8f)]
	public float Size = 0.55f;

	// Token: 0x040014BC RID: 5308
	[Range(0.01f, 0.4f)]
	public float Smooth = 0.01f;

	// Token: 0x040014BD RID: 5309
	[Range(0f, 10f)]
	private float StretchX = 1f;

	// Token: 0x040014BE RID: 5310
	[Range(0f, 10f)]
	private float StretchY = 1f;
}
