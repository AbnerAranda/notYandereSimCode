using System;
using UnityEngine;

// Token: 0x02000190 RID: 400
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/EXTRA/Rotation")]
public class CameraFilterPack_EXTRA_Rotation : MonoBehaviour
{
	// Token: 0x170002AF RID: 687
	// (get) Token: 0x06000E73 RID: 3699 RVA: 0x0006FFE7 File Offset: 0x0006E1E7
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

	// Token: 0x06000E74 RID: 3700 RVA: 0x0007001B File Offset: 0x0006E21B
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/EXTRA_Rotation");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000E75 RID: 3701 RVA: 0x0007003C File Offset: 0x0006E23C
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
			this.material.SetFloat("_Value", -this.Rotation);
			this.material.SetFloat("_Value2", this.PositionX);
			this.material.SetFloat("_Value3", this.PositionY);
			this.material.SetFloat("_Value4", this.Value4);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000E76 RID: 3702 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000E77 RID: 3703 RVA: 0x00070135 File Offset: 0x0006E335
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001151 RID: 4433
	public Shader SCShader;

	// Token: 0x04001152 RID: 4434
	private float TimeX = 1f;

	// Token: 0x04001153 RID: 4435
	private Material SCMaterial;

	// Token: 0x04001154 RID: 4436
	[Range(-360f, 360f)]
	public float Rotation;

	// Token: 0x04001155 RID: 4437
	[Range(-1f, 2f)]
	public float PositionX = 0.5f;

	// Token: 0x04001156 RID: 4438
	[Range(-1f, 2f)]
	public float PositionY = 0.5f;

	// Token: 0x04001157 RID: 4439
	[Range(0f, 10f)]
	private float Value4 = 1f;
}
