using System;
using UnityEngine;

// Token: 0x02000498 RID: 1176
public class BarScript : MonoBehaviour
{
	// Token: 0x06001E30 RID: 7728 RVA: 0x0017C1F2 File Offset: 0x0017A3F2
	private void Start()
	{
		base.transform.localScale = new Vector3(0f, 1f, 1f);
	}

	// Token: 0x06001E31 RID: 7729 RVA: 0x0017C214 File Offset: 0x0017A414
	private void Update()
	{
		base.transform.localScale = new Vector3(base.transform.localScale.x + this.Speed * Time.deltaTime, 1f, 1f);
		if ((double)base.transform.localScale.x > 0.1)
		{
			base.transform.localScale = new Vector3(0f, 1f, 1f);
		}
	}

	// Token: 0x04003C69 RID: 15465
	public float Speed;
}
