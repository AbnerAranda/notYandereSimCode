using System;
using UnityEngine;

// Token: 0x020003A1 RID: 929
public class OsanaThursdayAfterClassEventScript : MonoBehaviour
{
	// Token: 0x04002886 RID: 10374
	public StudentManagerScript StudentManager;

	// Token: 0x04002887 RID: 10375
	public PhoneMinigameScript PhoneMinigame;

	// Token: 0x04002888 RID: 10376
	public JukeboxScript Jukebox;

	// Token: 0x04002889 RID: 10377
	public UILabel EventSubtitle;

	// Token: 0x0400288A RID: 10378
	public YandereScript Yandere;

	// Token: 0x0400288B RID: 10379
	public ClockScript Clock;

	// Token: 0x0400288C RID: 10380
	public StudentScript Friend;

	// Token: 0x0400288D RID: 10381
	public StudentScript Rival;

	// Token: 0x0400288E RID: 10382
	public Transform FriendLocation;

	// Token: 0x0400288F RID: 10383
	public Transform Location;

	// Token: 0x04002890 RID: 10384
	public AudioClip[] SpeechClip;

	// Token: 0x04002891 RID: 10385
	public string[] SpeechText;

	// Token: 0x04002892 RID: 10386
	public string[] EventAnim;

	// Token: 0x04002893 RID: 10387
	public GameObject AlarmDisc;

	// Token: 0x04002894 RID: 10388
	public GameObject VoiceClip;

	// Token: 0x04002895 RID: 10389
	public float FriendWarningTimer;

	// Token: 0x04002896 RID: 10390
	public float Distance;

	// Token: 0x04002897 RID: 10391
	public float Scale;

	// Token: 0x04002898 RID: 10392
	public float Timer;

	// Token: 0x04002899 RID: 10393
	public DayOfWeek EventDay;

	// Token: 0x0400289A RID: 10394
	public int FriendID = 10;

	// Token: 0x0400289B RID: 10395
	public int RivalID = 11;

	// Token: 0x0400289C RID: 10396
	public int Phase;

	// Token: 0x0400289D RID: 10397
	public int Frame;

	// Token: 0x0400289E RID: 10398
	public bool FriendWarned;

	// Token: 0x0400289F RID: 10399
	public bool Sabotaged;

	// Token: 0x040028A0 RID: 10400
	public Vector3 OriginalPosition;

	// Token: 0x040028A1 RID: 10401
	public Vector3 OriginalRotation;
}
