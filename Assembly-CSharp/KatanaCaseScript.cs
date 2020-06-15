﻿using System;
using UnityEngine;

// Token: 0x02000317 RID: 791
public class KatanaCaseScript : MonoBehaviour
{
	// Token: 0x060017E9 RID: 6121 RVA: 0x000D3315 File Offset: 0x000D1515
	private void Start()
	{
		this.CasePrompt.enabled = false;
	}

	// Token: 0x060017EA RID: 6122 RVA: 0x000D3324 File Offset: 0x000D1524
	private void Update()
	{
		if (this.Key.activeInHierarchy && this.KeyPrompt.Circle[0].fillAmount == 0f)
		{
			this.KeyPrompt.Yandere.Inventory.CaseKey = true;
			this.CasePrompt.HideButton[0] = false;
			this.CasePrompt.enabled = true;
			this.Key.SetActive(false);
		}
		if (this.CasePrompt.Circle[0].fillAmount == 0f)
		{
			this.KeyPrompt.Yandere.Inventory.CaseKey = false;
			this.Open = true;
			this.CasePrompt.Hide();
			this.CasePrompt.enabled = false;
		}
		if (this.CasePrompt.Yandere.Inventory.LockPick)
		{
			this.CasePrompt.HideButton[2] = false;
			this.CasePrompt.enabled = true;
			if (this.CasePrompt.Circle[2].fillAmount == 0f)
			{
				this.KeyPrompt.Hide();
				this.KeyPrompt.enabled = false;
				this.CasePrompt.Yandere.Inventory.LockPick = false;
				this.CasePrompt.Label[0].text = "     Open";
				this.CasePrompt.HideButton[2] = true;
				this.CasePrompt.HideButton[0] = true;
				this.Open = true;
			}
		}
		else if (!this.CasePrompt.HideButton[2])
		{
			this.CasePrompt.HideButton[2] = true;
		}
		if (this.Open)
		{
			this.Rotation = Mathf.Lerp(this.Rotation, -180f, Time.deltaTime * 10f);
			this.Door.eulerAngles = new Vector3(this.Door.eulerAngles.x, this.Door.eulerAngles.y, this.Rotation);
			if (this.Rotation < -179.9f)
			{
				base.enabled = false;
			}
		}
	}

	// Token: 0x04002246 RID: 8774
	public PromptScript CasePrompt;

	// Token: 0x04002247 RID: 8775
	public PromptScript KeyPrompt;

	// Token: 0x04002248 RID: 8776
	public Transform Door;

	// Token: 0x04002249 RID: 8777
	public GameObject Key;

	// Token: 0x0400224A RID: 8778
	public float Rotation;

	// Token: 0x0400224B RID: 8779
	public bool Open;
}
