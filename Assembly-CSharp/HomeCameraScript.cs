using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020002E8 RID: 744
public class HomeCameraScript : MonoBehaviour
{
	// Token: 0x0600171D RID: 5917 RVA: 0x000C3E74 File Offset: 0x000C2074
	public void Start()
	{
		this.Button.color = new Color(this.Button.color.r, this.Button.color.g, this.Button.color.b, 0f);
		this.Focus.position = this.Target.position;
		base.transform.position = this.Destination.position;
		if (HomeGlobals.Night)
		{
			this.CeilingLight.SetActive(true);
			this.SenpaiLight.SetActive(true);
			this.NightLight.SetActive(true);
			this.DayLight.SetActive(false);
			this.Triggers[7].Disable();
			this.BasementJukebox.clip = this.NightBasement;
			this.RoomJukebox.clip = this.NightRoom;
			this.PlayMusic();
			this.PantiesMangaLabel.text = "Read Manga";
		}
		else
		{
			this.BasementJukebox.Play();
			this.RoomJukebox.Play();
			this.ComputerScreen.SetActive(false);
			this.Triggers[2].Disable();
			this.Triggers[3].Disable();
			this.Triggers[5].Disable();
			this.Triggers[9].Disable();
		}
		if (SchoolGlobals.KidnapVictim == 0)
		{
			this.RopeGroup.SetActive(false);
			this.Tripod.SetActive(false);
			this.Victim.SetActive(false);
			this.Triggers[10].Disable();
		}
		else
		{
			int kidnapVictim = SchoolGlobals.KidnapVictim;
			if (StudentGlobals.GetStudentArrested(kidnapVictim) || StudentGlobals.GetStudentDead(kidnapVictim))
			{
				this.RopeGroup.SetActive(false);
				this.Victim.SetActive(false);
				this.Triggers[10].Disable();
			}
		}
		if (GameGlobals.LoveSick)
		{
			this.LoveSickColorSwap();
		}
		Time.timeScale = 1f;
		this.HairLock.material.color = this.SenpaiCosmetic.ColorValue;
		if (SchoolGlobals.SchoolAtmosphere > 1f)
		{
			SchoolGlobals.SchoolAtmosphere = 1f;
		}
	}

	// Token: 0x0600171E RID: 5918 RVA: 0x000C4088 File Offset: 0x000C2288
	private void LateUpdate()
	{
		if (this.HomeYandere.transform.position.y > -5f)
		{
			Transform transform = this.Destinations[0];
			transform.position = new Vector3(-this.HomeYandere.transform.position.x, transform.position.y, transform.position.z);
		}
		this.Focus.position = Vector3.Lerp(this.Focus.position, this.Target.position, Time.deltaTime * 10f);
		base.transform.position = Vector3.Lerp(base.transform.position, this.Destination.position, Time.deltaTime * 10f);
		base.transform.LookAt(this.Focus.position);
		if (this.ID != 11 && Input.GetButtonDown("A") && this.HomeYandere.CanMove && this.ID != 0)
		{
			this.Destination = this.Destinations[this.ID];
			this.Target = this.Targets[this.ID];
			this.HomeWindows[this.ID].Show = true;
			this.HomeYandere.CanMove = false;
			if (this.ID == 1 || this.ID == 8)
			{
				this.HomeExit.enabled = true;
			}
			else if (this.ID == 2)
			{
				this.HomeSleep.enabled = true;
			}
			else if (this.ID == 3)
			{
				this.HomeInternet.enabled = true;
			}
			else if (this.ID == 4)
			{
				this.CorkboardLabel.SetActive(false);
				this.HomeCorkboard.enabled = true;
				this.LoadingScreen.SetActive(true);
				this.HomeYandere.gameObject.SetActive(false);
			}
			else if (this.ID == 5)
			{
				this.HomeYandere.enabled = false;
				this.Controller.transform.localPosition = new Vector3(0.1245f, 0.032f, 0f);
				this.HomeYandere.transform.position = new Vector3(1f, 0f, 0f);
				this.HomeYandere.transform.eulerAngles = new Vector3(0f, 90f, 0f);
				this.HomeYandere.Character.GetComponent<Animation>().Play("f02_gaming_00");
				this.PromptBar.ClearButtons();
				this.PromptBar.Label[0].text = "Play";
				this.PromptBar.Label[1].text = "Back";
				this.PromptBar.Label[4].text = "Select";
				this.PromptBar.UpdateButtons();
				this.PromptBar.Show = true;
			}
			else if (this.ID == 6)
			{
				this.HomeSenpaiShrine.enabled = true;
				this.HomeYandere.gameObject.SetActive(false);
			}
			else if (this.ID == 7)
			{
				this.HomePantyChanger.enabled = true;
			}
			else if (this.ID == 9)
			{
				this.HomeManga.enabled = true;
			}
			else if (this.ID == 10)
			{
				this.PromptBar.ClearButtons();
				this.PromptBar.Label[0].text = "Accept";
				this.PromptBar.Label[1].text = "Back";
				this.PromptBar.UpdateButtons();
				this.PromptBar.Show = true;
				this.HomePrisoner.UpdateDesc();
				this.HomeYandere.gameObject.SetActive(false);
			}
			else if (this.ID == 12)
			{
				this.HomeAnime.enabled = true;
			}
		}
		if (this.Destination == this.Destinations[0])
		{
			this.Vignette.intensity = ((this.HomeYandere.transform.position.y > -1f) ? Mathf.MoveTowards(this.Vignette.intensity, 1f, Time.deltaTime) : Mathf.MoveTowards(this.Vignette.intensity, 5f, Time.deltaTime * 5f));
			this.Vignette.chromaticAberration = Mathf.MoveTowards(this.Vignette.chromaticAberration, 1f, Time.deltaTime);
			this.Vignette.blur = Mathf.MoveTowards(this.Vignette.blur, 1f, Time.deltaTime);
		}
		else
		{
			this.Vignette.intensity = ((this.HomeYandere.transform.position.y > -1f) ? Mathf.MoveTowards(this.Vignette.intensity, 0f, Time.deltaTime) : Mathf.MoveTowards(this.Vignette.intensity, 0f, Time.deltaTime * 5f));
			this.Vignette.chromaticAberration = Mathf.MoveTowards(this.Vignette.chromaticAberration, 0f, Time.deltaTime);
			this.Vignette.blur = Mathf.MoveTowards(this.Vignette.blur, 0f, Time.deltaTime);
		}
		this.Button.color = new Color(this.Button.color.r, this.Button.color.g, this.Button.color.b, Mathf.MoveTowards(this.Button.color.a, (this.ID > 0 && this.HomeYandere.CanMove) ? 1f : 0f, Time.deltaTime * 10f));
		if (this.HomeDarkness.FadeOut)
		{
			this.BasementJukebox.volume = Mathf.MoveTowards(this.BasementJukebox.volume, 0f, Time.deltaTime);
			this.RoomJukebox.volume = Mathf.MoveTowards(this.RoomJukebox.volume, 0f, Time.deltaTime);
		}
		else if (this.HomeYandere.transform.position.y > -1f)
		{
			this.BasementJukebox.volume = Mathf.MoveTowards(this.BasementJukebox.volume, 0f, Time.deltaTime);
			this.RoomJukebox.volume = Mathf.MoveTowards(this.RoomJukebox.volume, 0.5f, Time.deltaTime);
		}
		else if (!this.Torturing)
		{
			this.BasementJukebox.volume = Mathf.MoveTowards(this.BasementJukebox.volume, 0.5f, Time.deltaTime);
			this.RoomJukebox.volume = Mathf.MoveTowards(this.RoomJukebox.volume, 0f, Time.deltaTime);
		}
		if (Input.GetKeyDown(KeyCode.Y))
		{
			TaskGlobals.SetTaskStatus(38, 1);
		}
		if (Input.GetKeyDown(KeyCode.M))
		{
			this.BasementJukebox.gameObject.SetActive(false);
			this.RoomJukebox.gameObject.SetActive(false);
		}
		if (Input.GetKeyDown(KeyCode.BackQuote))
		{
			HomeGlobals.Night = !HomeGlobals.Night;
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
		if (Input.GetKeyDown(KeyCode.Equals))
		{
			Time.timeScale += 1f;
		}
		if (Input.GetKeyDown(KeyCode.Minus) && Time.timeScale > 1f)
		{
			Time.timeScale -= 1f;
		}
	}

	// Token: 0x0600171F RID: 5919 RVA: 0x000C4829 File Offset: 0x000C2A29
	public void PlayMusic()
	{
		if (!YanvaniaGlobals.DraculaDefeated && !HomeGlobals.MiyukiDefeated)
		{
			if (!this.BasementJukebox.isPlaying)
			{
				this.BasementJukebox.Play();
			}
			if (!this.RoomJukebox.isPlaying)
			{
				this.RoomJukebox.Play();
			}
		}
	}

	// Token: 0x06001720 RID: 5920 RVA: 0x000C486C File Offset: 0x000C2A6C
	private void LoveSickColorSwap()
	{
		foreach (GameObject gameObject in UnityEngine.Object.FindObjectsOfType<GameObject>())
		{
			if (gameObject.transform.parent != this.PauseScreen && gameObject.transform.parent != this.PromptBarPanel)
			{
				UISprite component = gameObject.GetComponent<UISprite>();
				if (component != null && component.color != Color.black)
				{
					component.color = new Color(1f, 0f, 0f, component.color.a);
				}
				UILabel component2 = gameObject.GetComponent<UILabel>();
				if (component2 != null && component2.color != Color.black)
				{
					component2.color = new Color(1f, 0f, 0f, component2.color.a);
				}
			}
		}
		this.DayLight.GetComponent<Light>().color = new Color(0.5f, 0.5f, 0.5f, 1f);
		this.HomeDarkness.Sprite.color = Color.black;
		this.BasementJukebox.clip = this.HomeLoveSick;
		this.RoomJukebox.clip = this.HomeLoveSick;
		this.LoveSickCamera.SetActive(true);
		this.PlayMusic();
	}

	// Token: 0x04001F42 RID: 8002
	public HomeWindowScript[] HomeWindows;

	// Token: 0x04001F43 RID: 8003
	public HomeTriggerScript[] Triggers;

	// Token: 0x04001F44 RID: 8004
	public HomePantyChangerScript HomePantyChanger;

	// Token: 0x04001F45 RID: 8005
	public HomeSenpaiShrineScript HomeSenpaiShrine;

	// Token: 0x04001F46 RID: 8006
	public HomeVideoGamesScript HomeVideoGames;

	// Token: 0x04001F47 RID: 8007
	public HomeCorkboardScript HomeCorkboard;

	// Token: 0x04001F48 RID: 8008
	public HomeDarknessScript HomeDarkness;

	// Token: 0x04001F49 RID: 8009
	public HomeInternetScript HomeInternet;

	// Token: 0x04001F4A RID: 8010
	public HomePrisonerScript HomePrisoner;

	// Token: 0x04001F4B RID: 8011
	public HomeYandereScript HomeYandere;

	// Token: 0x04001F4C RID: 8012
	public HomeSleepScript HomeAnime;

	// Token: 0x04001F4D RID: 8013
	public HomeMangaScript HomeManga;

	// Token: 0x04001F4E RID: 8014
	public HomeSleepScript HomeSleep;

	// Token: 0x04001F4F RID: 8015
	public HomeExitScript HomeExit;

	// Token: 0x04001F50 RID: 8016
	public PromptBarScript PromptBar;

	// Token: 0x04001F51 RID: 8017
	public Vignetting Vignette;

	// Token: 0x04001F52 RID: 8018
	public UILabel PantiesMangaLabel;

	// Token: 0x04001F53 RID: 8019
	public UISprite Button;

	// Token: 0x04001F54 RID: 8020
	public GameObject CyberstalkWindow;

	// Token: 0x04001F55 RID: 8021
	public GameObject ComputerScreen;

	// Token: 0x04001F56 RID: 8022
	public GameObject CorkboardLabel;

	// Token: 0x04001F57 RID: 8023
	public GameObject LoveSickCamera;

	// Token: 0x04001F58 RID: 8024
	public GameObject LoadingScreen;

	// Token: 0x04001F59 RID: 8025
	public GameObject CeilingLight;

	// Token: 0x04001F5A RID: 8026
	public GameObject SenpaiLight;

	// Token: 0x04001F5B RID: 8027
	public GameObject Controller;

	// Token: 0x04001F5C RID: 8028
	public GameObject NightLight;

	// Token: 0x04001F5D RID: 8029
	public GameObject RopeGroup;

	// Token: 0x04001F5E RID: 8030
	public GameObject DayLight;

	// Token: 0x04001F5F RID: 8031
	public GameObject Tripod;

	// Token: 0x04001F60 RID: 8032
	public GameObject Victim;

	// Token: 0x04001F61 RID: 8033
	public Transform Destination;

	// Token: 0x04001F62 RID: 8034
	public Transform Target;

	// Token: 0x04001F63 RID: 8035
	public Transform Focus;

	// Token: 0x04001F64 RID: 8036
	public Transform[] Destinations;

	// Token: 0x04001F65 RID: 8037
	public Transform[] Targets;

	// Token: 0x04001F66 RID: 8038
	public int ID;

	// Token: 0x04001F67 RID: 8039
	public AudioSource BasementJukebox;

	// Token: 0x04001F68 RID: 8040
	public AudioSource RoomJukebox;

	// Token: 0x04001F69 RID: 8041
	public AudioClip NightBasement;

	// Token: 0x04001F6A RID: 8042
	public AudioClip NightRoom;

	// Token: 0x04001F6B RID: 8043
	public AudioClip HomeLoveSick;

	// Token: 0x04001F6C RID: 8044
	public bool Torturing;

	// Token: 0x04001F6D RID: 8045
	public CosmeticScript SenpaiCosmetic;

	// Token: 0x04001F6E RID: 8046
	public Renderer HairLock;

	// Token: 0x04001F6F RID: 8047
	public Transform PromptBarPanel;

	// Token: 0x04001F70 RID: 8048
	public Transform PauseScreen;
}
