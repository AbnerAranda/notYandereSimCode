using System;
using UnityEngine;

// Token: 0x02000148 RID: 328
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Blur/Tilt_Shift_V")]
public class CameraFilterPack_Blur_Tilt_Shift_V : MonoBehaviour
{
	// Token: 0x17000267 RID: 615
	// (get) Token: 0x06000CC1 RID: 3265 RVA: 0x00068F78 File Offset: 0x00067178
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

	// Token: 0x06000CC2 RID: 3266 RVA: 0x00068FAC File Offset: 0x000671AC
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/BlurTiltShift_V");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000CC3 RID: 3267 RVA: 0x00068FD0 File Offset: 0x000671D0
	private void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
	{
		if (!(this.SCShader != null))
		{
			Graphics.Blit(sourceTexture, destTexture);
			return;
		}
		int fastFilter = this.FastFilter;
		this.TimeX += Time.deltaTime;
		if (this.TimeX > 100f)
		{
			this.TimeX = 0f;
		}
		this.material.SetFloat("_TimeX", this.TimeX);
		this.material.SetFloat("_Amount", this.Amount);
		this.material.SetFloat("_Value1", this.Smooth);
		this.material.SetFloat("_Value2", this.Size);
		this.material.SetFloat("_Value3", this.Position);
		int width = sourceTexture.width / fastFilter;
		int height = sourceTexture.height / fastFilter;
		if (this.FastFilter > 1)
		{
			RenderTexture temporary = RenderTexture.GetTemporary(width, height, 0);
			RenderTexture temporary2 = RenderTexture.GetTemporary(width, height, 0);
			temporary.filterMode = FilterMode.Trilinear;
			Graphics.Blit(sourceTexture, temporary, this.material, 2);
			Graphics.Blit(temporary, temporary2, this.material, 0);
			this.material.SetFloat("_Amount", this.Amount * 2f);
			Graphics.Blit(temporary2, temporary, this.material, 2);
			Graphics.Blit(temporary, temporary2, this.material, 0);
			this.material.SetTexture("_MainTex2", temporary2);
			RenderTexture.ReleaseTemporary(temporary);
			RenderTexture.ReleaseTemporary(temporary2);
			Graphics.Blit(sourceTexture, destTexture, this.material, 1);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture, this.material, 0);
	}

	// Token: 0x06000CC4 RID: 3268 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000CC5 RID: 3269 RVA: 0x00069160 File Offset: 0x00067360
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000F97 RID: 3991
	public Shader SCShader;

	// Token: 0x04000F98 RID: 3992
	private float TimeX = 1f;

	// Token: 0x04000F99 RID: 3993
	private Material SCMaterial;

	// Token: 0x04000F9A RID: 3994
	[Range(0f, 20f)]
	public float Amount = 3f;

	// Token: 0x04000F9B RID: 3995
	[Range(2f, 16f)]
	public int FastFilter = 8;

	// Token: 0x04000F9C RID: 3996
	[Range(0f, 1f)]
	public float Smooth = 0.5f;

	// Token: 0x04000F9D RID: 3997
	[Range(0f, 1f)]
	public float Size = 0.5f;

	// Token: 0x04000F9E RID: 3998
	[Range(-1f, 1f)]
	public float Position = 0.5f;
}
