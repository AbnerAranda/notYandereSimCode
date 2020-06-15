using System;
using UnityEngine;

// Token: 0x020001B0 RID: 432
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/FX/Scan")]
public class CameraFilterPack_FX_Scan : MonoBehaviour
{
	// Token: 0x170002CF RID: 719
	// (get) Token: 0x06000F34 RID: 3892 RVA: 0x00072D06 File Offset: 0x00070F06
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

	// Token: 0x06000F35 RID: 3893 RVA: 0x00072D3A File Offset: 0x00070F3A
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/FX_Scan");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000F36 RID: 3894 RVA: 0x00072D5C File Offset: 0x00070F5C
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
			this.material.SetFloat("_Value2", this.Speed);
			this.material.SetFloat("_Value3", this.Value3);
			this.material.SetFloat("_Value4", this.Value4);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000F37 RID: 3895 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000F38 RID: 3896 RVA: 0x00072E54 File Offset: 0x00071054
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001200 RID: 4608
	public Shader SCShader;

	// Token: 0x04001201 RID: 4609
	private float TimeX = 1f;

	// Token: 0x04001202 RID: 4610
	private Material SCMaterial;

	// Token: 0x04001203 RID: 4611
	[Range(0.001f, 0.1f)]
	public float Size = 0.025f;

	// Token: 0x04001204 RID: 4612
	[Range(0f, 10f)]
	public float Speed = 1f;

	// Token: 0x04001205 RID: 4613
	[Range(0f, 10f)]
	private float Value3 = 1f;

	// Token: 0x04001206 RID: 4614
	[Range(0f, 10f)]
	private float Value4 = 1f;
}
