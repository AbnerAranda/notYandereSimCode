using System;
using UnityEngine;

// Token: 0x020001C6 RID: 454
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Gradients/Neon")]
public class CameraFilterPack_Gradients_NeonGradient : MonoBehaviour
{
	// Token: 0x170002E5 RID: 741
	// (get) Token: 0x06000FB8 RID: 4024 RVA: 0x0007573A File Offset: 0x0007393A
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

	// Token: 0x06000FB9 RID: 4025 RVA: 0x0007576E File Offset: 0x0007396E
	private void Start()
	{
		this.SCShader = Shader.Find(this.ShaderName);
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000FBA RID: 4026 RVA: 0x00075790 File Offset: 0x00073990
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
			this.material.SetFloat("_Value", this.Switch);
			this.material.SetFloat("_Value2", this.Fade);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000FBB RID: 4027 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000FBC RID: 4028 RVA: 0x0007585C File Offset: 0x00073A5C
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040012B8 RID: 4792
	public Shader SCShader;

	// Token: 0x040012B9 RID: 4793
	private string ShaderName = "CameraFilterPack/Gradients_NeonGradient";

	// Token: 0x040012BA RID: 4794
	private float TimeX = 1f;

	// Token: 0x040012BB RID: 4795
	private Material SCMaterial;

	// Token: 0x040012BC RID: 4796
	[Range(0f, 1f)]
	public float Switch = 1f;

	// Token: 0x040012BD RID: 4797
	[Range(0f, 1f)]
	public float Fade = 1f;
}
