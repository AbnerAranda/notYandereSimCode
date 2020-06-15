using System;
using UnityEngine;

// Token: 0x02000121 RID: 289
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Blend 2 Camera/Divide")]
public class CameraFilterPack_Blend2Camera_Divide : MonoBehaviour
{
	// Token: 0x17000240 RID: 576
	// (get) Token: 0x06000BA9 RID: 2985 RVA: 0x00063F7D File Offset: 0x0006217D
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

	// Token: 0x06000BAA RID: 2986 RVA: 0x00063FB4 File Offset: 0x000621B4
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

	// Token: 0x06000BAB RID: 2987 RVA: 0x00064018 File Offset: 0x00062218
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

	// Token: 0x06000BAC RID: 2988 RVA: 0x00064108 File Offset: 0x00062308
	private void OnValidate()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000BAD RID: 2989 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000BAE RID: 2990 RVA: 0x00064108 File Offset: 0x00062308
	private void OnEnable()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000BAF RID: 2991 RVA: 0x00064140 File Offset: 0x00062340
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

	// Token: 0x04000E64 RID: 3684
	private string ShaderName = "CameraFilterPack/Blend2Camera_Divide";

	// Token: 0x04000E65 RID: 3685
	public Shader SCShader;

	// Token: 0x04000E66 RID: 3686
	public Camera Camera2;

	// Token: 0x04000E67 RID: 3687
	private float TimeX = 1f;

	// Token: 0x04000E68 RID: 3688
	private Material SCMaterial;

	// Token: 0x04000E69 RID: 3689
	[Range(0f, 1f)]
	public float SwitchCameraToCamera2;

	// Token: 0x04000E6A RID: 3690
	[Range(0f, 1f)]
	public float BlendFX = 0.5f;

	// Token: 0x04000E6B RID: 3691
	private RenderTexture Camera2tex;
}
