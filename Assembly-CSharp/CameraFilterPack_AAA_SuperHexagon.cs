using System;
using UnityEngine;

// Token: 0x0200010E RID: 270
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/AAA/Super Hexagon")]
public class CameraFilterPack_AAA_SuperHexagon : MonoBehaviour
{
	// Token: 0x1700022D RID: 557
	// (get) Token: 0x06000B26 RID: 2854 RVA: 0x00061700 File Offset: 0x0005F900
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

	// Token: 0x06000B27 RID: 2855 RVA: 0x00061734 File Offset: 0x0005F934
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/AAA_Super_Hexagon");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000B28 RID: 2856 RVA: 0x00061758 File Offset: 0x0005F958
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
			this.material.SetFloat("_Value", this.HexaSize);
			this.material.SetFloat("_PositionX", this.center.x);
			this.material.SetFloat("_PositionY", this.center.y);
			this.material.SetFloat("_Radius", this.Radius);
			this.material.SetFloat("_BorderSize", this._BorderSize);
			this.material.SetColor("_BorderColor", this._BorderColor);
			this.material.SetColor("_HexaColor", this._HexaColor);
			this.material.SetFloat("_AlphaHexa", this._AlphaHexa);
			this.material.SetFloat("_SpotSize", this._SpotSize);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000B29 RID: 2857 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000B2A RID: 2858 RVA: 0x000618C8 File Offset: 0x0005FAC8
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000DAF RID: 3503
	public Shader SCShader;

	// Token: 0x04000DB0 RID: 3504
	[Range(0f, 1f)]
	public float _AlphaHexa = 1f;

	// Token: 0x04000DB1 RID: 3505
	private float TimeX = 1f;

	// Token: 0x04000DB2 RID: 3506
	private Material SCMaterial;

	// Token: 0x04000DB3 RID: 3507
	[Range(0.2f, 10f)]
	public float HexaSize = 2.5f;

	// Token: 0x04000DB4 RID: 3508
	public float _BorderSize = 1f;

	// Token: 0x04000DB5 RID: 3509
	public Color _BorderColor = new Color(0.75f, 0.75f, 1f, 1f);

	// Token: 0x04000DB6 RID: 3510
	public Color _HexaColor = new Color(0f, 0.5f, 1f, 1f);

	// Token: 0x04000DB7 RID: 3511
	public float _SpotSize = 2.5f;

	// Token: 0x04000DB8 RID: 3512
	public Vector2 center = new Vector2(0.5f, 0.5f);

	// Token: 0x04000DB9 RID: 3513
	public float Radius = 0.25f;
}
