using System;
using UnityEngine;

// Token: 0x020001E2 RID: 482
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Night Vision/Night Vision 1")]
public class CameraFilterPack_Oculus_NightVision1 : MonoBehaviour
{
	// Token: 0x17000301 RID: 769
	// (get) Token: 0x0600107E RID: 4222 RVA: 0x00079561 File Offset: 0x00077761
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

	// Token: 0x0600107F RID: 4223 RVA: 0x00079595 File Offset: 0x00077795
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Oculus_NightVision1");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06001080 RID: 4224 RVA: 0x000795B8 File Offset: 0x000777B8
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
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			this.material.SetFloat("_Vignette", this.Vignette);
			this.material.SetFloat("_Linecount", this.Linecount);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06001081 RID: 4225 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06001082 RID: 4226 RVA: 0x0007969A File Offset: 0x0007789A
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001390 RID: 5008
	public Shader SCShader;

	// Token: 0x04001391 RID: 5009
	private float TimeX = 1f;

	// Token: 0x04001392 RID: 5010
	private float Distortion = 1f;

	// Token: 0x04001393 RID: 5011
	private Material SCMaterial;

	// Token: 0x04001394 RID: 5012
	[Range(0f, 100f)]
	public float Vignette = 1.3f;

	// Token: 0x04001395 RID: 5013
	[Range(1f, 150f)]
	public float Linecount = 90f;
}
