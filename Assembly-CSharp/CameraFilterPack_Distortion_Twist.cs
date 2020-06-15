using System;
using UnityEngine;

// Token: 0x02000174 RID: 372
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Distortion/Twist")]
public class CameraFilterPack_Distortion_Twist : MonoBehaviour
{
	// Token: 0x17000293 RID: 659
	// (get) Token: 0x06000DCB RID: 3531 RVA: 0x0006D4D1 File Offset: 0x0006B6D1
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

	// Token: 0x06000DCC RID: 3532 RVA: 0x0006D505 File Offset: 0x0006B705
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Distortion_Twist");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000DCD RID: 3533 RVA: 0x0006D528 File Offset: 0x0006B728
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
			this.material.SetFloat("_CenterX", this.CenterX);
			this.material.SetFloat("_CenterY", this.CenterY);
			this.material.SetFloat("_Distortion", this.Distortion);
			this.material.SetFloat("_Size", this.Size);
			this.material.SetVector("_ScreenResolution", new Vector2((float)Screen.width, (float)Screen.height));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000DCE RID: 3534 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000DCF RID: 3535 RVA: 0x0006D619 File Offset: 0x0006B819
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001098 RID: 4248
	public Shader SCShader;

	// Token: 0x04001099 RID: 4249
	private float TimeX = 1f;

	// Token: 0x0400109A RID: 4250
	private Material SCMaterial;

	// Token: 0x0400109B RID: 4251
	[Range(-2f, 2f)]
	public float CenterX = 0.5f;

	// Token: 0x0400109C RID: 4252
	[Range(-2f, 2f)]
	public float CenterY = 0.5f;

	// Token: 0x0400109D RID: 4253
	[Range(-3.14f, 3.14f)]
	public float Distortion = 1f;

	// Token: 0x0400109E RID: 4254
	[Range(-2f, 2f)]
	public float Size = 1f;
}
