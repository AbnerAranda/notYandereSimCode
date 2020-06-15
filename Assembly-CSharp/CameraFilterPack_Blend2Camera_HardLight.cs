using System;
using UnityEngine;

// Token: 0x02000124 RID: 292
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Blend 2 Camera/HardLight")]
public class CameraFilterPack_Blend2Camera_HardLight : MonoBehaviour
{
	// Token: 0x17000243 RID: 579
	// (get) Token: 0x06000BC0 RID: 3008 RVA: 0x0006464F File Offset: 0x0006284F
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

	// Token: 0x06000BC1 RID: 3009 RVA: 0x00064684 File Offset: 0x00062884
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

	// Token: 0x06000BC2 RID: 3010 RVA: 0x000646E8 File Offset: 0x000628E8
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

	// Token: 0x06000BC3 RID: 3011 RVA: 0x000647D8 File Offset: 0x000629D8
	private void OnValidate()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000BC4 RID: 3012 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000BC5 RID: 3013 RVA: 0x000647D8 File Offset: 0x000629D8
	private void OnEnable()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000BC6 RID: 3014 RVA: 0x00064810 File Offset: 0x00062A10
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

	// Token: 0x04000E82 RID: 3714
	private string ShaderName = "CameraFilterPack/Blend2Camera_HardLight";

	// Token: 0x04000E83 RID: 3715
	public Shader SCShader;

	// Token: 0x04000E84 RID: 3716
	public Camera Camera2;

	// Token: 0x04000E85 RID: 3717
	private float TimeX = 1f;

	// Token: 0x04000E86 RID: 3718
	private Material SCMaterial;

	// Token: 0x04000E87 RID: 3719
	[Range(0f, 1f)]
	public float SwitchCameraToCamera2;

	// Token: 0x04000E88 RID: 3720
	[Range(0f, 1f)]
	public float BlendFX = 0.5f;

	// Token: 0x04000E89 RID: 3721
	private RenderTexture Camera2tex;
}
