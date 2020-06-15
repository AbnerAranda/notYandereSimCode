using System;
using UnityEngine;

// Token: 0x02000340 RID: 832
public class MythTreeScript : MonoBehaviour
{
	// Token: 0x0600186C RID: 6252 RVA: 0x000DC5EA File Offset: 0x000DA7EA
	private void Start()
	{
		if (SchemeGlobals.GetSchemeStage(2) > 2)
		{
			UnityEngine.Object.Destroy(this);
		}
	}

	// Token: 0x0600186D RID: 6253 RVA: 0x000DC5FC File Offset: 0x000DA7FC
	private void Update()
	{
		if (!this.Spoken)
		{
			if (this.Yandere.Inventory.Ring && Vector3.Distance(this.Yandere.transform.position, base.transform.position) < 5f)
			{
				this.EventSubtitle.transform.localScale = new Vector3(1f, 1f, 1f);
				this.EventSubtitle.text = "...that...ring...";
				this.Jukebox.Dip = 0.5f;
				this.Spoken = true;
				this.MyAudio.Play();
				return;
			}
		}
		else if (!this.MyAudio.isPlaying)
		{
			this.EventSubtitle.transform.localScale = Vector3.zero;
			this.EventSubtitle.text = string.Empty;
			this.Jukebox.Dip = 1f;
			UnityEngine.Object.Destroy(this);
		}
	}

	// Token: 0x040023A0 RID: 9120
	public UILabel EventSubtitle;

	// Token: 0x040023A1 RID: 9121
	public JukeboxScript Jukebox;

	// Token: 0x040023A2 RID: 9122
	public YandereScript Yandere;

	// Token: 0x040023A3 RID: 9123
	public bool Spoken;

	// Token: 0x040023A4 RID: 9124
	public AudioSource MyAudio;
}
