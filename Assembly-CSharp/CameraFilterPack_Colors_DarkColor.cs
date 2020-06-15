using System;
using UnityEngine;

// Token: 0x0200015E RID: 350
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Colors/DarkColor")]
public class CameraFilterPack_Colors_DarkColor : MonoBehaviour
{
	// Token: 0x1700027D RID: 637
	// (get) Token: 0x06000D47 RID: 3399 RVA: 0x0006B607 File Offset: 0x00069807
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

	// Token: 0x06000D48 RID: 3400 RVA: 0x0006B63B File Offset: 0x0006983B
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Colors_DarkColor");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000D49 RID: 3401 RVA: 0x0006B65C File Offset: 0x0006985C
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
			this.material.SetFloat("_Value", this.Alpha);
			this.material.SetFloat("_Value2", this.Colors);
			this.material.SetFloat("_Value3", this.Green_Mod);
			this.material.SetFloat("_Value4", this.Value4);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000D4A RID: 3402 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000D4B RID: 3403 RVA: 0x0006B754 File Offset: 0x00069954
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x0400101B RID: 4123
	public Shader SCShader;

	// Token: 0x0400101C RID: 4124
	private float TimeX = 1f;

	// Token: 0x0400101D RID: 4125
	private Material SCMaterial;

	// Token: 0x0400101E RID: 4126
	[Range(-5f, 5f)]
	public float Alpha = 1f;

	// Token: 0x0400101F RID: 4127
	[Range(0f, 16f)]
	private float Colors = 11f;

	// Token: 0x04001020 RID: 4128
	[Range(-1f, 1f)]
	private float Green_Mod = 1f;

	// Token: 0x04001021 RID: 4129
	[Range(0f, 10f)]
	private float Value4 = 1f;
}
