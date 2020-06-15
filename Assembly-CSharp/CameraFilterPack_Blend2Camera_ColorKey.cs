using System;
using UnityEngine;

// Token: 0x0200011D RID: 285
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Chroma Key/Color Key")]
public class CameraFilterPack_Blend2Camera_ColorKey : MonoBehaviour
{
	// Token: 0x1700023C RID: 572
	// (get) Token: 0x06000B8A RID: 2954 RVA: 0x00063679 File Offset: 0x00061879
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

	// Token: 0x06000B8B RID: 2955 RVA: 0x000636B0 File Offset: 0x000618B0
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

	// Token: 0x06000B8C RID: 2956 RVA: 0x00063724 File Offset: 0x00061924
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
			this.material.SetFloat("_Value2", this.Adjust);
			this.material.SetFloat("_Value3", this.Precision);
			this.material.SetFloat("_Value4", this.Luminosity);
			this.material.SetFloat("_Value5", this.Change_Red);
			this.material.SetFloat("_Value6", this.Change_Green);
			this.material.SetFloat("_Value7", this.Change_Blue);
			this.material.SetColor("_ColorKey", this.ColorKey);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000B8D RID: 2957 RVA: 0x0006386B File Offset: 0x00061A6B
	private void Update()
	{
		this.ScreenSize.x = (float)Screen.width;
		this.ScreenSize.y = (float)Screen.height;
		bool isPlaying = Application.isPlaying;
	}

	// Token: 0x06000B8E RID: 2958 RVA: 0x00063895 File Offset: 0x00061A95
	private void OnEnable()
	{
		this.Start();
		this.Update();
	}

	// Token: 0x06000B8F RID: 2959 RVA: 0x000638A4 File Offset: 0x00061AA4
	private void OnDisable()
	{
		if (this.Camera2 != null && this.Camera2.targetTexture != null)
		{
			this.Camera2.targetTexture = null;
		}
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000E3D RID: 3645
	private string ShaderName = "CameraFilterPack/Blend2Camera_ColorKey";

	// Token: 0x04000E3E RID: 3646
	public Shader SCShader;

	// Token: 0x04000E3F RID: 3647
	public Camera Camera2;

	// Token: 0x04000E40 RID: 3648
	private float TimeX = 1f;

	// Token: 0x04000E41 RID: 3649
	private Material SCMaterial;

	// Token: 0x04000E42 RID: 3650
	[Range(0f, 1f)]
	public float BlendFX = 1f;

	// Token: 0x04000E43 RID: 3651
	public Color ColorKey;

	// Token: 0x04000E44 RID: 3652
	[Range(-0.2f, 0.2f)]
	public float Adjust;

	// Token: 0x04000E45 RID: 3653
	[Range(-0.2f, 0.2f)]
	public float Precision;

	// Token: 0x04000E46 RID: 3654
	[Range(-0.2f, 0.2f)]
	public float Luminosity;

	// Token: 0x04000E47 RID: 3655
	[Range(-0.3f, 0.3f)]
	public float Change_Red;

	// Token: 0x04000E48 RID: 3656
	[Range(-0.3f, 0.3f)]
	public float Change_Green;

	// Token: 0x04000E49 RID: 3657
	[Range(-0.3f, 0.3f)]
	public float Change_Blue;

	// Token: 0x04000E4A RID: 3658
	private RenderTexture Camera2tex;

	// Token: 0x04000E4B RID: 3659
	private Vector2 ScreenSize;
}
