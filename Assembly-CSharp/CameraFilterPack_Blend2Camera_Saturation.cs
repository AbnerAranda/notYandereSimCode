using System;
using UnityEngine;

// Token: 0x02000131 RID: 305
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Blend 2 Camera/Saturation")]
public class CameraFilterPack_Blend2Camera_Saturation : MonoBehaviour
{
	// Token: 0x17000250 RID: 592
	// (get) Token: 0x06000C29 RID: 3113 RVA: 0x000664B9 File Offset: 0x000646B9
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

	// Token: 0x06000C2A RID: 3114 RVA: 0x000664F0 File Offset: 0x000646F0
	private void Start()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
		this.SCShader = Shader.Find(this.ShaderName);
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000C2B RID: 3115 RVA: 0x00066554 File Offset: 0x00064754
	private void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
	{
		if (this.SCShader != null)
		{
			this.TimeX += Time.deltaTime;
			if (this.TimeX > 100f)
			{
				this.TimeX = 0f;
			}
			if (this.Camera2 != null)
			{
				this.material.SetTexture("_MainTex2", this.Camera2tex);
			}
			this.material.SetFloat("_TimeX", this.TimeX);
			this.material.SetFloat("_Value", this.BlendFX);
			this.material.SetFloat("_Value2", this.SwitchCameraToCamera2);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000C2C RID: 3116 RVA: 0x00066644 File Offset: 0x00064844
	private void OnValidate()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000C2D RID: 3117 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000C2E RID: 3118 RVA: 0x00066644 File Offset: 0x00064844
	private void OnEnable()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000C2F RID: 3119 RVA: 0x0006667C File Offset: 0x0006487C
	private void OnDisable()
	{
		if (this.Camera2 != null)
		{
			this.Camera2.targetTexture = null;
		}
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000EEC RID: 3820
	private string ShaderName = "CameraFilterPack/Blend2Camera_Saturation";

	// Token: 0x04000EED RID: 3821
	public Shader SCShader;

	// Token: 0x04000EEE RID: 3822
	public Camera Camera2;

	// Token: 0x04000EEF RID: 3823
	private float TimeX = 1f;

	// Token: 0x04000EF0 RID: 3824
	private Material SCMaterial;

	// Token: 0x04000EF1 RID: 3825
	[Range(0f, 1f)]
	public float SwitchCameraToCamera2;

	// Token: 0x04000EF2 RID: 3826
	[Range(0f, 1f)]
	public float BlendFX = 0.5f;

	// Token: 0x04000EF3 RID: 3827
	private RenderTexture Camera2tex;
}
