using System;

// Token: 0x02000292 RID: 658
public interface IScheduledEventTime
{
	// Token: 0x17000377 RID: 887
	// (get) Token: 0x060013D3 RID: 5075
	ScheduledEventTimeType ScheduleType { get; }

	// Token: 0x060013D4 RID: 5076
	bool OccurringNow(DateAndTime currentTime);

	// Token: 0x060013D5 RID: 5077
	bool OccursInTheFuture(DateAndTime currentTime);

	// Token: 0x060013D6 RID: 5078
	bool OccurredInThePast(DateAndTime currentTime);
}
