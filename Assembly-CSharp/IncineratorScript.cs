using System;
using UnityEngine;

// Token: 0x02000303 RID: 771
public class IncineratorScript : MonoBehaviour
{
	// Token: 0x06001786 RID: 6022 RVA: 0x000CBED4 File Offset: 0x000CA0D4
	private void Start()
	{
		this.Panel.SetActive(false);
		this.Prompt.enabled = true;
	}

	// Token: 0x06001787 RID: 6023 RVA: 0x000CBEF0 File Offset: 0x000CA0F0
	private void Update()
	{
		AudioSource component = base.GetComponent<AudioSource>();
		if (!this.Open)
		{
			this.RightDoor.transform.localEulerAngles = new Vector3(this.RightDoor.transform.localEulerAngles.x, Mathf.MoveTowards(this.RightDoor.transform.localEulerAngles.y, 0f, Time.deltaTime * 360f), this.RightDoor.transform.localEulerAngles.z);
			this.LeftDoor.transform.localEulerAngles = new Vector3(this.LeftDoor.transform.localEulerAngles.x, Mathf.MoveTowards(this.LeftDoor.transform.localEulerAngles.y, 0f, Time.deltaTime * 360f), this.LeftDoor.transform.localEulerAngles.z);
			if (this.RightDoor.transform.localEulerAngles.y < 36f)
			{
				if (this.RightDoor.transform.localEulerAngles.y > 0f)
				{
					component.clip = this.IncineratorClose;
					component.Play();
				}
				this.RightDoor.transform.localEulerAngles = new Vector3(this.RightDoor.transform.localEulerAngles.x, 0f, this.RightDoor.transform.localEulerAngles.z);
			}
		}
		else
		{
			if (this.RightDoor.transform.localEulerAngles.y == 0f)
			{
				component.clip = this.IncineratorOpen;
				component.Play();
			}
			this.RightDoor.transform.localEulerAngles = new Vector3(this.RightDoor.transform.localEulerAngles.x, Mathf.Lerp(this.RightDoor.transform.localEulerAngles.y, 135f, Time.deltaTime * 10f), this.RightDoor.transform.localEulerAngles.z);
			this.LeftDoor.transform.localEulerAngles = new Vector3(this.LeftDoor.transform.localEulerAngles.x, Mathf.Lerp(this.LeftDoor.transform.localEulerAngles.y, 135f, Time.deltaTime * 10f), this.LeftDoor.transform.localEulerAngles.z);
			if (this.RightDoor.transform.localEulerAngles.y > 134f)
			{
				this.RightDoor.transform.localEulerAngles = new Vector3(this.RightDoor.transform.localEulerAngles.x, 135f, this.RightDoor.transform.localEulerAngles.z);
			}
		}
		if (this.OpenTimer > 0f)
		{
			this.OpenTimer -= Time.deltaTime;
			if (this.OpenTimer <= 1f)
			{
				this.Open = false;
			}
			if (this.OpenTimer <= 0f)
			{
				this.Prompt.enabled = true;
			}
		}
		else if (!this.Smoke.isPlaying)
		{
			this.YandereHoldingEvidence = (this.Yandere.Ragdoll != null);
			if (!this.YandereHoldingEvidence)
			{
				if (this.Yandere.PickUp != null)
				{
					this.YandereHoldingEvidence = (this.Yandere.PickUp.Evidence || this.Yandere.PickUp.Garbage);
				}
				else
				{
					this.YandereHoldingEvidence = false;
				}
			}
			if (!this.YandereHoldingEvidence)
			{
				if (this.Yandere.EquippedWeapon != null)
				{
					this.YandereHoldingEvidence = this.Yandere.EquippedWeapon.MurderWeapon;
				}
				else
				{
					this.YandereHoldingEvidence = false;
				}
			}
			if (!this.YandereHoldingEvidence)
			{
				if (!this.Prompt.HideButton[3])
				{
					this.Prompt.HideButton[3] = true;
				}
			}
			else if (this.Prompt.HideButton[3])
			{
				this.Prompt.HideButton[3] = false;
			}
			if ((this.Yandere.Chased || this.Yandere.Chasers > 0 || !this.YandereHoldingEvidence) && !this.Prompt.HideButton[3])
			{
				this.Prompt.HideButton[3] = true;
			}
			if (this.Ready)
			{
				if (!this.Smoke.isPlaying)
				{
					if (this.Prompt.HideButton[0])
					{
						this.Prompt.HideButton[0] = false;
					}
				}
				else if (!this.Prompt.HideButton[0])
				{
					this.Prompt.HideButton[0] = true;
				}
			}
		}
		if (this.Prompt.Circle[3].fillAmount == 0f)
		{
			Time.timeScale = 1f;
			if (this.Yandere.Ragdoll != null)
			{
				this.Yandere.Character.GetComponent<Animation>().CrossFade(this.Yandere.Carrying ? "f02_carryIdleA_00" : "f02_dragIdle_00");
				this.Yandere.YandereVision = false;
				this.Yandere.CanMove = false;
				this.Yandere.Dumping = true;
				this.Prompt.Hide();
				this.Prompt.enabled = false;
				this.Victims++;
				this.VictimList[this.Victims] = this.Yandere.Ragdoll.GetComponent<RagdollScript>().StudentID;
				this.Open = true;
			}
			if (this.Yandere.PickUp != null)
			{
				if (this.Yandere.PickUp.BodyPart != null)
				{
					this.Limbs++;
					this.LimbList[this.Limbs] = this.Yandere.PickUp.GetComponent<BodyPartScript>().StudentID;
				}
				this.Yandere.PickUp.Incinerator = this;
				this.Yandere.PickUp.Dumped = true;
				this.Yandere.PickUp.Drop();
				this.Prompt.Hide();
				this.Prompt.enabled = false;
				this.OpenTimer = 2f;
				this.Ready = true;
				this.Open = true;
			}
			WeaponScript equippedWeapon = this.Yandere.EquippedWeapon;
			if (equippedWeapon != null)
			{
				this.DestroyedEvidence++;
				this.EvidenceList[this.DestroyedEvidence] = equippedWeapon.WeaponID;
				equippedWeapon.Incinerator = this;
				equippedWeapon.Dumped = true;
				equippedWeapon.Drop();
				this.Prompt.Hide();
				this.Prompt.enabled = false;
				this.OpenTimer = 2f;
				this.Ready = true;
				this.Open = true;
			}
		}
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Circle[0].fillAmount = 1f;
			this.Panel.SetActive(true);
			this.Timer = 60f;
			component.clip = this.IncineratorActivate;
			component.Play();
			this.Flames.Play();
			this.Smoke.Play();
			this.Prompt.Hide();
			this.Prompt.enabled = false;
			this.Yandere.Police.IncineratedWeapons += this.MurderWeapons;
			this.Yandere.Police.BloodyClothing -= this.BloodyClothing;
			this.Yandere.Police.BloodyWeapons -= this.MurderWeapons;
			this.Yandere.Police.BodyParts -= this.BodyParts;
			this.Yandere.Police.Corpses -= this.Corpses;
			if (this.Yandere.Police.SuicideScene && this.Yandere.Police.Corpses == 1)
			{
				this.Yandere.Police.MurderScene = false;
			}
			if (this.Yandere.Police.Corpses == 0)
			{
				this.Yandere.Police.MurderScene = false;
			}
			this.BloodyClothing = 0;
			this.MurderWeapons = 0;
			this.BodyParts = 0;
			this.Corpses = 0;
			this.ID = 0;
			while (this.ID < 101)
			{
				if (this.Yandere.StudentManager.Students[this.CorpseList[this.ID]] != null)
				{
					this.Yandere.StudentManager.Students[this.CorpseList[this.ID]].Ragdoll.Disposed = true;
					this.ConfirmedDead[this.ID] = this.CorpseList[this.ID];
					if (this.Yandere.StudentManager.Students[this.CorpseList[this.ID]].Ragdoll.Drowned)
					{
						this.Yandere.Police.DrownVictims--;
					}
				}
				this.ID++;
			}
		}
		if (this.Smoke.isPlaying)
		{
			this.Timer -= Time.deltaTime * (this.Clock.TimeSpeed / 60f);
			this.FlameSound.volume += Time.deltaTime;
			this.Circle.fillAmount = 1f - this.Timer / 60f;
			if (this.Timer <= 0f)
			{
				this.Prompt.HideButton[0] = true;
				this.Prompt.enabled = true;
				this.Panel.SetActive(false);
				this.Ready = false;
				this.Flames.Stop();
				this.Smoke.Stop();
			}
		}
		else
		{
			this.FlameSound.volume -= Time.deltaTime;
		}
		if (this.Panel.activeInHierarchy)
		{
			float num = (float)Mathf.CeilToInt(this.Timer * 60f);
			float num2 = Mathf.Floor(num / 60f);
			float num3 = (float)Mathf.RoundToInt(num % 60f);
			this.TimeLabel.text = string.Format("{0:00}:{1:00}", num2, num3);
		}
	}

	// Token: 0x06001788 RID: 6024 RVA: 0x000CC958 File Offset: 0x000CAB58
	public void SetVictimsMissing()
	{
		int[] confirmedDead = this.ConfirmedDead;
		for (int i = 0; i < confirmedDead.Length; i++)
		{
			StudentGlobals.SetStudentMissing(confirmedDead[i], true);
		}
	}

	// Token: 0x040020C2 RID: 8386
	public YandereScript Yandere;

	// Token: 0x040020C3 RID: 8387
	public PromptScript Prompt;

	// Token: 0x040020C4 RID: 8388
	public ClockScript Clock;

	// Token: 0x040020C5 RID: 8389
	public AudioClip IncineratorActivate;

	// Token: 0x040020C6 RID: 8390
	public AudioClip IncineratorClose;

	// Token: 0x040020C7 RID: 8391
	public AudioClip IncineratorOpen;

	// Token: 0x040020C8 RID: 8392
	public AudioSource FlameSound;

	// Token: 0x040020C9 RID: 8393
	public ParticleSystem Flames;

	// Token: 0x040020CA RID: 8394
	public ParticleSystem Smoke;

	// Token: 0x040020CB RID: 8395
	public Transform DumpPoint;

	// Token: 0x040020CC RID: 8396
	public Transform RightDoor;

	// Token: 0x040020CD RID: 8397
	public Transform LeftDoor;

	// Token: 0x040020CE RID: 8398
	public GameObject Panel;

	// Token: 0x040020CF RID: 8399
	public UILabel TimeLabel;

	// Token: 0x040020D0 RID: 8400
	public UISprite Circle;

	// Token: 0x040020D1 RID: 8401
	public bool YandereHoldingEvidence;

	// Token: 0x040020D2 RID: 8402
	public bool Ready;

	// Token: 0x040020D3 RID: 8403
	public bool Open;

	// Token: 0x040020D4 RID: 8404
	public int DestroyedEvidence;

	// Token: 0x040020D5 RID: 8405
	public int BloodyClothing;

	// Token: 0x040020D6 RID: 8406
	public int MurderWeapons;

	// Token: 0x040020D7 RID: 8407
	public int BodyParts;

	// Token: 0x040020D8 RID: 8408
	public int Corpses;

	// Token: 0x040020D9 RID: 8409
	public int Victims;

	// Token: 0x040020DA RID: 8410
	public int Limbs;

	// Token: 0x040020DB RID: 8411
	public int ID;

	// Token: 0x040020DC RID: 8412
	public float OpenTimer;

	// Token: 0x040020DD RID: 8413
	public float Timer;

	// Token: 0x040020DE RID: 8414
	public int[] EvidenceList;

	// Token: 0x040020DF RID: 8415
	public int[] CorpseList;

	// Token: 0x040020E0 RID: 8416
	public int[] VictimList;

	// Token: 0x040020E1 RID: 8417
	public int[] LimbList;

	// Token: 0x040020E2 RID: 8418
	public int[] ConfirmedDead;
}
