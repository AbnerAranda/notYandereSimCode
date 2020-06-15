using System;
using UnityEngine;

// Token: 0x02000220 RID: 544
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Vision/Rainbow")]
public class CameraFilterPack_Vision_Rainbow : MonoBehaviour
{
	// Token: 0x1700033F RID: 831
	// (get) Token: 0x060011F7 RID: 4599 RVA: 0x0007FAFD File Offset: 0x0007DCFD
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

	// Token: 0x060011F8 RID: 4600 RVA: 0x0007FB31 File Offset: 0x0007DD31
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Vision_Rainbow");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x060011F9 RID: 4601 RVA: 0x0007FB54 File Offset: 0x0007DD54
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
			this.material.SetFloat("_Value", this.Speed);
			this.material.SetFloat("_Value2", this.PosX);
			this.material.SetFloat("_Value3", this.PosY);
			this.material.SetFloat("_Value4", this.Colors);
			this.material.SetFloat("_Value5", this.Vision);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x060011FA RID: 4602 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x060011FB RID: 4603 RVA: 0x0007FC62 File Offset: 0x0007DE62
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x0400151D RID: 5405
	public Shader SCShader;

	// Token: 0x0400151E RID: 5406
	private float TimeX = 1f;

	// Token: 0x0400151F RID: 5407
	private Material SCMaterial;

	// Token: 0x04001520 RID: 5408
	[Range(0f, 10f)]
	public float Speed = 1f;

	// Token: 0x04001521 RID: 5409
	[Range(0f, 1f)]
	public float PosX = 0.5f;

	// Token: 0x04001522 RID: 5410
	[Range(0f, 1f)]
	public float PosY = 0.5f;

	// Token: 0x04001523 RID: 5411
	[Range(0f, 5f)]
	public float Colors = 0.5f;

	// Token: 0x04001524 RID: 5412
	[Range(0f, 1f)]
	public float Vision = 0.5f;
}
