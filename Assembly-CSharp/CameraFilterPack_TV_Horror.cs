using System;
using UnityEngine;

// Token: 0x02000200 RID: 512
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/TV/Horror")]
public class CameraFilterPack_TV_Horror : MonoBehaviour
{
	// Token: 0x1700031F RID: 799
	// (get) Token: 0x06001137 RID: 4407 RVA: 0x0007CAFF File Offset: 0x0007ACFF
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

	// Token: 0x06001138 RID: 4408 RVA: 0x0007CB33 File Offset: 0x0007AD33
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_TV_HorrorFX") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/TV_Horror");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06001139 RID: 4409 RVA: 0x0007CB6C File Offset: 0x0007AD6C
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
			this.material.SetFloat("Fade", this.Fade);
			this.material.SetFloat("Distortion", this.Distortion);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			this.material.SetTexture("Texture2", this.Texture2);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x0600113A RID: 4410 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x0600113B RID: 4411 RVA: 0x0007CC4E File Offset: 0x0007AE4E
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x0400145B RID: 5211
	public Shader SCShader;

	// Token: 0x0400145C RID: 5212
	private float TimeX = 1f;

	// Token: 0x0400145D RID: 5213
	private Material SCMaterial;

	// Token: 0x0400145E RID: 5214
	[Range(0f, 1f)]
	public float Fade = 1f;

	// Token: 0x0400145F RID: 5215
	[Range(0f, 1f)]
	public float Distortion = 1f;

	// Token: 0x04001460 RID: 5216
	private Texture2D Texture2;
}
