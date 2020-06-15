using System;
using UnityEngine;

// Token: 0x0200039E RID: 926
public class OsanaMondayBeforeClassEventScript : MonoBehaviour
{
	// Token: 0x060019DA RID: 6618 RVA: 0x000FE15C File Offset: 0x000FC35C
	private void Start()
	{
		this.EventSubtitle.transform.localScale = Vector3.zero;
		this.Bentos[1].SetActive(false);
		this.Bentos[2].SetActive(false);
		if (DateGlobals.Weekday != DayOfWeek.Monday)
		{
			base.enabled = false;
		}
	}

	// Token: 0x04002845 RID: 10309
	public StudentManagerScript StudentManager;

	// Token: 0x04002846 RID: 10310
	public EventManagerScript NextEvent;

	// Token: 0x04002847 RID: 10311
	public JukeboxScript Jukebox;

	// Token: 0x04002848 RID: 10312
	public UILabel EventSubtitle;

	// Token: 0x04002849 RID: 10313
	public YandereScript Yandere;

	// Token: 0x0400284A RID: 10314
	public ClockScript Clock;

	// Token: 0x0400284B RID: 10315
	public StudentScript Rival;

	// Token: 0x0400284C RID: 10316
	public Transform Destination;

	// Token: 0x0400284D RID: 10317
	public AudioClip SpeechClip;

	// Token: 0x0400284E RID: 10318
	public string[] SpeechText;

	// Token: 0x0400284F RID: 10319
	public float[] SpeechTime;

	// Token: 0x04002850 RID: 10320
	public GameObject AlarmDisc;

	// Token: 0x04002851 RID: 10321
	public GameObject VoiceClip;

	// Token: 0x04002852 RID: 10322
	public GameObject[] Bentos;

	// Token: 0x04002853 RID: 10323
	public bool EventActive;

	// Token: 0x04002854 RID: 10324
	public float Distance;

	// Token: 0x04002855 RID: 10325
	public float Scale;

	// Token: 0x04002856 RID: 10326
	public float Timer;

	// Token: 0x04002857 RID: 10327
	public int SpeechPhase = 1;

	// Token: 0x04002858 RID: 10328
	public int RivalID = 11;

	// Token: 0x04002859 RID: 10329
	public int Phase;

	// Token: 0x0400285A RID: 10330
	public int Frame;
}
