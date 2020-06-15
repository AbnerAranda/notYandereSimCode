using System;
using UnityEngine;

// Token: 0x02000126 RID: 294
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Blend 2 Camera/Hue")]
public class CameraFilterPack_Blend2Camera_Hue : MonoBehaviour
{
	// Token: 0x17000245 RID: 581
	// (get) Token: 0x06000BD0 RID: 3024 RVA: 0x00064A8D File Offset: 0x00062C8D
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

	// Token: 0x06000BD1 RID: 3025 RVA: 0x00064AC4 File Offset: 0x00062CC4
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

	// Token: 0x06000BD2 RID: 3026 RVA: 0x00064B28 File Offset: 0x00062D28
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

	// Token: 0x06000BD3 RID: 3027 RVA: 0x00064C18 File Offset: 0x00062E18
	private void OnValidate()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000BD4 RID: 3028 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000BD5 RID: 3029 RVA: 0x00064C18 File Offset: 0x00062E18
	private void OnEnable()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000BD6 RID: 3030 RVA: 0x00064C50 File Offset: 0x00062E50
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

	// Token: 0x04000E92 RID: 3730
	private string ShaderName = "CameraFilterPack/Blend2Camera_Hue";

	// Token: 0x04000E93 RID: 3731
	public Shader SCShader;

	// Token: 0x04000E94 RID: 3732
	public Camera Camera2;

	// Token: 0x04000E95 RID: 3733
	private float TimeX = 1f;

	// Token: 0x04000E96 RID: 3734
	private Material SCMaterial;

	// Token: 0x04000E97 RID: 3735
	[Range(0f, 1f)]
	public float SwitchCameraToCamera2;

	// Token: 0x04000E98 RID: 3736
	[Range(0f, 1f)]
	public float BlendFX = 0.5f;

	// Token: 0x04000E99 RID: 3737
	private RenderTexture Camera2tex;
}
