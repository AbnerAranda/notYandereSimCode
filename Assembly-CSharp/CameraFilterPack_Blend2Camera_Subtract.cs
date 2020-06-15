using System;
using UnityEngine;

// Token: 0x02000136 RID: 310
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Blend 2 Camera/Subtract")]
public class CameraFilterPack_Blend2Camera_Subtract : MonoBehaviour
{
	// Token: 0x17000255 RID: 597
	// (get) Token: 0x06000C51 RID: 3153 RVA: 0x000670CD File Offset: 0x000652CD
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

	// Token: 0x06000C52 RID: 3154 RVA: 0x00067104 File Offset: 0x00065304
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

	// Token: 0x06000C53 RID: 3155 RVA: 0x00067168 File Offset: 0x00065368
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

	// Token: 0x06000C54 RID: 3156 RVA: 0x00067258 File Offset: 0x00065458
	private void OnValidate()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000C55 RID: 3157 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000C56 RID: 3158 RVA: 0x00067258 File Offset: 0x00065458
	private void OnEnable()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000C57 RID: 3159 RVA: 0x00067290 File Offset: 0x00065490
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

	// Token: 0x04000F23 RID: 3875
	private string ShaderName = "CameraFilterPack/Blend2Camera_Subtract";

	// Token: 0x04000F24 RID: 3876
	public Shader SCShader;

	// Token: 0x04000F25 RID: 3877
	public Camera Camera2;

	// Token: 0x04000F26 RID: 3878
	private float TimeX = 1f;

	// Token: 0x04000F27 RID: 3879
	private Material SCMaterial;

	// Token: 0x04000F28 RID: 3880
	[Range(0f, 1f)]
	public float SwitchCameraToCamera2;

	// Token: 0x04000F29 RID: 3881
	[Range(0f, 1f)]
	public float BlendFX = 0.5f;

	// Token: 0x04000F2A RID: 3882
	private RenderTexture Camera2tex;
}
