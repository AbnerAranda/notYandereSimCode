using System;
using UnityEngine;

// Token: 0x02000109 RID: 265
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/AAA/Blood")]
public class CameraFilterPack_AAA_Blood : MonoBehaviour
{
	// Token: 0x17000228 RID: 552
	// (get) Token: 0x06000B08 RID: 2824 RVA: 0x00060A92 File Offset: 0x0005EC92
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

	// Token: 0x06000B09 RID: 2825 RVA: 0x00060AC6 File Offset: 0x0005ECC6
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_AAA_Blood1") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/AAA_Blood");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000B0A RID: 2826 RVA: 0x00060AFC File Offset: 0x0005ECFC
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
			this.material.SetFloat("_Value2", this.Blood1);
			this.material.SetFloat("_Value3", this.Blood2);
			this.material.SetFloat("_Value4", this.Blood3);
			this.material.SetFloat("_Value5", this.Blood4);
			this.material.SetTexture("_MainTex2", this.Texture2);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000B0B RID: 2827 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000B0C RID: 2828 RVA: 0x00060BF3 File Offset: 0x0005EDF3
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000D6F RID: 3439
	public Shader SCShader;

	// Token: 0x04000D70 RID: 3440
	private float TimeX = 1f;

	// Token: 0x04000D71 RID: 3441
	[Range(0f, 128f)]
	public float Blood1;

	// Token: 0x04000D72 RID: 3442
	[Range(0f, 128f)]
	public float Blood2;

	// Token: 0x04000D73 RID: 3443
	[Range(0f, 128f)]
	public float Blood3;

	// Token: 0x04000D74 RID: 3444
	[Range(0f, 128f)]
	public float Blood4 = 1f;

	// Token: 0x04000D75 RID: 3445
	[Range(0f, 0.004f)]
	public float LightReflect = 0.002f;

	// Token: 0x04000D76 RID: 3446
	private Material SCMaterial;

	// Token: 0x04000D77 RID: 3447
	private Texture2D Texture2;
}
