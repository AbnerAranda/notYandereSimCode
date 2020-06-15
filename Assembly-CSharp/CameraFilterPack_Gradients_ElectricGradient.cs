using System;
using UnityEngine;

// Token: 0x020001C3 RID: 451
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Gradients/Electric")]
public class CameraFilterPack_Gradients_ElectricGradient : MonoBehaviour
{
	// Token: 0x170002E2 RID: 738
	// (get) Token: 0x06000FA6 RID: 4006 RVA: 0x000752EA File Offset: 0x000734EA
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

	// Token: 0x06000FA7 RID: 4007 RVA: 0x0007531E File Offset: 0x0007351E
	private void Start()
	{
		this.SCShader = Shader.Find(this.ShaderName);
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000FA8 RID: 4008 RVA: 0x00075340 File Offset: 0x00073540
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

	// Token: 0x06000FA9 RID: 4009 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000FAA RID: 4010 RVA: 0x0007540C File Offset: 0x0007360C
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040012A6 RID: 4774
	public Shader SCShader;

	// Token: 0x040012A7 RID: 4775
	private string ShaderName = "CameraFilterPack/Gradients_ElectricGradient";

	// Token: 0x040012A8 RID: 4776
	private float TimeX = 1f;

	// Token: 0x040012A9 RID: 4777
	private Material SCMaterial;

	// Token: 0x040012AA RID: 4778
	[Range(0f, 1f)]
	public float Switch = 1f;

	// Token: 0x040012AB RID: 4779
	[Range(0f, 1f)]
	public float Fade = 1f;
}
