using System;
using UnityEngine;

// Token: 0x020000FC RID: 252
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/3D/Binary")]
public class CameraFilterPack_3D_Binary : MonoBehaviour
{
	// Token: 0x1700021B RID: 539
	// (get) Token: 0x06000ABA RID: 2746 RVA: 0x0005E7D2 File Offset: 0x0005C9D2
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

	// Token: 0x06000ABB RID: 2747 RVA: 0x0005E806 File Offset: 0x0005CA06
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_3D_Binary1") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/3D_Binary");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000ABC RID: 2748 RVA: 0x0005E83C File Offset: 0x0005CA3C
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
			this.material.SetFloat("_DepthLevel", this.Fade);
			this.material.SetFloat("_FadeFromBinary", this.FadeFromBinary);
			this.material.SetFloat("_FixDistance", this._FixDistance);
			this.material.SetFloat("_MatrixSize", this.MatrixSize);
			this.material.SetColor("_MatrixColor", this._MatrixColor);
			this.material.SetFloat("_MatrixSpeed", this.MatrixSpeed * 2f);
			this.material.SetFloat("_Visualize", (float)(this._Visualize ? 1 : 0));
			this.material.SetFloat("_LightIntensity", this.LightIntensity);
			this.material.SetTexture("_MainTex2", this.Texture2);
			float farClipPlane = base.GetComponent<Camera>().farClipPlane;
			this.material.SetFloat("_FarCamera", 1000f / farClipPlane);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			base.GetComponent<Camera>().depthTextureMode = DepthTextureMode.Depth;
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000ABD RID: 2749 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000ABE RID: 2750 RVA: 0x0005E9DE File Offset: 0x0005CBDE
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000CC6 RID: 3270
	public Shader SCShader;

	// Token: 0x04000CC7 RID: 3271
	private float TimeX = 1f;

	// Token: 0x04000CC8 RID: 3272
	public bool _Visualize;

	// Token: 0x04000CC9 RID: 3273
	private Material SCMaterial;

	// Token: 0x04000CCA RID: 3274
	[Range(0f, 100f)]
	public float _FixDistance = 2f;

	// Token: 0x04000CCB RID: 3275
	[Range(-5f, 5f)]
	public float LightIntensity;

	// Token: 0x04000CCC RID: 3276
	[Range(0f, 8f)]
	public float MatrixSize = 2f;

	// Token: 0x04000CCD RID: 3277
	[Range(-4f, 4f)]
	public float MatrixSpeed = 1f;

	// Token: 0x04000CCE RID: 3278
	[Range(0f, 1f)]
	public float Fade = 1f;

	// Token: 0x04000CCF RID: 3279
	[Range(0f, 1f)]
	public float FadeFromBinary;

	// Token: 0x04000CD0 RID: 3280
	public Color _MatrixColor = new Color(1f, 0.3f, 0.3f, 1f);

	// Token: 0x04000CD1 RID: 3281
	public static Color ChangeColorRGB;

	// Token: 0x04000CD2 RID: 3282
	private Texture2D Texture2;
}
