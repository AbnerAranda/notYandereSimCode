using System;
using UnityEngine;

// Token: 0x020002EB RID: 747
public class HomeCorkboardScript : MonoBehaviour
{
	// Token: 0x0600172A RID: 5930 RVA: 0x000C4C9C File Offset: 0x000C2E9C
	private void Update()
	{
		if (!this.HomeYandere.CanMove)
		{
			if (!this.Loaded)
			{
				this.PhotoGallery.LoadingScreen.SetActive(false);
				this.PhotoGallery.UpdateButtonPrompts();
				this.PhotoGallery.enabled = true;
				this.PhotoGallery.gameObject.SetActive(true);
				this.Loaded = true;
			}
			if (!this.PhotoGallery.Adjusting && !this.PhotoGallery.Viewing && !this.PhotoGallery.LoadingScreen.activeInHierarchy && Input.GetButtonDown("B"))
			{
				this.HomeCamera.Destination = this.HomeCamera.Destinations[0];
				this.HomeCamera.Target = this.HomeCamera.Targets[0];
				this.HomeCamera.CorkboardLabel.SetActive(true);
				this.PhotoGallery.PromptBar.Show = false;
				this.PhotoGallery.enabled = false;
				this.HomeYandere.CanMove = true;
				this.HomeYandere.gameObject.SetActive(true);
				this.HomeWindow.Show = false;
				base.enabled = false;
				this.Loaded = false;
				this.PhotoGallery.SaveAllPhotographs();
				this.PhotoGallery.SaveAllStrings();
			}
		}
	}

	// Token: 0x04001F7B RID: 8059
	public InputManagerScript InputManager;

	// Token: 0x04001F7C RID: 8060
	public PhotoGalleryScript PhotoGallery;

	// Token: 0x04001F7D RID: 8061
	public HomeYandereScript HomeYandere;

	// Token: 0x04001F7E RID: 8062
	public HomeCameraScript HomeCamera;

	// Token: 0x04001F7F RID: 8063
	public HomeWindowScript HomeWindow;

	// Token: 0x04001F80 RID: 8064
	public bool Loaded;
}
