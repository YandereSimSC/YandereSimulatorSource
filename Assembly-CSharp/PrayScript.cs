using System;
using UnityEngine;

// Token: 0x02000379 RID: 889
public class PrayScript : MonoBehaviour
{
	// Token: 0x0600193D RID: 6461 RVA: 0x000F2080 File Offset: 0x000F0280
	private void Start()
	{
		if (StudentGlobals.GetStudentDead(39))
		{
			this.VictimLabel.color = new Color(this.VictimLabel.color.r, this.VictimLabel.color.g, this.VictimLabel.color.b, 0.5f);
		}
		this.PrayWindow.localScale = Vector3.zero;
		if (MissionModeGlobals.MissionMode || GameGlobals.AlphabetMode)
		{
			this.Disable();
		}
		if (GameGlobals.LoveSick || GameGlobals.AlphabetMode)
		{
			this.Disable();
		}
	}

	// Token: 0x0600193E RID: 6462 RVA: 0x000F2113 File Offset: 0x000F0313
	private void Disable()
	{
		this.GenderPrompt.gameObject.SetActive(false);
		base.enabled = false;
		this.Prompt.enabled = false;
		this.Prompt.Hide();
	}

	// Token: 0x0600193F RID: 6463 RVA: 0x000F2144 File Offset: 0x000F0344
	private void Update()
	{
		if (!this.FemaleVictimChecked)
		{
			if (this.StudentManager.Students[39] != null && !this.StudentManager.Students[39].Alive)
			{
				this.FemaleVictimChecked = true;
				this.Victims++;
			}
		}
		else if (this.StudentManager.Students[39] == null)
		{
			this.FemaleVictimChecked = false;
			this.Victims--;
		}
		if (!this.MaleVictimChecked)
		{
			if (this.StudentManager.Students[37] != null && !this.StudentManager.Students[37].Alive)
			{
				this.MaleVictimChecked = true;
				this.Victims++;
			}
		}
		else if (this.StudentManager.Students[37] == null)
		{
			this.MaleVictimChecked = false;
			this.Victims--;
		}
		if (this.JustSummoned)
		{
			this.StudentManager.UpdateMe(this.StudentID);
			this.JustSummoned = false;
		}
		if (this.GenderPrompt.Circle[0].fillAmount == 0f)
		{
			this.GenderPrompt.Circle[0].fillAmount = 1f;
			if (!this.SpawnMale)
			{
				this.VictimLabel.color = new Color(this.VictimLabel.color.r, this.VictimLabel.color.g, this.VictimLabel.color.b, StudentGlobals.GetStudentDead(37) ? 0.5f : 1f);
				this.GenderPrompt.Label[0].text = "     Male Victim";
				this.SpawnMale = true;
			}
			else
			{
				this.VictimLabel.color = new Color(this.VictimLabel.color.r, this.VictimLabel.color.g, this.VictimLabel.color.b, StudentGlobals.GetStudentDead(39) ? 0.5f : 1f);
				this.GenderPrompt.Label[0].text = "     Female Victim";
				this.SpawnMale = false;
			}
		}
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Circle[0].fillAmount = 1f;
			if (!this.Yandere.Chased && this.Yandere.Chasers == 0)
			{
				this.Yandere.TargetStudent = this.Student;
				this.StudentManager.DisablePrompts();
				this.PrayWindow.gameObject.SetActive(true);
				this.Show = true;
				this.Yandere.ShoulderCamera.OverShoulder = true;
				this.Yandere.WeaponMenu.KeyboardShow = false;
				this.Yandere.Obscurance.enabled = false;
				this.Yandere.WeaponMenu.Show = false;
				this.Yandere.YandereVision = false;
				this.Yandere.CanMove = false;
				this.Yandere.Talking = true;
				this.PromptBar.ClearButtons();
				this.PromptBar.Label[0].text = "Accept";
				this.PromptBar.Label[4].text = "Choose";
				this.PromptBar.UpdateButtons();
				this.PromptBar.Show = true;
				this.StudentNumber = (this.SpawnMale ? 37 : 39);
				if (this.StudentManager.Students[this.StudentNumber] != null)
				{
					if (!this.StudentManager.Students[this.StudentNumber].gameObject.activeInHierarchy)
					{
						this.VictimLabel.color = new Color(this.VictimLabel.color.r, this.VictimLabel.color.g, this.VictimLabel.color.b, 0.5f);
					}
					else
					{
						this.VictimLabel.color = new Color(this.VictimLabel.color.r, this.VictimLabel.color.g, this.VictimLabel.color.b, 1f);
					}
				}
			}
		}
		if (!this.Show)
		{
			if (this.PrayWindow.gameObject.activeInHierarchy)
			{
				if (this.PrayWindow.localScale.x > 0.1f)
				{
					this.PrayWindow.localScale = Vector3.Lerp(this.PrayWindow.localScale, Vector3.zero, Time.deltaTime * 10f);
					return;
				}
				this.PrayWindow.localScale = Vector3.zero;
				this.PrayWindow.gameObject.SetActive(false);
				return;
			}
		}
		else
		{
			this.PrayWindow.localScale = Vector3.Lerp(this.PrayWindow.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10f);
			if (this.InputManager.TappedUp)
			{
				this.Selected--;
				if (this.Selected == 7)
				{
					this.Selected = 6;
				}
				this.UpdateHighlight();
			}
			if (this.InputManager.TappedDown)
			{
				this.Selected++;
				if (this.Selected == 7)
				{
					this.Selected = 8;
				}
				this.UpdateHighlight();
			}
			if (Input.GetButtonDown("A"))
			{
				if (this.Selected == 1)
				{
					if (!this.Yandere.SanityBased)
					{
						this.SanityLabel.text = "Disable Sanity Anims";
						this.Yandere.SanityBased = true;
					}
					else
					{
						this.SanityLabel.text = "Enable Sanity Anims";
						this.Yandere.SanityBased = false;
					}
					this.Exit();
					return;
				}
				if (this.Selected == 2)
				{
					this.Yandere.Sanity -= 50f;
					this.Exit();
					return;
				}
				if (this.Selected == 3)
				{
					if (this.VictimLabel.color.a == 1f && this.StudentManager.NPCsSpawned >= this.StudentManager.NPCsTotal)
					{
						if (this.SpawnMale)
						{
							this.MaleVictimChecked = false;
							this.StudentID = 37;
						}
						else
						{
							this.FemaleVictimChecked = false;
							this.StudentID = 39;
						}
						if (this.StudentManager.Students[this.StudentID] != null)
						{
							UnityEngine.Object.Destroy(this.StudentManager.Students[this.StudentID].gameObject);
						}
						this.StudentManager.Students[this.StudentID] = null;
						this.StudentManager.ForceSpawn = true;
						this.StudentManager.SpawnPositions[this.StudentID] = this.SummonSpot;
						this.StudentManager.SpawnID = this.StudentID;
						this.StudentManager.SpawnStudent(this.StudentManager.SpawnID);
						this.StudentManager.SpawnID = 0;
						this.Police.Corpses -= this.Victims;
						this.Victims = 0;
						this.JustSummoned = true;
						this.Exit();
						return;
					}
				}
				else
				{
					if (this.Selected == 4)
					{
						this.SpawnWeapons();
						this.Exit();
						return;
					}
					if (this.Selected == 5)
					{
						if (this.Yandere.Gloved)
						{
							this.Yandere.Gloves.Blood.enabled = false;
						}
						if (this.Yandere.CurrentUniformOrigin == 1)
						{
							this.StudentManager.OriginalUniforms++;
						}
						else
						{
							this.StudentManager.NewUniforms++;
						}
						this.Police.BloodyClothing = 0;
						this.Yandere.Bloodiness = 0f;
						this.Yandere.Sanity = 100f;
						this.Exit();
						return;
					}
					if (this.Selected == 6)
					{
						this.WeaponManager.CleanWeapons();
						this.Exit();
						return;
					}
					if (this.Selected == 8)
					{
						this.Exit();
					}
				}
			}
		}
	}

	// Token: 0x06001940 RID: 6464 RVA: 0x000F2954 File Offset: 0x000F0B54
	private void UpdateHighlight()
	{
		if (this.Selected < 1)
		{
			this.Selected = 8;
		}
		else if (this.Selected > 8)
		{
			this.Selected = 1;
		}
		this.Highlight.transform.localPosition = new Vector3(this.Highlight.transform.localPosition.x, 225f - 50f * (float)this.Selected, this.Highlight.transform.localPosition.z);
	}

	// Token: 0x06001941 RID: 6465 RVA: 0x000F29D8 File Offset: 0x000F0BD8
	private void Exit()
	{
		this.Selected = 1;
		this.UpdateHighlight();
		this.Yandere.ShoulderCamera.OverShoulder = false;
		this.StudentManager.EnablePrompts();
		this.Yandere.TargetStudent = null;
		this.PromptBar.ClearButtons();
		this.PromptBar.Show = false;
		this.Show = false;
		this.Uses++;
		if (this.Uses > 9)
		{
			this.FemaleTurtle.SetActive(true);
		}
	}

	// Token: 0x06001942 RID: 6466 RVA: 0x000F2A5C File Offset: 0x000F0C5C
	public void SpawnWeapons()
	{
		for (int i = 1; i < 6; i++)
		{
			if (this.Weapon[i] != null)
			{
				this.Weapon[i].transform.position = this.WeaponSpot[i].position;
			}
		}
	}

	// Token: 0x04002662 RID: 9826
	public StudentManagerScript StudentManager;

	// Token: 0x04002663 RID: 9827
	public WeaponManagerScript WeaponManager;

	// Token: 0x04002664 RID: 9828
	public InputManagerScript InputManager;

	// Token: 0x04002665 RID: 9829
	public PromptBarScript PromptBar;

	// Token: 0x04002666 RID: 9830
	public StudentScript Student;

	// Token: 0x04002667 RID: 9831
	public YandereScript Yandere;

	// Token: 0x04002668 RID: 9832
	public PoliceScript Police;

	// Token: 0x04002669 RID: 9833
	public UILabel SanityLabel;

	// Token: 0x0400266A RID: 9834
	public UILabel VictimLabel;

	// Token: 0x0400266B RID: 9835
	public PromptScript GenderPrompt;

	// Token: 0x0400266C RID: 9836
	public PromptScript Prompt;

	// Token: 0x0400266D RID: 9837
	public Transform PrayWindow;

	// Token: 0x0400266E RID: 9838
	public Transform SummonSpot;

	// Token: 0x0400266F RID: 9839
	public Transform Highlight;

	// Token: 0x04002670 RID: 9840
	public Transform[] WeaponSpot;

	// Token: 0x04002671 RID: 9841
	public GameObject[] Weapon;

	// Token: 0x04002672 RID: 9842
	public GameObject FemaleTurtle;

	// Token: 0x04002673 RID: 9843
	public int StudentNumber;

	// Token: 0x04002674 RID: 9844
	public int StudentID;

	// Token: 0x04002675 RID: 9845
	public int Selected;

	// Token: 0x04002676 RID: 9846
	public int Victims;

	// Token: 0x04002677 RID: 9847
	public int Uses;

	// Token: 0x04002678 RID: 9848
	public bool FemaleVictimChecked;

	// Token: 0x04002679 RID: 9849
	public bool MaleVictimChecked;

	// Token: 0x0400267A RID: 9850
	public bool JustSummoned;

	// Token: 0x0400267B RID: 9851
	public bool SpawnMale;

	// Token: 0x0400267C RID: 9852
	public bool Show;
}
