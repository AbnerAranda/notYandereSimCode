using System;
using UnityEngine;

// Token: 0x02000396 RID: 918
public class RingEventScript : MonoBehaviour
{
	// Token: 0x060019C7 RID: 6599 RVA: 0x000FD0D5 File Offset: 0x000FB2D5
	private void Start()
	{
		this.HoldingPosition = new Vector3(0.0075f, -0.0355f, 0.0175f);
		this.HoldingRotation = new Vector3(15f, -70f, -135f);
	}

	// Token: 0x060019C8 RID: 6600 RVA: 0x000FD10C File Offset: 0x000FB30C
	private void Update()
	{
		if (!this.Clock.StopTime && !this.EventActive && this.Clock.HourTime > this.EventTime)
		{
			this.EventStudent = this.StudentManager.Students[2];
			if (this.EventStudent != null && !this.EventStudent.Distracted && !this.EventStudent.Talking)
			{
				if (!this.EventStudent.WitnessedMurder && !this.EventStudent.Bullied)
				{
					if (this.EventStudent.Cosmetic.FemaleAccessories[3].activeInHierarchy)
					{
						if (SchemeGlobals.GetSchemeStage(2) < 100)
						{
							this.RingPrompt = this.EventStudent.Cosmetic.FemaleAccessories[3].GetComponent<PromptScript>();
							this.RingCollider = this.EventStudent.Cosmetic.FemaleAccessories[3].GetComponent<BoxCollider>();
							this.OriginalPosition = this.EventStudent.Cosmetic.FemaleAccessories[3].transform.localPosition;
							this.EventStudent.CurrentDestination = this.EventStudent.Destinations[this.EventStudent.Phase];
							this.EventStudent.Pathfinding.target = this.EventStudent.Destinations[this.EventStudent.Phase];
							this.EventStudent.Obstacle.checkTime = 99f;
							this.EventStudent.InEvent = true;
							this.EventStudent.Private = true;
							this.EventStudent.Prompt.Hide();
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
					else
					{
						base.enabled = false;
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
			if (this.EventStudent.DistanceToDestination < 0.5f)
			{
				this.EventStudent.Pathfinding.canSearch = false;
				this.EventStudent.Pathfinding.canMove = false;
			}
			if (this.EventStudent.Alarmed && this.Yandere.TheftTimer > 0f)
			{
				Debug.Log("Event ended because Sakyu saw theft.");
				this.EventStudent.Cosmetic.FemaleAccessories[3].transform.parent = this.EventStudent.LeftMiddleFinger;
				this.EventStudent.Cosmetic.FemaleAccessories[3].transform.localPosition = this.OriginalPosition;
				this.EventStudent.Cosmetic.FemaleAccessories[3].transform.localEulerAngles = new Vector3(0f, 0f, 0f);
				this.RingCollider.gameObject.SetActive(true);
				this.RingCollider.enabled = false;
				this.EventStudent.RingReact = true;
				this.Yandere.Inventory.Ring = false;
				this.EndEvent();
				return;
			}
			if (this.Clock.HourTime > this.EventTime + 0.5f || this.EventStudent.WitnessedMurder || this.EventStudent.Splashed || this.EventStudent.Alarmed || this.EventStudent.Dying || !this.EventStudent.Alive)
			{
				this.EndEvent();
				return;
			}
			if (!this.EventStudent.Pathfinding.canMove)
			{
				if (this.EventPhase == 1)
				{
					this.Timer += Time.deltaTime;
					this.EventStudent.Character.GetComponent<Animation>().CrossFade(this.EventAnim[0]);
					this.EventPhase++;
				}
				else if (this.EventPhase == 2)
				{
					this.Timer += Time.deltaTime;
					if (this.Timer > this.EventStudent.Character.GetComponent<Animation>()[this.EventAnim[0]].length)
					{
						this.EventStudent.Character.GetComponent<Animation>().CrossFade(this.EventStudent.EatAnim);
						this.EventStudent.Bento.transform.localPosition = new Vector3(-0.025f, -0.105f, 0f);
						this.EventStudent.Bento.transform.localEulerAngles = new Vector3(0f, 165f, 82.5f);
						this.EventStudent.Chopsticks[0].SetActive(true);
						this.EventStudent.Chopsticks[1].SetActive(true);
						this.EventStudent.Bento.SetActive(true);
						this.EventStudent.Lid.SetActive(false);
						this.RingCollider.enabled = true;
						this.EventPhase++;
						this.Timer = 0f;
					}
					else if (this.Timer > 4f)
					{
						if (this.EventStudent.Cosmetic.FemaleAccessories[3] != null)
						{
							this.EventStudent.Cosmetic.FemaleAccessories[3].transform.parent = null;
							this.EventStudent.Cosmetic.FemaleAccessories[3].transform.position = new Vector3(-2.707666f, 12.4695f, -31.136f);
							this.EventStudent.Cosmetic.FemaleAccessories[3].transform.eulerAngles = new Vector3(-20f, 180f, 0f);
						}
					}
					else if (this.Timer > 2.5f)
					{
						this.EventStudent.Cosmetic.FemaleAccessories[3].transform.parent = this.EventStudent.RightHand;
						this.EventStudent.Cosmetic.FemaleAccessories[3].transform.localPosition = this.HoldingPosition;
						this.EventStudent.Cosmetic.FemaleAccessories[3].transform.localEulerAngles = this.HoldingRotation;
					}
				}
				else if (this.EventPhase == 3)
				{
					if (this.Clock.HourTime > 13.375f)
					{
						this.EventStudent.Bento.SetActive(false);
						this.EventStudent.Chopsticks[0].SetActive(false);
						this.EventStudent.Chopsticks[1].SetActive(false);
						if (this.RingCollider != null)
						{
							this.RingCollider.enabled = false;
						}
						if (this.RingPrompt != null)
						{
							this.RingPrompt.Hide();
							this.RingPrompt.enabled = false;
						}
						this.EventStudent.Character.GetComponent<Animation>()[this.EventAnim[0]].time = this.EventStudent.Character.GetComponent<Animation>()[this.EventAnim[0]].length;
						this.EventStudent.Character.GetComponent<Animation>()[this.EventAnim[0]].speed = -1f;
						this.EventStudent.Character.GetComponent<Animation>().CrossFade((this.EventStudent.Cosmetic.FemaleAccessories[3] != null) ? this.EventAnim[0] : this.EventAnim[1]);
						this.EventPhase++;
					}
				}
				else if (this.EventPhase == 4)
				{
					this.Timer += Time.deltaTime;
					if (this.EventStudent.Cosmetic.FemaleAccessories[3] != null)
					{
						if (this.Timer > 2f)
						{
							this.EventStudent.Cosmetic.FemaleAccessories[3].transform.parent = this.EventStudent.RightHand;
							this.EventStudent.Cosmetic.FemaleAccessories[3].transform.localPosition = this.HoldingPosition;
							this.EventStudent.Cosmetic.FemaleAccessories[3].transform.localEulerAngles = this.HoldingRotation;
						}
						if (this.Timer > 3f)
						{
							this.EventStudent.Cosmetic.FemaleAccessories[3].transform.parent = this.EventStudent.LeftMiddleFinger;
							this.EventStudent.Cosmetic.FemaleAccessories[3].transform.localPosition = this.OriginalPosition;
							this.RingCollider.enabled = false;
						}
						if (this.Timer > 6f)
						{
							this.EndEvent();
						}
					}
					else if (this.Timer > 1.5f && this.Yandere.transform.position.z < 0f)
					{
						this.EventSubtitle.text = this.EventSpeech[0];
						AudioClipPlayer.Play(this.EventClip[0], this.EventStudent.transform.position + Vector3.up, 5f, 10f, out this.VoiceClip, out this.CurrentClipLength);
						this.EventPhase++;
					}
				}
				else if (this.EventPhase == 5)
				{
					this.Timer += Time.deltaTime;
					if (this.Timer > 9.5f)
					{
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
	}

	// Token: 0x060019C9 RID: 6601 RVA: 0x000FDB90 File Offset: 0x000FBD90
	private void EndEvent()
	{
		Debug.Log("Rooftop ring event has ended.");
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
			}
			this.EventStudent.Pathfinding.speed = 1f;
			this.EventStudent.TargetDistance = 0.5f;
			this.EventStudent.InEvent = false;
			this.EventStudent.Private = false;
			this.EventSubtitle.text = string.Empty;
			this.StudentManager.UpdateStudents(0);
		}
		this.EventActive = false;
		base.enabled = false;
	}

	// Token: 0x060019CA RID: 6602 RVA: 0x000FDCB0 File Offset: 0x000FBEB0
	public void ReturnRing()
	{
		if (this.EventStudent.Cosmetic.FemaleAccessories[3] != null)
		{
			this.EventStudent.Cosmetic.FemaleAccessories[3].transform.parent = this.EventStudent.LeftMiddleFinger;
			this.EventStudent.Cosmetic.FemaleAccessories[3].transform.localPosition = this.OriginalPosition;
			this.RingCollider.enabled = false;
			this.RingPrompt.Hide();
			this.RingPrompt.enabled = false;
		}
	}

	// Token: 0x040027F3 RID: 10227
	public StudentManagerScript StudentManager;

	// Token: 0x040027F4 RID: 10228
	public YandereScript Yandere;

	// Token: 0x040027F5 RID: 10229
	public ClockScript Clock;

	// Token: 0x040027F6 RID: 10230
	public StudentScript EventStudent;

	// Token: 0x040027F7 RID: 10231
	public UILabel EventSubtitle;

	// Token: 0x040027F8 RID: 10232
	public AudioClip[] EventClip;

	// Token: 0x040027F9 RID: 10233
	public string[] EventSpeech;

	// Token: 0x040027FA RID: 10234
	public string[] EventAnim;

	// Token: 0x040027FB RID: 10235
	public GameObject VoiceClip;

	// Token: 0x040027FC RID: 10236
	public bool EventActive;

	// Token: 0x040027FD RID: 10237
	public bool EventOver;

	// Token: 0x040027FE RID: 10238
	public float EventTime = 13.1f;

	// Token: 0x040027FF RID: 10239
	public int EventPhase = 1;

	// Token: 0x04002800 RID: 10240
	public Vector3 OriginalPosition;

	// Token: 0x04002801 RID: 10241
	public Vector3 HoldingPosition;

	// Token: 0x04002802 RID: 10242
	public Vector3 HoldingRotation;

	// Token: 0x04002803 RID: 10243
	public float CurrentClipLength;

	// Token: 0x04002804 RID: 10244
	public float Timer;

	// Token: 0x04002805 RID: 10245
	public PromptScript RingPrompt;

	// Token: 0x04002806 RID: 10246
	public Collider RingCollider;
}
