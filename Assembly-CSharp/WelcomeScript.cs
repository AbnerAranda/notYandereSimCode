using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x0200046A RID: 1130
public class WelcomeScript : MonoBehaviour
{
	// Token: 0x06001D48 RID: 7496 RVA: 0x0015F58C File Offset: 0x0015D78C
	private void Start()
	{
		Time.timeScale = 1f;
		this.BeginLabel.color = new Color(this.BeginLabel.color.r, this.BeginLabel.color.g, this.BeginLabel.color.b, 0f);
		this.AltBeginLabel.color = this.BeginLabel.color;
		this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, 2f);
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		if (ApplicationGlobals.VersionNumber != this.VersionNumber)
		{
			ApplicationGlobals.VersionNumber = this.VersionNumber;
		}
		if (!Application.CanStreamedLevelBeLoaded("FunScene"))
		{
			Application.Quit();
		}
		if (File.Exists(Application.streamingAssetsPath + "/Fun.txt"))
		{
			this.Text = File.ReadAllText(Application.streamingAssetsPath + "/Fun.txt");
		}
		if (this.Text == "0" || this.Text == "1" || this.Text == "2" || this.Text == "3" || this.Text == "4" || this.Text == "5" || this.Text == "6" || this.Text == "7" || this.Text == "8" || this.Text == "9" || this.Text == "10" || this.Text == "69" || this.Text == "666")
		{
			SceneManager.LoadScene("VeryFunScene");
		}
		this.ID = 0;
		while (this.ID < 100)
		{
			if (this.ID != 10 && (this.JSON.Students[this.ID].Hairstyle == "21" || this.JSON.Students[this.ID].Persona == PersonaType.Protective))
			{
				Debug.Log("Player is cheating!");
				if (Application.CanStreamedLevelBeLoaded("FunScene"))
				{
					SceneManager.LoadScene("FunScene");
				}
			}
			this.ID++;
		}
	}

	// Token: 0x06001D49 RID: 7497 RVA: 0x0015F834 File Offset: 0x0015DA34
	private void Update()
	{
		Input.GetKeyDown(KeyCode.S);
		Input.GetKeyDown(KeyCode.Y);
		if (!this.Continue)
		{
			this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, this.Darkness.color.a - Time.deltaTime);
			if (this.Darkness.color.a <= 0f)
			{
				Input.GetKeyDown(KeyCode.W);
				if (Input.anyKeyDown)
				{
					this.Timer = 5f;
				}
				this.Timer += Time.deltaTime;
				if (this.Timer > 5f)
				{
					this.BeginLabel.color = new Color(this.BeginLabel.color.r, this.BeginLabel.color.g, this.BeginLabel.color.b, this.BeginLabel.color.a + Time.deltaTime);
					this.AltBeginLabel.color = this.BeginLabel.color;
					if (this.BeginLabel.color.a >= 1f && Input.anyKeyDown)
					{
						this.Darkness.color = new Color(1f, 1f, 1f, 0f);
						this.Continue = true;
					}
				}
			}
		}
		else
		{
			this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, this.Darkness.color.a + Time.deltaTime);
			if (this.Darkness.color.a >= 1f)
			{
				SceneManager.LoadScene("SponsorScene");
			}
		}
		if (!this.FlashRed)
		{
			this.ID = 0;
			while (this.ID < 3)
			{
				this.ID++;
				this.FlashingLabels[this.ID].color = new Color(this.FlashingLabels[this.ID].color.r + Time.deltaTime * 10f, this.FlashingLabels[this.ID].color.g, this.FlashingLabels[this.ID].color.b, this.FlashingLabels[this.ID].color.a);
				if (this.FlashingLabels[this.ID].color.r > 1f)
				{
					this.FlashRed = true;
				}
			}
			return;
		}
		this.ID = 0;
		while (this.ID < 3)
		{
			this.ID++;
			this.FlashingLabels[this.ID].color = new Color(this.FlashingLabels[this.ID].color.r - Time.deltaTime * 10f, this.FlashingLabels[this.ID].color.g, this.FlashingLabels[this.ID].color.b, this.FlashingLabels[this.ID].color.a);
			if (this.FlashingLabels[this.ID].color.r < 0f)
			{
				this.FlashRed = false;
			}
		}
	}

	// Token: 0x04003761 RID: 14177
	[SerializeField]
	private JsonScript JSON;

	// Token: 0x04003762 RID: 14178
	[SerializeField]
	private GameObject WelcomePanel;

	// Token: 0x04003763 RID: 14179
	[SerializeField]
	private UILabel[] FlashingLabels;

	// Token: 0x04003764 RID: 14180
	[SerializeField]
	private UILabel AltBeginLabel;

	// Token: 0x04003765 RID: 14181
	[SerializeField]
	private UILabel BeginLabel;

	// Token: 0x04003766 RID: 14182
	[SerializeField]
	private UISprite Darkness;

	// Token: 0x04003767 RID: 14183
	[SerializeField]
	private bool Continue;

	// Token: 0x04003768 RID: 14184
	[SerializeField]
	private bool FlashRed;

	// Token: 0x04003769 RID: 14185
	[SerializeField]
	private float VersionNumber;

	// Token: 0x0400376A RID: 14186
	[SerializeField]
	private float Timer;

	// Token: 0x0400376B RID: 14187
	private string Text;

	// Token: 0x0400376C RID: 14188
	private int ID;
}
