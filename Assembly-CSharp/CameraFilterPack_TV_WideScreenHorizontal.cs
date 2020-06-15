using System;
using UnityEngine;

// Token: 0x02000214 RID: 532
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/TV/WideScreenHorizontal")]
public class CameraFilterPack_TV_WideScreenHorizontal : MonoBehaviour
{
	// Token: 0x17000333 RID: 819
	// (get) Token: 0x060011AF RID: 4527 RVA: 0x0007E675 File Offset: 0x0007C875
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

	// Token: 0x060011B0 RID: 4528 RVA: 0x0007E6A9 File Offset: 0x0007C8A9
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/TV_WideScreenHorizontal");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x060011B1 RID: 4529 RVA: 0x0007E6CC File Offset: 0x0007C8CC
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
			this.material.SetFloat("_Value", this.Size);
			this.material.SetFloat("_Value2", this.Smooth);
			this.material.SetFloat("_Value3", this.StretchX);
			this.material.SetFloat("_Value4", this.StretchY);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x060011B2 RID: 4530 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x060011B3 RID: 4531 RVA: 0x0007E7C4 File Offset: 0x0007C9C4
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040014C6 RID: 5318
	public Shader SCShader;

	// Token: 0x040014C7 RID: 5319
	private float TimeX = 1f;

	// Token: 0x040014C8 RID: 5320
	private Material SCMaterial;

	// Token: 0x040014C9 RID: 5321
	[Range(0f, 0.8f)]
	public float Size = 0.55f;

	// Token: 0x040014CA RID: 5322
	[Range(0.001f, 0.4f)]
	public float Smooth = 0.01f;

	// Token: 0x040014CB RID: 5323
	[Range(0f, 10f)]
	private float StretchX = 1f;

	// Token: 0x040014CC RID: 5324
	[Range(0f, 10f)]
	private float StretchY = 1f;
}
