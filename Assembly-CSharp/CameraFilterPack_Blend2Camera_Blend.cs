using System;
using UnityEngine;

// Token: 0x02000118 RID: 280
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Blend 2 Camera/Blend")]
public class CameraFilterPack_Blend2Camera_Blend : MonoBehaviour
{
	// Token: 0x17000237 RID: 567
	// (get) Token: 0x06000B62 RID: 2914 RVA: 0x00062B92 File Offset: 0x00060D92
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

	// Token: 0x06000B63 RID: 2915 RVA: 0x00062BC8 File Offset: 0x00060DC8
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

	// Token: 0x06000B64 RID: 2916 RVA: 0x00062C2C File Offset: 0x00060E2C
	private void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
	{
		if (this.SCShader != null)
		{
			this.TimeX += Time.deltaTime;
			if (this.TimeX > 100f)
			{
				this.TimeX = 0f;
			}
			this.material.SetTexture("_MainTex2", this.Camera2tex);
			this.material.SetFloat("_TimeX", this.TimeX);
			this.material.SetFloat("_Value", this.BlendFX);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000B65 RID: 2917 RVA: 0x00062CF8 File Offset: 0x00060EF8
	private void OnValidate()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000B66 RID: 2918 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000B67 RID: 2919 RVA: 0x00062CF8 File Offset: 0x00060EF8
	private void OnEnable()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000B68 RID: 2920 RVA: 0x00062D30 File Offset: 0x00060F30
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

	// Token: 0x04000E10 RID: 3600
	private string ShaderName = "CameraFilterPack/Blend2Camera_Blend";

	// Token: 0x04000E11 RID: 3601
	public Shader SCShader;

	// Token: 0x04000E12 RID: 3602
	public Camera Camera2;

	// Token: 0x04000E13 RID: 3603
	private float TimeX = 1f;

	// Token: 0x04000E14 RID: 3604
	private Material SCMaterial;

	// Token: 0x04000E15 RID: 3605
	[Range(0f, 1f)]
	public float BlendFX = 0.5f;

	// Token: 0x04000E16 RID: 3606
	private RenderTexture Camera2tex;
}
