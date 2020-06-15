using System;
using UnityEngine;

// Token: 0x02000130 RID: 304
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Blend 2 Camera/PinLight")]
public class CameraFilterPack_Blend2Camera_PinLight : MonoBehaviour
{
	// Token: 0x1700024F RID: 591
	// (get) Token: 0x06000C21 RID: 3105 RVA: 0x0006629C File Offset: 0x0006449C
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

	// Token: 0x06000C22 RID: 3106 RVA: 0x000662D0 File Offset: 0x000644D0
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

	// Token: 0x06000C23 RID: 3107 RVA: 0x00066334 File Offset: 0x00064534
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

	// Token: 0x06000C24 RID: 3108 RVA: 0x00066424 File Offset: 0x00064624
	private void OnValidate()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000C25 RID: 3109 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000C26 RID: 3110 RVA: 0x00066424 File Offset: 0x00064624
	private void OnEnable()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000C27 RID: 3111 RVA: 0x0006645C File Offset: 0x0006465C
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

	// Token: 0x04000EE4 RID: 3812
	private string ShaderName = "CameraFilterPack/Blend2Camera_PinLight";

	// Token: 0x04000EE5 RID: 3813
	public Shader SCShader;

	// Token: 0x04000EE6 RID: 3814
	public Camera Camera2;

	// Token: 0x04000EE7 RID: 3815
	private float TimeX = 1f;

	// Token: 0x04000EE8 RID: 3816
	private Material SCMaterial;

	// Token: 0x04000EE9 RID: 3817
	[Range(0f, 1f)]
	public float SwitchCameraToCamera2;

	// Token: 0x04000EEA RID: 3818
	[Range(0f, 1f)]
	public float BlendFX = 0.5f;

	// Token: 0x04000EEB RID: 3819
	private RenderTexture Camera2tex;
}
