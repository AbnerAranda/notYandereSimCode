using System;
using UnityEngine;

// Token: 0x0200012B RID: 299
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Blend 2 Camera/LinearLight")]
public class CameraFilterPack_Blend2Camera_LinearLight : MonoBehaviour
{
	// Token: 0x1700024A RID: 586
	// (get) Token: 0x06000BF8 RID: 3064 RVA: 0x0006552D File Offset: 0x0006372D
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

	// Token: 0x06000BF9 RID: 3065 RVA: 0x00065564 File Offset: 0x00063764
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

	// Token: 0x06000BFA RID: 3066 RVA: 0x000655C8 File Offset: 0x000637C8
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

	// Token: 0x06000BFB RID: 3067 RVA: 0x000656B8 File Offset: 0x000638B8
	private void OnValidate()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000BFC RID: 3068 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000BFD RID: 3069 RVA: 0x000656B8 File Offset: 0x000638B8
	private void OnEnable()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000BFE RID: 3070 RVA: 0x000656F0 File Offset: 0x000638F0
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

	// Token: 0x04000EBA RID: 3770
	private string ShaderName = "CameraFilterPack/Blend2Camera_LinearLight";

	// Token: 0x04000EBB RID: 3771
	public Shader SCShader;

	// Token: 0x04000EBC RID: 3772
	public Camera Camera2;

	// Token: 0x04000EBD RID: 3773
	private float TimeX = 1f;

	// Token: 0x04000EBE RID: 3774
	private Material SCMaterial;

	// Token: 0x04000EBF RID: 3775
	[Range(0f, 1f)]
	public float SwitchCameraToCamera2;

	// Token: 0x04000EC0 RID: 3776
	[Range(0f, 1f)]
	public float BlendFX = 0.5f;

	// Token: 0x04000EC1 RID: 3777
	private RenderTexture Camera2tex;
}
