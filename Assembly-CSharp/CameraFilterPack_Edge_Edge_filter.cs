using System;
using UnityEngine;

// Token: 0x02000193 RID: 403
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Edge/Edge_filter")]
public class CameraFilterPack_Edge_Edge_filter : MonoBehaviour
{
	// Token: 0x170002B2 RID: 690
	// (get) Token: 0x06000E86 RID: 3718 RVA: 0x000704A3 File Offset: 0x0006E6A3
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

	// Token: 0x06000E87 RID: 3719 RVA: 0x000704D7 File Offset: 0x0006E6D7
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Edge_Edge_filter");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000E88 RID: 3720 RVA: 0x000704F8 File Offset: 0x0006E6F8
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
			this.material.SetFloat("_RedAmplifier", this.RedAmplifier);
			this.material.SetFloat("_GreenAmplifier", this.GreenAmplifier);
			this.material.SetFloat("_BlueAmplifier", this.BlueAmplifier);
			this.material.SetVector("_ScreenResolution", new Vector2((float)Screen.width, (float)Screen.height));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000E89 RID: 3721 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000E8A RID: 3722 RVA: 0x000705D3 File Offset: 0x0006E7D3
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001165 RID: 4453
	public Shader SCShader;

	// Token: 0x04001166 RID: 4454
	private float TimeX = 1f;

	// Token: 0x04001167 RID: 4455
	private Material SCMaterial;

	// Token: 0x04001168 RID: 4456
	[Range(0f, 10f)]
	public float RedAmplifier;

	// Token: 0x04001169 RID: 4457
	[Range(0f, 10f)]
	public float GreenAmplifier = 2f;

	// Token: 0x0400116A RID: 4458
	[Range(0f, 10f)]
	public float BlueAmplifier;
}
