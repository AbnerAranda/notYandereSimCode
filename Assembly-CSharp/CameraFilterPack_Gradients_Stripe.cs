using System;
using UnityEngine;

// Token: 0x020001C8 RID: 456
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Gradients/Stripe")]
public class CameraFilterPack_Gradients_Stripe : MonoBehaviour
{
	// Token: 0x170002E7 RID: 743
	// (get) Token: 0x06000FC4 RID: 4036 RVA: 0x00075A1A File Offset: 0x00073C1A
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

	// Token: 0x06000FC5 RID: 4037 RVA: 0x00075A4E File Offset: 0x00073C4E
	private void Start()
	{
		this.SCShader = Shader.Find(this.ShaderName);
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000FC6 RID: 4038 RVA: 0x00075A70 File Offset: 0x00073C70
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

	// Token: 0x06000FC7 RID: 4039 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000FC8 RID: 4040 RVA: 0x00075B3C File Offset: 0x00073D3C
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040012C4 RID: 4804
	public Shader SCShader;

	// Token: 0x040012C5 RID: 4805
	private string ShaderName = "CameraFilterPack/Gradients_Stripe";

	// Token: 0x040012C6 RID: 4806
	private float TimeX = 1f;

	// Token: 0x040012C7 RID: 4807
	private Material SCMaterial;

	// Token: 0x040012C8 RID: 4808
	[Range(0f, 1f)]
	public float Switch = 1f;

	// Token: 0x040012C9 RID: 4809
	[Range(0f, 1f)]
	public float Fade = 1f;
}
