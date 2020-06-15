using System;
using UnityEngine;

// Token: 0x0200010A RID: 266
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/AAA/Blood On Screen")]
public class CameraFilterPack_AAA_BloodOnScreen : MonoBehaviour
{
	// Token: 0x17000229 RID: 553
	// (get) Token: 0x06000B0E RID: 2830 RVA: 0x00060C36 File Offset: 0x0005EE36
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

	// Token: 0x06000B0F RID: 2831 RVA: 0x00060C6A File Offset: 0x0005EE6A
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_AAA_BloodOnScreen1") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/AAA_BloodOnScreen");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000B10 RID: 2832 RVA: 0x00060CA0 File Offset: 0x0005EEA0
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
			this.material.SetFloat("_Value", Mathf.Clamp(this.Blood_On_Screen, 0.02f, 1.6f));
			this.material.SetFloat("_Value2", Mathf.Clamp(this.Blood_Intensify, 0f, 2f));
			this.material.SetFloat("_Value3", Mathf.Clamp(this.Blood_Darkness, 0f, 2f));
			this.material.SetFloat("_Value4", Mathf.Clamp(this.Blood_Fade, 0f, 1f));
			this.material.SetFloat("_Value5", Mathf.Clamp(this.Blood_Distortion_Speed, 0f, 2f));
			this.material.SetColor("_Color2", this.Blood_Color);
			this.material.SetTexture("_MainTex2", this.Texture2);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000B11 RID: 2833 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000B12 RID: 2834 RVA: 0x00060DF8 File Offset: 0x0005EFF8
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000D78 RID: 3448
	public Shader SCShader;

	// Token: 0x04000D79 RID: 3449
	private float TimeX = 1f;

	// Token: 0x04000D7A RID: 3450
	[Range(0.02f, 1.6f)]
	public float Blood_On_Screen = 1f;

	// Token: 0x04000D7B RID: 3451
	public Color Blood_Color = Color.red;

	// Token: 0x04000D7C RID: 3452
	[Range(0f, 2f)]
	public float Blood_Intensify = 0.7f;

	// Token: 0x04000D7D RID: 3453
	[Range(0f, 2f)]
	public float Blood_Darkness = 0.5f;

	// Token: 0x04000D7E RID: 3454
	[Range(0f, 1f)]
	public float Blood_Distortion_Speed = 0.25f;

	// Token: 0x04000D7F RID: 3455
	[Range(0f, 1f)]
	public float Blood_Fade = 1f;

	// Token: 0x04000D80 RID: 3456
	private Material SCMaterial;

	// Token: 0x04000D81 RID: 3457
	private Texture2D Texture2;
}
