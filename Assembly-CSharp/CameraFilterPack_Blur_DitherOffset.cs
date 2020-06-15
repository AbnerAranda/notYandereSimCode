using System;
using UnityEngine;

// Token: 0x0200013C RID: 316
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Blur/DitherOffset")]
public class CameraFilterPack_Blur_DitherOffset : MonoBehaviour
{
	// Token: 0x1700025B RID: 603
	// (get) Token: 0x06000C79 RID: 3193 RVA: 0x00067B8F File Offset: 0x00065D8F
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

	// Token: 0x06000C7A RID: 3194 RVA: 0x00067BC3 File Offset: 0x00065DC3
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Blur_DitherOffset");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000C7B RID: 3195 RVA: 0x00067BE4 File Offset: 0x00065DE4
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

	// Token: 0x06000C7C RID: 3196 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000C7D RID: 3197 RVA: 0x00067CB6 File Offset: 0x00065EB6
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000F4E RID: 3918
	public Shader SCShader;

	// Token: 0x04000F4F RID: 3919
	private float TimeX = 1f;

	// Token: 0x04000F50 RID: 3920
	private Material SCMaterial;

	// Token: 0x04000F51 RID: 3921
	[Range(1f, 16f)]
	public int Level = 4;

	// Token: 0x04000F52 RID: 3922
	public Vector2 Distance = new Vector2(30f, 0f);
}
