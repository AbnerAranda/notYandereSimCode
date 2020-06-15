using System;
using UnityEngine;

// Token: 0x0200027B RID: 635
public class RivalEditorScript : MonoBehaviour
{
	// Token: 0x0600138D RID: 5005 RVA: 0x000A8D91 File Offset: 0x000A6F91
	private void Awake()
	{
		this.inputManager = UnityEngine.Object.FindObjectOfType<InputManagerScript>();
	}

	// Token: 0x0600138E RID: 5006 RVA: 0x000A8DA0 File Offset: 0x000A6FA0
	private void OnEnable()
	{
		this.promptBar.Label[0].text = string.Empty;
		this.promptBar.Label[1].text = "Back";
		this.promptBar.Label[4].text = string.Empty;
		this.promptBar.UpdateButtons();
	}

	// Token: 0x0600138F RID: 5007 RVA: 0x000A8DFD File Offset: 0x000A6FFD
	private void HandleInput()
	{
		if (Input.GetButtonDown("B"))
		{
			this.mainPanel.gameObject.SetActive(true);
			this.rivalPanel.gameObject.SetActive(false);
		}
	}

	// Token: 0x06001390 RID: 5008 RVA: 0x000A8E2D File Offset: 0x000A702D
	private void Update()
	{
		this.HandleInput();
	}

	// Token: 0x04001AD4 RID: 6868
	[SerializeField]
	private UIPanel mainPanel;

	// Token: 0x04001AD5 RID: 6869
	[SerializeField]
	private UIPanel rivalPanel;

	// Token: 0x04001AD6 RID: 6870
	[SerializeField]
	private UILabel titleLabel;

	// Token: 0x04001AD7 RID: 6871
	[SerializeField]
	private PromptBarScript promptBar;

	// Token: 0x04001AD8 RID: 6872
	private InputManagerScript inputManager;
}
