using System;
using UnityEngine;

// Token: 0x02000211 RID: 529
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/TV/Vintage")]
public class CameraFilterPack_TV_Vintage : MonoBehaviour
{
	// Token: 0x17000330 RID: 816
	// (get) Token: 0x0600119D RID: 4509 RVA: 0x0007E212 File Offset: 0x0007C412
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

	// Token: 0x0600119E RID: 4510 RVA: 0x0007E246 File Offset: 0x0007C446
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/TV_Vintage");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x0600119F RID: 4511 RVA: 0x0007E268 File Offset: 0x0007C468
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
			this.material.SetFloat("_Distortion", this.Distortion);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x060011A0 RID: 4512 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x060011A1 RID: 4513 RVA: 0x0007E2EE File Offset: 0x0007C4EE
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040014B4 RID: 5300
	public Shader SCShader;

	// Token: 0x040014B5 RID: 5301
	private float TimeX = 1f;

	// Token: 0x040014B6 RID: 5302
	[Range(1f, 10f)]
	public float Distortion = 1f;

	// Token: 0x040014B7 RID: 5303
	private Material SCMaterial;
}
