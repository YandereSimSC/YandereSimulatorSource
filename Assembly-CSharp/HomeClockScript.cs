using System;
using UnityEngine;

// Token: 0x020002E6 RID: 742
public class HomeClockScript : MonoBehaviour
{
	// Token: 0x06001705 RID: 5893 RVA: 0x000C28BC File Offset: 0x000C0ABC
	private void Start()
	{
		this.DayLabel.text = this.GetWeekdayText(DateGlobals.Weekday);
		if (HomeGlobals.Night)
		{
			this.HourLabel.text = "8:00 PM";
		}
		else
		{
			this.HourLabel.text = (HomeGlobals.LateForSchool ? "7:30 AM" : "6:30 AM");
		}
		this.UpdateMoneyLabel();
	}

	// Token: 0x06001706 RID: 5894 RVA: 0x000C291C File Offset: 0x000C0B1C
	private void Update()
	{
		if (this.ShakeMoney)
		{
			this.Shake = Mathf.MoveTowards(this.Shake, 0f, Time.deltaTime * 10f);
			this.MoneyLabel.transform.localPosition = new Vector3(1020f + UnityEngine.Random.Range(this.Shake * -1f, this.Shake * 1f), 375f + UnityEngine.Random.Range(this.Shake * -1f, this.Shake * 1f), 0f);
			this.G = Mathf.MoveTowards(this.G, 0.75f, Time.deltaTime);
			this.B = Mathf.MoveTowards(this.B, 1f, Time.deltaTime);
			this.MoneyLabel.color = new Color(1f, this.G, this.B, 1f);
			if (this.Shake == 0f)
			{
				this.ShakeMoney = false;
			}
		}
	}

	// Token: 0x06001707 RID: 5895 RVA: 0x000C2A28 File Offset: 0x000C0C28
	private string GetWeekdayText(DayOfWeek weekday)
	{
		if (weekday == DayOfWeek.Sunday)
		{
			return "SUNDAY";
		}
		if (weekday == DayOfWeek.Monday)
		{
			return "MONDAY";
		}
		if (weekday == DayOfWeek.Tuesday)
		{
			return "TUESDAY";
		}
		if (weekday == DayOfWeek.Wednesday)
		{
			return "WEDNESDAY";
		}
		if (weekday == DayOfWeek.Thursday)
		{
			return "THURSDAY";
		}
		if (weekday == DayOfWeek.Friday)
		{
			return "FRIDAY";
		}
		return "SATURDAY";
	}

	// Token: 0x06001708 RID: 5896 RVA: 0x000C2A78 File Offset: 0x000C0C78
	public void UpdateMoneyLabel()
	{
		this.MoneyLabel.text = "$" + PlayerGlobals.Money.ToString("F2");
	}

	// Token: 0x06001709 RID: 5897 RVA: 0x000C2AAC File Offset: 0x000C0CAC
	public void MoneyFail()
	{
		this.ShakeMoney = true;
		this.Shake = 10f;
		this.G = 0f;
		this.B = 0f;
		this.MyAudio.Play();
	}

	// Token: 0x04001F24 RID: 7972
	public UILabel MoneyLabel;

	// Token: 0x04001F25 RID: 7973
	public UILabel HourLabel;

	// Token: 0x04001F26 RID: 7974
	public UILabel DayLabel;

	// Token: 0x04001F27 RID: 7975
	public AudioSource MyAudio;

	// Token: 0x04001F28 RID: 7976
	public bool ShakeMoney;

	// Token: 0x04001F29 RID: 7977
	public float Shake;

	// Token: 0x04001F2A RID: 7978
	public float G;

	// Token: 0x04001F2B RID: 7979
	public float B;
}
