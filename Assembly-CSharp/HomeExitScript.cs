using System;
using UnityEngine;

// Token: 0x020002EF RID: 751
public class HomeExitScript : MonoBehaviour
{
	// Token: 0x06001736 RID: 5942 RVA: 0x000C566C File Offset: 0x000C386C
	private void Start()
	{
		UILabel uilabel = this.Labels[2];
		uilabel.color = new Color(uilabel.color.r, uilabel.color.g, uilabel.color.b, 0.5f);
		if (HomeGlobals.Night)
		{
			UILabel uilabel2 = this.Labels[1];
			uilabel2.color = new Color(uilabel2.color.r, uilabel2.color.g, uilabel2.color.b, 0.5f);
			uilabel.color = new Color(uilabel.color.r, uilabel.color.g, uilabel.color.b, 1f);
			Debug.Log("Scheme #6 is at stage: " + SchemeGlobals.GetSchemeStage(6));
			if (SchemeGlobals.GetSchemeStage(6) == 5)
			{
				UILabel uilabel3 = this.Labels[4];
				uilabel3.color = new Color(uilabel3.color.r, uilabel3.color.g, uilabel3.color.b, 1f);
				uilabel3.text = "Stalker's House";
			}
		}
	}

	// Token: 0x06001737 RID: 5943 RVA: 0x000C5790 File Offset: 0x000C3990
	private void Update()
	{
		if (!this.HomeYandere.CanMove && !this.HomeDarkness.FadeOut)
		{
			if (this.InputManager.TappedDown)
			{
				this.ID++;
				if (this.ID > 4)
				{
					this.ID = 1;
				}
				this.Highlight.localPosition = new Vector3(this.Highlight.localPosition.x, 50f - (float)this.ID * 50f, this.Highlight.localPosition.z);
			}
			if (this.InputManager.TappedUp)
			{
				this.ID--;
				if (this.ID < 1)
				{
					this.ID = 4;
				}
				this.Highlight.localPosition = new Vector3(this.Highlight.localPosition.x, 50f - (float)this.ID * 50f, this.Highlight.localPosition.z);
			}
			if (Input.GetButtonDown("A") && this.Labels[this.ID].color.a == 1f)
			{
				if (this.ID == 1)
				{
					if (SchoolGlobals.SchoolAtmosphere < 0.5f || GameGlobals.LoveSick)
					{
						this.HomeDarkness.Sprite.color = new Color(0f, 0f, 0f, 0f);
					}
					else
					{
						this.HomeDarkness.Sprite.color = new Color(1f, 1f, 1f, 0f);
					}
				}
				else if (this.ID == 2)
				{
					this.HomeDarkness.Sprite.color = new Color(1f, 1f, 1f, 0f);
				}
				else if (this.ID == 3)
				{
					this.HomeDarkness.Sprite.color = new Color(0f, 0f, 0f, 0f);
				}
				else if (this.ID == 4)
				{
					this.HomeDarkness.Sprite.color = new Color(0f, 0f, 0f, 0f);
				}
				this.HomeDarkness.FadeOut = true;
				this.HomeWindow.Show = false;
				base.enabled = false;
			}
			if (Input.GetButtonDown("B"))
			{
				this.HomeCamera.Destination = this.HomeCamera.Destinations[0];
				this.HomeCamera.Target = this.HomeCamera.Targets[0];
				this.HomeYandere.CanMove = true;
				this.HomeWindow.Show = false;
				base.enabled = false;
			}
		}
	}

	// Token: 0x04001F91 RID: 8081
	public InputManagerScript InputManager;

	// Token: 0x04001F92 RID: 8082
	public HomeDarknessScript HomeDarkness;

	// Token: 0x04001F93 RID: 8083
	public HomeYandereScript HomeYandere;

	// Token: 0x04001F94 RID: 8084
	public HomeCameraScript HomeCamera;

	// Token: 0x04001F95 RID: 8085
	public HomeWindowScript HomeWindow;

	// Token: 0x04001F96 RID: 8086
	public Transform Highlight;

	// Token: 0x04001F97 RID: 8087
	public UILabel[] Labels;

	// Token: 0x04001F98 RID: 8088
	public int ID = 1;
}
