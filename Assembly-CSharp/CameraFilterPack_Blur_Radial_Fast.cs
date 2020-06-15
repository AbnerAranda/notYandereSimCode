using System;
using UnityEngine;

// Token: 0x02000143 RID: 323
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Blur/Radial_Fast")]
public class CameraFilterPack_Blur_Radial_Fast : MonoBehaviour
{
	// Token: 0x17000262 RID: 610
	// (get) Token: 0x06000CA3 RID: 3235 RVA: 0x00068611 File Offset: 0x00066811
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

	// Token: 0x06000CA4 RID: 3236 RVA: 0x00068645 File Offset: 0x00066845
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Blur_Radial_Fast");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000CA5 RID: 3237 RVA: 0x00068668 File Offset: 0x00066868
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
			this.material.SetFloat("_Value2", this.MovX);
			this.material.SetFloat("_Value3", this.MovY);
			this.material.SetFloat("_Value4", this.blurWidth);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000CA6 RID: 3238 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000CA7 RID: 3239 RVA: 0x00068760 File Offset: 0x00066960
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000F75 RID: 3957
	public Shader SCShader;

	// Token: 0x04000F76 RID: 3958
	private float TimeX = 1f;

	// Token: 0x04000F77 RID: 3959
	private Material SCMaterial;

	// Token: 0x04000F78 RID: 3960
	[Range(-0.5f, 0.5f)]
	public float Intensity = 0.125f;

	// Token: 0x04000F79 RID: 3961
	[Range(-2f, 2f)]
	public float MovX = 0.5f;

	// Token: 0x04000F7A RID: 3962
	[Range(-2f, 2f)]
	public float MovY = 0.5f;

	// Token: 0x04000F7B RID: 3963
	[Range(0f, 10f)]
	private float blurWidth = 1f;
}
