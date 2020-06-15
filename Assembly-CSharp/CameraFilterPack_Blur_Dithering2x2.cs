using System;
using UnityEngine;

// Token: 0x0200013D RID: 317
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Blur/Dithering2x2")]
public class CameraFilterPack_Blur_Dithering2x2 : MonoBehaviour
{
	// Token: 0x1700025C RID: 604
	// (get) Token: 0x06000C7F RID: 3199 RVA: 0x00067CFF File Offset: 0x00065EFF
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

	// Token: 0x06000C80 RID: 3200 RVA: 0x00067D33 File Offset: 0x00065F33
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Blur_Dithering2x2");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000C81 RID: 3201 RVA: 0x00067D54 File Offset: 0x00065F54
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
			this.material.SetFloat("_Level", (float)this.Level);
			this.material.SetVector("_Distance", this.Distance);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000C82 RID: 3202 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000C83 RID: 3203 RVA: 0x00067E26 File Offset: 0x00066026
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000F53 RID: 3923
	public Shader SCShader;

	// Token: 0x04000F54 RID: 3924
	private float TimeX = 1f;

	// Token: 0x04000F55 RID: 3925
	private Material SCMaterial;

	// Token: 0x04000F56 RID: 3926
	[Range(2f, 16f)]
	public int Level = 4;

	// Token: 0x04000F57 RID: 3927
	public Vector2 Distance = new Vector2(30f, 0f);
}
