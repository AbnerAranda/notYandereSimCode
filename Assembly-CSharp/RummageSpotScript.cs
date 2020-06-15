using System;
using UnityEngine;

// Token: 0x020003AC RID: 940
public class RummageSpotScript : MonoBehaviour
{
	// Token: 0x060019F4 RID: 6644 RVA: 0x000FED70 File Offset: 0x000FCF70
	private void Start()
	{
		if (this.ID == 1)
		{
			if (GameGlobals.AnswerSheetUnavailable)
			{
				Debug.Log("The answer sheet is no longer available, due to events on a previous day.");
				this.Prompt.Hide();
				this.Prompt.enabled = false;
				base.gameObject.SetActive(false);
				return;
			}
			if (DateGlobals.Weekday == DayOfWeek.Friday && this.Clock.HourTime > 13.5f)
			{
				this.Prompt.Hide();
				this.Prompt.enabled = false;
				base.gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x060019F5 RID: 6645 RVA: 0x000FEDF8 File Offset: 0x000FCFF8
	private void Update()
	{
		AudioSource component = base.GetComponent<AudioSource>();
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Circle[0].fillAmount = 1f;
			if (!this.Yandere.Chased && this.Yandere.Chasers == 0)
			{
				this.Yandere.EmptyHands();
				this.Yandere.CharacterAnimation.CrossFade("f02_rummage_00");
				this.Yandere.ProgressBar.transform.parent.gameObject.SetActive(true);
				this.Yandere.RummageSpot = this;
				this.Yandere.Rummaging = true;
				this.Yandere.CanMove = false;
				component.Play();
			}
		}
		if (this.Yandere.Rummaging)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.AlarmDisc, base.transform.position, Quaternion.identity);
			gameObject.GetComponent<AlarmDiscScript>().NoScream = true;
			gameObject.transform.localScale = new Vector3(750f, gameObject.transform.localScale.y, 750f);
		}
		if (this.Yandere.Noticed)
		{
			component.Stop();
		}
	}

	// Token: 0x060019F6 RID: 6646 RVA: 0x000FEF38 File Offset: 0x000FD138
	public void GetReward()
	{
		if (this.ID == 1)
		{
			if (this.Phase == 1)
			{
				SchemeGlobals.SetSchemeStage(5, 5);
				this.Schemes.UpdateInstructions();
				this.Yandere.Inventory.AnswerSheet = true;
				this.Prompt.Hide();
				this.Prompt.enabled = false;
				this.DoorGap.Prompt.enabled = true;
				this.Phase++;
				return;
			}
			if (this.Phase == 2)
			{
				SchemeGlobals.SetSchemeStage(5, 8);
				this.Schemes.UpdateInstructions();
				this.Prompt.Yandere.Inventory.AnswerSheet = false;
				this.Prompt.Hide();
				this.Prompt.enabled = false;
				base.gameObject.SetActive(false);
				this.Phase++;
			}
		}
	}

	// Token: 0x040028C2 RID: 10434
	public GameObject AlarmDisc;

	// Token: 0x040028C3 RID: 10435
	public DoorGapScript DoorGap;

	// Token: 0x040028C4 RID: 10436
	public SchemesScript Schemes;

	// Token: 0x040028C5 RID: 10437
	public YandereScript Yandere;

	// Token: 0x040028C6 RID: 10438
	public PromptScript Prompt;

	// Token: 0x040028C7 RID: 10439
	public ClockScript Clock;

	// Token: 0x040028C8 RID: 10440
	public Transform Target;

	// Token: 0x040028C9 RID: 10441
	public int Phase;

	// Token: 0x040028CA RID: 10442
	public int ID;
}
