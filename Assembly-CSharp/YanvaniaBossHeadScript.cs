using System;
using UnityEngine;

// Token: 0x0200047C RID: 1148
public class YanvaniaBossHeadScript : MonoBehaviour
{
	// Token: 0x06001DCF RID: 7631 RVA: 0x00174621 File Offset: 0x00172821
	private void Update()
	{
		this.Timer -= Time.deltaTime;
	}

	// Token: 0x06001DD0 RID: 7632 RVA: 0x00174638 File Offset: 0x00172838
	private void OnTriggerEnter(Collider other)
	{
		if (this.Timer <= 0f && this.Dracula.NewTeleportEffect == null && other.gameObject.name == "Heart")
		{
			UnityEngine.Object.Instantiate<GameObject>(this.HitEffect, base.transform.position, Quaternion.identity);
			this.Timer = 1f;
			this.Dracula.TakeDamage();
		}
	}

	// Token: 0x04003B10 RID: 15120
	public YanvaniaDraculaScript Dracula;

	// Token: 0x04003B11 RID: 15121
	public GameObject HitEffect;

	// Token: 0x04003B12 RID: 15122
	public float Timer;
}
