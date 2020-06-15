using System;
using UnityEngine;

// Token: 0x0200016A RID: 362
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Distortion/Dream2")]
public class CameraFilterPack_Distortion_Dream2 : MonoBehaviour
{
	// Token: 0x17000289 RID: 649
	// (get) Token: 0x06000D8F RID: 3471 RVA: 0x0006C6E6 File Offset: 0x0006A8E6
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

	// Token: 0x06000D90 RID: 3472 RVA: 0x0006C71A File Offset: 0x0006A91A
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Distortion_Dream2");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000D91 RID: 3473 RVA: 0x0006C73C File Offset: 0x0006A93C
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
			this.material.SetFloat("_Speed", this.Speed);
			this.material.SetFloat("_Distortion", this.Distortion);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000D92 RID: 3474 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000D93 RID: 3475 RVA: 0x0006C808 File Offset: 0x0006AA08
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001063 RID: 4195
	public Shader SCShader;

	// Token: 0x04001064 RID: 4196
	private float TimeX = 1f;

	// Token: 0x04001065 RID: 4197
	private Material SCMaterial;

	// Token: 0x04001066 RID: 4198
	[Range(0f, 100f)]
	public float Distortion = 6f;

	// Token: 0x04001067 RID: 4199
	[Range(0f, 32f)]
	public float Speed = 5f;
}
