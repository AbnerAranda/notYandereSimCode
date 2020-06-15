using System;
using UnityEngine;

// Token: 0x0200041F RID: 1055
public class TaskWindowScript : MonoBehaviour
{
	// Token: 0x06001C39 RID: 7225 RVA: 0x001527C1 File Offset: 0x001509C1
	private void Start()
	{
		this.Window.SetActive(false);
		this.UpdateTaskObjects(30);
	}

	// Token: 0x06001C3A RID: 7226 RVA: 0x001527D8 File Offset: 0x001509D8
	public void UpdateWindow(int ID)
	{
		this.PromptBar.ClearButtons();
		this.PromptBar.Label[0].text = "Accept";
		this.PromptBar.Label[1].text = "Refuse";
		this.PromptBar.UpdateButtons();
		this.PromptBar.Show = true;
		this.GetPortrait(ID);
		this.StudentID = ID;
		this.GenericCheck();
		if (this.Generic)
		{
			ID = 0;
			this.Generic = false;
		}
		this.TaskDescLabel.transform.parent.gameObject.SetActive(true);
		this.TaskDescLabel.text = this.Descriptions[ID];
		this.Icon.mainTexture = this.Icons[ID];
		this.Window.SetActive(true);
		Time.timeScale = 0.0001f;
	}

	// Token: 0x06001C3B RID: 7227 RVA: 0x001528B4 File Offset: 0x00150AB4
	private void Update()
	{
		if (this.Window.activeInHierarchy)
		{
			if (Input.GetButtonDown("A"))
			{
				TaskGlobals.SetTaskStatus(this.StudentID, 1);
				this.Yandere.TargetStudent.TalkTimer = 100f;
				this.Yandere.TargetStudent.Interaction = StudentInteractionType.GivingTask;
				this.Yandere.TargetStudent.TaskPhase = 4;
				this.PromptBar.ClearButtons();
				this.PromptBar.Show = false;
				this.Window.SetActive(false);
				this.UpdateTaskObjects(this.StudentID);
				Time.timeScale = 1f;
			}
			else if (Input.GetButtonDown("B"))
			{
				this.Yandere.TargetStudent.TalkTimer = 100f;
				this.Yandere.TargetStudent.Interaction = StudentInteractionType.GivingTask;
				this.Yandere.TargetStudent.TaskPhase = 0;
				this.PromptBar.ClearButtons();
				this.PromptBar.Show = false;
				this.Window.SetActive(false);
				Time.timeScale = 1f;
			}
		}
		if (this.TaskComplete)
		{
			if (this.TrueTimer == 0f)
			{
				base.GetComponent<AudioSource>().Play();
			}
			this.TrueTimer += Time.deltaTime;
			this.Timer += Time.deltaTime;
			if (this.ID < this.TaskCompleteLetters.Length && this.Timer > 0.05f)
			{
				this.TaskCompleteLetters[this.ID].SetActive(true);
				this.Timer = 0f;
				this.ID++;
			}
			if (this.TaskCompleteLetters[12].transform.localPosition.y < -725f)
			{
				this.ID = 0;
				while (this.ID < this.TaskCompleteLetters.Length)
				{
					this.TaskCompleteLetters[this.ID].GetComponent<GrowShrinkScript>().Return();
					this.ID++;
				}
				this.TaskCheck();
				this.DialogueWheel.End();
				this.TaskComplete = false;
				this.TrueTimer = 0f;
				this.Timer = 0f;
				this.ID = 0;
			}
		}
	}

	// Token: 0x06001C3C RID: 7228 RVA: 0x00152AF0 File Offset: 0x00150CF0
	private void TaskCheck()
	{
		if (this.Yandere.TargetStudent.StudentID == 37)
		{
			this.DialogueWheel.Yandere.TargetStudent.Cosmetic.MaleAccessories[1].SetActive(true);
		}
		this.GenericCheck();
		if (this.Generic)
		{
			this.Yandere.Inventory.Book = false;
			this.CheckOutBook.UpdatePrompt();
			this.Generic = false;
		}
	}

	// Token: 0x06001C3D RID: 7229 RVA: 0x00152B64 File Offset: 0x00150D64
	private void GetPortrait(int ID)
	{
		WWW www = new WWW(string.Concat(new string[]
		{
			"file:///",
			Application.streamingAssetsPath,
			"/Portraits/Student_",
			ID.ToString(),
			".png"
		}));
		this.Portrait.mainTexture = www.texture;
	}

	// Token: 0x06001C3E RID: 7230 RVA: 0x00152BBD File Offset: 0x00150DBD
	private void UpdateTaskObjects(int StudentID)
	{
		if (this.StudentID == 30)
		{
			this.SewingMachine.Check = true;
		}
	}

	// Token: 0x06001C3F RID: 7231 RVA: 0x00152BD8 File Offset: 0x00150DD8
	public void GenericCheck()
	{
		this.Generic = false;
		if (this.Yandere.TargetStudent.StudentID != 8 && this.Yandere.TargetStudent.StudentID != 11 && this.Yandere.TargetStudent.StudentID != 25 && this.Yandere.TargetStudent.StudentID != 28 && this.Yandere.TargetStudent.StudentID != 30 && this.Yandere.TargetStudent.StudentID != 36 && this.Yandere.TargetStudent.StudentID != 37 && this.Yandere.TargetStudent.StudentID != 38 && this.Yandere.TargetStudent.StudentID != 52 && this.Yandere.TargetStudent.StudentID != 76 && this.Yandere.TargetStudent.StudentID != 77 && this.Yandere.TargetStudent.StudentID != 78 && this.Yandere.TargetStudent.StudentID != 79 && this.Yandere.TargetStudent.StudentID != 80 && this.Yandere.TargetStudent.StudentID != 81)
		{
			this.Generic = true;
		}
	}

	// Token: 0x06001C40 RID: 7232 RVA: 0x00152D38 File Offset: 0x00150F38
	public void AltGenericCheck(int TempID)
	{
		this.Generic = false;
		if (TempID != 8 && TempID != 11 && TempID != 25 && TempID != 28 && TempID != 30 && TempID != 36 && TempID != 37 && TempID != 38 && TempID != 52 && TempID != 76 && TempID != 77 && TempID != 78 && TempID != 79 && TempID != 80 && TempID != 81)
		{
			this.Generic = true;
		}
	}

	// Token: 0x040034E7 RID: 13543
	public DialogueWheelScript DialogueWheel;

	// Token: 0x040034E8 RID: 13544
	public SewingMachineScript SewingMachine;

	// Token: 0x040034E9 RID: 13545
	public CheckOutBookScript CheckOutBook;

	// Token: 0x040034EA RID: 13546
	public TaskManagerScript TaskManager;

	// Token: 0x040034EB RID: 13547
	public PromptBarScript PromptBar;

	// Token: 0x040034EC RID: 13548
	public UILabel TaskDescLabel;

	// Token: 0x040034ED RID: 13549
	public YandereScript Yandere;

	// Token: 0x040034EE RID: 13550
	public UITexture Portrait;

	// Token: 0x040034EF RID: 13551
	public UITexture Icon;

	// Token: 0x040034F0 RID: 13552
	public GameObject[] TaskCompleteLetters;

	// Token: 0x040034F1 RID: 13553
	public string[] Descriptions;

	// Token: 0x040034F2 RID: 13554
	public Texture[] Portraits;

	// Token: 0x040034F3 RID: 13555
	public Texture[] Icons;

	// Token: 0x040034F4 RID: 13556
	public bool TaskComplete;

	// Token: 0x040034F5 RID: 13557
	public bool Generic;

	// Token: 0x040034F6 RID: 13558
	public GameObject Window;

	// Token: 0x040034F7 RID: 13559
	public int StudentID;

	// Token: 0x040034F8 RID: 13560
	public int ID;

	// Token: 0x040034F9 RID: 13561
	public float TrueTimer;

	// Token: 0x040034FA RID: 13562
	public float Timer;
}
