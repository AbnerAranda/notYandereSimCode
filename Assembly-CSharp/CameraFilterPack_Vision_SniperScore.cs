using System;
using UnityEngine;

// Token: 0x02000221 RID: 545
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Vision/SniperScore")]
public class CameraFilterPack_Vision_SniperScore : MonoBehaviour
{
	// Token: 0x17000340 RID: 832
	// (get) Token: 0x060011FD RID: 4605 RVA: 0x0007FCD1 File Offset: 0x0007DED1
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

	// Token: 0x060011FE RID: 4606 RVA: 0x0007FD05 File Offset: 0x0007DF05
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Vision_SniperScore");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x060011FF RID: 4607 RVA: 0x0007FD28 File Offset: 0x0007DF28
	private void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
	{
		if (this.SCShader != null)
		{
			this.TimeX += Time.deltaTime;
			if (this.TimeX > 100f)
			{
				this.TimeX = 0f;
			}
			this.material.SetFloat("_Fade", this.Fade);
			this.material.SetFloat("_TimeX", this.TimeX);
			this.material.SetFloat("_Value", this.Size);
			this.material.SetFloat("_Value2", this.Smooth);
			this.material.SetFloat("_Value3", this.StretchX);
			this.material.SetFloat("_Value4", this.StretchY);
			this.material.SetFloat("_Cible", this._Cible);
			this.material.SetFloat("_ExtraColor", this._ExtraColor);
			this.material.SetFloat("_Distortion", this._Distortion);
			this.material.SetFloat("_PosX", this._PosX);
			this.material.SetFloat("_PosY", this._PosY);
			this.material.SetColor("_Tint", this._Tint);
			this.material.SetFloat("_ExtraLight", this._ExtraLight);
			Vector2 vector = new Vector2((float)Screen.width, (float)Screen.height);
			this.material.SetVector("_ScreenResolution", new Vector4(vector.x, vector.y, vector.y / vector.x, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06001200 RID: 4608 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06001201 RID: 4609 RVA: 0x0007FEE9 File Offset: 0x0007E0E9
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001525 RID: 5413
	public Shader SCShader;

	// Token: 0x04001526 RID: 5414
	private float TimeX = 1f;

	// Token: 0x04001527 RID: 5415
	private Material SCMaterial;

	// Token: 0x04001528 RID: 5416
	[Range(0f, 1f)]
	public float Fade = 1f;

	// Token: 0x04001529 RID: 5417
	[Range(0f, 1f)]
	public float Size = 0.45f;

	// Token: 0x0400152A RID: 5418
	[Range(0.01f, 0.4f)]
	public float Smooth = 0.045f;

	// Token: 0x0400152B RID: 5419
	[Range(0f, 1f)]
	public float _Cible = 0.5f;

	// Token: 0x0400152C RID: 5420
	[Range(0f, 1f)]
	public float _Distortion = 0.5f;

	// Token: 0x0400152D RID: 5421
	[Range(0f, 1f)]
	public float _ExtraColor = 0.5f;

	// Token: 0x0400152E RID: 5422
	[Range(0f, 1f)]
	public float _ExtraLight = 0.35f;

	// Token: 0x0400152F RID: 5423
	public Color _Tint = new Color(0f, 0.6f, 0f, 0.25f);

	// Token: 0x04001530 RID: 5424
	[Range(0f, 10f)]
	private float StretchX = 1f;

	// Token: 0x04001531 RID: 5425
	[Range(0f, 10f)]
	private float StretchY = 1f;

	// Token: 0x04001532 RID: 5426
	[Range(-1f, 1f)]
	public float _PosX;

	// Token: 0x04001533 RID: 5427
	[Range(-1f, 1f)]
	public float _PosY;
}
