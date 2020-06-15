using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020002EE RID: 750
public class HomeDarknessScript : MonoBehaviour
{
	// Token: 0x06001732 RID: 5938 RVA: 0x000C508C File Offset: 0x000C328C
	private void Start()
	{
		if (GameGlobals.LoveSick)
		{
			this.Sprite.color = new Color(0f, 0f, 0f, 1f);
		}
		this.Sprite.color = new Color(this.Sprite.color.r, this.Sprite.color.g, this.Sprite.color.b, 1f);
	}

	// Token: 0x06001733 RID: 5939 RVA: 0x000C510C File Offset: 0x000C330C
	private void Update()
	{
		if (this.FadeOut)
		{
			this.Sprite.color = new Color(this.Sprite.color.r, this.Sprite.color.g, this.Sprite.color.b, this.Sprite.color.a + Time.deltaTime * (this.FadeSlow ? 0.2f : 1f));
			if (this.Sprite.color.a >= 1f)
			{
				if (this.HomeCamera.ID == 2)
				{
					SceneManager.LoadScene("CalendarScene");
					return;
				}
				if (this.HomeCamera.ID == 3)
				{
					if (this.Cyberstalking)
					{
						SceneManager.LoadScene("CalendarScene");
						return;
					}
					SceneManager.LoadScene("YancordScene");
					return;
				}
				else if (this.HomeCamera.ID == 5)
				{
					if (this.HomeVideoGames.ID == 1)
					{
						SceneManager.LoadScene("YanvaniaTitleScene");
						return;
					}
					SceneManager.LoadScene("MiyukiTitleScene");
					return;
				}
				else
				{
					if (this.HomeCamera.ID == 9)
					{
						SceneManager.LoadScene("CalendarScene");
						return;
					}
					if (this.HomeCamera.ID == 10)
					{
						StudentGlobals.SetStudentKidnapped(SchoolGlobals.KidnapVictim, false);
						StudentGlobals.SetStudentSlave(SchoolGlobals.KidnapVictim);
						this.CheckForOsanaThursday();
						return;
					}
					if (this.HomeCamera.ID == 11)
					{
						EventGlobals.KidnapConversation = true;
						SceneManager.LoadScene("PhoneScene");
						return;
					}
					if (this.HomeCamera.ID == 12)
					{
						SceneManager.LoadScene("LifeNoteScene");
						return;
					}
					if (this.HomeExit.ID == 1)
					{
						this.CheckForOsanaThursday();
						return;
					}
					if (this.HomeExit.ID == 2)
					{
						SceneManager.LoadScene("StreetScene");
						return;
					}
					if (this.HomeExit.ID == 3)
					{
						if (this.HomeYandere.transform.position.y > -5f)
						{
							this.HomeYandere.transform.position = new Vector3(-2f, -10f, -2.75f);
							this.HomeYandere.transform.eulerAngles = new Vector3(0f, 90f, 0f);
							this.HomeYandere.CanMove = true;
							this.FadeOut = false;
							this.HomeCamera.Destinations[0].position = new Vector3(2.425f, -8f, 0f);
							this.HomeCamera.Destination = this.HomeCamera.Destinations[0];
							this.HomeCamera.transform.position = this.HomeCamera.Destination.position;
							this.HomeCamera.Target = this.HomeCamera.Targets[0];
							this.HomeCamera.Focus.position = this.HomeCamera.Target.position;
							this.BasementLabel.text = "Upstairs";
							this.HomeCamera.DayLight.SetActive(true);
							this.HomeCamera.DayLight.GetComponent<Light>().intensity = 0.66666f;
							Physics.SyncTransforms();
							return;
						}
						this.HomeYandere.transform.position = new Vector3(-1.6f, 0f, -1.6f);
						this.HomeYandere.transform.eulerAngles = new Vector3(0f, 45f, 0f);
						this.HomeYandere.CanMove = true;
						this.FadeOut = false;
						this.HomeCamera.Destinations[0].position = new Vector3(-2.0615f, 2f, 2.418f);
						this.HomeCamera.Destination = this.HomeCamera.Destinations[0];
						this.HomeCamera.transform.position = this.HomeCamera.Destination.position;
						this.HomeCamera.Target = this.HomeCamera.Targets[0];
						this.HomeCamera.Focus.position = this.HomeCamera.Target.position;
						this.BasementLabel.text = "Basement";
						if (HomeGlobals.Night)
						{
							this.HomeCamera.DayLight.SetActive(false);
						}
						this.HomeCamera.DayLight.GetComponent<Light>().intensity = 2f;
						Physics.SyncTransforms();
						return;
					}
					else if (this.HomeExit.ID == 4)
					{
						SceneManager.LoadScene("StalkerHouseScene");
						return;
					}
				}
			}
		}
		else
		{
			this.Sprite.color = new Color(this.Sprite.color.r, this.Sprite.color.g, this.Sprite.color.b, this.Sprite.color.a - Time.deltaTime);
			if (this.Sprite.color.a < 0f)
			{
				this.Sprite.color = new Color(this.Sprite.color.r, this.Sprite.color.g, this.Sprite.color.b, 0f);
			}
		}
	}

	// Token: 0x06001734 RID: 5940 RVA: 0x000C5639 File Offset: 0x000C3839
	private void CheckForOsanaThursday()
	{
		Debug.Log("Time to check if we need to display the Osana-walks-to-school cutscene...");
		if (this.InputDevice.Type == InputDeviceType.Gamepad)
		{
			PlayerGlobals.UsingGamepad = true;
		}
		else
		{
			PlayerGlobals.UsingGamepad = false;
		}
		SceneManager.LoadScene("LoadingScene");
	}

	// Token: 0x04001F87 RID: 8071
	public HomeVideoGamesScript HomeVideoGames;

	// Token: 0x04001F88 RID: 8072
	public HomeYandereScript HomeYandere;

	// Token: 0x04001F89 RID: 8073
	public HomeCameraScript HomeCamera;

	// Token: 0x04001F8A RID: 8074
	public HomeExitScript HomeExit;

	// Token: 0x04001F8B RID: 8075
	public InputDeviceScript InputDevice;

	// Token: 0x04001F8C RID: 8076
	public UILabel BasementLabel;

	// Token: 0x04001F8D RID: 8077
	public UISprite Sprite;

	// Token: 0x04001F8E RID: 8078
	public bool Cyberstalking;

	// Token: 0x04001F8F RID: 8079
	public bool FadeSlow;

	// Token: 0x04001F90 RID: 8080
	public bool FadeOut;
}
