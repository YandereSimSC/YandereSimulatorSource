using System;
using UnityEngine;

// Token: 0x0200040C RID: 1036
public class SubtitleScript : MonoBehaviour
{
	// Token: 0x06001BDE RID: 7134 RVA: 0x00142B38 File Offset: 0x00140D38
	private void Awake()
	{
		this.Club3Info = this.SubClub3Info;
		this.ClubGreetings[3] = this.ClubGreetings[13];
		this.ClubUnwelcomes[3] = this.ClubUnwelcomes[13];
		this.ClubKicks[3] = this.ClubKicks[13];
		this.ClubJoins[3] = this.ClubJoins[13];
		this.ClubAccepts[3] = this.ClubAccepts[13];
		this.ClubRefuses[3] = this.ClubRefuses[13];
		this.ClubRejoins[3] = this.ClubRejoins[13];
		this.ClubExclusives[3] = this.ClubExclusives[13];
		this.ClubGrudges[3] = this.ClubGrudges[13];
		this.ClubQuits[3] = this.ClubQuits[13];
		this.ClubConfirms[3] = this.ClubConfirms[13];
		this.ClubDenies[3] = this.ClubDenies[13];
		this.ClubFarewells[3] = this.ClubFarewells[13];
		this.ClubActivities[3] = this.ClubActivities[13];
		this.ClubEarlies[3] = this.ClubEarlies[13];
		this.ClubLates[3] = this.ClubLates[13];
		this.ClubYeses[3] = this.ClubYeses[13];
		this.ClubNoes[3] = this.ClubNoes[13];
		this.Club3Clips = this.SubClub3Clips;
		this.ClubGreetingClips[3] = this.ClubGreetingClips[13];
		this.ClubUnwelcomeClips[3] = this.ClubUnwelcomeClips[13];
		this.ClubKickClips[3] = this.ClubKickClips[13];
		this.ClubJoinClips[3] = this.ClubJoinClips[13];
		this.ClubAcceptClips[3] = this.ClubAcceptClips[13];
		this.ClubRefuseClips[3] = this.ClubRefuseClips[13];
		this.ClubRejoinClips[3] = this.ClubRejoinClips[13];
		this.ClubExclusiveClips[3] = this.ClubExclusiveClips[13];
		this.ClubGrudgeClips[3] = this.ClubGrudgeClips[13];
		this.ClubQuitClips[3] = this.ClubQuitClips[13];
		this.ClubConfirmClips[3] = this.ClubConfirmClips[13];
		this.ClubDenyClips[3] = this.ClubDenyClips[13];
		this.ClubFarewellClips[3] = this.ClubFarewellClips[13];
		this.ClubActivityClips[3] = this.ClubActivityClips[13];
		this.ClubEarlyClips[3] = this.ClubEarlyClips[13];
		this.ClubLateClips[3] = this.ClubLateClips[13];
		this.ClubYesClips[3] = this.ClubYesClips[13];
		this.ClubNoClips[3] = this.ClubNoClips[13];
		this.SubtitleClipArrays = new SubtitleTypeAndAudioClipArrayDictionary
		{
			{
				SubtitleType.ClubAccept,
				new AudioClipArrayWrapper(this.ClubAcceptClips)
			},
			{
				SubtitleType.ClubActivity,
				new AudioClipArrayWrapper(this.ClubActivityClips)
			},
			{
				SubtitleType.ClubConfirm,
				new AudioClipArrayWrapper(this.ClubConfirmClips)
			},
			{
				SubtitleType.ClubDeny,
				new AudioClipArrayWrapper(this.ClubDenyClips)
			},
			{
				SubtitleType.ClubEarly,
				new AudioClipArrayWrapper(this.ClubEarlyClips)
			},
			{
				SubtitleType.ClubExclusive,
				new AudioClipArrayWrapper(this.ClubExclusiveClips)
			},
			{
				SubtitleType.ClubFarewell,
				new AudioClipArrayWrapper(this.ClubFarewellClips)
			},
			{
				SubtitleType.ClubGreeting,
				new AudioClipArrayWrapper(this.ClubGreetingClips)
			},
			{
				SubtitleType.ClubGrudge,
				new AudioClipArrayWrapper(this.ClubGrudgeClips)
			},
			{
				SubtitleType.ClubJoin,
				new AudioClipArrayWrapper(this.ClubJoinClips)
			},
			{
				SubtitleType.ClubKick,
				new AudioClipArrayWrapper(this.ClubKickClips)
			},
			{
				SubtitleType.ClubLate,
				new AudioClipArrayWrapper(this.ClubLateClips)
			},
			{
				SubtitleType.ClubNo,
				new AudioClipArrayWrapper(this.ClubNoClips)
			},
			{
				SubtitleType.ClubPlaceholderInfo,
				new AudioClipArrayWrapper(this.Club0Clips)
			},
			{
				SubtitleType.ClubCookingInfo,
				new AudioClipArrayWrapper(this.Club1Clips)
			},
			{
				SubtitleType.ClubDramaInfo,
				new AudioClipArrayWrapper(this.Club2Clips)
			},
			{
				SubtitleType.ClubOccultInfo,
				new AudioClipArrayWrapper(this.Club3Clips)
			},
			{
				SubtitleType.ClubArtInfo,
				new AudioClipArrayWrapper(this.Club4Clips)
			},
			{
				SubtitleType.ClubLightMusicInfo,
				new AudioClipArrayWrapper(this.Club5Clips)
			},
			{
				SubtitleType.ClubMartialArtsInfo,
				new AudioClipArrayWrapper(this.Club6Clips)
			},
			{
				SubtitleType.ClubPhotoInfoLight,
				new AudioClipArrayWrapper(this.Club7ClipsLight)
			},
			{
				SubtitleType.ClubPhotoInfoDark,
				new AudioClipArrayWrapper(this.Club7ClipsDark)
			},
			{
				SubtitleType.ClubScienceInfo,
				new AudioClipArrayWrapper(this.Club8Clips)
			},
			{
				SubtitleType.ClubSportsInfo,
				new AudioClipArrayWrapper(this.Club9Clips)
			},
			{
				SubtitleType.ClubGardenInfo,
				new AudioClipArrayWrapper(this.Club10Clips)
			},
			{
				SubtitleType.ClubGamingInfo,
				new AudioClipArrayWrapper(this.Club11Clips)
			},
			{
				SubtitleType.ClubDelinquentInfo,
				new AudioClipArrayWrapper(this.Club12Clips)
			},
			{
				SubtitleType.ClubQuit,
				new AudioClipArrayWrapper(this.ClubQuitClips)
			},
			{
				SubtitleType.ClubRefuse,
				new AudioClipArrayWrapper(this.ClubRefuseClips)
			},
			{
				SubtitleType.ClubRejoin,
				new AudioClipArrayWrapper(this.ClubRejoinClips)
			},
			{
				SubtitleType.ClubUnwelcome,
				new AudioClipArrayWrapper(this.ClubUnwelcomeClips)
			},
			{
				SubtitleType.ClubYes,
				new AudioClipArrayWrapper(this.ClubYesClips)
			},
			{
				SubtitleType.ClubPractice,
				new AudioClipArrayWrapper(this.ClubPracticeClips)
			},
			{
				SubtitleType.ClubPracticeYes,
				new AudioClipArrayWrapper(this.ClubPracticeYesClips)
			},
			{
				SubtitleType.ClubPracticeNo,
				new AudioClipArrayWrapper(this.ClubPracticeNoClips)
			},
			{
				SubtitleType.DrownReaction,
				new AudioClipArrayWrapper(this.DrownReactionClips)
			},
			{
				SubtitleType.EavesdropReaction,
				new AudioClipArrayWrapper(this.EavesdropClips)
			},
			{
				SubtitleType.RejectFood,
				new AudioClipArrayWrapper(this.FoodRejectionClips)
			},
			{
				SubtitleType.ViolenceReaction,
				new AudioClipArrayWrapper(this.ViolenceClips)
			},
			{
				SubtitleType.EventEavesdropReaction,
				new AudioClipArrayWrapper(this.EventEavesdropClips)
			},
			{
				SubtitleType.RivalEavesdropReaction,
				new AudioClipArrayWrapper(this.RivalEavesdropClips)
			},
			{
				SubtitleType.GrudgeWarning,
				new AudioClipArrayWrapper(this.GrudgeWarningClips)
			},
			{
				SubtitleType.LightSwitchReaction,
				new AudioClipArrayWrapper(this.LightSwitchClips)
			},
			{
				SubtitleType.LostPhone,
				new AudioClipArrayWrapper(this.LostPhoneClips)
			},
			{
				SubtitleType.NoteReaction,
				new AudioClipArrayWrapper(this.NoteReactionClips)
			},
			{
				SubtitleType.NoteReactionMale,
				new AudioClipArrayWrapper(this.NoteReactionMaleClips)
			},
			{
				SubtitleType.PickpocketReaction,
				new AudioClipArrayWrapper(this.PickpocketReactionClips)
			},
			{
				SubtitleType.RivalLostPhone,
				new AudioClipArrayWrapper(this.RivalLostPhoneClips)
			},
			{
				SubtitleType.RivalPickpocketReaction,
				new AudioClipArrayWrapper(this.RivalPickpocketReactionClips)
			},
			{
				SubtitleType.RivalSplashReaction,
				new AudioClipArrayWrapper(this.RivalSplashReactionClips)
			},
			{
				SubtitleType.SenpaiBloodReaction,
				new AudioClipArrayWrapper(this.SenpaiBloodReactionClips)
			},
			{
				SubtitleType.SenpaiInsanityReaction,
				new AudioClipArrayWrapper(this.SenpaiInsanityReactionClips)
			},
			{
				SubtitleType.SenpaiLewdReaction,
				new AudioClipArrayWrapper(this.SenpaiLewdReactionClips)
			},
			{
				SubtitleType.SenpaiMurderReaction,
				new AudioClipArrayWrapper(this.SenpaiMurderReactionClips)
			},
			{
				SubtitleType.SenpaiStalkingReaction,
				new AudioClipArrayWrapper(this.SenpaiStalkingReactionClips)
			},
			{
				SubtitleType.SenpaiWeaponReaction,
				new AudioClipArrayWrapper(this.SenpaiWeaponReactionClips)
			},
			{
				SubtitleType.SenpaiViolenceReaction,
				new AudioClipArrayWrapper(this.SenpaiViolenceReactionClips)
			},
			{
				SubtitleType.SenpaiRivalDeathReaction,
				new AudioClipArrayWrapper(this.SenpaiRivalDeathReactionClips)
			},
			{
				SubtitleType.RaibaruRivalDeathReaction,
				new AudioClipArrayWrapper(this.RaibaruRivalDeathReactionClips)
			},
			{
				SubtitleType.SplashReaction,
				new AudioClipArrayWrapper(this.SplashReactionClips)
			},
			{
				SubtitleType.SplashReactionMale,
				new AudioClipArrayWrapper(this.SplashReactionMaleClips)
			},
			{
				SubtitleType.Task6Line,
				new AudioClipArrayWrapper(this.Task6Clips)
			},
			{
				SubtitleType.Task7Line,
				new AudioClipArrayWrapper(this.Task7Clips)
			},
			{
				SubtitleType.Task8Line,
				new AudioClipArrayWrapper(this.Task8Clips)
			},
			{
				SubtitleType.Task11Line,
				new AudioClipArrayWrapper(this.Task11Clips)
			},
			{
				SubtitleType.Task13Line,
				new AudioClipArrayWrapper(this.Task13Clips)
			},
			{
				SubtitleType.Task14Line,
				new AudioClipArrayWrapper(this.Task14Clips)
			},
			{
				SubtitleType.Task15Line,
				new AudioClipArrayWrapper(this.Task15Clips)
			},
			{
				SubtitleType.Task25Line,
				new AudioClipArrayWrapper(this.Task25Clips)
			},
			{
				SubtitleType.Task28Line,
				new AudioClipArrayWrapper(this.Task28Clips)
			},
			{
				SubtitleType.Task30Line,
				new AudioClipArrayWrapper(this.Task30Clips)
			},
			{
				SubtitleType.Task34Line,
				new AudioClipArrayWrapper(this.Task34Clips)
			},
			{
				SubtitleType.Task36Line,
				new AudioClipArrayWrapper(this.Task36Clips)
			},
			{
				SubtitleType.Task37Line,
				new AudioClipArrayWrapper(this.Task37Clips)
			},
			{
				SubtitleType.Task38Line,
				new AudioClipArrayWrapper(this.Task38Clips)
			},
			{
				SubtitleType.Task52Line,
				new AudioClipArrayWrapper(this.Task52Clips)
			},
			{
				SubtitleType.Task76Line,
				new AudioClipArrayWrapper(this.Task76Clips)
			},
			{
				SubtitleType.Task77Line,
				new AudioClipArrayWrapper(this.Task77Clips)
			},
			{
				SubtitleType.Task78Line,
				new AudioClipArrayWrapper(this.Task78Clips)
			},
			{
				SubtitleType.Task79Line,
				new AudioClipArrayWrapper(this.Task79Clips)
			},
			{
				SubtitleType.Task80Line,
				new AudioClipArrayWrapper(this.Task80Clips)
			},
			{
				SubtitleType.Task81Line,
				new AudioClipArrayWrapper(this.Task81Clips)
			},
			{
				SubtitleType.TaskGenericLineMale,
				new AudioClipArrayWrapper(this.TaskGenericMaleClips)
			},
			{
				SubtitleType.TaskGenericLineFemale,
				new AudioClipArrayWrapper(this.TaskGenericFemaleClips)
			},
			{
				SubtitleType.TaskInquiry,
				new AudioClipArrayWrapper(this.TaskInquiryClips)
			},
			{
				SubtitleType.TeacherAttackReaction,
				new AudioClipArrayWrapper(this.TeacherAttackClips)
			},
			{
				SubtitleType.TeacherBloodHostile,
				new AudioClipArrayWrapper(this.TeacherBloodHostileClips)
			},
			{
				SubtitleType.TeacherBloodReaction,
				new AudioClipArrayWrapper(this.TeacherBloodClips)
			},
			{
				SubtitleType.TeacherCorpseInspection,
				new AudioClipArrayWrapper(this.TeacherInspectClips)
			},
			{
				SubtitleType.TeacherCorpseReaction,
				new AudioClipArrayWrapper(this.TeacherCorpseClips)
			},
			{
				SubtitleType.TeacherInsanityHostile,
				new AudioClipArrayWrapper(this.TeacherInsanityHostileClips)
			},
			{
				SubtitleType.TeacherInsanityReaction,
				new AudioClipArrayWrapper(this.TeacherInsanityClips)
			},
			{
				SubtitleType.TeacherLateReaction,
				new AudioClipArrayWrapper(this.TeacherLateClips)
			},
			{
				SubtitleType.TeacherLewdReaction,
				new AudioClipArrayWrapper(this.TeacherLewdClips)
			},
			{
				SubtitleType.TeacherMurderReaction,
				new AudioClipArrayWrapper(this.TeacherMurderClips)
			},
			{
				SubtitleType.TeacherPoliceReport,
				new AudioClipArrayWrapper(this.TeacherPoliceClips)
			},
			{
				SubtitleType.TeacherPrankReaction,
				new AudioClipArrayWrapper(this.TeacherPrankClips)
			},
			{
				SubtitleType.TeacherReportReaction,
				new AudioClipArrayWrapper(this.TeacherReportClips)
			},
			{
				SubtitleType.TeacherTheftReaction,
				new AudioClipArrayWrapper(this.TeacherTheftClips)
			},
			{
				SubtitleType.TeacherTrespassingReaction,
				new AudioClipArrayWrapper(this.TeacherTrespassClips)
			},
			{
				SubtitleType.TeacherWeaponHostile,
				new AudioClipArrayWrapper(this.TeacherWeaponHostileClips)
			},
			{
				SubtitleType.TeacherWeaponReaction,
				new AudioClipArrayWrapper(this.TeacherWeaponClips)
			},
			{
				SubtitleType.TeacherCoverUpHostile,
				new AudioClipArrayWrapper(this.TeacherCoverUpHostileClips)
			},
			{
				SubtitleType.YandereWhimper,
				new AudioClipArrayWrapper(this.YandereWhimperClips)
			},
			{
				SubtitleType.DelinquentAnnoy,
				new AudioClipArrayWrapper(this.DelinquentAnnoyClips)
			},
			{
				SubtitleType.DelinquentCase,
				new AudioClipArrayWrapper(this.DelinquentCaseClips)
			},
			{
				SubtitleType.DelinquentShove,
				new AudioClipArrayWrapper(this.DelinquentShoveClips)
			},
			{
				SubtitleType.DelinquentReaction,
				new AudioClipArrayWrapper(this.DelinquentReactionClips)
			},
			{
				SubtitleType.DelinquentWeaponReaction,
				new AudioClipArrayWrapper(this.DelinquentWeaponReactionClips)
			},
			{
				SubtitleType.DelinquentThreatened,
				new AudioClipArrayWrapper(this.DelinquentThreatenedClips)
			},
			{
				SubtitleType.DelinquentTaunt,
				new AudioClipArrayWrapper(this.DelinquentTauntClips)
			},
			{
				SubtitleType.DelinquentCalm,
				new AudioClipArrayWrapper(this.DelinquentCalmClips)
			},
			{
				SubtitleType.DelinquentFight,
				new AudioClipArrayWrapper(this.DelinquentFightClips)
			},
			{
				SubtitleType.DelinquentAvenge,
				new AudioClipArrayWrapper(this.DelinquentAvengeClips)
			},
			{
				SubtitleType.DelinquentWin,
				new AudioClipArrayWrapper(this.DelinquentWinClips)
			},
			{
				SubtitleType.DelinquentSurrender,
				new AudioClipArrayWrapper(this.DelinquentSurrenderClips)
			},
			{
				SubtitleType.DelinquentNoSurrender,
				new AudioClipArrayWrapper(this.DelinquentNoSurrenderClips)
			},
			{
				SubtitleType.DelinquentMurderReaction,
				new AudioClipArrayWrapper(this.DelinquentMurderReactionClips)
			},
			{
				SubtitleType.DelinquentCorpseReaction,
				new AudioClipArrayWrapper(this.DelinquentCorpseReactionClips)
			},
			{
				SubtitleType.DelinquentFriendCorpseReaction,
				new AudioClipArrayWrapper(this.DelinquentFriendCorpseReactionClips)
			},
			{
				SubtitleType.DelinquentResume,
				new AudioClipArrayWrapper(this.DelinquentResumeClips)
			},
			{
				SubtitleType.DelinquentFlee,
				new AudioClipArrayWrapper(this.DelinquentFleeClips)
			},
			{
				SubtitleType.DelinquentEnemyFlee,
				new AudioClipArrayWrapper(this.DelinquentEnemyFleeClips)
			},
			{
				SubtitleType.DelinquentFriendFlee,
				new AudioClipArrayWrapper(this.DelinquentFriendFleeClips)
			},
			{
				SubtitleType.DelinquentInjuredFlee,
				new AudioClipArrayWrapper(this.DelinquentInjuredFleeClips)
			},
			{
				SubtitleType.DelinquentCheer,
				new AudioClipArrayWrapper(this.DelinquentCheerClips)
			},
			{
				SubtitleType.DelinquentHmm,
				new AudioClipArrayWrapper(this.DelinquentHmmClips)
			},
			{
				SubtitleType.DelinquentGrudge,
				new AudioClipArrayWrapper(this.DelinquentGrudgeClips)
			},
			{
				SubtitleType.Dismissive,
				new AudioClipArrayWrapper(this.DismissiveClips)
			},
			{
				SubtitleType.EvilDelinquentCorpseReaction,
				new AudioClipArrayWrapper(this.EvilDelinquentCorpseReactionClips)
			},
			{
				SubtitleType.Eulogy,
				new AudioClipArrayWrapper(this.EulogyClips)
			},
			{
				SubtitleType.GasWarning,
				new AudioClipArrayWrapper(this.GasWarningClips)
			},
			{
				SubtitleType.ObstacleMurderReaction,
				new AudioClipArrayWrapper(this.ObstacleMurderReactionClips)
			},
			{
				SubtitleType.ObstaclePoisonReaction,
				new AudioClipArrayWrapper(this.ObstaclePoisonReactionClips)
			}
		};
	}

	// Token: 0x06001BDF RID: 7135 RVA: 0x00143875 File Offset: 0x00141A75
	private void Start()
	{
		this.Label.text = string.Empty;
	}

	// Token: 0x06001BE0 RID: 7136 RVA: 0x00143887 File Offset: 0x00141A87
	private string GetRandomString(string[] strings)
	{
		return strings[UnityEngine.Random.Range(0, strings.Length)];
	}

	// Token: 0x06001BE1 RID: 7137 RVA: 0x00143894 File Offset: 0x00141A94
	public void UpdateLabel(SubtitleType subtitleType, int ID, float Duration)
	{
		if (subtitleType == SubtitleType.WeaponAndBloodAndInsanityReaction)
		{
			this.Label.text = this.GetRandomString(this.WeaponBloodInsanityReactions);
		}
		else if (subtitleType == SubtitleType.WeaponAndBloodReaction)
		{
			this.Label.text = this.GetRandomString(this.WeaponBloodReactions);
		}
		else if (subtitleType == SubtitleType.WeaponAndInsanityReaction)
		{
			this.Label.text = this.GetRandomString(this.WeaponInsanityReactions);
		}
		else if (subtitleType == SubtitleType.BloodAndInsanityReaction)
		{
			this.Label.text = this.GetRandomString(this.BloodInsanityReactions);
		}
		else if (subtitleType == SubtitleType.WeaponReaction)
		{
			if (ID == 1)
			{
				this.Label.text = this.GetRandomString(this.KnifeReactions);
			}
			else if (ID == 2)
			{
				this.Label.text = this.GetRandomString(this.KatanaReactions);
			}
			else if (ID == 3)
			{
				this.Label.text = this.GetRandomString(this.SyringeReactions);
			}
			else if (ID == 7)
			{
				this.Label.text = this.GetRandomString(this.SawReactions);
			}
			else if (ID == 8)
			{
				if (this.StudentID < 31 || this.StudentID > 35)
				{
					this.Label.text = this.RitualReactions[0];
				}
				else
				{
					this.Label.text = this.RitualReactions[1];
				}
			}
			else if (ID == 9)
			{
				this.Label.text = this.GetRandomString(this.BatReactions);
			}
			else if (ID == 10)
			{
				this.Label.text = this.GetRandomString(this.ShovelReactions);
			}
			else if (ID == 11 || ID == 14 || ID == 16 || ID == 17 || ID == 22)
			{
				this.Label.text = this.GetRandomString(this.PropReactions);
			}
			else if (ID == 12)
			{
				this.Label.text = this.GetRandomString(this.DumbbellReactions);
			}
			else if (ID == 13 || ID == 15)
			{
				this.Label.text = this.GetRandomString(this.AxeReactions);
			}
			else if (ID > 17 && ID < 22)
			{
				this.Label.text = this.GetRandomString(this.DelinkWeaponReactions);
			}
			else if (ID == 23)
			{
				this.Label.text = this.GetRandomString(this.ExtinguisherReactions);
			}
			else if (ID == 24)
			{
				this.Label.text = this.GetRandomString(this.WrenchReactions);
			}
			else if (ID == 25)
			{
				this.Label.text = this.GetRandomString(this.GuitarReactions);
			}
		}
		else if (subtitleType == SubtitleType.BloodReaction)
		{
			this.Label.text = this.GetRandomString(this.BloodReactions);
		}
		else if (subtitleType == SubtitleType.BloodPoolReaction)
		{
			this.Label.text = this.BloodPoolReactions[ID];
		}
		else if (subtitleType == SubtitleType.BloodyWeaponReaction)
		{
			this.Label.text = this.BloodyWeaponReactions[ID];
		}
		else if (subtitleType == SubtitleType.LimbReaction)
		{
			this.Label.text = this.LimbReactions[ID];
		}
		else if (subtitleType == SubtitleType.WetBloodReaction)
		{
			this.Label.text = this.GetRandomString(this.WetBloodReactions);
		}
		else if (subtitleType == SubtitleType.InsanityReaction)
		{
			this.Label.text = this.GetRandomString(this.InsanityReactions);
		}
		else if (subtitleType == SubtitleType.LewdReaction)
		{
			this.Label.text = this.GetRandomString(this.LewdReactions);
		}
		else if (subtitleType == SubtitleType.SuspiciousReaction)
		{
			this.Label.text = this.SuspiciousReactions[ID];
		}
		else if (subtitleType == SubtitleType.PoisonReaction)
		{
			this.Label.text = this.PoisonReactions[ID];
		}
		else if (subtitleType == SubtitleType.PrankReaction)
		{
			this.Label.text = this.GetRandomString(this.PrankReactions);
		}
		else if (subtitleType == SubtitleType.InterruptionReaction)
		{
			this.Label.text = this.InterruptReactions[ID];
		}
		else if (subtitleType == SubtitleType.IntrusionReaction)
		{
			this.Label.text = this.GetRandomString(this.IntrusionReactions);
		}
		else if (subtitleType == SubtitleType.TheftReaction)
		{
			this.Label.text = this.GetRandomString(this.TheftReactions);
		}
		else if (subtitleType == SubtitleType.KilledMood)
		{
			this.Label.text = this.GetRandomString(this.KilledMoods);
		}
		else if (subtitleType == SubtitleType.SendToLocker)
		{
			this.Label.text = this.SendToLockers[ID];
		}
		else if (subtitleType == SubtitleType.NoteReaction)
		{
			this.Label.text = this.NoteReactions[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.NoteReactionMale)
		{
			this.Label.text = this.NoteReactionsMale[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.OfferSnack)
		{
			this.Label.text = this.OfferSnacks[ID];
		}
		else if (subtitleType == SubtitleType.AcceptFood)
		{
			this.Label.text = this.GetRandomString(this.FoodAccepts);
		}
		else if (subtitleType == SubtitleType.RejectFood)
		{
			this.Label.text = this.FoodRejects[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.EavesdropReaction)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.EavesdropReactions.Length);
			this.Label.text = this.EavesdropReactions[this.RandomID];
		}
		else if (subtitleType == SubtitleType.ViolenceReaction)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.ViolenceReactions.Length);
			this.Label.text = this.ViolenceReactions[this.RandomID];
		}
		else if (subtitleType == SubtitleType.EventEavesdropReaction)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.EventEavesdropReactions.Length);
			this.Label.text = this.EventEavesdropReactions[this.RandomID];
		}
		else if (subtitleType == SubtitleType.RivalEavesdropReaction)
		{
			Debug.Log("Rival eavesdrop reaction. ID is: " + ID);
			this.Label.text = this.RivalEavesdropReactions[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.PickpocketReaction)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.PickpocketReactions.Length);
			this.Label.text = this.PickpocketReactions[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.PickpocketApology)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.PickpocketApologies.Length);
			this.Label.text = this.PickpocketApologies[this.RandomID];
		}
		else if (subtitleType == SubtitleType.CleaningApology)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.CleaningApologies.Length);
			this.Label.text = this.CleaningApologies[this.RandomID];
		}
		else if (subtitleType == SubtitleType.PoisonApology)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.PoisonApologies.Length);
			this.Label.text = this.PoisonApologies[this.RandomID];
		}
		else if (subtitleType == SubtitleType.HoldingBloodyClothingApology)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.HoldingBloodyClothingApologies.Length);
			this.Label.text = this.HoldingBloodyClothingApologies[this.RandomID];
		}
		else if (subtitleType == SubtitleType.RivalPickpocketReaction)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.RivalPickpocketReactions.Length);
			this.Label.text = this.RivalPickpocketReactions[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.DrownReaction)
		{
			this.Label.text = this.DrownReactions[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.HmmReaction)
		{
			if (this.Label.text == string.Empty)
			{
				this.RandomID = UnityEngine.Random.Range(0, this.HmmReactions.Length);
				this.Label.text = this.HmmReactions[this.RandomID];
			}
		}
		else if (subtitleType == SubtitleType.HoldingBloodyClothingReaction)
		{
			if (this.Label.text == string.Empty)
			{
				this.RandomID = UnityEngine.Random.Range(0, this.HoldingBloodyClothingReactions.Length);
				this.Label.text = this.HoldingBloodyClothingReactions[this.RandomID];
			}
		}
		else if (subtitleType == SubtitleType.ParanoidReaction)
		{
			if (this.Label.text == string.Empty)
			{
				this.RandomID = UnityEngine.Random.Range(0, this.ParanoidReactions.Length);
				this.Label.text = this.ParanoidReactions[this.RandomID];
			}
		}
		else if (subtitleType == SubtitleType.TeacherWeaponReaction)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.TeacherWeaponReactions.Length);
			this.Label.text = this.TeacherWeaponReactions[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.TeacherBloodReaction)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.TeacherBloodReactions.Length);
			this.Label.text = this.TeacherBloodReactions[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.TeacherInsanityReaction)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.TeacherInsanityReactions.Length);
			this.Label.text = this.TeacherInsanityReactions[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.TeacherWeaponHostile)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.TeacherWeaponHostiles.Length);
			this.Label.text = this.TeacherWeaponHostiles[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.TeacherBloodHostile)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.TeacherBloodHostiles.Length);
			this.Label.text = this.TeacherBloodHostiles[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.TeacherInsanityHostile)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.TeacherInsanityHostiles.Length);
			this.Label.text = this.TeacherInsanityHostiles[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.TeacherCoverUpHostile)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.TeacherCoverUpHostiles.Length);
			this.Label.text = this.TeacherCoverUpHostiles[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.TeacherLewdReaction)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.TeacherLewdReactions.Length);
			this.Label.text = this.TeacherLewdReactions[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.TeacherTrespassingReaction)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.TeacherTrespassReactions.Length);
			this.Label.text = this.TeacherTrespassReactions[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.TeacherLateReaction)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.TeacherLateReactions.Length);
			this.Label.text = this.TeacherLateReactions[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.TeacherReportReaction)
		{
			this.Label.text = this.TeacherReportReactions[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.TeacherCorpseReaction)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.TeacherCorpseReactions.Length);
			this.Label.text = this.TeacherCorpseReactions[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.TeacherCorpseInspection)
		{
			this.Label.text = this.TeacherCorpseInspections[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.TeacherPoliceReport)
		{
			this.Label.text = this.TeacherPoliceReports[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.TeacherAttackReaction)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.TeacherAttackReactions.Length);
			this.Label.text = this.TeacherAttackReactions[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.TeacherMurderReaction)
		{
			this.Label.text = this.TeacherMurderReactions[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.TeacherPrankReaction)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.TeacherPrankReactions.Length);
			this.Label.text = this.TeacherPrankReactions[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.TeacherTheftReaction)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.TeacherTheftReactions.Length);
			this.Label.text = this.TeacherTheftReactions[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.DelinquentAnnoy)
		{
			this.Label.text = this.DelinquentAnnoys[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.DelinquentCase)
		{
			this.Label.text = this.DelinquentCases[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.DelinquentShove)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.DelinquentShoves.Length);
			this.Label.text = this.DelinquentShoves[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.DelinquentReaction)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.DelinquentReactions.Length);
			this.Label.text = this.DelinquentReactions[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.DelinquentWeaponReaction)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.DelinquentWeaponReactions.Length);
			this.Label.text = this.DelinquentWeaponReactions[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.DelinquentThreatened)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.DelinquentThreateneds.Length);
			this.Label.text = this.DelinquentThreateneds[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.DelinquentTaunt)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.DelinquentTaunts.Length);
			this.Label.text = this.DelinquentTaunts[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.DelinquentCalm)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.DelinquentCalms.Length);
			this.Label.text = this.DelinquentCalms[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.DelinquentFight)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.DelinquentFights.Length);
			this.Label.text = this.DelinquentFights[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.DelinquentAvenge)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.DelinquentAvenges.Length);
			this.Label.text = this.DelinquentAvenges[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.DelinquentWin)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.DelinquentWins.Length);
			this.Label.text = this.DelinquentWins[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.DelinquentSurrender)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.DelinquentSurrenders.Length);
			this.Label.text = this.DelinquentSurrenders[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.DelinquentNoSurrender)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.DelinquentNoSurrenders.Length);
			this.Label.text = this.DelinquentNoSurrenders[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.DelinquentMurderReaction)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.DelinquentMurderReactions.Length);
			this.Label.text = this.DelinquentMurderReactions[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.DelinquentCorpseReaction)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.DelinquentCorpseReactions.Length);
			this.Label.text = this.DelinquentCorpseReactions[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.DelinquentFriendCorpseReaction)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.DelinquentFriendCorpseReactions.Length);
			this.Label.text = this.DelinquentFriendCorpseReactions[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.DelinquentResume)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.DelinquentResumes.Length);
			this.Label.text = this.DelinquentResumes[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.DelinquentFlee)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.DelinquentFlees.Length);
			this.Label.text = this.DelinquentFlees[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.DelinquentEnemyFlee)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.DelinquentEnemyFlees.Length);
			this.Label.text = this.DelinquentEnemyFlees[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.DelinquentFriendFlee)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.DelinquentFriendFlees.Length);
			this.Label.text = this.DelinquentFriendFlees[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.DelinquentInjuredFlee)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.DelinquentInjuredFlees.Length);
			this.Label.text = this.DelinquentInjuredFlees[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.DelinquentCheer)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.DelinquentCheers.Length);
			this.Label.text = this.DelinquentCheers[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.DelinquentHmm)
		{
			if (this.Label.text == string.Empty)
			{
				this.RandomID = UnityEngine.Random.Range(0, this.DelinquentHmms.Length);
				this.Label.text = this.DelinquentHmms[this.RandomID];
				this.PlayVoice(subtitleType, this.RandomID);
			}
		}
		else if (subtitleType == SubtitleType.DelinquentGrudge)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.DelinquentGrudges.Length);
			this.Label.text = this.DelinquentGrudges[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.Dismissive)
		{
			this.Label.text = this.Dismissives[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.LostPhone)
		{
			this.Label.text = this.LostPhones[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.RivalLostPhone)
		{
			this.Label.text = this.RivalLostPhones[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.MurderReaction)
		{
			this.Label.text = this.GetRandomString(this.MurderReactions);
		}
		else if (subtitleType == SubtitleType.CorpseReaction)
		{
			this.Label.text = this.CorpseReactions[ID];
		}
		else if (subtitleType == SubtitleType.CouncilCorpseReaction)
		{
			this.Label.text = this.CouncilCorpseReactions[ID];
		}
		else if (subtitleType == SubtitleType.CouncilToCounselor)
		{
			this.Label.text = this.CouncilToCounselors[ID];
		}
		else if (subtitleType == SubtitleType.LonerMurderReaction)
		{
			this.Label.text = this.GetRandomString(this.LonerMurderReactions);
		}
		else if (subtitleType == SubtitleType.LonerCorpseReaction)
		{
			this.Label.text = this.GetRandomString(this.LonerCorpseReactions);
		}
		else if (subtitleType == SubtitleType.PetBloodReport)
		{
			this.Label.text = this.PetBloodReports[ID];
		}
		else if (subtitleType == SubtitleType.PetBloodReaction)
		{
			this.Label.text = this.GetRandomString(this.PetBloodReactions);
		}
		else if (subtitleType == SubtitleType.PetCorpseReport)
		{
			this.Label.text = this.PetCorpseReports[ID];
		}
		else if (subtitleType == SubtitleType.PetCorpseReaction)
		{
			this.Label.text = this.GetRandomString(this.PetCorpseReactions);
		}
		else if (subtitleType == SubtitleType.PetLimbReport)
		{
			this.Label.text = this.PetLimbReports[ID];
		}
		else if (subtitleType == SubtitleType.PetLimbReaction)
		{
			this.Label.text = this.GetRandomString(this.PetLimbReactions);
		}
		else if (subtitleType == SubtitleType.PetMurderReport)
		{
			this.Label.text = this.PetMurderReports[ID];
		}
		else if (subtitleType == SubtitleType.PetMurderReaction)
		{
			this.Label.text = this.GetRandomString(this.PetMurderReactions);
		}
		else if (subtitleType == SubtitleType.PetWeaponReport)
		{
			this.Label.text = this.PetWeaponReports[ID];
		}
		else if (subtitleType == SubtitleType.PetWeaponReaction)
		{
			this.Label.text = this.PetWeaponReactions[ID];
		}
		else if (subtitleType == SubtitleType.PetBloodyWeaponReport)
		{
			this.Label.text = this.PetBloodyWeaponReports[ID];
		}
		else if (subtitleType == SubtitleType.PetBloodyWeaponReaction)
		{
			this.Label.text = this.GetRandomString(this.PetBloodyWeaponReactions);
		}
		else if (subtitleType == SubtitleType.EvilCorpseReaction)
		{
			this.Label.text = this.GetRandomString(this.EvilCorpseReactions);
		}
		else if (subtitleType == SubtitleType.EvilDelinquentCorpseReaction)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.EvilDelinquentCorpseReactions.Length);
			this.Label.text = this.EvilDelinquentCorpseReactions[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.HeroMurderReaction)
		{
			this.Label.text = this.GetRandomString(this.HeroMurderReactions);
		}
		else if (subtitleType == SubtitleType.CowardMurderReaction)
		{
			this.Label.text = this.GetRandomString(this.CowardMurderReactions);
		}
		else if (subtitleType == SubtitleType.EvilMurderReaction)
		{
			this.Label.text = this.GetRandomString(this.EvilMurderReactions);
		}
		else if (subtitleType == SubtitleType.SocialDeathReaction)
		{
			this.Label.text = this.GetRandomString(this.SocialDeathReactions);
		}
		else if (subtitleType == SubtitleType.LovestruckDeathReaction)
		{
			this.Label.text = this.LovestruckDeathReactions[ID];
		}
		else if (subtitleType == SubtitleType.LovestruckMurderReport)
		{
			this.Label.text = this.LovestruckMurderReports[ID];
		}
		else if (subtitleType == SubtitleType.LovestruckCorpseReport)
		{
			this.Label.text = this.LovestruckCorpseReports[ID];
		}
		else if (subtitleType == SubtitleType.SocialReport)
		{
			this.Label.text = this.GetRandomString(this.SocialReports);
		}
		else if (subtitleType == SubtitleType.SocialFear)
		{
			this.Label.text = this.GetRandomString(this.SocialFears);
		}
		else if (subtitleType == SubtitleType.SocialTerror)
		{
			this.Label.text = this.GetRandomString(this.SocialTerrors);
		}
		else if (subtitleType == SubtitleType.RepeatReaction)
		{
			this.Label.text = this.GetRandomString(this.RepeatReactions);
		}
		else if (subtitleType == SubtitleType.Greeting)
		{
			this.Label.text = this.GetRandomString(this.Greetings);
		}
		else if (subtitleType == SubtitleType.PlayerFarewell)
		{
			this.Label.text = this.GetRandomString(this.PlayerFarewells);
		}
		else if (subtitleType == SubtitleType.StudentFarewell)
		{
			this.Label.text = this.GetRandomString(this.StudentFarewells);
		}
		else if (subtitleType == SubtitleType.InsanityApology)
		{
			this.Label.text = this.GetRandomString(this.InsanityApologies);
		}
		else if (subtitleType == SubtitleType.WeaponAndBloodApology)
		{
			this.Label.text = this.GetRandomString(this.WeaponBloodApologies);
		}
		else if (subtitleType == SubtitleType.WeaponApology)
		{
			this.Label.text = this.GetRandomString(this.WeaponApologies);
		}
		else if (subtitleType == SubtitleType.BloodApology)
		{
			this.Label.text = this.GetRandomString(this.BloodApologies);
		}
		else if (subtitleType == SubtitleType.LewdApology)
		{
			this.Label.text = this.GetRandomString(this.LewdApologies);
		}
		else if (subtitleType == SubtitleType.SuspiciousApology)
		{
			this.Label.text = this.GetRandomString(this.SuspiciousApologies);
		}
		else if (subtitleType == SubtitleType.EavesdropApology)
		{
			this.Label.text = this.GetRandomString(this.EavesdropApologies);
		}
		else if (subtitleType == SubtitleType.ViolenceApology)
		{
			this.Label.text = this.GetRandomString(this.ViolenceApologies);
		}
		else if (subtitleType == SubtitleType.TheftApology)
		{
			this.Label.text = this.GetRandomString(this.TheftApologies);
		}
		else if (subtitleType == SubtitleType.EventApology)
		{
			this.Label.text = this.GetRandomString(this.EventApologies);
		}
		else if (subtitleType == SubtitleType.ClassApology)
		{
			this.Label.text = this.GetRandomString(this.ClassApologies);
		}
		else if (subtitleType == SubtitleType.AccidentApology)
		{
			this.Label.text = this.GetRandomString(this.AccidentApologies);
		}
		else if (subtitleType == SubtitleType.SadApology)
		{
			this.Label.text = this.GetRandomString(this.SadApologies);
		}
		else if (subtitleType == SubtitleType.Dismissive)
		{
			this.Label.text = this.Dismissives[ID];
		}
		else if (subtitleType == SubtitleType.Forgiving)
		{
			this.Label.text = this.GetRandomString(this.Forgivings);
		}
		else if (subtitleType == SubtitleType.ForgivingAccident)
		{
			this.Label.text = this.GetRandomString(this.AccidentForgivings);
		}
		else if (subtitleType == SubtitleType.ForgivingInsanity)
		{
			this.Label.text = this.GetRandomString(this.InsanityForgivings);
		}
		else if (subtitleType == SubtitleType.Impatience)
		{
			this.Label.text = this.Impatiences[ID];
		}
		else if (subtitleType == SubtitleType.PlayerCompliment)
		{
			this.Label.text = this.GetRandomString(this.PlayerCompliments);
		}
		else if (subtitleType == SubtitleType.StudentHighCompliment)
		{
			this.Label.text = this.GetRandomString(this.StudentHighCompliments);
		}
		else if (subtitleType == SubtitleType.StudentMidCompliment)
		{
			this.Label.text = this.GetRandomString(this.StudentMidCompliments);
		}
		else if (subtitleType == SubtitleType.StudentLowCompliment)
		{
			this.Label.text = this.GetRandomString(this.StudentLowCompliments);
		}
		else if (subtitleType == SubtitleType.PlayerGossip)
		{
			this.Label.text = this.GetRandomString(this.PlayerGossip);
		}
		else if (subtitleType == SubtitleType.StudentGossip)
		{
			this.Label.text = this.GetRandomString(this.StudentGossip);
		}
		else if (subtitleType == SubtitleType.PlayerFollow)
		{
			this.Label.text = this.PlayerFollows[ID];
		}
		else if (subtitleType == SubtitleType.StudentFollow)
		{
			this.Label.text = this.StudentFollows[ID];
		}
		else if (subtitleType == SubtitleType.PlayerLeave)
		{
			this.Label.text = this.PlayerLeaves[ID];
		}
		else if (subtitleType == SubtitleType.StudentLeave)
		{
			this.Label.text = this.StudentLeaves[ID];
		}
		else if (subtitleType == SubtitleType.StudentStay)
		{
			this.Label.text = this.StudentStays[ID];
		}
		else if (subtitleType == SubtitleType.PlayerDistract)
		{
			this.Label.text = this.PlayerDistracts[ID];
		}
		else if (subtitleType == SubtitleType.StudentDistract)
		{
			this.Label.text = this.StudentDistracts[ID];
		}
		else if (subtitleType == SubtitleType.StudentDistractRefuse)
		{
			this.Label.text = this.GetRandomString(this.StudentDistractRefuses);
		}
		else if (subtitleType == SubtitleType.StudentDistractBullyRefuse)
		{
			this.Label.text = this.GetRandomString(this.StudentDistractBullyRefuses);
		}
		else if (subtitleType == SubtitleType.StopFollowApology)
		{
			this.Label.text = this.StopFollowApologies[ID];
		}
		else if (subtitleType == SubtitleType.GrudgeWarning)
		{
			this.Label.text = this.GetRandomString(this.GrudgeWarnings);
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.GrudgeRefusal)
		{
			this.Label.text = this.GetRandomString(this.GrudgeRefusals);
		}
		else if (subtitleType == SubtitleType.CowardGrudge)
		{
			this.Label.text = this.GetRandomString(this.CowardGrudges);
		}
		else if (subtitleType == SubtitleType.EvilGrudge)
		{
			this.Label.text = this.GetRandomString(this.EvilGrudges);
		}
		else if (subtitleType == SubtitleType.PlayerLove)
		{
			this.Label.text = this.PlayerLove[ID];
		}
		else if (subtitleType == SubtitleType.SuitorLove)
		{
			this.Label.text = this.SuitorLove[ID];
		}
		else if (subtitleType == SubtitleType.RivalLove)
		{
			this.Label.text = this.RivalLove[ID];
		}
		else if (subtitleType == SubtitleType.RequestMedicine)
		{
			this.Label.text = this.RequestMedicines[ID];
		}
		else if (subtitleType == SubtitleType.ReturningWeapon)
		{
			this.Label.text = this.ReturningWeapons[ID];
		}
		else if (subtitleType == SubtitleType.Dying)
		{
			this.Label.text = this.GetRandomString(this.Deaths);
		}
		else if (subtitleType == SubtitleType.SenpaiInsanityReaction)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.SenpaiInsanityReactions.Length);
			this.Label.text = this.SenpaiInsanityReactions[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.SenpaiWeaponReaction)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.SenpaiWeaponReactions.Length);
			this.Label.text = this.SenpaiWeaponReactions[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.SenpaiBloodReaction)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.SenpaiBloodReactions.Length);
			this.Label.text = this.SenpaiBloodReactions[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.SenpaiLewdReaction)
		{
			this.Label.text = this.GetRandomString(this.SenpaiLewdReactions);
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.SenpaiStalkingReaction)
		{
			this.Label.text = this.SenpaiStalkingReactions[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.SenpaiMurderReaction)
		{
			this.Label.text = this.SenpaiMurderReactions[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.SenpaiCorpseReaction)
		{
			this.Label.text = this.GetRandomString(this.SenpaiCorpseReactions);
		}
		else if (subtitleType == SubtitleType.SenpaiViolenceReaction)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.SenpaiViolenceReactions.Length);
			this.Label.text = this.SenpaiViolenceReactions[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.SenpaiRivalDeathReaction)
		{
			this.Label.text = this.SenpaiRivalDeathReactions[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.RaibaruRivalDeathReaction)
		{
			this.Label.text = this.RaibaruRivalDeathReactions[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.YandereWhimper)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.YandereWhimpers.Length);
			this.Label.text = this.YandereWhimpers[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.StudentMurderReport)
		{
			this.Label.text = this.StudentMurderReports[ID];
		}
		else if (subtitleType == SubtitleType.SplashReaction)
		{
			this.Label.text = this.SplashReactions[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.SplashReactionMale)
		{
			this.Label.text = this.SplashReactionsMale[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.RivalSplashReaction)
		{
			this.Label.text = this.RivalSplashReactions[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.LightSwitchReaction)
		{
			this.Label.text = this.LightSwitchReactions[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.PhotoAnnoyance)
		{
			while (this.RandomID == this.PreviousRandom)
			{
				this.RandomID = UnityEngine.Random.Range(0, this.PhotoAnnoyances.Length);
			}
			this.PreviousRandom = this.RandomID;
			this.Label.text = this.PhotoAnnoyances[this.RandomID];
		}
		else if (subtitleType == SubtitleType.Task6Line)
		{
			this.Label.text = this.Task6Lines[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.Task7Line)
		{
			this.Label.text = this.Task7Lines[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.Task8Line)
		{
			this.Label.text = this.Task8Lines[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.Task11Line)
		{
			this.Label.text = this.Task11Lines[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.Task13Line)
		{
			this.Label.text = this.Task13Lines[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.Task14Line)
		{
			this.Label.text = this.Task14Lines[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.Task15Line)
		{
			this.Label.text = this.Task15Lines[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.Task25Line)
		{
			this.Label.text = this.Task25Lines[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.Task28Line)
		{
			this.Label.text = this.Task28Lines[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.Task30Line)
		{
			this.Label.text = this.Task30Lines[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.Task32Line)
		{
			this.Label.text = this.Task32Lines[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.Task33Line)
		{
			this.Label.text = this.Task33Lines[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.Task34Line)
		{
			this.Label.text = this.Task34Lines[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.Task36Line)
		{
			this.Label.text = this.Task36Lines[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.Task37Line)
		{
			this.Label.text = this.Task37Lines[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.Task38Line)
		{
			this.Label.text = this.Task38Lines[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.Task52Line)
		{
			this.Label.text = this.Task52Lines[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.Task76Line)
		{
			this.Label.text = this.Task76Lines[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.Task77Line)
		{
			this.Label.text = this.Task77Lines[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.Task78Line)
		{
			this.Label.text = this.Task78Lines[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.Task79Line)
		{
			this.Label.text = this.Task79Lines[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.Task80Line)
		{
			this.Label.text = this.Task80Lines[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.Task81Line)
		{
			this.Label.text = this.Task81Lines[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.TaskGenericLine)
		{
			this.Label.text = "(PLACEHOLDER TASK - WILL BE REPLACED AFTER DEMO)\n" + this.TaskGenericLines[ID];
			if (this.Yandere.GetComponent<YandereScript>().TargetStudent.Male)
			{
				this.PlayVoice(SubtitleType.TaskGenericLineMale, ID);
			}
			else
			{
				this.PlayVoice(SubtitleType.TaskGenericLineFemale, ID);
			}
		}
		else if (subtitleType == SubtitleType.TaskInquiry)
		{
			this.Label.text = this.TaskInquiries[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.ClubGreeting)
		{
			this.Label.text = this.ClubGreetings[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.ClubUnwelcome)
		{
			this.Label.text = this.ClubUnwelcomes[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.ClubKick)
		{
			this.Label.text = this.ClubKicks[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.ClubPractice)
		{
			this.Label.text = this.ClubPractices[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.ClubPracticeYes)
		{
			this.Label.text = this.ClubPracticeYeses[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.ClubPracticeNo)
		{
			this.Label.text = this.ClubPracticeNoes[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.ClubPlaceholderInfo)
		{
			this.Label.text = this.Club0Info[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.ClubCookingInfo)
		{
			this.Label.text = this.Club1Info[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.ClubDramaInfo)
		{
			this.Label.text = this.Club2Info[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.ClubOccultInfo)
		{
			this.Label.text = this.Club3Info[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.ClubArtInfo)
		{
			this.Label.text = this.Club4Info[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.ClubLightMusicInfo)
		{
			this.Label.text = this.Club5Info[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.ClubMartialArtsInfo)
		{
			this.Label.text = this.Club6Info[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.ClubPhotoInfoLight)
		{
			this.Label.text = this.Club7InfoLight[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.ClubPhotoInfoDark)
		{
			this.Label.text = this.Club7InfoDark[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.ClubScienceInfo)
		{
			this.Label.text = this.Club8Info[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.ClubSportsInfo)
		{
			this.Label.text = this.Club9Info[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.ClubGardenInfo)
		{
			this.Label.text = this.Club10Info[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.ClubGamingInfo)
		{
			this.Label.text = this.Club11Info[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.ClubDelinquentInfo)
		{
			this.Label.text = this.Club12Info[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.ClubJoin)
		{
			this.Label.text = this.ClubJoins[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.ClubAccept)
		{
			this.Label.text = this.ClubAccepts[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.ClubRefuse)
		{
			this.Label.text = this.ClubRefuses[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.ClubRejoin)
		{
			this.Label.text = this.ClubRejoins[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.ClubExclusive)
		{
			this.Label.text = this.ClubExclusives[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.ClubGrudge)
		{
			this.Label.text = this.ClubGrudges[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.ClubQuit)
		{
			this.Label.text = this.ClubQuits[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.ClubConfirm)
		{
			this.Label.text = this.ClubConfirms[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.ClubDeny)
		{
			this.Label.text = this.ClubDenies[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.ClubFarewell)
		{
			this.Label.text = this.ClubFarewells[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.ClubActivity)
		{
			this.Label.text = this.ClubActivities[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.ClubEarly)
		{
			this.Label.text = this.ClubEarlies[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.ClubLate)
		{
			this.Label.text = this.ClubLates[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.ClubYes)
		{
			this.Label.text = this.ClubYeses[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.ClubNo)
		{
			this.Label.text = this.ClubNoes[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.InfoNotice)
		{
			this.Label.text = this.InfoNotice;
		}
		else if (subtitleType == SubtitleType.StrictReaction)
		{
			this.Label.text = this.StrictReaction[ID];
		}
		else if (subtitleType == SubtitleType.CasualReaction)
		{
			this.Label.text = this.CasualReaction[ID];
		}
		else if (subtitleType == SubtitleType.GraceReaction)
		{
			this.Label.text = this.GraceReaction[ID];
		}
		else if (subtitleType == SubtitleType.EdgyReaction)
		{
			this.Label.text = this.EdgyReaction[ID];
		}
		else if (subtitleType == SubtitleType.Shoving)
		{
			this.Label.text = this.Shoving[ID];
		}
		else if (subtitleType == SubtitleType.Spraying)
		{
			this.Label.text = this.Spraying[ID];
		}
		else if (subtitleType == SubtitleType.Chasing)
		{
			this.Label.text = this.Chasing[ID];
		}
		else if (subtitleType == SubtitleType.Eulogy)
		{
			this.Label.text = this.Eulogies[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.AskForHelp)
		{
			this.Label.text = this.AskForHelps[ID];
		}
		else if (subtitleType == SubtitleType.GiveHelp)
		{
			this.Label.text = this.GiveHelps[ID];
		}
		else if (subtitleType == SubtitleType.RejectHelp)
		{
			this.Label.text = this.RejectHelps[ID];
		}
		else if (subtitleType == SubtitleType.ObstacleMurderReaction)
		{
			this.Label.text = this.ObstacleMurderReactions[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.ObstaclePoisonReaction)
		{
			this.Label.text = this.ObstaclePoisonReactions[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.GasWarning)
		{
			this.Label.text = this.GasWarnings[ID];
			this.PlayVoice(subtitleType, ID);
		}
		this.Timer = Duration;
	}

	// Token: 0x06001BE2 RID: 7138 RVA: 0x001464E4 File Offset: 0x001446E4
	private void Update()
	{
		if (this.Timer > 0f)
		{
			this.Timer -= Time.deltaTime;
			if (this.Timer <= 0f)
			{
				this.Jukebox.Dip = 1f;
				this.Label.text = string.Empty;
				this.Timer = 0f;
			}
		}
	}

	// Token: 0x06001BE3 RID: 7139 RVA: 0x00146548 File Offset: 0x00144748
	private void PlayVoice(SubtitleType subtitleType, int ID)
	{
		this.CurrentClip != null;
		this.Jukebox.Dip = 0.5f;
		AudioClipArrayWrapper audioClipArrayWrapper;
		this.SubtitleClipArrays.TryGetValue(subtitleType, out audioClipArrayWrapper);
		this.PlayClip(audioClipArrayWrapper[ID], base.transform.position);
	}

	// Token: 0x06001BE4 RID: 7140 RVA: 0x0014659C File Offset: 0x0014479C
	public float GetClipLength(int StudentID, int TaskPhase)
	{
		if (StudentID == 6)
		{
			return this.Task6Clips[TaskPhase].length + 0.5f;
		}
		if (StudentID == 8)
		{
			return this.Task8Clips[TaskPhase].length;
		}
		if (StudentID == 11)
		{
			return this.Task11Clips[TaskPhase].length;
		}
		if (StudentID == 25)
		{
			return this.Task25Clips[TaskPhase].length;
		}
		if (StudentID == 28)
		{
			return this.Task28Clips[TaskPhase].length;
		}
		if (StudentID == 30)
		{
			return this.Task30Clips[TaskPhase].length;
		}
		if (StudentID == 36)
		{
			return this.Task36Clips[TaskPhase].length;
		}
		if (StudentID == 37)
		{
			return this.Task37Clips[TaskPhase].length;
		}
		if (StudentID == 38)
		{
			return this.Task38Clips[TaskPhase].length;
		}
		if (StudentID == 52)
		{
			return this.Task52Clips[TaskPhase].length;
		}
		if (StudentID == 76)
		{
			return this.Task76Clips[TaskPhase].length;
		}
		if (StudentID == 77)
		{
			return this.Task77Clips[TaskPhase].length;
		}
		if (StudentID == 78)
		{
			return this.Task78Clips[TaskPhase].length;
		}
		if (StudentID == 79)
		{
			return this.Task79Clips[TaskPhase].length;
		}
		if (StudentID == 80)
		{
			return this.Task80Clips[TaskPhase].length;
		}
		if (StudentID == 81)
		{
			return this.Task81Clips[TaskPhase].length;
		}
		if (this.Yandere.GetComponent<YandereScript>().TargetStudent.Male)
		{
			return this.TaskGenericMaleClips[TaskPhase].length;
		}
		return this.TaskGenericFemaleClips[TaskPhase].length;
	}

	// Token: 0x06001BE5 RID: 7141 RVA: 0x00146710 File Offset: 0x00144910
	public float GetClubClipLength(ClubType Club, int ClubPhase)
	{
		if (Club == ClubType.Cooking)
		{
			return this.Club1Clips[ClubPhase].length + 0.5f;
		}
		if (Club == ClubType.Drama)
		{
			return this.Club2Clips[ClubPhase].length + 0.5f;
		}
		if (Club == ClubType.Occult)
		{
			return this.Club3Clips[ClubPhase].length + 0.5f;
		}
		if (Club == ClubType.Art)
		{
			return this.Club4Clips[ClubPhase].length + 0.5f;
		}
		if (Club == ClubType.LightMusic)
		{
			return this.Club5Clips[ClubPhase].length + 0.5f;
		}
		if (Club == ClubType.MartialArts)
		{
			return this.Club6Clips[ClubPhase].length + 0.5f;
		}
		if (Club == ClubType.Photography)
		{
			if (SchoolGlobals.SchoolAtmosphere <= 0.8f)
			{
				return this.Club7ClipsDark[ClubPhase].length + 0.5f;
			}
			return this.Club7ClipsLight[ClubPhase].length + 0.5f;
		}
		else
		{
			if (Club == ClubType.Science)
			{
				return this.Club8Clips[ClubPhase].length + 0.5f;
			}
			if (Club == ClubType.Sports)
			{
				return this.Club9Clips[ClubPhase].length + 0.5f;
			}
			if (Club == ClubType.Gardening)
			{
				return this.Club10Clips[ClubPhase].length + 0.5f;
			}
			if (Club == ClubType.Gaming)
			{
				return this.Club11Clips[ClubPhase].length + 0.5f;
			}
			if (Club == ClubType.Delinquent)
			{
				return this.Club12Clips[ClubPhase].length + 0.5f;
			}
			return 0f;
		}
	}

	// Token: 0x06001BE6 RID: 7142 RVA: 0x00146868 File Offset: 0x00144A68
	private void PlayClip(AudioClip clip, Vector3 pos)
	{
		if (clip != null)
		{
			GameObject gameObject = new GameObject("TempAudio");
			if (this.Speaker != null)
			{
				gameObject.transform.position = this.Speaker.transform.position + base.transform.up;
				gameObject.transform.parent = this.Speaker.transform;
			}
			else
			{
				gameObject.transform.position = this.Yandere.transform.position + base.transform.up;
				gameObject.transform.parent = this.Yandere.transform;
			}
			AudioSource audioSource = gameObject.AddComponent<AudioSource>();
			audioSource.clip = clip;
			audioSource.Play();
			UnityEngine.Object.Destroy(gameObject, clip.length);
			audioSource.rolloffMode = AudioRolloffMode.Linear;
			audioSource.spatialBlend = 1f;
			audioSource.minDistance = 5f;
			audioSource.maxDistance = 15f;
			this.CurrentClip = gameObject;
			audioSource.volume = ((this.Yandere.position.y < gameObject.transform.position.y - 2f) ? 0f : 1f);
			this.Speaker = null;
		}
	}

	// Token: 0x0400322A RID: 12842
	public JukeboxScript Jukebox;

	// Token: 0x0400322B RID: 12843
	public Transform Yandere;

	// Token: 0x0400322C RID: 12844
	public UILabel Label;

	// Token: 0x0400322D RID: 12845
	public string[] WeaponBloodInsanityReactions;

	// Token: 0x0400322E RID: 12846
	public string[] WeaponBloodReactions;

	// Token: 0x0400322F RID: 12847
	public string[] WeaponInsanityReactions;

	// Token: 0x04003230 RID: 12848
	public string[] BloodInsanityReactions;

	// Token: 0x04003231 RID: 12849
	public string[] BloodReactions;

	// Token: 0x04003232 RID: 12850
	public string[] BloodPoolReactions;

	// Token: 0x04003233 RID: 12851
	public string[] BloodyWeaponReactions;

	// Token: 0x04003234 RID: 12852
	public string[] LimbReactions;

	// Token: 0x04003235 RID: 12853
	public string[] WetBloodReactions;

	// Token: 0x04003236 RID: 12854
	public string[] InsanityReactions;

	// Token: 0x04003237 RID: 12855
	public string[] LewdReactions;

	// Token: 0x04003238 RID: 12856
	public string[] SuspiciousReactions;

	// Token: 0x04003239 RID: 12857
	public string[] MurderReactions;

	// Token: 0x0400323A RID: 12858
	public string[] CowardMurderReactions;

	// Token: 0x0400323B RID: 12859
	public string[] EvilMurderReactions;

	// Token: 0x0400323C RID: 12860
	public string[] HoldingBloodyClothingReactions;

	// Token: 0x0400323D RID: 12861
	public string[] PetBloodReports;

	// Token: 0x0400323E RID: 12862
	public string[] PetBloodReactions;

	// Token: 0x0400323F RID: 12863
	public string[] PetCorpseReports;

	// Token: 0x04003240 RID: 12864
	public string[] PetCorpseReactions;

	// Token: 0x04003241 RID: 12865
	public string[] PetLimbReports;

	// Token: 0x04003242 RID: 12866
	public string[] PetLimbReactions;

	// Token: 0x04003243 RID: 12867
	public string[] PetMurderReports;

	// Token: 0x04003244 RID: 12868
	public string[] PetMurderReactions;

	// Token: 0x04003245 RID: 12869
	public string[] PetWeaponReports;

	// Token: 0x04003246 RID: 12870
	public string[] PetWeaponReactions;

	// Token: 0x04003247 RID: 12871
	public string[] PetBloodyWeaponReports;

	// Token: 0x04003248 RID: 12872
	public string[] PetBloodyWeaponReactions;

	// Token: 0x04003249 RID: 12873
	public string[] HeroMurderReactions;

	// Token: 0x0400324A RID: 12874
	public string[] LonerMurderReactions;

	// Token: 0x0400324B RID: 12875
	public string[] LonerCorpseReactions;

	// Token: 0x0400324C RID: 12876
	public string[] EvilCorpseReactions;

	// Token: 0x0400324D RID: 12877
	public string[] EvilDelinquentCorpseReactions;

	// Token: 0x0400324E RID: 12878
	public string[] SocialDeathReactions;

	// Token: 0x0400324F RID: 12879
	public string[] LovestruckDeathReactions;

	// Token: 0x04003250 RID: 12880
	public string[] LovestruckMurderReports;

	// Token: 0x04003251 RID: 12881
	public string[] LovestruckCorpseReports;

	// Token: 0x04003252 RID: 12882
	public string[] SocialReports;

	// Token: 0x04003253 RID: 12883
	public string[] SocialFears;

	// Token: 0x04003254 RID: 12884
	public string[] SocialTerrors;

	// Token: 0x04003255 RID: 12885
	public string[] RepeatReactions;

	// Token: 0x04003256 RID: 12886
	public string[] CorpseReactions;

	// Token: 0x04003257 RID: 12887
	public string[] PoisonReactions;

	// Token: 0x04003258 RID: 12888
	public string[] PrankReactions;

	// Token: 0x04003259 RID: 12889
	public string[] InterruptReactions;

	// Token: 0x0400325A RID: 12890
	public string[] IntrusionReactions;

	// Token: 0x0400325B RID: 12891
	public string[] NoteReactions;

	// Token: 0x0400325C RID: 12892
	public string[] NoteReactionsMale;

	// Token: 0x0400325D RID: 12893
	public string[] OfferSnacks;

	// Token: 0x0400325E RID: 12894
	public string[] FoodAccepts;

	// Token: 0x0400325F RID: 12895
	public string[] FoodRejects;

	// Token: 0x04003260 RID: 12896
	public string[] EavesdropReactions;

	// Token: 0x04003261 RID: 12897
	public string[] ViolenceReactions;

	// Token: 0x04003262 RID: 12898
	public string[] EventEavesdropReactions;

	// Token: 0x04003263 RID: 12899
	public string[] RivalEavesdropReactions;

	// Token: 0x04003264 RID: 12900
	public string[] PickpocketReactions;

	// Token: 0x04003265 RID: 12901
	public string[] RivalPickpocketReactions;

	// Token: 0x04003266 RID: 12902
	public string[] DrownReactions;

	// Token: 0x04003267 RID: 12903
	public string[] ParanoidReactions;

	// Token: 0x04003268 RID: 12904
	public string[] TheftReactions;

	// Token: 0x04003269 RID: 12905
	public string[] KilledMoods;

	// Token: 0x0400326A RID: 12906
	public string[] SendToLockers;

	// Token: 0x0400326B RID: 12907
	public string[] KnifeReactions;

	// Token: 0x0400326C RID: 12908
	public string[] SyringeReactions;

	// Token: 0x0400326D RID: 12909
	public string[] KatanaReactions;

	// Token: 0x0400326E RID: 12910
	public string[] SawReactions;

	// Token: 0x0400326F RID: 12911
	public string[] RitualReactions;

	// Token: 0x04003270 RID: 12912
	public string[] BatReactions;

	// Token: 0x04003271 RID: 12913
	public string[] ShovelReactions;

	// Token: 0x04003272 RID: 12914
	public string[] DumbbellReactions;

	// Token: 0x04003273 RID: 12915
	public string[] AxeReactions;

	// Token: 0x04003274 RID: 12916
	public string[] PropReactions;

	// Token: 0x04003275 RID: 12917
	public string[] DelinkWeaponReactions;

	// Token: 0x04003276 RID: 12918
	public string[] ExtinguisherReactions;

	// Token: 0x04003277 RID: 12919
	public string[] WrenchReactions;

	// Token: 0x04003278 RID: 12920
	public string[] GuitarReactions;

	// Token: 0x04003279 RID: 12921
	public string[] WeaponBloodApologies;

	// Token: 0x0400327A RID: 12922
	public string[] WeaponApologies;

	// Token: 0x0400327B RID: 12923
	public string[] BloodApologies;

	// Token: 0x0400327C RID: 12924
	public string[] InsanityApologies;

	// Token: 0x0400327D RID: 12925
	public string[] LewdApologies;

	// Token: 0x0400327E RID: 12926
	public string[] SuspiciousApologies;

	// Token: 0x0400327F RID: 12927
	public string[] EventApologies;

	// Token: 0x04003280 RID: 12928
	public string[] ClassApologies;

	// Token: 0x04003281 RID: 12929
	public string[] AccidentApologies;

	// Token: 0x04003282 RID: 12930
	public string[] SadApologies;

	// Token: 0x04003283 RID: 12931
	public string[] EavesdropApologies;

	// Token: 0x04003284 RID: 12932
	public string[] ViolenceApologies;

	// Token: 0x04003285 RID: 12933
	public string[] PickpocketApologies;

	// Token: 0x04003286 RID: 12934
	public string[] CleaningApologies;

	// Token: 0x04003287 RID: 12935
	public string[] PoisonApologies;

	// Token: 0x04003288 RID: 12936
	public string[] HoldingBloodyClothingApologies;

	// Token: 0x04003289 RID: 12937
	public string[] TheftApologies;

	// Token: 0x0400328A RID: 12938
	public string[] Greetings;

	// Token: 0x0400328B RID: 12939
	public string[] PlayerFarewells;

	// Token: 0x0400328C RID: 12940
	public string[] StudentFarewells;

	// Token: 0x0400328D RID: 12941
	public string[] Forgivings;

	// Token: 0x0400328E RID: 12942
	public string[] AccidentForgivings;

	// Token: 0x0400328F RID: 12943
	public string[] InsanityForgivings;

	// Token: 0x04003290 RID: 12944
	public string[] PlayerCompliments;

	// Token: 0x04003291 RID: 12945
	public string[] StudentHighCompliments;

	// Token: 0x04003292 RID: 12946
	public string[] StudentMidCompliments;

	// Token: 0x04003293 RID: 12947
	public string[] StudentLowCompliments;

	// Token: 0x04003294 RID: 12948
	public string[] PlayerGossip;

	// Token: 0x04003295 RID: 12949
	public string[] StudentGossip;

	// Token: 0x04003296 RID: 12950
	public string[] PlayerFollows;

	// Token: 0x04003297 RID: 12951
	public string[] StudentFollows;

	// Token: 0x04003298 RID: 12952
	public string[] PlayerLeaves;

	// Token: 0x04003299 RID: 12953
	public string[] StudentLeaves;

	// Token: 0x0400329A RID: 12954
	public string[] StudentStays;

	// Token: 0x0400329B RID: 12955
	public string[] PlayerDistracts;

	// Token: 0x0400329C RID: 12956
	public string[] StudentDistracts;

	// Token: 0x0400329D RID: 12957
	public string[] StudentDistractRefuses;

	// Token: 0x0400329E RID: 12958
	public string[] StudentDistractBullyRefuses;

	// Token: 0x0400329F RID: 12959
	public string[] StopFollowApologies;

	// Token: 0x040032A0 RID: 12960
	public string[] GrudgeWarnings;

	// Token: 0x040032A1 RID: 12961
	public string[] GrudgeRefusals;

	// Token: 0x040032A2 RID: 12962
	public string[] CowardGrudges;

	// Token: 0x040032A3 RID: 12963
	public string[] EvilGrudges;

	// Token: 0x040032A4 RID: 12964
	public string[] PlayerLove;

	// Token: 0x040032A5 RID: 12965
	public string[] SuitorLove;

	// Token: 0x040032A6 RID: 12966
	public string[] RivalLove;

	// Token: 0x040032A7 RID: 12967
	public string[] RequestMedicines;

	// Token: 0x040032A8 RID: 12968
	public string[] ReturningWeapons;

	// Token: 0x040032A9 RID: 12969
	public string[] Impatiences;

	// Token: 0x040032AA RID: 12970
	public string[] ImpatientFarewells;

	// Token: 0x040032AB RID: 12971
	public string[] Deaths;

	// Token: 0x040032AC RID: 12972
	public string[] SenpaiInsanityReactions;

	// Token: 0x040032AD RID: 12973
	public string[] SenpaiWeaponReactions;

	// Token: 0x040032AE RID: 12974
	public string[] SenpaiBloodReactions;

	// Token: 0x040032AF RID: 12975
	public string[] SenpaiLewdReactions;

	// Token: 0x040032B0 RID: 12976
	public string[] SenpaiStalkingReactions;

	// Token: 0x040032B1 RID: 12977
	public string[] SenpaiMurderReactions;

	// Token: 0x040032B2 RID: 12978
	public string[] SenpaiCorpseReactions;

	// Token: 0x040032B3 RID: 12979
	public string[] SenpaiViolenceReactions;

	// Token: 0x040032B4 RID: 12980
	public string[] SenpaiRivalDeathReactions;

	// Token: 0x040032B5 RID: 12981
	public string[] RaibaruRivalDeathReactions;

	// Token: 0x040032B6 RID: 12982
	public string[] TeacherInsanityReactions;

	// Token: 0x040032B7 RID: 12983
	public string[] TeacherWeaponReactions;

	// Token: 0x040032B8 RID: 12984
	public string[] TeacherBloodReactions;

	// Token: 0x040032B9 RID: 12985
	public string[] TeacherInsanityHostiles;

	// Token: 0x040032BA RID: 12986
	public string[] TeacherWeaponHostiles;

	// Token: 0x040032BB RID: 12987
	public string[] TeacherBloodHostiles;

	// Token: 0x040032BC RID: 12988
	public string[] TeacherCoverUpHostiles;

	// Token: 0x040032BD RID: 12989
	public string[] TeacherLewdReactions;

	// Token: 0x040032BE RID: 12990
	public string[] TeacherTrespassReactions;

	// Token: 0x040032BF RID: 12991
	public string[] TeacherLateReactions;

	// Token: 0x040032C0 RID: 12992
	public string[] TeacherReportReactions;

	// Token: 0x040032C1 RID: 12993
	public string[] TeacherCorpseReactions;

	// Token: 0x040032C2 RID: 12994
	public string[] TeacherCorpseInspections;

	// Token: 0x040032C3 RID: 12995
	public string[] TeacherPoliceReports;

	// Token: 0x040032C4 RID: 12996
	public string[] TeacherAttackReactions;

	// Token: 0x040032C5 RID: 12997
	public string[] TeacherMurderReactions;

	// Token: 0x040032C6 RID: 12998
	public string[] TeacherPrankReactions;

	// Token: 0x040032C7 RID: 12999
	public string[] TeacherTheftReactions;

	// Token: 0x040032C8 RID: 13000
	public string[] DelinquentAnnoys;

	// Token: 0x040032C9 RID: 13001
	public string[] DelinquentCases;

	// Token: 0x040032CA RID: 13002
	public string[] DelinquentShoves;

	// Token: 0x040032CB RID: 13003
	public string[] DelinquentReactions;

	// Token: 0x040032CC RID: 13004
	public string[] DelinquentWeaponReactions;

	// Token: 0x040032CD RID: 13005
	public string[] DelinquentThreateneds;

	// Token: 0x040032CE RID: 13006
	public string[] DelinquentTaunts;

	// Token: 0x040032CF RID: 13007
	public string[] DelinquentCalms;

	// Token: 0x040032D0 RID: 13008
	public string[] DelinquentFights;

	// Token: 0x040032D1 RID: 13009
	public string[] DelinquentAvenges;

	// Token: 0x040032D2 RID: 13010
	public string[] DelinquentWins;

	// Token: 0x040032D3 RID: 13011
	public string[] DelinquentSurrenders;

	// Token: 0x040032D4 RID: 13012
	public string[] DelinquentNoSurrenders;

	// Token: 0x040032D5 RID: 13013
	public string[] DelinquentMurderReactions;

	// Token: 0x040032D6 RID: 13014
	public string[] DelinquentCorpseReactions;

	// Token: 0x040032D7 RID: 13015
	public string[] DelinquentFriendCorpseReactions;

	// Token: 0x040032D8 RID: 13016
	public string[] DelinquentResumes;

	// Token: 0x040032D9 RID: 13017
	public string[] DelinquentFlees;

	// Token: 0x040032DA RID: 13018
	public string[] DelinquentEnemyFlees;

	// Token: 0x040032DB RID: 13019
	public string[] DelinquentFriendFlees;

	// Token: 0x040032DC RID: 13020
	public string[] DelinquentInjuredFlees;

	// Token: 0x040032DD RID: 13021
	public string[] DelinquentCheers;

	// Token: 0x040032DE RID: 13022
	public string[] DelinquentHmms;

	// Token: 0x040032DF RID: 13023
	public string[] DelinquentGrudges;

	// Token: 0x040032E0 RID: 13024
	public string[] Dismissives;

	// Token: 0x040032E1 RID: 13025
	public string[] LostPhones;

	// Token: 0x040032E2 RID: 13026
	public string[] RivalLostPhones;

	// Token: 0x040032E3 RID: 13027
	public string[] StudentMurderReports;

	// Token: 0x040032E4 RID: 13028
	public string[] YandereWhimpers;

	// Token: 0x040032E5 RID: 13029
	public string[] SplashReactions;

	// Token: 0x040032E6 RID: 13030
	public string[] SplashReactionsMale;

	// Token: 0x040032E7 RID: 13031
	public string[] RivalSplashReactions;

	// Token: 0x040032E8 RID: 13032
	public string[] LightSwitchReactions;

	// Token: 0x040032E9 RID: 13033
	public string[] PhotoAnnoyances;

	// Token: 0x040032EA RID: 13034
	public string[] Task6Lines;

	// Token: 0x040032EB RID: 13035
	public string[] Task7Lines;

	// Token: 0x040032EC RID: 13036
	public string[] Task8Lines;

	// Token: 0x040032ED RID: 13037
	public string[] Task11Lines;

	// Token: 0x040032EE RID: 13038
	public string[] Task13Lines;

	// Token: 0x040032EF RID: 13039
	public string[] Task14Lines;

	// Token: 0x040032F0 RID: 13040
	public string[] Task15Lines;

	// Token: 0x040032F1 RID: 13041
	public string[] Task25Lines;

	// Token: 0x040032F2 RID: 13042
	public string[] Task28Lines;

	// Token: 0x040032F3 RID: 13043
	public string[] Task30Lines;

	// Token: 0x040032F4 RID: 13044
	public string[] Task32Lines;

	// Token: 0x040032F5 RID: 13045
	public string[] Task33Lines;

	// Token: 0x040032F6 RID: 13046
	public string[] Task34Lines;

	// Token: 0x040032F7 RID: 13047
	public string[] Task36Lines;

	// Token: 0x040032F8 RID: 13048
	public string[] Task37Lines;

	// Token: 0x040032F9 RID: 13049
	public string[] Task38Lines;

	// Token: 0x040032FA RID: 13050
	public string[] Task52Lines;

	// Token: 0x040032FB RID: 13051
	public string[] Task76Lines;

	// Token: 0x040032FC RID: 13052
	public string[] Task77Lines;

	// Token: 0x040032FD RID: 13053
	public string[] Task78Lines;

	// Token: 0x040032FE RID: 13054
	public string[] Task79Lines;

	// Token: 0x040032FF RID: 13055
	public string[] Task80Lines;

	// Token: 0x04003300 RID: 13056
	public string[] Task81Lines;

	// Token: 0x04003301 RID: 13057
	public string[] TaskGenericLines;

	// Token: 0x04003302 RID: 13058
	public string[] TaskInquiries;

	// Token: 0x04003303 RID: 13059
	public string[] Club0Info;

	// Token: 0x04003304 RID: 13060
	public string[] Club1Info;

	// Token: 0x04003305 RID: 13061
	public string[] Club2Info;

	// Token: 0x04003306 RID: 13062
	public string[] Club3Info;

	// Token: 0x04003307 RID: 13063
	public string[] Club4Info;

	// Token: 0x04003308 RID: 13064
	public string[] Club5Info;

	// Token: 0x04003309 RID: 13065
	public string[] Club6Info;

	// Token: 0x0400330A RID: 13066
	public string[] Club7InfoLight;

	// Token: 0x0400330B RID: 13067
	public string[] Club7InfoDark;

	// Token: 0x0400330C RID: 13068
	public string[] Club8Info;

	// Token: 0x0400330D RID: 13069
	public string[] Club9Info;

	// Token: 0x0400330E RID: 13070
	public string[] Club10Info;

	// Token: 0x0400330F RID: 13071
	public string[] Club11Info;

	// Token: 0x04003310 RID: 13072
	public string[] Club12Info;

	// Token: 0x04003311 RID: 13073
	public string[] SubClub3Info;

	// Token: 0x04003312 RID: 13074
	public string[] ClubGreetings;

	// Token: 0x04003313 RID: 13075
	public string[] ClubUnwelcomes;

	// Token: 0x04003314 RID: 13076
	public string[] ClubKicks;

	// Token: 0x04003315 RID: 13077
	public string[] ClubJoins;

	// Token: 0x04003316 RID: 13078
	public string[] ClubAccepts;

	// Token: 0x04003317 RID: 13079
	public string[] ClubRefuses;

	// Token: 0x04003318 RID: 13080
	public string[] ClubRejoins;

	// Token: 0x04003319 RID: 13081
	public string[] ClubExclusives;

	// Token: 0x0400331A RID: 13082
	public string[] ClubGrudges;

	// Token: 0x0400331B RID: 13083
	public string[] ClubQuits;

	// Token: 0x0400331C RID: 13084
	public string[] ClubConfirms;

	// Token: 0x0400331D RID: 13085
	public string[] ClubDenies;

	// Token: 0x0400331E RID: 13086
	public string[] ClubFarewells;

	// Token: 0x0400331F RID: 13087
	public string[] ClubActivities;

	// Token: 0x04003320 RID: 13088
	public string[] ClubEarlies;

	// Token: 0x04003321 RID: 13089
	public string[] ClubLates;

	// Token: 0x04003322 RID: 13090
	public string[] ClubYeses;

	// Token: 0x04003323 RID: 13091
	public string[] ClubNoes;

	// Token: 0x04003324 RID: 13092
	public string[] ClubPractices;

	// Token: 0x04003325 RID: 13093
	public string[] ClubPracticeYeses;

	// Token: 0x04003326 RID: 13094
	public string[] ClubPracticeNoes;

	// Token: 0x04003327 RID: 13095
	public string[] StrictReaction;

	// Token: 0x04003328 RID: 13096
	public string[] CasualReaction;

	// Token: 0x04003329 RID: 13097
	public string[] GraceReaction;

	// Token: 0x0400332A RID: 13098
	public string[] EdgyReaction;

	// Token: 0x0400332B RID: 13099
	public string[] Spraying;

	// Token: 0x0400332C RID: 13100
	public string[] Shoving;

	// Token: 0x0400332D RID: 13101
	public string[] Chasing;

	// Token: 0x0400332E RID: 13102
	public string[] CouncilCorpseReactions;

	// Token: 0x0400332F RID: 13103
	public string[] CouncilToCounselors;

	// Token: 0x04003330 RID: 13104
	public string[] HmmReactions;

	// Token: 0x04003331 RID: 13105
	public string[] Eulogies;

	// Token: 0x04003332 RID: 13106
	public string[] AskForHelps;

	// Token: 0x04003333 RID: 13107
	public string[] GiveHelps;

	// Token: 0x04003334 RID: 13108
	public string[] RejectHelps;

	// Token: 0x04003335 RID: 13109
	public string[] GasWarnings;

	// Token: 0x04003336 RID: 13110
	public string[] ObstacleMurderReactions;

	// Token: 0x04003337 RID: 13111
	public string[] ObstaclePoisonReactions;

	// Token: 0x04003338 RID: 13112
	public string InfoNotice;

	// Token: 0x04003339 RID: 13113
	public int PreviousRandom;

	// Token: 0x0400333A RID: 13114
	public int RandomID;

	// Token: 0x0400333B RID: 13115
	public float Timer;

	// Token: 0x0400333C RID: 13116
	public int StudentID;

	// Token: 0x0400333D RID: 13117
	public PersonaSubtitleScript PersonaSubtitle;

	// Token: 0x0400333E RID: 13118
	public AudioClip[] NoteReactionClips;

	// Token: 0x0400333F RID: 13119
	public AudioClip[] NoteReactionMaleClips;

	// Token: 0x04003340 RID: 13120
	public AudioClip[] GrudgeWarningClips;

	// Token: 0x04003341 RID: 13121
	public AudioClip[] SenpaiInsanityReactionClips;

	// Token: 0x04003342 RID: 13122
	public AudioClip[] SenpaiWeaponReactionClips;

	// Token: 0x04003343 RID: 13123
	public AudioClip[] SenpaiBloodReactionClips;

	// Token: 0x04003344 RID: 13124
	public AudioClip[] SenpaiLewdReactionClips;

	// Token: 0x04003345 RID: 13125
	public AudioClip[] SenpaiStalkingReactionClips;

	// Token: 0x04003346 RID: 13126
	public AudioClip[] SenpaiMurderReactionClips;

	// Token: 0x04003347 RID: 13127
	public AudioClip[] SenpaiViolenceReactionClips;

	// Token: 0x04003348 RID: 13128
	public AudioClip[] SenpaiRivalDeathReactionClips;

	// Token: 0x04003349 RID: 13129
	public AudioClip[] RaibaruRivalDeathReactionClips;

	// Token: 0x0400334A RID: 13130
	public AudioClip[] YandereWhimperClips;

	// Token: 0x0400334B RID: 13131
	public AudioClip[] TeacherWeaponClips;

	// Token: 0x0400334C RID: 13132
	public AudioClip[] TeacherBloodClips;

	// Token: 0x0400334D RID: 13133
	public AudioClip[] TeacherInsanityClips;

	// Token: 0x0400334E RID: 13134
	public AudioClip[] TeacherWeaponHostileClips;

	// Token: 0x0400334F RID: 13135
	public AudioClip[] TeacherBloodHostileClips;

	// Token: 0x04003350 RID: 13136
	public AudioClip[] TeacherInsanityHostileClips;

	// Token: 0x04003351 RID: 13137
	public AudioClip[] TeacherCoverUpHostileClips;

	// Token: 0x04003352 RID: 13138
	public AudioClip[] TeacherLewdClips;

	// Token: 0x04003353 RID: 13139
	public AudioClip[] TeacherTrespassClips;

	// Token: 0x04003354 RID: 13140
	public AudioClip[] TeacherLateClips;

	// Token: 0x04003355 RID: 13141
	public AudioClip[] TeacherReportClips;

	// Token: 0x04003356 RID: 13142
	public AudioClip[] TeacherCorpseClips;

	// Token: 0x04003357 RID: 13143
	public AudioClip[] TeacherInspectClips;

	// Token: 0x04003358 RID: 13144
	public AudioClip[] TeacherPoliceClips;

	// Token: 0x04003359 RID: 13145
	public AudioClip[] TeacherAttackClips;

	// Token: 0x0400335A RID: 13146
	public AudioClip[] TeacherMurderClips;

	// Token: 0x0400335B RID: 13147
	public AudioClip[] TeacherPrankClips;

	// Token: 0x0400335C RID: 13148
	public AudioClip[] TeacherTheftClips;

	// Token: 0x0400335D RID: 13149
	public AudioClip[] LostPhoneClips;

	// Token: 0x0400335E RID: 13150
	public AudioClip[] RivalLostPhoneClips;

	// Token: 0x0400335F RID: 13151
	public AudioClip[] PickpocketReactionClips;

	// Token: 0x04003360 RID: 13152
	public AudioClip[] RivalPickpocketReactionClips;

	// Token: 0x04003361 RID: 13153
	public AudioClip[] SplashReactionClips;

	// Token: 0x04003362 RID: 13154
	public AudioClip[] SplashReactionMaleClips;

	// Token: 0x04003363 RID: 13155
	public AudioClip[] RivalSplashReactionClips;

	// Token: 0x04003364 RID: 13156
	public AudioClip[] DrownReactionClips;

	// Token: 0x04003365 RID: 13157
	public AudioClip[] LightSwitchClips;

	// Token: 0x04003366 RID: 13158
	public AudioClip[] Task6Clips;

	// Token: 0x04003367 RID: 13159
	public AudioClip[] Task7Clips;

	// Token: 0x04003368 RID: 13160
	public AudioClip[] Task8Clips;

	// Token: 0x04003369 RID: 13161
	public AudioClip[] Task11Clips;

	// Token: 0x0400336A RID: 13162
	public AudioClip[] Task13Clips;

	// Token: 0x0400336B RID: 13163
	public AudioClip[] Task14Clips;

	// Token: 0x0400336C RID: 13164
	public AudioClip[] Task15Clips;

	// Token: 0x0400336D RID: 13165
	public AudioClip[] Task25Clips;

	// Token: 0x0400336E RID: 13166
	public AudioClip[] Task28Clips;

	// Token: 0x0400336F RID: 13167
	public AudioClip[] Task30Clips;

	// Token: 0x04003370 RID: 13168
	public AudioClip[] Task32Clips;

	// Token: 0x04003371 RID: 13169
	public AudioClip[] Task33Clips;

	// Token: 0x04003372 RID: 13170
	public AudioClip[] Task34Clips;

	// Token: 0x04003373 RID: 13171
	public AudioClip[] Task36Clips;

	// Token: 0x04003374 RID: 13172
	public AudioClip[] Task37Clips;

	// Token: 0x04003375 RID: 13173
	public AudioClip[] Task38Clips;

	// Token: 0x04003376 RID: 13174
	public AudioClip[] Task52Clips;

	// Token: 0x04003377 RID: 13175
	public AudioClip[] Task76Clips;

	// Token: 0x04003378 RID: 13176
	public AudioClip[] Task77Clips;

	// Token: 0x04003379 RID: 13177
	public AudioClip[] Task78Clips;

	// Token: 0x0400337A RID: 13178
	public AudioClip[] Task79Clips;

	// Token: 0x0400337B RID: 13179
	public AudioClip[] Task80Clips;

	// Token: 0x0400337C RID: 13180
	public AudioClip[] Task81Clips;

	// Token: 0x0400337D RID: 13181
	public AudioClip[] TaskGenericMaleClips;

	// Token: 0x0400337E RID: 13182
	public AudioClip[] TaskGenericFemaleClips;

	// Token: 0x0400337F RID: 13183
	public AudioClip[] TaskInquiryClips;

	// Token: 0x04003380 RID: 13184
	public AudioClip[] Club0Clips;

	// Token: 0x04003381 RID: 13185
	public AudioClip[] Club1Clips;

	// Token: 0x04003382 RID: 13186
	public AudioClip[] Club2Clips;

	// Token: 0x04003383 RID: 13187
	public AudioClip[] Club3Clips;

	// Token: 0x04003384 RID: 13188
	public AudioClip[] Club4Clips;

	// Token: 0x04003385 RID: 13189
	public AudioClip[] Club5Clips;

	// Token: 0x04003386 RID: 13190
	public AudioClip[] Club6Clips;

	// Token: 0x04003387 RID: 13191
	public AudioClip[] Club7ClipsLight;

	// Token: 0x04003388 RID: 13192
	public AudioClip[] Club7ClipsDark;

	// Token: 0x04003389 RID: 13193
	public AudioClip[] Club8Clips;

	// Token: 0x0400338A RID: 13194
	public AudioClip[] Club9Clips;

	// Token: 0x0400338B RID: 13195
	public AudioClip[] Club10Clips;

	// Token: 0x0400338C RID: 13196
	public AudioClip[] Club11Clips;

	// Token: 0x0400338D RID: 13197
	public AudioClip[] Club12Clips;

	// Token: 0x0400338E RID: 13198
	public AudioClip[] SubClub3Clips;

	// Token: 0x0400338F RID: 13199
	public AudioClip[] ClubGreetingClips;

	// Token: 0x04003390 RID: 13200
	public AudioClip[] ClubUnwelcomeClips;

	// Token: 0x04003391 RID: 13201
	public AudioClip[] ClubKickClips;

	// Token: 0x04003392 RID: 13202
	public AudioClip[] ClubJoinClips;

	// Token: 0x04003393 RID: 13203
	public AudioClip[] ClubAcceptClips;

	// Token: 0x04003394 RID: 13204
	public AudioClip[] ClubRefuseClips;

	// Token: 0x04003395 RID: 13205
	public AudioClip[] ClubRejoinClips;

	// Token: 0x04003396 RID: 13206
	public AudioClip[] ClubExclusiveClips;

	// Token: 0x04003397 RID: 13207
	public AudioClip[] ClubGrudgeClips;

	// Token: 0x04003398 RID: 13208
	public AudioClip[] ClubQuitClips;

	// Token: 0x04003399 RID: 13209
	public AudioClip[] ClubConfirmClips;

	// Token: 0x0400339A RID: 13210
	public AudioClip[] ClubDenyClips;

	// Token: 0x0400339B RID: 13211
	public AudioClip[] ClubFarewellClips;

	// Token: 0x0400339C RID: 13212
	public AudioClip[] ClubActivityClips;

	// Token: 0x0400339D RID: 13213
	public AudioClip[] ClubEarlyClips;

	// Token: 0x0400339E RID: 13214
	public AudioClip[] ClubLateClips;

	// Token: 0x0400339F RID: 13215
	public AudioClip[] ClubYesClips;

	// Token: 0x040033A0 RID: 13216
	public AudioClip[] ClubNoClips;

	// Token: 0x040033A1 RID: 13217
	public AudioClip[] ClubPracticeClips;

	// Token: 0x040033A2 RID: 13218
	public AudioClip[] ClubPracticeYesClips;

	// Token: 0x040033A3 RID: 13219
	public AudioClip[] ClubPracticeNoClips;

	// Token: 0x040033A4 RID: 13220
	public AudioClip[] EavesdropClips;

	// Token: 0x040033A5 RID: 13221
	public AudioClip[] FoodRejectionClips;

	// Token: 0x040033A6 RID: 13222
	public AudioClip[] ViolenceClips;

	// Token: 0x040033A7 RID: 13223
	public AudioClip[] EventEavesdropClips;

	// Token: 0x040033A8 RID: 13224
	public AudioClip[] RivalEavesdropClips;

	// Token: 0x040033A9 RID: 13225
	public AudioClip[] DelinquentAnnoyClips;

	// Token: 0x040033AA RID: 13226
	public AudioClip[] DelinquentCaseClips;

	// Token: 0x040033AB RID: 13227
	public AudioClip[] DelinquentShoveClips;

	// Token: 0x040033AC RID: 13228
	public AudioClip[] DelinquentReactionClips;

	// Token: 0x040033AD RID: 13229
	public AudioClip[] DelinquentWeaponReactionClips;

	// Token: 0x040033AE RID: 13230
	public AudioClip[] DelinquentThreatenedClips;

	// Token: 0x040033AF RID: 13231
	public AudioClip[] DelinquentTauntClips;

	// Token: 0x040033B0 RID: 13232
	public AudioClip[] DelinquentCalmClips;

	// Token: 0x040033B1 RID: 13233
	public AudioClip[] DelinquentFightClips;

	// Token: 0x040033B2 RID: 13234
	public AudioClip[] DelinquentAvengeClips;

	// Token: 0x040033B3 RID: 13235
	public AudioClip[] DelinquentWinClips;

	// Token: 0x040033B4 RID: 13236
	public AudioClip[] DelinquentSurrenderClips;

	// Token: 0x040033B5 RID: 13237
	public AudioClip[] DelinquentNoSurrenderClips;

	// Token: 0x040033B6 RID: 13238
	public AudioClip[] DelinquentMurderReactionClips;

	// Token: 0x040033B7 RID: 13239
	public AudioClip[] DelinquentCorpseReactionClips;

	// Token: 0x040033B8 RID: 13240
	public AudioClip[] DelinquentFriendCorpseReactionClips;

	// Token: 0x040033B9 RID: 13241
	public AudioClip[] DelinquentResumeClips;

	// Token: 0x040033BA RID: 13242
	public AudioClip[] DelinquentFleeClips;

	// Token: 0x040033BB RID: 13243
	public AudioClip[] DelinquentEnemyFleeClips;

	// Token: 0x040033BC RID: 13244
	public AudioClip[] DelinquentFriendFleeClips;

	// Token: 0x040033BD RID: 13245
	public AudioClip[] DelinquentInjuredFleeClips;

	// Token: 0x040033BE RID: 13246
	public AudioClip[] DelinquentCheerClips;

	// Token: 0x040033BF RID: 13247
	public AudioClip[] DelinquentHmmClips;

	// Token: 0x040033C0 RID: 13248
	public AudioClip[] DelinquentGrudgeClips;

	// Token: 0x040033C1 RID: 13249
	public AudioClip[] DismissiveClips;

	// Token: 0x040033C2 RID: 13250
	public AudioClip[] EvilDelinquentCorpseReactionClips;

	// Token: 0x040033C3 RID: 13251
	public AudioClip[] EulogyClips;

	// Token: 0x040033C4 RID: 13252
	public AudioClip[] ObstacleMurderReactionClips;

	// Token: 0x040033C5 RID: 13253
	public AudioClip[] ObstaclePoisonReactionClips;

	// Token: 0x040033C6 RID: 13254
	public AudioClip[] GasWarningClips;

	// Token: 0x040033C7 RID: 13255
	private SubtitleTypeAndAudioClipArrayDictionary SubtitleClipArrays;

	// Token: 0x040033C8 RID: 13256
	public GameObject CurrentClip;

	// Token: 0x040033C9 RID: 13257
	public StudentScript Speaker;
}
