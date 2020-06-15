using System;
using UnityEngine;

// Token: 0x02000135 RID: 309
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Split Screen/Split 3D")]
public class CameraFilterPack_Blend2Camera_SplitScreen3D : MonoBehaviour
{
	// Token: 0x17000254 RID: 596
	// (get) Token: 0x06000C49 RID: 3145 RVA: 0x00066DBC File Offset: 0x00064FBC
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

	// Token: 0x06000C4A RID: 3146 RVA: 0x00066DF0 File Offset: 0x00064FF0
	private void OnValidate()
	{
		this.ScreenSize.x = (float)Screen.width;
		this.ScreenSize.y = (float)Screen.height;
	}

	// Token: 0x06000C4B RID: 3147 RVA: 0x00066E14 File Offset: 0x00065014
	private void Start()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture((int)this.ScreenSize.x, (int)this.ScreenSize.y, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
		this.SCShader = Shader.Find(this.ShaderName);
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000C4C RID: 3148 RVA: 0x00066E88 File Offset: 0x00065088
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
			this.material.SetFloat("_Near", this._Distance);
			this.material.SetFloat("_Far", this._Size);
			this.material.SetFloat("_FixDistance", this._FixDistance);
			this.material.SetFloat("_TimeX", this.TimeX);
			this.material.SetFloat("_Value", this.BlendFX);
			this.material.SetFloat("_Value2", this.SwitchCameraToCamera2);
			this.material.SetFloat("_Value3", this.SplitX);
			this.material.SetFloat("_Value6", this.SplitY);
			this.material.SetFloat("_Value4", this.Smooth);
			this.material.SetFloat("_Value5", this.Rotation);
			this.material.SetInt("_ForceYSwap", this.ForceYSwap ? 0 : 1);
			base.GetComponent<Camera>().depthTextureMode = DepthTextureMode.Depth;
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000C4D RID: 3149 RVA: 0x00066DF0 File Offset: 0x00064FF0
	private void Update()
	{
		this.ScreenSize.x = (float)Screen.width;
		this.ScreenSize.y = (float)Screen.height;
	}

	// Token: 0x06000C4E RID: 3150 RVA: 0x0006700D File Offset: 0x0006520D
	private void OnEnable()
	{
		this.Start();
	}

	// Token: 0x06000C4F RID: 3151 RVA: 0x00067015 File Offset: 0x00065215
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

	// Token: 0x04000F12 RID: 3858
	private string ShaderName = "CameraFilterPack/Blend2Camera_SplitScreen3D";

	// Token: 0x04000F13 RID: 3859
	public Shader SCShader;

	// Token: 0x04000F14 RID: 3860
	public Camera Camera2;

	// Token: 0x04000F15 RID: 3861
	private float TimeX = 1f;

	// Token: 0x04000F16 RID: 3862
	private Material SCMaterial;

	// Token: 0x04000F17 RID: 3863
	[Range(0f, 100f)]
	public float _FixDistance = 1f;

	// Token: 0x04000F18 RID: 3864
	[Range(-0.99f, 0.99f)]
	public float _Distance = 0.5f;

	// Token: 0x04000F19 RID: 3865
	[Range(0f, 0.5f)]
	public float _Size = 0.1f;

	// Token: 0x04000F1A RID: 3866
	[Range(0f, 1f)]
	public float SwitchCameraToCamera2;

	// Token: 0x04000F1B RID: 3867
	[Range(0f, 1f)]
	public float BlendFX = 1f;

	// Token: 0x04000F1C RID: 3868
	[Range(-3f, 3f)]
	public float SplitX = 0.5f;

	// Token: 0x04000F1D RID: 3869
	[Range(-3f, 3f)]
	public float SplitY = 0.5f;

	// Token: 0x04000F1E RID: 3870
	[Range(0f, 2f)]
	public float Smooth = 0.1f;

	// Token: 0x04000F1F RID: 3871
	[Range(-3.14f, 3.14f)]
	public float Rotation = 3.14f;

	// Token: 0x04000F20 RID: 3872
	private bool ForceYSwap;

	// Token: 0x04000F21 RID: 3873
	private RenderTexture Camera2tex;

	// Token: 0x04000F22 RID: 3874
	private Vector2 ScreenSize;
}
