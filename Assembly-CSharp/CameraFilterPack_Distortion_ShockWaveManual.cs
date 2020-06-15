using System;
using UnityEngine;

// Token: 0x02000173 RID: 371
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Distortion/ShockWave Manual")]
public class CameraFilterPack_Distortion_ShockWaveManual : MonoBehaviour
{
	// Token: 0x17000292 RID: 658
	// (get) Token: 0x06000DC5 RID: 3525 RVA: 0x0006D329 File Offset: 0x0006B529
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

	// Token: 0x06000DC6 RID: 3526 RVA: 0x0006D35D File Offset: 0x0006B55D
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Distortion_ShockWaveManual");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000DC7 RID: 3527 RVA: 0x0006D380 File Offset: 0x0006B580
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
			this.material.SetFloat("_Value", this.PosX);
			this.material.SetFloat("_Value2", this.PosY);
			this.material.SetFloat("_Value3", this.Value);
			this.material.SetFloat("_Value4", this.Size);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000DC8 RID: 3528 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000DC9 RID: 3529 RVA: 0x0006D478 File Offset: 0x0006B678
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001091 RID: 4241
	public Shader SCShader;

	// Token: 0x04001092 RID: 4242
	private float TimeX = 1f;

	// Token: 0x04001093 RID: 4243
	private Material SCMaterial;

	// Token: 0x04001094 RID: 4244
	[Range(-1.5f, 1.5f)]
	public float PosX = 0.5f;

	// Token: 0x04001095 RID: 4245
	[Range(-1.5f, 1.5f)]
	public float PosY = 0.5f;

	// Token: 0x04001096 RID: 4246
	[Range(-0.1f, 2f)]
	public float Value = 0.5f;

	// Token: 0x04001097 RID: 4247
	[Range(0f, 10f)]
	public float Size = 1f;
}
