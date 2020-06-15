using System;
using UnityEngine;

// Token: 0x020002E0 RID: 736
public class HeadmasterScript : MonoBehaviour
{
	// Token: 0x060016FF RID: 5887 RVA: 0x000BF4FB File Offset: 0x000BD6FB
	private void Start()
	{
		this.MyAnimation["HeadmasterRaiseTazer"].speed = 2f;
		this.Tazer.SetActive(false);
	}

	// Token: 0x06001700 RID: 5888 RVA: 0x000BF524 File Offset: 0x000BD724
	private void Update()
	{
		if (this.Yandere.transform.position.y > base.transform.position.y - 1f && this.Yandere.transform.position.y < base.transform.position.y + 1f && this.Yandere.transform.position.x < 6f && this.Yandere.transform.position.x > -6f)
		{
			this.Distance = Vector3.Distance(base.transform.position, this.Yandere.transform.position);
			if (this.Shooting)
			{
				this.targetRotation = Quaternion.LookRotation(base.transform.position - this.Yandere.transform.position);
				this.Yandere.transform.rotation = Quaternion.Slerp(this.Yandere.transform.rotation, this.targetRotation, Time.deltaTime * 10f);
				this.AimWeaponAtYandere();
				this.AimBodyAtYandere();
			}
			else if ((double)this.Distance < 1.2)
			{
				this.AimBodyAtYandere();
				if (this.Yandere.CanMove && !this.Yandere.Egg && !this.Shooting)
				{
					this.Shoot();
				}
			}
			else if ((double)this.Distance < 2.8)
			{
				this.PlayedSitSound = false;
				if (!this.StudentManager.Clock.StopTime)
				{
					this.PatienceTimer -= Time.deltaTime;
				}
				if (this.PatienceTimer < 0f && !this.Yandere.Egg)
				{
					this.LostPatience = true;
					this.PatienceTimer = 60f;
					this.Patience = 0;
					this.Shoot();
				}
				if (!this.LostPatience)
				{
					this.LostPatience = true;
					this.Patience--;
					if (this.Patience < 1 && !this.Yandere.Egg && !this.Shooting)
					{
						this.Shoot();
					}
				}
				this.AimBodyAtYandere();
				this.Threatened = true;
				this.AimWeaponAtYandere();
				this.ThreatTimer = Mathf.MoveTowards(this.ThreatTimer, 0f, Time.deltaTime);
				if (this.ThreatTimer == 0f)
				{
					this.ThreatID++;
					if (this.ThreatID < 5)
					{
						this.HeadmasterSubtitle.text = this.HeadmasterThreatText[this.ThreatID];
						this.MyAudio.clip = this.HeadmasterThreatClips[this.ThreatID];
						this.MyAudio.Play();
						this.ThreatTimer = this.HeadmasterThreatClips[this.ThreatID].length + 1f;
					}
				}
				this.CheckBehavior();
			}
			else if (this.Distance < 10f)
			{
				this.PlayedStandSound = false;
				this.LostPatience = false;
				this.targetRotation = Quaternion.LookRotation(new Vector3(0f, 8f, 0f) - base.transform.position);
				base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.targetRotation, Time.deltaTime * 10f);
				this.Chair.localPosition = Vector3.Lerp(this.Chair.localPosition, new Vector3(this.Chair.localPosition.x, this.Chair.localPosition.y, -4.66666f), Time.deltaTime * 1f);
				this.LookAtPlayer = true;
				if (!this.Threatened)
				{
					this.MyAnimation.CrossFade("HeadmasterAttention", 1f);
					this.ScratchTimer = 0f;
					this.SpeechTimer = Mathf.MoveTowards(this.SpeechTimer, 0f, Time.deltaTime);
					if (this.SpeechTimer == 0f)
					{
						if (this.CardboardBox.parent == null && this.Yandere.Mask == null)
						{
							this.VoiceID++;
							if (this.VoiceID < 6)
							{
								this.HeadmasterSubtitle.text = this.HeadmasterSpeechText[this.VoiceID];
								this.MyAudio.clip = this.HeadmasterSpeechClips[this.VoiceID];
								this.MyAudio.Play();
								this.SpeechTimer = this.HeadmasterSpeechClips[this.VoiceID].length + 1f;
							}
						}
						else
						{
							this.BoxID++;
							if (this.BoxID < 6)
							{
								this.HeadmasterSubtitle.text = this.HeadmasterBoxText[this.BoxID];
								this.MyAudio.clip = this.HeadmasterBoxClips[this.BoxID];
								this.MyAudio.Play();
								this.SpeechTimer = this.HeadmasterBoxClips[this.BoxID].length + 1f;
							}
						}
					}
				}
				else if (!this.Relaxing)
				{
					this.HeadmasterSubtitle.text = this.HeadmasterRelaxText;
					this.MyAudio.clip = this.HeadmasterRelaxClip;
					this.MyAudio.Play();
					this.Relaxing = true;
				}
				else
				{
					if (!this.PlayedSitSound)
					{
						AudioSource.PlayClipAtPoint(this.SitDown, base.transform.position);
						this.PlayedSitSound = true;
					}
					this.MyAnimation.CrossFade("HeadmasterLowerTazer");
					this.Aiming = false;
					if ((double)this.MyAnimation["HeadmasterLowerTazer"].time > 1.33333)
					{
						this.Tazer.SetActive(false);
					}
					if (this.MyAnimation["HeadmasterLowerTazer"].time > this.MyAnimation["HeadmasterLowerTazer"].length)
					{
						this.Threatened = false;
						this.Relaxing = false;
					}
				}
				this.CheckBehavior();
			}
			else
			{
				if (this.LookAtPlayer)
				{
					this.MyAnimation.CrossFade("HeadmasterType");
					this.LookAtPlayer = false;
					this.Threatened = false;
					this.Relaxing = false;
					this.Aiming = false;
				}
				this.ScratchTimer += Time.deltaTime;
				if (this.ScratchTimer > 10f)
				{
					this.MyAnimation.CrossFade("HeadmasterScratch");
					if (this.MyAnimation["HeadmasterScratch"].time > this.MyAnimation["HeadmasterScratch"].length)
					{
						this.MyAnimation.CrossFade("HeadmasterType");
						this.ScratchTimer = 0f;
					}
				}
			}
			if (!this.MyAudio.isPlaying)
			{
				this.HeadmasterSubtitle.text = string.Empty;
				if (this.Shooting)
				{
					this.Taze();
				}
			}
			if (this.Yandere.Attacked && this.Yandere.Character.GetComponent<Animation>()["f02_swingB_00"].time >= this.Yandere.Character.GetComponent<Animation>()["f02_swingB_00"].length * 0.85f)
			{
				this.MyAudio.clip = this.Crumple;
				this.MyAudio.Play();
				base.enabled = false;
				return;
			}
		}
		else
		{
			this.HeadmasterSubtitle.text = string.Empty;
		}
	}

	// Token: 0x06001701 RID: 5889 RVA: 0x000BFCB8 File Offset: 0x000BDEB8
	private void LateUpdate()
	{
		this.LookAtTarget = Vector3.Lerp(this.LookAtTarget, this.LookAtPlayer ? this.Yandere.Head.position : this.Default.position, Time.deltaTime * 10f);
		this.Head.LookAt(this.LookAtTarget);
	}

	// Token: 0x06001702 RID: 5890 RVA: 0x000BFD18 File Offset: 0x000BDF18
	private void AimBodyAtYandere()
	{
		this.targetRotation = Quaternion.LookRotation(this.Yandere.transform.position - base.transform.position);
		base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.targetRotation, Time.deltaTime * 5f);
		this.Chair.localPosition = Vector3.Lerp(this.Chair.localPosition, new Vector3(this.Chair.localPosition.x, this.Chair.localPosition.y, -5.2f), Time.deltaTime * 1f);
	}

	// Token: 0x06001703 RID: 5891 RVA: 0x000BFDCC File Offset: 0x000BDFCC
	private void AimWeaponAtYandere()
	{
		if (!this.Aiming)
		{
			this.MyAnimation.CrossFade("HeadmasterRaiseTazer");
			if (!this.PlayedStandSound)
			{
				AudioSource.PlayClipAtPoint(this.StandUp, base.transform.position);
				this.PlayedStandSound = true;
			}
			if ((double)this.MyAnimation["HeadmasterRaiseTazer"].time > 1.166666)
			{
				this.Tazer.SetActive(true);
				this.Aiming = true;
				return;
			}
		}
		else if (this.MyAnimation["HeadmasterRaiseTazer"].time > this.MyAnimation["HeadmasterRaiseTazer"].length)
		{
			this.MyAnimation.CrossFade("HeadmasterAimTazer");
		}
	}

	// Token: 0x06001704 RID: 5892 RVA: 0x000BFE88 File Offset: 0x000BE088
	public void Shoot()
	{
		this.StudentManager.YandereDying = true;
		this.Yandere.StopAiming();
		this.Yandere.StopLaughing();
		this.Yandere.CharacterAnimation.CrossFade("f02_readyToFight_00");
		if (this.Patience < 1)
		{
			this.HeadmasterSubtitle.text = this.HeadmasterPatienceText;
			this.MyAudio.clip = this.HeadmasterPatienceClip;
		}
		else if (this.Yandere.Armed)
		{
			this.HeadmasterSubtitle.text = this.HeadmasterWeaponText;
			this.MyAudio.clip = this.HeadmasterWeaponClip;
		}
		else if (this.Yandere.Carrying || this.Yandere.Dragging || (this.Yandere.PickUp != null && this.Yandere.PickUp.BodyPart))
		{
			this.HeadmasterSubtitle.text = this.HeadmasterCorpseText;
			this.MyAudio.clip = this.HeadmasterCorpseClip;
		}
		else
		{
			this.HeadmasterSubtitle.text = this.HeadmasterAttackText;
			this.MyAudio.clip = this.HeadmasterAttackClip;
		}
		this.StudentManager.StopMoving();
		this.Yandere.EmptyHands();
		this.Yandere.CanMove = false;
		this.MyAudio.Play();
		this.Shooting = true;
	}

	// Token: 0x06001705 RID: 5893 RVA: 0x000BFFF0 File Offset: 0x000BE1F0
	private void CheckBehavior()
	{
		if (this.Yandere.CanMove && !this.Yandere.Egg)
		{
			if (this.Yandere.Chased || this.Yandere.Chasers > 0)
			{
				if (!this.Shooting)
				{
					this.Shoot();
					return;
				}
			}
			else if (this.Yandere.Armed)
			{
				if (!this.Shooting)
				{
					this.Shoot();
					return;
				}
			}
			else if ((this.Yandere.Carrying || this.Yandere.Dragging || (this.Yandere.PickUp != null && this.Yandere.PickUp.BodyPart)) && !this.Shooting)
			{
				this.Shoot();
			}
		}
	}

	// Token: 0x06001706 RID: 5894 RVA: 0x000C00B8 File Offset: 0x000BE2B8
	public void Taze()
	{
		if (this.Yandere.CanMove)
		{
			this.StudentManager.YandereDying = true;
			this.Yandere.StopAiming();
			this.Yandere.StopLaughing();
			this.StudentManager.StopMoving();
			this.Yandere.EmptyHands();
			this.Yandere.CanMove = false;
		}
		UnityEngine.Object.Instantiate<GameObject>(this.LightningEffect, this.TazerEffectTarget.position, Quaternion.identity);
		UnityEngine.Object.Instantiate<GameObject>(this.LightningEffect, this.Yandere.Spine[3].position, Quaternion.identity);
		this.MyAudio.clip = this.HeadmasterShockClip;
		this.MyAudio.Play();
		this.Yandere.CharacterAnimation.CrossFade("f02_swingB_00");
		this.Yandere.CharacterAnimation["f02_swingB_00"].time = 0.5f;
		this.Yandere.RPGCamera.enabled = false;
		this.Yandere.Attacked = true;
		this.Heartbroken.Headmaster = true;
		this.Jukebox.Volume = 0f;
		this.Shooting = false;
	}

	// Token: 0x04001E8A RID: 7818
	public StudentManagerScript StudentManager;

	// Token: 0x04001E8B RID: 7819
	public HeartbrokenScript Heartbroken;

	// Token: 0x04001E8C RID: 7820
	public YandereScript Yandere;

	// Token: 0x04001E8D RID: 7821
	public JukeboxScript Jukebox;

	// Token: 0x04001E8E RID: 7822
	public AudioClip[] HeadmasterSpeechClips;

	// Token: 0x04001E8F RID: 7823
	public AudioClip[] HeadmasterThreatClips;

	// Token: 0x04001E90 RID: 7824
	public AudioClip[] HeadmasterBoxClips;

	// Token: 0x04001E91 RID: 7825
	public AudioClip HeadmasterRelaxClip;

	// Token: 0x04001E92 RID: 7826
	public AudioClip HeadmasterAttackClip;

	// Token: 0x04001E93 RID: 7827
	public AudioClip HeadmasterCrypticClip;

	// Token: 0x04001E94 RID: 7828
	public AudioClip HeadmasterShockClip;

	// Token: 0x04001E95 RID: 7829
	public AudioClip HeadmasterPatienceClip;

	// Token: 0x04001E96 RID: 7830
	public AudioClip HeadmasterCorpseClip;

	// Token: 0x04001E97 RID: 7831
	public AudioClip HeadmasterWeaponClip;

	// Token: 0x04001E98 RID: 7832
	public AudioClip Crumple;

	// Token: 0x04001E99 RID: 7833
	public AudioClip StandUp;

	// Token: 0x04001E9A RID: 7834
	public AudioClip SitDown;

	// Token: 0x04001E9B RID: 7835
	public readonly string[] HeadmasterSpeechText = new string[]
	{
		"",
		"Ahh...! It's...it's you!",
		"No, that would be impossible...you must be...her daughter...",
		"I'll tolerate you in my school, but not in my office.",
		"Leave at once.",
		"There is nothing for you to achieve here. Just. Get. Out."
	};

	// Token: 0x04001E9C RID: 7836
	public readonly string[] HeadmasterThreatText = new string[]
	{
		"",
		"Not another step!",
		"You're up to no good! I know it!",
		"I'm not going to let you harm me!",
		"I'll use self-defense if I deem it necessary!",
		"This is your final warning. Get out of here...or else."
	};

	// Token: 0x04001E9D RID: 7837
	public readonly string[] HeadmasterBoxText = new string[]
	{
		"",
		"What...in...blazes are you doing?",
		"Are you trying to re-enact something you saw in a video game?",
		"Ugh, do you really think such a stupid ploy is going to work?",
		"I know who you are. It's obvious. You're not fooling anyone.",
		"I don't have time for this tomfoolery. Leave at once!"
	};

	// Token: 0x04001E9E RID: 7838
	public readonly string HeadmasterRelaxText = "Hmm...a wise decision.";

	// Token: 0x04001E9F RID: 7839
	public readonly string HeadmasterAttackText = "You asked for it!";

	// Token: 0x04001EA0 RID: 7840
	public readonly string HeadmasterCrypticText = "Mr. Saikou...the deal is off.";

	// Token: 0x04001EA1 RID: 7841
	public readonly string HeadmasterWeaponText = "How dare you raise a weapon in my office!";

	// Token: 0x04001EA2 RID: 7842
	public readonly string HeadmasterPatienceText = "Enough of this nonsense!";

	// Token: 0x04001EA3 RID: 7843
	public readonly string HeadmasterCorpseText = "You...you murderer!";

	// Token: 0x04001EA4 RID: 7844
	public UILabel HeadmasterSubtitle;

	// Token: 0x04001EA5 RID: 7845
	public Animation MyAnimation;

	// Token: 0x04001EA6 RID: 7846
	public AudioSource MyAudio;

	// Token: 0x04001EA7 RID: 7847
	public GameObject LightningEffect;

	// Token: 0x04001EA8 RID: 7848
	public GameObject Tazer;

	// Token: 0x04001EA9 RID: 7849
	public Transform TazerEffectTarget;

	// Token: 0x04001EAA RID: 7850
	public Transform CardboardBox;

	// Token: 0x04001EAB RID: 7851
	public Transform Chair;

	// Token: 0x04001EAC RID: 7852
	public Quaternion targetRotation;

	// Token: 0x04001EAD RID: 7853
	public float PatienceTimer;

	// Token: 0x04001EAE RID: 7854
	public float ScratchTimer;

	// Token: 0x04001EAF RID: 7855
	public float SpeechTimer;

	// Token: 0x04001EB0 RID: 7856
	public float ThreatTimer;

	// Token: 0x04001EB1 RID: 7857
	public float Distance;

	// Token: 0x04001EB2 RID: 7858
	public int Patience = 10;

	// Token: 0x04001EB3 RID: 7859
	public int ThreatID;

	// Token: 0x04001EB4 RID: 7860
	public int VoiceID;

	// Token: 0x04001EB5 RID: 7861
	public int BoxID;

	// Token: 0x04001EB6 RID: 7862
	public bool PlayedStandSound;

	// Token: 0x04001EB7 RID: 7863
	public bool PlayedSitSound;

	// Token: 0x04001EB8 RID: 7864
	public bool LostPatience;

	// Token: 0x04001EB9 RID: 7865
	public bool Threatened;

	// Token: 0x04001EBA RID: 7866
	public bool Relaxing;

	// Token: 0x04001EBB RID: 7867
	public bool Shooting;

	// Token: 0x04001EBC RID: 7868
	public bool Aiming;

	// Token: 0x04001EBD RID: 7869
	public Vector3 LookAtTarget;

	// Token: 0x04001EBE RID: 7870
	public bool LookAtPlayer;

	// Token: 0x04001EBF RID: 7871
	public Transform Default;

	// Token: 0x04001EC0 RID: 7872
	public Transform Head;
}
