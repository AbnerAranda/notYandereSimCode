using System;
using UnityEngine;

// Token: 0x02000198 RID: 408
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Vision/Eyes 1")]
public class CameraFilterPack_EyesVision_1 : MonoBehaviour
{
	// Token: 0x170002B7 RID: 695
	// (get) Token: 0x06000EA4 RID: 3748 RVA: 0x00070ABA File Offset: 0x0006ECBA
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

	// Token: 0x06000EA5 RID: 3749 RVA: 0x00070AEE File Offset: 0x0006ECEE
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_eyes_vision_1") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/EyesVision_1");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000EA6 RID: 3750 RVA: 0x00070B24 File Offset: 0x0006ED24
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

	// Token: 0x06000EA7 RID: 3751 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000EA8 RID: 3752 RVA: 0x00070C05 File Offset: 0x0006EE05
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001179 RID: 4473
	public Shader SCShader;

	// Token: 0x0400117A RID: 4474
	private float TimeX = 1f;

	// Token: 0x0400117B RID: 4475
	[Range(1f, 32f)]
	public float _EyeWave = 15f;

	// Token: 0x0400117C RID: 4476
	[Range(0f, 10f)]
	public float _EyeSpeed = 1f;

	// Token: 0x0400117D RID: 4477
	[Range(0f, 8f)]
	public float _EyeMove = 2f;

	// Token: 0x0400117E RID: 4478
	[Range(0f, 1f)]
	public float _EyeBlink = 1f;

	// Token: 0x0400117F RID: 4479
	private Material SCMaterial;

	// Token: 0x04001180 RID: 4480
	private Texture2D Texture2;
}
