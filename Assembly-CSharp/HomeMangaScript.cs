using System;
using UnityEngine;

// Token: 0x020002EF RID: 751
public class HomeMangaScript : MonoBehaviour
{
	// Token: 0x06001727 RID: 5927 RVA: 0x000C57C8 File Offset: 0x000C39C8
	private void Start()
	{
		this.UpdateCurrentLabel();
		for (int i = 0; i < this.TotalManga; i++)
		{
			if (CollectibleGlobals.GetMangaCollected(i + 1))
			{
				this.NewManga = UnityEngine.Object.Instantiate<GameObject>(this.MangaModels[i], new Vector3(base.transform.position.x, base.transform.position.y, base.transform.position.z - 1f), Quaternion.identity);
			}
			else
			{
				this.NewManga = UnityEngine.Object.Instantiate<GameObject>(this.MysteryManga, new Vector3(base.transform.position.x, base.transform.position.y, base.transform.position.z - 1f), Quaternion.identity);
			}
			this.NewManga.transform.parent = this.MangaParent;
			this.NewManga.GetComponent<HomeMangaBookScript>().Manga = this;
			this.NewManga.GetComponent<HomeMangaBookScript>().ID = i;
			this.NewManga.transform.localScale = new Vector3(1.45f, 1.45f, 1.45f);
			this.MangaParent.transform.localEulerAngles = new Vector3(this.MangaParent.transform.localEulerAngles.x, this.MangaParent.transform.localEulerAngles.y + 360f / (float)this.TotalManga, this.MangaParent.transform.localEulerAngles.z);
			this.MangaList[i] = this.NewManga;
		}
		this.MangaParent.transform.localEulerAngles = new Vector3(this.MangaParent.transform.localEulerAngles.x, 0f, this.MangaParent.transform.localEulerAngles.z);
		this.MangaParent.transform.localPosition = new Vector3(this.MangaParent.transform.localPosition.x, this.MangaParent.transform.localPosition.y, 1.8f);
		this.UpdateMangaLabels();
		this.MangaParent.transform.localScale = Vector3.zero;
		this.MangaParent.gameObject.SetActive(false);
	}

	// Token: 0x06001728 RID: 5928 RVA: 0x000C5A24 File Offset: 0x000C3C24
	private void Update()
	{
		if (this.HomeWindow.Show)
		{
			if (!this.AreYouSure.activeInHierarchy)
			{
				this.MangaParent.localScale = Vector3.Lerp(this.MangaParent.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10f);
				this.MangaParent.gameObject.SetActive(true);
				if (this.InputManager.TappedRight)
				{
					this.DestinationReached = false;
					this.TargetRotation += 360f / (float)this.TotalManga;
					this.Selected++;
					if (this.Selected > this.TotalManga - 1)
					{
						this.Selected = 0;
					}
					this.UpdateMangaLabels();
					this.UpdateCurrentLabel();
				}
				if (this.InputManager.TappedLeft)
				{
					this.DestinationReached = false;
					this.TargetRotation -= 360f / (float)this.TotalManga;
					this.Selected--;
					if (this.Selected < 0)
					{
						this.Selected = this.TotalManga - 1;
					}
					this.UpdateMangaLabels();
					this.UpdateCurrentLabel();
				}
				this.Rotation = Mathf.Lerp(this.Rotation, this.TargetRotation, Time.deltaTime * 10f);
				this.MangaParent.localEulerAngles = new Vector3(this.MangaParent.localEulerAngles.x, this.Rotation, this.MangaParent.localEulerAngles.z);
				if (Input.GetButtonDown("A") && this.ReadButtonGroup.activeInHierarchy)
				{
					this.MangaGroup.SetActive(false);
					this.AreYouSure.SetActive(true);
				}
				if (Input.GetKeyDown(KeyCode.S))
				{
					PlayerGlobals.Seduction++;
					PlayerGlobals.Numbness++;
					PlayerGlobals.Enlightenment++;
					if (PlayerGlobals.Seduction > 5)
					{
						PlayerGlobals.Seduction = 0;
						PlayerGlobals.Numbness = 0;
						PlayerGlobals.Enlightenment = 0;
					}
					this.UpdateCurrentLabel();
					this.UpdateMangaLabels();
				}
				if (Input.GetButtonDown("B"))
				{
					this.HomeCamera.Destination = this.HomeCamera.Destinations[0];
					this.HomeCamera.Target = this.HomeCamera.Targets[0];
					this.HomeYandere.CanMove = true;
					this.HomeWindow.Show = false;
				}
				if (Input.GetKeyDown(KeyCode.Space))
				{
					for (int i = 0; i < this.TotalManga; i++)
					{
						CollectibleGlobals.SetMangaCollected(i + 1, true);
					}
					return;
				}
			}
			else
			{
				if (Input.GetButtonDown("A"))
				{
					if (this.Selected < 5)
					{
						PlayerGlobals.Seduction++;
					}
					else if (this.Selected < 10)
					{
						PlayerGlobals.Numbness++;
					}
					else
					{
						PlayerGlobals.Enlightenment++;
					}
					this.AreYouSure.SetActive(false);
					this.Darkness.FadeOut = true;
				}
				if (Input.GetButtonDown("B"))
				{
					this.MangaGroup.SetActive(true);
					this.AreYouSure.SetActive(false);
					return;
				}
			}
		}
		else
		{
			this.MangaParent.localScale = Vector3.Lerp(this.MangaParent.localScale, Vector3.zero, Time.deltaTime * 10f);
			if (this.MangaParent.localScale.x < 0.01f)
			{
				this.MangaParent.gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x06001729 RID: 5929 RVA: 0x000C5D88 File Offset: 0x000C3F88
	private void UpdateMangaLabels()
	{
		if (this.Selected < 5)
		{
			this.ReadButtonGroup.SetActive(PlayerGlobals.Seduction == this.Selected);
			if (CollectibleGlobals.GetMangaCollected(this.Selected + 1))
			{
				if (PlayerGlobals.Seduction > this.Selected)
				{
					this.RequiredLabel.text = "You have already read this manga.";
				}
				else
				{
					this.RequiredLabel.text = "Required Seduction Level: " + this.Selected.ToString();
				}
			}
			else
			{
				this.RequiredLabel.text = "You have not yet collected this manga.";
				this.ReadButtonGroup.SetActive(false);
			}
		}
		else if (this.Selected < 10)
		{
			this.ReadButtonGroup.SetActive(PlayerGlobals.Numbness == this.Selected - 5);
			if (CollectibleGlobals.GetMangaCollected(this.Selected + 1))
			{
				if (PlayerGlobals.Numbness > this.Selected - 5)
				{
					this.RequiredLabel.text = "You have already read this manga.";
				}
				else
				{
					this.RequiredLabel.text = "Required Numbness Level: " + (this.Selected - 5).ToString();
				}
			}
			else
			{
				this.RequiredLabel.text = "You have not yet collected this manga.";
				this.ReadButtonGroup.SetActive(false);
			}
		}
		else
		{
			this.ReadButtonGroup.SetActive(PlayerGlobals.Enlightenment == this.Selected - 10);
			if (CollectibleGlobals.GetMangaCollected(this.Selected + 1))
			{
				if (PlayerGlobals.Enlightenment > this.Selected - 10)
				{
					this.RequiredLabel.text = "You have already read this manga.";
				}
				else
				{
					this.RequiredLabel.text = "Required Enlightenment Level: " + (this.Selected - 10).ToString();
				}
			}
			else
			{
				this.RequiredLabel.text = "You have not yet collected this manga.";
				this.ReadButtonGroup.SetActive(false);
			}
		}
		if (CollectibleGlobals.GetMangaCollected(this.Selected + 1))
		{
			this.MangaNameLabel.text = this.MangaNames[this.Selected];
			this.MangaDescLabel.text = this.MangaDescs[this.Selected];
			this.MangaBuffLabel.text = this.MangaBuffs[this.Selected];
			return;
		}
		this.MangaNameLabel.text = "?????";
		this.MangaDescLabel.text = "?????";
		this.MangaBuffLabel.text = "?????";
	}

	// Token: 0x0600172A RID: 5930 RVA: 0x000C5FF0 File Offset: 0x000C41F0
	private void UpdateCurrentLabel()
	{
		if (this.Selected < 5)
		{
			this.Title = HomeMangaScript.SeductionStrings[PlayerGlobals.Seduction];
			this.CurrentLabel.text = string.Concat(new string[]
			{
				"Current Seduction Level: ",
				PlayerGlobals.Seduction.ToString(),
				" (",
				this.Title,
				")"
			});
			return;
		}
		if (this.Selected < 10)
		{
			this.Title = HomeMangaScript.NumbnessStrings[PlayerGlobals.Numbness];
			this.CurrentLabel.text = string.Concat(new string[]
			{
				"Current Numbness Level: ",
				PlayerGlobals.Numbness.ToString(),
				" (",
				this.Title,
				")"
			});
			return;
		}
		this.Title = HomeMangaScript.EnlightenmentStrings[PlayerGlobals.Enlightenment];
		this.CurrentLabel.text = string.Concat(new string[]
		{
			"Current Enlightenment Level: ",
			PlayerGlobals.Enlightenment.ToString(),
			" (",
			this.Title,
			")"
		});
	}

	// Token: 0x04001F8E RID: 8078
	private static readonly string[] SeductionStrings = new string[]
	{
		"Innocent",
		"Flirty",
		"Charming",
		"Sensual",
		"Seductive",
		"Succubus"
	};

	// Token: 0x04001F8F RID: 8079
	private static readonly string[] NumbnessStrings = new string[]
	{
		"Stoic",
		"Somber",
		"Detached",
		"Unemotional",
		"Desensitized",
		"Dead Inside"
	};

	// Token: 0x04001F90 RID: 8080
	private static readonly string[] EnlightenmentStrings = new string[]
	{
		"Asleep",
		"Awoken",
		"Mindful",
		"Informed",
		"Eyes Open",
		"Omniscient"
	};

	// Token: 0x04001F91 RID: 8081
	public InputManagerScript InputManager;

	// Token: 0x04001F92 RID: 8082
	public HomeYandereScript HomeYandere;

	// Token: 0x04001F93 RID: 8083
	public HomeCameraScript HomeCamera;

	// Token: 0x04001F94 RID: 8084
	public HomeWindowScript HomeWindow;

	// Token: 0x04001F95 RID: 8085
	public HomeDarknessScript Darkness;

	// Token: 0x04001F96 RID: 8086
	private GameObject NewManga;

	// Token: 0x04001F97 RID: 8087
	public GameObject ReadButtonGroup;

	// Token: 0x04001F98 RID: 8088
	public GameObject MysteryManga;

	// Token: 0x04001F99 RID: 8089
	public GameObject AreYouSure;

	// Token: 0x04001F9A RID: 8090
	public GameObject MangaGroup;

	// Token: 0x04001F9B RID: 8091
	public GameObject[] MangaList;

	// Token: 0x04001F9C RID: 8092
	public UILabel MangaNameLabel;

	// Token: 0x04001F9D RID: 8093
	public UILabel MangaDescLabel;

	// Token: 0x04001F9E RID: 8094
	public UILabel MangaBuffLabel;

	// Token: 0x04001F9F RID: 8095
	public UILabel RequiredLabel;

	// Token: 0x04001FA0 RID: 8096
	public UILabel CurrentLabel;

	// Token: 0x04001FA1 RID: 8097
	public UILabel ButtonLabel;

	// Token: 0x04001FA2 RID: 8098
	public Transform MangaParent;

	// Token: 0x04001FA3 RID: 8099
	public bool DestinationReached;

	// Token: 0x04001FA4 RID: 8100
	public float TargetRotation;

	// Token: 0x04001FA5 RID: 8101
	public float Rotation;

	// Token: 0x04001FA6 RID: 8102
	public int TotalManga;

	// Token: 0x04001FA7 RID: 8103
	public int Selected;

	// Token: 0x04001FA8 RID: 8104
	public string Title = string.Empty;

	// Token: 0x04001FA9 RID: 8105
	public GameObject[] MangaModels;

	// Token: 0x04001FAA RID: 8106
	public string[] MangaNames;

	// Token: 0x04001FAB RID: 8107
	public string[] MangaDescs;

	// Token: 0x04001FAC RID: 8108
	public string[] MangaBuffs;
}
