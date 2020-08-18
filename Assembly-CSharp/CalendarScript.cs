using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020000F6 RID: 246
public class CalendarScript : MonoBehaviour
{
	// Token: 0x06000AA0 RID: 2720 RVA: 0x00057E7C File Offset: 0x0005607C
	private void Start()
	{
		Debug.Log("Upon entering the Calendar screen, DateGlobals.Weekday is: " + DateGlobals.Weekday);
		this.LoveSickCheck();
		if (!SchoolGlobals.SchoolAtmosphereSet)
		{
			OptionGlobals.EnableShadows = false;
			SchoolGlobals.SchoolAtmosphereSet = true;
			SchoolGlobals.SchoolAtmosphere = 1f;
			PlayerGlobals.Money = 10f;
		}
		if (SchoolGlobals.SchoolAtmosphere > 1f)
		{
			SchoolGlobals.SchoolAtmosphere = 1f;
		}
		if (DateGlobals.Weekday > DayOfWeek.Thursday)
		{
			DateGlobals.Weekday = DayOfWeek.Sunday;
			Globals.DeleteAll();
		}
		if (DateGlobals.PassDays < 1)
		{
			DateGlobals.PassDays = 1;
		}
		DateGlobals.DayPassed = true;
		this.Sun.color = new Color(this.Sun.color.r, this.Sun.color.g, this.Sun.color.b, SchoolGlobals.SchoolAtmosphere);
		this.Cloud.color = new Color(this.Cloud.color.r, this.Cloud.color.g, this.Cloud.color.b, 1f - SchoolGlobals.SchoolAtmosphere);
		this.AtmosphereLabel.text = (SchoolGlobals.SchoolAtmosphere * 100f).ToString("f0") + "%";
		float num = 1f - SchoolGlobals.SchoolAtmosphere;
		this.GrayscaleEffect.desaturation = num;
		this.Vignette.intensity = num * 5f;
		this.Vignette.blur = num;
		this.Vignette.chromaticAberration = num;
		this.Continue.transform.localPosition = new Vector3(this.Continue.transform.localPosition.x, -610f, this.Continue.transform.localPosition.z);
		this.Challenge.ViewButton.SetActive(false);
		this.Challenge.LargeIcon.color = new Color(this.Challenge.LargeIcon.color.r, this.Challenge.LargeIcon.color.g, this.Challenge.LargeIcon.color.b, 0f);
		this.Challenge.Panels[1].alpha = 0.5f;
		this.Challenge.Shadow.color = new Color(this.Challenge.Shadow.color.r, this.Challenge.Shadow.color.g, this.Challenge.Shadow.color.b, 0f);
		this.ChallengePanel.alpha = 0f;
		this.CalendarPanel.alpha = 1f;
		this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, 1f);
		Time.timeScale = 1f;
		this.Highlight.localPosition = new Vector3(-600f + 200f * (float)DateGlobals.Weekday, this.Highlight.localPosition.y, this.Highlight.localPosition.z);
		if (DateGlobals.Weekday == DayOfWeek.Saturday)
		{
			this.Highlight.localPosition = new Vector3(-1125f, this.Highlight.localPosition.y, this.Highlight.localPosition.z);
		}
		if (DateGlobals.Week == 2)
		{
			this.DayNumber[1].text = "11";
			this.DayNumber[2].text = "12";
			this.DayNumber[3].text = "13";
			this.DayNumber[4].text = "14";
			this.DayNumber[5].text = "15";
			this.DayNumber[6].text = "16";
			this.DayNumber[7].text = "17";
		}
		this.WeekNumber.text = "Week " + DateGlobals.Week;
		this.LoveSickCheck();
	}

	// Token: 0x06000AA1 RID: 2721 RVA: 0x000582C4 File Offset: 0x000564C4
	private void Update()
	{
		this.Timer += Time.deltaTime;
		if (!this.FadeOut)
		{
			this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, this.Darkness.color.a - Time.deltaTime);
			if (this.Darkness.color.a < 0f)
			{
				this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, 0f);
			}
			if (this.Timer > 1f)
			{
				if (!this.Incremented)
				{
					while (DateGlobals.PassDays > 0)
					{
						DateGlobals.Weekday++;
						DateGlobals.PassDays--;
					}
					this.Target = 200f * (float)DateGlobals.Weekday;
					if (DateGlobals.Weekday > DayOfWeek.Saturday)
					{
						this.Darkness.color = new Color(0f, 0f, 0f, 0f);
						DateGlobals.Weekday = DayOfWeek.Sunday;
						this.Target = 0f;
					}
					Debug.Log("And, as of now, DateGlobals.Weekday is: " + DateGlobals.Weekday);
					this.Incremented = true;
					base.GetComponent<AudioSource>().Play();
				}
				else
				{
					this.Highlight.localPosition = new Vector3(Mathf.Lerp(this.Highlight.localPosition.x, -600f + this.Target, Time.deltaTime * 10f), this.Highlight.localPosition.y, this.Highlight.localPosition.z);
				}
				if (this.Timer > 2f)
				{
					this.Continue.localPosition = new Vector3(this.Continue.localPosition.x, Mathf.Lerp(this.Continue.localPosition.y, -500f, Time.deltaTime * 10f), this.Continue.localPosition.z);
					if (!this.Switch)
					{
						if (!this.ConfirmationWindow.activeInHierarchy)
						{
							if (Input.GetButtonDown("A"))
							{
								this.FadeOut = true;
							}
							if (Input.GetButtonDown("Y"))
							{
								this.Switch = true;
							}
							if (Input.GetButtonDown("B"))
							{
								this.ConfirmationWindow.SetActive(true);
							}
							if (Input.GetKeyDown(KeyCode.Z))
							{
								if (SchoolGlobals.SchoolAtmosphere > 0f)
								{
									SchoolGlobals.SchoolAtmosphere -= 0.1f;
								}
								else
								{
									SchoolGlobals.SchoolAtmosphere = 100f;
								}
								SceneManager.LoadScene(SceneManager.GetActiveScene().name);
							}
						}
						else
						{
							if (Input.GetButtonDown("A"))
							{
								this.FadeOut = true;
								this.Reset = true;
							}
							if (Input.GetButtonDown("B"))
							{
								this.ConfirmationWindow.SetActive(false);
							}
						}
					}
				}
			}
		}
		else
		{
			this.Continue.localPosition = new Vector3(this.Continue.localPosition.x, Mathf.Lerp(this.Continue.localPosition.y, -610f, Time.deltaTime * 10f), this.Continue.localPosition.z);
			this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, this.Darkness.color.a + Time.deltaTime);
			if (this.Darkness.color.a >= 1f)
			{
				if (this.Reset)
				{
					int profile = GameGlobals.Profile;
					Globals.DeleteAll();
					PlayerPrefs.SetInt("ProfileCreated_" + profile, 1);
					GameGlobals.Profile = profile;
					GameGlobals.LoveSick = this.LoveSick;
					SceneManager.LoadScene(SceneManager.GetActiveScene().name);
				}
				else
				{
					if (HomeGlobals.Night)
					{
						HomeGlobals.Night = false;
					}
					if (DateGlobals.Weekday == DayOfWeek.Saturday)
					{
						SceneManager.LoadScene("BusStopScene");
					}
					else
					{
						if (DateGlobals.Weekday == DayOfWeek.Sunday)
						{
							HomeGlobals.Night = true;
						}
						SceneManager.LoadScene("HomeScene");
					}
				}
			}
		}
		if (this.Switch)
		{
			if (this.Phase == 1)
			{
				this.CalendarPanel.alpha -= Time.deltaTime;
				if (this.CalendarPanel.alpha <= 0f)
				{
					this.Phase++;
				}
			}
			else
			{
				this.ChallengePanel.alpha += Time.deltaTime;
				if (this.ChallengePanel.alpha >= 1f)
				{
					this.Challenge.enabled = true;
					base.enabled = false;
					this.Switch = false;
					this.Phase = 1;
				}
			}
		}
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			DateGlobals.Weekday = DayOfWeek.Monday;
			this.Target = 200f * (float)DateGlobals.Weekday;
		}
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			DateGlobals.Weekday = DayOfWeek.Tuesday;
			this.Target = 200f * (float)DateGlobals.Weekday;
		}
		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			DateGlobals.Weekday = DayOfWeek.Wednesday;
			this.Target = 200f * (float)DateGlobals.Weekday;
		}
		if (Input.GetKeyDown(KeyCode.Alpha4))
		{
			DateGlobals.Weekday = DayOfWeek.Thursday;
			this.Target = 200f * (float)DateGlobals.Weekday;
		}
		if (Input.GetKeyDown(KeyCode.Alpha5))
		{
			DateGlobals.Weekday = DayOfWeek.Friday;
			this.Target = 200f * (float)DateGlobals.Weekday;
		}
	}

	// Token: 0x06000AA2 RID: 2722 RVA: 0x00058880 File Offset: 0x00056A80
	public void LoveSickCheck()
	{
		if (GameGlobals.LoveSick)
		{
			SchoolGlobals.SchoolAtmosphereSet = true;
			SchoolGlobals.SchoolAtmosphere = 0f;
			this.LoveSick = true;
			Camera.main.backgroundColor = new Color(0f, 0f, 0f, 1f);
			foreach (GameObject gameObject in UnityEngine.Object.FindObjectsOfType<GameObject>())
			{
				UISprite component = gameObject.GetComponent<UISprite>();
				if (component != null)
				{
					component.color = new Color(1f, 0f, 0f, component.color.a);
				}
				UITexture component2 = gameObject.GetComponent<UITexture>();
				if (component2 != null)
				{
					component2.color = new Color(1f, 0f, 0f, component2.color.a);
				}
				UILabel component3 = gameObject.GetComponent<UILabel>();
				if (component3 != null)
				{
					if (component3.color != Color.black)
					{
						component3.color = new Color(1f, 0f, 0f, component3.color.a);
					}
					if (component3.text == "?")
					{
						component3.color = new Color(1f, 0f, 0f, component3.color.a);
					}
				}
			}
			this.Darkness.color = Color.black;
			this.AtmosphereLabel.enabled = false;
			this.Cloud.enabled = false;
			this.Sun.enabled = false;
		}
	}

	// Token: 0x04000B3C RID: 2876
	public SelectiveGrayscale GrayscaleEffect;

	// Token: 0x04000B3D RID: 2877
	public ChallengeScript Challenge;

	// Token: 0x04000B3E RID: 2878
	public Vignetting Vignette;

	// Token: 0x04000B3F RID: 2879
	public GameObject ConfirmationWindow;

	// Token: 0x04000B40 RID: 2880
	public UILabel AtmosphereLabel;

	// Token: 0x04000B41 RID: 2881
	public UIPanel ChallengePanel;

	// Token: 0x04000B42 RID: 2882
	public UIPanel CalendarPanel;

	// Token: 0x04000B43 RID: 2883
	public UISprite Darkness;

	// Token: 0x04000B44 RID: 2884
	public UITexture Cloud;

	// Token: 0x04000B45 RID: 2885
	public UITexture Sun;

	// Token: 0x04000B46 RID: 2886
	public Transform Highlight;

	// Token: 0x04000B47 RID: 2887
	public Transform Continue;

	// Token: 0x04000B48 RID: 2888
	public UILabel[] DayNumber;

	// Token: 0x04000B49 RID: 2889
	public UILabel WeekNumber;

	// Token: 0x04000B4A RID: 2890
	public bool Incremented;

	// Token: 0x04000B4B RID: 2891
	public bool LoveSick;

	// Token: 0x04000B4C RID: 2892
	public bool FadeOut;

	// Token: 0x04000B4D RID: 2893
	public bool Switch;

	// Token: 0x04000B4E RID: 2894
	public bool Reset;

	// Token: 0x04000B4F RID: 2895
	public float Timer;

	// Token: 0x04000B50 RID: 2896
	public float Target;

	// Token: 0x04000B51 RID: 2897
	public int Phase = 1;
}
