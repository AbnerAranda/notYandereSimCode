using System;
using UnityEngine;

// Token: 0x020001FC RID: 508
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/TV/Chromatical")]
public class CameraFilterPack_TV_Chromatical : MonoBehaviour
{
	// Token: 0x1700031B RID: 795
	// (get) Token: 0x0600111F RID: 4383 RVA: 0x0007C559 File Offset: 0x0007A759
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

	// Token: 0x06001120 RID: 4384 RVA: 0x0007C58D File Offset: 0x0007A78D
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/TV_Chromatical");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06001121 RID: 4385 RVA: 0x0007C5B0 File Offset: 0x0007A7B0
	private void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
	{
		if (this.SCShader != null)
		{
			this.TimeX += Time.deltaTime * 2f;
			if (this.TimeX > 100f)
			{
				this.TimeX = 0f;
			}
			this.material.SetFloat("_TimeX", this.TimeX);
			this.material.SetFloat("Fade", this.Fade);
			this.material.SetFloat("Intensity", this.Intensity);
			this.material.SetFloat("Speed", this.Speed);
			this.material.SetVector("_ScreenResolution", new Vector2((float)Screen.width, (float)Screen.height));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06001122 RID: 4386 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06001123 RID: 4387 RVA: 0x0007C691 File Offset: 0x0007A891
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001445 RID: 5189
	public Shader SCShader;

	// Token: 0x04001446 RID: 5190
	private float TimeX = 1f;

	// Token: 0x04001447 RID: 5191
	[Range(0f, 1f)]
	public float Fade = 1f;

	// Token: 0x04001448 RID: 5192
	[Range(0f, 1f)]
	public float Intensity = 1f;

	// Token: 0x04001449 RID: 5193
	[Range(0f, 3f)]
	public float Speed = 1f;

	// Token: 0x0400144A RID: 5194
	private Material SCMaterial;
}
