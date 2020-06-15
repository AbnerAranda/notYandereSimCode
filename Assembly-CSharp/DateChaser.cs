using System;
using UnityEngine;

// Token: 0x02000258 RID: 600
public class DateChaser : MonoBehaviour
{
	// Token: 0x060012F6 RID: 4854 RVA: 0x00099B88 File Offset: 0x00097D88
	private static DateTime fromUnix(long unix)
	{
		return DateChaser.epoch.AddSeconds((double)unix);
	}

	// Token: 0x060012F7 RID: 4855 RVA: 0x00099BA4 File Offset: 0x00097DA4
	private void Start()
	{
		Application.targetFrameRate = 60;
		Time.timeScale = 1f;
	}

	// Token: 0x060012F8 RID: 4856 RVA: 0x00099BB8 File Offset: 0x00097DB8
	private void Update()
	{
		if (this.Animate)
		{
			float num = Time.time - this.startTime;
			this.CurrentDate = (int)Mathf.Lerp((float)this.startDate, (float)this.endDate, this.curve.Evaluate(num / this.generalDuration));
			DateTime dateTime = DateChaser.fromUnix((long)this.CurrentDate);
			string text = (dateTime.Day == 22 || dateTime.Day == 2) ? "nd" : ((dateTime.Day == 3) ? "rd" : ((dateTime.Day == 1) ? "st" : "th"));
			this.CurrentTimeString = string.Format("{0} {1}{2}, {3}", new object[]
			{
				this.monthNames[dateTime.Month - 1],
				dateTime.Day,
				text,
				dateTime.Year
			});
			if (this.lastFrameDay != dateTime.Day)
			{
				this.onDayTick(dateTime.Day);
			}
			this.lastFrameDay = dateTime.Day;
			this.Timer += Time.deltaTime;
			return;
		}
		this.startTime = Time.time;
		this.CurrentDate = this.startDate;
	}

	// Token: 0x060012F9 RID: 4857 RVA: 0x00099CF9 File Offset: 0x00097EF9
	private void onDayTick(int day)
	{
		this.Label.text = this.CurrentTimeString;
	}

	// Token: 0x040018DF RID: 6367
	public int CurrentDate;

	// Token: 0x040018E0 RID: 6368
	public string CurrentTimeString;

	// Token: 0x040018E1 RID: 6369
	[Header("Epoch timestamps")]
	[SerializeField]
	private int startDate = 1581724799;

	// Token: 0x040018E2 RID: 6370
	[SerializeField]
	private int endDate = 1421366399;

	// Token: 0x040018E3 RID: 6371
	[Space(5f)]
	[Header("Settings")]
	[SerializeField]
	private float generalDuration = 10f;

	// Token: 0x040018E4 RID: 6372
	[SerializeField]
	private AnimationCurve curve;

	// Token: 0x040018E5 RID: 6373
	public bool Animate;

	// Token: 0x040018E6 RID: 6374
	private float startTime;

	// Token: 0x040018E7 RID: 6375
	private string[] monthNames = new string[]
	{
		"January",
		"February",
		"March",
		"April",
		"May",
		"June",
		"July",
		"August",
		"September",
		"October",
		"November",
		"December"
	};

	// Token: 0x040018E8 RID: 6376
	private int lastFrameDay;

	// Token: 0x040018E9 RID: 6377
	private static readonly DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

	// Token: 0x040018EA RID: 6378
	public UILabel Label;

	// Token: 0x040018EB RID: 6379
	public float Timer;

	// Token: 0x040018EC RID: 6380
	public int Stage;
}
