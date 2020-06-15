using System;
using UnityEngine;

// Token: 0x020001C5 RID: 453
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Gradients/Hue")]
public class CameraFilterPack_Gradients_Hue : MonoBehaviour
{
	// Token: 0x170002E4 RID: 740
	// (get) Token: 0x06000FB2 RID: 4018 RVA: 0x000755CA File Offset: 0x000737CA
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

	// Token: 0x06000FB3 RID: 4019 RVA: 0x000755FE File Offset: 0x000737FE
	private void Start()
	{
		this.SCShader = Shader.Find(this.ShaderName);
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000FB4 RID: 4020 RVA: 0x00075620 File Offset: 0x00073820
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

	// Token: 0x06000FB5 RID: 4021 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000FB6 RID: 4022 RVA: 0x000756EC File Offset: 0x000738EC
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040012B2 RID: 4786
	public Shader SCShader;

	// Token: 0x040012B3 RID: 4787
	private string ShaderName = "CameraFilterPack/Gradients_Hue";

	// Token: 0x040012B4 RID: 4788
	private float TimeX = 1f;

	// Token: 0x040012B5 RID: 4789
	private Material SCMaterial;

	// Token: 0x040012B6 RID: 4790
	[Range(0f, 1f)]
	public float Switch = 1f;

	// Token: 0x040012B7 RID: 4791
	[Range(0f, 1f)]
	public float Fade = 1f;
}
