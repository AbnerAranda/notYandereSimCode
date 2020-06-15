using System;
using UnityEngine;

// Token: 0x020001EA RID: 490
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Pixelisation/Deep OilPaint HQ")]
public class CameraFilterPack_Pixelisation_DeepOilPaintHQ : MonoBehaviour
{
	// Token: 0x17000309 RID: 777
	// (get) Token: 0x060010B2 RID: 4274 RVA: 0x0007A44D File Offset: 0x0007864D
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

	// Token: 0x060010B3 RID: 4275 RVA: 0x0007A481 File Offset: 0x00078681
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Deep_OilPaintHQ");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x060010B4 RID: 4276 RVA: 0x0007A4A4 File Offset: 0x000786A4
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
			this.material.SetFloat("_FixDistance", this._FixDistance);
			this.material.SetFloat("_LightIntensity", this.Intensity);
			this.material.SetFloat("_Visualize", (float)(this._Visualize ? 1 : 0));
			float farClipPlane = base.GetComponent<Camera>().farClipPlane;
			this.material.SetFloat("_FarCamera", 1000f / farClipPlane);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			base.GetComponent<Camera>().depthTextureMode = DepthTextureMode.Depth;
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x060010B5 RID: 4277 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x060010B6 RID: 4278 RVA: 0x0007A651 File Offset: 0x00078851
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040013C5 RID: 5061
	public Shader SCShader;

	// Token: 0x040013C6 RID: 5062
	private float TimeX = 1f;

	// Token: 0x040013C7 RID: 5063
	public bool _Visualize;

	// Token: 0x040013C8 RID: 5064
	private Material SCMaterial;

	// Token: 0x040013C9 RID: 5065
	[Range(0f, 100f)]
	public float _FixDistance = 1.5f;

	// Token: 0x040013CA RID: 5066
	[Range(-0.99f, 0.99f)]
	public float _Distance = 0.4f;

	// Token: 0x040013CB RID: 5067
	[Range(0f, 0.5f)]
	public float _Size = 0.5f;

	// Token: 0x040013CC RID: 5068
	[Range(0f, 8f)]
	public float Intensity = 1f;

	// Token: 0x040013CD RID: 5069
	public bool AutoAnimatedNear;

	// Token: 0x040013CE RID: 5070
	[Range(-5f, 5f)]
	public float AutoAnimatedNearSpeed = 0.5f;

	// Token: 0x040013CF RID: 5071
	public static Color ChangeColorRGB;
}
