using System;
using UnityEngine;

// Token: 0x02000478 RID: 1144
public class YandereShowerScript : MonoBehaviour
{
	// Token: 0x06001DC5 RID: 7621 RVA: 0x00174234 File Offset: 0x00172434
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			if (this.Yandere.Schoolwear > 0 || this.Yandere.Chased || this.Yandere.Chasers > 0 || this.Yandere.Bloodiness == 0f)
			{
				this.Prompt.Circle[0].fillAmount = 1f;
			}
			else
			{
				AudioSource.PlayClipAtPoint(this.CurtainOpen, base.transform.position);
				this.CensorSteam.SetActive(true);
				this.MyAudio.Play();
				this.Yandere.EmptyHands();
				this.Yandere.YandereShower = this;
				this.Yandere.CanMove = false;
				this.Yandere.Bathing = true;
				this.UpdateCurtain = true;
				this.Open = true;
				this.Timer = 6f;
			}
		}
		if (this.UpdateCurtain)
		{
			this.Timer = Mathf.MoveTowards(this.Timer, 0f, Time.deltaTime);
			if (this.Timer < 1f)
			{
				if (this.Open)
				{
					AudioSource.PlayClipAtPoint(this.CurtainClose, base.transform.position);
				}
				this.Open = false;
				if (this.Timer == 0f)
				{
					this.CensorSteam.SetActive(false);
					this.UpdateCurtain = false;
				}
			}
			if (this.Open)
			{
				this.OpenValue = Mathf.Lerp(this.OpenValue, 0f, Time.deltaTime * 10f);
				this.Curtain.SetBlendShapeWeight(0, this.OpenValue);
				return;
			}
			this.OpenValue = Mathf.Lerp(this.OpenValue, 100f, Time.deltaTime * 10f);
			this.Curtain.SetBlendShapeWeight(0, this.OpenValue);
		}
	}

	// Token: 0x04003AFD RID: 15101
	public SkinnedMeshRenderer Curtain;

	// Token: 0x04003AFE RID: 15102
	public GameObject CensorSteam;

	// Token: 0x04003AFF RID: 15103
	public YandereScript Yandere;

	// Token: 0x04003B00 RID: 15104
	public PromptScript Prompt;

	// Token: 0x04003B01 RID: 15105
	public Transform BatheSpot;

	// Token: 0x04003B02 RID: 15106
	public float OpenValue;

	// Token: 0x04003B03 RID: 15107
	public float Timer;

	// Token: 0x04003B04 RID: 15108
	public bool UpdateCurtain;

	// Token: 0x04003B05 RID: 15109
	public bool Open;

	// Token: 0x04003B06 RID: 15110
	public AudioSource MyAudio;

	// Token: 0x04003B07 RID: 15111
	public AudioClip CurtainClose;

	// Token: 0x04003B08 RID: 15112
	public AudioClip CurtainOpen;
}
