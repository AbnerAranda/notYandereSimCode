using System;
using UnityEngine;

// Token: 0x020000D6 RID: 214
public class AudioMenuScript : MonoBehaviour
{
	// Token: 0x06000A3B RID: 2619 RVA: 0x000544AE File Offset: 0x000526AE
	private void Start()
	{
		this.UpdateText();
	}

	// Token: 0x06000A3C RID: 2620 RVA: 0x000544B8 File Offset: 0x000526B8
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.C))
		{
			this.CustomMusicMenu.SetActive(true);
			base.gameObject.SetActive(false);
		}
		if (this.InputManager.TappedUp)
		{
			this.Selected--;
			this.UpdateHighlight();
		}
		else if (this.InputManager.TappedDown)
		{
			this.Selected++;
			this.UpdateHighlight();
		}
		if (this.Selected == 1)
		{
			if (this.InputManager.TappedRight)
			{
				if (this.Jukebox.Volume < 1f)
				{
					this.Jukebox.Volume += 0.05f;
				}
				this.UpdateText();
			}
			else if (this.InputManager.TappedLeft)
			{
				if (this.Jukebox.Volume > 0f)
				{
					this.Jukebox.Volume -= 0.05f;
				}
				this.UpdateText();
			}
		}
		else if (this.Selected == 2 && (this.InputManager.TappedRight || this.InputManager.TappedLeft))
		{
			this.Jukebox.StartStopMusic();
			this.UpdateText();
		}
		if (Input.GetButtonDown("B"))
		{
			this.PromptBar.ClearButtons();
			this.PromptBar.Label[0].text = "Accept";
			this.PromptBar.Label[1].text = "Exit";
			this.PromptBar.Label[4].text = "Choose";
			this.PromptBar.UpdateButtons();
			this.PauseScreen.ScreenBlur.enabled = true;
			this.PauseScreen.MainMenu.SetActive(true);
			this.PauseScreen.Sideways = false;
			this.PauseScreen.PressedB = true;
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x06000A3D RID: 2621 RVA: 0x00054694 File Offset: 0x00052894
	public void UpdateText()
	{
		if (this.Jukebox != null)
		{
			this.CurrentTrackLabel.text = "Current Track: " + this.Jukebox.BGM;
			this.MusicVolumeLabel.text = ((this.Jukebox.Volume * 10f).ToString("F1") ?? "");
			if (this.Jukebox.Volume == 0f)
			{
				this.MusicOnOffLabel.text = "Off";
				return;
			}
			this.MusicOnOffLabel.text = "On";
		}
	}

	// Token: 0x06000A3E RID: 2622 RVA: 0x0005473C File Offset: 0x0005293C
	private void UpdateHighlight()
	{
		if (this.Selected == 0)
		{
			this.Selected = this.SelectionLimit;
		}
		else if (this.Selected > this.SelectionLimit)
		{
			this.Selected = 1;
		}
		this.Highlight.localPosition = new Vector3(this.Highlight.localPosition.x, 440f - 60f * (float)this.Selected, this.Highlight.localPosition.z);
	}

	// Token: 0x04000A6A RID: 2666
	public InputManagerScript InputManager;

	// Token: 0x04000A6B RID: 2667
	public PauseScreenScript PauseScreen;

	// Token: 0x04000A6C RID: 2668
	public PromptBarScript PromptBar;

	// Token: 0x04000A6D RID: 2669
	public JukeboxScript Jukebox;

	// Token: 0x04000A6E RID: 2670
	public UILabel CurrentTrackLabel;

	// Token: 0x04000A6F RID: 2671
	public UILabel MusicVolumeLabel;

	// Token: 0x04000A70 RID: 2672
	public UILabel MusicOnOffLabel;

	// Token: 0x04000A71 RID: 2673
	public int SelectionLimit = 5;

	// Token: 0x04000A72 RID: 2674
	public int Selected = 1;

	// Token: 0x04000A73 RID: 2675
	public Transform Highlight;

	// Token: 0x04000A74 RID: 2676
	public GameObject CustomMusicMenu;
}
