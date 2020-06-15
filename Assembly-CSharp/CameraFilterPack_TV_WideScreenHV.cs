using System;
using UnityEngine;

// Token: 0x02000213 RID: 531
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/TV/WideScreenHV")]
public class CameraFilterPack_TV_WideScreenHV : MonoBehaviour
{
	// Token: 0x17000332 RID: 818
	// (get) Token: 0x060011A9 RID: 4521 RVA: 0x0007E4CD File Offset: 0x0007C6CD
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

	// Token: 0x060011AA RID: 4522 RVA: 0x0007E501 File Offset: 0x0007C701
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/TV_WideScreenHV");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x060011AB RID: 4523 RVA: 0x0007E524 File Offset: 0x0007C724
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

	// Token: 0x060011AC RID: 4524 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x060011AD RID: 4525 RVA: 0x0007E61C File Offset: 0x0007C81C
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040014BF RID: 5311
	public Shader SCShader;

	// Token: 0x040014C0 RID: 5312
	private float TimeX = 1f;

	// Token: 0x040014C1 RID: 5313
	private Material SCMaterial;

	// Token: 0x040014C2 RID: 5314
	[Range(0f, 0.8f)]
	public float Size = 0.55f;

	// Token: 0x040014C3 RID: 5315
	[Range(0.001f, 0.4f)]
	public float Smooth = 0.01f;

	// Token: 0x040014C4 RID: 5316
	[Range(0f, 10f)]
	private float StretchX = 1f;

	// Token: 0x040014C5 RID: 5317
	[Range(0f, 10f)]
	private float StretchY = 1f;
}
