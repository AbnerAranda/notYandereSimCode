using System;
using UnityEngine;

// Token: 0x020001A2 RID: 418
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/FX/Drunk2")]
public class CameraFilterPack_FX_Drunk2 : MonoBehaviour
{
	// Token: 0x170002C1 RID: 705
	// (get) Token: 0x06000EE0 RID: 3808 RVA: 0x00071AFD File Offset: 0x0006FCFD
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

	// Token: 0x06000EE1 RID: 3809 RVA: 0x00071B31 File Offset: 0x0006FD31
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/FX_Drunk2");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000EE2 RID: 3810 RVA: 0x00071B54 File Offset: 0x0006FD54
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
			this.material.SetFloat("_Value3", this.Value3);
			this.material.SetFloat("_Value4", this.Value4);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000EE3 RID: 3811 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000EE4 RID: 3812 RVA: 0x00071C4C File Offset: 0x0006FE4C
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040011C1 RID: 4545
	public Shader SCShader;

	// Token: 0x040011C2 RID: 4546
	private float TimeX = 1f;

	// Token: 0x040011C3 RID: 4547
	private Material SCMaterial;

	// Token: 0x040011C4 RID: 4548
	[Range(0f, 10f)]
	private float Value = 1f;

	// Token: 0x040011C5 RID: 4549
	[Range(0f, 10f)]
	private float Value2 = 1f;

	// Token: 0x040011C6 RID: 4550
	[Range(0f, 10f)]
	private float Value3 = 1f;

	// Token: 0x040011C7 RID: 4551
	[Range(0f, 10f)]
	private float Value4 = 1f;
}
