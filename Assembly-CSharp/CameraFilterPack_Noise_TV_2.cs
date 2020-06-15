using System;
using UnityEngine;

// Token: 0x020001E0 RID: 480
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Noise/TV_2")]
public class CameraFilterPack_Noise_TV_2 : MonoBehaviour
{
	// Token: 0x170002FF RID: 767
	// (get) Token: 0x06001072 RID: 4210 RVA: 0x000791E7 File Offset: 0x000773E7
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

	// Token: 0x06001073 RID: 4211 RVA: 0x0007921B File Offset: 0x0007741B
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_TV_Noise2") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/Noise_TV_2");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06001074 RID: 4212 RVA: 0x00079254 File Offset: 0x00077454
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
			this.material.SetFloat("_Value", this.Fade);
			this.material.SetFloat("_Value2", this.Fade_Additive);
			this.material.SetFloat("_Value3", this.Fade_Distortion);
			this.material.SetFloat("_Value4", this.Value4);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			this.material.SetTexture("Texture2", this.Texture2);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06001075 RID: 4213 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06001076 RID: 4214 RVA: 0x00079362 File Offset: 0x00077562
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001380 RID: 4992
	public Shader SCShader;

	// Token: 0x04001381 RID: 4993
	private float TimeX = 1f;

	// Token: 0x04001382 RID: 4994
	private Material SCMaterial;

	// Token: 0x04001383 RID: 4995
	[Range(0f, 1f)]
	public float Fade = 1f;

	// Token: 0x04001384 RID: 4996
	[Range(0f, 1f)]
	public float Fade_Additive;

	// Token: 0x04001385 RID: 4997
	[Range(0f, 1f)]
	public float Fade_Distortion;

	// Token: 0x04001386 RID: 4998
	[Range(0f, 10f)]
	private float Value4 = 1f;

	// Token: 0x04001387 RID: 4999
	private Texture2D Texture2;
}
