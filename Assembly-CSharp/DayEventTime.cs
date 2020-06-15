using System;
using UnityEngine;

// Token: 0x02000296 RID: 662
[Serializable]
public class DayEventTime : IScheduledEventTime
{
	// Token: 0x060013E9 RID: 5097 RVA: 0x000AE6C6 File Offset: 0x000AC8C6
	public DayEventTime(int week, DayOfWeek weekday)
	{
		this.week = week;
		this.weekday = weekday;
	}

	// Token: 0x1700037A RID: 890
	// (get) Token: 0x060013EA RID: 5098 RVA: 0x00033F12 File Offset: 0x00032112
	public ScheduledEventTimeType ScheduleType
	{
		get
		{
			return ScheduledEventTimeType.Day;
		}
	}

	// Token: 0x060013EB RID: 5099 RVA: 0x000AE6DC File Offset: 0x000AC8DC
	public bool OccurringNow(DateAndTime currentTime)
	{
		return currentTime.Week == this.week && currentTime.Weekday == this.weekday;
	}

	// Token: 0x060013EC RID: 5100 RVA: 0x000AE6FC File Offset: 0x000AC8FC
	public bool OccursInTheFuture(DateAndTime currentTime)
	{
		if (currentTime.Week == this.week)
		{
			return currentTime.Weekday < this.weekday;
		}
		return currentTime.Week < this.week;
	}

	// Token: 0x060013ED RID: 5101 RVA: 0x000AE729 File Offset: 0x000AC929
	public bool OccurredInThePast(DateAndTime currentTime)
	{
		if (currentTime.Week == this.week)
		{
			return currentTime.Weekday > this.weekday;
		}
		return currentTime.Week > this.week;
	}

	// Token: 0x04001BE0 RID: 7136
	[SerializeField]
	private int week;

	// Token: 0x04001BE1 RID: 7137
	[SerializeField]
	private DayOfWeek weekday;
}
