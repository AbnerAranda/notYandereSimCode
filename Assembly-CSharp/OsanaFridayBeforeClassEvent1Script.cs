using System;
using UnityEngine;

// Token: 0x0200039B RID: 923
public class OsanaFridayBeforeClassEvent1Script : MonoBehaviour
{
	// Token: 0x060019D6 RID: 6614 RVA: 0x000FE0EB File Offset: 0x000FC2EB
	private void Start()
	{
		this.EventSubtitle.transform.localScale = Vector3.zero;
		if (DateGlobals.Weekday != this.EventDay)
		{
			base.enabled = false;
		}
		this.Yoogle.SetActive(false);
	}

	// Token: 0x0400280F RID: 10255
	public OsanaFridayBeforeClassEvent2Script OtherEvent;

	// Token: 0x04002810 RID: 10256
	public StudentManagerScript StudentManager;

	// Token: 0x04002811 RID: 10257
	public JukeboxScript Jukebox;

	// Token: 0x04002812 RID: 10258
	public UILabel EventSubtitle;

	// Token: 0x04002813 RID: 10259
	public YandereScript Yandere;

	// Token: 0x04002814 RID: 10260
	public ClockScript Clock;

	// Token: 0x04002815 RID: 10261
	public StudentScript Rival;

	// Token: 0x04002816 RID: 10262
	public Transform Location;

	// Token: 0x04002817 RID: 10263
	public AudioClip[] SpeechClip;

	// Token: 0x04002818 RID: 10264
	public string[] SpeechText;

	// Token: 0x04002819 RID: 10265
	public string EventAnim;

	// Token: 0x0400281A RID: 10266
	public GameObject AlarmDisc;

	// Token: 0x0400281B RID: 10267
	public GameObject VoiceClip;

	// Token: 0x0400281C RID: 10268
	public GameObject Yoogle;

	// Token: 0x0400281D RID: 10269
	public float Distance;

	// Token: 0x0400281E RID: 10270
	public float Scale;

	// Token: 0x0400281F RID: 10271
	public float Timer;

	// Token: 0x04002820 RID: 10272
	public DayOfWeek EventDay;

	// Token: 0x04002821 RID: 10273
	public int RivalID = 11;

	// Token: 0x04002822 RID: 10274
	public int Phase;

	// Token: 0x04002823 RID: 10275
	public int Frame;

	// Token: 0x04002824 RID: 10276
	public Vector3 OriginalPosition;

	// Token: 0x04002825 RID: 10277
	public Vector3 OriginalRotation;
}
