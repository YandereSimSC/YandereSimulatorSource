using System;
using UnityEngine;

// Token: 0x020002ED RID: 749
public class HomeInternetScript : MonoBehaviour
{
	// Token: 0x0600171C RID: 5916 RVA: 0x000C3884 File Offset: 0x000C1A84
	private void Awake()
	{
		this.StudentPost1.localPosition = new Vector3(this.StudentPost1.localPosition.x, -180f, this.StudentPost1.localPosition.z);
		this.StudentPost2.localPosition = new Vector3(this.StudentPost2.localPosition.x, -365f, this.StudentPost2.localPosition.z);
		this.YandereReply.localPosition = new Vector3(this.YandereReply.localPosition.x, -88f, this.YandereReply.localPosition.z);
		this.YanderePost.localPosition = new Vector3(this.YanderePost.localPosition.x, -2f, this.YanderePost.localPosition.z);
		for (int i = 1; i < 6; i++)
		{
			Transform transform = this.StudentReplies[i];
			transform.localPosition = new Vector3(transform.localPosition.x, -40f, transform.localPosition.z);
		}
		this.LameReply.localPosition = new Vector3(this.LameReply.localPosition.x, -40f, this.LameReply.localPosition.z);
		this.Highlights[1].enabled = false;
		this.Highlights[2].enabled = false;
		this.Highlights[3].enabled = false;
		this.YanderePost.gameObject.SetActive(false);
		this.NavigationMenu.SetActive(true);
		this.ChangeLabel.SetActive(false);
		this.ChangeIcon.SetActive(false);
		this.PostLabel.SetActive(false);
		this.PostIcon.SetActive(false);
		this.OnlineShopping.SetActive(false);
		this.NewPostText.SetActive(false);
		this.BG.SetActive(false);
		if (!EventGlobals.Event2 || StudentGlobals.GetStudentExposed(30))
		{
			this.WriteLabel.SetActive(false);
			this.WriteIcon.SetActive(false);
		}
		this.GetPortrait(2);
		this.StudentPost1Portrait.mainTexture = this.CurrentPortrait;
		this.GetPortrait(39);
		this.StudentPost2Portrait.mainTexture = this.CurrentPortrait;
		this.GetPortrait(25);
		this.LamePostPortrait.mainTexture = this.CurrentPortrait;
		this.ID = 1;
		while (this.ID < 6)
		{
			this.GetPortrait(86 - this.ID);
			this.Portraits[this.ID].mainTexture = this.CurrentPortrait;
			this.ID++;
		}
		if (!DateGlobals.DayPassed)
		{
			this.YancordLabel.color = new Color(1f, 1f, 1f, 0.2f);
			this.YancordLogo.color = new Color(1f, 1f, 1f, 0.2f);
		}
	}

	// Token: 0x0600171D RID: 5917 RVA: 0x000C3B78 File Offset: 0x000C1D78
	private void Update()
	{
		if (!this.HomeYandere.CanMove && !this.PauseScreen.Show)
		{
			if (this.NavigationMenu.activeInHierarchy && !this.HomeCamera.CyberstalkWindow.activeInHierarchy)
			{
				if (Input.GetButtonDown("A"))
				{
					this.NavigationMenu.SetActive(false);
					this.SocialMedia.SetActive(true);
				}
				else if (Input.GetButtonDown("X"))
				{
					if (DateGlobals.DayPassed)
					{
						this.HomeCamera.HomeDarkness.FadeOut = true;
					}
				}
				else if (Input.GetButtonDown("Y"))
				{
					this.PauseScreen.MainMenu.SetActive(false);
					this.PauseScreen.Panel.enabled = true;
					this.PauseScreen.Sideways = true;
					this.PauseScreen.Show = true;
					this.StudentInfoMenu.gameObject.SetActive(true);
					this.StudentInfoMenu.CyberStalking = true;
					base.StartCoroutine(this.StudentInfoMenu.UpdatePortraits());
					this.PromptBar.ClearButtons();
					this.PromptBar.Label[0].text = "View Info";
					this.PromptBar.Label[1].text = "Back";
					this.PromptBar.UpdateButtons();
					this.PromptBar.Show = true;
				}
				else if (Input.GetButtonDown("LB"))
				{
					this.NavigationMenu.SetActive(false);
					this.OnlineShopping.SetActive(true);
					this.MoneyLabel.text = "$" + PlayerGlobals.Money.ToString("F2");
				}
				else if (Input.GetButtonDown("B"))
				{
					this.HomeCamera.Destination = this.HomeCamera.Destinations[0];
					this.HomeCamera.Target = this.HomeCamera.Targets[0];
					this.HomeYandere.CanMove = true;
					this.HomeWindow.Show = false;
					base.enabled = false;
				}
			}
			else if (this.SocialMedia.activeInHierarchy)
			{
				this.Menu.localScale = Vector3.Lerp(this.Menu.localScale, this.ShowMenu ? new Vector3(1f, 1f, 1f) : Vector3.zero, Time.deltaTime * 10f);
				if (this.WritingPost)
				{
					this.NewPost.transform.localPosition = Vector3.Lerp(this.NewPost.transform.localPosition, Vector3.zero, Time.deltaTime * 10f);
					this.NewPost.transform.localScale = Vector3.Lerp(this.NewPost.transform.localScale, new Vector3(2.43f, 2.43f, 2.43f), Time.deltaTime * 10f);
					for (int i = 1; i < this.Highlights.Length; i++)
					{
						UISprite uisprite = this.Highlights[i];
						uisprite.color = new Color(uisprite.color.r, uisprite.color.g, uisprite.color.b, Mathf.MoveTowards(uisprite.color.a, this.FadeOut ? 0f : 1f, Time.deltaTime));
					}
					if (this.Highlights[this.Selected].color.a == 1f)
					{
						this.FadeOut = true;
					}
					else if (this.Highlights[this.Selected].color.a == 0f)
					{
						this.FadeOut = false;
					}
					if (!this.ShowMenu)
					{
						if (this.InputManager.TappedRight)
						{
							this.Selected++;
							this.UpdateHighlight();
						}
						if (this.InputManager.TappedLeft)
						{
							this.Selected--;
							this.UpdateHighlight();
						}
					}
					else
					{
						if (this.InputManager.TappedDown)
						{
							this.MenuSelected++;
							this.UpdateMenuHighlight();
						}
						if (this.InputManager.TappedUp)
						{
							this.MenuSelected--;
							this.UpdateMenuHighlight();
						}
					}
				}
				else
				{
					this.NewPost.transform.localPosition = Vector3.Lerp(this.NewPost.transform.localPosition, new Vector3(175f, -10f, 0f), Time.deltaTime * 10f);
					this.NewPost.transform.localScale = Vector3.Lerp(this.NewPost.transform.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10f);
				}
				if (!this.PostSequence)
				{
					if (Input.GetButtonDown("A") && this.WriteIcon.activeInHierarchy && !this.Posted)
					{
						if (!this.ShowMenu)
						{
							if (!this.WritingPost)
							{
								this.AcceptLabel.text = "Select";
								this.ChangeLabel.SetActive(true);
								this.ChangeIcon.SetActive(true);
								this.NewPostText.SetActive(true);
								this.BG.SetActive(true);
								this.WritingPost = true;
								this.Selected = 1;
								this.UpdateHighlight();
							}
							else if (this.Selected == 1)
							{
								this.PauseScreen.MainMenu.SetActive(false);
								this.PauseScreen.Panel.enabled = true;
								this.PauseScreen.Sideways = true;
								this.PauseScreen.Show = true;
								this.StudentInfoMenu.gameObject.SetActive(true);
								this.StudentInfoMenu.CyberBullying = true;
								base.StartCoroutine(this.StudentInfoMenu.UpdatePortraits());
								this.PromptBar.ClearButtons();
								this.PromptBar.Label[0].text = "View Info";
								this.PromptBar.Label[1].text = "Back";
								this.PromptBar.UpdateButtons();
								this.PromptBar.Show = true;
							}
							else if (this.Selected == 2)
							{
								this.MenuSelected = 1;
								this.UpdateMenuHighlight();
								this.ShowMenu = true;
								for (int j = 1; j < this.MenuLabels.Length; j++)
								{
									this.MenuLabels[j].text = this.Locations[j];
								}
							}
							else if (this.Selected == 3)
							{
								this.MenuSelected = 1;
								this.UpdateMenuHighlight();
								this.ShowMenu = true;
								for (int k = 1; k < this.MenuLabels.Length; k++)
								{
									this.MenuLabels[k].text = this.Actions[k];
								}
							}
						}
						else
						{
							if (this.Selected == 2)
							{
								this.Location = this.MenuSelected;
								this.PostLabels[2].text = this.Locations[this.MenuSelected];
								this.ShowMenu = false;
							}
							else if (this.Selected == 3)
							{
								this.Action = this.MenuSelected;
								this.PostLabels[3].text = this.Actions[this.MenuSelected];
								this.ShowMenu = false;
							}
							this.CheckForCompletion();
						}
					}
					if (Input.GetButtonDown("B"))
					{
						if (!this.ShowMenu)
						{
							if (!this.WritingPost)
							{
								this.NavigationMenu.SetActive(true);
								this.SocialMedia.SetActive(false);
							}
							else
							{
								this.AcceptLabel.text = "Write";
								this.ChangeLabel.SetActive(false);
								this.ChangeIcon.SetActive(false);
								this.PostLabel.SetActive(false);
								this.PostIcon.SetActive(false);
								this.ExitPost();
							}
						}
						else
						{
							this.ShowMenu = false;
						}
					}
					if (Input.GetButtonDown("X") && this.PostIcon.activeInHierarchy)
					{
						this.YanderePostLabel.text = string.Concat(new string[]
						{
							"Today, I saw ",
							this.PostLabels[1].text,
							" in ",
							this.PostLabels[2].text,
							". She was ",
							this.PostLabels[3].text,
							"."
						});
						this.ExitPost();
						this.InternetPrompts.SetActive(false);
						this.ChangeLabel.SetActive(false);
						this.ChangeIcon.SetActive(false);
						this.WriteLabel.SetActive(false);
						this.WriteIcon.SetActive(false);
						this.PostLabel.SetActive(false);
						this.PostIcon.SetActive(false);
						this.PostSequence = true;
						this.Posted = true;
						if (this.Student == 30 && this.Location == 7 && this.Action == 9)
						{
							this.Success = true;
						}
					}
					if (Input.GetKeyDown("space"))
					{
						this.WriteLabel.SetActive(true);
						this.WriteIcon.SetActive(true);
					}
				}
				if (this.PostSequence)
				{
					if (Input.GetButtonDown("A"))
					{
						this.Timer += 2f;
					}
					this.Timer += Time.deltaTime;
					if (this.Timer > 1f && this.Timer < 3f)
					{
						this.YanderePost.gameObject.SetActive(true);
						this.YanderePost.transform.localPosition = new Vector3(this.YanderePost.transform.localPosition.x, Mathf.Lerp(this.YanderePost.transform.localPosition.y, -155f, Time.deltaTime * 10f), this.YanderePost.transform.localPosition.z);
						this.StudentPost1.transform.localPosition = new Vector3(this.StudentPost1.transform.localPosition.x, Mathf.Lerp(this.StudentPost1.transform.localPosition.y, -365f, Time.deltaTime * 10f), this.StudentPost1.transform.localPosition.z);
						this.StudentPost2.transform.localPosition = new Vector3(this.StudentPost2.transform.localPosition.x, Mathf.Lerp(this.StudentPost2.transform.localPosition.y, -550f, Time.deltaTime * 10f), this.StudentPost2.transform.localPosition.z);
					}
					if (!this.Success)
					{
						if (this.Timer > 3f && this.Timer < 5f)
						{
							this.LameReply.localPosition = new Vector3(this.LameReply.localPosition.x, Mathf.Lerp(this.LameReply.transform.localPosition.y, -88f, Time.deltaTime * 10f), this.LameReply.localPosition.z);
							this.YandereReply.localPosition = new Vector3(this.YandereReply.localPosition.x, Mathf.Lerp(this.YandereReply.transform.localPosition.y, -137f, Time.deltaTime * 10f), this.YandereReply.localPosition.z);
							this.StudentPost1.localPosition = new Vector3(this.StudentPost1.localPosition.x, Mathf.Lerp(this.StudentPost1.transform.localPosition.y, -415f, Time.deltaTime * 10f), this.StudentPost1.localPosition.z);
						}
						if (this.Timer > 5f)
						{
							PlayerGlobals.Reputation -= 5f;
							this.InternetPrompts.SetActive(true);
							this.PostSequence = false;
						}
					}
					else
					{
						if (this.Timer > 3f && this.Timer < 5f)
						{
							Transform transform = this.StudentReplies[1];
							transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Lerp(transform.transform.localPosition.y, -88f, Time.deltaTime * 10f), transform.localPosition.z);
							this.YandereReply.localPosition = new Vector3(this.YandereReply.localPosition.x, Mathf.Lerp(this.YandereReply.transform.localPosition.y, -137f, Time.deltaTime * 10f), this.YandereReply.localPosition.z);
							this.StudentPost1.localPosition = new Vector3(this.StudentPost1.localPosition.x, Mathf.Lerp(this.StudentPost1.transform.localPosition.y, -415f, Time.deltaTime * 10f), this.StudentPost1.localPosition.z);
						}
						if (this.Timer > 5f && this.Timer < 7f)
						{
							Transform transform2 = this.StudentReplies[2];
							transform2.localPosition = new Vector3(transform2.localPosition.x, Mathf.Lerp(transform2.transform.localPosition.y, -88f, Time.deltaTime * 10f), transform2.localPosition.z);
							Transform transform3 = this.StudentReplies[1];
							transform3.localPosition = new Vector3(transform3.localPosition.x, Mathf.Lerp(transform3.transform.localPosition.y, -136f, Time.deltaTime * 10f), transform3.localPosition.z);
							this.YandereReply.localPosition = new Vector3(this.YandereReply.localPosition.x, Mathf.Lerp(this.YandereReply.transform.localPosition.y, -185f, Time.deltaTime * 10f), this.YandereReply.localPosition.z);
							this.StudentPost1.localPosition = new Vector3(this.StudentPost1.localPosition.x, Mathf.Lerp(this.StudentPost1.transform.localPosition.y, -465f, Time.deltaTime * 10f), this.StudentPost1.localPosition.z);
						}
						if (this.Timer > 7f && this.Timer < 9f)
						{
							Transform transform4 = this.StudentReplies[3];
							transform4.localPosition = new Vector3(transform4.localPosition.x, Mathf.Lerp(transform4.transform.localPosition.y, -88f, Time.deltaTime * 10f), transform4.localPosition.z);
							Transform transform5 = this.StudentReplies[2];
							transform5.localPosition = new Vector3(transform5.localPosition.x, Mathf.Lerp(transform5.transform.localPosition.y, -136f, Time.deltaTime * 10f), transform5.localPosition.z);
							Transform transform6 = this.StudentReplies[1];
							transform6.localPosition = new Vector3(transform6.localPosition.x, Mathf.Lerp(transform6.transform.localPosition.y, -184f, Time.deltaTime * 10f), transform6.localPosition.z);
							this.YandereReply.localPosition = new Vector3(this.YandereReply.localPosition.x, Mathf.Lerp(this.YandereReply.transform.localPosition.y, -233f, Time.deltaTime * 10f), this.YandereReply.localPosition.z);
							this.StudentPost1.localPosition = new Vector3(this.StudentPost1.localPosition.x, Mathf.Lerp(this.StudentPost1.transform.localPosition.y, -510f, Time.deltaTime * 10f), this.StudentPost1.localPosition.z);
						}
						if (this.Timer > 9f && this.Timer < 11f)
						{
							Transform transform7 = this.StudentReplies[4];
							transform7.localPosition = new Vector3(transform7.localPosition.x, Mathf.Lerp(transform7.transform.localPosition.y, -88f, Time.deltaTime * 10f), transform7.localPosition.z);
							Transform transform8 = this.StudentReplies[3];
							transform8.localPosition = new Vector3(transform8.localPosition.x, Mathf.Lerp(transform8.transform.localPosition.y, -136f, Time.deltaTime * 10f), transform8.localPosition.z);
							Transform transform9 = this.StudentReplies[2];
							transform9.localPosition = new Vector3(transform9.localPosition.x, Mathf.Lerp(transform9.transform.localPosition.y, -184f, Time.deltaTime * 10f), transform9.localPosition.z);
							Transform transform10 = this.StudentReplies[1];
							transform10.localPosition = new Vector3(transform10.localPosition.x, Mathf.Lerp(transform10.transform.localPosition.y, -232f, Time.deltaTime * 10f), transform10.localPosition.z);
							this.YandereReply.localPosition = new Vector3(this.YandereReply.localPosition.x, Mathf.Lerp(this.YandereReply.transform.localPosition.y, -281f, Time.deltaTime * 10f), this.YandereReply.localPosition.z);
							this.StudentPost1.localPosition = new Vector3(this.StudentPost1.localPosition.x, Mathf.Lerp(this.StudentPost1.transform.localPosition.y, -560f, Time.deltaTime * 10f), this.StudentPost1.localPosition.z);
						}
						if (this.Timer > 11f && this.Timer < 13f)
						{
							Transform transform11 = this.StudentReplies[5];
							transform11.localPosition = new Vector3(transform11.localPosition.x, Mathf.Lerp(transform11.transform.localPosition.y, -88f, Time.deltaTime * 10f), transform11.localPosition.z);
							Transform transform12 = this.StudentReplies[4];
							transform12.localPosition = new Vector3(transform12.localPosition.x, Mathf.Lerp(transform12.transform.localPosition.y, -136f, Time.deltaTime * 10f), transform12.localPosition.z);
							Transform transform13 = this.StudentReplies[3];
							transform13.localPosition = new Vector3(transform13.localPosition.x, Mathf.Lerp(transform13.transform.localPosition.y, -184f, Time.deltaTime * 10f), transform13.localPosition.z);
							Transform transform14 = this.StudentReplies[2];
							transform14.localPosition = new Vector3(transform14.localPosition.x, Mathf.Lerp(transform14.transform.localPosition.y, -232f, Time.deltaTime * 10f), transform14.localPosition.z);
							Transform transform15 = this.StudentReplies[1];
							transform15.localPosition = new Vector3(transform15.localPosition.x, Mathf.Lerp(transform15.transform.localPosition.y, -280f, Time.deltaTime * 10f), transform15.localPosition.z);
							this.YandereReply.localPosition = new Vector3(this.YandereReply.localPosition.x, Mathf.Lerp(this.YandereReply.transform.localPosition.y, -329f, Time.deltaTime * 10f), this.YandereReply.localPosition.z);
						}
						if (this.Timer > 13f)
						{
							StudentGlobals.SetStudentExposed(30, true);
							StudentGlobals.SetStudentReputation(30, StudentGlobals.GetStudentReputation(30) - 50);
							this.InternetPrompts.SetActive(true);
							this.PostSequence = false;
						}
					}
				}
			}
			else if (this.OnlineShopping.activeInHierarchy)
			{
				if (Input.GetKeyDown("m"))
				{
					PlayerGlobals.Money = 100f;
				}
				if (Input.GetButtonDown("A"))
				{
					if (this.Height == 0 || this.Height > 1)
					{
						if (PlayerGlobals.Money > 33.33f)
						{
							if (!this.AreYouSure.activeInHierarchy)
							{
								this.AreYouSure.SetActive(true);
							}
							else
							{
								this.AreYouSure.SetActive(false);
								GameGlobals.SpareUniform = true;
								PlayerGlobals.Money -= 33.33f;
								this.MyAudio.Play();
								this.MoneyLabel.text = "$" + PlayerGlobals.Money.ToString("F2");
								this.Clock.UpdateMoneyLabel();
							}
						}
						else
						{
							this.Shake = 10f;
						}
					}
					else if (this.Height == 1)
					{
						if (PlayerGlobals.Money > 8.49f)
						{
							if (!this.AreYouSure.activeInHierarchy)
							{
								this.AreYouSure.SetActive(true);
							}
							else
							{
								this.AreYouSure.SetActive(false);
								GameGlobals.BlondeHair = true;
								PlayerGlobals.Money -= 8.49f;
								this.MyAudio.Play();
								this.MoneyLabel.text = "$" + PlayerGlobals.Money.ToString("F2");
								this.Clock.UpdateMoneyLabel();
							}
						}
						else
						{
							this.Shake = 10f;
						}
					}
				}
				this.Shake = Mathf.MoveTowards(this.Shake, 0f, Time.deltaTime * 10f);
				this.MoneyLabel.transform.localPosition = new Vector3(445f + UnityEngine.Random.Range(this.Shake * -1f, this.Shake * 1f), 410f + UnityEngine.Random.Range(this.Shake * -1f, this.Shake * 1f), 0f);
				if (Input.GetButtonDown("B"))
				{
					if (!this.AreYouSure.activeInHierarchy)
					{
						this.NavigationMenu.SetActive(true);
						this.OnlineShopping.SetActive(false);
					}
					else
					{
						this.AreYouSure.SetActive(false);
					}
				}
				if (this.InputManager.TappedDown)
				{
					this.Height++;
				}
				if (this.InputManager.TappedUp)
				{
					this.Height--;
				}
				if (this.Height < 0)
				{
					this.Height = 0;
				}
				if (this.Height > 4)
				{
					this.Height = 4;
				}
				if (this.Height == 0)
				{
					this.Highlight.localPosition = Vector3.Lerp(this.Highlight.localPosition, new Vector3(this.Highlight.localPosition.x, 130f, this.Highlight.localPosition.z), Time.deltaTime * 10f);
				}
				else if (this.Height > 0)
				{
					this.Highlight.localPosition = Vector3.Lerp(this.Highlight.localPosition, new Vector3(this.Highlight.localPosition.x, -85f, this.Highlight.localPosition.z), Time.deltaTime * 10f);
				}
				if (this.Height < 2)
				{
					this.ItemList.localPosition = Vector3.Lerp(this.ItemList.localPosition, new Vector3(this.ItemList.localPosition.x, 130f, this.ItemList.localPosition.z), Time.deltaTime * 10f);
				}
				else
				{
					this.ItemList.localPosition = Vector3.Lerp(this.ItemList.localPosition, new Vector3(this.ItemList.localPosition.x, (float)(130 + 215 * (this.Height - 1)), this.ItemList.localPosition.z), Time.deltaTime * 10f);
				}
			}
		}
		if (Input.GetKeyDown(KeyCode.Space))
		{
			StudentGlobals.SetStudentExposed(7, false);
		}
	}

	// Token: 0x0600171E RID: 5918 RVA: 0x000C54BC File Offset: 0x000C36BC
	private void ExitPost()
	{
		this.Highlights[1].enabled = false;
		this.Highlights[2].enabled = false;
		this.Highlights[3].enabled = false;
		this.NewPostText.SetActive(false);
		this.BG.SetActive(false);
		this.PostLabels[1].text = string.Empty;
		this.PostLabels[2].text = string.Empty;
		this.PostLabels[3].text = string.Empty;
		this.WritingPost = false;
	}

	// Token: 0x0600171F RID: 5919 RVA: 0x000C5548 File Offset: 0x000C3748
	private void UpdateHighlight()
	{
		if (this.Selected > 3)
		{
			this.Selected = 1;
		}
		if (this.Selected < 1)
		{
			this.Selected = 3;
		}
		this.Highlights[1].enabled = false;
		this.Highlights[2].enabled = false;
		this.Highlights[3].enabled = false;
		this.Highlights[this.Selected].enabled = true;
	}

	// Token: 0x06001720 RID: 5920 RVA: 0x000C55B4 File Offset: 0x000C37B4
	private void UpdateMenuHighlight()
	{
		if (this.MenuSelected > 10)
		{
			this.MenuSelected = 1;
		}
		if (this.MenuSelected < 1)
		{
			this.MenuSelected = 10;
		}
		this.MenuHighlight.transform.localPosition = new Vector3(this.MenuHighlight.transform.localPosition.x, 220f - 40f * (float)this.MenuSelected, this.MenuHighlight.transform.localPosition.z);
	}

	// Token: 0x06001721 RID: 5921 RVA: 0x000C5638 File Offset: 0x000C3838
	private void CheckForCompletion()
	{
		if (this.PostLabels[1].text != string.Empty && this.PostLabels[2].text != string.Empty && this.PostLabels[3].text != string.Empty)
		{
			this.PostLabel.SetActive(true);
			this.PostIcon.SetActive(true);
		}
	}

	// Token: 0x06001722 RID: 5922 RVA: 0x000C56A8 File Offset: 0x000C38A8
	private void GetPortrait(int ID)
	{
		WWW www = new WWW(string.Concat(new string[]
		{
			"file:///",
			Application.streamingAssetsPath,
			"/Portraits/Student_",
			ID.ToString(),
			".png"
		}));
		this.CurrentPortrait = www.texture;
	}

	// Token: 0x04001F4C RID: 8012
	public StudentInfoMenuScript StudentInfoMenu;

	// Token: 0x04001F4D RID: 8013
	public InputManagerScript InputManager;

	// Token: 0x04001F4E RID: 8014
	public PauseScreenScript PauseScreen;

	// Token: 0x04001F4F RID: 8015
	public PromptBarScript PromptBar;

	// Token: 0x04001F50 RID: 8016
	public HomeClockScript Clock;

	// Token: 0x04001F51 RID: 8017
	public HomeYandereScript HomeYandere;

	// Token: 0x04001F52 RID: 8018
	public HomeCameraScript HomeCamera;

	// Token: 0x04001F53 RID: 8019
	public HomeWindowScript HomeWindow;

	// Token: 0x04001F54 RID: 8020
	public UILabel YanderePostLabel;

	// Token: 0x04001F55 RID: 8021
	public UILabel YancordLabel;

	// Token: 0x04001F56 RID: 8022
	public UILabel AcceptLabel;

	// Token: 0x04001F57 RID: 8023
	public UITexture YancordLogo;

	// Token: 0x04001F58 RID: 8024
	public GameObject InternetPrompts;

	// Token: 0x04001F59 RID: 8025
	public GameObject NavigationMenu;

	// Token: 0x04001F5A RID: 8026
	public GameObject OnlineShopping;

	// Token: 0x04001F5B RID: 8027
	public GameObject SocialMedia;

	// Token: 0x04001F5C RID: 8028
	public GameObject NewPostText;

	// Token: 0x04001F5D RID: 8029
	public GameObject ChangeLabel;

	// Token: 0x04001F5E RID: 8030
	public GameObject ChangeIcon;

	// Token: 0x04001F5F RID: 8031
	public GameObject WriteLabel;

	// Token: 0x04001F60 RID: 8032
	public GameObject WriteIcon;

	// Token: 0x04001F61 RID: 8033
	public GameObject PostLabel;

	// Token: 0x04001F62 RID: 8034
	public GameObject PostIcon;

	// Token: 0x04001F63 RID: 8035
	public GameObject BG;

	// Token: 0x04001F64 RID: 8036
	public Transform MenuHighlight;

	// Token: 0x04001F65 RID: 8037
	public Transform StudentPost1;

	// Token: 0x04001F66 RID: 8038
	public Transform StudentPost2;

	// Token: 0x04001F67 RID: 8039
	public Transform YandereReply;

	// Token: 0x04001F68 RID: 8040
	public Transform YanderePost;

	// Token: 0x04001F69 RID: 8041
	public Transform LameReply;

	// Token: 0x04001F6A RID: 8042
	public Transform NewPost;

	// Token: 0x04001F6B RID: 8043
	public Transform Menu;

	// Token: 0x04001F6C RID: 8044
	public Transform[] StudentReplies;

	// Token: 0x04001F6D RID: 8045
	public UISprite[] Highlights;

	// Token: 0x04001F6E RID: 8046
	public UILabel[] PostLabels;

	// Token: 0x04001F6F RID: 8047
	public UILabel[] MenuLabels;

	// Token: 0x04001F70 RID: 8048
	public string[] Locations;

	// Token: 0x04001F71 RID: 8049
	public string[] Actions;

	// Token: 0x04001F72 RID: 8050
	public bool PostSequence;

	// Token: 0x04001F73 RID: 8051
	public bool WritingPost;

	// Token: 0x04001F74 RID: 8052
	public bool ShowMenu;

	// Token: 0x04001F75 RID: 8053
	public bool FadeOut;

	// Token: 0x04001F76 RID: 8054
	public bool Success;

	// Token: 0x04001F77 RID: 8055
	public bool Posted;

	// Token: 0x04001F78 RID: 8056
	public int MenuSelected = 1;

	// Token: 0x04001F79 RID: 8057
	public int Selected = 1;

	// Token: 0x04001F7A RID: 8058
	public int ID = 1;

	// Token: 0x04001F7B RID: 8059
	public int Location;

	// Token: 0x04001F7C RID: 8060
	public int Student;

	// Token: 0x04001F7D RID: 8061
	public int Action;

	// Token: 0x04001F7E RID: 8062
	public float Timer;

	// Token: 0x04001F7F RID: 8063
	public UITexture StudentPost1Portrait;

	// Token: 0x04001F80 RID: 8064
	public UITexture StudentPost2Portrait;

	// Token: 0x04001F81 RID: 8065
	public UITexture LamePostPortrait;

	// Token: 0x04001F82 RID: 8066
	public Texture CurrentPortrait;

	// Token: 0x04001F83 RID: 8067
	public UITexture[] Portraits;

	// Token: 0x04001F84 RID: 8068
	public int Height;

	// Token: 0x04001F85 RID: 8069
	public Transform Highlight;

	// Token: 0x04001F86 RID: 8070
	public Transform ItemList;

	// Token: 0x04001F87 RID: 8071
	public GameObject AreYouSure;

	// Token: 0x04001F88 RID: 8072
	public AudioSource MyAudio;

	// Token: 0x04001F89 RID: 8073
	public UILabel MoneyLabel;

	// Token: 0x04001F8A RID: 8074
	public float Shake;
}
