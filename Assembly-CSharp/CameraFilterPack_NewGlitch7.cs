using System;
using UnityEngine;

// Token: 0x020001DC RID: 476
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Glitch/Glitch Drawing")]
public class CameraFilterPack_NewGlitch7 : MonoBehaviour
{
	// Token: 0x170002FB RID: 763
	// (get) Token: 0x06001058 RID: 4184 RVA: 0x000786AC File Offset: 0x000768AC
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

	// Token: 0x06001059 RID: 4185 RVA: 0x000786E0 File Offset: 0x000768E0
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/CameraFilterPack_NewGlitch7");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x0600105A RID: 4186 RVA: 0x00078704 File Offset: 0x00076904
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
			this.material.SetFloat("_Speed", this.__Speed);
			this.material.SetFloat("LightMin", this._LightMin);
			this.material.SetFloat("LightMax", this._LightMax);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x0600105B RID: 4187 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x0600105C RID: 4188 RVA: 0x000787E6 File Offset: 0x000769E6
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001357 RID: 4951
	public Shader SCShader;

	// Token: 0x04001358 RID: 4952
	private float TimeX = 1f;

	// Token: 0x04001359 RID: 4953
	private Material SCMaterial;

	// Token: 0x0400135A RID: 4954
	[Range(0f, 1f)]
	public float __Speed = 1f;

	// Token: 0x0400135B RID: 4955
	[Range(0f, 1f)]
	public float _LightMin;

	// Token: 0x0400135C RID: 4956
	[Range(0f, 1f)]
	public float _LightMax = 1f;
}
