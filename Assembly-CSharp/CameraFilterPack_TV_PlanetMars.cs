using System;
using UnityEngine;

// Token: 0x02000207 RID: 519
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/TV/Planet Mars")]
public class CameraFilterPack_TV_PlanetMars : MonoBehaviour
{
	// Token: 0x17000326 RID: 806
	// (get) Token: 0x06001161 RID: 4449 RVA: 0x0007D477 File Offset: 0x0007B677
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

	// Token: 0x06001162 RID: 4450 RVA: 0x0007D4AB File Offset: 0x0007B6AB
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/TV_PlanetMars");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06001163 RID: 4451 RVA: 0x0007D4CC File Offset: 0x0007B6CC
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
			this.material.SetFloat("Fade", this.Fade);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06001164 RID: 4452 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06001165 RID: 4453 RVA: 0x0007D598 File Offset: 0x0007B798
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x0400147F RID: 5247
	public Shader SCShader;

	// Token: 0x04001480 RID: 5248
	[Range(0f, 1f)]
	public float Fade = 1f;

	// Token: 0x04001481 RID: 5249
	private float TimeX = 1f;

	// Token: 0x04001482 RID: 5250
	[Range(-10f, 10f)]
	public float Distortion = 1f;

	// Token: 0x04001483 RID: 5251
	private Material SCMaterial;
}
