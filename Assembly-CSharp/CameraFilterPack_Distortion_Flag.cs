using System;
using UnityEngine;

// Token: 0x0200016C RID: 364
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Distortion/Flag")]
public class CameraFilterPack_Distortion_Flag : MonoBehaviour
{
	// Token: 0x1700028B RID: 651
	// (get) Token: 0x06000D9B RID: 3483 RVA: 0x0006C98E File Offset: 0x0006AB8E
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

	// Token: 0x06000D9C RID: 3484 RVA: 0x0006C9C2 File Offset: 0x0006ABC2
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Distortion_Flag");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000D9D RID: 3485 RVA: 0x0006C9E4 File Offset: 0x0006ABE4
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
			this.material.SetFloat("_Distortion", this.Distortion);
			this.material.SetVector("_ScreenResolution", new Vector2((float)Screen.width, (float)Screen.height));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000D9E RID: 3486 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000D9F RID: 3487 RVA: 0x0006CA93 File Offset: 0x0006AC93
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x0400106C RID: 4204
	public Shader SCShader;

	// Token: 0x0400106D RID: 4205
	private float TimeX = 1f;

	// Token: 0x0400106E RID: 4206
	[Range(0f, 2f)]
	public float Distortion = 1f;

	// Token: 0x0400106F RID: 4207
	private Material SCMaterial;

	// Token: 0x04001070 RID: 4208
	public static float ChangeDistortion;
}
