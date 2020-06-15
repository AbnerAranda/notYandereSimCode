using System;
using UnityEngine;

// Token: 0x02000244 RID: 580
public class ComputerGamesScript : MonoBehaviour
{
	// Token: 0x06001282 RID: 4738 RVA: 0x0008A340 File Offset: 0x00088540
	private void Start()
	{
		this.GameWindow.gameObject.SetActive(false);
		this.DeactivateAllBenefits();
		this.OriginalColor = this.Yandere.PowerUp.color;
		if (ClubGlobals.Club == ClubType.Gaming)
		{
			this.EnableGames();
			return;
		}
		this.DisableGames();
	}

	// Token: 0x06001283 RID: 4739 RVA: 0x0008A390 File Offset: 0x00088590
	private void Update()
	{
		if (this.ShowWindow)
		{
			this.GameWindow.localScale = Vector3.Lerp(this.GameWindow.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10f);
			if (this.InputManager.TappedUp)
			{
				this.Subject--;
				this.UpdateHighlight();
			}
			else if (this.InputManager.TappedDown)
			{
				this.Subject++;
				this.UpdateHighlight();
			}
			if (Input.GetButtonDown("A"))
			{
				this.ShowWindow = false;
				this.PlayGames();
				this.PromptBar.ClearButtons();
				this.PromptBar.UpdateButtons();
				this.PromptBar.Show = false;
			}
			if (Input.GetButtonDown("B"))
			{
				this.Yandere.CanMove = true;
				this.ShowWindow = false;
				this.PromptBar.ClearButtons();
				this.PromptBar.UpdateButtons();
				this.PromptBar.Show = false;
			}
		}
		else if (this.GameWindow.localScale.x > 0.1f)
		{
			this.GameWindow.localScale = Vector3.Lerp(this.GameWindow.localScale, Vector3.zero, Time.deltaTime * 10f);
		}
		else
		{
			this.GameWindow.localScale = Vector3.zero;
			this.GameWindow.gameObject.SetActive(false);
		}
		if (this.Gaming)
		{
			this.targetRotation = Quaternion.LookRotation(new Vector3(this.ComputerGames[this.GameID].transform.position.x, this.Yandere.transform.position.y, this.ComputerGames[this.GameID].transform.position.z) - this.Yandere.transform.position);
			this.Yandere.transform.rotation = Quaternion.Slerp(this.Yandere.transform.rotation, this.targetRotation, Time.deltaTime * 10f);
			this.Yandere.MoveTowardsTarget(new Vector3(24.32233f, 4f, 12.58998f));
			this.Timer += Time.deltaTime;
			if (this.Timer > 5f)
			{
				this.Yandere.PowerUp.transform.parent.gameObject.SetActive(true);
				this.Yandere.MyController.radius = 0.2f;
				this.Yandere.CanMove = true;
				this.Yandere.EmptyHands();
				this.Gaming = false;
				this.ActivateBenefit();
			}
		}
		else if (this.Timer < 5f)
		{
			this.ID = 1;
			while (this.ID < this.ComputerGames.Length)
			{
				PromptScript promptScript = this.ComputerGames[this.ID];
				if (promptScript.Circle[0].fillAmount == 0f)
				{
					promptScript.Circle[0].fillAmount = 1f;
					if (!this.Yandere.Chased && this.Yandere.Chasers == 0)
					{
						this.GameID = this.ID;
						if (this.ID == 1)
						{
							this.PromptBar.ClearButtons();
							this.PromptBar.Label[0].text = "Confirm";
							this.PromptBar.Label[1].text = "Back";
							this.PromptBar.Label[4].text = "Select";
							this.PromptBar.UpdateButtons();
							this.PromptBar.Show = true;
							this.Yandere.Character.GetComponent<Animation>().Play(this.Yandere.IdleAnim);
							this.Yandere.CanMove = false;
							this.GameWindow.gameObject.SetActive(true);
							this.ShowWindow = true;
						}
						else
						{
							this.PlayGames();
						}
					}
				}
				this.ID++;
			}
		}
		if (this.Yandere.PowerUp.gameObject.activeInHierarchy)
		{
			this.Timer += Time.deltaTime;
			this.Yandere.PowerUp.transform.localPosition = new Vector3(this.Yandere.PowerUp.transform.localPosition.x, this.Yandere.PowerUp.transform.localPosition.y + Time.deltaTime, this.Yandere.PowerUp.transform.localPosition.z);
			this.Yandere.PowerUp.transform.LookAt(this.MainCamera.position);
			this.Yandere.PowerUp.transform.localEulerAngles = new Vector3(this.Yandere.PowerUp.transform.localEulerAngles.x, this.Yandere.PowerUp.transform.localEulerAngles.y + 180f, this.Yandere.PowerUp.transform.localEulerAngles.z);
			if (this.Yandere.PowerUp.color != new Color(1f, 1f, 1f, 1f))
			{
				this.Yandere.PowerUp.color = this.OriginalColor;
			}
			else
			{
				this.Yandere.PowerUp.color = new Color(1f, 1f, 1f, 1f);
			}
			if (this.Timer > 6f)
			{
				this.Yandere.PowerUp.transform.parent.gameObject.SetActive(false);
				base.gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x06001284 RID: 4740 RVA: 0x0008A990 File Offset: 0x00088B90
	public void EnableGames()
	{
		for (int i = 1; i < this.ComputerGames.Length; i++)
		{
			this.ComputerGames[i].enabled = true;
		}
		base.gameObject.SetActive(true);
	}

	// Token: 0x06001285 RID: 4741 RVA: 0x0008A9CC File Offset: 0x00088BCC
	private void PlayGames()
	{
		this.Yandere.Character.GetComponent<Animation>().CrossFade("f02_playingGames_00");
		this.Yandere.MyController.radius = 0.1f;
		this.Yandere.CanMove = false;
		this.Gaming = true;
		this.DisableGames();
		this.UpdateImage();
	}

	// Token: 0x06001286 RID: 4742 RVA: 0x0008AA27 File Offset: 0x00088C27
	private void UpdateImage()
	{
		this.MyTexture.mainTexture = this.Textures[this.Subject];
	}

	// Token: 0x06001287 RID: 4743 RVA: 0x0008AA44 File Offset: 0x00088C44
	public void DisableGames()
	{
		for (int i = 1; i < this.ComputerGames.Length; i++)
		{
			this.ComputerGames[i].enabled = false;
			this.ComputerGames[i].Hide();
		}
		if (!this.Gaming)
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x06001288 RID: 4744 RVA: 0x0008AA94 File Offset: 0x00088C94
	private void EnableChairs()
	{
		for (int i = 1; i < this.Chairs.Length; i++)
		{
			this.Chairs[i].enabled = true;
		}
		base.gameObject.SetActive(true);
	}

	// Token: 0x06001289 RID: 4745 RVA: 0x0008AAD0 File Offset: 0x00088CD0
	private void DisableChairs()
	{
		for (int i = 1; i < this.Chairs.Length; i++)
		{
			this.Chairs[i].enabled = false;
		}
	}

	// Token: 0x0600128A RID: 4746 RVA: 0x0008AB00 File Offset: 0x00088D00
	private void ActivateBenefit()
	{
		if (this.Subject == 1)
		{
			ClassGlobals.BiologyBonus = 1;
		}
		else if (this.Subject == 2)
		{
			ClassGlobals.ChemistryBonus = 1;
		}
		else if (this.Subject == 3)
		{
			ClassGlobals.LanguageBonus = 1;
		}
		else if (this.Subject == 4)
		{
			ClassGlobals.PsychologyBonus = 1;
		}
		else if (this.Subject == 5)
		{
			ClassGlobals.PhysicalBonus = 1;
		}
		else if (this.Subject == 6)
		{
			PlayerGlobals.SeductionBonus = 1;
		}
		else if (this.Subject == 7)
		{
			PlayerGlobals.NumbnessBonus = 1;
		}
		else if (this.Subject == 8)
		{
			PlayerGlobals.SocialBonus = 1;
		}
		else if (this.Subject == 9)
		{
			PlayerGlobals.StealthBonus = 1;
		}
		else if (this.Subject == 10)
		{
			PlayerGlobals.SpeedBonus = 1;
		}
		else if (this.Subject == 11)
		{
			PlayerGlobals.EnlightenmentBonus = 1;
		}
		if (this.Poison != null)
		{
			this.Poison.Start();
		}
		this.StudentManager.UpdatePerception();
		this.Yandere.UpdateNumbness();
		this.Police.UpdateCorpses();
	}

	// Token: 0x0600128B RID: 4747 RVA: 0x0008AC0C File Offset: 0x00088E0C
	private void DeactivateBenefit()
	{
		if (this.Subject == 1)
		{
			ClassGlobals.BiologyBonus = 0;
		}
		else if (this.Subject == 2)
		{
			ClassGlobals.ChemistryBonus = 0;
		}
		else if (this.Subject == 3)
		{
			ClassGlobals.LanguageBonus = 0;
		}
		else if (this.Subject == 4)
		{
			ClassGlobals.PsychologyBonus = 0;
		}
		else if (this.Subject == 5)
		{
			ClassGlobals.PhysicalBonus = 0;
		}
		else if (this.Subject == 6)
		{
			PlayerGlobals.SeductionBonus = 0;
		}
		else if (this.Subject == 7)
		{
			PlayerGlobals.NumbnessBonus = 0;
		}
		else if (this.Subject == 8)
		{
			PlayerGlobals.SocialBonus = 0;
		}
		else if (this.Subject == 9)
		{
			PlayerGlobals.StealthBonus = 0;
		}
		else if (this.Subject == 10)
		{
			PlayerGlobals.SpeedBonus = 0;
		}
		else if (this.Subject == 11)
		{
			PlayerGlobals.EnlightenmentBonus = 0;
		}
		if (this.Poison != null)
		{
			this.Poison.Start();
		}
		this.StudentManager.UpdatePerception();
		this.Yandere.UpdateNumbness();
		this.Police.UpdateCorpses();
	}

	// Token: 0x0600128C RID: 4748 RVA: 0x0008AD18 File Offset: 0x00088F18
	public void DeactivateAllBenefits()
	{
		ClassGlobals.BiologyBonus = 0;
		ClassGlobals.ChemistryBonus = 0;
		ClassGlobals.LanguageBonus = 0;
		ClassGlobals.PsychologyBonus = 0;
		ClassGlobals.PhysicalBonus = 0;
		PlayerGlobals.SeductionBonus = 0;
		PlayerGlobals.NumbnessBonus = 0;
		PlayerGlobals.SocialBonus = 0;
		PlayerGlobals.StealthBonus = 0;
		PlayerGlobals.SpeedBonus = 0;
		PlayerGlobals.EnlightenmentBonus = 0;
		if (this.Poison != null)
		{
			this.Poison.Start();
		}
	}

	// Token: 0x0600128D RID: 4749 RVA: 0x0008AD80 File Offset: 0x00088F80
	private void UpdateHighlight()
	{
		if (this.Subject < 1)
		{
			this.Subject = 11;
		}
		else if (this.Subject > 11)
		{
			this.Subject = 1;
		}
		this.Highlight.localPosition = new Vector3(this.Highlight.localPosition.x, 250f - (float)this.Subject * 50f, this.Highlight.localPosition.z);
		this.DescLabel.text = this.Descriptions[this.Subject];
	}

	// Token: 0x040016A1 RID: 5793
	public PromptScript[] ComputerGames;

	// Token: 0x040016A2 RID: 5794
	public Collider[] Chairs;

	// Token: 0x040016A3 RID: 5795
	public StudentManagerScript StudentManager;

	// Token: 0x040016A4 RID: 5796
	public InputManagerScript InputManager;

	// Token: 0x040016A5 RID: 5797
	public PromptBarScript PromptBar;

	// Token: 0x040016A6 RID: 5798
	public YandereScript Yandere;

	// Token: 0x040016A7 RID: 5799
	public PoliceScript Police;

	// Token: 0x040016A8 RID: 5800
	public PoisonScript Poison;

	// Token: 0x040016A9 RID: 5801
	public Quaternion targetRotation;

	// Token: 0x040016AA RID: 5802
	public Transform GameWindow;

	// Token: 0x040016AB RID: 5803
	public Transform MainCamera;

	// Token: 0x040016AC RID: 5804
	public Transform Highlight;

	// Token: 0x040016AD RID: 5805
	public bool ShowWindow;

	// Token: 0x040016AE RID: 5806
	public bool Gaming;

	// Token: 0x040016AF RID: 5807
	public float Timer;

	// Token: 0x040016B0 RID: 5808
	public int Subject = 1;

	// Token: 0x040016B1 RID: 5809
	public int GameID;

	// Token: 0x040016B2 RID: 5810
	public int ID = 1;

	// Token: 0x040016B3 RID: 5811
	public Color OriginalColor;

	// Token: 0x040016B4 RID: 5812
	public string[] Descriptions;

	// Token: 0x040016B5 RID: 5813
	public UITexture MyTexture;

	// Token: 0x040016B6 RID: 5814
	public Texture[] Textures;

	// Token: 0x040016B7 RID: 5815
	public UILabel DescLabel;
}
