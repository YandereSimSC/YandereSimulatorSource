using System;
using System.Collections;
using HighlightingSystem;
using Pathfinding;
using UnityEngine;
using XInputDotNetPure;

// Token: 0x0200046F RID: 1135
public class YandereScript : MonoBehaviour
{
	// Token: 0x06001D2C RID: 7468 RVA: 0x0015D550 File Offset: 0x0015B750
	private void Start()
	{
		this.PhysicalGrade = ClassGlobals.PhysicalGrade;
		this.SpeedBonus = PlayerGlobals.SpeedBonus;
		this.SanitySmudges.color = new Color(1f, 1f, 1f, 0f);
		this.SpiderLegs.SetActive(GameGlobals.EmptyDemon);
		this.MyRenderer.materials[2].SetFloat("_BlendAmount1", 0f);
		this.SetAnimationLayers();
		this.UpdateNumbness();
		this.RightEyeOrigin = this.RightEye.localPosition;
		this.LeftEyeOrigin = this.LeftEye.localPosition;
		this.CharacterAnimation["f02_yanderePose_00"].weight = 0f;
		this.CharacterAnimation["f02_cameraPose_00"].weight = 0f;
		this.CharacterAnimation["f02_selfie_00"].weight = 0f;
		this.CharacterAnimation["f02_shipGirlSnap_00"].speed = 2f;
		this.CharacterAnimation["f02_gazerSnap_00"].speed = 2f;
		this.CharacterAnimation["f02_performing_00"].speed = 0.9f;
		this.CharacterAnimation["f02_sithAttack_00"].speed = 1.5f;
		this.CharacterAnimation["f02_sithAttack_01"].speed = 1.5f;
		this.CharacterAnimation["f02_sithAttack_02"].speed = 1.5f;
		this.CharacterAnimation["f02_sithAttackHard_00"].speed = 1.5f;
		this.CharacterAnimation["f02_sithAttackHard_01"].speed = 1.5f;
		this.CharacterAnimation["f02_sithAttackHard_02"].speed = 1.5f;
		this.CharacterAnimation["f02_nierRun_00"].speed = 1.5f;
		ColorCorrectionCurves[] components = Camera.main.GetComponents<ColorCorrectionCurves>();
		Vignetting[] components2 = Camera.main.GetComponents<Vignetting>();
		this.YandereColorCorrection = components[1];
		this.Vignette = components2[1];
		this.ResetYandereEffects();
		this.ResetSenpaiEffects();
		this.Sanity = 100f;
		this.Bloodiness = 0f;
		this.SetUniform();
		this.EasterEggMenu.transform.localPosition = new Vector3(this.EasterEggMenu.transform.localPosition.x, 0f, this.EasterEggMenu.transform.localPosition.z);
		this.ProgressBar.transform.parent.gameObject.SetActive(false);
		this.Smartphone.transform.parent.gameObject.SetActive(false);
		this.ObstacleDetector.gameObject.SetActive(false);
		this.SithBeam[1].gameObject.SetActive(false);
		this.SithBeam[2].gameObject.SetActive(false);
		this.PunishedAccessories.SetActive(false);
		this.SukebanAccessories.SetActive(false);
		this.FalconShoulderpad.SetActive(false);
		this.CensorSteam[0].SetActive(false);
		this.CensorSteam[1].SetActive(false);
		this.CensorSteam[2].SetActive(false);
		this.CensorSteam[3].SetActive(false);
		this.FloatingShovel.SetActive(false);
		this.BlackEyePatch.SetActive(false);
		this.EasterEggMenu.SetActive(false);
		this.FalconKneepad1.SetActive(false);
		this.FalconKneepad2.SetActive(false);
		this.PunishedScarf.SetActive(false);
		this.FalconBuckle.SetActive(false);
		this.FalconHelmet.SetActive(false);
		this.TornadoDress.SetActive(false);
		this.Stand.Stand.SetActive(false);
		this.TornadoHair.SetActive(false);
		this.MemeGlasses.SetActive(false);
		this.CirnoWings.SetActive(false);
		this.KONGlasses.SetActive(false);
		this.EbolaWings.SetActive(false);
		this.Microphone.SetActive(false);
		this.Poisons[1].SetActive(false);
		this.Poisons[2].SetActive(false);
		this.Poisons[3].SetActive(false);
		this.BladeHair.SetActive(false);
		this.CirnoHair.SetActive(false);
		this.EbolaHair.SetActive(false);
		this.EyepatchL.SetActive(false);
		this.EyepatchR.SetActive(false);
		this.Handcuffs.SetActive(false);
		this.ZipTie[0].SetActive(false);
		this.ZipTie[1].SetActive(false);
		this.Shoes[0].SetActive(false);
		this.Shoes[1].SetActive(false);
		this.Phone.SetActive(false);
		this.Cape.SetActive(false);
		this.HeavySwordParent.gameObject.SetActive(false);
		this.LightSwordParent.gameObject.SetActive(false);
		this.Pod.SetActive(false);
		this.OriginalIdleAnim = this.IdleAnim;
		this.OriginalWalkAnim = this.WalkAnim;
		this.OriginalRunAnim = this.RunAnim;
		GameObject[] array = this.Armor;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(false);
		}
		this.ID = 1;
		while (this.ID < this.Accessories.Length)
		{
			this.Accessories[this.ID].SetActive(false);
			this.ID++;
		}
		array = this.PunishedArm;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(false);
		}
		array = this.GaloAccessories;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(false);
		}
		array = this.Vectors;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(false);
		}
		this.ID = 1;
		while (this.ID < this.CyborgParts.Length)
		{
			this.CyborgParts[this.ID].SetActive(false);
			this.ID++;
		}
		this.ID = 0;
		while (this.ID < this.KLKParts.Length)
		{
			this.KLKParts[this.ID].SetActive(false);
			this.ID++;
		}
		this.ID = 0;
		while (this.ID < this.BanchoAccessories.Length)
		{
			this.BanchoAccessories[this.ID].SetActive(false);
			this.ID++;
		}
		if (PlayerGlobals.PantiesEquipped == 5)
		{
			this.RunSpeed += 1f;
		}
		if (PlayerGlobals.Headset)
		{
			this.Inventory.Headset = true;
		}
		this.UpdateHair();
		this.ClubAccessory();
		if (MissionModeGlobals.MissionMode || GameGlobals.LoveSick)
		{
			this.NoDebug = true;
		}
		if (GameGlobals.BlondeHair)
		{
			this.PonytailRenderer.material.mainTexture = this.BlondePony;
		}
		if (this.StudentManager.Students[11] != null)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
		this.MyRenderer.materials[0].SetFloat("_BlendAmount", 1f);
		this.MyRenderer.materials[1].SetFloat("_BlendAmount", 1f);
	}

	// Token: 0x17000489 RID: 1161
	// (get) Token: 0x06001D2D RID: 7469 RVA: 0x0015DC9D File Offset: 0x0015BE9D
	// (set) Token: 0x06001D2E RID: 7470 RVA: 0x0015DCA8 File Offset: 0x0015BEA8
	public float Sanity
	{
		get
		{
			return this.sanity;
		}
		set
		{
			this.sanity = Mathf.Clamp(value, 0f, 100f);
			if (this.sanity > 66.66666f)
			{
				this.HeartRate.SetHeartRateColour(this.HeartRate.NormalColour);
				this.SanityWarning = false;
			}
			else if (this.sanity > 33.33333f)
			{
				this.HeartRate.SetHeartRateColour(this.HeartRate.MediumColour);
				this.SanityWarning = false;
				if (this.PreviousSanity < 33.33333f)
				{
					this.StudentManager.UpdateStudents(0);
				}
			}
			else
			{
				this.HeartRate.SetHeartRateColour(this.HeartRate.BadColour);
				if (!this.SanityWarning)
				{
					this.NotificationManager.DisplayNotification(NotificationType.Insane);
					this.StudentManager.TutorialWindow.ShowSanityMessage = true;
					this.SanityWarning = true;
				}
			}
			this.HeartRate.BeatsPerMinute = (int)(240f - this.sanity * 1.8f);
			if (!this.Laughing)
			{
				this.Teeth.SetActive(this.SanityWarning);
			}
			if (this.MyRenderer.sharedMesh != this.NudeMesh)
			{
				if (!this.Slender)
				{
					this.MyRenderer.materials[2].SetFloat("_BlendAmount", 1f - this.sanity / 100f);
				}
				else
				{
					this.MyRenderer.materials[2].SetFloat("_BlendAmount", 0f);
				}
			}
			else
			{
				this.MyRenderer.materials[2].SetFloat("_BlendAmount", 0f);
			}
			this.PreviousSanity = this.sanity;
			this.Hairstyles[2].GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, this.Sanity);
		}
	}

	// Token: 0x1700048A RID: 1162
	// (get) Token: 0x06001D2F RID: 7471 RVA: 0x0015DE62 File Offset: 0x0015C062
	// (set) Token: 0x06001D30 RID: 7472 RVA: 0x0015DE6C File Offset: 0x0015C06C
	public float Bloodiness
	{
		get
		{
			return this.bloodiness;
		}
		set
		{
			this.bloodiness = Mathf.Clamp(value, 0f, 100f);
			if (this.Bloodiness > 0f)
			{
				this.StudentManager.TutorialWindow.ShowBloodMessage = true;
			}
			if (!this.BloodyWarning && this.Bloodiness > 0f)
			{
				this.NotificationManager.DisplayNotification(NotificationType.Bloody);
				this.BloodyWarning = true;
				if (this.Schoolwear > 0 && this.ClubAttire)
				{
					this.Police.BloodyClothing++;
					if (this.CurrentUniformOrigin == 1)
					{
						this.StudentManager.OriginalUniforms--;
						Debug.Log("One of the original uniforms has become bloody. There are now " + this.StudentManager.OriginalUniforms + " clean original uniforms in the school.");
					}
					else
					{
						this.StudentManager.NewUniforms--;
						Debug.Log("One of the new uniforms has become bloody. There are now " + this.StudentManager.NewUniforms + " clean original uniforms in the school.");
					}
				}
			}
			this.MyProjector.enabled = true;
			this.RedPaint = false;
			if (!GameGlobals.CensorBlood)
			{
				this.MyProjector.material.SetColor("_TintColor", new Color(0.25f, 0.25f, 0.25f, 0.5f));
				if (this.Bloodiness == 100f)
				{
					this.MyProjector.material.mainTexture = this.BloodTextures[5];
				}
				else if (this.Bloodiness >= 80f)
				{
					this.MyProjector.material.mainTexture = this.BloodTextures[4];
				}
				else if (this.Bloodiness >= 60f)
				{
					this.MyProjector.material.mainTexture = this.BloodTextures[3];
				}
				else if (this.Bloodiness >= 40f)
				{
					this.MyProjector.material.mainTexture = this.BloodTextures[2];
				}
				else if (this.Bloodiness >= 20f)
				{
					this.MyProjector.material.mainTexture = this.BloodTextures[1];
				}
				else
				{
					this.MyProjector.enabled = false;
					this.BloodyWarning = false;
				}
			}
			else
			{
				this.MyProjector.material.SetColor("_TintColor", new Color(0.5f, 0.5f, 0.5f, 0.5f));
				if (this.Bloodiness == 100f)
				{
					this.MyProjector.material.mainTexture = this.FlowerTextures[5];
				}
				else if (this.Bloodiness >= 80f)
				{
					this.MyProjector.material.mainTexture = this.FlowerTextures[4];
				}
				else if (this.Bloodiness >= 60f)
				{
					this.MyProjector.material.mainTexture = this.FlowerTextures[3];
				}
				else if (this.Bloodiness >= 40f)
				{
					this.MyProjector.material.mainTexture = this.FlowerTextures[2];
				}
				else if (this.Bloodiness >= 20f)
				{
					this.MyProjector.material.mainTexture = this.FlowerTextures[1];
				}
				else
				{
					this.MyProjector.enabled = false;
					this.BloodyWarning = false;
				}
			}
			this.StudentManager.UpdateBooths();
			this.MyLocker.UpdateButtons();
			this.Outline.h.ReinitMaterials();
		}
	}

	// Token: 0x1700048B RID: 1163
	// (get) Token: 0x06001D31 RID: 7473 RVA: 0x0015E1E3 File Offset: 0x0015C3E3
	// (set) Token: 0x06001D32 RID: 7474 RVA: 0x0015E1F2 File Offset: 0x0015C3F2
	public WeaponScript EquippedWeapon
	{
		get
		{
			return this.Weapon[this.Equipped];
		}
		set
		{
			this.Weapon[this.Equipped] = value;
		}
	}

	// Token: 0x1700048C RID: 1164
	// (get) Token: 0x06001D33 RID: 7475 RVA: 0x0015E202 File Offset: 0x0015C402
	public bool Armed
	{
		get
		{
			return this.EquippedWeapon != null;
		}
	}

	// Token: 0x1700048D RID: 1165
	// (get) Token: 0x06001D34 RID: 7476 RVA: 0x0015E210 File Offset: 0x0015C410
	public SanityType SanityType
	{
		get
		{
			if (this.Sanity / 100f > 0.6666667f)
			{
				return SanityType.High;
			}
			if (this.Sanity / 100f > 0.33333334f)
			{
				return SanityType.Medium;
			}
			return SanityType.Low;
		}
	}

	// Token: 0x06001D35 RID: 7477 RVA: 0x0015E23D File Offset: 0x0015C43D
	public string GetSanityString(SanityType sanityType)
	{
		if (sanityType == SanityType.High)
		{
			return "High";
		}
		if (sanityType == SanityType.Medium)
		{
			return "Med";
		}
		return "Low";
	}

	// Token: 0x1700048E RID: 1166
	// (get) Token: 0x06001D36 RID: 7478 RVA: 0x0015E257 File Offset: 0x0015C457
	public Vector3 HeadPosition
	{
		get
		{
			return new Vector3(base.transform.position.x, this.Hips.position.y + 0.2f, base.transform.position.z);
		}
	}

	// Token: 0x06001D37 RID: 7479 RVA: 0x0015E294 File Offset: 0x0015C494
	public void SetAnimationLayers()
	{
		this.CharacterAnimation["f02_yanderePose_00"].layer = 1;
		this.CharacterAnimation.Play("f02_yanderePose_00");
		this.CharacterAnimation["f02_yanderePose_00"].weight = 0f;
		this.CharacterAnimation["f02_shy_00"].layer = 2;
		this.CharacterAnimation.Play("f02_shy_00");
		this.CharacterAnimation["f02_shy_00"].weight = 0f;
		this.CharacterAnimation["f02_singleSaw_00"].layer = 3;
		this.CharacterAnimation.Play("f02_singleSaw_00");
		this.CharacterAnimation["f02_singleSaw_00"].weight = 0f;
		this.CharacterAnimation["f02_fist_00"].layer = 4;
		this.CharacterAnimation.Play("f02_fist_00");
		this.CharacterAnimation["f02_fist_00"].weight = 0f;
		this.CharacterAnimation["f02_mopping_00"].layer = 5;
		this.CharacterAnimation["f02_mopping_00"].speed = 2f;
		this.CharacterAnimation.Play("f02_mopping_00");
		this.CharacterAnimation["f02_mopping_00"].weight = 0f;
		this.CharacterAnimation["f02_carry_00"].layer = 6;
		this.CharacterAnimation.Play("f02_carry_00");
		this.CharacterAnimation["f02_carry_00"].weight = 0f;
		this.CharacterAnimation["f02_mopCarry_00"].layer = 7;
		this.CharacterAnimation.Play("f02_mopCarry_00");
		this.CharacterAnimation["f02_mopCarry_00"].weight = 0f;
		this.CharacterAnimation["f02_bucketCarry_00"].layer = 8;
		this.CharacterAnimation.Play("f02_bucketCarry_00");
		this.CharacterAnimation["f02_bucketCarry_00"].weight = 0f;
		this.CharacterAnimation["f02_cameraPose_00"].layer = 9;
		this.CharacterAnimation.Play("f02_cameraPose_00");
		this.CharacterAnimation["f02_cameraPose_00"].weight = 0f;
		this.CharacterAnimation["f02_grip_00"].layer = 10;
		this.CharacterAnimation.Play("f02_grip_00");
		this.CharacterAnimation["f02_grip_00"].weight = 0f;
		this.CharacterAnimation["f02_holdHead_00"].layer = 11;
		this.CharacterAnimation.Play("f02_holdHead_00");
		this.CharacterAnimation["f02_holdHead_00"].weight = 0f;
		this.CharacterAnimation["f02_holdTorso_00"].layer = 12;
		this.CharacterAnimation.Play("f02_holdTorso_00");
		this.CharacterAnimation["f02_holdTorso_00"].weight = 0f;
		this.CharacterAnimation["f02_carryCan_00"].layer = 13;
		this.CharacterAnimation.Play("f02_carryCan_00");
		this.CharacterAnimation["f02_carryCan_00"].weight = 0f;
		this.CharacterAnimation["f02_leftGrip_00"].layer = 14;
		this.CharacterAnimation.Play("f02_leftGrip_00");
		this.CharacterAnimation["f02_leftGrip_00"].weight = 0f;
		this.CharacterAnimation["f02_carryShoulder_00"].layer = 15;
		this.CharacterAnimation.Play("f02_carryShoulder_00");
		this.CharacterAnimation["f02_carryShoulder_00"].weight = 0f;
		this.CharacterAnimation["f02_carryFlashlight_00"].layer = 16;
		this.CharacterAnimation.Play("f02_carryFlashlight_00");
		this.CharacterAnimation["f02_carryFlashlight_00"].weight = 0f;
		this.CharacterAnimation["f02_carryBox_00"].layer = 17;
		this.CharacterAnimation.Play("f02_carryBox_00");
		this.CharacterAnimation["f02_carryBox_00"].weight = 0f;
		this.CharacterAnimation["f02_holdBook_00"].layer = 18;
		this.CharacterAnimation.Play("f02_holdBook_00");
		this.CharacterAnimation["f02_holdBook_00"].weight = 0f;
		this.CharacterAnimation["f02_holdBook_00"].speed = 0.5f;
		this.CharacterAnimation[this.CreepyIdles[1]].layer = 19;
		this.CharacterAnimation.Play(this.CreepyIdles[1]);
		this.CharacterAnimation[this.CreepyIdles[1]].weight = 0f;
		this.CharacterAnimation[this.CreepyIdles[2]].layer = 20;
		this.CharacterAnimation.Play(this.CreepyIdles[2]);
		this.CharacterAnimation[this.CreepyIdles[2]].weight = 0f;
		this.CharacterAnimation[this.CreepyIdles[3]].layer = 21;
		this.CharacterAnimation.Play(this.CreepyIdles[3]);
		this.CharacterAnimation[this.CreepyIdles[3]].weight = 0f;
		this.CharacterAnimation[this.CreepyIdles[4]].layer = 22;
		this.CharacterAnimation.Play(this.CreepyIdles[4]);
		this.CharacterAnimation[this.CreepyIdles[4]].weight = 0f;
		this.CharacterAnimation[this.CreepyIdles[5]].layer = 23;
		this.CharacterAnimation.Play(this.CreepyIdles[5]);
		this.CharacterAnimation[this.CreepyIdles[5]].weight = 0f;
		this.CharacterAnimation[this.CreepyWalks[1]].layer = 24;
		this.CharacterAnimation.Play(this.CreepyWalks[1]);
		this.CharacterAnimation[this.CreepyWalks[1]].weight = 0f;
		this.CharacterAnimation[this.CreepyWalks[2]].layer = 25;
		this.CharacterAnimation.Play(this.CreepyWalks[2]);
		this.CharacterAnimation[this.CreepyWalks[2]].weight = 0f;
		this.CharacterAnimation[this.CreepyWalks[3]].layer = 26;
		this.CharacterAnimation.Play(this.CreepyWalks[3]);
		this.CharacterAnimation[this.CreepyWalks[3]].weight = 0f;
		this.CharacterAnimation[this.CreepyWalks[4]].layer = 27;
		this.CharacterAnimation.Play(this.CreepyWalks[4]);
		this.CharacterAnimation[this.CreepyWalks[4]].weight = 0f;
		this.CharacterAnimation[this.CreepyWalks[5]].layer = 28;
		this.CharacterAnimation.Play(this.CreepyWalks[5]);
		this.CharacterAnimation[this.CreepyWalks[5]].weight = 0f;
		this.CharacterAnimation["f02_carryDramatic_00"].layer = 29;
		this.CharacterAnimation.Play("f02_carryDramatic_00");
		this.CharacterAnimation["f02_carryDramatic_00"].weight = 0f;
		this.CharacterAnimation["f02_selfie_00"].layer = 30;
		this.CharacterAnimation.Play("f02_selfie_00");
		this.CharacterAnimation["f02_selfie_00"].weight = 0f;
		this.CharacterAnimation["f02_dramaticWriting_00"].layer = 31;
		this.CharacterAnimation.Play("f02_dramaticWriting_00");
		this.CharacterAnimation["f02_dramaticWriting_00"].weight = 0f;
		this.CharacterAnimation["f02_reachForWeapon_00"].layer = 32;
		this.CharacterAnimation.Play("f02_reachForWeapon_00");
		this.CharacterAnimation["f02_reachForWeapon_00"].weight = 0f;
		this.CharacterAnimation["f02_reachForWeapon_00"].speed = 2f;
		this.CharacterAnimation["f02_gutsEye_00"].layer = 33;
		this.CharacterAnimation.Play("f02_gutsEye_00");
		this.CharacterAnimation["f02_gutsEye_00"].weight = 0f;
		this.CharacterAnimation["f02_fingerSnap_00"].layer = 34;
		this.CharacterAnimation.Play("f02_fingerSnap_00");
		this.CharacterAnimation["f02_fingerSnap_00"].weight = 0f;
		this.CharacterAnimation["f02_sadEyebrows_00"].layer = 35;
		this.CharacterAnimation.Play("f02_sadEyebrows_00");
		this.CharacterAnimation["f02_sadEyebrows_00"].weight = 0f;
		this.CharacterAnimation["f02_dipping_00"].speed = 2f;
		this.CharacterAnimation["f02_stripping_00"].speed = 1.5f;
		this.CharacterAnimation["f02_falconIdle_00"].speed = 2f;
		this.CharacterAnimation["f02_carryIdleA_00"].speed = 1.75f;
		this.CharacterAnimation["CyborgNinja_Run_Armed"].speed = 2f;
		this.CharacterAnimation["CyborgNinja_Run_Unarmed"].speed = 2f;
	}

	// Token: 0x06001D38 RID: 7480 RVA: 0x0015ECE4 File Offset: 0x0015CEE4
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.LeftAlt))
		{
			this.CinematicCamera.SetActive(false);
		}
		if (!this.PauseScreen.Show)
		{
			this.UpdateMovement();
			this.UpdatePoisoning();
			if (!this.Laughing)
			{
				this.MyAudio.volume -= Time.deltaTime * 2f;
			}
			else if (this.PickUp != null && !this.PickUp.Clothing)
			{
				this.CharacterAnimation[this.CarryAnims[1]].weight = Mathf.Lerp(this.CharacterAnimation[this.CarryAnims[1]].weight, 1f, Time.deltaTime * 10f);
			}
			if (!this.Mopping)
			{
				this.CharacterAnimation["f02_mopping_00"].weight = Mathf.Lerp(this.CharacterAnimation["f02_mopping_00"].weight, 0f, Time.deltaTime * 10f);
			}
			else
			{
				this.CharacterAnimation["f02_mopping_00"].weight = Mathf.Lerp(this.CharacterAnimation["f02_mopping_00"].weight, 1f, Time.deltaTime * 10f);
				if (Input.GetButtonUp("A") || Input.GetKeyDown(KeyCode.Escape))
				{
					this.Mopping = false;
				}
			}
			if (this.LaughIntensity == 0f)
			{
				this.ID = 0;
				while (this.ID < this.CarryAnims.Length)
				{
					string name = this.CarryAnims[this.ID];
					if (this.PickUp != null && this.CarryAnimID == this.ID && !this.Mopping && !this.Dipping && !this.Pouring && !this.BucketDropping && !this.Digging && !this.Burying && !this.WritingName)
					{
						this.CharacterAnimation[name].weight = Mathf.Lerp(this.CharacterAnimation[name].weight, 1f, Time.deltaTime * 10f);
					}
					else
					{
						this.CharacterAnimation[name].weight = Mathf.Lerp(this.CharacterAnimation[name].weight, 0f, Time.deltaTime * 10f);
					}
					this.ID++;
				}
			}
			else if (this.Armed)
			{
				this.CharacterAnimation["f02_mopCarry_00"].weight = Mathf.Lerp(this.CharacterAnimation["f02_mopCarry_00"].weight, 1f, Time.deltaTime * 10f);
			}
			if (this.Noticed && !this.Attacking)
			{
				if (!this.Collapse)
				{
					if (this.ShoulderCamera.NoticedTimer < 1f)
					{
						this.CharacterAnimation.CrossFade("f02_scaredIdle_00");
					}
					this.targetRotation = Quaternion.LookRotation(this.Senpai.position - base.transform.position);
					base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.targetRotation, Time.deltaTime * 10f);
					base.transform.localEulerAngles = new Vector3(0f, base.transform.localEulerAngles.y, base.transform.localEulerAngles.z);
					if (Vector3.Distance(base.transform.position, this.Senpai.position) < 1.25f)
					{
						this.MyController.Move(base.transform.forward * (Time.deltaTime * -1f));
					}
				}
				else if (this.CharacterAnimation["f02_down_22"].time >= this.CharacterAnimation["f02_down_22"].length)
				{
					this.CharacterAnimation.CrossFade("f02_down_23");
				}
			}
			this.UpdateEffects();
			this.UpdateTalking();
			this.UpdateAttacking();
			this.UpdateSlouch();
			if (!this.Noticed)
			{
				this.RightYandereEye.material.color = new Color(this.RightYandereEye.material.color.r, this.RightYandereEye.material.color.g, this.RightYandereEye.material.color.b, 1f - this.Sanity / 100f);
				this.LeftYandereEye.material.color = new Color(this.LeftYandereEye.material.color.r, this.LeftYandereEye.material.color.g, this.LeftYandereEye.material.color.b, 1f - this.Sanity / 100f);
				this.EyeShrink = Mathf.Lerp(this.EyeShrink, 0.5f * (1f - this.Sanity / 100f), Time.deltaTime * 10f);
			}
			this.UpdateTwitch();
			this.UpdateWarnings();
			this.UpdateDebugFunctionality();
			if (base.transform.position.y < 0f)
			{
				base.transform.position = new Vector3(base.transform.position.x, 0f, base.transform.position.z);
			}
			if (base.transform.position.z < -99.5f)
			{
				base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y, -99.5f);
			}
			base.transform.eulerAngles = new Vector3(0f, base.transform.eulerAngles.y, 0f);
			return;
		}
		this.MyAudio.volume -= 0.33333334f;
	}

	// Token: 0x06001D39 RID: 7481 RVA: 0x0015F308 File Offset: 0x0015D508
	private void GoToPKDir(PKDirType pkDir, string sansAnim, Vector3 ragdollLocalPos)
	{
		this.CharacterAnimation.CrossFade(sansAnim);
		this.RagdollPK.transform.localPosition = ragdollLocalPos;
		if (this.PKDir != pkDir)
		{
			AudioSource.PlayClipAtPoint(this.Slam, base.transform.position + Vector3.up);
		}
		this.PKDir = pkDir;
	}

	// Token: 0x06001D3A RID: 7482 RVA: 0x0015F364 File Offset: 0x0015D564
	private void UpdateMovement()
	{
		if (this.CanMove)
		{
			if (!this.ToggleRun)
			{
				this.Running = false;
				if (Input.GetButton("LB"))
				{
					this.Running = true;
				}
			}
			else if (Input.GetButtonDown("LB"))
			{
				this.Running = !this.Running;
			}
			this.MyController.Move(Physics.gravity * Time.deltaTime);
			this.v = Input.GetAxis("Vertical");
			this.h = Input.GetAxis("Horizontal");
			this.FlapSpeed = Mathf.Abs(this.v) + Mathf.Abs(this.h);
			if (this.Selfie)
			{
				this.v = -1f * this.v;
				this.h = -1f * this.h;
			}
			if (!this.Aiming)
			{
				Vector3 vector = this.MainCamera.transform.TransformDirection(Vector3.forward);
				vector.y = 0f;
				vector = vector.normalized;
				Vector3 a = new Vector3(vector.z, 0f, -vector.x);
				this.targetDirection = this.h * a + this.v * vector;
				if (this.targetDirection != Vector3.zero)
				{
					this.targetRotation = Quaternion.LookRotation(this.targetDirection);
					base.transform.rotation = Quaternion.Lerp(base.transform.rotation, this.targetRotation, Time.deltaTime * 10f);
				}
				else
				{
					this.targetRotation = new Quaternion(0f, 0f, 0f, 0f);
				}
				if (this.v != 0f || this.h != 0f)
				{
					if (this.Running && Vector3.Distance(base.transform.position, this.Senpai.position) > 1f)
					{
						if (this.Stance.Current == StanceType.Crouching)
						{
							this.CharacterAnimation.CrossFade(this.CrouchRunAnim);
							this.MyController.Move(base.transform.forward * (this.CrouchRunSpeed + (float)(this.PhysicalGrade + this.SpeedBonus) * 0.25f) * Time.deltaTime);
						}
						else if (!this.Dragging && !this.Mopping)
						{
							this.CharacterAnimation.CrossFade(this.RunAnim);
							this.MyController.Move(base.transform.forward * (this.RunSpeed + (float)(this.PhysicalGrade + this.SpeedBonus) * 0.25f) * Time.deltaTime);
						}
						else if (this.Mopping)
						{
							this.CharacterAnimation.CrossFade(this.WalkAnim);
							this.MyController.Move(base.transform.forward * (this.WalkSpeed * Time.deltaTime));
						}
						StanceType stanceType = this.Stance.Current;
						if (this.Stance.Current == StanceType.Crawling)
						{
							this.Stance.Current = StanceType.Crouching;
							this.Crouch();
						}
					}
					else if (!this.Dragging)
					{
						if (this.Stance.Current == StanceType.Crawling)
						{
							this.CharacterAnimation.CrossFade(this.CrawlWalkAnim);
							this.MyController.Move(base.transform.forward * (this.CrawlSpeed * Time.deltaTime));
						}
						else if (this.Stance.Current == StanceType.Crouching)
						{
							this.CharacterAnimation[this.CrouchWalkAnim].speed = 1f;
							this.CharacterAnimation.CrossFade(this.CrouchWalkAnim);
							this.MyController.Move(base.transform.forward * (this.CrouchWalkSpeed * Time.deltaTime));
						}
						else
						{
							this.CharacterAnimation.CrossFade(this.WalkAnim);
							if (this.NearSenpai)
							{
								for (int i = 1; i < 6; i++)
								{
									if (i != this.Creepiness)
									{
										this.CharacterAnimation[this.CreepyIdles[i]].weight = Mathf.MoveTowards(this.CharacterAnimation[this.CreepyIdles[i]].weight, 0f, Time.deltaTime);
										this.CharacterAnimation[this.CreepyWalks[i]].weight = Mathf.MoveTowards(this.CharacterAnimation[this.CreepyWalks[i]].weight, 0f, Time.deltaTime);
									}
								}
								this.CharacterAnimation[this.CreepyIdles[this.Creepiness]].weight = Mathf.MoveTowards(this.CharacterAnimation[this.CreepyIdles[this.Creepiness]].weight, 0f, Time.deltaTime);
								this.CharacterAnimation[this.CreepyWalks[this.Creepiness]].weight = Mathf.MoveTowards(this.CharacterAnimation[this.CreepyWalks[this.Creepiness]].weight, 1f, Time.deltaTime);
							}
							this.MyController.Move(base.transform.forward * (this.WalkSpeed * Time.deltaTime));
						}
					}
					else
					{
						this.CharacterAnimation.CrossFade("f02_dragWalk_01");
						this.MyController.Move(base.transform.forward * (this.WalkSpeed * Time.deltaTime));
					}
				}
				else if (!this.Dragging)
				{
					if (this.Stance.Current == StanceType.Crawling)
					{
						this.CharacterAnimation.CrossFade(this.CrawlIdleAnim);
					}
					else if (this.Stance.Current == StanceType.Crouching)
					{
						this.CharacterAnimation.CrossFade(this.CrouchIdleAnim);
					}
					else
					{
						this.CharacterAnimation.CrossFade(this.IdleAnim);
						if (this.NearSenpai)
						{
							for (int j = 1; j < 6; j++)
							{
								if (j != this.Creepiness)
								{
									this.CharacterAnimation[this.CreepyIdles[j]].weight = Mathf.MoveTowards(this.CharacterAnimation[this.CreepyIdles[j]].weight, 0f, Time.deltaTime);
									this.CharacterAnimation[this.CreepyWalks[j]].weight = Mathf.MoveTowards(this.CharacterAnimation[this.CreepyWalks[j]].weight, 0f, Time.deltaTime);
								}
							}
							this.CharacterAnimation[this.CreepyIdles[this.Creepiness]].weight = Mathf.MoveTowards(this.CharacterAnimation[this.CreepyIdles[this.Creepiness]].weight, 1f, Time.deltaTime);
							this.CharacterAnimation[this.CreepyWalks[this.Creepiness]].weight = Mathf.MoveTowards(this.CharacterAnimation[this.CreepyWalks[this.Creepiness]].weight, 0f, Time.deltaTime);
						}
					}
				}
				else
				{
					this.CharacterAnimation.CrossFade("f02_dragIdle_02");
				}
			}
			else
			{
				if (this.v != 0f || this.h != 0f)
				{
					if (this.Stance.Current == StanceType.Crawling)
					{
						this.CharacterAnimation.CrossFade(this.CrawlWalkAnim);
						this.MyController.Move(base.transform.forward * (this.CrawlSpeed * Time.deltaTime * this.v));
						this.MyController.Move(base.transform.right * (this.CrawlSpeed * Time.deltaTime * this.h));
					}
					else if (this.Stance.Current == StanceType.Crouching)
					{
						this.CharacterAnimation.CrossFade(this.CrouchWalkAnim);
						this.MyController.Move(base.transform.forward * (this.CrouchWalkSpeed * Time.deltaTime * this.v));
						this.MyController.Move(base.transform.right * (this.CrouchWalkSpeed * Time.deltaTime * this.h));
					}
					else
					{
						this.CharacterAnimation.CrossFade(this.WalkAnim);
						this.MyController.Move(base.transform.forward * (this.WalkSpeed * Time.deltaTime * this.v));
						this.MyController.Move(base.transform.right * (this.WalkSpeed * Time.deltaTime * this.h));
					}
				}
				else if (this.Stance.Current == StanceType.Crawling)
				{
					this.CharacterAnimation.CrossFade(this.CrawlIdleAnim);
				}
				else if (this.Stance.Current == StanceType.Crouching)
				{
					this.CharacterAnimation.CrossFade(this.CrouchIdleAnim);
				}
				else
				{
					this.CharacterAnimation.CrossFade(this.IdleAnim);
				}
				if (!this.RPGCamera.invertAxis)
				{
					this.Bend += Input.GetAxis("Mouse Y") * 8f;
				}
				else
				{
					this.Bend -= Input.GetAxis("Mouse Y") * 8f;
				}
				if (this.Stance.Current == StanceType.Crawling)
				{
					if (this.Bend < 0f)
					{
						this.Bend = 0f;
					}
				}
				else if (this.Stance.Current == StanceType.Crouching)
				{
					if (this.Bend < -45f)
					{
						this.Bend = -45f;
					}
				}
				else if (this.Bend < -85f)
				{
					this.Bend = -85f;
				}
				if (this.Bend > 85f)
				{
					this.Bend = 85f;
				}
				base.transform.localEulerAngles = new Vector3(base.transform.localEulerAngles.x, base.transform.localEulerAngles.y + Input.GetAxis("Mouse X") * 8f, base.transform.localEulerAngles.z);
			}
			if (!this.NearSenpai)
			{
				if (!Input.GetButton("A") && !Input.GetButton("B") && !Input.GetButton("X") && !Input.GetButton("Y") && !this.StudentManager.Clock.UpdateBloom && (Input.GetAxis("LT") > 0.5f || Input.GetMouseButton(1)))
				{
					if (this.Inventory.RivalPhone)
					{
						if (Input.GetButtonDown("LB"))
						{
							this.CharacterAnimation["f02_cameraPose_00"].weight = 0f;
							this.CharacterAnimation["f02_selfie_00"].weight = 0f;
							if (!this.RivalPhone)
							{
								this.SmartphoneRenderer.material.mainTexture = this.RivalPhoneTexture;
								this.PhonePromptBar.Label.text = "SWITCH TO YOUR PHONE";
								this.RivalPhone = true;
							}
							else
							{
								this.SmartphoneRenderer.material.mainTexture = this.YanderePhoneTexture;
								this.PhonePromptBar.Label.text = "SWITCH TO STOLEN PHONE";
								this.RivalPhone = false;
							}
						}
					}
					else if (!this.Selfie && Input.GetButtonDown("LB"))
					{
						if (!this.AR)
						{
							this.Smartphone.cullingMask |= 1 << LayerMask.NameToLayer("Miyuki");
							this.AR = true;
						}
						else
						{
							this.Smartphone.cullingMask &= ~(1 << LayerMask.NameToLayer("Miyuki"));
							this.AR = false;
						}
					}
					if (Input.GetAxis("LT") > 0.5f)
					{
						this.UsingController = true;
					}
					if (!this.Aiming)
					{
						if (this.CameraEffects.OneCamera)
						{
							this.MainCamera.clearFlags = CameraClearFlags.Color;
							this.MainCamera.farClipPlane = 0.02f;
							this.HandCamera.clearFlags = CameraClearFlags.Color;
						}
						else
						{
							this.MainCamera.clearFlags = CameraClearFlags.Skybox;
							this.MainCamera.farClipPlane = (float)OptionGlobals.DrawDistance;
							this.HandCamera.clearFlags = CameraClearFlags.Depth;
						}
						base.transform.eulerAngles = new Vector3(base.transform.eulerAngles.x, this.MainCamera.transform.eulerAngles.y, base.transform.eulerAngles.z);
						this.CharacterAnimation.Play(this.IdleAnim);
						this.Smartphone.transform.parent.gameObject.SetActive(true);
						if (!this.CinematicCamera.activeInHierarchy)
						{
							this.DisableHairAndAccessories();
						}
						this.HandCamera.gameObject.SetActive(true);
						this.ShoulderCamera.AimingCamera = true;
						this.Obscurance.enabled = false;
						this.YandereVision = false;
						this.Blur.enabled = true;
						this.Mopping = false;
						this.Selfie = false;
						this.Aiming = true;
						this.EmptyHands();
						this.PhonePromptBar.Panel.enabled = true;
						this.PhonePromptBar.Show = true;
						if (this.Inventory.RivalPhone)
						{
							if (!this.RivalPhone)
							{
								this.PhonePromptBar.Label.text = "SWITCH TO STOLEN PHONE";
							}
							else
							{
								this.PhonePromptBar.Label.text = "SWITCH TO YOUR PHONE";
							}
						}
						else
						{
							this.PhonePromptBar.Label.text = "AR GAME ON/OFF";
						}
						Time.timeScale = 1f;
						this.UpdateSelfieStatus();
					}
				}
				if (!this.Aiming && !this.Accessories[9].activeInHierarchy && !this.Accessories[16].activeInHierarchy && !this.Pod.activeInHierarchy)
				{
					if (Input.GetButton("RB"))
					{
						if (this.MagicalGirl)
						{
							if (this.Armed && this.EquippedWeapon.WeaponID == 14 && Input.GetButtonDown("RB") && !this.ShootingBeam)
							{
								AudioSource.PlayClipAtPoint(this.LoveLoveBeamVoice, base.transform.position);
								this.CharacterAnimation["f02_LoveLoveBeam_00"].time = 0f;
								this.CharacterAnimation.CrossFade("f02_LoveLoveBeam_00");
								this.ShootingBeam = true;
								this.CanMove = false;
							}
						}
						else if (this.BlackRobe.activeInHierarchy)
						{
							if (Input.GetButtonDown("RB"))
							{
								AudioSource.PlayClipAtPoint(this.SithOn, base.transform.position);
							}
							this.SithTrailEnd1.localPosition = new Vector3(-1f, 0f, 0f);
							this.SithTrailEnd2.localPosition = new Vector3(1f, 0f, 0f);
							this.Beam[0].Play();
							this.Beam[1].Play();
							this.Beam[2].Play();
							this.Beam[3].Play();
							if (Input.GetButtonDown("X"))
							{
								this.CharacterAnimation["f02_sithAttack_00"].time = 0f;
								this.CharacterAnimation.Play("f02_sithAttack_00");
								this.SithBeam[1].Damage = 10f;
								this.SithBeam[2].Damage = 10f;
								this.SithAttacking = true;
								this.CanMove = false;
								this.SithPrefix = "";
								this.AttackPrefix = "sith";
							}
							if (Input.GetButtonDown("Y"))
							{
								this.CharacterAnimation["f02_sithAttackHard_00"].time = 0f;
								this.CharacterAnimation.Play("f02_sithAttackHard_00");
								this.SithBeam[1].Damage = 20f;
								this.SithBeam[2].Damage = 20f;
								this.SithAttacking = true;
								this.CanMove = false;
								this.SithPrefix = "Hard";
								this.AttackPrefix = "sith";
							}
						}
						else if (Input.GetButtonDown("RB") && this.SpiderLegs.activeInHierarchy)
						{
							this.SpiderGrow = !this.SpiderGrow;
							if (this.SpiderGrow)
							{
								AudioSource.PlayClipAtPoint(this.EmptyDemon.MouthOpen, base.transform.position);
							}
							else
							{
								AudioSource.PlayClipAtPoint(this.EmptyDemon.MouthClose, base.transform.position);
							}
							this.StudentManager.UpdateStudents(0);
						}
						this.YandereTimer += Time.deltaTime;
						if (this.YandereTimer > 0.5f)
						{
							if (!this.Sans && !this.BlackRobe.activeInHierarchy)
							{
								this.YandereVision = true;
							}
							else if (this.Sans)
							{
								this.SansEyes[0].SetActive(true);
								this.SansEyes[1].SetActive(true);
								this.GlowEffect.Play();
								this.SummonBones = true;
								this.YandereTimer = 0f;
								this.CanMove = false;
							}
						}
					}
					else
					{
						if (this.BlackRobe.activeInHierarchy)
						{
							this.SithTrailEnd1.localPosition = new Vector3(0f, 0f, 0f);
							this.SithTrailEnd2.localPosition = new Vector3(0f, 0f, 0f);
							if (Input.GetButtonUp("RB"))
							{
								AudioSource.PlayClipAtPoint(this.SithOff, base.transform.position);
							}
							this.Beam[0].Stop();
							this.Beam[1].Stop();
							this.Beam[2].Stop();
							this.Beam[3].Stop();
						}
						if (this.YandereVision)
						{
							this.Obscurance.enabled = false;
							this.YandereVision = false;
						}
					}
					if (Input.GetButtonUp("RB"))
					{
						if (this.Stance.Current != StanceType.Crouching && this.Stance.Current != StanceType.Crawling && this.YandereTimer < 0.5f && !this.Dragging && !this.Carrying && !this.Pod.activeInHierarchy && !this.Laughing)
						{
							if (this.Sans)
							{
								this.BlasterStage++;
								if (this.BlasterStage > 5)
								{
									this.BlasterStage = 1;
								}
								GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.BlasterSet[this.BlasterStage], base.transform.position, Quaternion.identity);
								gameObject.transform.position = base.transform.position;
								gameObject.transform.rotation = base.transform.rotation;
								AudioSource.PlayClipAtPoint(this.BlasterClip, base.transform.position + Vector3.up);
								this.CharacterAnimation["f02_sansBlaster_00"].time = 0f;
								this.CharacterAnimation.Play("f02_sansBlaster_00");
								this.SansEyes[0].SetActive(true);
								this.SansEyes[1].SetActive(true);
								this.GlowEffect.Play();
								this.Blasting = true;
								this.CanMove = false;
							}
							else if (!this.BlackRobe.activeInHierarchy)
							{
								if (this.Kagune[0].activeInHierarchy)
								{
									if (!this.SwingKagune)
									{
										AudioSource.PlayClipAtPoint(this.KaguneSwoosh, base.transform.position + Vector3.up);
										this.SwingKagune = true;
									}
								}
								else if (this.Gazing || this.Shipgirl)
								{
									if (this.Gazing)
									{
										this.CharacterAnimation["f02_gazerSnap_00"].time = 0f;
										this.CharacterAnimation.CrossFade("f02_gazerSnap_00");
									}
									else
									{
										this.CharacterAnimation["f02_shipGirlSnap_00"].time = 0f;
										this.CharacterAnimation.CrossFade("f02_shipGirlSnap_00");
									}
									this.Snapping = true;
									this.CanMove = false;
								}
								else if (this.WitchMode)
								{
									if (!this.StoppingTime)
									{
										this.CharacterAnimation["f02_summonStand_00"].time = 0f;
										if (this.Freezing)
										{
											AudioSource.PlayClipAtPoint(this.StartShout, base.transform.position);
										}
										else
										{
											AudioSource.PlayClipAtPoint(this.StopShout, base.transform.position);
										}
										this.Freezing = !this.InvertSphere.gameObject.activeInHierarchy;
										this.StoppingTime = true;
										this.CanMove = false;
										this.MyAudio.Stop();
										this.Egg = true;
									}
								}
								else if (this.PickUp != null && this.PickUp.CarryAnimID == 10)
								{
									this.StudentManager.NoteWindow.gameObject.SetActive(true);
									this.StudentManager.NoteWindow.Show = true;
									this.PromptBar.Show = true;
									this.Blur.enabled = true;
									this.CanMove = false;
									Time.timeScale = 0.0001f;
									this.HUD.alpha = 0f;
									this.PromptBar.Label[0].text = "Confirm";
									this.PromptBar.Label[1].text = "Cancel";
									this.PromptBar.Label[4].text = "Select";
									this.PromptBar.UpdateButtons();
								}
								else if (!this.FalconHelmet.activeInHierarchy && !this.Cape.activeInHierarchy && !this.MagicalGirl)
								{
									if (!this.Xtan)
									{
										if (!this.CirnoHair.activeInHierarchy && !this.TornadoHair.activeInHierarchy && !this.BladeHair.activeInHierarchy)
										{
											this.LaughAnim = "f02_laugh_01";
											this.LaughClip = this.Laugh1;
											this.LaughIntensity += 1f;
											this.MyAudio.clip = this.LaughClip;
											this.MyAudio.time = 0f;
											this.MyAudio.Play();
										}
										this.GiggleLines.Play();
										UnityEngine.Object.Instantiate<GameObject>(this.GiggleDisc, base.transform.position + Vector3.up, Quaternion.identity);
										this.MyAudio.volume = 1f;
										this.LaughTimer = 0.5f;
										this.Laughing = true;
										this.CanMove = false;
										this.Teeth.SetActive(false);
									}
									else if (this.LongHair[0].gameObject.activeInHierarchy)
									{
										this.LongHair[0].gameObject.SetActive(false);
										this.BlackEyePatch.SetActive(false);
										this.SlenderHair[0].transform.parent.gameObject.SetActive(true);
										this.SlenderHair[0].SetActive(true);
										this.SlenderHair[1].SetActive(true);
									}
									else
									{
										this.LongHair[0].gameObject.SetActive(true);
										this.BlackEyePatch.SetActive(true);
										this.SlenderHair[0].transform.parent.gameObject.SetActive(true);
										this.SlenderHair[0].SetActive(false);
										this.SlenderHair[1].SetActive(false);
									}
								}
								else if (!this.Punching)
								{
									if (this.FalconHelmet.activeInHierarchy)
									{
										GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.FalconWindUp);
										gameObject2.transform.parent = this.ItemParent;
										gameObject2.transform.localPosition = Vector3.zero;
										AudioClipPlayer.PlayAttached(this.FalconPunchVoice, this.MainCamera.transform, 5f, 10f);
										this.CharacterAnimation["f02_falconPunch_00"].time = 0f;
										this.CharacterAnimation.Play("f02_falconPunch_00");
										this.FalconSpeed = 0f;
									}
									else
									{
										GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(this.FalconWindUp);
										gameObject3.transform.parent = this.ItemParent;
										gameObject3.transform.localPosition = Vector3.zero;
										AudioSource.PlayClipAtPoint(this.OnePunchVoices[UnityEngine.Random.Range(0, this.OnePunchVoices.Length)], base.transform.position + Vector3.up);
										this.CharacterAnimation["f02_onePunch_00"].time = 0f;
										this.CharacterAnimation.CrossFade("f02_onePunch_00", 0.15f);
									}
									this.Punching = true;
									this.CanMove = false;
								}
							}
						}
						this.YandereTimer = 0f;
					}
				}
				if (this.Stance.Current != StanceType.Crouching && this.Stance.Current != StanceType.Crawling)
				{
					if (Input.GetButtonDown("RS"))
					{
						this.Obscurance.enabled = false;
						this.CrouchButtonDown = true;
						this.YandereVision = false;
						this.Stance.Current = StanceType.Crouching;
						this.Crouch();
						this.EmptyHands();
					}
				}
				else
				{
					if (this.Stance.Current == StanceType.Crouching)
					{
						if (Input.GetButton("RS") && !this.CameFromCrouch)
						{
							this.CrawlTimer += Time.deltaTime;
						}
						if (this.CrawlTimer > 0.5f)
						{
							if (!this.Selfie)
							{
								this.EmptyHands();
								this.Obscurance.enabled = false;
								this.YandereVision = false;
								this.Stance.Current = StanceType.Crawling;
								this.CrawlTimer = 0f;
								this.Crawl();
							}
						}
						else if (Input.GetButtonUp("RS") && !this.CrouchButtonDown && !this.CameFromCrouch)
						{
							this.Stance.Current = StanceType.Standing;
							this.CrawlTimer = 0f;
							this.Uncrouch();
						}
					}
					else if (Input.GetButtonDown("RS"))
					{
						this.CameFromCrouch = true;
						this.Stance.Current = StanceType.Crouching;
						this.Crouch();
					}
					if (Input.GetButtonUp("RS"))
					{
						this.CrouchButtonDown = false;
						this.CameFromCrouch = false;
						this.CrawlTimer = 0f;
					}
				}
			}
			if (this.Aiming)
			{
				if (!this.RivalPhone && Input.GetButtonDown("A"))
				{
					this.Selfie = !this.Selfie;
					this.UpdateSelfieStatus();
				}
				if (!this.Selfie)
				{
					this.CharacterAnimation["f02_cameraPose_00"].weight = Mathf.Lerp(this.CharacterAnimation["f02_cameraPose_00"].weight, 1f, Time.deltaTime * 10f);
					this.CharacterAnimation["f02_selfie_00"].weight = Mathf.Lerp(this.CharacterAnimation["f02_selfie_00"].weight, 0f, Time.deltaTime * 10f);
				}
				else
				{
					this.CharacterAnimation["f02_cameraPose_00"].weight = Mathf.Lerp(this.CharacterAnimation["f02_cameraPose_00"].weight, 0f, Time.deltaTime * 10f);
					this.CharacterAnimation["f02_selfie_00"].weight = Mathf.Lerp(this.CharacterAnimation["f02_selfie_00"].weight, 1f, Time.deltaTime * 10f);
					if (Input.GetButtonDown("B"))
					{
						if (!this.SelfieGuide.activeInHierarchy)
						{
							this.SelfieGuide.SetActive(true);
						}
						else
						{
							this.SelfieGuide.SetActive(false);
						}
					}
				}
				if (this.ClubAccessories[7].activeInHierarchy && (Input.GetAxis("DpadY") != 0f || Input.GetAxis("Mouse ScrollWheel") != 0f || Input.GetKey(KeyCode.Tab) || Input.GetKey(KeyCode.LeftShift)))
				{
					if (Input.GetKey(KeyCode.Tab))
					{
						this.Smartphone.fieldOfView -= Time.deltaTime * 100f;
					}
					if (Input.GetKey(KeyCode.LeftShift))
					{
						this.Smartphone.fieldOfView += Time.deltaTime * 100f;
					}
					this.Smartphone.fieldOfView -= Input.GetAxis("DpadY");
					this.Smartphone.fieldOfView -= Input.GetAxis("Mouse ScrollWheel") * 10f;
					if (this.Smartphone.fieldOfView > 60f)
					{
						this.Smartphone.fieldOfView = 60f;
					}
					if (this.Smartphone.fieldOfView < 30f)
					{
						this.Smartphone.fieldOfView = 30f;
					}
				}
				if (Input.GetAxis("RT") == 1f || Input.GetMouseButtonDown(0) || Input.GetButtonDown("RB"))
				{
					this.FixCamera();
					this.PauseScreen.CorrectingTime = false;
					Time.timeScale = 0.0001f;
					this.CanMove = false;
					this.Shutter.Snap();
				}
				if (Time.timeScale > 0.0001f && ((this.UsingController && Input.GetAxis("LT") < 0.5f) || (!this.UsingController && !Input.GetMouseButton(1))))
				{
					this.StopAiming();
				}
				if (Input.GetKey(KeyCode.LeftAlt))
				{
					if (!this.CinematicCamera.activeInHierarchy)
					{
						if (this.CinematicTimer > 0f)
						{
							this.CinematicCamera.transform.eulerAngles = this.Smartphone.transform.eulerAngles;
							this.CinematicCamera.transform.position = this.Smartphone.transform.position;
							this.CinematicCamera.SetActive(true);
							this.CinematicTimer = 0f;
							this.UpdateHair();
							this.StopAiming();
						}
						this.CinematicTimer += 1f;
					}
				}
				else
				{
					this.CinematicTimer = 0f;
				}
			}
			if (this.Gloved)
			{
				if (!this.Chased && this.Chasers == 0)
				{
					if (this.InputDevice.Type == InputDeviceType.Gamepad)
					{
						if (Input.GetAxis("DpadY") < -0.5f)
						{
							this.GloveTimer += Time.deltaTime;
							if (this.GloveTimer > 0.5f)
							{
								this.CharacterAnimation.CrossFade("f02_removeGloves_00");
								this.Degloving = true;
								this.CanMove = false;
							}
						}
						else
						{
							this.GloveTimer = 0f;
						}
					}
					else if (Input.GetKey(KeyCode.Alpha1))
					{
						this.GloveTimer += Time.deltaTime;
						if (this.GloveTimer > 0.1f)
						{
							this.CharacterAnimation.CrossFade("f02_removeGloves_00");
							this.Degloving = true;
							this.CanMove = false;
						}
					}
					else
					{
						this.GloveTimer = 0f;
					}
				}
				else
				{
					this.GloveTimer = 0f;
				}
			}
			if (this.Weapon[1] != null && this.DropTimer[2] == 0f)
			{
				if (this.InputDevice.Type == InputDeviceType.Gamepad)
				{
					if (Input.GetAxis("DpadX") < -0.5f)
					{
						this.DropWeapon(1);
					}
					else
					{
						this.DropTimer[1] = 0f;
					}
				}
				else if (Input.GetKey(KeyCode.Alpha2))
				{
					this.DropWeapon(1);
				}
				else
				{
					this.DropTimer[1] = 0f;
				}
			}
			if (this.Weapon[2] != null && this.DropTimer[1] == 0f)
			{
				if (this.InputDevice.Type == InputDeviceType.Gamepad)
				{
					if (Input.GetAxis("DpadX") > 0.5f)
					{
						this.DropWeapon(2);
					}
					else
					{
						this.DropTimer[2] = 0f;
					}
				}
				else if (Input.GetKey(KeyCode.Alpha3))
				{
					this.DropWeapon(2);
				}
				else
				{
					this.DropTimer[2] = 0f;
				}
			}
			if (Input.GetButtonDown("LS") || Input.GetKeyDown(KeyCode.T))
			{
				if (this.NewTrail != null)
				{
					UnityEngine.Object.Destroy(this.NewTrail);
				}
				this.NewTrail = UnityEngine.Object.Instantiate<GameObject>(this.Trail, base.transform.position + base.transform.forward * 0.5f + Vector3.up * 0.1f, Quaternion.identity);
				if (SchemeGlobals.CurrentScheme == 0)
				{
					this.NewTrail.GetComponent<AIPath>().target = this.Homeroom;
				}
				else if (this.PauseScreen.Schemes.SchemeDestinations[SchemeGlobals.GetSchemeStage(SchemeGlobals.CurrentScheme)] != null)
				{
					this.NewTrail.GetComponent<AIPath>().target = this.PauseScreen.Schemes.SchemeDestinations[SchemeGlobals.GetSchemeStage(SchemeGlobals.CurrentScheme)];
				}
				else
				{
					UnityEngine.Object.Destroy(this.NewTrail);
				}
			}
			if (this.Armed)
			{
				this.ID = 0;
				while (this.ID < this.ArmedAnims.Length)
				{
					string name = this.ArmedAnims[this.ID];
					this.CharacterAnimation[name].weight = Mathf.Lerp(this.CharacterAnimation[name].weight, (this.EquippedWeapon.AnimID == this.ID) ? 1f : 0f, Time.deltaTime * 10f);
					this.ID++;
				}
			}
			else
			{
				this.StopArmedAnim();
			}
			if (this.TheftTimer > 0f)
			{
				this.TheftTimer = Mathf.MoveTowards(this.TheftTimer, 0f, Time.deltaTime);
			}
			if (this.WeaponTimer > 0f)
			{
				this.WeaponTimer = Mathf.MoveTowards(this.WeaponTimer, 0f, Time.deltaTime);
			}
			if (this.MurderousActionTimer > 0f)
			{
				this.MurderousActionTimer = Mathf.MoveTowards(this.MurderousActionTimer, 0f, Time.deltaTime);
				if (this.MurderousActionTimer == 0f)
				{
					this.TargetStudent = null;
				}
			}
			if (this.Chased)
			{
				this.PreparedForStruggle = true;
				this.CanMove = false;
			}
			if (this.Egg)
			{
				if (this.Eating)
				{
					this.FollowHips = false;
					this.Attacking = false;
					this.CanMove = true;
					this.Eating = false;
					this.EatPhase = 0;
				}
				if (this.Pod.activeInHierarchy)
				{
					if (!this.SithAttacking)
					{
						if (this.LightSword.transform.parent != this.LightSwordParent)
						{
							this.LightSword.transform.parent = this.LightSwordParent;
							this.LightSword.transform.localPosition = new Vector3(0f, 0f, 0f);
							this.LightSword.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
							this.LightSwordParticles.Play();
						}
						if (this.HeavySword.transform.parent != this.HeavySwordParent)
						{
							this.HeavySword.transform.parent = this.HeavySwordParent;
							this.HeavySword.transform.localPosition = new Vector3(0f, 0f, 0f);
							this.HeavySword.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
							this.HeavySwordParticles.Play();
						}
					}
					if (Input.GetButtonDown("X"))
					{
						this.LightSword.transform.parent = this.LeftItemParent;
						this.LightSword.transform.localPosition = new Vector3(-0.015f, 0f, 0f);
						this.LightSword.transform.localEulerAngles = new Vector3(0f, 0f, -90f);
						this.LightSword.GetComponent<WeaponTrail>().enabled = true;
						this.LightSword.GetComponent<WeaponTrail>().Start();
						this.CharacterAnimation["f02_nierAttack_00"].time = 0f;
						this.CharacterAnimation.Play("f02_nierAttack_00");
						this.SithAttacking = true;
						this.CanMove = false;
						this.SithBeam[1].Damage = 10f;
						this.NierDamage = 10f;
						this.SithPrefix = "";
						this.AttackPrefix = "nier";
					}
					if (Input.GetButtonDown("Y"))
					{
						this.HeavySword.transform.parent = this.ItemParent;
						this.HeavySword.transform.localPosition = new Vector3(-0.015f, 0f, 0f);
						this.HeavySword.transform.localEulerAngles = new Vector3(0f, 0f, -90f);
						this.HeavySword.GetComponent<WeaponTrail>().enabled = true;
						this.HeavySword.GetComponent<WeaponTrail>().Start();
						this.CharacterAnimation["f02_nierAttackHard_00"].time = 0f;
						this.CharacterAnimation.Play("f02_nierAttackHard_00");
						this.SithAttacking = true;
						this.CanMove = false;
						this.SithBeam[1].Damage = 20f;
						this.NierDamage = 20f;
						this.SithPrefix = "Hard";
						this.AttackPrefix = "nier";
					}
				}
				if (this.WitchMode && Input.GetButtonDown("X") && this.InvertSphere.gameObject.activeInHierarchy)
				{
					this.CharacterAnimation["f02_fingerSnap_00"].time = 0f;
					this.CharacterAnimation.Play("f02_fingerSnap_00");
					this.CharacterAnimation.CrossFade(this.IdleAnim);
					this.Snapping = true;
					this.CanMove = false;
				}
				if (this.Armor[20].activeInHierarchy && this.Armor[20].transform.parent == this.ItemParent && (Input.GetButtonDown("X") || Input.GetButtonDown("Y")))
				{
					this.CharacterAnimation["f02_nierAttackHard_00"].time = 0f;
					this.CharacterAnimation.Play("f02_nierAttackHard_00");
					this.SithAttacking = true;
					this.CanMove = false;
					this.SithBeam[1].Damage = 20f;
					this.NierDamage = 20f;
					this.SithPrefix = "Hard";
					this.AttackPrefix = "nier";
					return;
				}
			}
		}
		else
		{
			if (this.Chased && !this.Sprayed && !this.Attacking && !this.Dumping && !this.StudentManager.PinningDown && !this.DelinquentFighting && !this.ShoulderCamera.HeartbrokenCamera.activeInHierarchy)
			{
				this.targetRotation = Quaternion.LookRotation(this.Pursuer.transform.position - base.transform.position);
				base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.targetRotation, Time.deltaTime * 10f);
				this.CharacterAnimation.CrossFade("f02_readyToFight_00");
				if (this.Dragging || this.Carrying)
				{
					this.EmptyHands();
				}
			}
			this.StopArmedAnim();
			if (this.Dumping)
			{
				this.targetRotation = Quaternion.LookRotation(this.Incinerator.transform.position - base.transform.position);
				base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.targetRotation, Time.deltaTime * 10f);
				this.MoveTowardsTarget(this.Incinerator.transform.position + Vector3.right * -2f);
				if (this.DumpTimer == 0f && this.Carrying)
				{
					this.CharacterAnimation["f02_carryDisposeA_00"].time = 2.5f;
				}
				this.DumpTimer += Time.deltaTime;
				if (this.DumpTimer > 1f)
				{
					if (this.Ragdoll != null && !this.Ragdoll.GetComponent<RagdollScript>().Dumped)
					{
						this.DumpRagdoll(RagdollDumpType.Incinerator);
					}
					this.CharacterAnimation.CrossFade("f02_carryDisposeA_00");
					if (this.CharacterAnimation["f02_carryDisposeA_00"].time >= this.CharacterAnimation["f02_carryDisposeA_00"].length)
					{
						this.Incinerator.Prompt.enabled = true;
						this.Incinerator.Ready = true;
						this.Incinerator.Open = false;
						this.Dragging = false;
						this.Dumping = false;
						this.CanMove = true;
						this.Ragdoll = null;
						this.StopCarrying();
						this.DumpTimer = 0f;
					}
				}
			}
			if (this.Chipping)
			{
				this.targetRotation = Quaternion.LookRotation(this.WoodChipper.gameObject.transform.position - base.transform.position);
				base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.targetRotation, Time.deltaTime * 10f);
				this.MoveTowardsTarget(this.WoodChipper.DumpPoint.position);
				if (this.DumpTimer == 0f && this.Carrying)
				{
					this.CharacterAnimation["f02_carryDisposeA_00"].time = 2.5f;
				}
				this.DumpTimer += Time.deltaTime;
				if (this.DumpTimer > 1f)
				{
					if (!this.Ragdoll.GetComponent<RagdollScript>().Dumped)
					{
						this.DumpRagdoll(RagdollDumpType.WoodChipper);
					}
					this.CharacterAnimation.CrossFade("f02_carryDisposeA_00");
					if (this.CharacterAnimation["f02_carryDisposeA_00"].time >= this.CharacterAnimation["f02_carryDisposeA_00"].length)
					{
						this.WoodChipper.Prompt.HideButton[0] = false;
						this.WoodChipper.Prompt.HideButton[3] = true;
						this.WoodChipper.Occupied = true;
						this.WoodChipper.Open = false;
						this.Dragging = false;
						this.Chipping = false;
						this.CanMove = true;
						this.Ragdoll = null;
						this.StopCarrying();
						this.DumpTimer = 0f;
					}
				}
			}
			if (this.TranquilHiding)
			{
				this.targetRotation = Quaternion.LookRotation(this.TranqCase.transform.position - base.transform.position);
				base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.targetRotation, Time.deltaTime * 10f);
				this.MoveTowardsTarget(this.TranqCase.transform.position + Vector3.right * 1.4f);
				if (this.DumpTimer == 0f && this.Carrying)
				{
					this.CharacterAnimation["f02_carryDisposeA_00"].time = 2.5f;
				}
				this.DumpTimer += Time.deltaTime;
				if (this.DumpTimer > 1f)
				{
					if (!this.Ragdoll.GetComponent<RagdollScript>().Dumped)
					{
						this.DumpRagdoll(RagdollDumpType.TranqCase);
					}
					this.CharacterAnimation.CrossFade("f02_carryDisposeA_00");
					if (this.CharacterAnimation["f02_carryDisposeA_00"].time >= this.CharacterAnimation["f02_carryDisposeA_00"].length)
					{
						this.TranquilHiding = false;
						this.Dragging = false;
						this.Dumping = false;
						this.CanMove = true;
						this.Ragdoll = null;
						this.StopCarrying();
						this.DumpTimer = 0f;
					}
				}
			}
			if (this.Dipping)
			{
				if (this.Bucket != null)
				{
					this.targetRotation = Quaternion.LookRotation(new Vector3(this.Bucket.transform.position.x, base.transform.position.y, this.Bucket.transform.position.z) - base.transform.position);
					base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.targetRotation, Time.deltaTime * 10f);
				}
				this.CharacterAnimation.CrossFade("f02_dipping_00");
				if (this.CharacterAnimation["f02_dipping_00"].time >= this.CharacterAnimation["f02_dipping_00"].length * 0.5f)
				{
					this.Mop.Bleached = true;
					this.Mop.Sparkles.Play();
					if (this.Mop.Bloodiness > 0f)
					{
						if (this.Bucket != null)
						{
							this.Bucket.Bloodiness += this.Mop.Bloodiness / 2f;
							this.Bucket.UpdateAppearance = true;
						}
						this.Mop.Bloodiness = 0f;
						this.Mop.UpdateBlood();
					}
				}
				if (this.CharacterAnimation["f02_dipping_00"].time >= this.CharacterAnimation["f02_dipping_00"].length)
				{
					this.CharacterAnimation["f02_dipping_00"].time = 0f;
					this.Mop.Prompt.enabled = true;
					this.Dipping = false;
					this.CanMove = true;
				}
			}
			if (this.Pouring)
			{
				this.MoveTowardsTarget(this.Stool.position);
				base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.Stool.rotation, 10f * Time.deltaTime);
				string text = "f02_bucketDump" + this.PourHeight + "_00";
				AnimationState animationState = this.CharacterAnimation[text];
				this.CharacterAnimation.CrossFade(text, 0f);
				if (animationState.time >= this.PourTime && !this.PickUp.Bucket.Poured)
				{
					if (this.PickUp.Bucket.Gasoline)
					{
						this.PickUp.Bucket.PourEffect.main.startColor = new Color(1f, 1f, 0f, 0.5f);
						UnityEngine.Object.Instantiate<GameObject>(this.PickUp.Bucket.GasCollider, this.PickUp.Bucket.PourEffect.transform.position + this.PourDistance * base.transform.forward, Quaternion.identity);
					}
					else if (this.PickUp.Bucket.Bloodiness < 50f)
					{
						this.PickUp.Bucket.PourEffect.main.startColor = new Color(0f, 1f, 1f, 0.5f);
						UnityEngine.Object.Instantiate<GameObject>(this.PickUp.Bucket.WaterCollider, this.PickUp.Bucket.PourEffect.transform.position + this.PourDistance * base.transform.forward, Quaternion.identity);
					}
					else
					{
						this.PickUp.Bucket.PourEffect.main.startColor = new Color(0.5f, 0f, 0f, 0.5f);
						UnityEngine.Object.Instantiate<GameObject>(this.PickUp.Bucket.BloodCollider, this.PickUp.Bucket.PourEffect.transform.position + this.PourDistance * base.transform.forward, Quaternion.identity);
					}
					this.PickUp.Bucket.PourEffect.Play();
					this.PickUp.Bucket.Poured = true;
					this.PickUp.Bucket.Empty();
				}
				if (animationState.time >= animationState.length)
				{
					animationState.time = 0f;
					this.PickUp.Bucket.Poured = false;
					this.Pouring = false;
					this.CanMove = true;
				}
			}
			if (this.Laughing)
			{
				if (this.Hairstyles[14].activeInHierarchy)
				{
					this.LaughAnim = "storepower_20";
					this.LaughClip = this.ChargeUp;
				}
				if (this.Stand.Stand.activeInHierarchy)
				{
					this.LaughAnim = "f02_jojoAttack_00";
					this.LaughClip = this.YanYan;
				}
				else if (this.FlameDemonic)
				{
					float axis = Input.GetAxis("Vertical");
					float axis2 = Input.GetAxis("Horizontal");
					Vector3 vector2 = this.MainCamera.transform.TransformDirection(Vector3.forward);
					vector2.y = 0f;
					vector2 = vector2.normalized;
					Vector3 a2 = new Vector3(vector2.z, 0f, -vector2.x);
					Vector3 vector3 = axis2 * a2 + axis * vector2;
					if (vector3 != Vector3.zero)
					{
						this.targetRotation = Quaternion.LookRotation(vector3);
						base.transform.rotation = Quaternion.Lerp(base.transform.rotation, this.targetRotation, Time.deltaTime * 10f);
					}
					this.LaughAnim = "f02_demonAttack_00";
					this.CirnoTimer -= Time.deltaTime;
					if (this.CirnoTimer < 0f)
					{
						UnityEngine.Object.Instantiate<GameObject>(this.Fireball, this.RightHand.position, base.transform.rotation).transform.localEulerAngles += new Vector3(UnityEngine.Random.Range(0f, 22.5f), UnityEngine.Random.Range(-22.5f, 22.5f), UnityEngine.Random.Range(-22.5f, 22.5f));
						UnityEngine.Object.Instantiate<GameObject>(this.Fireball, this.LeftHand.position, base.transform.rotation).transform.localEulerAngles += new Vector3(UnityEngine.Random.Range(0f, 22.5f), UnityEngine.Random.Range(-22.5f, 22.5f), UnityEngine.Random.Range(-22.5f, 22.5f));
						this.CirnoTimer = 0.1f;
					}
				}
				else if (this.CirnoHair.activeInHierarchy)
				{
					float axis3 = Input.GetAxis("Vertical");
					float axis4 = Input.GetAxis("Horizontal");
					Vector3 vector4 = this.MainCamera.transform.TransformDirection(Vector3.forward);
					vector4.y = 0f;
					vector4 = vector4.normalized;
					Vector3 a3 = new Vector3(vector4.z, 0f, -vector4.x);
					Vector3 vector5 = axis4 * a3 + axis3 * vector4;
					if (vector5 != Vector3.zero)
					{
						this.targetRotation = Quaternion.LookRotation(vector5);
						base.transform.rotation = Quaternion.Lerp(base.transform.rotation, this.targetRotation, Time.deltaTime * 10f);
					}
					this.LaughAnim = "f02_cirnoAttack_00";
					this.CirnoTimer -= Time.deltaTime;
					if (this.CirnoTimer < 0f)
					{
						UnityEngine.Object.Instantiate<GameObject>(this.CirnoIceAttack, base.transform.position + base.transform.up * 1.4f, base.transform.rotation).transform.localEulerAngles += new Vector3(UnityEngine.Random.Range(-5f, 5f), UnityEngine.Random.Range(-5f, 5f), UnityEngine.Random.Range(-5f, 5f));
						this.MyAudio.PlayOneShot(this.CirnoIceClip);
						this.CirnoTimer = 0.1f;
					}
				}
				else if (this.TornadoHair.activeInHierarchy)
				{
					this.LaughAnim = "f02_tornadoAttack_00";
					this.CirnoTimer -= Time.deltaTime;
					if (this.CirnoTimer < 0f)
					{
						GameObject gameObject4 = UnityEngine.Object.Instantiate<GameObject>(this.TornadoAttack, base.transform.forward * 5f + new Vector3(base.transform.position.x + UnityEngine.Random.Range(-5f, 5f), base.transform.position.y, base.transform.position.z + UnityEngine.Random.Range(-5f, 5f)), base.transform.rotation);
						while (Vector3.Distance(base.transform.position, gameObject4.transform.position) < 1f)
						{
							gameObject4.transform.position = base.transform.forward * 5f + new Vector3(base.transform.position.x + UnityEngine.Random.Range(-5f, 5f), base.transform.position.y, base.transform.position.z + UnityEngine.Random.Range(-5f, 5f));
						}
						this.CirnoTimer = 0.1f;
					}
				}
				else if (this.BladeHair.activeInHierarchy)
				{
					this.LaughAnim = "f02_spin_00";
					base.transform.localEulerAngles = new Vector3(base.transform.localEulerAngles.x, base.transform.localEulerAngles.y + Time.deltaTime * 360f * 2f, base.transform.localEulerAngles.z);
					this.BladeHairCollider1.enabled = true;
					this.BladeHairCollider2.enabled = true;
				}
				else if (this.BanchoActive)
				{
					this.BanchoFlurry.MyCollider.enabled = true;
					this.LaughAnim = "f02_banchoFlurry_00";
				}
				else if (this.MyAudio.clip != this.LaughClip)
				{
					this.MyAudio.clip = this.LaughClip;
					this.MyAudio.time = 0f;
					this.MyAudio.Play();
				}
				this.CharacterAnimation.CrossFade(this.LaughAnim);
				if (Input.GetButtonDown("RB"))
				{
					this.LaughIntensity += 1f;
					if (this.LaughIntensity <= 5f)
					{
						this.LaughAnim = "f02_laugh_01";
						this.LaughClip = this.Laugh1;
						this.LaughTimer = 0.5f;
					}
					else if (this.LaughIntensity <= 10f)
					{
						this.LaughAnim = "f02_laugh_02";
						this.LaughClip = this.Laugh2;
						this.LaughTimer = 1f;
					}
					else if (this.LaughIntensity <= 15f)
					{
						this.LaughAnim = "f02_laugh_03";
						this.LaughClip = this.Laugh3;
						this.LaughTimer = 1.5f;
					}
					else if (this.LaughIntensity <= 20f)
					{
						UnityEngine.Object.Instantiate<GameObject>(this.AlarmDisc, base.transform.position + Vector3.up, Quaternion.identity).GetComponent<AlarmDiscScript>().NoScream = true;
						this.LaughAnim = "f02_laugh_04";
						this.LaughClip = this.Laugh4;
						this.LaughTimer = 2f;
					}
					else
					{
						UnityEngine.Object.Instantiate<GameObject>(this.AlarmDisc, base.transform.position + Vector3.up, Quaternion.identity).GetComponent<AlarmDiscScript>().NoScream = true;
						this.LaughAnim = "f02_laugh_04";
						this.LaughIntensity = 20f;
						this.LaughTimer = 2f;
					}
				}
				if (this.LaughIntensity > 15f)
				{
					this.Sanity += Time.deltaTime * 10f;
				}
				this.LaughTimer -= Time.deltaTime;
				if (this.LaughTimer <= 0f)
				{
					this.StopLaughing();
				}
			}
			if (this.TimeSkipping)
			{
				base.transform.position = new Vector3(base.transform.position.x, this.TimeSkipHeight, base.transform.position.z);
				this.CharacterAnimation.CrossFade("f02_timeSkip_00");
				this.MyController.Move(base.transform.up * 0.0001f);
				this.Sanity += Time.deltaTime * 0.17f;
			}
			if (this.DumpsterGrabbing)
			{
				if (Input.GetAxis("Horizontal") > 0.5f || Input.GetAxis("DpadX") > 0.5f || Input.GetKey("right"))
				{
					this.CharacterAnimation.CrossFade((this.DumpsterHandle.Direction == -1f) ? "f02_dumpsterPull_00" : "f02_dumpsterPush_00");
				}
				else if (Input.GetAxis("Horizontal") < -0.5f || Input.GetAxis("DpadX") < -0.5f || Input.GetKey("left"))
				{
					this.CharacterAnimation.CrossFade((this.DumpsterHandle.Direction == -1f) ? "f02_dumpsterPush_00" : "f02_dumpsterPull_00");
				}
				else
				{
					this.CharacterAnimation.CrossFade("f02_dumpsterGrab_00");
				}
			}
			if (this.Stripping)
			{
				base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.StudentManager.YandereStripSpot.rotation, 10f * Time.deltaTime);
				if (this.CharacterAnimation["f02_stripping_00"].time >= this.CharacterAnimation["f02_stripping_00"].length)
				{
					this.Stripping = false;
					this.CanMove = true;
					this.MyLocker.UpdateSchoolwear();
				}
			}
			if (this.Bathing)
			{
				this.MoveTowardsTarget(this.YandereShower.BatheSpot.position);
				base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.YandereShower.BatheSpot.rotation, 10f * Time.deltaTime);
				this.CharacterAnimation.CrossFade(this.IdleAnim);
				if (this.YandereShower.Timer < 1f)
				{
					this.Bloodiness = 0f;
					this.Bathing = false;
					this.CanMove = true;
				}
			}
			if (this.Degloving)
			{
				this.CharacterAnimation.CrossFade("f02_removeGloves_00");
				if (this.CharacterAnimation["f02_removeGloves_00"].time >= this.CharacterAnimation["f02_removeGloves_00"].length)
				{
					this.Gloves.GetComponent<Rigidbody>().isKinematic = false;
					this.Gloves.transform.parent = null;
					this.GloveAttacher.newRenderer.enabled = false;
					this.Gloves.gameObject.SetActive(true);
					this.Degloving = false;
					this.CanMove = true;
					this.Gloved = false;
					this.Gloves = null;
					this.SetUniform();
					this.GloveBlood = 0;
					Debug.Log("Gloves removed.");
				}
				else if (this.Chased || this.Chasers > 0 || this.Noticed)
				{
					this.Degloving = false;
					this.GloveTimer = 0f;
					if (!this.Noticed)
					{
						this.CanMove = true;
					}
				}
				else if (this.InputDevice.Type == InputDeviceType.Gamepad)
				{
					if (Input.GetAxis("DpadY") > -0.5f)
					{
						this.Degloving = false;
						this.CanMove = true;
						this.GloveTimer = 0f;
					}
				}
				else if (Input.GetKeyUp(KeyCode.Alpha1))
				{
					this.Degloving = false;
					this.CanMove = true;
					this.GloveTimer = 0f;
				}
			}
			if (this.Struggling)
			{
				if (!this.Won && !this.Lost)
				{
					this.CharacterAnimation.CrossFade(this.TargetStudent.Teacher ? "f02_teacherStruggleA_00" : "f02_struggleA_00");
					this.targetRotation = Quaternion.LookRotation(this.TargetStudent.transform.position - base.transform.position);
					base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.targetRotation, 10f * Time.deltaTime);
				}
				else if (this.Won)
				{
					if (!this.TargetStudent.Teacher)
					{
						this.CharacterAnimation.CrossFade("f02_struggleWinA_00");
						if (this.CharacterAnimation["f02_struggleWinA_00"].time > this.CharacterAnimation["f02_struggleWinA_00"].length - 1f)
						{
							this.EquippedWeapon.transform.localEulerAngles = Vector3.Lerp(this.EquippedWeapon.transform.localEulerAngles, Vector3.zero, Time.deltaTime * 3.33333f);
						}
					}
					else
					{
						this.CharacterAnimation.CrossFade("f02_teacherStruggleWinA_00");
						this.EquippedWeapon.transform.localEulerAngles = Vector3.Lerp(this.EquippedWeapon.transform.localEulerAngles, Vector3.zero, Time.deltaTime);
					}
					if (this.StrugglePhase == 0)
					{
						if ((!this.TargetStudent.Teacher && this.CharacterAnimation["f02_struggleWinA_00"].time > 1.3f) || (this.TargetStudent.Teacher && this.CharacterAnimation["f02_teacherStruggleWinA_00"].time > 0.8f))
						{
							Debug.Log("Yandere-chan just killed " + this.TargetStudent.Name + " as a result of winning a struggling against them.");
							this.TargetStudent.DeathCause = this.EquippedWeapon.WeaponID;
							UnityEngine.Object.Instantiate<GameObject>(this.TargetStudent.StabBloodEffect, this.TargetStudent.Teacher ? this.EquippedWeapon.transform.position : this.TargetStudent.Head.position, Quaternion.identity);
							this.Bloodiness += 20f;
							this.Sanity -= 20f * this.Numbness;
							this.StainWeapon();
							this.StrugglePhase++;
						}
					}
					else if (this.StrugglePhase == 1)
					{
						if (this.TargetStudent.Teacher && this.CharacterAnimation["f02_teacherStruggleWinA_00"].time > 1.3f)
						{
							UnityEngine.Object.Instantiate<GameObject>(this.TargetStudent.StabBloodEffect, this.EquippedWeapon.transform.position, Quaternion.identity);
							this.StrugglePhase++;
						}
					}
					else if (this.StrugglePhase == 2 && this.TargetStudent.Teacher && this.CharacterAnimation["f02_teacherStruggleWinA_00"].time > 2.1f)
					{
						UnityEngine.Object.Instantiate<GameObject>(this.TargetStudent.StabBloodEffect, this.EquippedWeapon.transform.position, Quaternion.identity);
						this.StrugglePhase++;
					}
					if ((!this.TargetStudent.Teacher && this.CharacterAnimation["f02_struggleWinA_00"].time > this.CharacterAnimation["f02_struggleWinA_00"].length) || (this.TargetStudent.Teacher && this.CharacterAnimation["f02_teacherStruggleWinA_00"].time > this.CharacterAnimation["f02_teacherStruggleWinA_00"].length))
					{
						this.MyController.radius = 0.2f;
						this.CharacterAnimation.CrossFade(this.IdleAnim);
						this.ShoulderCamera.Struggle = false;
						this.Struggling = false;
						this.StrugglePhase = 0;
						if (this.TargetStudent == this.Pursuer)
						{
							this.Pursuer = null;
							this.Chased = false;
						}
						this.TargetStudent.BecomeRagdoll();
						this.TargetStudent.DeathType = DeathType.Weapon;
						this.SeenByAuthority = false;
					}
				}
				else if (this.Lost)
				{
					this.CharacterAnimation.CrossFade(this.TargetStudent.Teacher ? "f02_teacherStruggleLoseA_00" : "f02_struggleLoseA_00");
				}
			}
			if (this.ClubActivity)
			{
				if (ClubGlobals.Club == ClubType.Drama)
				{
					this.CharacterAnimation.Play("f02_performing_00");
				}
				else if (ClubGlobals.Club == ClubType.Art)
				{
					this.CharacterAnimation.Play("f02_painting_00");
				}
				else if (ClubGlobals.Club == ClubType.MartialArts)
				{
					this.CharacterAnimation.Play("f02_kick_23");
					if (this.CharacterAnimation["f02_kick_23"].time >= this.CharacterAnimation["f02_kick_23"].length)
					{
						this.CharacterAnimation["f02_kick_23"].time = 0f;
					}
				}
				else if (ClubGlobals.Club == ClubType.Photography)
				{
					this.CharacterAnimation.Play("f02_sit_00");
				}
				else if (ClubGlobals.Club == ClubType.Gaming)
				{
					this.CharacterAnimation.Play("f02_playingGames_00");
				}
			}
			if (this.Possessed)
			{
				this.CharacterAnimation.CrossFade("f02_possessionPose_00");
			}
			if (this.Lifting)
			{
				if (!this.HeavyWeight)
				{
					if (this.CharacterAnimation["f02_carryLiftA_00"].time >= this.CharacterAnimation["f02_carryLiftA_00"].length)
					{
						this.IdleAnim = this.CarryIdleAnim;
						this.WalkAnim = this.CarryWalkAnim;
						this.RunAnim = this.CarryRunAnim;
						this.CanMove = true;
						this.Carrying = true;
						this.Lifting = false;
					}
				}
				else if (this.CharacterAnimation["f02_heavyWeightLift_00"].time >= this.CharacterAnimation["f02_heavyWeightLift_00"].length)
				{
					this.CharacterAnimation[this.CarryAnims[0]].weight = 1f;
					this.IdleAnim = this.HeavyIdleAnim;
					this.WalkAnim = this.HeavyWalkAnim;
					this.RunAnim = this.CarryRunAnim;
					this.CanMove = true;
					this.Lifting = false;
				}
			}
			if (this.Dropping)
			{
				this.targetRotation = Quaternion.LookRotation(this.DropSpot.position + this.DropSpot.forward - base.transform.position);
				base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.targetRotation, Time.deltaTime * 10f);
				this.MoveTowardsTarget(this.DropSpot.position);
				if (this.Ragdoll != null && this.CurrentRagdoll == null)
				{
					this.CurrentRagdoll = this.Ragdoll.GetComponent<RagdollScript>();
				}
				if (this.DumpTimer == 0f && this.Carrying)
				{
					this.CurrentRagdoll.CharacterAnimation[this.CurrentRagdoll.DumpedAnim].time = 2.5f;
					this.CharacterAnimation["f02_carryDisposeA_00"].time = 2.5f;
				}
				this.DumpTimer += Time.deltaTime;
				if (this.DumpTimer > 1f)
				{
					if (this.Ragdoll != null)
					{
						this.CurrentRagdoll.PelvisRoot.localEulerAngles = new Vector3(this.CurrentRagdoll.PelvisRoot.localEulerAngles.x, 0f, this.CurrentRagdoll.PelvisRoot.localEulerAngles.z);
						this.CurrentRagdoll.PelvisRoot.localPosition = new Vector3(this.CurrentRagdoll.PelvisRoot.localPosition.x, this.CurrentRagdoll.PelvisRoot.localPosition.y, 0f);
					}
					this.CameraTarget.position = Vector3.MoveTowards(this.CameraTarget.position, new Vector3(this.Hips.position.x, base.transform.position.y + 1f, this.Hips.position.z), Time.deltaTime * 10f);
					if (this.CharacterAnimation["f02_carryDisposeA_00"].time >= 4.5f)
					{
						this.StopCarrying();
					}
					else
					{
						if (this.CurrentRagdoll.StopAnimation)
						{
							this.CurrentRagdoll.StopAnimation = false;
							this.ID = 0;
							while (this.ID < this.CurrentRagdoll.AllRigidbodies.Length)
							{
								this.CurrentRagdoll.AllRigidbodies[this.ID].isKinematic = true;
								this.ID++;
							}
						}
						this.CharacterAnimation.CrossFade("f02_carryDisposeA_00");
						this.CurrentRagdoll.CharacterAnimation.CrossFade(this.CurrentRagdoll.DumpedAnim);
						this.Ragdoll.transform.position = base.transform.position;
						this.Ragdoll.transform.eulerAngles = base.transform.eulerAngles;
					}
					if (this.CharacterAnimation["f02_carryDisposeA_00"].time >= this.CharacterAnimation["f02_carryDisposeA_00"].length)
					{
						this.CameraTarget.localPosition = new Vector3(0f, 1f, 0f);
						this.Dropping = false;
						this.CanMove = true;
						this.DumpTimer = 0f;
					}
				}
			}
			if (this.Dismembering && this.CharacterAnimation["f02_dismember_00"].time >= this.CharacterAnimation["f02_dismember_00"].length)
			{
				this.Ragdoll.GetComponent<RagdollScript>().Dismember();
				this.RPGCamera.enabled = true;
				this.TargetStudent = null;
				this.Dismembering = false;
				this.CanMove = true;
				this.Ragdoll = null;
			}
			if (this.Shoved)
			{
				if (this.CharacterAnimation["f02_shoveA_01"].time >= this.CharacterAnimation["f02_shoveA_01"].length)
				{
					this.CharacterAnimation.CrossFade(this.IdleAnim);
					this.Shoved = false;
					if (!this.CannotRecover)
					{
						this.CanMove = true;
					}
				}
				else if (this.CharacterAnimation["f02_shoveA_01"].time < 0.66666f)
				{
					this.MyController.Move(base.transform.forward * -1f * this.ShoveSpeed * Time.deltaTime);
					this.MyController.Move(Physics.gravity * 0.1f);
					if (this.ShoveSpeed > 0f)
					{
						this.ShoveSpeed = Mathf.MoveTowards(this.ShoveSpeed, 0f, Time.deltaTime * 3f);
					}
				}
			}
			if (this.Attacked && this.CharacterAnimation["f02_swingB_00"].time >= this.CharacterAnimation["f02_swingB_00"].length)
			{
				this.ShoulderCamera.HeartbrokenCamera.SetActive(true);
				base.enabled = false;
			}
			if (this.Hiding)
			{
				if (!this.Exiting)
				{
					base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.HidingSpot.rotation, Time.deltaTime * 10f);
					this.MoveTowardsTarget(this.HidingSpot.position);
					this.CharacterAnimation.CrossFade(this.HideAnim);
					if (Input.GetButtonDown("B"))
					{
						this.PromptBar.ClearButtons();
						this.PromptBar.Show = false;
						this.Exiting = true;
					}
				}
				else
				{
					this.MoveTowardsTarget(this.ExitSpot.position);
					this.CharacterAnimation.CrossFade(this.IdleAnim);
					this.ExitTimer += Time.deltaTime;
					if (this.ExitTimer > 1f || Vector3.Distance(base.transform.position, this.ExitSpot.position) < 0.1f)
					{
						this.MyController.center = new Vector3(this.MyController.center.x, 0.875f, this.MyController.center.z);
						this.MyController.radius = 0.2f;
						this.MyController.height = 1.55f;
						this.ExitTimer = 0f;
						this.Exiting = false;
						this.CanMove = true;
						this.Hiding = false;
					}
				}
			}
			if (this.BucketDropping)
			{
				this.targetRotation = Quaternion.LookRotation(this.DropSpot.position + this.DropSpot.forward - base.transform.position);
				base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.targetRotation, Time.deltaTime * 10f);
				this.MoveTowardsTarget(this.DropSpot.position);
				if (this.CharacterAnimation["f02_bucketDrop_00"].time >= this.CharacterAnimation["f02_bucketDrop_00"].length)
				{
					this.MyController.radius = 0.2f;
					this.BucketDropping = false;
					this.CanMove = true;
				}
				else if (this.CharacterAnimation["f02_bucketDrop_00"].time >= 1f && this.PickUp != null)
				{
					GameObjectUtils.SetLayerRecursively(this.PickUp.Bucket.gameObject, 0);
					this.PickUp.Bucket.UpdateAppearance = true;
					this.PickUp.Bucket.Dropped = true;
					this.EmptyHands();
				}
			}
			if (this.Flicking)
			{
				if (this.CharacterAnimation["f02_flickingMatch_00"].time >= this.CharacterAnimation["f02_flickingMatch_00"].length)
				{
					this.PickUp.GetComponent<MatchboxScript>().Prompt.enabled = true;
					this.Arc.SetActive(true);
					this.Flicking = false;
					this.CanMove = true;
				}
				else if (this.CharacterAnimation["f02_flickingMatch_00"].time > 1f && this.Match != null)
				{
					Rigidbody component = this.Match.GetComponent<Rigidbody>();
					component.isKinematic = false;
					component.useGravity = true;
					component.AddRelativeForce(Vector3.right * 250f);
					this.Match.transform.parent = null;
					this.Match = null;
				}
			}
			if (this.Rummaging)
			{
				this.MoveTowardsTarget(this.RummageSpot.Target.position);
				base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.RummageSpot.Target.rotation, Time.deltaTime * 10f);
				this.RummageTimer += Time.deltaTime;
				this.ProgressBar.transform.localScale = new Vector3(this.RummageTimer / 10f, this.ProgressBar.transform.localScale.y, this.ProgressBar.transform.localScale.z);
				if (this.RummageTimer > 10f)
				{
					this.RummageSpot.GetReward();
					this.ProgressBar.transform.parent.gameObject.SetActive(false);
					this.RummageSpot = null;
					this.Rummaging = false;
					this.RummageTimer = 0f;
					this.CanMove = true;
				}
			}
			if (this.Digging)
			{
				if (this.DigPhase == 1)
				{
					if (this.CharacterAnimation["f02_shovelDig_00"].time >= 1.6666666f)
					{
						this.MyAudio.volume = 1f;
						this.MyAudio.clip = this.Dig;
						this.MyAudio.Play();
						this.DigPhase++;
					}
				}
				else if (this.DigPhase == 2)
				{
					if (this.CharacterAnimation["f02_shovelDig_00"].time >= 3.5f)
					{
						this.MyAudio.volume = 1f;
						this.MyAudio.Play();
						this.DigPhase++;
					}
				}
				else if (this.DigPhase == 3)
				{
					if (this.CharacterAnimation["f02_shovelDig_00"].time >= 5.6666665f)
					{
						this.MyAudio.volume = 1f;
						this.MyAudio.Play();
						this.DigPhase++;
					}
				}
				else if (this.DigPhase == 4 && this.CharacterAnimation["f02_shovelDig_00"].time >= this.CharacterAnimation["f02_shovelDig_00"].length)
				{
					this.EquippedWeapon.gameObject.SetActive(true);
					this.FloatingShovel.SetActive(false);
					this.RPGCamera.enabled = true;
					this.Digging = false;
					this.CanMove = true;
				}
			}
			if (this.Burying)
			{
				if (this.DigPhase == 1)
				{
					if (this.CharacterAnimation["f02_shovelBury_00"].time >= 2.1666667f)
					{
						this.MyAudio.volume = 1f;
						this.MyAudio.clip = this.Dig;
						this.MyAudio.Play();
						this.DigPhase++;
					}
				}
				else if (this.DigPhase == 2)
				{
					if (this.CharacterAnimation["f02_shovelBury_00"].time >= 4.6666665f)
					{
						this.MyAudio.volume = 1f;
						this.MyAudio.Play();
						this.DigPhase++;
					}
				}
				else if (this.CharacterAnimation["f02_shovelBury_00"].time >= this.CharacterAnimation["f02_shovelBury_00"].length)
				{
					this.EquippedWeapon.gameObject.SetActive(true);
					this.FloatingShovel.SetActive(false);
					this.RPGCamera.enabled = true;
					this.Burying = false;
					this.CanMove = true;
				}
			}
			if (this.Pickpocketing && !this.Noticed && this.Caught)
			{
				this.CaughtTimer += Time.deltaTime;
				if (this.CaughtTimer > 1f)
				{
					if (!this.CannotRecover)
					{
						this.CanMove = true;
					}
					this.Pickpocketing = false;
					this.CaughtTimer = 0f;
					this.Caught = false;
				}
			}
			if (this.Sprayed)
			{
				if (this.SprayPhase == 0)
				{
					if ((double)this.CharacterAnimation["f02_sprayed_00"].time > 0.66666)
					{
						this.Blur.enabled = true;
						this.Blur.blurSize += Time.deltaTime;
						if (this.Blur.blurSize > (float)this.Blur.blurIterations)
						{
							this.Blur.blurIterations++;
						}
					}
					if (this.CharacterAnimation["f02_sprayed_00"].time > 5f)
					{
						this.Police.Darkness.enabled = true;
						this.Police.Darkness.color = new Color(0f, 0f, 0f, Mathf.MoveTowards(this.Police.Darkness.color.a, 1f, Time.deltaTime));
						if (this.Police.Darkness.color.a == 1f)
						{
							this.SprayTimer += Time.deltaTime;
							if (this.SprayTimer > 1f)
							{
								this.CharacterAnimation.Play("f02_tied_00");
								this.RPGCamera.enabled = false;
								this.ZipTie[0].SetActive(true);
								this.ZipTie[1].SetActive(true);
								this.Blur.enabled = false;
								this.SprayTimer = 0f;
								this.SprayPhase++;
							}
						}
					}
				}
				else if (this.SprayPhase == 1)
				{
					this.Police.Darkness.color = new Color(0f, 0f, 0f, Mathf.MoveTowards(this.Police.Darkness.color.a, 0f, Time.deltaTime));
					if (this.Police.Darkness.color.a == 0f)
					{
						this.SprayTimer += Time.deltaTime;
						if (this.SprayTimer > 1f)
						{
							this.ShoulderCamera.HeartbrokenCamera.SetActive(true);
							this.HeartCamera.gameObject.SetActive(false);
							this.HUD.alpha = 0f;
							this.SprayPhase++;
						}
					}
				}
			}
			if (this.CleaningWeapon)
			{
				base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.Target.rotation, Time.deltaTime * 10f);
				this.MoveTowardsTarget(this.Target.position);
				if (this.CharacterAnimation["f02_cleaningWeapon_00"].time >= this.CharacterAnimation["f02_cleaningWeapon_00"].length)
				{
					this.EquippedWeapon.Blood.enabled = false;
					this.EquippedWeapon.Bloody = false;
					if (this.Gloved)
					{
						this.EquippedWeapon.FingerprintID = 0;
					}
					this.CleaningWeapon = false;
					this.CanMove = true;
				}
			}
			if (this.CrushingPhone)
			{
				this.CharacterAnimation.CrossFade("f02_phoneCrush_00");
				this.targetRotation = Quaternion.LookRotation(new Vector3(this.PhoneToCrush.transform.position.x, base.transform.position.y, this.PhoneToCrush.transform.position.z) - base.transform.position);
				base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.targetRotation, Time.deltaTime * 10f);
				this.MoveTowardsTarget(this.PhoneToCrush.PhoneCrushingSpot.position);
				if (this.CharacterAnimation["f02_phoneCrush_00"].time >= 0.5f && this.PhoneToCrush.enabled)
				{
					this.PhoneToCrush.transform.localEulerAngles = new Vector3(this.PhoneToCrush.transform.localEulerAngles.x, this.PhoneToCrush.transform.localEulerAngles.y, 0f);
					UnityEngine.Object.Instantiate<GameObject>(this.PhoneToCrush.PhoneSmash, this.PhoneToCrush.transform.position, Quaternion.identity);
					this.Police.PhotoEvidence--;
					this.PhoneToCrush.MyRenderer.material.mainTexture = this.PhoneToCrush.SmashedTexture;
					this.PhoneToCrush.MyMesh.mesh = this.PhoneToCrush.SmashedMesh;
					this.PhoneToCrush.Prompt.Hide();
					this.PhoneToCrush.Prompt.enabled = false;
					this.PhoneToCrush.enabled = false;
				}
				if (this.CharacterAnimation["f02_phoneCrush_00"].time >= this.CharacterAnimation["f02_phoneCrush_00"].length)
				{
					this.CrushingPhone = false;
					this.CanMove = true;
				}
			}
			if (this.SubtleStabbing)
			{
				if (this.CharacterAnimation["f02_subtleStab_00"].time < 0.5f)
				{
					this.CharacterAnimation.CrossFade("f02_subtleStab_00");
					this.targetRotation = Quaternion.LookRotation(new Vector3(this.TargetStudent.transform.position.x, base.transform.position.y, this.TargetStudent.transform.position.z) - base.transform.position);
					base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.targetRotation, Time.deltaTime * 10f);
					this.MoveTowardsTarget(this.TargetStudent.transform.position + this.TargetStudent.transform.forward * -1f);
				}
				else if (this.TargetStudent.Strength > 0)
				{
					this.TargetStudent.Strength = 0;
					this.TargetStudent.Hunter.MurderSuicidePhase = 0;
					this.TargetStudent.Hunter.AttackWillFail = false;
					this.TargetStudent.Hunter.Pathfinding.canMove = true;
					this.TargetStudent.CharacterAnimation["f02_murderSuicide_01"].time = 1.5f;
					this.TargetStudent.Hunter.CharacterAnimation["f02_murderSuicide_00"].time = 1.5f;
					Debug.Log("Making the hunter's attack a success!");
				}
				if (this.CharacterAnimation["f02_subtleStab_00"].time >= this.CharacterAnimation["f02_subtleStab_00"].length)
				{
					this.SubtleStabbing = false;
					this.CanMove = true;
				}
			}
			if (this.CanMoveTimer > 0f)
			{
				this.CanMoveTimer = Mathf.MoveTowards(this.CanMoveTimer, 0f, Time.deltaTime);
				if (this.CanMoveTimer == 0f)
				{
					this.CanMove = true;
				}
			}
			if (this.Egg)
			{
				if (this.Punching)
				{
					if (this.FalconHelmet.activeInHierarchy)
					{
						if (this.CharacterAnimation["f02_falconPunch_00"].time >= 1f && this.CharacterAnimation["f02_falconPunch_00"].time <= 1.25f)
						{
							this.FalconSpeed = Mathf.MoveTowards(this.FalconSpeed, 2.5f, Time.deltaTime * 2.5f);
						}
						else if (this.CharacterAnimation["f02_falconPunch_00"].time >= 1.25f && this.CharacterAnimation["f02_falconPunch_00"].time <= 1.5f)
						{
							this.FalconSpeed = Mathf.MoveTowards(this.FalconSpeed, 0f, Time.deltaTime * 2.5f);
						}
						if (this.CharacterAnimation["f02_falconPunch_00"].time >= 1f && this.CharacterAnimation["f02_falconPunch_00"].time <= 1.5f)
						{
							if (this.NewFalconPunch == null)
							{
								this.NewFalconPunch = UnityEngine.Object.Instantiate<GameObject>(this.FalconPunch);
								this.NewFalconPunch.transform.parent = this.ItemParent;
								this.NewFalconPunch.transform.localPosition = Vector3.zero;
							}
							this.MyController.Move(base.transform.forward * this.FalconSpeed);
						}
						if (this.CharacterAnimation["f02_falconPunch_00"].time >= this.CharacterAnimation["f02_falconPunch_00"].length)
						{
							this.NewFalconPunch = null;
							this.Punching = false;
							this.CanMove = true;
						}
					}
					else
					{
						if (this.CharacterAnimation["f02_onePunch_00"].time >= 0.833333f && this.CharacterAnimation["f02_onePunch_00"].time <= 1f && this.NewOnePunch == null)
						{
							this.NewOnePunch = UnityEngine.Object.Instantiate<GameObject>(this.OnePunch);
							this.NewOnePunch.transform.parent = this.ItemParent;
							this.NewOnePunch.transform.localPosition = Vector3.zero;
						}
						if (this.CharacterAnimation["f02_onePunch_00"].time >= 2f)
						{
							this.NewOnePunch = null;
							this.Punching = false;
							this.CanMove = true;
						}
					}
				}
				if (this.PK)
				{
					if (Input.GetAxis("Vertical") > 0.5f)
					{
						this.GoToPKDir(PKDirType.Up, "f02_sansUp_00", new Vector3(0f, 3f, 2f));
					}
					else if (Input.GetAxis("Vertical") < -0.5f)
					{
						this.GoToPKDir(PKDirType.Down, "f02_sansDown_00", new Vector3(0f, 0f, 2f));
					}
					else if (Input.GetAxis("Horizontal") > 0.5f)
					{
						this.GoToPKDir(PKDirType.Right, "f02_sansRight_00", new Vector3(1.5f, 1.5f, 2f));
					}
					else if (Input.GetAxis("Horizontal") < -0.5f)
					{
						this.GoToPKDir(PKDirType.Left, "f02_sansLeft_00", new Vector3(-1.5f, 1.5f, 2f));
					}
					else
					{
						this.CharacterAnimation.CrossFade("f02_sansHold_00");
						this.RagdollPK.transform.localPosition = new Vector3(0f, 1.5f, 2f);
						this.PKDir = PKDirType.None;
					}
					if (Input.GetButtonDown("B"))
					{
						this.PromptBar.ClearButtons();
						this.PromptBar.UpdateButtons();
						this.PromptBar.Show = false;
						this.Ragdoll.GetComponent<RagdollScript>().StopDragging();
						this.SansEyes[0].SetActive(false);
						this.SansEyes[1].SetActive(false);
						this.GlowEffect.Stop();
						this.CanMove = true;
						this.PK = false;
					}
				}
				if (this.SummonBones)
				{
					this.CharacterAnimation.CrossFade("f02_sansBones_00");
					if (this.BoneTimer == 0f)
					{
						UnityEngine.Object.Instantiate<GameObject>(this.Bone, base.transform.position + base.transform.right * UnityEngine.Random.Range(-2.5f, 2.5f) + base.transform.up * -2f + base.transform.forward * UnityEngine.Random.Range(1f, 6f), Quaternion.identity);
					}
					this.BoneTimer += Time.deltaTime;
					if (this.BoneTimer > 0.1f)
					{
						this.BoneTimer = 0f;
					}
					if (Input.GetButtonUp("RB"))
					{
						this.SansEyes[0].SetActive(false);
						this.SansEyes[1].SetActive(false);
						this.GlowEffect.Stop();
						this.SummonBones = false;
						this.CanMove = true;
					}
					if (this.PK)
					{
						this.PromptBar.ClearButtons();
						this.PromptBar.UpdateButtons();
						this.PromptBar.Show = false;
						this.Ragdoll.GetComponent<RagdollScript>().StopDragging();
						this.SansEyes[0].SetActive(false);
						this.SansEyes[1].SetActive(false);
						this.GlowEffect.Stop();
						this.CanMove = true;
						this.PK = false;
					}
				}
				if (this.Blasting)
				{
					if (this.CharacterAnimation["f02_sansBlaster_00"].time >= this.CharacterAnimation["f02_sansBlaster_00"].length - 0.25f)
					{
						this.SansEyes[0].SetActive(false);
						this.SansEyes[1].SetActive(false);
						this.GlowEffect.Stop();
						this.Blasting = false;
						this.CanMove = true;
					}
					if (this.PK)
					{
						this.PromptBar.ClearButtons();
						this.PromptBar.UpdateButtons();
						this.PromptBar.Show = false;
						this.Ragdoll.GetComponent<RagdollScript>().StopDragging();
						this.SansEyes[0].SetActive(false);
						this.SansEyes[1].SetActive(false);
						this.GlowEffect.Stop();
						this.CanMove = true;
						this.PK = false;
					}
				}
				if (this.SithAttacking)
				{
					if (!this.SithRecovering)
					{
						if (this.SithBeam[1].Damage == 10f || this.NierDamage == 10f)
						{
							if (this.SithAttacks == 0 && this.CharacterAnimation[string.Concat(new object[]
							{
								"f02_",
								this.AttackPrefix,
								"Attack",
								this.SithPrefix,
								"_0",
								this.SithCombo
							})].time >= this.SithSpawnTime[this.SithCombo])
							{
								UnityEngine.Object.Instantiate<GameObject>(this.SithHitbox, base.transform.position + base.transform.forward * 1f + base.transform.up, base.transform.rotation);
								this.SithAttacks++;
							}
						}
						else if (this.Pod.activeInHierarchy || this.Armor[20].activeInHierarchy)
						{
							if (this.CharacterAnimation[string.Concat(new object[]
							{
								"f02_",
								this.AttackPrefix,
								"Attack",
								this.SithPrefix,
								"_0",
								this.SithCombo
							})].time >= this.SithHardSpawnTime1[this.SithCombo] && this.SithAttacks == 0)
							{
								UnityEngine.Object.Instantiate<GameObject>(this.SithHitbox, base.transform.position + base.transform.forward * 1.5f + base.transform.up, base.transform.rotation).GetComponent<SithBeamScript>().Damage = 20f;
								this.SithAttacks++;
								if (this.SithCombo < 2)
								{
									UnityEngine.Object.Instantiate<GameObject>(this.GroundImpact, base.transform.position + base.transform.forward * 1.5f, base.transform.rotation).transform.localScale = new Vector3(2f, 2f, 2f);
								}
							}
						}
						else if (this.SithAttacks == 0)
						{
							if (this.CharacterAnimation[string.Concat(new object[]
							{
								"f02_",
								this.AttackPrefix,
								"Attack",
								this.SithPrefix,
								"_0",
								this.SithCombo
							})].time >= this.SithHardSpawnTime1[this.SithCombo])
							{
								UnityEngine.Object.Instantiate<GameObject>(this.SithHardHitbox, base.transform.position + base.transform.forward * 1f + base.transform.up, base.transform.rotation);
								this.SithAttacks++;
							}
						}
						else if (this.SithAttacks == 1)
						{
							if (this.CharacterAnimation[string.Concat(new object[]
							{
								"f02_",
								this.AttackPrefix,
								"Attack",
								this.SithPrefix,
								"_0",
								this.SithCombo
							})].time >= this.SithHardSpawnTime2[this.SithCombo])
							{
								UnityEngine.Object.Instantiate<GameObject>(this.SithHardHitbox, base.transform.position + base.transform.forward * 1f + base.transform.up, base.transform.rotation);
								this.SithAttacks++;
							}
						}
						else if (this.SithAttacks == 2 && this.SithCombo == 1 && this.CharacterAnimation[string.Concat(new object[]
						{
							"f02_",
							this.AttackPrefix,
							"Attack",
							this.SithPrefix,
							"_0",
							this.SithCombo
						})].time >= 0.93333334f)
						{
							UnityEngine.Object.Instantiate<GameObject>(this.SithHardHitbox, base.transform.position + base.transform.forward * 1f + base.transform.up, base.transform.rotation);
							this.SithAttacks++;
						}
						this.SithSoundCheck();
						if (this.CharacterAnimation[string.Concat(new object[]
						{
							"f02_",
							this.AttackPrefix,
							"Attack",
							this.SithPrefix,
							"_0",
							this.SithCombo
						})].time >= this.CharacterAnimation[string.Concat(new object[]
						{
							"f02_",
							this.AttackPrefix,
							"Attack",
							this.SithPrefix,
							"_0",
							this.SithCombo
						})].length)
						{
							if (this.SithCombo < this.SithComboLength)
							{
								this.SithCombo++;
								this.SithSounds = 0;
								this.SithAttacks = 0;
								this.CharacterAnimation.Play(string.Concat(new object[]
								{
									"f02_",
									this.AttackPrefix,
									"Attack",
									this.SithPrefix,
									"_0",
									this.SithCombo
								}));
							}
							else
							{
								this.CharacterAnimation.Play(string.Concat(new object[]
								{
									"f02_",
									this.AttackPrefix,
									"Recover",
									this.SithPrefix,
									"_0",
									this.SithCombo
								}));
								if (!this.Pod.activeInHierarchy)
								{
									this.CharacterAnimation[string.Concat(new object[]
									{
										"f02_",
										this.AttackPrefix,
										"Recover",
										this.SithPrefix,
										"_0",
										this.SithCombo
									})].speed = 2f;
								}
								else
								{
									this.CharacterAnimation[string.Concat(new object[]
									{
										"f02_",
										this.AttackPrefix,
										"Recover",
										this.SithPrefix,
										"_0",
										this.SithCombo
									})].speed = 0.5f;
								}
								this.SithRecovering = true;
							}
						}
						else
						{
							if (Input.GetButtonDown("X") && this.SithComboLength < this.SithCombo + 1 && this.SithComboLength < 2)
							{
								this.SithComboLength++;
							}
							if (Input.GetButtonDown("Y") && this.SithComboLength < this.SithCombo + 1 && this.SithComboLength < 2)
							{
								this.SithComboLength++;
							}
						}
					}
					else if (this.CharacterAnimation[string.Concat(new object[]
					{
						"f02_",
						this.AttackPrefix,
						"Recover",
						this.SithPrefix,
						"_0",
						this.SithCombo
					})].time >= this.CharacterAnimation[string.Concat(new object[]
					{
						"f02_",
						this.AttackPrefix,
						"Recover",
						this.SithPrefix,
						"_0",
						this.SithCombo
					})].length || this.h + this.v != 0f)
					{
						if (this.SithPrefix == "")
						{
							this.LightSwordParticles.Play();
						}
						else
						{
							this.HeavySwordParticles.Play();
						}
						this.HeavySword.GetComponent<WeaponTrail>().enabled = false;
						this.LightSword.GetComponent<WeaponTrail>().enabled = false;
						this.SithRecovering = false;
						this.SithAttacking = false;
						this.SithComboLength = 0;
						this.SithAttacks = 0;
						this.SithSounds = 0;
						this.SithCombo = 0;
						this.CanMove = true;
					}
				}
				if (this.Eating)
				{
					if (Vector3.Distance(base.transform.position, this.TargetStudent.transform.position) > 0.5f)
					{
						base.transform.Translate(Vector3.forward * Time.deltaTime);
					}
					this.targetRotation = Quaternion.LookRotation(new Vector3(this.TargetStudent.transform.position.x, base.transform.position.y, this.TargetStudent.transform.position.z) - base.transform.position);
					base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.targetRotation, 10f * Time.deltaTime);
					if (this.CharacterAnimation["f02_sixEat_00"].time > this.BloodTimes[this.EatPhase])
					{
						UnityEngine.Object.Instantiate<GameObject>(this.TargetStudent.StabBloodEffect, this.Mouth.position, Quaternion.identity).GetComponent<RandomStabScript>().Biting = true;
						this.Bloodiness += 20f;
						this.EatPhase++;
					}
					if (this.CharacterAnimation["f02_sixEat_00"].time >= this.CharacterAnimation["f02_sixEat_00"].length)
					{
						if (!this.Kagune[0].activeInHierarchy && this.Hunger < 5)
						{
							this.CharacterAnimation["f02_sixRun_00"].speed += 0.1f;
							this.RunSpeed += 1f;
							this.Hunger++;
							if (this.Hunger == 5)
							{
								this.RisingSmoke.SetActive(true);
								this.RunAnim = "f02_sixFastRun_00";
							}
						}
						Debug.Log("Finished eating.");
						this.FollowHips = false;
						this.Attacking = false;
						this.CanMove = true;
						this.Eating = false;
						this.EatPhase = 0;
					}
				}
				if (this.Snapping)
				{
					if (this.SnapPhase == 0)
					{
						if (this.Gazing)
						{
							if (this.CharacterAnimation["f02_gazerSnap_00"].time >= 0.8f)
							{
								AudioSource.PlayClipAtPoint(this.FingerSnap, base.transform.position + Vector3.up);
								this.GazerEyes.ChangeEffect();
								this.SnapPhase++;
							}
						}
						else if (this.WitchMode)
						{
							if (this.CharacterAnimation["f02_fingerSnap_00"].time >= 1f)
							{
								AudioSource.PlayClipAtPoint(this.FingerSnap, base.transform.position + Vector3.up);
								UnityEngine.Object.Instantiate<GameObject>(this.KnifeArray, base.transform.position, base.transform.rotation).GetComponent<KnifeArrayScript>().GlobalKnifeArray = this.GlobalKnifeArray;
								this.SnapPhase++;
							}
						}
						else if (this.ShotsFired < 1)
						{
							if (this.CharacterAnimation["f02_shipGirlSnap_00"].time >= 1f)
							{
								UnityEngine.Object.Instantiate<GameObject>(this.Shell, this.Guns[1].position, base.transform.rotation);
								this.ShotsFired++;
							}
						}
						else if (this.ShotsFired < 2)
						{
							if (this.CharacterAnimation["f02_shipGirlSnap_00"].time >= 1.2f)
							{
								UnityEngine.Object.Instantiate<GameObject>(this.Shell, this.Guns[2].position, base.transform.rotation);
								this.ShotsFired++;
							}
						}
						else if (this.ShotsFired < 3)
						{
							if (this.CharacterAnimation["f02_shipGirlSnap_00"].time >= 1.4f)
							{
								UnityEngine.Object.Instantiate<GameObject>(this.Shell, this.Guns[3].position, base.transform.rotation);
								this.ShotsFired++;
							}
						}
						else if (this.ShotsFired < 4 && this.CharacterAnimation["f02_shipGirlSnap_00"].time >= 1.6f)
						{
							UnityEngine.Object.Instantiate<GameObject>(this.Shell, this.Guns[4].position, base.transform.rotation);
							this.ShotsFired++;
							this.SnapPhase++;
						}
					}
					else if (this.Gazing)
					{
						if (this.CharacterAnimation["f02_gazerSnap_00"].time >= this.CharacterAnimation["f02_gazerSnap_00"].length)
						{
							this.Snapping = false;
							this.CanMove = true;
							this.SnapPhase = 0;
						}
					}
					else if (this.WitchMode)
					{
						if (this.CharacterAnimation["f02_fingerSnap_00"].time >= this.CharacterAnimation["f02_fingerSnap_00"].length)
						{
							this.CharacterAnimation.Stop("f02_fingerSnap_00");
							this.Snapping = false;
							this.CanMove = true;
							this.SnapPhase = 0;
						}
					}
					else if (this.CharacterAnimation["f02_shipGirlSnap_00"].time >= this.CharacterAnimation["f02_shipGirlSnap_00"].length)
					{
						this.Snapping = false;
						this.CanMove = true;
						this.ShotsFired = 0;
						this.SnapPhase = 0;
					}
				}
				if (this.GazeAttacking)
				{
					if (this.TargetStudent != null)
					{
						this.targetRotation = Quaternion.LookRotation(new Vector3(this.TargetStudent.transform.position.x, base.transform.position.y, this.TargetStudent.transform.position.z) - base.transform.position);
						base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.targetRotation, 10f * Time.deltaTime);
					}
					if (this.SnapPhase == 0)
					{
						if (this.CharacterAnimation["f02_gazerPoint_00"].time >= 1f)
						{
							AudioSource.PlayClipAtPoint(this.Zap, base.transform.position + Vector3.up);
							this.GazerEyes.Attack();
							this.SnapPhase++;
						}
					}
					else if (this.CharacterAnimation["f02_gazerPoint_00"].time >= this.CharacterAnimation["f02_gazerPoint_00"].length)
					{
						this.GazerEyes.Attacking = false;
						this.GazeAttacking = false;
						this.CanMove = true;
						this.SnapPhase = 0;
					}
				}
				if (this.Finisher)
				{
					if (this.CharacterAnimation["f02_banchoFinisher_00"].time >= this.CharacterAnimation["f02_banchoFinisher_00"].length)
					{
						this.CharacterAnimation.CrossFade(this.IdleAnim);
						this.Finisher = false;
						this.CanMove = true;
					}
					else if (this.CharacterAnimation["f02_banchoFinisher_00"].time >= 1.6666666f)
					{
						this.BanchoFinisher.MyCollider.enabled = false;
					}
					else if (this.CharacterAnimation["f02_banchoFinisher_00"].time >= 0.8333333f)
					{
						this.BanchoFinisher.MyCollider.enabled = true;
					}
				}
				if (this.ShootingBeam)
				{
					this.CharacterAnimation.CrossFade("f02_LoveLoveBeam_00");
					if (this.CharacterAnimation["f02_LoveLoveBeam_00"].time >= 1.5f && this.BeamPhase == 0)
					{
						UnityEngine.Object.Instantiate<GameObject>(this.LoveLoveBeam, base.transform.position, base.transform.rotation);
						this.BeamPhase++;
					}
					if (this.CharacterAnimation["f02_LoveLoveBeam_00"].time >= this.CharacterAnimation["f02_LoveLoveBeam_00"].length - 1f)
					{
						this.ShootingBeam = false;
						this.YandereTimer = 0f;
						this.CanMove = true;
						this.BeamPhase = 0;
					}
				}
				if (this.WritingName)
				{
					this.CharacterAnimation.CrossFade("f02_dramaticWriting_00");
					if (this.CharacterAnimation["f02_dramaticWriting_00"].time == 0f)
					{
						AudioSource.PlayClipAtPoint(this.DramaticWriting, base.transform.position);
					}
					if (this.CharacterAnimation["f02_dramaticWriting_00"].time >= 5f && this.StudentManager.NoteWindow.TargetStudent > 0)
					{
						this.StudentManager.Students[this.StudentManager.NoteWindow.TargetStudent].Fate = this.StudentManager.NoteWindow.MeetID;
						this.StudentManager.Students[this.StudentManager.NoteWindow.TargetStudent].TimeOfDeath = this.StudentManager.NoteWindow.TimeID;
						this.StudentManager.NoteWindow.TargetStudent = 0;
					}
					if (this.CharacterAnimation["f02_dramaticWriting_00"].time >= this.CharacterAnimation["f02_dramaticWriting_00"].length)
					{
						this.CharacterAnimation[this.CarryAnims[10]].weight = 1f;
						this.CharacterAnimation["f02_dramaticWriting_00"].time = 0f;
						this.CharacterAnimation.Stop("f02_dramaticWriting_00");
						this.WritingName = false;
						this.CanMove = true;
					}
				}
				if (this.StoppingTime)
				{
					this.CharacterAnimation.CrossFade("f02_summonStand_00");
					if (this.CharacterAnimation["f02_summonStand_00"].time >= 1f)
					{
						if (this.Freezing)
						{
							if (!this.InvertSphere.gameObject.activeInHierarchy)
							{
								if (this.MyAudio.clip != this.ClockStop)
								{
									this.MyAudio.clip = this.ClockStop;
									this.MyAudio.volume = 1f;
									this.MyAudio.Play();
								}
								this.InvertSphere.gameObject.SetActive(true);
								this.PlayerOnlyCamera.SetActive(true);
								this.StudentManager.TimeFreeze();
							}
							this.InvertSphere.transform.localScale = Vector3.MoveTowards(this.InvertSphere.transform.localScale, new Vector3(0.2375f, 0.2375f, 0f), Time.deltaTime);
							this.MyAudio.volume = 1f;
							this.Jukebox.Ebola.pitch = Mathf.MoveTowards(this.Jukebox.Ebola.pitch, 0.2f, Time.deltaTime);
						}
						else
						{
							if (this.MyAudio.clip != this.ClockStart)
							{
								this.MyAudio.clip = this.ClockStart;
								this.MyAudio.volume = 1f;
								this.MyAudio.Play();
								this.StudentManager.TimeUnfreeze();
							}
							this.InvertSphere.transform.localScale = Vector3.MoveTowards(this.InvertSphere.transform.localScale, new Vector3(0f, 0f, 0f), Time.deltaTime);
							this.MyAudio.volume = 1f;
							this.Jukebox.Ebola.pitch = Mathf.MoveTowards(this.Jukebox.Ebola.pitch, 1f, Time.deltaTime);
							this.GlobalKnifeArray.ActivateKnives();
						}
					}
					if (this.CharacterAnimation["f02_summonStand_00"].time >= this.CharacterAnimation["f02_summonStand_00"].length)
					{
						this.StoppingTime = false;
						this.CanMove = true;
						this.InvertSphere.gameObject.SetActive(this.Freezing);
						this.PlayerOnlyCamera.SetActive(this.Freezing);
					}
				}
			}
		}
	}

	// Token: 0x06001D3B RID: 7483 RVA: 0x0016664C File Offset: 0x0016484C
	private void UpdatePoisoning()
	{
		if (this.Poisoning)
		{
			if (this.PoisonSpot != null)
			{
				this.MoveTowardsTarget(this.PoisonSpot.position);
				base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.PoisonSpot.rotation, Time.deltaTime * 10f);
			}
			else
			{
				this.targetRotation = Quaternion.LookRotation(new Vector3(this.TargetBento.transform.position.x, base.transform.position.y, this.TargetBento.transform.position.z) - base.transform.position);
				base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.targetRotation, Time.deltaTime * 10f);
				this.MoveTowardsTarget(this.TargetBento.PoisonSpot.position);
			}
			if (this.CharacterAnimation["f02_poisoning_00"].time >= this.CharacterAnimation["f02_poisoning_00"].length)
			{
				this.CharacterAnimation["f02_poisoning_00"].speed = 1f;
				this.PoisonSpot = null;
				this.Poisoning = false;
				this.CanMove = true;
				return;
			}
			if (this.CharacterAnimation["f02_poisoning_00"].time >= 5.25f)
			{
				this.Poisons[this.PoisonType].SetActive(false);
				return;
			}
			if ((double)this.CharacterAnimation["f02_poisoning_00"].time >= 0.75)
			{
				this.Poisons[this.PoisonType].SetActive(true);
			}
		}
	}

	// Token: 0x06001D3C RID: 7484 RVA: 0x00166818 File Offset: 0x00164A18
	private void UpdateEffects()
	{
		if (!this.Attacking && !this.DelinquentFighting && !this.Lost && this.CanMove)
		{
			if (Vector3.Distance(base.transform.position, this.Senpai.position) < 1f)
			{
				if (!this.Talking)
				{
					if (!this.NearSenpai && this.StudentManager.Students[1].Pathfinding.speed < 7.5f)
					{
						this.StudentManager.TutorialWindow.ShowSenpaiMessage = true;
						this.DepthOfField.focalSize = 150f;
						this.NearSenpai = true;
					}
					if (this.Laughing)
					{
						this.StopLaughing();
					}
					this.Stance.Current = StanceType.Standing;
					this.Obscurance.enabled = false;
					this.YandereVision = false;
					this.Mopping = false;
					this.Uncrouch();
					this.YandereTimer = 0f;
					this.EmptyHands();
					if (this.Aiming)
					{
						this.StopAiming();
					}
				}
			}
			else
			{
				this.NearSenpai = false;
			}
		}
		if (this.NearSenpai && !this.Noticed)
		{
			this.DepthOfField.enabled = true;
			this.DepthOfField.focalSize = Mathf.Lerp(this.DepthOfField.focalSize, 0f, Time.deltaTime * 10f);
			this.DepthOfField.focalZStartCurve = Mathf.Lerp(this.DepthOfField.focalZStartCurve, 20f, Time.deltaTime * 10f);
			this.DepthOfField.focalZEndCurve = Mathf.Lerp(this.DepthOfField.focalZEndCurve, 20f, Time.deltaTime * 10f);
			this.DepthOfField.objectFocus = this.Senpai.transform;
			this.ColorCorrection.enabled = true;
			this.SenpaiFade = Mathf.Lerp(this.SenpaiFade, 0f, Time.deltaTime * 10f);
			this.SenpaiTint = 1f - this.SenpaiFade / 100f;
			this.ColorCorrection.redChannel.MoveKey(1, new Keyframe(0.5f, 0.5f + this.SenpaiTint * 0.5f));
			this.ColorCorrection.greenChannel.MoveKey(1, new Keyframe(0.5f, 1f - this.SenpaiTint * 0.5f));
			this.ColorCorrection.blueChannel.MoveKey(1, new Keyframe(0.5f, 0.5f + this.SenpaiTint * 0.5f));
			this.ColorCorrection.redChannel.SmoothTangents(1, 0f);
			this.ColorCorrection.greenChannel.SmoothTangents(1, 0f);
			this.ColorCorrection.blueChannel.SmoothTangents(1, 0f);
			this.ColorCorrection.UpdateTextures();
			bool attacking = this.Attacking;
			this.SelectGrayscale.desaturation = Mathf.Lerp(this.SelectGrayscale.desaturation, 0f, Time.deltaTime * 10f);
			this.HeartBeat.volume = this.SenpaiTint;
			this.Sanity += Time.deltaTime * 10f;
			this.SenpaiTimer += Time.deltaTime;
			this.BeatTimer += Time.deltaTime;
			if (this.BeatTimer > 60f / (float)this.HeartRate.BeatsPerMinute)
			{
				GamePad.SetVibration(PlayerIndex.One, 1f, 1f);
				this.VibrationCheck = true;
				this.VibrationTimer = 0.1f;
				this.BeatTimer = 0f;
			}
			if (this.SenpaiTimer > 10f && this.Creepiness < 5)
			{
				this.SenpaiTimer = 0f;
				this.Creepiness++;
			}
		}
		else if (this.SenpaiFade < 99f)
		{
			this.DepthOfField.focalSize = Mathf.Lerp(this.DepthOfField.focalSize, 150f, Time.deltaTime * 10f);
			this.DepthOfField.focalZStartCurve = Mathf.Lerp(this.DepthOfField.focalZStartCurve, 0f, Time.deltaTime * 10f);
			this.DepthOfField.focalZEndCurve = Mathf.Lerp(this.DepthOfField.focalZEndCurve, 0f, Time.deltaTime * 10f);
			this.SenpaiFade = Mathf.Lerp(this.SenpaiFade, 100f, Time.deltaTime * 10f);
			this.SenpaiTint = this.SenpaiFade / 100f;
			this.ColorCorrection.redChannel.MoveKey(1, new Keyframe(0.5f, 1f - this.SenpaiTint * 0.5f));
			this.ColorCorrection.greenChannel.MoveKey(1, new Keyframe(0.5f, this.SenpaiTint * 0.5f));
			this.ColorCorrection.blueChannel.MoveKey(1, new Keyframe(0.5f, 1f - this.SenpaiTint * 0.5f));
			this.ColorCorrection.redChannel.SmoothTangents(1, 0f);
			this.ColorCorrection.greenChannel.SmoothTangents(1, 0f);
			this.ColorCorrection.blueChannel.SmoothTangents(1, 0f);
			this.ColorCorrection.UpdateTextures();
			this.SelectGrayscale.desaturation = Mathf.Lerp(this.SelectGrayscale.desaturation, this.GreyTarget, Time.deltaTime * 10f);
			this.CharacterAnimation["f02_shy_00"].weight = 1f - this.SenpaiTint;
			for (int i = 1; i < 6; i++)
			{
				this.CharacterAnimation[this.CreepyIdles[i]].weight = Mathf.MoveTowards(this.CharacterAnimation[this.CreepyIdles[i]].weight, 0f, Time.deltaTime * 10f);
				this.CharacterAnimation[this.CreepyWalks[i]].weight = Mathf.MoveTowards(this.CharacterAnimation[this.CreepyWalks[i]].weight, 0f, Time.deltaTime * 10f);
			}
			this.HeartBeat.volume = 1f - this.SenpaiTint;
		}
		else if (this.SenpaiFade < 100f)
		{
			this.ResetSenpaiEffects();
		}
		if (this.YandereVision)
		{
			if (!this.HighlightingR.enabled)
			{
				this.YandereColorCorrection.enabled = true;
				this.HighlightingR.enabled = true;
				this.HighlightingB.enabled = true;
				this.Obscurance.enabled = true;
				this.Vignette.enabled = true;
			}
			Time.timeScale = Mathf.Lerp(Time.timeScale, 0.5f, Time.unscaledDeltaTime * 10f);
			this.YandereFade = Mathf.Lerp(this.YandereFade, 0f, Time.deltaTime * 10f);
			this.YandereTint = 1f - this.YandereFade / 100f;
			this.YandereColorCorrection.redChannel.MoveKey(1, new Keyframe(0.5f, 0.5f - this.YandereTint * 0.25f));
			this.YandereColorCorrection.greenChannel.MoveKey(1, new Keyframe(0.5f, 0.5f - this.YandereTint * 0.25f));
			this.YandereColorCorrection.blueChannel.MoveKey(1, new Keyframe(0.5f, 0.5f + this.YandereTint * 0.25f));
			this.YandereColorCorrection.redChannel.SmoothTangents(1, 0f);
			this.YandereColorCorrection.greenChannel.SmoothTangents(1, 0f);
			this.YandereColorCorrection.blueChannel.SmoothTangents(1, 0f);
			this.YandereColorCorrection.UpdateTextures();
			this.Vignette.intensity = Mathf.Lerp(this.Vignette.intensity, this.YandereTint * 5f, Time.deltaTime * 10f);
			this.Vignette.blur = Mathf.Lerp(this.Vignette.blur, this.YandereTint, Time.deltaTime * 10f);
			this.Vignette.chromaticAberration = Mathf.Lerp(this.Vignette.chromaticAberration, this.YandereTint * 5f, Time.deltaTime * 10f);
			if (this.StudentManager.Tag.Target != null)
			{
				this.StudentManager.Tag.Sprite.color = new Color(1f, 0f, 0f, Mathf.Lerp(this.StudentManager.Tag.Sprite.color.a, 1f, Time.unscaledDeltaTime * 10f));
			}
			if (this.StudentManager.Students[this.StudentManager.RivalID] != null)
			{
				this.StudentManager.RedString.gameObject.SetActive(true);
			}
		}
		else
		{
			if (this.HighlightingR.enabled)
			{
				this.HighlightingR.enabled = false;
				this.HighlightingB.enabled = false;
				this.Obscurance.enabled = false;
			}
			if (this.YandereFade < 99f)
			{
				if (!this.Aiming)
				{
					Time.timeScale = Mathf.Lerp(Time.timeScale, 1f, Time.unscaledDeltaTime * 10f);
				}
				this.YandereFade = Mathf.Lerp(this.YandereFade, 100f, Time.deltaTime * 10f);
				this.YandereTint = this.YandereFade / 100f;
				this.YandereColorCorrection.redChannel.MoveKey(1, new Keyframe(0.5f, this.YandereTint * 0.5f));
				this.YandereColorCorrection.greenChannel.MoveKey(1, new Keyframe(0.5f, this.YandereTint * 0.5f));
				this.YandereColorCorrection.blueChannel.MoveKey(1, new Keyframe(0.5f, 1f - this.YandereTint * 0.5f));
				this.YandereColorCorrection.redChannel.SmoothTangents(1, 0f);
				this.YandereColorCorrection.greenChannel.SmoothTangents(1, 0f);
				this.YandereColorCorrection.blueChannel.SmoothTangents(1, 0f);
				this.YandereColorCorrection.UpdateTextures();
				this.Vignette.intensity = Mathf.Lerp(this.Vignette.intensity, 0f, Time.deltaTime * 10f);
				this.Vignette.blur = Mathf.Lerp(this.Vignette.blur, 0f, Time.deltaTime * 10f);
				this.Vignette.chromaticAberration = Mathf.Lerp(this.Vignette.chromaticAberration, 0f, Time.deltaTime * 10f);
				this.StudentManager.Tag.Sprite.color = new Color(1f, 0f, 0f, Mathf.Lerp(this.StudentManager.Tag.Sprite.color.a, 0f, Time.unscaledDeltaTime * 10f));
				this.StudentManager.RedString.gameObject.SetActive(false);
			}
			else if (this.YandereFade < 100f)
			{
				this.ResetYandereEffects();
			}
		}
		this.RightRedEye.material.color = new Color(this.RightRedEye.material.color.r, this.RightRedEye.material.color.g, this.RightRedEye.material.color.b, 1f - this.YandereFade / 100f);
		this.LeftRedEye.material.color = new Color(this.LeftRedEye.material.color.r, this.LeftRedEye.material.color.g, this.LeftRedEye.material.color.b, 1f - this.YandereFade / 100f);
		this.RightYandereEye.material.color = new Color(this.RightYandereEye.material.color.r, this.YandereFade / 100f, this.YandereFade / 100f, this.RightYandereEye.material.color.a);
		this.LeftYandereEye.material.color = new Color(this.LeftYandereEye.material.color.r, this.YandereFade / 100f, this.YandereFade / 100f, this.LeftYandereEye.material.color.a);
	}

	// Token: 0x06001D3D RID: 7485 RVA: 0x00167554 File Offset: 0x00165754
	private void UpdateTalking()
	{
		if (this.Talking)
		{
			if (this.TargetStudent != null)
			{
				this.targetRotation = Quaternion.LookRotation(new Vector3(this.TargetStudent.transform.position.x, base.transform.position.y, this.TargetStudent.transform.position.z) - base.transform.position);
				base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.targetRotation, Time.deltaTime * 10f);
				if (Vector3.Distance(base.transform.position, this.TargetStudent.transform.position) < 0.75f)
				{
					this.MyController.Move(base.transform.forward * Time.deltaTime * -1f);
				}
			}
			if (this.Interaction == YandereInteractionType.Idle)
			{
				if (this.TargetStudent != null && !this.TargetStudent.Counselor)
				{
					this.CharacterAnimation.CrossFade(this.IdleAnim);
					return;
				}
			}
			else
			{
				if (this.Interaction == YandereInteractionType.Apologizing)
				{
					if (this.TalkTimer == 3f)
					{
						this.CharacterAnimation.CrossFade("f02_greet_00");
						if (this.TargetStudent.Witnessed == StudentWitnessType.Insanity || this.TargetStudent.Witnessed == StudentWitnessType.WeaponAndBloodAndInsanity || this.TargetStudent.Witnessed == StudentWitnessType.WeaponAndInsanity || this.TargetStudent.Witnessed == StudentWitnessType.BloodAndInsanity)
						{
							this.Subtitle.UpdateLabel(SubtitleType.InsanityApology, 0, 3f);
						}
						else if (this.TargetStudent.Witnessed == StudentWitnessType.WeaponAndBlood)
						{
							this.Subtitle.UpdateLabel(SubtitleType.WeaponAndBloodApology, 0, 3f);
						}
						else if (this.TargetStudent.Witnessed == StudentWitnessType.Weapon)
						{
							this.Subtitle.UpdateLabel(SubtitleType.WeaponApology, 0, 3f);
						}
						else if (this.TargetStudent.Witnessed == StudentWitnessType.Blood)
						{
							this.Subtitle.UpdateLabel(SubtitleType.BloodApology, 0, 3f);
						}
						else if (this.TargetStudent.Witnessed == StudentWitnessType.Lewd)
						{
							this.Subtitle.UpdateLabel(SubtitleType.LewdApology, 0, 3f);
						}
						else if (this.TargetStudent.Witnessed == StudentWitnessType.Accident)
						{
							this.Subtitle.UpdateLabel(SubtitleType.AccidentApology, 0, 3f);
						}
						else if (this.TargetStudent.Witnessed == StudentWitnessType.Suspicious)
						{
							this.Subtitle.UpdateLabel(SubtitleType.SuspiciousApology, 0, 3f);
						}
						else if (this.TargetStudent.Witnessed == StudentWitnessType.Eavesdropping)
						{
							this.Subtitle.UpdateLabel(SubtitleType.EavesdropApology, 0, 3f);
						}
						else if (this.TargetStudent.Witnessed == StudentWitnessType.Theft)
						{
							this.Subtitle.UpdateLabel(SubtitleType.TheftApology, 0, 3f);
						}
						else if (this.TargetStudent.Witnessed == StudentWitnessType.Violence)
						{
							this.Subtitle.UpdateLabel(SubtitleType.ViolenceApology, 0, 3f);
						}
						else if (this.TargetStudent.Witnessed == StudentWitnessType.Pickpocketing)
						{
							this.Subtitle.UpdateLabel(SubtitleType.PickpocketApology, 0, 3f);
						}
						else if (this.TargetStudent.Witnessed == StudentWitnessType.CleaningItem)
						{
							this.Subtitle.UpdateLabel(SubtitleType.CleaningApology, 0, 3f);
						}
						else if (this.TargetStudent.Witnessed == StudentWitnessType.Poisoning)
						{
							this.Subtitle.UpdateLabel(SubtitleType.PoisonApology, 0, 3f);
						}
						else if (this.TargetStudent.Witnessed == StudentWitnessType.HoldingBloodyClothing)
						{
							this.Subtitle.UpdateLabel(SubtitleType.HoldingBloodyClothingApology, 0, 3f);
						}
					}
					else
					{
						if (Input.GetButtonDown("A"))
						{
							this.TalkTimer = 0f;
						}
						if (this.CharacterAnimation["f02_greet_00"].time >= this.CharacterAnimation["f02_greet_00"].length)
						{
							this.CharacterAnimation.CrossFade(this.IdleAnim);
						}
						if (this.TalkTimer <= 0f)
						{
							this.TargetStudent.Interaction = StudentInteractionType.Forgiving;
							this.TargetStudent.TalkTimer = 3f;
							this.Interaction = YandereInteractionType.Idle;
						}
					}
					this.TalkTimer -= Time.deltaTime;
					return;
				}
				if (this.Interaction == YandereInteractionType.Compliment)
				{
					if (this.TalkTimer == 3f)
					{
						this.CharacterAnimation.CrossFade("f02_greet_01");
						if (!this.TargetStudent.Male)
						{
							this.Subtitle.UpdateLabel(SubtitleType.PlayerCompliment, 0, 3f);
						}
						else
						{
							this.Subtitle.UpdateLabel(SubtitleType.PlayerCompliment, 1, 3f);
						}
					}
					else
					{
						if (Input.GetButtonDown("A"))
						{
							this.TalkTimer = 0f;
						}
						if (this.CharacterAnimation["f02_greet_01"].time >= this.CharacterAnimation["f02_greet_01"].length)
						{
							this.CharacterAnimation.CrossFade(this.IdleAnim);
						}
						if (this.TalkTimer <= 0f)
						{
							this.TargetStudent.Interaction = StudentInteractionType.ReceivingCompliment;
							this.TargetStudent.TalkTimer = 3f;
							this.Interaction = YandereInteractionType.Idle;
						}
					}
					this.TalkTimer -= Time.deltaTime;
					return;
				}
				if (this.Interaction == YandereInteractionType.Gossip)
				{
					if (this.TalkTimer == 3f)
					{
						this.CharacterAnimation.CrossFade("f02_lookdown_00");
						this.Subtitle.UpdateLabel(SubtitleType.PlayerGossip, 0, 3f);
					}
					else
					{
						if (Input.GetButtonDown("A"))
						{
							this.TalkTimer = 0f;
						}
						if (this.CharacterAnimation["f02_lookdown_00"].time >= this.CharacterAnimation["f02_lookdown_00"].length)
						{
							this.CharacterAnimation.CrossFade(this.IdleAnim);
						}
						if (this.TalkTimer <= 0f)
						{
							this.TargetStudent.Interaction = StudentInteractionType.Gossiping;
							this.TargetStudent.TalkTimer = 3f;
							this.Interaction = YandereInteractionType.Idle;
						}
					}
					this.TalkTimer -= Time.deltaTime;
					return;
				}
				if (this.Interaction == YandereInteractionType.Bye)
				{
					if (this.TalkTimer == 2f)
					{
						this.CharacterAnimation.CrossFade("f02_greet_00");
						this.Subtitle.UpdateLabel(SubtitleType.PlayerFarewell, 0, 2f);
					}
					else
					{
						if (Input.GetButtonDown("A"))
						{
							this.TalkTimer = 0f;
						}
						if (this.CharacterAnimation["f02_greet_00"].time >= this.CharacterAnimation["f02_greet_00"].length)
						{
							this.CharacterAnimation.CrossFade(this.IdleAnim);
						}
						if (this.TalkTimer <= 0f)
						{
							this.TargetStudent.Interaction = StudentInteractionType.Bye;
							this.TargetStudent.TalkTimer = 2f;
							this.Interaction = YandereInteractionType.Idle;
						}
					}
					this.TalkTimer -= Time.deltaTime;
					return;
				}
				if (this.Interaction == YandereInteractionType.FollowMe)
				{
					int num = 0;
					if (ClubGlobals.Club == ClubType.Delinquent)
					{
						num++;
					}
					if (this.TalkTimer == 3f)
					{
						if (ClubGlobals.Club == ClubType.Delinquent)
						{
							this.TalkAnim = "f02_delinquentGesture_01";
						}
						else
						{
							this.TalkAnim = "f02_greet_01";
						}
						this.CharacterAnimation.CrossFade(this.TalkAnim);
						this.Subtitle.UpdateLabel(SubtitleType.PlayerFollow, num, 3f);
					}
					else
					{
						if (Input.GetButtonDown("A"))
						{
							this.TalkTimer = 0f;
						}
						if (this.CharacterAnimation[this.TalkAnim].time >= this.CharacterAnimation[this.TalkAnim].length)
						{
							this.CharacterAnimation.CrossFade(this.IdleAnim);
						}
						if (this.TalkTimer <= 0f)
						{
							this.TargetStudent.Interaction = StudentInteractionType.FollowingPlayer;
							this.TargetStudent.TalkTimer = 2f;
							this.Interaction = YandereInteractionType.Idle;
						}
					}
					this.TalkTimer -= Time.deltaTime;
					return;
				}
				if (this.Interaction == YandereInteractionType.GoAway)
				{
					int num2 = 0;
					if (ClubGlobals.Club == ClubType.Delinquent)
					{
						num2++;
					}
					if (this.TalkTimer == 3f)
					{
						if (ClubGlobals.Club == ClubType.Delinquent)
						{
							this.TalkAnim = "f02_delinquentGesture_01";
						}
						else
						{
							this.TalkAnim = "f02_lookdown_00";
						}
						this.CharacterAnimation.CrossFade(this.TalkAnim);
						this.Subtitle.UpdateLabel(SubtitleType.PlayerLeave, num2, 3f);
					}
					else
					{
						if (Input.GetButtonDown("A"))
						{
							this.TalkTimer = 0f;
						}
						if (this.CharacterAnimation[this.TalkAnim].time >= this.CharacterAnimation[this.TalkAnim].length)
						{
							this.CharacterAnimation.CrossFade(this.IdleAnim);
						}
						if (this.TalkTimer <= 0f)
						{
							this.TargetStudent.Interaction = StudentInteractionType.GoingAway;
							this.TargetStudent.TalkTimer = 3f;
							this.Interaction = YandereInteractionType.Idle;
						}
					}
					this.TalkTimer -= Time.deltaTime;
					return;
				}
				if (this.Interaction == YandereInteractionType.DistractThem)
				{
					int num3 = 0;
					if (ClubGlobals.Club == ClubType.Delinquent)
					{
						num3++;
					}
					if (this.TalkTimer == 3f)
					{
						if (ClubGlobals.Club == ClubType.Delinquent)
						{
							this.TalkAnim = "f02_delinquentGesture_01";
						}
						else
						{
							this.TalkAnim = "f02_lookdown_00";
						}
						this.CharacterAnimation.CrossFade(this.TalkAnim);
						this.Subtitle.UpdateLabel(SubtitleType.PlayerDistract, num3, 3f);
					}
					else
					{
						if (Input.GetButtonDown("A"))
						{
							this.TalkTimer = 0f;
						}
						if (this.CharacterAnimation[this.TalkAnim].time >= this.CharacterAnimation[this.TalkAnim].length)
						{
							this.CharacterAnimation.CrossFade(this.IdleAnim);
						}
						if (this.TalkTimer <= 0f)
						{
							this.TargetStudent.Interaction = StudentInteractionType.DistractingTarget;
							this.TargetStudent.TalkTimer = 3f;
							this.Interaction = YandereInteractionType.Idle;
						}
					}
					this.TalkTimer -= Time.deltaTime;
					return;
				}
				if (this.Interaction == YandereInteractionType.NamingCrush)
				{
					if (this.TalkTimer == 3f)
					{
						this.CharacterAnimation.CrossFade("f02_greet_01");
						this.Subtitle.UpdateLabel(SubtitleType.PlayerLove, 0, 3f);
					}
					else
					{
						if (Input.GetButtonDown("A"))
						{
							this.TalkTimer = 0f;
						}
						if (this.CharacterAnimation["f02_greet_01"].time >= this.CharacterAnimation["f02_greet_01"].length)
						{
							this.CharacterAnimation.CrossFade(this.IdleAnim);
						}
						if (this.TalkTimer <= 0f)
						{
							this.TargetStudent.Interaction = StudentInteractionType.NamingCrush;
							this.TargetStudent.TalkTimer = 3f;
							this.Interaction = YandereInteractionType.Idle;
						}
					}
					this.TalkTimer -= Time.deltaTime;
					return;
				}
				if (this.Interaction == YandereInteractionType.ChangingAppearance)
				{
					if (this.TalkTimer == 3f)
					{
						this.CharacterAnimation.CrossFade("f02_greet_01");
						this.Subtitle.UpdateLabel(SubtitleType.PlayerLove, 2, 3f);
					}
					else
					{
						if (Input.GetButtonDown("A"))
						{
							this.TalkTimer = 0f;
						}
						if (this.CharacterAnimation["f02_greet_01"].time >= this.CharacterAnimation["f02_greet_01"].length)
						{
							this.CharacterAnimation.CrossFade(this.IdleAnim);
						}
						if (this.TalkTimer <= 0f)
						{
							this.TargetStudent.Interaction = StudentInteractionType.ChangingAppearance;
							this.TargetStudent.TalkTimer = 3f;
							this.Interaction = YandereInteractionType.Idle;
						}
					}
					this.TalkTimer -= Time.deltaTime;
					return;
				}
				if (this.Interaction == YandereInteractionType.Court)
				{
					if (this.TalkTimer == 5f)
					{
						this.CharacterAnimation.CrossFade("f02_greet_01");
						if (!this.TargetStudent.Male)
						{
							this.Subtitle.UpdateLabel(SubtitleType.PlayerLove, 3, 5f);
						}
						else
						{
							this.Subtitle.UpdateLabel(SubtitleType.PlayerLove, 4, 5f);
						}
					}
					else
					{
						if (Input.GetButtonDown("A"))
						{
							this.TalkTimer = 0f;
						}
						if (this.CharacterAnimation["f02_greet_01"].time >= this.CharacterAnimation["f02_greet_01"].length)
						{
							this.CharacterAnimation.CrossFade(this.IdleAnim);
						}
						if (this.TalkTimer <= 0f)
						{
							this.TargetStudent.Interaction = StudentInteractionType.Court;
							this.TargetStudent.TalkTimer = 3f;
							this.Interaction = YandereInteractionType.Idle;
						}
					}
					this.TalkTimer -= Time.deltaTime;
					return;
				}
				if (this.Interaction == YandereInteractionType.Confess)
				{
					if (this.TalkTimer == 5f)
					{
						this.CharacterAnimation.CrossFade("f02_greet_01");
						this.Subtitle.UpdateLabel(SubtitleType.PlayerLove, 5, 5f);
					}
					else
					{
						if (Input.GetButtonDown("A"))
						{
							this.TalkTimer = 0f;
						}
						if (this.CharacterAnimation["f02_greet_01"].time >= this.CharacterAnimation["f02_greet_01"].length)
						{
							this.CharacterAnimation.CrossFade(this.IdleAnim);
						}
						if (this.TalkTimer <= 0f)
						{
							this.TargetStudent.Interaction = StudentInteractionType.Gift;
							this.TargetStudent.TalkTimer = 5f;
							this.Interaction = YandereInteractionType.Idle;
						}
					}
					this.TalkTimer -= Time.deltaTime;
					return;
				}
				if (this.Interaction == YandereInteractionType.TaskInquiry)
				{
					if (this.TalkTimer == 3f)
					{
						this.CharacterAnimation.CrossFade("f02_greet_01");
						this.Subtitle.UpdateLabel(SubtitleType.TaskInquiry, 0, 5f);
					}
					else
					{
						if (Input.GetButtonDown("A"))
						{
							this.TalkTimer = 0f;
						}
						if (this.CharacterAnimation["f02_greet_01"].time >= this.CharacterAnimation["f02_greet_01"].length)
						{
							this.CharacterAnimation.CrossFade(this.IdleAnim);
						}
						if (this.TalkTimer <= 0f)
						{
							this.TargetStudent.Interaction = StudentInteractionType.TaskInquiry;
							this.TargetStudent.TalkTimer = 10f;
							this.Interaction = YandereInteractionType.Idle;
						}
					}
					this.TalkTimer -= Time.deltaTime;
					return;
				}
				if (this.Interaction == YandereInteractionType.GivingSnack)
				{
					if (this.TalkTimer == 3f)
					{
						this.CharacterAnimation.CrossFade("f02_greet_01");
						this.Subtitle.UpdateLabel(SubtitleType.OfferSnack, 0, 3f);
					}
					else
					{
						if (Input.GetButtonDown("A"))
						{
							this.TalkTimer = 0f;
						}
						if (this.CharacterAnimation["f02_greet_01"].time >= this.CharacterAnimation["f02_greet_01"].length)
						{
							this.CharacterAnimation.CrossFade(this.IdleAnim);
						}
						if (this.TalkTimer <= 0f)
						{
							this.TargetStudent.Interaction = StudentInteractionType.TakingSnack;
							this.TargetStudent.TalkTimer = 5f;
							this.Interaction = YandereInteractionType.Idle;
						}
					}
					this.TalkTimer -= Time.deltaTime;
					return;
				}
				if (this.Interaction == YandereInteractionType.AskingForHelp)
				{
					if (this.TalkTimer == 5f)
					{
						this.CharacterAnimation.CrossFade(this.IdleAnim);
						this.Subtitle.UpdateLabel(SubtitleType.AskForHelp, 0, 5f);
					}
					else
					{
						if (Input.GetButtonDown("A"))
						{
							this.TalkTimer = 0f;
						}
						if (this.TalkTimer <= 0f)
						{
							this.TargetStudent.Interaction = StudentInteractionType.GivingHelp;
							this.TargetStudent.TalkTimer = 4f;
							this.Interaction = YandereInteractionType.Idle;
						}
					}
					this.TalkTimer -= Time.deltaTime;
					return;
				}
				if (this.Interaction == YandereInteractionType.SendingToLocker)
				{
					if (this.TalkTimer == 5f)
					{
						this.CharacterAnimation.CrossFade("f02_greet_01");
						this.Subtitle.UpdateLabel(SubtitleType.SendToLocker, 0, 5f);
					}
					else
					{
						if (Input.GetButtonDown("A"))
						{
							this.TalkTimer = 0f;
						}
						if (this.TalkTimer <= 0f)
						{
							this.TargetStudent.Interaction = StudentInteractionType.SentToLocker;
							this.TargetStudent.TalkTimer = 5f;
							this.Interaction = YandereInteractionType.Idle;
						}
					}
					this.TalkTimer -= Time.deltaTime;
				}
			}
		}
	}

	// Token: 0x06001D3E RID: 7486 RVA: 0x00168620 File Offset: 0x00166820
	private void UpdateAttacking()
	{
		if (this.Attacking)
		{
			if (this.TargetStudent != null)
			{
				this.targetRotation = Quaternion.LookRotation(new Vector3(this.TargetStudent.transform.position.x, base.transform.position.y, this.TargetStudent.transform.position.z) - base.transform.position);
				base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.targetRotation, Time.deltaTime * 10f);
			}
			if (this.Drown)
			{
				this.MoveTowardsTarget(this.TargetStudent.transform.position + this.TargetStudent.transform.forward * -0.0001f);
				this.CharacterAnimation.CrossFade(this.DrownAnim);
				if (this.CharacterAnimation[this.DrownAnim].time > this.CharacterAnimation[this.DrownAnim].length)
				{
					this.TargetStudent.DeathType = DeathType.Drowning;
					this.Attacking = false;
					this.CanMove = true;
					this.Drown = false;
					this.Sanity -= ((PlayerGlobals.PantiesEquipped == 10) ? 10f : 20f) * this.Numbness;
					return;
				}
			}
			else if (this.RoofPush)
			{
				this.CameraTarget.position = Vector3.MoveTowards(this.CameraTarget.position, new Vector3(this.Hips.position.x, base.transform.position.y + 1f, this.Hips.position.z), Time.deltaTime * 10f);
				this.MoveTowardsTarget(this.TargetStudent.transform.position - this.TargetStudent.transform.forward);
				this.CharacterAnimation.CrossFade("f02_roofPushA_00");
				if (this.CharacterAnimation["f02_roofPushA_00"].time > 4.3333335f)
				{
					if (this.Shoes[0].activeInHierarchy)
					{
						UnityEngine.Object.Instantiate<GameObject>(this.ShoePair, base.transform.position + new Vector3(0f, 0.045f, 0f) + base.transform.forward * 1.6f, Quaternion.identity).transform.eulerAngles = base.transform.eulerAngles;
						this.Shoes[0].SetActive(false);
						this.Shoes[1].SetActive(false);
					}
				}
				else if (this.CharacterAnimation["f02_roofPushA_00"].time > 2.1666667f && this.TargetStudent.Schoolwear == 1 && !this.TargetStudent.ClubAttire && !this.Shoes[0].activeInHierarchy)
				{
					this.TargetStudent.RemoveShoes();
					this.Shoes[0].SetActive(true);
					this.Shoes[1].SetActive(true);
				}
				float num;
				if (this.TargetStudent.Schoolwear == 1 && !this.TargetStudent.ClubAttire)
				{
					num = this.CharacterAnimation["f02_roofPushA_00"].length;
				}
				else
				{
					num = 3.5f;
				}
				if (this.CharacterAnimation["f02_roofPushA_00"].time > num)
				{
					this.CameraTarget.localPosition = new Vector3(0f, 1f, 0f);
					this.TargetStudent.DeathType = DeathType.Falling;
					this.SplashCamera.transform.parent = null;
					this.Attacking = false;
					this.RoofPush = false;
					this.CanMove = true;
					this.Sanity -= 20f * this.Numbness;
				}
				if (Input.GetButtonDown("B"))
				{
					this.SplashCamera.transform.parent = base.transform;
					this.SplashCamera.transform.localPosition = new Vector3(2f, -10.65f, 4.775f);
					this.SplashCamera.transform.localEulerAngles = new Vector3(0f, -135f, 0f);
					this.SplashCamera.Show = true;
					this.SplashCamera.MyCamera.enabled = true;
					return;
				}
			}
			else
			{
				if (this.TargetStudent.Teacher)
				{
					this.CharacterAnimation.CrossFade("f02_teacherCounterA_00");
					if (this.EquippedWeapon != null)
					{
						this.EquippedWeapon.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
					}
					this.Character.transform.position = new Vector3(this.Character.transform.position.x, this.TargetStudent.transform.position.y, this.Character.transform.position.z);
					return;
				}
				if (!this.SanityBased)
				{
					if (this.EquippedWeapon.WeaponID == 11)
					{
						this.CharacterAnimation.CrossFade("CyborgNinja_Slash");
						if (this.CharacterAnimation["CyborgNinja_Slash"].time == 0f)
						{
							this.TargetStudent.CharacterAnimation[this.TargetStudent.PhoneAnim].weight = 0f;
							this.EquippedWeapon.gameObject.GetComponent<AudioSource>().Play();
						}
						if (this.CharacterAnimation["CyborgNinja_Slash"].time >= this.CharacterAnimation["CyborgNinja_Slash"].length)
						{
							this.Bloodiness += 20f;
							this.StainWeapon();
							this.CharacterAnimation["CyborgNinja_Slash"].time = 0f;
							this.CharacterAnimation.Stop("CyborgNinja_Slash");
							this.CharacterAnimation.CrossFade(this.IdleAnim);
							this.Attacking = false;
							if (!this.Noticed)
							{
								this.CanMove = true;
								return;
							}
							this.EquippedWeapon.Drop();
							return;
						}
					}
					else if (this.EquippedWeapon.WeaponID == 7)
					{
						this.CharacterAnimation.CrossFade("f02_buzzSawKill_A_00");
						if (this.CharacterAnimation["f02_buzzSawKill_A_00"].time == 0f)
						{
							this.TargetStudent.CharacterAnimation[this.TargetStudent.PhoneAnim].weight = 0f;
							this.EquippedWeapon.gameObject.GetComponent<AudioSource>().Play();
						}
						if (this.AttackPhase == 1)
						{
							if (this.CharacterAnimation["f02_buzzSawKill_A_00"].time > 0.33333334f)
							{
								this.TargetStudent.LiquidProjector.enabled = true;
								this.EquippedWeapon.Effect();
								this.StainWeapon();
								this.TargetStudent.LiquidProjector.material.mainTexture = this.BloodTextures[1];
								this.Bloodiness += 20f;
								this.AttackPhase++;
							}
						}
						else if (this.AttackPhase < 6 && this.CharacterAnimation["f02_buzzSawKill_A_00"].time > 0.33333334f * (float)this.AttackPhase)
						{
							this.TargetStudent.LiquidProjector.material.mainTexture = this.BloodTextures[this.AttackPhase];
							this.Bloodiness += 20f;
							this.AttackPhase++;
						}
						if (this.CharacterAnimation["f02_buzzSawKill_A_00"].time > this.CharacterAnimation["f02_buzzSawKill_A_00"].length)
						{
							if (this.TargetStudent == this.StudentManager.Reporter)
							{
								this.StudentManager.Reporter = null;
							}
							this.CharacterAnimation["f02_buzzSawKill_A_00"].time = 0f;
							this.CharacterAnimation.Stop("f02_buzzSawKill_A_00");
							this.CharacterAnimation.CrossFade(this.IdleAnim);
							this.MyController.radius = 0.2f;
							this.Attacking = false;
							this.AttackPhase = 1;
							this.Sanity -= 20f * this.Numbness;
							this.TargetStudent.DeathType = DeathType.Weapon;
							this.TargetStudent.BecomeRagdoll();
							if (!this.Noticed)
							{
								this.CanMove = true;
								return;
							}
							this.EquippedWeapon.Drop();
							return;
						}
					}
					else if (!this.EquippedWeapon.Concealable)
					{
						if (this.AttackPhase == 1)
						{
							this.CharacterAnimation.CrossFade("f02_swingA_00");
							if (this.CharacterAnimation["f02_swingA_00"].time > this.CharacterAnimation["f02_swingA_00"].length * 0.3f)
							{
								if (this.TargetStudent == this.StudentManager.Reporter)
								{
									this.StudentManager.Reporter = null;
								}
								UnityEngine.Object.Destroy(this.TargetStudent.DeathScream);
								this.EquippedWeapon.Effect();
								this.AttackPhase = 2;
								this.Bloodiness += 20f;
								this.StainWeapon();
								this.Sanity -= 20f * this.Numbness;
								return;
							}
						}
						else if (this.CharacterAnimation["f02_swingA_00"].time >= this.CharacterAnimation["f02_swingA_00"].length * 0.9f)
						{
							this.CharacterAnimation.CrossFade(this.IdleAnim);
							this.TargetStudent.DeathType = DeathType.Weapon;
							this.TargetStudent.BecomeRagdoll();
							this.MyController.radius = 0.2f;
							this.Attacking = false;
							this.AttackPhase = 1;
							this.AttackTimer = 0f;
							if (!this.Noticed)
							{
								this.CanMove = true;
								return;
							}
							this.EquippedWeapon.Drop();
							return;
						}
					}
					else if (this.AttackPhase == 1)
					{
						this.CharacterAnimation.CrossFade("f02_stab_00");
						if (this.CharacterAnimation["f02_stab_00"].time > this.CharacterAnimation["f02_stab_00"].length * 0.35f)
						{
							this.CharacterAnimation.CrossFade(this.IdleAnim);
							if (this.EquippedWeapon.Flaming)
							{
								this.Egg = true;
								this.TargetStudent.Combust();
							}
							else if (this.CanTranq && !this.TargetStudent.Male && this.TargetStudent.Club != ClubType.Council)
							{
								this.TargetStudent.Tranquil = true;
								this.CanTranq = false;
								this.Followers--;
							}
							else
							{
								this.TargetStudent.BloodSpray.SetActive(true);
								this.TargetStudent.DeathType = DeathType.Weapon;
								this.Bloodiness += 20f;
							}
							if (this.TargetStudent == this.StudentManager.Reporter)
							{
								this.StudentManager.Reporter = null;
							}
							AudioSource.PlayClipAtPoint(this.Stabs[UnityEngine.Random.Range(0, this.Stabs.Length)], base.transform.position + Vector3.up);
							UnityEngine.Object.Destroy(this.TargetStudent.DeathScream);
							this.AttackPhase = 2;
							this.Sanity -= 20f * this.Numbness;
							if (this.EquippedWeapon.WeaponID == 8)
							{
								this.TargetStudent.Ragdoll.Sacrifice = true;
								if (GameGlobals.Paranormal)
								{
									this.EquippedWeapon.Effect();
									return;
								}
							}
						}
					}
					else
					{
						this.AttackTimer += Time.deltaTime;
						if (this.AttackTimer > 0.3f)
						{
							if (!this.CanTranq)
							{
								this.StainWeapon();
							}
							this.MyController.radius = 0.2f;
							this.SanityBased = true;
							this.Attacking = false;
							this.AttackPhase = 1;
							this.AttackTimer = 0f;
							if (!this.Noticed)
							{
								this.CanMove = true;
								return;
							}
							this.EquippedWeapon.Drop();
						}
					}
				}
			}
		}
	}

	// Token: 0x06001D3F RID: 7487 RVA: 0x00169290 File Offset: 0x00167490
	public void UpdateSlouch()
	{
		if (!this.CanMove || this.Attacking || this.Dragging || !(this.PickUp == null) || this.Aiming || this.Stance.Current == StanceType.Crawling || this.Possessed || this.Carrying || this.CirnoWings.activeInHierarchy || this.LaughIntensity >= 16f)
		{
			this.CharacterAnimation["f02_yanderePose_00"].weight = Mathf.Lerp(this.CharacterAnimation["f02_yanderePose_00"].weight, 0f, Time.deltaTime * 10f);
			this.Slouch = Mathf.Lerp(this.Slouch, 0f, Time.deltaTime * 10f);
			return;
		}
		this.CharacterAnimation["f02_yanderePose_00"].weight = Mathf.Lerp(this.CharacterAnimation["f02_yanderePose_00"].weight, 1f - this.Sanity / 100f, Time.deltaTime * 10f);
		if (this.Hairstyle == 2 && this.Stance.Current == StanceType.Crouching)
		{
			this.Slouch = Mathf.Lerp(this.Slouch, 0f, Time.deltaTime * 20f);
			return;
		}
		this.Slouch = Mathf.Lerp(this.Slouch, 5f * (1f - this.Sanity / 100f), Time.deltaTime * 10f);
	}

	// Token: 0x06001D40 RID: 7488 RVA: 0x0016943C File Offset: 0x0016763C
	private void UpdateTwitch()
	{
		if (this.Sanity < 100f)
		{
			this.TwitchTimer += Time.deltaTime;
			if (this.TwitchTimer > this.NextTwitch)
			{
				this.Twitch = new Vector3((1f - this.Sanity / 100f) * UnityEngine.Random.Range(-10f, 10f), (1f - this.Sanity / 100f) * UnityEngine.Random.Range(-10f, 10f), (1f - this.Sanity / 100f) * UnityEngine.Random.Range(-10f, 10f));
				this.NextTwitch = UnityEngine.Random.Range(0f, 1f);
				this.TwitchTimer = 0f;
			}
			this.Twitch = Vector3.Lerp(this.Twitch, Vector3.zero, Time.deltaTime * 10f);
		}
	}

	// Token: 0x06001D41 RID: 7489 RVA: 0x00169530 File Offset: 0x00167730
	private void UpdateWarnings()
	{
		if (this.NearBodies > 0)
		{
			if (!this.CorpseWarning)
			{
				this.NotificationManager.DisplayNotification(NotificationType.Body);
				this.StudentManager.UpdateStudents(0);
				this.CorpseWarning = true;
			}
		}
		else if (this.CorpseWarning)
		{
			this.StudentManager.UpdateStudents(0);
			this.CorpseWarning = false;
		}
		if (this.Eavesdropping)
		{
			if (!this.EavesdropWarning)
			{
				this.NotificationManager.DisplayNotification(NotificationType.Eavesdropping);
				this.EavesdropWarning = true;
			}
		}
		else if (this.EavesdropWarning)
		{
			this.EavesdropWarning = false;
		}
		if (this.ClothingWarning)
		{
			this.ClothingTimer += Time.deltaTime;
			if (this.ClothingTimer > 1f)
			{
				this.ClothingWarning = false;
				this.ClothingTimer = 0f;
			}
		}
	}

	// Token: 0x06001D42 RID: 7490 RVA: 0x001695F8 File Offset: 0x001677F8
	private void UpdateDebugFunctionality()
	{
		if (!this.EasterEggMenu.activeInHierarchy && !this.DebugMenu.activeInHierarchy)
		{
			if (!this.Aiming && this.CanMove && Time.timeScale > 0f && Input.GetKeyDown(KeyCode.Escape))
			{
				this.PauseScreen.JumpToQuit();
			}
			if (Input.GetKeyDown(KeyCode.P))
			{
				this.CyborgParts[1].SetActive(false);
				this.MemeGlasses.SetActive(false);
				this.KONGlasses.SetActive(false);
				this.EyepatchR.SetActive(false);
				this.EyepatchL.SetActive(false);
				this.EyewearID++;
				if (this.EyewearID == 1)
				{
					this.EyepatchR.SetActive(true);
				}
				else if (this.EyewearID == 2)
				{
					this.EyepatchL.SetActive(true);
				}
				else if (this.EyewearID == 3)
				{
					this.EyepatchR.SetActive(true);
					this.EyepatchL.SetActive(true);
				}
				else if (this.EyewearID == 4)
				{
					this.KONGlasses.SetActive(true);
				}
				else if (this.EyewearID == 5)
				{
					this.MemeGlasses.SetActive(true);
				}
				else if (this.EyewearID == 6)
				{
					if (this.CyborgParts[2].activeInHierarchy)
					{
						this.CyborgParts[1].SetActive(true);
					}
					else
					{
						this.EyewearID = 0;
					}
				}
				else
				{
					this.EyewearID = 0;
				}
			}
			if (Input.GetKeyDown(KeyCode.H))
			{
				if (Input.GetButton("LB"))
				{
					this.Hairstyle += 10;
				}
				else
				{
					this.Hairstyle++;
				}
				this.UpdateHair();
			}
			if (Input.GetKey(KeyCode.H) && Input.GetKeyDown(KeyCode.LeftArrow))
			{
				this.Hairstyle--;
				this.UpdateHair();
			}
			if (Input.GetKeyDown(KeyCode.O) && !this.EasterEggMenu.activeInHierarchy)
			{
				if (this.AccessoryID > 0)
				{
					this.Accessories[this.AccessoryID].SetActive(false);
				}
				if (Input.GetButton("LB"))
				{
					this.AccessoryID += 10;
				}
				else
				{
					this.AccessoryID++;
				}
				this.UpdateAccessory();
			}
			if (Input.GetKey(KeyCode.O) && Input.GetKeyDown(KeyCode.LeftArrow))
			{
				if (this.AccessoryID > 0)
				{
					this.Accessories[this.AccessoryID].SetActive(false);
				}
				this.AccessoryID--;
				this.UpdateAccessory();
			}
			if (!this.NoDebug && !this.DebugMenu.activeInHierarchy && this.CanMove && !this.DebugMenu.activeInHierarchy)
			{
				if (Input.GetKeyDown("-"))
				{
					if (Time.timeScale < 6f)
					{
						Time.timeScale = 1f;
					}
					else
					{
						Time.timeScale -= 5f;
					}
				}
				if (Input.GetKeyDown("="))
				{
					if (Time.timeScale < 5f)
					{
						Time.timeScale = 5f;
					}
					else
					{
						Time.timeScale += 5f;
						if (Time.timeScale > 25f)
						{
							Time.timeScale = 25f;
						}
					}
				}
			}
			if (Input.GetKey(KeyCode.Period))
			{
				this.BreastSize += Time.deltaTime;
				if (this.BreastSize > 2f)
				{
					this.BreastSize = 2f;
				}
				this.RightBreast.localScale = new Vector3(this.BreastSize, this.BreastSize, this.BreastSize);
				this.LeftBreast.localScale = new Vector3(this.BreastSize, this.BreastSize, this.BreastSize);
			}
			if (Input.GetKey(KeyCode.Comma))
			{
				this.BreastSize -= Time.deltaTime;
				if (this.BreastSize < 0.5f)
				{
					this.BreastSize = 0.5f;
				}
				this.RightBreast.localScale = new Vector3(this.BreastSize, this.BreastSize, this.BreastSize);
				this.LeftBreast.localScale = new Vector3(this.BreastSize, this.BreastSize, this.BreastSize);
			}
		}
		if (!this.NoDebug)
		{
			if (this.CanMove)
			{
				if (!this.Egg || this.EggBypass > 8)
				{
					if (base.transform.position.y < 1000f)
					{
						if (Input.GetKeyDown(KeyCode.Slash))
						{
							this.DebugMenu.SetActive(false);
							this.EasterEggMenu.SetActive(!this.EasterEggMenu.activeInHierarchy);
						}
						if (this.EasterEggMenu.activeInHierarchy)
						{
							if (Input.GetKeyDown(KeyCode.P))
							{
								this.Punish();
								return;
							}
							if (Input.GetKeyDown(KeyCode.Z))
							{
								this.Slend();
								return;
							}
							if (Input.GetKeyDown(KeyCode.B))
							{
								this.Bancho();
								return;
							}
							if (Input.GetKeyDown(KeyCode.C))
							{
								this.Cirno();
								return;
							}
							if (Input.GetKeyDown(KeyCode.H))
							{
								this.EmptyHands();
								this.Hate();
								return;
							}
							if (Input.GetKeyDown(KeyCode.T))
							{
								this.StudentManager.AttackOnTitan();
								this.AttackOnTitan();
								return;
							}
							if (Input.GetKeyDown(KeyCode.G))
							{
								this.GaloSengen();
								return;
							}
							if (!Input.GetKeyDown(KeyCode.J))
							{
								if (Input.GetKeyDown(KeyCode.K))
								{
									this.EasterEggMenu.SetActive(false);
									this.StudentManager.Kong();
									this.DK = true;
									return;
								}
								if (Input.GetKeyDown(KeyCode.L))
								{
									this.Agent();
									return;
								}
								if (Input.GetKeyDown(KeyCode.N))
								{
									this.Nude();
									return;
								}
								if (Input.GetKeyDown(KeyCode.S))
								{
									this.EasterEggMenu.SetActive(false);
									this.Egg = true;
									this.StudentManager.Spook();
									return;
								}
								if (Input.GetKeyDown(KeyCode.F))
								{
									this.EasterEggMenu.SetActive(false);
									this.Falcon();
									return;
								}
								if (Input.GetKeyDown(KeyCode.X))
								{
									this.EasterEggMenu.SetActive(false);
									this.X();
									return;
								}
								if (Input.GetKeyDown(KeyCode.O))
								{
									this.EasterEggMenu.SetActive(false);
									this.Punch();
									return;
								}
								if (Input.GetKeyDown(KeyCode.U))
								{
									this.EasterEggMenu.SetActive(false);
									this.BadTime();
									return;
								}
								if (Input.GetKeyDown(KeyCode.Y))
								{
									this.EasterEggMenu.SetActive(false);
									this.CyborgNinja();
									return;
								}
								if (Input.GetKeyDown(KeyCode.E))
								{
									this.EasterEggMenu.SetActive(false);
									this.Ebola();
									return;
								}
								if (Input.GetKeyDown(KeyCode.Q))
								{
									this.EasterEggMenu.SetActive(false);
									this.Samus();
									return;
								}
								if (Input.GetKeyDown(KeyCode.W))
								{
									this.EasterEggMenu.SetActive(false);
									this.Witch();
									return;
								}
								if (Input.GetKeyDown(KeyCode.R))
								{
									this.EasterEggMenu.SetActive(false);
									this.Pose();
									return;
								}
								if (Input.GetKeyDown(KeyCode.V))
								{
									this.EasterEggMenu.SetActive(false);
									this.Vaporwave();
									return;
								}
								if (Input.GetKeyDown(KeyCode.Alpha2))
								{
									this.EasterEggMenu.SetActive(false);
									this.HairBlades();
									return;
								}
								if (Input.GetKeyDown(KeyCode.Alpha7))
								{
									this.EasterEggMenu.SetActive(false);
									this.Tornado();
									return;
								}
								if (Input.GetKeyDown(KeyCode.Alpha8))
								{
									this.EasterEggMenu.SetActive(false);
									this.GenderSwap();
									return;
								}
								if (Input.GetKeyDown("[5]"))
								{
									this.EasterEggMenu.SetActive(false);
									this.SwapMesh();
									return;
								}
								if (Input.GetKeyDown(KeyCode.A))
								{
									this.EasterEggMenu.SetActive(false);
									return;
								}
								if (Input.GetKeyDown(KeyCode.I))
								{
									this.StudentManager.NoGravity = true;
									this.EasterEggMenu.SetActive(false);
									return;
								}
								if (Input.GetKeyDown(KeyCode.D))
								{
									this.EasterEggMenu.SetActive(false);
									this.Sith();
									return;
								}
								if (Input.GetKeyDown(KeyCode.M))
								{
									this.EasterEggMenu.SetActive(false);
									this.Snake();
									return;
								}
								if (Input.GetKeyDown(KeyCode.Alpha1))
								{
									this.EasterEggMenu.SetActive(false);
									this.Gazer();
									return;
								}
								if (Input.GetKeyDown(KeyCode.Alpha3))
								{
									this.StudentManager.SecurityCameras();
									this.EasterEggMenu.SetActive(false);
									return;
								}
								if (Input.GetKeyDown(KeyCode.Alpha4))
								{
									this.KLK();
									this.EasterEggMenu.SetActive(false);
									return;
								}
								if (Input.GetKeyDown(KeyCode.Alpha6))
								{
									this.EasterEggMenu.SetActive(false);
									this.Six();
									return;
								}
								if (Input.GetKeyDown(KeyCode.F1))
								{
									this.Weather();
									this.EasterEggMenu.SetActive(false);
									return;
								}
								if (Input.GetKeyDown(KeyCode.F2))
								{
									this.Horror();
									this.EasterEggMenu.SetActive(false);
									return;
								}
								if (Input.GetKeyDown(KeyCode.F3))
								{
									this.LifeNote();
									this.EasterEggMenu.SetActive(false);
									return;
								}
								if (Input.GetKeyDown(KeyCode.F4))
								{
									this.Mandere();
									this.EasterEggMenu.SetActive(false);
									return;
								}
								if (Input.GetKeyDown(KeyCode.F5))
								{
									this.BlackHoleChan();
									this.EasterEggMenu.SetActive(false);
									return;
								}
								if (Input.GetKeyDown(KeyCode.F6))
								{
									this.ElfenLied();
									this.EasterEggMenu.SetActive(false);
									return;
								}
								if (Input.GetKeyDown(KeyCode.F7))
								{
									this.Berserk();
									this.EasterEggMenu.SetActive(false);
									return;
								}
								if (Input.GetKeyDown(KeyCode.F8))
								{
									this.Nier();
									this.EasterEggMenu.SetActive(false);
									return;
								}
								if (Input.GetKeyDown(KeyCode.F9))
								{
									this.Ghoul();
									this.EasterEggMenu.SetActive(false);
									return;
								}
								if (Input.GetKeyDown(KeyCode.F10))
								{
									this.CinematicCameraFilters.enabled = true;
									this.CameraFilters.enabled = true;
									this.EasterEggMenu.SetActive(false);
									return;
								}
								if (!Input.GetKeyDown(KeyCode.F11) && Input.GetKeyDown(KeyCode.Space))
								{
									this.EasterEggMenu.SetActive(false);
									return;
								}
							}
						}
					}
				}
				else if (Input.GetKeyDown(KeyCode.Slash))
				{
					this.EggBypass++;
					return;
				}
			}
		}
		else if (Input.GetKeyDown(KeyCode.Z))
		{
			this.DebugMenu.transform.parent.GetComponent<DebugMenuScript>().Censor();
		}
	}

	// Token: 0x06001D43 RID: 7491 RVA: 0x00169FA8 File Offset: 0x001681A8
	private void LateUpdate()
	{
		if (this.VibrationCheck)
		{
			this.VibrationTimer = Mathf.MoveTowards(this.VibrationTimer, 0f, Time.deltaTime);
			if (this.VibrationTimer == 0f)
			{
				GamePad.SetVibration(PlayerIndex.One, 0f, 0f);
				this.VibrationCheck = false;
			}
		}
		this.LeftEye.localPosition = new Vector3(this.LeftEye.localPosition.x, this.LeftEye.localPosition.y, this.LeftEyeOrigin.z - this.EyeShrink * 0.02f);
		this.RightEye.localPosition = new Vector3(this.RightEye.localPosition.x, this.RightEye.localPosition.y, this.RightEyeOrigin.z + this.EyeShrink * 0.02f);
		this.LeftEye.localScale = new Vector3(1f - this.EyeShrink, 1f - this.EyeShrink, this.LeftEye.localScale.z);
		this.RightEye.localScale = new Vector3(1f - this.EyeShrink, 1f - this.EyeShrink, this.RightEye.localScale.z);
		this.ID = 0;
		while (this.ID < this.Spine.Length)
		{
			Transform transform = this.Spine[this.ID].transform;
			transform.localEulerAngles = new Vector3(transform.localEulerAngles.x + this.Slouch, transform.localEulerAngles.y, transform.localEulerAngles.z);
			this.ID++;
		}
		if (this.Aiming)
		{
			float num = 1f;
			if (this.Selfie)
			{
				num = -1f;
			}
			Transform transform2 = this.Spine[3].transform;
			transform2.localEulerAngles = new Vector3(transform2.localEulerAngles.x - this.Bend * num, transform2.localEulerAngles.y, transform2.localEulerAngles.z);
		}
		float num2 = 1f;
		if (this.Stance.Current == StanceType.Crouching)
		{
			num2 = 3.66666f;
		}
		Transform transform3 = this.Arm[0].transform;
		transform3.localEulerAngles = new Vector3(transform3.localEulerAngles.x, transform3.localEulerAngles.y, transform3.localEulerAngles.z - this.Slouch * (3f + num2));
		Transform transform4 = this.Arm[1].transform;
		transform4.localEulerAngles = new Vector3(transform4.localEulerAngles.x, transform4.localEulerAngles.y, transform4.localEulerAngles.z + this.Slouch * (3f + num2));
		if (!this.Aiming)
		{
			this.Head.localEulerAngles += this.Twitch;
		}
		if (this.Aiming)
		{
			if (this.Stance.Current == StanceType.Crawling)
			{
				this.TargetHeight = -1.4f;
			}
			else if (this.Stance.Current == StanceType.Crouching)
			{
				this.TargetHeight = -0.6f;
			}
			else
			{
				this.TargetHeight = 0f;
			}
			this.Height = Mathf.Lerp(this.Height, this.TargetHeight, Time.deltaTime * 10f);
			this.PelvisRoot.transform.localPosition = new Vector3(this.PelvisRoot.transform.localPosition.x, this.Height, this.PelvisRoot.transform.localPosition.z);
		}
		if (this.Egg)
		{
			if (this.Slender)
			{
				Transform transform5 = this.Leg[0];
				transform5.localScale = new Vector3(transform5.localScale.x, 2f, transform5.localScale.z);
				Transform transform6 = this.Foot[0];
				transform6.localScale = new Vector3(transform6.localScale.x, 0.5f, transform6.localScale.z);
				Transform transform7 = this.Leg[1];
				transform7.localScale = new Vector3(transform7.localScale.x, 2f, transform7.localScale.z);
				Transform transform8 = this.Foot[1];
				transform8.localScale = new Vector3(transform8.localScale.x, 0.5f, transform8.localScale.z);
				Transform transform9 = this.Arm[0];
				transform9.localScale = new Vector3(2f, transform9.localScale.y, transform9.localScale.z);
				Transform transform10 = this.Arm[1];
				transform10.localScale = new Vector3(2f, transform10.localScale.y, transform10.localScale.z);
			}
			if (this.DK)
			{
				this.Arm[0].localScale = new Vector3(2f, 2f, 2f);
				this.Arm[1].localScale = new Vector3(2f, 2f, 2f);
				this.Head.localScale = new Vector3(2f, 2f, 2f);
			}
			if (this.CirnoWings.activeInHierarchy)
			{
				if (this.Running)
				{
					this.FlapSpeed = 5f;
				}
				else if (this.FlapSpeed == 0f)
				{
					this.FlapSpeed = 1f;
				}
				else
				{
					this.FlapSpeed = 3f;
				}
				Transform transform11 = this.CirnoWing[0];
				Transform transform12 = this.CirnoWing[1];
				if (!this.FlapOut)
				{
					this.CirnoRotation += Time.deltaTime * 100f * this.FlapSpeed;
					transform11.localEulerAngles = new Vector3(transform11.localEulerAngles.x, this.CirnoRotation, transform11.localEulerAngles.z);
					transform12.localEulerAngles = new Vector3(transform12.localEulerAngles.x, -this.CirnoRotation, transform12.localEulerAngles.z);
					if (this.CirnoRotation > 15f)
					{
						this.FlapOut = true;
					}
				}
				else
				{
					this.CirnoRotation -= Time.deltaTime * 100f * this.FlapSpeed;
					transform11.localEulerAngles = new Vector3(transform11.localEulerAngles.x, this.CirnoRotation, transform11.localEulerAngles.z);
					transform12.localEulerAngles = new Vector3(transform12.localEulerAngles.x, -this.CirnoRotation, transform12.localEulerAngles.z);
					if (this.CirnoRotation < -15f)
					{
						this.FlapOut = false;
					}
				}
			}
			if (this.SpiderLegs.activeInHierarchy)
			{
				if (this.SpiderGrow)
				{
					if (this.SpiderLegs.transform.localScale.x < 0.49f)
					{
						this.SpiderLegs.transform.localScale = Vector3.Lerp(this.SpiderLegs.transform.localScale, new Vector3(0.5f, 0.5f, 0.5f), Time.deltaTime * 5f);
						SchoolGlobals.SchoolAtmosphere = 1f - this.SpiderLegs.transform.localScale.x;
						this.StudentManager.SetAtmosphere();
					}
				}
				else if (this.SpiderLegs.transform.localScale.x > 0.001f)
				{
					this.SpiderLegs.transform.localScale = Vector3.Lerp(this.SpiderLegs.transform.localScale, new Vector3(0f, 0f, 0f), Time.deltaTime * 5f);
					SchoolGlobals.SchoolAtmosphere = 1f - this.SpiderLegs.transform.localScale.x;
					this.StudentManager.SetAtmosphere();
				}
			}
			if (this.BlackHole)
			{
				this.RightLeg.transform.Rotate(-60f, 0f, 0f, Space.Self);
				this.LeftLeg.transform.Rotate(-60f, 0f, 0f, Space.Self);
			}
			if (this.SwingKagune)
			{
				if (!this.ReturnKagune)
				{
					for (int i = 0; i < this.Kagune.Length; i++)
					{
						if (i < 2)
						{
							this.KaguneRotation[i] = new Vector3(Mathf.MoveTowards(this.KaguneRotation[i].x, 15f, Time.deltaTime * 1000f), Mathf.MoveTowards(this.KaguneRotation[i].y, 180f, Time.deltaTime * 1000f), Mathf.MoveTowards(this.KaguneRotation[i].z, 500f, Time.deltaTime * 1000f));
							this.Kagune[i].transform.localEulerAngles = this.KaguneRotation[i];
						}
						else
						{
							this.KaguneRotation[i] = new Vector3(Mathf.MoveTowards(this.KaguneRotation[i].x, 15f, Time.deltaTime * 1000f), Mathf.MoveTowards(this.KaguneRotation[i].y, -180f, Time.deltaTime * 1000f), Mathf.MoveTowards(this.KaguneRotation[i].z, -500f, Time.deltaTime * 1000f));
							this.Kagune[i].transform.localEulerAngles = this.KaguneRotation[i];
						}
					}
					if (this.KagunePhase == 0 && this.KaguneRotation[0].y == 180f)
					{
						UnityEngine.Object.Instantiate<GameObject>(this.DemonSlash, base.transform.position + base.transform.up + base.transform.forward, Quaternion.identity);
						this.KagunePhase = 1;
					}
					if (this.KaguneRotation[0] == new Vector3(15f, 180f, 500f))
					{
						this.ReturnKagune = true;
					}
				}
				else
				{
					for (int j = 0; j < this.Kagune.Length; j++)
					{
						if (j == 0)
						{
							this.KaguneRotation[j] = new Vector3(Mathf.Lerp(this.KaguneRotation[j].x, 22.5f, Time.deltaTime * 10f), Mathf.Lerp(this.KaguneRotation[j].y, 22.5f, Time.deltaTime * 10f), Mathf.Lerp(this.KaguneRotation[j].z, 0f, Time.deltaTime * 10f));
						}
						else if (j == 1)
						{
							this.KaguneRotation[j] = new Vector3(Mathf.Lerp(this.KaguneRotation[j].x, -22.5f, Time.deltaTime * 10f), Mathf.Lerp(this.KaguneRotation[j].y, 22.5f, Time.deltaTime * 10f), Mathf.Lerp(this.KaguneRotation[j].z, 0f, Time.deltaTime * 10f));
						}
						else if (j == 2)
						{
							this.KaguneRotation[j] = new Vector3(Mathf.Lerp(this.KaguneRotation[j].x, 22.5f, Time.deltaTime * 10f), Mathf.Lerp(this.KaguneRotation[j].y, -22.5f, Time.deltaTime * 10f), Mathf.Lerp(this.KaguneRotation[j].z, 0f, Time.deltaTime * 10f));
						}
						else if (j == 3)
						{
							this.KaguneRotation[j] = new Vector3(Mathf.Lerp(this.KaguneRotation[j].x, -22.5f, Time.deltaTime * 10f), Mathf.Lerp(this.KaguneRotation[j].y, -22.5f, Time.deltaTime * 10f), Mathf.Lerp(this.KaguneRotation[j].z, 0f, Time.deltaTime * 10f));
						}
						this.Kagune[j].transform.localEulerAngles = this.KaguneRotation[j];
					}
					if (Vector3.Distance(this.KaguneRotation[0], new Vector3(22.5f, 22.5f, 0f)) < 10f)
					{
						this.SwingKagune = false;
						this.ReturnKagune = false;
						this.KagunePhase = 0;
					}
				}
			}
		}
		if (this.PickUp != null && this.PickUp.Flashlight)
		{
			this.RightHand.transform.eulerAngles = new Vector3(0f, base.transform.eulerAngles.y, base.transform.eulerAngles.z);
		}
		if (this.ReachWeight > 0f)
		{
			this.CharacterAnimation["f02_reachForWeapon_00"].weight = this.ReachWeight;
			this.ReachWeight = Mathf.MoveTowards(this.ReachWeight, 0f, Time.deltaTime * 2f);
			if (this.ReachWeight < 0.01f)
			{
				this.ReachWeight = 0f;
				this.CharacterAnimation["f02_reachForWeapon_00"].weight = 0f;
			}
		}
		if (this.SanitySmudges.color.a > 1f - this.sanity / 100f + 0.0001f || this.SanitySmudges.color.a < 1f - this.sanity / 100f - 0.0001f)
		{
			float num3 = this.SanitySmudges.color.a;
			num3 = Mathf.MoveTowards(num3, 1f - this.sanity / 100f, Time.deltaTime);
			this.SanitySmudges.color = new Color(1f, 1f, 1f, num3);
			this.StudentManager.SelectiveGreyscale.desaturation = 1f - this.StudentManager.Atmosphere + num3;
			if (num3 > 0.66666f)
			{
				float faces = 1f - (1f - num3) / 0.33333f;
				this.StudentManager.SetFaces(faces);
			}
			else
			{
				this.StudentManager.SetFaces(0f);
			}
			float num4 = 100f - num3 * 100f;
			this.SanityLabel.text = num4.ToString("0") + "%";
		}
		if (this.CanMove && this.sanity < 33.333f && !this.NearSenpai)
		{
			this.GiggleTimer += Time.deltaTime * (1f - this.sanity / 33.333f);
			if (this.GiggleTimer > 10f)
			{
				UnityEngine.Object.Instantiate<GameObject>(this.GiggleDisc, base.transform.position + Vector3.up, Quaternion.identity);
				AudioSource.PlayClipAtPoint(this.CreepyGiggles[UnityEngine.Random.Range(0, this.CreepyGiggles.Length)], base.transform.position);
				this.InsaneLines.Play();
				this.GiggleTimer = 0f;
			}
		}
		if (this.FightHasBrokenUp)
		{
			this.BreakUpTimer = Mathf.MoveTowards(this.BreakUpTimer, 0f, Time.deltaTime);
			if (this.BreakUpTimer == 0f)
			{
				this.FightHasBrokenUp = false;
				this.SeenByAuthority = false;
			}
		}
	}

	// Token: 0x06001D44 RID: 7492 RVA: 0x0016AFFC File Offset: 0x001691FC
	public void StainWeapon()
	{
		if (this.EquippedWeapon != null)
		{
			if (this.TargetStudent != null && this.TargetStudent.StudentID < this.EquippedWeapon.Victims.Length)
			{
				this.EquippedWeapon.Victims[this.TargetStudent.StudentID] = true;
			}
			this.EquippedWeapon.Blood.enabled = true;
			this.EquippedWeapon.MurderWeapon = true;
			if (!this.NoStainGloves && this.Gloved && !this.Gloves.Blood.enabled)
			{
				this.GloveAttacher.newRenderer.material.mainTexture = this.BloodyGloveTexture;
				this.Gloves.PickUp.Evidence = true;
				this.Gloves.Blood.enabled = true;
				this.GloveBlood = 1;
				this.Police.BloodyClothing++;
			}
			this.NoStainGloves = false;
			if (this.Mask != null && !this.Mask.Blood.enabled)
			{
				this.Mask.PickUp.Evidence = true;
				this.Mask.Blood.enabled = true;
				this.Police.BloodyClothing++;
			}
			if (!this.EquippedWeapon.Evidence)
			{
				this.EquippedWeapon.Evidence = true;
				this.Police.MurderWeapons++;
			}
		}
	}

	// Token: 0x06001D45 RID: 7493 RVA: 0x0016B178 File Offset: 0x00169378
	public void MoveTowardsTarget(Vector3 target)
	{
		Vector3 a = target - base.transform.position;
		this.MyController.Move(a * (Time.deltaTime * 10f));
	}

	// Token: 0x06001D46 RID: 7494 RVA: 0x0016B1B4 File Offset: 0x001693B4
	public void StopAiming()
	{
		this.UpdateAccessory();
		this.UpdateHair();
		this.CharacterAnimation["f02_cameraPose_00"].weight = 0f;
		this.CharacterAnimation["f02_selfie_00"].weight = 0f;
		this.PelvisRoot.transform.localPosition = new Vector3(this.PelvisRoot.transform.localPosition.x, 0f, this.PelvisRoot.transform.localPosition.z);
		this.ShoulderCamera.AimingCamera = false;
		if (!Input.GetButtonDown("Start") && !Input.GetKeyDown(KeyCode.Escape))
		{
			this.FixCamera();
		}
		if (this.ShoulderCamera.Timer == 0f)
		{
			this.RPGCamera.enabled = true;
		}
		if (!OptionGlobals.Fog)
		{
			this.MainCamera.clearFlags = CameraClearFlags.Skybox;
		}
		else
		{
			this.MainCamera.clearFlags = CameraClearFlags.Color;
		}
		this.MainCamera.farClipPlane = (float)OptionGlobals.DrawDistance;
		this.Smartphone.transform.parent.gameObject.SetActive(false);
		this.Smartphone.targetTexture = this.Shutter.SmartphoneScreen;
		this.Smartphone.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
		this.Smartphone.fieldOfView = 60f;
		this.Shutter.TargetStudent = 0;
		this.Height = 0f;
		this.Bend = 0f;
		this.HandCamera.gameObject.SetActive(false);
		this.SelfieGuide.SetActive(false);
		this.PhonePromptBar.Show = false;
		this.MainCamera.enabled = true;
		this.UsingController = false;
		this.Aiming = false;
		this.Selfie = false;
		this.Lewd = false;
	}

	// Token: 0x06001D47 RID: 7495 RVA: 0x0016B394 File Offset: 0x00169594
	public void FixCamera()
	{
		this.RPGCamera.enabled = true;
		this.RPGCamera.UpdateRotation();
		this.RPGCamera.mouseSmoothingFactor = 0f;
		this.RPGCamera.GetInput();
		this.RPGCamera.GetDesiredPosition();
		this.RPGCamera.PositionUpdate();
		this.RPGCamera.mouseSmoothingFactor = 0.08f;
		this.Blur.enabled = false;
	}

	// Token: 0x06001D48 RID: 7496 RVA: 0x0016B408 File Offset: 0x00169608
	private void ResetSenpaiEffects()
	{
		this.DepthOfField.focalSize = 150f;
		this.DepthOfField.focalZStartCurve = 0f;
		this.DepthOfField.focalZEndCurve = 0f;
		this.DepthOfField.enabled = false;
		this.ColorCorrection.redChannel.MoveKey(1, new Keyframe(0.5f, 0.5f));
		this.ColorCorrection.greenChannel.MoveKey(1, new Keyframe(0.5f, 0.5f));
		this.ColorCorrection.blueChannel.MoveKey(1, new Keyframe(0.5f, 0.5f));
		this.ColorCorrection.redChannel.SmoothTangents(1, 0f);
		this.ColorCorrection.greenChannel.SmoothTangents(1, 0f);
		this.ColorCorrection.blueChannel.SmoothTangents(1, 0f);
		this.ColorCorrection.UpdateTextures();
		this.ColorCorrection.enabled = false;
		for (int i = 1; i < 6; i++)
		{
			this.CharacterAnimation[this.CreepyIdles[i]].weight = 0f;
			this.CharacterAnimation[this.CreepyWalks[i]].weight = 0f;
		}
		this.CharacterAnimation["f02_shy_00"].weight = 0f;
		this.HeartBeat.volume = 0f;
		this.SelectGrayscale.desaturation = this.GreyTarget;
		this.SenpaiFade = 100f;
		this.SenpaiTint = 0f;
	}

	// Token: 0x06001D49 RID: 7497 RVA: 0x0016B5A4 File Offset: 0x001697A4
	public void ResetYandereEffects()
	{
		this.Obscurance.enabled = false;
		this.Vignette.intensity = 0f;
		this.Vignette.blur = 0f;
		this.Vignette.chromaticAberration = 0f;
		this.Vignette.enabled = false;
		this.YandereColorCorrection.redChannel.MoveKey(1, new Keyframe(0.5f, 0.5f));
		this.YandereColorCorrection.greenChannel.MoveKey(1, new Keyframe(0.5f, 0.5f));
		this.YandereColorCorrection.blueChannel.MoveKey(1, new Keyframe(0.5f, 0.5f));
		this.YandereColorCorrection.redChannel.SmoothTangents(1, 0f);
		this.YandereColorCorrection.greenChannel.SmoothTangents(1, 0f);
		this.YandereColorCorrection.blueChannel.SmoothTangents(1, 0f);
		this.YandereColorCorrection.UpdateTextures();
		this.YandereColorCorrection.enabled = false;
		Time.timeScale = 1f;
		this.YandereFade = 100f;
		this.StudentManager.Tag.Sprite.color = new Color(1f, 0f, 0f, 0f);
		this.StudentManager.RedString.gameObject.SetActive(false);
	}

	// Token: 0x06001D4A RID: 7498 RVA: 0x0016B710 File Offset: 0x00169910
	private void DumpRagdoll(RagdollDumpType Type)
	{
		this.Ragdoll.transform.position = base.transform.position;
		if (Type == RagdollDumpType.Incinerator)
		{
			this.Ragdoll.transform.LookAt(this.Incinerator.transform.position);
			this.Ragdoll.transform.eulerAngles = new Vector3(this.Ragdoll.transform.eulerAngles.x, this.Ragdoll.transform.eulerAngles.y + 180f, this.Ragdoll.transform.eulerAngles.z);
		}
		else if (Type == RagdollDumpType.TranqCase)
		{
			this.Ragdoll.transform.LookAt(this.TranqCase.transform.position);
		}
		else if (Type == RagdollDumpType.WoodChipper)
		{
			this.Ragdoll.transform.LookAt(this.WoodChipper.transform.position);
		}
		RagdollScript component = this.Ragdoll.GetComponent<RagdollScript>();
		component.DumpType = Type;
		component.Dump();
	}

	// Token: 0x06001D4B RID: 7499 RVA: 0x0016B81C File Offset: 0x00169A1C
	public void Unequip()
	{
		if (this.CanMove || this.Noticed)
		{
			if (this.Equipped < 3)
			{
				this.CharacterAnimation["f02_reachForWeapon_00"].time = 0f;
				this.ReachWeight = 1f;
				if (this.EquippedWeapon != null)
				{
					this.EquippedWeapon.gameObject.SetActive(false);
				}
			}
			else
			{
				this.Weapon[3].Drop();
			}
			this.Equipped = 0;
			this.Mopping = false;
			this.StudentManager.UpdateStudents(0);
			this.WeaponManager.UpdateLabels();
			this.WeaponMenu.UpdateSprites();
			this.WeaponWarning = false;
		}
	}

	// Token: 0x06001D4C RID: 7500 RVA: 0x0016B8D0 File Offset: 0x00169AD0
	public void DropWeapon(int ID)
	{
		this.DropTimer[ID] += Time.deltaTime;
		if (this.DropTimer[ID] > 0.5f)
		{
			this.Weapon[ID].Drop();
			this.Weapon[ID] = null;
			this.Unequip();
			this.DropTimer[ID] = 0f;
		}
	}

	// Token: 0x06001D4D RID: 7501 RVA: 0x0016B92C File Offset: 0x00169B2C
	public void EmptyHands()
	{
		if (this.Carrying || this.HeavyWeight)
		{
			this.StopCarrying();
		}
		if (this.Armed)
		{
			this.Unequip();
		}
		if (this.PickUp != null)
		{
			this.PickUp.Drop();
		}
		if (this.Dragging)
		{
			this.Ragdoll.GetComponent<RagdollScript>().StopDragging();
		}
		this.ID = 1;
		while (this.ID < this.Poisons.Length)
		{
			this.Poisons[this.ID].SetActive(false);
			this.ID++;
		}
		this.Mopping = false;
	}

	// Token: 0x06001D4E RID: 7502 RVA: 0x0016B9CF File Offset: 0x00169BCF
	public void UpdateNumbness()
	{
		this.Numbness = 1f - 0.1f * (float)(PlayerGlobals.Numbness + PlayerGlobals.NumbnessBonus);
	}

	// Token: 0x06001D4F RID: 7503 RVA: 0x0016B9F0 File Offset: 0x00169BF0
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == "BloodPool(Clone)" && other.transform.localScale.x > 0.3f)
		{
			if (PlayerGlobals.PantiesEquipped == 8)
			{
				this.RightFootprintSpawner.Bloodiness = 5;
				this.LeftFootprintSpawner.Bloodiness = 5;
				return;
			}
			this.RightFootprintSpawner.Bloodiness = 10;
			this.LeftFootprintSpawner.Bloodiness = 10;
		}
	}

	// Token: 0x06001D50 RID: 7504 RVA: 0x0016BA68 File Offset: 0x00169C68
	public void UpdateHair()
	{
		if (this.Hairstyle > this.Hairstyles.Length - 1)
		{
			this.Hairstyle = 0;
		}
		if (this.Hairstyle < 0)
		{
			this.Hairstyle = this.Hairstyles.Length - 1;
		}
		this.ID = 1;
		while (this.ID < this.Hairstyles.Length)
		{
			this.Hairstyles[this.ID].SetActive(false);
			this.ID++;
		}
		if (this.Hairstyle > 0)
		{
			this.Hairstyles[this.Hairstyle].SetActive(true);
		}
	}

	// Token: 0x06001D51 RID: 7505 RVA: 0x0016BB00 File Offset: 0x00169D00
	public void StopLaughing()
	{
		this.BladeHairCollider1.enabled = false;
		this.BladeHairCollider2.enabled = false;
		if (this.Sanity < 33.33333f)
		{
			this.Teeth.SetActive(true);
		}
		this.LaughIntensity = 0f;
		this.Laughing = false;
		this.LaughClip = null;
		this.Twitch = Vector3.zero;
		if (!this.Stand.Stand.activeInHierarchy)
		{
			this.CanMove = true;
		}
		if (this.BanchoActive)
		{
			AudioSource.PlayClipAtPoint(this.BanchoFinalYan, base.transform.position);
			this.CharacterAnimation.CrossFade("f02_banchoFinisher_00");
			this.BanchoFlurry.MyCollider.enabled = false;
			this.Finisher = true;
			this.CanMove = false;
		}
	}

	// Token: 0x06001D52 RID: 7506 RVA: 0x0016BBC8 File Offset: 0x00169DC8
	private void SetUniform()
	{
		if (StudentGlobals.FemaleUniform == 0)
		{
			StudentGlobals.FemaleUniform = 1;
		}
		this.MyRenderer.sharedMesh = this.Uniforms[StudentGlobals.FemaleUniform];
		if (this.Casual)
		{
			this.TextureToUse = this.UniformTextures[StudentGlobals.FemaleUniform];
		}
		else
		{
			this.TextureToUse = this.CasualTextures[StudentGlobals.FemaleUniform];
		}
		this.MyRenderer.materials[0].mainTexture = this.TextureToUse;
		this.MyRenderer.materials[1].mainTexture = this.TextureToUse;
		this.MyRenderer.materials[2].mainTexture = this.FaceTexture;
		base.StartCoroutine(this.ApplyCustomCostume());
	}

	// Token: 0x06001D53 RID: 7507 RVA: 0x0016BC7C File Offset: 0x00169E7C
	private IEnumerator ApplyCustomCostume()
	{
		if (StudentGlobals.FemaleUniform == 1)
		{
			WWW CustomUniform = new WWW("file:///" + Application.streamingAssetsPath + "/CustomUniform.png");
			yield return CustomUniform;
			if (CustomUniform.error == null)
			{
				this.MyRenderer.materials[0].mainTexture = CustomUniform.texture;
				this.MyRenderer.materials[1].mainTexture = CustomUniform.texture;
			}
			CustomUniform = null;
		}
		else if (StudentGlobals.FemaleUniform == 2)
		{
			WWW CustomUniform = new WWW("file:///" + Application.streamingAssetsPath + "/CustomLong.png");
			yield return CustomUniform;
			if (CustomUniform.error == null)
			{
				this.MyRenderer.materials[0].mainTexture = CustomUniform.texture;
				this.MyRenderer.materials[1].mainTexture = CustomUniform.texture;
			}
			CustomUniform = null;
		}
		else if (StudentGlobals.FemaleUniform == 3)
		{
			WWW CustomUniform = new WWW("file:///" + Application.streamingAssetsPath + "/CustomSweater.png");
			yield return CustomUniform;
			if (CustomUniform.error == null)
			{
				this.MyRenderer.materials[0].mainTexture = CustomUniform.texture;
				this.MyRenderer.materials[1].mainTexture = CustomUniform.texture;
			}
			CustomUniform = null;
		}
		else if (StudentGlobals.FemaleUniform == 4 || StudentGlobals.FemaleUniform == 5)
		{
			WWW CustomUniform = new WWW("file:///" + Application.streamingAssetsPath + "/CustomBlazer.png");
			yield return CustomUniform;
			if (CustomUniform.error == null)
			{
				this.MyRenderer.materials[0].mainTexture = CustomUniform.texture;
				this.MyRenderer.materials[1].mainTexture = CustomUniform.texture;
			}
			CustomUniform = null;
		}
		WWW CustomFace = new WWW("file:///" + Application.streamingAssetsPath + "/CustomFace.png");
		yield return CustomFace;
		if (CustomFace.error == null)
		{
			this.MyRenderer.materials[2].mainTexture = CustomFace.texture;
			this.FaceTexture = CustomFace.texture;
		}
		WWW CustomHair = new WWW("file:///" + Application.streamingAssetsPath + "/CustomHair.png");
		yield return CustomHair;
		if (CustomHair.error == null)
		{
			this.PonytailRenderer.material.mainTexture = CustomHair.texture;
			this.PigtailR.material.mainTexture = CustomHair.texture;
			this.PigtailL.material.mainTexture = CustomHair.texture;
		}
		WWW CustomDrills = new WWW("file:///" + Application.streamingAssetsPath + "/CustomDrills.png");
		yield return CustomDrills;
		if (CustomDrills.error == null)
		{
			this.Drills.materials[0].mainTexture = CustomDrills.texture;
			this.Drills.material.mainTexture = CustomDrills.texture;
		}
		WWW CustomSwimsuit = new WWW("file:///" + Application.streamingAssetsPath + "/CustomSwimsuit.png");
		yield return CustomSwimsuit;
		if (CustomSwimsuit.error == null)
		{
			this.SwimsuitTexture = CustomSwimsuit.texture;
		}
		WWW CustomGym = new WWW("file:///" + Application.streamingAssetsPath + "/CustomGym.png");
		yield return CustomGym;
		if (CustomGym.error == null)
		{
			this.GymTexture = CustomGym.texture;
		}
		WWW CustomNude = new WWW("file:///" + Application.streamingAssetsPath + "/CustomNude.png");
		yield return CustomNude;
		if (CustomNude.error == null)
		{
			this.NudeTexture = CustomNude.texture;
		}
		WWW CustomLongHairA = new WWW("file:///" + Application.streamingAssetsPath + "/CustomLongHairA.png");
		yield return CustomDrills;
		WWW CustomLongHairB = new WWW("file:///" + Application.streamingAssetsPath + "/CustomLongHairB.png");
		yield return CustomDrills;
		WWW CustomLongHairC = new WWW("file:///" + Application.streamingAssetsPath + "/CustomLongHairC.png");
		yield return CustomDrills;
		if (CustomLongHairA.error == null && CustomLongHairB.error == null && CustomLongHairC.error == null)
		{
			this.LongHairRenderer.materials[0].mainTexture = CustomLongHairA.texture;
			this.LongHairRenderer.materials[1].mainTexture = CustomLongHairB.texture;
			this.LongHairRenderer.materials[2].mainTexture = CustomLongHairC.texture;
		}
		yield break;
	}

	// Token: 0x06001D54 RID: 7508 RVA: 0x0016BC8C File Offset: 0x00169E8C
	public void WearGloves()
	{
		if (this.Bloodiness > 0f && !this.Gloves.Blood.enabled)
		{
			this.Gloves.PickUp.Evidence = true;
			this.Gloves.Blood.enabled = true;
			this.Police.BloodyClothing++;
		}
		if (this.Gloves.Blood.enabled)
		{
			this.GloveBlood = 1;
		}
		this.Gloved = true;
		this.GloveAttacher.newRenderer.enabled = true;
	}

	// Token: 0x06001D55 RID: 7509 RVA: 0x0016BD20 File Offset: 0x00169F20
	private void AttackOnTitan()
	{
		this.PantyAttacher.newRenderer.enabled = false;
		this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0f);
		this.MyRenderer.materials[1].SetFloat("_BlendAmount", 0f);
		this.MusicCredit.SongLabel.text = "Now Playing: This Is My Choice";
		this.MusicCredit.BandLabel.text = "By: The Kira Justice";
		this.MusicCredit.Panel.enabled = true;
		this.MusicCredit.Slide = true;
		this.EasterEggMenu.SetActive(false);
		this.Egg = true;
		this.MyRenderer.sharedMesh = this.Uniforms[1];
		this.MyRenderer.materials[0].mainTexture = this.TitanTexture;
		this.MyRenderer.materials[1].mainTexture = this.TitanTexture;
		this.MyRenderer.materials[2].mainTexture = this.FaceTexture;
		this.Outline.h.ReinitMaterials();
	}

	// Token: 0x06001D56 RID: 7510 RVA: 0x0016BE3C File Offset: 0x0016A03C
	private void KON()
	{
		this.MyRenderer.sharedMesh = this.Uniforms[4];
		this.MyRenderer.materials[0].mainTexture = this.KONTexture;
		this.MyRenderer.materials[1].mainTexture = this.KONTexture;
		this.MyRenderer.materials[2].mainTexture = this.FaceTexture;
		this.Outline.h.ReinitMaterials();
	}

	// Token: 0x06001D57 RID: 7511 RVA: 0x0016BEB4 File Offset: 0x0016A0B4
	private void Punish()
	{
		this.PunishedShader = Shader.Find("Toon/Cutoff");
		this.PantyAttacher.newRenderer.enabled = false;
		this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0f);
		this.MyRenderer.materials[1].SetFloat("_BlendAmount", 0f);
		this.EasterEggMenu.SetActive(false);
		this.Egg = true;
		this.PunishedAccessories.SetActive(true);
		this.PunishedScarf.SetActive(true);
		this.EyepatchL.SetActive(false);
		this.EyepatchR.SetActive(false);
		this.ID = 0;
		while (this.ID < this.PunishedArm.Length)
		{
			this.PunishedArm[this.ID].SetActive(true);
			this.ID++;
		}
		this.MyRenderer.sharedMesh = this.PunishedMesh;
		this.MyRenderer.materials[0].mainTexture = this.PunishedTextures[1];
		this.MyRenderer.materials[1].mainTexture = this.PunishedTextures[1];
		this.MyRenderer.materials[2].mainTexture = this.PunishedTextures[0];
		this.MyRenderer.materials[1].shader = this.PunishedShader;
		this.MyRenderer.materials[1].SetFloat("_Shininess", 1f);
		this.MyRenderer.materials[1].SetFloat("_ShadowThreshold", 0f);
		this.MyRenderer.materials[1].SetFloat("_Cutoff", 0.9f);
		this.MyRenderer.materials[1].color = new Color(1f, 1f, 1f, 1f);
		this.Outline.h.ReinitMaterials();
	}

	// Token: 0x06001D58 RID: 7512 RVA: 0x0016C0A0 File Offset: 0x0016A2A0
	private void Hate()
	{
		this.PantyAttacher.newRenderer.enabled = false;
		this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0f);
		this.MyRenderer.materials[1].SetFloat("_BlendAmount", 0f);
		this.MyRenderer.sharedMesh = this.Uniforms[1];
		this.MyRenderer.materials[0].mainTexture = this.HatefulUniform;
		this.MyRenderer.materials[1].mainTexture = this.HatefulUniform;
		this.MyRenderer.materials[2].mainTexture = this.FaceTexture;
		RenderSettings.skybox = this.HatefulSkybox;
		this.SelectGrayscale.desaturation = 1f;
		this.HeartRate.gameObject.SetActive(false);
		this.Sanity = 0f;
		this.Hairstyle = 15;
		this.UpdateHair();
		this.EasterEggMenu.SetActive(false);
		this.Egg = true;
	}

	// Token: 0x06001D59 RID: 7513 RVA: 0x0016C1AC File Offset: 0x0016A3AC
	private void Sukeban()
	{
		this.IdleAnim = "f02_idle_00";
		this.SukebanAccessories.SetActive(true);
		this.MyRenderer.sharedMesh = this.Uniforms[1];
		this.MyRenderer.materials[1].mainTexture = this.SukebanBandages;
		this.MyRenderer.materials[0].mainTexture = this.SukebanUniform;
		this.MyRenderer.materials[2].mainTexture = this.FaceTexture;
		this.EasterEggMenu.SetActive(false);
		this.Egg = true;
	}

	// Token: 0x06001D5A RID: 7514 RVA: 0x0016C240 File Offset: 0x0016A440
	private void Bancho()
	{
		this.PantyAttacher.newRenderer.enabled = false;
		this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0f);
		this.MyRenderer.materials[1].SetFloat("_BlendAmount", 0f);
		this.BanchoCamera.SetActive(true);
		this.MotionObject.enabled = true;
		this.MotionBlur.enabled = true;
		this.BanchoAccessories[0].SetActive(true);
		this.BanchoAccessories[1].SetActive(true);
		this.BanchoAccessories[2].SetActive(true);
		this.BanchoAccessories[3].SetActive(true);
		this.BanchoAccessories[4].SetActive(true);
		this.BanchoAccessories[5].SetActive(true);
		this.BanchoAccessories[6].SetActive(true);
		this.BanchoAccessories[7].SetActive(true);
		this.BanchoAccessories[8].SetActive(true);
		this.Laugh1 = this.BanchoYanYan;
		this.Laugh2 = this.BanchoYanYan;
		this.Laugh3 = this.BanchoYanYan;
		this.Laugh4 = this.BanchoYanYan;
		this.IdleAnim = "f02_banchoIdle_00";
		this.WalkAnim = "f02_banchoWalk_00";
		this.RunAnim = "f02_banchoSprint_00";
		this.OriginalIdleAnim = this.IdleAnim;
		this.OriginalWalkAnim = this.WalkAnim;
		this.OriginalRunAnim = this.RunAnim;
		this.RunSpeed *= 2f;
		this.BanchoPants.SetActive(true);
		this.MyRenderer.sharedMesh = this.BanchoMesh;
		this.MyRenderer.materials[0].mainTexture = this.BanchoFace;
		this.MyRenderer.materials[1].mainTexture = this.BanchoBody;
		this.MyRenderer.materials[2].mainTexture = this.BanchoBody;
		this.BanchoActive = true;
		this.TheDebugMenuScript.UpdateCensor();
		this.Character.transform.localPosition = new Vector3(0f, 0.04f, 0f);
		this.Hairstyle = 0;
		this.UpdateHair();
		this.EasterEggMenu.SetActive(false);
		this.Egg = true;
	}

	// Token: 0x06001D5B RID: 7515 RVA: 0x0016C47C File Offset: 0x0016A67C
	private void Slend()
	{
		this.PantyAttacher.newRenderer.enabled = false;
		this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0f);
		this.MyRenderer.materials[1].SetFloat("_BlendAmount", 0f);
		RenderSettings.skybox = this.SlenderSkybox;
		this.SelectGrayscale.desaturation = 0.5f;
		this.SelectGrayscale.enabled = true;
		this.EasterEggMenu.SetActive(false);
		this.Slender = true;
		this.Egg = true;
		this.Hairstyle = 0;
		this.UpdateHair();
		this.SlenderHair[0].transform.parent.gameObject.SetActive(true);
		this.SlenderHair[0].SetActive(true);
		this.SlenderHair[1].SetActive(true);
		this.RightYandereEye.gameObject.SetActive(false);
		this.LeftYandereEye.gameObject.SetActive(false);
		this.Character.transform.localPosition = new Vector3(this.Character.transform.localPosition.x, 0.822f, this.Character.transform.localPosition.z);
		this.MyRenderer.sharedMesh = this.Uniforms[1];
		this.MyRenderer.materials[0].mainTexture = this.SlenderUniform;
		this.MyRenderer.materials[1].mainTexture = this.SlenderUniform;
		this.MyRenderer.materials[2].mainTexture = this.SlenderSkin;
		this.Sanity = 0f;
	}

	// Token: 0x06001D5C RID: 7516 RVA: 0x0016C628 File Offset: 0x0016A828
	private void X()
	{
		this.PantyAttacher.newRenderer.enabled = false;
		this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0f);
		this.MyRenderer.materials[1].SetFloat("_BlendAmount", 0f);
		this.Xtan = true;
		this.Egg = true;
		this.Hairstyle = 9;
		this.UpdateHair();
		this.BlackEyePatch.SetActive(true);
		this.XSclera.SetActive(true);
		this.XEye.SetActive(true);
		this.Schoolwear = 2;
		this.ChangeSchoolwear();
		this.CanMove = true;
		this.MyRenderer.materials[0].mainTexture = this.XBody;
		this.MyRenderer.materials[1].mainTexture = this.XBody;
		this.MyRenderer.materials[2].mainTexture = this.XFace;
	}

	// Token: 0x06001D5D RID: 7517 RVA: 0x0016C71C File Offset: 0x0016A91C
	private void GaloSengen()
	{
		this.PantyAttacher.newRenderer.enabled = false;
		this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0f);
		this.MyRenderer.materials[1].SetFloat("_BlendAmount", 0f);
		this.IdleAnim = "f02_gruntIdle_00";
		this.EasterEggMenu.SetActive(false);
		this.Egg = true;
		this.ID = 0;
		while (this.ID < this.GaloAccessories.Length)
		{
			this.GaloAccessories[this.ID].SetActive(true);
			this.ID++;
		}
		this.MyRenderer.sharedMesh = this.Uniforms[1];
		this.MyRenderer.materials[0].mainTexture = this.UniformTextures[1];
		this.MyRenderer.materials[1].mainTexture = this.GaloArms;
		this.MyRenderer.materials[2].mainTexture = this.GaloFace;
		this.Hairstyle = 14;
		this.UpdateHair();
	}

	// Token: 0x06001D5E RID: 7518 RVA: 0x0016C838 File Offset: 0x0016AA38
	public void Jojo()
	{
		this.ShoulderCamera.LastPosition = this.ShoulderCamera.transform.position;
		this.ShoulderCamera.Summoning = true;
		this.RPGCamera.enabled = false;
		AudioSource.PlayClipAtPoint(this.SummonStand, base.transform.position);
		this.IdleAnim = "f02_jojoPose_00";
		this.WalkAnim = "f02_jojoWalk_00";
		this.EasterEggMenu.SetActive(false);
		this.CanMove = false;
		this.Egg = true;
		this.CharacterAnimation.CrossFade("f02_summonStand_00");
		this.Laugh1 = this.YanYan;
		this.Laugh2 = this.YanYan;
		this.Laugh3 = this.YanYan;
		this.Laugh4 = this.YanYan;
	}

	// Token: 0x06001D5F RID: 7519 RVA: 0x0016C900 File Offset: 0x0016AB00
	private void Agent()
	{
		this.PantyAttacher.newRenderer.enabled = false;
		this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0f);
		this.MyRenderer.materials[1].SetFloat("_BlendAmount", 0f);
		this.MyRenderer.sharedMesh = this.Uniforms[4];
		this.MyRenderer.materials[0].mainTexture = this.AgentSuit;
		this.MyRenderer.materials[1].mainTexture = this.AgentSuit;
		this.MyRenderer.materials[2].mainTexture = this.AgentFace;
		this.EasterEggMenu.SetActive(false);
		this.Egg = true;
		this.Hairstyle = 0;
		this.UpdateHair();
	}

	// Token: 0x06001D60 RID: 7520 RVA: 0x0016C9D4 File Offset: 0x0016ABD4
	private void Cirno()
	{
		this.PantyAttacher.newRenderer.enabled = false;
		this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0f);
		this.MyRenderer.materials[1].SetFloat("_BlendAmount", 0f);
		this.MyRenderer.sharedMesh = this.Uniforms[3];
		this.MyRenderer.materials[0].mainTexture = this.CirnoUniform;
		this.MyRenderer.materials[1].mainTexture = this.CirnoUniform;
		this.MyRenderer.materials[2].mainTexture = this.CirnoFace;
		this.CirnoWings.SetActive(true);
		this.CirnoHair.SetActive(true);
		this.IdleAnim = "f02_cirnoIdle_00";
		this.WalkAnim = "f02_cirnoWalk_00";
		this.RunAnim = "f02_cirnoRun_00";
		this.EasterEggMenu.SetActive(false);
		this.Stance.Current = StanceType.Standing;
		this.Uncrouch();
		this.Egg = true;
		this.Hairstyle = 0;
		this.UpdateHair();
	}

	// Token: 0x06001D61 RID: 7521 RVA: 0x0016CAF0 File Offset: 0x0016ACF0
	private void Falcon()
	{
		this.MyRenderer.sharedMesh = this.NudeMesh;
		this.PantyAttacher.newRenderer.enabled = false;
		this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0f);
		this.MyRenderer.materials[1].SetFloat("_BlendAmount", 0f);
		this.MyRenderer.materials[0].mainTexture = this.FalconFace;
		this.MyRenderer.materials[1].mainTexture = this.FalconBody;
		this.MyRenderer.materials[2].mainTexture = this.FalconBody;
		this.FalconShoulderpad.SetActive(true);
		this.FalconKneepad1.SetActive(true);
		this.FalconKneepad2.SetActive(true);
		this.FalconBuckle.SetActive(true);
		this.FalconHelmet.SetActive(true);
		this.CharacterAnimation[this.RunAnim].speed = 5f;
		this.IdleAnim = "f02_falconIdle_00";
		this.RunSpeed *= 5f;
		this.Egg = true;
		this.Hairstyle = 3;
		this.UpdateHair();
		this.DebugMenu.transform.parent.GetComponent<DebugMenuScript>().UpdateCensor();
	}

	// Token: 0x06001D62 RID: 7522 RVA: 0x0016CC44 File Offset: 0x0016AE44
	private void Punch()
	{
		this.MusicCredit.SongLabel.text = "Now Playing: Unknown Hero";
		this.MusicCredit.BandLabel.text = "By: The Kira Justice";
		this.MusicCredit.Panel.enabled = true;
		this.MusicCredit.Slide = true;
		this.MyRenderer.sharedMesh = this.SchoolSwimsuit;
		this.PantyAttacher.newRenderer.enabled = false;
		this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0f);
		this.MyRenderer.materials[1].SetFloat("_BlendAmount", 0f);
		this.MyRenderer.materials[0].mainTexture = this.SaitamaSuit;
		this.MyRenderer.materials[1].mainTexture = this.SaitamaSuit;
		this.MyRenderer.materials[2].mainTexture = this.FaceTexture;
		this.EasterEggMenu.SetActive(false);
		this.Barcode.SetActive(false);
		this.Cape.SetActive(true);
		this.Egg = true;
		this.Hairstyle = 0;
		this.UpdateHair();
		this.DebugMenu.transform.parent.GetComponent<DebugMenuScript>().UpdateCensor();
	}

	// Token: 0x06001D63 RID: 7523 RVA: 0x0016CD8C File Offset: 0x0016AF8C
	private void BadTime()
	{
		this.MyRenderer.sharedMesh = this.Jersey;
		this.PantyAttacher.newRenderer.enabled = false;
		this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0f);
		this.MyRenderer.materials[1].SetFloat("_BlendAmount", 0f);
		this.MyRenderer.materials[0].mainTexture = this.SansFace;
		this.MyRenderer.materials[1].mainTexture = this.SansTexture;
		this.MyRenderer.materials[2].mainTexture = this.SansTexture;
		this.EasterEggMenu.SetActive(false);
		this.IdleAnim = "f02_sansIdle_00";
		this.WalkAnim = "f02_sansWalk_00";
		this.RunAnim = "f02_sansRun_00";
		this.StudentManager.BadTime();
		this.Barcode.SetActive(false);
		this.Sans = true;
		this.Egg = true;
		this.Hairstyle = 0;
		this.UpdateHair();
		this.DebugMenu.transform.parent.GetComponent<DebugMenuScript>().EasterEggCheck();
	}

	// Token: 0x06001D64 RID: 7524 RVA: 0x0016CEB4 File Offset: 0x0016B0B4
	private void CyborgNinja()
	{
		this.PantyAttacher.newRenderer.enabled = false;
		this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0f);
		this.MyRenderer.materials[1].SetFloat("_BlendAmount", 0f);
		this.EnergySword.SetActive(true);
		this.IdleAnim = "CyborgNinja_Idle_Unarmed";
		this.RunAnim = "CyborgNinja_Run_Unarmed";
		this.MyRenderer.sharedMesh = this.NudeMesh;
		this.MyRenderer.materials[0].mainTexture = this.CyborgFace;
		this.MyRenderer.materials[1].mainTexture = this.CyborgBody;
		this.MyRenderer.materials[2].mainTexture = this.CyborgBody;
		this.Schoolwear = 0;
		this.ID = 1;
		while (this.ID < this.CyborgParts.Length)
		{
			this.CyborgParts[this.ID].SetActive(true);
			this.ID++;
		}
		this.ID = 1;
		while (this.ID < this.StudentManager.Students.Length)
		{
			StudentScript studentScript = this.StudentManager.Students[this.ID];
			if (studentScript != null)
			{
				studentScript.Teacher = false;
			}
			this.ID++;
		}
		this.RunSpeed *= 2f;
		this.EyewearID = 6;
		this.Hairstyle = 45;
		this.UpdateHair();
		this.Ninja = true;
		this.Egg = true;
		this.DebugMenu.transform.parent.GetComponent<DebugMenuScript>().UpdateCensor();
	}

	// Token: 0x06001D65 RID: 7525 RVA: 0x0016D064 File Offset: 0x0016B264
	private void Ebola()
	{
		this.StudentManager.Ebola = true;
		this.PantyAttacher.newRenderer.enabled = false;
		this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0f);
		this.MyRenderer.materials[1].SetFloat("_BlendAmount", 0f);
		this.IdleAnim = "f02_ebolaIdle_00";
		this.MyRenderer.sharedMesh = this.Uniforms[1];
		this.MyRenderer.materials[0].mainTexture = this.EbolaUniform;
		this.MyRenderer.materials[1].mainTexture = this.EbolaUniform;
		this.MyRenderer.materials[2].mainTexture = this.EbolaFace;
		this.Hairstyle = 0;
		this.UpdateHair();
		this.EbolaWings.SetActive(true);
		this.EbolaHair.SetActive(true);
		this.Egg = true;
	}

	// Token: 0x06001D66 RID: 7526 RVA: 0x0016D158 File Offset: 0x0016B358
	private void Long()
	{
		this.MyRenderer.sharedMesh = this.LongUniform;
	}

	// Token: 0x06001D67 RID: 7527 RVA: 0x0016D16C File Offset: 0x0016B36C
	private void SwapMesh()
	{
		this.MyRenderer.sharedMesh = this.NewMesh;
		this.MyRenderer.materials[0].mainTexture = this.TextureToUse;
		this.MyRenderer.materials[1].mainTexture = this.NewFace;
		this.MyRenderer.materials[2].mainTexture = this.TextureToUse;
		this.RightYandereEye.gameObject.SetActive(false);
		this.LeftYandereEye.gameObject.SetActive(false);
	}

	// Token: 0x06001D68 RID: 7528 RVA: 0x0016D1F4 File Offset: 0x0016B3F4
	private void Nude()
	{
		Debug.Log("Making Yandere-chan nude.");
		this.MyRenderer.sharedMesh = this.NudeMesh;
		this.MyRenderer.materials[0].mainTexture = this.FaceTexture;
		this.MyRenderer.materials[1].mainTexture = this.NudeTexture;
		this.ID = 0;
		while (this.ID < this.CensorSteam.Length)
		{
			this.ID++;
		}
		this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0f);
		this.MyRenderer.materials[1].SetFloat("_BlendAmount", 0f);
		this.MyRenderer.materials[0].SetFloat("_BlendAmount1", 0f);
		this.MyRenderer.materials[1].SetFloat("_BlendAmount1", 0f);
		this.EasterEggMenu.SetActive(false);
		this.ClubAttire = false;
		this.Schoolwear = 0;
		this.ClubAccessory();
	}

	// Token: 0x06001D69 RID: 7529 RVA: 0x0016D304 File Offset: 0x0016B504
	private void Samus()
	{
		this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0f);
		this.MyRenderer.materials[1].SetFloat("_BlendAmount", 0f);
		this.MyRenderer.sharedMesh = this.NudeMesh;
		this.MyRenderer.materials[0].mainTexture = this.SamusFace;
		this.MyRenderer.materials[1].mainTexture = this.SamusBody;
		this.PantyAttacher.newRenderer.enabled = false;
		this.Schoolwear = 0;
		this.PonytailRenderer.material.mainTexture = this.SamusFace;
		this.Egg = true;
		this.DebugMenu.transform.parent.GetComponent<DebugMenuScript>().UpdateCensor();
	}

	// Token: 0x06001D6A RID: 7530 RVA: 0x0016D3DC File Offset: 0x0016B5DC
	private void Witch()
	{
		this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0f);
		this.MyRenderer.materials[1].SetFloat("_BlendAmount", 0f);
		this.MyRenderer.sharedMesh = this.NudeMesh;
		this.MyRenderer.materials[0].mainTexture = this.WitchFace;
		this.MyRenderer.materials[1].mainTexture = this.WitchBody;
		this.PantyAttacher.newRenderer.enabled = false;
		this.Schoolwear = 0;
		this.IdleAnim = "f02_idleElegant_00";
		this.WalkAnim = "f02_jojoWalk_00";
		this.WitchMode = true;
		this.Egg = true;
		this.Hairstyle = 100;
		this.UpdateHair();
		this.DebugMenu.transform.parent.GetComponent<DebugMenuScript>().UpdateCensor();
	}

	// Token: 0x06001D6B RID: 7531 RVA: 0x0016D4C6 File Offset: 0x0016B6C6
	private void Pose()
	{
		if (!this.StudentManager.Pose)
		{
			this.StudentManager.Pose = true;
		}
		else
		{
			this.StudentManager.Pose = false;
		}
		this.StudentManager.UpdateStudents(0);
	}

	// Token: 0x06001D6C RID: 7532 RVA: 0x0016D4FB File Offset: 0x0016B6FB
	private void HairBlades()
	{
		this.Hairstyle = 0;
		this.UpdateHair();
		this.BladeHair.SetActive(true);
		this.Egg = true;
	}

	// Token: 0x06001D6D RID: 7533 RVA: 0x0016D520 File Offset: 0x0016B720
	private void Tornado()
	{
		this.Hairstyle = 0;
		this.UpdateHair();
		this.IdleAnim = "f02_tornadoIdle_00";
		this.WalkAnim = "f02_tornadoWalk_00";
		this.RunAnim = "f02_tornadoRun_00";
		this.TornadoHair.SetActive(true);
		this.TornadoDress.SetActive(true);
		this.RiggedAccessory.SetActive(true);
		this.MyRenderer.sharedMesh = this.NoTorsoMesh;
		this.PantyAttacher.newRenderer.enabled = false;
		this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0f);
		this.MyRenderer.materials[1].SetFloat("_BlendAmount", 0f);
		this.Sanity = 100f;
		this.MyRenderer.materials[0].mainTexture = this.FaceTexture;
		this.MyRenderer.materials[1].mainTexture = this.NudePanties;
		this.MyRenderer.materials[2].mainTexture = this.NudePanties;
		this.TheDebugMenuScript.UpdateCensor();
		this.Stance.Current = StanceType.Standing;
		this.Egg = true;
	}

	// Token: 0x06001D6E RID: 7534 RVA: 0x0016D64C File Offset: 0x0016B84C
	private void GenderSwap()
	{
		this.Kun.SetActive(true);
		this.KunHair.SetActive(true);
		this.MyRenderer.enabled = false;
		this.PantyAttacher.newRenderer.enabled = false;
		this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0f);
		this.MyRenderer.materials[1].SetFloat("_BlendAmount", 0f);
		this.IdleAnim = "idleShort_00";
		this.WalkAnim = "walk_00";
		this.RunAnim = "newSprint_00";
		this.OriginalIdleAnim = this.IdleAnim;
		this.OriginalWalkAnim = this.WalkAnim;
		this.OriginalRunAnim = this.RunAnim;
		this.Hairstyle = 0;
		this.UpdateHair();
	}

	// Token: 0x06001D6F RID: 7535 RVA: 0x0016D718 File Offset: 0x0016B918
	private void Mandere()
	{
		this.Man.SetActive(true);
		this.Barcode.SetActive(false);
		this.MyRenderer.enabled = false;
		this.PantyAttacher.newRenderer.enabled = false;
		this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0f);
		this.MyRenderer.materials[1].SetFloat("_BlendAmount", 0f);
		this.RightYandereEye.gameObject.SetActive(false);
		this.LeftYandereEye.gameObject.SetActive(false);
		this.IdleAnim = "idleShort_00";
		this.WalkAnim = "walk_00";
		this.RunAnim = "newSprint_00";
		this.OriginalIdleAnim = this.IdleAnim;
		this.OriginalWalkAnim = this.WalkAnim;
		this.OriginalRunAnim = this.RunAnim;
		this.Hairstyle = 0;
		this.UpdateHair();
	}

	// Token: 0x06001D70 RID: 7536 RVA: 0x0016D808 File Offset: 0x0016BA08
	private void BlackHoleChan()
	{
		this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0f);
		this.MyRenderer.materials[1].SetFloat("_BlendAmount", 0f);
		this.MyRenderer.sharedMesh = this.NudeMesh;
		this.MyRenderer.materials[0].mainTexture = this.BlackHoleFace;
		this.MyRenderer.materials[1].mainTexture = this.Black;
		this.MyRenderer.materials[2].mainTexture = this.Black;
		this.PantyAttacher.newRenderer.enabled = false;
		this.Schoolwear = 0;
		this.IdleAnim = "f02_gazerIdle_00";
		this.WalkAnim = "f02_gazerWalk_00";
		this.RunAnim = "f02_gazerRun_00";
		this.OriginalIdleAnim = this.IdleAnim;
		this.OriginalWalkAnim = this.WalkAnim;
		this.OriginalRunAnim = this.RunAnim;
		this.Hairstyle = 182;
		this.UpdateHair();
		this.BlackHoleEffects.SetActive(true);
		this.BlackHole = true;
		this.Egg = true;
		this.DebugMenu.transform.parent.GetComponent<DebugMenuScript>().UpdateCensor();
	}

	// Token: 0x06001D71 RID: 7537 RVA: 0x0016D948 File Offset: 0x0016BB48
	private void ElfenLied()
	{
		this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0f);
		this.MyRenderer.materials[1].SetFloat("_BlendAmount", 0f);
		this.MyRenderer.sharedMesh = this.NudeMesh;
		this.MyRenderer.materials[0].mainTexture = this.FaceTexture;
		this.MyRenderer.materials[1].mainTexture = this.NudeTexture;
		this.MyRenderer.materials[2].mainTexture = this.NudeTexture;
		GameObject[] vectors = this.Vectors;
		for (int i = 0; i < vectors.Length; i++)
		{
			vectors[i].SetActive(true);
		}
		this.IdleAnim = "f02_sixIdle_00";
		this.WalkAnim = "f02_sixWalk_00";
		this.RunAnim = "f02_sixRun_00";
		this.OriginalIdleAnim = this.IdleAnim;
		this.OriginalWalkAnim = this.WalkAnim;
		this.OriginalRunAnim = this.RunAnim;
		this.LucyHelmet.SetActive(true);
		this.Bandages.SetActive(true);
		this.Egg = true;
		this.WalkSpeed = 0.75f;
		this.RunSpeed = 2f;
		this.Hairstyle = 0;
		this.UpdateHair();
		this.DebugMenu.transform.parent.GetComponent<DebugMenuScript>().UpdateCensor();
	}

	// Token: 0x06001D72 RID: 7538 RVA: 0x0016DAA8 File Offset: 0x0016BCA8
	private void Ghoul()
	{
		this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0f);
		this.MyRenderer.materials[1].SetFloat("_BlendAmount", 0f);
		this.MyRenderer.sharedMesh = this.NudeMesh;
		this.MyRenderer.materials[0].mainTexture = this.GhoulFace;
		this.MyRenderer.materials[1].mainTexture = this.GhoulBody;
		this.MyRenderer.materials[2].mainTexture = this.GhoulBody;
		GameObject[] kagune = this.Kagune;
		for (int i = 0; i < kagune.Length; i++)
		{
			kagune[i].SetActive(true);
		}
		this.IdleAnim = "f02_sixIdle_00";
		this.WalkAnim = "f02_sixWalk_00";
		this.RunAnim = "f02_psychoRun_00";
		this.OriginalIdleAnim = this.IdleAnim;
		this.OriginalWalkAnim = this.WalkAnim;
		this.OriginalRunAnim = this.RunAnim;
		this.StudentManager.Six = true;
		this.StudentManager.UpdateStudents(0);
		this.Egg = true;
		this.WalkSpeed = 0.75f;
		this.RunSpeed = 10f;
		this.Hairstyle = 15;
		this.UpdateHair();
		this.DebugMenu.transform.parent.GetComponent<DebugMenuScript>().UpdateCensor();
	}

	// Token: 0x06001D73 RID: 7539 RVA: 0x0016DC08 File Offset: 0x0016BE08
	private void Berserk()
	{
		SchoolGlobals.SchoolAtmosphere = 0.5f;
		this.StudentManager.SetAtmosphere();
		GameObject[] armor = this.Armor;
		for (int i = 0; i < armor.Length; i++)
		{
			armor[i].SetActive(true);
		}
		Renderer[] trees = this.StudentManager.Trees;
		for (int i = 0; i < trees.Length; i++)
		{
			trees[i].materials[1] = this.Trans;
		}
		this.SithSpawnTime = this.NierSpawnTime;
		this.SithHardSpawnTime1 = this.NierHardSpawnTime;
		this.SithHardSpawnTime2 = this.NierHardSpawnTime;
		this.SithAudio.clip = this.NierSwoosh;
		this.MyRenderer.sharedMesh = this.NudeMesh;
		this.MyRenderer.materials[0].mainTexture = this.Scarface;
		this.MyRenderer.materials[1].mainTexture = this.Chainmail;
		this.MyRenderer.materials[2].mainTexture = this.Chainmail;
		this.PantyAttacher.newRenderer.enabled = false;
		this.Schoolwear = 0;
		this.TheDebugMenuScript.UpdateCensor();
		this.IdleAnim = "f02_heroicIdle_00";
		this.WalkAnim = "f02_walkConfident_00";
		this.RunAnim = "f02_nierRun_00";
		this.CharacterAnimation["f02_nierRun_00"].speed = 1f;
		this.CharacterAnimation["f02_gutsEye_00"].weight = 1f;
		this.RunSpeed = 7.5f;
		this.OriginalIdleAnim = this.IdleAnim;
		this.OriginalWalkAnim = this.WalkAnim;
		this.OriginalRunAnim = this.RunAnim;
		this.Hairstyle = 188;
		this.UpdateHair();
		this.Egg = true;
	}

	// Token: 0x06001D74 RID: 7540 RVA: 0x0016DDC0 File Offset: 0x0016BFC0
	private void Sith()
	{
		this.Hairstyle = 67;
		this.UpdateHair();
		this.SithTrail1.SetActive(true);
		this.SithTrail2.SetActive(true);
		this.IdleAnim = "f02_sithIdle_00";
		this.WalkAnim = "f02_sithWalk_00";
		this.RunAnim = "f02_sithRun_00";
		this.BlackRobe.SetActive(true);
		this.MyRenderer.sharedMesh = this.NoUpperBodyMesh;
		this.MyRenderer.materials[0].mainTexture = this.NudePanties;
		this.MyRenderer.materials[1].mainTexture = this.FaceTexture;
		this.MyRenderer.materials[2].mainTexture = this.NudePanties;
		this.Stance.Current = StanceType.Standing;
		this.FollowHips = true;
		this.SithLord = true;
		this.Egg = true;
		this.TheDebugMenuScript.UpdateCensor();
		this.PantyAttacher.newRenderer.enabled = false;
		this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0f);
		this.MyRenderer.materials[1].SetFloat("_BlendAmount", 0f);
		this.RunSpeed *= 2f;
		this.Zoom.TargetZoom = 0.4f;
	}

	// Token: 0x06001D75 RID: 7541 RVA: 0x0016DF10 File Offset: 0x0016C110
	private void Snake()
	{
		this.PantyAttacher.newRenderer.enabled = false;
		this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0f);
		this.MyRenderer.materials[1].SetFloat("_BlendAmount", 0f);
		this.MyRenderer.sharedMesh = this.Uniforms[1];
		this.MyRenderer.materials[0].mainTexture = this.SnakeBody;
		this.MyRenderer.materials[1].mainTexture = this.SnakeBody;
		this.MyRenderer.materials[2].mainTexture = this.SnakeFace;
		this.Hairstyle = 161;
		this.UpdateHair();
		this.Medusa = true;
		this.Egg = true;
	}

	// Token: 0x06001D76 RID: 7542 RVA: 0x0016DFE0 File Offset: 0x0016C1E0
	private void Gazer()
	{
		this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0f);
		this.MyRenderer.materials[1].SetFloat("_BlendAmount", 0f);
		this.GazerEyes.gameObject.SetActive(true);
		this.MyRenderer.sharedMesh = this.NudeMesh;
		this.MyRenderer.materials[0].mainTexture = this.GazerFace;
		this.MyRenderer.materials[1].mainTexture = this.GazerBody;
		this.MyRenderer.materials[2].mainTexture = this.GazerBody;
		this.PantyAttacher.newRenderer.enabled = false;
		this.Schoolwear = 0;
		this.IdleAnim = "f02_gazerIdle_00";
		this.WalkAnim = "f02_gazerWalk_00";
		this.RunAnim = "f02_gazerRun_00";
		this.OriginalIdleAnim = this.IdleAnim;
		this.OriginalWalkAnim = this.WalkAnim;
		this.OriginalRunAnim = this.RunAnim;
		this.Hairstyle = 158;
		this.UpdateHair();
		this.StudentManager.Gaze = true;
		this.StudentManager.UpdateStudents(0);
		this.Gazing = true;
		this.Egg = true;
		this.DebugMenu.transform.parent.GetComponent<DebugMenuScript>().UpdateCensor();
	}

	// Token: 0x06001D77 RID: 7543 RVA: 0x0016E140 File Offset: 0x0016C340
	private void Six()
	{
		RenderSettings.skybox = this.HatefulSkybox;
		this.Hairstyle = 0;
		this.UpdateHair();
		this.IdleAnim = "f02_sixIdle_00";
		this.WalkAnim = "f02_sixWalk_00";
		this.RunAnim = "f02_sixRun_00";
		this.OriginalIdleAnim = this.IdleAnim;
		this.OriginalWalkAnim = this.WalkAnim;
		this.OriginalRunAnim = this.RunAnim;
		this.SixRaincoat.SetActive(true);
		this.MyRenderer.sharedMesh = this.SixBodyMesh;
		this.PantyAttacher.newRenderer.enabled = false;
		this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0f);
		this.MyRenderer.materials[1].SetFloat("_BlendAmount", 0f);
		this.MyRenderer.materials[0].mainTexture = this.SixFaceTexture;
		this.MyRenderer.materials[1].mainTexture = this.NudeTexture;
		this.MyRenderer.materials[2].mainTexture = this.NudeTexture;
		this.TheDebugMenuScript.UpdateCensor();
		SchoolGlobals.SchoolAtmosphere = 0f;
		this.StudentManager.SetAtmosphere();
		this.StudentManager.Six = true;
		this.StudentManager.UpdateStudents(0);
		this.WalkSpeed = 0.75f;
		this.RunSpeed = 2f;
		this.Hungry = true;
		this.Egg = true;
	}

	// Token: 0x06001D78 RID: 7544 RVA: 0x0016E2B4 File Offset: 0x0016C4B4
	private void KLK()
	{
		this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0f);
		this.MyRenderer.materials[1].SetFloat("_BlendAmount", 0f);
		this.KLKSword.SetActive(true);
		this.IdleAnim = "f02_heroicIdle_00";
		this.WalkAnim = "f02_walkConfident_00";
		this.MyRenderer.sharedMesh = this.NudeMesh;
		this.MyRenderer.materials[0].mainTexture = this.KLKFace;
		this.MyRenderer.materials[1].mainTexture = this.KLKBody;
		this.MyRenderer.materials[2].mainTexture = this.KLKBody;
		this.PantyAttacher.newRenderer.enabled = false;
		this.Schoolwear = 0;
		this.ID = 0;
		while (this.ID < this.KLKParts.Length)
		{
			this.KLKParts[this.ID].SetActive(true);
			this.ID++;
		}
		this.ID = 1;
		while (this.ID < this.StudentManager.Students.Length)
		{
			StudentScript studentScript = this.StudentManager.Students[this.ID];
			if (studentScript != null)
			{
				studentScript.Teacher = false;
			}
			this.ID++;
		}
		this.Egg = true;
		this.DebugMenu.transform.parent.GetComponent<DebugMenuScript>().UpdateCensor();
	}

	// Token: 0x06001D79 RID: 7545 RVA: 0x0016E438 File Offset: 0x0016C638
	public void Miyuki()
	{
		this.MiyukiCostume.SetActive(true);
		this.MiyukiWings.SetActive(true);
		this.IdleAnim = "f02_idleGirly_00";
		this.WalkAnim = "f02_walkGirly_00";
		this.MyRenderer.sharedMesh = this.NudeMesh;
		this.MyRenderer.materials[0].mainTexture = this.MiyukiFace;
		this.MyRenderer.materials[1].mainTexture = this.MiyukiSkin;
		this.MyRenderer.materials[2].mainTexture = this.MiyukiSkin;
		this.OriginalIdleAnim = this.IdleAnim;
		this.OriginalWalkAnim = this.WalkAnim;
		this.OriginalRunAnim = this.RunAnim;
		this.TheDebugMenuScript.UpdateCensor();
		this.Jukebox.MiyukiMusic();
		this.Hairstyle = 171;
		this.UpdateHair();
		this.MagicalGirl = true;
		this.Egg = true;
	}

	// Token: 0x06001D7A RID: 7546 RVA: 0x0016E528 File Offset: 0x0016C728
	public void AzurLane()
	{
		this.Schoolwear = 2;
		this.ChangeSchoolwear();
		this.PantyAttacher.newRenderer.enabled = false;
		this.IdleAnim = "f02_gazerIdle_00";
		this.WalkAnim = "f02_gazerWalk_00";
		this.RunAnim = "f02_gazerRun_00";
		this.OriginalIdleAnim = this.IdleAnim;
		this.OriginalWalkAnim = this.WalkAnim;
		this.OriginalRunAnim = this.RunAnim;
		this.AzurGuns.SetActive(true);
		this.AzurWater.SetActive(true);
		this.AzurMist.SetActive(true);
		this.Shipgirl = true;
		this.CanMove = true;
		this.Egg = true;
		this.Jukebox.Shipgirl();
	}

	// Token: 0x06001D7B RID: 7547 RVA: 0x0016E5DC File Offset: 0x0016C7DC
	public void Weather()
	{
		if (!this.Rain.activeInHierarchy)
		{
			this.StudentManager.Clock.BloomEffect.bloomIntensity = 10f;
			this.StudentManager.Clock.BloomEffect.bloomThreshhold = 0f;
			this.StudentManager.Clock.UpdateBloom = true;
			SchoolGlobals.SchoolAtmosphere = 0f;
			this.StudentManager.SetAtmosphere();
			this.Rain.SetActive(true);
			this.Jukebox.Start();
			return;
		}
		this.Hairstyle = 67;
		this.UpdateHair();
		this.Raincoat.SetActive(true);
		this.MyRenderer.sharedMesh = this.SixBodyMesh;
		this.PantyAttacher.newRenderer.enabled = false;
		this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0f);
		this.MyRenderer.materials[1].SetFloat("_BlendAmount", 0f);
		this.MyRenderer.materials[0].mainTexture = this.FaceTexture;
		this.MyRenderer.materials[1].mainTexture = this.NudeTexture;
		this.MyRenderer.materials[2].mainTexture = this.NudeTexture;
		this.TheDebugMenuScript.UpdateCensor();
	}

	// Token: 0x06001D7C RID: 7548 RVA: 0x0016E730 File Offset: 0x0016C930
	private void Horror()
	{
		this.Rain.SetActive(false);
		RenderSettings.ambientLight = new Color(0.1f, 0.1f, 0.1f);
		RenderSettings.skybox = this.HorrorSkybox;
		SchoolGlobals.SchoolAtmosphere = 0f;
		this.StudentManager.SetAtmosphere();
		this.RPGCamera.desiredDistance = 0.33333f;
		this.Zoom.OverShoulder = true;
		this.Zoom.TargetZoom = 0.2f;
		this.PauseScreen.MissionMode.FPS.transform.localPosition = new Vector3(0.998f, -3.85f, 0f);
		this.PauseScreen.MissionMode.Watermark.gameObject.SetActive(false);
		this.PauseScreen.MissionMode.Nemesis.SetActive(true);
		this.StudentManager.Clock.MainLight.color = new Color(0.5f, 0.5f, 0.5f, 1f);
		this.StudentManager.Clock.gameObject.SetActive(false);
		this.StudentManager.Clock.SunFlare.SetActive(false);
		this.StudentManager.Clock.Horror = true;
		this.StudentManager.Students[1].transform.position = new Vector3(0f, 0f, 0f);
		this.StudentManager.Headmaster.gameObject.SetActive(false);
		this.StudentManager.Reputation.gameObject.SetActive(false);
		this.StudentManager.Flashlight.gameObject.SetActive(true);
		this.StudentManager.Flashlight.BePickedUp();
		this.StudentManager.DelinquentRadio.SetActive(false);
		this.StudentManager.CounselorDoor[0].enabled = false;
		this.StudentManager.CounselorDoor[1].enabled = false;
		this.StudentManager.CounselorDoor[0].Prompt.enabled = false;
		this.StudentManager.CounselorDoor[1].Prompt.enabled = false;
		this.StudentManager.Portal.SetActive(false);
		RenderSettings.ambientLight = new Color(0.1f, 0.1f, 0.1f);
		this.ID = 1;
		while (this.ID < 101)
		{
			if (this.StudentManager.Students[this.ID] != null && this.StudentManager.Students[this.ID].gameObject.activeInHierarchy)
			{
				this.StudentManager.DisableStudent(this.ID);
			}
			this.ID++;
		}
		this.Egg = true;
	}

	// Token: 0x06001D7D RID: 7549 RVA: 0x0016E9F8 File Offset: 0x0016CBF8
	private void LifeNote()
	{
		this.ID = 1;
		while (this.ID < 101)
		{
			StudentGlobals.SetStudentPhotographed(this.ID, true);
			this.ID++;
		}
		this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0f);
		this.MyRenderer.materials[1].SetFloat("_BlendAmount", 0f);
		this.LifeNotebook.transform.position = base.transform.position + base.transform.forward + new Vector3(0f, 2.5f, 0f);
		this.LifeNotebook.GetComponent<Rigidbody>().useGravity = true;
		this.LifeNotebook.GetComponent<Rigidbody>().isKinematic = false;
		this.LifeNotebook.gameObject.SetActive(true);
		this.MyRenderer.sharedMesh = this.YamikoMesh;
		this.MyRenderer.materials[0].mainTexture = this.YamikoSkinTexture;
		this.MyRenderer.materials[1].mainTexture = this.YamikoAccessoryTexture;
		this.MyRenderer.materials[2].mainTexture = this.YamikoFaceTexture;
		this.PantyAttacher.newRenderer.enabled = false;
		this.Schoolwear = 0;
		this.Hairstyle = 180;
		this.UpdateHair();
		this.StudentManager.NoteWindow.BecomeLifeNote();
		this.Egg = true;
		this.DebugMenu.transform.parent.GetComponent<DebugMenuScript>().UpdateCensor();
	}

	// Token: 0x06001D7E RID: 7550 RVA: 0x0016EB94 File Offset: 0x0016CD94
	private void Nier()
	{
		this.NierCostume.SetActive(true);
		this.HeavySwordParent.gameObject.SetActive(true);
		this.LightSwordParent.gameObject.SetActive(true);
		this.HeavySword.GetComponent<WeaponTrail>().Start();
		this.LightSword.GetComponent<WeaponTrail>().Start();
		this.HeavySword.GetComponent<WeaponTrail>().enabled = false;
		this.LightSword.GetComponent<WeaponTrail>().enabled = false;
		this.Pod.SetActive(true);
		this.SithSpawnTime = this.NierSpawnTime;
		this.SithHardSpawnTime1 = this.NierHardSpawnTime;
		this.SithHardSpawnTime2 = this.NierHardSpawnTime;
		this.SithAudio.clip = this.NierSwoosh;
		this.Pod.transform.parent = null;
		this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0f);
		this.MyRenderer.materials[1].SetFloat("_BlendAmount", 0f);
		this.MyRenderer.sharedMesh = null;
		this.PantyAttacher.newRenderer.enabled = false;
		this.Schoolwear = 0;
		this.Hairstyle = 181;
		this.UpdateHair();
		this.Egg = true;
		this.IdleAnim = "f02_heroicIdle_00";
		this.WalkAnim = "f02_walkGraceful_00";
		this.RunAnim = "f02_nierRun_00";
		this.RunSpeed = 10f;
		this.DebugMenu.transform.parent.GetComponent<DebugMenuScript>().UpdateCensor();
	}

	// Token: 0x06001D7F RID: 7551 RVA: 0x0016ED20 File Offset: 0x0016CF20
	public void WearChinaDress()
	{
		this.EbolaHair.SetActive(false);
		this.EbolaWings.GetComponent<Renderer>().material.color = new Color(0f, 0f, 0f);
		this.EbolaWings.GetComponent<Renderer>().material.SetColor("_OutlineColor", new Color(0f, 0f, 0f));
		this.Hairstyle = 1;
		this.UpdateHair();
		this.ChinaDress.SetActive(true);
		this.Nude();
		this.PantyAttacher.newRenderer.enabled = true;
	}

	// Token: 0x06001D80 RID: 7552 RVA: 0x0016EDC0 File Offset: 0x0016CFC0
	private void Vaporwave()
	{
		this.VaporwaveVisuals.ApplyNormalView();
		RenderSettings.skybox = this.VaporwaveSkybox;
		this.PauseScreen.Settings.QualityManager.Obscurance.enabled = false;
		this.PalmTrees.SetActive(true);
		for (int i = 1; i < this.Trees.Length; i++)
		{
			this.Trees[i].SetActive(false);
		}
	}

	// Token: 0x06001D81 RID: 7553 RVA: 0x0016EE2C File Offset: 0x0016D02C
	public void ChangeSchoolwear()
	{
		this.PantyAttacher.newRenderer.enabled = false;
		this.RightFootprintSpawner.Bloodiness = 0;
		this.LeftFootprintSpawner.Bloodiness = 0;
		if (this.ClubAttire && this.Bloodiness == 0f)
		{
			this.Schoolwear = this.PreviousSchoolwear;
		}
		this.LabcoatAttacher.RemoveAccessory();
		this.Paint = false;
		this.ID = 0;
		while (this.ID < this.CensorSteam.Length)
		{
			this.CensorSteam[this.ID].SetActive(false);
			this.ID++;
		}
		if (this.Casual)
		{
			this.TextureToUse = this.UniformTextures[StudentGlobals.FemaleUniform];
		}
		else
		{
			this.TextureToUse = this.CasualTextures[StudentGlobals.FemaleUniform];
		}
		if ((this.ClubAttire && this.Bloodiness > 0f) || this.Schoolwear == 0)
		{
			this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0f);
			this.MyRenderer.materials[1].SetFloat("_BlendAmount", 0f);
			this.MyRenderer.materials[0].SetFloat("_BlendAmount1", 0f);
			this.MyRenderer.materials[1].SetFloat("_BlendAmount1", 0f);
			this.MyRenderer.sharedMesh = this.Towel;
			this.MyRenderer.materials[0].mainTexture = this.TowelTexture;
			this.MyRenderer.materials[1].mainTexture = this.TowelTexture;
			this.MyRenderer.materials[2].mainTexture = this.FaceTexture;
			this.ClubAttire = false;
			this.Schoolwear = 0;
		}
		else if (this.Schoolwear == 1)
		{
			this.PantyAttacher.newRenderer.enabled = true;
			this.MyRenderer.sharedMesh = this.Uniforms[StudentGlobals.FemaleUniform];
			this.MyRenderer.materials[0].SetFloat("_BlendAmount", 1f);
			this.MyRenderer.materials[1].SetFloat("_BlendAmount", 1f);
			if (this.StudentManager.Censor)
			{
				Debug.Log("Activating shadows on Yandere-chan.");
				this.MyRenderer.materials[0].SetFloat("_BlendAmount1", 1f);
				this.MyRenderer.materials[1].SetFloat("_BlendAmount1", 1f);
				this.PantyAttacher.newRenderer.enabled = false;
			}
			this.MyRenderer.materials[0].mainTexture = this.TextureToUse;
			this.MyRenderer.materials[1].mainTexture = this.TextureToUse;
			this.MyRenderer.materials[2].mainTexture = this.FaceTexture;
			base.StartCoroutine(this.ApplyCustomCostume());
		}
		else if (this.Schoolwear == 2)
		{
			this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0f);
			this.MyRenderer.materials[1].SetFloat("_BlendAmount", 0f);
			this.MyRenderer.materials[0].SetFloat("_BlendAmount1", 0f);
			this.MyRenderer.materials[1].SetFloat("_BlendAmount1", 0f);
			this.MyRenderer.sharedMesh = this.SchoolSwimsuit;
			this.MyRenderer.materials[0].mainTexture = this.SwimsuitTexture;
			this.MyRenderer.materials[1].mainTexture = this.SwimsuitTexture;
			this.MyRenderer.materials[2].mainTexture = this.FaceTexture;
		}
		else if (this.Schoolwear == 3)
		{
			this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0f);
			this.MyRenderer.materials[1].SetFloat("_BlendAmount", 0f);
			this.MyRenderer.materials[0].SetFloat("_BlendAmount1", 0f);
			this.MyRenderer.materials[1].SetFloat("_BlendAmount1", 0f);
			this.MyRenderer.sharedMesh = this.GymUniform;
			this.MyRenderer.materials[0].mainTexture = this.GymTexture;
			this.MyRenderer.materials[1].mainTexture = this.GymTexture;
			this.MyRenderer.materials[2].mainTexture = this.FaceTexture;
		}
		this.CanMove = false;
		this.Outline.h.ReinitMaterials();
		this.ClubAccessory();
	}

	// Token: 0x06001D82 RID: 7554 RVA: 0x0016F2EC File Offset: 0x0016D4EC
	public void ChangeClubwear()
	{
		this.PantyAttacher.newRenderer.enabled = false;
		this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0f);
		this.MyRenderer.materials[1].SetFloat("_BlendAmount", 0f);
		this.MyRenderer.materials[0].SetFloat("_BlendAmount1", 0f);
		this.MyRenderer.materials[1].SetFloat("_BlendAmount1", 0f);
		this.Paint = false;
		if (!this.ClubAttire)
		{
			this.ClubAttire = true;
			if (ClubGlobals.Club == ClubType.Art)
			{
				this.MyRenderer.sharedMesh = this.ApronMesh;
				this.MyRenderer.materials[0].mainTexture = this.ApronTexture;
				this.MyRenderer.materials[1].mainTexture = this.ApronTexture;
				this.MyRenderer.materials[2].mainTexture = this.FaceTexture;
				this.Schoolwear = 4;
				this.Paint = true;
			}
			else if (ClubGlobals.Club == ClubType.MartialArts)
			{
				this.MyRenderer.sharedMesh = this.JudoGiMesh;
				this.MyRenderer.materials[0].mainTexture = this.JudoGiTexture;
				this.MyRenderer.materials[1].mainTexture = this.JudoGiTexture;
				this.MyRenderer.materials[2].mainTexture = this.FaceTexture;
				this.Schoolwear = 5;
			}
			else if (ClubGlobals.Club == ClubType.Science)
			{
				this.LabcoatAttacher.enabled = true;
				this.MyRenderer.sharedMesh = this.HeadAndHands;
				if (this.LabcoatAttacher.Initialized)
				{
					this.LabcoatAttacher.AttachAccessory();
				}
				this.MyRenderer.materials[0].mainTexture = this.FaceTexture;
				this.MyRenderer.materials[1].mainTexture = this.NudeTexture;
				this.MyRenderer.materials[2].mainTexture = this.NudeTexture;
				this.Schoolwear = 6;
			}
		}
		else
		{
			this.ChangeSchoolwear();
			this.ClubAttire = false;
		}
		this.MyLocker.UpdateButtons();
	}

	// Token: 0x06001D83 RID: 7555 RVA: 0x0016F520 File Offset: 0x0016D720
	public void ClubAccessory()
	{
		this.ID = 0;
		while (this.ID < this.ClubAccessories.Length)
		{
			GameObject gameObject = this.ClubAccessories[this.ID];
			if (gameObject != null)
			{
				gameObject.SetActive(false);
			}
			this.ID++;
		}
		if (!this.CensorSteam[0].activeInHierarchy && ClubGlobals.Club > ClubType.None && this.ClubAccessories[(int)ClubGlobals.Club] != null)
		{
			this.ClubAccessories[(int)ClubGlobals.Club].SetActive(true);
		}
	}

	// Token: 0x06001D84 RID: 7556 RVA: 0x0016F5B0 File Offset: 0x0016D7B0
	public void StopCarrying()
	{
		if (this.Ragdoll != null)
		{
			this.Ragdoll.GetComponent<RagdollScript>().Fall();
		}
		this.HeavyWeight = false;
		this.Carrying = false;
		this.IdleAnim = this.OriginalIdleAnim;
		this.WalkAnim = this.OriginalWalkAnim;
		this.RunAnim = this.OriginalRunAnim;
	}

	// Token: 0x06001D85 RID: 7557 RVA: 0x0016F610 File Offset: 0x0016D810
	private void Crouch()
	{
		this.MyController.center = new Vector3(this.MyController.center.x, 0.55f, this.MyController.center.z);
		this.MyController.height = 0.9f;
	}

	// Token: 0x06001D86 RID: 7558 RVA: 0x0016F664 File Offset: 0x0016D864
	private void Crawl()
	{
		this.MyController.center = new Vector3(this.MyController.center.x, 0.25f, this.MyController.center.z);
		this.MyController.height = 0.1f;
	}

	// Token: 0x06001D87 RID: 7559 RVA: 0x0016F6B8 File Offset: 0x0016D8B8
	private void Uncrouch()
	{
		this.MyController.center = new Vector3(this.MyController.center.x, 0.875f, this.MyController.center.z);
		this.MyController.height = 1.55f;
	}

	// Token: 0x06001D88 RID: 7560 RVA: 0x0016F70C File Offset: 0x0016D90C
	private void StopArmedAnim()
	{
		this.ID = 0;
		while (this.ID < this.ArmedAnims.Length)
		{
			string name = this.ArmedAnims[this.ID];
			this.CharacterAnimation[name].weight = Mathf.Lerp(this.CharacterAnimation[name].weight, 0f, Time.deltaTime * 10f);
			this.ID++;
		}
	}

	// Token: 0x06001D89 RID: 7561 RVA: 0x0016F788 File Offset: 0x0016D988
	public void UpdateAccessory()
	{
		if (this.AccessoryGroup != null)
		{
			this.AccessoryGroup.SetPartsActive(false);
		}
		if (this.AccessoryID > this.Accessories.Length - 1)
		{
			this.AccessoryID = 0;
		}
		if (this.AccessoryID < 0)
		{
			this.AccessoryID = this.Accessories.Length - 1;
		}
		if (this.AccessoryID > 0)
		{
			this.Accessories[this.AccessoryID].SetActive(true);
			this.AccessoryGroup = this.Accessories[this.AccessoryID].GetComponent<AccessoryGroupScript>();
			if (this.AccessoryGroup != null)
			{
				this.AccessoryGroup.SetPartsActive(true);
			}
		}
	}

	// Token: 0x06001D8A RID: 7562 RVA: 0x0016F830 File Offset: 0x0016DA30
	private void DisableHairAndAccessories()
	{
		this.ID = 1;
		while (this.ID < this.Accessories.Length)
		{
			this.Accessories[this.ID].SetActive(false);
			this.ID++;
		}
		this.ID = 1;
		while (this.ID < this.Hairstyles.Length)
		{
			this.Hairstyles[this.ID].SetActive(false);
			this.ID++;
		}
	}

	// Token: 0x06001D8B RID: 7563 RVA: 0x0016F8B4 File Offset: 0x0016DAB4
	public void BullyPhotoCheck()
	{
		Debug.Log("We are now going to perform a bully photo check.");
		for (int i = 1; i < 26; i++)
		{
			if (PlayerGlobals.GetBullyPhoto(i) > 0)
			{
				Debug.Log("Yandere-chan has a bully photo in her photo gallery!");
				this.BullyPhoto = true;
			}
		}
	}

	// Token: 0x06001D8C RID: 7564 RVA: 0x0016F8F4 File Offset: 0x0016DAF4
	public void UpdatePersona(int NewPersona)
	{
		switch (NewPersona)
		{
		case 0:
			this.Persona = YanderePersonaType.Default;
			return;
		case 1:
			this.Persona = YanderePersonaType.Chill;
			return;
		case 2:
			this.Persona = YanderePersonaType.Confident;
			return;
		case 3:
			this.Persona = YanderePersonaType.Elegant;
			return;
		case 4:
			this.Persona = YanderePersonaType.Girly;
			return;
		case 5:
			this.Persona = YanderePersonaType.Graceful;
			return;
		case 6:
			this.Persona = YanderePersonaType.Haughty;
			return;
		case 7:
			this.Persona = YanderePersonaType.Lively;
			return;
		case 8:
			this.Persona = YanderePersonaType.Scholarly;
			return;
		case 9:
			this.Persona = YanderePersonaType.Shy;
			return;
		case 10:
			this.Persona = YanderePersonaType.Tough;
			return;
		case 11:
			this.Persona = YanderePersonaType.Aggressive;
			return;
		case 12:
			this.Persona = YanderePersonaType.Grunt;
			return;
		default:
			return;
		}
	}

	// Token: 0x06001D8D RID: 7565 RVA: 0x0016F9A8 File Offset: 0x0016DBA8
	private void SithSoundCheck()
	{
		if (this.SithBeam[1].Damage == 10f || this.NierDamage == 10f)
		{
			if (this.SithSounds == 0 && this.CharacterAnimation[string.Concat(new object[]
			{
				"f02_",
				this.AttackPrefix,
				"Attack",
				this.SithPrefix,
				"_0",
				this.SithCombo
			})].time >= this.SithSpawnTime[this.SithCombo] - 0.1f)
			{
				this.SithAudio.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
				this.SithAudio.Play();
				this.SithSounds++;
				return;
			}
		}
		else if (this.SithSounds == 0)
		{
			if (this.CharacterAnimation[string.Concat(new object[]
			{
				"f02_",
				this.AttackPrefix,
				"Attack",
				this.SithPrefix,
				"_0",
				this.SithCombo
			})].time >= this.SithHardSpawnTime1[this.SithCombo] - 0.1f)
			{
				this.SithAudio.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
				this.SithAudio.Play();
				this.SithSounds++;
				return;
			}
		}
		else if (this.SithSounds == 1)
		{
			if (this.CharacterAnimation[string.Concat(new object[]
			{
				"f02_",
				this.AttackPrefix,
				"Attack",
				this.SithPrefix,
				"_0",
				this.SithCombo
			})].time >= this.SithHardSpawnTime2[this.SithCombo] - 0.1f)
			{
				this.SithAudio.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
				this.SithAudio.Play();
				this.SithSounds++;
				return;
			}
		}
		else if (this.SithSounds == 2 && this.SithCombo == 1 && this.CharacterAnimation[string.Concat(new object[]
		{
			"f02_",
			this.AttackPrefix,
			"Attack",
			this.SithPrefix,
			"_0",
			this.SithCombo
		})].time >= 0.8333333f)
		{
			this.SithAudio.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
			this.SithAudio.Play();
			this.SithSounds++;
		}
	}

	// Token: 0x06001D8E RID: 7566 RVA: 0x0016FC80 File Offset: 0x0016DE80
	public void UpdateSelfieStatus()
	{
		if (!this.Selfie)
		{
			this.Smartphone.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
			this.Smartphone.targetTexture = this.Shutter.SmartphoneScreen;
			this.HandCamera.gameObject.SetActive(true);
			this.SelfieGuide.SetActive(false);
			this.MainCamera.enabled = true;
			this.Blur.enabled = true;
			return;
		}
		if (this.Stance.Current == StanceType.Crawling)
		{
			this.Stance.Current = StanceType.Crouching;
		}
		this.Smartphone.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
		this.UpdateAccessory();
		this.UpdateHair();
		this.HandCamera.gameObject.SetActive(false);
		this.Smartphone.targetTexture = null;
		this.MainCamera.enabled = false;
		this.Smartphone.cullingMask &= ~(1 << LayerMask.NameToLayer("Miyuki"));
		this.AR = false;
	}

	// Token: 0x06001D8F RID: 7567 RVA: 0x0016FDA1 File Offset: 0x0016DFA1
	private void OnApplicationFocus(bool hasFocus)
	{
		Cursor.lockState = CursorLockMode.Locked;
	}

	// Token: 0x06001D90 RID: 7568 RVA: 0x0016FDA9 File Offset: 0x0016DFA9
	private void OnApplicationPause(bool pauseStatus)
	{
		Cursor.lockState = CursorLockMode.None;
	}

	// Token: 0x040037B7 RID: 14263
	public Quaternion targetRotation;

	// Token: 0x040037B8 RID: 14264
	private Vector3 targetDirection;

	// Token: 0x040037B9 RID: 14265
	private GameObject NewTrail;

	// Token: 0x040037BA RID: 14266
	public int AccessoryID;

	// Token: 0x040037BB RID: 14267
	private int ID;

	// Token: 0x040037BC RID: 14268
	public FootprintSpawnerScript RightFootprintSpawner;

	// Token: 0x040037BD RID: 14269
	public FootprintSpawnerScript LeftFootprintSpawner;

	// Token: 0x040037BE RID: 14270
	public ColorCorrectionCurves YandereColorCorrection;

	// Token: 0x040037BF RID: 14271
	public ColorCorrectionCurves ColorCorrection;

	// Token: 0x040037C0 RID: 14272
	public SelectiveGrayscale SelectGrayscale;

	// Token: 0x040037C1 RID: 14273
	public HighlightingRenderer HighlightingR;

	// Token: 0x040037C2 RID: 14274
	public HighlightingBlitter HighlightingB;

	// Token: 0x040037C3 RID: 14275
	public AmbientObscurance Obscurance;

	// Token: 0x040037C4 RID: 14276
	public DepthOfField34 DepthOfField;

	// Token: 0x040037C5 RID: 14277
	public Vignetting Vignette;

	// Token: 0x040037C6 RID: 14278
	public Blur Blur;

	// Token: 0x040037C7 RID: 14279
	public NotificationManagerScript NotificationManager;

	// Token: 0x040037C8 RID: 14280
	public ObstacleDetectorScript ObstacleDetector;

	// Token: 0x040037C9 RID: 14281
	public RiggedAccessoryAttacher GloveAttacher;

	// Token: 0x040037CA RID: 14282
	public RiggedAccessoryAttacher PantyAttacher;

	// Token: 0x040037CB RID: 14283
	public AccessoryGroupScript AccessoryGroup;

	// Token: 0x040037CC RID: 14284
	public DumpsterHandleScript DumpsterHandle;

	// Token: 0x040037CD RID: 14285
	public PhonePromptBarScript PhonePromptBar;

	// Token: 0x040037CE RID: 14286
	public ShoulderCameraScript ShoulderCamera;

	// Token: 0x040037CF RID: 14287
	public StudentManagerScript StudentManager;

	// Token: 0x040037D0 RID: 14288
	public AttackManagerScript AttackManager;

	// Token: 0x040037D1 RID: 14289
	public CameraEffectsScript CameraEffects;

	// Token: 0x040037D2 RID: 14290
	public WeaponManagerScript WeaponManager;

	// Token: 0x040037D3 RID: 14291
	public YandereShowerScript YandereShower;

	// Token: 0x040037D4 RID: 14292
	public PromptParentScript PromptParent;

	// Token: 0x040037D5 RID: 14293
	public SplashCameraScript SplashCamera;

	// Token: 0x040037D6 RID: 14294
	public SWP_HeartRateMonitor HeartRate;

	// Token: 0x040037D7 RID: 14295
	public GenericBentoScript TargetBento;

	// Token: 0x040037D8 RID: 14296
	public LoveManagerScript LoveManager;

	// Token: 0x040037D9 RID: 14297
	public StruggleBarScript StruggleBar;

	// Token: 0x040037DA RID: 14298
	public RummageSpotScript RummageSpot;

	// Token: 0x040037DB RID: 14299
	public IncineratorScript Incinerator;

	// Token: 0x040037DC RID: 14300
	public InputDeviceScript InputDevice;

	// Token: 0x040037DD RID: 14301
	public MusicCreditScript MusicCredit;

	// Token: 0x040037DE RID: 14302
	public PauseScreenScript PauseScreen;

	// Token: 0x040037DF RID: 14303
	public SmartphoneScript PhoneToCrush;

	// Token: 0x040037E0 RID: 14304
	public WoodChipperScript WoodChipper;

	// Token: 0x040037E1 RID: 14305
	public RagdollScript CurrentRagdoll;

	// Token: 0x040037E2 RID: 14306
	public StudentScript TargetStudent;

	// Token: 0x040037E3 RID: 14307
	public WeaponMenuScript WeaponMenu;

	// Token: 0x040037E4 RID: 14308
	public PromptScript NearestPrompt;

	// Token: 0x040037E5 RID: 14309
	public ContainerScript Container;

	// Token: 0x040037E6 RID: 14310
	public InventoryScript Inventory;

	// Token: 0x040037E7 RID: 14311
	public TallLockerScript MyLocker;

	// Token: 0x040037E8 RID: 14312
	public PromptBarScript PromptBar;

	// Token: 0x040037E9 RID: 14313
	public TranqCaseScript TranqCase;

	// Token: 0x040037EA RID: 14314
	public LocationScript Location;

	// Token: 0x040037EB RID: 14315
	public SubtitleScript Subtitle;

	// Token: 0x040037EC RID: 14316
	public UITexture SanitySmudges;

	// Token: 0x040037ED RID: 14317
	public StudentScript Follower;

	// Token: 0x040037EE RID: 14318
	public DemonScript EmptyDemon;

	// Token: 0x040037EF RID: 14319
	public UIPanel DetectionPanel;

	// Token: 0x040037F0 RID: 14320
	public JukeboxScript Jukebox;

	// Token: 0x040037F1 RID: 14321
	public OutlineScript Outline;

	// Token: 0x040037F2 RID: 14322
	public StudentScript Pursuer;

	// Token: 0x040037F3 RID: 14323
	public ShutterScript Shutter;

	// Token: 0x040037F4 RID: 14324
	public Collider HipCollider;

	// Token: 0x040037F5 RID: 14325
	public UISprite ProgressBar;

	// Token: 0x040037F6 RID: 14326
	public RPG_Camera RPGCamera;

	// Token: 0x040037F7 RID: 14327
	public BucketScript Bucket;

	// Token: 0x040037F8 RID: 14328
	public LookAtTarget LookAt;

	// Token: 0x040037F9 RID: 14329
	public PickUpScript PickUp;

	// Token: 0x040037FA RID: 14330
	public PoliceScript Police;

	// Token: 0x040037FB RID: 14331
	public UILabel SanityLabel;

	// Token: 0x040037FC RID: 14332
	public GloveScript Gloves;

	// Token: 0x040037FD RID: 14333
	public Camera UICamera;

	// Token: 0x040037FE RID: 14334
	public UILabel PowerUp;

	// Token: 0x040037FF RID: 14335
	public MaskScript Mask;

	// Token: 0x04003800 RID: 14336
	public MopScript Mop;

	// Token: 0x04003801 RID: 14337
	public UIPanel HUD;

	// Token: 0x04003802 RID: 14338
	public CharacterController MyController;

	// Token: 0x04003803 RID: 14339
	public Transform LeftItemParent;

	// Token: 0x04003804 RID: 14340
	public Transform DismemberSpot;

	// Token: 0x04003805 RID: 14341
	public Transform CameraTarget;

	// Token: 0x04003806 RID: 14342
	public Transform InvertSphere;

	// Token: 0x04003807 RID: 14343
	public Transform RightArmRoll;

	// Token: 0x04003808 RID: 14344
	public Transform LeftArmRoll;

	// Token: 0x04003809 RID: 14345
	public Transform CameraFocus;

	// Token: 0x0400380A RID: 14346
	public Transform RightBreast;

	// Token: 0x0400380B RID: 14347
	public Transform HidingSpot;

	// Token: 0x0400380C RID: 14348
	public Transform ItemParent;

	// Token: 0x0400380D RID: 14349
	public Transform LeftBreast;

	// Token: 0x0400380E RID: 14350
	public Transform LimbParent;

	// Token: 0x0400380F RID: 14351
	public Transform PelvisRoot;

	// Token: 0x04003810 RID: 14352
	public Transform PoisonSpot;

	// Token: 0x04003811 RID: 14353
	public Transform CameraPOV;

	// Token: 0x04003812 RID: 14354
	public Transform RightHand;

	// Token: 0x04003813 RID: 14355
	public Transform RightKnee;

	// Token: 0x04003814 RID: 14356
	public Transform RightFoot;

	// Token: 0x04003815 RID: 14357
	public Transform ExitSpot;

	// Token: 0x04003816 RID: 14358
	public Transform LeftHand;

	// Token: 0x04003817 RID: 14359
	public Transform Backpack;

	// Token: 0x04003818 RID: 14360
	public Transform DropSpot;

	// Token: 0x04003819 RID: 14361
	public Transform Homeroom;

	// Token: 0x0400381A RID: 14362
	public Transform DigSpot;

	// Token: 0x0400381B RID: 14363
	public Transform Senpai;

	// Token: 0x0400381C RID: 14364
	public Transform Target;

	// Token: 0x0400381D RID: 14365
	public Transform Stool;

	// Token: 0x0400381E RID: 14366
	public Transform Eyes;

	// Token: 0x0400381F RID: 14367
	public Transform Head;

	// Token: 0x04003820 RID: 14368
	public Transform Hips;

	// Token: 0x04003821 RID: 14369
	public AudioSource HeartBeat;

	// Token: 0x04003822 RID: 14370
	public AudioSource MyAudio;

	// Token: 0x04003823 RID: 14371
	public GameObject[] Accessories;

	// Token: 0x04003824 RID: 14372
	public GameObject[] Hairstyles;

	// Token: 0x04003825 RID: 14373
	public GameObject[] Poisons;

	// Token: 0x04003826 RID: 14374
	public GameObject[] Shoes;

	// Token: 0x04003827 RID: 14375
	public float[] DropTimer;

	// Token: 0x04003828 RID: 14376
	public GameObject CinematicCamera;

	// Token: 0x04003829 RID: 14377
	public GameObject FloatingShovel;

	// Token: 0x0400382A RID: 14378
	public GameObject EasterEggMenu;

	// Token: 0x0400382B RID: 14379
	public GameObject StolenObject;

	// Token: 0x0400382C RID: 14380
	public GameObject SelfieGuide;

	// Token: 0x0400382D RID: 14381
	public GameObject MemeGlasses;

	// Token: 0x0400382E RID: 14382
	public GameObject GiggleDisc;

	// Token: 0x0400382F RID: 14383
	public GameObject KONGlasses;

	// Token: 0x04003830 RID: 14384
	public GameObject Microphone;

	// Token: 0x04003831 RID: 14385
	public GameObject SpiderLegs;

	// Token: 0x04003832 RID: 14386
	public GameObject AlarmDisc;

	// Token: 0x04003833 RID: 14387
	public GameObject Character;

	// Token: 0x04003834 RID: 14388
	public GameObject DebugMenu;

	// Token: 0x04003835 RID: 14389
	public GameObject EyepatchL;

	// Token: 0x04003836 RID: 14390
	public GameObject EyepatchR;

	// Token: 0x04003837 RID: 14391
	public GameObject EmptyHusk;

	// Token: 0x04003838 RID: 14392
	public GameObject Handcuffs;

	// Token: 0x04003839 RID: 14393
	public GameObject ShoePair;

	// Token: 0x0400383A RID: 14394
	public GameObject Barcode;

	// Token: 0x0400383B RID: 14395
	public GameObject Headset;

	// Token: 0x0400383C RID: 14396
	public GameObject Ragdoll;

	// Token: 0x0400383D RID: 14397
	public GameObject Hearts;

	// Token: 0x0400383E RID: 14398
	public GameObject Teeth;

	// Token: 0x0400383F RID: 14399
	public GameObject Phone;

	// Token: 0x04003840 RID: 14400
	public GameObject Trail;

	// Token: 0x04003841 RID: 14401
	public GameObject Match;

	// Token: 0x04003842 RID: 14402
	public GameObject Arc;

	// Token: 0x04003843 RID: 14403
	public SkinnedMeshRenderer MyRenderer;

	// Token: 0x04003844 RID: 14404
	public Animation CharacterAnimation;

	// Token: 0x04003845 RID: 14405
	public ParticleSystem GiggleLines;

	// Token: 0x04003846 RID: 14406
	public ParticleSystem InsaneLines;

	// Token: 0x04003847 RID: 14407
	public SpringJoint RagdollDragger;

	// Token: 0x04003848 RID: 14408
	public SpringJoint RagdollPK;

	// Token: 0x04003849 RID: 14409
	public Projector MyProjector;

	// Token: 0x0400384A RID: 14410
	public Camera HeartCamera;

	// Token: 0x0400384B RID: 14411
	public Camera MainCamera;

	// Token: 0x0400384C RID: 14412
	public Camera Smartphone;

	// Token: 0x0400384D RID: 14413
	public Camera HandCamera;

	// Token: 0x0400384E RID: 14414
	public Renderer SmartphoneRenderer;

	// Token: 0x0400384F RID: 14415
	public Renderer LongHairRenderer;

	// Token: 0x04003850 RID: 14416
	public Renderer PonytailRenderer;

	// Token: 0x04003851 RID: 14417
	public Renderer PigtailR;

	// Token: 0x04003852 RID: 14418
	public Renderer PigtailL;

	// Token: 0x04003853 RID: 14419
	public Renderer Drills;

	// Token: 0x04003854 RID: 14420
	public float MurderousActionTimer;

	// Token: 0x04003855 RID: 14421
	public float CinematicTimer;

	// Token: 0x04003856 RID: 14422
	public float ClothingTimer;

	// Token: 0x04003857 RID: 14423
	public float BreakUpTimer;

	// Token: 0x04003858 RID: 14424
	public float CanMoveTimer;

	// Token: 0x04003859 RID: 14425
	public float RummageTimer;

	// Token: 0x0400385A RID: 14426
	public float YandereTimer;

	// Token: 0x0400385B RID: 14427
	public float AttackTimer;

	// Token: 0x0400385C RID: 14428
	public float CaughtTimer;

	// Token: 0x0400385D RID: 14429
	public float GiggleTimer;

	// Token: 0x0400385E RID: 14430
	public float SenpaiTimer;

	// Token: 0x0400385F RID: 14431
	public float WeaponTimer;

	// Token: 0x04003860 RID: 14432
	public float CrawlTimer;

	// Token: 0x04003861 RID: 14433
	public float GloveTimer;

	// Token: 0x04003862 RID: 14434
	public float LaughTimer;

	// Token: 0x04003863 RID: 14435
	public float SprayTimer;

	// Token: 0x04003864 RID: 14436
	public float TheftTimer;

	// Token: 0x04003865 RID: 14437
	public float BeatTimer;

	// Token: 0x04003866 RID: 14438
	public float BoneTimer;

	// Token: 0x04003867 RID: 14439
	public float DumpTimer;

	// Token: 0x04003868 RID: 14440
	public float ExitTimer;

	// Token: 0x04003869 RID: 14441
	public float TalkTimer;

	// Token: 0x0400386A RID: 14442
	[SerializeField]
	private float bloodiness;

	// Token: 0x0400386B RID: 14443
	public float PreviousSanity = 100f;

	// Token: 0x0400386C RID: 14444
	[SerializeField]
	private float sanity;

	// Token: 0x0400386D RID: 14445
	public float TwitchTimer;

	// Token: 0x0400386E RID: 14446
	public float NextTwitch;

	// Token: 0x0400386F RID: 14447
	public float LaughIntensity;

	// Token: 0x04003870 RID: 14448
	public float TimeSkipHeight;

	// Token: 0x04003871 RID: 14449
	public float PourDistance;

	// Token: 0x04003872 RID: 14450
	public float TargetHeight;

	// Token: 0x04003873 RID: 14451
	public float ReachWeight;

	// Token: 0x04003874 RID: 14452
	public float BreastSize;

	// Token: 0x04003875 RID: 14453
	public float Numbness;

	// Token: 0x04003876 RID: 14454
	public float PourTime;

	// Token: 0x04003877 RID: 14455
	public float RunSpeed;

	// Token: 0x04003878 RID: 14456
	public float Height;

	// Token: 0x04003879 RID: 14457
	public float Slouch;

	// Token: 0x0400387A RID: 14458
	public float Bend;

	// Token: 0x0400387B RID: 14459
	public float CrouchWalkSpeed;

	// Token: 0x0400387C RID: 14460
	public float CrouchRunSpeed;

	// Token: 0x0400387D RID: 14461
	public float ShoveSpeed = 2f;

	// Token: 0x0400387E RID: 14462
	public float CrawlSpeed;

	// Token: 0x0400387F RID: 14463
	public float FlapSpeed;

	// Token: 0x04003880 RID: 14464
	public float WalkSpeed;

	// Token: 0x04003881 RID: 14465
	public float YandereFade;

	// Token: 0x04003882 RID: 14466
	public float YandereTint;

	// Token: 0x04003883 RID: 14467
	public float SenpaiFade;

	// Token: 0x04003884 RID: 14468
	public float SenpaiTint;

	// Token: 0x04003885 RID: 14469
	public float GreyTarget;

	// Token: 0x04003886 RID: 14470
	public int CurrentUniformOrigin = 1;

	// Token: 0x04003887 RID: 14471
	public int PreviousSchoolwear;

	// Token: 0x04003888 RID: 14472
	public int NearestCorpseID;

	// Token: 0x04003889 RID: 14473
	public int StrugglePhase;

	// Token: 0x0400388A RID: 14474
	public int PhysicalGrade;

	// Token: 0x0400388B RID: 14475
	public int CarryAnimID;

	// Token: 0x0400388C RID: 14476
	public int AttackPhase;

	// Token: 0x0400388D RID: 14477
	public int Creepiness = 1;

	// Token: 0x0400388E RID: 14478
	public int GloveBlood;

	// Token: 0x0400388F RID: 14479
	public int NearBodies;

	// Token: 0x04003890 RID: 14480
	public int PoisonType;

	// Token: 0x04003891 RID: 14481
	public int Schoolwear;

	// Token: 0x04003892 RID: 14482
	public int SpeedBonus;

	// Token: 0x04003893 RID: 14483
	public int SprayPhase;

	// Token: 0x04003894 RID: 14484
	public int DragState;

	// Token: 0x04003895 RID: 14485
	public int EggBypass;

	// Token: 0x04003896 RID: 14486
	public int EyewearID;

	// Token: 0x04003897 RID: 14487
	public int Followers;

	// Token: 0x04003898 RID: 14488
	public int Hairstyle;

	// Token: 0x04003899 RID: 14489
	public int DigPhase;

	// Token: 0x0400389A RID: 14490
	public int Equipped;

	// Token: 0x0400389B RID: 14491
	public int Chasers;

	// Token: 0x0400389C RID: 14492
	public int Costume;

	// Token: 0x0400389D RID: 14493
	public int Alerts;

	// Token: 0x0400389E RID: 14494
	public int Health = 5;

	// Token: 0x0400389F RID: 14495
	public YandereInteractionType Interaction;

	// Token: 0x040038A0 RID: 14496
	public YanderePersonaType Persona;

	// Token: 0x040038A1 RID: 14497
	public bool EavesdropWarning;

	// Token: 0x040038A2 RID: 14498
	public bool ClothingWarning;

	// Token: 0x040038A3 RID: 14499
	public bool BloodyWarning;

	// Token: 0x040038A4 RID: 14500
	public bool CorpseWarning;

	// Token: 0x040038A5 RID: 14501
	public bool SanityWarning;

	// Token: 0x040038A6 RID: 14502
	public bool WeaponWarning;

	// Token: 0x040038A7 RID: 14503
	public bool DelinquentFighting;

	// Token: 0x040038A8 RID: 14504
	public bool DumpsterGrabbing;

	// Token: 0x040038A9 RID: 14505
	public bool BucketDropping;

	// Token: 0x040038AA RID: 14506
	public bool CleaningWeapon;

	// Token: 0x040038AB RID: 14507
	public bool SubtleStabbing;

	// Token: 0x040038AC RID: 14508
	public bool TranquilHiding;

	// Token: 0x040038AD RID: 14509
	public bool CrushingPhone;

	// Token: 0x040038AE RID: 14510
	public bool Eavesdropping;

	// Token: 0x040038AF RID: 14511
	public bool Pickpocketing;

	// Token: 0x040038B0 RID: 14512
	public bool Dismembering;

	// Token: 0x040038B1 RID: 14513
	public bool ShootingBeam;

	// Token: 0x040038B2 RID: 14514
	public bool StoppingTime;

	// Token: 0x040038B3 RID: 14515
	public bool TimeSkipping;

	// Token: 0x040038B4 RID: 14516
	public bool Cauterizing;

	// Token: 0x040038B5 RID: 14517
	public bool HeavyWeight;

	// Token: 0x040038B6 RID: 14518
	public bool Trespassing;

	// Token: 0x040038B7 RID: 14519
	public bool WritingName;

	// Token: 0x040038B8 RID: 14520
	public bool Struggling;

	// Token: 0x040038B9 RID: 14521
	public bool Attacking;

	// Token: 0x040038BA RID: 14522
	public bool Degloving;

	// Token: 0x040038BB RID: 14523
	public bool Poisoning;

	// Token: 0x040038BC RID: 14524
	public bool Rummaging;

	// Token: 0x040038BD RID: 14525
	public bool Stripping;

	// Token: 0x040038BE RID: 14526
	public bool Blasting;

	// Token: 0x040038BF RID: 14527
	public bool Carrying;

	// Token: 0x040038C0 RID: 14528
	public bool Chipping;

	// Token: 0x040038C1 RID: 14529
	public bool Dragging;

	// Token: 0x040038C2 RID: 14530
	public bool Dropping;

	// Token: 0x040038C3 RID: 14531
	public bool Flicking;

	// Token: 0x040038C4 RID: 14532
	public bool Freezing;

	// Token: 0x040038C5 RID: 14533
	public bool Laughing;

	// Token: 0x040038C6 RID: 14534
	public bool Punching;

	// Token: 0x040038C7 RID: 14535
	public bool Throwing;

	// Token: 0x040038C8 RID: 14536
	public bool Tripping;

	// Token: 0x040038C9 RID: 14537
	public bool Bathing;

	// Token: 0x040038CA RID: 14538
	public bool Burying;

	// Token: 0x040038CB RID: 14539
	public bool Cooking;

	// Token: 0x040038CC RID: 14540
	public bool Digging;

	// Token: 0x040038CD RID: 14541
	public bool Dipping;

	// Token: 0x040038CE RID: 14542
	public bool Dumping;

	// Token: 0x040038CF RID: 14543
	public bool Exiting;

	// Token: 0x040038D0 RID: 14544
	public bool Lifting;

	// Token: 0x040038D1 RID: 14545
	public bool Mopping;

	// Token: 0x040038D2 RID: 14546
	public bool Pouring;

	// Token: 0x040038D3 RID: 14547
	public bool Resting;

	// Token: 0x040038D4 RID: 14548
	public bool Running;

	// Token: 0x040038D5 RID: 14549
	public bool Talking;

	// Token: 0x040038D6 RID: 14550
	public bool Testing;

	// Token: 0x040038D7 RID: 14551
	public bool Aiming;

	// Token: 0x040038D8 RID: 14552
	public bool Eating;

	// Token: 0x040038D9 RID: 14553
	public bool Hiding;

	// Token: 0x040038DA RID: 14554
	public bool Riding;

	// Token: 0x040038DB RID: 14555
	public Stance Stance = new Stance(StanceType.Standing);

	// Token: 0x040038DC RID: 14556
	public bool PreparedForStruggle;

	// Token: 0x040038DD RID: 14557
	public bool CrouchButtonDown;

	// Token: 0x040038DE RID: 14558
	public bool FightHasBrokenUp;

	// Token: 0x040038DF RID: 14559
	public bool UsingController;

	// Token: 0x040038E0 RID: 14560
	public bool SeenByAuthority;

	// Token: 0x040038E1 RID: 14561
	public bool CameFromCrouch;

	// Token: 0x040038E2 RID: 14562
	public bool CannotRecover;

	// Token: 0x040038E3 RID: 14563
	public bool NoStainGloves;

	// Token: 0x040038E4 RID: 14564
	public bool YandereVision;

	// Token: 0x040038E5 RID: 14565
	public bool ClubActivity;

	// Token: 0x040038E6 RID: 14566
	public bool FlameDemonic;

	// Token: 0x040038E7 RID: 14567
	public bool SanityBased;

	// Token: 0x040038E8 RID: 14568
	public bool SummonBones;

	// Token: 0x040038E9 RID: 14569
	public bool ClubAttire;

	// Token: 0x040038EA RID: 14570
	public bool FollowHips;

	// Token: 0x040038EB RID: 14571
	public bool NearSenpai;

	// Token: 0x040038EC RID: 14572
	public bool RivalPhone;

	// Token: 0x040038ED RID: 14573
	public bool SpiderGrow;

	// Token: 0x040038EE RID: 14574
	public bool Possessed;

	// Token: 0x040038EF RID: 14575
	public bool ToggleRun;

	// Token: 0x040038F0 RID: 14576
	public bool WitchMode;

	// Token: 0x040038F1 RID: 14577
	public bool Attacked;

	// Token: 0x040038F2 RID: 14578
	public bool CanTranq;

	// Token: 0x040038F3 RID: 14579
	public bool Collapse;

	// Token: 0x040038F4 RID: 14580
	public bool Unmasked;

	// Token: 0x040038F5 RID: 14581
	public bool RedPaint;

	// Token: 0x040038F6 RID: 14582
	public bool RoofPush;

	// Token: 0x040038F7 RID: 14583
	public bool Demonic;

	// Token: 0x040038F8 RID: 14584
	public bool FlapOut;

	// Token: 0x040038F9 RID: 14585
	public bool NoDebug;

	// Token: 0x040038FA RID: 14586
	public bool Noticed;

	// Token: 0x040038FB RID: 14587
	public bool InClass;

	// Token: 0x040038FC RID: 14588
	public bool Slender;

	// Token: 0x040038FD RID: 14589
	public bool Sprayed;

	// Token: 0x040038FE RID: 14590
	public bool Caught;

	// Token: 0x040038FF RID: 14591
	public bool CanMove = true;

	// Token: 0x04003900 RID: 14592
	public bool Chased;

	// Token: 0x04003901 RID: 14593
	public bool Gloved;

	// Token: 0x04003902 RID: 14594
	public bool Selfie;

	// Token: 0x04003903 RID: 14595
	public bool Shoved;

	// Token: 0x04003904 RID: 14596
	public bool Drown;

	// Token: 0x04003905 RID: 14597
	public bool Xtan;

	// Token: 0x04003906 RID: 14598
	public bool Lewd;

	// Token: 0x04003907 RID: 14599
	public bool Lost;

	// Token: 0x04003908 RID: 14600
	public bool Sans;

	// Token: 0x04003909 RID: 14601
	public bool Egg;

	// Token: 0x0400390A RID: 14602
	public bool Won;

	// Token: 0x0400390B RID: 14603
	public bool AR;

	// Token: 0x0400390C RID: 14604
	public bool DK;

	// Token: 0x0400390D RID: 14605
	public bool PK;

	// Token: 0x0400390E RID: 14606
	public Texture[] UniformTextures;

	// Token: 0x0400390F RID: 14607
	public Texture[] CasualTextures;

	// Token: 0x04003910 RID: 14608
	public Texture[] FlowerTextures;

	// Token: 0x04003911 RID: 14609
	public Texture[] BloodTextures;

	// Token: 0x04003912 RID: 14610
	public AudioClip[] CreepyGiggles;

	// Token: 0x04003913 RID: 14611
	public AudioClip[] Stabs;

	// Token: 0x04003914 RID: 14612
	public WeaponScript[] Weapon;

	// Token: 0x04003915 RID: 14613
	public GameObject[] ZipTie;

	// Token: 0x04003916 RID: 14614
	public string[] ArmedAnims;

	// Token: 0x04003917 RID: 14615
	public string[] CarryAnims;

	// Token: 0x04003918 RID: 14616
	public Transform[] Spine;

	// Token: 0x04003919 RID: 14617
	public Transform[] Foot;

	// Token: 0x0400391A RID: 14618
	public Transform[] Hand;

	// Token: 0x0400391B RID: 14619
	public Transform[] Arm;

	// Token: 0x0400391C RID: 14620
	public Transform[] Leg;

	// Token: 0x0400391D RID: 14621
	public Mesh[] Uniforms;

	// Token: 0x0400391E RID: 14622
	public Renderer RightYandereEye;

	// Token: 0x0400391F RID: 14623
	public Renderer LeftYandereEye;

	// Token: 0x04003920 RID: 14624
	public Vector3 RightEyeOrigin;

	// Token: 0x04003921 RID: 14625
	public Vector3 LeftEyeOrigin;

	// Token: 0x04003922 RID: 14626
	public Renderer RightRedEye;

	// Token: 0x04003923 RID: 14627
	public Renderer LeftRedEye;

	// Token: 0x04003924 RID: 14628
	public Transform RightEye;

	// Token: 0x04003925 RID: 14629
	public Transform LeftEye;

	// Token: 0x04003926 RID: 14630
	public float EyeShrink;

	// Token: 0x04003927 RID: 14631
	public Vector3 Twitch;

	// Token: 0x04003928 RID: 14632
	private AudioClip LaughClip;

	// Token: 0x04003929 RID: 14633
	public string PourHeight = string.Empty;

	// Token: 0x0400392A RID: 14634
	public string DrownAnim = string.Empty;

	// Token: 0x0400392B RID: 14635
	public string LaughAnim = string.Empty;

	// Token: 0x0400392C RID: 14636
	public string HideAnim = string.Empty;

	// Token: 0x0400392D RID: 14637
	public string IdleAnim = string.Empty;

	// Token: 0x0400392E RID: 14638
	public string TalkAnim = string.Empty;

	// Token: 0x0400392F RID: 14639
	public string WalkAnim = string.Empty;

	// Token: 0x04003930 RID: 14640
	public string RunAnim = string.Empty;

	// Token: 0x04003931 RID: 14641
	public string CrouchIdleAnim = string.Empty;

	// Token: 0x04003932 RID: 14642
	public string CrouchWalkAnim = string.Empty;

	// Token: 0x04003933 RID: 14643
	public string CrouchRunAnim = string.Empty;

	// Token: 0x04003934 RID: 14644
	public string CrawlIdleAnim = string.Empty;

	// Token: 0x04003935 RID: 14645
	public string CrawlWalkAnim = string.Empty;

	// Token: 0x04003936 RID: 14646
	public string HeavyIdleAnim = string.Empty;

	// Token: 0x04003937 RID: 14647
	public string HeavyWalkAnim = string.Empty;

	// Token: 0x04003938 RID: 14648
	public string CarryIdleAnim = string.Empty;

	// Token: 0x04003939 RID: 14649
	public string CarryWalkAnim = string.Empty;

	// Token: 0x0400393A RID: 14650
	public string CarryRunAnim = string.Empty;

	// Token: 0x0400393B RID: 14651
	public string[] CreepyIdles;

	// Token: 0x0400393C RID: 14652
	public string[] CreepyWalks;

	// Token: 0x0400393D RID: 14653
	public AudioClip DramaticWriting;

	// Token: 0x0400393E RID: 14654
	public AudioClip ChargeUp;

	// Token: 0x0400393F RID: 14655
	public AudioClip Laugh0;

	// Token: 0x04003940 RID: 14656
	public AudioClip Laugh1;

	// Token: 0x04003941 RID: 14657
	public AudioClip Laugh2;

	// Token: 0x04003942 RID: 14658
	public AudioClip Laugh3;

	// Token: 0x04003943 RID: 14659
	public AudioClip Laugh4;

	// Token: 0x04003944 RID: 14660
	public AudioClip Thud;

	// Token: 0x04003945 RID: 14661
	public AudioClip Dig;

	// Token: 0x04003946 RID: 14662
	public Vector3 PreviousPosition;

	// Token: 0x04003947 RID: 14663
	public string OriginalIdleAnim = string.Empty;

	// Token: 0x04003948 RID: 14664
	public string OriginalWalkAnim = string.Empty;

	// Token: 0x04003949 RID: 14665
	public string OriginalRunAnim = string.Empty;

	// Token: 0x0400394A RID: 14666
	public Texture YanderePhoneTexture;

	// Token: 0x0400394B RID: 14667
	public Texture BloodyGloveTexture;

	// Token: 0x0400394C RID: 14668
	public Texture RivalPhoneTexture;

	// Token: 0x0400394D RID: 14669
	public Texture BlondePony;

	// Token: 0x0400394E RID: 14670
	public float VibrationIntensity;

	// Token: 0x0400394F RID: 14671
	public float VibrationTimer;

	// Token: 0x04003950 RID: 14672
	public bool VibrationCheck;

	// Token: 0x04003951 RID: 14673
	public float v;

	// Token: 0x04003952 RID: 14674
	public float h;

	// Token: 0x04003953 RID: 14675
	private int DebugInt;

	// Token: 0x04003954 RID: 14676
	public GameObject CreepyArms;

	// Token: 0x04003955 RID: 14677
	public Texture[] GloveTextures;

	// Token: 0x04003956 RID: 14678
	public Texture TitanTexture;

	// Token: 0x04003957 RID: 14679
	public Texture KONTexture;

	// Token: 0x04003958 RID: 14680
	public GameObject PunishedAccessories;

	// Token: 0x04003959 RID: 14681
	public GameObject PunishedScarf;

	// Token: 0x0400395A RID: 14682
	public GameObject[] PunishedArm;

	// Token: 0x0400395B RID: 14683
	public Texture[] PunishedTextures;

	// Token: 0x0400395C RID: 14684
	public Shader PunishedShader;

	// Token: 0x0400395D RID: 14685
	public Mesh PunishedMesh;

	// Token: 0x0400395E RID: 14686
	public Material HatefulSkybox;

	// Token: 0x0400395F RID: 14687
	public Texture HatefulUniform;

	// Token: 0x04003960 RID: 14688
	public GameObject SukebanAccessories;

	// Token: 0x04003961 RID: 14689
	public Texture SukebanBandages;

	// Token: 0x04003962 RID: 14690
	public Texture SukebanUniform;

	// Token: 0x04003963 RID: 14691
	public FalconPunchScript BanchoFinisher;

	// Token: 0x04003964 RID: 14692
	public StandPunchScript BanchoFlurry;

	// Token: 0x04003965 RID: 14693
	public GameObject BanchoPants;

	// Token: 0x04003966 RID: 14694
	public Mesh BanchoMesh;

	// Token: 0x04003967 RID: 14695
	public Texture BanchoBody;

	// Token: 0x04003968 RID: 14696
	public Texture BanchoFace;

	// Token: 0x04003969 RID: 14697
	public GameObject[] BanchoAccessories;

	// Token: 0x0400396A RID: 14698
	public bool BanchoActive;

	// Token: 0x0400396B RID: 14699
	public bool Finisher;

	// Token: 0x0400396C RID: 14700
	public AudioClip BanchoYanYan;

	// Token: 0x0400396D RID: 14701
	public AudioClip BanchoFinalYan;

	// Token: 0x0400396E RID: 14702
	public AmplifyMotionObject MotionObject;

	// Token: 0x0400396F RID: 14703
	public AmplifyMotionEffect MotionBlur;

	// Token: 0x04003970 RID: 14704
	public GameObject BanchoCamera;

	// Token: 0x04003971 RID: 14705
	public GameObject[] SlenderHair;

	// Token: 0x04003972 RID: 14706
	public Texture SlenderUniform;

	// Token: 0x04003973 RID: 14707
	public Material SlenderSkybox;

	// Token: 0x04003974 RID: 14708
	public Texture SlenderSkin;

	// Token: 0x04003975 RID: 14709
	public Transform[] LongHair;

	// Token: 0x04003976 RID: 14710
	public GameObject BlackEyePatch;

	// Token: 0x04003977 RID: 14711
	public GameObject XSclera;

	// Token: 0x04003978 RID: 14712
	public GameObject XEye;

	// Token: 0x04003979 RID: 14713
	public Texture XBody;

	// Token: 0x0400397A RID: 14714
	public Texture XFace;

	// Token: 0x0400397B RID: 14715
	public GameObject[] GaloAccessories;

	// Token: 0x0400397C RID: 14716
	public Texture GaloArms;

	// Token: 0x0400397D RID: 14717
	public Texture GaloFace;

	// Token: 0x0400397E RID: 14718
	public AudioClip SummonStand;

	// Token: 0x0400397F RID: 14719
	public StandScript Stand;

	// Token: 0x04003980 RID: 14720
	public AudioClip YanYan;

	// Token: 0x04003981 RID: 14721
	public Texture AgentFace;

	// Token: 0x04003982 RID: 14722
	public Texture AgentSuit;

	// Token: 0x04003983 RID: 14723
	public GameObject CirnoIceAttack;

	// Token: 0x04003984 RID: 14724
	public AudioClip CirnoIceClip;

	// Token: 0x04003985 RID: 14725
	public GameObject CirnoWings;

	// Token: 0x04003986 RID: 14726
	public GameObject CirnoHair;

	// Token: 0x04003987 RID: 14727
	public Texture CirnoUniform;

	// Token: 0x04003988 RID: 14728
	public Texture CirnoFace;

	// Token: 0x04003989 RID: 14729
	public Transform[] CirnoWing;

	// Token: 0x0400398A RID: 14730
	public float CirnoRotation;

	// Token: 0x0400398B RID: 14731
	public float CirnoTimer;

	// Token: 0x0400398C RID: 14732
	public AudioClip FalconPunchVoice;

	// Token: 0x0400398D RID: 14733
	public Texture FalconBody;

	// Token: 0x0400398E RID: 14734
	public Texture FalconFace;

	// Token: 0x0400398F RID: 14735
	public float FalconSpeed;

	// Token: 0x04003990 RID: 14736
	public GameObject NewFalconPunch;

	// Token: 0x04003991 RID: 14737
	public GameObject FalconWindUp;

	// Token: 0x04003992 RID: 14738
	public GameObject FalconPunch;

	// Token: 0x04003993 RID: 14739
	public GameObject FalconShoulderpad;

	// Token: 0x04003994 RID: 14740
	public GameObject FalconKneepad1;

	// Token: 0x04003995 RID: 14741
	public GameObject FalconKneepad2;

	// Token: 0x04003996 RID: 14742
	public GameObject FalconBuckle;

	// Token: 0x04003997 RID: 14743
	public GameObject FalconHelmet;

	// Token: 0x04003998 RID: 14744
	public AudioClip[] OnePunchVoices;

	// Token: 0x04003999 RID: 14745
	public GameObject NewOnePunch;

	// Token: 0x0400399A RID: 14746
	public GameObject OnePunch;

	// Token: 0x0400399B RID: 14747
	public Texture SaitamaSuit;

	// Token: 0x0400399C RID: 14748
	public GameObject Cape;

	// Token: 0x0400399D RID: 14749
	public ParticleSystem GlowEffect;

	// Token: 0x0400399E RID: 14750
	public GameObject[] BlasterSet;

	// Token: 0x0400399F RID: 14751
	public GameObject[] SansEyes;

	// Token: 0x040039A0 RID: 14752
	public AudioClip BlasterClip;

	// Token: 0x040039A1 RID: 14753
	public Texture SansTexture;

	// Token: 0x040039A2 RID: 14754
	public Texture SansFace;

	// Token: 0x040039A3 RID: 14755
	public GameObject Bone;

	// Token: 0x040039A4 RID: 14756
	public AudioClip Slam;

	// Token: 0x040039A5 RID: 14757
	public Mesh Jersey;

	// Token: 0x040039A6 RID: 14758
	public int BlasterStage;

	// Token: 0x040039A7 RID: 14759
	public PKDirType PKDir;

	// Token: 0x040039A8 RID: 14760
	public Texture CyborgBody;

	// Token: 0x040039A9 RID: 14761
	public Texture CyborgFace;

	// Token: 0x040039AA RID: 14762
	public GameObject[] CyborgParts;

	// Token: 0x040039AB RID: 14763
	public GameObject EnergySword;

	// Token: 0x040039AC RID: 14764
	public bool Ninja;

	// Token: 0x040039AD RID: 14765
	public GameObject EbolaEffect;

	// Token: 0x040039AE RID: 14766
	public GameObject EbolaWings;

	// Token: 0x040039AF RID: 14767
	public GameObject EbolaHair;

	// Token: 0x040039B0 RID: 14768
	public Texture EbolaFace;

	// Token: 0x040039B1 RID: 14769
	public Texture EbolaUniform;

	// Token: 0x040039B2 RID: 14770
	public Mesh LongUniform;

	// Token: 0x040039B3 RID: 14771
	public Texture NewFace;

	// Token: 0x040039B4 RID: 14772
	public Mesh NewMesh;

	// Token: 0x040039B5 RID: 14773
	public GameObject[] CensorSteam;

	// Token: 0x040039B6 RID: 14774
	public Texture NudePanties;

	// Token: 0x040039B7 RID: 14775
	public Texture NudeTexture;

	// Token: 0x040039B8 RID: 14776
	public Mesh NudeMesh;

	// Token: 0x040039B9 RID: 14777
	public Texture SamusBody;

	// Token: 0x040039BA RID: 14778
	public Texture SamusFace;

	// Token: 0x040039BB RID: 14779
	public GlobalKnifeArrayScript GlobalKnifeArray;

	// Token: 0x040039BC RID: 14780
	public GameObject PlayerOnlyCamera;

	// Token: 0x040039BD RID: 14781
	public GameObject KnifeArray;

	// Token: 0x040039BE RID: 14782
	public AudioClip ClockStart;

	// Token: 0x040039BF RID: 14783
	public AudioClip ClockStop;

	// Token: 0x040039C0 RID: 14784
	public AudioClip ClockTick;

	// Token: 0x040039C1 RID: 14785
	public AudioClip StartShout;

	// Token: 0x040039C2 RID: 14786
	public AudioClip StopShout;

	// Token: 0x040039C3 RID: 14787
	public Texture WitchBody;

	// Token: 0x040039C4 RID: 14788
	public Texture WitchFace;

	// Token: 0x040039C5 RID: 14789
	public Collider BladeHairCollider1;

	// Token: 0x040039C6 RID: 14790
	public Collider BladeHairCollider2;

	// Token: 0x040039C7 RID: 14791
	public GameObject BladeHair;

	// Token: 0x040039C8 RID: 14792
	public DebugMenuScript TheDebugMenuScript;

	// Token: 0x040039C9 RID: 14793
	public GameObject RiggedAccessory;

	// Token: 0x040039CA RID: 14794
	public GameObject TornadoAttack;

	// Token: 0x040039CB RID: 14795
	public GameObject TornadoDress;

	// Token: 0x040039CC RID: 14796
	public GameObject TornadoHair;

	// Token: 0x040039CD RID: 14797
	public Renderer TornadoRenderer;

	// Token: 0x040039CE RID: 14798
	public Mesh NoTorsoMesh;

	// Token: 0x040039CF RID: 14799
	public GameObject KunHair;

	// Token: 0x040039D0 RID: 14800
	public GameObject Kun;

	// Token: 0x040039D1 RID: 14801
	public GameObject Man;

	// Token: 0x040039D2 RID: 14802
	public GameObject BlackHoleEffects;

	// Token: 0x040039D3 RID: 14803
	public Texture BlackHoleFace;

	// Token: 0x040039D4 RID: 14804
	public Texture Black;

	// Token: 0x040039D5 RID: 14805
	public bool BlackHole;

	// Token: 0x040039D6 RID: 14806
	public Transform RightLeg;

	// Token: 0x040039D7 RID: 14807
	public Transform LeftLeg;

	// Token: 0x040039D8 RID: 14808
	public GameObject Bandages;

	// Token: 0x040039D9 RID: 14809
	public GameObject LucyHelmet;

	// Token: 0x040039DA RID: 14810
	public GameObject[] Vectors;

	// Token: 0x040039DB RID: 14811
	public GameObject[] Kagune;

	// Token: 0x040039DC RID: 14812
	public Texture GhoulFace;

	// Token: 0x040039DD RID: 14813
	public Texture GhoulBody;

	// Token: 0x040039DE RID: 14814
	public bool ReturnKagune;

	// Token: 0x040039DF RID: 14815
	public bool SwingKagune;

	// Token: 0x040039E0 RID: 14816
	public Vector3[] KaguneRotation;

	// Token: 0x040039E1 RID: 14817
	public AudioClip KaguneSwoosh;

	// Token: 0x040039E2 RID: 14818
	public GameObject DemonSlash;

	// Token: 0x040039E3 RID: 14819
	public int KagunePhase;

	// Token: 0x040039E4 RID: 14820
	public GameObject[] Armor;

	// Token: 0x040039E5 RID: 14821
	public Texture Chainmail;

	// Token: 0x040039E6 RID: 14822
	public Texture Scarface;

	// Token: 0x040039E7 RID: 14823
	public Material Metal;

	// Token: 0x040039E8 RID: 14824
	public Material Trans;

	// Token: 0x040039E9 RID: 14825
	public GameObject BlackRobe;

	// Token: 0x040039EA RID: 14826
	public Mesh NoUpperBodyMesh;

	// Token: 0x040039EB RID: 14827
	public ParticleSystem[] Beam;

	// Token: 0x040039EC RID: 14828
	public SithBeamScript[] SithBeam;

	// Token: 0x040039ED RID: 14829
	public bool SithRecovering;

	// Token: 0x040039EE RID: 14830
	public bool SithAttacking;

	// Token: 0x040039EF RID: 14831
	public bool SithLord;

	// Token: 0x040039F0 RID: 14832
	public string SithPrefix;

	// Token: 0x040039F1 RID: 14833
	public int SithComboLength;

	// Token: 0x040039F2 RID: 14834
	public int SithAttacks;

	// Token: 0x040039F3 RID: 14835
	public int SithSounds;

	// Token: 0x040039F4 RID: 14836
	public int SithCombo;

	// Token: 0x040039F5 RID: 14837
	public GameObject SithHardHitbox;

	// Token: 0x040039F6 RID: 14838
	public GameObject SithHitbox;

	// Token: 0x040039F7 RID: 14839
	public GameObject SithTrail1;

	// Token: 0x040039F8 RID: 14840
	public GameObject SithTrail2;

	// Token: 0x040039F9 RID: 14841
	public Transform SithTrailEnd1;

	// Token: 0x040039FA RID: 14842
	public Transform SithTrailEnd2;

	// Token: 0x040039FB RID: 14843
	public ZoomScript Zoom;

	// Token: 0x040039FC RID: 14844
	public AudioClip SithOn;

	// Token: 0x040039FD RID: 14845
	public AudioClip SithOff;

	// Token: 0x040039FE RID: 14846
	public AudioClip SithSwing;

	// Token: 0x040039FF RID: 14847
	public AudioClip SithStrike;

	// Token: 0x04003A00 RID: 14848
	public AudioSource SithAudio;

	// Token: 0x04003A01 RID: 14849
	public float[] SithHardSpawnTime1;

	// Token: 0x04003A02 RID: 14850
	public float[] SithHardSpawnTime2;

	// Token: 0x04003A03 RID: 14851
	public float[] SithSpawnTime;

	// Token: 0x04003A04 RID: 14852
	public Texture SnakeFace;

	// Token: 0x04003A05 RID: 14853
	public Texture SnakeBody;

	// Token: 0x04003A06 RID: 14854
	public Texture Stone;

	// Token: 0x04003A07 RID: 14855
	public AudioClip Petrify;

	// Token: 0x04003A08 RID: 14856
	public GameObject Pebbles;

	// Token: 0x04003A09 RID: 14857
	public bool Medusa;

	// Token: 0x04003A0A RID: 14858
	public Texture GazerFace;

	// Token: 0x04003A0B RID: 14859
	public Texture GazerBody;

	// Token: 0x04003A0C RID: 14860
	public GazerEyesScript GazerEyes;

	// Token: 0x04003A0D RID: 14861
	public AudioClip FingerSnap;

	// Token: 0x04003A0E RID: 14862
	public AudioClip Zap;

	// Token: 0x04003A0F RID: 14863
	public bool GazeAttacking;

	// Token: 0x04003A10 RID: 14864
	public bool Snapping;

	// Token: 0x04003A11 RID: 14865
	public bool Gazing;

	// Token: 0x04003A12 RID: 14866
	public int SnapPhase;

	// Token: 0x04003A13 RID: 14867
	public GameObject SixRaincoat;

	// Token: 0x04003A14 RID: 14868
	public GameObject RisingSmoke;

	// Token: 0x04003A15 RID: 14869
	public GameObject DarkHelix;

	// Token: 0x04003A16 RID: 14870
	public Texture SixFaceTexture;

	// Token: 0x04003A17 RID: 14871
	public AudioClip SixTakedown;

	// Token: 0x04003A18 RID: 14872
	public Transform SixTarget;

	// Token: 0x04003A19 RID: 14873
	public Mesh SixBodyMesh;

	// Token: 0x04003A1A RID: 14874
	public Transform Mouth;

	// Token: 0x04003A1B RID: 14875
	public int EatPhase;

	// Token: 0x04003A1C RID: 14876
	public bool Hungry;

	// Token: 0x04003A1D RID: 14877
	public int Hunger;

	// Token: 0x04003A1E RID: 14878
	public float[] BloodTimes;

	// Token: 0x04003A1F RID: 14879
	public AudioClip[] Snarls;

	// Token: 0x04003A20 RID: 14880
	public Texture KLKBody;

	// Token: 0x04003A21 RID: 14881
	public Texture KLKFace;

	// Token: 0x04003A22 RID: 14882
	public GameObject[] KLKParts;

	// Token: 0x04003A23 RID: 14883
	public GameObject KLKSword;

	// Token: 0x04003A24 RID: 14884
	public AudioClip LoveLoveBeamVoice;

	// Token: 0x04003A25 RID: 14885
	public GameObject MiyukiCostume;

	// Token: 0x04003A26 RID: 14886
	public GameObject LoveLoveBeam;

	// Token: 0x04003A27 RID: 14887
	public GameObject MiyukiWings;

	// Token: 0x04003A28 RID: 14888
	public Texture MiyukiSkin;

	// Token: 0x04003A29 RID: 14889
	public Texture MiyukiFace;

	// Token: 0x04003A2A RID: 14890
	public bool MagicalGirl;

	// Token: 0x04003A2B RID: 14891
	public int BeamPhase;

	// Token: 0x04003A2C RID: 14892
	public GameObject AzurGuns;

	// Token: 0x04003A2D RID: 14893
	public GameObject AzurWater;

	// Token: 0x04003A2E RID: 14894
	public GameObject AzurMist;

	// Token: 0x04003A2F RID: 14895
	public GameObject Shell;

	// Token: 0x04003A30 RID: 14896
	public Transform[] Guns;

	// Token: 0x04003A31 RID: 14897
	public int ShotsFired;

	// Token: 0x04003A32 RID: 14898
	public bool Shipgirl;

	// Token: 0x04003A33 RID: 14899
	public GameObject Raincoat;

	// Token: 0x04003A34 RID: 14900
	public GameObject Rain;

	// Token: 0x04003A35 RID: 14901
	public Material HorrorSkybox;

	// Token: 0x04003A36 RID: 14902
	public Texture YamikoFaceTexture;

	// Token: 0x04003A37 RID: 14903
	public Texture YamikoSkinTexture;

	// Token: 0x04003A38 RID: 14904
	public Texture YamikoAccessoryTexture;

	// Token: 0x04003A39 RID: 14905
	public GameObject LifeNotebook;

	// Token: 0x04003A3A RID: 14906
	public GameObject LifeNotePen;

	// Token: 0x04003A3B RID: 14907
	public Mesh YamikoMesh;

	// Token: 0x04003A3C RID: 14908
	public GameObject GroundImpact;

	// Token: 0x04003A3D RID: 14909
	public GameObject NierCostume;

	// Token: 0x04003A3E RID: 14910
	public GameObject HeavySword;

	// Token: 0x04003A3F RID: 14911
	public GameObject LightSword;

	// Token: 0x04003A40 RID: 14912
	public GameObject Pod;

	// Token: 0x04003A41 RID: 14913
	public Transform LightSwordParent;

	// Token: 0x04003A42 RID: 14914
	public Transform HeavySwordParent;

	// Token: 0x04003A43 RID: 14915
	public ParticleSystem LightSwordParticles;

	// Token: 0x04003A44 RID: 14916
	public ParticleSystem HeavySwordParticles;

	// Token: 0x04003A45 RID: 14917
	public string AttackPrefix;

	// Token: 0x04003A46 RID: 14918
	public float NierDamage;

	// Token: 0x04003A47 RID: 14919
	public float[] NierSpawnTime;

	// Token: 0x04003A48 RID: 14920
	public float[] NierHardSpawnTime;

	// Token: 0x04003A49 RID: 14921
	public AudioClip NierSwoosh;

	// Token: 0x04003A4A RID: 14922
	public GameObject ChinaDress;

	// Token: 0x04003A4B RID: 14923
	public NormalBufferView VaporwaveVisuals;

	// Token: 0x04003A4C RID: 14924
	public Material VaporwaveSkybox;

	// Token: 0x04003A4D RID: 14925
	public GameObject PalmTrees;

	// Token: 0x04003A4E RID: 14926
	public GameObject[] Trees;

	// Token: 0x04003A4F RID: 14927
	public Mesh SchoolSwimsuit;

	// Token: 0x04003A50 RID: 14928
	public Mesh GymUniform;

	// Token: 0x04003A51 RID: 14929
	public Mesh Towel;

	// Token: 0x04003A52 RID: 14930
	public Texture FaceTexture;

	// Token: 0x04003A53 RID: 14931
	public Texture SwimsuitTexture;

	// Token: 0x04003A54 RID: 14932
	public Texture GymTexture;

	// Token: 0x04003A55 RID: 14933
	public Texture TextureToUse;

	// Token: 0x04003A56 RID: 14934
	public Texture TowelTexture;

	// Token: 0x04003A57 RID: 14935
	public bool Casual = true;

	// Token: 0x04003A58 RID: 14936
	public Mesh JudoGiMesh;

	// Token: 0x04003A59 RID: 14937
	public Texture JudoGiTexture;

	// Token: 0x04003A5A RID: 14938
	public Mesh ApronMesh;

	// Token: 0x04003A5B RID: 14939
	public Texture ApronTexture;

	// Token: 0x04003A5C RID: 14940
	public Mesh LabCoatMesh;

	// Token: 0x04003A5D RID: 14941
	public Mesh HeadAndHands;

	// Token: 0x04003A5E RID: 14942
	public Texture LabCoatTexture;

	// Token: 0x04003A5F RID: 14943
	public RiggedAccessoryAttacher LabcoatAttacher;

	// Token: 0x04003A60 RID: 14944
	public bool Paint;

	// Token: 0x04003A61 RID: 14945
	public GameObject[] ClubAccessories;

	// Token: 0x04003A62 RID: 14946
	public GameObject Fireball;

	// Token: 0x04003A63 RID: 14947
	public bool LiftOff;

	// Token: 0x04003A64 RID: 14948
	public GameObject LiftOffParticles;

	// Token: 0x04003A65 RID: 14949
	public float LiftOffSpeed;

	// Token: 0x04003A66 RID: 14950
	public SkinnedMeshUpdater SkinUpdater;

	// Token: 0x04003A67 RID: 14951
	public Mesh RivalChanMesh;

	// Token: 0x04003A68 RID: 14952
	public Mesh TestMesh;

	// Token: 0x04003A69 RID: 14953
	public bool BullyPhoto;

	// Token: 0x04003A6A RID: 14954
	public CameraFilterScript CinematicCameraFilters;

	// Token: 0x04003A6B RID: 14955
	public CameraFilterScript CameraFilters;
}
