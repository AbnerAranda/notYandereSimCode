using System;
using UnityEngine;

// Token: 0x020001B7 RID: 439
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Vision/Fly_Vision")]
public class CameraFilterPack_Fly_Vision : MonoBehaviour
{
	// Token: 0x170002D6 RID: 726
	// (get) Token: 0x06000F5E RID: 3934 RVA: 0x0007375E File Offset: 0x0007195E
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

	// Token: 0x06000F5F RID: 3935 RVA: 0x00073792 File Offset: 0x00071992
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_Fly_VisionFX") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/Fly_Vision");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000F60 RID: 3936 RVA: 0x000737C8 File Offset: 0x000719C8
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
			this.material.SetFloat("_Value", this.Zoom);
			this.material.SetFloat("_Value2", this.Distortion);
			this.material.SetFloat("_Value3", this.Fade);
			this.material.SetFloat("_Value4", this.Value4);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			this.material.SetTexture("Texture2", this.Texture2);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000F61 RID: 3937 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000F62 RID: 3938 RVA: 0x000738D6 File Offset: 0x00071AD6
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001226 RID: 4646
	public Shader SCShader;

	// Token: 0x04001227 RID: 4647
	private float TimeX = 1f;

	// Token: 0x04001228 RID: 4648
	private Material SCMaterial;

	// Token: 0x04001229 RID: 4649
	[Range(0.04f, 1.5f)]
	public float Zoom = 0.25f;

	// Token: 0x0400122A RID: 4650
	[Range(0f, 1f)]
	public float Distortion = 0.4f;

	// Token: 0x0400122B RID: 4651
	[Range(0f, 1f)]
	public float Fade = 0.4f;

	// Token: 0x0400122C RID: 4652
	[Range(0f, 10f)]
	private float Value4 = 1f;

	// Token: 0x0400122D RID: 4653
	private Texture2D Texture2;
}
