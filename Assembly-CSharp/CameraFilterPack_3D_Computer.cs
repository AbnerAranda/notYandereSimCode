using System;
using UnityEngine;

// Token: 0x020000FE RID: 254
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/3D/Computer")]
public class CameraFilterPack_3D_Computer : MonoBehaviour
{
	// Token: 0x1700021D RID: 541
	// (get) Token: 0x06000AC6 RID: 2758 RVA: 0x0005ECED File Offset: 0x0005CEED
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

	// Token: 0x06000AC7 RID: 2759 RVA: 0x0005ED21 File Offset: 0x0005CF21
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_3D_Computer1") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/3D_Computer");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000AC8 RID: 2760 RVA: 0x0005ED58 File Offset: 0x0005CF58
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

	// Token: 0x06000AC9 RID: 2761 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000ACA RID: 2762 RVA: 0x0005EEE4 File Offset: 0x0005D0E4
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000CDF RID: 3295
	public Shader SCShader;

	// Token: 0x04000CE0 RID: 3296
	private float TimeX = 1f;

	// Token: 0x04000CE1 RID: 3297
	public bool _Visualize;

	// Token: 0x04000CE2 RID: 3298
	private Material SCMaterial;

	// Token: 0x04000CE3 RID: 3299
	[Range(0f, 100f)]
	public float _FixDistance = 2f;

	// Token: 0x04000CE4 RID: 3300
	[Range(-5f, 5f)]
	public float LightIntensity = 1f;

	// Token: 0x04000CE5 RID: 3301
	[Range(0f, 8f)]
	public float MatrixSize = 2f;

	// Token: 0x04000CE6 RID: 3302
	[Range(-4f, 4f)]
	public float MatrixSpeed = 0.1f;

	// Token: 0x04000CE7 RID: 3303
	[Range(0f, 1f)]
	public float Fade = 1f;

	// Token: 0x04000CE8 RID: 3304
	public Color _MatrixColor = new Color(0f, 0.5f, 1f, 1f);

	// Token: 0x04000CE9 RID: 3305
	public static Color ChangeColorRGB;

	// Token: 0x04000CEA RID: 3306
	private Texture2D Texture2;
}
