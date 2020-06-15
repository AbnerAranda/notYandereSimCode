using System;
using UnityEngine;

// Token: 0x020001F0 RID: 496
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/VHS/Real VHS HQ")]
public class CameraFilterPack_Real_VHS : MonoBehaviour
{
	// Token: 0x1700030F RID: 783
	// (get) Token: 0x060010D6 RID: 4310 RVA: 0x0007AFD7 File Offset: 0x000791D7
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

	// Token: 0x060010D7 RID: 4311 RVA: 0x0007B00C File Offset: 0x0007920C
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Real_VHS");
		this.VHS = (Resources.Load("CameraFilterPack_VHS1") as Texture2D);
		this.VHS2 = (Resources.Load("CameraFilterPack_VHS2") as Texture2D);
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x060010D8 RID: 4312 RVA: 0x0007B062 File Offset: 0x00079262
	public static Texture2D GetRTPixels(Texture2D t, RenderTexture rt, int sx, int sy)
	{
		RenderTexture active = RenderTexture.active;
		RenderTexture.active = rt;
		t.ReadPixels(new Rect(0f, 0f, (float)t.width, (float)t.height), 0, 0);
		RenderTexture.active = active;
		return t;
	}

	// Token: 0x060010D9 RID: 4313 RVA: 0x0007B09C File Offset: 0x0007929C
	private void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
	{
		if (this.SCShader != null)
		{
			this.material.SetTexture("VHS", this.VHS);
			this.material.SetTexture("VHS2", this.VHS2);
			this.material.SetFloat("TRACKING", this.TRACKING);
			this.material.SetFloat("JITTER", this.JITTER);
			this.material.SetFloat("GLITCH", this.GLITCH);
			this.material.SetFloat("NOISE", this.NOISE);
			this.material.SetFloat("Brightness", this.Brightness);
			this.material.SetFloat("CONTRAST", 1f - this.Constrast);
			int width = 382;
			int height = 576;
			RenderTexture temporary = RenderTexture.GetTemporary(width, height, 0);
			temporary.filterMode = FilterMode.Trilinear;
			Graphics.Blit(sourceTexture, temporary, this.material);
			Graphics.Blit(temporary, destTexture);
			RenderTexture.ReleaseTemporary(temporary);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x060010DA RID: 4314 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x060010DB RID: 4315 RVA: 0x0007B1AC File Offset: 0x000793AC
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040013F0 RID: 5104
	public Shader SCShader;

	// Token: 0x040013F1 RID: 5105
	private Material SCMaterial;

	// Token: 0x040013F2 RID: 5106
	private Texture2D VHS;

	// Token: 0x040013F3 RID: 5107
	private Texture2D VHS2;

	// Token: 0x040013F4 RID: 5108
	[Range(0f, 1f)]
	public float TRACKING = 0.212f;

	// Token: 0x040013F5 RID: 5109
	[Range(0f, 1f)]
	public float JITTER = 1f;

	// Token: 0x040013F6 RID: 5110
	[Range(0f, 1f)]
	public float GLITCH = 1f;

	// Token: 0x040013F7 RID: 5111
	[Range(0f, 1f)]
	public float NOISE = 1f;

	// Token: 0x040013F8 RID: 5112
	[Range(-1f, 1f)]
	public float Brightness;

	// Token: 0x040013F9 RID: 5113
	[Range(0f, 1.5f)]
	public float Constrast = 1f;
}
