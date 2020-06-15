using System;
using UnityEngine;

// Token: 0x02000275 RID: 629
public class DropsScript : MonoBehaviour
{
	// Token: 0x06001371 RID: 4977 RVA: 0x000A7AC4 File Offset: 0x000A5CC4
	private void Start()
	{
		this.ID = 1;
		while (this.ID < this.DropNames.Length)
		{
			this.NameLabels[this.ID].text = this.DropNames[this.ID];
			this.ID++;
		}
	}

	// Token: 0x06001372 RID: 4978 RVA: 0x000A7B18 File Offset: 0x000A5D18
	private void Update()
	{
		if (this.InputManager.TappedUp)
		{
			this.Selected--;
			if (this.Selected < 1)
			{
				this.Selected = this.DropNames.Length - 1;
			}
			this.UpdateDesc();
		}
		if (this.InputManager.TappedDown)
		{
			this.Selected++;
			if (this.Selected > this.DropNames.Length - 1)
			{
				this.Selected = 1;
			}
			this.UpdateDesc();
		}
		if (Input.GetButtonDown("A"))
		{
			AudioSource component = base.GetComponent<AudioSource>();
			if (!this.Purchased[this.Selected])
			{
				if (this.PromptBar.Label[0].text != string.Empty)
				{
					if (this.Inventory.PantyShots >= this.DropCosts[this.Selected])
					{
						this.Inventory.PantyShots -= this.DropCosts[this.Selected];
						this.Purchased[this.Selected] = true;
						this.InfoChanWindow.Orders++;
						this.InfoChanWindow.ItemsToDrop[this.InfoChanWindow.Orders] = this.Selected;
						this.InfoChanWindow.DropObject();
						this.UpdateList();
						this.UpdateDesc();
						component.clip = this.InfoPurchase;
						component.Play();
						if (this.Selected == 2)
						{
							SchemeGlobals.SetSchemeStage(3, 2);
							this.Schemes.UpdateInstructions();
						}
					}
				}
				else if (this.Inventory.PantyShots < this.DropCosts[this.Selected])
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

	// Token: 0x06001373 RID: 4979 RVA: 0x000A7D78 File Offset: 0x000A5F78
	public void UpdateList()
	{
		this.ID = 1;
		while (this.ID < this.DropNames.Length)
		{
			UILabel uilabel = this.NameLabels[this.ID];
			if (!this.Purchased[this.ID])
			{
				this.CostLabels[this.ID].text = this.DropCosts[this.ID].ToString();
				uilabel.color = new Color(uilabel.color.r, uilabel.color.g, uilabel.color.b, 1f);
			}
			else
			{
				this.CostLabels[this.ID].text = string.Empty;
				uilabel.color = new Color(uilabel.color.r, uilabel.color.g, uilabel.color.b, 0.5f);
			}
			this.ID++;
		}
	}

	// Token: 0x06001374 RID: 4980 RVA: 0x000A7E74 File Offset: 0x000A6074
	public void UpdateDesc()
	{
		if (!this.Purchased[this.Selected])
		{
			if (this.Inventory.PantyShots >= this.DropCosts[this.Selected])
			{
				this.PromptBar.Label[0].text = "Purchase";
				this.PromptBar.UpdateButtons();
			}
			else
			{
				this.PromptBar.Label[0].text = string.Empty;
				this.PromptBar.UpdateButtons();
			}
		}
		else
		{
			this.PromptBar.Label[0].text = string.Empty;
			this.PromptBar.UpdateButtons();
		}
		this.Highlight.localPosition = new Vector3(this.Highlight.localPosition.x, 200f - 25f * (float)this.Selected, this.Highlight.localPosition.z);
		this.DropIcon.mainTexture = this.DropIcons[this.Selected];
		this.DropDesc.text = this.DropDescs[this.Selected];
		this.UpdatePantyCount();
	}

	// Token: 0x06001375 RID: 4981 RVA: 0x000A7F8D File Offset: 0x000A618D
	public void UpdatePantyCount()
	{
		this.PantyCount.text = this.Inventory.PantyShots.ToString();
	}

	// Token: 0x04001A98 RID: 6808
	public InfoChanWindowScript InfoChanWindow;

	// Token: 0x04001A99 RID: 6809
	public InputManagerScript InputManager;

	// Token: 0x04001A9A RID: 6810
	public InventoryScript Inventory;

	// Token: 0x04001A9B RID: 6811
	public PromptBarScript PromptBar;

	// Token: 0x04001A9C RID: 6812
	public SchemesScript Schemes;

	// Token: 0x04001A9D RID: 6813
	public GameObject FavorMenu;

	// Token: 0x04001A9E RID: 6814
	public Transform Highlight;

	// Token: 0x04001A9F RID: 6815
	public UILabel PantyCount;

	// Token: 0x04001AA0 RID: 6816
	public UITexture DropIcon;

	// Token: 0x04001AA1 RID: 6817
	public UILabel DropDesc;

	// Token: 0x04001AA2 RID: 6818
	public UILabel[] CostLabels;

	// Token: 0x04001AA3 RID: 6819
	public UILabel[] NameLabels;

	// Token: 0x04001AA4 RID: 6820
	public bool[] Purchased;

	// Token: 0x04001AA5 RID: 6821
	public Texture[] DropIcons;

	// Token: 0x04001AA6 RID: 6822
	public int[] DropCosts;

	// Token: 0x04001AA7 RID: 6823
	public string[] DropDescs;

	// Token: 0x04001AA8 RID: 6824
	public string[] DropNames;

	// Token: 0x04001AA9 RID: 6825
	public int Selected = 1;

	// Token: 0x04001AAA RID: 6826
	public int ID = 1;

	// Token: 0x04001AAB RID: 6827
	public AudioClip InfoUnavailable;

	// Token: 0x04001AAC RID: 6828
	public AudioClip InfoPurchase;

	// Token: 0x04001AAD RID: 6829
	public AudioClip InfoAfford;
}
