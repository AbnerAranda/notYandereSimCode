using System;
using UnityEngine;

// Token: 0x02000204 RID: 516
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Old Film/Old")]
public class CameraFilterPack_TV_Old : MonoBehaviour
{
	// Token: 0x17000323 RID: 803
	// (get) Token: 0x0600114F RID: 4431 RVA: 0x0007D092 File Offset: 0x0007B292
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

	// Token: 0x06001150 RID: 4432 RVA: 0x0007D0C6 File Offset: 0x0007B2C6
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/TV_Old");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06001151 RID: 4433 RVA: 0x0007D0E8 File Offset: 0x0007B2E8
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

	// Token: 0x06001152 RID: 4434 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06001153 RID: 4435 RVA: 0x0007D16E File Offset: 0x0007B36E
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x0400146F RID: 5231
	public Shader SCShader;

	// Token: 0x04001470 RID: 5232
	private float TimeX = 1f;

	// Token: 0x04001471 RID: 5233
	[Range(1f, 10f)]
	public float Distortion = 1f;

	// Token: 0x04001472 RID: 5234
	private Material SCMaterial;
}
