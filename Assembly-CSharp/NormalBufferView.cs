using System;
using UnityEngine;

// Token: 0x02000497 RID: 1175
public class NormalBufferView : MonoBehaviour
{
	// Token: 0x06001E2D RID: 7725 RVA: 0x0017C1CD File Offset: 0x0017A3CD
	public void ApplyNormalView()
	{
		this.camera.SetReplacementShader(this.normalShader, "RenderType");
	}

	// Token: 0x06001E2E RID: 7726 RVA: 0x0017C1E5 File Offset: 0x0017A3E5
	public void DisableNormalView()
	{
		this.camera.ResetReplacementShader();
	}

	// Token: 0x04003C67 RID: 15463
	[SerializeField]
	private Camera camera;

	// Token: 0x04003C68 RID: 15464
	[SerializeField]
	private Shader normalShader;
}
