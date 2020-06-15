using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200041C RID: 1052
public class TaskListScript : MonoBehaviour
{
	// Token: 0x06001C2F RID: 7215 RVA: 0x00151E44 File Offset: 0x00150044
	private void Update()
	{
		if (this.InputManager.TappedUp)
		{
			if (this.ID == 1)
			{
				this.ListPosition--;
				if (this.ListPosition < 0)
				{
					this.ListPosition = 84;
					this.ID = 16;
				}
			}
			else
			{
				this.ID--;
			}
			this.UpdateTaskList();
			base.StartCoroutine(this.UpdateTaskInfo());
		}
		if (this.InputManager.TappedDown)
		{
			if (this.ID == 16)
			{
				this.ListPosition++;
				if (this.ListPosition > 84)
				{
					this.ListPosition = 0;
					this.ID = 1;
				}
			}
			else
			{
				this.ID++;
			}
			this.UpdateTaskList();
			base.StartCoroutine(this.UpdateTaskInfo());
		}
		if (Input.GetButtonDown("B"))
		{
			this.PauseScreen.PromptBar.ClearButtons();
			this.PauseScreen.PromptBar.Label[0].text = "Accept";
			this.PauseScreen.PromptBar.Label[1].text = "Back";
			this.PauseScreen.PromptBar.Label[4].text = "Choose";
			this.PauseScreen.PromptBar.Label[5].text = "Choose";
			this.PauseScreen.PromptBar.UpdateButtons();
			this.PauseScreen.Sideways = false;
			this.PauseScreen.PressedB = true;
			this.MainMenu.SetActive(true);
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x06001C30 RID: 7216 RVA: 0x00151FE0 File Offset: 0x001501E0
	public void UpdateTaskList()
	{
		for (int i = 1; i < this.TaskNameLabels.Length; i++)
		{
			if (TaskGlobals.GetTaskStatus(i + this.ListPosition) == 0)
			{
				this.TaskNameLabels[i].text = "Undiscovered Task #" + (i + this.ListPosition);
			}
			else
			{
				this.TaskNameLabels[i].text = this.JSON.Students[i + this.ListPosition].Name + "'s Task";
			}
			this.Checkmarks[i].enabled = (TaskGlobals.GetTaskStatus(i + this.ListPosition) == 3);
		}
	}

	// Token: 0x06001C31 RID: 7217 RVA: 0x00152088 File Offset: 0x00150288
	public IEnumerator UpdateTaskInfo()
	{
		this.Highlight.localPosition = new Vector3(this.Highlight.localPosition.x, 200f - 25f * (float)this.ID, this.Highlight.localPosition.z);
		if (TaskGlobals.GetTaskStatus(this.ID + this.ListPosition) == 0)
		{
			this.StudentIcon.mainTexture = this.Silhouette;
			this.TaskIcon.mainTexture = this.QuestionMark;
			this.TaskDesc.text = "This task has not been discovered yet.";
		}
		else
		{
			string url = string.Concat(new string[]
			{
				"file:///",
				Application.streamingAssetsPath,
				"/Portraits/Student_",
				(this.ID + this.ListPosition).ToString(),
				".png"
			});
			WWW www = new WWW(url);
			yield return www;
			this.StudentIcon.mainTexture = www.texture;
			this.TaskWindow.AltGenericCheck(this.ID + this.ListPosition);
			if (this.TaskWindow.Generic)
			{
				this.TaskIcon.mainTexture = this.TaskWindow.Icons[0];
				this.TaskDesc.text = this.TaskWindow.Descriptions[0];
			}
			else
			{
				this.TaskIcon.mainTexture = this.TaskWindow.Icons[this.ID + this.ListPosition];
				this.TaskDesc.text = this.TaskWindow.Descriptions[this.ID + this.ListPosition];
			}
			www = null;
		}
		yield break;
	}

	// Token: 0x040034D1 RID: 13521
	public InputManagerScript InputManager;

	// Token: 0x040034D2 RID: 13522
	public PauseScreenScript PauseScreen;

	// Token: 0x040034D3 RID: 13523
	public TaskWindowScript TaskWindow;

	// Token: 0x040034D4 RID: 13524
	public JsonScript JSON;

	// Token: 0x040034D5 RID: 13525
	public GameObject MainMenu;

	// Token: 0x040034D6 RID: 13526
	public UITexture StudentIcon;

	// Token: 0x040034D7 RID: 13527
	public UITexture TaskIcon;

	// Token: 0x040034D8 RID: 13528
	public UILabel TaskDesc;

	// Token: 0x040034D9 RID: 13529
	public Texture QuestionMark;

	// Token: 0x040034DA RID: 13530
	public Transform Highlight;

	// Token: 0x040034DB RID: 13531
	public Texture Silhouette;

	// Token: 0x040034DC RID: 13532
	public UILabel[] TaskNameLabels;

	// Token: 0x040034DD RID: 13533
	public UISprite[] Checkmarks;

	// Token: 0x040034DE RID: 13534
	public int ListPosition;

	// Token: 0x040034DF RID: 13535
	public int ID = 1;
}
