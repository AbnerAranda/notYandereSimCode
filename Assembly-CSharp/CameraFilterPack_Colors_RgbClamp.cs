using System;
using UnityEngine;

// Token: 0x02000162 RID: 354
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Colors/RgbClamp")]
public class CameraFilterPack_Colors_RgbClamp : MonoBehaviour
{
	// Token: 0x17000281 RID: 641
	// (get) Token: 0x06000D5F RID: 3423 RVA: 0x0006BB8D File Offset: 0x00069D8D
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

	// Token: 0x06000D60 RID: 3424 RVA: 0x0006BBC1 File Offset: 0x00069DC1
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Colors_RgbClamp");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000D61 RID: 3425 RVA: 0x0006BBE4 File Offset: 0x00069DE4
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
			this.material.SetFloat("_Value", this.Red_Start);
			this.material.SetFloat("_Value2", this.Red_End);
			this.material.SetFloat("_Value3", this.Green_Start);
			this.material.SetFloat("_Value4", this.Green_End);
			this.material.SetFloat("_Value5", this.Blue_Start);
			this.material.SetFloat("_Value6", this.Blue_End);
			this.material.SetFloat("_Value7", this.RGB_Start);
			this.material.SetFloat("_Value8", this.RGB_End);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000D62 RID: 3426 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000D63 RID: 3427 RVA: 0x0006BD34 File Offset: 0x00069F34
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001032 RID: 4146
	public Shader SCShader;

	// Token: 0x04001033 RID: 4147
	private float TimeX = 1f;

	// Token: 0x04001034 RID: 4148
	private Material SCMaterial;

	// Token: 0x04001035 RID: 4149
	[Range(0f, 1f)]
	public float Red_Start;

	// Token: 0x04001036 RID: 4150
	[Range(0f, 1f)]
	public float Red_End = 1f;

	// Token: 0x04001037 RID: 4151
	[Range(0f, 1f)]
	public float Green_Start;

	// Token: 0x04001038 RID: 4152
	[Range(0f, 1f)]
	public float Green_End = 1f;

	// Token: 0x04001039 RID: 4153
	[Range(0f, 1f)]
	public float Blue_Start;

	// Token: 0x0400103A RID: 4154
	[Range(0f, 1f)]
	public float Blue_End = 1f;

	// Token: 0x0400103B RID: 4155
	[Range(0f, 1f)]
	public float RGB_Start;

	// Token: 0x0400103C RID: 4156
	[Range(0f, 1f)]
	public float RGB_End = 1f;
}
