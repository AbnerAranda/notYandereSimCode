using System;
using UnityEngine;

// Token: 0x020002A2 RID: 674
public class FavorMenuScript : MonoBehaviour
{
	// Token: 0x06001412 RID: 5138 RVA: 0x000B08F4 File Offset: 0x000AEAF4
	private void Update()
	{
		if (this.InputManager.TappedRight)
		{
			this.ID++;
			this.UpdateHighlight();
		}
		else if (this.InputManager.TappedLeft)
		{
			this.ID--;
			this.UpdateHighlight();
		}
		if (!this.TutorialWindow.Hide && !this.TutorialWindow.Show)
		{
			if (Input.GetButtonDown("A"))
			{
				this.PromptBar.ClearButtons();
				this.PromptBar.Label[0].text = "Accept";
				this.PromptBar.Label[1].text = "Exit";
				this.PromptBar.Label[4].text = "Choose";
				this.PromptBar.UpdateButtons();
				if (this.ID != 1)
				{
					if (this.ID == 2)
					{
						this.ServicesMenu.UpdatePantyCount();
						this.ServicesMenu.UpdateList();
						this.ServicesMenu.UpdateDesc();
						this.ServicesMenu.gameObject.SetActive(true);
						base.gameObject.SetActive(false);
					}
					else if (this.ID == 3)
					{
						this.DropsMenu.UpdatePantyCount();
						this.DropsMenu.UpdateList();
						this.DropsMenu.UpdateDesc();
						this.DropsMenu.gameObject.SetActive(true);
						base.gameObject.SetActive(false);
					}
				}
			}
			if (Input.GetButtonDown("X"))
			{
				TutorialGlobals.IgnoreClothing = true;
				this.TutorialWindow.IgnoreClothing = true;
				this.TutorialWindow.TitleLabel.text = "Info Points";
				this.TutorialWindow.TutorialLabel.text = this.TutorialWindow.PointsString;
				this.TutorialWindow.TutorialLabel.text = this.TutorialWindow.TutorialLabel.text.Replace('@', '\n');
				this.TutorialWindow.TutorialImage.mainTexture = this.TutorialWindow.InfoTexture;
				this.TutorialWindow.enabled = true;
				this.TutorialWindow.SummonWindow();
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
	}

	// Token: 0x06001413 RID: 5139 RVA: 0x000B0BB8 File Offset: 0x000AEDB8
	private void UpdateHighlight()
	{
		if (this.ID > 3)
		{
			this.ID = 1;
		}
		else if (this.ID < 1)
		{
			this.ID = 3;
		}
		this.Highlight.transform.localPosition = new Vector3(-500f + 250f * (float)this.ID, this.Highlight.transform.localPosition.y, this.Highlight.transform.localPosition.z);
	}

	// Token: 0x04001C59 RID: 7257
	public TutorialWindowScript TutorialWindow;

	// Token: 0x04001C5A RID: 7258
	public InputManagerScript InputManager;

	// Token: 0x04001C5B RID: 7259
	public PauseScreenScript PauseScreen;

	// Token: 0x04001C5C RID: 7260
	public ServicesScript ServicesMenu;

	// Token: 0x04001C5D RID: 7261
	public SchemesScript SchemesMenu;

	// Token: 0x04001C5E RID: 7262
	public DropsScript DropsMenu;

	// Token: 0x04001C5F RID: 7263
	public PromptBarScript PromptBar;

	// Token: 0x04001C60 RID: 7264
	public Transform Highlight;

	// Token: 0x04001C61 RID: 7265
	public int ID = 1;
}
