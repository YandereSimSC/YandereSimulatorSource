﻿using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x0200001D RID: 29
public class MissionModeScript : MonoBehaviour
{
	// Token: 0x060000BD RID: 189 RVA: 0x0000E6FC File Offset: 0x0000C8FC
	private void Start()
	{
		if (!SchoolGlobals.HighSecurity)
		{
			this.SecurityCameraGroup.SetActive(false);
			this.MetalDetectorGroup.SetActive(false);
		}
		this.NewMissionWindow.gameObject.SetActive(false);
		this.MissionModeHUD.SetActive(false);
		this.SpottedWindow.SetActive(false);
		this.ExitPortal.SetActive(false);
		this.Safe.SetActive(false);
		this.MainCamera = Camera.main;
		if (GameGlobals.LoveSick)
		{
			this.MurderKit.SetActive(false);
			this.Yandere.HeartRate.MediumColour = new Color(1f, 1f, 1f, 1f);
			this.Yandere.HeartRate.NormalColour = new Color(1f, 1f, 1f, 1f);
			this.Clock.PeriodLabel.color = new Color(1f, 0f, 0f, 1f);
			this.Clock.TimeLabel.color = new Color(1f, 0f, 0f, 1f);
			this.Clock.DayLabel.enabled = false;
			this.MoneyLabel.color = new Color(1f, 0f, 0f, 1f);
			this.Reputation.PendingRepMarker.GetComponent<UISprite>().color = new Color(1f, 0f, 0f, 1f);
			this.Reputation.CurrentRepMarker.gameObject.SetActive(false);
			this.Reputation.PendingRepLabel.color = new Color(1f, 0f, 0f, 1f);
			this.ReputationFace1.color = new Color(1f, 0f, 0f, 1f);
			this.ReputationFace2.color = new Color(1f, 0f, 0f, 1f);
			this.ReputationBG.color = new Color(1f, 0f, 0f, 1f);
			this.ReputationLabel.color = new Color(1f, 0f, 0f, 1f);
			this.Watermark.color = new Color(1f, 0f, 0f, 1f);
			this.EventSubtitleLabel.color = new Color(1f, 0f, 0f, 1f);
			this.SubtitleLabel.color = new Color(1f, 0f, 0f, 1f);
			this.CautionSign.color = new Color(1f, 0f, 0f, 1f);
			this.FPS.color = new Color(1f, 0f, 0f, 1f);
			this.SanityLabel.color = new Color(1f, 0f, 0f, 1f);
			this.ID = 1;
			while (this.ID < this.PoliceLabel.Length)
			{
				this.PoliceLabel[this.ID].color = new Color(1f, 0f, 0f, 1f);
				this.ID++;
			}
			this.ID = 1;
			while (this.ID < this.PoliceIcon.Length)
			{
				this.PoliceIcon[this.ID].color = new Color(1f, 0f, 0f, 1f);
				this.ID++;
			}
		}
		if (MissionModeGlobals.MissionMode)
		{
			this.AlphabetArrow.gameObject.SetActive(true);
			this.AlphabetArrow.gameObject.GetComponent<Renderer>().material.shader = this.StudentManager.QualityManager.ToonOutline;
			this.AlphabetArrow.gameObject.GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, 0f);
			this.Headmaster.SetActive(false);
			this.Yandere.HeartRate.MediumColour = new Color(1f, 0.5f, 0.5f, 1f);
			this.Yandere.HeartRate.NormalColour = new Color(1f, 1f, 1f, 1f);
			this.Clock.PeriodLabel.color = new Color(1f, 1f, 1f, 1f);
			this.Clock.TimeLabel.color = new Color(1f, 1f, 1f, 1f);
			this.Clock.DayLabel.enabled = false;
			this.MoneyLabel.color = new Color(1f, 1f, 1f, 1f);
			this.MoneyLabel.fontStyle = FontStyle.Bold;
			this.MoneyLabel.trueTypeFont = this.Arial;
			this.Reputation.PendingRepMarker.GetComponent<UISprite>().color = new Color(1f, 1f, 1f, 1f);
			this.Reputation.CurrentRepMarker.gameObject.SetActive(false);
			this.Reputation.PendingRepLabel.color = new Color(1f, 1f, 1f, 1f);
			this.ReputationLabel.fontStyle = FontStyle.Bold;
			this.ReputationLabel.trueTypeFont = this.Arial;
			this.ReputationLabel.color = new Color(1f, 1f, 1f, 1f);
			this.ReputationLabel.text = "AWARENESS";
			this.ReputationIcons[0].SetActive(true);
			this.ReputationIcons[1].SetActive(false);
			this.ReputationIcons[2].SetActive(false);
			this.ReputationIcons[3].SetActive(false);
			this.ReputationIcons[4].SetActive(false);
			this.ReputationIcons[5].SetActive(false);
			this.Clock.TimeLabel.fontStyle = FontStyle.Bold;
			this.Clock.TimeLabel.trueTypeFont = this.Arial;
			this.Clock.PeriodLabel.fontStyle = FontStyle.Bold;
			this.Clock.PeriodLabel.trueTypeFont = this.Arial;
			this.Watermark.fontStyle = FontStyle.Bold;
			this.Watermark.color = new Color(1f, 1f, 1f, 1f);
			this.Watermark.trueTypeFont = this.Arial;
			this.SubtitleLabel.color = new Color(1f, 1f, 1f, 1f);
			this.CautionSign.color = new Color(1f, 1f, 1f, 1f);
			this.FPS.color = new Color(1f, 1f, 1f, 1f);
			this.SanityLabel.color = new Color(1f, 1f, 1f, 1f);
			this.ColorCorrections = this.MainCamera.GetComponents<ColorCorrectionCurves>();
			this.StudentManager.MissionMode = true;
			this.NemesisDifficulty = MissionModeGlobals.NemesisDifficulty;
			this.Difficulty = MissionModeGlobals.MissionDifficulty;
			this.TargetID = MissionModeGlobals.MissionTarget;
			ClassGlobals.BiologyGrade = 1;
			ClassGlobals.ChemistryGrade = 1;
			ClassGlobals.LanguageGrade = 1;
			ClassGlobals.PhysicalGrade = 1;
			ClassGlobals.PsychologyGrade = 1;
			this.Yandere.StudentManager.TutorialWindow.gameObject.SetActive(false);
			OptionGlobals.TutorialsOff = true;
			SchoolGlobals.SchoolAtmosphereSet = true;
			SchoolGlobals.SchoolAtmosphere = 1f - (float)this.Difficulty * 0.1f;
			PlayerGlobals.Money = 20f;
			this.StudentManager.Atmosphere = 1f - (float)this.Difficulty * 0.1f;
			this.StudentManager.SetAtmosphere();
			this.ID = 1;
			while (this.ID < this.PoliceLabel.Length)
			{
				this.PoliceLabel[this.ID].fontStyle = FontStyle.Bold;
				this.PoliceLabel[this.ID].color = new Color(1f, 1f, 1f, 1f);
				this.PoliceLabel[this.ID].trueTypeFont = this.Arial;
				this.ID++;
			}
			this.ID = 1;
			while (this.ID < this.PoliceIcon.Length)
			{
				this.PoliceIcon[this.ID].color = new Color(1f, 1f, 1f, 1f);
				this.ID++;
			}
			if (this.Difficulty > 1)
			{
				this.ID = 2;
				while (this.ID < this.Difficulty + 1)
				{
					int missionCondition = MissionModeGlobals.GetMissionCondition(this.ID);
					if (missionCondition == 1)
					{
						this.RequiredWeaponID = MissionModeGlobals.MissionRequiredWeapon;
					}
					else if (missionCondition == 2)
					{
						this.RequiredClothingID = MissionModeGlobals.MissionRequiredClothing;
					}
					else if (missionCondition == 3)
					{
						this.RequiredDisposalID = MissionModeGlobals.MissionRequiredDisposal;
					}
					else if (missionCondition == 4)
					{
						this.NoCollateral = true;
					}
					else if (missionCondition == 5)
					{
						this.NoWitnesses = true;
					}
					else if (missionCondition == 6)
					{
						this.NoCorpses = true;
					}
					else if (missionCondition == 7)
					{
						this.NoWeapon = true;
					}
					else if (missionCondition == 8)
					{
						this.NoBlood = true;
					}
					else if (missionCondition == 9)
					{
						this.TimeLimit = true;
					}
					else if (missionCondition == 10)
					{
						this.NoSuspicion = true;
					}
					else if (missionCondition == 11)
					{
						this.SecurityCameras = true;
					}
					else if (missionCondition == 12)
					{
						this.MetalDetectors = true;
					}
					else if (missionCondition == 13)
					{
						this.NoSpeech = true;
					}
					else if (missionCondition == 14)
					{
						this.StealDocuments = true;
					}
					this.Conditions[this.ID] = missionCondition;
					this.ID++;
				}
			}
			if (!this.StealDocuments)
			{
				this.DocumentsStolen = true;
			}
			else
			{
				this.Safe.SetActive(true);
			}
			if (this.SecurityCameras)
			{
				this.SecurityCameraGroup.SetActive(true);
			}
			if (this.MetalDetectors)
			{
				this.MetalDetectorGroup.SetActive(true);
			}
			if (this.TimeLimit)
			{
				this.TimeLabel.gameObject.SetActive(true);
			}
			if (this.NoSpeech)
			{
				this.StudentManager.NoSpeech = true;
			}
			if (this.RequiredDisposalID == 0)
			{
				this.CorpseDisposed = true;
			}
			if (this.NemesisDifficulty > 0)
			{
				this.Nemesis.SetActive(true);
			}
			if (!this.NoWeapon)
			{
				this.WeaponDisposed = true;
			}
			if (!this.NoBlood)
			{
				this.BloodCleaned = true;
			}
			this.Jukebox.Egg = true;
			this.Jukebox.KillVolume();
			this.Jukebox.MissionMode.enabled = true;
			this.Jukebox.MissionMode.volume = 0f;
			this.Yandere.FixCamera();
			this.MainCamera.transform.position = new Vector3(this.MainCamera.transform.position.x, 6.51505f, -76.9222f);
			this.MainCamera.transform.eulerAngles = new Vector3(15f, this.MainCamera.transform.eulerAngles.y, this.MainCamera.transform.eulerAngles.z);
			this.Yandere.RPGCamera.enabled = false;
			this.Yandere.SanityBased = true;
			this.Yandere.CanMove = false;
			this.VoidGoddess.GetComponent<VoidGoddessScript>().Window.gameObject.SetActive(false);
			this.HeartbeatCamera.SetActive(false);
			this.TranqDetector.SetActive(false);
			this.VoidGoddess.SetActive(false);
			this.MurderKit.SetActive(false);
			this.TargetHeight = 1.51505f;
			this.Yandere.HUD.alpha = 0f;
			this.MusicIcon.color = new Color(this.MusicIcon.color.r, this.MusicIcon.color.g, this.MusicIcon.color.b, 1f);
			this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, 1f);
			this.MissionModeMenu.UpdateGraphics();
			this.MissionModeMenu.gameObject.SetActive(true);
			if (MissionModeGlobals.MultiMission)
			{
				this.NewMissionWindow.gameObject.SetActive(true);
				this.MissionModeMenu.gameObject.SetActive(false);
				this.NewMissionWindow.FillOutInfo();
				this.NewMissionWindow.HideButtons();
				this.MultiMission = true;
				for (int i = 1; i < 11; i++)
				{
					this.Target[i] = PlayerPrefs.GetInt("MissionModeTarget" + i);
					this.Method[i] = PlayerPrefs.GetInt("MissionModeMethod" + i);
				}
			}
			this.Enabled = true;
			return;
		}
		this.MissionModeMenu.gameObject.SetActive(false);
		this.TimeLabel.gameObject.SetActive(false);
		base.enabled = false;
	}

	// Token: 0x060000BE RID: 190 RVA: 0x0000F4B8 File Offset: 0x0000D6B8
	private void Update()
	{
		if (this.Phase == 1)
		{
			this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, Mathf.MoveTowards(this.Darkness.color.a, 0f, Time.deltaTime / 3f));
			if (this.Darkness.color.a == 0f)
			{
				this.Speed += Time.deltaTime / 3f;
				this.MainCamera.transform.position = new Vector3(this.MainCamera.transform.position.x, Mathf.Lerp(this.MainCamera.transform.position.y, this.TargetHeight, Time.deltaTime * this.Speed), this.MainCamera.transform.position.z);
				if (this.MainCamera.transform.position.y < this.TargetHeight + 0.1f)
				{
					this.Yandere.HUD.alpha = Mathf.MoveTowards(this.Yandere.HUD.alpha, 1f, Time.deltaTime / 3f);
					if (this.Yandere.HUD.alpha == 1f)
					{
						Debug.Log("Incrementing phase.");
						this.Yandere.RPGCamera.enabled = true;
						this.HeartbeatCamera.SetActive(true);
						this.Yandere.CanMove = true;
						this.Phase++;
					}
				}
			}
			if (Input.GetButtonDown("A"))
			{
				this.MainCamera.transform.position = new Vector3(this.MainCamera.transform.position.x, this.TargetHeight, this.MainCamera.transform.position.z);
				this.Yandere.RPGCamera.enabled = true;
				this.HeartbeatCamera.SetActive(true);
				this.Yandere.CanMove = true;
				this.Yandere.HUD.alpha = 1f;
				this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, 0f);
				this.Phase++;
				return;
			}
		}
		else if (this.Phase == 2)
		{
			if (this.MultiMission)
			{
				for (int i = 1; i < this.Target.Length; i++)
				{
					if (this.Target[i] == 0)
					{
						this.Checking[i] = false;
					}
					else if (this.Checking[i])
					{
						if (this.StudentManager.Students[this.Target[i]].transform.position.y < -11f)
						{
							this.GameOverID = 1;
							this.GameOver();
							this.Phase = 4;
						}
						else if (!this.StudentManager.Students[this.Target[i]].Alive)
						{
							if (this.Method[i] == 0)
							{
								if (this.StudentManager.Students[this.Target[i]].DeathType == DeathType.Weapon)
								{
									this.NewMissionWindow.DeathSkulls[i].SetActive(true);
									this.Checking[i] = false;
									this.CheckForCompletion();
								}
								else
								{
									this.GameOverID = 18;
									this.GameOver();
									this.Phase = 4;
								}
							}
							else if (this.Method[i] == 1)
							{
								if (this.StudentManager.Students[this.Target[i]].DeathType == DeathType.Drowning)
								{
									this.NewMissionWindow.DeathSkulls[i].SetActive(true);
									this.Checking[i] = false;
									this.CheckForCompletion();
								}
								else
								{
									this.GameOverID = 18;
									this.GameOver();
									this.Phase = 4;
								}
							}
							else if (this.Method[i] == 2)
							{
								if (this.StudentManager.Students[this.Target[i]].DeathType == DeathType.Poison)
								{
									this.NewMissionWindow.DeathSkulls[i].SetActive(true);
									this.Checking[i] = false;
									this.CheckForCompletion();
								}
								else
								{
									this.GameOverID = 18;
									this.GameOver();
									this.Phase = 4;
								}
							}
							else if (this.Method[i] == 3)
							{
								if (this.StudentManager.Students[this.Target[i]].DeathType == DeathType.Electrocution)
								{
									this.NewMissionWindow.DeathSkulls[i].SetActive(true);
									this.Checking[i] = false;
									this.CheckForCompletion();
								}
								else
								{
									this.GameOverID = 18;
									this.GameOver();
									this.Phase = 4;
								}
							}
							else if (this.Method[i] == 4)
							{
								if (this.StudentManager.Students[this.Target[i]].DeathType == DeathType.Burning)
								{
									this.NewMissionWindow.DeathSkulls[i].SetActive(true);
									this.Checking[i] = false;
									this.CheckForCompletion();
								}
								else
								{
									this.GameOverID = 18;
									this.GameOver();
									this.Phase = 4;
								}
							}
							else if (this.Method[i] == 5)
							{
								if (this.StudentManager.Students[this.Target[i]].DeathType == DeathType.Falling)
								{
									this.NewMissionWindow.DeathSkulls[i].SetActive(true);
									this.Checking[i] = false;
									this.CheckForCompletion();
								}
								else
								{
									this.GameOverID = 18;
									this.GameOver();
									this.Phase = 4;
								}
							}
							else if (this.Method[i] == 6)
							{
								if (this.StudentManager.Students[this.Target[i]].DeathType == DeathType.Weight)
								{
									this.NewMissionWindow.DeathSkulls[i].SetActive(true);
									this.Checking[i] = false;
									this.CheckForCompletion();
								}
								else
								{
									this.GameOverID = 18;
									this.GameOver();
									this.Phase = 4;
								}
							}
						}
					}
				}
			}
			if (!this.TargetDead && this.StudentManager.Students[this.TargetID] != null)
			{
				this.AlphabetArrow.LocalArrow.LookAt(this.StudentManager.Students[this.TargetID].transform.position);
				this.AlphabetArrow.transform.eulerAngles = this.AlphabetArrow.LocalArrow.eulerAngles - new Vector3(0f, this.StudentManager.MainCamera.transform.eulerAngles.y, 0f);
				this.AlphabetArrow.gameObject.SetActive(true);
				if (!this.StudentManager.Students[this.TargetID].Alive)
				{
					if (this.Yandere.Equipped > 0)
					{
						if (this.StudentManager.Students[this.TargetID].DeathType == DeathType.Weapon)
						{
							this.MurderWeaponID = this.Yandere.EquippedWeapon.WeaponID;
						}
						else
						{
							this.WeaponDisposed = true;
						}
					}
					else
					{
						this.WeaponDisposed = true;
					}
					this.AlphabetArrow.gameObject.SetActive(false);
					this.TargetDead = true;
				}
				if (this.StudentManager.Students[this.TargetID].transform.position.y < -11f)
				{
					this.GameOverID = 1;
					this.GameOver();
					this.Phase = 4;
				}
			}
			if (this.RequiredWeaponID > 0 && this.StudentManager.Students[this.TargetID] != null && !this.StudentManager.Students[this.TargetID].Alive && this.StudentManager.Students[this.TargetID].DeathCause != this.RequiredWeaponID)
			{
				this.Chastise = true;
				this.GameOverID = 2;
				this.GameOver();
				this.Phase = 4;
			}
			if (!this.CorrectClothingConfirmed && this.RequiredClothingID > 0 && this.StudentManager.Students[this.TargetID] != null && !this.StudentManager.Students[this.TargetID].Alive)
			{
				if (this.Yandere.Schoolwear != this.RequiredClothingID)
				{
					this.Chastise = true;
					this.GameOverID = 3;
					this.GameOver();
					this.Phase = 4;
				}
				else
				{
					this.CorrectClothingConfirmed = true;
				}
			}
			if (this.RequiredDisposalID > 0 && this.DisposalMethod == 0 && this.TargetDead)
			{
				this.ID = 1;
				while (this.ID < this.Incinerator.Victims + 1)
				{
					if (this.Incinerator.VictimList[this.ID] == this.TargetID && this.Incinerator.Smoke.isPlaying)
					{
						this.DisposalMethod = 1;
					}
					this.ID++;
				}
				int num = 0;
				this.ID = 1;
				while (this.ID < this.Incinerator.Limbs + 1)
				{
					if (this.Incinerator.LimbList[this.ID] == this.TargetID)
					{
						num++;
					}
					if (num == 6 && this.Incinerator.Smoke.isPlaying)
					{
						this.DisposalMethod = 1;
					}
					this.ID++;
				}
				this.ID = 1;
				while (this.ID < this.WoodChipper.Victims + 1)
				{
					if (this.WoodChipper.VictimList[this.ID] == this.TargetID && this.WoodChipper.Shredding)
					{
						this.DisposalMethod = 2;
					}
					this.ID++;
				}
				this.ID = 1;
				while (this.ID < this.GardenHoles.Length)
				{
					if (this.GardenHoles[this.ID].VictimID == this.TargetID && !this.GardenHoles[this.ID].enabled)
					{
						this.DisposalMethod = 3;
					}
					this.ID++;
				}
				if (this.DisposalMethod > 0)
				{
					if (this.DisposalMethod != this.RequiredDisposalID)
					{
						this.Chastise = true;
						this.GameOverID = 4;
						this.GameOver();
						this.Phase = 4;
					}
					else
					{
						this.CorpseDisposed = true;
					}
				}
			}
			if (this.NoCollateral)
			{
				if (this.Police.Corpses == 1)
				{
					if (this.Police.CorpseList[0].StudentID != this.TargetID)
					{
						this.Chastise = true;
						this.GameOverID = 5;
						this.GameOver();
						this.Phase = 4;
					}
				}
				else if (this.Police.Corpses > 1)
				{
					this.GameOverID = 5;
					this.GameOver();
					this.Phase = 4;
				}
			}
			if (this.NoWitnesses)
			{
				this.ID = 1;
				while (this.ID < this.StudentManager.Students.Length)
				{
					if (this.StudentManager.Students[this.ID] != null && this.StudentManager.Students[this.ID].WitnessedMurder && !this.Yandere.DelinquentFighting)
					{
						this.SpottedLabel.text = this.StudentManager.Students[this.ID].Name;
						this.SpottedWindow.SetActive(true);
						this.Chastise = true;
						this.GameOverID = 6;
						if (this.Yandere.Mopping)
						{
							this.GameOverID = 20;
						}
						this.GameOver();
						this.Phase = 4;
					}
					this.ID++;
				}
			}
			if (this.NoCorpses)
			{
				this.ID = 1;
				while (this.ID < this.StudentManager.Students.Length)
				{
					if (this.StudentManager.Students[this.ID] != null && (this.StudentManager.Students[this.ID].WitnessedCorpse || this.StudentManager.Students[this.ID].WitnessedMurder))
					{
						this.SpottedLabel.text = this.StudentManager.Students[this.ID].Name;
						this.SpottedWindow.SetActive(true);
						this.Chastise = true;
						if (this.Yandere.DelinquentFighting)
						{
							this.GameOverID = 19;
						}
						else
						{
							this.GameOverID = 7;
						}
						this.GameOver();
						this.Phase = 4;
					}
					this.ID++;
				}
			}
			if (this.NoBlood)
			{
				if (this.Police.Deaths > 0)
				{
					this.CheckForBlood = true;
				}
				if (this.CheckForBlood)
				{
					if (this.Police.BloodParent.childCount == 0)
					{
						this.BloodTimer += Time.deltaTime;
						if (this.BloodTimer > 2f)
						{
							this.BloodCleaned = true;
						}
					}
					else
					{
						this.BloodTimer = 0f;
					}
				}
			}
			if (this.NoWeapon && !this.WeaponDisposed && this.Incinerator.Timer > 0f)
			{
				this.ID = 1;
				while (this.ID < this.Incinerator.DestroyedEvidence + 1)
				{
					if (this.Incinerator.EvidenceList[this.ID] == this.MurderWeaponID)
					{
						this.WeaponDisposed = true;
					}
					this.ID++;
				}
			}
			if (this.TimeLimit)
			{
				if (!this.Yandere.PauseScreen.Show)
				{
					this.TimeRemaining = Mathf.MoveTowards(this.TimeRemaining, 0f, Time.deltaTime);
				}
				int num2 = Mathf.CeilToInt(this.TimeRemaining);
				int num3 = num2 / 60;
				int num4 = num2 % 60;
				this.TimeLabel.text = string.Format("{0:00}:{1:00}", num3, num4);
				if (this.TimeRemaining == 0f)
				{
					this.Chastise = true;
					this.GameOverID = 10;
					this.GameOver();
					this.Phase = 4;
				}
			}
			if (this.Reputation.Reputation + this.Reputation.PendingRep <= -100f)
			{
				this.GameOverID = 14;
				this.GameOver();
				this.Phase = 4;
			}
			if (this.NoSuspicion && this.Reputation.Reputation + this.Reputation.PendingRep < 0f)
			{
				this.GameOverID = 14;
				this.GameOver();
				this.Phase = 4;
			}
			if (this.HeartbrokenCamera.activeInHierarchy)
			{
				this.HeartbrokenCamera.SetActive(false);
				this.GameOverID = 0;
				this.GameOver();
				this.Phase = 4;
			}
			if (this.Clock.PresentTime > 1080f)
			{
				this.GameOverID = 11;
				this.GameOver();
				this.Phase = 4;
			}
			else if (this.Police.FadeOut)
			{
				this.GameOverID = 12;
				this.GameOver();
				this.Phase = 4;
			}
			if (this.Yandere.ShoulderCamera.Noticed)
			{
				if (this.Yandere.Senpai.GetComponent<StudentScript>().Club == ClubType.Council)
				{
					this.GameOverID = 21;
				}
				else
				{
					this.GameOverID = 17;
				}
				this.GameOver();
				this.Phase = 4;
			}
			if (this.ExitPortal.activeInHierarchy)
			{
				if (this.Yandere.Chased || this.Yandere.Chasers > 0)
				{
					this.ExitPortalPrompt.Label[0].text = "     Cannot Exfiltrate!";
					this.ExitPortalPrompt.Circle[0].fillAmount = 1f;
				}
				else
				{
					this.ExitPortalPrompt.Label[0].text = "     Exfiltrate";
					if (this.ExitPortalPrompt.Circle[0].fillAmount == 0f)
					{
						this.StudentManager.DisableChaseCameras();
						this.MainCamera.transform.position = new Vector3(0.5f, 2.25f, -100.5f);
						this.MainCamera.transform.eulerAngles = Vector3.zero;
						this.Yandere.transform.eulerAngles = new Vector3(0f, 180f, 0f);
						this.Yandere.transform.position = new Vector3(0f, 0f, -94.5f);
						this.Yandere.Character.GetComponent<Animation>().Play(this.Yandere.WalkAnim);
						this.Yandere.RPGCamera.enabled = false;
						this.Yandere.HUD.gameObject.SetActive(false);
						this.Yandere.CanMove = false;
						AudioSource component = this.Jukebox.MissionMode.GetComponent<AudioSource>();
						component.clip = this.StealthMusic[10];
						component.loop = false;
						component.Play();
						base.GetComponent<AudioSource>().PlayOneShot(this.InfoAccomplished);
						this.HeartbeatCamera.SetActive(false);
						this.Boundary.enabled = false;
						this.Phase++;
					}
				}
			}
			if (this.TargetDead && this.CorpseDisposed && this.BloodCleaned && this.WeaponDisposed && this.DocumentsStolen && this.GameOverID == 0 && !this.ExitPortal.activeInHierarchy)
			{
				this.NotificationManager.DisplayNotification(NotificationType.Complete);
				this.NotificationManager.DisplayNotification(NotificationType.Exfiltrate);
				base.GetComponent<AudioSource>().PlayOneShot(this.InfoExfiltrate);
				this.AlphabetArrow.gameObject.SetActive(true);
				this.ExitPortal.SetActive(true);
			}
			if (this.NoBlood && this.BloodCleaned && this.Police.BloodParent.childCount > 0)
			{
				this.ExitPortal.SetActive(false);
				this.BloodCleaned = false;
				this.BloodTimer = 0f;
			}
			if (!this.InfoRemark && this.GameOverID == 0 && this.TargetDead && (!this.CorpseDisposed || !this.BloodCleaned || !this.WeaponDisposed))
			{
				base.GetComponent<AudioSource>().PlayOneShot(this.InfoObjective);
				this.InfoRemark = true;
			}
			if (this.ExitPortal.activeInHierarchy)
			{
				this.AlphabetArrow.LocalArrow.LookAt(new Vector3(0f, 0f, this.ExitPortal.transform.position.z));
				this.AlphabetArrow.transform.eulerAngles = this.AlphabetArrow.LocalArrow.transform.eulerAngles - new Vector3(0f, this.StudentManager.MainCamera.transform.eulerAngles.y, 0f);
				return;
			}
		}
		else if (this.Phase == 3)
		{
			this.Timer += Time.deltaTime;
			this.MainCamera.transform.position = new Vector3(this.MainCamera.transform.position.x, this.MainCamera.transform.position.y - Time.deltaTime * 0.2f, this.MainCamera.transform.position.z);
			this.Yandere.transform.position = new Vector3(this.Yandere.transform.position.x, this.Yandere.transform.position.y, this.Yandere.transform.position.z - Time.deltaTime);
			if (this.Timer > 5f)
			{
				this.Success();
				this.Timer = 0f;
				this.Phase++;
				return;
			}
		}
		else if (this.Phase == 4)
		{
			this.Timer += 0.016666668f;
			if (this.Timer > 1f)
			{
				if (!this.FadeOut)
				{
					if (!this.PromptBar.Show)
					{
						this.PromptBar.Show = true;
					}
					else if (Input.GetButtonDown("A"))
					{
						this.PromptBar.Show = false;
						this.Destination = 1;
						this.FadeOut = true;
					}
					else if (Input.GetButtonDown("B"))
					{
						this.PromptBar.Show = false;
						this.Destination = 2;
						this.FadeOut = true;
					}
					else if (Input.GetButtonDown("X"))
					{
						this.PromptBar.Show = false;
						this.Destination = 3;
						this.FadeOut = true;
					}
					else if (Input.GetButtonDown("Y"))
					{
						this.PromptBar.Show = false;
						this.Destination = 4;
						this.FadeOut = true;
					}
				}
				else
				{
					this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, Mathf.MoveTowards(this.Darkness.color.a, 1f, 0.016666668f));
					this.Jukebox.Dip = Mathf.MoveTowards(this.Jukebox.Dip, 0f, 0.016666668f);
					if (this.Darkness.color.a > 0.9921875f)
					{
						if (this.Destination == 1)
						{
							this.LoadingLabel.enabled = true;
							this.ResetGlobals();
							SceneManager.LoadScene(SceneManager.GetActiveScene().name);
						}
						else if (this.Destination == 2)
						{
							Globals.DeleteAll();
							SceneManager.LoadScene("MissionModeScene");
						}
						else if (this.Destination == 3)
						{
							Globals.DeleteAll();
							SceneManager.LoadScene("TitleScene");
						}
						else if (this.Destination == 4)
						{
							Globals.DeleteAll();
							SceneManager.LoadScene("DiscordScene");
						}
					}
				}
			}
			if (this.GameOverPhase == 1)
			{
				if (this.Timer > 2.5f)
				{
					if (this.Chastise)
					{
						base.GetComponent<AudioSource>().PlayOneShot(this.InfoFailure);
						this.GameOverPhase++;
						return;
					}
					this.GameOverPhase++;
					this.Timer += 5f;
					return;
				}
			}
			else if (this.GameOverPhase == 2 && this.Timer > 7.5f)
			{
				this.Jukebox.MissionMode.GetComponent<AudioSource>().clip = this.StealthMusic[0];
				this.Jukebox.MissionMode.GetComponent<AudioSource>().Play();
				this.Jukebox.Volume = 0.5f;
				this.GameOverPhase++;
			}
		}
	}

	// Token: 0x060000BF RID: 191 RVA: 0x00010B68 File Offset: 0x0000ED68
	public void GameOver()
	{
		if (this.Yandere.Aiming)
		{
			this.Yandere.StopAiming();
		}
		if (this.Yandere.YandereVision)
		{
			this.Yandere.YandereVision = false;
			this.Yandere.ResetYandereEffects();
		}
		this.Yandere.enabled = false;
		this.GameOverReason.text = this.GameOverReasons[this.GameOverID];
		if (this.ColorCorrections.Length != 0)
		{
			this.ColorCorrections[2].enabled = true;
		}
		base.GetComponent<AudioSource>().PlayOneShot(this.GameOverSound);
		this.DetectionCamera.SetActive(false);
		this.HeartbeatCamera.SetActive(false);
		this.WitnessCamera.SetActive(false);
		this.GameOverText.SetActive(true);
		this.Yandere.HUD.gameObject.SetActive(false);
		this.Subtitle.SetActive(false);
		Time.timeScale = 0.0001f;
		this.GameOverPhase = 1;
		this.Jukebox.MissionMode.GetComponent<AudioSource>().Stop();
	}

	// Token: 0x060000C0 RID: 192 RVA: 0x00010C78 File Offset: 0x0000EE78
	private void Success()
	{
		while (!this.Valid)
		{
			this.RandomNumber = (float)UnityEngine.Random.Range(1000000, 10000000);
			if (this.RandomNumber / 9f % 5f == 0f)
			{
				this.Valid = true;
			}
		}
		this.DiscordCodeLabel.text = string.Concat(this.RandomNumber);
		this.DiscordCodeLabel.transform.parent.gameObject.SetActive(true);
		this.GameOverHeader.transform.localPosition = new Vector3(this.GameOverHeader.transform.localPosition.x, 0f, this.GameOverHeader.transform.localPosition.z);
		this.GameOverHeader.text = "MISSION ACCOMPLISHED";
		this.GameOverReason.gameObject.SetActive(false);
		this.ColorCorrections[2].enabled = true;
		this.DetectionCamera.SetActive(false);
		this.WitnessCamera.SetActive(false);
		this.GameOverText.SetActive(true);
		this.GameOverReason.text = string.Empty;
		this.Subtitle.SetActive(false);
		this.Jukebox.Volume = 1f;
		Time.timeScale = 0.0001f;
	}

	// Token: 0x060000C1 RID: 193 RVA: 0x00010DC8 File Offset: 0x0000EFC8
	public void ChangeMusic()
	{
		this.MusicID++;
		if (this.MusicID > 9)
		{
			this.MusicID = 1;
		}
		this.Jukebox.MissionMode.GetComponent<AudioSource>().clip = this.StealthMusic[this.MusicID];
		this.Jukebox.MissionMode.GetComponent<AudioSource>().Play();
	}

	// Token: 0x060000C2 RID: 194 RVA: 0x00010E2C File Offset: 0x0000F02C
	private void ResetGlobals()
	{
		Debug.Log("Mission Difficulty was: " + MissionModeGlobals.MissionDifficulty);
		int disableFarAnimations = OptionGlobals.DisableFarAnimations;
		bool disablePostAliasing = OptionGlobals.DisablePostAliasing;
		bool disableOutlines = OptionGlobals.DisableOutlines;
		int lowDetailStudents = OptionGlobals.LowDetailStudents;
		int particleCount = OptionGlobals.ParticleCount;
		bool enableShadows = OptionGlobals.EnableShadows;
		int drawDistance = OptionGlobals.DrawDistance;
		int drawDistanceLimit = OptionGlobals.DrawDistanceLimit;
		bool disableBloom = OptionGlobals.DisableBloom;
		bool fog = OptionGlobals.Fog;
		bool nemesisAggression = MissionModeGlobals.NemesisAggression;
		string missionTargetName = MissionModeGlobals.MissionTargetName;
		bool highPopulation = OptionGlobals.HighPopulation;
		Globals.DeleteAll();
		OptionGlobals.TutorialsOff = true;
		for (int i = 1; i < 11; i++)
		{
			PlayerPrefs.SetInt("MissionModeTarget" + i, this.Target[i]);
			PlayerPrefs.SetInt("MissionModeMethod" + i, this.Method[i]);
		}
		SchoolGlobals.SchoolAtmosphere = 1f - (float)this.Difficulty * 0.1f;
		MissionModeGlobals.MissionTargetName = missionTargetName;
		MissionModeGlobals.MissionDifficulty = this.Difficulty;
		OptionGlobals.HighPopulation = highPopulation;
		MissionModeGlobals.MissionTarget = this.TargetID;
		SchoolGlobals.SchoolAtmosphereSet = true;
		MissionModeGlobals.MissionMode = true;
		MissionModeGlobals.MultiMission = this.MultiMission;
		MissionModeGlobals.MissionRequiredWeapon = this.RequiredWeaponID;
		MissionModeGlobals.MissionRequiredClothing = this.RequiredClothingID;
		MissionModeGlobals.MissionRequiredDisposal = this.RequiredDisposalID;
		ClassGlobals.BiologyGrade = 1;
		ClassGlobals.ChemistryGrade = 1;
		ClassGlobals.LanguageGrade = 1;
		ClassGlobals.PhysicalGrade = 1;
		ClassGlobals.PsychologyGrade = 1;
		this.ID = 2;
		while (this.ID < 11)
		{
			MissionModeGlobals.SetMissionCondition(this.ID, this.Conditions[this.ID]);
			this.ID++;
		}
		MissionModeGlobals.NemesisDifficulty = this.NemesisDifficulty;
		MissionModeGlobals.NemesisAggression = nemesisAggression;
		OptionGlobals.DisableFarAnimations = disableFarAnimations;
		OptionGlobals.DisablePostAliasing = disablePostAliasing;
		OptionGlobals.DisableOutlines = disableOutlines;
		OptionGlobals.LowDetailStudents = lowDetailStudents;
		OptionGlobals.ParticleCount = particleCount;
		OptionGlobals.EnableShadows = enableShadows;
		OptionGlobals.DrawDistance = drawDistance;
		OptionGlobals.DrawDistanceLimit = drawDistanceLimit;
		OptionGlobals.DisableBloom = disableBloom;
		OptionGlobals.Fog = fog;
		Debug.Log("Mission Difficulty is now: " + MissionModeGlobals.MissionDifficulty);
	}

	// Token: 0x060000C3 RID: 195 RVA: 0x0001103C File Offset: 0x0000F23C
	private void ChangeAllText()
	{
		foreach (UILabel uilabel in UnityEngine.Object.FindObjectsOfType<UILabel>())
		{
			float a = uilabel.color.a;
			uilabel.color = new Color(1f, 1f, 1f, a);
			uilabel.trueTypeFont = this.Arial;
		}
		foreach (UISprite uisprite in UnityEngine.Object.FindObjectsOfType<UISprite>())
		{
			float a2 = uisprite.color.a;
			if (uisprite.color != new Color(0f, 0f, 0f, a2))
			{
				uisprite.color = new Color(1f, 1f, 1f, a2);
			}
		}
	}

	// Token: 0x060000C4 RID: 196 RVA: 0x000110FC File Offset: 0x0000F2FC
	private void CheckForCompletion()
	{
		if (!this.Checking[1] && !this.Checking[2] && !this.Checking[3] && !this.Checking[4] && !this.Checking[5] && !this.Checking[6] && !this.Checking[7] && !this.Checking[8] && !this.Checking[9] && !this.Checking[10])
		{
			this.TargetDead = true;
		}
	}

	// Token: 0x040001E0 RID: 480
	public NotificationManagerScript NotificationManager;

	// Token: 0x040001E1 RID: 481
	public NewMissionWindowScript NewMissionWindow;

	// Token: 0x040001E2 RID: 482
	public MissionModeMenuScript MissionModeMenu;

	// Token: 0x040001E3 RID: 483
	public StudentManagerScript StudentManager;

	// Token: 0x040001E4 RID: 484
	public WeaponManagerScript WeaponManager;

	// Token: 0x040001E5 RID: 485
	public PromptScript ExitPortalPrompt;

	// Token: 0x040001E6 RID: 486
	public IncineratorScript Incinerator;

	// Token: 0x040001E7 RID: 487
	public WoodChipperScript WoodChipper;

	// Token: 0x040001E8 RID: 488
	public AlphabetScript AlphabetArrow;

	// Token: 0x040001E9 RID: 489
	public ReputationScript Reputation;

	// Token: 0x040001EA RID: 490
	public GrayscaleEffect Grayscale;

	// Token: 0x040001EB RID: 491
	public PromptBarScript PromptBar;

	// Token: 0x040001EC RID: 492
	public BoundaryScript Boundary;

	// Token: 0x040001ED RID: 493
	public JukeboxScript Jukebox;

	// Token: 0x040001EE RID: 494
	public YandereScript Yandere;

	// Token: 0x040001EF RID: 495
	public PoliceScript Police;

	// Token: 0x040001F0 RID: 496
	public ClockScript Clock;

	// Token: 0x040001F1 RID: 497
	public UILabel EventSubtitleLabel;

	// Token: 0x040001F2 RID: 498
	public UILabel ReputationLabel;

	// Token: 0x040001F3 RID: 499
	public UILabel GameOverHeader;

	// Token: 0x040001F4 RID: 500
	public UILabel GameOverReason;

	// Token: 0x040001F5 RID: 501
	public UILabel SubtitleLabel;

	// Token: 0x040001F6 RID: 502
	public UILabel LoadingLabel;

	// Token: 0x040001F7 RID: 503
	public UILabel SpottedLabel;

	// Token: 0x040001F8 RID: 504
	public UILabel SanityLabel;

	// Token: 0x040001F9 RID: 505
	public UILabel MoneyLabel;

	// Token: 0x040001FA RID: 506
	public UILabel TimeLabel;

	// Token: 0x040001FB RID: 507
	public UISprite ReputationFace1;

	// Token: 0x040001FC RID: 508
	public UISprite ReputationFace2;

	// Token: 0x040001FD RID: 509
	public UISprite ReputationBG;

	// Token: 0x040001FE RID: 510
	public UISprite CautionSign;

	// Token: 0x040001FF RID: 511
	public UISprite MusicIcon;

	// Token: 0x04000200 RID: 512
	public UISprite Darkness;

	// Token: 0x04000201 RID: 513
	public UILabel FPS;

	// Token: 0x04000202 RID: 514
	public GardenHoleScript[] GardenHoles;

	// Token: 0x04000203 RID: 515
	public GameObject[] ReputationIcons;

	// Token: 0x04000204 RID: 516
	public string[] GameOverReasons;

	// Token: 0x04000205 RID: 517
	public AudioClip[] StealthMusic;

	// Token: 0x04000206 RID: 518
	public Transform[] SpawnPoints;

	// Token: 0x04000207 RID: 519
	public UISprite[] PoliceIcon;

	// Token: 0x04000208 RID: 520
	public UILabel[] PoliceLabel;

	// Token: 0x04000209 RID: 521
	public int[] Conditions;

	// Token: 0x0400020A RID: 522
	public GameObject SecurityCameraGroup;

	// Token: 0x0400020B RID: 523
	public GameObject MetalDetectorGroup;

	// Token: 0x0400020C RID: 524
	public GameObject HeartbrokenCamera;

	// Token: 0x0400020D RID: 525
	public GameObject DetectionCamera;

	// Token: 0x0400020E RID: 526
	public GameObject HeartbeatCamera;

	// Token: 0x0400020F RID: 527
	public GameObject MissionModeHUD;

	// Token: 0x04000210 RID: 528
	public GameObject SpottedWindow;

	// Token: 0x04000211 RID: 529
	public GameObject TranqDetector;

	// Token: 0x04000212 RID: 530
	public GameObject WitnessCamera;

	// Token: 0x04000213 RID: 531
	public GameObject GameOverText;

	// Token: 0x04000214 RID: 532
	public GameObject VoidGoddess;

	// Token: 0x04000215 RID: 533
	public GameObject Headmaster;

	// Token: 0x04000216 RID: 534
	public GameObject ExitPortal;

	// Token: 0x04000217 RID: 535
	public GameObject MurderKit;

	// Token: 0x04000218 RID: 536
	public GameObject Subtitle;

	// Token: 0x04000219 RID: 537
	public GameObject Nemesis;

	// Token: 0x0400021A RID: 538
	public GameObject Safe;

	// Token: 0x0400021B RID: 539
	public Transform LastKnownPosition;

	// Token: 0x0400021C RID: 540
	public int RequiredClothingID;

	// Token: 0x0400021D RID: 541
	public int RequiredDisposalID;

	// Token: 0x0400021E RID: 542
	public int RequiredWeaponID;

	// Token: 0x0400021F RID: 543
	public int NemesisDifficulty;

	// Token: 0x04000220 RID: 544
	public int DisposalMethod;

	// Token: 0x04000221 RID: 545
	public int MurderWeaponID;

	// Token: 0x04000222 RID: 546
	public int GameOverPhase;

	// Token: 0x04000223 RID: 547
	public int Destination;

	// Token: 0x04000224 RID: 548
	public int Difficulty;

	// Token: 0x04000225 RID: 549
	public int GameOverID;

	// Token: 0x04000226 RID: 550
	public int TargetID;

	// Token: 0x04000227 RID: 551
	public int MusicID = 1;

	// Token: 0x04000228 RID: 552
	public int Phase = 1;

	// Token: 0x04000229 RID: 553
	public int ID;

	// Token: 0x0400022A RID: 554
	public int[] Target;

	// Token: 0x0400022B RID: 555
	public int[] Method;

	// Token: 0x0400022C RID: 556
	public bool SecurityCameras;

	// Token: 0x0400022D RID: 557
	public bool MetalDetectors;

	// Token: 0x0400022E RID: 558
	public bool StealDocuments;

	// Token: 0x0400022F RID: 559
	public bool NoCollateral;

	// Token: 0x04000230 RID: 560
	public bool NoSuspicion;

	// Token: 0x04000231 RID: 561
	public bool NoWitnesses;

	// Token: 0x04000232 RID: 562
	public bool NoCorpses;

	// Token: 0x04000233 RID: 563
	public bool NoSpeech;

	// Token: 0x04000234 RID: 564
	public bool NoWeapon;

	// Token: 0x04000235 RID: 565
	public bool NoBlood;

	// Token: 0x04000236 RID: 566
	public bool TimeLimit;

	// Token: 0x04000237 RID: 567
	public bool CorrectClothingConfirmed;

	// Token: 0x04000238 RID: 568
	public bool DocumentsStolen;

	// Token: 0x04000239 RID: 569
	public bool CorpseDisposed;

	// Token: 0x0400023A RID: 570
	public bool WeaponDisposed;

	// Token: 0x0400023B RID: 571
	public bool CheckForBlood;

	// Token: 0x0400023C RID: 572
	public bool BloodCleaned;

	// Token: 0x0400023D RID: 573
	public bool MultiMission;

	// Token: 0x0400023E RID: 574
	public bool InfoRemark;

	// Token: 0x0400023F RID: 575
	public bool TargetDead;

	// Token: 0x04000240 RID: 576
	public bool Chastise;

	// Token: 0x04000241 RID: 577
	public bool FadeOut;

	// Token: 0x04000242 RID: 578
	public bool Enabled;

	// Token: 0x04000243 RID: 579
	public bool[] Checking;

	// Token: 0x04000244 RID: 580
	public string CauseOfFailure = string.Empty;

	// Token: 0x04000245 RID: 581
	public float TimeRemaining = 300f;

	// Token: 0x04000246 RID: 582
	public float TargetHeight;

	// Token: 0x04000247 RID: 583
	public float BloodTimer;

	// Token: 0x04000248 RID: 584
	public float Speed;

	// Token: 0x04000249 RID: 585
	public float Timer;

	// Token: 0x0400024A RID: 586
	public AudioClip InfoAccomplished;

	// Token: 0x0400024B RID: 587
	public AudioClip InfoExfiltrate;

	// Token: 0x0400024C RID: 588
	public AudioClip InfoObjective;

	// Token: 0x0400024D RID: 589
	public AudioClip InfoFailure;

	// Token: 0x0400024E RID: 590
	public AudioClip GameOverSound;

	// Token: 0x0400024F RID: 591
	public ColorCorrectionCurves[] ColorCorrections;

	// Token: 0x04000250 RID: 592
	public Camera MainCamera;

	// Token: 0x04000251 RID: 593
	public UILabel Watermark;

	// Token: 0x04000252 RID: 594
	public Font Arial;

	// Token: 0x04000253 RID: 595
	public int Frame;

	// Token: 0x04000254 RID: 596
	public UILabel DiscordCodeLabel;

	// Token: 0x04000255 RID: 597
	public float RandomNumber;

	// Token: 0x04000256 RID: 598
	public bool Valid;
}
