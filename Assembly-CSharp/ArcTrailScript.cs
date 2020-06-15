using System;
using UnityEngine;

// Token: 0x020000CF RID: 207
public class ArcTrailScript : MonoBehaviour
{
	// Token: 0x06000A16 RID: 2582 RVA: 0x000506C9 File Offset: 0x0004E8C9
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 9)
		{
			this.Trail.material.SetColor("_TintColor", ArcTrailScript.TRAIL_TINT_COLOR);
		}
	}

	// Token: 0x04000A27 RID: 2599
	private static readonly Color TRAIL_TINT_COLOR = new Color(1f, 0f, 0f, 1f);

	// Token: 0x04000A28 RID: 2600
	public TrailRenderer Trail;
}
