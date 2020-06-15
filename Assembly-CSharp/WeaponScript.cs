﻿using System;
using UnityEngine;

// Token: 0x02000468 RID: 1128
public class WeaponScript : MonoBehaviour
{
	// Token: 0x06001D38 RID: 7480 RVA: 0x0015D74C File Offset: 0x0015B94C
	private void Start()
	{
		this.Yandere = GameObject.Find("YandereChan").GetComponent<YandereScript>();
		this.StartingPosition = base.transform.position;
		this.StartingRotation = base.transform.eulerAngles;
		Physics.IgnoreCollision(this.Yandere.GetComponent<Collider>(), this.MyCollider);
		this.OriginalColor = this.Outline[0].color;
		if (this.StartLow)
		{
			this.OriginalOffset = this.Prompt.OffsetY[3];
			this.Prompt.OffsetY[3] = 0.2f;
		}
		if (this.DisableCollider)
		{
			this.MyCollider.enabled = false;
		}
		this.MyAudio = base.GetComponent<AudioSource>();
		if (this.MyAudio != null)
		{
			this.OriginalClip = this.MyAudio.clip;
		}
		this.MyRigidbody = base.GetComponent<Rigidbody>();
		this.MyRigidbody.isKinematic = true;
		Transform transform = GameObject.Find("WeaponOriginParent").transform;
		this.Origin = UnityEngine.Object.Instantiate<GameObject>(this.Prompt.Yandere.StudentManager.EmptyObject, base.transform.position, Quaternion.identity).transform;
		this.Origin.parent = transform;
	}

	// Token: 0x06001D39 RID: 7481 RVA: 0x0015D890 File Offset: 0x0015BA90
	public string GetTypePrefix()
	{
		if (this.Type == WeaponType.Knife)
		{
			return "knife";
		}
		if (this.Type == WeaponType.Katana)
		{
			return "katana";
		}
		if (this.Type == WeaponType.Bat)
		{
			return "bat";
		}
		if (this.Type == WeaponType.Saw)
		{
			return "saw";
		}
		if (this.Type == WeaponType.Syringe)
		{
			return "syringe";
		}
		if (this.Type == WeaponType.Weight)
		{
			return "weight";
		}
		if (this.Type == WeaponType.Garrote)
		{
			return "syringe";
		}
		Debug.LogError("Weapon type \"" + this.Type.ToString() + "\" not implemented.");
		return string.Empty;
	}

	// Token: 0x06001D3A RID: 7482 RVA: 0x0015D930 File Offset: 0x0015BB30
	public AudioClip GetClip(float sanity, bool stealth)
	{
		AudioClip[] array;
		if (this.Clips2.Length == 0)
		{
			array = this.Clips;
		}
		else
		{
			array = ((UnityEngine.Random.Range(2, 4) == 2) ? this.Clips2 : this.Clips3);
		}
		if (stealth)
		{
			return array[0];
		}
		if (sanity > 0.6666667f)
		{
			return array[1];
		}
		if (sanity > 0.333333343f)
		{
			return array[2];
		}
		return array[3];
	}

	// Token: 0x06001D3B RID: 7483 RVA: 0x0015D98C File Offset: 0x0015BB8C
	private void Update()
	{
		if (this.WeaponID == 16 && this.Yandere.EquippedWeapon == this && Input.GetButtonDown("RB"))
		{
			this.ExtraBlade.SetActive(!this.ExtraBlade.activeInHierarchy);
		}
		if (this.Dismembering)
		{
			AudioSource component = base.GetComponent<AudioSource>();
			if (this.DismemberPhase < 4)
			{
				if (component.time > 0.75f)
				{
					if (this.Speed < 36f)
					{
						this.Speed += Time.deltaTime + 10f;
					}
					this.Rotation += this.Speed;
					this.Blade.localEulerAngles = new Vector3(this.Rotation, this.Blade.localEulerAngles.y, this.Blade.localEulerAngles.z);
				}
				if (component.time > this.SoundTime[this.DismemberPhase])
				{
					this.Yandere.Sanity -= 5f * this.Yandere.Numbness;
					this.Yandere.Bloodiness += 25f;
					this.ShortBloodSpray[0].Play();
					this.ShortBloodSpray[1].Play();
					this.Blood.enabled = true;
					this.MurderWeapon = true;
					this.DismemberPhase++;
				}
			}
			else
			{
				this.Rotation = Mathf.Lerp(this.Rotation, 0f, Time.deltaTime * 2f);
				this.Blade.localEulerAngles = new Vector3(this.Rotation, this.Blade.localEulerAngles.y, this.Blade.localEulerAngles.z);
				if (!component.isPlaying)
				{
					component.clip = this.OriginalClip;
					this.Yandere.StainWeapon();
					this.Dismembering = false;
					this.DismemberPhase = 0;
					this.Rotation = 0f;
					this.Speed = 0f;
				}
			}
		}
		else if (this.Yandere.EquippedWeapon == this)
		{
			if (this.Yandere.AttackManager.IsAttacking())
			{
				if (this.Type == WeaponType.Knife)
				{
					base.transform.localEulerAngles = new Vector3(base.transform.localEulerAngles.x, Mathf.Lerp(base.transform.localEulerAngles.y, this.Flip ? 180f : 0f, Time.deltaTime * 10f), base.transform.localEulerAngles.z);
				}
				else if (this.Type == WeaponType.Saw && this.Spin)
				{
					this.Blade.transform.localEulerAngles = new Vector3(this.Blade.transform.localEulerAngles.x + Time.deltaTime * 360f, this.Blade.transform.localEulerAngles.y, this.Blade.transform.localEulerAngles.z);
				}
			}
		}
		else if (!this.MyRigidbody.isKinematic)
		{
			this.KinematicTimer = Mathf.MoveTowards(this.KinematicTimer, 5f, Time.deltaTime);
			if (this.KinematicTimer == 5f)
			{
				this.MyRigidbody.isKinematic = true;
				this.KinematicTimer = 0f;
			}
			if (base.transform.position.x > -71f && base.transform.position.x < -61f && base.transform.position.z > -37.5f && base.transform.position.z < -27.5f)
			{
				base.transform.position = new Vector3(-63f, 1f, -26.5f);
				this.KinematicTimer = 0f;
			}
			if (base.transform.position.x > -21f && base.transform.position.x < 21f && base.transform.position.z > 100f && base.transform.position.z < 135f)
			{
				base.transform.position = new Vector3(0f, 1f, 100f);
				this.KinematicTimer = 0f;
			}
			if (base.transform.position.x > -46f && base.transform.position.x < -18f && base.transform.position.z > 66f && base.transform.position.z < 78f)
			{
				base.transform.position = new Vector3(-16f, 5f, 72f);
				this.KinematicTimer = 0f;
			}
		}
		if (this.Rotate)
		{
			base.transform.Rotate(Vector3.forward * Time.deltaTime * 100f);
		}
	}

	// Token: 0x06001D3C RID: 7484 RVA: 0x0015DED0 File Offset: 0x0015C0D0
	private void LateUpdate()
	{
		if (this.Prompt.Circle[3].fillAmount == 0f)
		{
			if (this.WeaponID == 6 && SchemeGlobals.GetSchemeStage(4) == 1)
			{
				SchemeGlobals.SetSchemeStage(4, 2);
				this.Yandere.PauseScreen.Schemes.UpdateInstructions();
			}
			this.Prompt.Circle[3].fillAmount = 1f;
			if (this.Prompt.Suspicious)
			{
				this.Yandere.TheftTimer = 0.1f;
			}
			if (this.Dangerous || this.Suspicious)
			{
				this.Yandere.WeaponTimer = 0.1f;
			}
			if (!this.Yandere.Gloved)
			{
				this.FingerprintID = 100;
			}
			this.ID = 0;
			while (this.ID < this.Outline.Length)
			{
				this.Outline[this.ID].color = new Color(0f, 0f, 0f, 1f);
				this.ID++;
			}
			base.transform.parent = this.Yandere.ItemParent;
			base.transform.localPosition = Vector3.zero;
			if (this.Type == WeaponType.Bat)
			{
				base.transform.localEulerAngles = new Vector3(0f, 0f, -90f);
			}
			else
			{
				base.transform.localEulerAngles = Vector3.zero;
			}
			this.MyCollider.enabled = false;
			this.MyRigidbody.constraints = RigidbodyConstraints.FreezeAll;
			if (this.Yandere.Equipped == 3 && this.Yandere.Weapon[3] != null)
			{
				this.Yandere.Weapon[3].Drop();
			}
			if (this.Yandere.PickUp != null)
			{
				this.Yandere.PickUp.Drop();
			}
			if (this.Yandere.Dragging)
			{
				this.Yandere.Ragdoll.GetComponent<RagdollScript>().StopDragging();
			}
			if (this.Yandere.Carrying)
			{
				this.Yandere.StopCarrying();
			}
			if (this.Concealable)
			{
				if (this.Yandere.Weapon[1] == null)
				{
					if (this.Yandere.Weapon[2] != null)
					{
						this.Yandere.Weapon[2].gameObject.SetActive(false);
					}
					this.Yandere.Equipped = 1;
					this.Yandere.EquippedWeapon = this;
					this.Yandere.WeaponManager.SetEquippedWeapon1(this);
				}
				else if (this.Yandere.Weapon[2] == null)
				{
					if (this.Yandere.Weapon[1] != null)
					{
						if (!this.DoNotDisable)
						{
							Debug.Log("We reached this code.");
							this.Yandere.Weapon[1].gameObject.SetActive(false);
						}
						this.DoNotDisable = false;
					}
					this.Yandere.Equipped = 2;
					this.Yandere.EquippedWeapon = this;
					this.Yandere.WeaponManager.SetEquippedWeapon2(this);
				}
				else if (this.Yandere.Weapon[2].gameObject.activeInHierarchy)
				{
					this.Yandere.Weapon[2].Drop();
					this.Yandere.Equipped = 2;
					this.Yandere.EquippedWeapon = this;
					this.Yandere.WeaponManager.SetEquippedWeapon2(this);
				}
				else
				{
					this.Yandere.Weapon[1].Drop();
					this.Yandere.Equipped = 1;
					this.Yandere.EquippedWeapon = this;
					this.Yandere.WeaponManager.SetEquippedWeapon1(this);
				}
			}
			else
			{
				if (this.Yandere.Weapon[1] != null)
				{
					this.Yandere.Weapon[1].gameObject.SetActive(false);
				}
				if (this.Yandere.Weapon[2] != null)
				{
					this.Yandere.Weapon[2].gameObject.SetActive(false);
				}
				this.Yandere.Equipped = 3;
				this.Yandere.EquippedWeapon = this;
				this.Yandere.WeaponManager.SetEquippedWeapon3(this);
			}
			this.Yandere.StudentManager.UpdateStudents(0);
			this.Prompt.Hide();
			this.Prompt.enabled = false;
			this.Yandere.NearestPrompt = null;
			if (this.WeaponID == 9 || this.WeaponID == 10 || this.WeaponID == 12 || this.WeaponID == 25)
			{
				this.SuspicionCheck();
			}
			if (this.Yandere.EquippedWeapon.Suspicious)
			{
				if (!this.Yandere.WeaponWarning)
				{
					this.Yandere.NotificationManager.DisplayNotification(NotificationType.Armed);
					this.Yandere.WeaponWarning = true;
				}
			}
			else
			{
				this.Yandere.WeaponWarning = false;
			}
			this.Yandere.WeaponMenu.UpdateSprites();
			this.Yandere.WeaponManager.UpdateLabels();
			if (this.Evidence)
			{
				this.Yandere.Police.BloodyWeapons--;
			}
			if (this.WeaponID == 11)
			{
				this.Yandere.IdleAnim = "CyborgNinja_Idle_Armed";
				this.Yandere.WalkAnim = "CyborgNinja_Walk_Armed";
				this.Yandere.RunAnim = "CyborgNinja_Run_Armed";
			}
			if (this.WeaponID == 26)
			{
				this.WeaponTrail.SetActive(true);
			}
			this.KinematicTimer = 0f;
			AudioSource.PlayClipAtPoint(this.EquipClip, this.Yandere.MainCamera.transform.position);
			if (this.UnequipImmediately)
			{
				this.UnequipImmediately = false;
				this.Yandere.Unequip();
			}
		}
		if (this.Yandere.EquippedWeapon == this && this.Yandere.Armed)
		{
			base.transform.localScale = new Vector3(1f, 1f, 1f);
			if (!this.Yandere.Struggling)
			{
				if (this.Yandere.CanMove)
				{
					base.transform.localPosition = Vector3.zero;
					if (this.Type == WeaponType.Bat)
					{
						base.transform.localEulerAngles = new Vector3(0f, 0f, -90f);
					}
					else
					{
						base.transform.localEulerAngles = Vector3.zero;
					}
				}
			}
			else
			{
				base.transform.localPosition = new Vector3(-0.01f, 0.005f, -0.01f);
			}
		}
		if (this.Dumped)
		{
			this.DumpTimer += Time.deltaTime;
			if (this.DumpTimer > 1f)
			{
				this.Yandere.Incinerator.MurderWeapons++;
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}
		if (base.transform.parent == this.Yandere.ItemParent && this.Concealable && this.Yandere.Weapon[1] != this && this.Yandere.Weapon[2] != this)
		{
			this.Drop();
		}
	}

	// Token: 0x06001D3D RID: 7485 RVA: 0x0015E5F8 File Offset: 0x0015C7F8
	public void Drop()
	{
		if (this.WeaponID == 6 && SchemeGlobals.GetSchemeStage(4) == 2)
		{
			SchemeGlobals.SetSchemeStage(4, 1);
			this.Yandere.PauseScreen.Schemes.UpdateInstructions();
		}
		Debug.Log("A " + base.gameObject.name + " has been dropped.");
		if (this.WeaponID == 11)
		{
			this.Yandere.IdleAnim = "CyborgNinja_Idle_Unarmed";
			this.Yandere.WalkAnim = this.Yandere.OriginalWalkAnim;
			this.Yandere.RunAnim = "CyborgNinja_Run_Unarmed";
		}
		if (this.StartLow)
		{
			this.Prompt.OffsetY[3] = this.OriginalOffset;
		}
		if (this.Yandere.Weapon[1] == this)
		{
			this.Yandere.WeaponManager.YandereWeapon1 = -1;
		}
		else if (this.Yandere.Weapon[2] == this)
		{
			this.Yandere.WeaponManager.YandereWeapon2 = -1;
		}
		else if (this.Yandere.Weapon[3] == this)
		{
			this.Yandere.WeaponManager.YandereWeapon3 = -1;
		}
		if (this.Yandere.EquippedWeapon == this)
		{
			this.Yandere.EquippedWeapon = null;
			this.Yandere.Equipped = 0;
			this.Yandere.StudentManager.UpdateStudents(0);
		}
		base.gameObject.SetActive(true);
		base.transform.parent = null;
		this.MyRigidbody.constraints = RigidbodyConstraints.None;
		this.MyRigidbody.isKinematic = false;
		this.MyRigidbody.useGravity = true;
		this.MyCollider.isTrigger = false;
		if (this.Dumped)
		{
			base.transform.position = this.Incinerator.DumpPoint.position;
		}
		else
		{
			this.Prompt.enabled = true;
			this.MyCollider.enabled = true;
			if (this.Yandere.GetComponent<Collider>().enabled)
			{
				Physics.IgnoreCollision(this.Yandere.GetComponent<Collider>(), this.MyCollider);
			}
		}
		if (this.Evidence)
		{
			this.Yandere.Police.BloodyWeapons++;
		}
		if (Vector3.Distance(this.StartingPosition, base.transform.position) > 5f && Vector3.Distance(base.transform.position, this.Yandere.StudentManager.WeaponBoxSpot.parent.position) > 1f)
		{
			if (!this.Misplaced)
			{
				this.Prompt.Yandere.WeaponManager.MisplacedWeapons++;
				this.Misplaced = true;
			}
		}
		else if (this.Misplaced)
		{
			this.Prompt.Yandere.WeaponManager.MisplacedWeapons--;
			this.Misplaced = false;
		}
		this.ID = 0;
		while (this.ID < this.Outline.Length)
		{
			this.Outline[this.ID].color = (this.Evidence ? this.EvidenceColor : this.OriginalColor);
			this.ID++;
		}
		if (base.transform.position.y > 1000f)
		{
			base.transform.position = new Vector3(12f, 0f, 28f);
		}
		if (this.WeaponID == 26)
		{
			base.transform.parent = this.Parent;
			base.transform.localEulerAngles = Vector3.zero;
			base.transform.localPosition = Vector3.zero;
			this.MyRigidbody.isKinematic = true;
			this.WeaponTrail.SetActive(false);
		}
	}

	// Token: 0x06001D3E RID: 7486 RVA: 0x0015E9AC File Offset: 0x0015CBAC
	public void UpdateLabel()
	{
		if (this != null && base.gameObject.activeInHierarchy)
		{
			if (this.Yandere.Weapon[1] != null && this.Yandere.Weapon[2] != null && this.Concealable)
			{
				if (this.Prompt.Label[3] != null)
				{
					if (!this.Yandere.Armed || this.Yandere.Equipped == 3)
					{
						this.Prompt.Label[3].text = "     Swap " + this.Yandere.Weapon[1].Name + " for " + this.Name;
						return;
					}
					this.Prompt.Label[3].text = "     Swap " + this.Yandere.EquippedWeapon.Name + " for " + this.Name;
					return;
				}
			}
			else if (this.Prompt.Label[3] != null)
			{
				this.Prompt.Label[3].text = "     " + this.Name;
			}
		}
	}

	// Token: 0x06001D3F RID: 7487 RVA: 0x0015EAEC File Offset: 0x0015CCEC
	public void Effect()
	{
		if (this.WeaponID == 7)
		{
			this.BloodSpray[0].Play();
			this.BloodSpray[1].Play();
			return;
		}
		if (this.WeaponID == 8)
		{
			base.gameObject.GetComponent<ParticleSystem>().Play();
			this.MyAudio.clip = this.OriginalClip;
			this.MyAudio.Play();
			return;
		}
		if (this.WeaponID == 2 || this.WeaponID == 9 || this.WeaponID == 10 || this.WeaponID == 12 || this.WeaponID == 13)
		{
			this.MyAudio.Play();
			return;
		}
		if (this.WeaponID == 14)
		{
			UnityEngine.Object.Instantiate<GameObject>(this.HeartBurst, this.Yandere.TargetStudent.Head.position, Quaternion.identity);
			this.MyAudio.Play();
		}
	}

	// Token: 0x06001D40 RID: 7488 RVA: 0x0015EBCB File Offset: 0x0015CDCB
	public void Dismember()
	{
		this.MyAudio.clip = this.DismemberClip;
		this.MyAudio.Play();
		this.Dismembering = true;
	}

	// Token: 0x06001D41 RID: 7489 RVA: 0x0015EBF0 File Offset: 0x0015CDF0
	public void SuspicionCheck()
	{
		if ((this.WeaponID == 9 && this.Yandere.Club == ClubType.Sports) || (this.WeaponID == 10 && this.Yandere.Club == ClubType.Gardening) || (this.WeaponID == 12 && this.Yandere.Club == ClubType.Sports) || (this.WeaponID == 25 && this.Yandere.Club == ClubType.LightMusic) || (this.WeaponID == 14 && this.Yandere.Club == ClubType.Drama) || (this.WeaponID == 14 && this.Yandere.Club == ClubType.Drama) || (this.WeaponID == 22 && this.Yandere.Club == ClubType.Drama))
		{
			this.Suspicious = false;
		}
		else
		{
			this.Suspicious = true;
		}
		if (this.Bloody)
		{
			this.Suspicious = true;
		}
	}

	// Token: 0x04003709 RID: 14089
	public ParticleSystem[] ShortBloodSpray;

	// Token: 0x0400370A RID: 14090
	public ParticleSystem[] BloodSpray;

	// Token: 0x0400370B RID: 14091
	public OutlineScript[] Outline;

	// Token: 0x0400370C RID: 14092
	public float[] SoundTime;

	// Token: 0x0400370D RID: 14093
	public IncineratorScript Incinerator;

	// Token: 0x0400370E RID: 14094
	public StudentScript Returner;

	// Token: 0x0400370F RID: 14095
	public YandereScript Yandere;

	// Token: 0x04003710 RID: 14096
	public PromptScript Prompt;

	// Token: 0x04003711 RID: 14097
	public Transform Origin;

	// Token: 0x04003712 RID: 14098
	public Transform Parent;

	// Token: 0x04003713 RID: 14099
	public AudioClip[] Clips;

	// Token: 0x04003714 RID: 14100
	public AudioClip[] Clips2;

	// Token: 0x04003715 RID: 14101
	public AudioClip[] Clips3;

	// Token: 0x04003716 RID: 14102
	public AudioClip DismemberClip;

	// Token: 0x04003717 RID: 14103
	public AudioClip EquipClip;

	// Token: 0x04003718 RID: 14104
	public ParticleSystem FireEffect;

	// Token: 0x04003719 RID: 14105
	public GameObject WeaponTrail;

	// Token: 0x0400371A RID: 14106
	public GameObject ExtraBlade;

	// Token: 0x0400371B RID: 14107
	public AudioSource FireAudio;

	// Token: 0x0400371C RID: 14108
	public Rigidbody MyRigidbody;

	// Token: 0x0400371D RID: 14109
	public AudioSource MyAudio;

	// Token: 0x0400371E RID: 14110
	public Collider MyCollider;

	// Token: 0x0400371F RID: 14111
	public Renderer MyRenderer;

	// Token: 0x04003720 RID: 14112
	public Transform Blade;

	// Token: 0x04003721 RID: 14113
	public Projector Blood;

	// Token: 0x04003722 RID: 14114
	public Vector3 StartingPosition;

	// Token: 0x04003723 RID: 14115
	public Vector3 StartingRotation;

	// Token: 0x04003724 RID: 14116
	public bool UnequipImmediately;

	// Token: 0x04003725 RID: 14117
	public bool AlreadyExamined;

	// Token: 0x04003726 RID: 14118
	public bool DelinquentOwned;

	// Token: 0x04003727 RID: 14119
	public bool DisableCollider;

	// Token: 0x04003728 RID: 14120
	public bool DoNotDisable;

	// Token: 0x04003729 RID: 14121
	public bool Dismembering;

	// Token: 0x0400372A RID: 14122
	public bool MurderWeapon;

	// Token: 0x0400372B RID: 14123
	public bool WeaponEffect;

	// Token: 0x0400372C RID: 14124
	public bool Concealable;

	// Token: 0x0400372D RID: 14125
	public bool Suspicious;

	// Token: 0x0400372E RID: 14126
	public bool Dangerous;

	// Token: 0x0400372F RID: 14127
	public bool Misplaced;

	// Token: 0x04003730 RID: 14128
	public bool Evidence;

	// Token: 0x04003731 RID: 14129
	public bool StartLow;

	// Token: 0x04003732 RID: 14130
	public bool Flaming;

	// Token: 0x04003733 RID: 14131
	public bool Bloody;

	// Token: 0x04003734 RID: 14132
	public bool Dumped;

	// Token: 0x04003735 RID: 14133
	public bool Heated;

	// Token: 0x04003736 RID: 14134
	public bool Rotate;

	// Token: 0x04003737 RID: 14135
	public bool Blunt;

	// Token: 0x04003738 RID: 14136
	public bool Metal;

	// Token: 0x04003739 RID: 14137
	public bool Flip;

	// Token: 0x0400373A RID: 14138
	public bool Spin;

	// Token: 0x0400373B RID: 14139
	public Color EvidenceColor;

	// Token: 0x0400373C RID: 14140
	public Color OriginalColor;

	// Token: 0x0400373D RID: 14141
	public float OriginalOffset;

	// Token: 0x0400373E RID: 14142
	public float KinematicTimer;

	// Token: 0x0400373F RID: 14143
	public float DumpTimer;

	// Token: 0x04003740 RID: 14144
	public float Rotation;

	// Token: 0x04003741 RID: 14145
	public float Speed;

	// Token: 0x04003742 RID: 14146
	public string SpriteName;

	// Token: 0x04003743 RID: 14147
	public string Name;

	// Token: 0x04003744 RID: 14148
	public int DismemberPhase;

	// Token: 0x04003745 RID: 14149
	public int FingerprintID;

	// Token: 0x04003746 RID: 14150
	public int GlobalID;

	// Token: 0x04003747 RID: 14151
	public int WeaponID;

	// Token: 0x04003748 RID: 14152
	public int AnimID;

	// Token: 0x04003749 RID: 14153
	public WeaponType Type = WeaponType.Knife;

	// Token: 0x0400374A RID: 14154
	public bool[] Victims;

	// Token: 0x0400374B RID: 14155
	private AudioClip OriginalClip;

	// Token: 0x0400374C RID: 14156
	private int ID;

	// Token: 0x0400374D RID: 14157
	public GameObject HeartBurst;
}
