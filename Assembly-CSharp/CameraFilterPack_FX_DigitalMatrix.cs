using System;
using UnityEngine;

// Token: 0x0200019E RID: 414
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/FX/DigitalMatrix")]
public class CameraFilterPack_FX_DigitalMatrix : MonoBehaviour
{
	// Token: 0x170002BD RID: 701
	// (get) Token: 0x06000EC8 RID: 3784 RVA: 0x00071404 File Offset: 0x0006F604
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

	// Token: 0x06000EC9 RID: 3785 RVA: 0x00071438 File Offset: 0x0006F638
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/FX_DigitalMatrix");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000ECA RID: 3786 RVA: 0x0007145C File Offset: 0x0006F65C
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
			this.material.SetFloat("_Value", this.Size);
			this.material.SetFloat("_Value2", this.ColorR);
			this.material.SetFloat("_Value3", this.ColorG);
			this.material.SetFloat("_Value4", this.ColorB);
			this.material.SetFloat("_Value5", this.Speed);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000ECB RID: 3787 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000ECC RID: 3788 RVA: 0x0007156A File Offset: 0x0006F76A
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040011A1 RID: 4513
	public Shader SCShader;

	// Token: 0x040011A2 RID: 4514
	private float TimeX = 1f;

	// Token: 0x040011A3 RID: 4515
	private Material SCMaterial;

	// Token: 0x040011A4 RID: 4516
	[Range(0.4f, 5f)]
	public float Size = 1f;

	// Token: 0x040011A5 RID: 4517
	[Range(-10f, 10f)]
	public float Speed = 1f;

	// Token: 0x040011A6 RID: 4518
	[Range(-1f, 1f)]
	public float ColorR = -1f;

	// Token: 0x040011A7 RID: 4519
	[Range(-1f, 1f)]
	public float ColorG = 1f;

	// Token: 0x040011A8 RID: 4520
	[Range(-1f, 1f)]
	public float ColorB = -1f;
}
