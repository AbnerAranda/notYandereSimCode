using System;
using UnityEngine;

// Token: 0x02000488 RID: 1160
public class YanvaniaTeleportEffectScript : MonoBehaviour
{
	// Token: 0x06001DF4 RID: 7668 RVA: 0x001764DC File Offset: 0x001746DC
	private void Start()
	{
		this.FirstBeam.material.color = new Color(this.FirstBeam.material.color.r, this.FirstBeam.material.color.g, this.FirstBeam.material.color.b, 0f);
		this.SecondBeam.material.color = new Color(this.SecondBeam.material.color.r, this.SecondBeam.material.color.g, this.SecondBeam.material.color.b, 0f);
		this.FirstBeam.transform.localScale = new Vector3(0f, this.FirstBeam.transform.localScale.y, 0f);
		this.SecondBeamParent.transform.localScale = new Vector3(this.SecondBeamParent.transform.localScale.x, 0f, this.SecondBeamParent.transform.localScale.z);
	}

	// Token: 0x06001DF5 RID: 7669 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x04003B69 RID: 15209
	public YanvaniaDraculaScript Dracula;

	// Token: 0x04003B6A RID: 15210
	public Transform SecondBeamParent;

	// Token: 0x04003B6B RID: 15211
	public Renderer SecondBeam;

	// Token: 0x04003B6C RID: 15212
	public Renderer FirstBeam;

	// Token: 0x04003B6D RID: 15213
	public bool InformedDracula;

	// Token: 0x04003B6E RID: 15214
	public float Timer;
}
