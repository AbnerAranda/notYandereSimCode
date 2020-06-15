using System;
using UnityEngine;

// Token: 0x020000CE RID: 206
public class ArcScript : MonoBehaviour
{
	// Token: 0x06000A13 RID: 2579 RVA: 0x0005064C File Offset: 0x0004E84C
	private void Update()
	{
		this.Timer += Time.deltaTime;
		if (this.Timer > 1f)
		{
			UnityEngine.Object.Instantiate<GameObject>(this.ArcTrail, base.transform.position, base.transform.rotation).GetComponent<Rigidbody>().AddRelativeForce(ArcScript.NEW_ARC_RELATIVE_FORCE);
			this.Timer = 0f;
		}
	}

	// Token: 0x04000A24 RID: 2596
	private static readonly Vector3 NEW_ARC_RELATIVE_FORCE = Vector3.forward * 250f;

	// Token: 0x04000A25 RID: 2597
	public GameObject ArcTrail;

	// Token: 0x04000A26 RID: 2598
	public float Timer;
}
