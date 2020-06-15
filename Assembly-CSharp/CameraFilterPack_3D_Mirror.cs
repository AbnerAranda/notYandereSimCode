using System;
using UnityEngine;

// Token: 0x02000104 RID: 260
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/3D/Mirror")]
public class CameraFilterPack_3D_Mirror : MonoBehaviour
{
	// Token: 0x17000223 RID: 547
	// (get) Token: 0x06000AEA RID: 2794 RVA: 0x0005FCAC File Offset: 0x0005DEAC
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

	// Token: 0x06000AEB RID: 2795 RVA: 0x0005FCE0 File Offset: 0x0005DEE0
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/3D_Mirror");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000AEC RID: 2796 RVA: 0x0005FD04 File Offset: 0x0005DF04
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
			this.material.SetFloat("Fade", this.Fade);
			this.material.SetFloat("Lightning", this.Lightning);
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

	// Token: 0x06000AED RID: 2797 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000AEE RID: 2798 RVA: 0x0005FEC7 File Offset: 0x0005E0C7
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000D2B RID: 3371
	public Shader SCShader;

	// Token: 0x04000D2C RID: 3372
	public bool _Visualize;

	// Token: 0x04000D2D RID: 3373
	private float TimeX = 1f;

	// Token: 0x04000D2E RID: 3374
	private Material SCMaterial;

	// Token: 0x04000D2F RID: 3375
	[Range(0f, 100f)]
	public float _FixDistance = 1.5f;

	// Token: 0x04000D30 RID: 3376
	[Range(-0.99f, 0.99f)]
	public float _Distance = 0.4f;

	// Token: 0x04000D31 RID: 3377
	[Range(0f, 0.5f)]
	public float _Size = 0.5f;

	// Token: 0x04000D32 RID: 3378
	[Range(0f, 1f)]
	public float Fade = 1f;

	// Token: 0x04000D33 RID: 3379
	[Range(0f, 2f)]
	public float Lightning = 2f;

	// Token: 0x04000D34 RID: 3380
	public bool AutoAnimatedNear;

	// Token: 0x04000D35 RID: 3381
	[Range(-5f, 5f)]
	public float AutoAnimatedNearSpeed = 0.5f;

	// Token: 0x04000D36 RID: 3382
	public static Color ChangeColorRGB;
}
