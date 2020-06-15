using System;
using UnityEngine;

// Token: 0x0200049B RID: 1179
public class LargeTextScript : MonoBehaviour
{
	// Token: 0x06001E38 RID: 7736 RVA: 0x0017C3B7 File Offset: 0x0017A5B7
	private void Start()
	{
		this.Label.text = this.String[this.ID];
	}

	// Token: 0x06001E39 RID: 7737 RVA: 0x0017C3D1 File Offset: 0x0017A5D1
	private void Update()
	{
		if (Input.GetKeyDown("space"))
		{
			this.ID++;
			this.Label.text = this.String[this.ID];
		}
	}

	// Token: 0x04003C6E RID: 15470
	public UILabel Label;

	// Token: 0x04003C6F RID: 15471
	public string[] String;

	// Token: 0x04003C70 RID: 15472
	public int ID;
}
