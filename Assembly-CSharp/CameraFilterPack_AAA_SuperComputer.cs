using System;
using UnityEngine;

// Token: 0x0200010D RID: 269
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/AAA/Super Computer")]
public class CameraFilterPack_AAA_SuperComputer : MonoBehaviour
{
	// Token: 0x1700022C RID: 556
	// (get) Token: 0x06000B20 RID: 2848 RVA: 0x00061486 File Offset: 0x0005F686
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

	// Token: 0x06000B21 RID: 2849 RVA: 0x000614BA File Offset: 0x0005F6BA
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/AAA_Super_Computer");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000B22 RID: 2850 RVA: 0x000614DC File Offset: 0x0005F6DC
	private void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
	{
		if (this.SCShader != null)
		{
			this.TimeX += Time.deltaTime / 4f;
			if (this.TimeX > 100f)
			{
				this.TimeX = 0f;
			}
			this.material.SetFloat("_TimeX", this.TimeX);
			this.material.SetFloat("_Value", this.ShapeFormula);
			this.material.SetFloat("_Value2", this.Shape);
			this.material.SetFloat("_PositionX", this.center.x);
			this.material.SetFloat("_PositionY", this.center.y);
			this.material.SetFloat("_Radius", this.Radius);
			this.material.SetFloat("_BorderSize", this._BorderSize);
			this.material.SetColor("_BorderColor", this._BorderColor);
			this.material.SetFloat("_AlphaHexa", this._AlphaHexa);
			this.material.SetFloat("_SpotSize", this._SpotSize);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000B23 RID: 2851 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000B24 RID: 2852 RVA: 0x00061652 File Offset: 0x0005F852
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000DA4 RID: 3492
	public Shader SCShader;

	// Token: 0x04000DA5 RID: 3493
	[Range(0f, 1f)]
	public float _AlphaHexa = 1f;

	// Token: 0x04000DA6 RID: 3494
	private float TimeX = 1f;

	// Token: 0x04000DA7 RID: 3495
	private Material SCMaterial;

	// Token: 0x04000DA8 RID: 3496
	[Range(-20f, 20f)]
	public float ShapeFormula = 10f;

	// Token: 0x04000DA9 RID: 3497
	[Range(0f, 6f)]
	public float Shape = 1f;

	// Token: 0x04000DAA RID: 3498
	[Range(-4f, 4f)]
	public float _BorderSize = 1f;

	// Token: 0x04000DAB RID: 3499
	public Color _BorderColor = new Color(0f, 0.2f, 1f, 1f);

	// Token: 0x04000DAC RID: 3500
	public float _SpotSize = 2.5f;

	// Token: 0x04000DAD RID: 3501
	public Vector2 center = new Vector2(0f, 0f);

	// Token: 0x04000DAE RID: 3502
	public float Radius = 0.77f;
}
