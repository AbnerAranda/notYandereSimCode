using System;
using UnityEngine;

// Token: 0x020001F8 RID: 504
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/TV/ARCADE_Fast")]
public class CameraFilterPack_TV_ARCADE_Fast : MonoBehaviour
{
	// Token: 0x17000317 RID: 791
	// (get) Token: 0x06001107 RID: 4359 RVA: 0x0007BBC9 File Offset: 0x00079DC9
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

	// Token: 0x06001108 RID: 4360 RVA: 0x0007BBFD File Offset: 0x00079DFD
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_TV_Arcade1") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/TV_ARCADE_Fast");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06001109 RID: 4361 RVA: 0x0007BC34 File Offset: 0x00079E34
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
			this.material.SetFloat("_Value", this.Interferance_Size);
			this.material.SetFloat("_Value2", this.Interferance_Speed);
			this.material.SetFloat("_Value3", this.Contrast);
			this.material.SetFloat("Fade", this.Fade);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			this.material.SetTexture("_MainTex2", this.Texture2);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x0600110A RID: 4362 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x0600110B RID: 4363 RVA: 0x0007BD42 File Offset: 0x00079F42
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x0400141D RID: 5149
	public Shader SCShader;

	// Token: 0x0400141E RID: 5150
	private float TimeX = 1f;

	// Token: 0x0400141F RID: 5151
	private Material SCMaterial;

	// Token: 0x04001420 RID: 5152
	[Range(0f, 0.05f)]
	public float Interferance_Size = 0.02f;

	// Token: 0x04001421 RID: 5153
	[Range(0f, 4f)]
	public float Interferance_Speed = 0.5f;

	// Token: 0x04001422 RID: 5154
	[Range(0f, 10f)]
	public float Contrast = 1f;

	// Token: 0x04001423 RID: 5155
	[Range(0f, 1f)]
	public float Fade = 1f;

	// Token: 0x04001424 RID: 5156
	private Texture2D Texture2;
}
