using System;
using UnityEngine;

// Token: 0x020003A0 RID: 928
public class OsanaMorningFriendEventScript : MonoBehaviour
{
	// Token: 0x0400285B RID: 10331
	public RivalMorningEventManagerScript OtherEvent;

	// Token: 0x0400285C RID: 10332
	public StudentManagerScript StudentManager;

	// Token: 0x0400285D RID: 10333
	public EndOfDayScript EndOfDay;

	// Token: 0x0400285E RID: 10334
	public JukeboxScript Jukebox;

	// Token: 0x0400285F RID: 10335
	public UILabel EventSubtitle;

	// Token: 0x04002860 RID: 10336
	public YandereScript Yandere;

	// Token: 0x04002861 RID: 10337
	public ClockScript Clock;

	// Token: 0x04002862 RID: 10338
	public SpyScript Spy;

	// Token: 0x04002863 RID: 10339
	public StudentScript CurrentSpeaker;

	// Token: 0x04002864 RID: 10340
	public StudentScript Friend;

	// Token: 0x04002865 RID: 10341
	public StudentScript Rival;

	// Token: 0x04002866 RID: 10342
	public Transform Epicenter;

	// Token: 0x04002867 RID: 10343
	public Transform[] Location;

	// Token: 0x04002868 RID: 10344
	public AudioClip SpeechClip;

	// Token: 0x04002869 RID: 10345
	public string[] SpeechText;

	// Token: 0x0400286A RID: 10346
	public float[] SpeechTime;

	// Token: 0x0400286B RID: 10347
	public string[] EventAnim;

	// Token: 0x0400286C RID: 10348
	public int[] Speaker;

	// Token: 0x0400286D RID: 10349
	public AudioClip InterruptedClip;

	// Token: 0x0400286E RID: 10350
	public string[] InterruptedSpeech;

	// Token: 0x0400286F RID: 10351
	public float[] InterruptedTime;

	// Token: 0x04002870 RID: 10352
	public string[] InterruptedAnim;

	// Token: 0x04002871 RID: 10353
	public int[] InterruptedSpeaker;

	// Token: 0x04002872 RID: 10354
	public AudioClip AltSpeechClip;

	// Token: 0x04002873 RID: 10355
	public string[] AltSpeechText;

	// Token: 0x04002874 RID: 10356
	public float[] AltSpeechTime;

	// Token: 0x04002875 RID: 10357
	public string[] AltEventAnim;

	// Token: 0x04002876 RID: 10358
	public int[] AltSpeaker;

	// Token: 0x04002877 RID: 10359
	public GameObject AlarmDisc;

	// Token: 0x04002878 RID: 10360
	public GameObject VoiceClip;

	// Token: 0x04002879 RID: 10361
	public Quaternion targetRotation;

	// Token: 0x0400287A RID: 10362
	public float Distance;

	// Token: 0x0400287B RID: 10363
	public float Scale;

	// Token: 0x0400287C RID: 10364
	public float Timer;

	// Token: 0x0400287D RID: 10365
	public DayOfWeek EventDay;

	// Token: 0x0400287E RID: 10366
	public int SpeechPhase = 1;

	// Token: 0x0400287F RID: 10367
	public int FriendID = 6;

	// Token: 0x04002880 RID: 10368
	public int RivalID = 11;

	// Token: 0x04002881 RID: 10369
	public int Phase;

	// Token: 0x04002882 RID: 10370
	public int Frame;

	// Token: 0x04002883 RID: 10371
	public Vector3 OriginalPosition;

	// Token: 0x04002884 RID: 10372
	public Vector3 OriginalRotation;

	// Token: 0x04002885 RID: 10373
	public bool LosingFriend;
}
