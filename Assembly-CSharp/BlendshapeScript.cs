using System;
using UnityEngine;

// Token: 0x020000DC RID: 220
public class BlendshapeScript : MonoBehaviour
{
	// Token: 0x06000A4F RID: 2639 RVA: 0x000554A8 File Offset: 0x000536A8
	private void LateUpdate()
	{
		this.Happiness += Time.deltaTime * 10f;
		this.MyMesh.SetBlendShapeWeight(0, this.Happiness);
		this.Blink += Time.deltaTime * 10f;
		this.MyMesh.SetBlendShapeWeight(8, 100f);
	}

	// Token: 0x04000A9C RID: 2716
	public SkinnedMeshRenderer MyMesh;

	// Token: 0x04000A9D RID: 2717
	public float Happiness;

	// Token: 0x04000A9E RID: 2718
	public float Blink;
}
