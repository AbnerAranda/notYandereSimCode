using System;
using UnityEngine;

// Token: 0x02000116 RID: 278
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Weather/Rain_Pro_3D")]
public class CameraFilterPack_Atmosphere_Rain_Pro_3D : MonoBehaviour
{
	// Token: 0x17000235 RID: 565
	// (get) Token: 0x06000B56 RID: 2902 RVA: 0x00062626 File Offset: 0x00060826
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

	// Token: 0x06000B57 RID: 2903 RVA: 0x0006265A File Offset: 0x0006085A
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_Atmosphere_Rain_FX") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/Atmosphere_Rain_Pro_3D");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000B58 RID: 2904 RVA: 0x00062690 File Offset: 0x00060890
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
			this.material.SetFloat("_Value", this.Fade);
			this.material.SetFloat("_Value2", this.Intensity);
			if (this.DirectionFollowCameraZ)
			{
				float z = base.GetComponent<Camera>().transform.rotation.z;
				if (z > 0f && z < 360f)
				{
					this.material.SetFloat("_Value3", z);
				}
				if (z < 0f)
				{
					this.material.SetFloat("_Value3", z);
				}
			}
			else
			{
				this.material.SetFloat("_Value3", this.DirectionX);
			}
			this.material.SetFloat("_Value4", this.Speed);
			this.material.SetFloat("_Value5", this.Size);
			this.material.SetFloat("_Value6", this.Distortion);
			this.material.SetFloat("_Value7", this.StormFlashOnOff);
			this.material.SetFloat("_Value8", this.DropOnOff);
			this.material.SetFloat("_FixDistance", this._FixDistance);
			this.material.SetFloat("_Visualize", (float)(this._Visualize ? 1 : 0));
			this.material.SetFloat("Drop_Near", this.Drop_Near);
			this.material.SetFloat("Drop_Far", this.Drop_Far);
			this.material.SetFloat("Drop_With_Obj", 1f - this.Drop_With_Obj);
			this.material.SetFloat("Myst", this.Myst);
			this.material.SetColor("Myst_Color", this.Myst_Color);
			this.material.SetFloat("Drop_Floor_Fluid", this.Drop_Floor_Fluid);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			this.material.SetTexture("Texture2", this.Texture2);
			base.GetComponent<Camera>().depthTextureMode = DepthTextureMode.Depth;
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000B59 RID: 2905 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000B5A RID: 2906 RVA: 0x00062919 File Offset: 0x00060B19
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000DF4 RID: 3572
	public Shader SCShader;

	// Token: 0x04000DF5 RID: 3573
	public bool _Visualize;

	// Token: 0x04000DF6 RID: 3574
	private float TimeX = 1f;

	// Token: 0x04000DF7 RID: 3575
	private Material SCMaterial;

	// Token: 0x04000DF8 RID: 3576
	[Range(0f, 100f)]
	public float _FixDistance = 3f;

	// Token: 0x04000DF9 RID: 3577
	[Range(0f, 1f)]
	public float Fade = 1f;

	// Token: 0x04000DFA RID: 3578
	[Range(0f, 2f)]
	public float Intensity = 0.5f;

	// Token: 0x04000DFB RID: 3579
	public bool DirectionFollowCameraZ;

	// Token: 0x04000DFC RID: 3580
	[Range(-0.45f, 0.45f)]
	public float DirectionX = 0.12f;

	// Token: 0x04000DFD RID: 3581
	[Range(0.4f, 2f)]
	public float Size = 1.5f;

	// Token: 0x04000DFE RID: 3582
	[Range(0f, 0.5f)]
	public float Speed = 0.275f;

	// Token: 0x04000DFF RID: 3583
	[Range(0f, 0.5f)]
	public float Distortion = 0.025f;

	// Token: 0x04000E00 RID: 3584
	[Range(0f, 1f)]
	public float StormFlashOnOff = 1f;

	// Token: 0x04000E01 RID: 3585
	[Range(0f, 1f)]
	public float DropOnOff = 1f;

	// Token: 0x04000E02 RID: 3586
	[Range(-0.5f, 0.99f)]
	public float Drop_Near;

	// Token: 0x04000E03 RID: 3587
	[Range(0f, 1f)]
	public float Drop_Far = 0.5f;

	// Token: 0x04000E04 RID: 3588
	[Range(0f, 1f)]
	public float Drop_With_Obj = 0.2f;

	// Token: 0x04000E05 RID: 3589
	[Range(0f, 1f)]
	public float Myst = 0.1f;

	// Token: 0x04000E06 RID: 3590
	[Range(0f, 1f)]
	public float Drop_Floor_Fluid;

	// Token: 0x04000E07 RID: 3591
	public Color Myst_Color = new Color(0.5f, 0.5f, 0.5f, 1f);

	// Token: 0x04000E08 RID: 3592
	private Texture2D Texture2;
}
