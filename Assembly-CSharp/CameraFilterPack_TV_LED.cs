using System;
using UnityEngine;

// Token: 0x02000201 RID: 513
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/TV/LED")]
public class CameraFilterPack_TV_LED : MonoBehaviour
{
	// Token: 0x17000320 RID: 800
	// (get) Token: 0x0600113D RID: 4413 RVA: 0x0007CC91 File Offset: 0x0007AE91
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

	// Token: 0x0600113E RID: 4414 RVA: 0x0007CCC5 File Offset: 0x0007AEC5
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/TV_LED");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x0600113F RID: 4415 RVA: 0x0007CCE8 File Offset: 0x0007AEE8
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
			this.material.SetFloat("_Size", (float)this.Size);
			this.material.SetFloat("Fade", this.Fade);
			this.material.SetFloat("_Distortion", this.Distortion);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06001140 RID: 4416 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06001141 RID: 4417 RVA: 0x0007CDCB File Offset: 0x0007AFCB
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001461 RID: 5217
	public Shader SCShader;

	// Token: 0x04001462 RID: 5218
	private float TimeX = 1f;

	// Token: 0x04001463 RID: 5219
	[Range(0f, 1f)]
	public float Fade;

	// Token: 0x04001464 RID: 5220
	[Range(1f, 10f)]
	private float Distortion = 1f;

	// Token: 0x04001465 RID: 5221
	[Range(1f, 15f)]
	public int Size = 5;

	// Token: 0x04001466 RID: 5222
	private Material SCMaterial;
}
