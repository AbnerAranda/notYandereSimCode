using System;
using UnityEngine;

// Token: 0x020003F3 RID: 1011
public class SpyScript : MonoBehaviour
{
	// Token: 0x06001AEE RID: 6894 RVA: 0x0010F1EC File Offset: 0x0010D3EC
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Yandere.Character.GetComponent<Animation>().CrossFade("f02_spying_00");
			this.Yandere.CanMove = false;
			this.Phase++;
		}
		if (this.Phase == 1)
		{
			Quaternion b = Quaternion.LookRotation(this.SpyTarget.transform.position - this.Yandere.transform.position);
			this.Yandere.transform.rotation = Quaternion.Slerp(this.Yandere.transform.rotation, b, Time.deltaTime * 10f);
			this.Yandere.MoveTowardsTarget(this.SpySpot.position);
			this.Timer += Time.deltaTime;
			if (this.Timer > 1f)
			{
				if (this.Yandere.Inventory.DirectionalMic)
				{
					this.PromptBar.Label[0].text = "Record";
					this.CanRecord = true;
				}
				this.PromptBar.Label[1].text = "Stop";
				this.PromptBar.UpdateButtons();
				this.PromptBar.Show = true;
				this.Yandere.MainCamera.enabled = false;
				this.SpyCamera.SetActive(true);
				this.Phase++;
				return;
			}
		}
		else if (this.Phase == 2)
		{
			if (this.CanRecord && Input.GetButtonDown("A"))
			{
				this.Yandere.Character.GetComponent<Animation>().CrossFade("f02_spyRecord_00");
				this.Yandere.Microphone.SetActive(true);
				this.Recording = true;
			}
			if (Input.GetButtonDown("B"))
			{
				this.End();
			}
		}
	}

	// Token: 0x06001AEF RID: 6895 RVA: 0x0010F3D4 File Offset: 0x0010D5D4
	public void End()
	{
		this.PromptBar.ClearButtons();
		this.PromptBar.Show = false;
		this.Yandere.Microphone.SetActive(false);
		this.Yandere.MainCamera.enabled = true;
		this.Yandere.CanMove = true;
		this.SpyCamera.SetActive(false);
		this.Timer = 0f;
		this.Phase = 0;
	}

	// Token: 0x04002BB4 RID: 11188
	public PromptBarScript PromptBar;

	// Token: 0x04002BB5 RID: 11189
	public YandereScript Yandere;

	// Token: 0x04002BB6 RID: 11190
	public PromptScript Prompt;

	// Token: 0x04002BB7 RID: 11191
	public GameObject SpyCamera;

	// Token: 0x04002BB8 RID: 11192
	public Transform SpyTarget;

	// Token: 0x04002BB9 RID: 11193
	public Transform SpySpot;

	// Token: 0x04002BBA RID: 11194
	public float Timer;

	// Token: 0x04002BBB RID: 11195
	public bool CanRecord;

	// Token: 0x04002BBC RID: 11196
	public bool Recording;

	// Token: 0x04002BBD RID: 11197
	public int Phase;
}
