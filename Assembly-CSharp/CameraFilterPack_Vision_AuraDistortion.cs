using System;
using UnityEngine;

// Token: 0x02000218 RID: 536
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Vision/AuraDistortion")]
public class CameraFilterPack_Vision_AuraDistortion : MonoBehaviour
{
	// Token: 0x17000337 RID: 823
	// (get) Token: 0x060011C7 RID: 4551 RVA: 0x0007ECEC File Offset: 0x0007CEEC
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

	// Token: 0x060011C8 RID: 4552 RVA: 0x0007ED20 File Offset: 0x0007CF20
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Vision_AuraDistortion");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x060011C9 RID: 4553 RVA: 0x0007ED44 File Offset: 0x0007CF44
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
			this.material.SetFloat("_Value", this.Twist);
			this.material.SetColor("_Value2", this.Color);
			this.material.SetFloat("_Value3", this.PosX);
			this.material.SetFloat("_Value4", this.PosY);
			this.material.SetFloat("_Value5", this.Speed);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x060011CA RID: 4554 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x060011CB RID: 4555 RVA: 0x0007EE52 File Offset: 0x0007D052
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040014E0 RID: 5344
	public Shader SCShader;

	// Token: 0x040014E1 RID: 5345
	private float TimeX = 1f;

	// Token: 0x040014E2 RID: 5346
	private Material SCMaterial;

	// Token: 0x040014E3 RID: 5347
	[Range(0f, 2f)]
	public float Twist = 1f;

	// Token: 0x040014E4 RID: 5348
	[Range(-4f, 4f)]
	public float Speed = 1f;

	// Token: 0x040014E5 RID: 5349
	public Color Color = new Color(0.16f, 0.57f, 0.19f);

	// Token: 0x040014E6 RID: 5350
	[Range(-1f, 2f)]
	public float PosX = 0.5f;

	// Token: 0x040014E7 RID: 5351
	[Range(-1f, 2f)]
	public float PosY = 0.5f;
}
