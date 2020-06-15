using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200033E RID: 830
public class MusicMenuScript : MonoBehaviour
{
	// Token: 0x06001865 RID: 6245 RVA: 0x000DC2A0 File Offset: 0x000DA4A0
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.C))
		{
			this.AudioMenu.SetActive(true);
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
		if (Input.GetButtonDown("A"))
		{
			base.StartCoroutine(this.DownloadCoroutine());
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

	// Token: 0x06001866 RID: 6246 RVA: 0x000DC3CA File Offset: 0x000DA5CA
	private IEnumerator DownloadCoroutine()
	{
		WWW CurrentDownload = new WWW(string.Concat(new object[]
		{
			"File:///",
			Application.streamingAssetsPath,
			"/Music/track",
			this.Selected,
			".ogg"
		}));
		yield return CurrentDownload;
		this.CustomMusic = CurrentDownload.GetAudioClipCompressed();
		this.Jukebox.Custom.clip = this.CustomMusic;
		this.Jukebox.PlayCustom();
		yield break;
	}

	// Token: 0x06001867 RID: 6247 RVA: 0x000DC3DC File Offset: 0x000DA5DC
	private void UpdateHighlight()
	{
		if (this.Selected < 0)
		{
			this.Selected = this.SelectionLimit;
		}
		else if (this.Selected > this.SelectionLimit)
		{
			this.Selected = 0;
		}
		this.Highlight.localPosition = new Vector3(this.Highlight.localPosition.x, 365f - 80f * (float)this.Selected, this.Highlight.localPosition.z);
	}

	// Token: 0x04002392 RID: 9106
	public InputManagerScript InputManager;

	// Token: 0x04002393 RID: 9107
	public PauseScreenScript PauseScreen;

	// Token: 0x04002394 RID: 9108
	public PromptBarScript PromptBar;

	// Token: 0x04002395 RID: 9109
	public GameObject AudioMenu;

	// Token: 0x04002396 RID: 9110
	public JukeboxScript Jukebox;

	// Token: 0x04002397 RID: 9111
	public int SelectionLimit = 9;

	// Token: 0x04002398 RID: 9112
	public int Selected;

	// Token: 0x04002399 RID: 9113
	public Transform Highlight;

	// Token: 0x0400239A RID: 9114
	public string path = string.Empty;

	// Token: 0x0400239B RID: 9115
	public AudioClip CustomMusic;
}
