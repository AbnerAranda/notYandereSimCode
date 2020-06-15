using System;
using System.Globalization;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000393 RID: 915
public class ResolutionScript : MonoBehaviour
{
	// Token: 0x060019B9 RID: 6585 RVA: 0x000FC50C File Offset: 0x000FA70C
	private void Start()
	{
		this.Darkness.color = new Color(1f, 1f, 1f, 1f);
		Screen.fullScreen = false;
		Screen.SetResolution(1280, 720, false);
		this.ResolutionLabel.text = Screen.width + " x " + Screen.height;
		this.QualityLabel.text = (this.Qualities[QualitySettings.GetQualityLevel()] ?? "");
		this.FullScreenLabel.text = "No";
		Debug.Log("The quality level is set to: " + QualitySettings.GetQualityLevel());
		Thread.CurrentThread.CurrentCulture = new CultureInfo("en-us");
	}

	// Token: 0x060019BA RID: 6586 RVA: 0x000FC5DC File Offset: 0x000FA7DC
	private void Update()
	{
		if (this.FadeOut)
		{
			this.Alpha = Mathf.MoveTowards(this.Alpha, 1f, Time.deltaTime);
			if (this.Alpha == 1f)
			{
				SceneManager.LoadScene("WelcomeScene");
			}
		}
		else
		{
			this.Alpha = Mathf.MoveTowards(this.Alpha, 0f, Time.deltaTime);
		}
		this.Darkness.color = new Color(1f, 1f, 1f, this.Alpha);
		if (this.Alpha == 0f)
		{
			if (this.InputManager.TappedDown)
			{
				this.ID++;
				this.UpdateHighlight();
			}
			if (this.InputManager.TappedUp)
			{
				this.ID--;
				this.UpdateHighlight();
			}
			if (this.ID == 1)
			{
				if (this.InputManager.TappedRight)
				{
					this.ResID++;
					if (this.ResID == this.Widths.Length)
					{
						this.ResID = 0;
					}
					this.UpdateRes();
				}
				else if (this.InputManager.TappedLeft)
				{
					this.ResID--;
					if (this.ResID < 0)
					{
						this.ResID = this.Widths.Length - 1;
					}
					this.UpdateRes();
				}
			}
			else if (this.ID == 2)
			{
				if (this.InputManager.TappedRight || this.InputManager.TappedLeft)
				{
					this.FullScreen = !this.FullScreen;
					if (this.FullScreen)
					{
						this.FullScreenLabel.text = "Yes";
					}
					else
					{
						this.FullScreenLabel.text = "No";
					}
					Screen.SetResolution(Screen.width, Screen.height, this.FullScreen);
				}
			}
			else if (this.ID == 3)
			{
				if (this.InputManager.TappedRight)
				{
					this.QualityID++;
					if (this.QualityID == this.Qualities.Length)
					{
						this.QualityID = 0;
					}
					this.UpdateQuality();
				}
				else if (this.InputManager.TappedLeft)
				{
					this.QualityID--;
					if (this.QualityID < 0)
					{
						this.QualityID = this.Qualities.Length - 1;
					}
					this.UpdateQuality();
				}
			}
			else if (this.ID == 4 && Input.GetButtonUp("A"))
			{
				this.FadeOut = true;
			}
		}
		this.Highlight.localPosition = Vector3.Lerp(this.Highlight.localPosition, new Vector3(-307.5f, (float)(250 - this.ID * 100), 0f), Time.deltaTime * 10f);
	}

	// Token: 0x060019BB RID: 6587 RVA: 0x000FC898 File Offset: 0x000FAA98
	private void UpdateRes()
	{
		Screen.SetResolution(this.Widths[this.ResID], this.Heights[this.ResID], Screen.fullScreen);
		this.ResolutionLabel.text = this.Widths[this.ResID] + " x " + this.Heights[this.ResID];
	}

	// Token: 0x060019BC RID: 6588 RVA: 0x000FC904 File Offset: 0x000FAB04
	private void UpdateQuality()
	{
		QualitySettings.SetQualityLevel(this.QualityID, true);
		this.QualityLabel.text = (this.Qualities[this.QualityID] ?? "");
		Debug.Log("The quality level is set to: " + QualitySettings.GetQualityLevel());
	}

	// Token: 0x060019BD RID: 6589 RVA: 0x000FC957 File Offset: 0x000FAB57
	private void UpdateHighlight()
	{
		if (this.ID < 1)
		{
			this.ID = 4;
			return;
		}
		if (this.ID > 4)
		{
			this.ID = 1;
		}
	}

	// Token: 0x040027DD RID: 10205
	public InputManagerScript InputManager;

	// Token: 0x040027DE RID: 10206
	public UILabel ResolutionLabel;

	// Token: 0x040027DF RID: 10207
	public UILabel FullScreenLabel;

	// Token: 0x040027E0 RID: 10208
	public UILabel QualityLabel;

	// Token: 0x040027E1 RID: 10209
	public Transform Highlight;

	// Token: 0x040027E2 RID: 10210
	public UISprite Darkness;

	// Token: 0x040027E3 RID: 10211
	public float Alpha = 1f;

	// Token: 0x040027E4 RID: 10212
	public bool FullScreen;

	// Token: 0x040027E5 RID: 10213
	public bool FadeOut;

	// Token: 0x040027E6 RID: 10214
	public string[] Qualities;

	// Token: 0x040027E7 RID: 10215
	public int[] Widths;

	// Token: 0x040027E8 RID: 10216
	public int[] Heights;

	// Token: 0x040027E9 RID: 10217
	public int QualityID;

	// Token: 0x040027EA RID: 10218
	public int ResID = 1;

	// Token: 0x040027EB RID: 10219
	public int ID = 1;
}
