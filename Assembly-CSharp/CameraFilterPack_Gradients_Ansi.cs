using System;
using UnityEngine;

// Token: 0x020001C1 RID: 449
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Gradients/Ansi")]
public class CameraFilterPack_Gradients_Ansi : MonoBehaviour
{
	// Token: 0x170002E0 RID: 736
	// (get) Token: 0x06000F9A RID: 3994 RVA: 0x00075008 File Offset: 0x00073208
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

	// Token: 0x06000F9B RID: 3995 RVA: 0x0007503C File Offset: 0x0007323C
	private void Start()
	{
		this.SCShader = Shader.Find(this.ShaderName);
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000F9C RID: 3996 RVA: 0x00075060 File Offset: 0x00073260
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
			this.material.SetFloat("_Value", this.Switch);
			this.material.SetFloat("_Value2", this.Fade);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000F9D RID: 3997 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000F9E RID: 3998 RVA: 0x0007512C File Offset: 0x0007332C
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x0400129A RID: 4762
	public Shader SCShader;

	// Token: 0x0400129B RID: 4763
	private string ShaderName = "CameraFilterPack/Gradients_Ansi";

	// Token: 0x0400129C RID: 4764
	private float TimeX = 1f;

	// Token: 0x0400129D RID: 4765
	private Material SCMaterial;

	// Token: 0x0400129E RID: 4766
	[Range(0f, 1f)]
	public float Switch = 1f;

	// Token: 0x0400129F RID: 4767
	[Range(0f, 1f)]
	public float Fade = 1f;
}
