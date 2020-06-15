using System;
using UnityEngine;

// Token: 0x020001CE RID: 462
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Light/Water2")]
public class CameraFilterPack_Light_Water2 : MonoBehaviour
{
	// Token: 0x170002ED RID: 749
	// (get) Token: 0x06000FE8 RID: 4072 RVA: 0x0007628A File Offset: 0x0007448A
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

	// Token: 0x06000FE9 RID: 4073 RVA: 0x000762BE File Offset: 0x000744BE
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Light_Water2");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000FEA RID: 4074 RVA: 0x000762E0 File Offset: 0x000744E0
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
			this.material.SetFloat("_Value", this.Speed);
			this.material.SetFloat("_Value2", this.Speed_X);
			this.material.SetFloat("_Value3", this.Speed_Y);
			this.material.SetFloat("_Value4", this.Intensity);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000FEB RID: 4075 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000FEC RID: 4076 RVA: 0x000763D8 File Offset: 0x000745D8
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040012E5 RID: 4837
	public Shader SCShader;

	// Token: 0x040012E6 RID: 4838
	private float TimeX = 1f;

	// Token: 0x040012E7 RID: 4839
	private Material SCMaterial;

	// Token: 0x040012E8 RID: 4840
	[Range(0f, 10f)]
	public float Speed = 0.2f;

	// Token: 0x040012E9 RID: 4841
	[Range(0f, 10f)]
	public float Speed_X = 0.2f;

	// Token: 0x040012EA RID: 4842
	[Range(0f, 1f)]
	public float Speed_Y = 0.3f;

	// Token: 0x040012EB RID: 4843
	[Range(0f, 10f)]
	public float Intensity = 2.4f;
}
