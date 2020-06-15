using System;
using UnityEngine;

// Token: 0x02000260 RID: 608
public class DelinquentVoicesScript : MonoBehaviour
{
	// Token: 0x0600132D RID: 4909 RVA: 0x000A077A File Offset: 0x0009E97A
	private void Start()
	{
		this.Timer = 5f;
	}

	// Token: 0x0600132E RID: 4910 RVA: 0x000A0788 File Offset: 0x0009E988
	private void Update()
	{
		if (this.Radio.MyAudio.isPlaying && this.Yandere.CanMove && Vector3.Distance(this.Yandere.transform.position, base.transform.position) < 5f)
		{
			this.Timer = Mathf.MoveTowards(this.Timer, 0f, Time.deltaTime);
			if (this.Timer == 0f && this.Yandere.Club != ClubType.Delinquent)
			{
				if (this.Yandere.Container == null)
				{
					while (this.RandomID == this.LastID)
					{
						this.RandomID = UnityEngine.Random.Range(0, this.Subtitle.DelinquentAnnoyClips.Length);
					}
					this.LastID = this.RandomID;
					this.Subtitle.UpdateLabel(SubtitleType.DelinquentAnnoy, this.RandomID, 3f);
				}
				else
				{
					while (this.RandomID == this.LastID)
					{
						this.RandomID = UnityEngine.Random.Range(0, this.Subtitle.DelinquentCaseClips.Length);
					}
					this.LastID = this.RandomID;
					this.Subtitle.UpdateLabel(SubtitleType.DelinquentCase, this.RandomID, 3f);
				}
				this.Timer = 5f;
			}
		}
	}

	// Token: 0x040019DC RID: 6620
	public YandereScript Yandere;

	// Token: 0x040019DD RID: 6621
	public RadioScript Radio;

	// Token: 0x040019DE RID: 6622
	public SubtitleScript Subtitle;

	// Token: 0x040019DF RID: 6623
	public float Timer;

	// Token: 0x040019E0 RID: 6624
	public int RandomID;

	// Token: 0x040019E1 RID: 6625
	public int LastID;
}
