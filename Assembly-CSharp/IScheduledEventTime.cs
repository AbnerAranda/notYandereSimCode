using System;

// Token: 0x02000293 RID: 659
public interface IScheduledEventTime
{
	// Token: 0x17000377 RID: 887
	// (get) Token: 0x060013DB RID: 5083
	ScheduledEventTimeType ScheduleType { get; }

	// Token: 0x060013DC RID: 5084
	bool OccurringNow(DateAndTime currentTime);

	// Token: 0x060013DD RID: 5085
	bool OccursInTheFuture(DateAndTime currentTime);

	// Token: 0x060013DE RID: 5086
	bool OccurredInThePast(DateAndTime currentTime);
}
