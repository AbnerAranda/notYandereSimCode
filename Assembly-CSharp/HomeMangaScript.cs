using System;
using UnityEngine;

// Token: 0x020002F2 RID: 754
public class HomeMangaScript : MonoBehaviour
{
	// Token: 0x06001744 RID: 5956 RVA: 0x000C79BC File Offset: 0x000C5BBC
	private void Start()
	{
		this.UpdateCurrentLabel();
		for (int i = 0; i < this.TotalManga; i++)
		{
			if (CollectibleGlobals.GetMangaCollected(i + 1))
			{
				this.NewManga = UnityEngine.Object.Instantiate<GameObject>(this.MangaModels[i], new Vector3(base.transform.position.x, base.transform.position.y, base.transform.position.z - 1f), Quaternion.identity);
			}
			else
			{
				this.NewManga = UnityEngine.Object.Instantiate<GameObject>(this.MysteryManga, new Vector3(base.transform.position.x, base.transform.position.y, base.transform.position.z - 1f), Quaternion.identity);
			}
			this.NewManga.transform.parent = this.MangaParent;
			this.NewManga.GetComponent<HomeMangaBookScript>().Manga = this;
			this.NewManga.GetComponent<HomeMangaBookScript>().ID = i;
			this.NewManga.transform.localScale = new Vector3(1.45f, 1.45f, 1.45f);
			this.MangaParent.transform.localEulerAngles = new Vector3(this.MangaParent.transform.localEulerAngles.x, this.MangaParent.transform.localEulerAngles.y + 360f / (float)this.TotalManga, this.MangaParent.transform.localEulerAngles.z);
			this.MangaList[i] = this.NewManga;
		}
		this.MangaParent.transform.localEulerAngles = new Vector3(this.MangaParent.transform.localEulerAngles.x, 0f, this.MangaParent.transform.localEulerAngles.z);
		this.MangaParent.transform.localPosition = new Vector3(this.MangaParent.transform.localPosition.x, this.MangaParent.transform.localPosition.y, 1.8f);
		this.UpdateMangaLabels();
		this.MangaParent.transform.localScale = Vector3.zero;
		this.MangaParent.gameObject.SetActive(false);
	}

	// Token: 0x06001745 RID: 5957 RVA: 0x000C7C18 File Offset: 0x000C5E18
	private void Update()
	{
		if (this.HomeWindow.Show)
		{
			if (!this.AreYouSure.activeInHierarchy)
			{
				this.MangaParent.localScale = Vector3.Lerp(this.MangaParent.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10f);
				this.MangaParent.gameObject.SetActive(true);
				if (this.InputManager.TappedRight)
				{
					this.DestinationReached = false;
					this.TargetRotation += 360f / (float)this.TotalManga;
					this.Selected++;
					if (this.Selected > this.TotalManga - 1)
					{
						this.Selected = 0;
					}
					this.UpdateMangaLabels();
					this.UpdateCurrentLabel();
				}
				if (this.InputManager.TappedLeft)
				{
					this.DestinationReached = false;
					this.TargetRotation -= 360f / (float)this.TotalManga;
					this.Selected--;
					if (this.Selected < 0)
					{
						this.Selected = this.TotalManga - 1;
					}
					this.UpdateMangaLabels();
					this.UpdateCurrentLabel();
				}
				this.Rotation = Mathf.Lerp(this.Rotation, this.TargetRotation, Time.deltaTime * 10f);
				this.MangaParent.localEulerAngles = new Vector3(this.MangaParent.localEulerAngles.x, this.Rotation, this.MangaParent.localEulerAngles.z);
				if (Input.GetButtonDown("A") && this.ReadButtonGroup.activeInHierarchy)
				{
					this.MangaGroup.SetActive(false);
					this.AreYouSure.SetActive(true);
				}
				if (Input.GetKeyDown(KeyCode.S))
				{
					PlayerGlobals.Seduction++;
					PlayerGlobals.Numbness++;
					PlayerGlobals.Enlightenment++;
					if (PlayerGlobals.Seduction > 5)
					{
						PlayerGlobals.Seduction = 0;
						PlayerGlobals.Numbness = 0;
						PlayerGlobals.Enlightenment = 0;
					}
					this.UpdateCurrentLabel();
					this.UpdateMangaLabels();
				}
				if (Input.GetButtonDown("B"))
				{
					this.HomeCamera.Destination = this.HomeCamera.Destinations[0];
					this.HomeCamera.Target = this.HomeCamera.Targets[0];
					this.HomeYandere.CanMove = true;
					this.HomeWindow.Show = false;
				}
				if (Input.GetKeyDown(KeyCode.Space))
				{
					for (int i = 0; i < this.TotalManga; i++)
					{
						CollectibleGlobals.SetMangaCollected(i + 1, true);
					}
					return;
				}
			}
			else
			{
				if (Input.GetButtonDown("A"))
				{
					if (this.Selected < 5)
					{
						PlayerGlobals.Seduction++;
					}
					else if (this.Selected < 10)
					{
						PlayerGlobals.Numbness++;
					}
					else
					{
						PlayerGlobals.Enlightenment++;
					}
					this.AreYouSure.SetActive(false);
					this.Darkness.FadeOut = true;
				}
				if (Input.GetButtonDown("B"))
				{
					this.MangaGroup.SetActive(true);
					this.AreYouSure.SetActive(false);
					return;
				}
			}
		}
		else
		{
			this.MangaParent.localScale = Vector3.Lerp(this.MangaParent.localScale, Vector3.zero, Time.deltaTime * 10f);
			if (this.MangaParent.localScale.x < 0.01f)
			{
				this.MangaParent.gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x06001746 RID: 5958 RVA: 0x000C7F7C File Offset: 0x000C617C
	private void UpdateMangaLabels()
	{
		if (this.Selected < 5)
		{
			this.ReadButtonGroup.SetActive(PlayerGlobals.Seduction == this.Selected);
			if (CollectibleGlobals.GetMangaCollected(this.Selected + 1))
			{
				if (PlayerGlobals.Seduction > this.Selected)
				{
					this.RequiredLabel.text = "You have already read this manga.";
				}
				else
				{
					this.RequiredLabel.text = "Required Seduction Level: " + this.Selected.ToString();
				}
			}
			else
			{
				this.RequiredLabel.text = "You have not yet collected this manga.";
				this.ReadButtonGroup.SetActive(false);
			}
		}
		else if (this.Selected < 10)
		{
			this.ReadButtonGroup.SetActive(PlayerGlobals.Numbness == this.Selected - 5);
			if (CollectibleGlobals.GetMangaCollected(this.Selected + 1))
			{
				if (PlayerGlobals.Numbness > this.Selected - 5)
				{
					this.RequiredLabel.text = "You have already read this manga.";
				}
				else
				{
					this.RequiredLabel.text = "Required Numbness Level: " + (this.Selected - 5).ToString();
				}
			}
			else
			{
				this.RequiredLabel.text = "You have not yet collected this manga.";
				this.ReadButtonGroup.SetActive(false);
			}
		}
		else
		{
			this.ReadButtonGroup.SetActive(PlayerGlobals.Enlightenment == this.Selected - 10);
			if (CollectibleGlobals.GetMangaCollected(this.Selected + 1))
			{
				if (PlayerGlobals.Enlightenment > this.Selected - 10)
				{
					this.RequiredLabel.text = "You have already read this manga.";
				}
				else
				{
					this.RequiredLabel.text = "Required Enlightenment Level: " + (this.Selected - 10).ToString();
				}
			}
			else
			{
				this.RequiredLabel.text = "You have not yet collected this manga.";
				this.ReadButtonGroup.SetActive(false);
			}
		}
		if (CollectibleGlobals.GetMangaCollected(this.Selected + 1))
		{
			this.MangaNameLabel.text = this.MangaNames[this.Selected];
			this.MangaDescLabel.text = this.MangaDescs[this.Selected];
			this.MangaBuffLabel.text = this.MangaBuffs[this.Selected];
			return;
		}
		this.MangaNameLabel.text = "?????";
		this.MangaDescLabel.text = "?????";
		this.MangaBuffLabel.text = "?????";
	}

	// Token: 0x06001747 RID: 5959 RVA: 0x000C81E4 File Offset: 0x000C63E4
	private void UpdateCurrentLabel()
	{
		if (this.Selected < 5)
		{
			this.Title = HomeMangaScript.SeductionStrings[PlayerGlobals.Seduction];
			this.CurrentLabel.text = string.Concat(new string[]
			{
				"Current Seduction Level: ",
				PlayerGlobals.Seduction.ToString(),
				" (",
				this.Title,
				")"
			});
			return;
		}
		if (this.Selected < 10)
		{
			this.Title = HomeMangaScript.NumbnessStrings[PlayerGlobals.Numbness];
			this.CurrentLabel.text = string.Concat(new string[]
			{
				"Current Numbness Level: ",
				PlayerGlobals.Numbness.ToString(),
				" (",
				this.Title,
				")"
			});
			return;
		}
		this.Title = HomeMangaScript.EnlightenmentStrings[PlayerGlobals.Enlightenment];
		this.CurrentLabel.text = string.Concat(new string[]
		{
			"Current Enlightenment Level: ",
			PlayerGlobals.Enlightenment.ToString(),
			" (",
			this.Title,
			")"
		});
	}

	// Token: 0x04001FDB RID: 8155
	private static readonly string[] SeductionStrings = new string[]
	{
		"Innocent",
		"Flirty",
		"Charming",
		"Sensual",
		"Seductive",
		"Succubus"
	};

	// Token: 0x04001FDC RID: 8156
	private static readonly string[] NumbnessStrings = new string[]
	{
		"Stoic",
		"Somber",
		"Detached",
		"Unemotional",
		"Desensitized",
		"Dead Inside"
	};

	// Token: 0x04001FDD RID: 8157
	private static readonly string[] EnlightenmentStrings = new string[]
	{
		"Asleep",
		"Awoken",
		"Mindful",
		"Informed",
		"Eyes Open",
		"Omniscient"
	};

	// Token: 0x04001FDE RID: 8158
	public InputManagerScript InputManager;

	// Token: 0x04001FDF RID: 8159
	public HomeYandereScript HomeYandere;

	// Token: 0x04001FE0 RID: 8160
	public HomeCameraScript HomeCamera;

	// Token: 0x04001FE1 RID: 8161
	public HomeWindowScript HomeWindow;

	// Token: 0x04001FE2 RID: 8162
	public HomeDarknessScript Darkness;

	// Token: 0x04001FE3 RID: 8163
	private GameObject NewManga;

	// Token: 0x04001FE4 RID: 8164
	public GameObject ReadButtonGroup;

	// Token: 0x04001FE5 RID: 8165
	public GameObject MysteryManga;

	// Token: 0x04001FE6 RID: 8166
	public GameObject AreYouSure;

	// Token: 0x04001FE7 RID: 8167
	public GameObject MangaGroup;

	// Token: 0x04001FE8 RID: 8168
	public GameObject[] MangaList;

	// Token: 0x04001FE9 RID: 8169
	public UILabel MangaNameLabel;

	// Token: 0x04001FEA RID: 8170
	public UILabel MangaDescLabel;

	// Token: 0x04001FEB RID: 8171
	public UILabel MangaBuffLabel;

	// Token: 0x04001FEC RID: 8172
	public UILabel RequiredLabel;

	// Token: 0x04001FED RID: 8173
	public UILabel CurrentLabel;

	// Token: 0x04001FEE RID: 8174
	public UILabel ButtonLabel;

	// Token: 0x04001FEF RID: 8175
	public Transform MangaParent;

	// Token: 0x04001FF0 RID: 8176
	public bool DestinationReached;

	// Token: 0x04001FF1 RID: 8177
	public float TargetRotation;

	// Token: 0x04001FF2 RID: 8178
	public float Rotation;

	// Token: 0x04001FF3 RID: 8179
	public int TotalManga;

	// Token: 0x04001FF4 RID: 8180
	public int Selected;

	// Token: 0x04001FF5 RID: 8181
	public string Title = string.Empty;

	// Token: 0x04001FF6 RID: 8182
	public GameObject[] MangaModels;

	// Token: 0x04001FF7 RID: 8183
	public string[] MangaNames;

	// Token: 0x04001FF8 RID: 8184
	public string[] MangaDescs;

	// Token: 0x04001FF9 RID: 8185
	public string[] MangaBuffs;
}
