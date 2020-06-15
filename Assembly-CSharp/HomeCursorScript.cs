using System;
using UnityEngine;

// Token: 0x020002EC RID: 748
public class HomeCursorScript : MonoBehaviour
{
	// Token: 0x0600172C RID: 5932 RVA: 0x000C4DF0 File Offset: 0x000C2FF0
	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject == this.Photograph)
		{
			this.PhotographNull();
		}
		if (other.gameObject == this.Tack)
		{
			this.CircleHighlight.position = new Vector3(this.CircleHighlight.position.x, 100f, this.Highlight.position.z);
			this.Tack = null;
			this.PhotoGallery.UpdateButtonPrompts();
		}
	}

	// Token: 0x0600172D RID: 5933 RVA: 0x000C4E70 File Offset: 0x000C3070
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 16)
		{
			if (this.Tack == null)
			{
				this.Photograph = other.gameObject;
				this.Highlight.localEulerAngles = this.Photograph.transform.localEulerAngles;
				this.Highlight.localPosition = this.Photograph.transform.localPosition;
				this.Highlight.localScale = new Vector3(this.Photograph.transform.localScale.x * 1.12f, this.Photograph.transform.localScale.y * 1.2f, 1f);
				this.PhotoGallery.UpdateButtonPrompts();
				return;
			}
		}
		else if (other.gameObject.name != "SouthWall")
		{
			this.Tack = other.gameObject;
			this.CircleHighlight.position = this.Tack.transform.position;
			this.PhotoGallery.UpdateButtonPrompts();
			this.PhotographNull();
		}
	}

	// Token: 0x0600172E RID: 5934 RVA: 0x000C4F88 File Offset: 0x000C3188
	private void PhotographNull()
	{
		this.Highlight.position = new Vector3(this.Highlight.position.x, 100f, this.Highlight.position.z);
		this.Photograph = null;
		this.PhotoGallery.UpdateButtonPrompts();
	}

	// Token: 0x04001F81 RID: 8065
	public PhotoGalleryScript PhotoGallery;

	// Token: 0x04001F82 RID: 8066
	public GameObject Photograph;

	// Token: 0x04001F83 RID: 8067
	public Transform Highlight;

	// Token: 0x04001F84 RID: 8068
	public GameObject Tack;

	// Token: 0x04001F85 RID: 8069
	public Transform CircleHighlight;
}
