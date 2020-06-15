using System;
using UnityEngine;

// Token: 0x02000358 RID: 856
public class ParticleDeathScript : MonoBehaviour
{
	// Token: 0x060018C0 RID: 6336 RVA: 0x000E36E6 File Offset: 0x000E18E6
	private void LateUpdate()
	{
		if (this.Particles.isPlaying && this.Particles.particleCount == 0)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x0400249C RID: 9372
	public ParticleSystem Particles;
}
