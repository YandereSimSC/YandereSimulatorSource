using System;
using UnityEngine;

// Token: 0x0200023B RID: 571
public class ClockScript : MonoBehaviour
{
	// Token: 0x06001250 RID: 4688 RVA: 0x00082C4C File Offset: 0x00080E4C
	private void Start()
	{
		RenderSettings.ambientLight = new Color(0.75f, 0.75f, 0.75f);
		this.PeriodLabel.text = "BEFORE CLASS";
		this.PresentTime = this.StartHour * 60f;
		if (PlayerPrefs.GetInt("LoadingSave") == 1)
		{
			int profile = GameGlobals.Profile;
			int @int = PlayerPrefs.GetInt("SaveSlot");
			Debug.Log(string.Concat(new object[]
			{
				"Loading time! Profile_",
				profile,
				"_Slot_",
				@int,
				"_Time is ",
				PlayerPrefs.GetFloat(string.Concat(new object[]
				{
					"Profile_",
					profile,
					"_Slot_",
					@int,
					"_Time"
				}))
			}));
			this.PresentTime = PlayerPrefs.GetFloat(string.Concat(new object[]
			{
				"Profile_",
				profile,
				"_Slot_",
				@int,
				"_Time"
			}));
			this.Weekday = PlayerPrefs.GetInt(string.Concat(new object[]
			{
				"Profile_",
				profile,
				"_Slot_",
				@int,
				"_Weekday"
			}));
			if (this.Weekday == 1)
			{
				DateGlobals.Weekday = DayOfWeek.Monday;
			}
			else if (this.Weekday == 2)
			{
				DateGlobals.Weekday = DayOfWeek.Tuesday;
			}
			else if (this.Weekday == 3)
			{
				DateGlobals.Weekday = DayOfWeek.Wednesday;
			}
			else if (this.Weekday == 4)
			{
				DateGlobals.Weekday = DayOfWeek.Thursday;
			}
			else if (this.Weekday == 5)
			{
				DateGlobals.Weekday = DayOfWeek.Friday;
			}
		}
		if (DateGlobals.Weekday == DayOfWeek.Sunday)
		{
			DateGlobals.Weekday = DayOfWeek.Monday;
		}
		if (!SchoolGlobals.SchoolAtmosphereSet)
		{
			SchoolGlobals.SchoolAtmosphereSet = true;
			SchoolGlobals.SchoolAtmosphere = 1f;
		}
		if (SchoolGlobals.SchoolAtmosphere < 0.5f)
		{
			this.BloomEffect.bloomIntensity = 0.2f;
			this.BloomEffect.bloomThreshhold = 0f;
			this.Police.Darkness.enabled = true;
			this.Police.Darkness.color = new Color(this.Police.Darkness.color.r, this.Police.Darkness.color.g, this.Police.Darkness.color.b, 1f);
			this.FadeIn = true;
		}
		else
		{
			this.BloomEffect.bloomIntensity = 10f;
			this.BloomEffect.bloomThreshhold = 0f;
			this.UpdateBloom = true;
		}
		this.BloomEffect.bloomThreshhold = 0f;
		this.DayLabel.text = this.GetWeekdayText(DateGlobals.Weekday);
		this.MainLight.color = new Color(1f, 1f, 1f, 1f);
		RenderSettings.ambientLight = new Color(0.75f, 0.75f, 0.75f, 1f);
		RenderSettings.skybox.SetColor("_Tint", new Color(0.5f, 0.5f, 0.5f));
		if (ClubGlobals.GetClubClosed(ClubType.Photography) || StudentGlobals.GetStudentGrudge(56) || StudentGlobals.GetStudentGrudge(57) || StudentGlobals.GetStudentGrudge(58) || StudentGlobals.GetStudentGrudge(59) || StudentGlobals.GetStudentGrudge(60))
		{
			this.IgnorePhotographyClub = true;
		}
		this.MissionMode = MissionModeGlobals.MissionMode;
		this.HourTime = this.PresentTime / 60f;
		this.Hour = Mathf.Floor(this.PresentTime / 60f);
		this.Minute = Mathf.Floor((this.PresentTime / 60f - this.Hour) * 60f);
		this.UpdateClock();
	}

	// Token: 0x06001251 RID: 4689 RVA: 0x00083018 File Offset: 0x00081218
	private void Update()
	{
		if (this.FadeIn && Time.deltaTime < 1f)
		{
			this.Police.Darkness.color = new Color(this.Police.Darkness.color.r, this.Police.Darkness.color.g, this.Police.Darkness.color.b, Mathf.MoveTowards(this.Police.Darkness.color.a, 0f, Time.deltaTime));
			if (this.Police.Darkness.color.a == 0f)
			{
				this.Police.Darkness.enabled = false;
				this.FadeIn = false;
			}
		}
		if (!this.MissionMode && this.CameraTimer < 1f)
		{
			this.CameraTimer += Time.deltaTime;
			if (this.CameraTimer > 1f && !this.StudentManager.MemorialScene.enabled)
			{
				this.Yandere.RPGCamera.enabled = true;
				this.Yandere.CanMove = true;
			}
		}
		if (this.PresentTime < 1080f)
		{
			if (this.UpdateBloom)
			{
				this.BloomEffect.bloomIntensity = Mathf.MoveTowards(this.BloomEffect.bloomIntensity, 0.2f, Time.deltaTime * 5f);
				if (this.BloomEffect.bloomIntensity == 0.2f)
				{
					this.UpdateBloom = false;
				}
			}
		}
		else if (this.LoveManager.WaitingToConfess)
		{
			if (!this.StopTime)
			{
				this.LoveManager.BeginConfession();
			}
		}
		else if (!this.Police.FadeOut && !this.Yandere.Attacking && !this.Yandere.Struggling && !this.Yandere.DelinquentFighting && !this.Yandere.Pickpocketing && !this.Yandere.Noticed)
		{
			this.Police.DayOver = true;
			this.Yandere.StudentManager.StopMoving();
			this.Police.Darkness.enabled = true;
			this.Police.FadeOut = true;
			this.StopTime = true;
		}
		if (!this.StopTime)
		{
			if (this.Period == 3)
			{
				this.PresentTime += Time.deltaTime * 0.016666668f * this.TimeSpeed * 0.5f;
			}
			else
			{
				this.PresentTime += Time.deltaTime * 0.016666668f * this.TimeSpeed;
			}
		}
		this.HourTime = this.PresentTime / 60f;
		this.Hour = Mathf.Floor(this.PresentTime / 60f);
		this.Minute = Mathf.Floor((this.PresentTime / 60f - this.Hour) * 60f);
		if (this.Minute != this.LastMinute)
		{
			this.UpdateClock();
		}
		this.MinuteHand.localEulerAngles = new Vector3(this.MinuteHand.localEulerAngles.x, this.MinuteHand.localEulerAngles.y, this.Minute * 6f);
		this.HourHand.localEulerAngles = new Vector3(this.HourHand.localEulerAngles.x, this.HourHand.localEulerAngles.y, this.Hour * 30f);
		if (this.LateStudent && this.HourTime > 7.9f)
		{
			this.ActivateLateStudent();
		}
		if (this.HourTime < 8.5f)
		{
			if (this.Period < 1)
			{
				this.PeriodLabel.text = "BEFORE CLASS";
				this.DeactivateTrespassZones();
				this.Period++;
			}
		}
		else if (this.HourTime < 13f)
		{
			if (this.Period < 2)
			{
				this.PeriodLabel.text = "CLASS TIME";
				this.ActivateTrespassZones();
				this.Period++;
			}
		}
		else if (this.HourTime < 13.5f)
		{
			if (this.Period < 3)
			{
				this.PeriodLabel.text = "LUNCH TIME";
				this.StudentManager.DramaPhase = 0;
				this.StudentManager.UpdateDrama();
				this.DeactivateTrespassZones();
				this.Period++;
			}
		}
		else if (this.HourTime < 15.5f)
		{
			if (this.Period < 4)
			{
				this.PeriodLabel.text = "CLASS TIME";
				this.ActivateTrespassZones();
				this.Period++;
			}
		}
		else if (this.HourTime < 16f)
		{
			if (this.Period < 5)
			{
				foreach (GameObject gameObject in this.StudentManager.Graffiti)
				{
					if (gameObject != null)
					{
						gameObject.SetActive(false);
					}
				}
				this.PeriodLabel.text = "CLEANING TIME";
				this.DeactivateTrespassZones();
				if (this.Weekday == 5)
				{
					this.MeetingRoomTrespassZone.enabled = true;
				}
				this.Period++;
			}
		}
		else if (this.Period < 6)
		{
			this.PeriodLabel.text = "AFTER SCHOOL";
			this.StudentManager.DramaPhase = 0;
			this.StudentManager.UpdateDrama();
			this.Period++;
		}
		if (!this.IgnorePhotographyClub && this.HourTime > 16.75f && this.StudentManager.SleuthPhase < 4)
		{
			this.StudentManager.SleuthPhase = 3;
			this.StudentManager.UpdateSleuths();
		}
		this.Sun.eulerAngles = new Vector3(this.Sun.eulerAngles.x, this.Sun.eulerAngles.y, -45f + 90f * (this.PresentTime - 420f) / 660f);
		if (!this.Horror)
		{
			if (this.StudentManager.WestBathroomArea.bounds.Contains(this.Yandere.transform.position) || this.StudentManager.EastBathroomArea.bounds.Contains(this.Yandere.transform.position))
			{
				this.AmbientLightDim = Mathf.MoveTowards(this.AmbientLightDim, 0.1f, Time.deltaTime);
			}
			else
			{
				this.AmbientLightDim = Mathf.MoveTowards(this.AmbientLightDim, 0.75f, Time.deltaTime);
			}
			if (this.PresentTime > 930f)
			{
				this.DayProgress = (this.PresentTime - 930f) / 150f;
				this.MainLight.color = new Color(1f - 0.1490196f * this.DayProgress, 1f - 0.40392154f * this.DayProgress, 1f - 0.70980394f * this.DayProgress);
				RenderSettings.ambientLight = new Color(1f - 0.1490196f * this.DayProgress - (1f - this.AmbientLightDim) * (1f - this.DayProgress), 1f - 0.40392154f * this.DayProgress - (1f - this.AmbientLightDim) * (1f - this.DayProgress), 1f - 0.70980394f * this.DayProgress - (1f - this.AmbientLightDim) * (1f - this.DayProgress));
				this.SkyboxColor = new Color(1f - 0.1490196f * this.DayProgress - 0.5f * (1f - this.DayProgress), 1f - 0.40392154f * this.DayProgress - 0.5f * (1f - this.DayProgress), 1f - 0.70980394f * this.DayProgress - 0.5f * (1f - this.DayProgress));
				RenderSettings.skybox.SetColor("_Tint", new Color(this.SkyboxColor.r, this.SkyboxColor.g, this.SkyboxColor.b));
			}
			else
			{
				RenderSettings.ambientLight = new Color(this.AmbientLightDim, this.AmbientLightDim, this.AmbientLightDim);
			}
		}
		if (this.TimeSkip)
		{
			if (this.HalfwayTime == 0f)
			{
				this.HalfwayTime = this.PresentTime + (this.TargetTime - this.PresentTime) * 0.5f;
				this.Yandere.TimeSkipHeight = this.Yandere.transform.position.y;
				this.Yandere.Phone.SetActive(true);
				this.Yandere.TimeSkipping = true;
				this.Yandere.CanMove = false;
				this.Blur.enabled = true;
				if (this.Yandere.Armed)
				{
					this.Yandere.Unequip();
				}
			}
			if (Time.timeScale < 25f)
			{
				Time.timeScale += 1f;
			}
			this.Yandere.Character.GetComponent<Animation>()["f02_timeSkip_00"].speed = 1f / Time.timeScale;
			this.Blur.blurAmount = 0.92f * (Time.timeScale / 100f);
			if (this.PresentTime > this.TargetTime)
			{
				this.EndTimeSkip();
			}
			if (this.Yandere.CameraEffects.Streaks.color.a > 0f || this.Yandere.CameraEffects.MurderStreaks.color.a > 0f || this.Yandere.NearSenpai || Input.GetButtonDown("Start"))
			{
				this.EndTimeSkip();
			}
		}
	}

	// Token: 0x06001252 RID: 4690 RVA: 0x000839FC File Offset: 0x00081BFC
	public void EndTimeSkip()
	{
		if (GameGlobals.AlphabetMode)
		{
			this.StopTime = true;
		}
		this.PromptParent.localScale = new Vector3(1f, 1f, 1f);
		this.Yandere.Phone.SetActive(false);
		this.Yandere.TimeSkipping = false;
		this.Blur.enabled = false;
		Time.timeScale = 1f;
		this.TimeSkip = false;
		this.HalfwayTime = 0f;
		if (!this.Yandere.Noticed && !this.Police.FadeOut)
		{
			this.Yandere.CharacterAnimation.CrossFade(this.Yandere.IdleAnim);
			this.Yandere.CanMoveTimer = 0.5f;
		}
	}

	// Token: 0x06001253 RID: 4691 RVA: 0x00083AC0 File Offset: 0x00081CC0
	public string GetWeekdayText(DayOfWeek weekday)
	{
		if (weekday == DayOfWeek.Sunday)
		{
			this.Weekday = 0;
			return "SUNDAY";
		}
		if (weekday == DayOfWeek.Monday)
		{
			this.Weekday = 1;
			return "MONDAY";
		}
		if (weekday == DayOfWeek.Tuesday)
		{
			this.Weekday = 2;
			return "TUESDAY";
		}
		if (weekday == DayOfWeek.Wednesday)
		{
			this.Weekday = 3;
			return "WEDNESDAY";
		}
		if (weekday == DayOfWeek.Thursday)
		{
			this.Weekday = 4;
			return "THURSDAY";
		}
		if (weekday == DayOfWeek.Friday)
		{
			this.Weekday = 5;
			return "FRIDAY";
		}
		this.Weekday = 6;
		return "SATURDAY";
	}

	// Token: 0x06001254 RID: 4692 RVA: 0x00083B40 File Offset: 0x00081D40
	private void ActivateTrespassZones()
	{
		if (!this.SchoolBell.isPlaying || this.SchoolBell.time > 1f)
		{
			this.SchoolBell.Play();
		}
		Collider[] trespassZones = this.TrespassZones;
		for (int i = 0; i < trespassZones.Length; i++)
		{
			trespassZones[i].enabled = true;
		}
	}

	// Token: 0x06001255 RID: 4693 RVA: 0x00083B98 File Offset: 0x00081D98
	public void DeactivateTrespassZones()
	{
		this.Yandere.Trespassing = false;
		if (!this.SchoolBell.isPlaying || this.SchoolBell.time > 1f)
		{
			this.SchoolBell.Play();
		}
		foreach (Collider collider in this.TrespassZones)
		{
			if (!collider.GetComponent<TrespassScript>().OffLimits)
			{
				collider.enabled = false;
			}
		}
	}

	// Token: 0x06001256 RID: 4694 RVA: 0x00083C08 File Offset: 0x00081E08
	public void ActivateLateStudent()
	{
		if (this.StudentManager.Students[7] != null)
		{
			this.StudentManager.Students[7].gameObject.SetActive(true);
			this.StudentManager.Students[7].Pathfinding.speed = 4f;
			this.StudentManager.Students[7].Spawned = true;
			this.StudentManager.Students[7].Hurry = true;
		}
		this.LateStudent = false;
	}

	// Token: 0x06001257 RID: 4695 RVA: 0x00083C8C File Offset: 0x00081E8C
	public void NightLighting()
	{
		this.MainLight.color = new Color(0.25f, 0.25f, 0.5f);
		RenderSettings.ambientLight = new Color(0.25f, 0.25f, 0.5f);
		this.SkyboxColor = new Color(0.1f, 0.1f, 0.2f);
		RenderSettings.skybox.SetColor("_Tint", new Color(0.1f, 0.1f, 0.2f));
	}

	// Token: 0x06001258 RID: 4696 RVA: 0x00083D10 File Offset: 0x00081F10
	public void UpdateClock()
	{
		this.LastMinute = this.Minute;
		if (this.Hour == 0f || this.Hour == 12f)
		{
			this.HourNumber = "12";
		}
		else if (this.Hour < 12f)
		{
			this.HourNumber = this.Hour.ToString("f0");
		}
		else
		{
			this.HourNumber = (this.Hour - 12f).ToString("f0");
		}
		if (this.Minute < 10f)
		{
			this.MinuteNumber = "0" + this.Minute.ToString("f0");
		}
		else
		{
			this.MinuteNumber = this.Minute.ToString("f0");
		}
		this.TimeText = this.HourNumber + ":" + this.MinuteNumber + ((this.Hour < 12f) ? " AM" : " PM");
		this.TimeLabel.text = this.TimeText;
	}

	// Token: 0x040015B8 RID: 5560
	private string MinuteNumber = string.Empty;

	// Token: 0x040015B9 RID: 5561
	private string HourNumber = string.Empty;

	// Token: 0x040015BA RID: 5562
	public Collider MeetingRoomTrespassZone;

	// Token: 0x040015BB RID: 5563
	public Collider[] TrespassZones;

	// Token: 0x040015BC RID: 5564
	public StudentManagerScript StudentManager;

	// Token: 0x040015BD RID: 5565
	public LoveManagerScript LoveManager;

	// Token: 0x040015BE RID: 5566
	public YandereScript Yandere;

	// Token: 0x040015BF RID: 5567
	public PoliceScript Police;

	// Token: 0x040015C0 RID: 5568
	public ClockScript Clock;

	// Token: 0x040015C1 RID: 5569
	public Bloom BloomEffect;

	// Token: 0x040015C2 RID: 5570
	public MotionBlur Blur;

	// Token: 0x040015C3 RID: 5571
	public Transform PromptParent;

	// Token: 0x040015C4 RID: 5572
	public Transform MinuteHand;

	// Token: 0x040015C5 RID: 5573
	public Transform HourHand;

	// Token: 0x040015C6 RID: 5574
	public Transform Sun;

	// Token: 0x040015C7 RID: 5575
	public GameObject SunFlare;

	// Token: 0x040015C8 RID: 5576
	public UILabel PeriodLabel;

	// Token: 0x040015C9 RID: 5577
	public UILabel TimeLabel;

	// Token: 0x040015CA RID: 5578
	public UILabel DayLabel;

	// Token: 0x040015CB RID: 5579
	public Light MainLight;

	// Token: 0x040015CC RID: 5580
	public float HalfwayTime;

	// Token: 0x040015CD RID: 5581
	public float PresentTime;

	// Token: 0x040015CE RID: 5582
	public float TargetTime;

	// Token: 0x040015CF RID: 5583
	public float StartTime;

	// Token: 0x040015D0 RID: 5584
	public float HourTime;

	// Token: 0x040015D1 RID: 5585
	public float AmbientLightDim;

	// Token: 0x040015D2 RID: 5586
	public float CameraTimer;

	// Token: 0x040015D3 RID: 5587
	public float DayProgress;

	// Token: 0x040015D4 RID: 5588
	public float LastMinute;

	// Token: 0x040015D5 RID: 5589
	public float StartHour;

	// Token: 0x040015D6 RID: 5590
	public float TimeSpeed;

	// Token: 0x040015D7 RID: 5591
	public float Minute;

	// Token: 0x040015D8 RID: 5592
	public float Timer;

	// Token: 0x040015D9 RID: 5593
	public float Hour;

	// Token: 0x040015DA RID: 5594
	public PhaseOfDay Phase;

	// Token: 0x040015DB RID: 5595
	public int Period;

	// Token: 0x040015DC RID: 5596
	public int Weekday;

	// Token: 0x040015DD RID: 5597
	public int ID;

	// Token: 0x040015DE RID: 5598
	public string TimeText = string.Empty;

	// Token: 0x040015DF RID: 5599
	public bool IgnorePhotographyClub;

	// Token: 0x040015E0 RID: 5600
	public bool LateStudent;

	// Token: 0x040015E1 RID: 5601
	public bool UpdateBloom;

	// Token: 0x040015E2 RID: 5602
	public bool MissionMode;

	// Token: 0x040015E3 RID: 5603
	public bool StopTime;

	// Token: 0x040015E4 RID: 5604
	public bool TimeSkip;

	// Token: 0x040015E5 RID: 5605
	public bool FadeIn;

	// Token: 0x040015E6 RID: 5606
	public bool Horror;

	// Token: 0x040015E7 RID: 5607
	public AudioSource SchoolBell;

	// Token: 0x040015E8 RID: 5608
	public Color SkyboxColor;
}
