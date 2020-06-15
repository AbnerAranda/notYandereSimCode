using System;
using UnityEngine;

// Token: 0x02000123 RID: 291
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Chroma Key/GreenScreen")]
public class CameraFilterPack_Blend2Camera_GreenScreen : MonoBehaviour
{
	// Token: 0x17000242 RID: 578
	// (get) Token: 0x06000BB9 RID: 3001 RVA: 0x000643BD File Offset: 0x000625BD
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

	// Token: 0x06000BBA RID: 3002 RVA: 0x000643F4 File Offset: 0x000625F4
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

	// Token: 0x06000BBB RID: 3003 RVA: 0x00064468 File Offset: 0x00062668
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

	// Token: 0x06000BBC RID: 3004 RVA: 0x00064599 File Offset: 0x00062799
	private void Update()
	{
		this.ScreenSize.x = (float)Screen.width;
		this.ScreenSize.y = (float)Screen.height;
		bool isPlaying = Application.isPlaying;
	}

	// Token: 0x06000BBD RID: 3005 RVA: 0x000645C3 File Offset: 0x000627C3
	private void OnEnable()
	{
		this.Start();
		this.Update();
	}

	// Token: 0x06000BBE RID: 3006 RVA: 0x000645D4 File Offset: 0x000627D4
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

	// Token: 0x04000E74 RID: 3700
	private string ShaderName = "CameraFilterPack/Blend2Camera_GreenScreen";

	// Token: 0x04000E75 RID: 3701
	public Shader SCShader;

	// Token: 0x04000E76 RID: 3702
	public Camera Camera2;

	// Token: 0x04000E77 RID: 3703
	private float TimeX = 1f;

	// Token: 0x04000E78 RID: 3704
	private Material SCMaterial;

	// Token: 0x04000E79 RID: 3705
	[Range(0f, 1f)]
	public float BlendFX = 1f;

	// Token: 0x04000E7A RID: 3706
	[Range(-0.2f, 0.2f)]
	public float Adjust;

	// Token: 0x04000E7B RID: 3707
	[Range(-0.2f, 0.2f)]
	public float Precision;

	// Token: 0x04000E7C RID: 3708
	[Range(-0.2f, 0.2f)]
	public float Luminosity;

	// Token: 0x04000E7D RID: 3709
	[Range(-0.3f, 0.3f)]
	public float Change_Red;

	// Token: 0x04000E7E RID: 3710
	[Range(-0.3f, 0.3f)]
	public float Change_Green;

	// Token: 0x04000E7F RID: 3711
	[Range(-0.3f, 0.3f)]
	public float Change_Blue;

	// Token: 0x04000E80 RID: 3712
	private RenderTexture Camera2tex;

	// Token: 0x04000E81 RID: 3713
	private Vector2 ScreenSize;
}
