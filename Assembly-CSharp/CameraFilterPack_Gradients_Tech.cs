using System;
using UnityEngine;

// Token: 0x020001C9 RID: 457
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Gradients/Tech")]
public class CameraFilterPack_Gradients_Tech : MonoBehaviour
{
	// Token: 0x170002E8 RID: 744
	// (get) Token: 0x06000FCA RID: 4042 RVA: 0x00075B8A File Offset: 0x00073D8A
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

	// Token: 0x06000FCB RID: 4043 RVA: 0x00075BBE File Offset: 0x00073DBE
	private void Start()
	{
		this.SCShader = Shader.Find(this.ShaderName);
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000FCC RID: 4044 RVA: 0x00075BE0 File Offset: 0x00073DE0
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
			this.material.SetFloat("_Value", this.Switch);
			this.material.SetFloat("_Value2", this.Fade);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000FCD RID: 4045 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000FCE RID: 4046 RVA: 0x00075CAC File Offset: 0x00073EAC
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040012CA RID: 4810
	public Shader SCShader;

	// Token: 0x040012CB RID: 4811
	private string ShaderName = "CameraFilterPack/Gradients_Tech";

	// Token: 0x040012CC RID: 4812
	private float TimeX = 1f;

	// Token: 0x040012CD RID: 4813
	private Material SCMaterial;

	// Token: 0x040012CE RID: 4814
	[Range(0f, 1f)]
	public float Switch = 1f;

	// Token: 0x040012CF RID: 4815
	[Range(0f, 1f)]
	public float Fade = 1f;
}
