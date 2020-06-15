using System;
using UnityEngine;

// Token: 0x02000115 RID: 277
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Weather/Rain_Pro")]
public class CameraFilterPack_Atmosphere_Rain_Pro : MonoBehaviour
{
	// Token: 0x17000234 RID: 564
	// (get) Token: 0x06000B50 RID: 2896 RVA: 0x000623C3 File Offset: 0x000605C3
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

	// Token: 0x06000B51 RID: 2897 RVA: 0x000623F7 File Offset: 0x000605F7
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_Atmosphere_Rain_FX") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/Atmosphere_Rain_Pro");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000B52 RID: 2898 RVA: 0x00062430 File Offset: 0x00060630
	private void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
	{
		if (this.SCShader != null)
		{
			this.TimeX += Time.deltaTime;
			if (this.TimeX > 100f)
			{
				this.TimeX = 0f;
			}
			this.material.SetFloat("_TimeX", this.TimeX);
			this.material.SetFloat("_Value", this.Fade);
			this.material.SetFloat("_Value2", this.Intensity);
			this.material.SetFloat("_Value3", this.DirectionX);
			this.material.SetFloat("_Value4", this.Speed);
			this.material.SetFloat("_Value5", this.Size);
			this.material.SetFloat("_Value6", this.Distortion);
			this.material.SetFloat("_Value7", this.StormFlashOnOff);
			this.material.SetFloat("_Value8", this.DropOnOff);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			this.material.SetTexture("Texture2", this.Texture2);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000B53 RID: 2899 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000B54 RID: 2900 RVA: 0x00062596 File Offset: 0x00060796
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000DE8 RID: 3560
	public Shader SCShader;

	// Token: 0x04000DE9 RID: 3561
	private float TimeX = 1f;

	// Token: 0x04000DEA RID: 3562
	private Material SCMaterial;

	// Token: 0x04000DEB RID: 3563
	[Range(0f, 1f)]
	public float Fade = 1f;

	// Token: 0x04000DEC RID: 3564
	[Range(0f, 2f)]
	public float Intensity = 0.5f;

	// Token: 0x04000DED RID: 3565
	[Range(-0.25f, 0.25f)]
	public float DirectionX = 0.12f;

	// Token: 0x04000DEE RID: 3566
	[Range(0.4f, 2f)]
	public float Size = 1.5f;

	// Token: 0x04000DEF RID: 3567
	[Range(0f, 0.5f)]
	public float Speed = 0.275f;

	// Token: 0x04000DF0 RID: 3568
	[Range(0f, 0.5f)]
	public float Distortion = 0.025f;

	// Token: 0x04000DF1 RID: 3569
	[Range(0f, 1f)]
	public float StormFlashOnOff = 1f;

	// Token: 0x04000DF2 RID: 3570
	[Range(0f, 1f)]
	public float DropOnOff = 1f;

	// Token: 0x04000DF3 RID: 3571
	private Texture2D Texture2;
}
