using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000423 RID: 1059
public class TitleMenuScript : MonoBehaviour
{
	// Token: 0x06001C35 RID: 7221 RVA: 0x00150540 File Offset: 0x0014E740
	private void Awake()
	{
		Animation component = this.Yandere.GetComponent<Animation>();
		component["f02_yanderePose_00"].layer = 1;
		component.Blend("f02_yanderePose_00");
		component["f02_fist_00"].layer = 2;
		component.Blend("f02_fist_00");
	}

	// Token: 0x06001C36 RID: 7222 RVA: 0x00150590 File Offset: 0x0014E790
	private void Start()
	{
		if (GameGlobals.LoveSick)
		{
			this.LoveSick = true;
		}
		this.PromptBar.Label[0].text = "Confirm";
		this.PromptBar.Label[1].text = string.Empty;
		this.PromptBar.UpdateButtons();
		this.MediumColor = this.MediumSprites[0].color;
		this.LightColor = this.LightSprites[0].color;
		this.DarkColor = this.DarkSprites[0].color;
		if (!this.LoveSick)
		{
			base.transform.position = new Vector3(base.transform.position.x, 1.2f, base.transform.position.z);
			this.LoveSickLogo.SetActive(false);
			this.LoveSickMusic.volume = 0f;
			this.Grayscale.enabled = false;
			this.SSAO.enabled = false;
			this.Sun.SetActive(true);
			this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, 1f);
			this.TurnCute();
			RenderSettings.ambientLight = new Color(0.75f, 0.75f, 0.75f, 1f);
			RenderSettings.skybox.SetColor("_Tint", new Color(0.5f, 0.5f, 0.5f));
		}
		else
		{
			base.transform.position = new Vector3(base.transform.position.x, 101.2f, base.transform.position.z);
			this.Sun.SetActive(false);
			this.SSAO.enabled = true;
			this.FadeSpeed = 0.2f;
			this.Darkness.color = new Color(0f, 0f, 0f, 1f);
			this.TurnLoveSick();
		}
		Time.timeScale = 1f;
	}

	// Token: 0x06001C37 RID: 7223 RVA: 0x001507BC File Offset: 0x0014E9BC
	private void Update()
	{
		if (this.LoveSick)
		{
			this.Timer += Time.deltaTime * 0.001f;
			if (base.transform.position.z > -18f)
			{
				this.LateTimer = Mathf.Lerp(this.LateTimer, this.Timer, Time.deltaTime);
				this.RotationY = Mathf.Lerp(this.RotationY, -22.5f, Time.deltaTime * this.LateTimer);
			}
			this.RotationZ = Mathf.Lerp(this.RotationZ, 22.5f, Time.deltaTime * this.Timer);
			base.transform.position = Vector3.Lerp(base.transform.position, new Vector3(0.33333f, 101.45f, -16.5f), Time.deltaTime * this.Timer);
			base.transform.eulerAngles = new Vector3(0f, this.RotationY, this.RotationZ);
			if (!this.Turning)
			{
				if (base.transform.position.z > -17f)
				{
					this.LoveSickYandere.CrossFade("f02_edgyTurn_00");
					this.VictimHead.parent = this.RightHand;
					this.Turning = true;
				}
			}
			else if (this.LoveSickYandere["f02_edgyTurn_00"].time >= this.LoveSickYandere["f02_edgyTurn_00"].length)
			{
				this.LoveSickYandere.CrossFade("f02_edgyOverShoulder_00");
			}
		}
		if (!this.Sponsors.Show && !this.SaveFiles.Show && !this.Extras.Show)
		{
			this.InputTimer += Time.deltaTime;
			if (this.InputTimer > 1f)
			{
				if (this.InputManager.TappedDown)
				{
					this.Selected = ((this.Selected < this.SelectionCount - 1) ? (this.Selected + 1) : 0);
				}
				if (this.InputManager.TappedUp)
				{
					this.Selected = ((this.Selected > 0) ? (this.Selected - 1) : (this.SelectionCount - 1));
				}
				if (this.InputManager.TappedUp || this.InputManager.TappedDown)
				{
					this.Highlight.localPosition = new Vector3(this.Highlight.localPosition.x, 225f - 75f * (float)this.Selected, this.Highlight.localPosition.z);
				}
				if (Input.GetButtonDown("A"))
				{
					if (this.Selected == 0 || this.Selected == 2 || this.Selected == 5 || this.Selected == 7)
					{
						this.Darkness.color = new Color(0f, 0f, 0f, this.Darkness.color.a);
						this.InputTimer = -10f;
						this.FadeOut = true;
						this.Fading = true;
					}
					else if (this.Selected == 1)
					{
						this.PromptBar.Label[0].text = "Select";
						this.PromptBar.Label[1].text = "Back";
						this.PromptBar.Label[2].text = "Delete";
						this.PromptBar.UpdateButtons();
						this.SaveFiles.Show = true;
					}
					else if (this.Selected == 3)
					{
						this.PromptBar.Label[0].text = "Visit";
						this.PromptBar.Label[1].text = "Back";
						this.PromptBar.UpdateButtons();
						this.Sponsors.Show = true;
					}
					else if (this.Selected == 6)
					{
						this.PromptBar.Label[1].text = "Back";
						this.PromptBar.UpdateButtons();
						this.Extras.Show = true;
					}
					if (!this.LoveSick)
					{
						this.TurnCute();
					}
				}
				if (Input.GetKeyDown("l"))
				{
					GameGlobals.LoveSick = !GameGlobals.LoveSick;
					SceneManager.LoadScene("TitleScene");
				}
				Input.GetKeyDown("m");
				if (!this.LoveSick)
				{
					if (this.NeverChange)
					{
						this.Timer = 0f;
					}
					if (Input.GetKeyDown(KeyCode.Space))
					{
						this.Timer = 10f;
					}
					this.Timer += Time.deltaTime;
					if (this.Timer > 10f)
					{
						this.TurnDark();
					}
					if (this.Timer > 11f)
					{
						this.TurnCute();
					}
				}
			}
		}
		else
		{
			if (this.Sponsors.Show)
			{
				int sponsorIndex = this.Sponsors.GetSponsorIndex();
				if (this.Sponsors.SponsorHasWebsite(sponsorIndex))
				{
					this.PromptBar.Label[0].text = "Visit";
					this.PromptBar.UpdateButtons();
				}
				else
				{
					this.PromptBar.Label[0].text = string.Empty;
					this.PromptBar.UpdateButtons();
				}
			}
			else if (this.SaveFiles.Show)
			{
				if (this.SaveFiles.SaveDatas[this.SaveFiles.ID].EmptyFile.activeInHierarchy)
				{
					this.PromptBar.Label[0].text = "Create New";
					this.PromptBar.Label[2].text = "";
					this.PromptBar.UpdateButtons();
				}
				else
				{
					this.PromptBar.Label[0].text = "Load";
					this.PromptBar.Label[2].text = "Delete";
					this.PromptBar.UpdateButtons();
				}
			}
			if (Input.GetButtonDown("B") && !this.SaveFiles.ConfirmationWindow.activeInHierarchy)
			{
				this.SaveFiles.Show = false;
				this.Sponsors.Show = false;
				this.Extras.Show = false;
				this.PromptBar.Label[0].text = "Confirm";
				this.PromptBar.Label[1].text = string.Empty;
				this.PromptBar.Label[2].text = string.Empty;
				this.PromptBar.UpdateButtons();
			}
		}
		if (this.Fading)
		{
			if (!this.FadeOut)
			{
				if (this.Darkness.color.a > 0f)
				{
					this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, this.Darkness.color.a - Time.deltaTime * this.FadeSpeed);
					if (this.Darkness.color.a <= 0f)
					{
						this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, 0f);
						this.Fading = false;
					}
				}
			}
			else if (this.Darkness.color.a < 1f)
			{
				MissionModeGlobals.MissionMode = false;
				this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, this.Darkness.color.a + Time.deltaTime);
				if (this.Darkness.color.a >= 1f)
				{
					if (this.Selected == 0)
					{
						GameGlobals.Profile = 1;
						SceneManager.LoadScene("CalendarScene");
					}
					else if (this.Selected == 1)
					{
						if (this.LoveSick)
						{
							GameGlobals.LoveSick = true;
						}
						if (PlayerPrefs.GetInt("ProfileCreated_" + GameGlobals.Profile) == 0)
						{
							PlayerPrefs.SetInt("ProfileCreated_" + GameGlobals.Profile, 1);
							PlayerGlobals.Money = 10f;
							DateGlobals.Weekday = DayOfWeek.Sunday;
							SceneManager.LoadScene("SenpaiScene");
						}
						else
						{
							SceneManager.LoadScene("CalendarScene");
						}
					}
					else if (this.Selected == 2)
					{
						SceneManager.LoadScene("MissionModeScene");
					}
					else if (this.Selected == 5)
					{
						SceneManager.LoadScene("CreditsScene");
					}
					else if (this.Selected == 7)
					{
						Application.Quit();
					}
				}
				this.LoveSickMusic.volume -= Time.deltaTime;
				this.CuteMusic.volume -= Time.deltaTime;
			}
		}
		if (this.Timer < 10f)
		{
			Animation component = this.Yandere.GetComponent<Animation>();
			component["f02_yanderePose_00"].weight = 0f;
			component["f02_fist_00"].weight = 0f;
		}
		if (Input.GetKeyDown(KeyCode.Minus))
		{
			Time.timeScale -= 1f;
		}
		if (Input.GetKeyDown(KeyCode.Equals))
		{
			Time.timeScale += 1f;
		}
	}

	// Token: 0x06001C38 RID: 7224 RVA: 0x00151114 File Offset: 0x0014F314
	private void LateUpdate()
	{
		if (this.Knife.activeInHierarchy)
		{
			foreach (Transform transform in this.Spine)
			{
				transform.transform.localEulerAngles = new Vector3(transform.transform.localEulerAngles.x + 5f, transform.transform.localEulerAngles.y, transform.transform.localEulerAngles.z);
			}
			Transform transform2 = this.Arm[0];
			transform2.transform.localEulerAngles = new Vector3(transform2.transform.localEulerAngles.x, transform2.transform.localEulerAngles.y, transform2.transform.localEulerAngles.z - 15f);
			Transform transform3 = this.Arm[1];
			transform3.transform.localEulerAngles = new Vector3(transform3.transform.localEulerAngles.x, transform3.transform.localEulerAngles.y, transform3.transform.localEulerAngles.z + 15f);
		}
	}

	// Token: 0x06001C39 RID: 7225 RVA: 0x00151234 File Offset: 0x0014F434
	private void TurnDark()
	{
		GameObjectUtils.SetLayerRecursively(this.Yandere.transform.parent.gameObject, 14);
		Animation component = this.Yandere.GetComponent<Animation>();
		component["f02_yanderePose_00"].weight = 1f;
		component["f02_fist_00"].weight = 1f;
		component.Play("f02_fist_00");
		Renderer renderer = this.YandereEye[0];
		renderer.material.color = new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, 1f);
		Renderer renderer2 = this.YandereEye[1];
		renderer2.material.color = new Color(renderer2.material.color.r, renderer2.material.color.g, renderer2.material.color.b, 1f);
		this.ColorCorrection.enabled = true;
		this.BloodProjector.SetActive(true);
		this.BloodCamera.SetActive(true);
		this.Knife.SetActive(true);
		this.CuteMusic.volume = 0f;
		this.DarkMusic.volume = 1f;
		RenderSettings.ambientLight = new Color(0.5f, 0.5f, 0.5f, 1f);
		RenderSettings.skybox = this.DarkSkybox;
		RenderSettings.fog = true;
		foreach (UISprite uisprite in this.MediumSprites)
		{
			uisprite.color = new Color(1f, 0f, 0f, uisprite.color.a);
		}
		foreach (UISprite uisprite2 in this.LightSprites)
		{
			uisprite2.color = new Color(0f, 0f, 0f, uisprite2.color.a);
		}
		foreach (UISprite uisprite3 in this.DarkSprites)
		{
			uisprite3.color = new Color(0f, 0f, 0f, uisprite3.color.a);
		}
		foreach (UILabel uilabel in this.ColoredLabels)
		{
			uilabel.color = new Color(0f, 0f, 0f, uilabel.color.a);
		}
		this.SimulatorLabel.color = new Color(1f, 0f, 0f, 1f);
	}

	// Token: 0x06001C3A RID: 7226 RVA: 0x001514E4 File Offset: 0x0014F6E4
	private void TurnCute()
	{
		GameObjectUtils.SetLayerRecursively(this.Yandere.transform.parent.gameObject, 9);
		Animation component = this.Yandere.GetComponent<Animation>();
		component["f02_yanderePose_00"].weight = 0f;
		component["f02_fist_00"].weight = 0f;
		component.Stop("f02_fist_00");
		Renderer renderer = this.YandereEye[0];
		renderer.material.color = new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, 0f);
		Renderer renderer2 = this.YandereEye[1];
		renderer2.material.color = new Color(renderer2.material.color.r, renderer2.material.color.g, renderer2.material.color.b, 0f);
		this.ColorCorrection.enabled = false;
		this.BloodProjector.SetActive(false);
		this.BloodCamera.SetActive(false);
		this.Knife.SetActive(false);
		this.CuteMusic.volume = 1f;
		this.DarkMusic.volume = 0f;
		this.Timer = 0f;
		RenderSettings.ambientLight = new Color(0.75f, 0.75f, 0.75f, 1f);
		RenderSettings.skybox = this.CuteSkybox;
		RenderSettings.fog = false;
		foreach (UISprite uisprite in this.MediumSprites)
		{
			uisprite.color = new Color(this.MediumColor.r, this.MediumColor.g, this.MediumColor.b, uisprite.color.a);
		}
		foreach (UISprite uisprite2 in this.LightSprites)
		{
			uisprite2.color = new Color(this.LightColor.r, this.LightColor.g, this.LightColor.b, uisprite2.color.a);
		}
		foreach (UISprite uisprite3 in this.DarkSprites)
		{
			uisprite3.color = new Color(this.DarkColor.r, this.DarkColor.g, this.DarkColor.b, uisprite3.color.a);
		}
		foreach (UILabel uilabel in this.ColoredLabels)
		{
			uilabel.color = new Color(1f, 1f, 1f, uilabel.color.a);
		}
		this.SimulatorLabel.color = this.MediumColor;
	}

	// Token: 0x06001C3B RID: 7227 RVA: 0x001517C0 File Offset: 0x0014F9C0
	private void TurnLoveSick()
	{
		RenderSettings.ambientLight = new Color(0.25f, 0.25f, 0.25f, 1f);
		this.CuteMusic.volume = 0f;
		this.DarkMusic.volume = 0f;
		this.LoveSickMusic.volume = 1f;
		foreach (UISprite uisprite in this.MediumSprites)
		{
			uisprite.color = new Color(0f, 0f, 0f, uisprite.color.a);
		}
		foreach (UISprite uisprite2 in this.LightSprites)
		{
			uisprite2.color = new Color(1f, 0f, 0f, uisprite2.color.a);
		}
		foreach (UISprite uisprite3 in this.DarkSprites)
		{
			uisprite3.color = new Color(1f, 0f, 0f, uisprite3.color.a);
		}
		foreach (UILabel uilabel in this.ColoredLabels)
		{
			uilabel.color = new Color(1f, 0f, 0f, uilabel.color.a);
		}
		this.LoveSickLogo.SetActive(true);
		this.Logo.SetActive(false);
	}

	// Token: 0x040034B8 RID: 13496
	public ColorCorrectionCurves ColorCorrection;

	// Token: 0x040034B9 RID: 13497
	public InputManagerScript InputManager;

	// Token: 0x040034BA RID: 13498
	public TitleSaveFilesScript SaveFiles;

	// Token: 0x040034BB RID: 13499
	public SelectiveGrayscale Grayscale;

	// Token: 0x040034BC RID: 13500
	public TitleSponsorScript Sponsors;

	// Token: 0x040034BD RID: 13501
	public TitleExtrasScript Extras;

	// Token: 0x040034BE RID: 13502
	public PromptBarScript PromptBar;

	// Token: 0x040034BF RID: 13503
	public SSAOEffect SSAO;

	// Token: 0x040034C0 RID: 13504
	public JsonScript JSON;

	// Token: 0x040034C1 RID: 13505
	public UISprite[] MediumSprites;

	// Token: 0x040034C2 RID: 13506
	public UISprite[] LightSprites;

	// Token: 0x040034C3 RID: 13507
	public UISprite[] DarkSprites;

	// Token: 0x040034C4 RID: 13508
	public UILabel TitleLabel;

	// Token: 0x040034C5 RID: 13509
	public UILabel SimulatorLabel;

	// Token: 0x040034C6 RID: 13510
	public UILabel[] ColoredLabels;

	// Token: 0x040034C7 RID: 13511
	public Color MediumColor;

	// Token: 0x040034C8 RID: 13512
	public Color LightColor;

	// Token: 0x040034C9 RID: 13513
	public Color DarkColor;

	// Token: 0x040034CA RID: 13514
	public Transform VictimHead;

	// Token: 0x040034CB RID: 13515
	public Transform RightHand;

	// Token: 0x040034CC RID: 13516
	public Transform TwintailL;

	// Token: 0x040034CD RID: 13517
	public Transform TwintailR;

	// Token: 0x040034CE RID: 13518
	public Animation LoveSickYandere;

	// Token: 0x040034CF RID: 13519
	public GameObject BloodProjector;

	// Token: 0x040034D0 RID: 13520
	public GameObject LoveSickLogo;

	// Token: 0x040034D1 RID: 13521
	public GameObject BloodCamera;

	// Token: 0x040034D2 RID: 13522
	public GameObject Yandere;

	// Token: 0x040034D3 RID: 13523
	public GameObject Knife;

	// Token: 0x040034D4 RID: 13524
	public GameObject Logo;

	// Token: 0x040034D5 RID: 13525
	public GameObject Sun;

	// Token: 0x040034D6 RID: 13526
	public AudioSource LoveSickMusic;

	// Token: 0x040034D7 RID: 13527
	public AudioSource CuteMusic;

	// Token: 0x040034D8 RID: 13528
	public AudioSource DarkMusic;

	// Token: 0x040034D9 RID: 13529
	public Renderer[] YandereEye;

	// Token: 0x040034DA RID: 13530
	public Material CuteSkybox;

	// Token: 0x040034DB RID: 13531
	public Material DarkSkybox;

	// Token: 0x040034DC RID: 13532
	public Transform Highlight;

	// Token: 0x040034DD RID: 13533
	public Transform[] Spine;

	// Token: 0x040034DE RID: 13534
	public Transform[] Arm;

	// Token: 0x040034DF RID: 13535
	public UISprite Darkness;

	// Token: 0x040034E0 RID: 13536
	public Vector3 PermaPositionL;

	// Token: 0x040034E1 RID: 13537
	public Vector3 PermaPositionR;

	// Token: 0x040034E2 RID: 13538
	public bool NeverChange;

	// Token: 0x040034E3 RID: 13539
	public bool LoveSick;

	// Token: 0x040034E4 RID: 13540
	public bool FadeOut;

	// Token: 0x040034E5 RID: 13541
	public bool Turning;

	// Token: 0x040034E6 RID: 13542
	public bool Fading = true;

	// Token: 0x040034E7 RID: 13543
	public int SelectionCount = 8;

	// Token: 0x040034E8 RID: 13544
	public int Selected;

	// Token: 0x040034E9 RID: 13545
	public float InputTimer;

	// Token: 0x040034EA RID: 13546
	public float FadeSpeed = 1f;

	// Token: 0x040034EB RID: 13547
	public float LateTimer;

	// Token: 0x040034EC RID: 13548
	public float RotationY;

	// Token: 0x040034ED RID: 13549
	public float RotationZ;

	// Token: 0x040034EE RID: 13550
	public float Volume;

	// Token: 0x040034EF RID: 13551
	public float Timer;
}
