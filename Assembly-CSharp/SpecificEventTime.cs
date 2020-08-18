using System;
using UnityEngine;

// Token: 0x02000293 RID: 659
[Serializable]
public class SpecificEventTime : IScheduledEventTime
{
	// Token: 0x060013D7 RID: 5079 RVA: 0x000AD260 File Offset: 0x000AB460
	public SpecificEventTime(int week, DayOfWeek weekday, Clock startClock, Clock endClock)
	{
		this.week = week;
		this.weekday = weekday;
		this.startClock = startClock;
		this.endClock = endClock;
	}

	// Token: 0x17000378 RID: 888
	// (get) Token: 0x060013D8 RID: 5080 RVA: 0x0002D171 File Offset: 0x0002B371
	public ScheduledEventTimeType ScheduleType
	{
		get
		{
			return ScheduledEventTimeType.Specific;
		}
	}

	// Token: 0x060013D9 RID: 5081 RVA: 0x000AD288 File Offset: 0x000AB488
	public bool OccurringNow(DateAndTime currentTime)
	{
		bool flag = currentTime.Week == this.week;
		bool flag2 = currentTime.Weekday == this.weekday;
		Clock clock = currentTime.Clock;
		bool flag3 = clock.TotalSeconds >= this.startClock.TotalSeconds && clock.TotalSeconds < this.endClock.TotalSeconds;
		return flag && flag2 && flag3;
	}

	// Token: 0x060013DA RID: 5082 RVA: 0x000AD2E8 File Offset: 0x000AB4E8
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

	// Token: 0x060013DB RID: 5083 RVA: 0x000AD348 File Offset: 0x000AB548
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

	// Token: 0x04001BB9 RID: 7097
	[SerializeField]
	private int week;

	// Token: 0x04001BBA RID: 7098
	[SerializeField]
	private DayOfWeek weekday;

	// Token: 0x04001BBB RID: 7099
	[SerializeField]
	private Clock startClock;

	// Token: 0x04001BBC RID: 7100
	[SerializeField]
	private Clock endClock;
}
