using System;
using UnityEngine;

// Token: 0x020004AE RID: 1198
public class LookAtSCP : MonoBehaviour
{
	// Token: 0x06001E66 RID: 7782 RVA: 0x0017E3D5 File Offset: 0x0017C5D5
	private void Start()
	{
		if (this.SCP == null)
		{
			this.SCP = GameObject.Find("SCPTarget").transform;
		}
		base.transform.LookAt(this.SCP);
	}

	// Token: 0x06001E67 RID: 7783 RVA: 0x0017E40B File Offset: 0x0017C60B
	private void LateUpdate()
	{
		base.transform.LookAt(this.SCP);
	}

	// Token: 0x04003CAB RID: 15531
	public Transform SCP;
}
