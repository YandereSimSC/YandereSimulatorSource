using System;

// Token: 0x020003B1 RID: 945
[Serializable]
public class DateSaveData
{
	// Token: 0x060019F2 RID: 6642 RVA: 0x000FD536 File Offset: 0x000FB736
	public static DateSaveData ReadFromGlobals()
	{
		return new DateSaveData
		{
			week = DateGlobals.Week,
			weekday = DateGlobals.Weekday
		};
	}

	// Token: 0x060019F3 RID: 6643 RVA: 0x000FD553 File Offset: 0x000FB753
	public static void WriteToGlobals(DateSaveData data)
	{
		DateGlobals.Week = data.week;
		DateGlobals.Weekday = data.weekday;
	}

	// Token: 0x0400289A RID: 10394
	public int week;

	// Token: 0x0400289B RID: 10395
	public DayOfWeek weekday;
}
