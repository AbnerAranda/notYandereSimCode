using System;
using UnityEngine;

// Token: 0x02000120 RID: 288
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Blend 2 Camera/Difference")]
public class CameraFilterPack_Blend2Camera_Difference : MonoBehaviour
{
	// Token: 0x1700023F RID: 575
	// (get) Token: 0x06000BA1 RID: 2977 RVA: 0x00063D5D File Offset: 0x00061F5D
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

	// Token: 0x06000BA2 RID: 2978 RVA: 0x00063D94 File Offset: 0x00061F94
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

	// Token: 0x06000BA3 RID: 2979 RVA: 0x00063DF8 File Offset: 0x00061FF8
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

	// Token: 0x06000BA4 RID: 2980 RVA: 0x00063EE8 File Offset: 0x000620E8
	private void OnValidate()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000BA5 RID: 2981 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000BA6 RID: 2982 RVA: 0x00063EE8 File Offset: 0x000620E8
	private void OnEnable()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000BA7 RID: 2983 RVA: 0x00063F20 File Offset: 0x00062120
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

	// Token: 0x04000E5C RID: 3676
	private string ShaderName = "CameraFilterPack/Blend2Camera_Difference";

	// Token: 0x04000E5D RID: 3677
	public Shader SCShader;

	// Token: 0x04000E5E RID: 3678
	public Camera Camera2;

	// Token: 0x04000E5F RID: 3679
	private float TimeX = 1f;

	// Token: 0x04000E60 RID: 3680
	private Material SCMaterial;

	// Token: 0x04000E61 RID: 3681
	[Range(0f, 1f)]
	public float SwitchCameraToCamera2;

	// Token: 0x04000E62 RID: 3682
	[Range(0f, 1f)]
	public float BlendFX = 0.5f;

	// Token: 0x04000E63 RID: 3683
	private RenderTexture Camera2tex;
}
