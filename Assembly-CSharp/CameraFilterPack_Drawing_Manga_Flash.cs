using System;
using UnityEngine;

// Token: 0x02000188 RID: 392
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Drawing/Manga_Flash")]
public class CameraFilterPack_Drawing_Manga_Flash : MonoBehaviour
{
	// Token: 0x170002A7 RID: 679
	// (get) Token: 0x06000E43 RID: 3651 RVA: 0x0006F066 File Offset: 0x0006D266
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

	// Token: 0x06000E44 RID: 3652 RVA: 0x0006F09A File Offset: 0x0006D29A
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Drawing_Manga_Flash");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000E45 RID: 3653 RVA: 0x0006F0BC File Offset: 0x0006D2BC
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
			this.material.SetFloat("_Value2", (float)this.Speed);
			this.material.SetFloat("_Value3", this.PosX);
			this.material.SetFloat("_Value4", this.PosY);
			this.material.SetFloat("_Intensity", this.Intensity);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000E46 RID: 3654 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000E47 RID: 3655 RVA: 0x0006F1CB File Offset: 0x0006D3CB
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001108 RID: 4360
	public Shader SCShader;

	// Token: 0x04001109 RID: 4361
	private float TimeX = 1f;

	// Token: 0x0400110A RID: 4362
	private Material SCMaterial;

	// Token: 0x0400110B RID: 4363
	[Range(1f, 10f)]
	public float Size = 1f;

	// Token: 0x0400110C RID: 4364
	[Range(0f, 30f)]
	public int Speed = 5;

	// Token: 0x0400110D RID: 4365
	[Range(-1f, 1f)]
	public float PosX = 0.5f;

	// Token: 0x0400110E RID: 4366
	[Range(-1f, 1f)]
	public float PosY = 0.5f;

	// Token: 0x0400110F RID: 4367
	[Range(0f, 1f)]
	public float Intensity = 1f;
}
