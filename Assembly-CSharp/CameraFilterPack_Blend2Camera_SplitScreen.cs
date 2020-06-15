using System;
using UnityEngine;

// Token: 0x02000134 RID: 308
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Split Screen/SideBySide")]
public class CameraFilterPack_Blend2Camera_SplitScreen : MonoBehaviour
{
	// Token: 0x17000253 RID: 595
	// (get) Token: 0x06000C41 RID: 3137 RVA: 0x00066B19 File Offset: 0x00064D19
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

	// Token: 0x06000C42 RID: 3138 RVA: 0x00066B4D File Offset: 0x00064D4D
	private void OnValidate()
	{
		this.ScreenSize.x = (float)Screen.width;
		this.ScreenSize.y = (float)Screen.height;
	}

	// Token: 0x06000C43 RID: 3139 RVA: 0x00066B74 File Offset: 0x00064D74
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

	// Token: 0x06000C44 RID: 3140 RVA: 0x00066BE8 File Offset: 0x00064DE8
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
			this.material.SetFloat("_Value3", this.SplitX);
			this.material.SetFloat("_Value6", this.SplitY);
			this.material.SetFloat("_Value4", this.Smooth);
			this.material.SetFloat("_Value5", this.Rotation);
			this.material.SetInt("_ForceYSwap", this.ForceYSwap ? 0 : 1);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000C45 RID: 3141 RVA: 0x00066B4D File Offset: 0x00064D4D
	private void Update()
	{
		this.ScreenSize.x = (float)Screen.width;
		this.ScreenSize.y = (float)Screen.height;
	}

	// Token: 0x06000C46 RID: 3142 RVA: 0x00066D1F File Offset: 0x00064F1F
	private void OnEnable()
	{
		this.Start();
	}

	// Token: 0x06000C47 RID: 3143 RVA: 0x00066D27 File Offset: 0x00064F27
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

	// Token: 0x04000F04 RID: 3844
	private string ShaderName = "CameraFilterPack/Blend2Camera_SplitScreen";

	// Token: 0x04000F05 RID: 3845
	public Shader SCShader;

	// Token: 0x04000F06 RID: 3846
	public Camera Camera2;

	// Token: 0x04000F07 RID: 3847
	private float TimeX = 1f;

	// Token: 0x04000F08 RID: 3848
	private Material SCMaterial;

	// Token: 0x04000F09 RID: 3849
	[Range(0f, 1f)]
	public float SwitchCameraToCamera2;

	// Token: 0x04000F0A RID: 3850
	[Range(0f, 1f)]
	public float BlendFX = 1f;

	// Token: 0x04000F0B RID: 3851
	[Range(-3f, 3f)]
	public float SplitX = 0.5f;

	// Token: 0x04000F0C RID: 3852
	[Range(-3f, 3f)]
	public float SplitY = 0.5f;

	// Token: 0x04000F0D RID: 3853
	[Range(0f, 2f)]
	public float Smooth = 0.1f;

	// Token: 0x04000F0E RID: 3854
	[Range(-3.14f, 3.14f)]
	public float Rotation = 3.14f;

	// Token: 0x04000F0F RID: 3855
	private bool ForceYSwap;

	// Token: 0x04000F10 RID: 3856
	private RenderTexture Camera2tex;

	// Token: 0x04000F11 RID: 3857
	private Vector2 ScreenSize;
}
