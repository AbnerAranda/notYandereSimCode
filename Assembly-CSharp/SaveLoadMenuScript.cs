using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020003C8 RID: 968
public class SaveLoadMenuScript : MonoBehaviour
{
	// Token: 0x06001A52 RID: 6738 RVA: 0x00102129 File Offset: 0x00100329
	public void Start()
	{
		if (GameGlobals.Profile == 0)
		{
			GameGlobals.Profile = 1;
		}
		this.Profile = GameGlobals.Profile;
		this.WarningWindow.SetActive(true);
		this.ConfirmWindow.SetActive(false);
		base.StartCoroutine(this.GetThumbnails());
	}

	// Token: 0x06001A53 RID: 6739 RVA: 0x00102168 File Offset: 0x00100368
	public void Update()
	{
		if (!this.ConfirmWindow.activeInHierarchy)
		{
			if (this.InputManager.TappedUp)
			{
				this.Row--;
				this.UpdateHighlight();
			}
			else if (this.InputManager.TappedDown)
			{
				this.Row++;
				this.UpdateHighlight();
			}
			if (this.InputManager.TappedLeft)
			{
				this.Column--;
				this.UpdateHighlight();
			}
			else if (this.InputManager.TappedRight)
			{
				this.Column++;
				this.UpdateHighlight();
			}
		}
		if (this.GrabScreenshot)
		{
			if (GameGlobals.Profile == 0)
			{
				GameGlobals.Profile = 1;
				this.Profile = 1;
			}
			this.PauseScreen.ScreenBlur.enabled = true;
			this.UICamera.enabled = true;
			this.StudentManager.Save();
			base.StartCoroutine(this.GetThumbnails());
			if (DateGlobals.Weekday == DayOfWeek.Monday)
			{
				PlayerPrefs.SetInt(string.Concat(new object[]
				{
					"Profile_",
					this.Profile,
					"_Slot_",
					this.Selected,
					"_Weekday"
				}), 1);
			}
			else if (DateGlobals.Weekday == DayOfWeek.Tuesday)
			{
				PlayerPrefs.SetInt(string.Concat(new object[]
				{
					"Profile_",
					this.Profile,
					"_Slot_",
					this.Selected,
					"_Weekday"
				}), 2);
			}
			else if (DateGlobals.Weekday == DayOfWeek.Wednesday)
			{
				PlayerPrefs.SetInt(string.Concat(new object[]
				{
					"Profile_",
					this.Profile,
					"_Slot_",
					this.Selected,
					"_Weekday"
				}), 3);
			}
			else if (DateGlobals.Weekday == DayOfWeek.Thursday)
			{
				PlayerPrefs.SetInt(string.Concat(new object[]
				{
					"Profile_",
					this.Profile,
					"_Slot_",
					this.Selected,
					"_Weekday"
				}), 4);
			}
			else if (DateGlobals.Weekday == DayOfWeek.Friday)
			{
				PlayerPrefs.SetInt(string.Concat(new object[]
				{
					"Profile_",
					this.Profile,
					"_Slot_",
					this.Selected,
					"_Weekday"
				}), 5);
			}
			this.GrabScreenshot = false;
		}
		if (this.WarningWindow.activeInHierarchy)
		{
			if (Input.GetButtonDown("A"))
			{
				this.WarningWindow.SetActive(false);
				return;
			}
			if (Input.GetButtonDown("B"))
			{
				this.PauseScreen.MainMenu.SetActive(true);
				this.PauseScreen.Sideways = false;
				this.PauseScreen.PressedB = true;
				base.gameObject.SetActive(false);
				this.PauseScreen.PromptBar.ClearButtons();
				this.PauseScreen.PromptBar.Label[0].text = "Accept";
				this.PauseScreen.PromptBar.Label[1].text = "Exit";
				this.PauseScreen.PromptBar.Label[4].text = "Choose";
				this.PauseScreen.PromptBar.UpdateButtons();
				this.PauseScreen.PromptBar.Show = true;
				return;
			}
		}
		else
		{
			if (Input.GetButtonDown("A"))
			{
				if (this.Loading)
				{
					if (this.DataLabels[this.Selected].text != "No Data")
					{
						if (!this.ConfirmWindow.activeInHierarchy)
						{
							this.AreYouSureLabel.text = "Are you sure you'd like to load?";
							this.ConfirmWindow.SetActive(true);
						}
						else if (this.DataLabels[this.Selected].text != "No Data")
						{
							PlayerPrefs.SetInt("LoadingSave", 1);
							PlayerPrefs.SetInt("SaveSlot", this.Selected);
							SceneManager.LoadScene("LoadingScene");
						}
					}
				}
				else if (this.Saving)
				{
					if (!this.ConfirmWindow.activeInHierarchy)
					{
						this.AreYouSureLabel.text = "Are you sure you'd like to save?";
						this.ConfirmWindow.SetActive(true);
					}
					else
					{
						this.ConfirmWindow.SetActive(false);
						PlayerPrefs.SetInt("SaveSlot", this.Selected);
						GameGlobals.MostRecentSlot = this.Selected;
						PlayerPrefs.SetString(string.Concat(new object[]
						{
							"Profile_",
							this.Profile,
							"_Slot_",
							this.Selected,
							"_DateTime"
						}), DateTime.Now.ToString());
						ScreenCapture.CaptureScreenshot(string.Concat(new object[]
						{
							Application.streamingAssetsPath,
							"/SaveData/Profile_",
							this.Profile,
							"/Slot_",
							this.Selected,
							"_Thumbnail.png"
						}));
						this.PauseScreen.ScreenBlur.enabled = false;
						this.UICamera.enabled = false;
						this.GrabScreenshot = true;
					}
				}
			}
			if (Input.GetButtonDown("X"))
			{
				if (this.Loading)
				{
					if (this.DataLabels[this.Selected].text != "No Data")
					{
						PlayerPrefs.SetInt("SaveSlot", this.Selected);
						this.StudentManager.Load();
						Physics.SyncTransforms();
						if (PlayerPrefs.GetInt(string.Concat(new object[]
						{
							"Profile_",
							this.Profile,
							"_Slot_",
							this.Selected,
							"_Weekday"
						})) == 1)
						{
							DateGlobals.Weekday = DayOfWeek.Monday;
						}
						else if (PlayerPrefs.GetInt(string.Concat(new object[]
						{
							"Profile_",
							this.Profile,
							"_Slot_",
							this.Selected,
							"_Weekday"
						})) == 2)
						{
							DateGlobals.Weekday = DayOfWeek.Tuesday;
						}
						else if (PlayerPrefs.GetInt(string.Concat(new object[]
						{
							"Profile_",
							this.Profile,
							"_Slot_",
							this.Selected,
							"_Weekday"
						})) == 3)
						{
							DateGlobals.Weekday = DayOfWeek.Wednesday;
						}
						else if (PlayerPrefs.GetInt(string.Concat(new object[]
						{
							"Profile_",
							this.Profile,
							"_Slot_",
							this.Selected,
							"_Weekday"
						})) == 4)
						{
							DateGlobals.Weekday = DayOfWeek.Tuesday;
						}
						else if (PlayerPrefs.GetInt(string.Concat(new object[]
						{
							"Profile_",
							this.Profile,
							"_Slot_",
							this.Selected,
							"_Weekday"
						})) == 5)
						{
							DateGlobals.Weekday = DayOfWeek.Wednesday;
						}
						this.Clock.DayLabel.text = this.Clock.GetWeekdayText(DateGlobals.Weekday);
						this.PauseScreen.MainMenu.SetActive(true);
						this.PauseScreen.Sideways = false;
						this.PauseScreen.PressedB = true;
						base.gameObject.SetActive(false);
						this.PauseScreen.ExitPhone();
					}
				}
				else if (this.Saving && PlayerPrefs.GetString(string.Concat(new object[]
				{
					"Profile_",
					this.Profile,
					"_Slot_",
					this.Selected,
					"_DateTime"
				})) != "")
				{
					File.Delete(string.Concat(new object[]
					{
						Application.streamingAssetsPath,
						"/SaveData/Profile_",
						this.Profile,
						"/Slot_",
						this.Selected,
						"_Thumbnail.png"
					}));
					PlayerPrefs.SetString(string.Concat(new object[]
					{
						"Profile_",
						this.Profile,
						"_Slot_",
						this.Selected,
						"_DateTime"
					}), "");
					this.Thumbnails[this.Selected].mainTexture = this.DefaultThumbnail;
					this.DataLabels[this.Selected].text = "No Data";
				}
			}
			if (Input.GetButtonDown("B"))
			{
				if (this.ConfirmWindow.activeInHierarchy)
				{
					this.ConfirmWindow.SetActive(false);
					return;
				}
				this.PauseScreen.MainMenu.SetActive(true);
				this.PauseScreen.Sideways = false;
				this.PauseScreen.PressedB = true;
				base.gameObject.SetActive(false);
				this.PauseScreen.PromptBar.ClearButtons();
				this.PauseScreen.PromptBar.Label[0].text = "Accept";
				this.PauseScreen.PromptBar.Label[1].text = "Exit";
				this.PauseScreen.PromptBar.Label[4].text = "Choose";
				this.PauseScreen.PromptBar.UpdateButtons();
				this.PauseScreen.PromptBar.Show = true;
			}
		}
	}

	// Token: 0x06001A54 RID: 6740 RVA: 0x00102B19 File Offset: 0x00100D19
	public IEnumerator GetThumbnails()
	{
		int num;
		for (int ID = 1; ID < 11; ID = num + 1)
		{
			if (PlayerPrefs.GetString(string.Concat(new object[]
			{
				"Profile_",
				this.Profile,
				"_Slot_",
				ID,
				"_DateTime"
			})) != "")
			{
				this.DataLabels[ID].text = PlayerPrefs.GetString(string.Concat(new object[]
				{
					"Profile_",
					this.Profile,
					"_Slot_",
					ID,
					"_DateTime"
				}));
				string url = string.Concat(new object[]
				{
					"file:///",
					Application.streamingAssetsPath,
					"/SaveData/Profile_",
					this.Profile,
					"/Slot_",
					ID,
					"_Thumbnail.png"
				});
				WWW www = new WWW(url);
				yield return www;
				if (www.error == null)
				{
					this.Thumbnails[ID].mainTexture = www.texture;
				}
				else
				{
					Debug.Log("Could not retrieve the thumbnail. Maybe it was deleted from Streaming Assets?");
				}
				www = null;
			}
			else
			{
				this.DataLabels[ID].text = "No Data";
			}
			num = ID;
		}
		yield break;
	}

	// Token: 0x06001A55 RID: 6741 RVA: 0x00102B28 File Offset: 0x00100D28
	public void UpdateHighlight()
	{
		if (this.Row < 1)
		{
			this.Row = 2;
		}
		else if (this.Row > 2)
		{
			this.Row = 1;
		}
		if (this.Column < 1)
		{
			this.Column = 5;
		}
		else if (this.Column > 5)
		{
			this.Column = 1;
		}
		this.Highlight.localPosition = new Vector3((float)(-510 + 170 * this.Column), (float)(313 - 226 * this.Row), this.Highlight.localPosition.z);
		this.Selected = this.Column + (this.Row - 1) * 5;
	}

	// Token: 0x0400298F RID: 10639
	public StudentManagerScript StudentManager;

	// Token: 0x04002990 RID: 10640
	public InputManagerScript InputManager;

	// Token: 0x04002991 RID: 10641
	public PauseScreenScript PauseScreen;

	// Token: 0x04002992 RID: 10642
	public GameObject ConfirmWindow;

	// Token: 0x04002993 RID: 10643
	public GameObject WarningWindow;

	// Token: 0x04002994 RID: 10644
	public ClockScript Clock;

	// Token: 0x04002995 RID: 10645
	public Texture DefaultThumbnail;

	// Token: 0x04002996 RID: 10646
	public UILabel AreYouSureLabel;

	// Token: 0x04002997 RID: 10647
	public UILabel Header;

	// Token: 0x04002998 RID: 10648
	public UITexture[] Thumbnails;

	// Token: 0x04002999 RID: 10649
	public UILabel[] DataLabels;

	// Token: 0x0400299A RID: 10650
	public Transform Highlight;

	// Token: 0x0400299B RID: 10651
	public Camera UICamera;

	// Token: 0x0400299C RID: 10652
	public bool GrabScreenshot;

	// Token: 0x0400299D RID: 10653
	public bool Loading;

	// Token: 0x0400299E RID: 10654
	public bool Saving;

	// Token: 0x0400299F RID: 10655
	public int Profile;

	// Token: 0x040029A0 RID: 10656
	public int Row = 1;

	// Token: 0x040029A1 RID: 10657
	public int Column = 1;

	// Token: 0x040029A2 RID: 10658
	public int Selected = 1;
}
