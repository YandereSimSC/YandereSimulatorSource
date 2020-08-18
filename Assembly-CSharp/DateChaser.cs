using System;
using UnityEngine;

// Token: 0x02000257 RID: 599
public class DateChaser : MonoBehaviour
{
	// Token: 0x060012EF RID: 4847 RVA: 0x00098C4C File Offset: 0x00096E4C
	private static DateTime fromUnix(long unix)
	{
		return DateChaser.epoch.AddSeconds((double)unix);
	}

	// Token: 0x060012F0 RID: 4848 RVA: 0x00098C68 File Offset: 0x00096E68
	private void Start()
	{
		Application.targetFrameRate = 60;
		Time.timeScale = 1f;
	}

	// Token: 0x060012F1 RID: 4849 RVA: 0x00098C7C File Offset: 0x00096E7C
	private void Update()
	{
		if (this.Animate)
		{
			float num = Time.time - this.startTime;
			this.CurrentDate = (int)Mathf.Lerp((float)this.startDate, (float)this.endDate, this.curve.Evaluate(num / this.generalDuration));
			DateTime dateTime = DateChaser.fromUnix((long)this.CurrentDate);
			string text = (dateTime.Day == 22 || dateTime.Day == 2) ? "nd" : ((dateTime.Day == 3) ? "rd" : ((dateTime.Day == 1) ? "st" : "th"));
			this.CurrentTimeString = string.Format("{0} {1}{2}, {3}", new object[]
			{
				this.monthNames[dateTime.Month - 1],
				dateTime.Day,
				text,
				dateTime.Year
			});
			if (this.lastFrameDay != dateTime.Day)
			{
				this.onDayTick(dateTime.Day);
			}
			this.lastFrameDay = dateTime.Day;
			this.Timer += Time.deltaTime;
			return;
		}
		this.startTime = Time.time;
		this.CurrentDate = this.startDate;
	}

	// Token: 0x060012F2 RID: 4850 RVA: 0x00098DBD File Offset: 0x00096FBD
	private void onDayTick(int day)
	{
		this.Label.text = this.CurrentTimeString;
	}

	// Token: 0x040018C0 RID: 6336
	public int CurrentDate;

	// Token: 0x040018C1 RID: 6337
	public string CurrentTimeString;

	// Token: 0x040018C2 RID: 6338
	[Header("Epoch timestamps")]
	[SerializeField]
	private int startDate = 1581724799;

	// Token: 0x040018C3 RID: 6339
	[SerializeField]
	private int endDate = 1421366399;

	// Token: 0x040018C4 RID: 6340
	[Space(5f)]
	[Header("Settings")]
	[SerializeField]
	private float generalDuration = 10f;

	// Token: 0x040018C5 RID: 6341
	[SerializeField]
	private AnimationCurve curve;

	// Token: 0x040018C6 RID: 6342
	public bool Animate;

	// Token: 0x040018C7 RID: 6343
	private float startTime;

	// Token: 0x040018C8 RID: 6344
	private string[] monthNames = new string[]
	{
		"January",
		"February",
		"March",
		"April",
		"May",
		"June",
		"July",
		"August",
		"September",
		"October",
		"November",
		"December"
	};

	// Token: 0x040018C9 RID: 6345
	private int lastFrameDay;

	// Token: 0x040018CA RID: 6346
	private static readonly DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

	// Token: 0x040018CB RID: 6347
	public UILabel Label;

	// Token: 0x040018CC RID: 6348
	public float Timer;

	// Token: 0x040018CD RID: 6349
	public int Stage;
}
