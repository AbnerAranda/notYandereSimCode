using System;
using UnityEngine;

// Token: 0x0200025D RID: 605
public class DelinquentManagerScript : MonoBehaviour
{
	// Token: 0x06001320 RID: 4896 RVA: 0x0009F1E7 File Offset: 0x0009D3E7
	private void Start()
	{
		this.Delinquents.SetActive(false);
		this.TimerMax = 15f;
		this.Timer = 15f;
		this.Phase++;
	}

	// Token: 0x06001321 RID: 4897 RVA: 0x0009F21C File Offset: 0x0009D41C
	private void Update()
	{
		this.SpeechTimer = Mathf.MoveTowards(this.SpeechTimer, 0f, Time.deltaTime);
		if (this.Attacker != null && !this.Attacker.Attacking && this.Attacker.ExpressedSurprise && this.Attacker.Run && !this.Aggro)
		{
			AudioSource component = base.GetComponent<AudioSource>();
			component.clip = this.Attacker.AggroClips[UnityEngine.Random.Range(0, this.Attacker.AggroClips.Length)];
			component.Play();
			this.Aggro = true;
		}
		if (this.Panel.activeInHierarchy && this.Clock.HourTime > this.NextTime[this.Phase])
		{
			if (this.Phase == 3 && this.Clock.HourTime > 7.25f)
			{
				this.TimerMax = 75f;
				this.Timer = 75f;
				this.Phase++;
			}
			else if (this.Phase == 5 && this.Clock.HourTime > 8.5f)
			{
				this.TimerMax = 285f;
				this.Timer = 285f;
				this.Phase++;
			}
			else if (this.Phase == 7 && this.Clock.HourTime > 13.25f)
			{
				this.TimerMax = 15f;
				this.Timer = 15f;
				this.Phase++;
			}
			else if (this.Phase == 9 && this.Clock.HourTime > 13.5f)
			{
				this.TimerMax = 135f;
				this.Timer = 135f;
				this.Phase++;
			}
			if (this.Attacker == null)
			{
				this.Timer -= Time.deltaTime * (this.Clock.TimeSpeed / 60f);
			}
			this.Circle.fillAmount = 1f - this.Timer / this.TimerMax;
			if (this.Timer <= 0f)
			{
				this.Delinquents.SetActive(!this.Delinquents.activeInHierarchy);
				if (this.Phase < 8)
				{
					this.Phase++;
					return;
				}
				this.Delinquents.SetActive(false);
				this.Panel.SetActive(false);
			}
		}
	}

	// Token: 0x06001322 RID: 4898 RVA: 0x0009F494 File Offset: 0x0009D694
	public void CheckTime()
	{
		if (this.Clock.HourTime < 13f)
		{
			this.Delinquents.SetActive(false);
			this.TimerMax = 15f;
			this.Timer = 15f;
			this.Phase = 6;
			return;
		}
		if (this.Clock.HourTime < 15.5f)
		{
			this.Delinquents.SetActive(false);
			this.TimerMax = 15f;
			this.Timer = 15f;
			this.Phase = 8;
		}
	}

	// Token: 0x06001323 RID: 4899 RVA: 0x0009F518 File Offset: 0x0009D718
	public void EasterEgg()
	{
		this.RapBeat.SetActive(true);
		this.Mirror.Limit++;
	}

	// Token: 0x0400198D RID: 6541
	public GameObject Delinquents;

	// Token: 0x0400198E RID: 6542
	public GameObject RapBeat;

	// Token: 0x0400198F RID: 6543
	public GameObject Panel;

	// Token: 0x04001990 RID: 6544
	public float[] NextTime;

	// Token: 0x04001991 RID: 6545
	public DelinquentScript Attacker;

	// Token: 0x04001992 RID: 6546
	public MirrorScript Mirror;

	// Token: 0x04001993 RID: 6547
	public UILabel TimeLabel;

	// Token: 0x04001994 RID: 6548
	public ClockScript Clock;

	// Token: 0x04001995 RID: 6549
	public UISprite Circle;

	// Token: 0x04001996 RID: 6550
	public float SpeechTimer;

	// Token: 0x04001997 RID: 6551
	public float TimerMax;

	// Token: 0x04001998 RID: 6552
	public float Timer;

	// Token: 0x04001999 RID: 6553
	public bool Aggro;

	// Token: 0x0400199A RID: 6554
	public int Phase = 1;
}
