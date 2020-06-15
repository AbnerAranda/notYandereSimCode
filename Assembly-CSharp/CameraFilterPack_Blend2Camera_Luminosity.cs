using System;
using UnityEngine;

// Token: 0x0200012C RID: 300
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Blend 2 Camera/Luminosity")]
public class CameraFilterPack_Blend2Camera_Luminosity : MonoBehaviour
{
	// Token: 0x1700024B RID: 587
	// (get) Token: 0x06000C00 RID: 3072 RVA: 0x0006574D File Offset: 0x0006394D
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

	// Token: 0x06000C01 RID: 3073 RVA: 0x00065784 File Offset: 0x00063984
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

	// Token: 0x06000C02 RID: 3074 RVA: 0x000657E8 File Offset: 0x000639E8
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

	// Token: 0x06000C03 RID: 3075 RVA: 0x000658D8 File Offset: 0x00063AD8
	private void OnValidate()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000C04 RID: 3076 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000C05 RID: 3077 RVA: 0x000658D8 File Offset: 0x00063AD8
	private void OnEnable()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000C06 RID: 3078 RVA: 0x00065910 File Offset: 0x00063B10
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

	// Token: 0x04000EC2 RID: 3778
	private string ShaderName = "CameraFilterPack/Blend2Camera_Luminosity";

	// Token: 0x04000EC3 RID: 3779
	public Shader SCShader;

	// Token: 0x04000EC4 RID: 3780
	public Camera Camera2;

	// Token: 0x04000EC5 RID: 3781
	private float TimeX = 1f;

	// Token: 0x04000EC6 RID: 3782
	private Material SCMaterial;

	// Token: 0x04000EC7 RID: 3783
	[Range(0f, 1f)]
	public float SwitchCameraToCamera2;

	// Token: 0x04000EC8 RID: 3784
	[Range(0f, 1f)]
	public float BlendFX = 0.5f;

	// Token: 0x04000EC9 RID: 3785
	private RenderTexture Camera2tex;
}
