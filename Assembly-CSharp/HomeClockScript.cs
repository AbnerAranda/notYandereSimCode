using System;
using System.Globalization;
using UnityEngine;

// Token: 0x020002E9 RID: 745
public class HomeClockScript : MonoBehaviour
{
	// Token: 0x06001722 RID: 5922 RVA: 0x000C49D4 File Offset: 0x000C2BD4
	private void Start()
	{
		this.DayLabel.text = this.GetWeekdayText(DateGlobals.Weekday);
		if (HomeGlobals.Night)
		{
			this.HourLabel.text = "8:00 PM";
		}
		else
		{
			this.HourLabel.text = (HomeGlobals.LateForSchool ? "7:30 AM" : "6:30 AM");
		}
		this.UpdateMoneyLabel();
	}

	// Token: 0x06001723 RID: 5923 RVA: 0x000C4A34 File Offset: 0x000C2C34
	private void Update()
	{
		if (this.ShakeMoney)
		{
			this.Shake = Mathf.MoveTowards(this.Shake, 0f, Time.deltaTime * 10f);
			this.MoneyLabel.transform.localPosition = new Vector3(1020f + UnityEngine.Random.Range(this.Shake * -1f, this.Shake * 1f), 375f + UnityEngine.Random.Range(this.Shake * -1f, this.Shake * 1f), 0f);
			this.G = Mathf.MoveTowards(this.G, 0.75f, Time.deltaTime);
			this.B = Mathf.MoveTowards(this.B, 1f, Time.deltaTime);
			this.MoneyLabel.color = new Color(1f, this.G, this.B, 1f);
			if (this.Shake == 0f)
			{
				this.ShakeMoney = false;
			}
		}
	}

	// Token: 0x06001724 RID: 5924 RVA: 0x000C4B40 File Offset: 0x000C2D40
	private string GetWeekdayText(DayOfWeek weekday)
	{
		if (weekday == DayOfWeek.Sunday)
		{
			return "SUNDAY";
		}
		if (weekday == DayOfWeek.Monday)
		{
			return "MONDAY";
		}
		if (weekday == DayOfWeek.Tuesday)
		{
			return "TUESDAY";
		}
		if (weekday == DayOfWeek.Wednesday)
		{
			return "WEDNESDAY";
		}
		if (weekday == DayOfWeek.Thursday)
		{
			return "THURSDAY";
		}
		if (weekday == DayOfWeek.Friday)
		{
			return "FRIDAY";
		}
		return "SATURDAY";
	}

	// Token: 0x06001725 RID: 5925 RVA: 0x000C4B90 File Offset: 0x000C2D90
	public void UpdateMoneyLabel()
	{
		this.MoneyLabel.text = "$" + PlayerGlobals.Money.ToString("F2", NumberFormatInfo.InvariantInfo);
	}

	// Token: 0x06001726 RID: 5926 RVA: 0x000C4BC9 File Offset: 0x000C2DC9
	public void MoneyFail()
	{
		this.ShakeMoney = true;
		this.Shake = 10f;
		this.G = 0f;
		this.B = 0f;
		this.MyAudio.Play();
	}

	// Token: 0x04001F71 RID: 8049
	public UILabel MoneyLabel;

	// Token: 0x04001F72 RID: 8050
	public UILabel HourLabel;

	// Token: 0x04001F73 RID: 8051
	public UILabel DayLabel;

	// Token: 0x04001F74 RID: 8052
	public AudioSource MyAudio;

	// Token: 0x04001F75 RID: 8053
	public bool ShakeMoney;

	// Token: 0x04001F76 RID: 8054
	public float Shake;

	// Token: 0x04001F77 RID: 8055
	public float G;

	// Token: 0x04001F78 RID: 8056
	public float B;
}
