using System;
using UnityEngine;

// Token: 0x02000111 RID: 273
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Alien/Vision")]
public class CameraFilterPack_Alien_Vision : MonoBehaviour
{
	// Token: 0x17000230 RID: 560
	// (get) Token: 0x06000B38 RID: 2872 RVA: 0x00061CD6 File Offset: 0x0005FED6
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

	// Token: 0x06000B39 RID: 2873 RVA: 0x00061D0A File Offset: 0x0005FF0A
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Alien_Vision");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000B3A RID: 2874 RVA: 0x00061D2C File Offset: 0x0005FF2C
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
			this.material.SetFloat("_Value", this.Therma_Variation);
			this.material.SetFloat("_Value2", this.Speed);
			this.material.SetFloat("_Value3", this.Burn);
			this.material.SetFloat("_Value4", this.SceneCut);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000B3B RID: 2875 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000B3C RID: 2876 RVA: 0x00061E24 File Offset: 0x00060024
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000DCA RID: 3530
	public Shader SCShader;

	// Token: 0x04000DCB RID: 3531
	private float TimeX = 1f;

	// Token: 0x04000DCC RID: 3532
	private Material SCMaterial;

	// Token: 0x04000DCD RID: 3533
	[Range(0f, 0.5f)]
	public float Therma_Variation = 0.5f;

	// Token: 0x04000DCE RID: 3534
	[Range(0f, 1f)]
	public float Speed = 0.5f;

	// Token: 0x04000DCF RID: 3535
	[Range(0f, 4f)]
	private float Burn;

	// Token: 0x04000DD0 RID: 3536
	[Range(0f, 16f)]
	private float SceneCut = 1f;
}
