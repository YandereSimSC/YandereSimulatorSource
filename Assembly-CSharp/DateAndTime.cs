using System;
using UnityEngine;

// Token: 0x02000440 RID: 1088
[Serializable]
public class DateAndTime
{
	// Token: 0x06001CAC RID: 7340 RVA: 0x0015564F File Offset: 0x0015384F
	public DateAndTime(int week, DayOfWeek weekday, Clock clock)
	{
		this.week = week;
		this.weekday = weekday;
		this.clock = clock;
	}

	// Token: 0x1700047B RID: 1147
	// (get) Token: 0x06001CAD RID: 7341 RVA: 0x0015566C File Offset: 0x0015386C
	public int Week
	{
		get
		{
			return this.week;
		}
	}

	// Token: 0x1700047C RID: 1148
	// (get) Token: 0x06001CAE RID: 7342 RVA: 0x00155674 File Offset: 0x00153874
	public DayOfWeek Weekday
	{
		get
		{
			return this.weekday;
		}
	}

	// Token: 0x1700047D RID: 1149
	// (get) Token: 0x06001CAF RID: 7343 RVA: 0x0015567C File Offset: 0x0015387C
	public Clock Clock
	{
		get
		{
			return this.clock;
		}
	}

	// Token: 0x1700047E RID: 1150
	// (get) Token: 0x06001CB0 RID: 7344 RVA: 0x00155684 File Offset: 0x00153884
	public int TotalSeconds
	{
		get
		{
			int num = this.week * 604800;
			int num2 = (int)(this.weekday * (DayOfWeek)86400);
			int totalSeconds = this.clock.TotalSeconds;
			return num + num2 + totalSeconds;
		}
	}

	// Token: 0x06001CB1 RID: 7345 RVA: 0x001556BA File Offset: 0x001538BA
	public void IncrementWeek()
	{
		this.week++;
	}

	// Token: 0x06001CB2 RID: 7346 RVA: 0x001556CC File Offset: 0x001538CC
	public void IncrementWeekday()
	{
		int num = (int)this.weekday;
		num++;
		if (num == 7)
		{
			this.IncrementWeek();
			num = 0;
		}
		this.weekday = (DayOfWeek)num;
	}

	// Token: 0x06001CB3 RID: 7347 RVA: 0x001556F8 File Offset: 0x001538F8
	public void Tick(float dt)
	{
		int hours = this.clock.Hours24;
		this.clock.Tick(dt);
		if (this.clock.Hours24 < hours)
		{
			this.IncrementWeekday();
		}
	}

	// Token: 0x040035ED RID: 13805
	[SerializeField]
	private int week;

	// Token: 0x040035EE RID: 13806
	[SerializeField]
	private DayOfWeek weekday;

	// Token: 0x040035EF RID: 13807
	[SerializeField]
	private Clock clock;
}
