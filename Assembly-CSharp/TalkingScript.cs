using System;
using UnityEngine;

// Token: 0x02000412 RID: 1042
public class TalkingScript : MonoBehaviour
{
	// Token: 0x06001BF5 RID: 7157 RVA: 0x00147534 File Offset: 0x00145734
	private void Update()
	{
		if (this.S.Talking)
		{
			if (this.S.Sleuthing)
			{
				this.ClubBonus = 5;
			}
			else
			{
				this.ClubBonus = 0;
			}
			if (GameGlobals.EmptyDemon)
			{
				this.ClubBonus = (int)(this.S.Club * (ClubType)(-1));
			}
			if (this.S.Interaction == StudentInteractionType.Idle)
			{
				if (!this.Fake)
				{
					if (this.S.Sleuthing)
					{
						this.IdleAnim = this.S.SleuthCalmAnim;
					}
					else if (this.S.Club == ClubType.Art && this.S.DialogueWheel.ClubLeader && this.S.Paintbrush.activeInHierarchy)
					{
						this.IdleAnim = "paintingIdle_00";
					}
					else if (this.S.Club != ClubType.Bully)
					{
						this.IdleAnim = this.S.IdleAnim;
					}
					else if (this.S.StudentManager.Reputation.Reputation < 33.33333f || this.S.Persona == PersonaType.Coward)
					{
						if (this.S.CurrentAction == StudentActionType.Sunbathe && this.S.SunbathePhase > 2)
						{
							this.IdleAnim = this.S.OriginalIdleAnim;
						}
						else
						{
							this.IdleAnim = this.S.IdleAnim;
						}
					}
					else
					{
						this.IdleAnim = this.S.CuteAnim;
					}
					this.S.CharacterAnimation.CrossFade(this.IdleAnim);
				}
				else if (this.IdleAnim != "")
				{
					this.S.CharacterAnimation.CrossFade(this.IdleAnim);
				}
				if (this.S.TalkTimer == 0f)
				{
					if (!this.S.DialogueWheel.AppearanceWindow.Show)
					{
						this.S.DialogueWheel.Impatience.fillAmount += Time.deltaTime * 0.1f;
					}
					if (this.S.DialogueWheel.Impatience.fillAmount > 0.5f && this.S.Subtitle.Timer == 0f)
					{
						if (this.S.StudentID == 41)
						{
							this.S.Subtitle.UpdateLabel(SubtitleType.Impatience, 4, 5f);
						}
						else if (this.S.Pestered == 0)
						{
							this.S.Subtitle.UpdateLabel(SubtitleType.Impatience, 0, 5f);
						}
						else
						{
							this.S.Subtitle.UpdateLabel(SubtitleType.Impatience, 2, 5f);
						}
					}
					if (this.S.DialogueWheel.Impatience.fillAmount == 1f && this.S.DialogueWheel.Show)
					{
						if (this.S.StudentID == 41)
						{
							this.S.Subtitle.UpdateLabel(SubtitleType.Impatience, 4, 5f);
						}
						else if (this.S.Pestered == 0)
						{
							this.S.Subtitle.UpdateLabel(SubtitleType.Impatience, 1, 5f);
						}
						else
						{
							this.S.Subtitle.UpdateLabel(SubtitleType.Impatience, 3, 5f);
						}
						this.S.WaitTimer = 0f;
						this.S.Pestered += 5;
						this.S.DialogueWheel.Pestered = true;
						this.S.DialogueWheel.End();
					}
				}
			}
			else if (this.S.Interaction == StudentInteractionType.Forgiving)
			{
				if (this.S.TalkTimer == 3f)
				{
					if (this.S.Club != ClubType.Delinquent)
					{
						this.S.CharacterAnimation.CrossFade(this.S.Nod2Anim);
						this.S.RepRecovery = 5f;
						if (PlayerGlobals.PantiesEquipped == 6)
						{
							this.S.RepRecovery += 2.5f;
						}
						if (PlayerGlobals.SocialBonus > 0)
						{
							this.S.RepRecovery += 2.5f;
						}
						this.S.PendingRep += this.S.RepRecovery;
						this.S.Reputation.PendingRep += this.S.RepRecovery;
						this.S.ID = 0;
						while (this.S.ID < this.S.Outlines.Length)
						{
							this.S.Outlines[this.S.ID].color = new Color(0f, 1f, 0f, 1f);
							this.S.ID++;
						}
						this.S.Forgave = true;
						if (this.S.Witnessed == StudentWitnessType.Insanity || this.S.Witnessed == StudentWitnessType.WeaponAndBloodAndInsanity || this.S.Witnessed == StudentWitnessType.WeaponAndInsanity || this.S.Witnessed == StudentWitnessType.BloodAndInsanity)
						{
							this.S.Subtitle.UpdateLabel(SubtitleType.ForgivingInsanity, 0, 3f);
						}
						else if (this.S.Witnessed == StudentWitnessType.Accident)
						{
							this.S.Subtitle.UpdateLabel(SubtitleType.ForgivingAccident, 0, 5f);
						}
						else
						{
							this.S.Subtitle.UpdateLabel(SubtitleType.Forgiving, 0, 3f);
						}
					}
					else
					{
						this.S.Subtitle.UpdateLabel(SubtitleType.Dismissive, 0, 5f);
					}
				}
				else
				{
					if (Input.GetButtonDown("A"))
					{
						this.S.TalkTimer = 0f;
					}
					if (this.S.CharacterAnimation[this.S.Nod2Anim].time >= this.S.CharacterAnimation[this.S.Nod2Anim].length)
					{
						this.S.CharacterAnimation.CrossFade(this.IdleAnim);
					}
					if (this.S.TalkTimer <= 0f)
					{
						this.S.IgnoreTimer = 5f;
						this.S.DialogueWheel.End();
					}
				}
				this.S.TalkTimer -= Time.deltaTime;
			}
			else if (this.S.Interaction == StudentInteractionType.ReceivingCompliment)
			{
				if (this.S.TalkTimer == 3f)
				{
					this.S.CharacterAnimation.CrossFade(this.S.LookDownAnim);
					this.S.Subtitle.PersonaSubtitle.UpdateLabel(this.S.Persona, this.S.Reputation.Reputation, 5f);
					if (this.S.Club != ClubType.Delinquent)
					{
						this.CalculateRepBonus();
						this.S.Reputation.PendingRep += 1f + (float)this.S.RepBonus;
						this.S.PendingRep += 1f + (float)this.S.RepBonus;
					}
					this.S.Complimented = true;
				}
				else if (Input.GetButtonDown("A"))
				{
					this.S.TalkTimer = 0f;
				}
				this.S.TalkTimer -= Time.deltaTime;
				if (this.S.TalkTimer <= 0f)
				{
					this.S.DialogueWheel.End();
				}
			}
			else if (this.S.Interaction == StudentInteractionType.Gossiping)
			{
				if (this.S.TalkTimer == 3f)
				{
					if (this.S.Club != ClubType.Delinquent)
					{
						this.S.CharacterAnimation.CrossFade(this.S.GossipAnim);
						this.S.Subtitle.UpdateLabel(SubtitleType.StudentGossip, 0, 3f);
						this.S.GossipBonus = 0;
						if (this.S.Reputation.Reputation > 33.33333f)
						{
							this.S.GossipBonus++;
						}
						if (PlayerGlobals.PantiesEquipped == 9)
						{
							this.S.GossipBonus++;
						}
						if (SchemeGlobals.DarkSecret && this.S.DialogueWheel.Victim == this.S.StudentManager.RivalID)
						{
							this.S.GossipBonus++;
						}
						if (PlayerGlobals.GetStudentFriend(this.S.StudentID))
						{
							this.S.GossipBonus++;
						}
						if ((this.S.Male && PlayerGlobals.Seduction + PlayerGlobals.SeductionBonus > 0) || PlayerGlobals.Seduction == 5)
						{
							this.S.GossipBonus++;
						}
						if (PlayerGlobals.SocialBonus > 0)
						{
							this.S.GossipBonus++;
						}
						StudentGlobals.SetStudentReputation(this.S.DialogueWheel.Victim, StudentGlobals.GetStudentReputation(this.S.DialogueWheel.Victim) - (1 + this.S.GossipBonus));
						if (this.S.Club != ClubType.Bully)
						{
							this.S.Reputation.PendingRep -= 2f;
							this.S.PendingRep -= 2f;
						}
						this.S.Gossiped = true;
						this.S.Yandere.NotificationManager.TopicName = "Gossip";
						if (!ConversationGlobals.GetTopicDiscovered(19))
						{
							this.S.Yandere.NotificationManager.DisplayNotification(NotificationType.Topic);
							ConversationGlobals.SetTopicDiscovered(19, true);
						}
						if (!ConversationGlobals.GetTopicLearnedByStudent(19, this.S.StudentID))
						{
							this.S.Yandere.NotificationManager.DisplayNotification(NotificationType.Opinion);
							ConversationGlobals.SetTopicLearnedByStudent(19, this.S.StudentID, true);
						}
					}
					else
					{
						this.S.Subtitle.UpdateLabel(SubtitleType.Dismissive, 2, 3f);
					}
				}
				else
				{
					if (Input.GetButtonDown("A"))
					{
						this.S.TalkTimer = 0f;
					}
					if (this.S.CharacterAnimation[this.S.GossipAnim].time >= this.S.CharacterAnimation[this.S.GossipAnim].length)
					{
						this.S.CharacterAnimation.CrossFade(this.IdleAnim);
					}
					if (this.S.TalkTimer <= 0f)
					{
						this.S.DialogueWheel.End();
					}
				}
				this.S.TalkTimer -= Time.deltaTime;
			}
			else if (this.S.Interaction == StudentInteractionType.Bye)
			{
				if (this.S.TalkTimer == 2f)
				{
					if (this.S.Club != ClubType.Delinquent)
					{
						this.S.Subtitle.UpdateLabel(SubtitleType.StudentFarewell, 0, 2f);
					}
					else
					{
						this.S.Subtitle.UpdateLabel(SubtitleType.Dismissive, 3, 3f);
					}
				}
				else if (Input.GetButtonDown("A"))
				{
					this.S.TalkTimer = 0f;
				}
				this.S.CharacterAnimation.CrossFade(this.IdleAnim);
				this.S.TalkTimer -= Time.deltaTime;
				if (this.S.TalkTimer <= 0f)
				{
					this.S.Pestered += 2;
					this.S.DialogueWheel.End();
				}
			}
			else if (this.S.Interaction == StudentInteractionType.GivingTask)
			{
				if (this.S.TalkTimer == 100f)
				{
					this.S.Subtitle.UpdateLabel(this.S.TaskLineResponseType, this.S.TaskPhase, this.S.Subtitle.GetClipLength(this.S.StudentID, this.S.TaskPhase));
					this.S.CharacterAnimation.CrossFade(this.S.TaskAnims[this.S.TaskPhase]);
					this.S.CurrentAnim = this.S.TaskAnims[this.S.TaskPhase];
					this.S.TalkTimer = this.S.Subtitle.GetClipLength(this.S.StudentID, this.S.TaskPhase);
				}
				else if (Input.GetButtonDown("A"))
				{
					this.S.Subtitle.Label.text = string.Empty;
					UnityEngine.Object.Destroy(this.S.Subtitle.CurrentClip);
					this.S.TalkTimer = 0f;
				}
				if (this.S.CharacterAnimation[this.S.CurrentAnim].time >= this.S.CharacterAnimation[this.S.CurrentAnim].length)
				{
					this.S.CharacterAnimation.CrossFade(this.IdleAnim);
				}
				this.S.TalkTimer -= Time.deltaTime;
				if (this.S.TalkTimer <= 0f)
				{
					if (this.S.TaskPhase == 5)
					{
						this.S.DialogueWheel.TaskWindow.TaskComplete = true;
						TaskGlobals.SetTaskStatus(this.S.StudentID, 3);
						PlayerGlobals.SetStudentFriend(this.S.StudentID, true);
						this.S.Police.EndOfDay.NewFriends++;
						this.S.Interaction = StudentInteractionType.Idle;
						this.CalculateRepBonus();
						this.S.Reputation.PendingRep += 1f + (float)this.S.RepBonus;
						this.S.PendingRep += 1f + (float)this.S.RepBonus;
					}
					else if (this.S.TaskPhase == 4 || this.S.TaskPhase == 0)
					{
						this.S.StudentManager.TaskManager.UpdateTaskStatus();
						this.S.DialogueWheel.End();
					}
					else if (this.S.TaskPhase == 3)
					{
						this.S.DialogueWheel.TaskWindow.UpdateWindow(this.S.StudentID);
						this.S.Interaction = StudentInteractionType.Idle;
					}
					else
					{
						this.S.TaskPhase++;
						this.S.Subtitle.UpdateLabel(this.S.TaskLineResponseType, this.S.TaskPhase, this.S.Subtitle.GetClipLength(this.S.StudentID, this.S.TaskPhase));
						this.S.CharacterAnimation.CrossFade(this.S.TaskAnims[this.S.TaskPhase]);
						this.S.CurrentAnim = this.S.TaskAnims[this.S.TaskPhase];
						this.S.TalkTimer = this.S.Subtitle.GetClipLength(this.S.StudentID, this.S.TaskPhase);
					}
				}
			}
			else if (this.S.Interaction == StudentInteractionType.FollowingPlayer)
			{
				if (this.S.TalkTimer == 2f)
				{
					if (this.S.Club != ClubType.Delinquent)
					{
						if ((this.S.Clock.HourTime > 8f && this.S.Clock.HourTime < 13f) || (this.S.Clock.HourTime > 13.375f && this.S.Clock.HourTime < 15.5f))
						{
							this.S.CharacterAnimation.CrossFade(this.S.GossipAnim);
							this.S.Subtitle.UpdateLabel(SubtitleType.StudentStay, 0, 5f);
							this.NegativeResponse = true;
						}
						else if (this.S.StudentManager.LockerRoomArea.bounds.Contains(this.S.Yandere.transform.position) || this.S.StudentManager.WestBathroomArea.bounds.Contains(this.S.Yandere.transform.position) || this.S.StudentManager.EastBathroomArea.bounds.Contains(this.S.Yandere.transform.position) || this.S.StudentManager.HeadmasterArea.bounds.Contains(this.S.Yandere.transform.position) || this.S.MyRenderer.sharedMesh == this.S.SchoolSwimsuit || this.S.MyRenderer.sharedMesh == this.S.SwimmingTrunks)
						{
							this.S.CharacterAnimation.CrossFade(this.S.GossipAnim);
							this.S.Subtitle.UpdateLabel(SubtitleType.StudentStay, 1, 5f);
							this.NegativeResponse = true;
						}
						else
						{
							int num = 0;
							if (ClubGlobals.Club == ClubType.Delinquent)
							{
								this.S.Reputation.PendingRep -= 10f;
								this.S.PendingRep -= 10f;
								num++;
							}
							this.S.CharacterAnimation.CrossFade(this.S.Nod1Anim);
							this.S.Subtitle.UpdateLabel(SubtitleType.StudentFollow, num, 2f);
							this.Follow = true;
						}
					}
					else
					{
						this.S.Subtitle.UpdateLabel(SubtitleType.Dismissive, 4, 5f);
					}
				}
				else
				{
					if (Input.GetButtonDown("A"))
					{
						this.S.TalkTimer = 0f;
					}
					if (this.S.CharacterAnimation[this.S.Nod1Anim].time >= this.S.CharacterAnimation[this.S.Nod1Anim].length)
					{
						this.S.CharacterAnimation.CrossFade(this.IdleAnim);
					}
					if (this.S.TalkTimer <= 0f)
					{
						this.S.DialogueWheel.End();
						if (this.Follow)
						{
							this.S.Pathfinding.target = this.S.Yandere.transform;
							this.S.Prompt.Label[0].text = "     Stop";
							if (this.S.StudentID == 30)
							{
								this.S.StudentManager.FollowerLookAtTarget.position = this.S.DefaultTarget.position;
								this.S.StudentManager.LoveManager.Follower = this.S;
							}
							this.S.FollowCountdown.Sprite.fillAmount = 1f;
							if (ClubGlobals.Club != ClubType.Delinquent)
							{
								this.S.FollowCountdown.Speed = 1f / (35f + this.S.Reputation.Reputation * 0.25f);
							}
							else
							{
								this.S.FollowCountdown.Speed = 1f / (35f + this.S.Reputation.Reputation * -0.25f);
							}
							Debug.Log(string.Concat(new object[]
							{
								"Reputation is: ",
								this.S.Reputation.Reputation,
								" and Countdown Speed is: ",
								this.S.FollowCountdown.Speed
							}));
							this.S.FollowCountdown.gameObject.SetActive(true);
							this.S.Yandere.Follower = this.S;
							this.S.Yandere.Followers++;
							this.S.Following = true;
							this.S.Hurry = false;
						}
						this.Follow = false;
					}
				}
				this.S.TalkTimer -= Time.deltaTime;
			}
			else if (this.S.Interaction == StudentInteractionType.GoingAway)
			{
				if (this.S.TalkTimer == 3f)
				{
					if (this.S.Club != ClubType.Delinquent)
					{
						if ((this.S.Clock.HourTime > 8f && this.S.Clock.HourTime < 13f) || (this.S.Clock.HourTime > 13.375f && this.S.Clock.HourTime < 15.5f))
						{
							this.S.CharacterAnimation.CrossFade(this.S.GossipAnim);
							this.S.Subtitle.UpdateLabel(SubtitleType.StudentStay, 0, 5f);
						}
						else
						{
							int num2 = 0;
							if (ClubGlobals.Club == ClubType.Delinquent)
							{
								this.S.Reputation.PendingRep -= 10f;
								this.S.PendingRep -= 10f;
								num2++;
							}
							this.S.CharacterAnimation.CrossFade(this.S.Nod1Anim);
							this.S.Subtitle.UpdateLabel(SubtitleType.StudentLeave, num2, 3f);
							this.S.GoAway = true;
						}
					}
					else
					{
						this.S.Subtitle.UpdateLabel(SubtitleType.Dismissive, 5, 5f);
					}
				}
				else
				{
					if (Input.GetButtonDown("A"))
					{
						this.S.TalkTimer = 0f;
					}
					if (this.S.CharacterAnimation[this.S.Nod1Anim].time >= this.S.CharacterAnimation[this.S.Nod1Anim].length)
					{
						this.S.CharacterAnimation.CrossFade(this.IdleAnim);
					}
					if (this.S.TalkTimer <= 0f)
					{
						this.S.DialogueWheel.End();
					}
				}
				this.S.TalkTimer -= Time.deltaTime;
			}
			else if (this.S.Interaction == StudentInteractionType.DistractingTarget)
			{
				if (this.S.TalkTimer == 3f)
				{
					if (this.S.Club != ClubType.Delinquent)
					{
						if ((this.S.Clock.HourTime > 8f && this.S.Clock.HourTime < 13f) || (this.S.Clock.HourTime > 13.375f && this.S.Clock.HourTime < 15.5f))
						{
							this.S.CharacterAnimation.CrossFade(this.S.GossipAnim);
							this.S.Subtitle.UpdateLabel(SubtitleType.StudentStay, 0, 5f);
						}
						else
						{
							StudentScript studentScript = this.S.StudentManager.Students[this.S.DialogueWheel.Victim];
							this.Grudge = false;
							if (studentScript.Club == ClubType.Delinquent || (this.S.Bullied && studentScript.Club == ClubType.Bully) || (studentScript.StudentID == 36 && TaskGlobals.GetTaskStatus(36) < 3))
							{
								this.Grudge = true;
							}
							if (studentScript.Routine && !studentScript.TargetedForDistraction && !studentScript.InEvent && !this.Grudge && studentScript.Indoors && studentScript.gameObject.activeInHierarchy && studentScript.ClubActivityPhase < 16 && studentScript.CurrentAction != StudentActionType.Sunbathe && studentScript.FollowTarget == null)
							{
								int num3 = 0;
								if (ClubGlobals.Club == ClubType.Delinquent)
								{
									this.S.Reputation.PendingRep -= 10f;
									this.S.PendingRep -= 10f;
									num3++;
								}
								this.S.CharacterAnimation.CrossFade(this.S.Nod1Anim);
								this.S.Subtitle.UpdateLabel(SubtitleType.StudentDistract, num3, 3f);
								this.Refuse = false;
							}
							else
							{
								this.S.CharacterAnimation.CrossFade(this.S.GossipAnim);
								if (this.Grudge)
								{
									this.S.Subtitle.UpdateLabel(SubtitleType.StudentDistractBullyRefuse, 0, 3f);
								}
								else
								{
									this.S.Subtitle.UpdateLabel(SubtitleType.StudentDistractRefuse, 0, 3f);
								}
								this.Refuse = true;
							}
						}
					}
					else
					{
						this.S.Subtitle.UpdateLabel(SubtitleType.Dismissive, 6, 5f);
						this.Refuse = true;
					}
				}
				else
				{
					if (Input.GetButtonDown("A"))
					{
						this.S.TalkTimer = 0f;
					}
					if (this.S.CharacterAnimation[this.S.Nod1Anim].time >= this.S.CharacterAnimation[this.S.Nod1Anim].length)
					{
						this.S.CharacterAnimation.CrossFade(this.IdleAnim);
					}
					if (this.S.TalkTimer <= 0f)
					{
						this.S.DialogueWheel.End();
						if (!this.Refuse && (this.S.Clock.HourTime < 8f || (this.S.Clock.HourTime > 13f && this.S.Clock.HourTime < 13.375f) || this.S.Clock.HourTime > 15.5f) && !this.S.Distracting)
						{
							this.S.DistractionTarget = this.S.StudentManager.Students[this.S.DialogueWheel.Victim];
							this.S.DistractionTarget.TargetedForDistraction = true;
							this.S.CurrentDestination = this.S.DistractionTarget.transform;
							this.S.Pathfinding.target = this.S.DistractionTarget.transform;
							this.S.Pathfinding.speed = 4f;
							this.S.TargetDistance = 1f;
							this.S.DistractTimer = 10f;
							this.S.Distracting = true;
							this.S.Routine = false;
							this.S.CanTalk = false;
						}
					}
				}
				this.S.TalkTimer -= Time.deltaTime;
			}
			else if (this.S.Interaction == StudentInteractionType.PersonalGrudge)
			{
				if (this.S.TalkTimer == 5f)
				{
					if (this.S.Persona == PersonaType.Coward || this.S.Persona == PersonaType.Fragile)
					{
						this.S.Subtitle.UpdateLabel(SubtitleType.CowardGrudge, 0, 5f);
						this.S.CharacterAnimation.CrossFade(this.S.CowardAnim);
						this.S.TalkTimer = 5f;
					}
					else
					{
						if (!this.S.Male)
						{
							this.S.Subtitle.UpdateLabel(SubtitleType.GrudgeWarning, 0, 99f);
						}
						else if (this.S.Club == ClubType.Delinquent)
						{
							this.S.Subtitle.UpdateLabel(SubtitleType.DelinquentGrudge, 1, 99f);
						}
						else
						{
							this.S.Subtitle.UpdateLabel(SubtitleType.GrudgeWarning, 1, 99f);
						}
						this.S.TalkTimer = this.S.Subtitle.CurrentClip.GetComponent<AudioSource>().clip.length;
						this.S.CharacterAnimation.CrossFade(this.S.GrudgeAnim);
					}
				}
				else
				{
					if (Input.GetButtonDown("A"))
					{
						this.S.TalkTimer = 0f;
					}
					if (this.S.TalkTimer <= 0f)
					{
						this.S.DialogueWheel.End();
					}
				}
				this.S.TalkTimer -= Time.deltaTime;
			}
			else if (this.S.Interaction == StudentInteractionType.ClubInfo)
			{
				if (this.S.TalkTimer == 100f)
				{
					this.S.Subtitle.UpdateLabel(this.S.ClubInfoResponseType, this.S.ClubPhase, 99f);
					this.S.TalkTimer = this.S.Subtitle.GetClubClipLength(this.S.Club, this.S.ClubPhase);
				}
				else if (Input.GetButtonDown("A"))
				{
					this.S.Subtitle.Label.text = string.Empty;
					UnityEngine.Object.Destroy(this.S.Subtitle.CurrentClip);
					this.S.TalkTimer = 0f;
				}
				this.S.TalkTimer -= Time.deltaTime;
				if (this.S.TalkTimer <= 0f)
				{
					if (this.S.ClubPhase == 3)
					{
						this.S.DialogueWheel.Panel.enabled = true;
						this.S.DialogueWheel.Show = true;
						this.S.Subtitle.Label.text = string.Empty;
						this.S.Interaction = StudentInteractionType.Idle;
						this.S.TalkTimer = 0f;
					}
					else
					{
						this.S.ClubPhase++;
						this.S.Subtitle.UpdateLabel(this.S.ClubInfoResponseType, this.S.ClubPhase, 99f);
						this.S.TalkTimer = this.S.Subtitle.GetClubClipLength(this.S.Club, this.S.ClubPhase);
					}
				}
			}
			else if (this.S.Interaction == StudentInteractionType.ClubJoin)
			{
				if (this.S.TalkTimer == 100f)
				{
					if (this.S.ClubPhase == 1)
					{
						this.S.Subtitle.UpdateLabel(SubtitleType.ClubJoin, (int)(this.S.Club + this.ClubBonus), 99f);
						this.S.TalkTimer = this.S.Subtitle.CurrentClip.GetComponent<AudioSource>().clip.length;
					}
					else if (this.S.ClubPhase == 2)
					{
						this.S.Subtitle.UpdateLabel(SubtitleType.ClubAccept, (int)(this.S.Club + this.ClubBonus), 99f);
						this.S.TalkTimer = this.S.Subtitle.CurrentClip.GetComponent<AudioSource>().clip.length;
					}
					else if (this.S.ClubPhase == 3)
					{
						this.S.Subtitle.UpdateLabel(SubtitleType.ClubRefuse, (int)(this.S.Club + this.ClubBonus), 99f);
						this.S.TalkTimer = this.S.Subtitle.CurrentClip.GetComponent<AudioSource>().clip.length;
					}
					else if (this.S.ClubPhase == 4)
					{
						this.S.Subtitle.UpdateLabel(SubtitleType.ClubRejoin, (int)(this.S.Club + this.ClubBonus), 99f);
						this.S.TalkTimer = this.S.Subtitle.CurrentClip.GetComponent<AudioSource>().clip.length;
					}
					else if (this.S.ClubPhase == 5)
					{
						this.S.Subtitle.UpdateLabel(SubtitleType.ClubExclusive, (int)(this.S.Club + this.ClubBonus), 99f);
						this.S.TalkTimer = this.S.Subtitle.CurrentClip.GetComponent<AudioSource>().clip.length;
					}
					else if (this.S.ClubPhase == 6)
					{
						this.S.Subtitle.UpdateLabel(SubtitleType.ClubGrudge, (int)(this.S.Club + this.ClubBonus), 99f);
						this.S.TalkTimer = this.S.Subtitle.CurrentClip.GetComponent<AudioSource>().clip.length;
					}
				}
				else if (Input.GetButtonDown("A"))
				{
					this.S.Subtitle.Label.text = string.Empty;
					UnityEngine.Object.Destroy(this.S.Subtitle.CurrentClip);
					this.S.TalkTimer = 0f;
				}
				this.S.TalkTimer -= Time.deltaTime;
				if (this.S.TalkTimer <= 0f)
				{
					if (this.S.ClubPhase == 1)
					{
						this.S.DialogueWheel.ClubWindow.Club = this.S.Club;
						this.S.DialogueWheel.ClubWindow.UpdateWindow();
						this.S.Subtitle.Label.text = string.Empty;
						this.S.Interaction = StudentInteractionType.Idle;
					}
					else
					{
						this.S.DialogueWheel.End();
						if (this.S.Club == ClubType.MartialArts)
						{
							this.S.ChangingBooth.CheckYandereClub();
						}
					}
				}
			}
			else if (this.S.Interaction == StudentInteractionType.ClubQuit)
			{
				if (this.S.TalkTimer == 100f)
				{
					if (this.S.ClubPhase == 1)
					{
						this.S.Subtitle.UpdateLabel(SubtitleType.ClubQuit, (int)(this.S.Club + this.ClubBonus), 99f);
						this.S.TalkTimer = this.S.Subtitle.CurrentClip.GetComponent<AudioSource>().clip.length;
					}
					else if (this.S.ClubPhase == 2)
					{
						this.S.Subtitle.UpdateLabel(SubtitleType.ClubConfirm, (int)(this.S.Club + this.ClubBonus), 99f);
						this.S.TalkTimer = this.S.Subtitle.CurrentClip.GetComponent<AudioSource>().clip.length;
					}
					else if (this.S.ClubPhase == 3)
					{
						this.S.Subtitle.UpdateLabel(SubtitleType.ClubDeny, (int)(this.S.Club + this.ClubBonus), 99f);
						this.S.TalkTimer = this.S.Subtitle.CurrentClip.GetComponent<AudioSource>().clip.length;
					}
				}
				else if (Input.GetButtonDown("A"))
				{
					this.S.Subtitle.Label.text = string.Empty;
					UnityEngine.Object.Destroy(this.S.Subtitle.CurrentClip);
					this.S.TalkTimer = 0f;
				}
				this.S.TalkTimer -= Time.deltaTime;
				if (this.S.TalkTimer <= 0f)
				{
					if (this.S.ClubPhase == 1)
					{
						this.S.DialogueWheel.ClubWindow.Club = this.S.Club;
						this.S.DialogueWheel.ClubWindow.Quitting = true;
						this.S.DialogueWheel.ClubWindow.UpdateWindow();
						this.S.Subtitle.Label.text = string.Empty;
						this.S.Interaction = StudentInteractionType.Idle;
					}
					else
					{
						this.S.DialogueWheel.End();
						if (this.S.Club == ClubType.MartialArts)
						{
							this.S.ChangingBooth.CheckYandereClub();
						}
						if (this.S.ClubPhase == 2)
						{
						}
					}
				}
			}
			else if (this.S.Interaction == StudentInteractionType.ClubBye)
			{
				if (this.S.TalkTimer == this.S.Subtitle.ClubFarewellClips[(int)(this.S.Club + this.ClubBonus)].length)
				{
					this.S.Subtitle.UpdateLabel(SubtitleType.ClubFarewell, (int)(this.S.Club + this.ClubBonus), this.S.Subtitle.ClubFarewellClips[(int)(this.S.Club + this.ClubBonus)].length);
				}
				else if (Input.GetButtonDown("A"))
				{
					this.S.TalkTimer = 0f;
				}
				this.S.TalkTimer -= Time.deltaTime;
				if (this.S.TalkTimer <= 0f)
				{
					this.S.DialogueWheel.End();
				}
			}
			else if (this.S.Interaction == StudentInteractionType.ClubActivity)
			{
				if (this.S.TalkTimer == 100f)
				{
					if (this.S.ClubPhase == 1)
					{
						this.S.Subtitle.UpdateLabel(SubtitleType.ClubActivity, (int)(this.S.Club + this.ClubBonus), 99f);
						this.S.TalkTimer = this.S.Subtitle.CurrentClip.GetComponent<AudioSource>().clip.length;
					}
					else if (this.S.ClubPhase == 2)
					{
						this.S.Subtitle.UpdateLabel(SubtitleType.ClubYes, (int)(this.S.Club + this.ClubBonus), 99f);
						this.S.TalkTimer = this.S.Subtitle.CurrentClip.GetComponent<AudioSource>().clip.length;
					}
					else if (this.S.ClubPhase == 3)
					{
						this.S.Subtitle.UpdateLabel(SubtitleType.ClubNo, (int)(this.S.Club + this.ClubBonus), 99f);
						this.S.TalkTimer = this.S.Subtitle.CurrentClip.GetComponent<AudioSource>().clip.length;
					}
					else if (this.S.ClubPhase == 4)
					{
						this.S.Subtitle.UpdateLabel(SubtitleType.ClubEarly, (int)(this.S.Club + this.ClubBonus), 99f);
						this.S.TalkTimer = this.S.Subtitle.CurrentClip.GetComponent<AudioSource>().clip.length;
					}
					else if (this.S.ClubPhase == 5)
					{
						this.S.Subtitle.UpdateLabel(SubtitleType.ClubLate, (int)(this.S.Club + this.ClubBonus), 99f);
						this.S.TalkTimer = this.S.Subtitle.CurrentClip.GetComponent<AudioSource>().clip.length;
					}
				}
				else if (Input.GetButtonDown("A"))
				{
					this.S.Subtitle.Label.text = string.Empty;
					UnityEngine.Object.Destroy(this.S.Subtitle.CurrentClip);
					this.S.TalkTimer = 0f;
				}
				this.S.TalkTimer -= Time.deltaTime;
				if (this.S.TalkTimer <= 0f)
				{
					if (this.S.ClubPhase == 1)
					{
						this.S.DialogueWheel.ClubWindow.Club = this.S.Club;
						this.S.DialogueWheel.ClubWindow.Activity = true;
						this.S.DialogueWheel.ClubWindow.UpdateWindow();
						this.S.Subtitle.Label.text = string.Empty;
						this.S.Interaction = StudentInteractionType.Idle;
					}
					else if (this.S.ClubPhase == 2)
					{
						this.S.Police.Darkness.enabled = true;
						this.S.Police.ClubActivity = true;
						this.S.Police.FadeOut = true;
						this.S.Subtitle.Label.text = string.Empty;
						this.S.Interaction = StudentInteractionType.Idle;
					}
					else
					{
						this.S.DialogueWheel.End();
					}
				}
			}
			else if (this.S.Interaction == StudentInteractionType.ClubUnwelcome)
			{
				this.S.CharacterAnimation.CrossFade(this.S.IdleAnim);
				if (this.S.TalkTimer == 5f)
				{
					this.S.Subtitle.UpdateLabel(SubtitleType.ClubUnwelcome, (int)(this.S.Club + this.ClubBonus), 99f);
					this.S.TalkTimer = this.S.Subtitle.CurrentClip.GetComponent<AudioSource>().clip.length;
				}
				else
				{
					if (Input.GetButtonDown("A"))
					{
						this.S.TalkTimer = 0f;
					}
					if (this.S.TalkTimer <= 0f)
					{
						this.S.DialogueWheel.End();
					}
				}
				this.S.TalkTimer -= Time.deltaTime;
			}
			else if (this.S.Interaction == StudentInteractionType.ClubKick)
			{
				this.S.CharacterAnimation.CrossFade(this.S.IdleAnim);
				if (this.S.TalkTimer == 5f)
				{
					this.S.Subtitle.UpdateLabel(SubtitleType.ClubKick, (int)(this.S.Club + this.ClubBonus), 99f);
					this.S.TalkTimer = this.S.Subtitle.CurrentClip.GetComponent<AudioSource>().clip.length;
				}
				else
				{
					if (Input.GetButtonDown("A"))
					{
						this.S.TalkTimer = 0f;
					}
					if (this.S.TalkTimer <= 0f)
					{
						this.S.ClubManager.DeactivateClubBenefit();
						ClubGlobals.Club = ClubType.None;
						this.S.DialogueWheel.End();
						this.S.Yandere.ClubAccessory();
					}
				}
				this.S.TalkTimer -= Time.deltaTime;
			}
			else if (this.S.Interaction == StudentInteractionType.ClubGrudge)
			{
				this.S.CharacterAnimation.CrossFade(this.S.IdleAnim);
				if (this.S.TalkTimer == 5f)
				{
					this.S.Subtitle.UpdateLabel(SubtitleType.ClubGrudge, (int)(this.S.Club + this.ClubBonus), 99f);
					this.S.TalkTimer = this.S.Subtitle.CurrentClip.GetComponent<AudioSource>().clip.length;
				}
				else
				{
					if (Input.GetButtonDown("A"))
					{
						this.S.TalkTimer = 0f;
					}
					if (this.S.TalkTimer <= 0f)
					{
						this.S.DialogueWheel.End();
					}
				}
				this.S.TalkTimer -= Time.deltaTime;
			}
			else if (this.S.Interaction == StudentInteractionType.ClubPractice)
			{
				if (this.S.TalkTimer == 100f)
				{
					if (this.S.ClubPhase == 1)
					{
						this.S.Subtitle.UpdateLabel(SubtitleType.ClubPractice, (int)(this.S.Club + this.ClubBonus), 99f);
						this.S.TalkTimer = this.S.Subtitle.CurrentClip.GetComponent<AudioSource>().clip.length;
					}
					else if (this.S.ClubPhase == 2)
					{
						this.S.Subtitle.UpdateLabel(SubtitleType.ClubPracticeYes, (int)(this.S.Club + this.ClubBonus), 99f);
						this.S.TalkTimer = this.S.Subtitle.CurrentClip.GetComponent<AudioSource>().clip.length;
					}
					else if (this.S.ClubPhase == 3)
					{
						this.S.Subtitle.UpdateLabel(SubtitleType.ClubPracticeNo, (int)(this.S.Club + this.ClubBonus), 99f);
						this.S.TalkTimer = this.S.Subtitle.CurrentClip.GetComponent<AudioSource>().clip.length;
					}
				}
				else if (Input.GetButtonDown("A"))
				{
					this.S.Subtitle.Label.text = string.Empty;
					UnityEngine.Object.Destroy(this.S.Subtitle.CurrentClip);
					this.S.TalkTimer = 0f;
				}
				this.S.TalkTimer -= Time.deltaTime;
				if (this.S.TalkTimer <= 0f)
				{
					if (this.S.ClubPhase == 1)
					{
						this.S.DialogueWheel.PracticeWindow.Club = this.S.Club;
						this.S.DialogueWheel.PracticeWindow.UpdateWindow();
						this.S.DialogueWheel.PracticeWindow.ID = 1;
						this.S.Subtitle.Label.text = string.Empty;
						this.S.Interaction = StudentInteractionType.Idle;
					}
					else if (this.S.ClubPhase == 2)
					{
						this.S.DialogueWheel.PracticeWindow.Club = this.S.Club;
						this.S.DialogueWheel.PracticeWindow.FadeOut = true;
						this.S.Subtitle.Label.text = string.Empty;
						this.S.Interaction = StudentInteractionType.Idle;
					}
					else if (this.S.ClubPhase == 3)
					{
						this.S.DialogueWheel.End();
					}
				}
			}
			else if (this.S.Interaction == StudentInteractionType.NamingCrush)
			{
				if (this.S.TalkTimer == 3f)
				{
					if (this.S.DialogueWheel.Victim != this.S.Crush)
					{
						this.S.Subtitle.UpdateLabel(SubtitleType.SuitorLove, 0, 3f);
						this.S.CharacterAnimation.CrossFade(this.S.GossipAnim);
						this.S.CurrentAnim = this.S.GossipAnim;
					}
					else
					{
						DatingGlobals.SuitorProgress = 1;
						this.S.Yandere.LoveManager.SuitorProgress++;
						this.S.Subtitle.UpdateLabel(SubtitleType.SuitorLove, 1, 3f);
						this.S.CharacterAnimation.CrossFade(this.S.Nod1Anim);
						this.S.CurrentAnim = this.S.Nod1Anim;
					}
				}
				else
				{
					if (Input.GetButtonDown("A"))
					{
						this.S.TalkTimer = 0f;
					}
					if (this.S.CharacterAnimation[this.S.CurrentAnim].time >= this.S.CharacterAnimation[this.S.CurrentAnim].length)
					{
						this.S.CharacterAnimation.CrossFade(this.IdleAnim);
					}
					if (this.S.TalkTimer <= 0f)
					{
						this.S.DialogueWheel.End();
					}
				}
				this.S.TalkTimer -= Time.deltaTime;
			}
			else if (this.S.Interaction == StudentInteractionType.ChangingAppearance)
			{
				if (this.S.TalkTimer == 3f)
				{
					this.S.Subtitle.UpdateLabel(SubtitleType.SuitorLove, 2, 3f);
					this.S.CharacterAnimation.CrossFade(this.S.Nod1Anim);
				}
				else
				{
					if (Input.GetButtonDown("A"))
					{
						this.S.TalkTimer = 0f;
					}
					if (this.S.CharacterAnimation[this.S.Nod1Anim].time >= this.S.CharacterAnimation[this.S.Nod1Anim].length)
					{
						this.S.CharacterAnimation.CrossFade(this.IdleAnim);
					}
					if (this.S.TalkTimer <= 0f)
					{
						this.S.DialogueWheel.End();
					}
				}
				this.S.TalkTimer -= Time.deltaTime;
			}
			else if (this.S.Interaction == StudentInteractionType.Court)
			{
				if (this.S.TalkTimer == 3f)
				{
					if (this.S.Male)
					{
						this.S.Subtitle.UpdateLabel(SubtitleType.SuitorLove, 3, 5f);
					}
					else
					{
						this.S.Subtitle.UpdateLabel(SubtitleType.SuitorLove, 4, 5f);
					}
					this.S.CharacterAnimation.CrossFade(this.S.Nod1Anim);
				}
				else
				{
					if (Input.GetButtonDown("A"))
					{
						this.S.TalkTimer = 0f;
					}
					if (this.S.CharacterAnimation[this.S.Nod1Anim].time >= this.S.CharacterAnimation[this.S.Nod1Anim].length)
					{
						this.S.CharacterAnimation.CrossFade(this.IdleAnim);
					}
					if (this.S.TalkTimer <= 0f)
					{
						this.S.MeetTime = this.S.Clock.HourTime - 1f;
						if (this.S.Male)
						{
							this.S.MeetSpot = this.S.StudentManager.SuitorSpot;
						}
						else
						{
							this.S.MeetSpot = this.S.StudentManager.RomanceSpot;
							this.S.StudentManager.LoveManager.RivalWaiting = true;
						}
						this.S.DialogueWheel.End();
					}
				}
				this.S.TalkTimer -= Time.deltaTime;
			}
			else if (this.S.Interaction == StudentInteractionType.Gift)
			{
				if (this.S.TalkTimer == 5f)
				{
					this.S.Subtitle.UpdateLabel(SubtitleType.SuitorLove, 5, 99f);
					this.S.CharacterAnimation.CrossFade(this.S.Nod1Anim);
				}
				else
				{
					if (Input.GetButtonDown("A"))
					{
						this.S.TalkTimer = 0f;
					}
					if (this.S.CharacterAnimation[this.S.Nod1Anim].time >= this.S.CharacterAnimation[this.S.Nod1Anim].length)
					{
						this.S.CharacterAnimation.CrossFade(this.IdleAnim);
					}
					if (this.S.TalkTimer <= 0f)
					{
						this.S.Rose = true;
						this.S.DialogueWheel.End();
					}
				}
				this.S.TalkTimer -= Time.deltaTime;
			}
			else if (this.S.Interaction == StudentInteractionType.Feeding)
			{
				Debug.Log("Feeding.");
				if (this.S.TalkTimer == 10f)
				{
					if (this.S.Club == ClubType.Delinquent)
					{
						this.S.CharacterAnimation.CrossFade(this.S.IdleAnim);
						this.S.Subtitle.UpdateLabel(SubtitleType.RejectFood, 1, 3f);
					}
					else if (this.S.Fed || this.S.Club == ClubType.Council || this.S.StudentID == 22)
					{
						this.S.CharacterAnimation.CrossFade(this.S.GossipAnim);
						this.S.Subtitle.UpdateLabel(SubtitleType.RejectFood, 0, 3f);
						this.S.Fed = true;
					}
					else
					{
						this.S.CharacterAnimation.CrossFade(this.S.Nod2Anim);
						this.S.Subtitle.UpdateLabel(SubtitleType.AcceptFood, 0, 3f);
						this.CalculateRepBonus();
						this.S.Reputation.PendingRep += 1f + (float)this.S.RepBonus;
						this.S.PendingRep += 1f + (float)this.S.RepBonus;
					}
				}
				else if (Input.GetButtonDown("A"))
				{
					this.S.TalkTimer = 0f;
				}
				if (this.S.CharacterAnimation[this.S.Nod2Anim].time >= this.S.CharacterAnimation[this.S.Nod2Anim].length)
				{
					this.S.CharacterAnimation.CrossFade(this.S.IdleAnim);
				}
				if (this.S.CharacterAnimation[this.S.GossipAnim].time >= this.S.CharacterAnimation[this.S.GossipAnim].length)
				{
					this.S.CharacterAnimation.CrossFade(this.S.IdleAnim);
				}
				this.S.TalkTimer -= Time.deltaTime;
				if (this.S.TalkTimer <= 0f)
				{
					if (!this.S.Fed && this.S.Club != ClubType.Delinquent)
					{
						this.S.Yandere.PickUp.FoodPieces[this.S.Yandere.PickUp.Food].SetActive(false);
						this.S.Yandere.PickUp.Food--;
						this.S.Fed = true;
					}
					this.S.DialogueWheel.End();
					this.S.StudentManager.UpdateStudents(0);
				}
			}
			else if (this.S.Interaction == StudentInteractionType.TaskInquiry)
			{
				if (this.S.TalkTimer == 10f)
				{
					this.S.CharacterAnimation.CrossFade("f02_embar_00");
					this.S.Subtitle.UpdateLabel(SubtitleType.TaskInquiry, this.S.StudentID - 80, 10f);
				}
				else if (Input.GetButtonDown("A"))
				{
					this.S.TalkTimer = 0f;
				}
				if (this.S.CharacterAnimation["f02_embar_00"].time >= this.S.CharacterAnimation["f02_embar_00"].length)
				{
					this.S.CharacterAnimation.CrossFade(this.IdleAnim);
				}
				this.S.TalkTimer -= Time.deltaTime;
				if (this.S.TalkTimer <= 0f)
				{
					this.S.StudentManager.TaskManager.GirlsQuestioned[this.S.StudentID - 80] = true;
					this.S.DialogueWheel.End();
				}
			}
			else if (this.S.Interaction == StudentInteractionType.TakingSnack)
			{
				Debug.Log(this.S.Name + " is reacting to being offered a snack.");
				if (this.S.TalkTimer == 5f)
				{
					bool flag = false;
					if (this.S.StudentID == this.S.StudentManager.RivalID && !this.S.Hungry)
					{
						Debug.Log("Osana is not hungry, so she is going to refuse the snack.");
						flag = true;
					}
					if (this.S.Club == ClubType.Delinquent)
					{
						this.S.CharacterAnimation.CrossFade(this.S.IdleAnim);
						this.S.Subtitle.UpdateLabel(SubtitleType.RejectFood, 1, 3f);
					}
					else if (this.S.Fed || this.S.Club == ClubType.Council || flag || this.S.StudentID == 22)
					{
						this.S.CharacterAnimation.CrossFade(this.S.GossipAnim);
						this.S.Subtitle.UpdateLabel(SubtitleType.RejectFood, 0, 3f);
						this.S.Fed = true;
						if (this.S.StudentID == this.S.StudentManager.RivalID)
						{
							this.S.Subtitle.UpdateLabel(SubtitleType.RejectFood, 2, 5f);
						}
						Debug.Log(this.S.Name + " has refused the snack.");
					}
					else
					{
						this.S.CharacterAnimation.CrossFade(this.S.Nod2Anim);
						this.S.Subtitle.UpdateLabel(SubtitleType.AcceptFood, 0, 10f);
						this.CalculateRepBonus();
						this.S.Reputation.PendingRep += 1f + (float)this.S.RepBonus;
						this.S.PendingRep += 1f + (float)this.S.RepBonus;
					}
				}
				else if (Input.GetButtonDown("A"))
				{
					this.S.TalkTimer = 0f;
				}
				if (this.S.CharacterAnimation[this.S.Nod2Anim].time >= this.S.CharacterAnimation[this.S.Nod2Anim].length)
				{
					this.S.CharacterAnimation.CrossFade(this.S.IdleAnim);
				}
				if (this.S.CharacterAnimation[this.S.GossipAnim].time >= this.S.CharacterAnimation[this.S.GossipAnim].length)
				{
					this.S.CharacterAnimation.CrossFade(this.S.IdleAnim);
				}
				this.S.TalkTimer -= Time.deltaTime;
				if (this.S.TalkTimer <= 0f)
				{
					if (!this.S.Fed && this.S.Club != ClubType.Delinquent)
					{
						if (this.S.StudentID == this.S.StudentManager.RivalID && SchemeGlobals.GetSchemeStage(4) == 5)
						{
							SchemeGlobals.SetSchemeStage(4, 6);
							this.S.Yandere.PauseScreen.Schemes.UpdateInstructions();
						}
						PickUpScript pickUp = this.S.Yandere.PickUp;
						this.S.Yandere.EmptyHands();
						this.S.EmptyHands();
						pickUp.GetComponent<MeshFilter>().mesh = this.S.StudentManager.OpenChipBag;
						pickUp.transform.parent = this.S.LeftItemParent;
						pickUp.transform.localPosition = new Vector3(-0.02f, -0.075f, 0f);
						pickUp.transform.localEulerAngles = new Vector3(-15f, -15f, 30f);
						pickUp.MyRigidbody.useGravity = false;
						pickUp.MyRigidbody.isKinematic = true;
						pickUp.Prompt.Hide();
						pickUp.Prompt.enabled = false;
						pickUp.enabled = false;
						this.S.BagOfChips = pickUp.gameObject;
						this.S.EatingSnack = true;
						this.S.Private = true;
						this.S.Hungry = false;
						this.S.Fed = true;
					}
					this.S.DialogueWheel.End();
					this.S.StudentManager.UpdateStudents(0);
				}
			}
			else if (this.S.Interaction == StudentInteractionType.GivingHelp)
			{
				if (this.S.TalkTimer == 4f)
				{
					if (this.S.Club == ClubType.Council || this.S.Club == ClubType.Delinquent)
					{
						this.S.CharacterAnimation.CrossFade(this.S.GossipAnim);
						this.S.Subtitle.UpdateLabel(SubtitleType.RejectHelp, 0, 4f);
					}
					else if (this.S.Yandere.Bloodiness > 0f)
					{
						this.S.CharacterAnimation.CrossFade(this.S.GossipAnim);
						this.S.Subtitle.UpdateLabel(SubtitleType.RejectHelp, 1, 4f);
					}
					else
					{
						this.S.CharacterAnimation.CrossFade(this.S.PullBoxCutterAnim);
						this.S.SmartPhone.SetActive(false);
						this.S.Subtitle.UpdateLabel(SubtitleType.GiveHelp, 0, 4f);
					}
				}
				else
				{
					Input.GetButtonDown("A");
				}
				if (this.S.CharacterAnimation[this.S.GossipAnim].time >= this.S.CharacterAnimation[this.S.GossipAnim].length)
				{
					this.S.CharacterAnimation.CrossFade(this.S.IdleAnim);
				}
				if (this.S.CharacterAnimation[this.S.PullBoxCutterAnim].time >= this.S.CharacterAnimation[this.S.PullBoxCutterAnim].length)
				{
					this.S.CharacterAnimation.CrossFade(this.S.IdleAnim);
				}
				this.S.TalkTimer -= Time.deltaTime;
				if (this.S.Club != ClubType.Council && this.S.Club != ClubType.Delinquent)
				{
					this.S.MoveTowardsTarget(this.S.Yandere.transform.position + this.S.Yandere.transform.forward * 0.75f);
					if (this.S.CharacterAnimation[this.S.PullBoxCutterAnim].time >= this.S.CharacterAnimation[this.S.PullBoxCutterAnim].length)
					{
						this.S.CharacterAnimation.CrossFade(this.S.IdleAnim);
						this.StuckBoxCutter = null;
					}
					else if (this.S.CharacterAnimation[this.S.PullBoxCutterAnim].time >= 2f)
					{
						if (this.StuckBoxCutter.transform.parent != this.S.RightEye)
						{
							this.StuckBoxCutter.Prompt.enabled = true;
							this.StuckBoxCutter.enabled = true;
							this.StuckBoxCutter.transform.parent = this.S.Yandere.PickUp.transform;
							this.StuckBoxCutter.transform.localPosition = new Vector3(0f, 0.19f, 0f);
							this.StuckBoxCutter.transform.localEulerAngles = new Vector3(0f, -90f, 0f);
						}
					}
					else if (this.S.CharacterAnimation[this.S.PullBoxCutterAnim].time >= 1.166666f && this.StuckBoxCutter == null)
					{
						this.StuckBoxCutter = this.S.Yandere.PickUp.StuckBoxCutter;
						this.S.Yandere.PickUp.StuckBoxCutter = null;
						this.StuckBoxCutter.FingerprintID = this.S.StudentID;
						this.StuckBoxCutter.transform.parent = this.S.RightHand;
						this.StuckBoxCutter.transform.localPosition = new Vector3(0f, 0f, 0f);
						this.StuckBoxCutter.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
					}
				}
				if (this.S.TalkTimer <= 0f)
				{
					this.S.DialogueWheel.End();
					this.S.StudentManager.UpdateStudents(0);
				}
			}
			else if (this.S.Interaction == StudentInteractionType.SentToLocker)
			{
				bool flag2 = false;
				if (this.S.Club == ClubType.Delinquent)
				{
					flag2 = true;
				}
				if (PlayerGlobals.GetStudentFriend(this.S.StudentID))
				{
					flag2 = false;
				}
				if (this.S.TalkTimer == 5f)
				{
					if (!flag2)
					{
						this.Refuse = false;
						if ((this.S.Clock.HourTime > 8f && this.S.Clock.HourTime < 13f) || (this.S.Clock.HourTime > 13.375f && this.S.Clock.HourTime < 15.5f))
						{
							this.S.CharacterAnimation.CrossFade(this.S.GossipAnim);
							this.S.Subtitle.UpdateLabel(SubtitleType.SendToLocker, 1, 5f);
							this.Refuse = true;
						}
						else if (this.S.Club == ClubType.Council)
						{
							this.S.CharacterAnimation.CrossFade(this.S.GossipAnim);
							this.S.Subtitle.UpdateLabel(SubtitleType.SendToLocker, 3, 5f);
							this.Refuse = true;
						}
						else
						{
							this.S.CharacterAnimation.CrossFade(this.S.Nod1Anim);
							this.S.Subtitle.UpdateLabel(SubtitleType.SendToLocker, 2, 5f);
						}
					}
					else
					{
						this.S.Subtitle.UpdateLabel(SubtitleType.Dismissive, 5, 5f);
					}
				}
				else
				{
					if (Input.GetButtonDown("A"))
					{
						this.S.TalkTimer = 0f;
					}
					if (this.S.CharacterAnimation[this.S.Nod1Anim].time >= this.S.CharacterAnimation[this.S.Nod1Anim].length)
					{
						this.S.CharacterAnimation.CrossFade(this.IdleAnim);
					}
					if (this.S.TalkTimer <= 0f)
					{
						if (!this.Refuse)
						{
							if (!flag2)
							{
								this.S.Pathfinding.speed = 4f;
								this.S.TargetDistance = 1f;
								this.S.SentToLocker = true;
								this.S.Routine = false;
								this.S.CanTalk = false;
							}
							else
							{
								this.S.Pathfinding.speed = 1f;
								this.S.TargetDistance = 0.5f;
								this.S.Routine = true;
								this.S.CanTalk = true;
							}
						}
						this.S.DialogueWheel.End();
					}
				}
				this.S.TalkTimer -= Time.deltaTime;
			}
			if (this.S.StudentID == 41 && !this.S.DialogueWheel.ClubLeader && this.S.Interaction != StudentInteractionType.ClubUnwelcome && this.S.TalkTimer > 0f)
			{
				Debug.Log("Geiju response.");
				if (this.NegativeResponse)
				{
					Debug.Log("Negative response.");
					this.S.Subtitle.UpdateLabel(SubtitleType.Impatience, 6, 5f);
				}
				else
				{
					this.S.Subtitle.UpdateLabel(SubtitleType.Impatience, 5, 5f);
				}
			}
			if (this.S.Waiting)
			{
				this.S.WaitTimer -= Time.deltaTime;
				if (this.S.WaitTimer <= 0f)
				{
					this.S.DialogueWheel.TaskManager.UpdateTaskStatus();
					this.S.Talking = false;
					this.S.Waiting = false;
					base.enabled = false;
					if (!this.Fake && !this.S.StudentManager.CombatMinigame.Practice)
					{
						this.S.Pathfinding.canSearch = true;
						this.S.Pathfinding.canMove = true;
						this.S.Obstacle.enabled = false;
						this.S.Alarmed = false;
						if (!this.S.Following && !this.S.Distracting && !this.S.Wet && !this.S.EatingSnack && !this.S.SentToLocker)
						{
							this.S.Routine = true;
						}
						if (!this.S.Following)
						{
							this.S.Hearts.emission.enabled = false;
						}
					}
					this.S.StudentManager.EnablePrompts();
					if (this.S.GoAway)
					{
						Debug.Log("This student was just told to go away.");
						this.S.CurrentDestination = this.S.StudentManager.GoAwaySpots.List[this.S.StudentID];
						this.S.Pathfinding.target = this.S.StudentManager.GoAwaySpots.List[this.S.StudentID];
						this.S.Pathfinding.canSearch = true;
						this.S.Pathfinding.canMove = true;
						this.S.DistanceToDestination = 100f;
						return;
					}
				}
			}
			else
			{
				this.S.targetRotation = Quaternion.LookRotation(new Vector3(this.S.Yandere.transform.position.x, base.transform.position.y, this.S.Yandere.transform.position.z) - base.transform.position);
				base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.S.targetRotation, 10f * Time.deltaTime);
			}
		}
	}

	// Token: 0x06001BF6 RID: 7158 RVA: 0x0014BEE4 File Offset: 0x0014A0E4
	private void CalculateRepBonus()
	{
		this.S.RepBonus = 0;
		if (PlayerGlobals.PantiesEquipped == 3)
		{
			this.S.RepBonus++;
		}
		if ((this.S.Male && PlayerGlobals.Seduction + PlayerGlobals.SeductionBonus > 0) || PlayerGlobals.Seduction == 5)
		{
			this.S.RepBonus++;
		}
		if (PlayerGlobals.SocialBonus > 0)
		{
			this.S.RepBonus++;
		}
		this.S.ChameleonCheck();
		if (this.S.Chameleon)
		{
			this.S.RepBonus++;
		}
	}

	// Token: 0x040033E7 RID: 13287
	private const float LongestTime = 100f;

	// Token: 0x040033E8 RID: 13288
	private const float LongTime = 5f;

	// Token: 0x040033E9 RID: 13289
	private const float MediumTime = 3f;

	// Token: 0x040033EA RID: 13290
	private const float ShortTime = 2f;

	// Token: 0x040033EB RID: 13291
	public StudentScript S;

	// Token: 0x040033EC RID: 13292
	public WeaponScript StuckBoxCutter;

	// Token: 0x040033ED RID: 13293
	public bool NegativeResponse;

	// Token: 0x040033EE RID: 13294
	public bool Follow;

	// Token: 0x040033EF RID: 13295
	public bool Grudge;

	// Token: 0x040033F0 RID: 13296
	public bool Refuse;

	// Token: 0x040033F1 RID: 13297
	public bool Fake;

	// Token: 0x040033F2 RID: 13298
	public string IdleAnim = "";

	// Token: 0x040033F3 RID: 13299
	public int ClubBonus;
}
