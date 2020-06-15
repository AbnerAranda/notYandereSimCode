using System;
using UnityEngine;

// Token: 0x02000217 RID: 535
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Vision/Aura")]
public class CameraFilterPack_Vision_Aura : MonoBehaviour
{
	// Token: 0x17000336 RID: 822
	// (get) Token: 0x060011C1 RID: 4545 RVA: 0x0007EB0A File Offset: 0x0007CD0A
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

	// Token: 0x060011C2 RID: 4546 RVA: 0x0007EB3E File Offset: 0x0007CD3E
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Vision_Aura");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x060011C3 RID: 4547 RVA: 0x0007EB60 File Offset: 0x0007CD60
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
			this.material.SetFloat("_Value", this.Twist);
			this.material.SetColor("_Value2", this.Color);
			this.material.SetFloat("_Value3", this.PosX);
			this.material.SetFloat("_Value4", this.PosY);
			this.material.SetFloat("_Value5", this.Speed);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x060011C4 RID: 4548 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x060011C5 RID: 4549 RVA: 0x0007EC6E File Offset: 0x0007CE6E
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040014D8 RID: 5336
	public Shader SCShader;

	// Token: 0x040014D9 RID: 5337
	private float TimeX = 1f;

	// Token: 0x040014DA RID: 5338
	private Material SCMaterial;

	// Token: 0x040014DB RID: 5339
	[Range(0f, 2f)]
	public float Twist = 1f;

	// Token: 0x040014DC RID: 5340
	[Range(-4f, 4f)]
	public float Speed = 1f;

	// Token: 0x040014DD RID: 5341
	public Color Color = new Color(0.16f, 0.57f, 0.19f);

	// Token: 0x040014DE RID: 5342
	[Range(-1f, 2f)]
	public float PosX = 0.5f;

	// Token: 0x040014DF RID: 5343
	[Range(-1f, 2f)]
	public float PosY = 0.5f;
}
