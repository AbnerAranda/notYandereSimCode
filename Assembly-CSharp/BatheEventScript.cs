using System;
using UnityEngine;

// Token: 0x020000D8 RID: 216
public class BatheEventScript : MonoBehaviour
{
	// Token: 0x06000A43 RID: 2627 RVA: 0x00054A78 File Offset: 0x00052C78
	private void Start()
	{
		this.RivalPhone.SetActive(false);
		if (DateGlobals.Weekday != this.EventDay)
		{
			base.enabled = false;
		}
	}

	// Token: 0x06000A44 RID: 2628 RVA: 0x00054A9C File Offset: 0x00052C9C
	private void Update()
	{
		if (!this.Clock.StopTime && !this.EventActive && this.Clock.HourTime > this.EventTime)
		{
			this.EventStudent = this.StudentManager.Students[30];
			if (this.EventStudent != null && !this.EventStudent.Distracted && !this.EventStudent.Talking && !this.EventStudent.Meeting && this.EventStudent.Indoors)
			{
				if (!this.EventStudent.WitnessedMurder)
				{
					this.OriginalPosition = this.EventStudent.Cosmetic.FemaleAccessories[3].transform.localPosition;
					this.EventStudent.CurrentDestination = this.StudentManager.FemaleStripSpot;
					this.EventStudent.Pathfinding.target = this.StudentManager.FemaleStripSpot;
					this.EventStudent.Character.GetComponent<Animation>().CrossFade(this.EventStudent.WalkAnim);
					this.EventStudent.Pathfinding.canSearch = true;
					this.EventStudent.Pathfinding.canMove = true;
					this.EventStudent.Pathfinding.speed = 1f;
					this.EventStudent.SpeechLines.Stop();
					this.EventStudent.DistanceToDestination = 100f;
					this.EventStudent.SmartPhone.SetActive(false);
					this.EventStudent.Obstacle.checkTime = 99f;
					this.EventStudent.InEvent = true;
					this.EventStudent.Private = true;
					this.EventStudent.Prompt.Hide();
					this.EventStudent.Hearts.Stop();
					this.EventActive = true;
					if (this.EventStudent.Following)
					{
						this.EventStudent.Pathfinding.canMove = true;
						this.EventStudent.Pathfinding.speed = 1f;
						this.EventStudent.Following = false;
						this.EventStudent.Routine = true;
						this.Yandere.Followers--;
						this.EventStudent.Subtitle.UpdateLabel(SubtitleType.StopFollowApology, 0, 3f);
						this.EventStudent.Prompt.Label[0].text = "     Talk";
					}
				}
				else
				{
					base.enabled = false;
				}
			}
		}
		if (this.EventActive)
		{
			if (this.Clock.HourTime > this.EventTime + 1f || this.EventStudent.WitnessedMurder || this.EventStudent.Splashed || this.EventStudent.Alarmed || this.EventStudent.Dying || !this.EventStudent.Alive)
			{
				this.EndEvent();
				return;
			}
			if (this.EventStudent.DistanceToDestination < 0.5f)
			{
				if (this.EventPhase == 1)
				{
					this.EventStudent.Routine = false;
					this.EventStudent.BathePhase = 1;
					this.EventStudent.Wet = true;
					this.EventPhase++;
				}
				else if (this.EventPhase == 2)
				{
					if (this.EventStudent.BathePhase == 4)
					{
						this.RivalPhone.SetActive(true);
						this.EventPhase++;
					}
				}
				else if (this.EventPhase == 3 && !this.EventStudent.Wet)
				{
					this.EndEvent();
				}
			}
			if (this.EventPhase == 4)
			{
				this.Timer += Time.deltaTime;
				if (this.Timer > this.CurrentClipLength + 1f)
				{
					this.EventStudent.Routine = true;
					this.EndEvent();
				}
			}
			float num = Vector3.Distance(this.Yandere.transform.position, this.EventStudent.transform.position);
			if (num < 11f)
			{
				if (num < 10f)
				{
					float num2 = Mathf.Abs((num - 10f) * 0.2f);
					if (num2 < 0f)
					{
						num2 = 0f;
					}
					if (num2 > 1f)
					{
						num2 = 1f;
					}
					this.EventSubtitle.transform.localScale = new Vector3(num2, num2, num2);
					return;
				}
				this.EventSubtitle.transform.localScale = Vector3.zero;
			}
		}
	}

	// Token: 0x06000A45 RID: 2629 RVA: 0x00054F00 File Offset: 0x00053100
	private void EndEvent()
	{
		if (!this.EventOver)
		{
			if (this.VoiceClip != null)
			{
				UnityEngine.Object.Destroy(this.VoiceClip);
			}
			this.EventStudent.CurrentDestination = this.EventStudent.Destinations[this.EventStudent.Phase];
			this.EventStudent.Pathfinding.target = this.EventStudent.Destinations[this.EventStudent.Phase];
			this.EventStudent.Obstacle.checkTime = 1f;
			if (!this.EventStudent.Dying)
			{
				this.EventStudent.Prompt.enabled = true;
				this.EventStudent.Pathfinding.canSearch = true;
				this.EventStudent.Pathfinding.canMove = true;
				this.EventStudent.Pathfinding.speed = 1f;
				this.EventStudent.TargetDistance = 1f;
				this.EventStudent.Private = false;
			}
			this.EventStudent.InEvent = false;
			this.EventSubtitle.text = string.Empty;
			this.StudentManager.UpdateStudents(0);
		}
		this.EventActive = false;
		base.enabled = false;
	}

	// Token: 0x04000A80 RID: 2688
	public StudentManagerScript StudentManager;

	// Token: 0x04000A81 RID: 2689
	public YandereScript Yandere;

	// Token: 0x04000A82 RID: 2690
	public ClockScript Clock;

	// Token: 0x04000A83 RID: 2691
	public StudentScript EventStudent;

	// Token: 0x04000A84 RID: 2692
	public UILabel EventSubtitle;

	// Token: 0x04000A85 RID: 2693
	public AudioClip[] EventClip;

	// Token: 0x04000A86 RID: 2694
	public string[] EventSpeech;

	// Token: 0x04000A87 RID: 2695
	public string[] EventAnim;

	// Token: 0x04000A88 RID: 2696
	public GameObject RivalPhone;

	// Token: 0x04000A89 RID: 2697
	public GameObject VoiceClip;

	// Token: 0x04000A8A RID: 2698
	public bool EventActive;

	// Token: 0x04000A8B RID: 2699
	public bool EventOver;

	// Token: 0x04000A8C RID: 2700
	public float EventTime = 15.1f;

	// Token: 0x04000A8D RID: 2701
	public int EventPhase = 1;

	// Token: 0x04000A8E RID: 2702
	public DayOfWeek EventDay = DayOfWeek.Thursday;

	// Token: 0x04000A8F RID: 2703
	public Vector3 OriginalPosition;

	// Token: 0x04000A90 RID: 2704
	public float CurrentClipLength;

	// Token: 0x04000A91 RID: 2705
	public float Timer;
}
