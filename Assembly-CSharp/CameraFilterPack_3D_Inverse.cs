using System;
using UnityEngine;

// Token: 0x02000102 RID: 258
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/3D/Inverse")]
public class CameraFilterPack_3D_Inverse : MonoBehaviour
{
	// Token: 0x17000221 RID: 545
	// (get) Token: 0x06000ADE RID: 2782 RVA: 0x0005F7B3 File Offset: 0x0005D9B3
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

	// Token: 0x06000ADF RID: 2783 RVA: 0x0005F7E7 File Offset: 0x0005D9E7
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/3D_Inverse");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000AE0 RID: 2784 RVA: 0x0005F808 File Offset: 0x0005DA08
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
			this.material.SetFloat("_LightIntensity", this.LightIntensity);
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

	// Token: 0x06000AE1 RID: 2785 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000AE2 RID: 2786 RVA: 0x0005F9B5 File Offset: 0x0005DBB5
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000D14 RID: 3348
	public Shader SCShader;

	// Token: 0x04000D15 RID: 3349
	private float TimeX = 1f;

	// Token: 0x04000D16 RID: 3350
	public bool _Visualize;

	// Token: 0x04000D17 RID: 3351
	private Material SCMaterial;

	// Token: 0x04000D18 RID: 3352
	[Range(0f, 100f)]
	public float _FixDistance = 1.5f;

	// Token: 0x04000D19 RID: 3353
	[Range(-0.99f, 0.99f)]
	public float _Distance = 0.4f;

	// Token: 0x04000D1A RID: 3354
	[Range(0f, 0.5f)]
	public float _Size = 0.5f;

	// Token: 0x04000D1B RID: 3355
	[Range(0f, 1f)]
	public float LightIntensity = 1f;

	// Token: 0x04000D1C RID: 3356
	public bool AutoAnimatedNear;

	// Token: 0x04000D1D RID: 3357
	[Range(-5f, 5f)]
	public float AutoAnimatedNearSpeed = 0.5f;

	// Token: 0x04000D1E RID: 3358
	public static Color ChangeColorRGB;
}
