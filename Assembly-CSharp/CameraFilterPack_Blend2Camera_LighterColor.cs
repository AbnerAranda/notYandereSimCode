using System;
using UnityEngine;

// Token: 0x02000128 RID: 296
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Blend 2 Camera/LighterColor")]
public class CameraFilterPack_Blend2Camera_LighterColor : MonoBehaviour
{
	// Token: 0x17000247 RID: 583
	// (get) Token: 0x06000BE0 RID: 3040 RVA: 0x00064ECD File Offset: 0x000630CD
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

	// Token: 0x06000BE1 RID: 3041 RVA: 0x00064F04 File Offset: 0x00063104
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

	// Token: 0x06000BE2 RID: 3042 RVA: 0x00064F68 File Offset: 0x00063168
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

	// Token: 0x06000BE3 RID: 3043 RVA: 0x00065058 File Offset: 0x00063258
	private void OnValidate()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000BE4 RID: 3044 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000BE5 RID: 3045 RVA: 0x00065058 File Offset: 0x00063258
	private void OnEnable()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000BE6 RID: 3046 RVA: 0x00065090 File Offset: 0x00063290
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

	// Token: 0x04000EA2 RID: 3746
	private string ShaderName = "CameraFilterPack/Blend2Camera_LighterColor";

	// Token: 0x04000EA3 RID: 3747
	public Shader SCShader;

	// Token: 0x04000EA4 RID: 3748
	public Camera Camera2;

	// Token: 0x04000EA5 RID: 3749
	private float TimeX = 1f;

	// Token: 0x04000EA6 RID: 3750
	private Material SCMaterial;

	// Token: 0x04000EA7 RID: 3751
	[Range(0f, 1f)]
	public float SwitchCameraToCamera2;

	// Token: 0x04000EA8 RID: 3752
	[Range(0f, 1f)]
	public float BlendFX = 0.5f;

	// Token: 0x04000EA9 RID: 3753
	private RenderTexture Camera2tex;
}
