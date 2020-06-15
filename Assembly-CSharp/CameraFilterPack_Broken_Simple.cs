using System;
using UnityEngine;

// Token: 0x0200014A RID: 330
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Broken/Simple")]
public class CameraFilterPack_Broken_Simple : MonoBehaviour
{
	// Token: 0x17000269 RID: 617
	// (get) Token: 0x06000CCD RID: 3277 RVA: 0x00069330 File Offset: 0x00067530
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

	// Token: 0x06000CCE RID: 3278 RVA: 0x00069364 File Offset: 0x00067564
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/CameraFilterPack_Broken_Simple");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000CCF RID: 3279 RVA: 0x00069388 File Offset: 0x00067588
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
			this.material.SetFloat("_Speed", this.__Speed);
			this.material.SetFloat("Broke1", this._Broke1);
			this.material.SetFloat("Broke2", this._Broke2);
			this.material.SetFloat("PosX", this._PosX);
			this.material.SetFloat("PosY", this._PosY);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000CD0 RID: 3280 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000CD1 RID: 3281 RVA: 0x00069496 File Offset: 0x00067696
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000FA5 RID: 4005
	public Shader SCShader;

	// Token: 0x04000FA6 RID: 4006
	private float TimeX = 1f;

	// Token: 0x04000FA7 RID: 4007
	private Material SCMaterial;

	// Token: 0x04000FA8 RID: 4008
	[Range(0f, 1f)]
	public float __Speed = 1f;

	// Token: 0x04000FA9 RID: 4009
	[Range(0f, 1f)]
	public float _Broke1 = 1f;

	// Token: 0x04000FAA RID: 4010
	[Range(0f, 1f)]
	public float _Broke2 = 1f;

	// Token: 0x04000FAB RID: 4011
	[Range(0f, 1f)]
	public float _PosX = 0.5f;

	// Token: 0x04000FAC RID: 4012
	[Range(0f, 1f)]
	public float _PosY = 0.5f;
}
