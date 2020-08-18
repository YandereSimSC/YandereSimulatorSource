using System;
using UnityEngine;

// Token: 0x02000267 RID: 615
public class DialogueWheelScript : MonoBehaviour
{
	// Token: 0x0600133F RID: 4927 RVA: 0x000A0960 File Offset: 0x0009EB60
	private void Start()
	{
		this.Interaction.localScale = new Vector3(1f, 1f, 1f);
		this.Favors.localScale = Vector3.zero;
		this.Club.localScale = Vector3.zero;
		this.Love.localScale = Vector3.zero;
		base.transform.localScale = Vector3.zero;
		this.OriginalColor = this.CenterLabel.color;
	}

	// Token: 0x06001340 RID: 4928 RVA: 0x000A09E0 File Offset: 0x0009EBE0
	private void Update()
	{
		if (!this.Show)
		{
			if (base.transform.localScale.x > 0.1f)
			{
				base.transform.localScale = Vector3.Lerp(base.transform.localScale, Vector3.zero, Time.deltaTime * 10f);
			}
			else if (this.Panel.enabled)
			{
				base.transform.localScale = Vector3.zero;
				this.Panel.enabled = false;
			}
		}
		else
		{
			if (this.Yandere.PauseScreen.Show)
			{
				this.Yandere.PauseScreen.ExitPhone();
			}
			if (this.ClubLeader)
			{
				this.Interaction.localScale = Vector3.Lerp(this.Interaction.localScale, Vector3.zero, Time.deltaTime * 10f);
				this.Favors.localScale = Vector3.Lerp(this.Favors.localScale, Vector3.zero, Time.deltaTime * 10f);
				this.Club.localScale = Vector3.Lerp(this.Club.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10f);
				this.Love.localScale = Vector3.Lerp(this.Love.localScale, Vector3.zero, Time.deltaTime * 10f);
			}
			else if (this.AskingFavor)
			{
				this.Interaction.localScale = Vector3.Lerp(this.Interaction.localScale, Vector3.zero, Time.deltaTime * 10f);
				this.Favors.localScale = Vector3.Lerp(this.Favors.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10f);
				this.Club.localScale = Vector3.Lerp(this.Club.localScale, Vector3.zero, Time.deltaTime * 10f);
				this.Love.localScale = Vector3.Lerp(this.Love.localScale, Vector3.zero, Time.deltaTime * 10f);
			}
			else if (this.Matchmaking)
			{
				this.Interaction.localScale = Vector3.Lerp(this.Interaction.localScale, Vector3.zero, Time.deltaTime * 10f);
				this.Favors.localScale = Vector3.Lerp(this.Favors.localScale, Vector3.zero, Time.deltaTime * 10f);
				this.Club.localScale = Vector3.Lerp(this.Club.localScale, Vector3.zero, Time.deltaTime * 10f);
				this.Love.localScale = Vector3.Lerp(this.Love.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10f);
			}
			else
			{
				this.Interaction.localScale = Vector3.Lerp(this.Interaction.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10f);
				this.Favors.localScale = Vector3.Lerp(this.Favors.localScale, Vector3.zero, Time.deltaTime * 10f);
				this.Club.localScale = Vector3.Lerp(this.Club.localScale, Vector3.zero, Time.deltaTime * 10f);
				this.Love.localScale = Vector3.Lerp(this.Love.localScale, Vector3.zero, Time.deltaTime * 10f);
			}
			this.MouseDelta.x = this.MouseDelta.x + Input.GetAxis("Mouse X");
			this.MouseDelta.y = this.MouseDelta.y + Input.GetAxis("Mouse Y");
			if (this.MouseDelta.x > 11f)
			{
				this.MouseDelta.x = 11f;
			}
			else if (this.MouseDelta.x < -11f)
			{
				this.MouseDelta.x = -11f;
			}
			if (this.MouseDelta.y > 11f)
			{
				this.MouseDelta.y = 11f;
			}
			else if (this.MouseDelta.y < -11f)
			{
				this.MouseDelta.y = -11f;
			}
			base.transform.localScale = Vector3.Lerp(base.transform.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10f);
			if (!this.AskingFavor && !this.Matchmaking)
			{
				if (Input.GetAxis("Vertical") < 0.5f && Input.GetAxis("Vertical") > -0.5f && Input.GetAxis("Horizontal") < 0.5f && Input.GetAxis("Horizontal") > -0.5f)
				{
					this.Selected = 0;
				}
				if ((Input.GetAxis("Vertical") > 0.5f && Input.GetAxis("Horizontal") < 0.5f && Input.GetAxis("Horizontal") > -0.5f) || (this.MouseDelta.y > 10f && this.MouseDelta.x < 10f && this.MouseDelta.x > -10f))
				{
					this.Selected = 1;
				}
				if ((Input.GetAxis("Vertical") > 0f && Input.GetAxis("Horizontal") > 0.5f) || (this.MouseDelta.y > 0f && this.MouseDelta.x > 10f))
				{
					this.Selected = 2;
				}
				if ((Input.GetAxis("Vertical") < 0f && Input.GetAxis("Horizontal") > 0.5f) || (this.MouseDelta.y < 0f && this.MouseDelta.x > 10f))
				{
					this.Selected = 3;
				}
				if ((Input.GetAxis("Vertical") < -0.5f && Input.GetAxis("Horizontal") < 0.5f && Input.GetAxis("Horizontal") > -0.5f) || (this.MouseDelta.y < -10f && this.MouseDelta.x < 10f && this.MouseDelta.x > -10f))
				{
					this.Selected = 4;
				}
				if ((Input.GetAxis("Vertical") < 0f && Input.GetAxis("Horizontal") < -0.5f) || (this.MouseDelta.y < 0f && this.MouseDelta.x < -10f))
				{
					this.Selected = 5;
				}
				if ((Input.GetAxis("Vertical") > 0f && Input.GetAxis("Horizontal") < -0.5f) || (this.MouseDelta.y > 0f && this.MouseDelta.x < -10f))
				{
					this.Selected = 6;
				}
				this.CenterLabel.text = this.Text[this.Selected];
				this.CenterLabel.color = this.OriginalColor;
				if (!this.ClubLeader)
				{
					if (this.Selected == 5)
					{
						if (PlayerGlobals.GetStudentFriend(this.Yandere.TargetStudent.StudentID))
						{
							this.CenterLabel.text = "Love";
						}
					}
					else if (this.Selected == 6 && ClubGlobals.Club == ClubType.Delinquent)
					{
						this.CenterLabel.text = "Intimidate";
						this.CenterLabel.color = new Color(1f, 0f, 0f, 1f);
					}
				}
				else
				{
					this.CenterLabel.text = this.ClubText[this.Selected];
				}
			}
			else
			{
				if (Input.GetAxis("Vertical") < 0.5f && Input.GetAxis("Vertical") > -0.5f && Input.GetAxis("Horizontal") < 0.5f && Input.GetAxis("Horizontal") > -0.5f)
				{
					this.Selected = 0;
				}
				if ((Input.GetAxis("Vertical") > 0.5f && Input.GetAxis("Horizontal") < 0.5f && Input.GetAxis("Horizontal") > -0.5f) || (this.MouseDelta.y > 10f && this.MouseDelta.x < 10f && this.MouseDelta.x > -10f))
				{
					this.Selected = 1;
				}
				if ((Input.GetAxis("Vertical") < 0.5f && Input.GetAxis("Vertical") > -0.5f && Input.GetAxis("Horizontal") > 0.5f) || (this.MouseDelta.y < 10f && this.MouseDelta.y > -10f && this.MouseDelta.x > 10f))
				{
					this.Selected = 2;
				}
				if ((Input.GetAxis("Vertical") < -0.5f && Input.GetAxis("Horizontal") < 0.5f && Input.GetAxis("Horizontal") > -0.5f) || (this.MouseDelta.y < -10f && this.MouseDelta.x < 10f && this.MouseDelta.x > -10f))
				{
					this.Selected = 3;
				}
				if ((Input.GetAxis("Vertical") < 0.5f && Input.GetAxis("Vertical") > -0.5f && Input.GetAxis("Horizontal") < -0.5f) || (this.MouseDelta.y < 10f && this.MouseDelta.y > -10f && this.MouseDelta.x < -10f))
				{
					this.Selected = 4;
				}
				if (this.Selected < this.FavorText.Length)
				{
					this.CenterLabel.text = (this.AskingFavor ? this.FavorText[this.Selected] : this.LoveText[this.Selected]);
				}
			}
			if (!this.ClubLeader)
			{
				for (int i = 1; i < 7; i++)
				{
					Transform transform = this.Segment[i].transform;
					transform.localScale = Vector3.Lerp(transform.localScale, (this.Selected == i) ? new Vector3(1.3f, 1.3f, 1f) : new Vector3(1f, 1f, 1f), Time.deltaTime * 10f);
				}
			}
			else
			{
				for (int j = 1; j < 7; j++)
				{
					Transform transform2 = this.ClubSegment[j].transform;
					transform2.localScale = Vector3.Lerp(transform2.localScale, (this.Selected == j) ? new Vector3(1.3f, 1.3f, 1f) : new Vector3(1f, 1f, 1f), Time.deltaTime * 10f);
				}
			}
			if (!this.Matchmaking)
			{
				for (int k = 1; k < 5; k++)
				{
					Transform transform3 = this.FavorSegment[k].transform;
					transform3.localScale = Vector3.Lerp(transform3.localScale, (this.Selected == k) ? new Vector3(1.3f, 1.3f, 1f) : new Vector3(1f, 1f, 1f), Time.deltaTime * 10f);
				}
			}
			else
			{
				for (int l = 1; l < 5; l++)
				{
					Transform transform4 = this.LoveSegment[l].transform;
					transform4.localScale = Vector3.Lerp(transform4.localScale, (this.Selected == l) ? new Vector3(1.3f, 1.3f, 1f) : new Vector3(1f, 1f, 1f), Time.deltaTime * 10f);
				}
			}
			if (Input.GetButtonDown("A"))
			{
				if (this.ClubLeader)
				{
					if (this.Selected != 0 && this.ClubShadow[this.Selected].color.a == 0f)
					{
						int num = 0;
						if (this.Yandere.TargetStudent.Sleuthing)
						{
							num = 5;
						}
						if (this.Selected == 1)
						{
							this.Impatience.fillAmount = 0f;
							this.Yandere.TargetStudent.Interaction = StudentInteractionType.ClubInfo;
							this.Yandere.TargetStudent.TalkTimer = 100f;
							this.Yandere.TargetStudent.ClubPhase = 1;
							this.Show = false;
						}
						else if (this.Selected == 2)
						{
							this.Impatience.fillAmount = 0f;
							this.Yandere.TargetStudent.Interaction = StudentInteractionType.ClubJoin;
							this.Yandere.TargetStudent.TalkTimer = 100f;
							this.Show = false;
							this.ClubManager.CheckGrudge(this.Yandere.TargetStudent.Club);
							if (ClubGlobals.GetQuitClub(this.Yandere.TargetStudent.Club))
							{
								this.Yandere.TargetStudent.ClubPhase = 4;
							}
							else if (ClubGlobals.Club != ClubType.None)
							{
								this.Yandere.TargetStudent.ClubPhase = 5;
							}
							else if (this.ClubManager.ClubGrudge)
							{
								this.Yandere.TargetStudent.ClubPhase = 6;
							}
							else
							{
								this.Yandere.TargetStudent.ClubPhase = 1;
							}
						}
						else if (this.Selected == 3)
						{
							this.Impatience.fillAmount = 0f;
							this.Yandere.TargetStudent.Interaction = StudentInteractionType.ClubQuit;
							this.Yandere.TargetStudent.TalkTimer = 100f;
							this.Yandere.TargetStudent.ClubPhase = 1;
							this.Show = false;
						}
						else if (this.Selected == 4)
						{
							this.Impatience.fillAmount = 0f;
							this.Yandere.TargetStudent.Interaction = StudentInteractionType.ClubBye;
							this.Yandere.TargetStudent.TalkTimer = this.Yandere.Subtitle.ClubFarewellClips[(int)(this.Yandere.TargetStudent.Club + num)].length;
							this.Show = false;
							Debug.Log("This club leader exchange is over.");
						}
						else if (this.Selected == 5)
						{
							this.Impatience.fillAmount = 0f;
							this.Yandere.TargetStudent.Interaction = StudentInteractionType.ClubActivity;
							this.Yandere.TargetStudent.TalkTimer = 100f;
							if (this.Clock.HourTime < 17f)
							{
								this.Yandere.TargetStudent.ClubPhase = 4;
							}
							else if (this.Clock.HourTime > 17.5f)
							{
								this.Yandere.TargetStudent.ClubPhase = 5;
							}
							else
							{
								this.Yandere.TargetStudent.ClubPhase = 1;
							}
							this.Show = false;
						}
						else if (this.Selected == 6)
						{
							this.Impatience.fillAmount = 0f;
							this.Yandere.TargetStudent.Interaction = StudentInteractionType.ClubPractice;
							this.Yandere.TargetStudent.TalkTimer = 100f;
							this.Yandere.TargetStudent.ClubPhase = 1;
							this.Show = false;
						}
					}
				}
				else if (this.AskingFavor)
				{
					if (this.Selected != 0)
					{
						if (this.Selected < this.FavorShadow.Length && this.FavorShadow[this.Selected] != null && this.FavorShadow[this.Selected].color.a == 0f)
						{
							if (this.Selected == 1)
							{
								this.Impatience.fillAmount = 0f;
								this.Yandere.Interaction = YandereInteractionType.FollowMe;
								this.Yandere.TalkTimer = 3f;
								this.Show = false;
							}
							else if (this.Selected == 2)
							{
								this.Impatience.fillAmount = 0f;
								this.Yandere.Interaction = YandereInteractionType.GoAway;
								this.Yandere.TalkTimer = 3f;
								this.Show = false;
							}
							else if (this.Selected == 4)
							{
								this.PauseScreen.StudentInfoMenu.Distracting = true;
								this.PauseScreen.StudentInfoMenu.gameObject.SetActive(true);
								this.PauseScreen.StudentInfoMenu.Column = 0;
								this.PauseScreen.StudentInfoMenu.Row = 0;
								this.PauseScreen.StudentInfoMenu.UpdateHighlight();
								base.StartCoroutine(this.PauseScreen.StudentInfoMenu.UpdatePortraits());
								this.PauseScreen.MainMenu.SetActive(false);
								this.PauseScreen.Panel.enabled = true;
								this.PauseScreen.Sideways = true;
								this.PauseScreen.Show = true;
								Time.timeScale = 0.0001f;
								this.PromptBar.ClearButtons();
								this.PromptBar.Label[1].text = "Cancel";
								this.PromptBar.UpdateButtons();
								this.PromptBar.Show = true;
								this.Impatience.fillAmount = 0f;
								this.Yandere.Interaction = YandereInteractionType.DistractThem;
								this.Yandere.TalkTimer = 3f;
								this.Show = false;
							}
						}
						if (this.Selected == 3)
						{
							this.AskingFavor = false;
						}
					}
				}
				else if (this.Matchmaking)
				{
					if (this.Selected != 0)
					{
						if (this.Selected < this.LoveShadow.Length && this.LoveShadow[this.Selected] != null && this.LoveShadow[this.Selected].color.a == 0f)
						{
							if (this.Selected == 1)
							{
								this.PromptBar.ClearButtons();
								this.PromptBar.Label[0].text = "Select";
								this.PromptBar.Label[4].text = "Change";
								this.PromptBar.UpdateButtons();
								this.PromptBar.Show = true;
								this.AppearanceWindow.gameObject.SetActive(true);
								this.AppearanceWindow.Show = true;
								this.Show = false;
							}
							else if (this.Selected == 2)
							{
								this.Impatience.fillAmount = 0f;
								this.Yandere.Interaction = YandereInteractionType.Court;
								this.Yandere.TalkTimer = 5f;
								this.Show = false;
							}
							else if (this.Selected == 4)
							{
								this.Impatience.fillAmount = 0f;
								this.Yandere.Interaction = YandereInteractionType.Confess;
								this.Yandere.TalkTimer = 5f;
								this.Show = false;
							}
						}
						if (this.Selected == 3)
						{
							this.Matchmaking = false;
						}
					}
				}
				else if (this.Selected != 0 && this.Shadow[this.Selected].color.a == 0f)
				{
					if (this.Selected == 1)
					{
						this.Impatience.fillAmount = 0f;
						this.Yandere.Interaction = YandereInteractionType.Apologizing;
						this.Yandere.TalkTimer = 3f;
						this.Show = false;
					}
					else if (this.Selected == 2)
					{
						this.Impatience.fillAmount = 0f;
						this.Yandere.Interaction = YandereInteractionType.Compliment;
						this.Yandere.TalkTimer = 3f;
						this.Show = false;
					}
					else if (this.Selected == 3)
					{
						this.PauseScreen.StudentInfoMenu.Gossiping = true;
						this.PauseScreen.StudentInfoMenu.gameObject.SetActive(true);
						this.PauseScreen.StudentInfoMenu.Column = 0;
						this.PauseScreen.StudentInfoMenu.Row = 0;
						this.PauseScreen.StudentInfoMenu.UpdateHighlight();
						base.StartCoroutine(this.PauseScreen.StudentInfoMenu.UpdatePortraits());
						this.PauseScreen.MainMenu.SetActive(false);
						this.PauseScreen.Panel.enabled = true;
						this.PauseScreen.Sideways = true;
						this.PauseScreen.Show = true;
						Time.timeScale = 0.0001f;
						this.PromptBar.ClearButtons();
						this.PromptBar.Label[0].text = string.Empty;
						this.PromptBar.Label[1].text = "Cancel";
						this.PromptBar.UpdateButtons();
						this.PromptBar.Show = true;
						this.Impatience.fillAmount = 0f;
						this.Yandere.Interaction = YandereInteractionType.Gossip;
						this.Yandere.TalkTimer = 3f;
						this.Show = false;
					}
					else if (this.Selected == 4)
					{
						this.Impatience.fillAmount = 0f;
						this.Yandere.Interaction = YandereInteractionType.Bye;
						this.Yandere.TalkTimer = 2f;
						this.Show = false;
						Debug.Log("This exchange is over.");
					}
					else if (this.Selected == 5)
					{
						if (!PlayerGlobals.GetStudentFriend(this.Yandere.TargetStudent.StudentID))
						{
							this.CheckTaskCompletion();
							if (this.Yandere.TargetStudent.TaskPhase == 0)
							{
								this.Impatience.fillAmount = 0f;
								this.Yandere.TargetStudent.Interaction = StudentInteractionType.GivingTask;
								this.Yandere.TargetStudent.TalkTimer = 100f;
								this.Yandere.TargetStudent.TaskPhase = 1;
							}
							else
							{
								this.Impatience.fillAmount = 0f;
								this.Yandere.TargetStudent.Interaction = StudentInteractionType.GivingTask;
								this.Yandere.TargetStudent.TalkTimer = 100f;
							}
							this.Show = false;
						}
						else if (this.Yandere.LoveManager.SuitorProgress == 0)
						{
							this.PauseScreen.StudentInfoMenu.MatchMaking = true;
							this.PauseScreen.StudentInfoMenu.gameObject.SetActive(true);
							this.PauseScreen.StudentInfoMenu.Column = 0;
							this.PauseScreen.StudentInfoMenu.Row = 0;
							this.PauseScreen.StudentInfoMenu.UpdateHighlight();
							base.StartCoroutine(this.PauseScreen.StudentInfoMenu.UpdatePortraits());
							this.PauseScreen.MainMenu.SetActive(false);
							this.PauseScreen.Panel.enabled = true;
							this.PauseScreen.Sideways = true;
							this.PauseScreen.Show = true;
							Time.timeScale = 0.0001f;
							this.PromptBar.ClearButtons();
							this.PromptBar.Label[0].text = "View Info";
							this.PromptBar.Label[1].text = "Cancel";
							this.PromptBar.UpdateButtons();
							this.PromptBar.Show = true;
							this.Impatience.fillAmount = 0f;
							this.Yandere.Interaction = YandereInteractionType.NamingCrush;
							this.Yandere.TalkTimer = 3f;
							this.Show = false;
						}
					}
					else if (this.Selected == 6)
					{
						this.AskingFavor = true;
					}
				}
			}
			else if (Input.GetButtonDown("X"))
			{
				if (this.TaskDialogueWindow.activeInHierarchy)
				{
					this.Impatience.fillAmount = 0f;
					this.Yandere.Interaction = YandereInteractionType.TaskInquiry;
					this.Yandere.TalkTimer = 3f;
					this.Show = false;
				}
				else if (this.SwitchTopicsWindow.activeInHierarchy)
				{
					this.ClubLeader = !this.ClubLeader;
					this.HideShadows();
				}
			}
			else if (Input.GetButtonDown("B") && this.LockerWindow.activeInHierarchy)
			{
				this.Impatience.fillAmount = 0f;
				this.Yandere.Interaction = YandereInteractionType.SendingToLocker;
				this.Yandere.TalkTimer = 5f;
				this.Show = false;
			}
		}
		this.PreviousPosition = Input.mousePosition;
	}

	// Token: 0x06001341 RID: 4929 RVA: 0x000A224C File Offset: 0x000A044C
	public void HideShadows()
	{
		this.Jukebox.Dip = 0.5f;
		this.TaskDialogueWindow.SetActive(false);
		this.ClubLeaderWindow.SetActive(false);
		this.LockerWindow.SetActive(false);
		if (this.ClubLeader && !this.Yandere.TargetStudent.Talk.Fake)
		{
			this.SwitchTopicsWindow.SetActive(true);
		}
		else
		{
			this.SwitchTopicsWindow.SetActive(false);
		}
		if (this.Yandere.TargetStudent.Armband.activeInHierarchy && !this.ClubLeader && this.Yandere.TargetStudent.Club != ClubType.Council)
		{
			this.ClubLeaderWindow.SetActive(true);
		}
		if (this.NoteLocker.NoteLeft && this.NoteLocker.Student == this.Yandere.TargetStudent)
		{
			this.LockerWindow.SetActive(true);
		}
		if (this.Yandere.TargetStudent.Club == ClubType.Bully && TaskGlobals.GetTaskStatus(36) == 1)
		{
			this.TaskDialogueWindow.SetActive(true);
		}
		this.TaskIcon.spriteName = (PlayerGlobals.GetStudentFriend(this.Yandere.TargetStudent.StudentID) ? "Heart" : "Task");
		this.Impatience.fillAmount = 0f;
		for (int i = 1; i < 7; i++)
		{
			UISprite uisprite = this.Shadow[i];
			uisprite.color = new Color(uisprite.color.r, uisprite.color.g, uisprite.color.b, 0f);
		}
		for (int j = 1; j < 5; j++)
		{
			UISprite uisprite2 = this.FavorShadow[j];
			uisprite2.color = new Color(uisprite2.color.r, uisprite2.color.g, uisprite2.color.b, 0f);
		}
		for (int k = 1; k < 7; k++)
		{
			UISprite uisprite3 = this.ClubShadow[k];
			uisprite3.color = new Color(uisprite3.color.r, uisprite3.color.g, uisprite3.color.b, 0f);
		}
		for (int l = 1; l < 5; l++)
		{
			UISprite uisprite4 = this.LoveShadow[l];
			uisprite4.color = new Color(uisprite4.color.r, uisprite4.color.g, uisprite4.color.b, 0f);
		}
		if (!this.Yandere.TargetStudent.Witness || this.Yandere.TargetStudent.Forgave || this.Yandere.TargetStudent.Club == ClubType.Council)
		{
			UISprite uisprite5 = this.Shadow[1];
			uisprite5.color = new Color(uisprite5.color.r, uisprite5.color.g, uisprite5.color.b, 0.75f);
		}
		if (this.Yandere.TargetStudent.Complimented || this.Yandere.TargetStudent.Club == ClubType.Council)
		{
			UISprite uisprite6 = this.Shadow[2];
			uisprite6.color = new Color(uisprite6.color.r, uisprite6.color.g, uisprite6.color.b, 0.75f);
		}
		if (this.Yandere.TargetStudent.Gossiped || this.Yandere.TargetStudent.Club == ClubType.Council)
		{
			UISprite uisprite7 = this.Shadow[3];
			uisprite7.color = new Color(uisprite7.color.r, uisprite7.color.g, uisprite7.color.b, 0.75f);
		}
		if (this.Yandere.Bloodiness > 0f || this.Yandere.Sanity < 33.33333f || this.Yandere.TargetStudent.Club == ClubType.Council)
		{
			UISprite uisprite8 = this.Shadow[3];
			uisprite8.color = new Color(uisprite8.color.r, uisprite8.color.g, uisprite8.color.b, 0.75f);
			this.Shadow[5].color = new Color(0f, 0f, 0f, 0.75f);
			UISprite uisprite9 = this.Shadow[6];
			uisprite9.color = new Color(uisprite9.color.r, uisprite9.color.g, uisprite9.color.b, 0.75f);
		}
		else if (this.Reputation.Reputation < -33.33333f)
		{
			UISprite uisprite10 = this.Shadow[3];
			uisprite10.color = new Color(uisprite10.color.r, uisprite10.color.g, uisprite10.color.b, 0.75f);
		}
		if (!this.Yandere.TargetStudent.Indoors || this.Yandere.TargetStudent.Club == ClubType.Council)
		{
			this.Shadow[5].color = new Color(0f, 0f, 0f, 0.75f);
		}
		else if (!PlayerGlobals.GetStudentFriend(this.Yandere.TargetStudent.StudentID))
		{
			bool flag = false;
			if (this.Yandere.TargetStudent.StudentID != 8 && this.Yandere.TargetStudent.StudentID != 11 && this.Yandere.TargetStudent.StudentID != 25 && this.Yandere.TargetStudent.StudentID != 28 && this.Yandere.TargetStudent.StudentID != 30 && this.Yandere.TargetStudent.StudentID != 36 && this.Yandere.TargetStudent.StudentID != 37 && this.Yandere.TargetStudent.StudentID != 38 && this.Yandere.TargetStudent.StudentID != 52 && this.Yandere.TargetStudent.StudentID != 76 && this.Yandere.TargetStudent.StudentID != 77 && this.Yandere.TargetStudent.StudentID != 78 && this.Yandere.TargetStudent.StudentID != 79 && this.Yandere.TargetStudent.StudentID != 80 && this.Yandere.TargetStudent.StudentID != 81)
			{
				flag = true;
			}
			if (this.Yandere.TargetStudent.StudentID == 1 || this.Yandere.TargetStudent.StudentID == 41)
			{
				this.Shadow[5].color = new Color(0f, 0f, 0f, 0.75f);
			}
			else
			{
				if ((this.Yandere.TargetStudent.TaskPhase > 0 && this.Yandere.TargetStudent.TaskPhase < 5) || (TaskGlobals.GetTaskStatus(this.Yandere.TargetStudent.StudentID) > 0 && TaskGlobals.GetTaskStatus(this.Yandere.TargetStudent.StudentID) < 5 && TaskGlobals.GetTaskStatus(this.Yandere.TargetStudent.StudentID) != 2) || this.Yandere.TargetStudent.TaskPhase == 100)
				{
					Debug.Log("Hiding task button.");
					this.Shadow[5].color = new Color(0f, 0f, 0f, 0.75f);
				}
				if (this.Yandere.TargetStudent.TaskPhase == 5)
				{
					Debug.Log("Unhiding task button.");
					this.Shadow[5].color = new Color(0f, 0f, 0f, 0f);
				}
				if (this.Yandere.TargetStudent.StudentID == 6)
				{
					Debug.Log("The status of Task #6 is:" + TaskGlobals.GetTaskStatus(6));
					if (TaskGlobals.GetTaskStatus(6) == 1 && this.Yandere.Inventory.Headset)
					{
						this.Shadow[5].color = new Color(0f, 0f, 0f, 0f);
						Debug.Log("Player has a headset.");
					}
				}
				else if (this.Yandere.TargetStudent.StudentID == 36)
				{
					if (TaskGlobals.GetTaskStatus(36) == 0 && (StudentGlobals.GetStudentDead(81) || StudentGlobals.GetStudentDead(82) || StudentGlobals.GetStudentDead(83) || StudentGlobals.GetStudentDead(84) || StudentGlobals.GetStudentDead(85)))
					{
						this.Shadow[5].color = new Color(0f, 0f, 0f, 0.75f);
					}
				}
				else if (this.Yandere.TargetStudent.StudentID == 76)
				{
					Debug.Log("The status of Task #76 is:" + TaskGlobals.GetTaskStatus(76));
					if (TaskGlobals.GetTaskStatus(76) == 1 && this.Yandere.Inventory.Money >= 100f)
					{
						this.Shadow[5].color = new Color(0f, 0f, 0f, 0f);
						Debug.Log("Player has over $100.");
					}
				}
				else if (this.Yandere.TargetStudent.StudentID == 77)
				{
					if (TaskGlobals.GetTaskStatus(77) == 1 && ((this.Yandere.Weapon[1] != null && this.Yandere.Weapon[1].WeaponID == 1) || (this.Yandere.Weapon[1] != null && this.Yandere.Weapon[1].WeaponID == 8) || (this.Yandere.Weapon[2] != null && this.Yandere.Weapon[2].WeaponID == 1) || (this.Yandere.Weapon[2] != null && this.Yandere.Weapon[2].WeaponID == 8)))
					{
						this.Shadow[5].color = new Color(0f, 0f, 0f, 0f);
						Debug.Log("Player has a knife.");
					}
				}
				else if (this.Yandere.TargetStudent.StudentID == 78)
				{
					if (TaskGlobals.GetTaskStatus(78) == 1 && this.Yandere.Inventory.Sake)
					{
						this.Shadow[5].color = new Color(0f, 0f, 0f, 0f);
						Debug.Log("Player has sake.");
					}
				}
				else if (this.Yandere.TargetStudent.StudentID == 79)
				{
					if (TaskGlobals.GetTaskStatus(79) == 1 && this.Yandere.Inventory.Cigs)
					{
						this.Shadow[5].color = new Color(0f, 0f, 0f, 0f);
						Debug.Log("Player has ciggies.");
					}
				}
				else if (this.Yandere.TargetStudent.StudentID == 80 && TaskGlobals.GetTaskStatus(80) == 1 && this.Yandere.Inventory.AnswerSheet)
				{
					this.Shadow[5].color = new Color(0f, 0f, 0f, 0f);
					Debug.Log("Player has the answer sheet.");
				}
				if (flag && TaskGlobals.GetTaskStatus(this.Yandere.TargetStudent.StudentID) == 1 && this.Yandere.Inventory.Book)
				{
					this.Shadow[5].color = new Color(0f, 0f, 0f, 0f);
					Debug.Log("The player has a library book.");
				}
			}
		}
		else if (this.Yandere.TargetStudent.StudentID != this.LoveManager.RivalID && this.Yandere.TargetStudent.StudentID != this.LoveManager.SuitorID)
		{
			this.Shadow[5].color = new Color(0f, 0f, 0f, 0.75f);
		}
		else if (!this.Yandere.TargetStudent.Male && this.LoveManager.SuitorProgress == 0)
		{
			this.Shadow[5].color = new Color(0f, 0f, 0f, 0.75f);
		}
		if (!this.Yandere.TargetStudent.Indoors || this.Yandere.TargetStudent.Club == ClubType.Council)
		{
			UISprite uisprite11 = this.Shadow[6];
			uisprite11.color = new Color(uisprite11.color.r, uisprite11.color.g, uisprite11.color.b, 0.75f);
		}
		else
		{
			if (!PlayerGlobals.GetStudentFriend(this.Yandere.TargetStudent.StudentID))
			{
				this.Shadow[6].color = new Color(0f, 0f, 0f, 0.75f);
			}
			if ((this.Yandere.TargetStudent.Male && PlayerGlobals.Seduction + PlayerGlobals.SeductionBonus > 3) || PlayerGlobals.Seduction + PlayerGlobals.SeductionBonus > 4 || ClubGlobals.Club == ClubType.Delinquent)
			{
				this.Shadow[6].color = new Color(0f, 0f, 0f, 0f);
			}
			if (this.Yandere.TargetStudent.Club == ClubType.Delinquent)
			{
				this.Shadow[6].color = new Color(0f, 0f, 0f, 0.75f);
			}
			if (this.Yandere.TargetStudent.CurrentAction == StudentActionType.Sunbathe)
			{
				this.Shadow[6].color = new Color(0f, 0f, 0f, 0.75f);
			}
		}
		if (ClubGlobals.Club == this.Yandere.TargetStudent.Club)
		{
			UISprite uisprite12 = this.ClubShadow[1];
			uisprite12.color = new Color(uisprite12.color.r, uisprite12.color.g, uisprite12.color.b, 0.75f);
			UISprite uisprite13 = this.ClubShadow[2];
			uisprite13.color = new Color(uisprite13.color.r, uisprite13.color.g, uisprite13.color.b, 0.75f);
		}
		if (this.Yandere.ClubAttire || this.Yandere.Mask != null || this.Yandere.Gloves != null || this.Yandere.Container != null)
		{
			UISprite uisprite14 = this.ClubShadow[3];
			uisprite14.color = new Color(uisprite14.color.r, uisprite14.color.g, uisprite14.color.b, 0.75f);
		}
		if (ClubGlobals.Club != this.Yandere.TargetStudent.Club)
		{
			UISprite uisprite15 = this.ClubShadow[2];
			uisprite15.color = new Color(uisprite15.color.r, uisprite15.color.g, uisprite15.color.b, 0f);
			UISprite uisprite16 = this.ClubShadow[3];
			uisprite16.color = new Color(uisprite16.color.r, uisprite16.color.g, uisprite16.color.b, 0.75f);
			this.ClubShadow[5].color = new Color(0f, 0f, 0f, 0.75f);
		}
		if (this.Yandere.StudentManager.MurderTakingPlace)
		{
			this.ClubShadow[5].color = new Color(0f, 0f, 0f, 0.75f);
		}
		if ((this.Yandere.TargetStudent.StudentID != 46 && this.Yandere.TargetStudent.StudentID != 51) || this.Yandere.Police.Show)
		{
			UISprite uisprite17 = this.ClubShadow[6];
			uisprite17.color = new Color(uisprite17.color.r, uisprite17.color.g, uisprite17.color.b, 0.75f);
		}
		if (this.Yandere.TargetStudent.StudentID == 51)
		{
			int num = 4;
			if (ClubGlobals.Club != ClubType.LightMusic || this.PracticeWindow.PlayedRhythmMinigame)
			{
				num = 0;
			}
			for (int m = 52; m < 56; m++)
			{
				if (this.Yandere.StudentManager.Students[m] == null)
				{
					num--;
				}
				else if (!this.Yandere.StudentManager.Students[m].gameObject.activeInHierarchy || this.Yandere.StudentManager.Students[m].Investigating || this.Yandere.StudentManager.Students[m].Distracting || this.Yandere.StudentManager.Students[m].Distracted || this.Yandere.StudentManager.Students[m].SentHome || this.Yandere.StudentManager.Students[m].Tranquil || this.Yandere.StudentManager.Students[m].GoAway || !this.Yandere.StudentManager.Students[m].Routine || !this.Yandere.StudentManager.Students[m].Alive)
				{
					num--;
				}
			}
			if (num < 4)
			{
				UISprite uisprite18 = this.ClubShadow[6];
				uisprite18.color = new Color(uisprite18.color.r, uisprite18.color.g, uisprite18.color.b, 0.75f);
			}
		}
		if (this.Yandere.Followers > 0)
		{
			UISprite uisprite19 = this.FavorShadow[1];
			uisprite19.color = new Color(uisprite19.color.r, uisprite19.color.g, uisprite19.color.b, 0.75f);
		}
		if (this.Yandere.TargetStudent.DistanceToDestination > 0.5f)
		{
			UISprite uisprite20 = this.FavorShadow[2];
			uisprite20.color = new Color(uisprite20.color.r, uisprite20.color.g, uisprite20.color.b, 0.75f);
		}
		if (!this.Yandere.TargetStudent.Male)
		{
			UISprite uisprite21 = this.LoveShadow[1];
			uisprite21.color = new Color(uisprite21.color.r, uisprite21.color.g, uisprite21.color.b, 0.75f);
		}
		if (this.DatingMinigame == null || (this.Yandere.TargetStudent.Male && !this.LoveManager.RivalWaiting) || this.LoveManager.Courted)
		{
			UISprite uisprite22 = this.LoveShadow[2];
			uisprite22.color = new Color(uisprite22.color.r, uisprite22.color.g, uisprite22.color.b, 0.75f);
		}
		if (!this.Yandere.TargetStudent.Male || !this.Yandere.Inventory.Rose || this.Yandere.TargetStudent.Rose)
		{
			UISprite uisprite23 = this.LoveShadow[4];
			uisprite23.color = new Color(uisprite23.color.r, uisprite23.color.g, uisprite23.color.b, 0.75f);
		}
	}

	// Token: 0x06001342 RID: 4930 RVA: 0x000A36B0 File Offset: 0x000A18B0
	private void CheckTaskCompletion()
	{
		Debug.Log("This student's Task Status is: " + TaskGlobals.GetTaskStatus(this.Yandere.TargetStudent.StudentID));
		Debug.Log("Checking for task completion.");
		if (this.Yandere.TargetStudent.StudentID == 6 && TaskGlobals.GetTaskStatus(6) == 1 && this.Yandere.Inventory.Headset)
		{
			this.Yandere.TargetStudent.TaskPhase = 5;
			this.Yandere.LoveManager.SuitorProgress = 1;
			DatingGlobals.SuitorProgress = 1;
		}
		if (this.Yandere.TargetStudent.StudentID == 76 && TaskGlobals.GetTaskStatus(76) == 1)
		{
			this.Yandere.TargetStudent.RespectEarned = true;
			this.Yandere.TargetStudent.TaskPhase = 5;
			this.Yandere.Inventory.Money -= 100f;
			this.Yandere.Inventory.UpdateMoney();
		}
		else if (this.Yandere.TargetStudent.StudentID == 77 && TaskGlobals.GetTaskStatus(77) == 1)
		{
			this.Yandere.TargetStudent.RespectEarned = true;
			this.Yandere.TargetStudent.TaskPhase = 5;
			WeaponScript weaponScript;
			if ((this.Yandere.Weapon[1] != null && this.Yandere.Weapon[1].WeaponID == 1) || (this.Yandere.Weapon[1] != null && this.Yandere.Weapon[1].WeaponID == 8))
			{
				weaponScript = this.Yandere.Weapon[1];
				this.Yandere.Weapon[1] = null;
			}
			else
			{
				weaponScript = this.Yandere.Weapon[2];
				this.Yandere.Weapon[2] = null;
			}
			weaponScript.Drop();
			weaponScript.FingerprintID = 77;
			weaponScript.gameObject.SetActive(false);
			this.Yandere.WeaponManager.UpdateLabels();
			this.Yandere.WeaponMenu.UpdateSprites();
		}
		else if (this.Yandere.TargetStudent.StudentID == 78 && TaskGlobals.GetTaskStatus(78) == 1)
		{
			this.Yandere.TargetStudent.RespectEarned = true;
			this.Yandere.TargetStudent.TaskPhase = 5;
			this.Yandere.Inventory.Sake = false;
		}
		else if (this.Yandere.TargetStudent.StudentID == 79 && TaskGlobals.GetTaskStatus(79) == 1)
		{
			this.Yandere.TargetStudent.RespectEarned = true;
			this.Yandere.TargetStudent.TaskPhase = 5;
			this.Yandere.Inventory.Cigs = false;
		}
		else if (this.Yandere.TargetStudent.StudentID == 80 && TaskGlobals.GetTaskStatus(80) == 1)
		{
			this.Yandere.TargetStudent.RespectEarned = true;
			this.Yandere.TargetStudent.TaskPhase = 5;
			this.Yandere.Inventory.AnswerSheet = false;
		}
		bool flag = false;
		if (this.Yandere.TargetStudent.StudentID != 8 && this.Yandere.TargetStudent.StudentID != 11 && this.Yandere.TargetStudent.StudentID != 25 && this.Yandere.TargetStudent.StudentID != 28 && this.Yandere.TargetStudent.StudentID != 30 && this.Yandere.TargetStudent.StudentID != 36 && this.Yandere.TargetStudent.StudentID != 37 && this.Yandere.TargetStudent.StudentID != 38 && this.Yandere.TargetStudent.StudentID != 52 && this.Yandere.TargetStudent.StudentID != 76 && this.Yandere.TargetStudent.StudentID != 77 && this.Yandere.TargetStudent.StudentID != 78 && this.Yandere.TargetStudent.StudentID != 79 && this.Yandere.TargetStudent.StudentID != 80 && this.Yandere.TargetStudent.StudentID != 81)
		{
			flag = true;
		}
		if (flag && TaskGlobals.GetTaskStatus(this.Yandere.TargetStudent.StudentID) == 1 && this.Yandere.Inventory.Book)
		{
			this.Yandere.TargetStudent.TaskPhase = 5;
		}
		if (ClubGlobals.Club == ClubType.Delinquent)
		{
			this.Text[6] = "Intimidate";
			return;
		}
		this.Text[6] = "Ask Favor";
	}

	// Token: 0x06001343 RID: 4931 RVA: 0x000A3B70 File Offset: 0x000A1D70
	public void End()
	{
		if (this.Yandere.TargetStudent != null)
		{
			if (this.Yandere.TargetStudent.Pestered >= 10)
			{
				this.Yandere.TargetStudent.Ignoring = true;
			}
			if (!this.Pestered)
			{
				this.Yandere.Subtitle.Label.text = string.Empty;
			}
			this.Yandere.TargetStudent.Interaction = StudentInteractionType.Idle;
			this.Yandere.TargetStudent.WaitTimer = 1f;
			if (this.Yandere.TargetStudent.enabled)
			{
				this.Yandere.TargetStudent.CurrentDestination = this.Yandere.TargetStudent.Destinations[this.Yandere.TargetStudent.Phase];
				this.Yandere.TargetStudent.Pathfinding.target = this.Yandere.TargetStudent.Destinations[this.Yandere.TargetStudent.Phase];
				if (this.Yandere.TargetStudent.Actions[this.Yandere.TargetStudent.Phase] == StudentActionType.Clean)
				{
					this.Yandere.TargetStudent.EquipCleaningItems();
				}
				if (this.Yandere.TargetStudent.Actions[this.Yandere.TargetStudent.Phase] == StudentActionType.Patrol)
				{
					this.Yandere.TargetStudent.CurrentDestination = this.Yandere.TargetStudent.StudentManager.Patrols.List[this.Yandere.TargetStudent.StudentID].GetChild(this.Yandere.TargetStudent.PatrolID);
					this.Yandere.TargetStudent.Pathfinding.target = this.Yandere.TargetStudent.CurrentDestination;
				}
				if (this.Yandere.TargetStudent.Actions[this.Yandere.TargetStudent.Phase] == StudentActionType.Sleuth)
				{
					this.Yandere.TargetStudent.CurrentDestination = this.Yandere.TargetStudent.SleuthTarget;
					this.Yandere.TargetStudent.Pathfinding.target = this.Yandere.TargetStudent.SleuthTarget;
				}
				if (this.Yandere.TargetStudent.Actions[this.Yandere.TargetStudent.Phase] == StudentActionType.Sunbathe && this.Yandere.TargetStudent.SunbathePhase > 1)
				{
					this.Yandere.TargetStudent.CurrentDestination = this.Yandere.StudentManager.SunbatheSpots[this.Yandere.TargetStudent.StudentID];
					this.Yandere.TargetStudent.Pathfinding.target = this.Yandere.StudentManager.SunbatheSpots[this.Yandere.TargetStudent.StudentID];
				}
			}
			if (this.Yandere.TargetStudent.Persona == PersonaType.PhoneAddict)
			{
				bool flag = false;
				if (this.Yandere.TargetStudent.CurrentAction == StudentActionType.Sunbathe && this.Yandere.TargetStudent.SunbathePhase > 2)
				{
					flag = true;
				}
				if (!this.Yandere.TargetStudent.Scrubber.activeInHierarchy && !flag && !this.Yandere.TargetStudent.Phoneless)
				{
					this.Yandere.TargetStudent.SmartPhone.SetActive(true);
					this.Yandere.TargetStudent.WalkAnim = this.Yandere.TargetStudent.PhoneAnims[1];
				}
				else
				{
					this.Yandere.TargetStudent.SmartPhone.SetActive(false);
				}
			}
			if (this.Yandere.TargetStudent.LostTeacherTrust)
			{
				this.Yandere.TargetStudent.WalkAnim = this.Yandere.TargetStudent.BulliedWalkAnim;
				this.Yandere.TargetStudent.SmartPhone.SetActive(false);
			}
			if (this.Yandere.TargetStudent.EatingSnack)
			{
				this.Yandere.TargetStudent.Scrubber.SetActive(false);
				this.Yandere.TargetStudent.Eraser.SetActive(false);
			}
			if (this.Yandere.TargetStudent.SentToLocker)
			{
				this.Yandere.TargetStudent.CurrentDestination = this.Yandere.TargetStudent.MyLocker;
				this.Yandere.TargetStudent.Pathfinding.target = this.Yandere.TargetStudent.MyLocker;
			}
			this.Yandere.TargetStudent.Talk.NegativeResponse = false;
			this.Yandere.ShoulderCamera.OverShoulder = false;
			this.Yandere.TargetStudent.Waiting = true;
			this.Yandere.TargetStudent = null;
		}
		this.Yandere.StudentManager.VolumeUp();
		this.Jukebox.Dip = 1f;
		this.AskingFavor = false;
		this.Matchmaking = false;
		this.ClubLeader = false;
		this.Pestered = false;
		this.Show = false;
	}

	// Token: 0x040019FA RID: 6650
	public AppearanceWindowScript AppearanceWindow;

	// Token: 0x040019FB RID: 6651
	public PracticeWindowScript PracticeWindow;

	// Token: 0x040019FC RID: 6652
	public ClubManagerScript ClubManager;

	// Token: 0x040019FD RID: 6653
	public LoveManagerScript LoveManager;

	// Token: 0x040019FE RID: 6654
	public PauseScreenScript PauseScreen;

	// Token: 0x040019FF RID: 6655
	public TaskManagerScript TaskManager;

	// Token: 0x04001A00 RID: 6656
	public ClubWindowScript ClubWindow;

	// Token: 0x04001A01 RID: 6657
	public NoteLockerScript NoteLocker;

	// Token: 0x04001A02 RID: 6658
	public ReputationScript Reputation;

	// Token: 0x04001A03 RID: 6659
	public TaskWindowScript TaskWindow;

	// Token: 0x04001A04 RID: 6660
	public PromptBarScript PromptBar;

	// Token: 0x04001A05 RID: 6661
	public JukeboxScript Jukebox;

	// Token: 0x04001A06 RID: 6662
	public YandereScript Yandere;

	// Token: 0x04001A07 RID: 6663
	public ClockScript Clock;

	// Token: 0x04001A08 RID: 6664
	public UIPanel Panel;

	// Token: 0x04001A09 RID: 6665
	public GameObject SwitchTopicsWindow;

	// Token: 0x04001A0A RID: 6666
	public GameObject TaskDialogueWindow;

	// Token: 0x04001A0B RID: 6667
	public GameObject ClubLeaderWindow;

	// Token: 0x04001A0C RID: 6668
	public GameObject DatingMinigame;

	// Token: 0x04001A0D RID: 6669
	public GameObject LockerWindow;

	// Token: 0x04001A0E RID: 6670
	public Transform Interaction;

	// Token: 0x04001A0F RID: 6671
	public Transform Favors;

	// Token: 0x04001A10 RID: 6672
	public Transform Club;

	// Token: 0x04001A11 RID: 6673
	public Transform Love;

	// Token: 0x04001A12 RID: 6674
	public UISprite TaskIcon;

	// Token: 0x04001A13 RID: 6675
	public UISprite Impatience;

	// Token: 0x04001A14 RID: 6676
	public UILabel CenterLabel;

	// Token: 0x04001A15 RID: 6677
	public UISprite[] Segment;

	// Token: 0x04001A16 RID: 6678
	public UISprite[] Shadow;

	// Token: 0x04001A17 RID: 6679
	public string[] Text;

	// Token: 0x04001A18 RID: 6680
	public UISprite[] FavorSegment;

	// Token: 0x04001A19 RID: 6681
	public UISprite[] FavorShadow;

	// Token: 0x04001A1A RID: 6682
	public UISprite[] ClubSegment;

	// Token: 0x04001A1B RID: 6683
	public UISprite[] ClubShadow;

	// Token: 0x04001A1C RID: 6684
	public UISprite[] LoveSegment;

	// Token: 0x04001A1D RID: 6685
	public UISprite[] LoveShadow;

	// Token: 0x04001A1E RID: 6686
	public string[] FavorText;

	// Token: 0x04001A1F RID: 6687
	public string[] ClubText;

	// Token: 0x04001A20 RID: 6688
	public string[] LoveText;

	// Token: 0x04001A21 RID: 6689
	public int Selected;

	// Token: 0x04001A22 RID: 6690
	public int Victim;

	// Token: 0x04001A23 RID: 6691
	public bool AskingFavor;

	// Token: 0x04001A24 RID: 6692
	public bool Matchmaking;

	// Token: 0x04001A25 RID: 6693
	public bool ClubLeader;

	// Token: 0x04001A26 RID: 6694
	public bool Pestered;

	// Token: 0x04001A27 RID: 6695
	public bool Show;

	// Token: 0x04001A28 RID: 6696
	public Vector3 PreviousPosition;

	// Token: 0x04001A29 RID: 6697
	public Vector2 MouseDelta;

	// Token: 0x04001A2A RID: 6698
	public Color OriginalColor;
}
