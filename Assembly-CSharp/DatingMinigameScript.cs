using System;
using UnityEngine;

// Token: 0x02000259 RID: 601
public class DatingMinigameScript : MonoBehaviour
{
	// Token: 0x060012F9 RID: 4857 RVA: 0x000991A4 File Offset: 0x000973A4
	private void Start()
	{
		this.MainCamera = Camera.main;
		this.Affection = DatingGlobals.Affection;
		this.AffectionBar.localScale = new Vector3(this.Affection / 100f, this.AffectionBar.localScale.y, this.AffectionBar.localScale.z);
		this.CalculateAffection();
		this.OriginalColor = this.ComplimentBGs[1].color;
		this.ComplimentSet.localScale = Vector3.zero;
		this.GiveGift.localScale = Vector3.zero;
		this.ShowOff.localScale = Vector3.zero;
		this.Topics.localScale = Vector3.zero;
		this.DatingSimHUD.gameObject.SetActive(false);
		this.DatingSimHUD.alpha = 0f;
		for (int i = 1; i < 26; i++)
		{
			if (DatingGlobals.GetTopicDiscussed(i))
			{
				UISprite uisprite = this.TopicIcons[i];
				uisprite.color = new Color(uisprite.color.r, uisprite.color.g, uisprite.color.b, 0.5f);
			}
		}
		for (int j = 1; j < 11; j++)
		{
			if (DatingGlobals.GetComplimentGiven(j))
			{
				UILabel uilabel = this.ComplimentLabels[j];
				uilabel.color = new Color(uilabel.color.r, uilabel.color.g, uilabel.color.b, 0.5f);
			}
		}
		this.UpdateComplimentHighlight();
		this.UpdateTraitHighlight();
		this.UpdateGiftHighlight();
	}

	// Token: 0x060012FA RID: 4858 RVA: 0x00099330 File Offset: 0x00097530
	private void CalculateAffection()
	{
		if (this.Affection > 100f)
		{
			this.Affection = 100f;
		}
		if (this.Affection == 0f)
		{
			this.AffectionLevel = 0;
		}
		else if (this.Affection < 25f)
		{
			this.AffectionLevel = 1;
		}
		else if (this.Affection < 50f)
		{
			this.AffectionLevel = 2;
		}
		else if (this.Affection < 75f)
		{
			this.AffectionLevel = 3;
		}
		else if (this.Affection < 100f)
		{
			this.AffectionLevel = 4;
		}
		else
		{
			this.AffectionLevel = 5;
		}
		Debug.Log("Affection is: " + this.Affection);
		Debug.Log("AffectionLevel is now: " + this.AffectionLevel);
	}

	// Token: 0x060012FB RID: 4859 RVA: 0x00099400 File Offset: 0x00097600
	private void Update()
	{
		if (this.Testing)
		{
			this.Prompt.enabled = true;
		}
		else if (this.LoveManager.RivalWaiting)
		{
			if (this.Rival == null)
			{
				this.Suitor = this.StudentManager.Students[this.LoveManager.SuitorID];
				this.Rival = this.StudentManager.Students[this.LoveManager.RivalID];
			}
			if (this.Rival.MeetTimer > 0f && this.Suitor.MeetTimer > 0f)
			{
				this.Prompt.enabled = true;
			}
		}
		else if (this.Prompt.enabled)
		{
			this.Prompt.Hide();
			this.Prompt.enabled = false;
		}
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Circle[0].fillAmount = 1f;
			if (!this.Yandere.Chased && this.Yandere.Chasers == 0 && !this.Rival.Hunted)
			{
				this.Suitor.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
				this.Rival.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
				this.Suitor.CharacterAnimation.enabled = true;
				this.Rival.CharacterAnimation.enabled = true;
				this.Suitor.enabled = false;
				this.Rival.enabled = false;
				this.Rival.CharacterAnimation["f02_smile_00"].layer = 1;
				this.Rival.CharacterAnimation.Play("f02_smile_00");
				this.Rival.CharacterAnimation["f02_smile_00"].weight = 0f;
				this.StudentManager.Clock.StopTime = true;
				this.Yandere.RPGCamera.enabled = false;
				this.HeartbeatCamera.SetActive(false);
				this.Yandere.Headset.SetActive(true);
				this.Yandere.CanMove = false;
				this.Yandere.EmptyHands();
				if (this.Yandere.YandereVision)
				{
					this.Yandere.ResetYandereEffects();
					this.Yandere.YandereVision = false;
				}
				this.Yandere.transform.position = this.PeekSpot.position;
				this.Yandere.transform.eulerAngles = this.PeekSpot.eulerAngles;
				this.Yandere.CharacterAnimation.Play("f02_treePeeking_00");
				this.MainCamera.transform.position = new Vector3(48f, 3f, -44f);
				this.MainCamera.transform.eulerAngles = new Vector3(15f, 90f, 0f);
				this.WisdomLabel.text = "Wisdom: " + DatingGlobals.GetSuitorTrait(2).ToString();
				this.GiftIcons[1].enabled = CollectibleGlobals.GetGiftPurchased(6);
				this.GiftIcons[2].enabled = CollectibleGlobals.GetGiftPurchased(7);
				this.GiftIcons[3].enabled = CollectibleGlobals.GetGiftPurchased(8);
				this.GiftIcons[4].enabled = CollectibleGlobals.GetGiftPurchased(9);
				this.Matchmaking = true;
				this.UpdateTopics();
				Time.timeScale = 1f;
			}
		}
		if (this.Matchmaking)
		{
			if (this.CurrentAnim != string.Empty && this.Rival.CharacterAnimation[this.CurrentAnim].time >= this.Rival.CharacterAnimation[this.CurrentAnim].length)
			{
				this.Rival.CharacterAnimation.Play(this.Rival.IdleAnim);
			}
			if (this.Phase == 1)
			{
				this.Panel.alpha = Mathf.MoveTowards(this.Panel.alpha, 0f, Time.deltaTime);
				this.Timer += Time.deltaTime;
				this.MainCamera.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(54f, 1.25f, -45.25f), this.Timer * 0.02f);
				this.MainCamera.transform.eulerAngles = Vector3.Lerp(this.MainCamera.transform.eulerAngles, new Vector3(0f, 45f, 0f), this.Timer * 0.02f);
				if (this.Timer > 5f)
				{
					this.Suitor.CharacterAnimation.Play("insertEarpiece_00");
					this.Suitor.CharacterAnimation["insertEarpiece_00"].time = 0f;
					this.Suitor.CharacterAnimation.Play("insertEarpiece_00");
					this.Suitor.Earpiece.SetActive(true);
					this.MainCamera.transform.position = new Vector3(45.5f, 1.25f, -44.5f);
					this.MainCamera.transform.eulerAngles = new Vector3(0f, -45f, 0f);
					this.Rotation = -45f;
					this.Timer = 0f;
					this.Phase++;
					return;
				}
			}
			else if (this.Phase == 2)
			{
				this.Timer += Time.deltaTime;
				if (this.Timer > 4f)
				{
					this.Suitor.Earpiece.transform.parent = this.Suitor.Head;
					this.Suitor.Earpiece.transform.localPosition = new Vector3(0f, -1.12f, 1.14f);
					this.Suitor.Earpiece.transform.localEulerAngles = new Vector3(45f, -180f, 0f);
					this.MainCamera.transform.position = Vector3.Lerp(this.MainCamera.transform.position, new Vector3(45.11f, 1.375f, -44f), (this.Timer - 4f) * 0.02f);
					this.Rotation = Mathf.Lerp(this.Rotation, 90f, (this.Timer - 4f) * 0.02f);
					this.MainCamera.transform.eulerAngles = new Vector3(this.MainCamera.transform.eulerAngles.x, this.Rotation, this.MainCamera.transform.eulerAngles.z);
					if (this.Rotation > 89.9f)
					{
						this.Rival.CharacterAnimation["f02_turnAround_00"].time = 0f;
						this.Rival.CharacterAnimation.CrossFade("f02_turnAround_00");
						this.AffectionBar.localScale = new Vector3(this.Affection / 100f, this.AffectionBar.localScale.y, this.AffectionBar.localScale.z);
						this.DialogueLabel.text = this.Greetings[this.AffectionLevel];
						this.CalculateMultiplier();
						this.DatingSimHUD.gameObject.SetActive(true);
						this.Timer = 0f;
						this.Phase++;
						return;
					}
				}
			}
			else if (this.Phase == 3)
			{
				this.DatingSimHUD.alpha = Mathf.MoveTowards(this.DatingSimHUD.alpha, 1f, Time.deltaTime);
				if (this.Rival.CharacterAnimation["f02_turnAround_00"].time >= this.Rival.CharacterAnimation["f02_turnAround_00"].length)
				{
					this.Rival.transform.eulerAngles = new Vector3(this.Rival.transform.eulerAngles.x, -90f, this.Rival.transform.eulerAngles.z);
					this.Rival.CharacterAnimation.Play("f02_turnAround_00");
					this.Rival.CharacterAnimation["f02_turnAround_00"].time = 0f;
					this.Rival.CharacterAnimation["f02_turnAround_00"].speed = 0f;
					this.Rival.CharacterAnimation.Play("f02_turnAround_00");
					this.Rival.CharacterAnimation.CrossFade(this.Rival.IdleAnim);
					Time.timeScale = 1f;
					this.PromptBar.ClearButtons();
					this.PromptBar.Label[0].text = "Confirm";
					this.PromptBar.Label[1].text = "Back";
					this.PromptBar.Label[4].text = "Select";
					this.PromptBar.UpdateButtons();
					this.PromptBar.Show = true;
					this.Phase++;
					return;
				}
			}
			else if (this.Phase == 4)
			{
				if (this.AffectionGrow)
				{
					this.Affection = Mathf.MoveTowards(this.Affection, 100f, Time.deltaTime * 10f);
					this.CalculateAffection();
				}
				this.Rival.Cosmetic.MyRenderer.materials[2].SetFloat("_BlendAmount", this.Affection * 0.01f);
				this.Rival.CharacterAnimation["f02_smile_00"].weight = this.Affection * 0.01f;
				this.Highlight.localPosition = new Vector3(this.Highlight.localPosition.x, Mathf.Lerp(this.Highlight.localPosition.y, this.HighlightTarget, Time.deltaTime * 10f), this.Highlight.localPosition.z);
				for (int i = 1; i < this.Options.Length; i++)
				{
					Transform transform = this.Options[i];
					transform.localPosition = new Vector3(Mathf.Lerp(transform.localPosition.x, (i == this.Selected) ? 750f : 800f, Time.deltaTime * 10f), transform.localPosition.y, transform.localPosition.z);
				}
				this.AffectionBar.localScale = new Vector3(Mathf.Lerp(this.AffectionBar.localScale.x, this.Affection / 100f, Time.deltaTime * 10f), this.AffectionBar.localScale.y, this.AffectionBar.localScale.z);
				if (!this.SelectingTopic && !this.Complimenting && !this.ShowingOff && !this.GivingGift)
				{
					this.Topics.localScale = Vector3.Lerp(this.Topics.localScale, Vector3.zero, Time.deltaTime * 10f);
					this.ComplimentSet.localScale = Vector3.Lerp(this.ComplimentSet.localScale, Vector3.zero, Time.deltaTime * 10f);
					this.ShowOff.localScale = Vector3.Lerp(this.ShowOff.localScale, Vector3.zero, Time.deltaTime * 10f);
					this.GiveGift.localScale = Vector3.Lerp(this.GiveGift.localScale, Vector3.zero, Time.deltaTime * 10f);
					if (this.InputManager.TappedUp)
					{
						this.Selected--;
						this.UpdateHighlight();
					}
					if (this.InputManager.TappedDown)
					{
						this.Selected++;
						this.UpdateHighlight();
					}
					if (Input.GetButtonDown("A") && this.Labels[this.Selected].color.a == 1f)
					{
						if (this.Selected == 1)
						{
							this.SelectingTopic = true;
							this.Negative = true;
							return;
						}
						if (this.Selected == 2)
						{
							this.SelectingTopic = true;
							this.Negative = false;
							return;
						}
						if (this.Selected == 3)
						{
							this.Complimenting = true;
							return;
						}
						if (this.Selected == 4)
						{
							this.ShowingOff = true;
							return;
						}
						if (this.Selected == 5)
						{
							this.GivingGift = true;
							return;
						}
						if (this.Selected == 6)
						{
							this.PromptBar.ClearButtons();
							this.PromptBar.Label[0].text = "Confirm";
							this.PromptBar.UpdateButtons();
							this.CalculateAffection();
							this.DialogueLabel.text = this.Farewells[this.AffectionLevel];
							this.Phase++;
							return;
						}
					}
				}
				else if (this.SelectingTopic)
				{
					this.Topics.localScale = Vector3.Lerp(this.Topics.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10f);
					if (this.InputManager.TappedUp)
					{
						this.Row--;
						this.UpdateTopicHighlight();
					}
					else if (this.InputManager.TappedDown)
					{
						this.Row++;
						this.UpdateTopicHighlight();
					}
					if (this.InputManager.TappedLeft)
					{
						this.Column--;
						this.UpdateTopicHighlight();
					}
					else if (this.InputManager.TappedRight)
					{
						this.Column++;
						this.UpdateTopicHighlight();
					}
					if (Input.GetButtonDown("A") && this.TopicIcons[this.TopicSelected].color.a == 1f)
					{
						this.SelectingTopic = false;
						UISprite uisprite = this.TopicIcons[this.TopicSelected];
						uisprite.color = new Color(uisprite.color.r, uisprite.color.g, uisprite.color.b, 0.5f);
						DatingGlobals.SetTopicDiscussed(this.TopicSelected, true);
						this.DetermineOpinion();
						if (!ConversationGlobals.GetTopicLearnedByStudent(this.Opinion, this.LoveManager.RivalID))
						{
							ConversationGlobals.SetTopicLearnedByStudent(this.Opinion, this.LoveManager.RivalID, true);
						}
						if (this.Negative)
						{
							UILabel uilabel = this.Labels[1];
							uilabel.color = new Color(uilabel.color.r, uilabel.color.g, uilabel.color.b, 0.5f);
							if (this.Opinion == 2)
							{
								this.DialogueLabel.text = "Hey! Just so you know, I take offense to that...";
								this.Rival.CharacterAnimation.CrossFade("f02_refuse_00");
								this.CurrentAnim = "f02_refuse_00";
								this.Affection -= 1f;
								this.CalculateAffection();
							}
							else if (this.Opinion == 1)
							{
								this.DialogueLabel.text = this.Negatives[this.AffectionLevel];
								this.Rival.CharacterAnimation.CrossFade("f02_lookdown_00");
								this.CurrentAnim = "f02_lookdown_00";
								this.Affection += (float)this.Multiplier;
								this.CalculateAffection();
							}
							else if (this.Opinion == 0)
							{
								this.DialogueLabel.text = "Um...okay.";
							}
						}
						else
						{
							UILabel uilabel2 = this.Labels[2];
							uilabel2.color = new Color(uilabel2.color.r, uilabel2.color.g, uilabel2.color.b, 0.5f);
							if (this.Opinion == 2)
							{
								this.DialogueLabel.text = this.Positives[this.AffectionLevel];
								this.Rival.CharacterAnimation.CrossFade("f02_lookdown_00");
								this.CurrentAnim = "f02_lookdown_00";
								this.Affection += (float)this.Multiplier;
								this.CalculateAffection();
							}
							else if (this.Opinion == 1)
							{
								this.DialogueLabel.text = "To be honest with you, I strongly disagree...";
								this.Rival.CharacterAnimation.CrossFade("f02_refuse_00");
								this.CurrentAnim = "f02_refuse_00";
								this.Affection -= 1f;
								this.CalculateAffection();
							}
							else if (this.Opinion == 0)
							{
								this.DialogueLabel.text = "Um...all right.";
							}
						}
						if (this.Affection > 100f)
						{
							this.Affection = 100f;
						}
						else if (this.Affection < 0f)
						{
							this.Affection = 0f;
						}
					}
					if (Input.GetButtonDown("B"))
					{
						this.SelectingTopic = false;
						return;
					}
				}
				else if (this.Complimenting)
				{
					this.ComplimentSet.localScale = Vector3.Lerp(this.ComplimentSet.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10f);
					if (this.InputManager.TappedUp)
					{
						this.Line--;
						this.UpdateComplimentHighlight();
					}
					else if (this.InputManager.TappedDown)
					{
						this.Line++;
						this.UpdateComplimentHighlight();
					}
					if (this.InputManager.TappedLeft)
					{
						this.Side--;
						this.UpdateComplimentHighlight();
					}
					else if (this.InputManager.TappedRight)
					{
						this.Side++;
						this.UpdateComplimentHighlight();
					}
					if (Input.GetButtonDown("A") && this.ComplimentLabels[this.ComplimentSelected].color.a == 1f)
					{
						UILabel uilabel3 = this.Labels[3];
						uilabel3.color = new Color(uilabel3.color.r, uilabel3.color.g, uilabel3.color.b, 0.5f);
						this.Complimenting = false;
						this.DialogueLabel.text = this.Compliments[this.ComplimentSelected];
						DatingGlobals.SetComplimentGiven(this.ComplimentSelected, true);
						if (this.ComplimentSelected == 1 || this.ComplimentSelected == 4 || this.ComplimentSelected == 5 || this.ComplimentSelected == 8 || this.ComplimentSelected == 9)
						{
							this.Rival.CharacterAnimation.CrossFade("f02_lookdown_00");
							this.CurrentAnim = "f02_lookdown_00";
							this.Affection += (float)this.Multiplier;
							this.CalculateAffection();
						}
						else
						{
							this.Rival.CharacterAnimation.CrossFade("f02_refuse_00");
							this.CurrentAnim = "f02_refuse_00";
							this.Affection -= 1f;
							this.CalculateAffection();
						}
						if (this.Affection > 100f)
						{
							this.Affection = 100f;
						}
						else if (this.Affection < 0f)
						{
							this.Affection = 0f;
						}
					}
					if (Input.GetButtonDown("B"))
					{
						this.Complimenting = false;
						return;
					}
				}
				else if (this.ShowingOff)
				{
					this.ShowOff.localScale = Vector3.Lerp(this.ShowOff.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10f);
					if (this.InputManager.TappedUp)
					{
						this.TraitSelected--;
						this.UpdateTraitHighlight();
					}
					else if (this.InputManager.TappedDown)
					{
						this.TraitSelected++;
						this.UpdateTraitHighlight();
					}
					if (Input.GetButtonDown("A"))
					{
						UILabel uilabel4 = this.Labels[4];
						uilabel4.color = new Color(uilabel4.color.r, uilabel4.color.g, uilabel4.color.b, 0.5f);
						this.ShowingOff = false;
						if (this.TraitSelected == 2)
						{
							Debug.Log(string.Concat(new object[]
							{
								"Wisdom trait is ",
								DatingGlobals.GetSuitorTrait(2),
								". Wisdom Demonstrated is ",
								DatingGlobals.GetTraitDemonstrated(2),
								"."
							}));
							if (DatingGlobals.GetSuitorTrait(2) > DatingGlobals.GetTraitDemonstrated(2))
							{
								DatingGlobals.SetTraitDemonstrated(2, DatingGlobals.GetTraitDemonstrated(2) + 1);
								this.DialogueLabel.text = this.ShowOffs[this.AffectionLevel];
								this.Rival.CharacterAnimation.CrossFade("f02_lookdown_00");
								this.CurrentAnim = "f02_lookdown_00";
								this.Affection += (float)this.Multiplier;
								this.CalculateAffection();
							}
							else if (DatingGlobals.GetSuitorTrait(2) == 0)
							{
								this.DialogueLabel.text = "Uh...that doesn't sound correct...";
							}
							else if (DatingGlobals.GetSuitorTrait(2) == DatingGlobals.GetTraitDemonstrated(2))
							{
								this.DialogueLabel.text = "Uh...you already told me about that...";
							}
						}
						else
						{
							this.DialogueLabel.text = "Um...well...that sort of thing doesn't really matter to me...";
						}
						if (this.Affection > 100f)
						{
							this.Affection = 100f;
						}
						else if (this.Affection < 0f)
						{
							this.Affection = 0f;
						}
					}
					if (Input.GetButtonDown("B"))
					{
						this.ShowingOff = false;
						return;
					}
				}
				else if (this.GivingGift)
				{
					this.GiveGift.localScale = Vector3.Lerp(this.GiveGift.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10f);
					if (this.InputManager.TappedUp)
					{
						this.GiftRow--;
						this.UpdateGiftHighlight();
					}
					else if (this.InputManager.TappedDown)
					{
						this.GiftRow++;
						this.UpdateGiftHighlight();
					}
					if (this.InputManager.TappedLeft)
					{
						this.GiftColumn--;
						this.UpdateGiftHighlight();
					}
					else if (this.InputManager.TappedRight)
					{
						this.GiftColumn++;
						this.UpdateGiftHighlight();
					}
					if (Input.GetButtonDown("A"))
					{
						if (this.GiftIcons[this.GiftSelected].enabled)
						{
							CollectibleGlobals.SetGiftPurchased(this.GiftSelected + 5, false);
							CollectibleGlobals.SetGiftGiven(this.GiftSelected, false);
							this.Rival.Cosmetic.CatGifts[this.GiftSelected].SetActive(true);
							UILabel uilabel5 = this.Labels[5];
							uilabel5.color = new Color(uilabel5.color.r, uilabel5.color.g, uilabel5.color.b, 0.5f);
							this.GivingGift = false;
							this.DialogueLabel.text = this.GiveGifts[this.GiftSelected];
							this.Rival.CharacterAnimation.CrossFade("f02_lookdown_00");
							this.CurrentAnim = "f02_lookdown_00";
							this.Affection += (float)this.Multiplier;
							this.CalculateAffection();
						}
						if (this.Affection > 100f)
						{
							this.Affection = 100f;
						}
						else if (this.Affection < 0f)
						{
							this.Affection = 0f;
						}
					}
					if (Input.GetButtonDown("B"))
					{
						this.GivingGift = false;
						return;
					}
				}
			}
			else if (this.Phase == 5)
			{
				this.Speed += Time.deltaTime * 100f;
				this.AffectionSet.localPosition = new Vector3(this.AffectionSet.localPosition.x, this.AffectionSet.localPosition.y + this.Speed, this.AffectionSet.localPosition.z);
				this.OptionSet.localPosition = new Vector3(this.OptionSet.localPosition.x + this.Speed, this.OptionSet.localPosition.y, this.OptionSet.localPosition.z);
				if (this.Speed > 100f && Input.GetButtonDown("A"))
				{
					this.Phase++;
					return;
				}
			}
			else if (this.Phase == 6)
			{
				this.DatingSimHUD.alpha = Mathf.MoveTowards(this.DatingSimHUD.alpha, 0f, Time.deltaTime);
				if (this.DatingSimHUD.alpha == 0f)
				{
					this.DatingSimHUD.gameObject.SetActive(false);
					this.Phase++;
					return;
				}
			}
			else if (this.Phase == 7)
			{
				if (this.Panel.alpha == 0f)
				{
					this.Suitor.CharacterAnimation.cullingType = AnimationCullingType.BasedOnRenderers;
					this.LoveManager.RivalWaiting = false;
					this.LoveManager.Courted = true;
					this.Suitor.enabled = true;
					this.Rival.enabled = true;
					this.Suitor.CurrentDestination = this.Suitor.Destinations[this.Suitor.Phase];
					this.Suitor.Pathfinding.target = this.Suitor.Destinations[this.Suitor.Phase];
					this.Suitor.Prompt.Label[0].text = "     Talk";
					this.Suitor.Pathfinding.canSearch = true;
					this.Suitor.Pathfinding.canMove = true;
					this.Suitor.Pushable = false;
					this.Suitor.Meeting = false;
					this.Suitor.Routine = true;
					this.Suitor.MeetTimer = 0f;
					this.Rival.Cosmetic.MyRenderer.materials[2].SetFloat("_BlendAmount", 0f);
					this.Rival.CurrentDestination = this.Rival.Destinations[this.Rival.Phase];
					this.Rival.Pathfinding.target = this.Rival.Destinations[this.Rival.Phase];
					this.Rival.CharacterAnimation["f02_smile_00"].weight = 0f;
					this.Rival.Prompt.Label[0].text = "     Talk";
					this.Rival.Pathfinding.canSearch = true;
					this.Rival.Pathfinding.canMove = true;
					this.Rival.Pushable = false;
					this.Rival.Meeting = false;
					this.Rival.Routine = true;
					this.Rival.MeetTimer = 0f;
					this.StudentManager.Clock.StopTime = false;
					this.Yandere.RPGCamera.enabled = true;
					this.Suitor.Earpiece.SetActive(false);
					this.HeartbeatCamera.SetActive(true);
					this.Yandere.Headset.SetActive(false);
					DatingGlobals.Affection = this.Affection;
					if (this.AffectionLevel == 5)
					{
						this.LoveManager.ConfessToSuitor = true;
					}
					this.PromptBar.ClearButtons();
					this.PromptBar.Show = false;
					if (this.StudentManager.Students[10] != null)
					{
						this.StudentManager.Students[10].CurrentDestination = this.StudentManager.Students[10].FollowTarget.transform;
						this.StudentManager.Students[10].Pathfinding.target = this.StudentManager.Students[10].FollowTarget.transform;
					}
				}
				else if (this.Panel.alpha == 1f)
				{
					this.Matchmaking = false;
					this.Yandere.CanMove = true;
					base.gameObject.SetActive(false);
				}
				this.Panel.alpha = Mathf.MoveTowards(this.Panel.alpha, 1f, Time.deltaTime);
			}
		}
	}

	// Token: 0x060012FC RID: 4860 RVA: 0x0009B054 File Offset: 0x00099254
	private void LateUpdate()
	{
		int phase = this.Phase;
	}

	// Token: 0x060012FD RID: 4861 RVA: 0x0009B060 File Offset: 0x00099260
	private void CalculateMultiplier()
	{
		this.Multiplier = 5;
		if (!this.Suitor.Cosmetic.MaleHair[55].activeInHierarchy)
		{
			this.MultiplierIcons[1].mainTexture = this.X;
			this.Multiplier--;
		}
		if (!this.Suitor.Cosmetic.MaleAccessories[17].activeInHierarchy)
		{
			this.MultiplierIcons[2].mainTexture = this.X;
			this.Multiplier--;
		}
		if (!this.Suitor.Cosmetic.Eyewear[6].activeInHierarchy)
		{
			this.MultiplierIcons[3].mainTexture = this.X;
			this.Multiplier--;
		}
		if (this.Suitor.Cosmetic.SkinColor != 6)
		{
			this.MultiplierIcons[4].mainTexture = this.X;
			this.Multiplier--;
		}
		if (PlayerGlobals.PantiesEquipped == 2)
		{
			this.PantyIcon.SetActive(true);
			this.Multiplier++;
		}
		else
		{
			this.PantyIcon.SetActive(false);
		}
		if (PlayerGlobals.Seduction + PlayerGlobals.SeductionBonus > 0)
		{
			this.SeductionLabel.text = (PlayerGlobals.Seduction + PlayerGlobals.SeductionBonus).ToString();
			this.Multiplier += PlayerGlobals.Seduction + PlayerGlobals.SeductionBonus;
			this.SeductionIcon.SetActive(true);
		}
		else
		{
			this.SeductionIcon.SetActive(false);
		}
		this.MultiplierLabel.text = "Multiplier: " + this.Multiplier.ToString() + "x";
	}

	// Token: 0x060012FE RID: 4862 RVA: 0x0009B20B File Offset: 0x0009940B
	private void UpdateHighlight()
	{
		if (this.Selected < 1)
		{
			this.Selected = 6;
		}
		else if (this.Selected > 6)
		{
			this.Selected = 1;
		}
		this.HighlightTarget = 450f - 100f * (float)this.Selected;
	}

	// Token: 0x060012FF RID: 4863 RVA: 0x0009B248 File Offset: 0x00099448
	private void UpdateTopicHighlight()
	{
		if (this.Row < 1)
		{
			this.Row = 5;
		}
		else if (this.Row > 5)
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
		this.TopicHighlight.localPosition = new Vector3((float)(-375 + 125 * this.Column), (float)(375 - 125 * this.Row), this.TopicHighlight.localPosition.z);
		this.TopicSelected = (this.Row - 1) * 5 + this.Column;
		this.TopicNameLabel.text = (ConversationGlobals.GetTopicDiscovered(this.TopicSelected) ? this.TopicNames[this.TopicSelected] : "??????????");
	}

	// Token: 0x06001300 RID: 4864 RVA: 0x0009B31C File Offset: 0x0009951C
	private void DetermineOpinion()
	{
		int[] topics = this.JSON.Topics[this.LoveManager.RivalID].Topics;
		this.Opinion = topics[this.TopicSelected];
	}

	// Token: 0x06001301 RID: 4865 RVA: 0x0009B354 File Offset: 0x00099554
	private void UpdateTopics()
	{
		for (int i = 1; i < this.TopicIcons.Length; i++)
		{
			UISprite uisprite = this.TopicIcons[i];
			if (!ConversationGlobals.GetTopicDiscovered(i))
			{
				uisprite.spriteName = 0.ToString();
				uisprite.color = new Color(uisprite.color.r, uisprite.color.g, uisprite.color.b, 0.5f);
			}
			else
			{
				uisprite.spriteName = i.ToString();
			}
		}
		for (int j = 1; j <= 25; j++)
		{
			UISprite uisprite2 = this.OpinionIcons[j];
			if (!ConversationGlobals.GetTopicLearnedByStudent(j, this.LoveManager.RivalID))
			{
				uisprite2.spriteName = "Unknown";
			}
			else
			{
				int[] topics = this.JSON.Topics[this.LoveManager.RivalID].Topics;
				uisprite2.spriteName = this.OpinionSpriteNames[topics[j]];
			}
		}
	}

	// Token: 0x06001302 RID: 4866 RVA: 0x0009B43C File Offset: 0x0009963C
	private void UpdateComplimentHighlight()
	{
		for (int i = 1; i < this.TopicIcons.Length; i++)
		{
			this.ComplimentBGs[this.ComplimentSelected].color = this.OriginalColor;
		}
		if (this.Line < 1)
		{
			this.Line = 5;
		}
		else if (this.Line > 5)
		{
			this.Line = 1;
		}
		if (this.Side < 1)
		{
			this.Side = 2;
		}
		else if (this.Side > 2)
		{
			this.Side = 1;
		}
		this.ComplimentSelected = (this.Line - 1) * 2 + this.Side;
		this.ComplimentBGs[this.ComplimentSelected].color = Color.white;
	}

	// Token: 0x06001303 RID: 4867 RVA: 0x0009B4E8 File Offset: 0x000996E8
	private void UpdateTraitHighlight()
	{
		if (this.TraitSelected < 1)
		{
			this.TraitSelected = 3;
		}
		else if (this.TraitSelected > 3)
		{
			this.TraitSelected = 1;
		}
		for (int i = 1; i < this.TraitBGs.Length; i++)
		{
			this.TraitBGs[i].color = this.OriginalColor;
		}
		this.TraitBGs[this.TraitSelected].color = Color.white;
	}

	// Token: 0x06001304 RID: 4868 RVA: 0x0009B554 File Offset: 0x00099754
	private void UpdateGiftHighlight()
	{
		for (int i = 1; i < this.GiftBGs.Length; i++)
		{
			this.GiftBGs[i].color = this.OriginalColor;
		}
		if (this.GiftRow < 1)
		{
			this.GiftRow = 2;
		}
		else if (this.GiftRow > 2)
		{
			this.GiftRow = 1;
		}
		if (this.GiftColumn < 1)
		{
			this.GiftColumn = 2;
		}
		else if (this.GiftColumn > 2)
		{
			this.GiftColumn = 1;
		}
		this.GiftSelected = (this.GiftRow - 1) * 2 + this.GiftColumn;
		this.GiftBGs[this.GiftSelected].color = Color.white;
	}

	// Token: 0x040018E1 RID: 6369
	public StudentManagerScript StudentManager;

	// Token: 0x040018E2 RID: 6370
	public InputManagerScript InputManager;

	// Token: 0x040018E3 RID: 6371
	public LoveManagerScript LoveManager;

	// Token: 0x040018E4 RID: 6372
	public PromptBarScript PromptBar;

	// Token: 0x040018E5 RID: 6373
	public YandereScript Yandere;

	// Token: 0x040018E6 RID: 6374
	public StudentScript Suitor;

	// Token: 0x040018E7 RID: 6375
	public StudentScript Rival;

	// Token: 0x040018E8 RID: 6376
	public PromptScript Prompt;

	// Token: 0x040018E9 RID: 6377
	public JsonScript JSON;

	// Token: 0x040018EA RID: 6378
	public Transform AffectionSet;

	// Token: 0x040018EB RID: 6379
	public Transform OptionSet;

	// Token: 0x040018EC RID: 6380
	public GameObject HeartbeatCamera;

	// Token: 0x040018ED RID: 6381
	public GameObject SeductionIcon;

	// Token: 0x040018EE RID: 6382
	public GameObject PantyIcon;

	// Token: 0x040018EF RID: 6383
	public Transform TopicHighlight;

	// Token: 0x040018F0 RID: 6384
	public Transform ComplimentSet;

	// Token: 0x040018F1 RID: 6385
	public Transform AffectionBar;

	// Token: 0x040018F2 RID: 6386
	public Transform Highlight;

	// Token: 0x040018F3 RID: 6387
	public Transform GiveGift;

	// Token: 0x040018F4 RID: 6388
	public Transform PeekSpot;

	// Token: 0x040018F5 RID: 6389
	public Transform[] Options;

	// Token: 0x040018F6 RID: 6390
	public Transform ShowOff;

	// Token: 0x040018F7 RID: 6391
	public Transform Topics;

	// Token: 0x040018F8 RID: 6392
	public Texture X;

	// Token: 0x040018F9 RID: 6393
	public UISprite[] OpinionIcons;

	// Token: 0x040018FA RID: 6394
	public UISprite[] TopicIcons;

	// Token: 0x040018FB RID: 6395
	public UITexture[] MultiplierIcons;

	// Token: 0x040018FC RID: 6396
	public UILabel[] ComplimentLabels;

	// Token: 0x040018FD RID: 6397
	public UISprite[] ComplimentBGs;

	// Token: 0x040018FE RID: 6398
	public UILabel MultiplierLabel;

	// Token: 0x040018FF RID: 6399
	public UILabel SeductionLabel;

	// Token: 0x04001900 RID: 6400
	public UILabel TopicNameLabel;

	// Token: 0x04001901 RID: 6401
	public UILabel DialogueLabel;

	// Token: 0x04001902 RID: 6402
	public UIPanel DatingSimHUD;

	// Token: 0x04001903 RID: 6403
	public UILabel WisdomLabel;

	// Token: 0x04001904 RID: 6404
	public UIPanel Panel;

	// Token: 0x04001905 RID: 6405
	public UITexture[] GiftIcons;

	// Token: 0x04001906 RID: 6406
	public UISprite[] TraitBGs;

	// Token: 0x04001907 RID: 6407
	public UISprite[] GiftBGs;

	// Token: 0x04001908 RID: 6408
	public UILabel[] Labels;

	// Token: 0x04001909 RID: 6409
	public string[] OpinionSpriteNames;

	// Token: 0x0400190A RID: 6410
	public string[] Compliments;

	// Token: 0x0400190B RID: 6411
	public string[] TopicNames;

	// Token: 0x0400190C RID: 6412
	public string[] GiveGifts;

	// Token: 0x0400190D RID: 6413
	public string[] Greetings;

	// Token: 0x0400190E RID: 6414
	public string[] Farewells;

	// Token: 0x0400190F RID: 6415
	public string[] Negatives;

	// Token: 0x04001910 RID: 6416
	public string[] Positives;

	// Token: 0x04001911 RID: 6417
	public string[] ShowOffs;

	// Token: 0x04001912 RID: 6418
	public bool SelectingTopic;

	// Token: 0x04001913 RID: 6419
	public bool AffectionGrow;

	// Token: 0x04001914 RID: 6420
	public bool Complimenting;

	// Token: 0x04001915 RID: 6421
	public bool Matchmaking;

	// Token: 0x04001916 RID: 6422
	public bool GivingGift;

	// Token: 0x04001917 RID: 6423
	public bool ShowingOff;

	// Token: 0x04001918 RID: 6424
	public bool Negative;

	// Token: 0x04001919 RID: 6425
	public bool SlideOut;

	// Token: 0x0400191A RID: 6426
	public bool Testing;

	// Token: 0x0400191B RID: 6427
	public float HighlightTarget;

	// Token: 0x0400191C RID: 6428
	public float Affection;

	// Token: 0x0400191D RID: 6429
	public float Rotation;

	// Token: 0x0400191E RID: 6430
	public float Speed;

	// Token: 0x0400191F RID: 6431
	public float Timer;

	// Token: 0x04001920 RID: 6432
	public int ComplimentSelected = 1;

	// Token: 0x04001921 RID: 6433
	public int TraitSelected = 1;

	// Token: 0x04001922 RID: 6434
	public int TopicSelected = 1;

	// Token: 0x04001923 RID: 6435
	public int GiftSelected = 1;

	// Token: 0x04001924 RID: 6436
	public int Selected = 1;

	// Token: 0x04001925 RID: 6437
	public int AffectionLevel;

	// Token: 0x04001926 RID: 6438
	public int Multiplier;

	// Token: 0x04001927 RID: 6439
	public int Opinion;

	// Token: 0x04001928 RID: 6440
	public int Phase = 1;

	// Token: 0x04001929 RID: 6441
	public int GiftColumn = 1;

	// Token: 0x0400192A RID: 6442
	public int GiftRow = 1;

	// Token: 0x0400192B RID: 6443
	public int Column = 1;

	// Token: 0x0400192C RID: 6444
	public int Row = 1;

	// Token: 0x0400192D RID: 6445
	public int Side = 1;

	// Token: 0x0400192E RID: 6446
	public int Line = 1;

	// Token: 0x0400192F RID: 6447
	public string CurrentAnim = string.Empty;

	// Token: 0x04001930 RID: 6448
	public Color OriginalColor;

	// Token: 0x04001931 RID: 6449
	public Camera MainCamera;
}
