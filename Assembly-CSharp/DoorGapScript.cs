using System;
using UnityEngine;

// Token: 0x0200026F RID: 623
public class DoorGapScript : MonoBehaviour
{
	// Token: 0x06001359 RID: 4953 RVA: 0x000A5583 File Offset: 0x000A3783
	private void Start()
	{
		this.Papers[1].gameObject.SetActive(false);
	}

	// Token: 0x0600135A RID: 4954 RVA: 0x000A5598 File Offset: 0x000A3798
	private void Update()
	{
		if (!this.StolenPhoneDropoff)
		{
			if (this.Prompt.Circle[0].fillAmount == 0f)
			{
				if (this.Phase == 1)
				{
					this.Prompt.Hide();
					this.Prompt.enabled = false;
					this.Prompt.Yandere.Inventory.AnswerSheet = false;
					this.Papers[1].gameObject.SetActive(true);
					SchemeGlobals.SetSchemeStage(5, 6);
					this.Schemes.UpdateInstructions();
					base.GetComponent<AudioSource>().Play();
				}
				else
				{
					this.Prompt.Hide();
					this.Prompt.enabled = false;
					this.Prompt.Yandere.Inventory.AnswerSheet = true;
					this.Prompt.Yandere.Inventory.DuplicateSheet = true;
					this.Papers[2].gameObject.SetActive(false);
					this.RummageSpot.Prompt.Label[0].text = "     Return Answer Sheet";
					this.RummageSpot.Prompt.enabled = true;
					SchemeGlobals.SetSchemeStage(5, 7);
					this.Schemes.UpdateInstructions();
				}
				this.Phase++;
			}
			if (this.Phase == 2)
			{
				this.Timer += Time.deltaTime;
				if (this.Timer > 4f)
				{
					this.Prompt.Label[0].text = "     Pick Up Sheets";
					this.Prompt.enabled = true;
					this.Phase = 2;
					return;
				}
				if (this.Timer > 3f)
				{
					Transform transform = this.Papers[2];
					transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, Mathf.Lerp(transform.localPosition.z, -0.166f, Time.deltaTime * 10f));
					return;
				}
				if (this.Timer > 1f)
				{
					Transform transform2 = this.Papers[1];
					transform2.localPosition = new Vector3(transform2.localPosition.x, transform2.localPosition.y, Mathf.Lerp(transform2.localPosition.z, 0.166f, Time.deltaTime * 10f));
					return;
				}
			}
		}
		else
		{
			if (this.Prompt.Circle[0].fillAmount == 0f)
			{
				this.Prompt.Circle[0].fillAmount = 1f;
				if (this.Phase == 1)
				{
					if (StudentGlobals.GetStudentPhoneStolen(this.Prompt.Yandere.StudentManager.CommunalLocker.RivalPhone.StudentID))
					{
						this.Prompt.Yandere.NotificationManager.CustomText = "Info-chan doesn't need this phone";
						this.Prompt.Yandere.NotificationManager.DisplayNotification(NotificationType.Custom);
					}
					else
					{
						this.Prompt.Hide();
						this.Prompt.enabled = false;
						this.Prompt.Yandere.Inventory.RivalPhone = false;
						this.Prompt.Yandere.RivalPhone = false;
						this.PhoneHacked[this.Prompt.Yandere.StudentManager.CommunalLocker.RivalPhone.StudentID] = true;
						this.Prompt.Yandere.Inventory.PantyShots += 10;
						this.Papers[1].gameObject.SetActive(true);
						base.GetComponent<AudioSource>().Play();
						this.Phase++;
					}
				}
				else if (this.Phase == 2)
				{
					this.Prompt.Yandere.Inventory.RivalPhone = true;
					this.Papers[1].gameObject.SetActive(false);
					this.Prompt.Hide();
					this.Prompt.enabled = false;
					this.Phase++;
				}
			}
			if (this.Phase == 2)
			{
				this.Timer += Time.deltaTime;
				if (this.Timer > 4f)
				{
					this.Prompt.Label[0].text = "     Pick Up Phone";
					this.Prompt.enabled = true;
					return;
				}
				if (this.Timer > 3f)
				{
					this.Papers[1].localPosition = new Vector3(this.Papers[1].localPosition.x, this.Papers[1].localPosition.y, Mathf.Lerp(this.Papers[1].localPosition.z, -0.166f, Time.deltaTime * 10f));
					return;
				}
				if (this.Timer > 1f)
				{
					this.Papers[1].localPosition = new Vector3(this.Papers[1].localPosition.x, this.Papers[1].localPosition.y, Mathf.Lerp(this.Papers[1].localPosition.z, 0.166f, Time.deltaTime * 10f));
				}
			}
		}
	}

	// Token: 0x0600135B RID: 4955 RVA: 0x000A5AAC File Offset: 0x000A3CAC
	public void SetPhonesHacked()
	{
		for (int i = 1; i < 101; i++)
		{
			if (this.PhoneHacked[i])
			{
				StudentGlobals.SetStudentPhoneStolen(i, true);
			}
		}
	}

	// Token: 0x04001A5C RID: 6748
	public RummageSpotScript RummageSpot;

	// Token: 0x04001A5D RID: 6749
	public SchemesScript Schemes;

	// Token: 0x04001A5E RID: 6750
	public PromptScript Prompt;

	// Token: 0x04001A5F RID: 6751
	public Transform[] Papers;

	// Token: 0x04001A60 RID: 6752
	public bool[] PhoneHacked;

	// Token: 0x04001A61 RID: 6753
	public bool StolenPhoneDropoff;

	// Token: 0x04001A62 RID: 6754
	public float Timer;

	// Token: 0x04001A63 RID: 6755
	public int Phase = 1;
}
