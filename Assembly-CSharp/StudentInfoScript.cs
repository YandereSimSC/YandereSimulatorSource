using System;
using UnityEngine;

// Token: 0x02000403 RID: 1027
public class StudentInfoScript : MonoBehaviour
{
	// Token: 0x06001B16 RID: 6934 RVA: 0x0011248E File Offset: 0x0011068E
	private void Start()
	{
		StudentGlobals.SetStudentPhotographed(98, true);
		StudentGlobals.SetStudentPhotographed(99, true);
		StudentGlobals.SetStudentPhotographed(100, true);
		this.Topics.SetActive(false);
	}

	// Token: 0x06001B17 RID: 6935 RVA: 0x001124B4 File Offset: 0x001106B4
	public void UpdateInfo(int ID)
	{
		StudentJson studentJson = this.JSON.Students[ID];
		this.NameLabel.text = studentJson.Name;
		string text = string.Concat(studentJson.Class);
		text = text.Insert(1, "-");
		this.ClassLabel.text = "Class " + text;
		if (ID == 90 || ID > 96)
		{
			this.ClassLabel.text = "";
		}
		if (StudentGlobals.GetStudentReputation(ID) < 0)
		{
			this.ReputationLabel.text = StudentGlobals.GetStudentReputation(ID).ToString();
		}
		else if (StudentGlobals.GetStudentReputation(ID) > 0)
		{
			this.ReputationLabel.text = "+" + StudentGlobals.GetStudentReputation(ID).ToString();
		}
		else
		{
			this.ReputationLabel.text = "0";
		}
		this.ReputationBar.localPosition = new Vector3((float)StudentGlobals.GetStudentReputation(ID) * 0.96f, this.ReputationBar.localPosition.y, this.ReputationBar.localPosition.z);
		if (this.ReputationBar.localPosition.x > 96f)
		{
			this.ReputationBar.localPosition = new Vector3(96f, this.ReputationBar.localPosition.y, this.ReputationBar.localPosition.z);
		}
		if (this.ReputationBar.localPosition.x < -96f)
		{
			this.ReputationBar.localPosition = new Vector3(-96f, this.ReputationBar.localPosition.y, this.ReputationBar.localPosition.z);
		}
		this.PersonaLabel.text = Persona.PersonaNames[studentJson.Persona];
		if (studentJson.Persona == PersonaType.Strict && studentJson.Club == ClubType.GymTeacher && !StudentGlobals.GetStudentReplaced(ID))
		{
			this.PersonaLabel.text = "Friendly but Strict";
		}
		if (studentJson.Crush == 0)
		{
			this.CrushLabel.text = "None";
		}
		else if (studentJson.Crush == 99)
		{
			this.CrushLabel.text = "?????";
		}
		else
		{
			this.CrushLabel.text = this.JSON.Students[studentJson.Crush].Name;
		}
		if (studentJson.Club < ClubType.Teacher)
		{
			this.OccupationLabel.text = "Club";
		}
		else
		{
			this.OccupationLabel.text = "Occupation";
		}
		if (studentJson.Club < ClubType.Teacher)
		{
			this.ClubLabel.text = Club.ClubNames[studentJson.Club];
		}
		else
		{
			this.ClubLabel.text = Club.TeacherClubNames[studentJson.Class];
		}
		if (ClubGlobals.GetClubClosed(studentJson.Club))
		{
			this.ClubLabel.text = "No Club";
		}
		this.StrengthLabel.text = StudentInfoScript.StrengthStrings[studentJson.Strength];
		AudioSource component = base.GetComponent<AudioSource>();
		component.enabled = false;
		this.Static.SetActive(false);
		component.volume = 0f;
		component.Stop();
		if (ID < 12 || (ID > 20 && ID < 98))
		{
			WWW www = new WWW(string.Concat(new string[]
			{
				"file:///",
				Application.streamingAssetsPath,
				"/Portraits/Student_",
				ID.ToString(),
				".png"
			}));
			if (!StudentGlobals.GetStudentReplaced(ID))
			{
				this.Portrait.mainTexture = www.texture;
			}
			else
			{
				this.Portrait.mainTexture = this.BlankPortrait;
			}
		}
		else if (ID == 98)
		{
			this.Portrait.mainTexture = this.GuidanceCounselor;
		}
		else if (ID == 99)
		{
			this.Portrait.mainTexture = this.Headmaster;
		}
		else if (ID == 100)
		{
			this.Portrait.mainTexture = this.InfoChan;
			this.Static.SetActive(true);
			if (!this.StudentInfoMenu.Gossiping && !this.StudentInfoMenu.Distracting && !this.StudentInfoMenu.CyberBullying && !this.StudentInfoMenu.CyberStalking)
			{
				component.enabled = true;
				component.volume = 1f;
				component.Play();
			}
		}
		else
		{
			this.Portrait.mainTexture = this.StudentInfoMenu.RivalPortraits[ID];
		}
		this.UpdateAdditionalInfo(ID);
		this.CurrentStudent = ID;
		this.UpdateRepChart();
	}

	// Token: 0x06001B18 RID: 6936 RVA: 0x00112928 File Offset: 0x00110B28
	private void Update()
	{
		if (this.CurrentStudent == 100)
		{
			this.UpdateRepChart();
		}
		if (Input.GetButtonDown("A"))
		{
			if (this.StudentInfoMenu.Gossiping)
			{
				this.StudentInfoMenu.PauseScreen.MainMenu.SetActive(true);
				this.StudentInfoMenu.PauseScreen.Show = false;
				this.DialogueWheel.Victim = this.CurrentStudent;
				this.StudentInfoMenu.Gossiping = false;
				base.gameObject.SetActive(false);
				Time.timeScale = 1f;
				this.PromptBar.ClearButtons();
				this.PromptBar.Show = false;
			}
			else if (this.StudentInfoMenu.Distracting)
			{
				this.StudentInfoMenu.PauseScreen.MainMenu.SetActive(true);
				this.StudentInfoMenu.PauseScreen.Show = false;
				this.DialogueWheel.Victim = this.CurrentStudent;
				this.StudentInfoMenu.Distracting = false;
				base.gameObject.SetActive(false);
				Time.timeScale = 1f;
				this.PromptBar.ClearButtons();
				this.PromptBar.Show = false;
			}
			else if (this.StudentInfoMenu.CyberBullying)
			{
				this.HomeInternet.PostLabels[1].text = this.JSON.Students[this.CurrentStudent].Name;
				this.HomeInternet.Student = this.CurrentStudent;
				this.StudentInfoMenu.PauseScreen.MainMenu.SetActive(true);
				this.StudentInfoMenu.PauseScreen.Show = false;
				this.StudentInfoMenu.CyberBullying = false;
				base.gameObject.SetActive(false);
				this.PromptBar.ClearButtons();
				this.PromptBar.Show = false;
			}
			else if (this.StudentInfoMenu.CyberStalking)
			{
				this.HomeInternet.HomeCamera.CyberstalkWindow.SetActive(true);
				this.HomeInternet.Student = this.CurrentStudent;
				this.StudentInfoMenu.PauseScreen.MainMenu.SetActive(true);
				this.StudentInfoMenu.PauseScreen.Show = false;
				this.StudentInfoMenu.CyberStalking = false;
				base.gameObject.SetActive(false);
				this.PromptBar.ClearButtons();
				this.PromptBar.Show = false;
			}
			else if (this.StudentInfoMenu.MatchMaking)
			{
				this.StudentInfoMenu.PauseScreen.MainMenu.SetActive(true);
				this.StudentInfoMenu.PauseScreen.Show = false;
				this.DialogueWheel.Victim = this.CurrentStudent;
				this.StudentInfoMenu.MatchMaking = false;
				base.gameObject.SetActive(false);
				Time.timeScale = 1f;
				this.PromptBar.ClearButtons();
				this.PromptBar.Show = false;
			}
			else if (this.StudentInfoMenu.Targeting)
			{
				this.StudentInfoMenu.PauseScreen.MainMenu.SetActive(true);
				this.StudentInfoMenu.PauseScreen.Show = false;
				this.Yandere.TargetStudent.HuntTarget = this.StudentManager.Students[this.CurrentStudent];
				this.Yandere.TargetStudent.HuntTarget.Hunted = true;
				this.Yandere.TargetStudent.GoCommitMurder();
				this.Yandere.RPGCamera.enabled = true;
				this.Yandere.TargetStudent = null;
				this.StudentInfoMenu.Targeting = false;
				base.gameObject.SetActive(false);
				Time.timeScale = 1f;
				this.PromptBar.ClearButtons();
				this.PromptBar.Show = false;
			}
			else if (this.StudentInfoMenu.SendingHome)
			{
				if (this.CurrentStudent == 10)
				{
					this.StudentInfoMenu.PauseScreen.ServiceMenu.TextMessageManager.SpawnMessage(10);
					this.Yandere.Inventory.PantyShots += this.Yandere.PauseScreen.ServiceMenu.ServiceCosts[8];
					base.gameObject.SetActive(false);
					this.PromptBar.ClearButtons();
					this.PromptBar.Label[0].text = string.Empty;
					this.PromptBar.Label[1].text = "Back";
					this.PromptBar.UpdateButtons();
				}
				else if (this.StudentManager.Students[this.CurrentStudent].Routine && !this.StudentManager.Students[this.CurrentStudent].InEvent && !this.StudentManager.Students[this.CurrentStudent].TargetedForDistraction && this.StudentManager.Students[this.CurrentStudent].ClubActivityPhase < 16 && !this.StudentManager.Students[this.CurrentStudent].MyBento.Tampered)
				{
					this.StudentManager.Students[this.CurrentStudent].Routine = false;
					this.StudentManager.Students[this.CurrentStudent].SentHome = true;
					this.StudentManager.Students[this.CurrentStudent].CameraReacting = false;
					this.StudentManager.Students[this.CurrentStudent].SpeechLines.Stop();
					this.StudentManager.Students[this.CurrentStudent].EmptyHands();
					this.StudentInfoMenu.PauseScreen.ServiceMenu.gameObject.SetActive(true);
					this.StudentInfoMenu.PauseScreen.ServiceMenu.UpdateList();
					this.StudentInfoMenu.PauseScreen.ServiceMenu.UpdateDesc();
					this.StudentInfoMenu.PauseScreen.ServiceMenu.Purchase();
					this.StudentInfoMenu.SendingHome = false;
					base.gameObject.SetActive(false);
					this.PromptBar.ClearButtons();
					this.PromptBar.Show = false;
				}
				else
				{
					this.StudentInfoMenu.PauseScreen.ServiceMenu.TextMessageManager.SpawnMessage(0);
					base.gameObject.SetActive(false);
					this.PromptBar.ClearButtons();
					this.PromptBar.Label[0].text = string.Empty;
					this.PromptBar.Label[1].text = "Back";
					this.PromptBar.UpdateButtons();
				}
			}
			else if (this.StudentInfoMenu.FindingLocker)
			{
				this.NoteLocker.gameObject.SetActive(true);
				this.NoteLocker.transform.position = this.StudentManager.Students[this.StudentInfoMenu.StudentID].MyLocker.position;
				this.NoteLocker.transform.position += new Vector3(0f, 1.355f, 0f);
				this.NoteLocker.transform.position += this.StudentManager.Students[this.StudentInfoMenu.StudentID].MyLocker.forward * 0.33333f;
				this.NoteLocker.Prompt.Label[0].text = "     Leave note for " + this.StudentManager.Students[this.StudentInfoMenu.StudentID].Name;
				this.NoteLocker.Student = this.StudentManager.Students[this.StudentInfoMenu.StudentID];
				this.NoteLocker.LockerOwner = this.StudentInfoMenu.StudentID;
				this.NoteLocker.Prompt.enabled = true;
				this.NoteLocker.transform.GetChild(0).gameObject.SetActive(true);
				this.NoteLocker.CheckingNote = false;
				this.NoteLocker.CanLeaveNote = true;
				this.NoteLocker.SpawnedNote = false;
				this.NoteLocker.NoteLeft = false;
				this.NoteLocker.Success = false;
				this.NoteLocker.Timer = 0f;
				this.StudentInfoMenu.PauseScreen.MainMenu.SetActive(true);
				this.StudentInfoMenu.PauseScreen.Show = false;
				this.StudentInfoMenu.FindingLocker = false;
				base.gameObject.SetActive(false);
				this.PromptBar.ClearButtons();
				this.PromptBar.Show = false;
				this.Yandere.RPGCamera.enabled = true;
				Time.timeScale = 1f;
			}
		}
		if (Input.GetButtonDown("B"))
		{
			this.ShowRep = false;
			this.Topics.SetActive(false);
			base.GetComponent<AudioSource>().Stop();
			this.ReputationChart.transform.localScale = new Vector3(0f, 0f, 0f);
			if (this.Shutter != null)
			{
				if (!this.Shutter.PhotoIcons.activeInHierarchy)
				{
					this.Back = true;
				}
			}
			else
			{
				this.Back = true;
			}
			if (this.Back)
			{
				this.StudentInfoMenu.gameObject.SetActive(true);
				base.gameObject.SetActive(false);
				this.PromptBar.ClearButtons();
				this.PromptBar.Label[0].text = "View Info";
				if (!this.StudentInfoMenu.Gossiping)
				{
					this.PromptBar.Label[1].text = "Back";
				}
				this.PromptBar.UpdateButtons();
				this.Back = false;
			}
		}
		if (Input.GetButtonDown("X") && this.PromptBar.Button[2].enabled)
		{
			if (this.StudentManager.Tag.Target != this.StudentManager.Students[this.CurrentStudent].Head)
			{
				this.StudentManager.Tag.Target = this.StudentManager.Students[this.CurrentStudent].Head;
				this.PromptBar.Label[2].text = "Untag";
			}
			else
			{
				this.StudentManager.Tag.Target = null;
				this.PromptBar.Label[2].text = "Tag";
			}
		}
		if (Input.GetButtonDown("Y") && this.PromptBar.Button[3].enabled)
		{
			if (!this.Topics.activeInHierarchy)
			{
				this.PromptBar.Label[3].text = "Basic Info";
				this.PromptBar.UpdateButtons();
				this.Topics.SetActive(true);
				this.UpdateTopics();
			}
			else
			{
				this.PromptBar.Label[3].text = "Interests";
				this.PromptBar.UpdateButtons();
				this.Topics.SetActive(false);
			}
		}
		if (Input.GetButtonDown("LB"))
		{
			this.UpdateRepChart();
			this.ShowRep = !this.ShowRep;
		}
		if (Input.GetKeyDown(KeyCode.Equals))
		{
			StudentGlobals.SetStudentReputation(this.CurrentStudent, StudentGlobals.GetStudentReputation(this.CurrentStudent) + 10);
			this.UpdateInfo(this.CurrentStudent);
		}
		if (Input.GetKeyDown(KeyCode.Minus))
		{
			StudentGlobals.SetStudentReputation(this.CurrentStudent, StudentGlobals.GetStudentReputation(this.CurrentStudent) - 10);
			this.UpdateInfo(this.CurrentStudent);
		}
		StudentInfoMenuScript studentInfoMenu = this.StudentInfoMenu;
		if (!studentInfoMenu.CyberBullying && !studentInfoMenu.CyberStalking && !studentInfoMenu.FindingLocker && !studentInfoMenu.UsingLifeNote && !studentInfoMenu.GettingInfo && !studentInfoMenu.MatchMaking && !studentInfoMenu.Distracting && !studentInfoMenu.SendingHome && !studentInfoMenu.Gossiping && !studentInfoMenu.Targeting && !studentInfoMenu.Dead)
		{
			if (this.StudentInfoMenu.PauseScreen.InputManager.TappedRight)
			{
				this.CurrentStudent++;
				if (this.CurrentStudent > 100)
				{
					this.CurrentStudent = 1;
				}
				while (!StudentGlobals.GetStudentPhotographed(this.CurrentStudent))
				{
					this.CurrentStudent++;
					if (this.CurrentStudent > 100)
					{
						this.CurrentStudent = 1;
					}
				}
				this.UpdateInfo(this.CurrentStudent);
			}
			if (this.StudentInfoMenu.PauseScreen.InputManager.TappedLeft)
			{
				this.CurrentStudent--;
				if (this.CurrentStudent < 1)
				{
					this.CurrentStudent = 100;
				}
				while (!StudentGlobals.GetStudentPhotographed(this.CurrentStudent))
				{
					this.CurrentStudent--;
					if (this.CurrentStudent < 1)
					{
						this.CurrentStudent = 100;
					}
				}
				this.UpdateInfo(this.CurrentStudent);
			}
		}
		if (this.ShowRep)
		{
			this.ReputationChart.transform.localScale = Vector3.Lerp(this.ReputationChart.transform.localScale, new Vector3(138f, 138f, 138f), Time.unscaledDeltaTime * 10f);
			return;
		}
		this.ReputationChart.transform.localScale = Vector3.Lerp(this.ReputationChart.transform.localScale, new Vector3(0f, 0f, 0f), Time.unscaledDeltaTime * 10f);
	}

	// Token: 0x06001B19 RID: 6937 RVA: 0x00113698 File Offset: 0x00111898
	private void UpdateAdditionalInfo(int ID)
	{
		Debug.Log("EventGlobals.Event1 is: " + EventGlobals.Event1.ToString());
		if (ID == 11)
		{
			this.Strings[1] = (EventGlobals.OsanaEvent1 ? "May be a victim of blackmail" : "?????");
			this.Strings[2] = (EventGlobals.OsanaEvent2 ? "Has a stalker." : "?????");
			this.InfoLabel.text = this.Strings[1] + "\n\n" + this.Strings[2];
			return;
		}
		if (ID == 30)
		{
			this.Strings[1] = (EventGlobals.Event1 ? "May be a victim of domestic abuse." : "?????");
			this.Strings[2] = (EventGlobals.Event2 ? "May be engaging in compensated dating in Shisuta Town." : "?????");
			this.InfoLabel.text = this.Strings[1] + "\n\n" + this.Strings[2];
			return;
		}
		if (ID == 51)
		{
			if (ClubGlobals.GetClubClosed(ClubType.LightMusic))
			{
				this.InfoLabel.text = "Disbanded the Light Music Club, dyed her hair back to its original color, removed her piercings, and stopped socializing with others.";
				return;
			}
			this.InfoLabel.text = this.JSON.Students[ID].Info;
			return;
		}
		else
		{
			if (StudentGlobals.GetStudentReplaced(ID))
			{
				this.InfoLabel.text = "No additional information is available at this time.";
				return;
			}
			if (this.JSON.Students[ID].Info == string.Empty)
			{
				this.InfoLabel.text = "No additional information is available at this time.";
				return;
			}
			this.InfoLabel.text = this.JSON.Students[ID].Info;
			return;
		}
	}

	// Token: 0x06001B1A RID: 6938 RVA: 0x00113824 File Offset: 0x00111A24
	private void UpdateTopics()
	{
		for (int i = 1; i < this.TopicIcons.Length; i++)
		{
			this.TopicIcons[i].spriteName = ((!ConversationGlobals.GetTopicDiscovered(i)) ? 0 : i).ToString();
		}
		for (int j = 1; j <= 25; j++)
		{
			UISprite uisprite = this.TopicOpinionIcons[j];
			if (!ConversationGlobals.GetTopicLearnedByStudent(j, this.CurrentStudent))
			{
				uisprite.spriteName = "Unknown";
			}
			else
			{
				int[] topics = this.JSON.Topics[this.CurrentStudent].Topics;
				uisprite.spriteName = this.OpinionSpriteNames[topics[j]];
			}
		}
	}

	// Token: 0x06001B1B RID: 6939 RVA: 0x001138C0 File Offset: 0x00111AC0
	private void UpdateRepChart()
	{
		Vector3 reputationTriangle;
		if (this.CurrentStudent < 100)
		{
			reputationTriangle = StudentGlobals.GetReputationTriangle(this.CurrentStudent);
		}
		else
		{
			reputationTriangle = new Vector3((float)UnityEngine.Random.Range(-100, 101), (float)UnityEngine.Random.Range(-100, 101), (float)UnityEngine.Random.Range(-100, 101));
		}
		this.ReputationChart.fields[0].Value = reputationTriangle.x;
		this.ReputationChart.fields[1].Value = reputationTriangle.y;
		this.ReputationChart.fields[2].Value = reputationTriangle.z;
	}

	// Token: 0x04002C6A RID: 11370
	public StudentInfoMenuScript StudentInfoMenu;

	// Token: 0x04002C6B RID: 11371
	public StudentManagerScript StudentManager;

	// Token: 0x04002C6C RID: 11372
	public DialogueWheelScript DialogueWheel;

	// Token: 0x04002C6D RID: 11373
	public HomeInternetScript HomeInternet;

	// Token: 0x04002C6E RID: 11374
	public TopicManagerScript TopicManager;

	// Token: 0x04002C6F RID: 11375
	public NoteLockerScript NoteLocker;

	// Token: 0x04002C70 RID: 11376
	public RadarChart ReputationChart;

	// Token: 0x04002C71 RID: 11377
	public PromptBarScript PromptBar;

	// Token: 0x04002C72 RID: 11378
	public ShutterScript Shutter;

	// Token: 0x04002C73 RID: 11379
	public YandereScript Yandere;

	// Token: 0x04002C74 RID: 11380
	public JsonScript JSON;

	// Token: 0x04002C75 RID: 11381
	public Texture GuidanceCounselor;

	// Token: 0x04002C76 RID: 11382
	public Texture DefaultPortrait;

	// Token: 0x04002C77 RID: 11383
	public Texture BlankPortrait;

	// Token: 0x04002C78 RID: 11384
	public Texture Headmaster;

	// Token: 0x04002C79 RID: 11385
	public Texture InfoChan;

	// Token: 0x04002C7A RID: 11386
	public Transform ReputationBar;

	// Token: 0x04002C7B RID: 11387
	public GameObject Static;

	// Token: 0x04002C7C RID: 11388
	public GameObject Topics;

	// Token: 0x04002C7D RID: 11389
	public UILabel OccupationLabel;

	// Token: 0x04002C7E RID: 11390
	public UILabel ReputationLabel;

	// Token: 0x04002C7F RID: 11391
	public UILabel StrengthLabel;

	// Token: 0x04002C80 RID: 11392
	public UILabel PersonaLabel;

	// Token: 0x04002C81 RID: 11393
	public UILabel ClassLabel;

	// Token: 0x04002C82 RID: 11394
	public UILabel CrushLabel;

	// Token: 0x04002C83 RID: 11395
	public UILabel ClubLabel;

	// Token: 0x04002C84 RID: 11396
	public UILabel InfoLabel;

	// Token: 0x04002C85 RID: 11397
	public UILabel NameLabel;

	// Token: 0x04002C86 RID: 11398
	public UITexture Portrait;

	// Token: 0x04002C87 RID: 11399
	public string[] OpinionSpriteNames;

	// Token: 0x04002C88 RID: 11400
	public string[] Strings;

	// Token: 0x04002C89 RID: 11401
	public int CurrentStudent;

	// Token: 0x04002C8A RID: 11402
	public bool ShowRep;

	// Token: 0x04002C8B RID: 11403
	public bool Back;

	// Token: 0x04002C8C RID: 11404
	public UISprite[] TopicIcons;

	// Token: 0x04002C8D RID: 11405
	public UISprite[] TopicOpinionIcons;

	// Token: 0x04002C8E RID: 11406
	private static readonly IntAndStringDictionary StrengthStrings = new IntAndStringDictionary
	{
		{
			0,
			"Incapable"
		},
		{
			1,
			"Very Weak"
		},
		{
			2,
			"Weak"
		},
		{
			3,
			"Strong"
		},
		{
			4,
			"Very Strong"
		},
		{
			5,
			"Peak Physical Strength"
		},
		{
			6,
			"Extensive Training"
		},
		{
			7,
			"Carries Pepper Spray"
		},
		{
			8,
			"Armed"
		},
		{
			9,
			"Invincible"
		},
		{
			99,
			"?????"
		}
	};
}
