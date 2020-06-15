using System;
using UnityEngine;

// Token: 0x02000150 RID: 336
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Colors/Chromatic_Plus")]
public class CameraFilterPack_Color_Chromatic_Plus : MonoBehaviour
{
	// Token: 0x1700026F RID: 623
	// (get) Token: 0x06000CF1 RID: 3313 RVA: 0x00069D0A File Offset: 0x00067F0A
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

	// Token: 0x06000CF2 RID: 3314 RVA: 0x00069D3E File Offset: 0x00067F3E
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Color_Chromatic_Plus");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000CF3 RID: 3315 RVA: 0x00069D60 File Offset: 0x00067F60
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
			this.material.SetFloat("_Value", this.Size);
			this.material.SetFloat("_Value2", this.Smooth);
			this.material.SetFloat("_Distortion", this.Offset);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000CF4 RID: 3316 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000CF5 RID: 3317 RVA: 0x00069E42 File Offset: 0x00068042
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000FCF RID: 4047
	public Shader SCShader;

	// Token: 0x04000FD0 RID: 4048
	private float TimeX = 1f;

	// Token: 0x04000FD1 RID: 4049
	private Material SCMaterial;

	// Token: 0x04000FD2 RID: 4050
	[Range(0f, 0.8f)]
	public float Size = 0.55f;

	// Token: 0x04000FD3 RID: 4051
	[Range(0.01f, 0.4f)]
	public float Smooth = 0.26f;

	// Token: 0x04000FD4 RID: 4052
	[Range(-0.02f, 0.02f)]
	public float Offset = 0.005f;
}
