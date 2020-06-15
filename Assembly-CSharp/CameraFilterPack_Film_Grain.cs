using System;
using UnityEngine;

// Token: 0x020001B6 RID: 438
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Film/Grain")]
public class CameraFilterPack_Film_Grain : MonoBehaviour
{
	// Token: 0x170002D5 RID: 725
	// (get) Token: 0x06000F58 RID: 3928 RVA: 0x00073619 File Offset: 0x00071819
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

	// Token: 0x06000F59 RID: 3929 RVA: 0x0007364D File Offset: 0x0007184D
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Film_Grain");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000F5A RID: 3930 RVA: 0x00073670 File Offset: 0x00071870
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
			this.material.SetFloat("_Value", this.Value);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000F5B RID: 3931 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000F5C RID: 3932 RVA: 0x00073726 File Offset: 0x00071926
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001222 RID: 4642
	public Shader SCShader;

	// Token: 0x04001223 RID: 4643
	private float TimeX = 1f;

	// Token: 0x04001224 RID: 4644
	private Material SCMaterial;

	// Token: 0x04001225 RID: 4645
	[Range(-64f, 64f)]
	public float Value = 32f;
}
