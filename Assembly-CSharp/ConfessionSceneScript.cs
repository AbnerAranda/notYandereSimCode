using System;
using UnityEngine;

// Token: 0x02000246 RID: 582
public class ConfessionSceneScript : MonoBehaviour
{
	// Token: 0x06001290 RID: 4752 RVA: 0x0004F6ED File Offset: 0x0004D8ED
	private void Start()
	{
		Time.timeScale = 1f;
	}

	// Token: 0x06001291 RID: 4753 RVA: 0x0008AE24 File Offset: 0x00089024
	private void Update()
	{
		if (this.Phase == 1)
		{
			this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, Mathf.MoveTowards(this.Darkness.color.a, 1f, Time.deltaTime));
			this.Panel.alpha = Mathf.MoveTowards(this.Panel.alpha, 0f, Time.deltaTime);
			this.Jukebox.Volume = Mathf.MoveTowards(this.Jukebox.Volume, 0f, Time.deltaTime);
			if (this.Darkness.color.a == 1f)
			{
				this.Timer += Time.deltaTime;
				if (this.Timer > 1f)
				{
					this.BloomEffect.bloomIntensity = 1f;
					this.BloomEffect.bloomThreshhold = 0f;
					this.BloomEffect.bloomBlurIterations = 1;
					this.Suitor = this.StudentManager.Students[this.LoveManager.SuitorID];
					this.Rival = this.StudentManager.Students[this.LoveManager.RivalID];
					this.Rival.transform.position = this.RivalSpot.position;
					this.Rival.transform.eulerAngles = this.RivalSpot.eulerAngles;
					this.Suitor.Cosmetic.MyRenderer.materials[this.Suitor.Cosmetic.FaceID].SetFloat("_BlendAmount", 1f);
					this.Suitor.transform.eulerAngles = this.StudentManager.SuitorConfessionSpot.eulerAngles;
					this.Suitor.transform.position = this.StudentManager.SuitorConfessionSpot.position;
					this.Suitor.CharacterAnimation.Play(this.Suitor.IdleAnim);
					this.MythBlossoms.emission.rateOverTime = 100f;
					this.HeartBeatCamera.SetActive(false);
					this.ConfessionBG.SetActive(true);
					base.GetComponent<AudioSource>().Play();
					this.MainCamera.position = this.CameraDestinations[1].position;
					this.MainCamera.eulerAngles = this.CameraDestinations[1].eulerAngles;
					this.Timer = 0f;
					this.Phase++;
				}
			}
		}
		else if (this.Phase == 2)
		{
			this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, Mathf.MoveTowards(this.Darkness.color.a, 0f, Time.deltaTime));
			if (this.Darkness.color.a == 0f)
			{
				if (!this.ShowLabel)
				{
					this.Label.color = new Color(this.Label.color.r, this.Label.color.g, this.Label.color.b, Mathf.MoveTowards(this.Label.color.a, 0f, Time.deltaTime));
					if (this.Label.color.a == 0f)
					{
						if (this.TextPhase < 5)
						{
							this.MainCamera.position = this.CameraDestinations[this.TextPhase].position;
							this.MainCamera.eulerAngles = this.CameraDestinations[this.TextPhase].eulerAngles;
							if (this.TextPhase == 4 && !this.Kissing)
							{
								ParticleSystem.EmissionModule emission = this.Suitor.Hearts.emission;
								emission.enabled = true;
								emission.rateOverTime = 10f;
								this.Suitor.Hearts.Play();
								ParticleSystem.EmissionModule emission2 = this.Rival.Hearts.emission;
								emission2.enabled = true;
								emission2.rateOverTime = 10f;
								this.Rival.Hearts.Play();
								this.Suitor.Character.transform.localScale = new Vector3(1f, 1f, 1f);
								this.Suitor.CharacterAnimation.Play("kiss_00");
								this.Suitor.transform.position = this.KissSpot.position;
								this.Rival.CharacterAnimation[this.Rival.ShyAnim].weight = 0f;
								this.Rival.CharacterAnimation.Play("f02_kiss_00");
								this.Kissing = true;
							}
							this.Label.text = this.Text[this.TextPhase];
							this.ShowLabel = true;
						}
						else
						{
							this.Jingle.Play();
							this.Phase++;
						}
					}
				}
				else
				{
					this.Label.color = new Color(this.Label.color.r, this.Label.color.g, this.Label.color.b, Mathf.MoveTowards(this.Label.color.a, 1f, Time.deltaTime));
					if (this.Label.color.a == 1f)
					{
						if (!this.PromptBar.Show)
						{
							this.PromptBar.ClearButtons();
							this.PromptBar.Label[0].text = "Continue";
							this.PromptBar.UpdateButtons();
							this.PromptBar.Show = true;
						}
						if (Input.GetButtonDown("A"))
						{
							this.TextPhase++;
							this.ShowLabel = false;
						}
					}
				}
			}
		}
		else if (this.Phase == 3)
		{
			this.LetterTimer += Time.deltaTime;
			if (this.LetterTimer > 0.1f && this.LetterID < this.Letters.Length)
			{
				this.Letters[this.LetterID].SetActive(true);
				this.LetterTimer = 0f;
				this.LetterID++;
			}
			if (this.LetterTimer > 5f)
			{
				this.Phase++;
			}
		}
		else if (this.Phase == 4)
		{
			this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, Mathf.MoveTowards(this.Darkness.color.a, 1f, Time.deltaTime));
			if (this.Darkness.color.a == 1f)
			{
				this.Timer += Time.deltaTime;
				if (this.Timer > 1f)
				{
					DatingGlobals.SuitorProgress = 2;
					this.Suitor.Character.transform.localScale = new Vector3(0.94f, 0.94f, 0.94f);
					this.PromptBar.ClearButtons();
					this.PromptBar.UpdateButtons();
					this.PromptBar.Show = false;
					this.ConfessionBG.SetActive(false);
					this.Yandere.FixCamera();
					this.Phase++;
				}
			}
		}
		else
		{
			this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, Mathf.MoveTowards(this.Darkness.color.a, 0f, Time.deltaTime));
			this.Panel.alpha = Mathf.MoveTowards(this.Panel.alpha, 1f, Time.deltaTime);
			if (this.Darkness.color.a == 0f)
			{
				this.StudentManager.ComeBack();
				this.Suitor.enabled = false;
				this.Suitor.Prompt.enabled = false;
				this.Suitor.Pathfinding.canMove = false;
				this.Suitor.Pathfinding.canSearch = false;
				this.Rival.enabled = false;
				this.Rival.Prompt.enabled = false;
				this.Rival.Pathfinding.canMove = false;
				this.Rival.Pathfinding.canSearch = false;
				this.Yandere.RPGCamera.enabled = true;
				this.Yandere.CanMove = true;
				this.HeartBeatCamera.SetActive(true);
				this.MythBlossoms.emission.rateOverTime = 20f;
				this.Clock.StopTime = false;
				base.enabled = false;
				this.Suitor.CoupleID = this.LoveManager.SuitorID;
				this.Rival.CoupleID = this.LoveManager.RivalID;
				this.Suitor.CharacterAnimation.CrossFade("holdHandsLoop_00");
				this.Rival.CharacterAnimation.CrossFade("f02_holdHandsLoop_00");
			}
		}
		if (this.Kissing)
		{
			if (this.Suitor.CharacterAnimation["kiss_00"].time >= this.Suitor.CharacterAnimation["kiss_00"].length * 0.66666f)
			{
				this.Suitor.Character.transform.localScale = Vector3.Lerp(this.Suitor.Character.transform.localScale, new Vector3(0.94f, 0.94f, 0.94f), Time.deltaTime);
			}
			if (this.Suitor.CharacterAnimation["kiss_00"].time >= this.Suitor.CharacterAnimation["kiss_00"].length)
			{
				this.Rival.CharacterAnimation.CrossFade("f02_introHoldHands_00");
				this.Suitor.CharacterAnimation.CrossFade("introHoldHands_00");
				this.Kissing = false;
				this.MoveSuitor = true;
				return;
			}
		}
		else if (this.Suitor != null)
		{
			this.Suitor.Character.transform.localScale = Vector3.Lerp(this.Suitor.Character.transform.localScale, new Vector3(0.94f, 0.94f, 0.94f), Time.deltaTime);
			if (this.MoveSuitor)
			{
				this.Speed += Time.deltaTime;
				this.Suitor.Character.transform.position = Vector3.Lerp(this.Suitor.Character.transform.position, new Vector3(0f, 6.6f, 119.2f), Time.deltaTime * this.Speed);
			}
		}
	}

	// Token: 0x040016B8 RID: 5816
	public Transform[] CameraDestinations;

	// Token: 0x040016B9 RID: 5817
	public StudentManagerScript StudentManager;

	// Token: 0x040016BA RID: 5818
	public LoveManagerScript LoveManager;

	// Token: 0x040016BB RID: 5819
	public PromptBarScript PromptBar;

	// Token: 0x040016BC RID: 5820
	public JukeboxScript Jukebox;

	// Token: 0x040016BD RID: 5821
	public YandereScript Yandere;

	// Token: 0x040016BE RID: 5822
	public ClockScript Clock;

	// Token: 0x040016BF RID: 5823
	public Bloom BloomEffect;

	// Token: 0x040016C0 RID: 5824
	public StudentScript Suitor;

	// Token: 0x040016C1 RID: 5825
	public StudentScript Rival;

	// Token: 0x040016C2 RID: 5826
	public ParticleSystem MythBlossoms;

	// Token: 0x040016C3 RID: 5827
	public GameObject HeartBeatCamera;

	// Token: 0x040016C4 RID: 5828
	public GameObject ConfessionBG;

	// Token: 0x040016C5 RID: 5829
	public Transform MainCamera;

	// Token: 0x040016C6 RID: 5830
	public Transform RivalSpot;

	// Token: 0x040016C7 RID: 5831
	public Transform KissSpot;

	// Token: 0x040016C8 RID: 5832
	public string[] Text;

	// Token: 0x040016C9 RID: 5833
	public GameObject[] Letters;

	// Token: 0x040016CA RID: 5834
	public UISprite Darkness;

	// Token: 0x040016CB RID: 5835
	public UILabel Label;

	// Token: 0x040016CC RID: 5836
	public UIPanel Panel;

	// Token: 0x040016CD RID: 5837
	public AudioSource Jingle;

	// Token: 0x040016CE RID: 5838
	public bool MoveSuitor;

	// Token: 0x040016CF RID: 5839
	public bool ShowLabel;

	// Token: 0x040016D0 RID: 5840
	public bool Kissing;

	// Token: 0x040016D1 RID: 5841
	public int TextPhase = 1;

	// Token: 0x040016D2 RID: 5842
	public int LetterID = 1;

	// Token: 0x040016D3 RID: 5843
	public int Phase = 1;

	// Token: 0x040016D4 RID: 5844
	public float LetterTimer = 0.1f;

	// Token: 0x040016D5 RID: 5845
	public float Speed;

	// Token: 0x040016D6 RID: 5846
	public float Timer;
}
