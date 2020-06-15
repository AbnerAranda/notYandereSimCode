using System;
using UnityEngine;

// Token: 0x020003ED RID: 1005
public class SpeedrunMenuScript : MonoBehaviour
{
	// Token: 0x06001ADC RID: 6876 RVA: 0x0010EC11 File Offset: 0x0010CE11
	private void Start()
	{
		this.YandereAnim["f02_nierRun_00"].speed = 1.5f;
	}

	// Token: 0x06001ADD RID: 6877 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x04002B9C RID: 11164
	public Animation YandereAnim;
}
