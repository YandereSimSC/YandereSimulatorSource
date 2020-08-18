using System;
using UnityEngine;

// Token: 0x02000295 RID: 661
[Serializable]
public class DayEventTime : IScheduledEventTime
{
	// Token: 0x060013E1 RID: 5089 RVA: 0x000AD4C2 File Offset: 0x000AB6C2
	public DayEventTime(int week, DayOfWeek weekday)
	{
		this.week = week;
		this.weekday = weekday;
	}

	// Token: 0x1700037A RID: 890
	// (get) Token: 0x060013E2 RID: 5090 RVA: 0x00033EEA File Offset: 0x000320EA
	public ScheduledEventTimeType ScheduleType
	{
		get
		{
			return ScheduledEventTimeType.Day;
		}
	}

	// Token: 0x060013E3 RID: 5091 RVA: 0x000AD4D8 File Offset: 0x000AB6D8
	public bool OccurringNow(DateAndTime currentTime)
	{
		return currentTime.Week == this.week && currentTime.Weekday == this.weekday;
	}

	// Token: 0x060013E4 RID: 5092 RVA: 0x000AD4F8 File Offset: 0x000AB6F8
	public bool OccursInTheFuture(DateAndTime currentTime)
	{
		if (currentTime.Week == this.week)
		{
			return currentTime.Weekday < this.weekday;
		}
		return currentTime.Week < this.week;
	}

	// Token: 0x060013E5 RID: 5093 RVA: 0x000AD525 File Offset: 0x000AB725
	public bool OccurredInThePast(DateAndTime currentTime)
	{
		if (currentTime.Week == this.week)
		{
			return currentTime.Weekday > this.weekday;
		}
		return currentTime.Week > this.week;
	}

	// Token: 0x04001BC0 RID: 7104
	[SerializeField]
	private int week;

	// Token: 0x04001BC1 RID: 7105
	[SerializeField]
	private DayOfWeek weekday;
}
