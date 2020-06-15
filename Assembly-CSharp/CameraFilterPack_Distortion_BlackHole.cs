using System;
using UnityEngine;

// Token: 0x02000167 RID: 359
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Distortion/BlackHole")]
public class CameraFilterPack_Distortion_BlackHole : MonoBehaviour
{
	// Token: 0x17000286 RID: 646
	// (get) Token: 0x06000D7D RID: 3453 RVA: 0x0006C29F File Offset: 0x0006A49F
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

	// Token: 0x06000D7E RID: 3454 RVA: 0x0006C2D3 File Offset: 0x0006A4D3
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Distortion_BlackHole");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000D7F RID: 3455 RVA: 0x0006C2F4 File Offset: 0x0006A4F4
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
			this.material.SetFloat("_PositionX", this.PositionX);
			this.material.SetFloat("_PositionY", this.PositionY);
			this.material.SetFloat("_Distortion", this.Size);
			this.material.SetFloat("_Distortion2", this.Distortion);
			this.material.SetVector("_ScreenResolution", new Vector2((float)Screen.width, (float)Screen.height));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000D80 RID: 3456 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000D81 RID: 3457 RVA: 0x0006C3E5 File Offset: 0x0006A5E5
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001051 RID: 4177
	public Shader SCShader;

	// Token: 0x04001052 RID: 4178
	private float TimeX = 1f;

	// Token: 0x04001053 RID: 4179
	private Material SCMaterial;

	// Token: 0x04001054 RID: 4180
	[Range(-1f, 1f)]
	public float PositionX;

	// Token: 0x04001055 RID: 4181
	[Range(-1f, 1f)]
	public float PositionY;

	// Token: 0x04001056 RID: 4182
	[Range(-5f, 5f)]
	public float Size = 0.05f;

	// Token: 0x04001057 RID: 4183
	[Range(0f, 180f)]
	public float Distortion = 30f;
}
