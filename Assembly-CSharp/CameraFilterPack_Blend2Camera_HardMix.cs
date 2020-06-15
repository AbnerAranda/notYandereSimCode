using System;
using UnityEngine;

// Token: 0x02000125 RID: 293
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Blend 2 Camera/HardMix")]
public class CameraFilterPack_Blend2Camera_HardMix : MonoBehaviour
{
	// Token: 0x17000244 RID: 580
	// (get) Token: 0x06000BC8 RID: 3016 RVA: 0x0006486D File Offset: 0x00062A6D
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

	// Token: 0x06000BC9 RID: 3017 RVA: 0x000648A4 File Offset: 0x00062AA4
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

	// Token: 0x06000BCA RID: 3018 RVA: 0x00064908 File Offset: 0x00062B08
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

	// Token: 0x06000BCB RID: 3019 RVA: 0x000649F8 File Offset: 0x00062BF8
	private void OnValidate()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000BCC RID: 3020 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000BCD RID: 3021 RVA: 0x000649F8 File Offset: 0x00062BF8
	private void OnEnable()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000BCE RID: 3022 RVA: 0x00064A30 File Offset: 0x00062C30
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

	// Token: 0x04000E8A RID: 3722
	private string ShaderName = "CameraFilterPack/Blend2Camera_HardMix";

	// Token: 0x04000E8B RID: 3723
	public Shader SCShader;

	// Token: 0x04000E8C RID: 3724
	public Camera Camera2;

	// Token: 0x04000E8D RID: 3725
	private float TimeX = 1f;

	// Token: 0x04000E8E RID: 3726
	private Material SCMaterial;

	// Token: 0x04000E8F RID: 3727
	[Range(0f, 1f)]
	public float SwitchCameraToCamera2;

	// Token: 0x04000E90 RID: 3728
	[Range(0f, 1f)]
	public float BlendFX = 0.5f;

	// Token: 0x04000E91 RID: 3729
	private RenderTexture Camera2tex;
}
