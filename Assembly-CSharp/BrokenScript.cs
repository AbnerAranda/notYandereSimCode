using System;
using UnityEngine;

// Token: 0x020000EA RID: 234
public class BrokenScript : MonoBehaviour
{
	// Token: 0x06000A77 RID: 2679 RVA: 0x00056B3C File Offset: 0x00054D3C
	private void Start()
	{
		this.HairPhysics[0].enabled = false;
		this.HairPhysics[1].enabled = false;
		this.PermanentAngleR = this.TwintailR.eulerAngles;
		this.PermanentAngleL = this.TwintailL.eulerAngles;
		this.Subtitle = GameObject.Find("EventSubtitle").GetComponent<UILabel>();
		this.Yandere = GameObject.Find("YandereChan");
	}

	// Token: 0x06000A78 RID: 2680 RVA: 0x00056BAC File Offset: 0x00054DAC
	private void Update()
	{
		if (!this.Done)
		{
			float num = Vector3.Distance(this.Yandere.transform.position, base.transform.root.position);
			if (num < 6f)
			{
				if (num < 5f)
				{
					if (!this.Hunting)
					{
						this.Timer += Time.deltaTime;
						if (this.VoiceClip == null)
						{
							this.Subtitle.text = "";
						}
						if (this.Timer > 5f)
						{
							this.Timer = 0f;
							this.Subtitle.text = this.MutterTexts[this.ID];
							AudioClipPlayer.PlayAttached(this.Mutters[this.ID], base.transform.position, base.transform, 1f, 5f, out this.VoiceClip, this.Yandere.transform.position.y);
							this.ID++;
							if (this.ID == this.Mutters.Length)
							{
								this.ID = 1;
							}
						}
					}
					else if (!this.Began)
					{
						if (this.VoiceClip != null)
						{
							UnityEngine.Object.Destroy(this.VoiceClip);
						}
						this.Subtitle.text = "Do it.";
						AudioClipPlayer.PlayAttached(this.DoIt, base.transform.position, base.transform, 1f, 5f, out this.VoiceClip, this.Yandere.transform.position.y);
						this.Began = true;
					}
					else if (this.VoiceClip == null)
					{
						this.Subtitle.text = "...kill...kill...kill...";
						AudioClipPlayer.PlayAttached(this.KillKillKill, base.transform.position, base.transform, 1f, 5f, out this.VoiceClip, this.Yandere.transform.position.y);
					}
					float num2 = Mathf.Abs((num - 5f) * 0.2f);
					num2 = ((num2 > 1f) ? 1f : num2);
					this.Subtitle.transform.localScale = new Vector3(num2, num2, num2);
				}
				else
				{
					this.Subtitle.transform.localScale = Vector3.zero;
				}
			}
		}
		Vector3 eulerAngles = this.TwintailR.eulerAngles;
		Vector3 eulerAngles2 = this.TwintailL.eulerAngles;
		eulerAngles.x = this.PermanentAngleR.x;
		eulerAngles.z = this.PermanentAngleR.z;
		eulerAngles2.x = this.PermanentAngleL.x;
		eulerAngles2.z = this.PermanentAngleL.z;
		this.TwintailR.eulerAngles = eulerAngles;
		this.TwintailL.eulerAngles = eulerAngles2;
	}

	// Token: 0x04000B0B RID: 2827
	public DynamicBone[] HairPhysics;

	// Token: 0x04000B0C RID: 2828
	public string[] MutterTexts;

	// Token: 0x04000B0D RID: 2829
	public AudioClip[] Mutters;

	// Token: 0x04000B0E RID: 2830
	public Vector3 PermanentAngleR;

	// Token: 0x04000B0F RID: 2831
	public Vector3 PermanentAngleL;

	// Token: 0x04000B10 RID: 2832
	public Transform TwintailR;

	// Token: 0x04000B11 RID: 2833
	public Transform TwintailL;

	// Token: 0x04000B12 RID: 2834
	public AudioClip KillKillKill;

	// Token: 0x04000B13 RID: 2835
	public AudioClip Stab;

	// Token: 0x04000B14 RID: 2836
	public AudioClip DoIt;

	// Token: 0x04000B15 RID: 2837
	public GameObject VoiceClip;

	// Token: 0x04000B16 RID: 2838
	public GameObject Yandere;

	// Token: 0x04000B17 RID: 2839
	public UILabel Subtitle;

	// Token: 0x04000B18 RID: 2840
	public AudioSource MyAudio;

	// Token: 0x04000B19 RID: 2841
	public bool Hunting;

	// Token: 0x04000B1A RID: 2842
	public bool Stabbed;

	// Token: 0x04000B1B RID: 2843
	public bool Began;

	// Token: 0x04000B1C RID: 2844
	public bool Done;

	// Token: 0x04000B1D RID: 2845
	public float SuicideTimer;

	// Token: 0x04000B1E RID: 2846
	public float Timer;

	// Token: 0x04000B1F RID: 2847
	public int ID = 1;
}
