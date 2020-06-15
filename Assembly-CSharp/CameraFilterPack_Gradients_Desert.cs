using System;
using UnityEngine;

// Token: 0x020001C2 RID: 450
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Gradients/Desert")]
public class CameraFilterPack_Gradients_Desert : MonoBehaviour
{
	// Token: 0x170002E1 RID: 737
	// (get) Token: 0x06000FA0 RID: 4000 RVA: 0x0007517A File Offset: 0x0007337A
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

	// Token: 0x06000FA1 RID: 4001 RVA: 0x000751AE File Offset: 0x000733AE
	private void Start()
	{
		this.SCShader = Shader.Find(this.ShaderName);
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000FA2 RID: 4002 RVA: 0x000751D0 File Offset: 0x000733D0
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

	// Token: 0x06000FA3 RID: 4003 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000FA4 RID: 4004 RVA: 0x0007529C File Offset: 0x0007349C
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040012A0 RID: 4768
	public Shader SCShader;

	// Token: 0x040012A1 RID: 4769
	private string ShaderName = "CameraFilterPack/Gradients_Desert";

	// Token: 0x040012A2 RID: 4770
	private float TimeX = 1f;

	// Token: 0x040012A3 RID: 4771
	private Material SCMaterial;

	// Token: 0x040012A4 RID: 4772
	[Range(0f, 1f)]
	public float Switch = 1f;

	// Token: 0x040012A5 RID: 4773
	[Range(0f, 1f)]
	public float Fade = 1f;
}
