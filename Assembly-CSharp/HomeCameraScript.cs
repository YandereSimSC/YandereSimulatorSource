using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020002E5 RID: 741
public class HomeCameraScript : MonoBehaviour
{
	// Token: 0x06001700 RID: 5888 RVA: 0x000C1D5C File Offset: 0x000BFF5C
	public void Start()
	{
		this.Button.color = new Color(this.Button.color.r, this.Button.color.g, this.Button.color.b, 0f);
		this.Focus.position = this.Target.position;
		base.transform.position = this.Destination.position;
		if (HomeGlobals.Night)
		{
			this.CeilingLight.SetActive(true);
			this.SenpaiLight.SetActive(true);
			this.NightLight.SetActive(true);
			this.DayLight.SetActive(false);
			this.Triggers[7].Disable();
			this.BasementJukebox.clip = this.NightBasement;
			this.RoomJukebox.clip = this.NightRoom;
			this.PlayMusic();
			this.PantiesMangaLabel.text = "Read Manga";
		}
		else
		{
			this.BasementJukebox.Play();
			this.RoomJukebox.Play();
			this.ComputerScreen.SetActive(false);
			this.Triggers[2].Disable();
			this.Triggers[3].Disable();
			this.Triggers[5].Disable();
			this.Triggers[9].Disable();
		}
		if (SchoolGlobals.KidnapVictim == 0)
		{
			this.RopeGroup.SetActive(false);
			this.Tripod.SetActive(false);
			this.Victim.SetActive(false);
			this.Triggers[10].Disable();
		}
		else
		{
			int kidnapVictim = SchoolGlobals.KidnapVictim;
			if (StudentGlobals.GetStudentArrested(kidnapVictim) || StudentGlobals.GetStudentDead(kidnapVictim))
			{
				this.RopeGroup.SetActive(false);
				this.Victim.SetActive(false);
				this.Triggers[10].Disable();
			}
		}
		if (GameGlobals.LoveSick)
		{
			this.LoveSickColorSwap();
		}
		Time.timeScale = 1f;
		this.HairLock.material.color = this.SenpaiCosmetic.ColorValue;
		if (SchoolGlobals.SchoolAtmosphere > 1f)
		{
			SchoolGlobals.SchoolAtmosphere = 1f;
		}
	}

	// Token: 0x06001701 RID: 5889 RVA: 0x000C1F70 File Offset: 0x000C0170
	private void LateUpdate()
	{
		if (this.HomeYandere.transform.position.y > -5f)
		{
			Transform transform = this.Destinations[0];
			transform.position = new Vector3(-this.HomeYandere.transform.position.x, transform.position.y, transform.position.z);
		}
		this.Focus.position = Vector3.Lerp(this.Focus.position, this.Target.position, Time.deltaTime * 10f);
		base.transform.position = Vector3.Lerp(base.transform.position, this.Destination.position, Time.deltaTime * 10f);
		base.transform.LookAt(this.Focus.position);
		if (this.ID != 11 && Input.GetButtonDown("A") && this.HomeYandere.CanMove && this.ID != 0)
		{
			this.Destination = this.Destinations[this.ID];
			this.Target = this.Targets[this.ID];
			this.HomeWindows[this.ID].Show = true;
			this.HomeYandere.CanMove = false;
			if (this.ID == 1 || this.ID == 8)
			{
				this.HomeExit.enabled = true;
			}
			else if (this.ID == 2)
			{
				this.HomeSleep.enabled = true;
			}
			else if (this.ID == 3)
			{
				this.HomeInternet.enabled = true;
			}
			else if (this.ID == 4)
			{
				this.CorkboardLabel.SetActive(false);
				this.HomeCorkboard.enabled = true;
				this.LoadingScreen.SetActive(true);
				this.HomeYandere.gameObject.SetActive(false);
			}
			else if (this.ID == 5)
			{
				this.HomeYandere.enabled = false;
				this.Controller.transform.localPosition = new Vector3(0.1245f, 0.032f, 0f);
				this.HomeYandere.transform.position = new Vector3(1f, 0f, 0f);
				this.HomeYandere.transform.eulerAngles = new Vector3(0f, 90f, 0f);
				this.HomeYandere.Character.GetComponent<Animation>().Play("f02_gaming_00");
				this.PromptBar.ClearButtons();
				this.PromptBar.Label[0].text = "Play";
				this.PromptBar.Label[1].text = "Back";
				this.PromptBar.Label[4].text = "Select";
				this.PromptBar.UpdateButtons();
				this.PromptBar.Show = true;
			}
			else if (this.ID == 6)
			{
				this.HomeSenpaiShrine.enabled = true;
				this.HomeYandere.gameObject.SetActive(false);
			}
			else if (this.ID == 7)
			{
				this.HomePantyChanger.enabled = true;
			}
			else if (this.ID == 9)
			{
				this.HomeManga.enabled = true;
			}
			else if (this.ID == 10)
			{
				this.PromptBar.ClearButtons();
				this.PromptBar.Label[0].text = "Accept";
				this.PromptBar.Label[1].text = "Back";
				this.PromptBar.UpdateButtons();
				this.PromptBar.Show = true;
				this.HomePrisoner.UpdateDesc();
				this.HomeYandere.gameObject.SetActive(false);
			}
			else if (this.ID == 12)
			{
				this.HomeAnime.enabled = true;
			}
		}
		if (this.Destination == this.Destinations[0])
		{
			this.Vignette.intensity = ((this.HomeYandere.transform.position.y > -1f) ? Mathf.MoveTowards(this.Vignette.intensity, 1f, Time.deltaTime) : Mathf.MoveTowards(this.Vignette.intensity, 5f, Time.deltaTime * 5f));
			this.Vignette.chromaticAberration = Mathf.MoveTowards(this.Vignette.chromaticAberration, 1f, Time.deltaTime);
			this.Vignette.blur = Mathf.MoveTowards(this.Vignette.blur, 1f, Time.deltaTime);
		}
		else
		{
			this.Vignette.intensity = ((this.HomeYandere.transform.position.y > -1f) ? Mathf.MoveTowards(this.Vignette.intensity, 0f, Time.deltaTime) : Mathf.MoveTowards(this.Vignette.intensity, 0f, Time.deltaTime * 5f));
			this.Vignette.chromaticAberration = Mathf.MoveTowards(this.Vignette.chromaticAberration, 0f, Time.deltaTime);
			this.Vignette.blur = Mathf.MoveTowards(this.Vignette.blur, 0f, Time.deltaTime);
		}
		this.Button.color = new Color(this.Button.color.r, this.Button.color.g, this.Button.color.b, Mathf.MoveTowards(this.Button.color.a, (this.ID > 0 && this.HomeYandere.CanMove) ? 1f : 0f, Time.deltaTime * 10f));
		if (this.HomeDarkness.FadeOut)
		{
			this.BasementJukebox.volume = Mathf.MoveTowards(this.BasementJukebox.volume, 0f, Time.deltaTime);
			this.RoomJukebox.volume = Mathf.MoveTowards(this.RoomJukebox.volume, 0f, Time.deltaTime);
		}
		else if (this.HomeYandere.transform.position.y > -1f)
		{
			this.BasementJukebox.volume = Mathf.MoveTowards(this.BasementJukebox.volume, 0f, Time.deltaTime);
			this.RoomJukebox.volume = Mathf.MoveTowards(this.RoomJukebox.volume, 0.5f, Time.deltaTime);
		}
		else if (!this.Torturing)
		{
			this.BasementJukebox.volume = Mathf.MoveTowards(this.BasementJukebox.volume, 0.5f, Time.deltaTime);
			this.RoomJukebox.volume = Mathf.MoveTowards(this.RoomJukebox.volume, 0f, Time.deltaTime);
		}
		if (Input.GetKeyDown(KeyCode.Y))
		{
			TaskGlobals.SetTaskStatus(38, 1);
		}
		if (Input.GetKeyDown(KeyCode.M))
		{
			this.BasementJukebox.gameObject.SetActive(false);
			this.RoomJukebox.gameObject.SetActive(false);
		}
		if (Input.GetKeyDown(KeyCode.BackQuote))
		{
			HomeGlobals.Night = !HomeGlobals.Night;
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
		if (Input.GetKeyDown(KeyCode.Equals))
		{
			Time.timeScale += 1f;
		}
		if (Input.GetKeyDown(KeyCode.Minus) && Time.timeScale > 1f)
		{
			Time.timeScale -= 1f;
		}
	}

	// Token: 0x06001702 RID: 5890 RVA: 0x000C2711 File Offset: 0x000C0911
	public void PlayMusic()
	{
		if (!YanvaniaGlobals.DraculaDefeated && !HomeGlobals.MiyukiDefeated)
		{
			if (!this.BasementJukebox.isPlaying)
			{
				this.BasementJukebox.Play();
			}
			if (!this.RoomJukebox.isPlaying)
			{
				this.RoomJukebox.Play();
			}
		}
	}

	// Token: 0x06001703 RID: 5891 RVA: 0x000C2754 File Offset: 0x000C0954
	private void LoveSickColorSwap()
	{
		foreach (GameObject gameObject in UnityEngine.Object.FindObjectsOfType<GameObject>())
		{
			if (gameObject.transform.parent != this.PauseScreen && gameObject.transform.parent != this.PromptBarPanel)
			{
				UISprite component = gameObject.GetComponent<UISprite>();
				if (component != null && component.color != Color.black)
				{
					component.color = new Color(1f, 0f, 0f, component.color.a);
				}
				UILabel component2 = gameObject.GetComponent<UILabel>();
				if (component2 != null && component2.color != Color.black)
				{
					component2.color = new Color(1f, 0f, 0f, component2.color.a);
				}
			}
		}
		this.DayLight.GetComponent<Light>().color = new Color(0.5f, 0.5f, 0.5f, 1f);
		this.HomeDarkness.Sprite.color = Color.black;
		this.BasementJukebox.clip = this.HomeLoveSick;
		this.RoomJukebox.clip = this.HomeLoveSick;
		this.LoveSickCamera.SetActive(true);
		this.PlayMusic();
	}

	// Token: 0x04001EF5 RID: 7925
	public HomeWindowScript[] HomeWindows;

	// Token: 0x04001EF6 RID: 7926
	public HomeTriggerScript[] Triggers;

	// Token: 0x04001EF7 RID: 7927
	public HomePantyChangerScript HomePantyChanger;

	// Token: 0x04001EF8 RID: 7928
	public HomeSenpaiShrineScript HomeSenpaiShrine;

	// Token: 0x04001EF9 RID: 7929
	public HomeVideoGamesScript HomeVideoGames;

	// Token: 0x04001EFA RID: 7930
	public HomeCorkboardScript HomeCorkboard;

	// Token: 0x04001EFB RID: 7931
	public HomeDarknessScript HomeDarkness;

	// Token: 0x04001EFC RID: 7932
	public HomeInternetScript HomeInternet;

	// Token: 0x04001EFD RID: 7933
	public HomePrisonerScript HomePrisoner;

	// Token: 0x04001EFE RID: 7934
	public HomeYandereScript HomeYandere;

	// Token: 0x04001EFF RID: 7935
	public HomeSleepScript HomeAnime;

	// Token: 0x04001F00 RID: 7936
	public HomeMangaScript HomeManga;

	// Token: 0x04001F01 RID: 7937
	public HomeSleepScript HomeSleep;

	// Token: 0x04001F02 RID: 7938
	public HomeExitScript HomeExit;

	// Token: 0x04001F03 RID: 7939
	public PromptBarScript PromptBar;

	// Token: 0x04001F04 RID: 7940
	public Vignetting Vignette;

	// Token: 0x04001F05 RID: 7941
	public UILabel PantiesMangaLabel;

	// Token: 0x04001F06 RID: 7942
	public UISprite Button;

	// Token: 0x04001F07 RID: 7943
	public GameObject CyberstalkWindow;

	// Token: 0x04001F08 RID: 7944
	public GameObject ComputerScreen;

	// Token: 0x04001F09 RID: 7945
	public GameObject CorkboardLabel;

	// Token: 0x04001F0A RID: 7946
	public GameObject LoveSickCamera;

	// Token: 0x04001F0B RID: 7947
	public GameObject LoadingScreen;

	// Token: 0x04001F0C RID: 7948
	public GameObject CeilingLight;

	// Token: 0x04001F0D RID: 7949
	public GameObject SenpaiLight;

	// Token: 0x04001F0E RID: 7950
	public GameObject Controller;

	// Token: 0x04001F0F RID: 7951
	public GameObject NightLight;

	// Token: 0x04001F10 RID: 7952
	public GameObject RopeGroup;

	// Token: 0x04001F11 RID: 7953
	public GameObject DayLight;

	// Token: 0x04001F12 RID: 7954
	public GameObject Tripod;

	// Token: 0x04001F13 RID: 7955
	public GameObject Victim;

	// Token: 0x04001F14 RID: 7956
	public Transform Destination;

	// Token: 0x04001F15 RID: 7957
	public Transform Target;

	// Token: 0x04001F16 RID: 7958
	public Transform Focus;

	// Token: 0x04001F17 RID: 7959
	public Transform[] Destinations;

	// Token: 0x04001F18 RID: 7960
	public Transform[] Targets;

	// Token: 0x04001F19 RID: 7961
	public int ID;

	// Token: 0x04001F1A RID: 7962
	public AudioSource BasementJukebox;

	// Token: 0x04001F1B RID: 7963
	public AudioSource RoomJukebox;

	// Token: 0x04001F1C RID: 7964
	public AudioClip NightBasement;

	// Token: 0x04001F1D RID: 7965
	public AudioClip NightRoom;

	// Token: 0x04001F1E RID: 7966
	public AudioClip HomeLoveSick;

	// Token: 0x04001F1F RID: 7967
	public bool Torturing;

	// Token: 0x04001F20 RID: 7968
	public CosmeticScript SenpaiCosmetic;

	// Token: 0x04001F21 RID: 7969
	public Renderer HairLock;

	// Token: 0x04001F22 RID: 7970
	public Transform PromptBarPanel;

	// Token: 0x04001F23 RID: 7971
	public Transform PauseScreen;
}
