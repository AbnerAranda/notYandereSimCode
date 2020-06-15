using System;
using UnityEngine;

// Token: 0x02000106 RID: 262
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/3D/Scan_Scene")]
public class CameraFilterPack_3D_Scan_Scene : MonoBehaviour
{
	// Token: 0x17000225 RID: 549
	// (get) Token: 0x06000AF6 RID: 2806 RVA: 0x00060237 File Offset: 0x0005E437
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

	// Token: 0x06000AF7 RID: 2807 RVA: 0x0006026B File Offset: 0x0005E46B
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/3D_Scan_Scene");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000AF8 RID: 2808 RVA: 0x0006028C File Offset: 0x0005E48C
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
			if (this.AutoAnimatedNear)
			{
				this._Distance += Time.deltaTime * this.AutoAnimatedNearSpeed;
				if (this._Distance > 1f)
				{
					this._Distance = 0f;
				}
				if (this._Distance < 0f)
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
			this.material.SetColor("_ColorRGB", this.ScanColor);
			this.material.SetFloat("_FixDistance", this._FixDistance);
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

	// Token: 0x06000AF9 RID: 2809 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000AFA RID: 2810 RVA: 0x0006044F File Offset: 0x0005E64F
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000D45 RID: 3397
	public Shader SCShader;

	// Token: 0x04000D46 RID: 3398
	public bool _Visualize;

	// Token: 0x04000D47 RID: 3399
	private float TimeX = 1f;

	// Token: 0x04000D48 RID: 3400
	private Material SCMaterial;

	// Token: 0x04000D49 RID: 3401
	[Range(0f, 100f)]
	public float _FixDistance = 1f;

	// Token: 0x04000D4A RID: 3402
	[Range(0f, 0.99f)]
	public float _Distance = 1f;

	// Token: 0x04000D4B RID: 3403
	[Range(0f, 0.1f)]
	public float _Size = 0.01f;

	// Token: 0x04000D4C RID: 3404
	public bool AutoAnimatedNear;

	// Token: 0x04000D4D RID: 3405
	[Range(-5f, 5f)]
	public float AutoAnimatedNearSpeed = 1f;

	// Token: 0x04000D4E RID: 3406
	public Color ScanColor = new Color(2f, 0f, 0f, 1f);

	// Token: 0x04000D4F RID: 3407
	[Range(0f, 1f)]
	public float Fade = 1f;

	// Token: 0x04000D50 RID: 3408
	public static Color ChangeColorRGB;

	// Token: 0x04000D51 RID: 3409
	private Texture2D Texture2;
}
