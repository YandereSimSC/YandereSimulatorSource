using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020003C3 RID: 963
public class SaveLoadMenuScript : MonoBehaviour
{
	// Token: 0x06001A31 RID: 6705 RVA: 0x000FFA25 File Offset: 0x000FDC25
	public void Start()
	{
		if (GameGlobals.Profile == 0)
		{
			GameGlobals.Profile = 1;
		}
		this.Profile = GameGlobals.Profile;
		this.ConfirmWindow.SetActive(false);
	}

	// Token: 0x06001A32 RID: 6706 RVA: 0x000FFA4C File Offset: 0x000FDC4C
	public void Update()
	{
		if (!this.ConfirmWindow.activeInHierarchy)
		{
			if (this.InputManager.TappedUp)
			{
				this.Row--;
				this.UpdateHighlight();
			}
			else if (this.InputManager.TappedDown)
			{
				this.Row++;
				this.UpdateHighlight();
			}
			if (this.InputManager.TappedLeft)
			{
				this.Column--;
				this.UpdateHighlight();
			}
			else if (this.InputManager.TappedRight)
			{
				this.Column++;
				this.UpdateHighlight();
			}
		}
		if (this.GrabScreenshot)
		{
			if (GameGlobals.Profile == 0)
			{
				GameGlobals.Profile = 1;
				this.Profile = 1;
			}
			this.PauseScreen.ScreenBlur.enabled = true;
			this.UICamera.enabled = true;
			this.StudentManager.Save();
			base.StartCoroutine(this.GetThumbnails());
			PlayerPrefs.SetFloat(string.Concat(new object[]
			{
				"Profile_",
				this.Profile,
				"_Slot_",
				this.Selected,
				"_YanderePosX"
			}), this.PauseScreen.Yandere.transform.position.x);
			PlayerPrefs.SetFloat(string.Concat(new object[]
			{
				"Profile_",
				this.Profile,
				"_Slot_",
				this.Selected,
				"_YanderePosY"
			}), this.PauseScreen.Yandere.transform.position.y);
			PlayerPrefs.SetFloat(string.Concat(new object[]
			{
				"Profile_",
				this.Profile,
				"_Slot_",
				this.Selected,
				"_YanderePosZ"
			}), this.PauseScreen.Yandere.transform.position.z);
			PlayerPrefs.SetFloat(string.Concat(new object[]
			{
				"Profile_",
				this.Profile,
				"_Slot_",
				this.Selected,
				"_YandereRotX"
			}), this.PauseScreen.Yandere.transform.eulerAngles.x);
			PlayerPrefs.SetFloat(string.Concat(new object[]
			{
				"Profile_",
				this.Profile,
				"_Slot_",
				this.Selected,
				"_YandereRotY"
			}), this.PauseScreen.Yandere.transform.eulerAngles.y);
			PlayerPrefs.SetFloat(string.Concat(new object[]
			{
				"Profile_",
				this.Profile,
				"_Slot_",
				this.Selected,
				"_YandereRotZ"
			}), this.PauseScreen.Yandere.transform.eulerAngles.z);
			PlayerPrefs.SetFloat(string.Concat(new object[]
			{
				"Profile_",
				this.Profile,
				"_Slot_",
				this.Selected,
				"_Time"
			}), this.Clock.PresentTime);
			if (DateGlobals.Weekday == DayOfWeek.Monday)
			{
				PlayerPrefs.SetInt(string.Concat(new object[]
				{
					"Profile_",
					this.Profile,
					"_Slot_",
					this.Selected,
					"_Weekday"
				}), 1);
			}
			else if (DateGlobals.Weekday == DayOfWeek.Tuesday)
			{
				PlayerPrefs.SetInt(string.Concat(new object[]
				{
					"Profile_",
					this.Profile,
					"_Slot_",
					this.Selected,
					"_Weekday"
				}), 2);
			}
			else if (DateGlobals.Weekday == DayOfWeek.Wednesday)
			{
				PlayerPrefs.SetInt(string.Concat(new object[]
				{
					"Profile_",
					this.Profile,
					"_Slot_",
					this.Selected,
					"_Weekday"
				}), 3);
			}
			else if (DateGlobals.Weekday == DayOfWeek.Thursday)
			{
				PlayerPrefs.SetInt(string.Concat(new object[]
				{
					"Profile_",
					this.Profile,
					"_Slot_",
					this.Selected,
					"_Weekday"
				}), 4);
			}
			else if (DateGlobals.Weekday == DayOfWeek.Friday)
			{
				PlayerPrefs.SetInt(string.Concat(new object[]
				{
					"Profile_",
					this.Profile,
					"_Slot_",
					this.Selected,
					"_Weekday"
				}), 5);
			}
			this.GrabScreenshot = false;
		}
		if (Input.GetButtonDown("A"))
		{
			if (this.Loading)
			{
				if (this.DataLabels[this.Selected].text != "No Data")
				{
					if (!this.ConfirmWindow.activeInHierarchy)
					{
						this.AreYouSureLabel.text = "Are you sure you'd like to load?";
						this.ConfirmWindow.SetActive(true);
					}
					else if (this.DataLabels[this.Selected].text != "No Data")
					{
						PlayerPrefs.SetInt("LoadingSave", 1);
						PlayerPrefs.SetInt("SaveSlot", this.Selected);
						SceneManager.LoadScene("LoadingScene");
					}
				}
			}
			else if (this.Saving)
			{
				if (!this.ConfirmWindow.activeInHierarchy)
				{
					this.AreYouSureLabel.text = "Are you sure you'd like to save?";
					this.ConfirmWindow.SetActive(true);
				}
				else
				{
					this.ConfirmWindow.SetActive(false);
					PlayerPrefs.SetInt("SaveSlot", this.Selected);
					PlayerPrefs.SetString(string.Concat(new object[]
					{
						"Profile_",
						this.Profile,
						"_Slot_",
						this.Selected,
						"_DateTime"
					}), DateTime.Now.ToString());
					ScreenCapture.CaptureScreenshot(string.Concat(new object[]
					{
						Application.streamingAssetsPath,
						"/SaveData/Profile_",
						this.Profile,
						"/Slot_",
						this.Selected,
						"/Thumbnail.png"
					}));
					this.PauseScreen.ScreenBlur.enabled = false;
					this.UICamera.enabled = false;
					this.GrabScreenshot = true;
				}
			}
		}
		if (Input.GetButtonDown("X") && this.DataLabels[this.Selected].text != "No Data")
		{
			PlayerPrefs.SetInt("SaveSlot", this.Selected);
			this.StudentManager.Load();
			if (PlayerPrefs.GetInt(string.Concat(new object[]
			{
				"Profile_",
				this.Profile,
				"_Slot_",
				this.Selected,
				"_Weekday"
			})) == 1)
			{
				DateGlobals.Weekday = DayOfWeek.Monday;
			}
			else if (PlayerPrefs.GetInt(string.Concat(new object[]
			{
				"Profile_",
				this.Profile,
				"_Slot_",
				this.Selected,
				"_Weekday"
			})) == 2)
			{
				DateGlobals.Weekday = DayOfWeek.Tuesday;
			}
			else if (PlayerPrefs.GetInt(string.Concat(new object[]
			{
				"Profile_",
				this.Profile,
				"_Slot_",
				this.Selected,
				"_Weekday"
			})) == 3)
			{
				DateGlobals.Weekday = DayOfWeek.Wednesday;
			}
			else if (PlayerPrefs.GetInt(string.Concat(new object[]
			{
				"Profile_",
				this.Profile,
				"_Slot_",
				this.Selected,
				"_Weekday"
			})) == 4)
			{
				DateGlobals.Weekday = DayOfWeek.Tuesday;
			}
			else if (PlayerPrefs.GetInt(string.Concat(new object[]
			{
				"Profile_",
				this.Profile,
				"_Slot_",
				this.Selected,
				"_Weekday"
			})) == 5)
			{
				DateGlobals.Weekday = DayOfWeek.Wednesday;
			}
			this.Clock.PresentTime = PlayerPrefs.GetFloat(string.Concat(new object[]
			{
				"Profile_",
				this.Profile,
				"_Slot_",
				this.Selected,
				"_Time"
			}), this.Clock.PresentTime);
			this.Clock.DayLabel.text = this.Clock.GetWeekdayText(DateGlobals.Weekday);
			this.PauseScreen.MainMenu.SetActive(true);
			this.PauseScreen.Sideways = false;
			this.PauseScreen.PressedB = true;
			base.gameObject.SetActive(false);
			this.PauseScreen.ExitPhone();
		}
		if (Input.GetButtonDown("B"))
		{
			if (this.ConfirmWindow.activeInHierarchy)
			{
				this.ConfirmWindow.SetActive(false);
				return;
			}
			this.PauseScreen.MainMenu.SetActive(true);
			this.PauseScreen.Sideways = false;
			this.PauseScreen.PressedB = true;
			base.gameObject.SetActive(false);
			this.PauseScreen.PromptBar.ClearButtons();
			this.PauseScreen.PromptBar.Label[0].text = "Accept";
			this.PauseScreen.PromptBar.Label[1].text = "Exit";
			this.PauseScreen.PromptBar.Label[4].text = "Choose";
			this.PauseScreen.PromptBar.UpdateButtons();
			this.PauseScreen.PromptBar.Show = true;
		}
	}

	// Token: 0x06001A33 RID: 6707 RVA: 0x001004A5 File Offset: 0x000FE6A5
	public IEnumerator GetThumbnails()
	{
		int num;
		for (int ID = 1; ID < 11; ID = num + 1)
		{
			if (PlayerPrefs.GetString(string.Concat(new object[]
			{
				"Profile_",
				this.Profile,
				"_Slot_",
				ID,
				"_DateTime"
			})) != "")
			{
				this.DataLabels[ID].text = PlayerPrefs.GetString(string.Concat(new object[]
				{
					"Profile_",
					this.Profile,
					"_Slot_",
					ID,
					"_DateTime"
				}));
				string url = string.Concat(new object[]
				{
					"file:///",
					Application.streamingAssetsPath,
					"/SaveData/Profile_",
					this.Profile,
					"/Slot_",
					ID,
					"/Thumbnail.png"
				});
				WWW www = new WWW(url);
				yield return www;
				if (www.error == null)
				{
					this.Thumbnails[ID].mainTexture = www.texture;
				}
				else
				{
					Debug.Log("Could not retrieve the thumbnail. Maybe it was deleted from Streaming Assets?");
				}
				www = null;
			}
			else
			{
				this.DataLabels[ID].text = "No Data";
			}
			num = ID;
		}
		yield break;
	}

	// Token: 0x06001A34 RID: 6708 RVA: 0x001004B4 File Offset: 0x000FE6B4
	public void UpdateHighlight()
	{
		if (this.Row < 1)
		{
			this.Row = 2;
		}
		else if (this.Row > 2)
		{
			this.Row = 1;
		}
		if (this.Column < 1)
		{
			this.Column = 5;
		}
		else if (this.Column > 5)
		{
			this.Column = 1;
		}
		this.Highlight.localPosition = new Vector3((float)(-510 + 170 * this.Column), (float)(313 - 226 * this.Row), this.Highlight.localPosition.z);
		this.Selected = this.Column + (this.Row - 1) * 5;
	}

	// Token: 0x04002932 RID: 10546
	public StudentManagerScript StudentManager;

	// Token: 0x04002933 RID: 10547
	public InputManagerScript InputManager;

	// Token: 0x04002934 RID: 10548
	public PauseScreenScript PauseScreen;

	// Token: 0x04002935 RID: 10549
	public GameObject ConfirmWindow;

	// Token: 0x04002936 RID: 10550
	public ClockScript Clock;

	// Token: 0x04002937 RID: 10551
	public UILabel AreYouSureLabel;

	// Token: 0x04002938 RID: 10552
	public UILabel Header;

	// Token: 0x04002939 RID: 10553
	public UITexture[] Thumbnails;

	// Token: 0x0400293A RID: 10554
	public UILabel[] DataLabels;

	// Token: 0x0400293B RID: 10555
	public Transform Highlight;

	// Token: 0x0400293C RID: 10556
	public Camera UICamera;

	// Token: 0x0400293D RID: 10557
	public bool GrabScreenshot;

	// Token: 0x0400293E RID: 10558
	public bool Loading;

	// Token: 0x0400293F RID: 10559
	public bool Saving;

	// Token: 0x04002940 RID: 10560
	public int Profile;

	// Token: 0x04002941 RID: 10561
	public int Row = 1;

	// Token: 0x04002942 RID: 10562
	public int Column = 1;

	// Token: 0x04002943 RID: 10563
	public int Selected = 1;
}
