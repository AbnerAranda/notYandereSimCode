using System;
using UnityEngine;

// Token: 0x020001CA RID: 458
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Gradients/Thermal")]
public class CameraFilterPack_Gradients_Therma : MonoBehaviour
{
	// Token: 0x170002E9 RID: 745
	// (get) Token: 0x06000FD0 RID: 4048 RVA: 0x00075CFA File Offset: 0x00073EFA
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

	// Token: 0x06000FD1 RID: 4049 RVA: 0x00075D2E File Offset: 0x00073F2E
	private void Start()
	{
		this.SCShader = Shader.Find(this.ShaderName);
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000FD2 RID: 4050 RVA: 0x00075D50 File Offset: 0x00073F50
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

	// Token: 0x06000FD3 RID: 4051 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000FD4 RID: 4052 RVA: 0x00075E1C File Offset: 0x0007401C
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040012D0 RID: 4816
	public Shader SCShader;

	// Token: 0x040012D1 RID: 4817
	private string ShaderName = "CameraFilterPack/Gradients_Therma";

	// Token: 0x040012D2 RID: 4818
	private float TimeX = 1f;

	// Token: 0x040012D3 RID: 4819
	private Material SCMaterial;

	// Token: 0x040012D4 RID: 4820
	[Range(0f, 1f)]
	public float Switch = 1f;

	// Token: 0x040012D5 RID: 4821
	[Range(0f, 1f)]
	public float Fade = 1f;
}
