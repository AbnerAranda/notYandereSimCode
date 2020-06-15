using System;
using UnityEngine;

// Token: 0x02000222 RID: 546
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Vision/Tunnel")]
public class CameraFilterPack_Vision_Tunnel : MonoBehaviour
{
	// Token: 0x17000341 RID: 833
	// (get) Token: 0x06001203 RID: 4611 RVA: 0x0007FFA4 File Offset: 0x0007E1A4
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

	// Token: 0x06001204 RID: 4612 RVA: 0x0007FFD8 File Offset: 0x0007E1D8
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Vision_Tunnel");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06001205 RID: 4613 RVA: 0x0007FFFC File Offset: 0x0007E1FC
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
			this.material.SetFloat("_Value", this.Value);
			this.material.SetFloat("_Value2", this.Value2);
			this.material.SetFloat("_Value3", this.Intensity);
			this.material.SetFloat("_Value4", this.Value4);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06001206 RID: 4614 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06001207 RID: 4615 RVA: 0x000800F4 File Offset: 0x0007E2F4
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001534 RID: 5428
	public Shader SCShader;

	// Token: 0x04001535 RID: 5429
	private float TimeX = 1f;

	// Token: 0x04001536 RID: 5430
	private Material SCMaterial;

	// Token: 0x04001537 RID: 5431
	[Range(0f, 1f)]
	public float Value = 0.6f;

	// Token: 0x04001538 RID: 5432
	[Range(0f, 1f)]
	public float Value2 = 0.4f;

	// Token: 0x04001539 RID: 5433
	[Range(0f, 1f)]
	public float Intensity = 1f;

	// Token: 0x0400153A RID: 5434
	[Range(0f, 10f)]
	private float Value4 = 1f;
}
