using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200043F RID: 1087
[Serializable]
public class Clock
{
	// Token: 0x06001C98 RID: 7320 RVA: 0x001553DC File Offset: 0x001535DC
	public Clock(int hours, int minutes, int seconds, float currentSecond)
	{
		this.hours = hours;
		this.minutes = minutes;
		this.seconds = seconds;
		this.currentSecond = currentSecond;
	}

	// Token: 0x06001C99 RID: 7321 RVA: 0x00155401 File Offset: 0x00153601
	public Clock(int hours, int minutes, int seconds) : this(hours, minutes, seconds, 0f)
	{
	}

	// Token: 0x06001C9A RID: 7322 RVA: 0x00155411 File Offset: 0x00153611
	public Clock() : this(0, 0, 0, 0f)
	{
	}

	// Token: 0x17000471 RID: 1137
	// (get) Token: 0x06001C9B RID: 7323 RVA: 0x00155421 File Offset: 0x00153621
	public int Hours24
	{
		get
		{
			return this.hours;
		}
	}

	// Token: 0x17000472 RID: 1138
	// (get) Token: 0x06001C9C RID: 7324 RVA: 0x0015542C File Offset: 0x0015362C
	public int Hours12
	{
		get
		{
			int num = this.hours % 12;
			if (num != 0)
			{
				return num;
			}
			return 12;
		}
	}

	// Token: 0x17000473 RID: 1139
	// (get) Token: 0x06001C9D RID: 7325 RVA: 0x0015544A File Offset: 0x0015364A
	public int Minutes
	{
		get
		{
			return this.minutes;
		}
	}

	// Token: 0x17000474 RID: 1140
	// (get) Token: 0x06001C9E RID: 7326 RVA: 0x00155452 File Offset: 0x00153652
	public int Seconds
	{
		get
		{
			return this.seconds;
		}
	}

	// Token: 0x17000475 RID: 1141
	// (get) Token: 0x06001C9F RID: 7327 RVA: 0x0015545A File Offset: 0x0015365A
	public float CurrentSecond
	{
		get
		{
			return this.currentSecond;
		}
	}

	// Token: 0x17000476 RID: 1142
	// (get) Token: 0x06001CA0 RID: 7328 RVA: 0x00155462 File Offset: 0x00153662
	public int TotalSeconds
	{
		get
		{
			return this.hours * 3600 + this.minutes * 60 + this.seconds;
		}
	}

	// Token: 0x17000477 RID: 1143
	// (get) Token: 0x06001CA1 RID: 7329 RVA: 0x00155481 File Offset: 0x00153681
	public float PreciseTotalSeconds
	{
		get
		{
			return (float)this.TotalSeconds + this.currentSecond;
		}
	}

	// Token: 0x17000478 RID: 1144
	// (get) Token: 0x06001CA2 RID: 7330 RVA: 0x00155491 File Offset: 0x00153691
	public bool IsAM
	{
		get
		{
			return this.hours < 12;
		}
	}

	// Token: 0x17000479 RID: 1145
	// (get) Token: 0x06001CA3 RID: 7331 RVA: 0x001554A0 File Offset: 0x001536A0
	public TimeOfDay TimeOfDay
	{
		get
		{
			if (this.hours < 3)
			{
				return TimeOfDay.Midnight;
			}
			if (this.hours < 6)
			{
				return TimeOfDay.EarlyMorning;
			}
			if (this.hours < 9)
			{
				return TimeOfDay.Morning;
			}
			if (this.hours < 12)
			{
				return TimeOfDay.LateMorning;
			}
			if (this.hours < 15)
			{
				return TimeOfDay.Noon;
			}
			if (this.hours < 18)
			{
				return TimeOfDay.Afternoon;
			}
			if (this.hours < 21)
			{
				return TimeOfDay.Evening;
			}
			return TimeOfDay.Night;
		}
	}

	// Token: 0x1700047A RID: 1146
	// (get) Token: 0x06001CA4 RID: 7332 RVA: 0x00155500 File Offset: 0x00153700
	public string TimeOfDayString
	{
		get
		{
			return Clock.TimeOfDayStrings[this.TimeOfDay];
		}
	}

	// Token: 0x06001CA5 RID: 7333 RVA: 0x00155512 File Offset: 0x00153712
	public bool IsBefore(Clock clock)
	{
		return this.TotalSeconds < clock.TotalSeconds;
	}

	// Token: 0x06001CA6 RID: 7334 RVA: 0x00155522 File Offset: 0x00153722
	public bool IsAfter(Clock clock)
	{
		return this.TotalSeconds > clock.TotalSeconds;
	}

	// Token: 0x06001CA7 RID: 7335 RVA: 0x00155532 File Offset: 0x00153732
	public void IncrementHour()
	{
		this.hours++;
		if (this.hours == 24)
		{
			this.hours = 0;
		}
	}

	// Token: 0x06001CA8 RID: 7336 RVA: 0x00155553 File Offset: 0x00153753
	public void IncrementMinute()
	{
		this.minutes++;
		if (this.minutes == 60)
		{
			this.IncrementHour();
			this.minutes = 0;
		}
	}

	// Token: 0x06001CA9 RID: 7337 RVA: 0x0015557A File Offset: 0x0015377A
	public void IncrementSecond()
	{
		this.seconds++;
		if (this.seconds == 60)
		{
			this.IncrementMinute();
			this.seconds = 0;
		}
	}

	// Token: 0x06001CAA RID: 7338 RVA: 0x001555A1 File Offset: 0x001537A1
	public void Tick(float dt)
	{
		this.currentSecond += dt;
		while (this.currentSecond >= 1f)
		{
			this.IncrementSecond();
			this.currentSecond -= 1f;
		}
	}

	// Token: 0x040035E8 RID: 13800
	[SerializeField]
	private int hours;

	// Token: 0x040035E9 RID: 13801
	[SerializeField]
	private int minutes;

	// Token: 0x040035EA RID: 13802
	[SerializeField]
	private int seconds;

	// Token: 0x040035EB RID: 13803
	[SerializeField]
	private float currentSecond;

	// Token: 0x040035EC RID: 13804
	private static readonly Dictionary<TimeOfDay, string> TimeOfDayStrings = new Dictionary<TimeOfDay, string>
	{
		{
			TimeOfDay.Midnight,
			"Midnight"
		},
		{
			TimeOfDay.EarlyMorning,
			"Early Morning"
		},
		{
			TimeOfDay.Morning,
			"Morning"
		},
		{
			TimeOfDay.LateMorning,
			"Late Morning"
		},
		{
			TimeOfDay.Noon,
			"Noon"
		},
		{
			TimeOfDay.Afternoon,
			"Afternoon"
		},
		{
			TimeOfDay.Evening,
			"Evening"
		},
		{
			TimeOfDay.Night,
			"Night"
		}
	};
}
