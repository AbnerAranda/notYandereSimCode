using System;
using UnityEngine;

// Token: 0x02000389 RID: 905
public class RagdollScript : MonoBehaviour
{
	// Token: 0x06001994 RID: 6548 RVA: 0x000F8BDC File Offset: 0x000F6DDC
	private void Start()
	{
		this.ElectrocutionAnimation = false;
		this.MurderSuicideAnimation = false;
		this.BurningAnimation = false;
		this.ChokingAnimation = false;
		this.Disturbing = false;
		Physics.IgnoreLayerCollision(11, 13, true);
		this.Zs.SetActive(this.Tranquil);
		if (!this.Tranquil && !this.Poisoned && !this.Drowned && !this.Electrocuted && !this.Burning && !this.NeckSnapped)
		{
			this.Student.StudentManager.TutorialWindow.ShowPoolMessage = true;
			this.BloodPoolSpawner.gameObject.SetActive(true);
			if (this.Pushed)
			{
				this.BloodPoolSpawner.Timer = 5f;
			}
		}
		for (int i = 0; i < this.AllRigidbodies.Length; i++)
		{
			this.AllRigidbodies[i].isKinematic = false;
			this.AllColliders[i].enabled = true;
			if (this.Yandere.StudentManager.NoGravity)
			{
				this.AllRigidbodies[i].useGravity = false;
			}
		}
		this.Prompt.enabled = true;
		if (ClassGlobals.PhysicalGrade + ClassGlobals.PhysicalBonus > 0 && !this.Tranquil)
		{
			this.Prompt.HideButton[3] = false;
		}
		if (this.Student.Yandere.BlackHole)
		{
			this.DisableRigidbodies();
		}
	}

	// Token: 0x06001995 RID: 6549 RVA: 0x000F8D30 File Offset: 0x000F6F30
	private void Update()
	{
		if (this.UpdateNextFrame)
		{
			this.Student.Hips.localPosition = this.NextPosition;
			this.Student.Hips.localRotation = this.NextRotation;
			Physics.SyncTransforms();
			this.UpdateNextFrame = false;
		}
		if (!this.Dragged && !this.Carried && !this.Settled && !this.Yandere.PK && !this.Yandere.StudentManager.NoGravity)
		{
			this.SettleTimer += Time.deltaTime;
			if (this.SettleTimer > 5f)
			{
				this.Settled = true;
				for (int i = 0; i < this.AllRigidbodies.Length; i++)
				{
					this.AllRigidbodies[i].isKinematic = true;
					this.AllColliders[i].enabled = false;
				}
			}
		}
		if (this.DetectionMarker != null)
		{
			if (this.DetectionMarker.Tex.color.a > 0.1f)
			{
				this.DetectionMarker.Tex.color = new Color(this.DetectionMarker.Tex.color.r, this.DetectionMarker.Tex.color.g, this.DetectionMarker.Tex.color.b, Mathf.MoveTowards(this.DetectionMarker.Tex.color.a, 0f, Time.deltaTime * 10f));
			}
			else
			{
				this.DetectionMarker.Tex.color = new Color(this.DetectionMarker.Tex.color.r, this.DetectionMarker.Tex.color.g, this.DetectionMarker.Tex.color.b, 0f);
				this.DetectionMarker = null;
			}
		}
		if (!this.Dumped)
		{
			if (this.StopAnimation && this.Student.CharacterAnimation.isPlaying)
			{
				this.Student.CharacterAnimation.Stop();
			}
			if (this.BloodPoolSpawner != null && this.BloodPoolSpawner.gameObject.activeInHierarchy && !this.Cauterized)
			{
				if (this.Yandere.PickUp != null)
				{
					if (this.Yandere.PickUp.Blowtorch)
					{
						if (!this.Cauterizable)
						{
							this.Prompt.Label[0].text = "     Cauterize";
							this.Cauterizable = true;
						}
					}
					else if (this.Cauterizable)
					{
						this.Prompt.Label[0].text = "     Dismember";
						this.Cauterizable = false;
					}
				}
				else if (this.Cauterizable)
				{
					this.Prompt.Label[0].text = "     Dismember";
					this.Cauterizable = false;
				}
			}
			if (this.Prompt.Circle[0].fillAmount == 0f)
			{
				this.Prompt.Circle[0].fillAmount = 1f;
				if (!this.Yandere.Chased && this.Yandere.Chasers == 0)
				{
					if (this.Cauterizable)
					{
						this.Prompt.Label[0].text = "     Dismember";
						this.BloodPoolSpawner.enabled = false;
						this.Cauterizable = false;
						this.Cauterized = true;
						this.Yandere.CharacterAnimation.CrossFade("f02_cauterize_00");
						this.Yandere.Cauterizing = true;
						this.Yandere.CanMove = false;
						this.Yandere.PickUp.GetComponent<BlowtorchScript>().enabled = true;
						this.Yandere.PickUp.GetComponent<AudioSource>().Play();
					}
					else if (this.Yandere.StudentManager.OriginalUniforms + this.Yandere.StudentManager.NewUniforms > 1)
					{
						this.Yandere.CharacterAnimation.CrossFade("f02_dismember_00");
						this.Yandere.transform.LookAt(base.transform);
						this.Yandere.RPGCamera.transform.position = this.Yandere.DismemberSpot.position;
						this.Yandere.RPGCamera.transform.eulerAngles = this.Yandere.DismemberSpot.eulerAngles;
						this.Yandere.EquippedWeapon.Dismember();
						this.Yandere.RPGCamera.enabled = false;
						this.Yandere.TargetStudent = this.Student;
						this.Yandere.Ragdoll = base.gameObject;
						this.Yandere.Dismembering = true;
						this.Yandere.CanMove = false;
					}
					else if (!this.Yandere.ClothingWarning)
					{
						this.Yandere.NotificationManager.DisplayNotification(NotificationType.Clothing);
						this.Yandere.StudentManager.TutorialWindow.ShowClothingMessage = true;
						this.Yandere.ClothingWarning = true;
					}
				}
			}
			if (this.Prompt.Circle[1].fillAmount == 0f)
			{
				this.Prompt.Circle[1].fillAmount = 1f;
				if (!this.Student.FireEmitters[1].isPlaying)
				{
					if (!this.Dragged)
					{
						this.Yandere.EmptyHands();
						this.Prompt.AcceptingInput[1] = false;
						this.Prompt.Label[1].text = "     Drop";
						this.PickNearestLimb();
						this.Yandere.RagdollDragger.connectedBody = this.Rigidbodies[this.LimbID];
						this.Yandere.RagdollDragger.connectedAnchor = this.LimbAnchor[this.LimbID];
						this.Yandere.Dragging = true;
						this.Yandere.Running = false;
						this.Yandere.DragState = 0;
						this.Yandere.Ragdoll = base.gameObject;
						this.Dragged = true;
						this.Yandere.StudentManager.UpdateStudents(0);
						if (this.MurderSuicide)
						{
							this.Police.MurderSuicideScene = false;
							this.MurderSuicide = false;
						}
						if (this.Suicide)
						{
							this.Police.Suicide = false;
							this.Suicide = false;
						}
						for (int j = 0; j < this.Student.Ragdoll.AllRigidbodies.Length; j++)
						{
							this.Student.Ragdoll.AllRigidbodies[j].drag = 2f;
						}
						for (int k = 0; k < this.AllRigidbodies.Length; k++)
						{
							this.AllRigidbodies[k].isKinematic = false;
							this.AllColliders[k].enabled = true;
							if (this.Yandere.StudentManager.NoGravity)
							{
								this.AllRigidbodies[k].useGravity = false;
							}
						}
					}
					else
					{
						this.StopDragging();
					}
				}
			}
			if (this.Prompt.Circle[3].fillAmount == 0f)
			{
				this.Prompt.Circle[3].fillAmount = 1f;
				if (!this.Student.FireEmitters[1].isPlaying)
				{
					this.Yandere.EmptyHands();
					this.Prompt.Label[1].text = "     Drop";
					this.Prompt.HideButton[1] = true;
					this.Prompt.HideButton[3] = true;
					this.Prompt.enabled = false;
					this.Prompt.Hide();
					for (int l = 0; l < this.AllRigidbodies.Length; l++)
					{
						this.AllRigidbodies[l].isKinematic = true;
						this.AllColliders[l].enabled = false;
					}
					if (this.Male)
					{
						Rigidbody rigidbody = this.AllRigidbodies[0];
						rigidbody.transform.parent.transform.localPosition = new Vector3(rigidbody.transform.parent.transform.localPosition.x, 0.2f, rigidbody.transform.parent.transform.localPosition.z);
					}
					this.Yandere.CharacterAnimation.Play("f02_carryLiftA_00");
					this.Student.CharacterAnimation.enabled = true;
					this.Student.CharacterAnimation.Play(this.LiftAnim);
					this.BloodSpawnerCollider.enabled = false;
					this.PelvisRoot.localEulerAngles = new Vector3(this.PelvisRoot.localEulerAngles.x, 0f, this.PelvisRoot.localEulerAngles.z);
					this.Prompt.MyCollider.enabled = false;
					this.PelvisRoot.localPosition = new Vector3(this.PelvisRoot.localPosition.x, this.PelvisRoot.localPosition.y, 0f);
					this.Yandere.Ragdoll = base.gameObject;
					this.Yandere.CurrentRagdoll = this;
					this.Yandere.CanMove = false;
					this.Yandere.Lifting = true;
					this.StopAnimation = false;
					this.Carried = true;
					this.Falling = false;
					this.FallTimer = 0f;
				}
			}
			if (this.Yandere.Running && this.Yandere.CanMove && this.Dragged)
			{
				this.StopDragging();
			}
			if (Vector3.Distance(this.Yandere.transform.position, this.Prompt.transform.position) < 2f)
			{
				if (!this.Suicide && !this.AddingToCount)
				{
					this.Yandere.NearestCorpseID = this.StudentID;
					this.Yandere.NearBodies++;
					this.AddingToCount = true;
				}
			}
			else if (this.AddingToCount)
			{
				this.Yandere.NearBodies--;
				this.AddingToCount = false;
			}
			if (!this.Prompt.AcceptingInput[1] && Input.GetButtonUp("B"))
			{
				this.Prompt.AcceptingInput[1] = true;
			}
			bool flag = false;
			if (this.Yandere.Armed && this.Yandere.EquippedWeapon.WeaponID == 7 && !this.Student.Nemesis)
			{
				flag = true;
			}
			if (!this.Cauterized && this.Yandere.PickUp != null && this.Yandere.PickUp.Blowtorch && this.BloodPoolSpawner.gameObject.activeInHierarchy)
			{
				flag = true;
			}
			this.Prompt.HideButton[0] = (this.Dragged || this.Carried || this.Tranquil || !flag);
		}
		else if (this.DumpType == RagdollDumpType.Incinerator)
		{
			if (this.DumpTimer == 0f && this.Yandere.Carrying)
			{
				this.Student.CharacterAnimation[this.DumpedAnim].time = 2.5f;
			}
			this.Student.CharacterAnimation.CrossFade(this.DumpedAnim);
			this.DumpTimer += Time.deltaTime;
			if (this.Student.CharacterAnimation[this.DumpedAnim].time >= this.Student.CharacterAnimation[this.DumpedAnim].length)
			{
				this.Incinerator.Corpses++;
				this.Incinerator.CorpseList[this.Incinerator.Corpses] = this.StudentID;
				this.Remove();
				base.enabled = false;
			}
		}
		else if (this.DumpType == RagdollDumpType.TranqCase)
		{
			if (this.DumpTimer == 0f && this.Yandere.Carrying)
			{
				this.Student.CharacterAnimation[this.DumpedAnim].time = 2.5f;
			}
			this.Student.CharacterAnimation.CrossFade(this.DumpedAnim);
			this.DumpTimer += Time.deltaTime;
			if (this.Student.CharacterAnimation[this.DumpedAnim].time >= this.Student.CharacterAnimation[this.DumpedAnim].length)
			{
				this.TranqCase.Open = false;
				if (this.AddingToCount)
				{
					this.Yandere.NearBodies--;
				}
				base.enabled = false;
			}
		}
		else if (this.DumpType == RagdollDumpType.WoodChipper)
		{
			if (this.DumpTimer == 0f && this.Yandere.Carrying)
			{
				this.Student.CharacterAnimation[this.DumpedAnim].time = 2.5f;
			}
			this.Student.CharacterAnimation.CrossFade(this.DumpedAnim);
			this.DumpTimer += Time.deltaTime;
			if (this.Student.CharacterAnimation[this.DumpedAnim].time >= this.Student.CharacterAnimation[this.DumpedAnim].length)
			{
				this.WoodChipper.VictimID = this.StudentID;
				this.Remove();
				base.enabled = false;
			}
		}
		if (this.Hidden && this.HideCollider == null)
		{
			this.Police.HiddenCorpses--;
			this.Hidden = false;
		}
		if (this.Falling)
		{
			this.FallTimer += Time.deltaTime;
			if (this.FallTimer > 2f)
			{
				this.BloodSpawnerCollider.enabled = true;
				this.FallTimer = 0f;
				this.Falling = false;
			}
		}
		if (this.Burning)
		{
			for (int m = 0; m < 3; m++)
			{
				Material material = this.MyRenderer.materials[m];
				material.color = Vector4.MoveTowards(material.color, new Vector4(0.1f, 0.1f, 0.1f, 1f), Time.deltaTime * 0.1f);
			}
			this.Student.Cosmetic.HairRenderer.material.color = Vector4.MoveTowards(this.Student.Cosmetic.HairRenderer.material.color, new Vector4(0.1f, 0.1f, 0.1f, 1f), Time.deltaTime * 0.1f);
			if (this.MyRenderer.materials[0].color == new Color(0.1f, 0.1f, 0.1f, 1f))
			{
				this.Burning = false;
				this.Burned = true;
			}
		}
		if (this.Burned)
		{
			this.Sacrifice = (Vector3.Distance(this.Prompt.transform.position, this.Yandere.StudentManager.SacrificeSpot.position) < 1.5f);
		}
	}

	// Token: 0x06001996 RID: 6550 RVA: 0x000F9C44 File Offset: 0x000F7E44
	private void LateUpdate()
	{
		if (!this.Male)
		{
			if (this.LeftEye != null)
			{
				this.LeftEye.localPosition = new Vector3(this.LeftEye.localPosition.x, this.LeftEye.localPosition.y, this.LeftEyeOrigin.z - this.EyeShrink * 0.01f);
				this.RightEye.localPosition = new Vector3(this.RightEye.localPosition.x, this.RightEye.localPosition.y, this.RightEyeOrigin.z + this.EyeShrink * 0.01f);
				this.LeftEye.localScale = new Vector3(1f - this.EyeShrink * 0.5f, 1f - this.EyeShrink * 0.5f, this.LeftEye.localScale.z);
				this.RightEye.localScale = new Vector3(1f - this.EyeShrink * 0.5f, 1f - this.EyeShrink * 0.5f, this.RightEye.localScale.z);
			}
			if (this.StudentID == 81)
			{
				for (int i = 0; i < 4; i++)
				{
					Transform transform = this.Student.Skirt[i];
					transform.transform.localScale = new Vector3(transform.transform.localScale.x, 0.6666667f, transform.transform.localScale.z);
				}
			}
		}
		if (this.Decapitated)
		{
			this.Head.localScale = Vector3.zero;
		}
		if (this.Yandere.Ragdoll == base.gameObject)
		{
			if (this.Yandere.DumpTimer < 1f)
			{
				if (this.Yandere.Lifting)
				{
					base.transform.position = this.Yandere.transform.position;
					base.transform.eulerAngles = this.Yandere.transform.eulerAngles;
				}
				else if (this.Carried)
				{
					base.transform.position = this.Yandere.transform.position;
					base.transform.eulerAngles = this.Yandere.transform.eulerAngles;
					float axis = Input.GetAxis("Vertical");
					float axis2 = Input.GetAxis("Horizontal");
					if (axis != 0f || axis2 != 0f)
					{
						this.Student.CharacterAnimation.CrossFade(this.Yandere.Running ? this.RunAnim : this.WalkAnim);
					}
					else
					{
						this.Student.CharacterAnimation.CrossFade(this.IdleAnim);
					}
					this.Student.CharacterAnimation[this.IdleAnim].time = this.Yandere.CharacterAnimation["f02_carryIdleA_00"].time;
					this.Student.CharacterAnimation[this.WalkAnim].time = this.Yandere.CharacterAnimation["f02_carryWalkA_00"].time;
					this.Student.CharacterAnimation[this.RunAnim].time = this.Yandere.CharacterAnimation["f02_carryRunA_00"].time;
				}
			}
			if (this.Carried)
			{
				if (this.Male)
				{
					Rigidbody rigidbody = this.AllRigidbodies[0];
					rigidbody.transform.parent.transform.localPosition = new Vector3(rigidbody.transform.parent.transform.localPosition.x, 0.2f, rigidbody.transform.parent.transform.localPosition.z);
				}
				if (this.Yandere.Struggling || this.Yandere.DelinquentFighting || this.Yandere.Sprayed)
				{
					this.Fall();
				}
			}
		}
	}

	// Token: 0x06001997 RID: 6551 RVA: 0x000FA054 File Offset: 0x000F8254
	public void StopDragging()
	{
		Rigidbody[] allRigidbodies = this.Student.Ragdoll.AllRigidbodies;
		for (int i = 0; i < allRigidbodies.Length; i++)
		{
			allRigidbodies[i].drag = 0f;
		}
		if (ClassGlobals.PhysicalGrade + ClassGlobals.PhysicalBonus > 0 && !this.Tranquil)
		{
			this.Prompt.HideButton[3] = false;
		}
		this.Prompt.AcceptingInput[1] = true;
		this.Prompt.Circle[1].fillAmount = 1f;
		this.Prompt.Label[1].text = "     Drag";
		this.Yandere.RagdollDragger.connectedBody = null;
		this.Yandere.RagdollPK.connectedBody = null;
		this.Yandere.Dragging = false;
		this.Yandere.Ragdoll = null;
		this.Yandere.StudentManager.UpdateStudents(0);
		this.SettleTimer = 0f;
		this.Settled = false;
		this.Dragged = false;
	}

	// Token: 0x06001998 RID: 6552 RVA: 0x000FA154 File Offset: 0x000F8354
	private void PickNearestLimb()
	{
		this.NearestLimb = this.Limb[0];
		this.LimbID = 0;
		for (int i = 1; i < 4; i++)
		{
			Transform transform = this.Limb[i];
			if (Vector3.Distance(transform.position, this.Yandere.transform.position) < Vector3.Distance(this.NearestLimb.position, this.Yandere.transform.position))
			{
				this.NearestLimb = transform;
				this.LimbID = i;
			}
		}
	}

	// Token: 0x06001999 RID: 6553 RVA: 0x000FA1D8 File Offset: 0x000F83D8
	public void Dump()
	{
		if (this.DumpType == RagdollDumpType.Incinerator)
		{
			base.transform.eulerAngles = this.Yandere.transform.eulerAngles;
			base.transform.position = this.Yandere.transform.position;
			this.Incinerator = this.Yandere.Incinerator;
			this.BloodPoolSpawner.enabled = false;
		}
		else if (this.DumpType == RagdollDumpType.TranqCase)
		{
			this.TranqCase = this.Yandere.TranqCase;
		}
		else if (this.DumpType == RagdollDumpType.WoodChipper)
		{
			this.WoodChipper = this.Yandere.WoodChipper;
		}
		this.Prompt.Hide();
		this.Prompt.enabled = false;
		this.Dumped = true;
		Rigidbody[] allRigidbodies = this.AllRigidbodies;
		for (int i = 0; i < allRigidbodies.Length; i++)
		{
			allRigidbodies[i].isKinematic = true;
		}
	}

	// Token: 0x0600199A RID: 6554 RVA: 0x000FA2B8 File Offset: 0x000F84B8
	public void Fall()
	{
		base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y + 0.0001f, base.transform.position.z);
		this.Prompt.Label[1].text = "     Drag";
		this.Prompt.HideButton[1] = false;
		this.Prompt.enabled = true;
		if (ClassGlobals.PhysicalGrade + ClassGlobals.PhysicalBonus > 0 && !this.Tranquil)
		{
			this.Prompt.HideButton[3] = false;
		}
		if (this.Dragged)
		{
			this.Yandere.RagdollDragger.connectedBody = null;
			this.Yandere.RagdollPK.connectedBody = null;
			this.Yandere.Dragging = false;
			this.Dragged = false;
		}
		this.Yandere.Ragdoll = null;
		this.Prompt.MyCollider.enabled = true;
		this.BloodPoolSpawner.NearbyBlood = 0;
		this.StopAnimation = true;
		this.SettleTimer = 0f;
		this.Carried = false;
		this.Settled = false;
		this.Falling = true;
		for (int i = 0; i < this.AllRigidbodies.Length; i++)
		{
			this.AllRigidbodies[i].isKinematic = false;
			this.AllColliders[i].enabled = true;
		}
	}

	// Token: 0x0600199B RID: 6555 RVA: 0x000FA41C File Offset: 0x000F861C
	public void QuickDismember()
	{
		for (int i = 0; i < this.BodyParts.Length; i++)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.BodyParts[i], this.SpawnPoints[i].position, Quaternion.identity);
			gameObject.transform.eulerAngles = this.SpawnPoints[i].eulerAngles;
			gameObject.GetComponent<PromptScript>().enabled = false;
			gameObject.GetComponent<PickUpScript>().enabled = false;
			gameObject.GetComponent<OutlineScript>().enabled = false;
		}
		if (this.BloodPoolSpawner.BloodParent == null)
		{
			this.BloodPoolSpawner.Start();
		}
		Debug.Log("BloodPoolSpawner.transform.position is: " + this.BloodPoolSpawner.transform.position);
		Debug.Log("Student.StudentManager.SEStairs.bounds is: " + this.Student.StudentManager.SEStairs.bounds);
		if (!this.Student.StudentManager.NEStairs.bounds.Contains(this.BloodPoolSpawner.transform.position) && !this.Student.StudentManager.NWStairs.bounds.Contains(this.BloodPoolSpawner.transform.position) && !this.Student.StudentManager.SEStairs.bounds.Contains(this.BloodPoolSpawner.transform.position) && !this.Student.StudentManager.SWStairs.bounds.Contains(this.BloodPoolSpawner.transform.position))
		{
			this.BloodPoolSpawner.SpawnBigPool();
		}
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600199C RID: 6556 RVA: 0x000FA5D8 File Offset: 0x000F87D8
	public void Dismember()
	{
		if (!this.Dismembered)
		{
			this.Student.LiquidProjector.material.mainTexture = this.Student.BloodTexture;
			for (int i = 0; i < this.BodyParts.Length; i++)
			{
				if (this.Decapitated)
				{
					i++;
					this.Decapitated = false;
				}
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.BodyParts[i], this.SpawnPoints[i].position, Quaternion.identity);
				gameObject.transform.parent = this.Yandere.LimbParent;
				gameObject.transform.eulerAngles = this.SpawnPoints[i].eulerAngles;
				BodyPartScript component = gameObject.GetComponent<BodyPartScript>();
				component.StudentID = this.StudentID;
				component.Sacrifice = this.Sacrifice;
				if (this.Yandere.StudentManager.NoGravity)
				{
					gameObject.GetComponent<Rigidbody>().useGravity = false;
				}
				if (i == 0)
				{
					if (!this.Student.OriginallyTeacher)
					{
						if (!this.Male)
						{
							this.Student.Cosmetic.FemaleHair[this.Student.Cosmetic.Hairstyle].transform.parent = gameObject.transform;
							if (this.Student.Cosmetic.FemaleAccessories[this.Student.Cosmetic.Accessory] != null && this.Student.Cosmetic.Accessory != 3 && this.Student.Cosmetic.Accessory != 6)
							{
								this.Student.Cosmetic.FemaleAccessories[this.Student.Cosmetic.Accessory].transform.parent = gameObject.transform;
							}
						}
						else
						{
							Transform transform = this.Student.Cosmetic.MaleHair[this.Student.Cosmetic.Hairstyle].transform;
							transform.parent = gameObject.transform;
							transform.localScale *= 1.06382978f;
							if (transform.transform.localPosition.y < -1f)
							{
								transform.transform.localPosition = new Vector3(transform.transform.localPosition.x, transform.transform.localPosition.y - 0.095f, transform.transform.localPosition.z);
							}
							if (this.Student.Cosmetic.MaleAccessories[this.Student.Cosmetic.Accessory] != null)
							{
								this.Student.Cosmetic.MaleAccessories[this.Student.Cosmetic.Accessory].transform.parent = gameObject.transform;
							}
						}
					}
					else
					{
						this.Student.Cosmetic.TeacherHair[this.Student.Cosmetic.Hairstyle].transform.parent = gameObject.transform;
						if (this.Student.Cosmetic.TeacherAccessories[this.Student.Cosmetic.Accessory] != null)
						{
							this.Student.Cosmetic.TeacherAccessories[this.Student.Cosmetic.Accessory].transform.parent = gameObject.transform;
						}
					}
					if (this.Student.Club != ClubType.Photography && this.Student.Club < ClubType.Gaming && this.Student.Cosmetic.ClubAccessories[(int)this.Student.Club] != null)
					{
						this.Student.Cosmetic.ClubAccessories[(int)this.Student.Club].transform.parent = gameObject.transform;
						if (this.Student.Club == ClubType.Occult)
						{
							if (!this.Male)
							{
								this.Student.Cosmetic.ClubAccessories[(int)this.Student.Club].transform.localPosition = new Vector3(0f, -1.5f, 0.01f);
								this.Student.Cosmetic.ClubAccessories[(int)this.Student.Club].transform.localEulerAngles = Vector3.zero;
							}
							else
							{
								this.Student.Cosmetic.ClubAccessories[(int)this.Student.Club].transform.localPosition = new Vector3(0f, -1.42f, 0.005f);
								this.Student.Cosmetic.ClubAccessories[(int)this.Student.Club].transform.localEulerAngles = Vector3.zero;
							}
						}
					}
					gameObject.GetComponent<Renderer>().materials[0].mainTexture = this.Student.Cosmetic.FaceTexture;
					if (i == 0)
					{
						gameObject.transform.position += new Vector3(0f, 1f, 0f);
					}
				}
				else if (i == 1)
				{
					if (this.Student.Club == ClubType.Photography && this.Student.Cosmetic.ClubAccessories[(int)this.Student.Club] != null)
					{
						this.Student.Cosmetic.ClubAccessories[(int)this.Student.Club].transform.parent = gameObject.transform;
					}
				}
				else if (i == 2 && !this.Student.Male && this.Student.Cosmetic.Accessory == 6)
				{
					this.Student.Cosmetic.FemaleAccessories[this.Student.Cosmetic.Accessory].transform.parent = gameObject.transform;
				}
			}
			if (this.BloodPoolSpawner.BloodParent == null)
			{
				this.BloodPoolSpawner.Start();
			}
			Debug.Log("BloodPoolSpawner.transform.position is: " + this.BloodPoolSpawner.transform.position);
			Debug.Log("Student.StudentManager.SEStairs.bounds is: " + this.Student.StudentManager.SEStairs.bounds);
			Debug.Log("Student.StudentManager.SEStairs.bounds.Contains(BloodPoolSpawner.transform.position) is: " + this.Student.StudentManager.SEStairs.bounds.Contains(this.BloodPoolSpawner.transform.position).ToString());
			if (!this.Student.StudentManager.NEStairs.bounds.Contains(this.BloodPoolSpawner.transform.position) && !this.Student.StudentManager.NWStairs.bounds.Contains(this.BloodPoolSpawner.transform.position) && !this.Student.StudentManager.SEStairs.bounds.Contains(this.BloodPoolSpawner.transform.position) && !this.Student.StudentManager.SWStairs.bounds.Contains(this.BloodPoolSpawner.transform.position))
			{
				this.BloodPoolSpawner.SpawnBigPool();
			}
			this.Police.PartsIcon.gameObject.SetActive(true);
			this.Police.BodyParts += 6;
			this.Yandere.NearBodies--;
			this.Police.Corpses--;
			base.gameObject.SetActive(false);
			this.Dismembered = true;
		}
	}

	// Token: 0x0600199D RID: 6557 RVA: 0x000FAD88 File Offset: 0x000F8F88
	public void Remove()
	{
		Debug.Log("The Remove() function has been called on " + this.Student.Name + "'s RagdollScript.");
		this.Student.Removed = true;
		this.BloodPoolSpawner.enabled = false;
		if (this.AddingToCount)
		{
			this.Yandere.NearBodies--;
		}
		if (this.Poisoned)
		{
			this.Police.PoisonScene = false;
		}
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600199E RID: 6558 RVA: 0x000FAE08 File Offset: 0x000F9008
	public void DisableRigidbodies()
	{
		this.BloodPoolSpawner.gameObject.SetActive(false);
		for (int i = 0; i < this.AllRigidbodies.Length; i++)
		{
			if (this.AllRigidbodies[i].gameObject.GetComponent<CharacterJoint>() != null)
			{
				UnityEngine.Object.Destroy(this.AllRigidbodies[i].gameObject.GetComponent<CharacterJoint>());
			}
			UnityEngine.Object.Destroy(this.AllRigidbodies[i]);
			this.AllColliders[i].enabled = false;
		}
		this.Prompt.Hide();
		this.Prompt.enabled = false;
		base.enabled = false;
	}

	// Token: 0x0400275B RID: 10075
	public BloodPoolSpawnerScript BloodPoolSpawner;

	// Token: 0x0400275C RID: 10076
	public DetectionMarkerScript DetectionMarker;

	// Token: 0x0400275D RID: 10077
	public IncineratorScript Incinerator;

	// Token: 0x0400275E RID: 10078
	public WoodChipperScript WoodChipper;

	// Token: 0x0400275F RID: 10079
	public TranqCaseScript TranqCase;

	// Token: 0x04002760 RID: 10080
	public StudentScript Student;

	// Token: 0x04002761 RID: 10081
	public YandereScript Yandere;

	// Token: 0x04002762 RID: 10082
	public PoliceScript Police;

	// Token: 0x04002763 RID: 10083
	public PromptScript Prompt;

	// Token: 0x04002764 RID: 10084
	public SkinnedMeshRenderer MyRenderer;

	// Token: 0x04002765 RID: 10085
	public Collider BloodSpawnerCollider;

	// Token: 0x04002766 RID: 10086
	public Animation CharacterAnimation;

	// Token: 0x04002767 RID: 10087
	public Collider HideCollider;

	// Token: 0x04002768 RID: 10088
	public Rigidbody[] AllRigidbodies;

	// Token: 0x04002769 RID: 10089
	public Collider[] AllColliders;

	// Token: 0x0400276A RID: 10090
	public Rigidbody[] Rigidbodies;

	// Token: 0x0400276B RID: 10091
	public Transform[] SpawnPoints;

	// Token: 0x0400276C RID: 10092
	public GameObject[] BodyParts;

	// Token: 0x0400276D RID: 10093
	public Transform NearestLimb;

	// Token: 0x0400276E RID: 10094
	public Transform RightBreast;

	// Token: 0x0400276F RID: 10095
	public Transform LeftBreast;

	// Token: 0x04002770 RID: 10096
	public Transform PelvisRoot;

	// Token: 0x04002771 RID: 10097
	public Transform Ponytail;

	// Token: 0x04002772 RID: 10098
	public Transform RightEye;

	// Token: 0x04002773 RID: 10099
	public Transform LeftEye;

	// Token: 0x04002774 RID: 10100
	public Transform HairR;

	// Token: 0x04002775 RID: 10101
	public Transform HairL;

	// Token: 0x04002776 RID: 10102
	public Transform[] Limb;

	// Token: 0x04002777 RID: 10103
	public Transform Head;

	// Token: 0x04002778 RID: 10104
	public Vector3 RightEyeOrigin;

	// Token: 0x04002779 RID: 10105
	public Vector3 LeftEyeOrigin;

	// Token: 0x0400277A RID: 10106
	public Vector3[] LimbAnchor;

	// Token: 0x0400277B RID: 10107
	public GameObject Character;

	// Token: 0x0400277C RID: 10108
	public GameObject Zs;

	// Token: 0x0400277D RID: 10109
	public bool ElectrocutionAnimation;

	// Token: 0x0400277E RID: 10110
	public bool MurderSuicideAnimation;

	// Token: 0x0400277F RID: 10111
	public bool BurningAnimation;

	// Token: 0x04002780 RID: 10112
	public bool ChokingAnimation;

	// Token: 0x04002781 RID: 10113
	public bool AddingToCount;

	// Token: 0x04002782 RID: 10114
	public bool MurderSuicide;

	// Token: 0x04002783 RID: 10115
	public bool Cauterizable;

	// Token: 0x04002784 RID: 10116
	public bool Electrocuted;

	// Token: 0x04002785 RID: 10117
	public bool StopAnimation = true;

	// Token: 0x04002786 RID: 10118
	public bool Decapitated;

	// Token: 0x04002787 RID: 10119
	public bool Dismembered;

	// Token: 0x04002788 RID: 10120
	public bool NeckSnapped;

	// Token: 0x04002789 RID: 10121
	public bool Cauterized;

	// Token: 0x0400278A RID: 10122
	public bool Disturbing;

	// Token: 0x0400278B RID: 10123
	public bool Sacrifice;

	// Token: 0x0400278C RID: 10124
	public bool Disposed;

	// Token: 0x0400278D RID: 10125
	public bool Poisoned;

	// Token: 0x0400278E RID: 10126
	public bool Tranquil;

	// Token: 0x0400278F RID: 10127
	public bool Burning;

	// Token: 0x04002790 RID: 10128
	public bool Carried;

	// Token: 0x04002791 RID: 10129
	public bool Choking;

	// Token: 0x04002792 RID: 10130
	public bool Dragged;

	// Token: 0x04002793 RID: 10131
	public bool Drowned;

	// Token: 0x04002794 RID: 10132
	public bool Falling;

	// Token: 0x04002795 RID: 10133
	public bool Nemesis;

	// Token: 0x04002796 RID: 10134
	public bool Settled;

	// Token: 0x04002797 RID: 10135
	public bool Suicide;

	// Token: 0x04002798 RID: 10136
	public bool Burned;

	// Token: 0x04002799 RID: 10137
	public bool Dumped;

	// Token: 0x0400279A RID: 10138
	public bool Hidden;

	// Token: 0x0400279B RID: 10139
	public bool Pushed;

	// Token: 0x0400279C RID: 10140
	public bool Male;

	// Token: 0x0400279D RID: 10141
	public float AnimStartTime;

	// Token: 0x0400279E RID: 10142
	public float SettleTimer;

	// Token: 0x0400279F RID: 10143
	public float BreastSize;

	// Token: 0x040027A0 RID: 10144
	public float DumpTimer;

	// Token: 0x040027A1 RID: 10145
	public float EyeShrink;

	// Token: 0x040027A2 RID: 10146
	public float FallTimer;

	// Token: 0x040027A3 RID: 10147
	public int StudentID;

	// Token: 0x040027A4 RID: 10148
	public RagdollDumpType DumpType;

	// Token: 0x040027A5 RID: 10149
	public int LimbID;

	// Token: 0x040027A6 RID: 10150
	public int Frame;

	// Token: 0x040027A7 RID: 10151
	public string DumpedAnim = string.Empty;

	// Token: 0x040027A8 RID: 10152
	public string LiftAnim = string.Empty;

	// Token: 0x040027A9 RID: 10153
	public string IdleAnim = string.Empty;

	// Token: 0x040027AA RID: 10154
	public string WalkAnim = string.Empty;

	// Token: 0x040027AB RID: 10155
	public string RunAnim = string.Empty;

	// Token: 0x040027AC RID: 10156
	public bool UpdateNextFrame;

	// Token: 0x040027AD RID: 10157
	public Vector3 NextPosition;

	// Token: 0x040027AE RID: 10158
	public Quaternion NextRotation;

	// Token: 0x040027AF RID: 10159
	public int Frames;
}
