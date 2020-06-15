using System;
using UnityEngine;

// Token: 0x020000C3 RID: 195
public class AmbientEventScript : MonoBehaviour
{
	// Token: 0x060009F0 RID: 2544 RVA: 0x0004E7C8 File Offset: 0x0004C9C8
	private void Start()
	{
		if (this.Sitting)
		{
			if (DateGlobals.Weekday != this.EventDay)
			{
				base.enabled = false;
				return;
			}
			if (StudentGlobals.GetStudentGrudge(2) || StudentGlobals.GetStudentGrudge(3))
			{
				this.EventClip = this.GrudgeReaction.EventClip;
				this.EventSpeech = this.GrudgeReaction.EventSpeech;
				this.EventSpeaker = this.GrudgeReaction.EventSpeaker;
				return;
			}
			if (GameGlobals.PoliceYesterday)
			{
				this.EventClip = this.PoliceReaction.EventClip;
				this.EventSpeech = this.PoliceReaction.EventSpeech;
				this.EventSpeaker = this.PoliceReaction.EventSpeaker;
				return;
			}
		}
		else if (DateGlobals.Weekday != this.EventDay)
		{
			base.enabled = false;
		}
	}

	// Token: 0x060009F1 RID: 2545 RVA: 0x0004E888 File Offset: 0x0004CA88
	private void Update()
	{
		if (!this.EventOn)
		{
			for (int i = 1; i < 3; i++)
			{
				if (this.EventStudent[i] == null)
				{
					this.EventStudent[i] = this.StudentManager.Students[this.StudentID[i]];
				}
				else if (!this.EventStudent[i].Alive || this.EventStudent[i].Slave)
				{
					base.enabled = false;
				}
			}
			if (this.Clock.HourTime > this.StartTime && this.EventStudent[1] != null && this.EventStudent[2] != null && this.EventStudent[1].Indoors && this.EventStudent[2].Indoors && this.EventStudent[1].Pathfinding.canMove && this.EventStudent[2].Pathfinding.canMove)
			{
				if (this.Sitting && this.Yandere.Hiding && this.Yandere.HidingSpot == this.HidingSpot.Spot)
				{
					this.Yandere.PromptBar.ClearButtons();
					this.Yandere.PromptBar.Show = false;
					this.Yandere.Exiting = true;
					this.HidingSpot.Prompt.enabled = false;
					this.HidingSpot.Prompt.Hide();
				}
				this.EventStudent[1].CharacterAnimation.CrossFade(this.EventStudent[1].WalkAnim);
				this.EventStudent[1].CurrentDestination = this.EventLocation[1];
				this.EventStudent[1].Pathfinding.target = this.EventLocation[1];
				this.EventStudent[1].InEvent = true;
				this.EventStudent[2].CharacterAnimation.CrossFade(this.EventStudent[2].WalkAnim);
				this.EventStudent[2].CurrentDestination = this.EventLocation[2];
				this.EventStudent[2].Pathfinding.target = this.EventLocation[2];
				this.EventStudent[2].InEvent = true;
				this.EventOn = true;
				return;
			}
		}
		else
		{
			float num = Vector3.Distance(this.Yandere.transform.position, this.EventLocation[1].parent.position);
			if (this.Clock.HourTime > this.StartTime + 0.5f || this.EventStudent[1].WitnessedCorpse || this.EventStudent[2].WitnessedCorpse || this.EventStudent[1].Alarmed || this.EventStudent[2].Alarmed || this.EventStudent[1].Dying || this.EventStudent[2].Dying)
			{
				this.EndEvent();
				return;
			}
			for (int j = 1; j < 3; j++)
			{
				if (!this.EventStudent[j].Pathfinding.canMove && !this.EventStudent[j].Private)
				{
					this.EventStudent[j].Character.GetComponent<Animation>().CrossFade(this.EventStudent[j].IdleAnim);
					this.EventStudent[j].Private = true;
					this.StudentManager.UpdateStudents(0);
				}
			}
			if (!this.EventStudent[1].Pathfinding.canMove && !this.EventStudent[2].Pathfinding.canMove)
			{
				if (this.Sitting)
				{
					this.EventStudent[1].CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
					this.EventStudent[1].CharacterAnimation[this.EventStudent[1].SocialSitAnim].layer = 99;
					this.EventStudent[1].CharacterAnimation.Play(this.EventStudent[1].SocialSitAnim);
					this.EventStudent[1].CharacterAnimation[this.EventStudent[1].SocialSitAnim].weight = 1f;
					this.EventStudent[2].CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
					this.EventStudent[2].CharacterAnimation[this.EventStudent[2].SocialSitAnim].layer = 99;
					this.EventStudent[2].CharacterAnimation.Play(this.EventStudent[2].SocialSitAnim);
					this.EventStudent[2].CharacterAnimation[this.EventStudent[2].SocialSitAnim].weight = 1f;
					this.EventStudent[1].MyController.radius = 0f;
					this.EventStudent[2].MyController.radius = 0f;
					this.RotateSpine = true;
				}
				if (!this.Spoken)
				{
					if (this.Sitting)
					{
						this.EventStudent[this.EventSpeaker[1]].CharacterAnimation.CrossFade("f02_benchSit_00");
						this.EventStudent[this.EventSpeaker[2]].CharacterAnimation.CrossFade("f02_benchSit_00");
					}
					else
					{
						this.EventStudent[this.EventSpeaker[1]].CharacterAnimation.CrossFade(this.EventStudent[1].IdleAnim);
						this.EventStudent[this.EventSpeaker[2]].CharacterAnimation.CrossFade(this.EventStudent[2].IdleAnim);
					}
					this.EventStudent[this.EventSpeaker[this.EventPhase]].PickRandomAnim();
					this.EventStudent[this.EventSpeaker[this.EventPhase]].CharacterAnimation.CrossFade(this.EventStudent[this.EventSpeaker[this.EventPhase]].RandomAnim);
					if (!this.Sitting && this.StartTime < 16f && DateGlobals.Weekday == DayOfWeek.Monday && this.EventPhase == 13)
					{
						this.EventStudent[this.EventSpeaker[this.EventPhase]].CharacterAnimation.CrossFade("jojoPose_00");
					}
					AudioClipPlayer.Play(this.EventClip[this.EventPhase], this.EventStudent[this.EventSpeaker[this.EventPhase]].transform.position + Vector3.up * 1.5f, 5f, 10f, out this.VoiceClip, this.Yandere.transform.position.y);
					this.Spoken = true;
				}
				else
				{
					int num2 = this.EventSpeaker[this.EventPhase];
					if (this.EventStudent[num2].CharacterAnimation[this.EventStudent[num2].RandomAnim].time >= this.EventStudent[num2].CharacterAnimation[this.EventStudent[num2].RandomAnim].length)
					{
						this.EventStudent[num2].PickRandomAnim();
						this.EventStudent[num2].CharacterAnimation.CrossFade(this.EventStudent[num2].RandomAnim);
					}
					this.Timer += Time.deltaTime;
					if (this.Yandere.transform.position.y > this.EventLocation[1].parent.position.y - 1f && this.Yandere.transform.position.y < this.EventLocation[1].parent.position.y + 1f)
					{
						if (this.VoiceClip != null)
						{
							this.VoiceClip.GetComponent<AudioSource>().volume = 1f;
						}
						if (num < 10f)
						{
							if (this.Timer > this.EventClip[this.EventPhase].length)
							{
								this.EventSubtitle.text = string.Empty;
							}
							else
							{
								this.EventSubtitle.text = this.EventSpeech[this.EventPhase];
							}
							this.Scale = Mathf.Abs((num - 10f) * 0.2f);
							if (this.Scale < 0f)
							{
								this.Scale = 0f;
							}
							if (this.Scale > 1f)
							{
								this.Scale = 1f;
							}
							this.EventSubtitle.transform.localScale = new Vector3(this.Scale, this.Scale, this.Scale);
						}
						else
						{
							this.EventSubtitle.transform.localScale = Vector3.zero;
							this.EventSubtitle.text = string.Empty;
						}
					}
					else if (this.VoiceClip != null)
					{
						this.VoiceClip.GetComponent<AudioSource>().volume = 0f;
					}
					if (this.Timer > this.EventClip[this.EventPhase].length + this.Delay)
					{
						this.Spoken = false;
						this.EventPhase++;
						this.Timer = 0f;
						if (this.EventPhase == this.EventSpeech.Length)
						{
							this.EndEvent();
						}
					}
				}
				if (this.Private)
				{
					if (num < 5f)
					{
						this.Yandere.Eavesdropping = true;
						return;
					}
					this.Yandere.Eavesdropping = false;
				}
			}
		}
	}

	// Token: 0x060009F2 RID: 2546 RVA: 0x0004F1AC File Offset: 0x0004D3AC
	private void LateUpdate()
	{
		if (this.RotateSpine)
		{
			this.EventStudent[1].Head.transform.localEulerAngles += new Vector3(0f, 15f, 0f);
			this.EventStudent[1].Neck.transform.localEulerAngles += new Vector3(0f, 15f, 0f);
			this.EventStudent[1].Spine.transform.localEulerAngles += new Vector3(0f, 15f, 0f);
			this.EventStudent[1].LeftEye.transform.localEulerAngles += new Vector3(0f, 5f, 0f);
			this.EventStudent[1].RightEye.transform.localEulerAngles += new Vector3(0f, 5f, 0f);
			this.EventStudent[2].Head.transform.localEulerAngles += new Vector3(0f, -15f, 0f);
			this.EventStudent[2].Neck.transform.localEulerAngles += new Vector3(0f, -15f, 0f);
			this.EventStudent[2].Spine.transform.localEulerAngles += new Vector3(0f, -15f, 0f);
			this.EventStudent[2].LeftEye.transform.localEulerAngles += new Vector3(0f, -5f, 0f);
			this.EventStudent[2].RightEye.transform.localEulerAngles += new Vector3(0f, -5f, 0f);
			this.MouthTimer += Time.deltaTime;
			if (this.MouthTimer > this.TimerLimit)
			{
				this.MouthTarget = UnityEngine.Random.Range(40f, 40f + this.MouthExtent);
				this.MouthTimer = 0f;
			}
			Transform jaw = this.EventStudent[this.EventSpeaker[this.EventPhase]].Jaw;
			Transform lipL = this.EventStudent[this.EventSpeaker[this.EventPhase]].LipL;
			Transform lipR = this.EventStudent[this.EventSpeaker[this.EventPhase]].LipR;
			jaw.localEulerAngles = new Vector3(jaw.localEulerAngles.x, jaw.localEulerAngles.y, Mathf.Lerp(jaw.localEulerAngles.z, this.MouthTarget, Time.deltaTime * this.TalkSpeed));
			lipL.localPosition = new Vector3(lipL.localPosition.x, Mathf.Lerp(lipL.localPosition.y, 0.02632812f + this.MouthTarget * this.LipStrength, Time.deltaTime * this.TalkSpeed), lipL.localPosition.z);
			lipR.localPosition = new Vector3(lipR.localPosition.x, Mathf.Lerp(lipR.localPosition.y, 0.02632812f + this.MouthTarget * this.LipStrength, Time.deltaTime * this.TalkSpeed), lipR.localPosition.z);
		}
	}

	// Token: 0x060009F3 RID: 2547 RVA: 0x0004F558 File Offset: 0x0004D758
	public void EndEvent()
	{
		Debug.Log("An Ambient Event named " + base.gameObject.name + " has ended.");
		if (this.VoiceClip != null)
		{
			UnityEngine.Object.Destroy(this.VoiceClip);
		}
		for (int i = 1; i < 3; i++)
		{
			this.EventStudent[i].CharacterAnimation.cullingType = AnimationCullingType.BasedOnRenderers;
			this.EventStudent[i].CharacterAnimation.Stop(this.EventStudent[i].SocialSitAnim);
			this.EventStudent[1].MyController.radius = 0.1f;
			this.EventStudent[i].CurrentDestination = this.EventStudent[i].Destinations[this.EventStudent[i].Phase];
			this.EventStudent[i].Pathfinding.target = this.EventStudent[i].Destinations[this.EventStudent[i].Phase];
			this.EventStudent[i].InEvent = false;
			this.EventStudent[i].Private = false;
		}
		if (!this.StudentManager.Stop)
		{
			this.StudentManager.UpdateStudents(0);
		}
		if (this.HidingSpot != null)
		{
			this.HidingSpot.Prompt.enabled = true;
		}
		this.EventSubtitle.text = string.Empty;
		this.Yandere.Eavesdropping = false;
		base.enabled = false;
	}

	// Token: 0x0400088D RID: 2189
	public StudentManagerScript StudentManager;

	// Token: 0x0400088E RID: 2190
	public AmbientEventScript GrudgeReaction;

	// Token: 0x0400088F RID: 2191
	public AmbientEventScript PoliceReaction;

	// Token: 0x04000890 RID: 2192
	public HidingSpotScript HidingSpot;

	// Token: 0x04000891 RID: 2193
	public UILabel EventSubtitle;

	// Token: 0x04000892 RID: 2194
	public YandereScript Yandere;

	// Token: 0x04000893 RID: 2195
	public ClockScript Clock;

	// Token: 0x04000894 RID: 2196
	public StudentScript[] EventStudent;

	// Token: 0x04000895 RID: 2197
	public Transform[] EventLocation;

	// Token: 0x04000896 RID: 2198
	public AudioClip[] EventClip;

	// Token: 0x04000897 RID: 2199
	public string[] EventSpeech;

	// Token: 0x04000898 RID: 2200
	public string[] EventAnim;

	// Token: 0x04000899 RID: 2201
	public int[] EventSpeaker;

	// Token: 0x0400089A RID: 2202
	public GameObject VoiceClip;

	// Token: 0x0400089B RID: 2203
	public bool RotateSpine;

	// Token: 0x0400089C RID: 2204
	public bool Sitting;

	// Token: 0x0400089D RID: 2205
	public bool EventOn;

	// Token: 0x0400089E RID: 2206
	public bool Spoken;

	// Token: 0x0400089F RID: 2207
	public bool Private;

	// Token: 0x040008A0 RID: 2208
	public int EventPhase;

	// Token: 0x040008A1 RID: 2209
	public float StartTime = 13.001f;

	// Token: 0x040008A2 RID: 2210
	public float Delay = 0.5f;

	// Token: 0x040008A3 RID: 2211
	public float Timer;

	// Token: 0x040008A4 RID: 2212
	public float Scale;

	// Token: 0x040008A5 RID: 2213
	public int[] StudentID;

	// Token: 0x040008A6 RID: 2214
	public DayOfWeek EventDay;

	// Token: 0x040008A7 RID: 2215
	public float MouthTimer;

	// Token: 0x040008A8 RID: 2216
	public float MouthTarget;

	// Token: 0x040008A9 RID: 2217
	public float MouthExtent;

	// Token: 0x040008AA RID: 2218
	public float TimerLimit = 0.1f;

	// Token: 0x040008AB RID: 2219
	public float TalkSpeed;

	// Token: 0x040008AC RID: 2220
	public float LipStrength;
}
