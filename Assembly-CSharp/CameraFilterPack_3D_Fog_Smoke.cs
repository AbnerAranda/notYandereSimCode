using System;
using UnityEngine;

// Token: 0x02000100 RID: 256
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/3D/Fog_Smoke")]
public class CameraFilterPack_3D_Fog_Smoke : MonoBehaviour
{
	// Token: 0x1700021F RID: 543
	// (get) Token: 0x06000AD2 RID: 2770 RVA: 0x0005F23F File Offset: 0x0005D43F
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

	// Token: 0x06000AD3 RID: 2771 RVA: 0x0005F273 File Offset: 0x0005D473
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_3D_Myst1") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/3D_Myst");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000AD4 RID: 2772 RVA: 0x0005F2AC File Offset: 0x0005D4AC
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
			if (this.AutoAnimatedNear)
			{
				this._Distance += Time.deltaTime * this.AutoAnimatedNearSpeed;
				if (this._Distance > 1f)
				{
					this._Distance = -1f;
				}
				if (this._Distance < -1f)
				{
					this._Distance = 1f;
				}
				this.material.SetFloat("_Near", this._Distance);
			}
			else
			{
				this.material.SetFloat("_Near", this._Distance);
			}
			this.material.SetFloat("_Far", this._Size);
			this.material.SetFloat("_Visualize", (float)(this._Visualize ? 1 : 0));
			this.material.SetFloat("_FixDistance", this._FixDistance);
			this.material.SetFloat("_DistortionLevel", this.DistortionLevel * 28f);
			this.material.SetFloat("_DistortionSize", this.DistortionSize * 16f);
			this.material.SetFloat("_LightIntensity", this.LightIntensity * 64f);
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

	// Token: 0x06000AD5 RID: 2773 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000AD6 RID: 2774 RVA: 0x0005F4AD File Offset: 0x0005D6AD
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000CF8 RID: 3320
	public Shader SCShader;

	// Token: 0x04000CF9 RID: 3321
	public bool _Visualize;

	// Token: 0x04000CFA RID: 3322
	private float TimeX = 1f;

	// Token: 0x04000CFB RID: 3323
	private Material SCMaterial;

	// Token: 0x04000CFC RID: 3324
	[Range(0f, 100f)]
	public float _FixDistance = 1f;

	// Token: 0x04000CFD RID: 3325
	[Range(-0.99f, 0.99f)]
	public float _Distance = 0.5f;

	// Token: 0x04000CFE RID: 3326
	[Range(0f, 0.5f)]
	public float _Size = 0.1f;

	// Token: 0x04000CFF RID: 3327
	[Range(0f, 10f)]
	public float DistortionLevel = 1.2f;

	// Token: 0x04000D00 RID: 3328
	[Range(0.1f, 10f)]
	public float DistortionSize = 1.4f;

	// Token: 0x04000D01 RID: 3329
	[Range(-2f, 4f)]
	public float LightIntensity = 0.08f;

	// Token: 0x04000D02 RID: 3330
	public bool AutoAnimatedNear;

	// Token: 0x04000D03 RID: 3331
	[Range(-5f, 5f)]
	public float AutoAnimatedNearSpeed = 0.5f;

	// Token: 0x04000D04 RID: 3332
	private Texture2D Texture2;

	// Token: 0x04000D05 RID: 3333
	public static Color ChangeColorRGB;
}
