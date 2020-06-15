using System;
using System.Collections.Generic;
using System.IO;
using JsonFx.Json;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000279 RID: 633
public class EditorManagerScript : MonoBehaviour
{
	// Token: 0x06001381 RID: 4993 RVA: 0x000A8B02 File Offset: 0x000A6D02
	private void Awake()
	{
		this.buttonIndex = 0;
		this.inputManager = UnityEngine.Object.FindObjectOfType<InputManagerScript>();
	}

	// Token: 0x06001382 RID: 4994 RVA: 0x000A8B18 File Offset: 0x000A6D18
	private void Start()
	{
		this.promptBar.Label[0].text = "Select";
		this.promptBar.Label[1].text = "Exit";
		this.promptBar.Label[4].text = "Choose";
		this.promptBar.UpdateButtons();
	}

	// Token: 0x06001383 RID: 4995 RVA: 0x000A8B78 File Offset: 0x000A6D78
	private void OnEnable()
	{
		this.promptBar.Label[0].text = "Select";
		this.promptBar.Label[1].text = "Exit";
		this.promptBar.Label[4].text = "Choose";
		this.promptBar.UpdateButtons();
	}

	// Token: 0x06001384 RID: 4996 RVA: 0x000A8BD5 File Offset: 0x000A6DD5
	public static Dictionary<string, object>[] DeserializeJson(string filename)
	{
		return JsonReader.Deserialize<Dictionary<string, object>[]>(File.ReadAllText(Path.Combine(Application.streamingAssetsPath, Path.Combine("JSON", filename))));
	}

	// Token: 0x06001385 RID: 4997 RVA: 0x000A8BF8 File Offset: 0x000A6DF8
	private void HandleInput()
	{
		if (Input.GetButtonDown("B"))
		{
			SceneManager.LoadScene("TitleScene");
		}
		bool tappedUp = this.inputManager.TappedUp;
		bool tappedDown = this.inputManager.TappedDown;
		if (tappedUp)
		{
			this.buttonIndex = ((this.buttonIndex > 0) ? (this.buttonIndex - 1) : 2);
		}
		else if (tappedDown)
		{
			this.buttonIndex = ((this.buttonIndex < 2) ? (this.buttonIndex + 1) : 0);
		}
		if (tappedUp || tappedDown)
		{
			Transform transform = this.cursorLabel.transform;
			transform.localPosition = new Vector3(transform.localPosition.x, 100f - (float)this.buttonIndex * 100f, transform.localPosition.z);
		}
		if (Input.GetButtonDown("A"))
		{
			this.editorPanels[this.buttonIndex].gameObject.SetActive(true);
			this.mainPanel.gameObject.SetActive(false);
		}
	}

	// Token: 0x06001386 RID: 4998 RVA: 0x000A8CE7 File Offset: 0x000A6EE7
	private void Update()
	{
		this.HandleInput();
	}

	// Token: 0x04001AC8 RID: 6856
	[SerializeField]
	private UIPanel mainPanel;

	// Token: 0x04001AC9 RID: 6857
	[SerializeField]
	private UIPanel[] editorPanels;

	// Token: 0x04001ACA RID: 6858
	[SerializeField]
	private UILabel cursorLabel;

	// Token: 0x04001ACB RID: 6859
	[SerializeField]
	private PromptBarScript promptBar;

	// Token: 0x04001ACC RID: 6860
	private int buttonIndex;

	// Token: 0x04001ACD RID: 6861
	private const int ButtonCount = 3;

	// Token: 0x04001ACE RID: 6862
	private InputManagerScript inputManager;
}
