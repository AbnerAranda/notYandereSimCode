using System;
using UnityEngine;

// Token: 0x0200014B RID: 331
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Broken/Spliter")]
public class CameraFilterPack_Broken_Spliter : MonoBehaviour
{
	// Token: 0x1700026A RID: 618
	// (get) Token: 0x06000CD3 RID: 3283 RVA: 0x00069505 File Offset: 0x00067705
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

	// Token: 0x06000CD4 RID: 3284 RVA: 0x00069539 File Offset: 0x00067739
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/CameraFilterPack_Broken_Spliter");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000CD5 RID: 3285 RVA: 0x0006955C File Offset: 0x0006775C
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
			this.material.SetFloat("_Speed", this.__Speed);
			this.material.SetFloat("PosX", this._PosX);
			this.material.SetFloat("PosY", this._PosY);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000CD6 RID: 3286 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000CD7 RID: 3287 RVA: 0x0006963E File Offset: 0x0006783E
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000FAD RID: 4013
	public Shader SCShader;

	// Token: 0x04000FAE RID: 4014
	private float TimeX = 1f;

	// Token: 0x04000FAF RID: 4015
	private Material SCMaterial;

	// Token: 0x04000FB0 RID: 4016
	[Range(0f, 1f)]
	private float __Speed = 1f;

	// Token: 0x04000FB1 RID: 4017
	[Range(0f, 1f)]
	public float _PosX = 0.5f;

	// Token: 0x04000FB2 RID: 4018
	[Range(0f, 1f)]
	public float _PosY = 0.5f;
}
