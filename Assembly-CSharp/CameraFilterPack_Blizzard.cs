using System;
using UnityEngine;

// Token: 0x02000138 RID: 312
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Weather/Blizzard")]
public class CameraFilterPack_Blizzard : MonoBehaviour
{
	// Token: 0x17000257 RID: 599
	// (get) Token: 0x06000C61 RID: 3169 RVA: 0x0006750D File Offset: 0x0006570D
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

	// Token: 0x06000C62 RID: 3170 RVA: 0x00067541 File Offset: 0x00065741
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_Blizzard1") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/Blizzard");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000C63 RID: 3171 RVA: 0x00067578 File Offset: 0x00065778
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
			this.material.SetFloat("_Value", this._Speed);
			this.material.SetFloat("_Value2", this._Size);
			this.material.SetFloat("_Value3", this._Fade);
			this.material.SetTexture("_MainTex2", this.Texture2);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000C64 RID: 3172 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000C65 RID: 3173 RVA: 0x00067643 File Offset: 0x00065843
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000F33 RID: 3891
	public Shader SCShader;

	// Token: 0x04000F34 RID: 3892
	private float TimeX = 1f;

	// Token: 0x04000F35 RID: 3893
	[Range(0f, 2f)]
	public float _Speed = 1f;

	// Token: 0x04000F36 RID: 3894
	[Range(0.2f, 2f)]
	public float _Size = 1f;

	// Token: 0x04000F37 RID: 3895
	[Range(0f, 1f)]
	public float _Fade = 1f;

	// Token: 0x04000F38 RID: 3896
	private Material SCMaterial;

	// Token: 0x04000F39 RID: 3897
	private Texture2D Texture2;
}
