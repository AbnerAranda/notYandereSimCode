using System;
using UnityEngine;

// Token: 0x0200010F RID: 271
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/AAA/WaterDrop")]
public class CameraFilterPack_AAA_WaterDrop : MonoBehaviour
{
	// Token: 0x1700022E RID: 558
	// (get) Token: 0x06000B2C RID: 2860 RVA: 0x0006198C File Offset: 0x0005FB8C
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

	// Token: 0x06000B2D RID: 2861 RVA: 0x000619C0 File Offset: 0x0005FBC0
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_WaterDrop") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/AAA_WaterDrop");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000B2E RID: 2862 RVA: 0x000619F8 File Offset: 0x0005FBF8
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

	// Token: 0x06000B2F RID: 2863 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000B30 RID: 2864 RVA: 0x00061AD9 File Offset: 0x0005FCD9
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000DBA RID: 3514
	public Shader SCShader;

	// Token: 0x04000DBB RID: 3515
	private float TimeX = 1f;

	// Token: 0x04000DBC RID: 3516
	[Range(8f, 64f)]
	public float Distortion = 8f;

	// Token: 0x04000DBD RID: 3517
	[Range(0f, 7f)]
	public float SizeX = 1f;

	// Token: 0x04000DBE RID: 3518
	[Range(0f, 7f)]
	public float SizeY = 0.5f;

	// Token: 0x04000DBF RID: 3519
	[Range(0f, 10f)]
	public float Speed = 1f;

	// Token: 0x04000DC0 RID: 3520
	private Material SCMaterial;

	// Token: 0x04000DC1 RID: 3521
	private Texture2D Texture2;
}
