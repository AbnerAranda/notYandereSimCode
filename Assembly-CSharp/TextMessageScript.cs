using System;
using UnityEngine;

// Token: 0x02000360 RID: 864
public class TextMessageScript : MonoBehaviour
{
	// Token: 0x060018D9 RID: 6361 RVA: 0x000E668C File Offset: 0x000E488C
	private void Start()
	{
		if (!this.Attachment && this.Image != null)
		{
			this.Image.SetActive(false);
		}
		if (this.Right && EventGlobals.OsanaConversation)
		{
			base.gameObject.GetComponent<UISprite>().color = new Color(1f, 0.5f, 0f);
			this.Label.color = new Color(1f, 1f, 1f);
		}
	}

	// Token: 0x060018DA RID: 6362 RVA: 0x000E670D File Offset: 0x000E490D
	private void Update()
	{
		base.transform.localScale = Vector3.Lerp(base.transform.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10f);
	}

	// Token: 0x04002521 RID: 9505
	public UILabel Label;

	// Token: 0x04002522 RID: 9506
	public GameObject Image;

	// Token: 0x04002523 RID: 9507
	public bool Attachment;

	// Token: 0x04002524 RID: 9508
	public bool Right;
}
