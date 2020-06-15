using System;
using UnityEngine;

// Token: 0x0200025E RID: 606
public class DelinquentMaskScript : MonoBehaviour
{
	// Token: 0x06001325 RID: 4901 RVA: 0x0009F548 File Offset: 0x0009D748
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.LeftAlt))
		{
			this.ID++;
			if (this.ID > 4)
			{
				this.ID = 0;
			}
			this.MyRenderer.mesh = this.Meshes[this.ID];
		}
	}

	// Token: 0x0400199B RID: 6555
	public MeshFilter MyRenderer;

	// Token: 0x0400199C RID: 6556
	public Mesh[] Meshes;

	// Token: 0x0400199D RID: 6557
	public int ID;
}
