using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x0200035F RID: 863
public class PhoneScript : MonoBehaviour
{
	// Token: 0x060018D4 RID: 6356 RVA: 0x000E5D38 File Offset: 0x000E3F38
	private void Start()
	{
		this.Buttons.localPosition = new Vector3(this.Buttons.localPosition.x, -135f, this.Buttons.localPosition.z);
		this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, 1f);
		if (DateGlobals.Week > 1 && DateGlobals.Weekday == DayOfWeek.Sunday)
		{
			this.Darkness.color = new Color(0f, 0f, 0f, 0f);
		}
		if (EventGlobals.KidnapConversation)
		{
			this.VoiceClips = this.KidnapClip;
			this.Speaker = this.KidnapSpeaker;
			this.Text = this.KidnapText;
			this.Height = this.KidnapHeight;
			EventGlobals.BefriendConversation = true;
			EventGlobals.KidnapConversation = false;
		}
		else if (EventGlobals.BefriendConversation)
		{
			this.VoiceClips = this.BefriendClip;
			this.Speaker = this.BefriendSpeaker;
			this.Text = this.BefriendText;
			this.Height = this.BefriendHeight;
			EventGlobals.LivingRoom = true;
			EventGlobals.BefriendConversation = false;
		}
		else if (EventGlobals.OsanaConversation)
		{
			Debug.Log("Osana's text message conversation!");
			this.VoiceClips = this.OsanaMessages.OsanaClips;
			this.Speaker = this.OsanaMessages.OsanaSpeakers;
			this.Text = this.OsanaMessages.OsanaTexts;
			this.Height = this.OsanaMessages.OsanaHeights;
			EventGlobals.LivingRoom = true;
		}
		if (GameGlobals.LoveSick)
		{
			Camera.main.backgroundColor = Color.black;
			this.LoveSickColorSwap();
		}
		if (this.PostElimination && GameGlobals.NonlethalElimination)
		{
			this.VoiceClips[1] = this.NonlethalClip[1];
			this.VoiceClips[2] = this.NonlethalClip[2];
			this.VoiceClips[3] = this.NonlethalClip[3];
			this.Text[1] = this.NonlethalText[1];
			this.Text[2] = this.NonlethalText[2];
			this.Text[3] = this.NonlethalText[3];
			this.Height[1] = this.NonlethalHeight[1];
			this.Height[2] = this.NonlethalHeight[2];
			this.Height[3] = this.NonlethalHeight[3];
		}
	}

	// Token: 0x060018D5 RID: 6357 RVA: 0x000E5F9C File Offset: 0x000E419C
	private void Update()
	{
		if (!this.FadeOut)
		{
			if (this.Timer > 0f && this.Buttons.gameObject.activeInHierarchy)
			{
				this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, Mathf.MoveTowards(this.Darkness.color.a, 0f, Time.deltaTime));
				if (this.Darkness.color.a == 0f)
				{
					if (!this.Jukebox.isPlaying)
					{
						this.Jukebox.Play();
					}
					if (this.NewMessage == null)
					{
						this.SpawnMessage();
					}
				}
			}
			if (this.NewMessage != null)
			{
				this.Buttons.localPosition = new Vector3(this.Buttons.localPosition.x, Mathf.Lerp(this.Buttons.localPosition.y, 0f, Time.deltaTime * 10f), this.Buttons.localPosition.z);
				this.AutoTimer += Time.deltaTime;
				if ((this.Auto && this.AutoTimer > this.VoiceClips[this.ID].length + 1f) || Input.GetButtonDown("A"))
				{
					this.AutoTimer = 0f;
					if (this.ID < this.Text.Length - 1)
					{
						this.ID++;
						this.SpawnMessage();
					}
					else
					{
						this.Darkness.color = new Color(0f, 0f, 0f, 0f);
						this.FadeOut = true;
						if (!this.Buttons.gameObject.activeInHierarchy)
						{
							this.Darkness.color = new Color(0f, 0f, 0f, 1f);
						}
					}
				}
				if (Input.GetButtonDown("X"))
				{
					this.FadeOut = true;
				}
			}
		}
		else
		{
			this.Buttons.localPosition = new Vector3(this.Buttons.localPosition.x, Mathf.Lerp(this.Buttons.localPosition.y, -135f, Time.deltaTime * 10f), this.Buttons.localPosition.z);
			base.GetComponent<AudioSource>().volume = 1f - this.Darkness.color.a;
			this.Jukebox.volume = 1f - this.Darkness.color.a;
			if (this.Darkness.color.a >= 1f)
			{
				if (DateGlobals.Week == 2)
				{
					SceneManager.LoadScene("CreditsScene");
				}
				else if (DateGlobals.Weekday == DayOfWeek.Sunday)
				{
					SceneManager.LoadScene("OsanaWarningScene");
				}
				else if (!EventGlobals.BefriendConversation && !EventGlobals.LivingRoom)
				{
					SceneManager.LoadScene("CalendarScene");
				}
				else if (EventGlobals.LivingRoom)
				{
					SceneManager.LoadScene("LivingRoomScene");
				}
				else
				{
					SceneManager.LoadScene(SceneManager.GetActiveScene().name);
				}
			}
			this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, this.Darkness.color.a + Time.deltaTime);
		}
		this.Timer += Time.deltaTime;
	}

	// Token: 0x060018D6 RID: 6358 RVA: 0x000E6358 File Offset: 0x000E4558
	private void SpawnMessage()
	{
		if (this.NewMessage != null)
		{
			this.NewMessage.transform.parent = this.OldMessages;
			this.OldMessages.localPosition = new Vector3(this.OldMessages.localPosition.x, this.OldMessages.localPosition.y + (72f + (float)this.Height[this.ID] * 32f), this.OldMessages.localPosition.z);
		}
		AudioSource component = base.GetComponent<AudioSource>();
		component.clip = this.VoiceClips[this.ID];
		component.Play();
		if (this.Speaker[this.ID] == 1)
		{
			this.NewMessage = UnityEngine.Object.Instantiate<GameObject>(this.LeftMessage[this.Height[this.ID]]);
			this.NewMessage.transform.parent = this.Panel;
			this.NewMessage.transform.localPosition = new Vector3(-225f, -375f, 0f);
			this.NewMessage.transform.localScale = Vector3.zero;
		}
		else
		{
			this.NewMessage = UnityEngine.Object.Instantiate<GameObject>(this.RightMessage[this.Height[this.ID]]);
			this.NewMessage.transform.parent = this.Panel;
			this.NewMessage.transform.localPosition = new Vector3(225f, -375f, 0f);
			this.NewMessage.transform.localScale = Vector3.zero;
			if (this.Speaker == this.KidnapSpeaker && this.Height[this.ID] == 8)
			{
				this.NewMessage.GetComponent<TextMessageScript>().Attachment = true;
			}
		}
		if (this.Height[this.ID] == 9 && this.Speaker[this.ID] == 2)
		{
			this.Buttons.gameObject.SetActive(false);
			this.Darkness.enabled = true;
			this.Jukebox.Stop();
			this.Timer = -99999f;
		}
		this.AutoLimit = (float)(this.Height[this.ID] + 1);
		this.NewMessage.GetComponent<TextMessageScript>().Label.text = this.Text[this.ID];
	}

	// Token: 0x060018D7 RID: 6359 RVA: 0x000E65B0 File Offset: 0x000E47B0
	private void LoveSickColorSwap()
	{
		foreach (GameObject gameObject in UnityEngine.Object.FindObjectsOfType<GameObject>())
		{
			UISprite component = gameObject.GetComponent<UISprite>();
			if (component != null && component.color != Color.black && component.transform.parent)
			{
				component.color = new Color(1f, 0f, 0f, component.color.a);
			}
			UILabel component2 = gameObject.GetComponent<UILabel>();
			if (component2 != null && component2.color != Color.black)
			{
				component2.color = new Color(1f, 0f, 0f, component2.color.a);
			}
			this.Darkness.color = Color.black;
		}
	}

	// Token: 0x04002500 RID: 9472
	public OsanaTextMessageScript OsanaMessages;

	// Token: 0x04002501 RID: 9473
	public GameObject[] RightMessage;

	// Token: 0x04002502 RID: 9474
	public GameObject[] LeftMessage;

	// Token: 0x04002503 RID: 9475
	public AudioClip[] VoiceClips;

	// Token: 0x04002504 RID: 9476
	public GameObject NewMessage;

	// Token: 0x04002505 RID: 9477
	public AudioSource Jukebox;

	// Token: 0x04002506 RID: 9478
	public Transform OldMessages;

	// Token: 0x04002507 RID: 9479
	public Transform Buttons;

	// Token: 0x04002508 RID: 9480
	public Transform Panel;

	// Token: 0x04002509 RID: 9481
	public Vignetting Vignette;

	// Token: 0x0400250A RID: 9482
	public UISprite Darkness;

	// Token: 0x0400250B RID: 9483
	public UISprite Sprite;

	// Token: 0x0400250C RID: 9484
	public int[] Speaker;

	// Token: 0x0400250D RID: 9485
	public string[] Text;

	// Token: 0x0400250E RID: 9486
	public int[] Height;

	// Token: 0x0400250F RID: 9487
	public AudioClip[] KidnapClip;

	// Token: 0x04002510 RID: 9488
	public int[] KidnapSpeaker;

	// Token: 0x04002511 RID: 9489
	public string[] KidnapText;

	// Token: 0x04002512 RID: 9490
	public int[] KidnapHeight;

	// Token: 0x04002513 RID: 9491
	public AudioClip[] BefriendClip;

	// Token: 0x04002514 RID: 9492
	public int[] BefriendSpeaker;

	// Token: 0x04002515 RID: 9493
	public string[] BefriendText;

	// Token: 0x04002516 RID: 9494
	public int[] BefriendHeight;

	// Token: 0x04002517 RID: 9495
	public AudioClip[] NonlethalClip;

	// Token: 0x04002518 RID: 9496
	public string[] NonlethalText;

	// Token: 0x04002519 RID: 9497
	public int[] NonlethalHeight;

	// Token: 0x0400251A RID: 9498
	public bool PostElimination;

	// Token: 0x0400251B RID: 9499
	public bool FadeOut;

	// Token: 0x0400251C RID: 9500
	public bool Auto;

	// Token: 0x0400251D RID: 9501
	public float AutoLimit;

	// Token: 0x0400251E RID: 9502
	public float AutoTimer;

	// Token: 0x0400251F RID: 9503
	public float Timer;

	// Token: 0x04002520 RID: 9504
	public int ID;
}
