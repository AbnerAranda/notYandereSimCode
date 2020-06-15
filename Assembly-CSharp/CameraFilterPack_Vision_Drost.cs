using System;
using UnityEngine;

// Token: 0x0200021C RID: 540
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Vision/Drost")]
public class CameraFilterPack_Vision_Drost : MonoBehaviour
{
	// Token: 0x1700033B RID: 827
	// (get) Token: 0x060011DF RID: 4575 RVA: 0x0007F3C9 File Offset: 0x0007D5C9
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

	// Token: 0x060011E0 RID: 4576 RVA: 0x0007F3FD File Offset: 0x0007D5FD
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Vision_Drost");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x060011E1 RID: 4577 RVA: 0x0007F420 File Offset: 0x0007D620
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
			this.material.SetFloat("_Value", this.Intensity);
			this.material.SetFloat("_Value2", this.Speed);
			this.material.SetFloat("_Value3", this.Value3);
			this.material.SetFloat("_Value4", this.Value4);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x060011E2 RID: 4578 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x060011E3 RID: 4579 RVA: 0x0007F518 File Offset: 0x0007D718
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040014FD RID: 5373
	public Shader SCShader;

	// Token: 0x040014FE RID: 5374
	private float TimeX = 1f;

	// Token: 0x040014FF RID: 5375
	private Material SCMaterial;

	// Token: 0x04001500 RID: 5376
	[Range(0f, 0.4f)]
	public float Intensity = 0.4f;

	// Token: 0x04001501 RID: 5377
	[Range(0f, 10f)]
	public float Speed = 1f;

	// Token: 0x04001502 RID: 5378
	[Range(0f, 10f)]
	private float Value3 = 1f;

	// Token: 0x04001503 RID: 5379
	[Range(0f, 10f)]
	private float Value4 = 1f;
}
