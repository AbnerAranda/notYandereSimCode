using System;
using UnityEngine;

// Token: 0x0200010C RID: 268
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/AAA/Blood_Plus")]
public class CameraFilterPack_AAA_Blood_Plus : MonoBehaviour
{
	// Token: 0x1700022B RID: 555
	// (get) Token: 0x06000B1A RID: 2842 RVA: 0x0006117E File Offset: 0x0005F37E
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

	// Token: 0x06000B1B RID: 2843 RVA: 0x000611B2 File Offset: 0x0005F3B2
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_AAA_Blood2") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/AAA_Blood_Plus");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000B1C RID: 2844 RVA: 0x000611E8 File Offset: 0x0005F3E8
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
			this.material.SetFloat("_Value", this.LightReflect);
			this.material.SetFloat("_Value2", Mathf.Clamp(this.Blood_1, 0f, 1f));
			this.material.SetFloat("_Value3", Mathf.Clamp(this.Blood_2, 0f, 1f));
			this.material.SetFloat("_Value4", Mathf.Clamp(this.Blood_3, 0f, 1f));
			this.material.SetFloat("_Value5", Mathf.Clamp(this.Blood_4, 0f, 1f));
			this.material.SetFloat("_Value6", Mathf.Clamp(this.Blood_5, 0f, 1f));
			this.material.SetFloat("_Value7", Mathf.Clamp(this.Blood_6, 0f, 1f));
			this.material.SetFloat("_Value8", Mathf.Clamp(this.Blood_7, 0f, 1f));
			this.material.SetFloat("_Value9", Mathf.Clamp(this.Blood_8, 0f, 1f));
			this.material.SetFloat("_Value10", Mathf.Clamp(this.Blood_9, 0f, 1f));
			this.material.SetFloat("_Value11", Mathf.Clamp(this.Blood_10, 0f, 1f));
			this.material.SetFloat("_Value12", Mathf.Clamp(this.Blood_11, 0f, 1f));
			this.material.SetFloat("_Value13", Mathf.Clamp(this.Blood_12, 0f, 1f));
			this.material.SetTexture("_MainTex2", this.Texture2);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000B1D RID: 2845 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000B1E RID: 2846 RVA: 0x00061443 File Offset: 0x0005F643
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000D93 RID: 3475
	public Shader SCShader;

	// Token: 0x04000D94 RID: 3476
	private float TimeX = 1f;

	// Token: 0x04000D95 RID: 3477
	[Range(0f, 1f)]
	public float Blood_1 = 1f;

	// Token: 0x04000D96 RID: 3478
	[Range(0f, 1f)]
	public float Blood_2;

	// Token: 0x04000D97 RID: 3479
	[Range(0f, 1f)]
	public float Blood_3;

	// Token: 0x04000D98 RID: 3480
	[Range(0f, 1f)]
	public float Blood_4;

	// Token: 0x04000D99 RID: 3481
	[Range(0f, 1f)]
	public float Blood_5;

	// Token: 0x04000D9A RID: 3482
	[Range(0f, 1f)]
	public float Blood_6;

	// Token: 0x04000D9B RID: 3483
	[Range(0f, 1f)]
	public float Blood_7;

	// Token: 0x04000D9C RID: 3484
	[Range(0f, 1f)]
	public float Blood_8;

	// Token: 0x04000D9D RID: 3485
	[Range(0f, 1f)]
	public float Blood_9;

	// Token: 0x04000D9E RID: 3486
	[Range(0f, 1f)]
	public float Blood_10;

	// Token: 0x04000D9F RID: 3487
	[Range(0f, 1f)]
	public float Blood_11;

	// Token: 0x04000DA0 RID: 3488
	[Range(0f, 1f)]
	public float Blood_12;

	// Token: 0x04000DA1 RID: 3489
	[Range(0f, 1f)]
	public float LightReflect = 0.5f;

	// Token: 0x04000DA2 RID: 3490
	private Material SCMaterial;

	// Token: 0x04000DA3 RID: 3491
	private Texture2D Texture2;
}
