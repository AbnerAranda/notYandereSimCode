using System;
using UnityEngine;

// Token: 0x02000290 RID: 656
public class EventManagerScript : MonoBehaviour
{
	// Token: 0x060013D5 RID: 5077 RVA: 0x000AD824 File Offset: 0x000ABA24
	private void Start()
	{
		this.EventSubtitle.transform.localScale = Vector3.zero;
		if (DateGlobals.Weekday == DayOfWeek.Monday)
		{
			this.EventCheck = true;
		}
		if (this.OsanaID == 3)
		{
			if (DateGlobals.Weekday != DayOfWeek.Thursday)
			{
				base.enabled = false;
			}
			else
			{
				this.EventCheck = true;
			}
		}
		this.NoteLocker.Prompt.enabled = true;
		this.NoteLocker.CanLeaveNote = true;
		if (this.EventStudent1 == 11)
		{
			UnityEngine.Object.Destroy(this);
		}
	}

	// Token: 0x060013D6 RID: 5078 RVA: 0x000AD8A4 File Offset: 0x000ABAA4
	private void Update()
	{
		if (!this.Clock.StopTime && this.EventCheck && this.Clock.HourTime > this.StartTime)
		{
			if (this.EventStudent[1] == null)
			{
				this.EventStudent[1] = this.StudentManager.Students[this.EventStudent1];
			}
			else if (!this.EventStudent[1].Alive)
			{
				this.EventCheck = false;
				base.enabled = false;
			}
			if (this.EventStudent[2] == null)
			{
				this.EventStudent[2] = this.StudentManager.Students[this.EventStudent2];
			}
			else if (!this.EventStudent[2].Alive)
			{
				this.EventCheck = false;
				base.enabled = false;
			}
			if (this.EventStudent[1] != null && this.EventStudent[2] != null && !this.EventStudent[1].Slave && !this.EventStudent[2].Slave && this.EventStudent[1].Indoors && !this.EventStudent[1].Wet && !this.EventStudent[1].Meeting && (this.OsanaID < 2 || (this.OsanaID > 1 && Vector3.Distance(this.EventStudent[1].transform.position, this.EventLocation[1].position) < 1f)))
			{
				this.StartTimer += Time.deltaTime;
				if (this.StartTimer > 1f && this.EventStudent[1].Routine && this.EventStudent[2].Routine && !this.EventStudent[1].InEvent && !this.EventStudent[2].InEvent)
				{
					this.EventStudent[1].CurrentDestination = this.EventLocation[1];
					this.EventStudent[1].Pathfinding.target = this.EventLocation[1];
					this.EventStudent[1].EventManager = this;
					this.EventStudent[1].InEvent = true;
					this.EventStudent[1].EmptyHands();
					if (!this.Osana)
					{
						this.EventStudent[2].CurrentDestination = this.EventLocation[2];
						this.EventStudent[2].Pathfinding.target = this.EventLocation[2];
						this.EventStudent[2].EventManager = this;
						this.EventStudent[2].InEvent = true;
					}
					else
					{
						Debug.Log("One of Osana's ''talk privately with Raibaru'' events is beginning.");
					}
					this.EventStudent[2].EmptyHands();
					this.EventStudent[1].SpeechLines.Stop();
					this.EventStudent[2].SpeechLines.Stop();
					this.EventCheck = false;
					this.EventOn = true;
				}
			}
		}
		if (this.EventOn)
		{
			float num = Vector3.Distance(this.Yandere.transform.position, this.EventStudent[this.EventSpeaker[this.EventPhase]].transform.position);
			if (this.Clock.HourTime > this.EndTime || this.EventStudent[1].WitnessedCorpse || this.EventStudent[2].WitnessedCorpse || this.EventStudent[1].Dying || this.EventStudent[2].Dying || this.EventStudent[1].Splashed || this.EventStudent[2].Splashed || this.EventStudent[1].Alarmed || this.EventStudent[2].Alarmed)
			{
				this.EndEvent();
				return;
			}
			if (this.Osana && this.EventStudent[1].DistanceToDestination < 1f)
			{
				this.EventStudent[2].CurrentDestination = this.EventLocation[2];
				this.EventStudent[2].Pathfinding.target = this.EventLocation[2];
				this.EventStudent[2].EventManager = this;
				this.EventStudent[2].InEvent = true;
			}
			if (!this.EventStudent[1].Pathfinding.canMove && !this.EventStudent[1].Private)
			{
				this.EventStudent[1].CharacterAnimation.CrossFade(this.EventStudent[1].IdleAnim);
				this.EventStudent[1].Private = true;
				this.StudentManager.UpdateStudents(0);
			}
			if (Vector3.Distance(this.EventStudent[2].transform.position, this.EventLocation[2].position) < 1f && !this.EventStudent[2].Pathfinding.canMove && !this.StopWalking)
			{
				this.StopWalking = true;
				this.EventStudent[2].CharacterAnimation.CrossFade(this.EventStudent[2].IdleAnim);
				this.EventStudent[2].Private = true;
				this.StudentManager.UpdateStudents(0);
			}
			if (this.StopWalking && this.EventPhase == 1)
			{
				this.EventStudent[2].CharacterAnimation.CrossFade(this.EventStudent[2].IdleAnim);
			}
			if (Vector3.Distance(this.EventStudent[1].transform.position, this.EventLocation[1].position) < 1f && !this.EventStudent[1].Pathfinding.canMove && !this.EventStudent[2].Pathfinding.canMove)
			{
				if (this.EventPhase == 1)
				{
					this.EventStudent[1].CharacterAnimation.CrossFade(this.EventStudent[1].IdleAnim);
				}
				if (this.Osana)
				{
					this.SettleFriend();
				}
				if (!this.Spoken)
				{
					this.EventStudent[this.EventSpeaker[this.EventPhase]].CharacterAnimation.CrossFade(this.EventAnim[this.EventPhase]);
					if (num < 10f)
					{
						this.EventSubtitle.text = this.EventSpeech[this.EventPhase];
					}
					AudioClipPlayer.Play(this.EventClip[this.EventPhase], this.EventStudent[this.EventSpeaker[this.EventPhase]].transform.position + Vector3.up * 1.5f, 5f, 10f, out this.VoiceClip, this.Yandere.transform.position.y);
					this.Spoken = true;
				}
				else
				{
					this.Timer += Time.deltaTime;
					if (this.Timer > this.EventClip[this.EventPhase].length)
					{
						this.EventSubtitle.text = string.Empty;
					}
					if (this.Yandere.transform.position.y < this.EventStudent[1].transform.position.y - 1f)
					{
						this.EventSubtitle.transform.localScale = Vector3.zero;
					}
					else if (num < 10f)
					{
						this.Scale = Mathf.Abs((num - 10f) * 0.2f);
						if (this.Scale < 0f)
						{
							this.Scale = 0f;
						}
						if (this.Scale > 1f)
						{
							this.Scale = 1f;
						}
						this.Jukebox.Dip = 1f - 0.5f * this.Scale;
						this.EventSubtitle.transform.localScale = new Vector3(this.Scale, this.Scale, this.Scale);
					}
					else
					{
						this.EventSubtitle.transform.localScale = Vector3.zero;
					}
					Animation characterAnimation = this.EventStudent[this.EventSpeaker[this.EventPhase]].CharacterAnimation;
					if (characterAnimation[this.EventAnim[this.EventPhase]].time >= characterAnimation[this.EventAnim[this.EventPhase]].length - 1f)
					{
						characterAnimation.CrossFade(this.EventStudent[this.EventSpeaker[this.EventPhase]].IdleAnim, 1f);
					}
					if (this.Timer > this.EventClip[this.EventPhase].length + 1f)
					{
						this.Spoken = false;
						this.EventPhase++;
						this.Timer = 0f;
						if (this.EventPhase == this.EventSpeech.Length)
						{
							this.EndEvent();
						}
					}
					if (!this.Suitor && this.Yandere.transform.position.y > this.EventStudent[1].transform.position.y - 1f && this.EventPhase == 7 && num < 5f)
					{
						if (this.EventStudent1 == 25)
						{
							if (!EventGlobals.Event1)
							{
								this.Yandere.NotificationManager.DisplayNotification(NotificationType.Info);
								EventGlobals.Event1 = true;
							}
						}
						else if (this.OsanaID < 2 && !EventGlobals.OsanaEvent2)
						{
							this.Yandere.NotificationManager.DisplayNotification(NotificationType.Info);
							EventGlobals.OsanaEvent2 = true;
						}
					}
				}
				if (base.enabled)
				{
					if (num < 3f)
					{
						this.Yandere.Eavesdropping = true;
						return;
					}
					this.Yandere.Eavesdropping = false;
				}
			}
		}
	}

	// Token: 0x060013D7 RID: 5079 RVA: 0x000AE210 File Offset: 0x000AC410
	private void SettleFriend()
	{
		this.EventStudent[2].MoveTowardsTarget(this.EventLocation[2].position);
		if (Quaternion.Angle(this.EventStudent[2].transform.rotation, this.EventLocation[2].rotation) > 1f)
		{
			this.EventStudent[2].transform.rotation = Quaternion.Slerp(this.EventStudent[2].transform.rotation, this.EventLocation[2].rotation, 10f * Time.deltaTime);
		}
	}

	// Token: 0x060013D8 RID: 5080 RVA: 0x000AE2A4 File Offset: 0x000AC4A4
	public void EndEvent()
	{
		if (this.VoiceClip != null)
		{
			UnityEngine.Object.Destroy(this.VoiceClip);
		}
		this.EventStudent[1].CurrentDestination = this.EventStudent[1].Destinations[this.EventStudent[1].Phase];
		this.EventStudent[1].Pathfinding.target = this.EventStudent[1].Destinations[this.EventStudent[1].Phase];
		this.EventStudent[1].EventManager = null;
		this.EventStudent[1].InEvent = false;
		this.EventStudent[1].Private = false;
		this.EventStudent[2].CurrentDestination = this.EventStudent[2].Destinations[this.EventStudent[2].Phase];
		this.EventStudent[2].Pathfinding.target = this.EventStudent[2].Destinations[this.EventStudent[2].Phase];
		this.EventStudent[2].EventManager = null;
		this.EventStudent[2].InEvent = false;
		this.EventStudent[2].Private = false;
		if (!this.StudentManager.Stop)
		{
			this.StudentManager.UpdateStudents(0);
		}
		this.Jukebox.Dip = 1f;
		this.Yandere.Eavesdropping = false;
		this.EventSubtitle.text = string.Empty;
		this.EventCheck = false;
		this.EventOn = false;
		base.enabled = false;
	}

	// Token: 0x04001BB5 RID: 7093
	public StudentManagerScript StudentManager;

	// Token: 0x04001BB6 RID: 7094
	public NoteLockerScript NoteLocker;

	// Token: 0x04001BB7 RID: 7095
	public UILabel EventSubtitle;

	// Token: 0x04001BB8 RID: 7096
	public YandereScript Yandere;

	// Token: 0x04001BB9 RID: 7097
	public JukeboxScript Jukebox;

	// Token: 0x04001BBA RID: 7098
	public ClockScript Clock;

	// Token: 0x04001BBB RID: 7099
	public StudentScript[] EventStudent;

	// Token: 0x04001BBC RID: 7100
	public Transform[] EventLocation;

	// Token: 0x04001BBD RID: 7101
	public AudioClip[] EventClip;

	// Token: 0x04001BBE RID: 7102
	public string[] EventSpeech;

	// Token: 0x04001BBF RID: 7103
	public string[] EventAnim;

	// Token: 0x04001BC0 RID: 7104
	public int[] EventSpeaker;

	// Token: 0x04001BC1 RID: 7105
	public GameObject VoiceClip;

	// Token: 0x04001BC2 RID: 7106
	public bool StopWalking;

	// Token: 0x04001BC3 RID: 7107
	public bool EventCheck;

	// Token: 0x04001BC4 RID: 7108
	public bool EventOn;

	// Token: 0x04001BC5 RID: 7109
	public bool Suitor;

	// Token: 0x04001BC6 RID: 7110
	public bool Spoken;

	// Token: 0x04001BC7 RID: 7111
	public bool Osana;

	// Token: 0x04001BC8 RID: 7112
	public float StartTimer;

	// Token: 0x04001BC9 RID: 7113
	public float Timer;

	// Token: 0x04001BCA RID: 7114
	public float Scale;

	// Token: 0x04001BCB RID: 7115
	public float StartTime = 13.01f;

	// Token: 0x04001BCC RID: 7116
	public float EndTime = 13.5f;

	// Token: 0x04001BCD RID: 7117
	public int EventStudent1;

	// Token: 0x04001BCE RID: 7118
	public int EventStudent2;

	// Token: 0x04001BCF RID: 7119
	public int EventPhase;

	// Token: 0x04001BD0 RID: 7120
	public int OsanaID = 1;
}
