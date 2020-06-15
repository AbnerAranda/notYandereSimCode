using System;
using UnityEngine;

// Token: 0x02000133 RID: 307
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Blend 2 Camera/SoftLight")]
public class CameraFilterPack_Blend2Camera_SoftLight : MonoBehaviour
{
	// Token: 0x17000252 RID: 594
	// (get) Token: 0x06000C39 RID: 3129 RVA: 0x000668F9 File Offset: 0x00064AF9
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

	// Token: 0x06000C3A RID: 3130 RVA: 0x00066930 File Offset: 0x00064B30
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

	// Token: 0x06000C3B RID: 3131 RVA: 0x00066994 File Offset: 0x00064B94
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

	// Token: 0x06000C3C RID: 3132 RVA: 0x00066A84 File Offset: 0x00064C84
	private void OnValidate()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000C3D RID: 3133 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000C3E RID: 3134 RVA: 0x00066A84 File Offset: 0x00064C84
	private void OnEnable()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000C3F RID: 3135 RVA: 0x00066ABC File Offset: 0x00064CBC
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

	// Token: 0x04000EFC RID: 3836
	private string ShaderName = "CameraFilterPack/Blend2Camera_SoftLight";

	// Token: 0x04000EFD RID: 3837
	public Shader SCShader;

	// Token: 0x04000EFE RID: 3838
	public Camera Camera2;

	// Token: 0x04000EFF RID: 3839
	private float TimeX = 1f;

	// Token: 0x04000F00 RID: 3840
	private Material SCMaterial;

	// Token: 0x04000F01 RID: 3841
	[Range(0f, 1f)]
	public float SwitchCameraToCamera2;

	// Token: 0x04000F02 RID: 3842
	[Range(0f, 1f)]
	public float BlendFX = 0.5f;

	// Token: 0x04000F03 RID: 3843
	private RenderTexture Camera2tex;
}
