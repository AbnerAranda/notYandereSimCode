using System;
using UnityEngine;

// Token: 0x02000137 RID: 311
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Blend 2 Camera/VividLight")]
public class CameraFilterPack_Blend2Camera_VividLight : MonoBehaviour
{
	// Token: 0x17000256 RID: 598
	// (get) Token: 0x06000C59 RID: 3161 RVA: 0x000672ED File Offset: 0x000654ED
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

	// Token: 0x06000C5A RID: 3162 RVA: 0x00067324 File Offset: 0x00065524
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

	// Token: 0x06000C5B RID: 3163 RVA: 0x00067388 File Offset: 0x00065588
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

	// Token: 0x06000C5C RID: 3164 RVA: 0x00067478 File Offset: 0x00065678
	private void OnValidate()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000C5D RID: 3165 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000C5E RID: 3166 RVA: 0x00067478 File Offset: 0x00065678
	private void OnEnable()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000C5F RID: 3167 RVA: 0x000674B0 File Offset: 0x000656B0
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

	// Token: 0x04000F2B RID: 3883
	private string ShaderName = "CameraFilterPack/Blend2Camera_VividLight";

	// Token: 0x04000F2C RID: 3884
	public Shader SCShader;

	// Token: 0x04000F2D RID: 3885
	public Camera Camera2;

	// Token: 0x04000F2E RID: 3886
	private float TimeX = 1f;

	// Token: 0x04000F2F RID: 3887
	private Material SCMaterial;

	// Token: 0x04000F30 RID: 3888
	[Range(0f, 1f)]
	public float SwitchCameraToCamera2;

	// Token: 0x04000F31 RID: 3889
	[Range(0f, 1f)]
	public float BlendFX = 0.5f;

	// Token: 0x04000F32 RID: 3890
	private RenderTexture Camera2tex;
}
