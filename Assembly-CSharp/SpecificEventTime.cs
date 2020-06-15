using System;
using UnityEngine;

// Token: 0x02000294 RID: 660
[Serializable]
public class SpecificEventTime : IScheduledEventTime
{
	// Token: 0x060013DF RID: 5087 RVA: 0x000AE464 File Offset: 0x000AC664
	public SpecificEventTime(int week, DayOfWeek weekday, Clock startClock, Clock endClock)
	{
		this.week = week;
		this.weekday = weekday;
		this.startClock = startClock;
		this.endClock = endClock;
	}

	// Token: 0x17000378 RID: 888
	// (get) Token: 0x060013E0 RID: 5088 RVA: 0x0002D199 File Offset: 0x0002B399
	public ScheduledEventTimeType ScheduleType
	{
		get
		{
			return ScheduledEventTimeType.Specific;
		}
	}

	// Token: 0x060013E1 RID: 5089 RVA: 0x000AE48C File Offset: 0x000AC68C
	public bool OccurringNow(DateAndTime currentTime)
	{
		bool flag = currentTime.Week == this.week;
		bool flag2 = currentTime.Weekday == this.weekday;
		Clock clock = currentTime.Clock;
		bool flag3 = clock.TotalSeconds >= this.startClock.TotalSeconds && clock.TotalSeconds < this.endClock.TotalSeconds;
		return flag && flag2 && flag3;
	}

	// Token: 0x060013E2 RID: 5090 RVA: 0x000AE4EC File Offset: 0x000AC6EC
	public bool OccursInTheFuture(DateAndTime currentTime)
	{
		if (currentTime.Week != this.week)
		{
			return currentTime.Week < this.week;
		}
		if (currentTime.Weekday == this.weekday)
		{
			return currentTime.Clock.TotalSeconds < this.startClock.TotalSeconds;
		}
		return currentTime.Weekday < this.weekday;
	}

	// Token: 0x060013E3 RID: 5091 RVA: 0x000AE54C File Offset: 0x000AC74C
	public bool OccurredInThePast(DateAndTime currentTime)
	{
		if (currentTime.Week != this.week)
		{
			return currentTime.Week > this.week;
		}
		if (currentTime.Weekday == this.weekday)
		{
			return currentTime.Clock.TotalSeconds >= this.endClock.TotalSeconds;
		}
		return currentTime.Weekday > this.weekday;
	}

	// Token: 0x04001BD9 RID: 7129
	[SerializeField]
	private int week;

	// Token: 0x04001BDA RID: 7130
	[SerializeField]
	private DayOfWeek weekday;

	// Token: 0x04001BDB RID: 7131
	[SerializeField]
	private Clock startClock;

	// Token: 0x04001BDC RID: 7132
	[SerializeField]
	private Clock endClock;
}
