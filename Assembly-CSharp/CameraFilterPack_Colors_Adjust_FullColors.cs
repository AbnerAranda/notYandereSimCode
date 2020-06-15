using System;
using UnityEngine;

// Token: 0x0200015A RID: 346
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/ColorsAdjust/FullColors")]
public class CameraFilterPack_Colors_Adjust_FullColors : MonoBehaviour
{
	// Token: 0x17000279 RID: 633
	// (get) Token: 0x06000D2D RID: 3373 RVA: 0x0006AA59 File Offset: 0x00068C59
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

	// Token: 0x06000D2E RID: 3374 RVA: 0x0006AA8D File Offset: 0x00068C8D
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Colors_Adjust_FullColors");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000D2F RID: 3375 RVA: 0x0006AAB0 File Offset: 0x00068CB0
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
			this.material.SetFloat("_Red_R", this.Red_R / 100f);
			this.material.SetFloat("_Red_G", this.Red_G / 100f);
			this.material.SetFloat("_Red_B", this.Red_B / 100f);
			this.material.SetFloat("_Green_R", this.Green_R / 100f);
			this.material.SetFloat("_Green_G", this.Green_G / 100f);
			this.material.SetFloat("_Green_B", this.Green_B / 100f);
			this.material.SetFloat("_Blue_R", this.Blue_R / 100f);
			this.material.SetFloat("_Blue_G", this.Blue_G / 100f);
			this.material.SetFloat("_Blue_B", this.Blue_B / 100f);
			this.material.SetFloat("_Red_C", this.Red_Constant / 100f);
			this.material.SetFloat("_Green_C", this.Green_Constant / 100f);
			this.material.SetFloat("_Blue_C", this.Blue_Constant / 100f);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000D30 RID: 3376 RVA: 0x0006ACA0 File Offset: 0x00068EA0
	private void Update()
	{
		bool isPlaying = Application.isPlaying;
	}

	// Token: 0x06000D31 RID: 3377 RVA: 0x0006ACA8 File Offset: 0x00068EA8
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000FFE RID: 4094
	public Shader SCShader;

	// Token: 0x04000FFF RID: 4095
	private float TimeX = 1f;

	// Token: 0x04001000 RID: 4096
	private Material SCMaterial;

	// Token: 0x04001001 RID: 4097
	[Range(-200f, 200f)]
	public float Red_R = 100f;

	// Token: 0x04001002 RID: 4098
	[Range(-200f, 200f)]
	public float Red_G;

	// Token: 0x04001003 RID: 4099
	[Range(-200f, 200f)]
	public float Red_B;

	// Token: 0x04001004 RID: 4100
	[Range(-200f, 200f)]
	public float Red_Constant;

	// Token: 0x04001005 RID: 4101
	[Range(-200f, 200f)]
	public float Green_R;

	// Token: 0x04001006 RID: 4102
	[Range(-200f, 200f)]
	public float Green_G = 100f;

	// Token: 0x04001007 RID: 4103
	[Range(-200f, 200f)]
	public float Green_B;

	// Token: 0x04001008 RID: 4104
	[Range(-200f, 200f)]
	public float Green_Constant;

	// Token: 0x04001009 RID: 4105
	[Range(-200f, 200f)]
	public float Blue_R;

	// Token: 0x0400100A RID: 4106
	[Range(-200f, 200f)]
	public float Blue_G;

	// Token: 0x0400100B RID: 4107
	[Range(-200f, 200f)]
	public float Blue_B = 100f;

	// Token: 0x0400100C RID: 4108
	[Range(-200f, 200f)]
	public float Blue_Constant;
}
