using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000191 RID: 401
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/EXTRA/SHOWFPS")]
public class CameraFilterPack_EXTRA_SHOWFPS : MonoBehaviour
{
	// Token: 0x170002B0 RID: 688
	// (get) Token: 0x06000E79 RID: 3705 RVA: 0x00070183 File Offset: 0x0006E383
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

	// Token: 0x06000E7A RID: 3706 RVA: 0x000701B7 File Offset: 0x0006E3B7
	private void Start()
	{
		this.FPS = 0;
		base.StartCoroutine(this.FPSX());
		this.SCShader = Shader.Find("CameraFilterPack/EXTRA_SHOWFPS");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000E7B RID: 3707 RVA: 0x000701EC File Offset: 0x0006E3EC
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
			this.material.SetFloat("_Value2", (float)this.FPS);
			this.material.SetFloat("_Value3", this.Value3);
			this.material.SetFloat("_Value4", this.Value4);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000E7C RID: 3708 RVA: 0x000702E5 File Offset: 0x0006E4E5
	private IEnumerator FPSX()
	{
		for (;;)
		{
			float num = this.accum / (float)this.frames;
			this.FPS = (int)num;
			this.accum = 0f;
			this.frames = 0;
			yield return new WaitForSeconds(this.frequency);
		}
		yield break;
	}

	// Token: 0x06000E7D RID: 3709 RVA: 0x000702F4 File Offset: 0x0006E4F4
	private void Update()
	{
		this.accum += Time.timeScale / Time.deltaTime;
		this.frames++;
	}

	// Token: 0x06000E7E RID: 3710 RVA: 0x0007031C File Offset: 0x0006E51C
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001158 RID: 4440
	public Shader SCShader;

	// Token: 0x04001159 RID: 4441
	private float TimeX = 1f;

	// Token: 0x0400115A RID: 4442
	private Material SCMaterial;

	// Token: 0x0400115B RID: 4443
	[Range(8f, 42f)]
	public float Size = 12f;

	// Token: 0x0400115C RID: 4444
	[Range(0f, 100f)]
	private int FPS = 1;

	// Token: 0x0400115D RID: 4445
	[Range(0f, 10f)]
	private float Value3 = 1f;

	// Token: 0x0400115E RID: 4446
	[Range(0f, 10f)]
	private float Value4 = 1f;

	// Token: 0x0400115F RID: 4447
	private float accum;

	// Token: 0x04001160 RID: 4448
	private int frames;

	// Token: 0x04001161 RID: 4449
	public float frequency = 0.5f;
}
