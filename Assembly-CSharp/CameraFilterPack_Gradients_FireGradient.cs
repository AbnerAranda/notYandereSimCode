using System;
using UnityEngine;

// Token: 0x020001C4 RID: 452
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Gradients/Fire")]
public class CameraFilterPack_Gradients_FireGradient : MonoBehaviour
{
	// Token: 0x170002E3 RID: 739
	// (get) Token: 0x06000FAC RID: 4012 RVA: 0x0007545A File Offset: 0x0007365A
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

	// Token: 0x06000FAD RID: 4013 RVA: 0x0007548E File Offset: 0x0007368E
	private void Start()
	{
		this.SCShader = Shader.Find(this.ShaderName);
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000FAE RID: 4014 RVA: 0x000754B0 File Offset: 0x000736B0
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

	// Token: 0x06000FAF RID: 4015 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000FB0 RID: 4016 RVA: 0x0007557C File Offset: 0x0007377C
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040012AC RID: 4780
	public Shader SCShader;

	// Token: 0x040012AD RID: 4781
	private string ShaderName = "CameraFilterPack/Gradients_FireGradient";

	// Token: 0x040012AE RID: 4782
	private float TimeX = 1f;

	// Token: 0x040012AF RID: 4783
	private Material SCMaterial;

	// Token: 0x040012B0 RID: 4784
	[Range(0f, 1f)]
	public float Switch = 1f;

	// Token: 0x040012B1 RID: 4785
	[Range(0f, 1f)]
	public float Fade = 1f;
}
