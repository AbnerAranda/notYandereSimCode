using System;
using UnityEngine;

// Token: 0x02000181 RID: 385
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Drawing/Lines")]
public class CameraFilterPack_Drawing_Lines : MonoBehaviour
{
	// Token: 0x170002A0 RID: 672
	// (get) Token: 0x06000E19 RID: 3609 RVA: 0x0006E846 File Offset: 0x0006CA46
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

	// Token: 0x06000E1A RID: 3610 RVA: 0x0006E87A File Offset: 0x0006CA7A
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Drawing_Lines");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000E1B RID: 3611 RVA: 0x0006E89C File Offset: 0x0006CA9C
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
			this.material.SetFloat("_Value", this.Number);
			this.material.SetFloat("_Value2", this.Random);
			this.material.SetFloat("_Value3", this.PositionY);
			this.material.SetFloat("_Value4", this.Value4);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000E1C RID: 3612 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000E1D RID: 3613 RVA: 0x0006E994 File Offset: 0x0006CB94
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040010E8 RID: 4328
	public Shader SCShader;

	// Token: 0x040010E9 RID: 4329
	private float TimeX = 1f;

	// Token: 0x040010EA RID: 4330
	private Material SCMaterial;

	// Token: 0x040010EB RID: 4331
	[Range(0.1f, 10f)]
	public float Number = 1f;

	// Token: 0x040010EC RID: 4332
	[Range(0f, 1f)]
	public float Random = 0.5f;

	// Token: 0x040010ED RID: 4333
	[Range(0f, 10f)]
	private float PositionY = 1f;

	// Token: 0x040010EE RID: 4334
	[Range(0f, 10f)]
	private float Value4 = 1f;
}
