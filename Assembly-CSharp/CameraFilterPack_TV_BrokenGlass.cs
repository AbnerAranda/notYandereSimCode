using System;
using UnityEngine;

// Token: 0x020001FA RID: 506
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/TV/Broken Glass")]
public class CameraFilterPack_TV_BrokenGlass : MonoBehaviour
{
	// Token: 0x17000319 RID: 793
	// (get) Token: 0x06001113 RID: 4371 RVA: 0x0007BF41 File Offset: 0x0007A141
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

	// Token: 0x06001114 RID: 4372 RVA: 0x0007BF75 File Offset: 0x0007A175
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_TV_BrokenGlass1") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/TV_BrokenGlass");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06001115 RID: 4373 RVA: 0x0007BFAC File Offset: 0x0007A1AC
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
			this.material.SetFloat("_Value2", this.Broken_Small);
			this.material.SetFloat("_Value3", this.Broken_Medium);
			this.material.SetFloat("_Value4", this.Broken_High);
			this.material.SetFloat("_Value5", this.Broken_Big);
			this.material.SetTexture("_MainTex2", this.Texture2);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06001116 RID: 4374 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06001117 RID: 4375 RVA: 0x0007C0A3 File Offset: 0x0007A2A3
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x0400142C RID: 5164
	public Shader SCShader;

	// Token: 0x0400142D RID: 5165
	private float TimeX = 1f;

	// Token: 0x0400142E RID: 5166
	[Range(0f, 128f)]
	public float Broken_Small;

	// Token: 0x0400142F RID: 5167
	[Range(0f, 128f)]
	public float Broken_Medium;

	// Token: 0x04001430 RID: 5168
	[Range(0f, 128f)]
	public float Broken_High;

	// Token: 0x04001431 RID: 5169
	[Range(0f, 128f)]
	public float Broken_Big = 1f;

	// Token: 0x04001432 RID: 5170
	[Range(0f, 0.004f)]
	public float LightReflect = 0.002f;

	// Token: 0x04001433 RID: 5171
	private Material SCMaterial;

	// Token: 0x04001434 RID: 5172
	private Texture2D Texture2;
}
