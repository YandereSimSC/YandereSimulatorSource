using System;
using UnityEngine;

// Token: 0x02000294 RID: 660
[Serializable]
public class TimeOfDayEventTime : IScheduledEventTime
{
	// Token: 0x060013DC RID: 5084 RVA: 0x000AD3AA File Offset: 0x000AB5AA
	public TimeOfDayEventTime(int week, DayOfWeek weekday, TimeOfDay timeOfDay)
	{
		this.week = week;
		this.weekday = weekday;
		this.timeOfDay = timeOfDay;
	}

	// Token: 0x17000379 RID: 889
	// (get) Token: 0x060013DD RID: 5085 RVA: 0x0002291C File Offset: 0x00020B1C
	public ScheduledEventTimeType ScheduleType
	{
		get
		{
			return ScheduledEventTimeType.TimeOfDay;
		}
	}

	// Token: 0x060013DE RID: 5086 RVA: 0x000AD3C8 File Offset: 0x000AB5C8
	public bool OccurringNow(DateAndTime currentTime)
	{
		bool flag = currentTime.Week == this.week;
		bool flag2 = currentTime.Weekday == this.weekday;
		bool flag3 = currentTime.Clock.TimeOfDay == this.timeOfDay;
		return flag && flag2 && flag3;
	}

	// Token: 0x060013DF RID: 5087 RVA: 0x000AD40C File Offset: 0x000AB60C
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

	// Token: 0x060013E0 RID: 5088 RVA: 0x000AD468 File Offset: 0x000AB668
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

	// Token: 0x04001BBD RID: 7101
	[SerializeField]
	private int week;

	// Token: 0x04001BBE RID: 7102
	[SerializeField]
	private DayOfWeek weekday;

	// Token: 0x04001BBF RID: 7103
	[SerializeField]
	private TimeOfDay timeOfDay;
}
