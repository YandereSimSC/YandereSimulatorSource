using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000355 RID: 853
public class PauseScreenScript : MonoBehaviour
{
	// Token: 0x060018A6 RID: 6310 RVA: 0x000E1980 File Offset: 0x000DFB80
	private void Start()
	{
		if (SceneManager.GetActiveScene().name != "SchoolScene")
		{
			MissionModeGlobals.MultiMission = false;
		}
		if (!MissionModeGlobals.MultiMission)
		{
			this.MissionModeLabel.SetActive(false);
		}
		this.MultiMission = MissionModeGlobals.MultiMission;
		StudentGlobals.SetStudentPhotographed(0, true);
		StudentGlobals.SetStudentPhotographed(1, true);
		base.transform.localPosition = new Vector3(1350f, 0f, 0f);
		base.transform.localScale = new Vector3(0.9133334f, 0.9133334f, 0.9133334f);
		base.transform.localEulerAngles = new Vector3(base.transform.localEulerAngles.x, base.transform.localEulerAngles.y, 0f);
		this.StudentInfoMenu.gameObject.SetActive(false);
		this.PhotoGallery.gameObject.SetActive(false);
		this.SaveLoadMenu.gameObject.SetActive(false);
		this.ServiceMenu.gameObject.SetActive(false);
		this.FavorMenu.gameObject.SetActive(false);
		this.AudioMenu.gameObject.SetActive(false);
		this.PassTime.gameObject.SetActive(false);
		this.Settings.gameObject.SetActive(false);
		this.TaskList.gameObject.SetActive(false);
		this.Stats.gameObject.SetActive(false);
		this.LoadingScreen.SetActive(false);
		this.SchemesMenu.SetActive(false);
		this.StudentInfo.SetActive(false);
		this.DropsMenu.SetActive(false);
		this.MainMenu.SetActive(true);
		if (SceneManager.GetActiveScene().name == "SchoolScene")
		{
			this.Schemes.UpdateInstructions();
		}
		else
		{
			this.MissionModeIcons.SetActive(false);
			UISprite uisprite = this.PhoneIcons[5];
			uisprite.color = new Color(uisprite.color.r, uisprite.color.g, uisprite.color.b, 0.5f);
			UISprite uisprite2 = this.PhoneIcons[7];
			uisprite2.color = new Color(uisprite2.color.r, uisprite2.color.g, uisprite2.color.b, 0.5f);
			UISprite uisprite3 = this.PhoneIcons[8];
			uisprite3.color = new Color(uisprite3.color.r, uisprite3.color.g, uisprite3.color.b, 1f);
			UISprite uisprite4 = this.PhoneIcons[9];
			uisprite4.color = new Color(uisprite4.color.r, uisprite4.color.g, uisprite4.color.b, 0.5f);
			if (this.NewMissionModeWindow != null)
			{
				this.NewMissionModeWindow.SetActive(false);
			}
		}
		if (MissionModeGlobals.MissionMode)
		{
			UISprite uisprite5 = this.PhoneIcons[7];
			uisprite5.color = new Color(uisprite5.color.r, uisprite5.color.g, uisprite5.color.b, 0.5f);
			UISprite uisprite6 = this.PhoneIcons[9];
			uisprite6.color = new Color(uisprite6.color.r, uisprite6.color.g, uisprite6.color.b, 0.5f);
			UISprite uisprite7 = this.PhoneIcons[10];
			uisprite7.color = new Color(uisprite7.color.r, uisprite7.color.g, uisprite7.color.b, 1f);
		}
		this.UpdateSelection();
		this.CorrectingTime = false;
	}

	// Token: 0x060018A7 RID: 6311 RVA: 0x000E1D44 File Offset: 0x000DFF44
	private void Update()
	{
		this.Speed = Time.unscaledDeltaTime * 10f;
		if (!this.Police.FadeOut && !this.Map.Show)
		{
			if (!this.Show)
			{
				if (base.transform.localPosition.x > 1349f)
				{
					if (this.Panel.enabled)
					{
						this.Panel.enabled = false;
						base.transform.localPosition = new Vector3(1350f, 50f, 0f);
						base.transform.localScale = new Vector3(0.9133334f, 0.9133334f, 0.9133334f);
						base.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
					}
				}
				else
				{
					base.transform.localPosition = Vector3.Lerp(base.transform.localPosition, new Vector3(1350f, 50f, 0f), this.Speed);
					base.transform.localScale = Vector3.Lerp(base.transform.localScale, new Vector3(0.9133334f, 0.9133334f, 0.9133334f), this.Speed);
					base.transform.localEulerAngles = new Vector3(base.transform.localEulerAngles.x, base.transform.localEulerAngles.y, Mathf.Lerp(base.transform.localEulerAngles.z, 0f, this.Speed));
				}
				if (this.CorrectingTime && Time.timeScale < 0.9f)
				{
					Time.timeScale = Mathf.Lerp(Time.timeScale, 1f, this.Speed);
					if (Time.timeScale > 0.9f)
					{
						this.CorrectingTime = false;
						Time.timeScale = 1f;
					}
				}
				if (Input.GetButtonDown("Start"))
				{
					if (!this.Home)
					{
						if (!this.Yandere.Shutter.Snapping && !this.Yandere.TimeSkipping && !this.Yandere.Talking && !this.Yandere.Noticed && !this.Yandere.InClass && !this.Yandere.Struggling && !this.Yandere.Won && !this.Yandere.Dismembering && !this.Yandere.Attacked && this.Yandere.CanMove && Time.timeScale > 0.0001f)
						{
							this.Yandere.StopAiming();
							this.PromptParent.localScale = Vector3.zero;
							this.Yandere.Obscurance.enabled = false;
							this.Yandere.YandereVision = false;
							this.ScreenBlur.enabled = true;
							this.Yandere.YandereTimer = 0f;
							this.Yandere.Mopping = false;
							this.Panel.enabled = true;
							this.Sideways = false;
							this.Show = true;
							this.PromptBar.ClearButtons();
							this.PromptBar.Label[0].text = "Accept";
							this.PromptBar.Label[1].text = "Exit";
							this.PromptBar.Label[4].text = "Choose";
							this.PromptBar.Label[5].text = "Choose";
							this.PromptBar.UpdateButtons();
							this.PromptBar.Show = true;
							UISprite uisprite = this.PhoneIcons[3];
							if (!this.Yandere.CanMove || this.Yandere.Dragging || (this.Police.Corpses - this.Police.HiddenCorpses > 0 && !this.Police.SuicideScene && !this.Police.PoisonScene))
							{
								uisprite.color = new Color(uisprite.color.r, uisprite.color.g, uisprite.color.b, 0.5f);
								return;
							}
							uisprite.color = new Color(uisprite.color.r, uisprite.color.g, uisprite.color.b, 1f);
							return;
						}
					}
					else if (this.HomeCamera.Destination == this.HomeCamera.Destinations[0])
					{
						this.PromptBar.ClearButtons();
						this.PromptBar.Label[0].text = "Accept";
						this.PromptBar.Label[1].text = "Exit";
						this.PromptBar.Label[4].text = "Choose";
						this.PromptBar.UpdateButtons();
						this.PromptBar.Show = true;
						this.HomeYandere.CanMove = false;
						UISprite uisprite2 = this.PhoneIcons[3];
						uisprite2.color = new Color(uisprite2.color.r, uisprite2.color.g, uisprite2.color.b, 0.5f);
						this.Panel.enabled = true;
						this.Sideways = false;
						this.Show = true;
						return;
					}
				}
			}
			else
			{
				if (!this.EggsChecked)
				{
					float num = 99999f;
					for (int i = 0; i < this.Eggs.Length; i++)
					{
						if (this.Eggs[i] != null)
						{
							float num2 = Vector3.Distance(this.Yandere.transform.position, this.Eggs[i].position);
							if (num2 < num)
							{
								num = num2;
							}
						}
					}
					if (num < 5f)
					{
						this.Wifi.spriteName = "5Bars";
					}
					else if (num < 10f)
					{
						this.Wifi.spriteName = "4Bars";
					}
					else if (num < 15f)
					{
						this.Wifi.spriteName = "3Bars";
					}
					else if (num < 20f)
					{
						this.Wifi.spriteName = "2Bars";
					}
					else if (num < 25f)
					{
						this.Wifi.spriteName = "1Bars";
					}
					else
					{
						this.Wifi.spriteName = "0Bars";
					}
					this.EggsChecked = true;
				}
				if (!this.Home)
				{
					Time.timeScale = Mathf.Lerp(Time.timeScale, 0f, this.Speed);
					this.RPGCamera.enabled = false;
				}
				if (this.ShowMissionModeDetails)
				{
					base.transform.localScale = Vector3.Lerp(base.transform.localScale, new Vector3(1f, 1f, 1f), this.Speed);
					base.transform.localPosition = Vector3.Lerp(base.transform.localPosition, new Vector3(0f, 1200f, 0f), this.Speed);
				}
				else if (this.Quitting)
				{
					base.transform.localScale = Vector3.Lerp(base.transform.localScale, new Vector3(1f, 1f, 1f), this.Speed);
					base.transform.localPosition = Vector3.Lerp(base.transform.localPosition, new Vector3(0f, -1200f, 0f), this.Speed);
				}
				else if (!this.Sideways)
				{
					if (!this.Settings.gameObject.activeInHierarchy)
					{
						base.transform.localPosition = Vector3.Lerp(base.transform.localPosition, new Vector3(0f, 50f, 0f), this.Speed);
					}
					else
					{
						base.transform.localPosition = Vector3.Lerp(base.transform.localPosition, new Vector3(-774f, 50f, 0f), this.Speed);
					}
					base.transform.localScale = Vector3.Lerp(base.transform.localScale, new Vector3(0.9133334f, 0.9133334f, 0.9133334f), this.Speed);
					base.transform.localEulerAngles = new Vector3(base.transform.localEulerAngles.x, base.transform.localEulerAngles.y, Mathf.Lerp(base.transform.localEulerAngles.z, 0f, this.Speed));
				}
				else
				{
					base.transform.localScale = Vector3.Lerp(base.transform.localScale, new Vector3(1.78f, 1.78f, 1.78f), this.Speed);
					base.transform.localPosition = Vector3.Lerp(base.transform.localPosition, new Vector3(0f, 14f, 0f), this.Speed);
					base.transform.localEulerAngles = new Vector3(base.transform.localEulerAngles.x, base.transform.localEulerAngles.y, Mathf.Lerp(base.transform.localEulerAngles.z, 90f, this.Speed));
				}
				if (this.MainMenu.activeInHierarchy && !this.Quitting)
				{
					if (this.InputManager.TappedUp)
					{
						this.Row--;
						this.UpdateSelection();
					}
					if (this.InputManager.TappedDown)
					{
						this.Row++;
						this.UpdateSelection();
					}
					if (this.InputManager.TappedRight)
					{
						this.Column++;
						this.UpdateSelection();
					}
					if (this.InputManager.TappedLeft)
					{
						this.Column--;
						this.UpdateSelection();
					}
					if (Input.GetKeyDown("space") && this.MultiMission)
					{
						this.ShowMissionModeDetails = !this.ShowMissionModeDetails;
					}
					if (this.ShowMissionModeDetails && Input.GetButtonDown("B"))
					{
						this.ShowMissionModeDetails = false;
					}
					for (int j = 1; j < this.PhoneIcons.Length; j++)
					{
						if (this.PhoneIcons[j] != null)
						{
							Vector3 b = (this.Selected != j) ? new Vector3(1f, 1f, 1f) : new Vector3(1.5f, 1.5f, 1.5f);
							this.PhoneIcons[j].transform.localScale = Vector3.Lerp(this.PhoneIcons[j].transform.localScale, b, this.Speed);
						}
					}
					if (!this.ShowMissionModeDetails)
					{
						if (Input.GetButtonDown("A"))
						{
							this.PressedA = true;
							if (this.PhoneIcons[this.Selected].color.a == 1f)
							{
								if (this.Selected == 1)
								{
									this.MainMenu.SetActive(false);
									this.LoadingScreen.SetActive(true);
									this.PromptBar.ClearButtons();
									this.PromptBar.Label[1].text = "Back";
									this.PromptBar.Label[4].text = "Choose";
									this.PromptBar.Label[5].text = "Choose";
									this.PromptBar.UpdateButtons();
									base.StartCoroutine(this.PhotoGallery.GetPhotos());
								}
								else if (this.Selected == 2)
								{
									this.TaskList.gameObject.SetActive(true);
									this.MainMenu.SetActive(false);
									this.Sideways = true;
									this.PromptBar.ClearButtons();
									this.PromptBar.Label[1].text = "Back";
									this.PromptBar.Label[4].text = "Choose";
									this.PromptBar.UpdateButtons();
									this.TaskList.UpdateTaskList();
									base.StartCoroutine(this.TaskList.UpdateTaskInfo());
								}
								else if (this.Selected == 3)
								{
									if (this.PhoneIcons[3].color.a == 1f && this.Yandere.CanMove && !this.Yandere.Dragging)
									{
										for (int k = 0; k < this.Yandere.ArmedAnims.Length; k++)
										{
											this.Yandere.CharacterAnimation[this.Yandere.ArmedAnims[k]].weight = 0f;
										}
										this.MainMenu.SetActive(false);
										this.PromptBar.ClearButtons();
										this.PromptBar.Label[0].text = "Begin";
										this.PromptBar.Label[1].text = "Back";
										this.PromptBar.Label[4].text = "Adjust";
										this.PromptBar.Label[5].text = "Choose";
										this.PromptBar.UpdateButtons();
										this.PassTime.gameObject.SetActive(true);
										this.PassTime.GetCurrentTime();
									}
								}
								else if (this.Selected == 4)
								{
									this.PromptBar.ClearButtons();
									this.PromptBar.Label[1].text = "Exit";
									this.PromptBar.UpdateButtons();
									this.Stats.gameObject.SetActive(true);
									this.Stats.UpdateStats();
									this.MainMenu.SetActive(false);
									this.Sideways = true;
								}
								else if (this.Selected == 5)
								{
									if (this.PhoneIcons[5].color.a == 1f)
									{
										this.PromptBar.ClearButtons();
										this.PromptBar.Label[0].text = "Accept";
										this.PromptBar.Label[1].text = "Exit";
										this.PromptBar.Label[5].text = "Choose";
										this.PromptBar.UpdateButtons();
										this.FavorMenu.gameObject.SetActive(true);
										this.FavorMenu.gameObject.GetComponent<AudioSource>().Play();
										this.MainMenu.SetActive(false);
										this.Sideways = true;
									}
								}
								else if (this.Selected == 6)
								{
									this.StudentInfoMenu.gameObject.SetActive(true);
									base.StartCoroutine(this.StudentInfoMenu.UpdatePortraits());
									this.MainMenu.SetActive(false);
									this.Sideways = true;
									this.PromptBar.ClearButtons();
									this.PromptBar.Label[0].text = "View Info";
									this.PromptBar.Label[1].text = "Back";
									this.PromptBar.UpdateButtons();
									this.PromptBar.Show = true;
								}
								else if (this.Selected == 7)
								{
									this.SaveLoadMenu.gameObject.SetActive(true);
									this.SaveLoadMenu.Header.text = "Load Data";
									this.SaveLoadMenu.Loading = true;
									this.SaveLoadMenu.Saving = false;
									this.SaveLoadMenu.Column = 1;
									this.SaveLoadMenu.Row = 1;
									this.SaveLoadMenu.UpdateHighlight();
									base.StartCoroutine(this.SaveLoadMenu.GetThumbnails());
									this.MainMenu.SetActive(false);
									this.Sideways = true;
									this.PromptBar.ClearButtons();
									this.PromptBar.Label[0].text = "Choose";
									this.PromptBar.Label[1].text = "Back";
									this.PromptBar.Label[2].text = "Debug";
									this.PromptBar.Label[4].text = "Change";
									this.PromptBar.Label[5].text = "Change";
									this.PromptBar.UpdateButtons();
									this.PromptBar.Show = true;
								}
								else if (this.Selected == 8)
								{
									this.Settings.gameObject.SetActive(true);
									if (this.ScreenBlur != null)
									{
										this.ScreenBlur.enabled = false;
									}
									this.Settings.UpdateText();
									this.MainMenu.SetActive(false);
									this.PromptBar.ClearButtons();
									this.PromptBar.Label[1].text = "Back";
									this.PromptBar.Label[4].text = "Choose";
									this.PromptBar.Label[5].text = "Change";
									this.PromptBar.UpdateButtons();
									this.PromptBar.Show = true;
								}
								else if (this.Selected == 9)
								{
									this.SaveLoadMenu.gameObject.SetActive(true);
									this.SaveLoadMenu.Header.text = "Save Data";
									this.SaveLoadMenu.Loading = false;
									this.SaveLoadMenu.Saving = true;
									this.SaveLoadMenu.Column = 1;
									this.SaveLoadMenu.Row = 1;
									this.SaveLoadMenu.UpdateHighlight();
									base.StartCoroutine(this.SaveLoadMenu.GetThumbnails());
									this.MainMenu.SetActive(false);
									this.Sideways = true;
									this.PromptBar.ClearButtons();
									this.PromptBar.Label[0].text = "Choose";
									this.PromptBar.Label[1].text = "Back";
									this.PromptBar.Label[4].text = "Change";
									this.PromptBar.Label[5].text = "Change";
									this.PromptBar.UpdateButtons();
									this.PromptBar.Show = true;
								}
								else if (this.Selected == 10)
								{
									if (!MissionModeGlobals.MissionMode)
									{
										this.AudioMenu.gameObject.SetActive(true);
										this.AudioMenu.UpdateText();
										this.MainMenu.SetActive(false);
										this.PromptBar.ClearButtons();
										this.PromptBar.Label[0].text = "Play";
										this.PromptBar.Label[1].text = "Back";
										this.PromptBar.Label[4].text = "Choose";
										this.PromptBar.UpdateButtons();
										this.PromptBar.Show = true;
									}
									else
									{
										this.PhoneIcons[this.Selected].transform.localScale = new Vector3(1f, 1f, 1f);
										this.MissionMode.ChangeMusic();
									}
								}
								else if (this.Selected == 11)
								{
									this.PromptBar.ClearButtons();
									this.PromptBar.Show = false;
									this.Quitting = true;
								}
								else if (this.Selected == 12)
								{
								}
							}
						}
						else if (!this.PressedB)
						{
							if (Input.GetButtonDown("Start") || Input.GetButtonDown("B"))
							{
								this.ExitPhone();
							}
						}
						else if (Input.GetButtonUp("B"))
						{
							this.PressedB = false;
						}
					}
				}
				if (!this.PressedA)
				{
					if (this.PassTime.gameObject.activeInHierarchy)
					{
						if (Input.GetButtonDown("A"))
						{
							if (this.Yandere.PickUp != null)
							{
								this.Yandere.PickUp.Drop();
							}
							this.Yandere.Unequip();
							this.ScreenBlur.enabled = false;
							this.RPGCamera.enabled = true;
							this.PassTime.gameObject.SetActive(false);
							this.MainMenu.SetActive(true);
							this.PromptBar.Show = false;
							this.Show = false;
							this.Clock.TargetTime = (float)this.PassTime.TargetTime;
							this.Clock.StopTime = false;
							this.Clock.TimeSkip = true;
							Time.timeScale = 1f;
						}
						else if (Input.GetButtonDown("B"))
						{
							this.MainMenu.SetActive(true);
							this.PromptBar.ClearButtons();
							this.PromptBar.Label[0].text = "Accept";
							this.PromptBar.Label[1].text = "Exit";
							this.PromptBar.Label[4].text = "Choose";
							this.PromptBar.Label[5].text = "Choose";
							this.PromptBar.UpdateButtons();
							this.PassTime.gameObject.SetActive(false);
						}
					}
					if (this.Quitting)
					{
						if (Input.GetButtonDown("A"))
						{
							SceneManager.LoadScene("TitleScene");
						}
						else if (Input.GetButtonDown("B"))
						{
							this.PromptBar.ClearButtons();
							this.PromptBar.Label[0].text = "Accept";
							this.PromptBar.Label[1].text = "Exit";
							this.PromptBar.Label[4].text = "Choose";
							this.PromptBar.Label[5].text = "Choose";
							this.PromptBar.UpdateButtons();
							this.PromptBar.Show = true;
							this.Quitting = false;
							if (this.BypassPhone)
							{
								base.transform.localPosition = new Vector3(1350f, 0f, 0f);
								this.ExitPhone();
							}
						}
					}
				}
				if (Input.GetButtonUp("A"))
				{
					this.PressedA = false;
				}
			}
		}
	}

	// Token: 0x060018A8 RID: 6312 RVA: 0x000E3310 File Offset: 0x000E1510
	public void JumpToQuit()
	{
		if (!this.Police.FadeOut && !this.Clock.TimeSkip && !this.Yandere.Noticed)
		{
			base.transform.localPosition = new Vector3(0f, -1200f, 0f);
			this.Yandere.YandereVision = false;
			if (!this.Yandere.Talking && !this.Yandere.Dismembering)
			{
				this.RPGCamera.enabled = false;
				this.Yandere.StopAiming();
			}
			this.ScreenBlur.enabled = true;
			this.Panel.enabled = true;
			this.BypassPhone = true;
			this.Quitting = true;
			this.Show = true;
		}
	}

	// Token: 0x060018A9 RID: 6313 RVA: 0x000E33D8 File Offset: 0x000E15D8
	public void ExitPhone()
	{
		if (!this.Home)
		{
			this.PromptParent.localScale = new Vector3(1f, 1f, 1f);
			this.ScreenBlur.enabled = false;
			this.CorrectingTime = true;
			if (!this.Yandere.Talking && !this.Yandere.Dismembering)
			{
				this.RPGCamera.enabled = true;
			}
			if (this.Yandere.Laughing)
			{
				this.Yandere.GetComponent<AudioSource>().volume = 1f;
			}
		}
		else
		{
			this.HomeYandere.CanMove = true;
		}
		this.PromptBar.ClearButtons();
		this.PromptBar.Show = false;
		this.BypassPhone = false;
		this.EggsChecked = false;
		this.PressedA = false;
		this.Show = false;
	}

	// Token: 0x060018AA RID: 6314 RVA: 0x000E34A8 File Offset: 0x000E16A8
	private void UpdateSelection()
	{
		if (this.Row < 0)
		{
			this.Row = 3;
		}
		else if (this.Row > 3)
		{
			this.Row = 0;
		}
		if (this.Column < 1)
		{
			this.Column = 3;
		}
		else if (this.Column > 3)
		{
			this.Column = 1;
		}
		this.Selected = this.Row * 3 + this.Column;
		this.SelectionLabel.text = this.SelectionNames[this.Selected];
	}

	// Token: 0x04002452 RID: 9298
	public StudentInfoMenuScript StudentInfoMenu;

	// Token: 0x04002453 RID: 9299
	public PhotoGalleryScript PhotoGallery;

	// Token: 0x04002454 RID: 9300
	public InputManagerScript InputManager;

	// Token: 0x04002455 RID: 9301
	public SaveLoadMenuScript SaveLoadMenu;

	// Token: 0x04002456 RID: 9302
	public HomeYandereScript HomeYandere;

	// Token: 0x04002457 RID: 9303
	public MissionModeScript MissionMode;

	// Token: 0x04002458 RID: 9304
	public HomeCameraScript HomeCamera;

	// Token: 0x04002459 RID: 9305
	public ServicesScript ServiceMenu;

	// Token: 0x0400245A RID: 9306
	public FavorMenuScript FavorMenu;

	// Token: 0x0400245B RID: 9307
	public AudioMenuScript AudioMenu;

	// Token: 0x0400245C RID: 9308
	public PromptBarScript PromptBar;

	// Token: 0x0400245D RID: 9309
	public PassTimeScript PassTime;

	// Token: 0x0400245E RID: 9310
	public SettingsScript Settings;

	// Token: 0x0400245F RID: 9311
	public TaskListScript TaskList;

	// Token: 0x04002460 RID: 9312
	public SchemesScript Schemes;

	// Token: 0x04002461 RID: 9313
	public YandereScript Yandere;

	// Token: 0x04002462 RID: 9314
	public RPG_Camera RPGCamera;

	// Token: 0x04002463 RID: 9315
	public PoliceScript Police;

	// Token: 0x04002464 RID: 9316
	public ClockScript Clock;

	// Token: 0x04002465 RID: 9317
	public StatsScript Stats;

	// Token: 0x04002466 RID: 9318
	public Blur ScreenBlur;

	// Token: 0x04002467 RID: 9319
	public MapScript Map;

	// Token: 0x04002468 RID: 9320
	public UILabel SelectionLabel;

	// Token: 0x04002469 RID: 9321
	public UIPanel Panel;

	// Token: 0x0400246A RID: 9322
	public UISprite Wifi;

	// Token: 0x0400246B RID: 9323
	public GameObject NewMissionModeWindow;

	// Token: 0x0400246C RID: 9324
	public GameObject MissionModeLabel;

	// Token: 0x0400246D RID: 9325
	public GameObject MissionModeIcons;

	// Token: 0x0400246E RID: 9326
	public GameObject LoadingScreen;

	// Token: 0x0400246F RID: 9327
	public GameObject SchemesMenu;

	// Token: 0x04002470 RID: 9328
	public GameObject StudentInfo;

	// Token: 0x04002471 RID: 9329
	public GameObject DropsMenu;

	// Token: 0x04002472 RID: 9330
	public GameObject MainMenu;

	// Token: 0x04002473 RID: 9331
	public Transform PromptParent;

	// Token: 0x04002474 RID: 9332
	public string[] SelectionNames;

	// Token: 0x04002475 RID: 9333
	public UISprite[] PhoneIcons;

	// Token: 0x04002476 RID: 9334
	public Transform[] Eggs;

	// Token: 0x04002477 RID: 9335
	public int Prompts;

	// Token: 0x04002478 RID: 9336
	public int Selected = 1;

	// Token: 0x04002479 RID: 9337
	public float Speed;

	// Token: 0x0400247A RID: 9338
	public bool ShowMissionModeDetails;

	// Token: 0x0400247B RID: 9339
	public bool CorrectingTime;

	// Token: 0x0400247C RID: 9340
	public bool MultiMission;

	// Token: 0x0400247D RID: 9341
	public bool BypassPhone;

	// Token: 0x0400247E RID: 9342
	public bool EggsChecked;

	// Token: 0x0400247F RID: 9343
	public bool PressedA;

	// Token: 0x04002480 RID: 9344
	public bool PressedB;

	// Token: 0x04002481 RID: 9345
	public bool Quitting;

	// Token: 0x04002482 RID: 9346
	public bool Sideways;

	// Token: 0x04002483 RID: 9347
	public bool Home;

	// Token: 0x04002484 RID: 9348
	public bool Show;

	// Token: 0x04002485 RID: 9349
	public int Row = 1;

	// Token: 0x04002486 RID: 9350
	public int Column = 2;
}
