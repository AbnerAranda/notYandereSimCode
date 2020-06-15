using System;
using UnityEngine;

// Token: 0x0200016E RID: 366
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Distortion/Half_Sphere")]
public class CameraFilterPack_Distortion_Half_Sphere : MonoBehaviour
{
	// Token: 0x1700028D RID: 653
	// (get) Token: 0x06000DA7 RID: 3495 RVA: 0x0006CC0E File Offset: 0x0006AE0E
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

	// Token: 0x06000DA8 RID: 3496 RVA: 0x0006CC42 File Offset: 0x0006AE42
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Distortion_Half_Sphere");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000DA9 RID: 3497 RVA: 0x0006CC64 File Offset: 0x0006AE64
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
			this.material.SetFloat("_SphereSize", this.SphereSize);
			this.material.SetFloat("_SpherePositionX", this.SpherePositionX);
			this.material.SetFloat("_SpherePositionY", this.SpherePositionY);
			this.material.SetFloat("_Strength", this.Strength);
			this.material.SetVector("_ScreenResolution", new Vector2((float)Screen.width, (float)Screen.height));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000DAA RID: 3498 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000DAB RID: 3499 RVA: 0x0006CD55 File Offset: 0x0006AF55
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001075 RID: 4213
	public Shader SCShader;

	// Token: 0x04001076 RID: 4214
	private float TimeX = 1f;

	// Token: 0x04001077 RID: 4215
	[Range(1f, 6f)]
	private Material SCMaterial;

	// Token: 0x04001078 RID: 4216
	public float SphereSize = 2.5f;

	// Token: 0x04001079 RID: 4217
	[Range(-1f, 1f)]
	public float SpherePositionX;

	// Token: 0x0400107A RID: 4218
	[Range(-1f, 1f)]
	public float SpherePositionY;

	// Token: 0x0400107B RID: 4219
	[Range(1f, 10f)]
	public float Strength = 5f;
}
