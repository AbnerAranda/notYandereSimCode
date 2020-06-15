using System;
using UnityEngine;

// Token: 0x0200038F RID: 911
public class RedStringScript : MonoBehaviour
{
	// Token: 0x060019AD RID: 6573 RVA: 0x000FB2C0 File Offset: 0x000F94C0
	private void LateUpdate()
	{
		base.transform.LookAt(this.Target.position);
		base.transform.localScale = new Vector3(1f, 1f, Vector3.Distance(base.transform.position, this.Target.position));
	}

	// Token: 0x040027BB RID: 10171
	public Transform Target;
}
