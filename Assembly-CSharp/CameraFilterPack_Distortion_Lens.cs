using System;
using UnityEngine;

// Token: 0x02000170 RID: 368
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Distortion/Lens")]
public class CameraFilterPack_Distortion_Lens : MonoBehaviour
{
	// Token: 0x1700028F RID: 655
	// (get) Token: 0x06000DB3 RID: 3507 RVA: 0x0006CEDE File Offset: 0x0006B0DE
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

	// Token: 0x06000DB4 RID: 3508 RVA: 0x0006CF12 File Offset: 0x0006B112
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Distortion_Lens");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000DB5 RID: 3509 RVA: 0x0006CF34 File Offset: 0x0006B134
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
			this.material.SetVector("_ScreenResolution", new Vector2((float)Screen.width, (float)Screen.height));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000DB6 RID: 3510 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000DB7 RID: 3511 RVA: 0x0006D00F File Offset: 0x0006B20F
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001080 RID: 4224
	public Shader SCShader;

	// Token: 0x04001081 RID: 4225
	private float TimeX = 1f;

	// Token: 0x04001082 RID: 4226
	private Material SCMaterial;

	// Token: 0x04001083 RID: 4227
	[Range(-1f, 1f)]
	public float CenterX;

	// Token: 0x04001084 RID: 4228
	[Range(-1f, 1f)]
	public float CenterY;

	// Token: 0x04001085 RID: 4229
	[Range(0f, 3f)]
	public float Distortion = 1f;
}
