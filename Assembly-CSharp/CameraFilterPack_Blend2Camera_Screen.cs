using System;
using UnityEngine;

// Token: 0x02000132 RID: 306
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Blend 2 Camera/Screen")]
public class CameraFilterPack_Blend2Camera_Screen : MonoBehaviour
{
	// Token: 0x17000251 RID: 593
	// (get) Token: 0x06000C31 RID: 3121 RVA: 0x000666D9 File Offset: 0x000648D9
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

	// Token: 0x06000C32 RID: 3122 RVA: 0x00066710 File Offset: 0x00064910
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

	// Token: 0x06000C33 RID: 3123 RVA: 0x00066774 File Offset: 0x00064974
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

	// Token: 0x06000C34 RID: 3124 RVA: 0x00066864 File Offset: 0x00064A64
	private void OnValidate()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000C35 RID: 3125 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000C36 RID: 3126 RVA: 0x00066864 File Offset: 0x00064A64
	private void OnEnable()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000C37 RID: 3127 RVA: 0x0006689C File Offset: 0x00064A9C
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

	// Token: 0x04000EF4 RID: 3828
	private string ShaderName = "CameraFilterPack/Blend2Camera_Screen";

	// Token: 0x04000EF5 RID: 3829
	public Shader SCShader;

	// Token: 0x04000EF6 RID: 3830
	public Camera Camera2;

	// Token: 0x04000EF7 RID: 3831
	private float TimeX = 1f;

	// Token: 0x04000EF8 RID: 3832
	private Material SCMaterial;

	// Token: 0x04000EF9 RID: 3833
	[Range(0f, 1f)]
	public float SwitchCameraToCamera2;

	// Token: 0x04000EFA RID: 3834
	[Range(0f, 1f)]
	public float BlendFX = 0.5f;

	// Token: 0x04000EFB RID: 3835
	private RenderTexture Camera2tex;
}
