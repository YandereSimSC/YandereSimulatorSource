using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000378 RID: 888
public class PracticeWindowScript : MonoBehaviour
{
	// Token: 0x06001937 RID: 6455 RVA: 0x000F1032 File Offset: 0x000EF232
	private void Start()
	{
		this.Window.SetActive(false);
	}

	// Token: 0x06001938 RID: 6456 RVA: 0x000F1040 File Offset: 0x000EF240
	private void Update()
	{
		if (this.Window.activeInHierarchy)
		{
			if (this.InputManager.TappedUp)
			{
				this.Selected--;
				this.UpdateHighlight();
			}
			else if (this.InputManager.TappedDown)
			{
				this.Selected++;
				this.UpdateHighlight();
			}
			if (this.ButtonUp)
			{
				if (Input.GetButtonDown("A"))
				{
					this.UpdateWindow();
					if (this.Texture[this.Selected].color.r == 1f)
					{
						this.Yandere.TargetStudent.Interaction = StudentInteractionType.ClubPractice;
						this.Yandere.TargetStudent.TalkTimer = 100f;
						this.Yandere.TargetStudent.ClubPhase = 2;
						if (this.Club == ClubType.MartialArts)
						{
							this.StudentManager.Students[this.ClubID - this.Selected].Distracted = true;
						}
						this.PromptBar.ClearButtons();
						this.PromptBar.Show = false;
						this.Window.SetActive(false);
						this.ButtonUp = false;
					}
				}
				else if (Input.GetButtonDown("B"))
				{
					this.Yandere.TargetStudent.Interaction = StudentInteractionType.ClubPractice;
					this.Yandere.TargetStudent.TalkTimer = 100f;
					this.Yandere.TargetStudent.ClubPhase = 3;
					this.PromptBar.ClearButtons();
					this.PromptBar.Show = false;
					this.Window.SetActive(false);
					this.ButtonUp = false;
				}
			}
			else if (Input.GetButtonUp("A"))
			{
				this.ButtonUp = true;
			}
		}
		if (this.FadeOut)
		{
			this.Darkness.enabled = true;
			this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, Mathf.MoveTowards(this.Darkness.color.a, 1f, Time.deltaTime));
			if (this.Darkness.color.a == 1f)
			{
				if (this.DialogueWheel.ClubLeader)
				{
					this.DialogueWheel.End();
				}
				if (this.Club == ClubType.LightMusic)
				{
					if (!this.PlayedRhythmMinigame)
					{
						for (int i = 52; i < 56; i++)
						{
							this.StudentManager.Students[i].transform.position = this.StudentManager.Clubs.List[i].position;
							this.StudentManager.Students[i].EmptyHands();
						}
						Physics.SyncTransforms();
						PlayerPrefs.SetFloat("TempReputation", this.StudentManager.Reputation.Reputation);
						this.PlayedRhythmMinigame = true;
						this.FadeOut = false;
						this.FadeIn = true;
						SceneManager.LoadScene("RhythmMinigameScene", LoadSceneMode.Additive);
						GameObject[] rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
						for (int j = 0; j < rootGameObjects.Length; j++)
						{
							rootGameObjects[j].SetActive(false);
						}
					}
				}
				else if (this.Club == ClubType.MartialArts && this.Yandere.CanMove)
				{
					this.StudentManager.CombatMinigame.Practice = true;
					this.StudentManager.Students[46].CharacterAnimation.CrossFade(this.StudentManager.Students[46].IdleAnim);
					this.StudentManager.Students[46].transform.eulerAngles = new Vector3(0f, 0f, 0f);
					this.StudentManager.Students[46].transform.position = this.KneelSpot[0].position;
					this.StudentManager.Students[46].Pathfinding.canSearch = false;
					this.StudentManager.Students[46].Pathfinding.canMove = false;
					this.StudentManager.Students[46].Distracted = true;
					this.StudentManager.Students[46].enabled = false;
					this.StudentManager.Students[46].Routine = false;
					this.StudentManager.Students[46].Hearts.Stop();
					for (int k = 1; k < 5; k++)
					{
						if (this.StudentManager.Students[46 + k] != null && this.StudentManager.Students[46 + k].Alive)
						{
							this.StudentManager.Students[46 + k].transform.position = this.KneelSpot[k].position;
							this.StudentManager.Students[46 + k].transform.eulerAngles = this.KneelSpot[k].eulerAngles;
							this.StudentManager.Students[46 + k].Pathfinding.canSearch = false;
							this.StudentManager.Students[46 + k].Pathfinding.canMove = false;
							this.StudentManager.Students[46 + k].Distracted = true;
							this.StudentManager.Students[46 + k].enabled = false;
							this.StudentManager.Students[46 + k].Routine = false;
							if (this.StudentManager.Students[46 + k].Male)
							{
								this.StudentManager.Students[46 + k].CharacterAnimation.CrossFade("sit_04");
							}
							else
							{
								this.StudentManager.Students[46 + k].CharacterAnimation.CrossFade("f02_sit_05");
							}
						}
					}
					this.Yandere.transform.eulerAngles = this.SparSpot[1].eulerAngles;
					this.Yandere.transform.position = this.SparSpot[1].position;
					this.Yandere.CanMove = false;
					this.SparringPartner = this.StudentManager.Students[this.ClubID - this.Selected];
					this.SparringPartner.CharacterAnimation.CrossFade(this.SparringPartner.IdleAnim);
					this.SparringPartner.transform.eulerAngles = this.SparSpot[2].eulerAngles;
					this.SparringPartner.transform.position = this.SparSpot[2].position;
					this.SparringPartner.MyWeapon = this.Baton;
					this.SparringPartner.MyWeapon.transform.parent = this.SparringPartner.WeaponBagParent;
					this.SparringPartner.MyWeapon.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
					this.SparringPartner.MyWeapon.transform.localPosition = new Vector3(0f, 0f, 0f);
					this.SparringPartner.MyWeapon.GetComponent<Rigidbody>().useGravity = false;
					this.SparringPartner.MyWeapon.FingerprintID = this.SparringPartner.StudentID;
					this.SparringPartner.MyWeapon.MyCollider.enabled = false;
					Physics.SyncTransforms();
					this.FadeOut = false;
					this.FadeIn = true;
				}
			}
		}
		if (this.FadeIn)
		{
			this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, Mathf.MoveTowards(this.Darkness.color.a, 0f, Time.deltaTime));
			if (this.Darkness.color.a == 0f)
			{
				if (this.Club == ClubType.LightMusic)
				{
					this.Timer += Time.deltaTime;
					if (this.Timer > 1f)
					{
						this.Yandere.SetAnimationLayers();
						this.StudentManager.UpdateAllAnimLayers();
						this.StudentManager.Reputation.PendingRep += PlayerPrefs.GetFloat("TempReputation");
						PlayerPrefs.SetFloat("TempReputation", 0f);
						this.FadeIn = false;
						this.Timer = 0f;
						return;
					}
				}
				else if (this.Club == ClubType.MartialArts)
				{
					this.SparringPartner.Pathfinding.canSearch = false;
					this.SparringPartner.Pathfinding.canMove = false;
					this.Timer += Time.deltaTime;
					if (this.Timer > 1f)
					{
						if (this.Selected == 1)
						{
							this.StudentManager.CombatMinigame.Difficulty = 0.5f;
						}
						else if (this.Selected == 2)
						{
							this.StudentManager.CombatMinigame.Difficulty = 0.75f;
						}
						else if (this.Selected == 3)
						{
							this.StudentManager.CombatMinigame.Difficulty = 1f;
						}
						else if (this.Selected == 4)
						{
							this.StudentManager.CombatMinigame.Difficulty = 1.5f;
						}
						else if (this.Selected == 5)
						{
							this.StudentManager.CombatMinigame.Difficulty = 2f;
						}
						this.StudentManager.Students[this.ClubID - this.Selected].Threatened = true;
						this.StudentManager.Students[this.ClubID - this.Selected].Alarmed = true;
						this.StudentManager.Students[this.ClubID - this.Selected].enabled = true;
						this.FadeIn = false;
						this.Timer = 0f;
					}
				}
			}
		}
	}

	// Token: 0x06001939 RID: 6457 RVA: 0x000F1A0C File Offset: 0x000EFC0C
	public void Finish()
	{
		for (int i = 1; i < 6; i++)
		{
			if (this.StudentManager.Students[45 + i] != null)
			{
				this.StudentManager.Students[45 + i].Pathfinding.canSearch = true;
				this.StudentManager.Students[45 + i].Pathfinding.canMove = true;
				this.StudentManager.Students[45 + i].Distracted = false;
				this.StudentManager.Students[45 + i].enabled = true;
				this.StudentManager.Students[45 + i].Routine = true;
			}
		}
	}

	// Token: 0x0600193A RID: 6458 RVA: 0x000F1ABC File Offset: 0x000EFCBC
	public void UpdateWindow()
	{
		this.PromptBar.ClearButtons();
		this.PromptBar.Label[0].text = "Confirm";
		this.PromptBar.Label[1].text = "Back";
		this.PromptBar.Label[4].text = "Choose";
		this.PromptBar.UpdateButtons();
		this.PromptBar.Show = true;
		if (this.Club == ClubType.LightMusic)
		{
			this.Texture[1].mainTexture = this.AlbumCovers[1];
			this.Texture[2].mainTexture = this.AlbumCovers[2];
			this.Texture[3].mainTexture = this.AlbumCovers[3];
			this.Texture[4].mainTexture = this.AlbumCovers[4];
			this.Texture[5].mainTexture = this.AlbumCovers[5];
			this.Label[1].text = "Panther\n" + this.Difficulties[1];
			this.Label[2].text = "?????\n" + this.Difficulties[2];
			this.Label[3].text = "?????\n" + this.Difficulties[3];
			this.Label[4].text = "?????\n" + this.Difficulties[4];
			this.Label[5].text = "?????\n" + this.Difficulties[5];
			this.Texture[2].color = new Color(0.5f, 0.5f, 0.5f, 1f);
			this.Texture[3].color = new Color(0.5f, 0.5f, 0.5f, 1f);
			this.Texture[4].color = new Color(0.5f, 0.5f, 0.5f, 1f);
			this.Texture[5].color = new Color(0.5f, 0.5f, 0.5f, 1f);
			this.Label[2].color = new Color(0f, 0f, 0f, 0.5f);
			this.Label[3].color = new Color(0f, 0f, 0f, 0.5f);
			this.Label[4].color = new Color(0f, 0f, 0f, 0.5f);
			this.Label[5].color = new Color(0f, 0f, 0f, 0.5f);
		}
		else if (this.Club == ClubType.MartialArts)
		{
			this.ClubID = 51;
			this.ID = 1;
			while (this.ID < 6)
			{
				WWW www = new WWW(string.Concat(new string[]
				{
					"file:///",
					Application.streamingAssetsPath,
					"/Portraits/Student_",
					(this.ClubID - this.ID).ToString(),
					".png"
				}));
				this.Texture[this.ID].mainTexture = www.texture;
				this.Label[this.ID].text = this.StudentManager.JSON.Students[this.ClubID - this.ID].Name + "\n" + this.Difficulties[this.ID];
				if (this.StudentManager.Students[this.ClubID - this.ID] != null)
				{
					if (!this.StudentManager.Students[this.ClubID - this.ID].Routine)
					{
						Debug.Log("A student is not doing their routine.");
						this.Texture[this.ID].color = new Color(0.5f, 0.5f, 0.5f, 1f);
						this.Label[this.ID].color = new Color(0f, 0f, 0f, 0.5f);
					}
					else
					{
						this.Texture[this.ID].color = new Color(1f, 1f, 1f, 1f);
						this.Label[this.ID].color = new Color(0f, 0f, 0f, 1f);
					}
				}
				else
				{
					this.Texture[this.ID].color = new Color(0.5f, 0.5f, 0.5f, 1f);
					this.Label[this.ID].color = new Color(0f, 0f, 0f, 0.5f);
				}
				this.ID++;
			}
			this.Texture[5].color = new Color(1f, 1f, 1f, 1f);
			this.Label[5].color = new Color(0f, 0f, 0f, 1f);
		}
		this.Window.SetActive(true);
		this.UpdateHighlight();
	}

	// Token: 0x0600193B RID: 6459 RVA: 0x000F2014 File Offset: 0x000F0214
	public void UpdateHighlight()
	{
		if (this.Selected < 1)
		{
			this.Selected = 5;
		}
		else if (this.Selected > 5)
		{
			this.Selected = 1;
		}
		this.Highlight.localPosition = new Vector3(0f, (float)(660 - 220 * this.Selected), 0f);
	}

	// Token: 0x04002649 RID: 9801
	public StudentManagerScript StudentManager;

	// Token: 0x0400264A RID: 9802
	public DialogueWheelScript DialogueWheel;

	// Token: 0x0400264B RID: 9803
	public InputManagerScript InputManager;

	// Token: 0x0400264C RID: 9804
	public StudentScript SparringPartner;

	// Token: 0x0400264D RID: 9805
	public PromptBarScript PromptBar;

	// Token: 0x0400264E RID: 9806
	public YandereScript Yandere;

	// Token: 0x0400264F RID: 9807
	public WeaponScript Baton;

	// Token: 0x04002650 RID: 9808
	public Transform[] KneelSpot;

	// Token: 0x04002651 RID: 9809
	public Transform[] SparSpot;

	// Token: 0x04002652 RID: 9810
	public string[] Difficulties;

	// Token: 0x04002653 RID: 9811
	public Texture[] AlbumCovers;

	// Token: 0x04002654 RID: 9812
	public UITexture[] Texture;

	// Token: 0x04002655 RID: 9813
	public UILabel[] Label;

	// Token: 0x04002656 RID: 9814
	public Transform Highlight;

	// Token: 0x04002657 RID: 9815
	public GameObject Window;

	// Token: 0x04002658 RID: 9816
	public UISprite Darkness;

	// Token: 0x04002659 RID: 9817
	public int Selected;

	// Token: 0x0400265A RID: 9818
	public int ClubID;

	// Token: 0x0400265B RID: 9819
	public int ID = 1;

	// Token: 0x0400265C RID: 9820
	public ClubType Club;

	// Token: 0x0400265D RID: 9821
	public bool PlayedRhythmMinigame;

	// Token: 0x0400265E RID: 9822
	public bool ButtonUp;

	// Token: 0x0400265F RID: 9823
	public bool FadeOut;

	// Token: 0x04002660 RID: 9824
	public bool FadeIn;

	// Token: 0x04002661 RID: 9825
	public float Timer;
}
