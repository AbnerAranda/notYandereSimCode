using System;
using UnityEngine;

// Token: 0x02000113 RID: 275
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Weather/Fog")]
public class CameraFilterPack_Atmosphere_Fog : MonoBehaviour
{
	// Token: 0x17000232 RID: 562
	// (get) Token: 0x06000B44 RID: 2884 RVA: 0x00061F8B File Offset: 0x0006018B
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

	// Token: 0x06000B45 RID: 2885 RVA: 0x00061FBF File Offset: 0x000601BF
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_Atmosphere_Rain_FX") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/Atmosphere_Fog");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000B46 RID: 2886 RVA: 0x00061FF8 File Offset: 0x000601F8
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
			this.material.SetFloat("_Near", this._Near);
			this.material.SetFloat("_Far", this._Far);
			this.material.SetColor("_ColorRGB", this.FogColor);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			base.GetComponent<Camera>().depthTextureMode = DepthTextureMode.Depth;
			this.material.SetTexture("Texture2", this.Texture2);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000B47 RID: 2887 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000B48 RID: 2888 RVA: 0x00062112 File Offset: 0x00060312
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000DD4 RID: 3540
	public Shader SCShader;

	// Token: 0x04000DD5 RID: 3541
	private float TimeX = 1f;

	// Token: 0x04000DD6 RID: 3542
	private Material SCMaterial;

	// Token: 0x04000DD7 RID: 3543
	[Range(0f, 1f)]
	public float _Near;

	// Token: 0x04000DD8 RID: 3544
	[Range(0f, 1f)]
	public float _Far = 0.05f;

	// Token: 0x04000DD9 RID: 3545
	public Color FogColor = new Color(0.4f, 0.4f, 0.4f, 1f);

	// Token: 0x04000DDA RID: 3546
	[Range(0f, 1f)]
	public float Fade = 1f;

	// Token: 0x04000DDB RID: 3547
	public static Color ChangeColorRGB;

	// Token: 0x04000DDC RID: 3548
	private Texture2D Texture2;
}
