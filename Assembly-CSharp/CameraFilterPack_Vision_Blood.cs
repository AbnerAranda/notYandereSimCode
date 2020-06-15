using System;
using UnityEngine;

// Token: 0x02000219 RID: 537
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Vision/Blood")]
public class CameraFilterPack_Vision_Blood : MonoBehaviour
{
	// Token: 0x17000338 RID: 824
	// (get) Token: 0x060011CD RID: 4557 RVA: 0x0007EED0 File Offset: 0x0007D0D0
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

	// Token: 0x060011CE RID: 4558 RVA: 0x0007EF04 File Offset: 0x0007D104
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Vision_Blood");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x060011CF RID: 4559 RVA: 0x0007EF28 File Offset: 0x0007D128
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
			this.material.SetFloat("_Value", this.HoleSize);
			this.material.SetFloat("_Value2", this.HoleSmooth);
			this.material.SetFloat("_Value3", this.Color1);
			this.material.SetFloat("_Value4", this.Color2);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x060011D0 RID: 4560 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x060011D1 RID: 4561 RVA: 0x0007F020 File Offset: 0x0007D220
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040014E8 RID: 5352
	public Shader SCShader;

	// Token: 0x040014E9 RID: 5353
	private float TimeX = 1f;

	// Token: 0x040014EA RID: 5354
	private Material SCMaterial;

	// Token: 0x040014EB RID: 5355
	[Range(0.01f, 1f)]
	public float HoleSize = 0.6f;

	// Token: 0x040014EC RID: 5356
	[Range(-1f, 1f)]
	public float HoleSmooth = 0.3f;

	// Token: 0x040014ED RID: 5357
	[Range(-2f, 2f)]
	public float Color1 = 0.2f;

	// Token: 0x040014EE RID: 5358
	[Range(-2f, 2f)]
	public float Color2 = 0.9f;
}
