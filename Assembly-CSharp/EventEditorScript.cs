using System;
using UnityEngine;

// Token: 0x0200027A RID: 634
public class EventEditorScript : MonoBehaviour
{
	// Token: 0x06001388 RID: 5000 RVA: 0x000A8CEF File Offset: 0x000A6EEF
	private void Awake()
	{
		this.inputManager = UnityEngine.Object.FindObjectOfType<InputManagerScript>();
	}

	// Token: 0x06001389 RID: 5001 RVA: 0x000A8CFC File Offset: 0x000A6EFC
	private void OnEnable()
	{
		this.promptBar.Label[0].text = string.Empty;
		this.promptBar.Label[1].text = "Back";
		this.promptBar.Label[4].text = string.Empty;
		this.promptBar.UpdateButtons();
	}

	// Token: 0x0600138A RID: 5002 RVA: 0x000A8D59 File Offset: 0x000A6F59
	private void HandleInput()
	{
		if (Input.GetButtonDown("B"))
		{
			this.mainPanel.gameObject.SetActive(true);
			this.eventPanel.gameObject.SetActive(false);
		}
	}

	// Token: 0x0600138B RID: 5003 RVA: 0x000A8D89 File Offset: 0x000A6F89
	private void Update()
	{
		this.HandleInput();
	}

	// Token: 0x04001ACF RID: 6863
	[SerializeField]
	private UIPanel mainPanel;

	// Token: 0x04001AD0 RID: 6864
	[SerializeField]
	private UIPanel eventPanel;

	// Token: 0x04001AD1 RID: 6865
	[SerializeField]
	private UILabel titleLabel;

	// Token: 0x04001AD2 RID: 6866
	[SerializeField]
	private PromptBarScript promptBar;

	// Token: 0x04001AD3 RID: 6867
	private InputManagerScript inputManager;
}
