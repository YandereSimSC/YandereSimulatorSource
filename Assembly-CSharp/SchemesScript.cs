using System;
using UnityEngine;

// Token: 0x020003C8 RID: 968
public class SchemesScript : MonoBehaviour
{
	// Token: 0x06001A40 RID: 6720 RVA: 0x00100E58 File Offset: 0x000FF058
	private void Start()
	{
		for (int i = 1; i < this.SchemeNames.Length; i++)
		{
			if (!SchemeGlobals.GetSchemeStatus(i))
			{
				this.SchemeDeadlineLabels[i].text = this.SchemeDeadlines[i];
				this.SchemeNameLabels[i].text = this.SchemeNames[i];
			}
		}
		this.SchemeNameLabels[1].color = new Color(0f, 0f, 0f, 0.5f);
		this.SchemeNameLabels[2].color = new Color(0f, 0f, 0f, 0.5f);
		this.SchemeNameLabels[3].color = new Color(0f, 0f, 0f, 0.5f);
		this.SchemeNameLabels[4].color = new Color(0f, 0f, 0f, 0.5f);
		this.SchemeNameLabels[5].color = new Color(0f, 0f, 0f, 0.5f);
		if (DateGlobals.Weekday == DayOfWeek.Monday)
		{
			this.SchemeNameLabels[1].color = new Color(0f, 0f, 0f, 1f);
		}
		if (DateGlobals.Weekday == DayOfWeek.Tuesday)
		{
			this.SchemeNameLabels[2].color = new Color(0f, 0f, 0f, 1f);
		}
		if (DateGlobals.Weekday == DayOfWeek.Wednesday)
		{
			this.SchemeNameLabels[3].color = new Color(0f, 0f, 0f, 1f);
		}
		if (DateGlobals.Weekday == DayOfWeek.Thursday)
		{
			this.SchemeNameLabels[4].color = new Color(0f, 0f, 0f, 1f);
		}
		if (DateGlobals.Weekday == DayOfWeek.Friday)
		{
			this.SchemeNameLabels[5].color = new Color(0f, 0f, 0f, 1f);
		}
	}

	// Token: 0x06001A41 RID: 6721 RVA: 0x00101050 File Offset: 0x000FF250
	private void Update()
	{
		if (this.InputManager.TappedUp)
		{
			this.ID--;
			if (this.ID < 1)
			{
				this.ID = this.SchemeNames.Length - 1;
			}
			this.UpdateSchemeInfo();
		}
		if (this.InputManager.TappedDown)
		{
			this.ID++;
			if (this.ID > this.SchemeNames.Length - 1)
			{
				this.ID = 1;
			}
			this.UpdateSchemeInfo();
		}
		if (Input.GetButtonDown("A"))
		{
			AudioSource component = base.GetComponent<AudioSource>();
			if (this.PromptBar.Label[0].text != string.Empty)
			{
				if (this.SchemeNameLabels[this.ID].color.a == 1f)
				{
					this.SchemeManager.enabled = true;
					if (this.ID == 5)
					{
						this.SchemeManager.ClockCheck = true;
					}
					if (!SchemeGlobals.GetSchemeUnlocked(this.ID))
					{
						if (this.Inventory.PantyShots >= this.SchemeCosts[this.ID])
						{
							this.Inventory.PantyShots -= this.SchemeCosts[this.ID];
							SchemeGlobals.SetSchemeUnlocked(this.ID, true);
							SchemeGlobals.CurrentScheme = this.ID;
							if (SchemeGlobals.GetSchemeStage(this.ID) == 0)
							{
								SchemeGlobals.SetSchemeStage(this.ID, 1);
							}
							this.UpdateSchemeDestinations();
							this.UpdateInstructions();
							this.UpdateSchemeList();
							this.UpdateSchemeInfo();
							component.clip = this.InfoPurchase;
							component.Play();
						}
					}
					else
					{
						if (SchemeGlobals.CurrentScheme == this.ID)
						{
							SchemeGlobals.CurrentScheme = 0;
							this.SchemeManager.enabled = false;
						}
						else
						{
							SchemeGlobals.CurrentScheme = this.ID;
						}
						this.UpdateSchemeDestinations();
						this.UpdateInstructions();
						this.UpdateSchemeInfo();
					}
				}
			}
			else if (SchemeGlobals.GetSchemeStage(this.ID) != 100 && this.Inventory.PantyShots < this.SchemeCosts[this.ID])
			{
				component.clip = this.InfoAfford;
				component.Play();
			}
		}
		if (Input.GetButtonDown("B"))
		{
			this.PromptBar.ClearButtons();
			this.PromptBar.Label[0].text = "Accept";
			this.PromptBar.Label[1].text = "Exit";
			this.PromptBar.Label[5].text = "Choose";
			this.PromptBar.UpdateButtons();
			this.FavorMenu.SetActive(true);
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x06001A42 RID: 6722 RVA: 0x001012EC File Offset: 0x000FF4EC
	public void UpdateSchemeList()
	{
		for (int i = 1; i < this.SchemeNames.Length; i++)
		{
			if (SchemeGlobals.GetSchemeStage(i) == 100)
			{
				UILabel uilabel = this.SchemeNameLabels[i];
				uilabel.color = new Color(uilabel.color.r, uilabel.color.g, uilabel.color.b, 0.5f);
				this.Exclamations[i].enabled = false;
				this.SchemeCostLabels[i].text = string.Empty;
			}
			else
			{
				this.SchemeCostLabels[i].text = ((!SchemeGlobals.GetSchemeUnlocked(i)) ? this.SchemeCosts[i].ToString() : string.Empty);
				if (SchemeGlobals.GetSchemeStage(i) > SchemeGlobals.GetSchemePreviousStage(i))
				{
					SchemeGlobals.SetSchemePreviousStage(i, SchemeGlobals.GetSchemeStage(i));
					this.Exclamations[i].enabled = true;
				}
				else
				{
					this.Exclamations[i].enabled = false;
				}
			}
		}
	}

	// Token: 0x06001A43 RID: 6723 RVA: 0x001013E0 File Offset: 0x000FF5E0
	public void UpdateSchemeInfo()
	{
		if (SchemeGlobals.GetSchemeStage(this.ID) != 100)
		{
			if (!SchemeGlobals.GetSchemeUnlocked(this.ID))
			{
				this.Arrow.gameObject.SetActive(false);
				this.PromptBar.Label[0].text = ((this.Inventory.PantyShots >= this.SchemeCosts[this.ID]) ? "Purchase" : string.Empty);
				this.PromptBar.UpdateButtons();
			}
			else if (SchemeGlobals.CurrentScheme == this.ID)
			{
				this.Arrow.gameObject.SetActive(true);
				this.Arrow.localPosition = new Vector3(this.Arrow.localPosition.x, -10f - 20f * (float)SchemeGlobals.GetSchemeStage(this.ID), this.Arrow.localPosition.z);
				this.PromptBar.Label[0].text = "Stop Tracking";
				this.PromptBar.UpdateButtons();
			}
			else
			{
				this.Arrow.gameObject.SetActive(false);
				this.PromptBar.Label[0].text = "Start Tracking";
				this.PromptBar.UpdateButtons();
			}
		}
		else
		{
			this.PromptBar.Label[0].text = string.Empty;
			this.PromptBar.UpdateButtons();
		}
		this.Highlight.localPosition = new Vector3(this.Highlight.localPosition.x, 200f - 25f * (float)this.ID, this.Highlight.localPosition.z);
		this.SchemeIcon.mainTexture = this.SchemeIcons[this.ID];
		this.SchemeDesc.text = this.SchemeDescs[this.ID];
		if (SchemeGlobals.GetSchemeStage(this.ID) == 100)
		{
			this.SchemeInstructions.text = "This scheme is no longer available.";
		}
		else
		{
			this.SchemeInstructions.text = ((!SchemeGlobals.GetSchemeUnlocked(this.ID)) ? ("Skills Required:\n" + this.SchemeSkills[this.ID]) : this.SchemeSteps[this.ID]);
		}
		this.UpdatePantyCount();
	}

	// Token: 0x06001A44 RID: 6724 RVA: 0x0010161E File Offset: 0x000FF81E
	public void UpdatePantyCount()
	{
		this.PantyCount.text = this.Inventory.PantyShots.ToString();
	}

	// Token: 0x06001A45 RID: 6725 RVA: 0x0010163C File Offset: 0x000FF83C
	public void UpdateInstructions()
	{
		this.Steps = this.SchemeSteps[SchemeGlobals.CurrentScheme].Split(new char[]
		{
			'\n'
		});
		if (SchemeGlobals.CurrentScheme <= 0)
		{
			this.HUDIcon.SetActive(false);
			this.HUDInstructions.text = string.Empty;
			return;
		}
		if (SchemeGlobals.GetSchemeStage(SchemeGlobals.CurrentScheme) < 100)
		{
			this.HUDIcon.SetActive(true);
			this.HUDInstructions.text = this.Steps[SchemeGlobals.GetSchemeStage(SchemeGlobals.CurrentScheme) - 1].ToString();
			return;
		}
		this.Arrow.gameObject.SetActive(false);
		this.HUDIcon.gameObject.SetActive(false);
		this.HUDInstructions.text = string.Empty;
		SchemeGlobals.CurrentScheme = 0;
	}

	// Token: 0x06001A46 RID: 6726 RVA: 0x00101708 File Offset: 0x000FF908
	public void UpdateSchemeDestinations()
	{
		if (this.StudentManager.Students[this.StudentManager.RivalID] != null)
		{
			this.Scheme1Destinations[3] = this.StudentManager.Students[this.StudentManager.RivalID].transform;
			this.Scheme1Destinations[7] = this.StudentManager.Students[this.StudentManager.RivalID].transform;
			this.Scheme4Destinations[5] = this.StudentManager.Students[this.StudentManager.RivalID].transform;
			this.Scheme4Destinations[6] = this.StudentManager.Students[this.StudentManager.RivalID].transform;
		}
		if (this.StudentManager.Students[2] != null)
		{
			this.Scheme2Destinations[1] = this.StudentManager.Students[2].transform;
		}
		if (this.StudentManager.Students[97] != null)
		{
			this.Scheme5Destinations[3] = this.StudentManager.Students[97].transform;
		}
		if (SchemeGlobals.CurrentScheme == 1)
		{
			this.SchemeDestinations = this.Scheme1Destinations;
			return;
		}
		if (SchemeGlobals.CurrentScheme == 2)
		{
			this.SchemeDestinations = this.Scheme2Destinations;
			return;
		}
		if (SchemeGlobals.CurrentScheme == 3)
		{
			this.SchemeDestinations = this.Scheme3Destinations;
			return;
		}
		if (SchemeGlobals.CurrentScheme == 4)
		{
			this.SchemeDestinations = this.Scheme4Destinations;
			return;
		}
		if (SchemeGlobals.CurrentScheme == 5)
		{
			this.SchemeDestinations = this.Scheme5Destinations;
		}
	}

	// Token: 0x0400297F RID: 10623
	public StudentManagerScript StudentManager;

	// Token: 0x04002980 RID: 10624
	public SchemeManagerScript SchemeManager;

	// Token: 0x04002981 RID: 10625
	public InputManagerScript InputManager;

	// Token: 0x04002982 RID: 10626
	public InventoryScript Inventory;

	// Token: 0x04002983 RID: 10627
	public PromptBarScript PromptBar;

	// Token: 0x04002984 RID: 10628
	public GameObject FavorMenu;

	// Token: 0x04002985 RID: 10629
	public Transform Highlight;

	// Token: 0x04002986 RID: 10630
	public Transform Arrow;

	// Token: 0x04002987 RID: 10631
	public UILabel SchemeInstructions;

	// Token: 0x04002988 RID: 10632
	public UITexture SchemeIcon;

	// Token: 0x04002989 RID: 10633
	public UILabel PantyCount;

	// Token: 0x0400298A RID: 10634
	public UILabel SchemeDesc;

	// Token: 0x0400298B RID: 10635
	public UILabel[] SchemeDeadlineLabels;

	// Token: 0x0400298C RID: 10636
	public UILabel[] SchemeCostLabels;

	// Token: 0x0400298D RID: 10637
	public UILabel[] SchemeNameLabels;

	// Token: 0x0400298E RID: 10638
	public UISprite[] Exclamations;

	// Token: 0x0400298F RID: 10639
	public Texture[] SchemeIcons;

	// Token: 0x04002990 RID: 10640
	public int[] SchemeCosts;

	// Token: 0x04002991 RID: 10641
	public Transform[] SchemeDestinations;

	// Token: 0x04002992 RID: 10642
	public string[] SchemeDeadlines;

	// Token: 0x04002993 RID: 10643
	public string[] SchemeSkills;

	// Token: 0x04002994 RID: 10644
	public string[] SchemeDescs;

	// Token: 0x04002995 RID: 10645
	public string[] SchemeNames;

	// Token: 0x04002996 RID: 10646
	[Multiline]
	[SerializeField]
	public string[] SchemeSteps;

	// Token: 0x04002997 RID: 10647
	public int ID = 1;

	// Token: 0x04002998 RID: 10648
	public string[] Steps;

	// Token: 0x04002999 RID: 10649
	public AudioClip InfoPurchase;

	// Token: 0x0400299A RID: 10650
	public AudioClip InfoAfford;

	// Token: 0x0400299B RID: 10651
	public Transform[] Scheme1Destinations;

	// Token: 0x0400299C RID: 10652
	public Transform[] Scheme2Destinations;

	// Token: 0x0400299D RID: 10653
	public Transform[] Scheme3Destinations;

	// Token: 0x0400299E RID: 10654
	public Transform[] Scheme4Destinations;

	// Token: 0x0400299F RID: 10655
	public Transform[] Scheme5Destinations;

	// Token: 0x040029A0 RID: 10656
	public GameObject HUDIcon;

	// Token: 0x040029A1 RID: 10657
	public UILabel HUDInstructions;
}
