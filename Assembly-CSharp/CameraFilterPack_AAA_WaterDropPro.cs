using System;
using UnityEngine;

// Token: 0x02000110 RID: 272
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/AAA/WaterDropPro")]
public class CameraFilterPack_AAA_WaterDropPro : MonoBehaviour
{
	// Token: 0x1700022F RID: 559
	// (get) Token: 0x06000B32 RID: 2866 RVA: 0x00061B32 File Offset: 0x0005FD32
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

	// Token: 0x06000B33 RID: 2867 RVA: 0x00061B66 File Offset: 0x0005FD66
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_WaterDrop") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/AAA_WaterDropPro");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000B34 RID: 2868 RVA: 0x00061B9C File Offset: 0x0005FD9C
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
			this.material.SetFloat("_Distortion", this.Distortion);
			this.material.SetFloat("_SizeX", this.SizeX);
			this.material.SetFloat("_SizeY", this.SizeY);
			this.material.SetFloat("_Speed", this.Speed);
			this.material.SetTexture("_MainTex2", this.Texture2);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000B35 RID: 2869 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000B36 RID: 2870 RVA: 0x00061C7D File Offset: 0x0005FE7D
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000DC2 RID: 3522
	public Shader SCShader;

	// Token: 0x04000DC3 RID: 3523
	private float TimeX = 1f;

	// Token: 0x04000DC4 RID: 3524
	[Range(8f, 64f)]
	public float Distortion = 8f;

	// Token: 0x04000DC5 RID: 3525
	[Range(0f, 7f)]
	public float SizeX = 1f;

	// Token: 0x04000DC6 RID: 3526
	[Range(0f, 7f)]
	public float SizeY = 0.5f;

	// Token: 0x04000DC7 RID: 3527
	[Range(0f, 10f)]
	public float Speed = 1f;

	// Token: 0x04000DC8 RID: 3528
	private Material SCMaterial;

	// Token: 0x04000DC9 RID: 3529
	private Texture2D Texture2;
}
