using System;
using UnityEngine;

// Token: 0x0200018E RID: 398
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Drawing/Paper3")]
public class CameraFilterPack_Drawing_Paper3 : MonoBehaviour
{
	// Token: 0x170002AD RID: 685
	// (get) Token: 0x06000E67 RID: 3687 RVA: 0x0006FC3E File Offset: 0x0006DE3E
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

	// Token: 0x06000E68 RID: 3688 RVA: 0x0006FC72 File Offset: 0x0006DE72
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_Paper4") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/Drawing_Paper3");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000E69 RID: 3689 RVA: 0x0006FCA8 File Offset: 0x0006DEA8
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
			this.material.SetColor("_PColor", this.Pencil_Color);
			this.material.SetFloat("_Value1", this.Pencil_Size);
			this.material.SetFloat("_Value2", this.Pencil_Correction);
			this.material.SetFloat("_Value3", this.Intensity);
			this.material.SetFloat("_Value4", this.Speed_Animation);
			this.material.SetFloat("_Value5", this.Corner_Lose);
			this.material.SetFloat("_Value6", this.Fade_Paper_to_BackColor);
			this.material.SetFloat("_Value7", this.Fade_With_Original);
			this.material.SetColor("_PColor2", this.Back_Color);
			this.material.SetTexture("_MainTex2", this.Texture2);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000E6A RID: 3690 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000E6B RID: 3691 RVA: 0x0006FDF7 File Offset: 0x0006DFF7
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x0400113F RID: 4415
	public Shader SCShader;

	// Token: 0x04001140 RID: 4416
	private float TimeX = 1f;

	// Token: 0x04001141 RID: 4417
	public Color Pencil_Color = new Color(0f, 0f, 0f, 0f);

	// Token: 0x04001142 RID: 4418
	[Range(0.0001f, 0.0022f)]
	public float Pencil_Size = 0.00125f;

	// Token: 0x04001143 RID: 4419
	[Range(0f, 2f)]
	public float Pencil_Correction = 0.35f;

	// Token: 0x04001144 RID: 4420
	[Range(0f, 1f)]
	public float Intensity = 1f;

	// Token: 0x04001145 RID: 4421
	[Range(0f, 2f)]
	public float Speed_Animation = 1f;

	// Token: 0x04001146 RID: 4422
	[Range(0f, 1f)]
	public float Corner_Lose = 1f;

	// Token: 0x04001147 RID: 4423
	[Range(0f, 1f)]
	public float Fade_Paper_to_BackColor;

	// Token: 0x04001148 RID: 4424
	[Range(0f, 1f)]
	public float Fade_With_Original = 1f;

	// Token: 0x04001149 RID: 4425
	public Color Back_Color = new Color(1f, 1f, 1f, 1f);

	// Token: 0x0400114A RID: 4426
	private Material SCMaterial;

	// Token: 0x0400114B RID: 4427
	private Texture2D Texture2;
}
