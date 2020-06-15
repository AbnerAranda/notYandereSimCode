using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020003D9 RID: 985
public class SettingsScript : MonoBehaviour
{
	// Token: 0x06001A88 RID: 6792 RVA: 0x00104F38 File Offset: 0x00103138
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			OptionGlobals.DepthOfField = !OptionGlobals.DepthOfField;
			this.QualityManager.ToggleExperiment();
		}
		if (Input.GetKeyDown(KeyCode.R))
		{
			OptionGlobals.RimLight = !OptionGlobals.RimLight;
			this.QualityManager.RimLight();
		}
		if (Input.GetKeyDown(KeyCode.Z))
		{
			this.StudentManager.OpaqueWindows = !this.StudentManager.OpaqueWindows;
			this.StudentManager.LateUpdate();
			if (!this.StudentManager.OpaqueWindows && !this.StudentManager.WindowOccluder.open)
			{
				this.StudentManager.SetWindowsOpaque();
			}
		}
		if (Input.GetKeyDown(KeyCode.B))
		{
			this.ToggleBackground();
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
				OptionGlobals.ParticleCount++;
				this.QualityManager.UpdateParticles();
				this.UpdateText();
			}
			else if (this.InputManager.TappedLeft)
			{
				OptionGlobals.ParticleCount--;
				this.QualityManager.UpdateParticles();
				this.UpdateText();
			}
		}
		else if (this.Selected == 2)
		{
			if (this.InputManager.TappedRight || this.InputManager.TappedLeft)
			{
				OptionGlobals.DisableOutlines = !OptionGlobals.DisableOutlines;
				this.UpdateText();
				this.QualityManager.UpdateOutlines();
			}
		}
		else if (this.Selected == 3)
		{
			if (this.InputManager.TappedRight)
			{
				if (QualitySettings.antiAliasing > 0)
				{
					QualitySettings.antiAliasing *= 2;
				}
				else
				{
					QualitySettings.antiAliasing = 2;
				}
				this.UpdateText();
			}
			else if (this.InputManager.TappedLeft)
			{
				if (QualitySettings.antiAliasing > 0)
				{
					QualitySettings.antiAliasing /= 2;
				}
				else
				{
					QualitySettings.antiAliasing = 0;
				}
				this.UpdateText();
			}
		}
		else if (this.Selected == 4)
		{
			if (this.InputManager.TappedRight || this.InputManager.TappedLeft)
			{
				OptionGlobals.DisablePostAliasing = !OptionGlobals.DisablePostAliasing;
				this.UpdateText();
				this.QualityManager.UpdatePostAliasing();
			}
		}
		else if (this.Selected == 5)
		{
			if (this.InputManager.TappedRight || this.InputManager.TappedLeft)
			{
				OptionGlobals.DisableBloom = !OptionGlobals.DisableBloom;
				this.UpdateText();
				this.QualityManager.UpdateBloom();
			}
		}
		else if (this.Selected == 6)
		{
			if (this.InputManager.TappedRight)
			{
				OptionGlobals.LowDetailStudents--;
				this.QualityManager.UpdateLowDetailStudents();
				this.UpdateText();
			}
			else if (this.InputManager.TappedLeft)
			{
				OptionGlobals.LowDetailStudents++;
				this.QualityManager.UpdateLowDetailStudents();
				this.UpdateText();
			}
		}
		else if (this.Selected == 7)
		{
			if (this.InputManager.TappedRight)
			{
				OptionGlobals.DrawDistance += 10;
				this.QualityManager.UpdateDrawDistance();
				this.UpdateText();
			}
			else if (this.InputManager.TappedLeft)
			{
				OptionGlobals.DrawDistance -= 10;
				this.QualityManager.UpdateDrawDistance();
				this.UpdateText();
			}
		}
		else if (this.Selected == 8)
		{
			if (this.InputManager.TappedRight || this.InputManager.TappedLeft)
			{
				OptionGlobals.Fog = !OptionGlobals.Fog;
				this.UpdateText();
				this.QualityManager.UpdateFog();
			}
		}
		else if (this.Selected == 9)
		{
			if (this.InputManager.TappedRight || this.InputManager.TappedLeft)
			{
				OptionGlobals.ToggleRun = !OptionGlobals.ToggleRun;
				this.UpdateText();
				this.QualityManager.ToggleRun();
			}
		}
		else if (this.Selected == 10)
		{
			if (this.InputManager.TappedRight)
			{
				OptionGlobals.DisableFarAnimations++;
				this.QualityManager.UpdateAnims();
				this.UpdateText();
			}
			else if (this.InputManager.TappedLeft)
			{
				OptionGlobals.DisableFarAnimations--;
				this.QualityManager.UpdateAnims();
				this.UpdateText();
			}
		}
		else if (this.Selected == 11)
		{
			if (this.InputManager.TappedRight)
			{
				OptionGlobals.FPSIndex++;
				this.QualityManager.UpdateFPSIndex();
			}
			else if (this.InputManager.TappedLeft)
			{
				OptionGlobals.FPSIndex--;
				this.QualityManager.UpdateFPSIndex();
			}
			this.UpdateText();
		}
		else if (this.Selected == 12)
		{
			if (this.InputManager.TappedRight)
			{
				if (OptionGlobals.Sensitivity < 10)
				{
					OptionGlobals.Sensitivity++;
				}
			}
			else if (this.InputManager.TappedLeft && OptionGlobals.Sensitivity > 1)
			{
				OptionGlobals.Sensitivity--;
			}
			this.PauseScreen.RPGCamera.sensitivity = (float)OptionGlobals.Sensitivity;
			this.UpdateText();
		}
		else if (this.Selected == 13)
		{
			if (this.InputManager.TappedRight || this.InputManager.TappedLeft)
			{
				OptionGlobals.InvertAxis = !OptionGlobals.InvertAxis;
				this.PauseScreen.RPGCamera.invertAxis = OptionGlobals.InvertAxis;
				this.UpdateText();
			}
			this.UpdateText();
		}
		else if (this.Selected == 14)
		{
			if (this.InputManager.TappedRight || this.InputManager.TappedLeft)
			{
				OptionGlobals.TutorialsOff = !OptionGlobals.TutorialsOff;
				if (SceneManager.GetActiveScene().name == "SchoolScene")
				{
					this.StudentManager.TutorialWindow.enabled = !OptionGlobals.TutorialsOff;
				}
				this.UpdateText();
			}
			this.UpdateText();
		}
		else if (this.Selected == 15)
		{
			if (this.InputManager.TappedRight || this.InputManager.TappedLeft)
			{
				Screen.SetResolution(Screen.width, Screen.height, !Screen.fullScreen);
				this.UpdateText();
			}
			this.UpdateText();
		}
		else if (this.Selected == 16)
		{
			if (this.InputManager.TappedRight || this.InputManager.TappedLeft)
			{
				OptionGlobals.DisableObscurance = !OptionGlobals.DisableObscurance;
				this.QualityManager.UpdateObscurance();
				this.UpdateText();
			}
			this.UpdateText();
		}
		else if (this.Selected == 17)
		{
			this.WarningMessage.SetActive(true);
			if (this.InputManager.TappedRight || this.InputManager.TappedLeft)
			{
				OptionGlobals.EnableShadows = !OptionGlobals.EnableShadows;
				this.QualityManager.UpdateShadows();
				this.UpdateText();
			}
			this.UpdateText();
		}
		if (this.Selected != 17)
		{
			this.WarningMessage.SetActive(false);
		}
		if (Input.GetKeyDown("l"))
		{
			OptionGlobals.ParticleCount = 1;
			OptionGlobals.DisableOutlines = true;
			QualitySettings.antiAliasing = 0;
			OptionGlobals.DisablePostAliasing = true;
			OptionGlobals.DisableBloom = true;
			OptionGlobals.LowDetailStudents = 1;
			OptionGlobals.DrawDistance = 50;
			OptionGlobals.EnableShadows = false;
			OptionGlobals.DisableFarAnimations = 1;
			OptionGlobals.RimLight = false;
			OptionGlobals.DepthOfField = false;
			this.QualityManager.UpdateFog();
			this.QualityManager.UpdateAnims();
			this.QualityManager.UpdateBloom();
			this.QualityManager.UpdateFPSIndex();
			this.QualityManager.UpdateShadows();
			this.QualityManager.UpdateParticles();
			this.QualityManager.UpdatePostAliasing();
			this.QualityManager.UpdateDrawDistance();
			this.QualityManager.UpdateLowDetailStudents();
			this.QualityManager.UpdateOutlines();
			this.UpdateText();
		}
		if (Input.GetButtonDown("B"))
		{
			this.WarningMessage.SetActive(false);
			this.PromptBar.ClearButtons();
			this.PromptBar.Label[0].text = "Accept";
			this.PromptBar.Label[1].text = "Exit";
			this.PromptBar.Label[4].text = "Choose";
			this.PromptBar.UpdateButtons();
			if (this.PauseScreen.ScreenBlur != null)
			{
				this.PauseScreen.ScreenBlur.enabled = true;
			}
			this.PauseScreen.MainMenu.SetActive(true);
			this.PauseScreen.Sideways = false;
			this.PauseScreen.PressedB = true;
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x06001A89 RID: 6793 RVA: 0x001057EC File Offset: 0x001039EC
	public void UpdateText()
	{
		if (OptionGlobals.ParticleCount == 3)
		{
			this.ParticleLabel.text = "High";
		}
		else if (OptionGlobals.ParticleCount == 2)
		{
			this.ParticleLabel.text = "Low";
		}
		else if (OptionGlobals.ParticleCount == 1)
		{
			this.ParticleLabel.text = "None";
		}
		this.FPSCapLabel.text = QualityManagerScript.FPSStrings[OptionGlobals.FPSIndex];
		this.OutlinesLabel.text = (OptionGlobals.DisableOutlines ? "Off" : "On");
		this.AliasingLabel.text = QualitySettings.antiAliasing + "x";
		this.PostAliasingLabel.text = (OptionGlobals.DisablePostAliasing ? "Off" : "On");
		this.BloomLabel.text = (OptionGlobals.DisableBloom ? "Off" : "On");
		this.LowDetailLabel.text = ((OptionGlobals.LowDetailStudents == 0) ? "Off" : ((OptionGlobals.LowDetailStudents * 10).ToString() + "m"));
		this.FarAnimsLabel.text = ((OptionGlobals.DisableFarAnimations == 0) ? "Off" : ((OptionGlobals.DisableFarAnimations * 5).ToString() + "m"));
		this.DrawDistanceLabel.text = OptionGlobals.DrawDistance + "m";
		this.FogLabel.text = (OptionGlobals.Fog ? "On" : "Off");
		this.ToggleRunLabel.text = (OptionGlobals.ToggleRun ? "Toggle" : "Hold");
		this.SensitivityLabel.text = string.Concat(OptionGlobals.Sensitivity);
		this.InvertAxisLabel.text = (OptionGlobals.InvertAxis ? "Yes" : "No");
		this.DisableTutorialsLabel.text = (OptionGlobals.TutorialsOff ? "Yes" : "No");
		this.WindowedMode.text = (Screen.fullScreen ? "No" : "Yes");
		this.AmbientObscurance.text = (OptionGlobals.DisableObscurance ? "Off" : "On");
		this.ShadowsLabel.text = (OptionGlobals.EnableShadows ? "Yes" : "No");
	}

	// Token: 0x06001A8A RID: 6794 RVA: 0x00105A44 File Offset: 0x00103C44
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
		this.Highlight.localPosition = new Vector3(this.Highlight.localPosition.x, 430f - 50f * (float)this.Selected, this.Highlight.localPosition.z);
	}

	// Token: 0x06001A8B RID: 6795 RVA: 0x00105ABF File Offset: 0x00103CBF
	public void ToggleBackground()
	{
		OptionGlobals.DrawDistanceLimit = 350;
		OptionGlobals.DrawDistance = 350;
		this.QualityManager.UpdateDrawDistance();
		this.Background.SetActive(false);
	}

	// Token: 0x04002A42 RID: 10818
	public StudentManagerScript StudentManager;

	// Token: 0x04002A43 RID: 10819
	public QualityManagerScript QualityManager;

	// Token: 0x04002A44 RID: 10820
	public InputManagerScript InputManager;

	// Token: 0x04002A45 RID: 10821
	public PauseScreenScript PauseScreen;

	// Token: 0x04002A46 RID: 10822
	public PromptBarScript PromptBar;

	// Token: 0x04002A47 RID: 10823
	public UILabel DrawDistanceLabel;

	// Token: 0x04002A48 RID: 10824
	public UILabel PostAliasingLabel;

	// Token: 0x04002A49 RID: 10825
	public UILabel LowDetailLabel;

	// Token: 0x04002A4A RID: 10826
	public UILabel AliasingLabel;

	// Token: 0x04002A4B RID: 10827
	public UILabel OutlinesLabel;

	// Token: 0x04002A4C RID: 10828
	public UILabel ParticleLabel;

	// Token: 0x04002A4D RID: 10829
	public UILabel BloomLabel;

	// Token: 0x04002A4E RID: 10830
	public UILabel FogLabel;

	// Token: 0x04002A4F RID: 10831
	public UILabel ToggleRunLabel;

	// Token: 0x04002A50 RID: 10832
	public UILabel FarAnimsLabel;

	// Token: 0x04002A51 RID: 10833
	public UILabel FPSCapLabel;

	// Token: 0x04002A52 RID: 10834
	public UILabel SensitivityLabel;

	// Token: 0x04002A53 RID: 10835
	public UILabel InvertAxisLabel;

	// Token: 0x04002A54 RID: 10836
	public UILabel DisableTutorialsLabel;

	// Token: 0x04002A55 RID: 10837
	public UILabel WindowedMode;

	// Token: 0x04002A56 RID: 10838
	public UILabel AmbientObscurance;

	// Token: 0x04002A57 RID: 10839
	public UILabel ShadowsLabel;

	// Token: 0x04002A58 RID: 10840
	public int SelectionLimit = 2;

	// Token: 0x04002A59 RID: 10841
	public int Selected = 1;

	// Token: 0x04002A5A RID: 10842
	public Transform CloudSystem;

	// Token: 0x04002A5B RID: 10843
	public Transform Highlight;

	// Token: 0x04002A5C RID: 10844
	public GameObject Background;

	// Token: 0x04002A5D RID: 10845
	public GameObject WarningMessage;
}
