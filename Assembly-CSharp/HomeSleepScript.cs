using System;
using UnityEngine;

// Token: 0x020002F8 RID: 760
public class HomeSleepScript : MonoBehaviour
{
	// Token: 0x0600175F RID: 5983 RVA: 0x000CA490 File Offset: 0x000C8690
	private void Update()
	{
		if (!this.HomeYandere.CanMove && !this.HomeDarkness.FadeOut)
		{
			if (Input.GetButtonDown("A"))
			{
				this.HomeDarkness.Sprite.color = new Color(0f, 0f, 0f, 0f);
				this.HomeDarkness.Cyberstalking = true;
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

	// Token: 0x04002075 RID: 8309
	public HomeDarknessScript HomeDarkness;

	// Token: 0x04002076 RID: 8310
	public HomeYandereScript HomeYandere;

	// Token: 0x04002077 RID: 8311
	public HomeCameraScript HomeCamera;

	// Token: 0x04002078 RID: 8312
	public HomeWindowScript HomeWindow;
}
