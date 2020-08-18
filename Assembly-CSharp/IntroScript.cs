using System;
using UnityEngine;
using UnityEngine.PostProcessing;
using UnityEngine.SceneManagement;
using XInputDotNetPure;

// Token: 0x02000308 RID: 776
public class IntroScript : MonoBehaviour
{
	// Token: 0x0600177A RID: 6010 RVA: 0x000CB39C File Offset: 0x000C959C
	private void Start()
	{
		Application.targetFrameRate = 60;
		this.LoveSickCheck();
		this.Circle.fillAmount = 0f;
		if (!this.New)
		{
			this.Darkness.color = new Color(0f, 0f, 0f, 1f);
		}
		this.Label.text = string.Empty;
		this.SkipTimer = 15f;
		if (this.New)
		{
			RenderSettings.ambientLight = new Color(1f, 1f, 1f);
			this.BloodyHandsAnim["f02_clenchFists_00"].speed = 0.166666f;
			this.HoleInChestAnim["f02_holeInChest_00"].speed = 0f;
			this.YoungRyobaAnim["f02_introHoldHands_00"].speed = 0f;
			this.YoungFatherAnim["introHoldHands_00"].speed = 0f;
			this.BrightenEnvironment();
			base.transform.position = new Vector3(0f, 1.255f, 0.2f);
			base.transform.eulerAngles = new Vector3(45f, 0f, 0f);
			this.HoleInChestAnim.gameObject.SetActive(false);
			this.BloodyHandsAnim.gameObject.SetActive(true);
			this.Montage.gameObject.SetActive(false);
			this.ConfessionScene.SetActive(false);
			this.ParentAndChild.SetActive(false);
			this.DeathCorridor.SetActive(false);
			this.Stalking.SetActive(false);
			this.School.SetActive(false);
			this.Room.SetActive(false);
			this.SetToDefault();
			DepthOfFieldModel.Settings settings = this.Profile.depthOfField.settings;
			settings.focusDistance = 0.66666f;
			settings.aperture = 32f;
			this.Profile.depthOfField.settings = settings;
			Renderer[] componentsInChildren = this.Corridors.gameObject.GetComponentsInChildren<Renderer>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].material.color = new Color(0.75f, 0.75f, 0.75f, 1f);
			}
			this.AttackAnim[1]["f02_katanaHighSanityA_00"].speed = 2.5f;
			this.AttackAnim[2]["f02_katanaHighSanityB_00"].speed = 2.5f;
			this.AttackAnim[3]["f02_batLowSanityA_00"].speed = 2.5f;
			this.AttackAnim[4]["f02_batLowSanityB_00"].speed = 2.5f;
			this.AttackAnim[5]["f02_katanaLowSanityA_00"].speed = 2.5f;
			this.AttackAnim[6]["f02_katanaLowSanityB_00"].speed = 2.5f;
			this.MotherAnim["f02_parentTalking_00"].speed = 0.75f;
			this.ChildAnim["f02_childListening_00"].speed = 0.75f;
			for (int j = 4; j < this.Cue.Length; j++)
			{
				if (j < 21)
				{
					this.Cue[j] += 3.898f;
				}
				else if (j > 32)
				{
					this.Cue[j] += 4f;
				}
				else
				{
					this.Cue[j] += 2f;
				}
				if (j > 40)
				{
					this.Cue[j] += 3f;
				}
			}
		}
	}

	// Token: 0x0600177B RID: 6011 RVA: 0x000CB734 File Offset: 0x000C9934
	private void Update()
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
		if (Input.GetButton("X"))
		{
			this.Circle.fillAmount = Mathf.MoveTowards(this.Circle.fillAmount, 1f, Time.deltaTime);
			this.SkipTimer = 15f;
			if (this.Circle.fillAmount == 1f)
			{
				this.FadeOut = true;
			}
			this.SkipPanel.alpha = 1f;
		}
		else
		{
			this.Circle.fillAmount = Mathf.MoveTowards(this.Circle.fillAmount, 0f, Time.deltaTime);
			this.SkipTimer -= Time.deltaTime * 2f;
			this.SkipPanel.alpha = this.SkipTimer / 10f;
		}
		this.StartTimer += Time.deltaTime;
		if (this.StartTimer > 1f && !this.Narrating)
		{
			this.Narration.Play();
			this.Narrating = true;
			if (this.BGM != null)
			{
				this.BGM.Play();
			}
		}
		this.Timer = this.Narration.time;
		if (this.ID < this.Cue.Length && this.Narration.time > this.Cue[this.ID])
		{
			this.Label.text = this.Lines[this.ID];
			this.ID++;
		}
		if (this.FadeOut)
		{
			this.FadeOutDarkness.color = new Color(this.FadeOutDarkness.color.r, this.FadeOutDarkness.color.g, this.FadeOutDarkness.color.b, Mathf.MoveTowards(this.FadeOutDarkness.color.a, 1f, Time.deltaTime));
			this.Circle.fillAmount = 1f;
			this.Narration.volume = this.FadeOutDarkness.color.a;
			if (this.FadeOutDarkness.color.a == 1f)
			{
				SceneManager.LoadScene("PhoneScene");
			}
		}
		if (!this.New)
		{
			if (this.Narration.time > 39.75f && this.Narration.time < 73f)
			{
				this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, Mathf.MoveTowards(this.Darkness.color.a, 0f, Time.deltaTime * 0.5f));
			}
			if (this.Narration.time > 73f && this.Narration.time < 105.5f)
			{
				this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, Mathf.MoveTowards(this.Darkness.color.a, 1f, Time.deltaTime));
			}
			if (this.Narration.time > 105.5f && this.Narration.time < 134f)
			{
				this.Darkness.color = new Color(1f, 0f, 0f, 1f);
			}
			if (this.Narration.time > 134f && this.Narration.time < 147f)
			{
				this.Darkness.color = new Color(0f, 0f, 0f, 1f);
			}
			if (this.Narration.time > 147f && this.Narration.time < 152f)
			{
				this.Logo.transform.localScale = new Vector3(this.Logo.transform.localScale.x + Time.deltaTime * 0.1f, this.Logo.transform.localScale.y + Time.deltaTime * 0.1f, this.Logo.transform.localScale.z + Time.deltaTime * 0.1f);
				this.LoveSickLogo.transform.localScale = new Vector3(this.LoveSickLogo.transform.localScale.x + Time.deltaTime * 0.05f, this.LoveSickLogo.transform.localScale.y + Time.deltaTime * 0.05f, this.LoveSickLogo.transform.localScale.z + Time.deltaTime * 0.05f);
				this.Logo.color = new Color(this.Logo.color.r, this.Logo.color.g, this.Logo.color.b, 1f);
				this.LoveSickLogo.color = new Color(this.LoveSickLogo.color.r, this.LoveSickLogo.color.g, this.LoveSickLogo.color.b, 1f);
			}
			if (this.Narration.time > 155.898f)
			{
				this.Logo.color = new Color(this.Logo.color.r, this.Logo.color.g, this.Logo.color.b, 0f);
				this.LoveSickLogo.color = new Color(this.LoveSickLogo.color.r, this.LoveSickLogo.color.g, this.LoveSickLogo.color.b, 0f);
			}
			if (this.Narration.time > 159.898f)
			{
				SceneManager.LoadScene("PhoneScene");
			}
		}
		if (this.New)
		{
			if (this.ID > 19)
			{
				this.AnimTimer += Time.deltaTime;
			}
			if (this.ID > 52)
			{
				if (!this.Narration.isPlaying)
				{
					if (this.Montage.gameObject.activeInHierarchy)
					{
						this.Darkness.color = new Color(0f, 0f, 0f, 0f);
						this.Montage.gameObject.SetActive(false);
						this.PostProcessing.enabled = true;
						this.Label.enabled = false;
						this.GUIPP.enabled = true;
						this.Speed = 0f;
						if (GameGlobals.LoveSick)
						{
							this.LoveSickLogo.gameObject.SetActive(true);
						}
						else
						{
							this.Logo.gameObject.SetActive(true);
						}
						DepthOfFieldModel.Settings settings = this.Profile.depthOfField.settings;
						settings.focusDistance = 10f;
						this.Profile.depthOfField.settings = settings;
						BloomModel.Settings settings2 = this.Profile.bloom.settings;
						settings2.bloom.intensity = 1f;
						this.Profile.bloom.settings = settings2;
						GamePad.SetVibration(PlayerIndex.One, 1f, 1f);
						this.VibrationCheck = true;
						this.VibrationTimer = 0.1f;
					}
					this.Logo.transform.localScale = new Vector3(this.Logo.transform.localScale.x + Time.deltaTime * 0.1f, this.Logo.transform.localScale.y + Time.deltaTime * 0.1f, this.Logo.transform.localScale.z + Time.deltaTime * 0.1f);
					this.LoveSickLogo.transform.localScale = new Vector3(this.LoveSickLogo.transform.localScale.x + Time.deltaTime * 0.05f, this.LoveSickLogo.transform.localScale.y + Time.deltaTime * 0.05f, this.LoveSickLogo.transform.localScale.z + Time.deltaTime * 0.05f);
					this.Logo.color = new Color(this.Logo.color.r, this.Logo.color.g, this.Logo.color.b, 1f);
					this.LoveSickLogo.color = new Color(this.LoveSickLogo.color.r, this.LoveSickLogo.color.g, this.LoveSickLogo.color.b, 1f);
					this.Speed += Time.deltaTime;
					if (this.Speed > 11f)
					{
						SceneManager.LoadScene("PhoneScene");
						return;
					}
					if (this.Speed > 5f && (this.Logo.gameObject.activeInHierarchy || this.LoveSickLogo.gameObject.activeInHierarchy))
					{
						this.LoveSickLogo.gameObject.SetActive(false);
						this.Logo.gameObject.SetActive(false);
						GamePad.SetVibration(PlayerIndex.One, 1f, 1f);
						this.VibrationCheck = true;
						this.VibrationTimer = 0.1f;
						return;
					}
				}
			}
			else if (this.ID > 51)
			{
				if (!this.Montage.gameObject.activeInHierarchy)
				{
					this.Darkness.color = new Color(0f, 0f, 0f, 0f);
					this.Montage.gameObject.SetActive(true);
					this.DeathCorridor.SetActive(false);
					this.PostProcessing.enabled = false;
					this.BloodParent.SetActive(false);
					this.Stalking.SetActive(false);
					this.BGM.volume = 1f;
					this.Speed = 0f;
					this.SetToDefault();
					DepthOfFieldModel.Settings settings3 = this.Profile.depthOfField.settings;
					settings3.focusDistance = 10f;
					this.Profile.depthOfField.settings = settings3;
					BloomModel.Settings settings4 = this.Profile.bloom.settings;
					settings4.bloom.intensity = 0.5f;
					this.Profile.bloom.settings = settings4;
					this.VibrationIntensity = 0f;
				}
				this.Speed += 1f;
				if (this.Speed > 2f)
				{
					this.Speed = 0f;
					this.TextureID++;
					if (this.TextureID == 31)
					{
						this.TextureID = 1;
					}
					if (this.TextureID > 10 && this.TextureID < 21)
					{
						this.PostProcessing.enabled = true;
					}
					else
					{
						this.PostProcessing.enabled = false;
					}
					this.Montage.material.mainTexture = this.Textures[this.TextureID];
				}
				if (this.Timer > 225f)
				{
					this.Darkness.color = new Color(0f, 0f, 0f, 1f);
					return;
				}
				this.VibrationIntensity += Time.deltaTime * 0.2f;
				GamePad.SetVibration(PlayerIndex.One, this.VibrationIntensity, this.VibrationIntensity);
				this.VibrationCheck = true;
				this.VibrationTimer = 0.1f;
				return;
			}
			else
			{
				if (this.ID > 41)
				{
					if (base.transform.position.z < 0f)
					{
						RenderSettings.ambientLight = new Color(0.2f, 0.2f, 0.2f);
						this.AttackPair[3].SetActive(false);
						this.DeathCorridor.SetActive(true);
						this.Stalking.SetActive(false);
						this.Quad.SetActive(false);
						base.transform.position = new Vector3(0f, 1f, 0f);
						base.transform.eulerAngles = new Vector3(0f, 0f, -15f);
						ColorGradingModel.Settings settings5 = this.Profile.colorGrading.settings;
						settings5.basic.saturation = 1f;
						settings5.channelMixer.red = new Vector3(1f, 0f, 0f);
						settings5.channelMixer.green = new Vector3(0f, 1f, 0f);
						settings5.channelMixer.blue = new Vector3(0f, 0f, 1f);
						this.Profile.colorGrading.settings = settings5;
						this.Rotation = -15f;
						this.Speed = 0f;
						this.BGM.volume = 0.5f;
					}
					this.Speed += Time.deltaTime * 0.015f;
					base.transform.position = Vector3.Lerp(base.transform.position, new Vector3(0f, 1f, 34f), Time.deltaTime * this.Speed);
					this.Rotation = Mathf.Lerp(this.Rotation, 15f, Time.deltaTime * this.Speed);
					base.transform.eulerAngles = new Vector3(0f, 0f, this.Rotation);
					if (this.ID < 51)
					{
						this.Alpha = Mathf.MoveTowards(this.Alpha, 0f, Time.deltaTime * 0.2f);
					}
					else
					{
						this.Alpha = 1f;
					}
					this.Darkness.color = new Color(0f, 0f, 0f, this.Alpha);
					return;
				}
				if (this.ID > 33)
				{
					if (this.School.activeInHierarchy)
					{
						this.School.SetActive(false);
						this.Stalking.SetActive(true);
						base.transform.position = new Vector3(-0.02f, 1.12f, 1f);
						base.transform.eulerAngles = new Vector3(0f, 0f, 0f);
						this.SetToDefault();
						this.Speed = 0f;
					}
					if (this.ID < 40)
					{
						this.VibrationIntensity += Time.deltaTime * 0.05f;
						GamePad.SetVibration(PlayerIndex.One, this.VibrationIntensity, this.VibrationIntensity);
						this.VibrationCheck = true;
						this.VibrationTimer = 0.1f;
					}
					if (this.ID < 37)
					{
						this.Speed += Time.deltaTime * 0.1f;
						base.transform.position = Vector3.Lerp(base.transform.position, new Vector3(-0.02f, 1.12f, -0.25f), Time.deltaTime * this.Speed);
						this.Alpha = Mathf.MoveTowards(this.Alpha, 0f, Time.deltaTime * 0.2f);
					}
					else
					{
						if (this.Speed > 0f)
						{
							this.CameraRotationY = 0f;
							this.Speed = 0f;
						}
						this.Speed -= Time.deltaTime * 0.1f;
						base.transform.position = Vector3.Lerp(base.transform.position, new Vector3(0.3f, 0.8f, -0.25f), Time.deltaTime * this.Speed * -1f);
						this.CameraRotationY = Mathf.Lerp(this.CameraRotationY, -15f, Time.deltaTime * this.Speed * -1f);
						base.transform.eulerAngles = new Vector3(0f, this.CameraRotationY, 0f);
						if (this.Timer > 179f)
						{
							this.StalkerAnim.Play("f02_clenchFist_00");
						}
						if (this.ID == 40)
						{
							this.Alpha = 1f;
						}
						if (this.ID > 39)
						{
							this.BGM.volume += Time.deltaTime;
						}
						if (this.Timer > 186f)
						{
							this.DeathCorridor.SetActive(true);
							this.Alpha = 1f;
						}
						else if (this.Timer > 185.6f)
						{
							this.AttackPair[2].SetActive(false);
							this.AttackPair[3].SetActive(true);
							GamePad.SetVibration(PlayerIndex.One, 1f, 1f);
							this.VibrationCheck = true;
							this.VibrationTimer = 0.2f;
							this.Alpha = 0f;
						}
						else if (this.Timer > 185.2f)
						{
							this.Alpha = 1f;
						}
						else if (this.Timer > 184.8f)
						{
							this.AttackPair[1].SetActive(false);
							this.AttackPair[2].SetActive(true);
							GamePad.SetVibration(PlayerIndex.One, 1f, 1f);
							this.VibrationCheck = true;
							this.VibrationTimer = 0.2f;
							this.Alpha = 0f;
						}
						else if (this.Timer > 184.4f)
						{
							this.Alpha = 1f;
						}
						else if (this.Timer > 184f)
						{
							ColorGradingModel.Settings settings6 = this.Profile.colorGrading.settings;
							settings6.channelMixer.red = new Vector3(0.1f, 0f, 0f);
							settings6.channelMixer.green = Vector3.zero;
							settings6.channelMixer.blue = Vector3.zero;
							this.Profile.colorGrading.settings = settings6;
							this.Alpha = 0f;
							this.Stalking.SetActive(false);
							this.Quad.SetActive(true);
							this.AttackPair[1].SetActive(true);
							GamePad.SetVibration(PlayerIndex.One, 1f, 1f);
							this.VibrationCheck = true;
							this.VibrationTimer = 0.2f;
						}
					}
					this.Darkness.color = new Color(0f, 0f, 0f, this.Alpha);
					return;
				}
				if (this.ID > 31)
				{
					if (!this.Osana.activeInHierarchy)
					{
						base.transform.position = new Vector3(0.5f, 1.366666f, 0.25f);
						base.transform.eulerAngles = new Vector3(15f, -67.5f, 0f);
						this.SenpaiAnim.transform.parent.localPosition = new Vector3(0.533333f, 0f, -6.9f);
						this.SenpaiAnim.transform.parent.localEulerAngles = new Vector3(0f, 90f, 0f);
						this.SenpaiAnim.Play("Monday_1");
						this.Osana.SetActive(true);
						DepthOfFieldModel.Settings settings7 = this.Profile.depthOfField.settings;
						settings7.focusDistance = 1.5f;
						this.Profile.depthOfField.settings = settings7;
						this.YandereAnim["f02_Intro_00"].speed = 0.33333f;
						this.Darkness.color = new Color(0f, 0f, 0f, 0f);
						this.Alpha = 0f;
					}
					base.transform.Translate(Vector3.forward * 0.01f * Time.deltaTime, Space.Self);
					this.TurnRed();
					if (this.Narration.time > 156.2f)
					{
						this.Darkness.color = new Color(0f, 0f, 0f, 1f);
						this.Alpha = 1f;
						return;
					}
				}
				else if (this.ID > 27)
				{
					if (base.transform.position.x > 0f)
					{
						base.transform.position = new Vector3(-1.5f, 1f, -1.5f);
						base.transform.eulerAngles = new Vector3(0f, 45f, 0f);
						this.YandereAnim["f02_Intro_00"].time += 0f;
						this.SenpaiAnim["Intro_00"].time += 0f;
						this.YandereAnim["f02_Intro_00"].speed = 1.33333f;
						this.SenpaiAnim["Intro_00"].speed = 1.33333f;
						this.Speed = 0f;
						DepthOfFieldModel.Settings settings8 = this.Profile.depthOfField.settings;
						settings8.focusDistance = 1.5f;
						settings8.aperture = 11.2f;
						this.Profile.depthOfField.settings = settings8;
					}
					if (this.ID > 28)
					{
						this.Speed += Time.deltaTime * 0.1f;
						base.transform.position = Vector3.Lerp(base.transform.position, new Vector3(-1.1f, 1.33333f, 1f), Time.deltaTime * this.Speed);
						base.transform.eulerAngles = Vector3.Lerp(base.transform.eulerAngles, new Vector3(0f, 135f, 0f), Time.deltaTime * this.Speed);
					}
					if (this.ID > 29)
					{
						this.Stars.Stop();
						this.Bubbles.Stop();
					}
					if (this.ID > 30)
					{
						this.TurnNeutral();
						return;
					}
				}
				else
				{
					if (this.ID > 23)
					{
						if (this.EyeTimer == 0f)
						{
							base.transform.position = new Vector3(0f, 0.9f, 0f);
							base.transform.eulerAngles = new Vector3(15f, -45f, 0f);
							this.YandereAnim["f02_Intro_00"].speed = 0.2f;
							this.SenpaiAnim["Intro_00"].speed = 0.2f;
							this.Yandere.materials[2].SetFloat("_BlendAmount", 1f);
							DepthOfFieldModel.Settings settings9 = this.Profile.depthOfField.settings;
							settings9.focusDistance = 1.5f;
							settings9.aperture = 11.2f;
							this.Profile.depthOfField.settings = settings9;
						}
						this.EyeTimer += Time.deltaTime;
						if (this.EyeTimer > 0.1f)
						{
							this.Yandere.materials[2].SetTextureOffset("_OverlayTex", new Vector2(UnityEngine.Random.Range(-0.01f, 0.01f), UnityEngine.Random.Range(-0.01f, 0.01f)));
							this.EyeTimer -= 0.1f;
						}
						base.transform.Translate(Vector3.forward * -0.1f * Time.deltaTime, Space.Self);
						return;
					}
					if (this.ID > 19)
					{
						if (this.Room.gameObject.activeInHierarchy)
						{
							this.Room.gameObject.SetActive(false);
							this.School.SetActive(true);
							base.transform.localPosition = new Vector3(-3f, 1f, 1.5f);
							this.Darkness.color = new Color(0f, 0f, 0f, 1f);
							this.Alpha = 1f;
							this.Speed = 0f;
							ColorGradingModel.Settings settings10 = this.Profile.colorGrading.settings;
							settings10.basic.saturation = 0f;
							this.Profile.colorGrading.settings = settings10;
							DepthOfFieldModel.Settings settings11 = this.Profile.depthOfField.settings;
							settings11.focusDistance = 10f;
							settings11.aperture = 5.6f;
							this.Profile.depthOfField.settings = settings11;
							this.Yandere.materials[2].SetFloat("_BlendAmount", 0f);
						}
						if (this.Narration.time < 94.898f)
						{
							base.transform.position = Vector3.MoveTowards(base.transform.position, new Vector3(0f, 1f, -2f), Time.deltaTime * 1.09f);
						}
						else
						{
							DepthOfFieldModel.Settings settings12 = this.Profile.depthOfField.settings;
							settings12.focusDistance = 1.2f;
							settings12.aperture = 11.2f;
							this.Profile.depthOfField.settings = settings12;
							if (this.Narration.time < 101.5f)
							{
								base.transform.position = new Vector3(-0.25f, 0.75f, -0.25f);
								base.transform.eulerAngles = new Vector3(22.5f, -45f, 0f);
								this.Senpai.transform.position = new Vector3(0f, -0.2f, 0f);
							}
							else
							{
								this.Speed += Time.deltaTime * 0.5f;
								this.Senpai.transform.position = Vector3.Lerp(this.Senpai.transform.position, new Vector3(0f, 0f, 0f), Time.deltaTime * this.Speed * 2f);
								base.transform.position = Vector3.Lerp(base.transform.position, new Vector3(-0.25f, 0.66666f, -1.25f), Time.deltaTime * this.Speed * 0.5f);
								this.CameraRotationX = Mathf.Lerp(this.CameraRotationX, 0f, Time.deltaTime * this.Speed);
								this.CameraRotationY = Mathf.Lerp(this.CameraRotationY, 0f, Time.deltaTime * this.Speed);
								base.transform.eulerAngles = new Vector3(this.CameraRotationX, this.CameraRotationY, 0f);
							}
						}
						if (this.ID > 21)
						{
							this.YandereAnim["f02_Intro_00"].speed = Mathf.MoveTowards(this.YandereAnim["f02_Intro_00"].speed, 0.1f, Time.deltaTime);
							this.SenpaiAnim["Intro_00"].speed = Mathf.MoveTowards(this.SenpaiAnim["Intro_00"].speed, 0.1f, Time.deltaTime);
							if (this.Narration.time > 106.5f)
							{
								ColorGradingModel.Settings settings13 = this.Profile.colorGrading.settings;
								settings13.basic.saturation = Mathf.MoveTowards(settings13.basic.saturation, 2f, Time.deltaTime);
								settings13.channelMixer.red = Vector3.MoveTowards(settings13.channelMixer.red, new Vector3(2f, 0f, 0f), Time.deltaTime);
								settings13.channelMixer.blue = Vector3.MoveTowards(settings13.channelMixer.blue, new Vector3(0f, 0f, 2f), Time.deltaTime);
								this.Profile.colorGrading.settings = settings13;
								this.Particles.SetActive(true);
							}
						}
						else if (this.Narration.time > 98f)
						{
							this.YandereAnim["f02_Intro_00"].speed = 1f;
							this.SenpaiAnim["Intro_00"].speed = 1f;
						}
						else if (this.Narration.time > 97f)
						{
							this.YandereAnim["f02_Intro_00"].speed = 3f;
							this.SenpaiAnim["Intro_00"].speed = 3f;
						}
						this.Alpha = Mathf.MoveTowards(this.Alpha, 0f, Time.deltaTime * 0.2f);
						this.Darkness.color = new Color(0f, 0f, 0f, this.Alpha);
						return;
					}
					if (this.ID > 15)
					{
						if (this.ParentAndChild.gameObject.activeInHierarchy)
						{
							this.ParentAndChild.gameObject.SetActive(false);
							this.Room.SetActive(true);
							base.transform.position = new Vector3(0f, 1f, 0f);
							this.Darkness.color = new Color(0f, 0f, 0f, 1f);
							this.Alpha = 1f;
							this.Speed = 0.1f;
							DepthOfFieldModel.Settings settings14 = this.Profile.depthOfField.settings;
							settings14.focusDistance = 10f;
							this.Profile.depthOfField.settings = settings14;
						}
						base.transform.position -= new Vector3(0f, 0f, Time.deltaTime * this.Speed * 0.75f);
						if (this.Narration.time > 88.898f)
						{
							this.Alpha = Mathf.MoveTowards(this.Alpha, 1f, Time.deltaTime * 0.66666f);
						}
						else
						{
							this.Alpha = Mathf.MoveTowards(this.Alpha, 0f, Time.deltaTime * 0.2f);
						}
						this.Darkness.color = new Color(0f, 0f, 0f, this.Alpha);
						return;
					}
					if (this.ID > 13)
					{
						if (this.ConfessionScene.gameObject.activeInHierarchy)
						{
							this.ConfessionScene.gameObject.SetActive(false);
							this.ParentAndChild.SetActive(true);
							this.X = 220f;
							this.Y = -90f;
							this.X2 = 150f;
							this.Y2 = -90f;
							base.transform.position = new Vector3(0f, 0.5f, -1f);
							this.Darkness.color = new Color(0f, 0f, 0f, 1f);
							this.Alpha = 1f;
							this.Speed = 0.1f;
							ColorGradingModel.Settings settings15 = this.Profile.colorGrading.settings;
							settings15.basic.saturation = 1f;
							this.Profile.colorGrading.settings = settings15;
							BloomModel.Settings settings16 = this.Profile.bloom.settings;
							settings16.bloom.intensity = 1f;
							this.Profile.bloom.settings = settings16;
							DepthOfFieldModel.Settings settings17 = this.Profile.depthOfField.settings;
							settings17.focusDistance = 10f;
							settings17.aperture = 11.2f;
							this.Profile.depthOfField.settings = settings17;
						}
						base.transform.position -= new Vector3(0f, 0f, Time.deltaTime * this.Speed);
						if (this.Narration.time > 69.898f)
						{
							this.Alpha = Mathf.MoveTowards(this.Alpha, 1f, Time.deltaTime * 0.5f);
						}
						else
						{
							this.Alpha = Mathf.MoveTowards(this.Alpha, 0f, Time.deltaTime * 0.2f);
						}
						this.Darkness.color = new Color(0f, 0f, 0f, this.Alpha);
						return;
					}
					if (this.ID > 9)
					{
						if (this.HoleInChestAnim.gameObject.activeInHierarchy)
						{
							this.HoleInChestAnim.gameObject.SetActive(false);
							this.ConfessionScene.SetActive(true);
							base.transform.position = new Vector3(0f, 1f, -1f);
							this.Darkness.color = new Color(0f, 0f, 0f, 1f);
							this.Alpha = 1f;
							this.Speed = 0.1f;
							DepthOfFieldModel.Settings settings18 = this.Profile.depthOfField.settings;
							settings18.focusDistance = 1f;
							this.Profile.depthOfField.settings = settings18;
							ColorGradingModel.Settings settings19 = this.Profile.colorGrading.settings;
							settings19.basic.saturation = 0f;
							this.Profile.colorGrading.settings = settings19;
						}
						base.transform.position -= new Vector3(0f, 0f, Time.deltaTime * this.Speed);
						if (this.ID > 10)
						{
							this.YoungRyobaAnim["f02_introHoldHands_00"].speed = 0.5f;
							this.YoungRyobaAnim.Play();
							this.YoungFatherAnim["introHoldHands_00"].speed = 0.5f;
							this.YoungFatherAnim.Play();
							this.Brightness = Mathf.MoveTowards(this.Brightness, 1f, Time.deltaTime * 0.25f);
							this.BrightenEnvironment();
							this.Blossoms.Play();
						}
						if (this.ID > 11)
						{
							ColorGradingModel.Settings settings20 = this.Profile.colorGrading.settings;
							settings20.basic.saturation = settings20.basic.saturation + Time.deltaTime * 0.25f;
							BloomModel.Settings settings21 = this.Profile.bloom.settings;
							settings21.bloom.intensity = 1f + settings20.basic.saturation;
							this.Profile.bloom.settings = settings21;
							this.Profile.colorGrading.settings = settings20;
						}
						if (this.Narration.time > 56.898f)
						{
							this.Alpha = Mathf.MoveTowards(this.Alpha, 1f, Time.deltaTime * 0.5f);
						}
						else
						{
							this.Alpha = Mathf.MoveTowards(this.Alpha, 0f, Time.deltaTime * 0.2f);
						}
						this.Darkness.color = new Color(0f, 0f, 0f, this.Alpha);
						return;
					}
					if (this.ID > 4)
					{
						if (this.BloodyHandsAnim.gameObject.activeInHierarchy)
						{
							base.transform.position = new Vector3(0.012f, 1.13f, 0.029f);
							base.transform.eulerAngles = Vector3.zero;
							this.BloodyHandsAnim.gameObject.SetActive(false);
							this.HoleInChestAnim.gameObject.SetActive(true);
							this.Alpha = 0f;
							this.Darkness.color = new Color(0f, 0f, 0f, this.Alpha);
							this.SetToDefault();
						}
						this.Speed = Mathf.MoveTowards(this.Speed, 0.1f, Time.deltaTime * 0.01f);
						base.transform.position -= new Vector3(0f, 0f, Time.deltaTime * this.Speed);
						if (base.transform.position.z < -0.05f)
						{
							this.SecondSpeed = Mathf.MoveTowards(this.SecondSpeed, 0.1f, Time.deltaTime * 0.1f);
							base.transform.position = Vector3.Lerp(base.transform.position, new Vector3(base.transform.position.x, 1f, base.transform.position.z), Time.deltaTime * this.SecondSpeed);
						}
						if (base.transform.position.z < -0.5f)
						{
							this.HoleInChestAnim["f02_holeInChest_00"].speed = 0.5f;
							this.HoleInChestAnim.Play();
						}
						if (this.ID > 8)
						{
							this.Alpha = Mathf.MoveTowards(this.Alpha, 1f, Time.deltaTime * 0.2f);
							this.Darkness.color = new Color(0f, 0f, 0f, this.Alpha);
							return;
						}
					}
					else if (this.ID > 0)
					{
						if (this.Timer < 2f)
						{
							this.BloodyHandsAnim["f02_clenchFists_00"].time = 0.6f;
							this.BloodyHandsAnim["f02_clenchFists_00"].speed = 0f;
						}
						else
						{
							this.BloodyHandsAnim["f02_clenchFists_00"].speed = 0.07f;
						}
						if (this.ID > 3)
						{
							this.Alpha = 1f;
							this.Darkness.color = new Color(0f, 0f, 0f, this.Alpha);
							this.BGM.volume = Mathf.MoveTowards(this.BGM.volume, 0.5f, Time.deltaTime * 0.266666f);
						}
					}
				}
			}
		}
	}

	// Token: 0x0600177C RID: 6012 RVA: 0x000CDDD0 File Offset: 0x000CBFD0
	private void LateUpdate()
	{
		if (this.New)
		{
			if (this.ID < 41)
			{
				if (this.Narration.time > 103f)
				{
					if (this.ID < 24)
					{
						this.LeftArm.localEulerAngles = new Vector3(0f, 15f, 0f);
						this.BookRight.localEulerAngles = new Vector3(0f, 180f, -90f);
						this.BookLeft.localEulerAngles = new Vector3(0f, 180f, 0f);
						this.BookRight.parent.parent.localPosition = new Vector3(-0.12f, -0.04f, 0f);
						this.BookRight.parent.parent.localEulerAngles = new Vector3(0f, -15f, -135f);
						this.BookRight.parent.parent.parent.localEulerAngles = new Vector3(45f, 45f, 0f);
					}
					else
					{
						this.BookRight.parent.parent.localPosition = new Vector3(-0.075f, -0.085f, 0f);
						this.BookRight.parent.parent.localEulerAngles = new Vector3(0f, -45f, -90f);
						this.BookRight.localEulerAngles = new Vector3(0f, 180f, -45f);
						this.BookLeft.localEulerAngles = new Vector3(0f, 180f, 45f);
					}
				}
				else if (this.Narration.time > 94.898f)
				{
					this.Rotation = 22.5f;
				}
			}
			if (this.Narration.time > 104f && this.Narration.time < 190f)
			{
				this.ThirdSpeed += Time.deltaTime;
				this.Rotation = Mathf.Lerp(this.Rotation, 0f, Time.deltaTime * 3.6f * this.ThirdSpeed);
			}
			this.Neck.localEulerAngles += new Vector3(this.Rotation, 0f, 0f);
			if (this.Narration.time > 99f)
			{
				this.Weight = Mathf.MoveTowards(this.Weight, 0f, Time.deltaTime * 20f);
			}
			else if (this.Narration.time > 96.648f)
			{
				this.Weight = Mathf.MoveTowards(this.Weight, 50f, Time.deltaTime * 100f);
			}
			this.Yandere.SetBlendShapeWeight(5, this.Weight);
			this.Ponytail.transform.eulerAngles = new Vector3(this.X, this.Y, this.Z);
			this.RightHair.transform.eulerAngles = new Vector3(this.X2, this.Y2, this.Z2);
			this.LeftHair.transform.eulerAngles = new Vector3(this.X2, this.Y2, this.Z2);
			this.Ponytail2.transform.eulerAngles = new Vector3(this.X, this.Y, this.Z);
			this.RightHair2.transform.eulerAngles = new Vector3(this.X2, this.Y2, this.Z2);
			this.LeftHair2.transform.eulerAngles = new Vector3(this.X2, this.Y2, this.Z2);
			this.RightHand.localEulerAngles += new Vector3(UnityEngine.Random.Range(1f, -1f), UnityEngine.Random.Range(1f, -1f), UnityEngine.Random.Range(1f, -1f));
		}
	}

	// Token: 0x0600177D RID: 6013 RVA: 0x000CE1E0 File Offset: 0x000CC3E0
	private void LoveSickCheck()
	{
		if (GameGlobals.LoveSick)
		{
			Camera.main.backgroundColor = new Color(0f, 0f, 0f, 1f);
			foreach (GameObject gameObject in UnityEngine.Object.FindObjectsOfType<GameObject>())
			{
				UISprite component = gameObject.GetComponent<UISprite>();
				if (component != null)
				{
					component.color = new Color(1f, 0f, 0f, component.color.a);
				}
				UITexture component2 = gameObject.GetComponent<UITexture>();
				if (component2 != null)
				{
					component2.color = new Color(1f, 0f, 0f, component2.color.a);
				}
				UILabel component3 = gameObject.GetComponent<UILabel>();
				if (component3 != null && component3.color != Color.black)
				{
					component3.color = new Color(1f, 0f, 0f, component3.color.a);
				}
			}
			this.FadeOutDarkness.color = new Color(0f, 0f, 0f, 0f);
			this.LoveSickLogo.enabled = true;
			this.Logo.enabled = false;
			return;
		}
		this.LoveSickLogo.enabled = false;
	}

	// Token: 0x0600177E RID: 6014 RVA: 0x000CE338 File Offset: 0x000CC538
	private void BrightenEnvironment()
	{
		this.TreeRenderer[0].materials[0].color = new Color(this.Brightness, this.Brightness, this.Brightness, 1f);
		this.TreeRenderer[0].materials[1].color = new Color(this.Brightness, this.Brightness, this.Brightness, 1f);
		this.TreeRenderer[1].materials[0].color = new Color(this.Brightness, this.Brightness, this.Brightness, 1f);
		this.TreeRenderer[1].materials[1].color = new Color(this.Brightness, this.Brightness, this.Brightness, 1f);
		this.TreeRenderer[2].materials[0].color = new Color(this.Brightness, this.Brightness, this.Brightness, 1f);
		this.TreeRenderer[2].materials[1].color = new Color(this.Brightness, this.Brightness, this.Brightness, 1f);
		this.Mound.material.color = new Color(this.Brightness, this.Brightness, this.Brightness, 1f);
		this.Sky.material.color = new Color(this.Brightness, this.Brightness, this.Brightness, 1f);
		this.YoungFatherRenderer.materials[0].color = new Color(this.Brightness, this.Brightness, this.Brightness, 1f);
		this.YoungFatherRenderer.materials[1].color = new Color(this.Brightness, this.Brightness, this.Brightness, 1f);
		this.YoungFatherRenderer.materials[2].color = new Color(this.Brightness, this.Brightness, this.Brightness, 1f);
		this.YoungFatherHairRenderer.material.color = new Color(this.Brightness * 0.5f, this.Brightness * 0.5f, this.Brightness * 0.5f, 1f);
	}

	// Token: 0x0600177F RID: 6015 RVA: 0x000CE588 File Offset: 0x000CC788
	private void TurnNeutral()
	{
		ColorGradingModel.Settings settings = this.Profile.colorGrading.settings;
		settings.channelMixer.red = Vector3.MoveTowards(settings.channelMixer.red, new Vector3(1f, 0f, 0f), Time.deltaTime * 0.66666f);
		settings.channelMixer.green = Vector3.MoveTowards(settings.channelMixer.green, new Vector3(0f, 1f, 0f), Time.deltaTime * 0.66666f);
		settings.channelMixer.blue = Vector3.MoveTowards(settings.channelMixer.blue, new Vector3(0f, 0f, 1f), Time.deltaTime * 0.66666f);
		this.Profile.colorGrading.settings = settings;
	}

	// Token: 0x06001780 RID: 6016 RVA: 0x000CE668 File Offset: 0x000CC868
	private void TurnRed()
	{
		ColorGradingModel.Settings settings = this.Profile.colorGrading.settings;
		settings.basic.saturation = Mathf.MoveTowards(settings.basic.saturation, 1f, Time.deltaTime * 0.1f);
		settings.channelMixer.red = Vector3.MoveTowards(settings.channelMixer.red, new Vector3(1f, 0f, 0f), Time.deltaTime * 0.1f);
		settings.channelMixer.green = Vector3.MoveTowards(settings.channelMixer.green, new Vector3(0f, 0.5f, 0f), Time.deltaTime * 0.1f);
		settings.channelMixer.blue = Vector3.MoveTowards(settings.channelMixer.blue, new Vector3(0f, 0f, 0.5f), Time.deltaTime * 0.1f);
		this.Profile.colorGrading.settings = settings;
	}

	// Token: 0x06001781 RID: 6017 RVA: 0x000CE774 File Offset: 0x000CC974
	private void SetToDefault()
	{
		ColorGradingModel.Settings settings = this.Profile.colorGrading.settings;
		settings.basic.saturation = 1f;
		settings.channelMixer.red = new Vector3(1f, 0f, 0f);
		settings.channelMixer.green = new Vector3(0f, 1f, 0f);
		settings.channelMixer.blue = new Vector3(0f, 0f, 1f);
		this.Profile.colorGrading.settings = settings;
		DepthOfFieldModel.Settings settings2 = this.Profile.depthOfField.settings;
		settings2.focusDistance = 10f;
		settings2.aperture = 11.2f;
		this.Profile.depthOfField.settings = settings2;
		BloomModel.Settings settings3 = this.Profile.bloom.settings;
		settings3.bloom.intensity = 1f;
		this.Profile.bloom.settings = settings3;
	}

	// Token: 0x040020D8 RID: 8408
	public PostProcessingBehaviour PostProcessing;

	// Token: 0x040020D9 RID: 8409
	public PostProcessingBehaviour GUIPP;

	// Token: 0x040020DA RID: 8410
	public PostProcessingProfile Profile;

	// Token: 0x040020DB RID: 8411
	public GameObject[] AttackPair;

	// Token: 0x040020DC RID: 8412
	public GameObject ConfessionScene;

	// Token: 0x040020DD RID: 8413
	public GameObject ParentAndChild;

	// Token: 0x040020DE RID: 8414
	public GameObject DeathCorridor;

	// Token: 0x040020DF RID: 8415
	public GameObject BloodParent;

	// Token: 0x040020E0 RID: 8416
	public GameObject Particles;

	// Token: 0x040020E1 RID: 8417
	public GameObject Stalking;

	// Token: 0x040020E2 RID: 8418
	public GameObject School;

	// Token: 0x040020E3 RID: 8419
	public GameObject Osana;

	// Token: 0x040020E4 RID: 8420
	public GameObject Room;

	// Token: 0x040020E5 RID: 8421
	public GameObject Quad;

	// Token: 0x040020E6 RID: 8422
	public Texture[] Textures;

	// Token: 0x040020E7 RID: 8423
	public Transform RightForeArm;

	// Token: 0x040020E8 RID: 8424
	public Transform LeftForeArm;

	// Token: 0x040020E9 RID: 8425
	public Transform RightWrist;

	// Token: 0x040020EA RID: 8426
	public Transform LeftWrist;

	// Token: 0x040020EB RID: 8427
	public Transform Corridors;

	// Token: 0x040020EC RID: 8428
	public Transform RightHand;

	// Token: 0x040020ED RID: 8429
	public Transform Senpai;

	// Token: 0x040020EE RID: 8430
	public Transform Head;

	// Token: 0x040020EF RID: 8431
	public Animation BloodyHandsAnim;

	// Token: 0x040020F0 RID: 8432
	public Animation HoleInChestAnim;

	// Token: 0x040020F1 RID: 8433
	public Animation YoungFatherAnim;

	// Token: 0x040020F2 RID: 8434
	public Animation YoungRyobaAnim;

	// Token: 0x040020F3 RID: 8435
	public Animation StalkerAnim;

	// Token: 0x040020F4 RID: 8436
	public Animation YandereAnim;

	// Token: 0x040020F5 RID: 8437
	public Animation SenpaiAnim;

	// Token: 0x040020F6 RID: 8438
	public Animation MotherAnim;

	// Token: 0x040020F7 RID: 8439
	public Animation ChildAnim;

	// Token: 0x040020F8 RID: 8440
	public Animation[] AttackAnim;

	// Token: 0x040020F9 RID: 8441
	public Renderer[] TreeRenderer;

	// Token: 0x040020FA RID: 8442
	public Renderer YoungFatherHairRenderer;

	// Token: 0x040020FB RID: 8443
	public Renderer YoungFatherRenderer;

	// Token: 0x040020FC RID: 8444
	public Renderer Montage;

	// Token: 0x040020FD RID: 8445
	public Renderer Mound;

	// Token: 0x040020FE RID: 8446
	public Renderer Sky;

	// Token: 0x040020FF RID: 8447
	public SkinnedMeshRenderer Yandere;

	// Token: 0x04002100 RID: 8448
	public ParticleSystem Blossoms;

	// Token: 0x04002101 RID: 8449
	public ParticleSystem Bubbles;

	// Token: 0x04002102 RID: 8450
	public ParticleSystem Stars;

	// Token: 0x04002103 RID: 8451
	public UISprite FadeOutDarkness;

	// Token: 0x04002104 RID: 8452
	public UITexture LoveSickLogo;

	// Token: 0x04002105 RID: 8453
	public UIPanel SkipPanel;

	// Token: 0x04002106 RID: 8454
	public UISprite Darkness;

	// Token: 0x04002107 RID: 8455
	public UISprite Circle;

	// Token: 0x04002108 RID: 8456
	public UITexture Logo;

	// Token: 0x04002109 RID: 8457
	public UILabel Label;

	// Token: 0x0400210A RID: 8458
	public AudioSource Narration;

	// Token: 0x0400210B RID: 8459
	public AudioSource BGM;

	// Token: 0x0400210C RID: 8460
	public string[] Lines;

	// Token: 0x0400210D RID: 8461
	public float[] Cue;

	// Token: 0x0400210E RID: 8462
	public bool Narrating;

	// Token: 0x0400210F RID: 8463
	public bool Musicing;

	// Token: 0x04002110 RID: 8464
	public bool FadeOut;

	// Token: 0x04002111 RID: 8465
	public bool New;

	// Token: 0x04002112 RID: 8466
	public float CameraRotationX;

	// Token: 0x04002113 RID: 8467
	public float CameraRotationY;

	// Token: 0x04002114 RID: 8468
	public float ThirdSpeed;

	// Token: 0x04002115 RID: 8469
	public float SecondSpeed;

	// Token: 0x04002116 RID: 8470
	public float Speed;

	// Token: 0x04002117 RID: 8471
	public float Brightness;

	// Token: 0x04002118 RID: 8472
	public float StartTimer;

	// Token: 0x04002119 RID: 8473
	public float SkipTimer;

	// Token: 0x0400211A RID: 8474
	public float EyeTimer;

	// Token: 0x0400211B RID: 8475
	public float Alpha;

	// Token: 0x0400211C RID: 8476
	public float Timer;

	// Token: 0x0400211D RID: 8477
	public float AnimTimer;

	// Token: 0x0400211E RID: 8478
	public int TextureID;

	// Token: 0x0400211F RID: 8479
	public int ID;

	// Token: 0x04002120 RID: 8480
	public float VibrationIntensity;

	// Token: 0x04002121 RID: 8481
	public float VibrationTimer;

	// Token: 0x04002122 RID: 8482
	public bool VibrationCheck;

	// Token: 0x04002123 RID: 8483
	public Transform RightHair;

	// Token: 0x04002124 RID: 8484
	public Transform LeftHair;

	// Token: 0x04002125 RID: 8485
	public Transform Ponytail;

	// Token: 0x04002126 RID: 8486
	public Transform RightHair2;

	// Token: 0x04002127 RID: 8487
	public Transform LeftHair2;

	// Token: 0x04002128 RID: 8488
	public Transform Ponytail2;

	// Token: 0x04002129 RID: 8489
	public Transform BookRight;

	// Token: 0x0400212A RID: 8490
	public Transform BookLeft;

	// Token: 0x0400212B RID: 8491
	public Transform LeftArm;

	// Token: 0x0400212C RID: 8492
	public Transform Neck;

	// Token: 0x0400212D RID: 8493
	public float Rotation;

	// Token: 0x0400212E RID: 8494
	public float Weight;

	// Token: 0x0400212F RID: 8495
	public float X;

	// Token: 0x04002130 RID: 8496
	public float Y;

	// Token: 0x04002131 RID: 8497
	public float Z;

	// Token: 0x04002132 RID: 8498
	public float X2;

	// Token: 0x04002133 RID: 8499
	public float Y2;

	// Token: 0x04002134 RID: 8500
	public float Z2;
}
