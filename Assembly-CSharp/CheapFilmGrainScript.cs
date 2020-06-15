using System;
using UnityEngine;

// Token: 0x02000231 RID: 561
public class CheapFilmGrainScript : MonoBehaviour
{
	// Token: 0x06001236 RID: 4662 RVA: 0x0008198E File Offset: 0x0007FB8E
	private void Update()
	{
		this.MyRenderer.material.mainTextureScale = new Vector2(UnityEngine.Random.Range(this.Floor, this.Ceiling), UnityEngine.Random.Range(this.Floor, this.Ceiling));
	}

	// Token: 0x04001590 RID: 5520
	public Renderer MyRenderer;

	// Token: 0x04001591 RID: 5521
	public float Floor = 100f;

	// Token: 0x04001592 RID: 5522
	public float Ceiling = 200f;
}
