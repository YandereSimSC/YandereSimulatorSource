using System;
using UnityEngine;

// Token: 0x02000258 RID: 600
public class DateReverseScript : MonoBehaviour
{
	// Token: 0x060012F5 RID: 4853 RVA: 0x00098E8B File Offset: 0x0009708B
	private void Start()
	{
		Time.timeScale = 1f;
		this.UpdateDate();
	}

	// Token: 0x060012F6 RID: 4854 RVA: 0x00098EA0 File Offset: 0x000970A0
	private void Update()
	{
		if (Input.GetKeyDown("space"))
		{
			this.Rollback = true;
		}
		if (this.Rollback)
		{
			this.LifeTime += Time.deltaTime;
			this.Timer += Time.deltaTime;
			if (this.Timer > this.TimeLimit)
			{
				if ((this.Year == this.SlowYear && this.Month == this.SlowMonth && this.Day < this.SlowDay) || (this.Year == this.SlowYear && this.Month < this.SlowMonth))
				{
					this.TimeLimit *= 1.09f;
					if (this.Month == this.EndMonth && this.Day == this.EndDay + 1)
					{
						this.MyAudio.clip = this.Finish;
						this.Label.color = new Color(1f, 0f, 0f, 1f);
						base.enabled = false;
					}
				}
				else if (this.TimeLimit > 0.01f)
				{
					this.TimeLimit *= 0.9f;
				}
				else
				{
					this.Day += this.RollDirection * 19;
				}
				this.Timer = 0f;
				this.Day += this.RollDirection;
				this.UpdateDate();
				this.MyAudio.Play();
				if (!(this.MyAudio.clip != this.Finish))
				{
					this.MyAudio.pitch = 1f;
				}
			}
		}
	}

	// Token: 0x060012F7 RID: 4855 RVA: 0x00099048 File Offset: 0x00097248
	private void UpdateDate()
	{
		if (this.Day < 1)
		{
			this.Day = 31;
			this.Month--;
			if (this.Month < 1)
			{
				this.Month = 12;
				this.Year--;
			}
		}
		else if (this.Day > 31)
		{
			this.Day = 1;
			this.Month++;
			if (this.Month > 11)
			{
				this.Month = 1;
				this.Year++;
			}
		}
		if (this.Day == 1 || this.Day == 21 || this.Day == 31)
		{
			this.Prefix = "st";
		}
		else if (this.Day == 2 || this.Day == 22)
		{
			this.Prefix = "nd";
		}
		else if (this.Day == 3 || this.Day == 23)
		{
			this.Prefix = "rd";
		}
		else
		{
			this.Prefix = "th";
		}
		this.Label.text = string.Concat(new object[]
		{
			this.MonthName[this.Month],
			" ",
			this.Day,
			this.Prefix,
			", ",
			this.Year
		});
	}

	// Token: 0x040018CE RID: 6350
	public AudioSource MyAudio;

	// Token: 0x040018CF RID: 6351
	public string[] MonthName;

	// Token: 0x040018D0 RID: 6352
	public string Prefix;

	// Token: 0x040018D1 RID: 6353
	public UILabel Label;

	// Token: 0x040018D2 RID: 6354
	public AudioClip Finish;

	// Token: 0x040018D3 RID: 6355
	public float TimeLimit;

	// Token: 0x040018D4 RID: 6356
	public float LifeTime;

	// Token: 0x040018D5 RID: 6357
	public float Timer;

	// Token: 0x040018D6 RID: 6358
	public int RollDirection;

	// Token: 0x040018D7 RID: 6359
	public int Month;

	// Token: 0x040018D8 RID: 6360
	public int Year;

	// Token: 0x040018D9 RID: 6361
	public int Day;

	// Token: 0x040018DA RID: 6362
	public int SlowMonth;

	// Token: 0x040018DB RID: 6363
	public int SlowYear;

	// Token: 0x040018DC RID: 6364
	public int SlowDay;

	// Token: 0x040018DD RID: 6365
	public int EndMonth;

	// Token: 0x040018DE RID: 6366
	public int EndYear;

	// Token: 0x040018DF RID: 6367
	public int EndDay;

	// Token: 0x040018E0 RID: 6368
	public bool Rollback;
}
