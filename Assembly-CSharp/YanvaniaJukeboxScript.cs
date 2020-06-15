using System;
using UnityEngine;

// Token: 0x02000486 RID: 1158
public class YanvaniaJukeboxScript : MonoBehaviour
{
	// Token: 0x06001DEF RID: 7663 RVA: 0x001763B8 File Offset: 0x001745B8
	private void Update()
	{
		AudioSource component = base.GetComponent<AudioSource>();
		if (component.time + Time.deltaTime > component.clip.length)
		{
			component.clip = (this.Boss ? this.BossMain : this.ApproachMain);
			component.loop = true;
			component.Play();
		}
	}

	// Token: 0x06001DF0 RID: 7664 RVA: 0x0017640E File Offset: 0x0017460E
	public void BossBattle()
	{
		AudioSource component = base.GetComponent<AudioSource>();
		component.clip = this.BossIntro;
		component.loop = false;
		component.volume = 0.25f;
		component.Play();
		this.Boss = true;
	}

	// Token: 0x04003B63 RID: 15203
	public AudioClip BossIntro;

	// Token: 0x04003B64 RID: 15204
	public AudioClip BossMain;

	// Token: 0x04003B65 RID: 15205
	public AudioClip ApproachIntro;

	// Token: 0x04003B66 RID: 15206
	public AudioClip ApproachMain;

	// Token: 0x04003B67 RID: 15207
	public bool Boss;
}
