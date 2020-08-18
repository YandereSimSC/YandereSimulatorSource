using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace YandereSimulator.Yancord
{
	// Token: 0x020004A6 RID: 1190
	public class YancordManager : MonoBehaviour
	{
		// Token: 0x06001E35 RID: 7733 RVA: 0x001792E8 File Offset: 0x001774E8
		public void Start()
		{
			if (!YancordGlobals.JoinedYancord)
			{
				Debug.Log("This is the player's first time launching Yancord.");
				YancordGlobals.CurrentConversation = 1;
				Debug.Log("YancordGlobals.CurrentConversation is: " + YancordGlobals.CurrentConversation);
				if (this.ConversationID != YancordGlobals.CurrentConversation)
				{
					base.enabled = false;
				}
				this.ChatLabel.text = string.Empty;
				this.Dialogue[1].isSystemMessage = true;
				this.Dialogue[1].Message = "Ayano Aishi has joined the Moonlit Warrior Selene Fanserver.";
				this.FirstTimeUI.gameObject.SetActive(true);
			}
			else
			{
				Debug.Log("The player has launched Yancord before.");
				if (this.ConversationID != YancordGlobals.CurrentConversation)
				{
					base.enabled = false;
				}
				this.JoinServer();
				this.Dialogue[1].isSystemMessage = true;
				this.Dialogue[1].Message = "Ayano Aishi has logged in.";
				this.PartnerOnline.SetActive(true);
				this.BlueDiscordIcon.alpha = 0f;
				this.ChatLabel.text = "Press E to start chatting on the Moonlit Warrior Selene Fanserver!";
			}
			this.CurrentPartner.CurrentStatus = Status.Online;
			this.SpawnAll();
			this.Choice = new int[this.Dialogue.Count];
			this.Darkness.color = new Color(0f, 0f, 0f, 1f);
			this.FadeIn = true;
		}

		// Token: 0x06001E36 RID: 7734 RVA: 0x00179454 File Offset: 0x00177654
		public void Update()
		{
			if (this.FadeIn)
			{
				float num = this.Darkness.color.a;
				num = Mathf.MoveTowards(num, 0f, Time.deltaTime);
				this.Darkness.color = new Color(0f, 0f, 0f, num);
				if (this.Darkness.color.a == 0f)
				{
					this.FadeIn = false;
				}
			}
			else if (this.FadeOut)
			{
				float num2 = this.Darkness.color.a;
				num2 = Mathf.MoveTowards(num2, 1f, Time.deltaTime);
				this.Darkness.color = new Color(0f, 0f, 0f, num2);
				if (this.Darkness.color.a == 1f)
				{
					SceneManager.LoadScene("HomeScene");
					DateGlobals.DayPassed = false;
				}
			}
			else if (this.Chatting)
			{
				if (this.currentPhase < this.Dialogue.Count)
				{
					this.CalculateMessageDelay();
					if (this.Dialogue[this.currentPhase].isQuestion)
					{
						if (!this.ShowingDialogueOption)
						{
							this.timer += Time.deltaTime;
							if (string.IsNullOrEmpty(this.ChatLabel.text))
							{
								this.ChatLabel.text = this.CurrentPartner.FirstName + " is typing...";
							}
							if (this.timer > this.messageDelay)
							{
								this.ChatLabel.text = string.Empty;
								this.Messages[this.currentPhase].MyProfile = this.CurrentPartner;
								this.SpawnChatMessage();
								this.timer = 0f;
								this.ShowingDialogueOption = true;
							}
						}
					}
					else if (this.Dialogue[this.currentPhase].isSystemMessage)
					{
						this.timer += Time.deltaTime;
						if (this.timer > this.SystemMessageDelay)
						{
							this.ChatLabel.text = string.Empty;
							this.SpawnChatMessage();
							this.Messages[this.currentPhase].MyProfile = this.SystemProfile;
							this.timer = 0f;
							this.currentPhase++;
						}
					}
					else if (this.currentPhase < this.Dialogue.Count)
					{
						if (this.Dialogue[this.currentPhase].sentByPlayer)
						{
							this.Messages[this.currentPhase].MyProfile = this.MyProfile;
							this.SpawnChatMessage();
							this.currentPhase++;
						}
						else
						{
							this.timer += Time.deltaTime;
							if (string.IsNullOrEmpty(this.ChatLabel.text))
							{
								this.ChatLabel.text = this.CurrentPartner.FirstName + " is typing...";
							}
							if (this.timer > this.messageDelay)
							{
								this.ChatLabel.text = string.Empty;
								this.SpawnChatMessage();
								this.Messages[this.currentPhase].MyProfile = this.CurrentPartner;
								this.timer = 0f;
								this.currentPhase++;
							}
						}
					}
					else
					{
						this.currentPhase++;
					}
					if (Input.GetKeyDown(KeyCode.E))
					{
						this.timer = this.messageDelay;
					}
				}
				else
				{
					if (string.IsNullOrEmpty(this.ChatLabel.text))
					{
						this.ChatLabel.text = "Press E to log out of Yancord.";
						this.CurrentPartner.CurrentStatus = Status.Invisible;
						this.PartnerOnline.SetActive(false);
						this.PartnerOffline.SetActive(true);
					}
					if (Input.GetKeyDown(KeyCode.E))
					{
						Debug.Log("Quitting!");
						YancordGlobals.CurrentConversation++;
						this.FadeOut = true;
					}
				}
				if (this.ShowingDialogueOption)
				{
					if (Input.GetKeyDown(KeyCode.E) && !this.DialogueChooseMenu.activeInHierarchy)
					{
						this.ChatLabel.text = "Choose one of the following answers to respond.";
						this.DialogueChooseMenu.SetActive(true);
						this.DialogueChooseLabel[1].text = this.Dialogue[this.currentPhase].OptionQ;
						this.DialogueChooseLabel[2].text = this.Dialogue[this.currentPhase].OptionR;
						this.DialogueChooseLabel[3].text = this.Dialogue[this.currentPhase].OptionF;
						this.DialogueQuestion.MyProfile = this.CurrentPartner;
						this.DialogueQuestion.MessageLabel.text = this.Dialogue[this.currentPhase].Message;
						this.DialogueQuestion.Awake();
					}
					if (this.DialogueChooseMenu.activeInHierarchy)
					{
						if (Input.GetKeyDown(KeyCode.Q))
						{
							this.Choice[this.currentPhase] = 1;
						}
						else if (Input.GetKeyDown(KeyCode.R))
						{
							this.Choice[this.currentPhase] = 2;
						}
						else if (Input.GetKeyDown(KeyCode.F))
						{
							this.Choice[this.currentPhase] = 3;
						}
						if (this.Choice[this.currentPhase] != 0)
						{
							this.Dialogue[this.currentPhase + 1].Message = this.GetAnswer(this.currentPhase);
							this.Dialogue[this.currentPhase + 2].Message = this.GetReaction(this.currentPhase);
							this.Dialogue[this.currentPhase + 1].sentByPlayer = true;
							this.DialogueChooseMenu.SetActive(false);
							this.ChatLabel.text = "";
							this.ShowingDialogueOption = false;
							this.timer = 0f;
							this.currentPhase++;
						}
					}
					else if (string.IsNullOrEmpty(this.ChatLabel.text))
					{
						this.ChatLabel.text = "Press E to respond.";
					}
				}
				if (this.BlueDiscordIcon.alpha >= 0f)
				{
					this.BlueDiscordIcon.alpha -= Time.deltaTime * 10f;
				}
			}
			else if (!YancordGlobals.JoinedYancord)
			{
				if (Input.GetKeyDown(KeyCode.E))
				{
					YancordGlobals.JoinedYancord = true;
					this.JoinServer();
					this.SpawnChatMessage();
					this.PartnerOnline.SetActive(true);
					this.Chatting = true;
				}
				else if (Input.GetKeyDown(KeyCode.Q))
				{
				}
			}
			else if (Input.GetKeyDown(KeyCode.E))
			{
				this.ChatLabel.text = string.Empty;
				this.SpawnChatMessage();
				this.Chatting = true;
			}
			else
			{
				Input.GetKeyDown(KeyCode.Q);
			}
			if (Input.GetKeyDown(KeyCode.Space))
			{
				YancordGlobals.JoinedYancord = false;
			}
			if (Input.GetKeyDown(KeyCode.Alpha1))
			{
				YancordGlobals.CurrentConversation = 1;
			}
			if (Input.GetKeyDown(KeyCode.Alpha2))
			{
				YancordGlobals.CurrentConversation = 2;
			}
			if (Input.GetKeyDown(KeyCode.Alpha3))
			{
				YancordGlobals.CurrentConversation = 3;
			}
			if (Input.GetKeyDown(KeyCode.Alpha4))
			{
				YancordGlobals.CurrentConversation = 4;
			}
			if (Input.GetKeyDown(KeyCode.Alpha5))
			{
				YancordGlobals.CurrentConversation = 5;
			}
		}

		// Token: 0x06001E37 RID: 7735 RVA: 0x00179B70 File Offset: 0x00177D70
		private string GetReaction(int phase)
		{
			switch (this.Choice[phase])
			{
			case 1:
				return this.Dialogue[phase].ReactionQ;
			case 2:
				return this.Dialogue[phase].ReactionR;
			case 3:
				return this.Dialogue[phase].ReactionF;
			default:
				return null;
			}
		}

		// Token: 0x06001E38 RID: 7736 RVA: 0x00179BD4 File Offset: 0x00177DD4
		private string GetAnswer(int phase)
		{
			switch (this.Choice[phase])
			{
			case 1:
				return this.Dialogue[phase].OptionQ;
			case 2:
				return this.Dialogue[phase].OptionR;
			case 3:
				return this.Dialogue[phase].OptionF;
			default:
				return null;
			}
		}

		// Token: 0x06001E39 RID: 7737 RVA: 0x00179C38 File Offset: 0x00177E38
		private void SpawnAll()
		{
			for (int i = 1; i < this.Dialogue.Count; i++)
			{
				MessageScript item = UnityEngine.Object.Instantiate<MessageScript>(this.MessagePrefab, new Vector3(0f, this.Messages[i - 1].transform.position.y - ((float)this.Messages[i - 1].MessageLabel.height * 0.0016723945f + this.Distance * 0.0016723945f), 0f), Quaternion.identity, this.ConversationParent);
				this.Messages.Add(item);
				this.Messages[i].MessageLabel.text = this.Dialogue[i].Message;
				if (this.Dialogue[i].isQuestion)
				{
					this.Dialogue[i + 1].sentByPlayer = true;
				}
				if (this.Dialogue[i].isSystemMessage)
				{
					this.Messages[i].MyProfile = this.SystemProfile;
				}
				else if (this.Dialogue[i].sentByPlayer)
				{
					this.Messages[i].MyProfile = this.MyProfile;
				}
				else
				{
					this.Messages[i].MyProfile = this.CurrentPartner;
				}
				this.Messages[i].Awake();
				this.Messages[i].gameObject.SetActive(false);
			}
		}

		// Token: 0x06001E3A RID: 7738 RVA: 0x00179DC4 File Offset: 0x00177FC4
		private void SpawnChatMessage()
		{
			if (this.Messages[this.currentPhase].transform.position.y < -400f || this.Messages[this.currentPhase].transform.localPosition.y - (float)this.Messages[this.currentPhase].MessageLabel.height < -400f)
			{
				if (!this.Messages[this.currentPhase].gameObject.activeInHierarchy)
				{
					this.Messages[this.currentPhase].gameObject.SetActive(true);
					this.Messages[this.currentPhase].MessageLabel.text = this.Dialogue[this.currentPhase].Message;
					float num = -400f + (float)this.Messages[this.currentPhase].MessageLabel.height - 10f;
					Vector3 position = this.Messages[this.currentPhase].transform.position;
					this.Messages[this.currentPhase].transform.position = new Vector3(0f, num * 0.0016723945f, 0f);
					for (int i = this.currentPhase - 1; i >= 0; i--)
					{
						this.Messages[i].transform.position = new Vector3(0f, this.Messages[i + 1].transform.position.y + ((float)this.Messages[i].MessageLabel.height * 0.0016723945f + this.Distance * 0.0016723945f), 0f);
					}
					for (int j = 1; j < this.Messages.Count; j++)
					{
						this.Messages[j].transform.position = new Vector3(0f, this.Messages[j - 1].transform.position.y - ((float)this.Messages[j - 1].MessageLabel.height * 0.0016723945f + this.Distance * 0.0016723945f), 0f);
					}
					return;
				}
			}
			else if (!this.Messages[this.currentPhase].gameObject.activeInHierarchy)
			{
				this.Messages[this.currentPhase].gameObject.SetActive(true);
				this.Messages[this.currentPhase].MessageLabel.text = this.Dialogue[this.currentPhase].Message;
				for (int k = this.currentPhase; k < this.Messages.Count; k++)
				{
					this.Messages[k].transform.position = new Vector3(0f, this.Messages[k - 1].transform.position.y - ((float)this.Messages[k - 1].MessageLabel.height * 0.0016723945f + this.Distance * 0.0016723945f), 0f);
				}
			}
		}

		// Token: 0x06001E3B RID: 7739 RVA: 0x0017A128 File Offset: 0x00178328
		private void JoinServer()
		{
			this.NewServer.SetActive(true);
			this.SelectedServer.gameObject.SetActive(true);
			this.SelectedServer.position = new Vector3(this.SelectedServer.position.x, this.NewServer.transform.position.y, this.SelectedServer.position.z);
			this.CreateNewServer.position = new Vector3(this.CreateNewServer.position.x, 0.37407407f, this.CreateNewServer.position.z);
			this.DirectMessages.SetActive(false);
			this.FindLabel.SetActive(false);
			this.ServerRelated.SetActive(true);
			this.FirstTimeUI.gameObject.SetActive(false);
		}

		// Token: 0x06001E3C RID: 7740 RVA: 0x0017A201 File Offset: 0x00178401
		private void CalculateMessageDelay()
		{
			this.messageDelay = 3f;
		}

		// Token: 0x04003C20 RID: 15392
		[Header("== Conversation related ==")]
		[Range(1f, 50f)]
		public int ConversationID = 1;

		// Token: 0x04003C21 RID: 15393
		[Header("== Chatpartner related ==")]
		public Profile CurrentPartner;

		// Token: 0x04003C22 RID: 15394
		public Profile MyProfile;

		// Token: 0x04003C23 RID: 15395
		public Profile SystemProfile;

		// Token: 0x04003C24 RID: 15396
		[Space(20f)]
		[Header("== Chat related ==")]
		public MessageScript MessagePrefab;

		// Token: 0x04003C25 RID: 15397
		public List<MessageScript> Messages = new List<MessageScript>();

		// Token: 0x04003C26 RID: 15398
		public List<NewTextMessage> Dialogue = new List<NewTextMessage>();

		// Token: 0x04003C27 RID: 15399
		public Transform ConversationParent;

		// Token: 0x04003C28 RID: 15400
		private int[] Choice;

		// Token: 0x04003C29 RID: 15401
		public int currentPhase = 1;

		// Token: 0x04003C2A RID: 15402
		public float Distance;

		// Token: 0x04003C2B RID: 15403
		[Space(20f)]
		public UILabel ChatLabel;

		// Token: 0x04003C2C RID: 15404
		[Header("== Dialogue Menu related ==")]
		public UILabel[] DialogueChooseLabel;

		// Token: 0x04003C2D RID: 15405
		public GameObject DialogueChooseMenu;

		// Token: 0x04003C2E RID: 15406
		public MessageScript DialogueQuestion;

		// Token: 0x04003C2F RID: 15407
		[Header("== Server related ==")]
		public GameObject NewServer;

		// Token: 0x04003C30 RID: 15408
		public Transform SelectedServer;

		// Token: 0x04003C31 RID: 15409
		public Transform CreateNewServer;

		// Token: 0x04003C32 RID: 15410
		public GameObject ServerRelated;

		// Token: 0x04003C33 RID: 15411
		public GameObject PartnerOffline;

		// Token: 0x04003C34 RID: 15412
		public GameObject PartnerOnline;

		// Token: 0x04003C35 RID: 15413
		[Space(20f)]
		public UITexture BlueDiscordIcon;

		// Token: 0x04003C36 RID: 15414
		public GameObject DirectMessages;

		// Token: 0x04003C37 RID: 15415
		public GameObject FindLabel;

		// Token: 0x04003C38 RID: 15416
		public Transform FirstTimeUI;

		// Token: 0x04003C39 RID: 15417
		[SerializeField]
		private bool IsDebug;

		// Token: 0x04003C3A RID: 15418
		[Header("== Delay related ==")]
		public float SystemMessageDelay = 3f;

		// Token: 0x04003C3B RID: 15419
		public float LetterPerSecond = 0.05f;

		// Token: 0x04003C3C RID: 15420
		public float messageDelay;

		// Token: 0x04003C3D RID: 15421
		private bool Chatting;

		// Token: 0x04003C3E RID: 15422
		private bool ShowingDialogueOption;

		// Token: 0x04003C3F RID: 15423
		private bool FadeOut;

		// Token: 0x04003C40 RID: 15424
		private bool FadeIn;

		// Token: 0x04003C41 RID: 15425
		public UITexture Darkness;

		// Token: 0x04003C42 RID: 15426
		public float timer;

		// Token: 0x04003C43 RID: 15427
		private bool shouldScroll;
	}
}
