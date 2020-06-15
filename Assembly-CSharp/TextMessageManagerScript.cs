using System;
using UnityEngine;

// Token: 0x02000421 RID: 1057
public class TextMessageManagerScript : MonoBehaviour
{
	// Token: 0x06001C44 RID: 7236 RVA: 0x00152DE0 File Offset: 0x00150FE0
	private void Update()
	{
		if (Input.GetButtonDown("B"))
		{
			UnityEngine.Object.Destroy(this.NewMessage);
			this.PromptBar.ClearButtons();
			this.PromptBar.Label[0].text = "Accept";
			this.PromptBar.Label[1].text = "Exit";
			this.PromptBar.Label[5].text = "Choose";
			this.PromptBar.UpdateButtons();
			this.PauseScreen.Sideways = true;
			this.ServicesMenu.SetActive(true);
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x06001C45 RID: 7237 RVA: 0x00152E88 File Offset: 0x00151088
	public void SpawnMessage(int ServiceID)
	{
		this.PromptBar.ClearButtons();
		this.PromptBar.Label[1].text = "Exit";
		this.PromptBar.UpdateButtons();
		this.PauseScreen.Sideways = false;
		this.ServicesMenu.SetActive(false);
		base.gameObject.SetActive(true);
		if (this.NewMessage != null)
		{
			UnityEngine.Object.Destroy(this.NewMessage);
		}
		this.NewMessage = UnityEngine.Object.Instantiate<GameObject>(this.Message);
		this.NewMessage.transform.parent = base.transform;
		this.NewMessage.transform.localPosition = new Vector3(-225f, -275f, 0f);
		this.NewMessage.transform.localEulerAngles = Vector3.zero;
		this.NewMessage.transform.localScale = new Vector3(1f, 1f, 1f);
		this.MessageText = this.Messages[ServiceID];
		if (ServiceID == 7 || ServiceID == 4)
		{
			this.MessageHeight = 11;
		}
		else
		{
			this.MessageHeight = 5;
		}
		this.NewMessage.GetComponent<UISprite>().height = 36 + 36 * this.MessageHeight;
		this.NewMessage.GetComponent<TextMessageScript>().Label.text = this.MessageText;
	}

	// Token: 0x040034FD RID: 13565
	public PauseScreenScript PauseScreen;

	// Token: 0x040034FE RID: 13566
	public PromptBarScript PromptBar;

	// Token: 0x040034FF RID: 13567
	public GameObject ServicesMenu;

	// Token: 0x04003500 RID: 13568
	public string[] Messages;

	// Token: 0x04003501 RID: 13569
	private GameObject NewMessage;

	// Token: 0x04003502 RID: 13570
	public GameObject Message;

	// Token: 0x04003503 RID: 13571
	public int MessageHeight;

	// Token: 0x04003504 RID: 13572
	public string MessageText = string.Empty;
}
