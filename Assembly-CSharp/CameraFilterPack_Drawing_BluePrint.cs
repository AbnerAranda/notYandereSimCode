using System;
using UnityEngine;

// Token: 0x02000178 RID: 376
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Drawing/BluePrint")]
public class CameraFilterPack_Drawing_BluePrint : MonoBehaviour
{
	// Token: 0x17000297 RID: 663
	// (get) Token: 0x06000DE3 RID: 3555 RVA: 0x0006DAD7 File Offset: 0x0006BCD7
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

	// Token: 0x06000DE4 RID: 3556 RVA: 0x0006DB0B File Offset: 0x0006BD0B
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_Paper2") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/Drawing_BluePrint");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000DE5 RID: 3557 RVA: 0x0006DB44 File Offset: 0x0006BD44
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

	// Token: 0x06000DE6 RID: 3558 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000DE7 RID: 3559 RVA: 0x0006DC93 File Offset: 0x0006BE93
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040010B1 RID: 4273
	public Shader SCShader;

	// Token: 0x040010B2 RID: 4274
	private float TimeX = 1f;

	// Token: 0x040010B3 RID: 4275
	public Color Pencil_Color = new Color(1f, 1f, 1f, 1f);

	// Token: 0x040010B4 RID: 4276
	[Range(0.0001f, 0.0022f)]
	public float Pencil_Size = 0.0008f;

	// Token: 0x040010B5 RID: 4277
	[Range(0f, 2f)]
	public float Pencil_Correction = 0.76f;

	// Token: 0x040010B6 RID: 4278
	[Range(0f, 1f)]
	public float Intensity = 1f;

	// Token: 0x040010B7 RID: 4279
	[Range(0f, 2f)]
	public float Speed_Animation = 1f;

	// Token: 0x040010B8 RID: 4280
	[Range(0f, 1f)]
	public float Corner_Lose = 0.5f;

	// Token: 0x040010B9 RID: 4281
	[Range(0f, 1f)]
	public float Fade_Paper_to_BackColor = 0.2f;

	// Token: 0x040010BA RID: 4282
	[Range(0f, 1f)]
	public float Fade_With_Original = 1f;

	// Token: 0x040010BB RID: 4283
	public Color Back_Color = new Color(0.175f, 0.402f, 0.687f, 1f);

	// Token: 0x040010BC RID: 4284
	private Material SCMaterial;

	// Token: 0x040010BD RID: 4285
	private Texture2D Texture2;
}
