using System;
using UnityEngine;

// Token: 0x020001AB RID: 427
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/FX/Hypno")]
public class CameraFilterPack_FX_Hypno : MonoBehaviour
{
	// Token: 0x170002CA RID: 714
	// (get) Token: 0x06000F16 RID: 3862 RVA: 0x000726D2 File Offset: 0x000708D2
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

	// Token: 0x06000F17 RID: 3863 RVA: 0x00072706 File Offset: 0x00070906
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/FX_Hypno");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000F18 RID: 3864 RVA: 0x00072728 File Offset: 0x00070928
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
			this.material.SetFloat("_Value", this.Speed);
			this.material.SetFloat("_Value2", this.Red);
			this.material.SetFloat("_Value3", this.Green);
			this.material.SetFloat("_Value4", this.Blue);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000F19 RID: 3865 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000F1A RID: 3866 RVA: 0x00072820 File Offset: 0x00070A20
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040011EB RID: 4587
	public Shader SCShader;

	// Token: 0x040011EC RID: 4588
	private float TimeX = 1f;

	// Token: 0x040011ED RID: 4589
	private Material SCMaterial;

	// Token: 0x040011EE RID: 4590
	[Range(0f, 1f)]
	public float Speed = 1f;

	// Token: 0x040011EF RID: 4591
	[Range(-2f, 2f)]
	public float Red;

	// Token: 0x040011F0 RID: 4592
	[Range(-2f, 2f)]
	public float Green = 1f;

	// Token: 0x040011F1 RID: 4593
	[Range(-2f, 2f)]
	public float Blue = 1f;
}
