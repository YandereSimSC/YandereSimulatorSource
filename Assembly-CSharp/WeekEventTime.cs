using System;
using UnityEngine;

// Token: 0x02000296 RID: 662
[Serializable]
public class WeekEventTime : IScheduledEventTime
{
	// Token: 0x060013E6 RID: 5094 RVA: 0x000AD552 File Offset: 0x000AB752
	public WeekEventTime(int week)
	{
		this.week = week;
	}

	// Token: 0x1700037B RID: 891
	// (get) Token: 0x060013E7 RID: 5095 RVA: 0x000AD561 File Offset: 0x000AB761
	public ScheduledEventTimeType ScheduleType
	{
		get
		{
			return ScheduledEventTimeType.Week;
		}
	}

	// Token: 0x060013E8 RID: 5096 RVA: 0x000AD564 File Offset: 0x000AB764
	public bool OccurringNow(DateAndTime currentTime)
	{
		return currentTime.Week == this.week;
	}

	// Token: 0x060013E9 RID: 5097 RVA: 0x000AD574 File Offset: 0x000AB774
	public bool OccursInTheFuture(DateAndTime currentTime)
	{
		return currentTime.Week < this.week;
	}

	// Token: 0x060013EA RID: 5098 RVA: 0x000AD584 File Offset: 0x000AB784
	public bool OccurredInThePast(DateAndTime currentTime)
	{
		return currentTime.Week > this.week;
	}

	// Token: 0x04001BC2 RID: 7106
	[SerializeField]
	private int week;
}
