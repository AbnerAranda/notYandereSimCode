using System;
using UnityEngine;

// Token: 0x0200023E RID: 574
public class ClubAmbienceScript : MonoBehaviour
{
	// Token: 0x06001264 RID: 4708 RVA: 0x00084BA4 File Offset: 0x00082DA4
	private void Update()
	{
		if (this.Yandere.position.y > base.transform.position.y - 0.1f && this.Yandere.position.y < base.transform.position.y + 0.1f)
		{
			if (Vector3.Distance(base.transform.position, this.Yandere.position) < 4f)
			{
				this.CreateAmbience = true;
				this.EffectJukebox = true;
			}
			else
			{
				this.CreateAmbience = false;
			}
		}
		if (this.EffectJukebox)
		{
			AudioSource component = base.GetComponent<AudioSource>();
			if (this.CreateAmbience)
			{
				component.volume = Mathf.MoveTowards(component.volume, this.MaxVolume, Time.deltaTime * 0.1f);
				this.Jukebox.ClubDip = Mathf.MoveTowards(this.Jukebox.ClubDip, this.ClubDip, Time.deltaTime * 0.1f);
				return;
			}
			component.volume = Mathf.MoveTowards(component.volume, 0f, Time.deltaTime * 0.1f);
			this.Jukebox.ClubDip = Mathf.MoveTowards(this.Jukebox.ClubDip, 0f, Time.deltaTime * 0.1f);
			if (this.Jukebox.ClubDip == 0f)
			{
				this.EffectJukebox = false;
			}
		}
	}

	// Token: 0x0400160C RID: 5644
	public JukeboxScript Jukebox;

	// Token: 0x0400160D RID: 5645
	public Transform Yandere;

	// Token: 0x0400160E RID: 5646
	public bool CreateAmbience;

	// Token: 0x0400160F RID: 5647
	public bool EffectJukebox;

	// Token: 0x04001610 RID: 5648
	public float ClubDip;

	// Token: 0x04001611 RID: 5649
	public float MaxVolume;
}
