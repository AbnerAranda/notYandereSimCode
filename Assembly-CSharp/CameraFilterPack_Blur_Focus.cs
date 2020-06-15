using System;
using UnityEngine;

// Token: 0x0200013E RID: 318
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Blur/Focus")]
public class CameraFilterPack_Blur_Focus : MonoBehaviour
{
	// Token: 0x1700025D RID: 605
	// (get) Token: 0x06000C85 RID: 3205 RVA: 0x00067E6F File Offset: 0x0006606F
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

	// Token: 0x06000C86 RID: 3206 RVA: 0x00067EA3 File Offset: 0x000660A3
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Blur_Focus");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000C87 RID: 3207 RVA: 0x00067EC4 File Offset: 0x000660C4
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
			this.material.SetFloat("_CenterX", this.CenterX);
			this.material.SetFloat("_CenterY", this.CenterY);
			float value = Mathf.Round(this._Size / 0.2f) * 0.2f;
			this.material.SetFloat("_Size", value);
			this.material.SetFloat("_Circle", this._Eyes);
			this.material.SetVector("_ScreenResolution", new Vector2((float)Screen.width, (float)Screen.height));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000C88 RID: 3208 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000C89 RID: 3209 RVA: 0x00067FC8 File Offset: 0x000661C8
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000F58 RID: 3928
	public Shader SCShader;

	// Token: 0x04000F59 RID: 3929
	private float TimeX = 1f;

	// Token: 0x04000F5A RID: 3930
	private Material SCMaterial;

	// Token: 0x04000F5B RID: 3931
	[Range(-1f, 1f)]
	public float CenterX;

	// Token: 0x04000F5C RID: 3932
	[Range(-1f, 1f)]
	public float CenterY;

	// Token: 0x04000F5D RID: 3933
	[Range(0f, 10f)]
	public float _Size = 5f;

	// Token: 0x04000F5E RID: 3934
	[Range(0.12f, 64f)]
	public float _Eyes = 2f;
}
