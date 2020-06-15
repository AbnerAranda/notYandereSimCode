using System;
using UnityEngine;

// Token: 0x02000269 RID: 617
public class DipJukeboxScript : MonoBehaviour
{
	// Token: 0x0600134C RID: 4940 RVA: 0x000A502C File Offset: 0x000A322C
	private void Update()
	{
		if (this.MyAudio.isPlaying)
		{
			float num = Vector3.Distance(this.Yandere.position, base.transform.position);
			if (num < 8f)
			{
				this.Jukebox.ClubDip = Mathf.MoveTowards(this.Jukebox.ClubDip, (7f - num) * 0.25f * this.Jukebox.Volume, Time.deltaTime);
				if (this.Jukebox.ClubDip < 0f)
				{
					this.Jukebox.ClubDip = 0f;
				}
				if (this.Jukebox.ClubDip > this.Jukebox.Volume)
				{
					this.Jukebox.ClubDip = this.Jukebox.Volume;
					return;
				}
			}
		}
		else if (this.MyAudio.isPlaying)
		{
			this.Jukebox.ClubDip = 0f;
		}
	}

	// Token: 0x04001A4A RID: 6730
	public JukeboxScript Jukebox;

	// Token: 0x04001A4B RID: 6731
	public AudioSource MyAudio;

	// Token: 0x04001A4C RID: 6732
	public Transform Yandere;
}
