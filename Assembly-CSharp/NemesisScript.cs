using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000341 RID: 833
public class NemesisScript : MonoBehaviour
{
	// Token: 0x0600186F RID: 6255 RVA: 0x000DC6F4 File Offset: 0x000DA8F4
	private void Start()
	{
		foreach (GameObject gameObject in this.Cosmetic.FemaleHair)
		{
			if (gameObject != null)
			{
				gameObject.SetActive(false);
			}
		}
		foreach (GameObject gameObject2 in this.Cosmetic.TeacherHair)
		{
			if (gameObject2 != null)
			{
				gameObject2.SetActive(false);
			}
		}
		foreach (GameObject gameObject3 in this.Cosmetic.FemaleAccessories)
		{
			if (gameObject3 != null)
			{
				gameObject3.SetActive(false);
			}
		}
		foreach (GameObject gameObject4 in this.Cosmetic.TeacherAccessories)
		{
			if (gameObject4 != null)
			{
				gameObject4.SetActive(false);
			}
		}
		foreach (GameObject gameObject5 in this.Cosmetic.ClubAccessories)
		{
			if (gameObject5 != null)
			{
				gameObject5.SetActive(false);
			}
		}
		foreach (GameObject gameObject6 in this.Cosmetic.Kerchiefs)
		{
			if (gameObject6 != null)
			{
				gameObject6.SetActive(false);
			}
		}
		foreach (GameObject gameObject7 in this.Cosmetic.CatGifts)
		{
			if (gameObject7 != null)
			{
				gameObject7.SetActive(false);
			}
		}
		this.Difficulty = MissionModeGlobals.NemesisDifficulty;
		if (this.Difficulty == 0)
		{
			this.Difficulty = 1;
		}
		this.Student.StudentManager = GameObject.Find("StudentManager").GetComponent<StudentManagerScript>();
		this.Student.WitnessCamera = GameObject.Find("WitnessCamera").GetComponent<WitnessCameraScript>();
		this.Student.Police = GameObject.Find("Police").GetComponent<PoliceScript>();
		this.Student.JSON = GameObject.Find("JSON").GetComponent<JsonScript>();
		this.Student.CharacterAnimation = this.Student.Character.GetComponent<Animation>();
		this.Student.Ragdoll.Nemesis = true;
		this.Student.Yandere = this.Yandere;
		this.Student.IdleAnim = "f02_newIdle_00";
		this.Student.WalkAnim = "f02_newWalk_00";
		this.Student.ShoeRemoval.RightCasualShoe.gameObject.SetActive(false);
		this.Student.ShoeRemoval.LeftCasualShoe.gameObject.SetActive(false);
		if (this.Difficulty < 3)
		{
			this.Student.Character.GetComponent<Animation>()["f02_nemesisEyes_00"].layer = 2;
			this.Student.Character.GetComponent<Animation>().Play("f02_nemesisEyes_00");
			this.Cosmetic.MyRenderer.sharedMesh = this.Cosmetic.FemaleUniforms[5];
			this.Cosmetic.MyRenderer.materials[0].mainTexture = this.NemesisUniform;
			this.Cosmetic.MyRenderer.materials[1].mainTexture = this.NemesisUniform;
			this.Cosmetic.MyRenderer.materials[2].mainTexture = this.NemesisFace;
			this.Cosmetic.RightEyeRenderer.material.mainTexture = this.NemesisEyes;
			this.Cosmetic.LeftEyeRenderer.material.mainTexture = this.NemesisEyes;
			this.Student.FaceCollider.tag = "Nemesis";
			this.NemesisHair.SetActive(true);
		}
		else
		{
			this.NemesisHair.SetActive(false);
			this.PutOnDisguise = true;
		}
		this.Student.LowPoly.enabled = false;
		this.Student.DisableEffects();
		this.HideObjects();
		this.ID = 0;
		while (this.ID < this.Student.Ragdoll.AllRigidbodies.Length)
		{
			this.Student.Ragdoll.AllRigidbodies[this.ID].isKinematic = true;
			this.Student.Ragdoll.AllColliders[this.ID].enabled = false;
			this.ID++;
		}
		this.Student.Ragdoll.AllColliders[10].enabled = true;
		this.Student.Prompt.HideButton[0] = true;
		this.Student.Prompt.HideButton[2] = true;
		UnityEngine.Object.Destroy(this.Student.MyRigidbody);
		base.transform.position = this.MissionMode.SpawnPoints[UnityEngine.Random.Range(0, 4)].position;
		this.MissionMode.LastKnownPosition.position = new Vector3(0f, 0f, -36f);
		this.UpdateLKP();
		base.transform.parent = null;
		this.Student.Name = "Nemesis";
		this.Aggressive = MissionModeGlobals.NemesisAggression;
	}

	// Token: 0x06001870 RID: 6256 RVA: 0x000DCBE0 File Offset: 0x000DADE0
	private void Update()
	{
		if (this.PutOnDisguise)
		{
			bool flag = false;
			int num = 1;
			while ((this.Student.StudentManager.Students[num] != null && this.Student.StudentManager.Students[num].Male) || (num > 5 && num < 21) || num == 21 || num == 26 || num == 31 || num == 36 || num == 41 || num == 46 || num == 51 || num == 56 || num == 61 || num == 66 || num == 71 || num == this.MissionMode.TargetID || flag)
			{
				num = UnityEngine.Random.Range(2, 90);
				if (this.MissionMode.MultiMission)
				{
					flag = false;
					for (int i = 1; i < 11; i++)
					{
						if (num == PlayerPrefs.GetInt("MissionModeTarget" + i))
						{
							flag = true;
						}
					}
				}
			}
			this.Student.StudentManager.Students[num].gameObject.SetActive(false);
			this.Student.StudentManager.Students[num].Replaced = true;
			this.Cosmetic.StudentID = num;
			this.Cosmetic.Start();
			OutlineScript component = this.Cosmetic.FemaleHair[this.Cosmetic.Hairstyle].GetComponent<OutlineScript>();
			if (component != null)
			{
				component.enabled = false;
			}
			else
			{
				component = this.Cosmetic.FemaleHairRenderers[this.Cosmetic.Hairstyle].GetComponent<OutlineScript>();
				if (component != null)
				{
					component.enabled = false;
				}
			}
			this.Student.FaceCollider.tag = "Disguise";
			Debug.Log("Nemesis has disguised herself as " + this.Student.StudentManager.Students[num].Name);
			this.PutOnDisguise = false;
		}
		if (!this.Dying)
		{
			if (!this.Attacking)
			{
				if (this.Yandere.Laughing && Vector3.Distance(base.transform.position, this.Yandere.transform.position) < 10f)
				{
					this.MissionMode.LastKnownPosition.position = this.Yandere.transform.position;
					this.UpdateLKP();
				}
				if (!this.Yandere.CanMove && !this.Yandere.Laughing)
				{
					if (this.Student.Pathfinding.canSearch)
					{
						this.Student.Character.GetComponent<Animation>().CrossFade("f02_idleShort_00");
						this.Student.Pathfinding.canSearch = false;
						this.Student.Pathfinding.canMove = false;
						this.Student.Pathfinding.speed = 0f;
					}
				}
				else
				{
					if (this.Yandere.Stance.Current != StanceType.Crouching && this.Yandere.Stance.Current != StanceType.Crawling && Vector3.Distance(base.transform.position, this.Yandere.transform.position) < 10f && this.Yandere.Running)
					{
						this.MissionMode.LastKnownPosition.position = this.Yandere.transform.position;
						this.UpdateLKP();
					}
					if (!this.Student.Pathfinding.canSearch)
					{
						if (!this.Chasing)
						{
							this.Student.Character.GetComponent<Animation>().CrossFade(this.Student.WalkAnim);
							this.Student.Pathfinding.speed = 1f;
						}
						else
						{
							this.Student.Character.GetComponent<Animation>().CrossFade("f02_sithRun_00");
							this.Student.Pathfinding.speed = 5f;
						}
						this.Student.Pathfinding.canSearch = true;
						this.Student.Pathfinding.canMove = true;
					}
					this.InView = false;
					this.LookForYandere();
					if (!this.Chasing)
					{
						this.Student.Pathfinding.speed = Mathf.MoveTowards(this.Student.Pathfinding.speed, this.InView ? 2f : 1f, Time.deltaTime * 0.1f);
						this.Student.Character.GetComponent<Animation>()[this.Student.WalkAnim].speed = this.Student.Pathfinding.speed;
					}
					else
					{
						this.Student.Pathfinding.speed = 5f;
					}
					if (Vector3.Distance(base.transform.position, this.Yandere.transform.position) < 1f)
					{
						if (this.InView || this.Chasing)
						{
							this.Student.CharacterAnimation.CrossFade("f02_knifeLowSanityA_00");
							this.Yandere.CharacterAnimation.CrossFade("f02_knifeLowSanityB_00");
							AudioSource.PlayClipAtPoint(this.YandereDeath, base.transform.position);
							this.Student.Pathfinding.canSearch = false;
							this.Student.Pathfinding.canMove = false;
							this.Knife.SetActive(true);
							this.Attacking = true;
							this.OriginalYPosition = this.Yandere.transform.position.y;
							this.Yandere.StudentManager.YandereDying = true;
							this.Yandere.StudentManager.StopMoving();
							base.GetComponent<AudioSource>().Play();
							this.Yandere.YandereVision = false;
							this.Yandere.FollowHips = true;
							this.Yandere.Laughing = false;
							this.Yandere.CanMove = false;
							this.Yandere.EyeShrink = 0.5f;
							this.Yandere.StopAiming();
							this.Yandere.EmptyHands();
						}
					}
					else if (Vector3.Distance(base.transform.position, this.MissionMode.LastKnownPosition.position) < 1f)
					{
						this.Student.Character.GetComponent<Animation>().CrossFade("f02_nemesisScan_00");
						this.Student.Pathfinding.speed = 0f;
						this.ScanTimer += Time.deltaTime;
						if (this.ScanTimer > 6f)
						{
							Vector3 vector = new Vector3(0f, 0f, -2.5f);
							this.MissionMode.LastKnownPosition.position = ((this.MissionMode.LastKnownPosition.position == vector) ? this.Yandere.transform.position : vector);
							this.Chasing = false;
							this.UpdateLKP();
						}
					}
				}
				if (this.Difficulty == 1 || this.Difficulty == 3)
				{
					if (Vector3.Distance(base.transform.position, this.Yandere.transform.position) >= 1f)
					{
						this.Student.Prompt.HideButton[2] = true;
						return;
					}
					if (Mathf.Abs(Vector3.Angle(-base.transform.forward, this.Yandere.transform.position - base.transform.position)) > 45f)
					{
						this.Student.Prompt.HideButton[2] = true;
					}
					else if (this.Yandere.Armed)
					{
						this.Student.Prompt.HideButton[2] = false;
					}
					if (!this.Yandere.Armed)
					{
						this.Student.Prompt.HideButton[2] = true;
					}
					if (this.Student.Prompt.Circle[2].fillAmount < 1f)
					{
						this.Yandere.TargetStudent = this.Student;
						this.Yandere.AttackManager.Stealth = true;
						this.Student.AttackReaction();
						this.Student.Pathfinding.canSearch = false;
						this.Student.Pathfinding.canMove = false;
						this.Student.Prompt.HideButton[2] = true;
						this.Dying = true;
						return;
					}
				}
			}
			else
			{
				this.SpecialEffect();
				this.Yandere.targetRotation = Quaternion.LookRotation(base.transform.position - this.Yandere.transform.position);
				this.Yandere.transform.rotation = Quaternion.Slerp(this.Yandere.transform.rotation, this.Yandere.targetRotation, Time.deltaTime * 10f);
				this.Yandere.MoveTowardsTarget(base.transform.position + base.transform.forward * 0.5f);
				this.Yandere.EyeShrink = 0.5f;
				this.Yandere.transform.position = new Vector3(this.Yandere.transform.position.x, this.OriginalYPosition, this.Yandere.transform.position.z);
				Quaternion b = Quaternion.LookRotation(this.Yandere.transform.position - base.transform.position);
				base.transform.rotation = Quaternion.Slerp(base.transform.rotation, b, Time.deltaTime * 10f);
				Animation component2 = this.Student.Character.GetComponent<Animation>();
				if (component2["f02_knifeLowSanityA_00"].time >= component2["f02_knifeLowSanityA_00"].length)
				{
					if (this.MissionMode.enabled)
					{
						this.MissionMode.GameOverID = 13;
						this.MissionMode.GameOver();
						this.MissionMode.Phase = 4;
						base.enabled = false;
						return;
					}
					SceneManager.LoadScene("LoadingScene");
					return;
				}
			}
		}
		else
		{
			if (this.Student.Alive)
			{
				this.Student.MoveTowardsTarget(this.Yandere.transform.position + this.Yandere.transform.forward * this.Yandere.AttackManager.Distance);
				Quaternion b2 = Quaternion.LookRotation(base.transform.position - new Vector3(this.Yandere.transform.position.x, base.transform.position.y, this.Yandere.transform.position.z));
				base.transform.rotation = Quaternion.Slerp(base.transform.rotation, b2, Time.deltaTime * 10f);
				return;
			}
			base.enabled = false;
		}
	}

	// Token: 0x06001871 RID: 6257 RVA: 0x000DD6B0 File Offset: 0x000DB8B0
	private void LookForYandere()
	{
		Debug.Log("Nemesis is looking for Yan-chan...");
		this.Student.VisionDistance = 25f;
		if (this.Student.CanSeeObject(this.Yandere.gameObject, this.Yandere.HeadPosition))
		{
			Debug.Log("Nemesis has spotted Yan-chan.");
			this.MissionMode.LastKnownPosition.position = this.Yandere.transform.position;
			this.InView = true;
			this.UpdateLKP();
			if (this.Aggressive)
			{
				this.Chasing = true;
				return;
			}
		}
		else
		{
			Debug.Log("Nemesis currently cannot see Yandere-chan.");
		}
	}

	// Token: 0x06001872 RID: 6258 RVA: 0x000DD74C File Offset: 0x000DB94C
	private void UpdateLKP()
	{
		if (!this.Chasing)
		{
			this.Student.Character.GetComponent<Animation>().CrossFade(this.Student.WalkAnim);
		}
		else
		{
			this.Student.Character.GetComponent<Animation>().CrossFade("f02_sithRun_00");
		}
		if (this.Student.Pathfinding.speed == 0f)
		{
			if (!this.Chasing)
			{
				this.Student.Pathfinding.speed = 1f;
			}
			else
			{
				this.Student.Pathfinding.speed = 5f;
			}
		}
		this.ScanTimer = 0f;
		this.InView = true;
	}

	// Token: 0x06001873 RID: 6259 RVA: 0x000DD7FC File Offset: 0x000DB9FC
	private void SpecialEffect()
	{
		Animation component = this.Student.Character.GetComponent<Animation>();
		if (this.EffectPhase == 0)
		{
			if (component["f02_knifeLowSanityA_00"].time > 2.76666665f)
			{
				UnityEngine.Object.Instantiate<GameObject>(this.BloodEffect, this.Knife.transform.position + this.Knife.transform.forward * 0.1f, Quaternion.identity);
				this.EffectPhase++;
				return;
			}
		}
		else if (this.EffectPhase == 1)
		{
			if (component["f02_knifeLowSanityA_00"].time > 3.5333333f)
			{
				UnityEngine.Object.Instantiate<GameObject>(this.BloodEffect, this.Knife.transform.position + this.Knife.transform.forward * 0.1f, Quaternion.identity);
				this.EffectPhase++;
				return;
			}
		}
		else if (this.EffectPhase == 2 && component["f02_knifeLowSanityA_00"].time > 4.16666651f)
		{
			UnityEngine.Object.Instantiate<GameObject>(this.BloodEffect, this.Knife.transform.position + this.Knife.transform.forward * 0.1f, Quaternion.identity);
			this.EffectPhase++;
		}
	}

	// Token: 0x06001874 RID: 6260 RVA: 0x000DD96C File Offset: 0x000DBB6C
	private void HideObjects()
	{
		this.Student.Cosmetic.RightStockings[0].SetActive(false);
		this.Student.Cosmetic.LeftStockings[0].SetActive(false);
		this.Student.Cosmetic.RightWristband.SetActive(false);
		this.Student.Cosmetic.LeftWristband.SetActive(false);
		this.Student.FollowCountdown.gameObject.SetActive(false);
		this.Student.DramaticCamera.gameObject.SetActive(false);
		this.Student.VomitEmitter.gameObject.SetActive(false);
		this.Student.Countdown.gameObject.SetActive(false);
		this.Student.ScienceProps[0].SetActive(false);
		this.Student.Chopsticks[0].SetActive(false);
		this.Student.Chopsticks[1].SetActive(false);
		this.Student.Handkerchief.SetActive(false);
		this.Student.ChaseCamera.SetActive(false);
		this.Student.PepperSpray.SetActive(false);
		this.Student.WateringCan.SetActive(false);
		this.Student.OccultBook.SetActive(false);
		this.Student.Cigarette.SetActive(false);
		this.Student.EventBook.SetActive(false);
		this.Student.Handcuffs.SetActive(false);
		this.Student.CandyBar.SetActive(false);
		this.Student.Scrubber.SetActive(false);
		this.Student.Lighter.SetActive(false);
		this.Student.Octodog.SetActive(false);
		this.Student.Eraser.SetActive(false);
		this.Student.Bento.SetActive(false);
		this.Student.Pen.SetActive(false);
		this.Student.SpeechLines.Stop();
		this.Student.InstrumentBag[1].SetActive(false);
		this.Student.InstrumentBag[2].SetActive(false);
		this.Student.InstrumentBag[3].SetActive(false);
		this.Student.InstrumentBag[4].SetActive(false);
		this.Student.InstrumentBag[5].SetActive(false);
		this.Student.Instruments[1].SetActive(false);
		this.Student.Instruments[2].SetActive(false);
		this.Student.Instruments[3].SetActive(false);
		this.Student.Instruments[4].SetActive(false);
		this.Student.Instruments[5].SetActive(false);
		this.Student.Drumsticks[0].SetActive(false);
		this.Student.Drumsticks[1].SetActive(false);
		this.Student.Cosmetic.ThickBrows.SetActive(false);
		foreach (GameObject gameObject in this.Student.Cosmetic.PunkAccessories)
		{
			if (gameObject != null)
			{
				gameObject.SetActive(false);
			}
		}
		foreach (GameObject gameObject2 in this.Student.Fingerfood)
		{
			if (gameObject2 != null)
			{
				gameObject2.SetActive(false);
			}
		}
	}

	// Token: 0x040023A5 RID: 9125
	public MissionModeScript MissionMode;

	// Token: 0x040023A6 RID: 9126
	public CosmeticScript Cosmetic;

	// Token: 0x040023A7 RID: 9127
	public StudentScript Student;

	// Token: 0x040023A8 RID: 9128
	public YandereScript Yandere;

	// Token: 0x040023A9 RID: 9129
	public AudioClip YandereDeath;

	// Token: 0x040023AA RID: 9130
	public Texture NemesisUniform;

	// Token: 0x040023AB RID: 9131
	public Texture NemesisFace;

	// Token: 0x040023AC RID: 9132
	public Texture NemesisEyes;

	// Token: 0x040023AD RID: 9133
	public GameObject BloodEffect;

	// Token: 0x040023AE RID: 9134
	public GameObject NemesisHair;

	// Token: 0x040023AF RID: 9135
	public GameObject Knife;

	// Token: 0x040023B0 RID: 9136
	public bool PutOnDisguise;

	// Token: 0x040023B1 RID: 9137
	public bool Aggressive;

	// Token: 0x040023B2 RID: 9138
	public bool Attacking;

	// Token: 0x040023B3 RID: 9139
	public bool Chasing;

	// Token: 0x040023B4 RID: 9140
	public bool InView;

	// Token: 0x040023B5 RID: 9141
	public bool Dying;

	// Token: 0x040023B6 RID: 9142
	public int EffectPhase;

	// Token: 0x040023B7 RID: 9143
	public int Difficulty;

	// Token: 0x040023B8 RID: 9144
	public int ID;

	// Token: 0x040023B9 RID: 9145
	public float OriginalYPosition;

	// Token: 0x040023BA RID: 9146
	public float ScanTimer = 6f;
}
