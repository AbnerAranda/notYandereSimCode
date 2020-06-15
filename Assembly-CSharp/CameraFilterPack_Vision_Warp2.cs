using System;
using UnityEngine;

// Token: 0x02000224 RID: 548
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Vision/Warp2")]
public class CameraFilterPack_Vision_Warp2 : MonoBehaviour
{
	// Token: 0x17000343 RID: 835
	// (get) Token: 0x0600120F RID: 4623 RVA: 0x000802F5 File Offset: 0x0007E4F5
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

	// Token: 0x06001210 RID: 4624 RVA: 0x00080329 File Offset: 0x0007E529
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Vision_Warp2");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06001211 RID: 4625 RVA: 0x0008034C File Offset: 0x0007E54C
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
			this.material.SetFloat("_Value", this.Value);
			this.material.SetFloat("_Value2", this.Value2);
			this.material.SetFloat("_Value3", this.Intensity);
			this.material.SetFloat("_Value4", this.Value4);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06001212 RID: 4626 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06001213 RID: 4627 RVA: 0x00080444 File Offset: 0x0007E644
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001542 RID: 5442
	public Shader SCShader;

	// Token: 0x04001543 RID: 5443
	private float TimeX = 1f;

	// Token: 0x04001544 RID: 5444
	private Material SCMaterial;

	// Token: 0x04001545 RID: 5445
	[Range(0f, 1f)]
	public float Value = 0.5f;

	// Token: 0x04001546 RID: 5446
	[Range(0f, 1f)]
	public float Value2 = 0.2f;

	// Token: 0x04001547 RID: 5447
	[Range(-1f, 2f)]
	public float Intensity = 1f;

	// Token: 0x04001548 RID: 5448
	[Range(0f, 10f)]
	private float Value4 = 1f;
}
