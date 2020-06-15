using System;
using UnityEngine;

// Token: 0x020001CD RID: 461
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Light/Water")]
public class CameraFilterPack_Light_Water : MonoBehaviour
{
	// Token: 0x170002EC RID: 748
	// (get) Token: 0x06000FE2 RID: 4066 RVA: 0x000760F2 File Offset: 0x000742F2
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

	// Token: 0x06000FE3 RID: 4067 RVA: 0x00076126 File Offset: 0x00074326
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Light_Water");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000FE4 RID: 4068 RVA: 0x00076148 File Offset: 0x00074348
	private void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
	{
		if (this.SCShader != null)
		{
			this.TimeX += Time.deltaTime * this.Speed;
			if (this.TimeX > 100f)
			{
				this.TimeX = 0f;
			}
			this.material.SetFloat("_TimeX", this.TimeX);
			this.material.SetFloat("_Alpha", this.Alpha);
			this.material.SetFloat("_Distance", this.Distance);
			this.material.SetFloat("_Size", this.Size);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000FE5 RID: 4069 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000FE6 RID: 4070 RVA: 0x00076231 File Offset: 0x00074431
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040012DE RID: 4830
	public Shader SCShader;

	// Token: 0x040012DF RID: 4831
	private float TimeX = 1f;

	// Token: 0x040012E0 RID: 4832
	private Material SCMaterial;

	// Token: 0x040012E1 RID: 4833
	[Range(0f, 1f)]
	public float Size = 4f;

	// Token: 0x040012E2 RID: 4834
	[Range(0f, 2f)]
	public float Alpha = 0.07f;

	// Token: 0x040012E3 RID: 4835
	[Range(0f, 32f)]
	public float Distance = 10f;

	// Token: 0x040012E4 RID: 4836
	[Range(-2f, 2f)]
	public float Speed = 0.4f;
}
