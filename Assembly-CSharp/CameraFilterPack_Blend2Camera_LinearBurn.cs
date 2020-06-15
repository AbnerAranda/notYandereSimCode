using System;
using UnityEngine;

// Token: 0x02000129 RID: 297
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Blend 2 Camera/LinearBurn")]
public class CameraFilterPack_Blend2Camera_LinearBurn : MonoBehaviour
{
	// Token: 0x17000248 RID: 584
	// (get) Token: 0x06000BE8 RID: 3048 RVA: 0x000650ED File Offset: 0x000632ED
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

	// Token: 0x06000BE9 RID: 3049 RVA: 0x00065124 File Offset: 0x00063324
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

	// Token: 0x06000BEA RID: 3050 RVA: 0x00065188 File Offset: 0x00063388
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

	// Token: 0x06000BEB RID: 3051 RVA: 0x00065278 File Offset: 0x00063478
	private void OnValidate()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000BEC RID: 3052 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000BED RID: 3053 RVA: 0x00065278 File Offset: 0x00063478
	private void OnEnable()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000BEE RID: 3054 RVA: 0x000652B0 File Offset: 0x000634B0
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

	// Token: 0x04000EAA RID: 3754
	private string ShaderName = "CameraFilterPack/Blend2Camera_LinearBurn";

	// Token: 0x04000EAB RID: 3755
	public Shader SCShader;

	// Token: 0x04000EAC RID: 3756
	public Camera Camera2;

	// Token: 0x04000EAD RID: 3757
	private float TimeX = 1f;

	// Token: 0x04000EAE RID: 3758
	private Material SCMaterial;

	// Token: 0x04000EAF RID: 3759
	[Range(0f, 1f)]
	public float SwitchCameraToCamera2;

	// Token: 0x04000EB0 RID: 3760
	[Range(0f, 1f)]
	public float BlendFX = 0.5f;

	// Token: 0x04000EB1 RID: 3761
	private RenderTexture Camera2tex;
}
