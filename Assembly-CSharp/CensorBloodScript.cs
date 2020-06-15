using System;
using UnityEngine;

// Token: 0x0200022C RID: 556
public class CensorBloodScript : MonoBehaviour
{
	// Token: 0x0600122A RID: 4650 RVA: 0x00081054 File Offset: 0x0007F254
	private void Start()
	{
		if (GameGlobals.CensorBlood)
		{
			this.MyParticles.main.startColor = new Color(1f, 1f, 1f, 1f);
			this.MyParticles.colorOverLifetime.enabled = false;
			this.MyParticles.GetComponent<Renderer>().material.mainTexture = this.Flower;
		}
	}

	// Token: 0x0400156A RID: 5482
	public ParticleSystem MyParticles;

	// Token: 0x0400156B RID: 5483
	public Texture Flower;
}
