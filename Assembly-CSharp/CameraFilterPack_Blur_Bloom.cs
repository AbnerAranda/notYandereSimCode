using System;
using UnityEngine;

// Token: 0x02000139 RID: 313
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Blur/Bloom")]
public class CameraFilterPack_Blur_Bloom : MonoBehaviour
{
	// Token: 0x17000258 RID: 600
	// (get) Token: 0x06000C67 RID: 3175 RVA: 0x00067691 File Offset: 0x00065891
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

	// Token: 0x06000C68 RID: 3176 RVA: 0x000676C5 File Offset: 0x000658C5
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Blur_Bloom");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000C69 RID: 3177 RVA: 0x000676E8 File Offset: 0x000658E8
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
			this.material.SetFloat("_Amount", this.Amount);
			this.material.SetFloat("_Glow", this.Glow);
			this.material.SetVector("_ScreenResolution", new Vector2((float)Screen.width, (float)Screen.height));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000C6A RID: 3178 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000C6B RID: 3179 RVA: 0x000677AD File Offset: 0x000659AD
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000F3A RID: 3898
	public Shader SCShader;

	// Token: 0x04000F3B RID: 3899
	private float TimeX = 1f;

	// Token: 0x04000F3C RID: 3900
	private Material SCMaterial;

	// Token: 0x04000F3D RID: 3901
	[Range(0f, 10f)]
	public float Amount = 4.5f;

	// Token: 0x04000F3E RID: 3902
	[Range(0f, 1f)]
	public float Glow = 0.5f;
}
