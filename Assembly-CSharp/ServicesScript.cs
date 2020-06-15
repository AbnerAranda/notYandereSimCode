using System;
using UnityEngine;

// Token: 0x020003D8 RID: 984
public class ServicesScript : MonoBehaviour
{
	// Token: 0x06001A81 RID: 6785 RVA: 0x00104608 File Offset: 0x00102808
	private void Start()
	{
		for (int i = 1; i < this.ServiceNames.Length; i++)
		{
			SchemeGlobals.SetServicePurchased(i, false);
			this.NameLabels[i].text = this.ServiceNames[i];
		}
	}

	// Token: 0x06001A82 RID: 6786 RVA: 0x00104644 File Offset: 0x00102844
	private void Update()
	{
		if (this.InputManager.TappedUp)
		{
			this.Selected--;
			if (this.Selected < 1)
			{
				this.Selected = this.ServiceNames.Length - 1;
			}
			this.UpdateDesc();
		}
		if (this.InputManager.TappedDown)
		{
			this.Selected++;
			if (this.Selected > this.ServiceNames.Length - 1)
			{
				this.Selected = 1;
			}
			this.UpdateDesc();
		}
		AudioSource component = base.GetComponent<AudioSource>();
		if (Input.GetButtonDown("A"))
		{
			if (!SchemeGlobals.GetServicePurchased(this.Selected) && (double)this.NameLabels[this.Selected].color.a == 1.0)
			{
				if (this.PromptBar.Label[0].text != string.Empty)
				{
					if (this.Inventory.PantyShots >= this.ServiceCosts[this.Selected])
					{
						if (this.Selected == 1)
						{
							this.Yandere.PauseScreen.StudentInfoMenu.GettingInfo = true;
							this.Yandere.PauseScreen.StudentInfoMenu.gameObject.SetActive(true);
							base.StartCoroutine(this.Yandere.PauseScreen.StudentInfoMenu.UpdatePortraits());
							this.Yandere.PauseScreen.StudentInfoMenu.Column = 0;
							this.Yandere.PauseScreen.StudentInfoMenu.Row = 0;
							this.Yandere.PauseScreen.StudentInfoMenu.UpdateHighlight();
							this.Yandere.PauseScreen.Sideways = true;
							this.Yandere.PromptBar.ClearButtons();
							this.Yandere.PromptBar.Label[1].text = "Cancel";
							this.Yandere.PromptBar.UpdateButtons();
							this.Yandere.PromptBar.Show = true;
							base.gameObject.SetActive(false);
						}
						if (this.Selected == 2)
						{
							this.Reputation.PendingRep += 5f;
							this.Purchase();
						}
						else if (this.Selected == 3)
						{
							StudentGlobals.SetStudentReputation(this.StudentManager.RivalID, StudentGlobals.GetStudentReputation(this.StudentManager.RivalID) - 5);
							this.Purchase();
						}
						else if (this.Selected == 4)
						{
							SchemeGlobals.SetServicePurchased(this.Selected, true);
							SchemeGlobals.DarkSecret = true;
							this.Purchase();
						}
						else if (this.Selected == 5)
						{
							this.Yandere.PauseScreen.StudentInfoMenu.SendingHome = true;
							this.Yandere.PauseScreen.StudentInfoMenu.gameObject.SetActive(true);
							base.StartCoroutine(this.Yandere.PauseScreen.StudentInfoMenu.UpdatePortraits());
							this.Yandere.PauseScreen.StudentInfoMenu.Column = 0;
							this.Yandere.PauseScreen.StudentInfoMenu.Row = 0;
							this.Yandere.PauseScreen.StudentInfoMenu.UpdateHighlight();
							this.Yandere.PauseScreen.Sideways = true;
							this.Yandere.PromptBar.ClearButtons();
							this.Yandere.PromptBar.Label[1].text = "Cancel";
							this.Yandere.PromptBar.UpdateButtons();
							this.Yandere.PromptBar.Show = true;
							base.gameObject.SetActive(false);
						}
						else if (this.Selected == 6)
						{
							this.Police.Timer += 300f;
							this.Police.Delayed = true;
							this.Purchase();
						}
						else if (this.Selected == 7)
						{
							SchemeGlobals.SetServicePurchased(this.Selected, true);
							CounselorGlobals.CounselorTape = 1;
							this.Purchase();
						}
						else if (this.Selected == 8)
						{
							SchemeGlobals.SetServicePurchased(this.Selected, true);
							for (int i = 1; i < 26; i++)
							{
								ConversationGlobals.SetTopicLearnedByStudent(i, 11, true);
							}
							this.Purchase();
						}
					}
				}
				else if (this.Inventory.PantyShots < this.ServiceCosts[this.Selected])
				{
					component.clip = this.InfoAfford;
					component.Play();
				}
				else
				{
					component.clip = this.InfoUnavailable;
					component.Play();
				}
			}
			else
			{
				component.clip = this.InfoUnavailable;
				component.Play();
			}
		}
		if (Input.GetButtonDown("B"))
		{
			this.PromptBar.ClearButtons();
			this.PromptBar.Label[0].text = "Accept";
			this.PromptBar.Label[1].text = "Exit";
			this.PromptBar.Label[5].text = "Choose";
			this.PromptBar.UpdateButtons();
			this.FavorMenu.SetActive(true);
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x06001A83 RID: 6787 RVA: 0x00104B48 File Offset: 0x00102D48
	public void UpdateList()
	{
		this.ID = 1;
		while (this.ID < this.ServiceNames.Length)
		{
			this.CostLabels[this.ID].text = this.ServiceCosts[this.ID].ToString();
			bool servicePurchased = SchemeGlobals.GetServicePurchased(this.ID);
			this.ServiceAvailable[this.ID] = false;
			if (this.ID == 1 || this.ID == 2 || this.ID == 3)
			{
				this.ServiceAvailable[this.ID] = true;
			}
			else if (this.ID == 4)
			{
				if (!SchemeGlobals.DarkSecret)
				{
					this.ServiceAvailable[this.ID] = true;
				}
			}
			else if (this.ID == 5)
			{
				if (!this.ServicePurchased[this.ID])
				{
					this.ServiceAvailable[this.ID] = true;
				}
			}
			else if (this.ID == 6)
			{
				if (this.Police.Show && !this.Police.Delayed)
				{
					this.ServiceAvailable[this.ID] = true;
				}
			}
			else if (this.ID == 7)
			{
				if (CounselorGlobals.CounselorTape == 0)
				{
					this.ServiceAvailable[this.ID] = true;
				}
			}
			else if (this.ID == 8 && !SchemeGlobals.GetServicePurchased(8))
			{
				this.ServiceAvailable[this.ID] = true;
			}
			UILabel uilabel = this.NameLabels[this.ID];
			uilabel.color = new Color(uilabel.color.r, uilabel.color.g, uilabel.color.b, (this.ServiceAvailable[this.ID] && !servicePurchased) ? 1f : 0.5f);
			this.ID++;
		}
	}

	// Token: 0x06001A84 RID: 6788 RVA: 0x00104D10 File Offset: 0x00102F10
	public void UpdateDesc()
	{
		if (this.ServiceAvailable[this.Selected] && !SchemeGlobals.GetServicePurchased(this.Selected))
		{
			this.PromptBar.Label[0].text = ((this.Inventory.PantyShots >= this.ServiceCosts[this.Selected]) ? "Purchase" : string.Empty);
			this.PromptBar.UpdateButtons();
		}
		else
		{
			this.PromptBar.Label[0].text = string.Empty;
			this.PromptBar.UpdateButtons();
		}
		this.Highlight.localPosition = new Vector3(this.Highlight.localPosition.x, 200f - 25f * (float)this.Selected, this.Highlight.localPosition.z);
		this.ServiceIcon.mainTexture = this.ServiceIcons[this.Selected];
		this.ServiceLimit.text = this.ServiceLimits[this.Selected];
		this.ServiceDesc.text = this.ServiceDescs[this.Selected];
		if (this.Selected == 5)
		{
			this.ServiceDesc.text = this.ServiceDescs[this.Selected] + "\n\nIf student portraits don't appear, back out of the menu, load the Student Info menu, then return to this screen.";
		}
		this.UpdatePantyCount();
	}

	// Token: 0x06001A85 RID: 6789 RVA: 0x00104E5C File Offset: 0x0010305C
	public void UpdatePantyCount()
	{
		this.PantyCount.text = this.Inventory.PantyShots.ToString();
	}

	// Token: 0x06001A86 RID: 6790 RVA: 0x00104E7C File Offset: 0x0010307C
	public void Purchase()
	{
		this.ServicePurchased[this.Selected] = true;
		this.TextMessageManager.SpawnMessage(this.Selected);
		this.Inventory.PantyShots -= this.ServiceCosts[this.Selected];
		AudioSource.PlayClipAtPoint(this.InfoPurchase, base.transform.position);
		this.UpdateList();
		this.UpdateDesc();
		this.PromptBar.Label[0].text = string.Empty;
		this.PromptBar.Label[1].text = "Back";
		this.PromptBar.UpdateButtons();
	}

	// Token: 0x04002A25 RID: 10789
	public TextMessageManagerScript TextMessageManager;

	// Token: 0x04002A26 RID: 10790
	public StudentManagerScript StudentManager;

	// Token: 0x04002A27 RID: 10791
	public InputManagerScript InputManager;

	// Token: 0x04002A28 RID: 10792
	public ReputationScript Reputation;

	// Token: 0x04002A29 RID: 10793
	public InventoryScript Inventory;

	// Token: 0x04002A2A RID: 10794
	public PromptBarScript PromptBar;

	// Token: 0x04002A2B RID: 10795
	public SchemesScript Schemes;

	// Token: 0x04002A2C RID: 10796
	public YandereScript Yandere;

	// Token: 0x04002A2D RID: 10797
	public GameObject FavorMenu;

	// Token: 0x04002A2E RID: 10798
	public Transform Highlight;

	// Token: 0x04002A2F RID: 10799
	public PoliceScript Police;

	// Token: 0x04002A30 RID: 10800
	public UITexture ServiceIcon;

	// Token: 0x04002A31 RID: 10801
	public UILabel ServiceLimit;

	// Token: 0x04002A32 RID: 10802
	public UILabel ServiceDesc;

	// Token: 0x04002A33 RID: 10803
	public UILabel PantyCount;

	// Token: 0x04002A34 RID: 10804
	public UILabel[] CostLabels;

	// Token: 0x04002A35 RID: 10805
	public UILabel[] NameLabels;

	// Token: 0x04002A36 RID: 10806
	public Texture[] ServiceIcons;

	// Token: 0x04002A37 RID: 10807
	public string[] ServiceLimits;

	// Token: 0x04002A38 RID: 10808
	public string[] ServiceDescs;

	// Token: 0x04002A39 RID: 10809
	public string[] ServiceNames;

	// Token: 0x04002A3A RID: 10810
	public bool[] ServiceAvailable;

	// Token: 0x04002A3B RID: 10811
	public bool[] ServicePurchased;

	// Token: 0x04002A3C RID: 10812
	public int[] ServiceCosts;

	// Token: 0x04002A3D RID: 10813
	public int Selected = 1;

	// Token: 0x04002A3E RID: 10814
	public int ID = 1;

	// Token: 0x04002A3F RID: 10815
	public AudioClip InfoUnavailable;

	// Token: 0x04002A40 RID: 10816
	public AudioClip InfoPurchase;

	// Token: 0x04002A41 RID: 10817
	public AudioClip InfoAfford;
}
