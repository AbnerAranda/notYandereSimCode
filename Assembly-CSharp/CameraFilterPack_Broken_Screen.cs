using System;
using UnityEngine;

// Token: 0x02000149 RID: 329
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Broken/Broken_Screen")]
public class CameraFilterPack_Broken_Screen : MonoBehaviour
{
	// Token: 0x17000268 RID: 616
	// (get) Token: 0x06000CC7 RID: 3271 RVA: 0x000691CD File Offset: 0x000673CD
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

	// Token: 0x06000CC8 RID: 3272 RVA: 0x00069201 File Offset: 0x00067401
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_Broken_Screen1") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/Broken_Screen");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000CC9 RID: 3273 RVA: 0x00069238 File Offset: 0x00067438
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
			this.material.SetFloat("_Fade", this.Fade);
			this.material.SetFloat("_Shadow", this.Shadow);
			this.material.SetTexture("_MainTex2", this.Texture2);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000CCA RID: 3274 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000CCB RID: 3275 RVA: 0x000692ED File Offset: 0x000674ED
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000F9F RID: 3999
	public Shader SCShader;

	// Token: 0x04000FA0 RID: 4000
	private float TimeX = 1f;

	// Token: 0x04000FA1 RID: 4001
	[Range(0f, 1f)]
	public float Fade = 1f;

	// Token: 0x04000FA2 RID: 4002
	[Range(-1f, 1f)]
	public float Shadow = 1f;

	// Token: 0x04000FA3 RID: 4003
	private Material SCMaterial;

	// Token: 0x04000FA4 RID: 4004
	private Texture2D Texture2;
}
