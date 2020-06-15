using System;
using UnityEngine;

// Token: 0x02000295 RID: 661
[Serializable]
public class TimeOfDayEventTime : IScheduledEventTime
{
	// Token: 0x060013E4 RID: 5092 RVA: 0x000AE5AE File Offset: 0x000AC7AE
	public TimeOfDayEventTime(int week, DayOfWeek weekday, TimeOfDay timeOfDay)
	{
		this.week = week;
		this.weekday = weekday;
		this.timeOfDay = timeOfDay;
	}

	// Token: 0x17000379 RID: 889
	// (get) Token: 0x060013E5 RID: 5093 RVA: 0x00022944 File Offset: 0x00020B44
	public ScheduledEventTimeType ScheduleType
	{
		get
		{
			return ScheduledEventTimeType.TimeOfDay;
		}
	}

	// Token: 0x060013E6 RID: 5094 RVA: 0x000AE5CC File Offset: 0x000AC7CC
	public bool OccurringNow(DateAndTime currentTime)
	{
		bool flag = currentTime.Week == this.week;
		bool flag2 = currentTime.Weekday == this.weekday;
		bool flag3 = currentTime.Clock.TimeOfDay == this.timeOfDay;
		return flag && flag2 && flag3;
	}

	// Token: 0x060013E7 RID: 5095 RVA: 0x000AE610 File Offset: 0x000AC810
	public bool OccursInTheFuture(DateAndTime currentTime)
	{
		if (currentTime.Week != this.week)
		{
			return currentTime.Week < this.week;
		}
		if (currentTime.Weekday == this.weekday)
		{
			return currentTime.Clock.TimeOfDay < this.timeOfDay;
		}
		return currentTime.Weekday < this.weekday;
	}

	// Token: 0x060013E8 RID: 5096 RVA: 0x000AE66C File Offset: 0x000AC86C
	public bool OccurredInThePast(DateAndTime currentTime)
	{
		if (currentTime.Week != this.week)
		{
			return currentTime.Week > this.week;
		}
		if (currentTime.Weekday == this.weekday)
		{
			return currentTime.Clock.TimeOfDay > this.timeOfDay;
		}
		return currentTime.Weekday > this.weekday;
	}

	// Token: 0x04001BDD RID: 7133
	[SerializeField]
	private int week;

	// Token: 0x04001BDE RID: 7134
	[SerializeField]
	private DayOfWeek weekday;

	// Token: 0x04001BDF RID: 7135
	[SerializeField]
	private TimeOfDay timeOfDay;
}
