using System;
using UnityEngine;

// Token: 0x0200011F RID: 287
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Blend 2 Camera/DarkerColor")]
public class CameraFilterPack_Blend2Camera_DarkerColor : MonoBehaviour
{
	// Token: 0x1700023E RID: 574
	// (get) Token: 0x06000B99 RID: 2969 RVA: 0x00063B3D File Offset: 0x00061D3D
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

	// Token: 0x06000B9A RID: 2970 RVA: 0x00063B74 File Offset: 0x00061D74
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

	// Token: 0x06000B9B RID: 2971 RVA: 0x00063BD8 File Offset: 0x00061DD8
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

	// Token: 0x06000B9C RID: 2972 RVA: 0x00063CC8 File Offset: 0x00061EC8
	private void OnValidate()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000B9D RID: 2973 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000B9E RID: 2974 RVA: 0x00063CC8 File Offset: 0x00061EC8
	private void OnEnable()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000B9F RID: 2975 RVA: 0x00063D00 File Offset: 0x00061F00
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

	// Token: 0x04000E54 RID: 3668
	private string ShaderName = "CameraFilterPack/Blend2Camera_DarkerColor";

	// Token: 0x04000E55 RID: 3669
	public Shader SCShader;

	// Token: 0x04000E56 RID: 3670
	public Camera Camera2;

	// Token: 0x04000E57 RID: 3671
	private float TimeX = 1f;

	// Token: 0x04000E58 RID: 3672
	private Material SCMaterial;

	// Token: 0x04000E59 RID: 3673
	[Range(0f, 1f)]
	public float SwitchCameraToCamera2;

	// Token: 0x04000E5A RID: 3674
	[Range(0f, 1f)]
	public float BlendFX = 0.5f;

	// Token: 0x04000E5B RID: 3675
	private RenderTexture Camera2tex;
}
