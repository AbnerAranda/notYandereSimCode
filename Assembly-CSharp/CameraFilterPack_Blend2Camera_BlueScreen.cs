using System;
using UnityEngine;

// Token: 0x02000119 RID: 281
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Chroma Key/BlueScreen")]
public class CameraFilterPack_Blend2Camera_BlueScreen : MonoBehaviour
{
	// Token: 0x17000238 RID: 568
	// (get) Token: 0x06000B6A RID: 2922 RVA: 0x00062D8D File Offset: 0x00060F8D
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

	// Token: 0x06000B6B RID: 2923 RVA: 0x00062DC1 File Offset: 0x00060FC1
	private void OnValidate()
	{
		this.ScreenSize.x = (float)Screen.width;
		this.ScreenSize.y = (float)Screen.height;
	}

	// Token: 0x06000B6C RID: 2924 RVA: 0x00062DE8 File Offset: 0x00060FE8
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

	// Token: 0x06000B6D RID: 2925 RVA: 0x00062E5C File Offset: 0x0006105C
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
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000B6E RID: 2926 RVA: 0x00062F8D File Offset: 0x0006118D
	private void Update()
	{
		this.ScreenSize.x = (float)Screen.width;
		this.ScreenSize.y = (float)Screen.height;
		bool isPlaying = Application.isPlaying;
	}

	// Token: 0x06000B6F RID: 2927 RVA: 0x00062FB7 File Offset: 0x000611B7
	private void OnEnable()
	{
		this.Start();
	}

	// Token: 0x06000B70 RID: 2928 RVA: 0x00062FBF File Offset: 0x000611BF
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

	// Token: 0x04000E17 RID: 3607
	private string ShaderName = "CameraFilterPack/Blend2Camera_BlueScreen";

	// Token: 0x04000E18 RID: 3608
	public Shader SCShader;

	// Token: 0x04000E19 RID: 3609
	public Camera Camera2;

	// Token: 0x04000E1A RID: 3610
	private float TimeX = 1f;

	// Token: 0x04000E1B RID: 3611
	private Material SCMaterial;

	// Token: 0x04000E1C RID: 3612
	[Range(0f, 1f)]
	public float BlendFX = 1f;

	// Token: 0x04000E1D RID: 3613
	[Range(-0.2f, 0.2f)]
	public float Adjust;

	// Token: 0x04000E1E RID: 3614
	[Range(-0.2f, 0.2f)]
	public float Precision;

	// Token: 0x04000E1F RID: 3615
	[Range(-0.2f, 0.2f)]
	public float Luminosity;

	// Token: 0x04000E20 RID: 3616
	[Range(-0.3f, 0.3f)]
	public float Change_Red;

	// Token: 0x04000E21 RID: 3617
	[Range(-0.3f, 0.3f)]
	public float Change_Green;

	// Token: 0x04000E22 RID: 3618
	[Range(-0.3f, 0.3f)]
	public float Change_Blue;

	// Token: 0x04000E23 RID: 3619
	private RenderTexture Camera2tex;

	// Token: 0x04000E24 RID: 3620
	private Vector2 ScreenSize;
}
