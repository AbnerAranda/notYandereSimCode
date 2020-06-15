using System;
using UnityEngine;

// Token: 0x0200041A RID: 1050
public class TapePlayerScript : MonoBehaviour
{
	// Token: 0x06001C29 RID: 7209 RVA: 0x0015195A File Offset: 0x0014FB5A
	private void Start()
	{
		this.Tape.SetActive(false);
	}

	// Token: 0x06001C2A RID: 7210 RVA: 0x00151968 File Offset: 0x0014FB68
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Yandere.HeartCamera.enabled = false;
			this.Yandere.RPGCamera.enabled = false;
			this.TapePlayerMenu.TimeBar.gameObject.SetActive(true);
			this.TapePlayerMenu.List.gameObject.SetActive(true);
			this.TapePlayerCamera.enabled = true;
			this.TapePlayerMenu.UpdateLabels();
			this.TapePlayerMenu.Show = true;
			this.NoteWindow.SetActive(false);
			this.Yandere.CanMove = false;
			this.Yandere.HUD.alpha = 0f;
			Time.timeScale = 0.0001f;
			this.PromptBar.ClearButtons();
			this.PromptBar.Label[1].text = "EXIT";
			this.PromptBar.Label[4].text = "CHOOSE";
			this.PromptBar.Label[5].text = "CATEGORY";
			this.TapePlayerMenu.CheckSelection();
			this.PromptBar.Show = true;
			this.Prompt.Hide();
			this.Prompt.enabled = false;
		}
		if (this.Spin)
		{
			Transform transform = this.Rolls[0];
			transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + 0.0166666675f * (360f * this.SpinSpeed), transform.localEulerAngles.z);
			Transform transform2 = this.Rolls[1];
			transform2.localEulerAngles = new Vector3(transform2.localEulerAngles.x, transform2.localEulerAngles.y + 0.0166666675f * (360f * this.SpinSpeed), transform2.localEulerAngles.z);
		}
		if (this.FastForward)
		{
			this.FFButton.localEulerAngles = new Vector3(Mathf.MoveTowards(this.FFButton.localEulerAngles.x, 6.25f, 1.66666663f), this.FFButton.localEulerAngles.y, this.FFButton.localEulerAngles.z);
			this.SpinSpeed = 2f;
		}
		else
		{
			this.FFButton.localEulerAngles = new Vector3(Mathf.MoveTowards(this.FFButton.localEulerAngles.x, 0f, 1.66666663f), this.FFButton.localEulerAngles.y, this.FFButton.localEulerAngles.z);
			this.SpinSpeed = 1f;
		}
		if (this.Rewind)
		{
			this.RWButton.localEulerAngles = new Vector3(Mathf.MoveTowards(this.RWButton.localEulerAngles.x, 6.25f, 1.66666663f), this.RWButton.localEulerAngles.y, this.RWButton.localEulerAngles.z);
			this.SpinSpeed = -2f;
			return;
		}
		this.RWButton.localEulerAngles = new Vector3(Mathf.MoveTowards(this.RWButton.localEulerAngles.x, 0f, 1.66666663f), this.RWButton.localEulerAngles.y, this.RWButton.localEulerAngles.z);
	}

	// Token: 0x040034BD RID: 13501
	public TapePlayerMenuScript TapePlayerMenu;

	// Token: 0x040034BE RID: 13502
	public PromptBarScript PromptBar;

	// Token: 0x040034BF RID: 13503
	public YandereScript Yandere;

	// Token: 0x040034C0 RID: 13504
	public PromptScript Prompt;

	// Token: 0x040034C1 RID: 13505
	public Transform RWButton;

	// Token: 0x040034C2 RID: 13506
	public Transform FFButton;

	// Token: 0x040034C3 RID: 13507
	public Camera TapePlayerCamera;

	// Token: 0x040034C4 RID: 13508
	public Transform[] Rolls;

	// Token: 0x040034C5 RID: 13509
	public GameObject NoteWindow;

	// Token: 0x040034C6 RID: 13510
	public GameObject Tape;

	// Token: 0x040034C7 RID: 13511
	public bool FastForward;

	// Token: 0x040034C8 RID: 13512
	public bool Rewind;

	// Token: 0x040034C9 RID: 13513
	public bool Spin;

	// Token: 0x040034CA RID: 13514
	public float SpinSpeed;
}
