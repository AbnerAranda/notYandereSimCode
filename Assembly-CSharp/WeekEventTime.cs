using System;
using UnityEngine;

// Token: 0x02000297 RID: 663
[Serializable]
public class WeekEventTime : IScheduledEventTime
{
	// Token: 0x060013EE RID: 5102 RVA: 0x000AE756 File Offset: 0x000AC956
	public WeekEventTime(int week)
	{
		this.week = week;
	}

	// Token: 0x1700037B RID: 891
	// (get) Token: 0x060013EF RID: 5103 RVA: 0x000AE765 File Offset: 0x000AC965
	public ScheduledEventTimeType ScheduleType
	{
		get
		{
			return ScheduledEventTimeType.Week;
		}
	}

	// Token: 0x060013F0 RID: 5104 RVA: 0x000AE768 File Offset: 0x000AC968
	public bool OccurringNow(DateAndTime currentTime)
	{
		return currentTime.Week == this.week;
	}

	// Token: 0x060013F1 RID: 5105 RVA: 0x000AE778 File Offset: 0x000AC978
	public bool OccursInTheFuture(DateAndTime currentTime)
	{
		return currentTime.Week < this.week;
	}

	// Token: 0x060013F2 RID: 5106 RVA: 0x000AE788 File Offset: 0x000AC988
	public bool OccurredInThePast(DateAndTime currentTime)
	{
		return currentTime.Week > this.week;
	}

	// Token: 0x04001BE2 RID: 7138
	[SerializeField]
	private int week;
}
