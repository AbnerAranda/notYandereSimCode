using System;
using UnityEngine;

// Token: 0x02000409 RID: 1033
public class StudentManagerScript : MonoBehaviour
{
	// Token: 0x06001B40 RID: 6976 RVA: 0x00116304 File Offset: 0x00114504
	private void Start()
	{
		this.LoveSick = GameGlobals.LoveSick;
		this.MetalDetectors = SchoolGlobals.HighSecurity;
		this.RoofFenceUp = SchoolGlobals.RoofFence;
		SchemeGlobals.DeleteAll();
		if (ClubGlobals.GetClubClosed(ClubType.LightMusic))
		{
			this.SpawnPositions[51].position = new Vector3(3f, 0f, -95f);
		}
		if (HomeGlobals.LateForSchool)
		{
			HomeGlobals.LateForSchool = false;
			this.YandereLate = true;
			Debug.Log("Yandere-chan is late for school!");
		}
		if (!this.YandereLate && StudentGlobals.MemorialStudents > 0)
		{
			this.Yandere.HUD.alpha = 0f;
			this.Yandere.HeartCamera.enabled = false;
		}
		if (GameGlobals.Profile == 0)
		{
			GameGlobals.Profile = 1;
			PlayerGlobals.Money = 10f;
		}
		if (!GameGlobals.ReputationsInitialized)
		{
			GameGlobals.ReputationsInitialized = true;
			this.InitializeReputations();
		}
		this.ID = 76;
		while (this.ID < 81)
		{
			if (StudentGlobals.GetStudentReputation(this.ID) > -67)
			{
				StudentGlobals.SetStudentReputation(this.ID, -67);
			}
			this.ID++;
		}
		if (ClubGlobals.GetClubClosed(ClubType.Gardening))
		{
			this.GardenBlockade.SetActive(true);
			this.Flowers.SetActive(false);
		}
		this.ID = 0;
		this.ID = 1;
		while (this.ID < this.JSON.Students.Length)
		{
			if (!this.JSON.Students[this.ID].Success)
			{
				this.ProblemID = this.ID;
				break;
			}
			this.ID++;
		}
		if (this.FridayPaintings.Length != 0)
		{
			this.ID = 1;
			while (this.ID < this.FridayPaintings.Length)
			{
				this.FridayPaintings[this.ID].material.color = new Color(1f, 1f, 1f, 0f);
				this.ID++;
			}
		}
		if (DateGlobals.Weekday != DayOfWeek.Friday)
		{
			if (this.Canvases != null)
			{
				this.Canvases.SetActive(false);
			}
		}
		else if (ClubGlobals.GetClubClosed(ClubType.Art))
		{
			this.Canvases.SetActive(false);
		}
		if (this.ProblemID != -1)
		{
			if (this.ErrorLabel != null)
			{
				this.ErrorLabel.text = string.Empty;
				this.ErrorLabel.enabled = false;
			}
			if (MissionModeGlobals.MissionMode)
			{
				StudentGlobals.FemaleUniform = 5;
				StudentGlobals.MaleUniform = 5;
				this.RedString.gameObject.SetActive(false);
			}
			this.SetAtmosphere();
			GameGlobals.Paranormal = false;
			if (StudentGlobals.GetStudentSlave() > 0 && !StudentGlobals.GetStudentDead(StudentGlobals.GetStudentSlave()))
			{
				int studentSlave = StudentGlobals.GetStudentSlave();
				this.ForceSpawn = true;
				this.SpawnPositions[studentSlave] = this.SlaveSpot;
				this.SpawnID = studentSlave;
				StudentGlobals.SetStudentDead(studentSlave, false);
				this.SpawnStudent(this.SpawnID);
				this.Students[studentSlave].Slave = true;
				this.SpawnID = 0;
			}
			if (StudentGlobals.GetStudentFragileSlave() > 0 && !StudentGlobals.GetStudentDead(StudentGlobals.GetStudentFragileSlave()))
			{
				int studentFragileSlave = StudentGlobals.GetStudentFragileSlave();
				this.ForceSpawn = true;
				this.SpawnPositions[studentFragileSlave] = this.FragileSlaveSpot;
				this.SpawnID = studentFragileSlave;
				StudentGlobals.SetStudentDead(studentFragileSlave, false);
				this.SpawnStudent(this.SpawnID);
				this.Students[studentFragileSlave].FragileSlave = true;
				this.Students[studentFragileSlave].Slave = true;
				this.SpawnID = 0;
			}
			this.NPCsTotal = this.StudentsTotal + this.TeachersTotal;
			this.SpawnID = 1;
			if (StudentGlobals.MaleUniform == 0)
			{
				StudentGlobals.MaleUniform = 1;
			}
			this.ID = 1;
			while (this.ID < this.NPCsTotal + 1)
			{
				if (!StudentGlobals.GetStudentDead(this.ID))
				{
					StudentGlobals.SetStudentDying(this.ID, false);
				}
				this.ID++;
			}
			if (!this.TakingPortraits)
			{
				this.ID = 1;
				while (this.ID < this.Lockers.List.Length)
				{
					this.LockerPositions[this.ID].transform.position = this.Lockers.List[this.ID].position + this.Lockers.List[this.ID].forward * 0.5f;
					this.LockerPositions[this.ID].LookAt(this.Lockers.List[this.ID].position);
					this.ID++;
				}
				this.ID = 1;
				while (this.ID < this.ShowerLockers.List.Length)
				{
					Transform transform = UnityEngine.Object.Instantiate<GameObject>(this.EmptyObject, this.ShowerLockers.List[this.ID].position + this.ShowerLockers.List[this.ID].forward * 0.5f, this.ShowerLockers.List[this.ID].rotation).transform;
					transform.parent = this.ShowerLockers.transform;
					transform.transform.eulerAngles = new Vector3(transform.transform.eulerAngles.x, transform.transform.eulerAngles.y + 180f, transform.transform.eulerAngles.z);
					this.StrippingPositions[this.ID] = transform;
					this.ID++;
				}
				this.ID = 1;
				while (this.ID < this.HidingSpots.List.Length)
				{
					if (this.HidingSpots.List[this.ID] == null)
					{
						GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.EmptyObject, new Vector3(UnityEngine.Random.Range(-17f, 17f), 0f, UnityEngine.Random.Range(-17f, 17f)), Quaternion.identity);
						while (gameObject.transform.position.x < 2.5f && gameObject.transform.position.x > -2.5f && gameObject.transform.position.z > -2.5f && gameObject.transform.position.z < 2.5f)
						{
							gameObject.transform.position = new Vector3(UnityEngine.Random.Range(-17f, 17f), 0f, UnityEngine.Random.Range(-17f, 17f));
						}
						gameObject.transform.parent = this.HidingSpots.transform;
						this.HidingSpots.List[this.ID] = gameObject.transform;
					}
					this.ID++;
				}
			}
			if (this.YandereLate)
			{
				this.Clock.PresentTime = 480f;
				this.Clock.HourTime = 8f;
				this.Clock.UpdateClock();
				this.SkipTo8();
			}
			if (GameGlobals.AlphabetMode)
			{
				Debug.Log("Entering Alphabet Killer Mode. Repositioning Yandere-chan and others.");
				this.Yandere.transform.position = this.Portal.transform.position + new Vector3(1f, 0f, 0f);
				this.Clock.StopTime = true;
				this.SkipTo730();
			}
			if (!this.TakingPortraits)
			{
				while (this.SpawnID < this.NPCsTotal + 1)
				{
					this.SpawnStudent(this.SpawnID);
					this.SpawnID++;
				}
				this.Graffiti[1].SetActive(false);
				this.Graffiti[2].SetActive(false);
				this.Graffiti[3].SetActive(false);
				this.Graffiti[4].SetActive(false);
				this.Graffiti[5].SetActive(false);
			}
		}
		else
		{
			string str = string.Empty;
			if (this.ProblemID > 1)
			{
				str = "The problem may be caused by Student " + this.ProblemID.ToString() + ".";
			}
			if (this.ErrorLabel != null)
			{
				this.ErrorLabel.text = "The game cannot compile Students.JSON! There is a typo somewhere in the JSON file. The problem might be a missing quotation mark, a missing colon, a missing comma, or something else like that. Please find your typo and fix it, or revert to a backup of the JSON file. " + str;
				this.ErrorLabel.enabled = true;
			}
		}
		if (!this.TakingPortraits)
		{
			this.NEStairs = GameObject.Find("NEStairs").GetComponent<Collider>();
			this.NWStairs = GameObject.Find("NWStairs").GetComponent<Collider>();
			this.SEStairs = GameObject.Find("SEStairs").GetComponent<Collider>();
			this.SWStairs = GameObject.Find("SWStairs").GetComponent<Collider>();
		}
	}

	// Token: 0x06001B41 RID: 6977 RVA: 0x00116B9C File Offset: 0x00114D9C
	public void SetAtmosphere()
	{
		if (GameGlobals.LoveSick)
		{
			SchoolGlobals.SchoolAtmosphereSet = true;
			SchoolGlobals.SchoolAtmosphere = 0f;
		}
		if (!MissionModeGlobals.MissionMode)
		{
			if (!SchoolGlobals.SchoolAtmosphereSet)
			{
				SchoolGlobals.SchoolAtmosphereSet = true;
				SchoolGlobals.SchoolAtmosphere = 1f;
			}
			this.Atmosphere = SchoolGlobals.SchoolAtmosphere;
		}
		this.Vignettes = Camera.main.GetComponents<Vignetting>();
		float num = 1f - this.Atmosphere;
		if (!this.TakingPortraits)
		{
			this.SelectiveGreyscale.desaturation = num;
			if (this.HandSelectiveGreyscale != null)
			{
				this.HandSelectiveGreyscale.desaturation = num;
				this.SmartphoneSelectiveGreyscale.desaturation = num;
			}
			this.Vignettes[2].intensity = num * 5f;
			this.Vignettes[2].blur = num;
			this.Vignettes[2].chromaticAberration = num * 5f;
			float num2 = 1f - num;
			RenderSettings.fogColor = new Color(num2, num2, num2, 1f);
			Camera.main.backgroundColor = new Color(num2, num2, num2, 1f);
			RenderSettings.fogDensity = num * 0.1f;
		}
		if (this.Yandere != null)
		{
			this.Yandere.GreyTarget = num;
		}
	}

	// Token: 0x06001B42 RID: 6978 RVA: 0x00116CD0 File Offset: 0x00114ED0
	private void Update()
	{
		if (!this.TakingPortraits)
		{
			if (!this.Yandere.ShoulderCamera.Counselor.Interrogating)
			{
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
			}
			this.Frame++;
			if (!this.FirstUpdate)
			{
				this.QualityManager.UpdateOutlines();
				this.FirstUpdate = true;
				this.AssignTeachers();
			}
			if (this.Frame == 3)
			{
				this.LoveManager.CoupleCheck();
				if (this.Bullies > 0)
				{
					this.DetermineVictim();
				}
				this.UpdateStudents(0);
				if (!OptionGlobals.RimLight)
				{
					this.QualityManager.RimLight();
				}
				this.ID = 26;
				while (this.ID < 31)
				{
					if (this.Students[this.ID] != null)
					{
						this.OriginalClubPositions[this.ID - 25] = this.Clubs.List[this.ID].position;
						this.OriginalClubRotations[this.ID - 25] = this.Clubs.List[this.ID].rotation;
					}
					this.ID++;
				}
				if (!this.TakingPortraits)
				{
					this.TaskManager.UpdateTaskStatus();
				}
				this.Yandere.GloveAttacher.newRenderer.enabled = false;
				this.UpdateAprons();
				if (PlayerPrefs.GetInt("LoadingSave") == 1)
				{
					PlayerPrefs.SetInt("LoadingSave", 0);
					this.Load();
				}
				if (!this.YandereLate && StudentGlobals.MemorialStudents > 0)
				{
					this.Yandere.HUD.alpha = 0f;
					this.Yandere.RPGCamera.transform.position = new Vector3(38f, 4.125f, 68.825f);
					this.Yandere.RPGCamera.transform.eulerAngles = new Vector3(22.5f, 67.5f, 0f);
					this.Yandere.RPGCamera.transform.Translate(Vector3.forward, Space.Self);
					this.Yandere.RPGCamera.enabled = false;
					this.Yandere.HeartCamera.enabled = false;
					this.Yandere.CanMove = false;
					this.Clock.StopTime = true;
					this.StopMoving();
					this.MemorialScene.gameObject.SetActive(true);
					this.MemorialScene.enabled = true;
				}
				this.ID = 1;
				while (this.ID < 90)
				{
					if (this.Students[this.ID] != null)
					{
						this.Students[this.ID].ShoeRemoval.Start();
					}
					this.ID++;
				}
			}
			if ((double)this.Clock.HourTime > 16.9)
			{
				this.CheckMusic();
			}
		}
		else if (this.NPCsSpawned < this.StudentsTotal + this.TeachersTotal)
		{
			this.Frame++;
			if (this.Frame == 1)
			{
				if (this.NewStudent != null)
				{
					UnityEngine.Object.Destroy(this.NewStudent);
				}
				if (this.Randomize)
				{
					int num = UnityEngine.Random.Range(0, 2);
					this.NewStudent = UnityEngine.Object.Instantiate<GameObject>((num == 0) ? this.PortraitChan : this.PortraitKun, Vector3.zero, Quaternion.identity);
				}
				else
				{
					this.NewStudent = UnityEngine.Object.Instantiate<GameObject>((this.JSON.Students[this.NPCsSpawned + 1].Gender == 0) ? this.PortraitChan : this.PortraitKun, Vector3.zero, Quaternion.identity);
				}
				CosmeticScript component = this.NewStudent.GetComponent<CosmeticScript>();
				component.StudentID = this.NPCsSpawned + 1;
				component.StudentManager = this;
				component.TakingPortrait = true;
				component.Randomize = this.Randomize;
				component.JSON = this.JSON;
				if (!this.Randomize)
				{
					this.NPCsSpawned++;
				}
			}
			if (this.Frame == 2)
			{
				ScreenCapture.CaptureScreenshot(Application.streamingAssetsPath + "/Portraits/Student_" + this.NPCsSpawned.ToString() + ".png");
				this.Frame = 0;
			}
		}
		else
		{
			ScreenCapture.CaptureScreenshot(Application.streamingAssetsPath + "/Portraits/Student_" + this.NPCsSpawned.ToString() + ".png");
			base.gameObject.SetActive(false);
		}
		if (this.Witnesses > 0)
		{
			this.ID = 1;
			while (this.ID < this.WitnessList.Length)
			{
				StudentScript studentScript = this.WitnessList[this.ID];
				if (studentScript != null && (!studentScript.Alive || studentScript.Attacked || studentScript.Dying || studentScript.Routine || (studentScript.Fleeing && !studentScript.PinningDown)))
				{
					studentScript.PinDownWitness = false;
					if (this.ID != this.WitnessList.Length - 1)
					{
						this.Shuffle(this.ID);
					}
					this.Witnesses--;
				}
				this.ID++;
			}
			if (this.PinningDown && this.Witnesses < 4)
			{
				Debug.Log("Students were going to pin Yandere-chan down, but now there are less than 4 witnesses, so it's not going to happen.");
				if (!this.Yandere.Chased && this.Yandere.Chasers == 0)
				{
					this.Yandere.CanMove = true;
				}
				this.PinningDown = false;
				this.PinDownTimer = 0f;
				this.PinPhase = 0;
			}
		}
		if (this.PinningDown)
		{
			if (!this.Yandere.Attacking && this.Yandere.CanMove)
			{
				this.Yandere.CharacterAnimation.CrossFade("f02_pinDownPanic_00");
				this.Yandere.EmptyHands();
				this.Yandere.CanMove = false;
			}
			if (this.PinPhase == 1)
			{
				if (!this.Yandere.Attacking && !this.Yandere.Struggling)
				{
					this.PinTimer += Time.deltaTime;
				}
				if (this.PinTimer > 1f)
				{
					this.ID = 1;
					while (this.ID < 5)
					{
						StudentScript studentScript2 = this.WitnessList[this.ID];
						if (studentScript2 != null)
						{
							studentScript2.transform.position = new Vector3(studentScript2.transform.position.x, studentScript2.transform.position.y + 0.1f, studentScript2.transform.position.z);
							studentScript2.CurrentDestination = this.PinDownSpots[this.ID];
							studentScript2.Pathfinding.target = this.PinDownSpots[this.ID];
							studentScript2.SprintAnim = studentScript2.OriginalSprintAnim;
							studentScript2.DistanceToDestination = 100f;
							studentScript2.Pathfinding.speed = 5f;
							studentScript2.MyController.radius = 0f;
							studentScript2.PinningDown = true;
							studentScript2.Alarmed = false;
							studentScript2.Routine = false;
							studentScript2.Fleeing = true;
							studentScript2.AlarmTimer = 0f;
							studentScript2.SmartPhone.SetActive(false);
							studentScript2.Safe = true;
							studentScript2.Prompt.Hide();
							studentScript2.Prompt.enabled = false;
							Debug.Log(studentScript2 + "'s current destination is " + studentScript2.CurrentDestination);
						}
						this.ID++;
					}
					this.PinPhase++;
				}
			}
			else if (this.WitnessList[1].PinPhase == 0)
			{
				if (!this.Yandere.ShoulderCamera.Noticed && !this.Yandere.ShoulderCamera.HeartbrokenCamera.activeInHierarchy)
				{
					this.PinDownTimer += Time.deltaTime;
					if (this.PinDownTimer > 10f || (this.WitnessList[1].DistanceToDestination < 1f && this.WitnessList[2].DistanceToDestination < 1f && this.WitnessList[3].DistanceToDestination < 1f && this.WitnessList[4].DistanceToDestination < 1f))
					{
						this.Clock.StopTime = true;
						this.Yandere.HUD.enabled = false;
						if (this.Yandere.Aiming)
						{
							this.Yandere.StopAiming();
							this.Yandere.enabled = false;
						}
						this.Yandere.Mopping = false;
						this.Yandere.EmptyHands();
						AudioSource component2 = base.GetComponent<AudioSource>();
						component2.PlayOneShot(this.PinDownSFX);
						component2.PlayOneShot(this.YanderePinDown);
						this.Yandere.CharacterAnimation.CrossFade("f02_pinDown_00");
						this.Yandere.CanMove = false;
						this.Yandere.ShoulderCamera.LookDown = true;
						this.Yandere.RPGCamera.enabled = false;
						this.StopMoving();
						this.Yandere.ShoulderCamera.HeartbrokenCamera.GetComponent<Camera>().cullingMask |= 512;
						this.ID = 1;
						while (this.ID < 5)
						{
							StudentScript studentScript3 = this.WitnessList[this.ID];
							if (studentScript3.MyWeapon != null)
							{
								GameObjectUtils.SetLayerRecursively(studentScript3.MyWeapon.gameObject, 13);
							}
							studentScript3.CharacterAnimation.CrossFade(((studentScript3.Male ? "pinDown_0" : "f02_pinDown_0") + this.ID).ToString());
							studentScript3.PinPhase++;
							this.ID++;
						}
					}
				}
			}
			else
			{
				bool flag = false;
				if (!this.WitnessList[1].Male)
				{
					if (this.WitnessList[1].CharacterAnimation["f02_pinDown_01"].time >= this.WitnessList[1].CharacterAnimation["f02_pinDown_01"].length)
					{
						flag = true;
					}
				}
				else if (this.WitnessList[1].CharacterAnimation["pinDown_01"].time >= this.WitnessList[1].CharacterAnimation["pinDown_01"].length)
				{
					flag = true;
				}
				if (flag)
				{
					this.Yandere.CharacterAnimation.CrossFade("f02_pinDownLoop_00");
					this.ID = 1;
					while (this.ID < 5)
					{
						StudentScript studentScript4 = this.WitnessList[this.ID];
						studentScript4.CharacterAnimation.CrossFade(((studentScript4.Male ? "pinDownLoop_0" : "f02_pinDownLoop_0") + this.ID).ToString());
						this.ID++;
					}
					this.PinningDown = false;
				}
			}
		}
		if (this.Meeting)
		{
			this.UpdateMeeting();
		}
		if (Input.GetKeyDown("space"))
		{
			this.DetermineVictim();
		}
		if (this.Police != null && (this.Police.BloodParent.childCount > 0 || this.Police.LimbParent.childCount > 0 || this.Yandere.WeaponManager.MisplacedWeapons > 0))
		{
			this.CurrentID++;
			if (this.CurrentID > 97)
			{
				this.UpdateBlood();
				this.CurrentID = 1;
			}
			if (this.Students[this.CurrentID] == null)
			{
				this.CurrentID++;
			}
			else if (!this.Students[this.CurrentID].gameObject.activeInHierarchy)
			{
				this.CurrentID++;
			}
		}
		if (this.OpenCurtain)
		{
			this.OpenValue = Mathf.Lerp(this.OpenValue, 100f, Time.deltaTime * 10f);
			if (this.OpenValue > 99f)
			{
				this.OpenCurtain = false;
			}
			this.FemaleShowerCurtain.SetBlendShapeWeight(0, this.OpenValue);
		}
		if (this.AoT)
		{
			this.ID = 1;
			while (this.ID < this.Students.Length)
			{
				StudentScript studentScript5 = this.Students[this.ID];
				if (studentScript5 != null && studentScript5.transform.localScale.x < 9.99f)
				{
					studentScript5.transform.localScale = Vector3.Lerp(studentScript5.transform.localScale, new Vector3(10f, 10f, 10f), Time.deltaTime);
				}
				this.ID++;
			}
		}
		if (this.Pose)
		{
			this.ID = 1;
			while (this.ID < this.Students.Length)
			{
				StudentScript studentScript6 = this.Students[this.ID];
				if (studentScript6 != null && studentScript6.Prompt.Label[0] != null)
				{
					studentScript6.Prompt.Label[0].text = "     Pose";
				}
				this.ID++;
			}
		}
		if (this.Yandere.Egg)
		{
			if (this.Sans)
			{
				this.ID = 1;
				while (this.ID < this.Students.Length)
				{
					StudentScript studentScript7 = this.Students[this.ID];
					if (studentScript7 != null && studentScript7.Prompt.Label[0] != null)
					{
						studentScript7.Prompt.Label[0].text = "     Psychokinesis";
					}
					this.ID++;
				}
			}
			if (this.Ebola)
			{
				this.ID = 2;
				while (this.ID < this.Students.Length)
				{
					StudentScript studentScript8 = this.Students[this.ID];
					if (studentScript8 != null && studentScript8.isActiveAndEnabled && studentScript8.DistanceToPlayer < 1f)
					{
						UnityEngine.Object.Instantiate<GameObject>(this.Yandere.EbolaEffect, studentScript8.transform.position + Vector3.up, Quaternion.identity);
						studentScript8.SpawnAlarmDisc();
						studentScript8.BecomeRagdoll();
						studentScript8.DeathType = DeathType.EasterEgg;
					}
					this.ID++;
				}
			}
			if (this.Yandere.Hunger >= 5)
			{
				this.ID = 2;
				while (this.ID < this.Students.Length)
				{
					StudentScript studentScript9 = this.Students[this.ID];
					if (studentScript9 != null && studentScript9.isActiveAndEnabled && studentScript9.DistanceToPlayer < 5f)
					{
						UnityEngine.Object.Instantiate<GameObject>(this.Yandere.DarkHelix, studentScript9.transform.position + Vector3.up, Quaternion.identity);
						studentScript9.SpawnAlarmDisc();
						studentScript9.BecomeRagdoll();
						studentScript9.DeathType = DeathType.EasterEgg;
					}
					this.ID++;
				}
			}
		}
		if (this.Yandere.transform.position.z < -50f)
		{
			this.PlazaOccluder.open = false;
		}
		else
		{
			this.PlazaOccluder.open = true;
		}
		this.YandereVisible = false;
	}

	// Token: 0x06001B43 RID: 6979 RVA: 0x00117BE8 File Offset: 0x00115DE8
	public void SpawnStudent(int spawnID)
	{
		bool flag = false;
		if (this.JSON.Students[spawnID].Club != ClubType.Delinquent && StudentGlobals.GetStudentReputation(spawnID) < -100)
		{
			flag = true;
		}
		if (spawnID > 9 && spawnID < 21)
		{
			flag = true;
		}
		if (!flag && this.Students[spawnID] == null && !StudentGlobals.GetStudentDead(spawnID) && !StudentGlobals.GetStudentKidnapped(spawnID) && !StudentGlobals.GetStudentArrested(spawnID) && !StudentGlobals.GetStudentExpelled(spawnID))
		{
			int num;
			if (this.JSON.Students[spawnID].Name == "Random")
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.EmptyObject, new Vector3(UnityEngine.Random.Range(-17f, 17f), 0f, UnityEngine.Random.Range(-17f, 17f)), Quaternion.identity);
				while (gameObject.transform.position.x < 2.5f && gameObject.transform.position.x > -2.5f && gameObject.transform.position.z > -2.5f && gameObject.transform.position.z < 2.5f)
				{
					gameObject.transform.position = new Vector3(UnityEngine.Random.Range(-17f, 17f), 0f, UnityEngine.Random.Range(-17f, 17f));
				}
				gameObject.transform.parent = this.HidingSpots.transform;
				this.HidingSpots.List[spawnID] = gameObject.transform;
				GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.RandomPatrol, Vector3.zero, Quaternion.identity);
				gameObject2.transform.parent = this.Patrols.transform;
				this.Patrols.List[spawnID] = gameObject2.transform;
				GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(this.RandomPatrol, Vector3.zero, Quaternion.identity);
				gameObject3.transform.parent = this.CleaningSpots.transform;
				this.CleaningSpots.List[spawnID] = gameObject3.transform;
				num = ((MissionModeGlobals.MissionMode && MissionModeGlobals.MissionTarget == spawnID) ? 0 : UnityEngine.Random.Range(0, 2));
				this.FindUnoccupiedSeat();
			}
			else
			{
				num = this.JSON.Students[spawnID].Gender;
			}
			this.NewStudent = UnityEngine.Object.Instantiate<GameObject>((num == 0) ? this.StudentChan : this.StudentKun, this.SpawnPositions[spawnID].position, Quaternion.identity);
			CosmeticScript component = this.NewStudent.GetComponent<CosmeticScript>();
			component.LoveManager = this.LoveManager;
			component.StudentManager = this;
			component.Randomize = this.Randomize;
			component.StudentID = spawnID;
			component.JSON = this.JSON;
			if (this.JSON.Students[spawnID].Name == "Random")
			{
				this.NewStudent.GetComponent<StudentScript>().CleaningSpot = this.CleaningSpots.List[spawnID];
				this.NewStudent.GetComponent<StudentScript>().CleaningRole = 3;
			}
			if (this.JSON.Students[spawnID].Club == ClubType.Bully)
			{
				this.Bullies++;
			}
			this.Students[spawnID] = this.NewStudent.GetComponent<StudentScript>();
			StudentScript studentScript = this.Students[spawnID];
			studentScript.ChaseSelectiveGrayscale.desaturation = 1f - SchoolGlobals.SchoolAtmosphere;
			studentScript.Cosmetic.TextureManager = this.TextureManager;
			studentScript.WitnessCamera = this.WitnessCamera;
			studentScript.StudentManager = this;
			studentScript.StudentID = spawnID;
			studentScript.JSON = this.JSON;
			studentScript.BloodSpawnerIdentifier.ObjectID = "Student_" + spawnID + "_BloodSpawner";
			studentScript.HipsIdentifier.ObjectID = "Student_" + spawnID + "_Hips";
			studentScript.YanSave.ObjectID = "Student_" + spawnID;
			if (studentScript.Miyuki != null)
			{
				studentScript.Miyuki.Enemy = this.MiyukiCat;
			}
			if (this.AoT)
			{
				studentScript.AoT = true;
			}
			if (this.DK)
			{
				studentScript.DK = true;
			}
			if (this.Spooky)
			{
				studentScript.Spooky = true;
			}
			if (this.Sans)
			{
				studentScript.BadTime = true;
			}
			if (spawnID == this.RivalID)
			{
				studentScript.Rival = true;
				this.RedString.transform.parent = studentScript.LeftPinky;
				this.RedString.transform.localPosition = new Vector3(0f, 0f, 0f);
			}
			if (spawnID == 1)
			{
				this.RedString.Target = studentScript.LeftPinky;
			}
			if (this.JSON.Students[spawnID].Persona == PersonaType.Protective || this.JSON.Students[spawnID].Hairstyle == "20" || this.JSON.Students[spawnID].Hairstyle == "21")
			{
				UnityEngine.Object.Destroy(studentScript);
			}
			if (num == 0)
			{
				this.GirlsSpawned++;
				studentScript.GirlID = this.GirlsSpawned;
			}
			this.OccupySeat();
		}
		this.NPCsSpawned++;
		this.ForceSpawn = false;
		if (this.Students[10] != null || this.Students[11] != null)
		{
			UnityEngine.Object.Destroy(this.Students[10].gameObject);
			UnityEngine.Object.Destroy(this.Students[11].gameObject);
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06001B44 RID: 6980 RVA: 0x00118178 File Offset: 0x00116378
	public void UpdateStudents(int SpecificStudent = 0)
	{
		this.ID = 2;
		while (this.ID < this.Students.Length)
		{
			bool flag = false;
			if (SpecificStudent != 0)
			{
				this.ID = SpecificStudent;
				flag = true;
			}
			StudentScript studentScript = this.Students[this.ID];
			if (studentScript != null)
			{
				if (studentScript.gameObject.activeInHierarchy || studentScript.Hurry)
				{
					if (!studentScript.Safe)
					{
						if (!studentScript.Slave)
						{
							if (studentScript.Pushable)
							{
								studentScript.Prompt.Label[0].text = "     Push";
							}
							else if (this.Yandere.SpiderGrow)
							{
								if (!studentScript.Cosmetic.Empty)
								{
									studentScript.Prompt.Label[0].text = "     Send Husk";
								}
								else
								{
									studentScript.Prompt.Label[0].text = "     Talk";
								}
							}
							else if (!studentScript.Following)
							{
								studentScript.Prompt.Label[0].text = "     Talk";
							}
							else
							{
								studentScript.Prompt.Label[0].text = "     Stop";
							}
							studentScript.Prompt.HideButton[0] = false;
							studentScript.Prompt.HideButton[2] = false;
							studentScript.Prompt.Attack = false;
							if (this.Yandere.Mask != null || studentScript.Ragdoll.Zs.activeInHierarchy)
							{
								studentScript.Prompt.HideButton[0] = true;
							}
							if (this.Yandere.Dragging || this.Yandere.PickUp != null || this.Yandere.Chased)
							{
								studentScript.Prompt.HideButton[0] = true;
								studentScript.Prompt.HideButton[2] = true;
								if (this.Yandere.PickUp != null && !studentScript.Following)
								{
									if (this.Yandere.PickUp.Food > 0)
									{
										studentScript.Prompt.Label[0].text = "     Feed";
										studentScript.Prompt.HideButton[0] = false;
										studentScript.Prompt.HideButton[2] = true;
									}
									else if (this.Yandere.PickUp.Salty)
									{
										studentScript.Prompt.Label[0].text = "     Give Snack";
										studentScript.Prompt.HideButton[0] = false;
										studentScript.Prompt.HideButton[2] = true;
									}
									else if (this.Yandere.PickUp.StuckBoxCutter != null)
									{
										studentScript.Prompt.Label[0].text = "     Ask For Help";
										studentScript.Prompt.HideButton[0] = false;
										studentScript.Prompt.HideButton[2] = true;
									}
									else if (this.Yandere.PickUp.PuzzleCube)
									{
										studentScript.Prompt.Label[0].text = "     Give Puzzle";
										studentScript.Prompt.HideButton[0] = false;
										studentScript.Prompt.HideButton[2] = true;
									}
								}
							}
							if (this.Yandere.Armed)
							{
								studentScript.Prompt.HideButton[0] = true;
								studentScript.Prompt.Attack = true;
								studentScript.Prompt.MinimumDistanceSqr = 1f;
								studentScript.Prompt.MinimumDistance = 1f;
							}
							else
							{
								studentScript.Prompt.HideButton[2] = true;
								studentScript.Prompt.MinimumDistanceSqr = 2f;
								studentScript.Prompt.MinimumDistance = 2f;
								if (studentScript.WitnessedMurder || studentScript.WitnessedCorpse || studentScript.Private)
								{
									studentScript.Prompt.HideButton[0] = true;
								}
							}
							if (this.Yandere.NearBodies > 0 || this.Yandere.Sanity < 33.33333f)
							{
								studentScript.Prompt.HideButton[0] = true;
							}
							if (studentScript.Teacher)
							{
								studentScript.Prompt.HideButton[0] = true;
							}
						}
						else if (!studentScript.FragileSlave)
						{
							if (this.Yandere.Armed)
							{
								if (this.Yandere.EquippedWeapon.Concealable)
								{
									studentScript.Prompt.HideButton[0] = false;
									studentScript.Prompt.Label[0].text = "     Give Weapon";
								}
								else
								{
									studentScript.Prompt.HideButton[0] = true;
									studentScript.Prompt.Label[0].text = string.Empty;
								}
							}
							else
							{
								studentScript.Prompt.HideButton[0] = true;
								studentScript.Prompt.Label[0].text = string.Empty;
							}
						}
					}
					if (studentScript.FightingSlave && this.Yandere.Armed)
					{
						Debug.Log("Fighting with a slave!");
						studentScript.Prompt.Label[0].text = "     Stab";
						studentScript.Prompt.HideButton[0] = false;
						studentScript.Prompt.HideButton[2] = true;
						studentScript.Prompt.enabled = true;
					}
					if (this.NoSpeech && !studentScript.Armband.activeInHierarchy)
					{
						studentScript.Prompt.HideButton[0] = true;
					}
				}
				if (studentScript.Prompt.Label[0] != null)
				{
					if (this.Sans)
					{
						studentScript.Prompt.HideButton[0] = false;
						studentScript.Prompt.Label[0].text = "     Psychokinesis";
					}
					if (this.Pose)
					{
						studentScript.Prompt.HideButton[0] = false;
						studentScript.Prompt.Label[0].text = "     Pose";
						studentScript.Prompt.BloodMask = 1;
						studentScript.Prompt.BloodMask |= 2;
						studentScript.Prompt.BloodMask |= 512;
						studentScript.Prompt.BloodMask |= 8192;
						studentScript.Prompt.BloodMask |= 16384;
						studentScript.Prompt.BloodMask |= 65536;
						studentScript.Prompt.BloodMask |= 2097152;
						studentScript.Prompt.BloodMask = ~studentScript.Prompt.BloodMask;
					}
					if (!studentScript.Teacher && this.Six)
					{
						studentScript.Prompt.MinimumDistance = 0.75f;
						studentScript.Prompt.HideButton[0] = false;
						studentScript.Prompt.Label[0].text = "     Eat";
					}
					if (this.Gaze)
					{
						studentScript.Prompt.MinimumDistance = 5f;
						studentScript.Prompt.HideButton[0] = false;
						studentScript.Prompt.Label[0].text = "     Gaze";
					}
				}
				if (GameGlobals.EmptyDemon)
				{
					studentScript.Prompt.HideButton[0] = false;
				}
			}
			this.ID++;
			if (flag)
			{
				this.ID = this.Students.Length;
			}
		}
		this.Container.UpdatePrompts();
		this.TrashCan.UpdatePrompt();
	}

	// Token: 0x06001B45 RID: 6981 RVA: 0x00118884 File Offset: 0x00116A84
	public void UpdateMe(int ID)
	{
		if (ID > 1)
		{
			StudentScript studentScript = this.Students[ID];
			if (!studentScript.Safe)
			{
				studentScript.Prompt.Label[0].text = "     Talk";
				studentScript.Prompt.HideButton[0] = false;
				studentScript.Prompt.HideButton[2] = false;
				studentScript.Prompt.Attack = false;
				if (studentScript.FightingSlave)
				{
					if (this.Yandere.Armed)
					{
						Debug.Log("Fighting with a slave!");
						studentScript.Prompt.Label[0].text = "     Stab";
						studentScript.Prompt.HideButton[0] = false;
						studentScript.Prompt.HideButton[2] = true;
						studentScript.Prompt.enabled = true;
					}
				}
				else
				{
					if (this.Yandere.Armed && this.OriginalUniforms + this.NewUniforms > 0)
					{
						studentScript.Prompt.HideButton[0] = true;
						studentScript.Prompt.MinimumDistance = 1f;
						studentScript.Prompt.Attack = true;
					}
					else
					{
						studentScript.Prompt.HideButton[2] = true;
						studentScript.Prompt.MinimumDistance = 2f;
						if (studentScript.WitnessedMurder || studentScript.WitnessedCorpse || studentScript.Private)
						{
							studentScript.Prompt.HideButton[0] = true;
						}
					}
					if (this.Yandere.Dragging || this.Yandere.PickUp != null || this.Yandere.Chased || this.Yandere.Chasers > 0)
					{
						studentScript.Prompt.HideButton[0] = true;
						studentScript.Prompt.HideButton[2] = true;
					}
					if (this.Yandere.NearBodies > 0 || this.Yandere.Sanity < 33.33333f)
					{
						studentScript.Prompt.HideButton[0] = true;
					}
					if (studentScript.Teacher)
					{
						studentScript.Prompt.HideButton[0] = true;
					}
				}
			}
			if (this.Sans)
			{
				studentScript.Prompt.HideButton[0] = false;
				studentScript.Prompt.Label[0].text = "     Psychokinesis";
			}
			if (this.Pose)
			{
				studentScript.Prompt.HideButton[0] = false;
				studentScript.Prompt.Label[0].text = "     Pose";
			}
			if (this.NoSpeech || studentScript.Ragdoll.Zs.activeInHierarchy)
			{
				studentScript.Prompt.HideButton[0] = true;
			}
		}
	}

	// Token: 0x06001B46 RID: 6982 RVA: 0x00118AFC File Offset: 0x00116CFC
	public void AttendClass()
	{
		this.ConvoManager.Confirmed = false;
		this.SleuthPhase = 3;
		if (this.RingEvent.EventActive)
		{
			this.RingEvent.ReturnRing();
		}
		while (this.NPCsSpawned < this.NPCsTotal)
		{
			this.SpawnStudent(this.SpawnID);
			this.SpawnID++;
		}
		if (this.Clock.LateStudent)
		{
			this.Clock.ActivateLateStudent();
		}
		this.ID = 1;
		while (this.ID < this.Students.Length)
		{
			StudentScript studentScript = this.Students[this.ID];
			if (studentScript != null)
			{
				if (studentScript.WitnessedBloodPool && !studentScript.WitnessedMurder && !studentScript.WitnessedCorpse)
				{
					studentScript.Fleeing = false;
					studentScript.Alarmed = false;
					studentScript.AlarmTimer = 0f;
					studentScript.ReportPhase = 0;
					studentScript.WitnessedBloodPool = false;
				}
				if (studentScript.HoldingHands)
				{
					studentScript.HoldingHands = false;
					studentScript.Paired = false;
					studentScript.enabled = true;
				}
				if (studentScript.Alive && !studentScript.Slave && !studentScript.Tranquil && !studentScript.Fleeing && studentScript.enabled && studentScript.gameObject.activeInHierarchy)
				{
					if (!studentScript.Started)
					{
						studentScript.Start();
					}
					if (!studentScript.Teacher)
					{
						if (!studentScript.Indoors)
						{
							if (studentScript.ShoeRemoval.Locker == null)
							{
								studentScript.ShoeRemoval.Start();
							}
							studentScript.ShoeRemoval.PutOnShoes();
						}
						studentScript.transform.position = studentScript.Seat.position + Vector3.up * 0.01f;
						studentScript.transform.rotation = studentScript.Seat.rotation;
						studentScript.CharacterAnimation.Play(studentScript.SitAnim);
						studentScript.Pathfinding.canSearch = false;
						studentScript.Pathfinding.canMove = false;
						studentScript.Pathfinding.speed = 0f;
						studentScript.ClubActivityPhase = 0;
						studentScript.ClubTimer = 0f;
						studentScript.Pestered = 0;
						studentScript.Distracting = false;
						studentScript.Distracted = false;
						studentScript.Tripping = false;
						studentScript.Ignoring = false;
						studentScript.Pushable = false;
						studentScript.Vomiting = false;
						studentScript.Private = false;
						studentScript.Sedated = false;
						studentScript.Emetic = false;
						studentScript.Hurry = false;
						studentScript.Safe = false;
						studentScript.CanTalk = true;
						studentScript.Routine = true;
						if (studentScript.Wet)
						{
							studentScript.CharacterAnimation[studentScript.WetAnim].weight = 0f;
							this.CommunalLocker.Student = null;
							studentScript.Schoolwear = 3;
							studentScript.ChangeSchoolwear();
							studentScript.LiquidProjector.enabled = false;
							studentScript.Splashed = false;
							studentScript.Bloody = false;
							studentScript.BathePhase = 1;
							studentScript.Wet = false;
							studentScript.UnWet();
							if (studentScript.Rival && this.CommunalLocker.RivalPhone.Stolen)
							{
								studentScript.RealizePhoneIsMissing();
							}
						}
						if (studentScript.ClubAttire)
						{
							studentScript.ChangeSchoolwear();
							studentScript.ClubAttire = false;
						}
						if (studentScript.Schoolwear != 1 && !studentScript.BeenSplashed)
						{
							studentScript.Schoolwear = 1;
							studentScript.ChangeSchoolwear();
						}
						if (studentScript.Meeting && this.Clock.HourTime > studentScript.MeetTime)
						{
							studentScript.Meeting = false;
						}
						if (studentScript.Club == ClubType.Sports)
						{
							studentScript.SetSplashes(false);
							studentScript.WalkAnim = studentScript.OriginalWalkAnim;
							studentScript.Character.transform.localPosition = new Vector3(0f, 0f, 0f);
							studentScript.Cosmetic.Goggles[studentScript.StudentID].GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, 0f);
							if (!studentScript.Cosmetic.Empty)
							{
								studentScript.Cosmetic.MaleHair[studentScript.Cosmetic.Hairstyle].GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, 0f);
							}
						}
						if (studentScript.MyPlate != null && studentScript.MyPlate.transform.parent == studentScript.RightHand)
						{
							studentScript.MyPlate.transform.parent = null;
							studentScript.MyPlate.transform.position = studentScript.OriginalPlatePosition;
							studentScript.MyPlate.transform.rotation = studentScript.OriginalPlateRotation;
							studentScript.IdleAnim = studentScript.OriginalIdleAnim;
							studentScript.WalkAnim = studentScript.OriginalWalkAnim;
						}
						if (studentScript.ReturningMisplacedWeapon)
						{
							studentScript.ReturnMisplacedWeapon();
						}
					}
					else if (this.ID != this.GymTeacherID && this.ID != this.NurseID)
					{
						studentScript.transform.position = this.Podiums.List[studentScript.Class].position + Vector3.up * 0.01f;
						studentScript.transform.rotation = this.Podiums.List[studentScript.Class].rotation;
					}
					else
					{
						studentScript.transform.position = studentScript.Seat.position + Vector3.up * 0.01f;
						studentScript.transform.rotation = studentScript.Seat.rotation;
					}
				}
			}
			this.ID++;
		}
		this.UpdateStudents(0);
		Physics.SyncTransforms();
		if (GameGlobals.SenpaiMourning)
		{
			this.Students[1].gameObject.SetActive(false);
		}
		for (int i = 1; i < 10; i++)
		{
			if (this.ShrineCollectibles[i] != null)
			{
				this.ShrineCollectibles[i].SetActive(true);
			}
		}
		this.Gift.SetActive(false);
	}

	// Token: 0x06001B47 RID: 6983 RVA: 0x001190C0 File Offset: 0x001172C0
	public void SkipTo8()
	{
		while (this.NPCsSpawned < this.NPCsTotal)
		{
			this.SpawnStudent(this.SpawnID);
			this.SpawnID++;
		}
		int num = 0;
		int num2 = 0;
		this.ID = 1;
		while (this.ID < this.Students.Length)
		{
			StudentScript studentScript = this.Students[this.ID];
			if (studentScript != null && studentScript.Alive && !studentScript.Slave && !studentScript.Tranquil)
			{
				if (!studentScript.Started)
				{
					studentScript.Start();
				}
				bool flag = false;
				if (this.MemorialScene.enabled && studentScript.Teacher)
				{
					flag = true;
					studentScript.Teacher = false;
				}
				if (!studentScript.Teacher)
				{
					if (!studentScript.Indoors)
					{
						if (studentScript.ShoeRemoval.Locker == null)
						{
							studentScript.ShoeRemoval.Start();
						}
						studentScript.ShoeRemoval.PutOnShoes();
					}
					studentScript.transform.position = studentScript.Seat.position + Vector3.up * 0.01f;
					studentScript.transform.rotation = studentScript.Seat.rotation;
					studentScript.Pathfinding.canSearch = true;
					studentScript.Pathfinding.canMove = true;
					studentScript.Pathfinding.speed = 1f;
					studentScript.ClubActivityPhase = 0;
					studentScript.Distracted = false;
					studentScript.Spawned = true;
					studentScript.Routine = true;
					studentScript.Safe = false;
					studentScript.SprintAnim = studentScript.OriginalSprintAnim;
					if (studentScript.ClubAttire)
					{
						studentScript.ChangeSchoolwear();
						studentScript.ClubAttire = true;
					}
					studentScript.TeleportToDestination();
					studentScript.TeleportToDestination();
				}
				else
				{
					studentScript.TeleportToDestination();
					studentScript.TeleportToDestination();
				}
				if (this.MemorialScene.enabled)
				{
					if (flag)
					{
						studentScript.Teacher = true;
					}
					if (studentScript.Persona == PersonaType.PhoneAddict)
					{
						studentScript.SmartPhone.SetActive(true);
					}
					if (studentScript.Actions[studentScript.Phase] == StudentActionType.Graffiti && !this.Bully)
					{
						ScheduleBlock scheduleBlock = studentScript.ScheduleBlocks[2];
						scheduleBlock.destination = "Patrol";
						scheduleBlock.action = "Patrol";
						studentScript.GetDestinations();
					}
					studentScript.SpeechLines.Stop();
					studentScript.transform.position = new Vector3(20f + (float)num * 1.1f, 0f, (float)(82 - num2 * 5));
					num2++;
					if (num2 > 4)
					{
						num++;
						num2 = 0;
					}
				}
			}
			this.ID++;
		}
	}

	// Token: 0x06001B48 RID: 6984 RVA: 0x00119344 File Offset: 0x00117544
	public void SkipTo730()
	{
		while (this.NPCsSpawned < this.NPCsTotal)
		{
			this.SpawnStudent(this.SpawnID);
			this.SpawnID++;
		}
		this.ID = 1;
		while (this.ID < this.Students.Length)
		{
			StudentScript studentScript = this.Students[this.ID];
			if (studentScript != null && studentScript.Alive && !studentScript.Slave && !studentScript.Tranquil)
			{
				if (!studentScript.Started)
				{
					studentScript.Start();
				}
				if (!studentScript.Teacher)
				{
					if (!studentScript.Indoors)
					{
						if (studentScript.ShoeRemoval.Locker == null)
						{
							studentScript.ShoeRemoval.Start();
						}
						studentScript.ShoeRemoval.PutOnShoes();
					}
					studentScript.transform.position = studentScript.Seat.position + Vector3.up * 0.01f;
					studentScript.transform.rotation = studentScript.Seat.rotation;
					studentScript.Pathfinding.canSearch = true;
					studentScript.Pathfinding.canMove = true;
					studentScript.Pathfinding.speed = 1f;
					studentScript.ClubActivityPhase = 0;
					studentScript.Distracted = false;
					studentScript.Spawned = true;
					studentScript.Routine = true;
					studentScript.Safe = false;
					studentScript.SprintAnim = studentScript.OriginalSprintAnim;
					if (studentScript.ClubAttire)
					{
						studentScript.ChangeSchoolwear();
						studentScript.ClubAttire = true;
					}
					studentScript.AltTeleportToDestination();
					studentScript.AltTeleportToDestination();
				}
				else
				{
					studentScript.AltTeleportToDestination();
					studentScript.AltTeleportToDestination();
				}
			}
			this.ID++;
		}
		Physics.SyncTransforms();
	}

	// Token: 0x06001B49 RID: 6985 RVA: 0x001194F8 File Offset: 0x001176F8
	public void ResumeMovement()
	{
		this.ID = 1;
		while (this.ID < this.Students.Length)
		{
			StudentScript studentScript = this.Students[this.ID];
			if (studentScript != null && !studentScript.Fleeing)
			{
				studentScript.Pathfinding.canSearch = true;
				studentScript.Pathfinding.canMove = true;
				studentScript.Pathfinding.speed = 1f;
				studentScript.Routine = true;
			}
			this.ID++;
		}
	}

	// Token: 0x06001B4A RID: 6986 RVA: 0x0011957C File Offset: 0x0011777C
	public void StopMoving()
	{
		this.CombatMinigame.enabled = false;
		this.Stop = true;
		if (this.GameOverIminent)
		{
			this.Portal.GetComponent<PortalScript>().EndEvents();
			this.Portal.GetComponent<PortalScript>().EndLaterEvents();
		}
		this.ID = 1;
		while (this.ID < this.Students.Length)
		{
			StudentScript studentScript = this.Students[this.ID];
			if (studentScript != null)
			{
				if (!studentScript.Dying && !studentScript.PinningDown && !studentScript.Spraying && !studentScript.Struggling)
				{
					if (this.YandereDying && studentScript.Club != ClubType.Council)
					{
						studentScript.IdleAnim = studentScript.ScaredAnim;
					}
					if (this.Yandere.Attacking)
					{
						if (studentScript.MurderReaction == 0)
						{
							studentScript.Character.GetComponent<Animation>().CrossFade(studentScript.ScaredAnim);
						}
					}
					else if (this.ID > 1 && studentScript.CharacterAnimation != null)
					{
						studentScript.CharacterAnimation.CrossFade(studentScript.IdleAnim);
					}
					studentScript.Pathfinding.canSearch = false;
					studentScript.Pathfinding.canMove = false;
					studentScript.Pathfinding.speed = 0f;
					studentScript.Stop = true;
					if (studentScript.EventManager != null)
					{
						studentScript.EventManager.EndEvent();
					}
				}
				if (studentScript.Alive && studentScript.SawMask)
				{
					this.Police.MaskReported = true;
				}
				if (studentScript.Slave && this.Police.DayOver)
				{
					Debug.Log("A mind-broken slave committed suicide.");
					studentScript.Broken.Subtitle.text = string.Empty;
					studentScript.Broken.Done = true;
					UnityEngine.Object.Destroy(studentScript.Broken);
					studentScript.BecomeRagdoll();
					studentScript.Slave = false;
					studentScript.Suicide = true;
					studentScript.DeathType = DeathType.Mystery;
					StudentGlobals.SetStudentSlave(studentScript.StudentID);
				}
			}
			this.ID++;
		}
	}

	// Token: 0x06001B4B RID: 6987 RVA: 0x00119780 File Offset: 0x00117980
	public void TimeFreeze()
	{
		this.ID = 1;
		while (this.ID < this.Students.Length)
		{
			StudentScript studentScript = this.Students[this.ID];
			if (studentScript != null && studentScript.Alive)
			{
				studentScript.enabled = false;
				studentScript.CharacterAnimation.Stop();
				studentScript.Pathfinding.canSearch = false;
				studentScript.Pathfinding.canMove = false;
				studentScript.Prompt.Hide();
				studentScript.Prompt.enabled = false;
			}
			this.ID++;
		}
	}

	// Token: 0x06001B4C RID: 6988 RVA: 0x00119814 File Offset: 0x00117A14
	public void TimeUnfreeze()
	{
		this.ID = 1;
		while (this.ID < this.Students.Length)
		{
			StudentScript studentScript = this.Students[this.ID];
			if (studentScript != null && studentScript.Alive)
			{
				studentScript.enabled = true;
				studentScript.Prompt.enabled = true;
				studentScript.Pathfinding.canSearch = true;
				studentScript.Pathfinding.canMove = true;
			}
			this.ID++;
		}
	}

	// Token: 0x06001B4D RID: 6989 RVA: 0x00119894 File Offset: 0x00117A94
	public void ComeBack()
	{
		this.Stop = false;
		this.ID = 1;
		while (this.ID < this.Students.Length)
		{
			StudentScript studentScript = this.Students[this.ID];
			if (studentScript != null)
			{
				if (!studentScript.Dying && !studentScript.Replaced && studentScript.Spawned && !StudentGlobals.GetStudentExpelled(this.ID) && !studentScript.Ragdoll.Disposed)
				{
					studentScript.gameObject.SetActive(true);
					studentScript.Pathfinding.canSearch = true;
					studentScript.Pathfinding.canMove = true;
					studentScript.Pathfinding.speed = 1f;
					studentScript.Stop = false;
				}
				if (studentScript.Teacher)
				{
					studentScript.CurrentDestination = studentScript.Destinations[studentScript.Phase];
					studentScript.Pathfinding.target = studentScript.Destinations[studentScript.Phase];
					studentScript.Alarmed = false;
					studentScript.Reacted = false;
					studentScript.Witness = false;
					studentScript.Routine = true;
					studentScript.AlarmTimer = 0f;
					studentScript.Concern = 0;
				}
				if (studentScript.Club == ClubType.Council)
				{
					studentScript.Teacher = false;
				}
				if (studentScript.Slave)
				{
					studentScript.Stop = false;
				}
			}
			this.ID++;
		}
		this.UpdateAllAnimLayers();
		if (this.Police.EndOfDay.RivalEliminationMethod == RivalEliminationType.Expelled)
		{
			this.Students[this.RivalID].gameObject.SetActive(false);
		}
		if (GameGlobals.SenpaiMourning)
		{
			this.Students[1].gameObject.SetActive(false);
		}
	}

	// Token: 0x06001B4E RID: 6990 RVA: 0x00119A28 File Offset: 0x00117C28
	public void StopFleeing()
	{
		this.ID = 1;
		while (this.ID < this.Students.Length)
		{
			StudentScript studentScript = this.Students[this.ID];
			if (studentScript != null && !studentScript.Teacher)
			{
				studentScript.Pathfinding.target = studentScript.Destinations[studentScript.Phase];
				studentScript.Pathfinding.speed = 1f;
				studentScript.WitnessedCorpse = false;
				studentScript.WitnessedMurder = false;
				studentScript.Alarmed = false;
				studentScript.Fleeing = false;
				studentScript.Reacted = false;
				studentScript.Witness = false;
				studentScript.Routine = true;
			}
			this.ID++;
		}
	}

	// Token: 0x06001B4F RID: 6991 RVA: 0x00119ADC File Offset: 0x00117CDC
	public void EnablePrompts()
	{
		this.ID = 2;
		while (this.ID < this.Students.Length)
		{
			StudentScript studentScript = this.Students[this.ID];
			if (studentScript != null)
			{
				studentScript.Prompt.enabled = true;
			}
			this.ID++;
		}
	}

	// Token: 0x06001B50 RID: 6992 RVA: 0x00119B34 File Offset: 0x00117D34
	public void DisablePrompts()
	{
		this.ID = 2;
		while (this.ID < this.Students.Length)
		{
			StudentScript studentScript = this.Students[this.ID];
			if (studentScript != null)
			{
				studentScript.Prompt.Hide();
				studentScript.Prompt.enabled = false;
			}
			this.ID++;
		}
	}

	// Token: 0x06001B51 RID: 6993 RVA: 0x00119B98 File Offset: 0x00117D98
	public void WipePendingRep()
	{
		this.ID = 2;
		while (this.ID < this.Students.Length)
		{
			StudentScript studentScript = this.Students[this.ID];
			if (studentScript != null)
			{
				studentScript.PendingRep = 0f;
			}
			this.ID++;
		}
	}

	// Token: 0x06001B52 RID: 6994 RVA: 0x00119BF0 File Offset: 0x00117DF0
	public void AttackOnTitan()
	{
		this.AoT = true;
		this.ID = 2;
		while (this.ID < this.Students.Length)
		{
			StudentScript studentScript = this.Students[this.ID];
			if (studentScript != null && !studentScript.Teacher)
			{
				studentScript.AttackOnTitan();
			}
			this.ID++;
		}
	}

	// Token: 0x06001B53 RID: 6995 RVA: 0x00119C50 File Offset: 0x00117E50
	public void Kong()
	{
		this.DK = true;
		this.ID = 1;
		while (this.ID < this.Students.Length)
		{
			StudentScript studentScript = this.Students[this.ID];
			if (studentScript != null)
			{
				studentScript.DK = true;
			}
			this.ID++;
		}
	}

	// Token: 0x06001B54 RID: 6996 RVA: 0x00119CAC File Offset: 0x00117EAC
	public void Spook()
	{
		this.Spooky = true;
		this.ID = 2;
		while (this.ID < this.Students.Length)
		{
			StudentScript studentScript = this.Students[this.ID];
			if (studentScript != null && !studentScript.Male)
			{
				studentScript.Spook();
			}
			this.ID++;
		}
	}

	// Token: 0x06001B55 RID: 6997 RVA: 0x00119D0C File Offset: 0x00117F0C
	public void BadTime()
	{
		this.Sans = true;
		this.ID = 2;
		while (this.ID < this.Students.Length)
		{
			StudentScript studentScript = this.Students[this.ID];
			if (studentScript != null)
			{
				studentScript.Prompt.HideButton[0] = false;
				studentScript.BadTime = true;
			}
			this.ID++;
		}
	}

	// Token: 0x06001B56 RID: 6998 RVA: 0x00119D74 File Offset: 0x00117F74
	public void UpdateBooths()
	{
		this.ID = 0;
		while (this.ID < this.ChangingBooths.Length)
		{
			ChangingBoothScript changingBoothScript = this.ChangingBooths[this.ID];
			if (changingBoothScript != null)
			{
				changingBoothScript.CheckYandereClub();
			}
			this.ID++;
		}
	}

	// Token: 0x06001B57 RID: 6999 RVA: 0x00119DC8 File Offset: 0x00117FC8
	public void UpdatePerception()
	{
		this.ID = 0;
		while (this.ID < this.Students.Length)
		{
			StudentScript studentScript = this.Students[this.ID];
			if (studentScript != null)
			{
				studentScript.UpdatePerception();
			}
			this.ID++;
		}
	}

	// Token: 0x06001B58 RID: 7000 RVA: 0x00119E1C File Offset: 0x0011801C
	public void StopHesitating()
	{
		this.ID = 0;
		while (this.ID < this.Students.Length)
		{
			StudentScript studentScript = this.Students[this.ID];
			if (studentScript != null)
			{
				if (studentScript.AlarmTimer > 0f)
				{
					studentScript.AlarmTimer = 1f;
				}
				studentScript.Hesitation = 0f;
			}
			this.ID++;
		}
	}

	// Token: 0x06001B59 RID: 7001 RVA: 0x00119E8C File Offset: 0x0011808C
	public void Unstop()
	{
		this.ID = 0;
		while (this.ID < this.Students.Length)
		{
			StudentScript studentScript = this.Students[this.ID];
			if (studentScript != null)
			{
				studentScript.Stop = false;
			}
			this.ID++;
		}
	}

	// Token: 0x06001B5A RID: 7002 RVA: 0x00119EE0 File Offset: 0x001180E0
	public void LowerCorpsePosition()
	{
		Debug.Log("Corpse's Y position is: " + this.CorpseLocation.position.y);
		int num;
		if (this.CorpseLocation.position.y < 2f)
		{
			num = 0;
		}
		else if (this.CorpseLocation.position.y < 4f)
		{
			num = 2;
		}
		else if (this.CorpseLocation.position.y < 6f)
		{
			num = 4;
		}
		else if (this.CorpseLocation.position.y < 8f)
		{
			num = 6;
		}
		else if (this.CorpseLocation.position.y < 10f)
		{
			num = 8;
		}
		else if (this.CorpseLocation.position.y < 12f)
		{
			num = 10;
		}
		else
		{
			num = 12;
		}
		this.CorpseLocation.position = new Vector3(this.CorpseLocation.position.x, (float)num, this.CorpseLocation.position.z);
		Debug.Log("The corpse's height is: " + num);
	}

	// Token: 0x06001B5B RID: 7003 RVA: 0x0011A004 File Offset: 0x00118204
	public void LowerBloodPosition()
	{
		int num;
		if (this.BloodLocation.position.y < 2f)
		{
			num = 0;
		}
		else if (this.BloodLocation.position.y < 4f)
		{
			num = 2;
		}
		else if (this.BloodLocation.position.y < 6f)
		{
			num = 4;
		}
		else if (this.BloodLocation.position.y < 8f)
		{
			num = 6;
		}
		else if (this.BloodLocation.position.y < 10f)
		{
			num = 8;
		}
		else if (this.BloodLocation.position.y < 12f)
		{
			num = 10;
		}
		else
		{
			num = 12;
		}
		this.BloodLocation.position = new Vector3(this.BloodLocation.position.x, (float)num, this.BloodLocation.position.z);
	}

	// Token: 0x06001B5C RID: 7004 RVA: 0x0011A0F0 File Offset: 0x001182F0
	public void CensorStudents()
	{
		this.ID = 0;
		while (this.ID < this.Students.Length)
		{
			StudentScript studentScript = this.Students[this.ID];
			if (studentScript != null && !studentScript.Male && studentScript.Club != ClubType.Teacher && studentScript.Club != ClubType.GymTeacher && studentScript.Club != ClubType.Nurse)
			{
				if (this.Censor)
				{
					studentScript.Cosmetic.CensorPanties();
				}
				else
				{
					studentScript.Cosmetic.RemoveCensor();
				}
			}
			this.ID++;
		}
	}

	// Token: 0x06001B5D RID: 7005 RVA: 0x0011A184 File Offset: 0x00118384
	private void OccupySeat()
	{
		int @class = this.JSON.Students[this.SpawnID].Class;
		int seat = this.JSON.Students[this.SpawnID].Seat;
		if (@class == 11)
		{
			this.SeatsTaken11[seat] = true;
			return;
		}
		if (@class == 12)
		{
			this.SeatsTaken12[seat] = true;
			return;
		}
		if (@class == 21)
		{
			this.SeatsTaken21[seat] = true;
			return;
		}
		if (@class == 22)
		{
			this.SeatsTaken22[seat] = true;
			return;
		}
		if (@class == 31)
		{
			this.SeatsTaken31[seat] = true;
			return;
		}
		if (@class == 32)
		{
			this.SeatsTaken32[seat] = true;
		}
	}

	// Token: 0x06001B5E RID: 7006 RVA: 0x0011A21C File Offset: 0x0011841C
	private void FindUnoccupiedSeat()
	{
		this.SeatOccupied = false;
		if (this.Class == 1)
		{
			this.JSON.Students[this.SpawnID].Class = 11;
			this.ID = 1;
			while (this.ID < this.SeatsTaken11.Length)
			{
				if (this.SeatOccupied)
				{
					break;
				}
				if (!this.SeatsTaken11[this.ID])
				{
					this.JSON.Students[this.SpawnID].Seat = this.ID;
					this.SeatsTaken11[this.ID] = true;
					this.SeatOccupied = true;
				}
				this.ID++;
				if (this.ID > 15)
				{
					this.Class++;
				}
			}
		}
		else if (this.Class == 2)
		{
			this.JSON.Students[this.SpawnID].Class = 12;
			this.ID = 1;
			while (this.ID < this.SeatsTaken12.Length)
			{
				if (this.SeatOccupied)
				{
					break;
				}
				if (!this.SeatsTaken12[this.ID])
				{
					this.JSON.Students[this.SpawnID].Seat = this.ID;
					this.SeatsTaken12[this.ID] = true;
					this.SeatOccupied = true;
				}
				this.ID++;
				if (this.ID > 15)
				{
					this.Class++;
				}
			}
		}
		else if (this.Class == 3)
		{
			this.JSON.Students[this.SpawnID].Class = 21;
			this.ID = 1;
			while (this.ID < this.SeatsTaken21.Length)
			{
				if (this.SeatOccupied)
				{
					break;
				}
				if (!this.SeatsTaken21[this.ID])
				{
					this.JSON.Students[this.SpawnID].Seat = this.ID;
					this.SeatsTaken21[this.ID] = true;
					this.SeatOccupied = true;
				}
				this.ID++;
				if (this.ID > 15)
				{
					this.Class++;
				}
			}
		}
		else if (this.Class == 4)
		{
			this.JSON.Students[this.SpawnID].Class = 22;
			this.ID = 1;
			while (this.ID < this.SeatsTaken22.Length)
			{
				if (this.SeatOccupied)
				{
					break;
				}
				if (!this.SeatsTaken22[this.ID])
				{
					this.JSON.Students[this.SpawnID].Seat = this.ID;
					this.SeatsTaken22[this.ID] = true;
					this.SeatOccupied = true;
				}
				this.ID++;
				if (this.ID > 15)
				{
					this.Class++;
				}
			}
		}
		else if (this.Class == 5)
		{
			this.JSON.Students[this.SpawnID].Class = 31;
			this.ID = 1;
			while (this.ID < this.SeatsTaken31.Length)
			{
				if (this.SeatOccupied)
				{
					break;
				}
				if (!this.SeatsTaken31[this.ID])
				{
					this.JSON.Students[this.SpawnID].Seat = this.ID;
					this.SeatsTaken31[this.ID] = true;
					this.SeatOccupied = true;
				}
				this.ID++;
				if (this.ID > 15)
				{
					this.Class++;
				}
			}
		}
		else if (this.Class == 6)
		{
			this.JSON.Students[this.SpawnID].Class = 32;
			this.ID = 1;
			while (this.ID < this.SeatsTaken32.Length && !this.SeatOccupied)
			{
				if (!this.SeatsTaken32[this.ID])
				{
					this.JSON.Students[this.SpawnID].Seat = this.ID;
					this.SeatsTaken32[this.ID] = true;
					this.SeatOccupied = true;
				}
				this.ID++;
				if (this.ID > 15)
				{
					this.Class++;
				}
			}
		}
		if (!this.SeatOccupied)
		{
			this.FindUnoccupiedSeat();
		}
	}

	// Token: 0x06001B5F RID: 7007 RVA: 0x0011A684 File Offset: 0x00118884
	public void PinDownCheck()
	{
		if (!this.PinningDown && this.Witnesses > 3)
		{
			this.ID = 1;
			while (this.ID < this.WitnessList.Length)
			{
				StudentScript studentScript = this.WitnessList[this.ID];
				if (studentScript != null && (!studentScript.Alive || studentScript.Attacked || studentScript.Fleeing || studentScript.Dying || studentScript.Routine))
				{
					if (this.ID != this.WitnessList.Length - 1)
					{
						this.Shuffle(this.ID);
					}
					this.Witnesses--;
				}
				this.ID++;
			}
			if (this.Witnesses > 3)
			{
				this.PinningDown = true;
				this.PinPhase = 1;
			}
		}
	}

	// Token: 0x06001B60 RID: 7008 RVA: 0x0011A758 File Offset: 0x00118958
	private void Shuffle(int Start)
	{
		for (int i = Start; i < this.WitnessList.Length - 1; i++)
		{
			this.WitnessList[i] = this.WitnessList[i + 1];
		}
	}

	// Token: 0x06001B61 RID: 7009 RVA: 0x0011A78C File Offset: 0x0011898C
	public void RemovePapersFromDesks()
	{
		this.ID = 1;
		while (this.ID < this.Students.Length)
		{
			StudentScript studentScript = this.Students[this.ID];
			if (studentScript != null && studentScript.MyPaper != null)
			{
				studentScript.MyPaper.SetActive(false);
			}
			this.ID++;
		}
	}

	// Token: 0x06001B62 RID: 7010 RVA: 0x0011A7F4 File Offset: 0x001189F4
	public void SetStudentsActive(bool active)
	{
		this.ID = 1;
		while (this.ID < this.Students.Length)
		{
			StudentScript studentScript = this.Students[this.ID];
			if (studentScript != null)
			{
				studentScript.gameObject.SetActive(active);
			}
			this.ID++;
		}
	}

	// Token: 0x06001B63 RID: 7011 RVA: 0x0011A84C File Offset: 0x00118A4C
	public void AssignTeachers()
	{
		this.ID = 1;
		while (this.ID < this.Students.Length)
		{
			StudentScript studentScript = this.Students[this.ID];
			if (studentScript != null)
			{
				studentScript.MyTeacher = this.Teachers[this.JSON.Students[studentScript.StudentID].Class];
			}
			this.ID++;
		}
	}

	// Token: 0x06001B64 RID: 7012 RVA: 0x0011A8BC File Offset: 0x00118ABC
	public void ToggleBookBags()
	{
		this.ID = 1;
		while (this.ID < this.Students.Length)
		{
			StudentScript studentScript = this.Students[this.ID];
			if (studentScript != null)
			{
				studentScript.BookBag.SetActive(!studentScript.BookBag.activeInHierarchy);
			}
			this.ID++;
		}
	}

	// Token: 0x06001B65 RID: 7013 RVA: 0x0011A920 File Offset: 0x00118B20
	public void DetermineVictim()
	{
		this.Bully = false;
		this.ID = 2;
		while (this.ID < this.Students.Length)
		{
			StudentScript studentScript = this.Students[this.ID];
			if (studentScript != null && (float)StudentGlobals.GetStudentReputation(this.ID) < 33.33333f && (this.ID != 36 || TaskGlobals.GetTaskStatus(36) != 3) && !studentScript.Teacher && !studentScript.Slave && studentScript.Club != ClubType.Bully && studentScript.Club != ClubType.Council && studentScript.Club != ClubType.Photography && studentScript.Club != ClubType.Delinquent && (float)StudentGlobals.GetStudentReputation(this.ID) < this.LowestRep)
			{
				this.LowestRep = (float)StudentGlobals.GetStudentReputation(this.ID);
				this.VictimID = this.ID;
				this.Bully = true;
			}
			this.ID++;
		}
		if (this.Bully)
		{
			Debug.Log("A student has been chosen to be bullied. It's Student #" + this.VictimID + ".");
			if (this.Students[this.VictimID].Seat.position.x > 0f)
			{
				this.BullyGroup.position = this.Students[this.VictimID].Seat.position + new Vector3(0.33333f, 0f, 0f);
			}
			else
			{
				this.BullyGroup.position = this.Students[this.VictimID].Seat.position - new Vector3(0.33333f, 0f, 0f);
				this.BullyGroup.eulerAngles = new Vector3(0f, 90f, 0f);
			}
			StudentScript studentScript2 = this.Students[this.VictimID];
			ScheduleBlock scheduleBlock = studentScript2.ScheduleBlocks[2];
			scheduleBlock.destination = "ShameSpot";
			scheduleBlock.action = "Shamed";
			scheduleBlock.time = 8f;
			ScheduleBlock scheduleBlock2 = studentScript2.ScheduleBlocks[4];
			scheduleBlock2.destination = "Seat";
			scheduleBlock2.action = "Sit";
			if (studentScript2.Male)
			{
				studentScript2.ChemistScanner.MyRenderer.materials[1].mainTexture = studentScript2.ChemistScanner.SadEyes;
				studentScript2.ChemistScanner.enabled = false;
			}
			studentScript2.IdleAnim = studentScript2.BulliedIdleAnim;
			studentScript2.WalkAnim = studentScript2.BulliedWalkAnim;
			studentScript2.Bullied = true;
			studentScript2.GetDestinations();
			studentScript2.CameraAnims = studentScript2.CowardAnims;
			studentScript2.BusyAtLunch = true;
			studentScript2.Shy = false;
		}
	}

	// Token: 0x06001B66 RID: 7014 RVA: 0x0011ABC4 File Offset: 0x00118DC4
	public void SecurityCameras()
	{
		this.Egg = true;
		this.ID = 1;
		while (this.ID < this.Students.Length)
		{
			StudentScript studentScript = this.Students[this.ID];
			if (studentScript != null && studentScript.SecurityCamera != null && studentScript.Alive)
			{
				Debug.Log("Enabling security camera on this character's head.");
				studentScript.SecurityCamera.SetActive(true);
			}
			this.ID++;
		}
	}

	// Token: 0x06001B67 RID: 7015 RVA: 0x0011AC44 File Offset: 0x00118E44
	public void DisableEveryone()
	{
		this.ID = 1;
		while (this.ID < this.Students.Length)
		{
			StudentScript studentScript = this.Students[this.ID];
			if (studentScript != null && !studentScript.Ragdoll.enabled)
			{
				studentScript.gameObject.SetActive(false);
			}
			this.ID++;
		}
	}

	// Token: 0x06001B68 RID: 7016 RVA: 0x0011ACA8 File Offset: 0x00118EA8
	public void DisableStudent(int DisableID)
	{
		StudentScript studentScript = this.Students[DisableID];
		if (studentScript != null)
		{
			if (studentScript.gameObject.activeInHierarchy)
			{
				studentScript.gameObject.SetActive(false);
				return;
			}
			studentScript.gameObject.SetActive(true);
			this.UpdateOneAnimLayer(DisableID);
			this.Students[DisableID].ReadPhase = 0;
		}
	}

	// Token: 0x06001B69 RID: 7017 RVA: 0x0011AD02 File Offset: 0x00118F02
	public void UpdateOneAnimLayer(int DisableID)
	{
		this.Students[DisableID].UpdateAnimLayers();
		this.Students[DisableID].ReadPhase = 0;
	}

	// Token: 0x06001B6A RID: 7018 RVA: 0x0011AD20 File Offset: 0x00118F20
	public void UpdateAllAnimLayers()
	{
		this.ID = 1;
		while (this.ID < this.Students.Length)
		{
			StudentScript studentScript = this.Students[this.ID];
			if (studentScript != null)
			{
				studentScript.UpdateAnimLayers();
				studentScript.ReadPhase = 0;
			}
			this.ID++;
		}
	}

	// Token: 0x06001B6B RID: 7019 RVA: 0x0011AD78 File Offset: 0x00118F78
	public void UpdateGrafitti()
	{
		this.ID = 1;
		while (this.ID < 6)
		{
			if (!this.NoBully[this.ID])
			{
				this.Graffiti[this.ID].SetActive(true);
			}
			this.ID++;
		}
	}

	// Token: 0x06001B6C RID: 7020 RVA: 0x0011ADC8 File Offset: 0x00118FC8
	public void UpdateAllBentos()
	{
		this.ID = 1;
		while (this.ID < this.Students.Length)
		{
			StudentScript studentScript = this.Students[this.ID];
			if (studentScript != null)
			{
				studentScript.Bento.GetComponent<GenericBentoScript>().Prompt.Yandere = this.Yandere;
				studentScript.Bento.GetComponent<GenericBentoScript>().UpdatePrompts();
			}
			this.ID++;
		}
	}

	// Token: 0x06001B6D RID: 7021 RVA: 0x0011AE40 File Offset: 0x00119040
	public void UpdateSleuths()
	{
		this.SleuthPhase++;
		this.ID = 56;
		while (this.ID < 61)
		{
			if (this.Students[this.ID] != null && !this.Students[this.ID].Slave && !this.Students[this.ID].Following)
			{
				if (this.SleuthPhase < 3)
				{
					this.Students[this.ID].SleuthTarget = this.SleuthDestinations[this.ID - 55];
					this.Students[this.ID].Pathfinding.target = this.Students[this.ID].SleuthTarget;
					this.Students[this.ID].CurrentDestination = this.Students[this.ID].SleuthTarget;
				}
				else if (this.SleuthPhase == 3)
				{
					this.Students[this.ID].GetSleuthTarget();
				}
				else if (this.SleuthPhase == 4)
				{
					this.Students[this.ID].SleuthTarget = this.Clubs.List[this.ID];
					this.Students[this.ID].Pathfinding.target = this.Students[this.ID].SleuthTarget;
					this.Students[this.ID].CurrentDestination = this.Students[this.ID].SleuthTarget;
				}
				this.Students[this.ID].SmartPhone.SetActive(true);
				this.Students[this.ID].SpeechLines.Stop();
			}
			this.ID++;
		}
	}

	// Token: 0x06001B6E RID: 7022 RVA: 0x0011B00C File Offset: 0x0011920C
	public void UpdateDrama()
	{
		if (!this.MemorialScene.gameObject.activeInHierarchy)
		{
			this.DramaPhase++;
			this.ID = 26;
			while (this.ID < 31)
			{
				if (this.Students[this.ID] != null)
				{
					if (this.DramaPhase == 1)
					{
						this.Clubs.List[this.ID].position = this.OriginalClubPositions[this.ID - 25];
						this.Clubs.List[this.ID].rotation = this.OriginalClubRotations[this.ID - 25];
						this.Students[this.ID].ClubAnim = this.Students[this.ID].OriginalClubAnim;
					}
					else if (this.DramaPhase == 2)
					{
						this.Clubs.List[this.ID].position = this.DramaSpots[this.ID - 25].position;
						this.Clubs.List[this.ID].rotation = this.DramaSpots[this.ID - 25].rotation;
						if (this.ID == 26)
						{
							this.Students[this.ID].ClubAnim = this.Students[this.ID].ActAnim;
						}
						else if (this.ID == 27)
						{
							this.Students[this.ID].ClubAnim = this.Students[this.ID].ThinkAnim;
						}
						else if (this.ID == 28)
						{
							this.Students[this.ID].ClubAnim = this.Students[this.ID].ThinkAnim;
						}
						else if (this.ID == 29)
						{
							this.Students[this.ID].ClubAnim = this.Students[this.ID].ActAnim;
						}
						else if (this.ID == 30)
						{
							this.Students[this.ID].ClubAnim = this.Students[this.ID].ThinkAnim;
						}
					}
					else if (this.DramaPhase == 3)
					{
						this.Clubs.List[this.ID].position = this.BackstageSpots[this.ID - 25].position;
						this.Clubs.List[this.ID].rotation = this.BackstageSpots[this.ID - 25].rotation;
					}
					else if (this.DramaPhase == 4)
					{
						this.DramaPhase = 1;
						this.UpdateDrama();
					}
					this.Students[this.ID].DistanceToDestination = 100f;
					this.Students[this.ID].SmartPhone.SetActive(false);
					this.Students[this.ID].SpeechLines.Stop();
				}
				this.ID++;
			}
		}
	}

	// Token: 0x06001B6F RID: 7023 RVA: 0x0011B328 File Offset: 0x00119528
	public void UpdateMartialArts()
	{
		this.ConvoManager.Confirmed = false;
		this.MartialArtsPhase++;
		this.ID = 46;
		while (this.ID < 51)
		{
			if (this.Students[this.ID] != null)
			{
				if (this.MartialArtsPhase == 1)
				{
					this.Clubs.List[this.ID].position = this.MartialArtsSpots[this.ID - 45].position;
					this.Clubs.List[this.ID].rotation = this.MartialArtsSpots[this.ID - 45].rotation;
				}
				else if (this.MartialArtsPhase == 2)
				{
					this.Clubs.List[this.ID].position = this.MartialArtsSpots[this.ID - 40].position;
					this.Clubs.List[this.ID].rotation = this.MartialArtsSpots[this.ID - 40].rotation;
				}
				else if (this.MartialArtsPhase == 3)
				{
					this.Clubs.List[this.ID].position = this.MartialArtsSpots[this.ID - 35].position;
					this.Clubs.List[this.ID].rotation = this.MartialArtsSpots[this.ID - 35].rotation;
				}
				else if (this.MartialArtsPhase == 4)
				{
					this.MartialArtsPhase = 0;
					this.UpdateMartialArts();
				}
				this.Students[this.ID].DistanceToDestination = 100f;
				this.Students[this.ID].SmartPhone.SetActive(false);
				this.Students[this.ID].SpeechLines.Stop();
			}
			this.ID++;
		}
	}

	// Token: 0x06001B70 RID: 7024 RVA: 0x0011B518 File Offset: 0x00119718
	public void UpdateMeeting()
	{
		this.MeetingTimer += Time.deltaTime;
		if (this.MeetingTimer > 5f)
		{
			this.Speaker += 5;
			if (this.Speaker == 91)
			{
				this.Speaker = 21;
			}
			else if (this.Speaker == 76)
			{
				this.Speaker = 86;
			}
			else if (this.Speaker == 36)
			{
				this.Speaker = 41;
			}
			this.MeetingTimer = 0f;
		}
	}

	// Token: 0x06001B71 RID: 7025 RVA: 0x0011B598 File Offset: 0x00119798
	public void CheckMusic()
	{
		int num = 0;
		this.ID = 51;
		while (this.ID < 56)
		{
			if (this.Students[this.ID] != null && this.Students[this.ID].Routine && this.Students[this.ID].DistanceToDestination < 0.1f)
			{
				num++;
			}
			this.ID++;
		}
		if (num == 5)
		{
			this.PracticeVocals.pitch = Time.timeScale;
			this.PracticeMusic.pitch = Time.timeScale;
			if (!this.PracticeMusic.isPlaying)
			{
				this.PracticeVocals.Play();
				this.PracticeMusic.Play();
				return;
			}
		}
		else
		{
			this.PracticeVocals.Stop();
			this.PracticeMusic.Stop();
		}
	}

	// Token: 0x06001B72 RID: 7026 RVA: 0x0011B670 File Offset: 0x00119870
	public void UpdateAprons()
	{
		this.ID = 21;
		while (this.ID < 26)
		{
			if (this.Students[this.ID] != null && this.Students[this.ID].ClubMemberID > 0 && this.Students[this.ID].ApronAttacher != null && this.Students[this.ID].ApronAttacher.newRenderer != null)
			{
				this.Students[this.ID].ApronAttacher.newRenderer.material.mainTexture = this.Students[this.ID].Cosmetic.ApronTextures[this.Students[this.ID].ClubMemberID];
			}
			this.ID++;
		}
	}

	// Token: 0x06001B73 RID: 7027 RVA: 0x0011B75C File Offset: 0x0011995C
	public void PreventAlarm()
	{
		this.ID = 1;
		while (this.ID < 101)
		{
			if (this.Students[this.ID] != null)
			{
				this.Students[this.ID].Alarm = 0f;
			}
			this.ID++;
		}
	}

	// Token: 0x06001B74 RID: 7028 RVA: 0x0011B7B8 File Offset: 0x001199B8
	public void VolumeDown()
	{
		this.ID = 51;
		while (this.ID < 56)
		{
			if (this.Students[this.ID] != null && this.Students[this.ID].Instruments[this.Students[this.ID].ClubMemberID] != null)
			{
				this.Students[this.ID].Instruments[this.Students[this.ID].ClubMemberID].GetComponent<AudioSource>().volume = 0.2f;
			}
			this.ID++;
		}
	}

	// Token: 0x06001B75 RID: 7029 RVA: 0x0011B864 File Offset: 0x00119A64
	public void VolumeUp()
	{
		this.ID = 51;
		while (this.ID < 56)
		{
			if (this.Students[this.ID] != null && this.Students[this.ID].Instruments[this.Students[this.ID].ClubMemberID] != null)
			{
				this.Students[this.ID].Instruments[this.Students[this.ID].ClubMemberID].GetComponent<AudioSource>().volume = 1f;
			}
			this.ID++;
		}
	}

	// Token: 0x06001B76 RID: 7030 RVA: 0x0011B910 File Offset: 0x00119B10
	public void GetMaleVomitSpot(StudentScript VomitStudent)
	{
		this.MaleVomitSpot = this.MaleVomitSpots[1];
		VomitStudent.VomitDoor = this.MaleToiletDoors[1];
		this.ID = 2;
		while (this.ID < 7)
		{
			if (Vector3.Distance(VomitStudent.transform.position, this.MaleVomitSpots[this.ID].position) < Vector3.Distance(VomitStudent.transform.position, this.MaleVomitSpot.position))
			{
				this.MaleVomitSpot = this.MaleVomitSpots[this.ID];
				VomitStudent.VomitDoor = this.MaleToiletDoors[this.ID];
			}
			this.ID++;
		}
	}

	// Token: 0x06001B77 RID: 7031 RVA: 0x0011B9C0 File Offset: 0x00119BC0
	public void GetFemaleVomitSpot(StudentScript VomitStudent)
	{
		this.FemaleVomitSpot = this.FemaleVomitSpots[1];
		VomitStudent.VomitDoor = this.FemaleToiletDoors[1];
		this.ID = 2;
		while (this.ID < 7)
		{
			if (Vector3.Distance(VomitStudent.transform.position, this.FemaleVomitSpots[this.ID].position) < Vector3.Distance(VomitStudent.transform.position, this.FemaleVomitSpot.position))
			{
				this.FemaleVomitSpot = this.FemaleVomitSpots[this.ID];
				VomitStudent.VomitDoor = this.FemaleToiletDoors[this.ID];
			}
			this.ID++;
		}
	}

	// Token: 0x06001B78 RID: 7032 RVA: 0x0011BA70 File Offset: 0x00119C70
	public void GetMaleWashSpot(StudentScript VomitStudent)
	{
		Transform transform = this.MaleWashSpots[1];
		this.ID = 2;
		while (this.ID < 7)
		{
			if (Vector3.Distance(VomitStudent.transform.position, this.MaleWashSpots[this.ID].position) < Vector3.Distance(VomitStudent.transform.position, transform.position))
			{
				transform = this.MaleWashSpots[this.ID];
			}
			this.ID++;
		}
		this.MaleWashSpot = transform;
	}

	// Token: 0x06001B79 RID: 7033 RVA: 0x0011BAF8 File Offset: 0x00119CF8
	public void GetFemaleWashSpot(StudentScript VomitStudent)
	{
		Transform transform = this.FemaleWashSpots[1];
		this.ID = 2;
		while (this.ID < 7)
		{
			if (Vector3.Distance(VomitStudent.transform.position, this.FemaleWashSpots[this.ID].position) < Vector3.Distance(VomitStudent.transform.position, transform.position))
			{
				transform = this.FemaleWashSpots[this.ID];
			}
			this.ID++;
		}
		this.FemaleWashSpot = transform;
	}

	// Token: 0x06001B7A RID: 7034 RVA: 0x0011BB80 File Offset: 0x00119D80
	public void GetNearestFountain(StudentScript Student)
	{
		DrinkingFountainScript drinkingFountainScript = this.DrinkingFountains[1];
		bool flag = false;
		this.ID = 1;
		while (drinkingFountainScript.Occupied)
		{
			drinkingFountainScript = this.DrinkingFountains[1 + this.ID];
			this.ID++;
			if (1 + this.ID == this.DrinkingFountains.Length)
			{
				flag = true;
				break;
			}
		}
		if (flag)
		{
			Student.EquipCleaningItems();
			Student.EatingSnack = false;
			Student.Private = false;
			Student.Routine = true;
			Student.StudentManager.UpdateMe(Student.StudentID);
			Student.CurrentDestination = Student.Destinations[Student.Phase];
			Student.Pathfinding.target = Student.Destinations[Student.Phase];
			return;
		}
		this.ID = 2;
		while (this.ID < 8)
		{
			if (Vector3.Distance(Student.transform.position, this.DrinkingFountains[this.ID].transform.position) < Vector3.Distance(Student.transform.position, drinkingFountainScript.transform.position) && !this.DrinkingFountains[this.ID].Occupied)
			{
				drinkingFountainScript = this.DrinkingFountains[this.ID];
			}
			this.ID++;
		}
		Student.DrinkingFountain = drinkingFountainScript;
		Student.DrinkingFountain.Occupied = true;
	}

	// Token: 0x06001B7B RID: 7035 RVA: 0x0011BCD4 File Offset: 0x00119ED4
	public void Save()
	{
		int profile = GameGlobals.Profile;
		int @int = PlayerPrefs.GetInt("SaveSlot");
		Debug.Log("At the moment of saving, ClubGlobals.Club is: " + ClubGlobals.Club);
		this.BloodParent.RecordAllBlood();
		YanSave.SaveData(string.Concat(new object[]
		{
			"Profile_",
			profile,
			"_Slot_",
			@int
		}));
	}

	// Token: 0x06001B7C RID: 7036 RVA: 0x0011BD48 File Offset: 0x00119F48
	public void Load()
	{
		Debug.Log("Now loading save data.");
		int profile = GameGlobals.Profile;
		int @int = PlayerPrefs.GetInt("SaveSlot");
		YanSave.LoadData(string.Concat(new object[]
		{
			"Profile_",
			profile,
			"_Slot_",
			@int
		}), false);
		Debug.Log("Upon saving, ClubGlobals.Club is: " + ClubGlobals.Club);
		Physics.SyncTransforms();
		this.ID = 1;
		while (this.ID < 101)
		{
			if (this.Students[this.ID] != null)
			{
				if (!this.Students[this.ID].Alive)
				{
					Debug.Log(this.Students[this.ID].Name + " is confirmed to be dead.");
					Vector3 localPosition = this.Students[this.ID].Hips.localPosition;
					Quaternion localRotation = this.Students[this.ID].Hips.localRotation;
					this.Students[this.ID].BecomeRagdoll();
					this.Students[this.ID].Ragdoll.UpdateNextFrame = true;
					this.Students[this.ID].Ragdoll.NextPosition = localPosition;
					this.Students[this.ID].Ragdoll.NextRotation = localRotation;
					Debug.Log("Adding " + this.Students[this.ID].Name + " to the Police CorpseList.");
					this.Police.CorpseList[this.Police.Corpses] = this.Students[this.ID].Ragdoll;
					this.Police.Corpses++;
					if (this.Students[this.ID].Removed)
					{
						Debug.Log("Removing " + this.Students[this.ID].Name + " from the Police CorpseList.");
						this.Students[this.ID].Ragdoll.Remove();
						this.Police.Corpses--;
					}
				}
				else
				{
					if (this.Students[this.ID].ChangingShoes)
					{
						this.Students[this.ID].ShoeRemoval.enabled = true;
					}
					if (this.Students[this.ID].Schoolwear != 1)
					{
						this.Students[this.ID].ChangeSchoolwear();
					}
					if (this.Students[this.ID].ClubAttire)
					{
						this.Students[this.ID].ClubAttire = false;
						this.Students[this.ID].ChangeClubwear();
					}
					if (this.Students[this.ID].Defeats > 0)
					{
						this.Students[this.ID].IdleAnim = "idleInjured_00";
						this.Students[this.ID].WalkAnim = "walkInjured_00";
						this.Students[this.ID].OriginalIdleAnim = this.Students[this.ID].IdleAnim;
						this.Students[this.ID].OriginalWalkAnim = this.Students[this.ID].WalkAnim;
						this.Students[this.ID].LeanAnim = this.Students[this.ID].IdleAnim;
						this.Students[this.ID].CharacterAnimation.CrossFade(this.Students[this.ID].IdleAnim);
						this.Students[this.ID].Injured = true;
						this.Students[this.ID].Strength = 0;
						ScheduleBlock scheduleBlock = this.Students[this.ID].ScheduleBlocks[2];
						scheduleBlock.destination = "Sulk";
						scheduleBlock.action = "Sulk";
						ScheduleBlock scheduleBlock2 = this.Students[this.ID].ScheduleBlocks[4];
						scheduleBlock2.destination = "Sulk";
						scheduleBlock2.action = "Sulk";
						ScheduleBlock scheduleBlock3 = this.Students[this.ID].ScheduleBlocks[6];
						scheduleBlock3.destination = "Sulk";
						scheduleBlock3.action = "Sulk";
						ScheduleBlock scheduleBlock4 = this.Students[this.ID].ScheduleBlocks[7];
						scheduleBlock4.destination = "Sulk";
						scheduleBlock4.action = "Sulk";
						this.Students[this.ID].GetDestinations();
					}
					if (this.Students[this.ID].Actions[this.Students[this.ID].Phase] == StudentActionType.ClubAction && this.Students[this.ID].Club == ClubType.Cooking && this.Students[this.ID].ClubActivityPhase > 0)
					{
						this.Students[this.ID].MyPlate.parent = this.Students[this.ID].RightHand;
						this.Students[this.ID].MyPlate.localPosition = new Vector3(0.02f, -0.02f, -0.15f);
						this.Students[this.ID].MyPlate.localEulerAngles = new Vector3(-5f, -90f, 172.5f);
						this.Students[this.ID].IdleAnim = this.Students[this.ID].PlateIdleAnim;
						this.Students[this.ID].WalkAnim = this.Students[this.ID].PlateWalkAnim;
						this.Students[this.ID].LeanAnim = this.Students[this.ID].PlateIdleAnim;
						this.Students[this.ID].GetFoodTarget();
						this.Students[this.ID].ClubTimer = 0f;
					}
					else if (this.Students[this.ID].Phase > 0)
					{
						this.Students[this.ID].Phase--;
					}
				}
			}
			this.ID++;
		}
		this.Clock.UpdateClock();
		this.Alphabet.UpdateText();
		this.ClubManager.ActivateClubBenefit();
		this.Yandere.CanMove = true;
		this.Yandere.ClubAccessory();
		this.Yandere.WeaponManager.EquipWeaponsFromSave();
		this.Yandere.WeaponManager.RestoreWeaponToStudent();
		this.Yandere.WeaponManager.UpdateDelinquentWeapons();
		if (this.Yandere.ClubAttire)
		{
			this.Yandere.ClubAttire = false;
			this.Yandere.ChangeClubwear();
		}
		foreach (DoorScript doorScript in this.Doors)
		{
			if (doorScript != null && doorScript.Open)
			{
				doorScript.OpenDoor();
			}
		}
		this.BloodParent.RestoreAllBlood();
	}

	// Token: 0x06001B7D RID: 7037 RVA: 0x0011C448 File Offset: 0x0011A648
	public void UpdateBlood()
	{
		if (this.Police.BloodParent.childCount > 0)
		{
			this.ID = 0;
			foreach (object obj in this.Police.BloodParent)
			{
				Transform transform = (Transform)obj;
				if (this.ID < 100)
				{
					this.Blood[this.ID] = transform.gameObject.GetComponent<Collider>();
					this.ID++;
				}
			}
		}
		if (this.Police.BloodParent.childCount > 0 || this.Police.LimbParent.childCount > 0)
		{
			this.ID = 0;
			foreach (object obj2 in this.Police.LimbParent)
			{
				Transform transform2 = (Transform)obj2;
				if (this.ID < 100)
				{
					this.Limbs[this.ID] = transform2.gameObject.GetComponent<Collider>();
					this.ID++;
				}
			}
		}
	}

	// Token: 0x06001B7E RID: 7038 RVA: 0x0011C58C File Offset: 0x0011A78C
	public void CanAnyoneSeeYandere()
	{
		this.YandereVisible = false;
		foreach (StudentScript studentScript in this.Students)
		{
			if (studentScript != null && studentScript.CanSeeObject(studentScript.Yandere.gameObject, studentScript.Yandere.HeadPosition))
			{
				this.YandereVisible = true;
				return;
			}
		}
	}

	// Token: 0x06001B7F RID: 7039 RVA: 0x0011C5E8 File Offset: 0x0011A7E8
	public void SetFaces(float alpha)
	{
		foreach (StudentScript studentScript in this.Students)
		{
			if (studentScript != null && studentScript.StudentID > 1)
			{
				studentScript.MyRenderer.materials[0].color = new Color(1f - alpha, 1f - alpha, 1f - alpha, 1f);
				studentScript.MyRenderer.materials[1].color = new Color(1f - alpha, 1f - alpha, 1f - alpha, 1f);
				studentScript.MyRenderer.materials[2].color = new Color(1f - alpha, 1f - alpha, 1f - alpha, 1f);
				studentScript.Cosmetic.LeftEyeRenderer.material.color = new Color(1f - alpha, 1f - alpha, 1f - alpha, 1f);
				studentScript.Cosmetic.RightEyeRenderer.material.color = new Color(1f - alpha, 1f - alpha, 1f - alpha, 1f);
				studentScript.Cosmetic.HairRenderer.material.color = new Color(1f - alpha, 1f - alpha, 1f - alpha, 1f);
			}
		}
	}

	// Token: 0x06001B80 RID: 7040 RVA: 0x0011C75C File Offset: 0x0011A95C
	public void DisableChaseCameras()
	{
		foreach (StudentScript studentScript in this.Students)
		{
			if (studentScript != null)
			{
				studentScript.ChaseCamera.SetActive(false);
			}
		}
	}

	// Token: 0x06001B81 RID: 7041 RVA: 0x0011C798 File Offset: 0x0011A998
	public void InitializeReputations()
	{
		StudentGlobals.SetReputationTriangle(1, new Vector3(0f, 0f, 0f));
		StudentGlobals.SetReputationTriangle(2, new Vector3(70f, -10f, 10f));
		StudentGlobals.SetReputationTriangle(3, new Vector3(50f, -10f, 30f));
		StudentGlobals.SetReputationTriangle(4, new Vector3(0f, 10f, 0f));
		StudentGlobals.SetReputationTriangle(5, new Vector3(-50f, -30f, 10f));
		StudentGlobals.SetReputationTriangle(6, new Vector3(30f, 0f, 0f));
		StudentGlobals.SetReputationTriangle(7, new Vector3(-10f, -10f, -10f));
		StudentGlobals.SetReputationTriangle(8, new Vector3(0f, 10f, -30f));
		StudentGlobals.SetReputationTriangle(9, new Vector3(0f, 0f, 0f));
		StudentGlobals.SetReputationTriangle(10, new Vector3(100f, 100f, 100f));
		StudentGlobals.SetReputationTriangle(11, new Vector3(100f, 100f, 0f));
		StudentGlobals.SetReputationTriangle(12, new Vector3(100f, 100f, -10f));
		StudentGlobals.SetReputationTriangle(13, new Vector3(-10f, 100f, 100f));
		StudentGlobals.SetReputationTriangle(14, new Vector3(0f, 100f, -10f));
		StudentGlobals.SetReputationTriangle(15, new Vector3(100f, 100f, 0f));
		StudentGlobals.SetReputationTriangle(16, new Vector3(0f, -10f, 0f));
		StudentGlobals.SetReputationTriangle(17, new Vector3(-10f, -10f, 50f));
		StudentGlobals.SetReputationTriangle(18, new Vector3(-100f, -100f, 100f));
		StudentGlobals.SetReputationTriangle(19, new Vector3(10f, 0f, 0f));
		StudentGlobals.SetReputationTriangle(20, new Vector3(100f, 100f, 100f));
		StudentGlobals.SetReputationTriangle(21, new Vector3(50f, 100f, 0f));
		StudentGlobals.SetReputationTriangle(22, new Vector3(30f, 50f, 0f));
		StudentGlobals.SetReputationTriangle(23, new Vector3(50f, 50f, 0f));
		StudentGlobals.SetReputationTriangle(24, new Vector3(30f, 50f, 10f));
		StudentGlobals.SetReputationTriangle(25, new Vector3(70f, 50f, -30f));
		StudentGlobals.SetReputationTriangle(26, new Vector3(-10f, 100f, 0f));
		StudentGlobals.SetReputationTriangle(27, new Vector3(0f, 70f, 0f));
		StudentGlobals.SetReputationTriangle(28, new Vector3(0f, 50f, 0f));
		StudentGlobals.SetReputationTriangle(29, new Vector3(-10f, 50f, 0f));
		StudentGlobals.SetReputationTriangle(30, new Vector3(30f, 50f, 0f));
		StudentGlobals.SetReputationTriangle(31, new Vector3(-70f, 100f, 10f));
		StudentGlobals.SetReputationTriangle(32, new Vector3(-70f, -10f, 10f));
		StudentGlobals.SetReputationTriangle(33, new Vector3(-70f, -10f, 10f));
		StudentGlobals.SetReputationTriangle(34, new Vector3(-70f, -10f, 10f));
		StudentGlobals.SetReputationTriangle(35, new Vector3(-70f, -10f, 10f));
		StudentGlobals.SetReputationTriangle(36, new Vector3(-70f, 100f, 0f));
		StudentGlobals.SetReputationTriangle(37, new Vector3(0f, -10f, 0f));
		StudentGlobals.SetReputationTriangle(38, new Vector3(50f, 0f, 0f));
		StudentGlobals.SetReputationTriangle(39, new Vector3(-50f, -10f, 0f));
		StudentGlobals.SetReputationTriangle(40, new Vector3(70f, -30f, 10f));
		StudentGlobals.SetReputationTriangle(41, new Vector3(0f, 100f, 0f));
		StudentGlobals.SetReputationTriangle(42, new Vector3(-50f, -30f, 30f));
		StudentGlobals.SetReputationTriangle(43, new Vector3(-10f, -10f, 0f));
		StudentGlobals.SetReputationTriangle(44, new Vector3(-10f, 0f, 0f));
		StudentGlobals.SetReputationTriangle(45, new Vector3(0f, -10f, 0f));
		StudentGlobals.SetReputationTriangle(46, new Vector3(100f, 100f, 100f));
		StudentGlobals.SetReputationTriangle(47, new Vector3(10f, 30f, 10f));
		StudentGlobals.SetReputationTriangle(48, new Vector3(30f, 10f, 10f));
		StudentGlobals.SetReputationTriangle(49, new Vector3(30f, 30f, 10f));
		StudentGlobals.SetReputationTriangle(50, new Vector3(30f, 10f, 10f));
		StudentGlobals.SetReputationTriangle(51, new Vector3(10f, 100f, 0f));
		StudentGlobals.SetReputationTriangle(52, new Vector3(30f, 70f, 0f));
		StudentGlobals.SetReputationTriangle(53, new Vector3(50f, 10f, 0f));
		StudentGlobals.SetReputationTriangle(54, new Vector3(50f, 50f, -10f));
		StudentGlobals.SetReputationTriangle(55, new Vector3(30f, 30f, 0f));
		StudentGlobals.SetReputationTriangle(56, new Vector3(70f, 100f, 0f));
		StudentGlobals.SetReputationTriangle(57, new Vector3(70f, -30f, 0f));
		StudentGlobals.SetReputationTriangle(58, new Vector3(70f, -30f, 0f));
		StudentGlobals.SetReputationTriangle(59, new Vector3(50f, -10f, 0f));
		StudentGlobals.SetReputationTriangle(60, new Vector3(-10f, -50f, 0f));
		StudentGlobals.SetReputationTriangle(61, new Vector3(-50f, 100f, 100f));
		StudentGlobals.SetReputationTriangle(62, new Vector3(0f, 70f, 10f));
		StudentGlobals.SetReputationTriangle(63, new Vector3(0f, 30f, 50f));
		StudentGlobals.SetReputationTriangle(64, new Vector3(-10f, 30f, 50f));
		StudentGlobals.SetReputationTriangle(65, new Vector3(-10f, 30f, 50f));
		StudentGlobals.SetReputationTriangle(66, new Vector3(-50f, 100f, 50f));
		StudentGlobals.SetReputationTriangle(67, new Vector3(30f, 70f, 0f));
		StudentGlobals.SetReputationTriangle(68, new Vector3(0f, 0f, 50f));
		StudentGlobals.SetReputationTriangle(69, new Vector3(30f, 50f, 0f));
		StudentGlobals.SetReputationTriangle(70, new Vector3(50f, 30f, 0f));
		StudentGlobals.SetReputationTriangle(71, new Vector3(100f, 100f, -100f));
		StudentGlobals.SetReputationTriangle(72, new Vector3(50f, 30f, 0f));
		StudentGlobals.SetReputationTriangle(73, new Vector3(100f, 100f, -100f));
		StudentGlobals.SetReputationTriangle(74, new Vector3(70f, 50f, -50f));
		StudentGlobals.SetReputationTriangle(75, new Vector3(10f, 50f, 0f));
		StudentGlobals.SetReputationTriangle(76, new Vector3(-100f, -100f, 100f));
		StudentGlobals.SetReputationTriangle(77, new Vector3(-100f, -100f, 100f));
		StudentGlobals.SetReputationTriangle(78, new Vector3(-100f, -100f, 100f));
		StudentGlobals.SetReputationTriangle(79, new Vector3(-100f, -100f, 100f));
		StudentGlobals.SetReputationTriangle(80, new Vector3(-100f, -100f, 100f));
		StudentGlobals.SetReputationTriangle(81, new Vector3(50f, -10f, 50f));
		StudentGlobals.SetReputationTriangle(82, new Vector3(50f, -10f, 50f));
		StudentGlobals.SetReputationTriangle(83, new Vector3(50f, -10f, 50f));
		StudentGlobals.SetReputationTriangle(84, new Vector3(50f, -10f, 50f));
		StudentGlobals.SetReputationTriangle(85, new Vector3(50f, -10f, 50f));
		StudentGlobals.SetReputationTriangle(86, new Vector3(30f, 100f, 70f));
		StudentGlobals.SetReputationTriangle(87, new Vector3(30f, -10f, 100f));
		StudentGlobals.SetReputationTriangle(88, new Vector3(100f, 30f, 50f));
		StudentGlobals.SetReputationTriangle(89, new Vector3(-10f, 30f, 100f));
		StudentGlobals.SetReputationTriangle(90, new Vector3(10f, 100f, 10f));
		StudentGlobals.SetReputationTriangle(91, new Vector3(0f, 50f, 100f));
		StudentGlobals.SetReputationTriangle(92, new Vector3(0f, 70f, 50f));
		StudentGlobals.SetReputationTriangle(93, new Vector3(0f, 100f, 50f));
		StudentGlobals.SetReputationTriangle(94, new Vector3(0f, 70f, 100f));
		StudentGlobals.SetReputationTriangle(95, new Vector3(0f, 50f, 70f));
		StudentGlobals.SetReputationTriangle(96, new Vector3(0f, 100f, 50f));
		StudentGlobals.SetReputationTriangle(97, new Vector3(50f, 100f, 30f));
		StudentGlobals.SetReputationTriangle(98, new Vector3(0f, 100f, 100f));
		StudentGlobals.SetReputationTriangle(99, new Vector3(-50f, 50f, 100f));
		StudentGlobals.SetReputationTriangle(99, new Vector3(-100f, -100f, 100f));
		this.ID = 2;
		while (this.ID < 101)
		{
			Vector3 reputationTriangle = StudentGlobals.GetReputationTriangle(this.ID);
			reputationTriangle.x *= 0.33333f;
			reputationTriangle.y *= 0.33333f;
			reputationTriangle.z *= 0.33333f;
			StudentGlobals.SetStudentReputation(this.ID, Mathf.RoundToInt(reputationTriangle.x + reputationTriangle.y + reputationTriangle.z));
			this.ID++;
		}
	}

	// Token: 0x06001B82 RID: 7042 RVA: 0x0011D2B8 File Offset: 0x0011B4B8
	public void GracePeriod(float Length)
	{
		this.ID = 1;
		while (this.ID < this.Students.Length)
		{
			StudentScript studentScript = this.Students[this.ID];
			if (studentScript != null)
			{
				studentScript.IgnoreTimer = Length;
			}
			this.ID++;
		}
	}

	// Token: 0x06001B83 RID: 7043 RVA: 0x0011D30C File Offset: 0x0011B50C
	public void OpenSomeDoors()
	{
		int openedDoors = this.OpenedDoors;
		while (this.OpenedDoors < openedDoors + 11)
		{
			if (this.OpenedDoors < this.Doors.Length && this.Doors[this.OpenedDoors] != null && this.Doors[this.OpenedDoors].enabled)
			{
				this.Doors[this.OpenedDoors].Open = true;
				this.Doors[this.OpenedDoors].OpenDoor();
			}
			this.OpenedDoors++;
		}
	}

	// Token: 0x06001B84 RID: 7044 RVA: 0x0011D39C File Offset: 0x0011B59C
	public void SnapSomeStudents()
	{
		int snappedStudents = this.SnappedStudents;
		while (this.SnappedStudents < snappedStudents + 10)
		{
			if (this.SnappedStudents < this.Students.Length)
			{
				StudentScript studentScript = this.Students[this.SnappedStudents];
				if (studentScript != null && studentScript.gameObject.activeInHierarchy && studentScript.Alive)
				{
					studentScript.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
					studentScript.CharacterAnimation[studentScript.SocialSitAnim].weight = 0f;
					studentScript.SnapStudent.Yandere = this.SnappedYandere;
					studentScript.SnapStudent.enabled = true;
					studentScript.SpeechLines.Stop();
					studentScript.enabled = false;
					studentScript.EmptyHands();
					if (studentScript.Shy)
					{
						studentScript.CharacterAnimation[studentScript.ShyAnim].weight = 0f;
					}
					if (studentScript.Club == ClubType.LightMusic)
					{
						studentScript.StopMusic();
					}
				}
			}
			this.SnappedStudents++;
		}
	}

	// Token: 0x06001B85 RID: 7045 RVA: 0x0011D4A8 File Offset: 0x0011B6A8
	public void DarkenAllStudents()
	{
		foreach (StudentScript studentScript in this.Students)
		{
			if (studentScript != null && studentScript.StudentID > 1)
			{
				studentScript.MyRenderer.materials[0].mainTexture = this.PureWhite;
				studentScript.MyRenderer.materials[1].mainTexture = this.PureWhite;
				studentScript.MyRenderer.materials[2].mainTexture = this.PureWhite;
				studentScript.MyRenderer.materials[0].color = new Color(1f, 1f, 1f, 1f);
				studentScript.MyRenderer.materials[1].color = new Color(1f, 1f, 1f, 1f);
				studentScript.MyRenderer.materials[2].color = new Color(1f, 1f, 1f, 1f);
				studentScript.Cosmetic.LeftEyeRenderer.material.mainTexture = this.PureWhite;
				studentScript.Cosmetic.RightEyeRenderer.material.mainTexture = this.PureWhite;
				studentScript.Cosmetic.HairRenderer.material.mainTexture = this.PureWhite;
				studentScript.Cosmetic.LeftEyeRenderer.material.color = new Color(1f, 1f, 1f, 1f);
				studentScript.Cosmetic.RightEyeRenderer.material.color = new Color(1f, 1f, 1f, 1f);
				studentScript.Cosmetic.HairRenderer.material.color = new Color(1f, 1f, 1f, 1f);
			}
		}
	}

	// Token: 0x06001B86 RID: 7046 RVA: 0x0011D690 File Offset: 0x0011B890
	public void LockDownOccultClub()
	{
		for (int i = 31; i < 36; i++)
		{
			this.Patrols.List[i].GetChild(1).position = this.Patrols.List[i].GetChild(0).position;
			this.Patrols.List[i].GetChild(2).position = this.Patrols.List[i].GetChild(0).position;
			this.Patrols.List[i].GetChild(3).position = this.Patrols.List[i].GetChild(0).position;
			this.Patrols.List[i].GetChild(4).position = this.Patrols.List[i].GetChild(0).position;
			this.Patrols.List[i].GetChild(5).position = this.Patrols.List[i].GetChild(0).position;
		}
		for (int j = 81; j < 86; j++)
		{
			this.Patrols.List[j].GetChild(0).position = this.BullySnapPosition[j].position;
			this.Patrols.List[j].GetChild(1).position = this.BullySnapPosition[j].position;
			this.Patrols.List[j].GetChild(2).position = this.BullySnapPosition[j].position;
			this.Patrols.List[j].GetChild(3).position = this.BullySnapPosition[j].position;
		}
	}

	// Token: 0x06001B87 RID: 7047 RVA: 0x0011D84C File Offset: 0x0011BA4C
	public void SetWindowsOpaque()
	{
		this.WindowOccluder.open = !this.WindowOccluder.open;
		if (this.WindowOccluder.open)
		{
			this.Window.sharedMaterial.color = new Color(1f, 1f, 1f, 0.5f);
			this.Window.sharedMaterial.shader = Shader.Find("Transparent/Diffuse");
			return;
		}
		this.Window.sharedMaterial.color = new Color(1f, 1f, 1f, 1f);
		this.Window.sharedMaterial.shader = Shader.Find("Diffuse");
	}

	// Token: 0x06001B88 RID: 7048 RVA: 0x0011D908 File Offset: 0x0011BB08
	public void LateUpdate()
	{
		if (this.OpaqueWindows)
		{
			if (this.Yandere.transform.position.y > 0.1f && this.Yandere.transform.position.y < 11f)
			{
				if (this.WindowOccluder.open)
				{
					this.SetWindowsOpaque();
					return;
				}
			}
			else if (!this.WindowOccluder.open)
			{
				this.SetWindowsOpaque();
			}
		}
	}

	// Token: 0x06001B89 RID: 7049 RVA: 0x0011D97C File Offset: 0x0011BB7C
	public void UpdateSkirts(bool Status)
	{
		foreach (StudentScript studentScript in this.Students)
		{
			if (studentScript != null)
			{
				if (!studentScript.Male && !studentScript.Teacher && studentScript.Schoolwear == 1)
				{
					studentScript.SkirtCollider.gameObject.SetActive(Status);
				}
				studentScript.RightHandCollider.enabled = Status;
				studentScript.LeftHandCollider.enabled = Status;
			}
		}
	}

	// Token: 0x06001B8A RID: 7050 RVA: 0x0011D9F0 File Offset: 0x0011BBF0
	public void UpdatePanties(bool Status)
	{
		foreach (StudentScript studentScript in this.Students)
		{
			if (studentScript != null)
			{
				if (!studentScript.Male && !studentScript.Teacher && studentScript.Schoolwear == 1)
				{
					studentScript.PantyCollider.gameObject.SetActive(Status);
				}
				studentScript.NotFaceCollider.enabled = Status;
				studentScript.FaceCollider.enabled = Status;
			}
		}
	}

	// Token: 0x04002CF1 RID: 11505
	private PortraitChanScript NewPortraitChan;

	// Token: 0x04002CF2 RID: 11506
	private GameObject NewStudent;

	// Token: 0x04002CF3 RID: 11507
	public StudentScript[] Students;

	// Token: 0x04002CF4 RID: 11508
	public SelectiveGrayscale SmartphoneSelectiveGreyscale;

	// Token: 0x04002CF5 RID: 11509
	public PickpocketMinigameScript PickpocketMinigame;

	// Token: 0x04002CF6 RID: 11510
	public PopulationManagerScript PopulationManager;

	// Token: 0x04002CF7 RID: 11511
	public SelectiveGrayscale HandSelectiveGreyscale;

	// Token: 0x04002CF8 RID: 11512
	public SkinnedMeshRenderer FemaleShowerCurtain;

	// Token: 0x04002CF9 RID: 11513
	public CleaningManagerScript CleaningManager;

	// Token: 0x04002CFA RID: 11514
	public StolenPhoneSpotScript StolenPhoneSpot;

	// Token: 0x04002CFB RID: 11515
	public SelectiveGrayscale SelectiveGreyscale;

	// Token: 0x04002CFC RID: 11516
	public CombatMinigameScript CombatMinigame;

	// Token: 0x04002CFD RID: 11517
	public DatingMinigameScript DatingMinigame;

	// Token: 0x04002CFE RID: 11518
	public SnappedYandereScript SnappedYandere;

	// Token: 0x04002CFF RID: 11519
	public TextureManagerScript TextureManager;

	// Token: 0x04002D00 RID: 11520
	public TutorialWindowScript TutorialWindow;

	// Token: 0x04002D01 RID: 11521
	public QualityManagerScript QualityManager;

	// Token: 0x04002D02 RID: 11522
	public ComputerGamesScript ComputerGames;

	// Token: 0x04002D03 RID: 11523
	public EmergencyExitScript EmergencyExit;

	// Token: 0x04002D04 RID: 11524
	public MemorialSceneScript MemorialScene;

	// Token: 0x04002D05 RID: 11525
	public TranqDetectorScript TranqDetector;

	// Token: 0x04002D06 RID: 11526
	public WitnessCameraScript WitnessCamera;

	// Token: 0x04002D07 RID: 11527
	public ConvoManagerScript ConvoManager;

	// Token: 0x04002D08 RID: 11528
	public TallLockerScript CommunalLocker;

	// Token: 0x04002D09 RID: 11529
	public BloodParentScript BloodParent;

	// Token: 0x04002D0A RID: 11530
	public CabinetDoorScript CabinetDoor;

	// Token: 0x04002D0B RID: 11531
	public ClubManagerScript ClubManager;

	// Token: 0x04002D0C RID: 11532
	public LightSwitchScript LightSwitch;

	// Token: 0x04002D0D RID: 11533
	public LoveManagerScript LoveManager;

	// Token: 0x04002D0E RID: 11534
	public MiyukiEnemyScript MiyukiEnemy;

	// Token: 0x04002D0F RID: 11535
	public TaskManagerScript TaskManager;

	// Token: 0x04002D10 RID: 11536
	public Collider MaleLockerRoomArea;

	// Token: 0x04002D11 RID: 11537
	public StudentScript BloodReporter;

	// Token: 0x04002D12 RID: 11538
	public HeadmasterScript Headmaster;

	// Token: 0x04002D13 RID: 11539
	public NoteWindowScript NoteWindow;

	// Token: 0x04002D14 RID: 11540
	public ReputationScript Reputation;

	// Token: 0x04002D15 RID: 11541
	public WeaponScript FragileWeapon;

	// Token: 0x04002D16 RID: 11542
	public AudioSource PracticeVocals;

	// Token: 0x04002D17 RID: 11543
	public AudioSource PracticeMusic;

	// Token: 0x04002D18 RID: 11544
	public ContainerScript Container;

	// Token: 0x04002D19 RID: 11545
	public RedStringScript RedString;

	// Token: 0x04002D1A RID: 11546
	public RingEventScript RingEvent;

	// Token: 0x04002D1B RID: 11547
	public RivalPoseScript RivalPose;

	// Token: 0x04002D1C RID: 11548
	public GazerEyesScript Shinigami;

	// Token: 0x04002D1D RID: 11549
	public HologramScript Holograms;

	// Token: 0x04002D1E RID: 11550
	public RobotArmScript RobotArms;

	// Token: 0x04002D1F RID: 11551
	public AlphabetScript Alphabet;

	// Token: 0x04002D20 RID: 11552
	public PickUpScript Flashlight;

	// Token: 0x04002D21 RID: 11553
	public FountainScript Fountain;

	// Token: 0x04002D22 RID: 11554
	public PoseModeScript PoseMode;

	// Token: 0x04002D23 RID: 11555
	public TrashCanScript TrashCan;

	// Token: 0x04002D24 RID: 11556
	public Collider LockerRoomArea;

	// Token: 0x04002D25 RID: 11557
	public StudentScript Reporter;

	// Token: 0x04002D26 RID: 11558
	public DoorScript GamingDoor;

	// Token: 0x04002D27 RID: 11559
	public GhostScript GhostChan;

	// Token: 0x04002D28 RID: 11560
	public YandereScript Yandere;

	// Token: 0x04002D29 RID: 11561
	public ListScript MeetSpots;

	// Token: 0x04002D2A RID: 11562
	public PoliceScript Police;

	// Token: 0x04002D2B RID: 11563
	public DoorScript ShedDoor;

	// Token: 0x04002D2C RID: 11564
	public UILabel ErrorLabel;

	// Token: 0x04002D2D RID: 11565
	public RestScript Rest;

	// Token: 0x04002D2E RID: 11566
	public TagScript Tag;

	// Token: 0x04002D2F RID: 11567
	public Collider EastBathroomArea;

	// Token: 0x04002D30 RID: 11568
	public Collider WestBathroomArea;

	// Token: 0x04002D31 RID: 11569
	public Collider IncineratorArea;

	// Token: 0x04002D32 RID: 11570
	public Collider HeadmasterArea;

	// Token: 0x04002D33 RID: 11571
	public Collider NEStairs;

	// Token: 0x04002D34 RID: 11572
	public Collider NWStairs;

	// Token: 0x04002D35 RID: 11573
	public Collider SEStairs;

	// Token: 0x04002D36 RID: 11574
	public Collider SWStairs;

	// Token: 0x04002D37 RID: 11575
	public DoorScript AltFemaleVomitDoor;

	// Token: 0x04002D38 RID: 11576
	public DoorScript FemaleVomitDoor;

	// Token: 0x04002D39 RID: 11577
	public CounselorDoorScript[] CounselorDoor;

	// Token: 0x04002D3A RID: 11578
	public ParticleSystem AltFemaleDrownSplashes;

	// Token: 0x04002D3B RID: 11579
	public ParticleSystem FemaleDrownSplashes;

	// Token: 0x04002D3C RID: 11580
	public OfferHelpScript FragileOfferHelp;

	// Token: 0x04002D3D RID: 11581
	public OfferHelpScript OsanaOfferHelp;

	// Token: 0x04002D3E RID: 11582
	public OfferHelpScript OfferHelp;

	// Token: 0x04002D3F RID: 11583
	public Transform AltFemaleVomitSpot;

	// Token: 0x04002D40 RID: 11584
	public ListScript SearchPatrols;

	// Token: 0x04002D41 RID: 11585
	public ListScript CleaningSpots;

	// Token: 0x04002D42 RID: 11586
	public ListScript Patrols;

	// Token: 0x04002D43 RID: 11587
	public ClockScript Clock;

	// Token: 0x04002D44 RID: 11588
	public JsonScript JSON;

	// Token: 0x04002D45 RID: 11589
	public GateScript Gate;

	// Token: 0x04002D46 RID: 11590
	public ListScript EntranceVectors;

	// Token: 0x04002D47 RID: 11591
	public ListScript ShowerLockers;

	// Token: 0x04002D48 RID: 11592
	public ListScript GoAwaySpots;

	// Token: 0x04002D49 RID: 11593
	public ListScript HidingSpots;

	// Token: 0x04002D4A RID: 11594
	public ListScript LunchSpots;

	// Token: 0x04002D4B RID: 11595
	public ListScript Hangouts;

	// Token: 0x04002D4C RID: 11596
	public ListScript Lockers;

	// Token: 0x04002D4D RID: 11597
	public ListScript Podiums;

	// Token: 0x04002D4E RID: 11598
	public ListScript Clubs;

	// Token: 0x04002D4F RID: 11599
	public ChangingBoothScript[] ChangingBooths;

	// Token: 0x04002D50 RID: 11600
	public GradingPaperScript[] FacultyDesks;

	// Token: 0x04002D51 RID: 11601
	public GameObject[] ShrineCollectibles;

	// Token: 0x04002D52 RID: 11602
	public StudentScript[] WitnessList;

	// Token: 0x04002D53 RID: 11603
	public StudentScript[] Teachers;

	// Token: 0x04002D54 RID: 11604
	public GameObject[] Graffiti;

	// Token: 0x04002D55 RID: 11605
	public GameObject[] Canvas;

	// Token: 0x04002D56 RID: 11606
	public ListScript[] Seats;

	// Token: 0x04002D57 RID: 11607
	public Collider[] Blood;

	// Token: 0x04002D58 RID: 11608
	public Collider[] Limbs;

	// Token: 0x04002D59 RID: 11609
	public Transform[] TeacherGuardLocation;

	// Token: 0x04002D5A RID: 11610
	public Transform[] CorpseGuardLocation;

	// Token: 0x04002D5B RID: 11611
	public Transform[] BloodGuardLocation;

	// Token: 0x04002D5C RID: 11612
	public Transform[] SleuthDestinations;

	// Token: 0x04002D5D RID: 11613
	public Transform[] StrippingPositions;

	// Token: 0x04002D5E RID: 11614
	public Transform[] GardeningPatrols;

	// Token: 0x04002D5F RID: 11615
	public Transform[] MartialArtsSpots;

	// Token: 0x04002D60 RID: 11616
	public Transform[] LockerPositions;

	// Token: 0x04002D61 RID: 11617
	public Transform[] BackstageSpots;

	// Token: 0x04002D62 RID: 11618
	public Transform[] SpawnPositions;

	// Token: 0x04002D63 RID: 11619
	public Transform[] GraffitiSpots;

	// Token: 0x04002D64 RID: 11620
	public Transform[] PracticeSpots;

	// Token: 0x04002D65 RID: 11621
	public Transform[] SunbatheSpots;

	// Token: 0x04002D66 RID: 11622
	public Transform[] MeetingSpots;

	// Token: 0x04002D67 RID: 11623
	public Transform[] PinDownSpots;

	// Token: 0x04002D68 RID: 11624
	public Transform[] ShockedSpots;

	// Token: 0x04002D69 RID: 11625
	public Transform[] FridaySpots;

	// Token: 0x04002D6A RID: 11626
	public Transform[] MiyukiSpots;

	// Token: 0x04002D6B RID: 11627
	public Transform[] SocialSeats;

	// Token: 0x04002D6C RID: 11628
	public Transform[] SocialSpots;

	// Token: 0x04002D6D RID: 11629
	public Transform[] SupplySpots;

	// Token: 0x04002D6E RID: 11630
	public Transform[] BullySpots;

	// Token: 0x04002D6F RID: 11631
	public Transform[] DramaSpots;

	// Token: 0x04002D70 RID: 11632
	public Transform[] MournSpots;

	// Token: 0x04002D71 RID: 11633
	public Transform[] ClubZones;

	// Token: 0x04002D72 RID: 11634
	public Transform[] SulkSpots;

	// Token: 0x04002D73 RID: 11635
	public Transform[] FleeSpots;

	// Token: 0x04002D74 RID: 11636
	public Transform[] Uniforms;

	// Token: 0x04002D75 RID: 11637
	public Transform[] Plates;

	// Token: 0x04002D76 RID: 11638
	public Transform[] FemaleVomitSpots;

	// Token: 0x04002D77 RID: 11639
	public Transform[] MaleVomitSpots;

	// Token: 0x04002D78 RID: 11640
	public Transform[] FemaleWashSpots;

	// Token: 0x04002D79 RID: 11641
	public Transform[] MaleWashSpots;

	// Token: 0x04002D7A RID: 11642
	public DoorScript[] FemaleToiletDoors;

	// Token: 0x04002D7B RID: 11643
	public DoorScript[] MaleToiletDoors;

	// Token: 0x04002D7C RID: 11644
	public DrinkingFountainScript[] DrinkingFountains;

	// Token: 0x04002D7D RID: 11645
	public Renderer[] FridayPaintings;

	// Token: 0x04002D7E RID: 11646
	public bool[] SeatsTaken11;

	// Token: 0x04002D7F RID: 11647
	public bool[] SeatsTaken12;

	// Token: 0x04002D80 RID: 11648
	public bool[] SeatsTaken21;

	// Token: 0x04002D81 RID: 11649
	public bool[] SeatsTaken22;

	// Token: 0x04002D82 RID: 11650
	public bool[] SeatsTaken31;

	// Token: 0x04002D83 RID: 11651
	public bool[] SeatsTaken32;

	// Token: 0x04002D84 RID: 11652
	public bool[] NoBully;

	// Token: 0x04002D85 RID: 11653
	public Quaternion[] OriginalClubRotations;

	// Token: 0x04002D86 RID: 11654
	public Vector3[] OriginalClubPositions;

	// Token: 0x04002D87 RID: 11655
	public Collider RivalDeskCollider;

	// Token: 0x04002D88 RID: 11656
	public Transform FollowerLookAtTarget;

	// Token: 0x04002D89 RID: 11657
	public Transform SuitorConfessionSpot;

	// Token: 0x04002D8A RID: 11658
	public Transform RivalConfessionSpot;

	// Token: 0x04002D8B RID: 11659
	public Transform OriginalLyricsSpot;

	// Token: 0x04002D8C RID: 11660
	public Transform FragileSlaveSpot;

	// Token: 0x04002D8D RID: 11661
	public Transform FemaleCoupleSpot;

	// Token: 0x04002D8E RID: 11662
	public Transform YandereStripSpot;

	// Token: 0x04002D8F RID: 11663
	public Transform FemaleBatheSpot;

	// Token: 0x04002D90 RID: 11664
	public Transform FemaleStalkSpot;

	// Token: 0x04002D91 RID: 11665
	public Transform FemaleStripSpot;

	// Token: 0x04002D92 RID: 11666
	public Transform FemaleVomitSpot;

	// Token: 0x04002D93 RID: 11667
	public Transform MedicineCabinet;

	// Token: 0x04002D94 RID: 11668
	public Transform ConfessionSpot;

	// Token: 0x04002D95 RID: 11669
	public Transform CorpseLocation;

	// Token: 0x04002D96 RID: 11670
	public Transform FemaleRestSpot;

	// Token: 0x04002D97 RID: 11671
	public Transform FemaleWashSpot;

	// Token: 0x04002D98 RID: 11672
	public Transform MaleCoupleSpot;

	// Token: 0x04002D99 RID: 11673
	public Transform AirGuitarSpot;

	// Token: 0x04002D9A RID: 11674
	public Transform BloodLocation;

	// Token: 0x04002D9B RID: 11675
	public Transform FastBatheSpot;

	// Token: 0x04002D9C RID: 11676
	public Transform InfirmarySeat;

	// Token: 0x04002D9D RID: 11677
	public Transform MaleBatheSpot;

	// Token: 0x04002D9E RID: 11678
	public Transform MaleStalkSpot;

	// Token: 0x04002D9F RID: 11679
	public Transform MaleStripSpot;

	// Token: 0x04002DA0 RID: 11680
	public Transform MaleVomitSpot;

	// Token: 0x04002DA1 RID: 11681
	public Transform SacrificeSpot;

	// Token: 0x04002DA2 RID: 11682
	public Transform WeaponBoxSpot;

	// Token: 0x04002DA3 RID: 11683
	public Transform FountainSpot;

	// Token: 0x04002DA4 RID: 11684
	public Transform MaleWashSpot;

	// Token: 0x04002DA5 RID: 11685
	public Transform SenpaiLocker;

	// Token: 0x04002DA6 RID: 11686
	public Transform SuitorLocker;

	// Token: 0x04002DA7 RID: 11687
	public Transform MaleRestSpot;

	// Token: 0x04002DA8 RID: 11688
	public Transform RomanceSpot;

	// Token: 0x04002DA9 RID: 11689
	public Transform BrokenSpot;

	// Token: 0x04002DAA RID: 11690
	public Transform BullyGroup;

	// Token: 0x04002DAB RID: 11691
	public Transform EdgeOfGrid;

	// Token: 0x04002DAC RID: 11692
	public Transform GoAwaySpot;

	// Token: 0x04002DAD RID: 11693
	public Transform LyricsSpot;

	// Token: 0x04002DAE RID: 11694
	public Transform MainCamera;

	// Token: 0x04002DAF RID: 11695
	public Transform SuitorSpot;

	// Token: 0x04002DB0 RID: 11696
	public Transform ToolTarget;

	// Token: 0x04002DB1 RID: 11697
	public Transform MiyukiCat;

	// Token: 0x04002DB2 RID: 11698
	public Transform ShameSpot;

	// Token: 0x04002DB3 RID: 11699
	public Transform SlaveSpot;

	// Token: 0x04002DB4 RID: 11700
	public Transform Papers;

	// Token: 0x04002DB5 RID: 11701
	public Transform Exit;

	// Token: 0x04002DB6 RID: 11702
	public GameObject LovestruckCamera;

	// Token: 0x04002DB7 RID: 11703
	public GameObject DelinquentRadio;

	// Token: 0x04002DB8 RID: 11704
	public GameObject GardenBlockade;

	// Token: 0x04002DB9 RID: 11705
	public GameObject PortraitChan;

	// Token: 0x04002DBA RID: 11706
	public GameObject RandomPatrol;

	// Token: 0x04002DBB RID: 11707
	public GameObject ChaseCamera;

	// Token: 0x04002DBC RID: 11708
	public GameObject EmptyObject;

	// Token: 0x04002DBD RID: 11709
	public GameObject PortraitKun;

	// Token: 0x04002DBE RID: 11710
	public GameObject StudentChan;

	// Token: 0x04002DBF RID: 11711
	public GameObject StudentKun;

	// Token: 0x04002DC0 RID: 11712
	public GameObject RivalChan;

	// Token: 0x04002DC1 RID: 11713
	public GameObject Canvases;

	// Token: 0x04002DC2 RID: 11714
	public GameObject Medicine;

	// Token: 0x04002DC3 RID: 11715
	public GameObject DrumSet;

	// Token: 0x04002DC4 RID: 11716
	public GameObject Flowers;

	// Token: 0x04002DC5 RID: 11717
	public GameObject Portal;

	// Token: 0x04002DC6 RID: 11718
	public GameObject Gift;

	// Token: 0x04002DC7 RID: 11719
	public float[] SpawnTimes;

	// Token: 0x04002DC8 RID: 11720
	public int LowDetailThreshold;

	// Token: 0x04002DC9 RID: 11721
	public int FarAnimThreshold;

	// Token: 0x04002DCA RID: 11722
	public int MartialArtsPhase;

	// Token: 0x04002DCB RID: 11723
	public int OriginalUniforms = 2;

	// Token: 0x04002DCC RID: 11724
	public int StudentsSpawned;

	// Token: 0x04002DCD RID: 11725
	public int SedatedStudents;

	// Token: 0x04002DCE RID: 11726
	public int StudentsTotal = 13;

	// Token: 0x04002DCF RID: 11727
	public int TeachersTotal = 6;

	// Token: 0x04002DD0 RID: 11728
	public int GirlsSpawned;

	// Token: 0x04002DD1 RID: 11729
	public int NewUniforms;

	// Token: 0x04002DD2 RID: 11730
	public int NPCsSpawned;

	// Token: 0x04002DD3 RID: 11731
	public int SleuthPhase = 1;

	// Token: 0x04002DD4 RID: 11732
	public int DramaPhase = 1;

	// Token: 0x04002DD5 RID: 11733
	public int NPCsTotal;

	// Token: 0x04002DD6 RID: 11734
	public int Witnesses;

	// Token: 0x04002DD7 RID: 11735
	public int PinPhase;

	// Token: 0x04002DD8 RID: 11736
	public int Bullies;

	// Token: 0x04002DD9 RID: 11737
	public int Speaker = 21;

	// Token: 0x04002DDA RID: 11738
	public int Frame;

	// Token: 0x04002DDB RID: 11739
	public int GymTeacherID = 100;

	// Token: 0x04002DDC RID: 11740
	public int ObstacleID = 6;

	// Token: 0x04002DDD RID: 11741
	public int CurrentID;

	// Token: 0x04002DDE RID: 11742
	public int SuitorID = 13;

	// Token: 0x04002DDF RID: 11743
	public int VictimID;

	// Token: 0x04002DE0 RID: 11744
	public int NurseID = 93;

	// Token: 0x04002DE1 RID: 11745
	public int RivalID = 7;

	// Token: 0x04002DE2 RID: 11746
	public int SpawnID;

	// Token: 0x04002DE3 RID: 11747
	public int ID;

	// Token: 0x04002DE4 RID: 11748
	public bool ReactedToGameLeader;

	// Token: 0x04002DE5 RID: 11749
	public bool MurderTakingPlace;

	// Token: 0x04002DE6 RID: 11750
	public bool ControllerShrink;

	// Token: 0x04002DE7 RID: 11751
	public bool DisableFarAnims;

	// Token: 0x04002DE8 RID: 11752
	public bool GameOverIminent;

	// Token: 0x04002DE9 RID: 11753
	public bool RivalEliminated;

	// Token: 0x04002DEA RID: 11754
	public bool TakingPortraits;

	// Token: 0x04002DEB RID: 11755
	public bool TeachersSpawned;

	// Token: 0x04002DEC RID: 11756
	public bool MetalDetectors;

	// Token: 0x04002DED RID: 11757
	public bool YandereVisible;

	// Token: 0x04002DEE RID: 11758
	public bool NoClubMeeting;

	// Token: 0x04002DEF RID: 11759
	public bool UpdatedBlood;

	// Token: 0x04002DF0 RID: 11760
	public bool YandereDying;

	// Token: 0x04002DF1 RID: 11761
	public bool FirstUpdate;

	// Token: 0x04002DF2 RID: 11762
	public bool MissionMode;

	// Token: 0x04002DF3 RID: 11763
	public bool OpenCurtain;

	// Token: 0x04002DF4 RID: 11764
	public bool PinningDown;

	// Token: 0x04002DF5 RID: 11765
	public bool RoofFenceUp;

	// Token: 0x04002DF6 RID: 11766
	public bool YandereLate;

	// Token: 0x04002DF7 RID: 11767
	public bool ForceSpawn;

	// Token: 0x04002DF8 RID: 11768
	public bool NoGravity;

	// Token: 0x04002DF9 RID: 11769
	public bool Randomize;

	// Token: 0x04002DFA RID: 11770
	public bool LoveSick;

	// Token: 0x04002DFB RID: 11771
	public bool NoSpeech;

	// Token: 0x04002DFC RID: 11772
	public bool Meeting;

	// Token: 0x04002DFD RID: 11773
	public bool Censor;

	// Token: 0x04002DFE RID: 11774
	public bool Spooky;

	// Token: 0x04002DFF RID: 11775
	public bool Bully;

	// Token: 0x04002E00 RID: 11776
	public bool Ebola;

	// Token: 0x04002E01 RID: 11777
	public bool Gaze;

	// Token: 0x04002E02 RID: 11778
	public bool Pose;

	// Token: 0x04002E03 RID: 11779
	public bool Sans;

	// Token: 0x04002E04 RID: 11780
	public bool Stop;

	// Token: 0x04002E05 RID: 11781
	public bool Egg;

	// Token: 0x04002E06 RID: 11782
	public bool Six;

	// Token: 0x04002E07 RID: 11783
	public bool AoT;

	// Token: 0x04002E08 RID: 11784
	public bool DK;

	// Token: 0x04002E09 RID: 11785
	public float Atmosphere;

	// Token: 0x04002E0A RID: 11786
	public float OpenValue = 100f;

	// Token: 0x04002E0B RID: 11787
	public float YandereHeight = 999f;

	// Token: 0x04002E0C RID: 11788
	public float MeetingTimer;

	// Token: 0x04002E0D RID: 11789
	public float PinDownTimer;

	// Token: 0x04002E0E RID: 11790
	public float ChangeTimer;

	// Token: 0x04002E0F RID: 11791
	public float SleuthTimer;

	// Token: 0x04002E10 RID: 11792
	public float DramaTimer;

	// Token: 0x04002E11 RID: 11793
	public float LowestRep;

	// Token: 0x04002E12 RID: 11794
	public float PinTimer;

	// Token: 0x04002E13 RID: 11795
	public float Timer;

	// Token: 0x04002E14 RID: 11796
	public string[] ColorNames;

	// Token: 0x04002E15 RID: 11797
	public string[] MaleNames;

	// Token: 0x04002E16 RID: 11798
	public string[] FirstNames;

	// Token: 0x04002E17 RID: 11799
	public string[] LastNames;

	// Token: 0x04002E18 RID: 11800
	public AudioSource[] FountainAudio;

	// Token: 0x04002E19 RID: 11801
	public AudioClip YanderePinDown;

	// Token: 0x04002E1A RID: 11802
	public AudioClip PinDownSFX;

	// Token: 0x04002E1B RID: 11803
	[SerializeField]
	private int ProblemID = -1;

	// Token: 0x04002E1C RID: 11804
	public GameObject Cardigan;

	// Token: 0x04002E1D RID: 11805
	public SkinnedMeshRenderer CardiganRenderer;

	// Token: 0x04002E1E RID: 11806
	public Mesh OpenChipBag;

	// Token: 0x04002E1F RID: 11807
	public Vignetting[] Vignettes;

	// Token: 0x04002E20 RID: 11808
	public Renderer[] Trees;

	// Token: 0x04002E21 RID: 11809
	public DoorScript[] AllDoors;

	// Token: 0x04002E22 RID: 11810
	public OcclusionPortal PlazaOccluder;

	// Token: 0x04002E23 RID: 11811
	public bool SeatOccupied;

	// Token: 0x04002E24 RID: 11812
	public int Class = 1;

	// Token: 0x04002E25 RID: 11813
	public int Thins;

	// Token: 0x04002E26 RID: 11814
	public int Seriouses;

	// Token: 0x04002E27 RID: 11815
	public int Rounds;

	// Token: 0x04002E28 RID: 11816
	public int Sads;

	// Token: 0x04002E29 RID: 11817
	public int Means;

	// Token: 0x04002E2A RID: 11818
	public int Smugs;

	// Token: 0x04002E2B RID: 11819
	public int Gentles;

	// Token: 0x04002E2C RID: 11820
	public int Rival1s;

	// Token: 0x04002E2D RID: 11821
	public DoorScript[] Doors;

	// Token: 0x04002E2E RID: 11822
	public int DoorID;

	// Token: 0x04002E2F RID: 11823
	private int OpenedDoors;

	// Token: 0x04002E30 RID: 11824
	private int SnappedStudents = 1;

	// Token: 0x04002E31 RID: 11825
	public Texture PureWhite;

	// Token: 0x04002E32 RID: 11826
	public Transform[] BullySnapPosition;

	// Token: 0x04002E33 RID: 11827
	public OcclusionPortal WindowOccluder;

	// Token: 0x04002E34 RID: 11828
	public bool OpaqueWindows;

	// Token: 0x04002E35 RID: 11829
	public Renderer Window;
}
