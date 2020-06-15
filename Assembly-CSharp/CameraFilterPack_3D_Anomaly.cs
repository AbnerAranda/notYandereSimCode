using System;
using UnityEngine;

// Token: 0x020000FB RID: 251
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/3D/Anomaly")]
public class CameraFilterPack_3D_Anomaly : MonoBehaviour
{
	// Token: 0x1700021A RID: 538
	// (get) Token: 0x06000AB4 RID: 2740 RVA: 0x0005E572 File Offset: 0x0005C772
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

	// Token: 0x06000AB5 RID: 2741 RVA: 0x0005E5A6 File Offset: 0x0005C7A6
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/3D_Anomaly");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000AB6 RID: 2742 RVA: 0x0005E5C8 File Offset: 0x0005C7C8
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
			this.material.SetFloat("_Value2", this.Intensity);
			this.material.SetFloat("Anomaly_Distortion", this.Anomaly_Distortion);
			this.material.SetFloat("Anomaly_Distortion_Size", this.Anomaly_Distortion_Size);
			this.material.SetFloat("Anomaly_Intensity", this.Anomaly_Intensity);
			this.material.SetFloat("_Visualize", (float)(this._Visualize ? 1 : 0));
			this.material.SetFloat("_FixDistance", this._FixDistance);
			this.material.SetFloat("Anomaly_Near", this.Anomaly_Near);
			this.material.SetFloat("Anomaly_Far", this.Anomaly_Far);
			this.material.SetFloat("Anomaly_With_Obj", this.AnomalyWithoutObject);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			base.GetComponent<Camera>().depthTextureMode = DepthTextureMode.Depth;
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000AB7 RID: 2743 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000AB8 RID: 2744 RVA: 0x0005E741 File Offset: 0x0005C941
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000CBA RID: 3258
	public Shader SCShader;

	// Token: 0x04000CBB RID: 3259
	public bool _Visualize;

	// Token: 0x04000CBC RID: 3260
	private float TimeX = 1f;

	// Token: 0x04000CBD RID: 3261
	private Material SCMaterial;

	// Token: 0x04000CBE RID: 3262
	[Range(0f, 100f)]
	public float _FixDistance = 23f;

	// Token: 0x04000CBF RID: 3263
	[Range(-0.5f, 0.99f)]
	public float Anomaly_Near = 0.045f;

	// Token: 0x04000CC0 RID: 3264
	[Range(0f, 1f)]
	public float Anomaly_Far = 0.11f;

	// Token: 0x04000CC1 RID: 3265
	[Range(0f, 2f)]
	public float Intensity = 1f;

	// Token: 0x04000CC2 RID: 3266
	[Range(0f, 1f)]
	public float AnomalyWithoutObject = 1f;

	// Token: 0x04000CC3 RID: 3267
	[Range(0.1f, 1f)]
	public float Anomaly_Distortion = 0.25f;

	// Token: 0x04000CC4 RID: 3268
	[Range(4f, 64f)]
	public float Anomaly_Distortion_Size = 12f;

	// Token: 0x04000CC5 RID: 3269
	[Range(-4f, 8f)]
	public float Anomaly_Intensity = 2f;
}
