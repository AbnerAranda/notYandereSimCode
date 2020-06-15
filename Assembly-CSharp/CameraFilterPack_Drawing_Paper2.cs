using System;
using UnityEngine;

// Token: 0x0200018D RID: 397
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Drawing/Paper2")]
public class CameraFilterPack_Drawing_Paper2 : MonoBehaviour
{
	// Token: 0x170002AC RID: 684
	// (get) Token: 0x06000E61 RID: 3681 RVA: 0x0006F9CA File Offset: 0x0006DBCA
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

	// Token: 0x06000E62 RID: 3682 RVA: 0x0006F9FE File Offset: 0x0006DBFE
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_Paper3") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/Drawing_Paper2");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000E63 RID: 3683 RVA: 0x0006FA34 File Offset: 0x0006DC34
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

	// Token: 0x06000E64 RID: 3684 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000E65 RID: 3685 RVA: 0x0006FB83 File Offset: 0x0006DD83
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001132 RID: 4402
	public Shader SCShader;

	// Token: 0x04001133 RID: 4403
	private float TimeX = 1f;

	// Token: 0x04001134 RID: 4404
	public Color Pencil_Color = new Color(0f, 0.371f, 0.78f, 1f);

	// Token: 0x04001135 RID: 4405
	[Range(0.0001f, 0.0022f)]
	public float Pencil_Size = 0.0008f;

	// Token: 0x04001136 RID: 4406
	[Range(0f, 2f)]
	public float Pencil_Correction = 0.76f;

	// Token: 0x04001137 RID: 4407
	[Range(0f, 1f)]
	public float Intensity = 1f;

	// Token: 0x04001138 RID: 4408
	[Range(0f, 2f)]
	public float Speed_Animation = 1f;

	// Token: 0x04001139 RID: 4409
	[Range(0f, 1f)]
	public float Corner_Lose = 0.85f;

	// Token: 0x0400113A RID: 4410
	[Range(0f, 1f)]
	public float Fade_Paper_to_BackColor;

	// Token: 0x0400113B RID: 4411
	[Range(0f, 1f)]
	public float Fade_With_Original = 1f;

	// Token: 0x0400113C RID: 4412
	public Color Back_Color = new Color(1f, 1f, 1f, 1f);

	// Token: 0x0400113D RID: 4413
	private Material SCMaterial;

	// Token: 0x0400113E RID: 4414
	private Texture2D Texture2;
}
