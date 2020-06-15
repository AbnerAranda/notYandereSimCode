using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020003FF RID: 1023
public class StreetManagerScript : MonoBehaviour
{
	// Token: 0x06001B17 RID: 6935 RVA: 0x00111AA0 File Offset: 0x0010FCA0
	private void Start()
	{
		this.MaidAnimation["f02_faceCouncilGrace_00"].layer = 1;
		this.MaidAnimation.Play("f02_faceCouncilGrace_00");
		this.MaidAnimation["f02_faceCouncilGrace_00"].weight = 1f;
		this.Gossip1["f02_socialSit_00"].layer = 1;
		this.Gossip1.Play("f02_socialSit_00");
		this.Gossip1["f02_socialSit_00"].weight = 1f;
		this.Gossip2["f02_socialSit_00"].layer = 1;
		this.Gossip2.Play("f02_socialSit_00");
		this.Gossip2["f02_socialSit_00"].weight = 1f;
		for (int i = 2; i < 5; i++)
		{
			this.Civilian[i]["f02_smile_00"].layer = 1;
			this.Civilian[i].Play("f02_smile_00");
			this.Civilian[i]["f02_smile_00"].weight = 1f;
		}
		this.Darkness.color = new Color(1f, 1f, 1f, 1f);
		this.CurrentlyActiveJukebox = this.JukeboxNight;
		this.Alpha = 1f;
		if (StudentGlobals.GetStudentDead(30) || StudentGlobals.GetStudentKidnapped(30) || StudentGlobals.GetStudentBroken(81))
		{
			this.Couple.SetActive(false);
		}
		this.Sunlight.shadows = LightShadows.None;
	}

	// Token: 0x06001B18 RID: 6936 RVA: 0x00111C34 File Offset: 0x0010FE34
	private void Update()
	{
		if (Input.GetKeyDown("m"))
		{
			PlayerGlobals.Money += 1f;
			this.Clock.UpdateMoneyLabel();
			if (this.JukeboxNight.isPlaying)
			{
				this.JukeboxNight.Stop();
				this.JukeboxDay.Stop();
			}
			else
			{
				this.JukeboxNight.Play();
				this.JukeboxDay.Stop();
			}
		}
		if (Input.GetKeyDown("f"))
		{
			PlayerGlobals.FakeID = !PlayerGlobals.FakeID;
			this.StreetShopInterface.UpdateFakeID();
		}
		this.Timer += Time.deltaTime;
		if (this.Timer > 0.5f)
		{
			if (this.Alpha == 1f)
			{
				this.JukeboxNight.volume = 0.5f;
				this.JukeboxNight.Play();
				this.JukeboxDay.volume = 0f;
				this.JukeboxDay.Play();
			}
			if (!this.FadeOut)
			{
				this.Alpha = Mathf.MoveTowards(this.Alpha, 0f, Time.deltaTime);
				this.Darkness.color = new Color(1f, 1f, 1f, this.Alpha);
			}
			else
			{
				this.Alpha = Mathf.MoveTowards(this.Alpha, 1f, Time.deltaTime);
				this.CurrentlyActiveJukebox.volume = (1f - this.Alpha) * 0.5f;
				if (this.GoToCafe)
				{
					this.Darkness.color = new Color(1f, 1f, 1f, this.Alpha);
					if (this.Alpha == 1f)
					{
						SceneManager.LoadScene("MaidMenuScene");
					}
				}
				else
				{
					this.Darkness.color = new Color(0f, 0f, 0f, this.Alpha);
					if (this.Alpha == 1f)
					{
						SceneManager.LoadScene("HomeScene");
					}
				}
			}
		}
		if (!this.FadeOut && !this.BinocularCamera.gameObject.activeInHierarchy)
		{
			if (Vector3.Distance(this.Yandere.position, this.Yakuza.transform.position) > 5f)
			{
				this.DesiredValue = 0.5f;
			}
			else
			{
				this.DesiredValue = Vector3.Distance(this.Yandere.position, this.Yakuza.transform.position) * 0.1f;
			}
			if (this.Day)
			{
				this.JukeboxDay.volume = Mathf.Lerp(this.JukeboxDay.volume, this.DesiredValue, Time.deltaTime * 10f);
				this.JukeboxNight.volume = Mathf.Lerp(this.JukeboxNight.volume, 0f, Time.deltaTime * 10f);
			}
			else
			{
				this.JukeboxDay.volume = Mathf.Lerp(this.JukeboxDay.volume, 0f, Time.deltaTime * 10f);
				this.JukeboxNight.volume = Mathf.Lerp(this.JukeboxNight.volume, this.DesiredValue, Time.deltaTime * 10f);
			}
			if (Vector3.Distance(this.Yandere.position, this.Yakuza.transform.position) < 1f && !this.Threatened)
			{
				this.Threatened = true;
				this.Yakuza.Play();
			}
		}
		if (Input.GetKeyDown("space"))
		{
			this.Day = !this.Day;
			if (this.Day)
			{
				this.Clock.HourLabel.text = "12:00 PM";
				this.Sunlight.shadows = LightShadows.Soft;
			}
			else
			{
				this.Clock.HourLabel.text = "8:00 PM";
				this.Sunlight.shadows = LightShadows.None;
			}
		}
		if (this.Day)
		{
			this.CurrentlyActiveJukebox = this.JukeboxDay;
			this.Rotation = Mathf.Lerp(this.Rotation, 45f, Time.deltaTime * 10f);
			this.StarAlpha = Mathf.Lerp(this.StarAlpha, 0f, Time.deltaTime * 10f);
		}
		else
		{
			this.CurrentlyActiveJukebox = this.JukeboxNight;
			this.Rotation = Mathf.Lerp(this.Rotation, -45f, Time.deltaTime * 10f);
			this.StarAlpha = Mathf.Lerp(this.StarAlpha, 1f, Time.deltaTime * 10f);
		}
		this.Sun.transform.eulerAngles = new Vector3(this.Rotation, this.Rotation, 0f);
		this.Stars.material.SetColor("_TintColor", new Color(1f, 1f, 1f, this.StarAlpha));
	}

	// Token: 0x06001B19 RID: 6937 RVA: 0x0011210E File Offset: 0x0011030E
	private void LateUpdate()
	{
		this.Hips.LookAt(this.BinocularCamera.position);
	}

	// Token: 0x04002C1C RID: 11292
	public StreetShopInterfaceScript StreetShopInterface;

	// Token: 0x04002C1D RID: 11293
	public Transform BinocularCamera;

	// Token: 0x04002C1E RID: 11294
	public Transform Yandere;

	// Token: 0x04002C1F RID: 11295
	public Transform Hips;

	// Token: 0x04002C20 RID: 11296
	public Transform Sun;

	// Token: 0x04002C21 RID: 11297
	public Animation MaidAnimation;

	// Token: 0x04002C22 RID: 11298
	public Animation Gossip1;

	// Token: 0x04002C23 RID: 11299
	public Animation Gossip2;

	// Token: 0x04002C24 RID: 11300
	public AudioSource CurrentlyActiveJukebox;

	// Token: 0x04002C25 RID: 11301
	public AudioSource JukeboxNight;

	// Token: 0x04002C26 RID: 11302
	public AudioSource JukeboxDay;

	// Token: 0x04002C27 RID: 11303
	public AudioSource Yakuza;

	// Token: 0x04002C28 RID: 11304
	public HomeClockScript Clock;

	// Token: 0x04002C29 RID: 11305
	public Animation[] Civilian;

	// Token: 0x04002C2A RID: 11306
	public GameObject Couple;

	// Token: 0x04002C2B RID: 11307
	public UISprite Darkness;

	// Token: 0x04002C2C RID: 11308
	public Renderer Stars;

	// Token: 0x04002C2D RID: 11309
	public Light Sunlight;

	// Token: 0x04002C2E RID: 11310
	public bool Threatened;

	// Token: 0x04002C2F RID: 11311
	public bool GoToCafe;

	// Token: 0x04002C30 RID: 11312
	public bool FadeOut;

	// Token: 0x04002C31 RID: 11313
	public bool Day;

	// Token: 0x04002C32 RID: 11314
	public float Rotation;

	// Token: 0x04002C33 RID: 11315
	public float Timer;

	// Token: 0x04002C34 RID: 11316
	public float DesiredValue;

	// Token: 0x04002C35 RID: 11317
	public float StarAlpha;

	// Token: 0x04002C36 RID: 11318
	public float Alpha;
}
