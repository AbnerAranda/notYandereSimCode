using System;
using UnityEngine;

// Token: 0x02000223 RID: 547
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Vision/Warp")]
public class CameraFilterPack_Vision_Warp : MonoBehaviour
{
	// Token: 0x17000342 RID: 834
	// (get) Token: 0x06001209 RID: 4617 RVA: 0x0008014D File Offset: 0x0007E34D
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

	// Token: 0x0600120A RID: 4618 RVA: 0x00080181 File Offset: 0x0007E381
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Vision_Warp");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x0600120B RID: 4619 RVA: 0x000801A4 File Offset: 0x0007E3A4
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
			this.material.SetFloat("_Value", this.Value);
			this.material.SetFloat("_Value2", this.Value2);
			this.material.SetFloat("_Value3", this.Value3);
			this.material.SetFloat("_Value4", this.Value4);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x0600120C RID: 4620 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x0600120D RID: 4621 RVA: 0x0008029C File Offset: 0x0007E49C
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x0400153B RID: 5435
	public Shader SCShader;

	// Token: 0x0400153C RID: 5436
	private float TimeX = 1f;

	// Token: 0x0400153D RID: 5437
	private Material SCMaterial;

	// Token: 0x0400153E RID: 5438
	[Range(0f, 1f)]
	public float Value = 0.6f;

	// Token: 0x0400153F RID: 5439
	[Range(0f, 1f)]
	public float Value2 = 0.6f;

	// Token: 0x04001540 RID: 5440
	[Range(0f, 10f)]
	private float Value3 = 1f;

	// Token: 0x04001541 RID: 5441
	[Range(0f, 10f)]
	private float Value4 = 1f;
}
