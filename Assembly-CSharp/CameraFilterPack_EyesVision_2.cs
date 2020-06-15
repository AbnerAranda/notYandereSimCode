using System;
using UnityEngine;

// Token: 0x02000199 RID: 409
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Vision/Eyes 2")]
public class CameraFilterPack_EyesVision_2 : MonoBehaviour
{
	// Token: 0x170002B8 RID: 696
	// (get) Token: 0x06000EAA RID: 3754 RVA: 0x00070C5E File Offset: 0x0006EE5E
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

	// Token: 0x06000EAB RID: 3755 RVA: 0x00070C92 File Offset: 0x0006EE92
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_eyes_vision_2") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/EyesVision_2");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000EAC RID: 3756 RVA: 0x00070CC8 File Offset: 0x0006EEC8
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
			this.material.SetFloat("_Value", this._EyeWave);
			this.material.SetFloat("_Value2", this._EyeSpeed);
			this.material.SetFloat("_Value3", this._EyeMove);
			this.material.SetFloat("_Value4", this._EyeBlink);
			this.material.SetTexture("_MainTex2", this.Texture2);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000EAD RID: 3757 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000EAE RID: 3758 RVA: 0x00070DA9 File Offset: 0x0006EFA9
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001181 RID: 4481
	public Shader SCShader;

	// Token: 0x04001182 RID: 4482
	private float TimeX = 1f;

	// Token: 0x04001183 RID: 4483
	[Range(1f, 32f)]
	public float _EyeWave = 15f;

	// Token: 0x04001184 RID: 4484
	[Range(0f, 10f)]
	public float _EyeSpeed = 1f;

	// Token: 0x04001185 RID: 4485
	[Range(0f, 8f)]
	public float _EyeMove = 2f;

	// Token: 0x04001186 RID: 4486
	[Range(0f, 1f)]
	public float _EyeBlink = 1f;

	// Token: 0x04001187 RID: 4487
	private Material SCMaterial;

	// Token: 0x04001188 RID: 4488
	private Texture2D Texture2;
}
