using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020002EB RID: 747
public class HomeDarknessScript : MonoBehaviour
{
	// Token: 0x06001715 RID: 5909 RVA: 0x000C2F70 File Offset: 0x000C1170
	private void Start()
	{
		if (GameGlobals.LoveSick)
		{
			this.Sprite.color = new Color(0f, 0f, 0f, 1f);
		}
		this.Sprite.color = new Color(this.Sprite.color.r, this.Sprite.color.g, this.Sprite.color.b, 1f);
	}

	// Token: 0x06001716 RID: 5910 RVA: 0x000C2FF0 File Offset: 0x000C11F0
	private void Update()
	{
		if (this.FadeOut)
		{
			this.Sprite.color = new Color(this.Sprite.color.r, this.Sprite.color.g, this.Sprite.color.b, this.Sprite.color.a + Time.deltaTime * (this.FadeSlow ? 0.2f : 1f));
			if (this.Sprite.color.a >= 1f)
			{
				if (this.HomeCamera.ID == 2)
				{
					SceneManager.LoadScene("CalendarScene");
					return;
				}
				if (this.HomeCamera.ID == 3)
				{
					if (this.Cyberstalking)
					{
						SceneManager.LoadScene("CalendarScene");
						return;
					}
					SceneManager.LoadScene("YancordScene");
					return;
				}
				else if (this.HomeCamera.ID == 5)
				{
					if (this.HomeVideoGames.ID == 1)
					{
						SceneManager.LoadScene("YanvaniaTitleScene");
						return;
					}
					SceneManager.LoadScene("MiyukiTitleScene");
					return;
				}
				else
				{
					if (this.HomeCamera.ID == 9)
					{
						SceneManager.LoadScene("CalendarScene");
						return;
					}
					if (this.HomeCamera.ID == 10)
					{
						StudentGlobals.SetStudentKidnapped(SchoolGlobals.KidnapVictim, false);
						StudentGlobals.SetStudentSlave(SchoolGlobals.KidnapVictim);
						this.CheckForOsanaThursday();
						return;
					}
					if (this.HomeCamera.ID == 11)
					{
						EventGlobals.KidnapConversation = true;
						SceneManager.LoadScene("PhoneScene");
						return;
					}
					if (this.HomeCamera.ID == 12)
					{
						SceneManager.LoadScene("LifeNoteScene");
						return;
					}
					if (this.HomeExit.ID == 1)
					{
						this.CheckForOsanaThursday();
						return;
					}
					if (this.HomeExit.ID == 2)
					{
						SceneManager.LoadScene("StreetScene");
						return;
					}
					if (this.HomeExit.ID == 3)
					{
						if (this.HomeYandere.transform.position.y > -5f)
						{
							this.HomeYandere.transform.position = new Vector3(-2f, -10f, -2.75f);
							this.HomeYandere.transform.eulerAngles = new Vector3(0f, 90f, 0f);
							this.HomeYandere.CanMove = true;
							this.FadeOut = false;
							this.HomeCamera.Destinations[0].position = new Vector3(2.425f, -8f, 0f);
							this.HomeCamera.Destination = this.HomeCamera.Destinations[0];
							this.HomeCamera.transform.position = this.HomeCamera.Destination.position;
							this.HomeCamera.Target = this.HomeCamera.Targets[0];
							this.HomeCamera.Focus.position = this.HomeCamera.Target.position;
							this.BasementLabel.text = "Upstairs";
							this.HomeCamera.DayLight.SetActive(true);
							this.HomeCamera.DayLight.GetComponent<Light>().intensity = 0.66666f;
							Physics.SyncTransforms();
							return;
						}
						this.HomeYandere.transform.position = new Vector3(-1.6f, 0f, -1.6f);
						this.HomeYandere.transform.eulerAngles = new Vector3(0f, 45f, 0f);
						this.HomeYandere.CanMove = true;
						this.FadeOut = false;
						this.HomeCamera.Destinations[0].position = new Vector3(-2.0615f, 2f, 2.418f);
						this.HomeCamera.Destination = this.HomeCamera.Destinations[0];
						this.HomeCamera.transform.position = this.HomeCamera.Destination.position;
						this.HomeCamera.Target = this.HomeCamera.Targets[0];
						this.HomeCamera.Focus.position = this.HomeCamera.Target.position;
						this.BasementLabel.text = "Basement";
						if (HomeGlobals.Night)
						{
							this.HomeCamera.DayLight.SetActive(false);
						}
						this.HomeCamera.DayLight.GetComponent<Light>().intensity = 2f;
						Physics.SyncTransforms();
						return;
					}
				}
			}
		}
		else
		{
			this.Sprite.color = new Color(this.Sprite.color.r, this.Sprite.color.g, this.Sprite.color.b, this.Sprite.color.a - Time.deltaTime);
			if (this.Sprite.color.a < 0f)
			{
				this.Sprite.color = new Color(this.Sprite.color.r, this.Sprite.color.g, this.Sprite.color.b, 0f);
			}
		}
	}

	// Token: 0x06001717 RID: 5911 RVA: 0x000C3501 File Offset: 0x000C1701
	private void CheckForOsanaThursday()
	{
		Debug.Log("Time to check if we need to display the Osana-walks-to-school cutscene...");
		if (this.InputDevice.Type == InputDeviceType.Gamepad)
		{
			PlayerGlobals.UsingGamepad = true;
		}
		else
		{
			PlayerGlobals.UsingGamepad = false;
		}
		SceneManager.LoadScene("LoadingScene");
	}

	// Token: 0x04001F3A RID: 7994
	public HomeVideoGamesScript HomeVideoGames;

	// Token: 0x04001F3B RID: 7995
	public HomeYandereScript HomeYandere;

	// Token: 0x04001F3C RID: 7996
	public HomeCameraScript HomeCamera;

	// Token: 0x04001F3D RID: 7997
	public HomeExitScript HomeExit;

	// Token: 0x04001F3E RID: 7998
	public InputDeviceScript InputDevice;

	// Token: 0x04001F3F RID: 7999
	public UILabel BasementLabel;

	// Token: 0x04001F40 RID: 8000
	public UISprite Sprite;

	// Token: 0x04001F41 RID: 8001
	public bool Cyberstalking;

	// Token: 0x04001F42 RID: 8002
	public bool FadeSlow;

	// Token: 0x04001F43 RID: 8003
	public bool FadeOut;
}
