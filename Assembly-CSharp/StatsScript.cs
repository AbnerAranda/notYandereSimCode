using System;
using System.IO;
using UnityEngine;

// Token: 0x020003FB RID: 1019
public class StatsScript : MonoBehaviour
{
	// Token: 0x06001B09 RID: 6921 RVA: 0x00110AA4 File Offset: 0x0010ECA4
	private void Awake()
	{
		this.ClubLabels = new ClubTypeAndStringDictionary
		{
			{
				ClubType.None,
				"None"
			},
			{
				ClubType.Cooking,
				"Cooking"
			},
			{
				ClubType.Drama,
				"Drama"
			},
			{
				ClubType.Occult,
				"Occult"
			},
			{
				ClubType.Art,
				"Art"
			},
			{
				ClubType.LightMusic,
				"Light Music"
			},
			{
				ClubType.MartialArts,
				"Martial Arts"
			},
			{
				ClubType.Photography,
				"Photography"
			},
			{
				ClubType.Science,
				"Science"
			},
			{
				ClubType.Sports,
				"Sports"
			},
			{
				ClubType.Gardening,
				"Gardening"
			},
			{
				ClubType.Gaming,
				"Gaming"
			}
		};
	}

	// Token: 0x06001B0A RID: 6922 RVA: 0x00110B50 File Offset: 0x0010ED50
	private void Start()
	{
		if (File.Exists(Application.streamingAssetsPath + "/CustomPortrait.txt") && File.ReadAllText(Application.streamingAssetsPath + "/CustomPortrait.txt") == "1")
		{
			WWW www = new WWW("file:///" + Application.streamingAssetsPath + "/CustomPortrait.png");
			this.Portrait.mainTexture = www.texture;
		}
	}

	// Token: 0x06001B0B RID: 6923 RVA: 0x00110BC0 File Offset: 0x0010EDC0
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Backslash))
		{
			ClassGlobals.BiologyGrade = 1;
			ClassGlobals.ChemistryGrade = 5;
			ClassGlobals.LanguageGrade = 2;
			ClassGlobals.PhysicalGrade = 4;
			ClassGlobals.PsychologyGrade = 3;
			PlayerGlobals.Seduction = 4;
			PlayerGlobals.Numbness = 2;
			PlayerGlobals.Enlightenment = 5;
			this.UpdateStats();
		}
		if (Input.GetButtonDown("B"))
		{
			this.PromptBar.ClearButtons();
			this.PromptBar.Label[0].text = "Accept";
			this.PromptBar.Label[1].text = "Exit";
			this.PromptBar.Label[4].text = "Choose";
			this.PromptBar.UpdateButtons();
			this.PauseScreen.MainMenu.SetActive(true);
			this.PauseScreen.Sideways = false;
			this.PauseScreen.PressedB = true;
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x06001B0C RID: 6924 RVA: 0x00110CAC File Offset: 0x0010EEAC
	public void UpdateStats()
	{
		this.Grade = ClassGlobals.BiologyGrade;
		this.BarID = 1;
		while (this.BarID < 6)
		{
			UISprite uisprite = this.Subject1Bars[this.BarID];
			if (this.Grade > 0)
			{
				uisprite.color = new Color(1f, 1f, 1f, 1f);
				this.Grade--;
			}
			else
			{
				uisprite.color = new Color(1f, 1f, 1f, 0.5f);
			}
			this.BarID++;
		}
		if (ClassGlobals.BiologyGrade < 5)
		{
			this.Subject1Bars[ClassGlobals.BiologyGrade + 1].color = ((ClassGlobals.BiologyBonus > 0) ? new Color(1f, 0f, 0f, 1f) : new Color(1f, 1f, 1f, 0.5f));
		}
		this.Grade = ClassGlobals.ChemistryGrade;
		this.BarID = 1;
		while (this.BarID < 6)
		{
			UISprite uisprite2 = this.Subject2Bars[this.BarID];
			if (this.Grade > 0)
			{
				uisprite2.color = new Color(uisprite2.color.r, uisprite2.color.g, uisprite2.color.b, 1f);
				this.Grade--;
			}
			else
			{
				uisprite2.color = new Color(uisprite2.color.r, uisprite2.color.g, uisprite2.color.b, 0.5f);
			}
			this.BarID++;
		}
		if (ClassGlobals.ChemistryGrade < 5)
		{
			this.Subject2Bars[ClassGlobals.ChemistryGrade + 1].color = ((ClassGlobals.ChemistryBonus > 0) ? new Color(1f, 0f, 0f, 1f) : new Color(1f, 1f, 1f, 0.5f));
		}
		this.Grade = ClassGlobals.LanguageGrade;
		this.BarID = 1;
		while (this.BarID < 6)
		{
			UISprite uisprite3 = this.Subject3Bars[this.BarID];
			if (this.Grade > 0)
			{
				uisprite3.color = new Color(uisprite3.color.r, uisprite3.color.g, uisprite3.color.b, 1f);
				this.Grade--;
			}
			else
			{
				uisprite3.color = new Color(uisprite3.color.r, uisprite3.color.g, uisprite3.color.b, 0.5f);
			}
			this.BarID++;
		}
		if (ClassGlobals.LanguageGrade < 5)
		{
			this.Subject3Bars[ClassGlobals.LanguageGrade + 1].color = ((ClassGlobals.LanguageBonus > 0) ? new Color(1f, 0f, 0f, 1f) : new Color(1f, 1f, 1f, 0.5f));
		}
		this.Grade = ClassGlobals.PhysicalGrade;
		this.BarID = 1;
		while (this.BarID < 6)
		{
			UISprite uisprite4 = this.Subject4Bars[this.BarID];
			if (this.Grade > 0)
			{
				uisprite4.color = new Color(uisprite4.color.r, uisprite4.color.g, uisprite4.color.b, 1f);
				this.Grade--;
			}
			else
			{
				uisprite4.color = new Color(uisprite4.color.r, uisprite4.color.g, uisprite4.color.b, 0.5f);
			}
			this.BarID++;
		}
		if (ClassGlobals.PhysicalGrade < 5)
		{
			this.Subject4Bars[ClassGlobals.PhysicalGrade + 1].color = ((ClassGlobals.PhysicalBonus > 0) ? new Color(1f, 0f, 0f, 1f) : new Color(1f, 1f, 1f, 0.5f));
		}
		this.Grade = ClassGlobals.PsychologyGrade;
		this.BarID = 1;
		while (this.BarID < 6)
		{
			UISprite uisprite5 = this.Subject5Bars[this.BarID];
			if (this.Grade > 0)
			{
				uisprite5.color = new Color(uisprite5.color.r, uisprite5.color.g, uisprite5.color.b, 1f);
				this.Grade--;
			}
			else
			{
				uisprite5.color = new Color(uisprite5.color.r, uisprite5.color.g, uisprite5.color.b, 0.5f);
			}
			this.BarID++;
		}
		if (ClassGlobals.PsychologyGrade < 5)
		{
			this.Subject5Bars[ClassGlobals.PsychologyGrade + 1].color = ((ClassGlobals.PsychologyBonus > 0) ? new Color(1f, 0f, 0f, 1f) : new Color(1f, 1f, 1f, 0.5f));
		}
		this.Grade = PlayerGlobals.Seduction;
		this.BarID = 1;
		while (this.BarID < 6)
		{
			UISprite uisprite6 = this.Subject6Bars[this.BarID];
			if (this.Grade > 0)
			{
				uisprite6.color = new Color(uisprite6.color.r, uisprite6.color.g, uisprite6.color.b, 1f);
				this.Grade--;
			}
			else
			{
				uisprite6.color = new Color(uisprite6.color.r, uisprite6.color.g, uisprite6.color.b, 0.5f);
			}
			this.BarID++;
		}
		if (PlayerGlobals.Seduction < 5)
		{
			this.Subject6Bars[PlayerGlobals.Seduction + 1].color = ((PlayerGlobals.SeductionBonus > 0) ? new Color(1f, 0f, 0f, 1f) : new Color(1f, 1f, 1f, 0.5f));
		}
		this.Grade = PlayerGlobals.Numbness;
		this.BarID = 1;
		while (this.BarID < 6)
		{
			UISprite uisprite7 = this.Subject7Bars[this.BarID];
			if (this.Grade > 0)
			{
				uisprite7.color = new Color(uisprite7.color.r, uisprite7.color.g, uisprite7.color.b, 1f);
				this.Grade--;
			}
			else
			{
				uisprite7.color = new Color(uisprite7.color.r, uisprite7.color.g, uisprite7.color.b, 0.5f);
			}
			this.BarID++;
		}
		if (PlayerGlobals.Numbness < 5)
		{
			this.Subject7Bars[PlayerGlobals.Numbness + 1].color = ((PlayerGlobals.NumbnessBonus > 0) ? new Color(1f, 0f, 0f, 1f) : new Color(1f, 1f, 1f, 0.5f));
		}
		this.Grade = PlayerGlobals.Enlightenment;
		this.BarID = 1;
		while (this.BarID < 6)
		{
			UISprite uisprite8 = this.Subject8Bars[this.BarID];
			if (this.Grade > 0)
			{
				uisprite8.color = new Color(uisprite8.color.r, uisprite8.color.g, uisprite8.color.b, 1f);
				this.Grade--;
			}
			else
			{
				uisprite8.color = new Color(uisprite8.color.r, uisprite8.color.g, uisprite8.color.b, 0.5f);
			}
			this.BarID++;
		}
		if (PlayerGlobals.Enlightenment < 5)
		{
			this.Subject8Bars[PlayerGlobals.Enlightenment + 1].color = ((PlayerGlobals.EnlightenmentBonus > 0) ? new Color(1f, 0f, 0f, 1f) : new Color(1f, 1f, 1f, 0.5f));
		}
		this.Ranks[1].text = "Rank: " + ClassGlobals.BiologyGrade.ToString();
		this.Ranks[2].text = "Rank: " + ClassGlobals.ChemistryGrade.ToString();
		this.Ranks[3].text = "Rank: " + ClassGlobals.LanguageGrade.ToString();
		this.Ranks[4].text = "Rank: " + ClassGlobals.PhysicalGrade.ToString();
		this.Ranks[5].text = "Rank: " + ClassGlobals.PsychologyGrade.ToString();
		this.Ranks[6].text = "Rank: " + PlayerGlobals.Seduction.ToString();
		this.Ranks[7].text = "Rank: " + PlayerGlobals.Numbness.ToString();
		this.Ranks[8].text = "Rank: " + PlayerGlobals.Enlightenment.ToString();
		ClubType club = this.PauseScreen.Yandere.Club;
		string str;
		this.ClubLabels.TryGetValue(club, out str);
		this.ClubLabel.text = "Club: " + str;
	}

	// Token: 0x04002C00 RID: 11264
	public PauseScreenScript PauseScreen;

	// Token: 0x04002C01 RID: 11265
	public PromptBarScript PromptBar;

	// Token: 0x04002C02 RID: 11266
	public UISprite[] Subject1Bars;

	// Token: 0x04002C03 RID: 11267
	public UISprite[] Subject2Bars;

	// Token: 0x04002C04 RID: 11268
	public UISprite[] Subject3Bars;

	// Token: 0x04002C05 RID: 11269
	public UISprite[] Subject4Bars;

	// Token: 0x04002C06 RID: 11270
	public UISprite[] Subject5Bars;

	// Token: 0x04002C07 RID: 11271
	public UISprite[] Subject6Bars;

	// Token: 0x04002C08 RID: 11272
	public UISprite[] Subject7Bars;

	// Token: 0x04002C09 RID: 11273
	public UISprite[] Subject8Bars;

	// Token: 0x04002C0A RID: 11274
	public UILabel[] Ranks;

	// Token: 0x04002C0B RID: 11275
	public UILabel ClubLabel;

	// Token: 0x04002C0C RID: 11276
	public int Grade;

	// Token: 0x04002C0D RID: 11277
	public int BarID;

	// Token: 0x04002C0E RID: 11278
	public UITexture Portrait;

	// Token: 0x04002C0F RID: 11279
	private ClubTypeAndStringDictionary ClubLabels;
}
