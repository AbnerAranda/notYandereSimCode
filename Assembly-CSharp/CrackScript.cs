using System;
using UnityEngine;

// Token: 0x0200024F RID: 591
public class CrackScript : MonoBehaviour
{
	// Token: 0x060012C1 RID: 4801 RVA: 0x00096AAE File Offset: 0x00094CAE
	private void Update()
	{
		this.Texture.fillAmount += Time.deltaTime * 10f;
	}

	// Token: 0x04001870 RID: 6256
	public UITexture Texture;
}
