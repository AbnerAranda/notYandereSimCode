using System;
using UnityEngine;

// Token: 0x0200012E RID: 302
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Blend 2 Camera/Overlay")]
public class CameraFilterPack_Blend2Camera_Overlay : MonoBehaviour
{
	// Token: 0x1700024D RID: 589
	// (get) Token: 0x06000C10 RID: 3088 RVA: 0x00065B8D File Offset: 0x00063D8D
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

	// Token: 0x06000C11 RID: 3089 RVA: 0x00065BC4 File Offset: 0x00063DC4
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

	// Token: 0x06000C12 RID: 3090 RVA: 0x00065C28 File Offset: 0x00063E28
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

	// Token: 0x06000C13 RID: 3091 RVA: 0x00065D18 File Offset: 0x00063F18
	private void OnValidate()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000C14 RID: 3092 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000C15 RID: 3093 RVA: 0x00065D18 File Offset: 0x00063F18
	private void OnEnable()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000C16 RID: 3094 RVA: 0x00065D50 File Offset: 0x00063F50
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

	// Token: 0x04000ED2 RID: 3794
	private string ShaderName = "CameraFilterPack/Blend2Camera_Overlay";

	// Token: 0x04000ED3 RID: 3795
	public Shader SCShader;

	// Token: 0x04000ED4 RID: 3796
	public Camera Camera2;

	// Token: 0x04000ED5 RID: 3797
	private float TimeX = 1f;

	// Token: 0x04000ED6 RID: 3798
	private Material SCMaterial;

	// Token: 0x04000ED7 RID: 3799
	[Range(0f, 1f)]
	public float SwitchCameraToCamera2;

	// Token: 0x04000ED8 RID: 3800
	[Range(0f, 1f)]
	public float BlendFX = 0.5f;

	// Token: 0x04000ED9 RID: 3801
	private RenderTexture Camera2tex;
}
