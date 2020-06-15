using System;
using UnityEngine;

// Token: 0x020001A1 RID: 417
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/FX/Drunk")]
public class CameraFilterPack_FX_Drunk : MonoBehaviour
{
	// Token: 0x170002C0 RID: 704
	// (get) Token: 0x06000EDA RID: 3802 RVA: 0x000718A6 File Offset: 0x0006FAA6
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

	// Token: 0x06000EDB RID: 3803 RVA: 0x000718DA File Offset: 0x0006FADA
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/FX_Drunk");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000EDC RID: 3804 RVA: 0x000718FC File Offset: 0x0006FAFC
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
			this.material.SetFloat("_Speed", this.Speed);
			this.material.SetFloat("_Distortion", this.Distortion);
			this.material.SetFloat("_DistortionWave", this.DistortionWave);
			this.material.SetFloat("_Wavy", this.Wavy);
			this.material.SetFloat("_Fade", this.Fade);
			this.material.SetFloat("_ColoredChange", this.ColoredChange);
			this.material.SetFloat("_ChangeRed", this.ChangeRed);
			this.material.SetFloat("_ChangeGreen", this.ChangeGreen);
			this.material.SetFloat("_ChangeBlue", this.ChangeBlue);
			this.material.SetFloat("_Colored", this.ColoredSaturate);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000EDD RID: 3805 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000EDE RID: 3806 RVA: 0x00071A8E File Offset: 0x0006FC8E
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040011B3 RID: 4531
	public Shader SCShader;

	// Token: 0x040011B4 RID: 4532
	private float TimeX = 1f;

	// Token: 0x040011B5 RID: 4533
	private Material SCMaterial;

	// Token: 0x040011B6 RID: 4534
	[HideInInspector]
	[Range(0f, 20f)]
	public float Value = 6f;

	// Token: 0x040011B7 RID: 4535
	[Range(0f, 10f)]
	public float Speed = 1f;

	// Token: 0x040011B8 RID: 4536
	[Range(0f, 1f)]
	public float Wavy = 1f;

	// Token: 0x040011B9 RID: 4537
	[Range(0f, 1f)]
	public float Distortion;

	// Token: 0x040011BA RID: 4538
	[Range(0f, 1f)]
	public float DistortionWave;

	// Token: 0x040011BB RID: 4539
	[Range(0f, 1f)]
	public float Fade = 1f;

	// Token: 0x040011BC RID: 4540
	[Range(-2f, 2f)]
	public float ColoredSaturate = 1f;

	// Token: 0x040011BD RID: 4541
	[Range(-1f, 2f)]
	public float ColoredChange;

	// Token: 0x040011BE RID: 4542
	[Range(-1f, 1f)]
	public float ChangeRed;

	// Token: 0x040011BF RID: 4543
	[Range(-1f, 1f)]
	public float ChangeGreen;

	// Token: 0x040011C0 RID: 4544
	[Range(-1f, 1f)]
	public float ChangeBlue;
}
