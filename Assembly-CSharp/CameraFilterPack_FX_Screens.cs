using System;
using UnityEngine;

// Token: 0x020001B1 RID: 433
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/FX/Screens")]
public class CameraFilterPack_FX_Screens : MonoBehaviour
{
	// Token: 0x170002D0 RID: 720
	// (get) Token: 0x06000F3A RID: 3898 RVA: 0x00072EAD File Offset: 0x000710AD
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

	// Token: 0x06000F3B RID: 3899 RVA: 0x00072EE1 File Offset: 0x000710E1
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/FX_Screens");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000F3C RID: 3900 RVA: 0x00072F04 File Offset: 0x00071104
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
			this.material.SetFloat("_Value", this.Tiles);
			this.material.SetFloat("_Value2", this.Speed);
			this.material.SetFloat("_Value3", this.PosX);
			this.material.SetFloat("_Value4", this.PosY);
			this.material.SetColor("_color", this.color);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000F3D RID: 3901 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000F3E RID: 3902 RVA: 0x00073012 File Offset: 0x00071212
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001207 RID: 4615
	public Shader SCShader;

	// Token: 0x04001208 RID: 4616
	private float TimeX = 1f;

	// Token: 0x04001209 RID: 4617
	private Material SCMaterial;

	// Token: 0x0400120A RID: 4618
	[Range(0f, 256f)]
	public float Tiles = 8f;

	// Token: 0x0400120B RID: 4619
	[Range(0f, 5f)]
	public float Speed = 0.25f;

	// Token: 0x0400120C RID: 4620
	public Color color = new Color(0f, 1f, 1f, 1f);

	// Token: 0x0400120D RID: 4621
	[Range(-1f, 1f)]
	public float PosX;

	// Token: 0x0400120E RID: 4622
	[Range(-1f, 1f)]
	public float PosY;
}
