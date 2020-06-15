using System;
using UnityEngine;

// Token: 0x0200012D RID: 301
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Blend 2 Camera/Multiply")]
public class CameraFilterPack_Blend2Camera_Multiply : MonoBehaviour
{
	// Token: 0x1700024C RID: 588
	// (get) Token: 0x06000C08 RID: 3080 RVA: 0x0006596D File Offset: 0x00063B6D
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

	// Token: 0x06000C09 RID: 3081 RVA: 0x000659A4 File Offset: 0x00063BA4
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

	// Token: 0x06000C0A RID: 3082 RVA: 0x00065A08 File Offset: 0x00063C08
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

	// Token: 0x06000C0B RID: 3083 RVA: 0x00065AF8 File Offset: 0x00063CF8
	private void OnValidate()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000C0C RID: 3084 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000C0D RID: 3085 RVA: 0x00065AF8 File Offset: 0x00063CF8
	private void OnEnable()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000C0E RID: 3086 RVA: 0x00065B30 File Offset: 0x00063D30
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

	// Token: 0x04000ECA RID: 3786
	private string ShaderName = "CameraFilterPack/Blend2Camera_Multiply";

	// Token: 0x04000ECB RID: 3787
	public Shader SCShader;

	// Token: 0x04000ECC RID: 3788
	public Camera Camera2;

	// Token: 0x04000ECD RID: 3789
	private float TimeX = 1f;

	// Token: 0x04000ECE RID: 3790
	private Material SCMaterial;

	// Token: 0x04000ECF RID: 3791
	[Range(0f, 1f)]
	public float SwitchCameraToCamera2;

	// Token: 0x04000ED0 RID: 3792
	[Range(0f, 1f)]
	public float BlendFX = 0.5f;

	// Token: 0x04000ED1 RID: 3793
	private RenderTexture Camera2tex;
}
