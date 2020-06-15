using System;
using UnityEngine;

// Token: 0x02000176 RID: 374
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Distortion/Water_Drop")]
public class CameraFilterPack_Distortion_Water_Drop : MonoBehaviour
{
	// Token: 0x17000295 RID: 661
	// (get) Token: 0x06000DD7 RID: 3543 RVA: 0x0006D812 File Offset: 0x0006BA12
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

	// Token: 0x06000DD8 RID: 3544 RVA: 0x0006D846 File Offset: 0x0006BA46
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Distortion_Water_Drop");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000DD9 RID: 3545 RVA: 0x0006D868 File Offset: 0x0006BA68
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
			this.material.SetVector("_ScreenResolution", new Vector2((float)Screen.width, (float)Screen.height));
			this.material.SetFloat("_CenterX", this.CenterX);
			this.material.SetFloat("_CenterY", this.CenterY);
			this.material.SetFloat("_WaveIntensity", this.WaveIntensity);
			this.material.SetInt("_NumberOfWaves", this.NumberOfWaves);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000DDA RID: 3546 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000DDB RID: 3547 RVA: 0x0006D959 File Offset: 0x0006BB59
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040010A6 RID: 4262
	public Shader SCShader;

	// Token: 0x040010A7 RID: 4263
	private float TimeX = 1f;

	// Token: 0x040010A8 RID: 4264
	private Material SCMaterial;

	// Token: 0x040010A9 RID: 4265
	[Range(-1f, 1f)]
	public float CenterX;

	// Token: 0x040010AA RID: 4266
	[Range(-1f, 1f)]
	public float CenterY;

	// Token: 0x040010AB RID: 4267
	[Range(0f, 10f)]
	public float WaveIntensity = 1f;

	// Token: 0x040010AC RID: 4268
	[Range(0f, 20f)]
	public int NumberOfWaves = 5;
}
