using System;
using UnityEngine;

// Token: 0x0200034D RID: 845
public class OfferHelpScript : MonoBehaviour
{
	// Token: 0x060018A4 RID: 6308 RVA: 0x000E2419 File Offset: 0x000E0619
	private void Start()
	{
		this.Prompt.enabled = true;
	}

	// Token: 0x060018A5 RID: 6309 RVA: 0x000E2428 File Offset: 0x000E0628
	private void Update()
	{
		if (!this.Unable)
		{
			if (this.Prompt.Circle[0].fillAmount == 0f)
			{
				this.Prompt.Circle[0].fillAmount = 1f;
				if (!this.Yandere.Chased && this.Yandere.Chasers == 0)
				{
					this.Jukebox.Dip = 0.1f;
					this.Yandere.EmptyHands();
					this.Yandere.CanMove = false;
					this.Student = this.StudentManager.Students[this.EventStudentID];
					this.Student.Prompt.Label[0].text = "     Talk";
					this.Student.Pushable = false;
					this.Student.Meeting = false;
					this.Student.Routine = false;
					this.Student.MeetTimer = 0f;
					this.Offering = true;
				}
			}
			if (this.Offering)
			{
				this.Yandere.transform.rotation = Quaternion.Slerp(this.Yandere.transform.rotation, base.transform.rotation, Time.deltaTime * 10f);
				this.Yandere.MoveTowardsTarget(base.transform.position + Vector3.down);
				Quaternion b = Quaternion.LookRotation(this.Yandere.transform.position - this.Student.transform.position);
				this.Student.transform.rotation = Quaternion.Slerp(this.Student.transform.rotation, b, Time.deltaTime * 10f);
				Animation component = this.Yandere.Character.GetComponent<Animation>();
				Animation component2 = this.Student.Character.GetComponent<Animation>();
				if (!this.Spoken)
				{
					if (this.EventSpeaker[this.EventPhase] == 1)
					{
						component.CrossFade(this.EventAnim[this.EventPhase]);
						component2.CrossFade(this.Student.IdleAnim, 1f);
					}
					else
					{
						component2.CrossFade(this.EventAnim[this.EventPhase]);
						component.CrossFade(this.Yandere.IdleAnim, 1f);
					}
					this.EventSubtitle.transform.localScale = new Vector3(1f, 1f, 1f);
					this.EventSubtitle.text = this.EventSpeech[this.EventPhase];
					AudioSource component3 = base.GetComponent<AudioSource>();
					component3.clip = this.EventClip[this.EventPhase];
					component3.Play();
					this.Spoken = true;
					return;
				}
				if (!this.Yandere.PauseScreen.Show && Input.GetButtonDown("A"))
				{
					this.Timer += this.EventClip[this.EventPhase].length + 1f;
				}
				if (this.EventSpeaker[this.EventPhase] == 1)
				{
					if (component[this.EventAnim[this.EventPhase]].time >= component[this.EventAnim[this.EventPhase]].length)
					{
						component.CrossFade(this.Yandere.IdleAnim);
					}
				}
				else if (component2[this.EventAnim[this.EventPhase]].time >= component2[this.EventAnim[this.EventPhase]].length)
				{
					component2.CrossFade(this.Student.IdleAnim);
				}
				this.Timer += Time.deltaTime;
				if (this.Timer > this.EventClip[this.EventPhase].length)
				{
					Debug.Log("Emptying string.");
					this.EventSubtitle.text = string.Empty;
				}
				else
				{
					this.EventSubtitle.text = this.EventSpeech[this.EventPhase];
				}
				if (this.Timer > this.EventClip[this.EventPhase].length + 1f)
				{
					if (this.EventStudentID == 5 && this.EventPhase == 2)
					{
						this.Yandere.PauseScreen.StudentInfoMenu.Targeting = true;
						base.StartCoroutine(this.Yandere.PauseScreen.PhotoGallery.GetPhotos());
						this.Yandere.PauseScreen.PhotoGallery.gameObject.SetActive(true);
						this.Yandere.PauseScreen.PhotoGallery.NamingBully = true;
						this.Yandere.PauseScreen.MainMenu.SetActive(false);
						this.Yandere.PauseScreen.Panel.enabled = true;
						this.Yandere.PauseScreen.Sideways = true;
						this.Yandere.PauseScreen.Show = true;
						Time.timeScale = 0.0001f;
						this.Yandere.PauseScreen.PhotoGallery.UpdateButtonPrompts();
						this.Offering = false;
						return;
					}
					this.Continue();
					return;
				}
			}
			else if (this.StudentManager.Students[this.EventStudentID].Pushed || !this.StudentManager.Students[this.EventStudentID].Alive)
			{
				base.gameObject.SetActive(false);
				return;
			}
		}
		else
		{
			this.Prompt.Circle[0].fillAmount = 1f;
		}
	}

	// Token: 0x060018A6 RID: 6310 RVA: 0x000E298C File Offset: 0x000E0B8C
	public void UpdateLocation()
	{
		Debug.Log("The ''Offer Help'' prompt for Student " + this.EventStudentID + " was told to update its location.");
		this.Student = this.StudentManager.Students[this.EventStudentID];
		if (this.Student.CurrentDestination == this.StudentManager.MeetSpots.List[7])
		{
			base.transform.position = this.Locations[1].position;
			base.transform.eulerAngles = this.Locations[1].eulerAngles;
		}
		else if (this.Student.CurrentDestination == this.StudentManager.MeetSpots.List[8])
		{
			base.transform.position = this.Locations[2].position;
			base.transform.eulerAngles = this.Locations[2].eulerAngles;
		}
		else if (this.Student.CurrentDestination == this.StudentManager.MeetSpots.List[9])
		{
			base.transform.position = this.Locations[3].position;
			base.transform.eulerAngles = this.Locations[3].eulerAngles;
		}
		else if (this.Student.CurrentDestination == this.StudentManager.MeetSpots.List[10])
		{
			base.transform.position = this.Locations[4].position;
			base.transform.eulerAngles = this.Locations[4].eulerAngles;
		}
		if (this.EventStudentID == 11)
		{
			if (!PlayerGlobals.GetStudentFriend(11))
			{
				this.Prompt.Label[0].text = "     Must Befriend Student First";
				this.Unable = true;
			}
			this.Prompt.MyCollider.enabled = true;
			return;
		}
		if (this.EventStudentID == 30)
		{
			if (!PlayerGlobals.GetStudentFriend(30))
			{
				this.Prompt.Label[0].text = "     Must Befriend Student First";
				this.Unable = true;
			}
			this.Prompt.MyCollider.enabled = true;
			return;
		}
		if (this.EventStudentID == 5)
		{
			this.Prompt.MyCollider.enabled = true;
		}
	}

	// Token: 0x060018A7 RID: 6311 RVA: 0x000E2BD0 File Offset: 0x000E0DD0
	public void Continue()
	{
		Debug.Log("Proceeding to next line.");
		this.Offering = true;
		this.Spoken = false;
		this.EventPhase++;
		this.Timer = 0f;
		if (this.EventStudentID == 30 && this.EventPhase == 14)
		{
			if (!ConversationGlobals.GetTopicDiscovered(23))
			{
				this.Yandere.NotificationManager.TopicName = "Family";
				this.Yandere.NotificationManager.DisplayNotification(NotificationType.Topic);
				ConversationGlobals.SetTopicDiscovered(23, true);
			}
			if (!ConversationGlobals.GetTopicLearnedByStudent(23, this.EventStudentID))
			{
				this.Yandere.NotificationManager.TopicName = "Family";
				this.Yandere.NotificationManager.DisplayNotification(NotificationType.Opinion);
				ConversationGlobals.SetTopicLearnedByStudent(23, this.EventStudentID, true);
			}
		}
		if (this.EventPhase == this.EventSpeech.Length)
		{
			if (this.EventStudentID == 11)
			{
				Debug.Log("Scheme #6 has advanced to stage 5.");
				SchemeGlobals.SetSchemeStage(6, 5);
			}
			else if (this.EventStudentID == 30)
			{
				SchemeGlobals.HelpingKokona = true;
			}
			this.Student.CurrentDestination = this.Student.Destinations[this.Student.Phase];
			this.Student.Pathfinding.target = this.Student.Destinations[this.Student.Phase];
			this.Student.Pathfinding.canSearch = true;
			this.Student.Pathfinding.canMove = true;
			this.Student.Routine = true;
			this.Yandere.CanMove = true;
			this.Jukebox.Dip = 1f;
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04002454 RID: 9300
	public StudentManagerScript StudentManager;

	// Token: 0x04002455 RID: 9301
	public JukeboxScript Jukebox;

	// Token: 0x04002456 RID: 9302
	public StudentScript Student;

	// Token: 0x04002457 RID: 9303
	public YandereScript Yandere;

	// Token: 0x04002458 RID: 9304
	public PromptScript Prompt;

	// Token: 0x04002459 RID: 9305
	public UILabel EventSubtitle;

	// Token: 0x0400245A RID: 9306
	public Transform[] Locations;

	// Token: 0x0400245B RID: 9307
	public AudioClip[] EventClip;

	// Token: 0x0400245C RID: 9308
	public string[] EventSpeech;

	// Token: 0x0400245D RID: 9309
	public string[] EventAnim;

	// Token: 0x0400245E RID: 9310
	public int[] EventSpeaker;

	// Token: 0x0400245F RID: 9311
	public bool Offering;

	// Token: 0x04002460 RID: 9312
	public bool Spoken;

	// Token: 0x04002461 RID: 9313
	public bool Unable;

	// Token: 0x04002462 RID: 9314
	public int EventStudentID;

	// Token: 0x04002463 RID: 9315
	public int EventPhase = 1;

	// Token: 0x04002464 RID: 9316
	public float Timer;
}
